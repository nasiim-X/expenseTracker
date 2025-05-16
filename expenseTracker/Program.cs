using System;

class  Program
{
    static void Main(string[] args)
    {
        
        List<(string Name, double Amount) > expense = new List<(string, double)>();

        while (true)
        {

            Console.WriteLine("Welcome to the Expense traker! ");
            Console.WriteLine("\nChoose any actions");

            Console.WriteLine("1. Add Expense");
            Console.WriteLine("2. View Expense");
            Console.WriteLine("3. Save Expense");
            Console.WriteLine("4. Load Expense");
            Console.WriteLine("5. Exit");

            string option = Console.ReadLine();
            int chosenOption;
            if (!int.TryParse(option, out chosenOption))
            {
                Console.WriteLine("\nInvalid input. Please enter a number between 1-5.");
                return;
            }

            switch (chosenOption)
            {
                case 1:
                    Console.WriteLine("\nEnter the expense name: ");
                    string expenseName = Console.ReadLine();

                    Console.WriteLine("\nEnter the expense amount: ");
                    string amountInput = Console.ReadLine();
                    double amount;

                    if (!double.TryParse(amountInput, out amount) || amount < 0)
                    {
                        Console.WriteLine("Enter a positve number.");
                        break;
                    }

                    expense.Add((expenseName, amount));
                    Console.WriteLine($"Expense '{expenseName}' of amount {amount} added successfully.");
                    break;


                case 2:
                    if (expense.Count == 0)
                    {
                        Console.WriteLine("\nNo expense recorded.");
                        break;
                    }

                    Console.WriteLine("\nYour Expense: ");
                    double totalAmount = 0;
                    foreach (var exp in expense)
                    {
                        Console.WriteLine($"- {exp.Name}: {exp.Amount}");
                        totalAmount += exp.Amount;
                    }
                    Console.WriteLine($"\nTotal Expense: {totalAmount}");
                    break;

                case 3:
                    string filePath = "expenses.txt";

                    try
                    {
                        using (StreamWriter writer = new StreamWriter(filePath))
                        {
                            foreach (var exp in expense)
                            {
                                writer.WriteLine($"{exp.Name},{exp.Amount}");
                            }
                        }
                        Console.WriteLine("\nExpenses saved successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nError saving expenses: {ex.Message}");
                    }
                    break;

                case 4:
                    if (!File.Exists("expenses.txt"))
                    {
                        Console.WriteLine("\nNo saved expenses found.");
                        break;
                    }

                    try
                    {
                        expense.Clear();

                        using (StreamReader reader = new StreamReader("expenses.txt"))
                        {
                            string line;
                            while((line = reader.ReadLine()) != null)
                            {
                                string[] parts = line.Split(',');
                                if (parts.Length == 2 && double.TryParse(parts[1], out double loadedAmount))
                                {
                                    expense.Add((parts[0], loadedAmount));
                                }
                            }
                        }
                        Console.WriteLine("\nExpenses loaded successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nError loading expenses: {ex.Message}");
                    }
                    break;

                case 5:
                    Console.WriteLine("\nExiting the Expense Tracker.");
                    return;

                default:
                    Console.WriteLine("\nPlease choose a valid option.");
                    break;

            }//switch
        }//while
    }//Main
}