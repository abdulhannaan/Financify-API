using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Commons
{
    public static class Attachment
    {
        //public static string GetFileExtension(string base64String)
        //{
        //    var data = base64String.Substring(0, 5);

        //    switch (data.ToUpper())
        //    {
        //        case "IVBOR":
        //            return "png";
        //        case "/9J/4":
        //            return "jpg";
        //        case "AAAAF":
        //            return "mp4";
        //        case "JVBER":
        //            return "pdf";
        //        case "AAABA":
        //            return "ico";
        //        case "UMFYI":
        //            return "rar";
        //        case "E1XYD":
        //            return "rtf";
        //        case "U1PKC":
        //            return "txt";
        //        case "MQOWM":
        //        case "77U/M":
        //            return "srt";
        //        default:
        //            return string.Empty;
        //    }
        //}

        //public static string GetUploadPath(string folderName)
        //{
        //    return "Uploads\\" + folderName;
        //}

        //public static bool SaveAttachment(string base64Image, string name, string extension, string folderName)
        //{
        //    try
        //    {
        //        byte[] bytes = Convert.FromBase64String(base64Image);

        //        var path = GetUploadPath(folderName);
        //        string filedir = Path.Combine(Directory.GetCurrentDirectory(), path);

        //        Debug.WriteLine(filedir);
        //        Debug.WriteLine(Directory.Exists(filedir));
        //        if (!Directory.Exists(filedir))//check if the folder exists;
        //            Directory.CreateDirectory(filedir);

        //        var fileName = Encryption.Encrypt(name);
        //        fileName += "." + extension;
        //        string file = Path.Combine(filedir, fileName);

        //        //getting all file
        //        string[] allFiles = Directory.GetFiles(filedir);

        //        //deleting if exist
        //        foreach (string f in allFiles)
        //        {
        //            string fName = f.Substring(filedir.Length + 1);
        //            if (fName == fileName)
        //                File.Delete(f); break;
        //        }

        //        Debug.WriteLine(file);

        //        if (bytes.Length > 0)
        //        {
        //            using (var stream = new FileStream(file, FileMode.Create))
        //            {
        //                stream.Write(bytes, 0, bytes.Length);
        //                stream.Flush();
        //            }
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }

        //}

        //public static string GetImageById(string name, string extension, string folderName)
        //{
        //    String base64ImgString = "";

        //    try
        //    {
        //        var path = GetUploadPath(folderName);
        //        string filedir = Path.Combine(Directory.GetCurrentDirectory(), path);
        //        var fileName = Encryption.Encrypt(name);
        //        string file = Path.Combine(filedir, fileName + "." + extension);

        //        Byte[] bytes = File.ReadAllBytes(file);
        //        base64ImgString = Convert.ToBase64String(bytes);
        //        return base64ImgString;
        //    }
        //    catch (Exception ex)
        //    {
        //        return "";
        //    }
        //}
    }
}
