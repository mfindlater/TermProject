using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace TermProjectLogin
{
    public static class FileUploadExtensions
    {
        public static string Save(this FileUpload fileUpload, HttpServerUtility server, string filename)
        {

            try
            {
#if DEBUG
                fileUpload.SaveAs(Constants.StoragePath + filename);

#else
            fileUpload.SaveAs(server.MapPath("~/Storage/") + filename);
#endif
                return Constants.StorageURL + filename;
            }
            catch(Exception)
            {
                return "http://cis-iis2.temple.edu/Fall2018/CIS3342_tug98770/TermProject/Storage/Error.jpg";
            }
        }

    }
}