using System;
using Microsoft.EntityFrameworkCore;

namespace UploudFilesWithFackExtentions.Models
{
	public class ApplicationDbContext : DbContext { 

      public  DbSet<UploadedFile> UploadedFiles { set; get; }

		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
		{

		}



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}


