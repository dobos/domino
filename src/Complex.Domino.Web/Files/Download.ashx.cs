using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.SessionState;
using Complex.Domino.Lib;

namespace Complex.Domino.Web.Files
{
    /// <summary>
    /// Summary description for Download
    /// </summary>
    public class Download : IHttpHandler, IReadOnlySessionState
    {
        public static string GetUrl(string fileName)
        {
            return String.Format("~/Files/Download.ashx?file={0}", HttpUtility.UrlEncode(fileName.Replace('\\', '/')));
        }

        public void ProcessRequest(HttpContext context)
        {
            var guid = (string)context.Session[Constants.SessionGuid];
            var username = ((Lib.User)context.Session[Constants.SessionUser]).Name;
            var filename = context.Request.QueryString[Constants.RequestFile].Replace('/', '\\');
            var extension = Path.GetExtension(filename);
            var type = FileTypes.GetFileTypeByExtension(extension);

            if (type != null)
            {
                context.Response.ContentType = type.MimeType;
            }
            else
            {
                context.Response.ContentType = System.Net.Mime.MediaTypeNames.Application.Octet;
            }

            var path = Path.Combine(DominoConfiguration.Instance.ScratchPath, guid, filename);

            // Copy file to the output

            var buffer = new byte[0x10000];     // 64k

            using (var infile = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                while (true)
                {
                    var res = infile.Read(buffer, 0, buffer.Length);

                    if (res > 0)
                    {
                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, res);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}