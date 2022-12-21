using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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
        #region Custom Data Types
        private enum Symbol { WILD = 0, P = 1, B = 2, A = 3 }
        #endregion Custom Data Types

        #region Variables
        private static float stakeAmount = 0.0F;
        private static float currentBalance = 0.0F;

        private const int rowCount = 4;
        private const int columnCount = 3;

        private static List<Tuple<int, Symbol>> thresholds = new List<Tuple<int, Symbol>> {
            Tuple.Create(5, Symbol.WILD),
            Tuple.Create(5 + 15, Symbol.P),
            Tuple.Create(5 + 15 + 35, Symbol.B),
            Tuple.Create(5 + 15 + 35 + 45, Symbol.A),
        };

        private static Dictionary<Symbol, float> coeffs = new Dictionary<Symbol, float>() {
            { Symbol.WILD, 0f },
            { Symbol.P, 0.8f },
            { Symbol.B, 0.6f },
            { Symbol.A, 0.4f },
        };
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

                // For each spin we need to check if the bet amount is valid
                if (currentBalance > stakeAmount)
                {
                    // We create a table of the spin that a player made which contains 4 rows and 3 symbols in each row 
                    List<List<int>> table = GetSpinTable();

                    float coeff = 0.0F;

                    // We print out the spin table with the symbols generated randomly
                    // Calculate the total winning coefficients for the current spin
                    // We print and calculate row wise
                    for (int i = 0; i < rowCount; i++)
                    {
                        //Console.WriteLine(table[i].Select(x => (Symbol)x).ToList().Print().Replace("WILD", "*"));
                        coeff += GetRowCoeff(table[i]);
                    }
                }
            }
        }

        // Method to create the spin table
        private static List<List<int>> GetSpinTable()
        {
            List<List<int>> spinTable = new List<List<int>>();
            Random random = new Random();

            for (int i = 0; i < rowCount; i++)
            {
                List<int> rowValues = new List<int>();
                for (int j = 0; j < columnCount; j++)
                {
                    // Generate a random number from 0 - 99 which will determine our symbols to be displayed
                    int value = random.Next(0, 100);
                    for (int k = 0; k < thresholds.Count; k++)
                    {
                        // We check if the random number generated is smaller that the chance of a symbol appearing in a given position
                        if (thresholds[k].Item1 > value)
                        {
                            // We add the symbol to the a list and break n loop again to find the next symbol
                            rowValues.Add((int)thresholds[k].Item2);
                            break;
                        }
                    }
                }
                // We add all the rows of symbol in another list and return this list back to the main method
                spinTable.Add(rowValues);
            }
            return spinTable;
        }

        // Method to Calcuate the coefficients of each row to calculate the winning amount of a spin
        private static float GetRowCoeff(List<int> list)
        {
            // Removing all WILDs from the list
            list.RemoveAll(x => x == (int)Symbol.WILD);

            // We check the number of unique values in the list
            // This determines the if a row is a winning row or not
            int uniques = list.Distinct().Count();

            // We will check if all symbols in the row are same by checking the unique value count previously
            bool isLineMatch = uniques == 1;

            // We will return 0 if there are not matches else return the total coeff of the matched symbols
            if (!isLineMatch)
                return 0f;
            else
                return coeffs[(Symbol)list[0]] * list.Count;
        }
    }
}
