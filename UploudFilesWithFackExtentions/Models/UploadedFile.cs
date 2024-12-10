using System;
namespace UploudFilesWithFackExtentions.Models
{
	public class UploadedFile
	{

		public int Id { set; get; }

		public string? FileName { set; get; }

        public string? StoredFileName { set; get; }

	    public  string? ContentType { set; get; }		


        public UploadedFile()
		{

		}
	}
}

