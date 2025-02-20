using System;
using System.Collections.Generic;
using System.IO;

// Recipe class to store details
class Recipe
{
    public string Name { get; set; }
    public string Ingredients { get; set; }
    public string Instructions { get; set; }

    public Recipe(string name, string ingredients, string instructions)
    {
        Name = name;
        Ingredients = ingredients;
        Instructions = instructions;
    }

    public void DisplayRecipe()
    {
        Console.WriteLine("\nRecipe: " + Name);
        Console.WriteLine("Ingredients: " + Ingredients);
        Console.WriteLine("Instructions: " + Instructions);
    }
}

class Program
{
    static List<Recipe> recipes = new List<Recipe>();
    static string filePath = "recipes.txt";

    static void Main()
    {
        LoadRecipes();
        while (true)
        {
            Console.WriteLine("\n1. Add Recipe  2. Search Recipe  3. Display All  4. Delete Recipe  5. Exit");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1": AddRecipe(); break;
                case "2": SearchRecipe(); break;
                case "3": DisplayAllRecipes(); break;
                case "4": DeleteRecipe(); break;
                case "5": SaveRecipes(); return;
                default: Console.WriteLine("Invalid choice, try again."); break;
            }
        }
    }

    static void AddRecipe()
    {
        Console.Write("Enter Recipe Name: ");
        string name = Console.ReadLine();
        Console.Write("Enter Ingredients: ");
        string ingredients = Console.ReadLine();
        Console.Write("Enter Instructions: ");
        string instructions = Console.ReadLine();

        Recipe newRecipe = new Recipe(name, ingredients, instructions);
        recipes.Add(newRecipe);
        Console.WriteLine("Recipe added successfully!");
    }

    static void SearchRecipe()
    {
        Console.Write("Enter Recipe Name to Search: ");
        string name = Console.ReadLine();
        foreach (var recipe in recipes)
        {
            if (recipe.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                recipe.DisplayRecipe();
                return;
            }
        }
        Console.WriteLine("Recipe not found!");
    }

    static void DisplayAllRecipes()
    {
        if (recipes.Count == 0)
        {
            Console.WriteLine("No recipes available.");
            return;
        }
        foreach (var recipe in recipes)
        {
            recipe.DisplayRecipe();
        }
    }

    static void DeleteRecipe()
    {
        Console.Write("Enter Recipe Name to Delete: ");
        string name = Console.ReadLine();
        Recipe recipeToRemove = recipes.Find(r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (recipeToRemove != null)
        {
            recipes.Remove(recipeToRemove);
            Console.WriteLine("Recipe deleted successfully!");
        }
        else
        {
            Console.WriteLine("Recipe not found!");
        }
    }

    static void SaveRecipes()
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            foreach (var recipe in recipes)
            {
                writer.WriteLine(recipe.Name + "|" + recipe.Ingredients + "|" + recipe.Instructions);
            }
        }
    }

    static void LoadRecipes()
    {
        if (!File.Exists(filePath)) return;
        foreach (var line in File.ReadAllLines(filePath))
        {
            var parts = line.Split('|');
            if (parts.Length == 3)
            {
                recipes.Add(new Recipe(parts[0], parts[1], parts[2]));
            }
        }
    }
}

