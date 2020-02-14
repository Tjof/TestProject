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
            Regex regexSum = new Regex(@"((\d{1,}[\,])|(\d{1,})|(^$))$"); //исправил, но не идеальный вариант регулярки, продолжает вылетать на вводе двух запятых
            //проверяем полученную строку
            MatchCollection matches = regexSum.Matches(s);
            //если строка проходит регулярку возвращаем true
            if (matches.Count > 0)
            {
                return true;
            }
            //если нет - false
            else
            {
                return false;
            }
        }
    }
}
