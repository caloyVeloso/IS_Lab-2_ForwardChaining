using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Json;
using Newtonsoft.Json;

class Program
{
    static string rootDirectory = AppDomain.CurrentDomain.BaseDirectory;

    static string factFilename = "Facts.json";
    static string rulesFilename = "Rules.json";

    static List<string> facts = new List<string>(); // Create a list to store facts.
    static List<string> rules = new List<string>(); // Create a list to store rules.

    static List<string> givenRules = new List<string>(); // Create a list to store user-provided rules.

    static void Main()
    {
        facts = getData(factFilename); // Load existing facts from a JSON file.
        givenRules = getData(rulesFilename); // Load existing rules from a JSON file.

        int c11 = 1;
        Console.WriteLine("Facts: ");
        foreach (var f in facts) Console.WriteLine(c11++ + ": " + f); // Display the facts.

        Console.Write("\n");
        int c2 = 1;
        Console.WriteLine("Rules: ");
        foreach (var r in givenRules) Console.WriteLine(c2++ + ": " + r); // Display the rules.
        Console.Write("\n");

        Console.WriteLine("New Added Rules: ");
        Console.WriteLine("\n");

        while (true)
        {
            Console.WriteLine("[1] Add Facts");
            Console.WriteLine("[2] Add Rules");
            Console.WriteLine("[3] Generate new Facts");
            Console.WriteLine("[4] Delete Current Facts");
            Console.WriteLine("[5] Delete Current Rules");
            Console.WriteLine("[6] Exit");
            char n = Console.ReadKey().KeyChar; // Read a character input from the user.

            switch (n)
            {
                case '1': // Add Facts
                    Console.Write("\nAdd Facts: ");
                    string fct = Console.ReadLine().ToLower(); // Read a new fact from the user.
                    if (!facts.Contains(fct))
                        facts.Add(fct); // Add the new fact to the list if it doesn't already exist.
                    else
                        Console.WriteLine("fact already exists"); // Inform the user that the fact already exists.
                    saveData(facts, factFilename); // Save the updated list of facts to a JSON file.
                    foreach (var f in facts) Console.WriteLine(f); // Display the updated list of facts.
                    Console.WriteLine("\n");
                    break;

                case '2': // Add Rules
                    Console.Write("\nAdd Rules: ");
                    givenRules.Add(Console.ReadLine().ToLower()); // Read a new rule from the user and add it to the list.
                    saveData(givenRules, rulesFilename); // Save the updated list of rules to a JSON file.
                    int cnt = 1;
                    foreach (var r in givenRules) Console.WriteLine(cnt++ + ": " + r); // Display the updated list of rules.
                    Console.WriteLine("\n");
                    break;

                case '3': // Generate new Facts
                    checkAllRules(); // Check the rules to generate new facts.
                    int c1 = 1;
                    Console.WriteLine("\nFacts: ");
                    foreach (var f in facts) Console.WriteLine(c1++ + ": " + f); // Display the updated list of facts.
                    saveData(facts, factFilename); // Save the updated list of facts to a JSON file.
                    Console.WriteLine("\n");
                    break;

                case '4': // Delete Facts
                    List<string> fStr = new List<string>();
                    saveData(fStr, factFilename); // Clear the list of facts and save it to a JSON file.
                    facts = getData(factFilename); // Reload the list of rules.
                    foreach (var f in facts) Console.WriteLine(f); // Display the cleared list of facts.
                    Console.WriteLine("\n");
                    break;

                case '5': // Delete Rules
                    List<string> rStr = new List<string>();
                    saveData(rStr, rulesFilename); // Clear the list of rules and save it to a JSON file.
                    givenRules = getData(rulesFilename); // Reload the list of rules.
                    foreach (var r in givenRules) Console.WriteLine(r); // Display the cleared list of rules.
                    Console.WriteLine("\n");
                    break;

                case '6': // Exit
                    Environment.Exit(0); // Exit the application.
                    break;

                default: Console.WriteLine("Invalid Input!"); break; // Handle invalid input.
            }
        }
    }

    // Define a method to check all rules and generate new facts.
    static void checkAllRules()
    {
        bool flag;
        do
        {
            flag = false;
            foreach (var str in givenRules)
            {
                string[] splitStrings = str.Split(", then ");// Split Consequent from Antecedent
                splitStrings[0] = splitStrings[0].Replace("if ", "");// Remove 'if' from Antecedent/s
                string[] antecedents = splitStrings[0].Split(" and ");// Separate all Antecedent

                bool isTrue = true;
                foreach (var a in antecedents)
                {
                    if (!facts.Contains(a))
                        isTrue = false;
                }

                if (isTrue)
                {
                    if (!rules.Contains(str))
                        rules.Add(str); // Add the rule to the list of valid rules.

                    if (!facts.Contains(splitStrings[1]))// Consequent
                    {
                        facts.Add(splitStrings[1]); // Add the new fact to the list of facts.
                        flag = true;
                    }
                }
            }

        } while (flag);
    }

    // Define a method to save a list of strings to a JSON file.
    static void saveData(List<string> str, string filename)
    {
        string json = JsonConvert.SerializeObject(str); // Convert the list to JSON format.
        string filepath = Path.Combine(rootDirectory, filename); // Construct the file path.
        File.WriteAllText(filepath, json); // Write the JSON data to the file.
    }

    // Define a method to load a list of strings from a JSON file.
    static List<string> getData(string filename)
    {
        List<string> listData = new List<string>();
        string filepath = Path.Combine(rootDirectory, filename); // Construct the file path.
        if (File.Exists(filepath))
        {
            string json = File.ReadAllText(filepath); // Read the JSON data from the file.
            listData = JsonConvert.DeserializeObject<List<string>>(json); // Deserialize the JSON data into a list.
        }
        return listData;
    }
}
