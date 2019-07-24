using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace OnlineExamGenerator.Models
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
            Lecturer lecturer = HttpContext.Current.Session["USER"] as Lecturer;
            DirectoryInfo directoryInfo = new DirectoryInfo(HostingEnvironment.MapPath("~\\Exams\\" + lecturer.Username));

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