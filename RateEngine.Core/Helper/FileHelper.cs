using System;
using System.Collections.Generic;
using System.Text;

namespace RateEngine.Core.Helper
{
    public static class FileHelper
    {
       

        public static string Read(string location)
        {
            try
            {
                return System.IO.File.ReadAllText(location);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine("Error Reading File : " + ex);
                System.Console.WriteLine("Continue ... ");

                return "";
            }            
        }
    }
}
