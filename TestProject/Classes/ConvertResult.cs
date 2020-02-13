using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject.Classes
{
    public class ConvertResult
    {
        private float currencySum;
        private float result;

        public string Result(string currencyFrom, string currencyTo, Valute valuteFrom, Valute valuteTo)
        {
            if (currencyFrom != String.Empty)
            {
                currencySum = float.Parse(currencyFrom); //вылетает на 2ух "," . Указал в регулярке - не смог пофиксить.
                result = (valuteFrom.Value / valuteFrom.Nominal * currencySum) / (valuteTo.Value / valuteTo.Nominal);
                return result.ToString();
            }
            else
                return String.Empty;
        }
    }
}
