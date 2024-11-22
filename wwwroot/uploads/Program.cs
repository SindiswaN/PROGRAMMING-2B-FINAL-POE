using System;

public class Recipe
{
    //properties of a recipe
    public string Name { get; set; }
    public Ingredient[] Ingredients { get; set; }
    public string[] Steps { get; set; }

    //constructor to initialize a recipe with specified counts of ingredients and steps 
    public Recipe(int ingredientCount, int stepCount)
    {
        Ingredients = new Ingredient[ingredientCount];
        Steps = new string[stepCount];
    }

    //method to add an ingredient to the recipe
    public void AddIngredient(string name, double quantity, string unit)
    {
        for (int i = 0; i < Ingredients.Length; i++)
        {
            if (Ingredients[i] == null)
            {
                Ingredients[i] = new Ingredient(name, quantity, unit);
                break;
            }
        }
    }

    //method to add a step to the recipe
    public void AddStep(string description)
    {
        for (int i = 0; i < Steps.Length; i++)
        {
            if (Steps[i] == null)
            {
                Steps[i] = description;
                break;
            }
        }
    }

    //method to print the recipe
    public void PrintRecipe()
    {
        Console.WriteLine($"Recipe: {Name}\n");
        Console.WriteLine("Ingredients:");
        foreach (var ingredient in Ingredients)
        {
            if (ingredient != null)
            {
                Console.WriteLine($"- {ingredient.Quantity} {ingredient.Unit} {ingredient.Name}");
            }
        }
        Console.WriteLine("\nSteps:");
        int stepNumber = 1;
        foreach (var step in Steps)
        {
            if (step != null)
            {
                Console.WriteLine($"{stepNumber++}. {step}");
            }
        }
    }

    //method to scale the recipe by a factor
    public void ScaleRecipe(double factor)
    {
        foreach (var ingredient in Ingredients)
        {
            if (ingredient != null)
            {
                ingredient.Quantity *= factor;
            }
        }
    }

    //method to reset the quantities of the ingredients
    public void ResetQuantities()
    {
        foreach (var ingredient in Ingredients)
        {
            if (ingredient != null)
            {
                ingredient.Quantity = ingredient.OriginalQuantity;
            }
        }
    }

    //method to clear the recipe
    public void ClearRecipe()
    {
        Name = null;
        Ingredients = new Ingredient[Ingredients.Length];
        Steps = new string[Steps.Length];
    }
}

public class Ingredient
{
    //properties of an ingredient
    public string Name { get; set; }
    public double Quantity { get; set; }
    public string Unit { get; set; }
    public double OriginalQuantity { get; private set; }


    //constructor to initialize an ingredient with name, quantity and unit
    public Ingredient(string name, double quantity, string unit)
    {
        Name = name;
        Quantity = quantity;
        Unit = unit;
        OriginalQuantity = quantity;
    }
}

class Program
{
    static void Main(string[] args)
    {
        bool exit = false;
        Recipe recipe = null;

        while (!exit)
        {
            Console.WriteLine("Recipe Creator\n");
            Console.WriteLine("1. Create New Recipe");
            Console.WriteLine("2. Print Recipe");
            Console.WriteLine("3. Scale Recipe");
            Console.WriteLine("4. Reset Quantities");
            Console.WriteLine("5. Clear Recipe");
            Console.WriteLine("6. Exit\n");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    recipe = CreateNewRecipe();
                    break;
                case "2":
                    PrintRecipe(recipe);
                    break;
                case "3":
                    ScaleRecipe(recipe);
                    break;
                case "4":
                    ResetQuantities(recipe);
                    break;
                case "5":
                    ClearRecipe(ref recipe);
                    break;
                case "6":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice! Please enter a number from 1 to 6.\n");
                    break;
            }
        }
    }

    //method to create a new recipe
    static Recipe CreateNewRecipe()
    {
        int ingredientCount = GetIntInput("Enter the number of ingredients: \n");
        int stepCount = GetIntInput("Enter the number of steps: \n");
        Recipe recipe = new Recipe(ingredientCount, stepCount);

        for (int i = 0; i < ingredientCount; i++)
        {
            string name = GetStringInput($"Enter ingredient name ({i + 1}): \n");
            double quantity = GetDoubleInput($"Enter ingredient quantity ({i + 1}): \n");
            string unit = GetStringInput($"Enter ingredient unit ({i + 1}): \n");
            recipe.AddIngredient(name, quantity, unit);
        }

        for (int i = 0; i < stepCount; i++)
        {
            string description = GetStringInput($"Enter step  {i + 1}: \n");
            recipe.AddStep(description);
        }

        Console.WriteLine("Recipe created successfully!\n");
        return recipe;
    }

    //method to print the recipe
    static void PrintRecipe(Recipe recipe)
    {
        if (recipe == null)
        {
            Console.WriteLine("No recipe created yet!\n");
        }
        else
        {
            recipe.PrintRecipe();
            Console.WriteLine();
        }
    }

    //method to scale the recipe
    static void ScaleRecipe(Recipe recipe)
    {
        if (recipe == null)
        {
            Console.WriteLine("No recipe created yet!\n");
            return;
        }

        double factor = GetDoubleInput("Enter the scaling factor (0.5, 2, or 3): \n");
        if (factor != 0.5 && factor != 2 && factor != 3)
        {
            Console.WriteLine("Invalid scaling factor! Please enter 0.5, 2, or 3.\n");
            return;
        }

        recipe.ScaleRecipe(factor);
        Console.WriteLine("Recipe scaled successfully!\n");
        PrintRecipe(recipe);
    }

    //method to reset the quantities of ingredients
    static void ResetQuantities(Recipe recipe)
    {
        if (recipe == null)
        {
            Console.WriteLine("No recipe created yet!\n");
            return;
        }

        recipe.ResetQuantities();
        Console.WriteLine("Quantities reset successfully!\n");
        PrintRecipe(recipe);
    }

    //method to clear recipe
    static void ClearRecipe(ref Recipe recipe)
    {
        recipe = null;
        Console.WriteLine("Recipe cleared!\n");
    }

    //helper method to get an integer input from the user
    static int GetIntInput(string message)
    {
        Console.Write(message);
        int input;
        while (!int.TryParse(Console.ReadLine(), out input))
        {
            Console.WriteLine("Invalid input! Please enter an integer.\n");
            Console.Write(message);
        }
        return input;
    }

    //helper method to get a double input from the user
    static double GetDoubleInput(string message)
    {
        Console.Write(message);
        double input;
        while (!double.TryParse(Console.ReadLine(), out input))
        {
            Console.WriteLine("Invalid input! Please enter a number.\n");
            Console.Write(message);
        }
        return input;
    }

    static string GetStringInput(string message)
    {
        Console.Write(message);
        return Console.ReadLine();
    }
}