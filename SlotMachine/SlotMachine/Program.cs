using System;
using System.Globalization;

//read deposit amount from user
//read stake amount from user for every spin
//create slot table
//create new list of lists (table)
// fill each list/row with 3 values
//assign symbols
//display the table
//calculate winning coeffs for current spin
//calculate win amount
//update balance with the winning amount and display it

namespace SlotMachine
{
    class Program
    {

        #region Variables
        private static float stakeAmount = 0.0F;
        private static float currentBalance = 0.0F;
        #endregion Variables

        static void Main(string[] args)
        {
            // Read Input from user for the initial deposit to start playing
            Console.WriteLine("Please deposit money you would like to play with : ");
            currentBalance = float.Parse(Console.ReadLine(), CultureInfo.InvariantCulture.NumberFormat);

            // Keep Game active till the player has money and he has a valid bet amount
            while (currentBalance > 0.0F && currentBalance > stakeAmount)
            {
                // Read Input from user for the initial deposit to start playing
                Console.WriteLine("Enter Stake Amount : ");
                stakeAmount = float.Parse(Console.ReadLine(), CultureInfo.InvariantCulture.NumberFormat);
            }
        }
    }
}
