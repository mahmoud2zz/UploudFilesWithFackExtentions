using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UploudFilesWithFackExtentions.Models;
using UploudFilesWithFackExtentions.ViweModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UploudFilesWithFackExtentions.Controllers
{
    public class FilesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public FilesController(ApplicationDbContext dbContext,IWebHostEnvironment webHostEnvironment) 
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }





        // GET: /<controller>/
        public IActionResult Index()
        {
            var files = _dbContext.UploadedFiles.ToList();

            return View(files);
        }

        public IActionResult Upload()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult UploadedFile(UploadfilesFormViewModel uploadfilesFormViewModel)
        {
           

            List<UploadedFile> uploadedFiles = new List<UploadedFile>();
           

            foreach (var file in uploadfilesFormViewModel.Files)
            {
                var fakeName = Path.GetRandomFileName();
                UploadedFile uploadedFile = new UploadedFile() {FileName=file.FileName,ContentType=file.ContentType,StoredFileName=fakeName };

                var path = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", fakeName);

                using FileStream fileStream = new FileStream(path,FileMode.Create);

                file.CopyTo(fileStream);

                uploadedFiles.Add(uploadedFile);



            }

            _dbContext.UploadedFiles.AddRange(uploadedFiles);

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));


        }



        [HttpGet]

        public IActionResult Downloadfile(string fileName)
        {

            var uploadFile = _dbContext.UploadedFiles.Single(f => f.StoredFileName == fileName);

            if (uploadFile == null)
            {
                return NotFound();
            }
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", fileName);

            MemoryStream memoryStream = new MemoryStream();

            using FileStream fileStream = new FileStream(path, FileMode.Open);

            fileStream.CopyTo(memoryStream);

            memoryStream.Position = 0;

            return File(memoryStream,uploadFile!.ContentType,uploadFile.FileName);
        }


    }
}

