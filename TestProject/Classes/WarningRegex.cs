using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace TestProject.Classes
{
    class WarningRegex
    {
        public string Warning(string warningCurrency)
        {
            if (warningCurrency.Length == 0)
                return String.Empty;
            else
                return warningCurrency.Substring(0, warningCurrency.Length - 1);
        }
    }
}
