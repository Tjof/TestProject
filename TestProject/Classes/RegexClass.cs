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
            Regex regexSum = new Regex(@"\d{1,12}(,)*$"); //имеется проблема с 2 и более "," . Решения пока не нашёл.
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
