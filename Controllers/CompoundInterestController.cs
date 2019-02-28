using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompoundInterest.Controllers
{
    [Produces("application/json")]
    [Route("api/CompoundInterest")]
    public class CompoundInterestController : Controller
    {
        private IHostingEnvironment _env;

        public CompoundInterestController(IHostingEnvironment env)
        {
            _env = env;

        }

        [HttpGet("/api/CompoundInterest/Calculate/{principalAmount}/{noTimesCompounded}/{noYears}")]
        public CalculatedInterest Calculate(string principalAmount,int noTimesCompounded,int noYears)
        {
            CalculatedInterest toReturn = new CalculatedInterest();
            double mainPrincipalAmount = -1;

            // If we are able to convert the given value to a decimal
            if (Double.TryParse(principalAmount, out mainPrincipalAmount))
            {
                toReturn.PincipalAmount = mainPrincipalAmount;
                toReturn.Rate = GetInterestRate(mainPrincipalAmount);
                toReturn.NumberOfTimesCompounded = noTimesCompounded;
                toReturn.TimeInYear = noYears;
                //Because I have these 4 values that make up the formulae I can use the V = P(1+r/n)(nt)
                toReturn.CalculatedTotal = (toReturn.PincipalAmount * (Math.Pow(1 + (toReturn.Rate / toReturn.NumberOfTimesCompounded), (toReturn.NumberOfTimesCompounded * toReturn.TimeInYear))));

                //We have to multiply Calculated Total by 100 to convert to pence and then round up to the nearest penny
                //We then divide by 100 to get it back into money value
                toReturn.CalculatedTotal = Math.Ceiling(toReturn.CalculatedTotal * 100) / 100;

                //Calculate interest rate by subtracting Principal from CalculateTotal
                toReturn.InterestRate = Math.Ceiling((toReturn.CalculatedTotal - toReturn.PincipalAmount) * 100) / 100;
            }
            else
                toReturn.Warning = "Unable to convert the value given to a currency";

            return toReturn;

        }

        private double GetInterestRate(double mainPrincipalAmount)
        {
            double rate = 0.01;

            if((mainPrincipalAmount>=1000) && (mainPrincipalAmount < 5000))
                rate = 0.015;
            if ((mainPrincipalAmount >= 5000) && (mainPrincipalAmount < 10000))
                rate = 0.02;
            if ((mainPrincipalAmount >= 10000) && (mainPrincipalAmount < 50000))
                rate = 0.025;
            if ((mainPrincipalAmount >= 50000))
                rate = 0.03;


            return rate;
        }
    }
}