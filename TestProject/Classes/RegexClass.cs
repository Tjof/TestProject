using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TestProject.Classes
{
    class RegexClass
    {
        public static bool RegexSum(string sum)
        {
            string s = sum;
            Regex regexSum = new Regex(@"((\d{1,}[\,])|(\d{1,})|(^$))$"); //исправил, но не идеальный вариант регулярки.
            MatchCollection matches = regexSum.Matches(s);
            if (matches.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
