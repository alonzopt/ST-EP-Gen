using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public static class NameGenerator
{
    private static string[] numbers =
    {
        "Zero",
        "One",
        "Two",
        "Three",
        "Four",
        "Five",
        "Six",
        "Seven",
        "Eight",
        "Nine",
        "Ten",
        "Eleven",
        "Twelve",
        "Thirteen",
        "Fourteen",
        "Fifteen",
    };

    private static string[] firstNames =
    {
        "Dr.",
        "Mr.",
        "Ms.",
        "Mx.",
        "Joel",
        "Mike",
        "Tom",
        "Crow",
        "Clayton",
        "TV's",
        "Pearl",
        "Professor",
        "Observer",
        "Trace",
        "Kevin",
        "Bridget",
        "Mary Jo",
        "Bill",
        "Marida",
        "Cassandra",
        "Cosmo",
        "Kasha",
        "Bes",
        "Lou",
        "Piper",
        "Sheryl",
        "Karala",
        "Harulu",
        "Gije",
        "Sayla",
        "Artesia",
        "Joliver",
        "Lalah",
        "Casval",
        "Edouard",
        "Judau",
        "Leina",
        "Roux",
        "Kamille",
        "Ellen",
        "Cirocco",
        "Gaby",
        "Calvin",
        "Cayce",
        "Kathryn",
        "Benjamin",
        "Benny",
        "James",
        "Jean-Luc",
        "Kira",
    };

    private static string[] middleNames =
    {
        "A.",
        "B.",
        "C.",
        "D.",
        "E.",
        "F.",
        "G.",
        "H.",
        "I.",
        "J.",
        "K.",
        "L.",
        "M.",
        "N.",
        "O.",
        "P.",
        "Q.",
        "R.",
        "S.",
        "T.",
        "U.",
        "V.",
        "W.",
        "X.",
        "Y.",
        "Z.",
    };

    private static string[] requiresLastName =
    {
        "The",
        "Of",
        "\"Danger\"",
    };

    private static string[] lastNames =
    {
        "Robinson",
        "Hodgson",
        "Nelson",
        "Servo",
        "Robot",
        "Forrester",
        "Frank",
        "Murphy",
        "Conniff",
        "Jones",
        "Pehl",
        "Corbett",
        "Minovsky",
        "Tomino",
        "Yuki",
        "Imhof",
        "Jordan",
        "Piper",
        "Lou",
        "Formosa",
        "Ajiba",
        "Ira",
        "Zaral",
        "Ray",
        "Mass",
        "Bow",
        "Ashta",
        "Bidan",
        "Murasame",
        "Jones",
        "Ripley",
        "Plauget",
        "Greene",
        "Pollard",
        "Janeway",
        "Sisko",
        "Russel",
        "Kirk",
        "Picard",
        "Nerys",
    };

    private static string[] fullFirst = firstNames.Union(numbers).ToArray();
    private static string[] fullMiddle = middleNames.Union(requiresLastName).ToArray();
    private static string[] fullLast = lastNames.Union(numbers).ToArray();

    public static string generate_name()
    {
        string to_return = "";

        // Generate a name and then loop a used name is generated
        // TODO: Adjust various Random.value if needed
        while (to_return == "")
        {
            // Have a chance to keep first name blank
            if(Random.value <= 0.05f)
            {
                to_return = "";
            }
            else
            {
                to_return = fullFirst[Random.Range(0, fullFirst.Length)];
            }

            // Get lastname here because logic changes if it is empty
            string temp_lastname = "";
            if (Random.value <= 0.05f)
            {
                temp_lastname = "";
            }
            else
            {
                temp_lastname = fullLast[Random.Range(0, fullLast.Length)];
            }

            // 50% chance for middleNames
            if (Random.value <= 0.5f)
            {
                // to_return can be empty so only add a space if it isn't
                if(to_return != "")
                {
                    to_return += " ";
                }

                // if temp_lastname isn't empty, use the bigger list of middle names
                if(temp_lastname != "")
                {
                    to_return += fullMiddle[Random.Range(0, middleNames.Length)];
                }
                else
                {
                    to_return += middleNames[Random.Range(0, middleNames.Length)];
                }
            }

            
            // to_return can be empty at this point so only add a space if it isn't and temp_lastname isn't empty either
            if (to_return != "" && temp_lastname != "")
            {
                to_return += " ";
            }
            to_return += temp_lastname;
        }

        return to_return;
    }
}
