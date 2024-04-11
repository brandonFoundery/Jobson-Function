using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Jobson.Enums
{
    public class StringConstants
    {
        public const string UpworkApiKey = "UpworkApiKey";

        public static string GetTrelloBoardName(string name)
        {
            return $"Upwork: {name}";
        }
    }
}
