using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Multilanguage_Example
{
   public class INIClass
    {

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        static extern uint GetPrivateProfileString(
           string lpAppName,
           string lpKeyName,
           string lpDefault,
           StringBuilder lpReturnedString,
           UInt32 nSize,
           string lpFileName);

        public string iniPath { get; set; }
    
        public INIClass(string P)
        {
            iniPath = P;
        }
    
        public string ReadValueBetween(string Sec , string Item)
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder(255);
            UInt32 i = GetPrivateProfileString(Sec, Item, string.Empty, str, 255, iniPath);
            return str.ToString();
        }

    }
}
