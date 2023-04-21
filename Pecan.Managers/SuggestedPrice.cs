using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pecan.Managers
{
    public static class SuggestedPrice
    {
        public static double CalculatePrice(double value)
        {
			try
			{				
					double total = value * 1.50;
					return Math.Round(total, 2);                					
			}
			catch (Exception)
			{

				throw new Exception("No ingreso valores");
			}
        }
    }
}
