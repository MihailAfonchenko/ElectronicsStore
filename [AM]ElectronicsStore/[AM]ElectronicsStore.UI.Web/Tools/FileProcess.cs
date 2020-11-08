using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _AM_ElectronicsStore.UI.Web.Tools
{
    public class FileProcess
    {
        public static string GetPath(string path)
        {
            return "/" + path.Replace("\\", "/");
        }
    }
}