using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using PRP321MVC.Models;


namespace PRP321MVC.Models
{
    public class DownloadFile
    {
        public int lecturerId { get; set; }
        public int fileId { get; set; }
        public string fileName { get; set; }
        public string filePath { get; set; }


        public List<DownloadFile> GetFiles()
        {
            List<DownloadFile> downloadFiles = new List<DownloadFile>();
            LecturerModel lecturer = HttpContext.Current.Session["USER"] as LecturerModel;
            DirectoryInfo directoryInfo = new DirectoryInfo(HostingEnvironment.MapPath("~\\Exams\\"+lecturer.Username));

            int i = 0;

            foreach (FileInfo item in directoryInfo.GetFiles())
            {
                downloadFiles.Add(new DownloadFile()
                {
                    lecturerId = lecturer.LectID,
                    fileId = i + 1,
                    fileName = item.Name,
                    filePath = directoryInfo.FullName + "\\" + item.Name
                });
                i++;
            }

            return downloadFiles;
        }

    }
}