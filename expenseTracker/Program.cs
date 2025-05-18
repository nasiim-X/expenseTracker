using System;

class  Program
{
    static void Main(string[] args)
    {
        
        List<(DateTime Date, string Name, double Amount) > expense = new List<(DateTime, string, double)>();

        while (true)
        {

            Console.WriteLine("\nWelcome to the Expense traker! ");
            Console.WriteLine("\nChoose any actions");

            Console.WriteLine("1. Add Expense");
            Console.WriteLine("2. View Expense");
            Console.WriteLine("3. Save Expense");
            Console.WriteLine("4. Load Expense");
            Console.WriteLine("5. Delete Expense");
            Console.WriteLine("6. Exit");

            string option = Console.ReadLine();
            int chosenOption;
            if (!int.TryParse(option, out chosenOption))
            {
                Console.WriteLine("\nInvalid input. Please enter a number between 1-5.");
                return;
            }

            switch (chosenOption)
            {
                case 1: //Add Eepense
                    Console.WriteLine("\nEnter the expense name: ");
                    string expenseName = Console.ReadLine();

                    Console.WriteLine("\nEnter the expense amount: ");
                    string amountInput = Console.ReadLine();
                    double amount;

                    if (!double.TryParse(amountInput, out amount) || amount < 0)
                    {
                        Console.WriteLine("Enter a positve number.");
                        break ;
                    }

                    DateTime date = DateTime.Now;

                    expense.Add((date, expenseName, amount));
                    Console.WriteLine($"Expense '{expenseName}' of amount {amount} on |{date}| added successfully.");
                    break;
                    
                    //

                case 2: //View Expense
                    if (expense.Count == 0)
                    {
                        Console.WriteLine("\nNo expense recorded.");
                        break ;
                    }

                    Console.WriteLine("\nYour Expense: ");
                    double totalAmount = 0;
                    foreach (var exp in expense)
                    {
                        Console.WriteLine($"- {exp.Date} | {exp.Name}: {exp.Amount}");
                        totalAmount += exp.Amount;
                    }
                    Console.WriteLine($"\nTotal Expense: {totalAmount}");
                    break;
                    
                    //

                case 3: //Save Expense
                    string filePath = "expenses.txt";

                    try
                    {
                        using (StreamWriter writer = new StreamWriter(filePath))
                        {
                            foreach (var exp in expense)
                            {
                                writer.WriteLine($"{exp.Date},{exp.Name},{exp.Amount}");
                            }
                        }
                        Console.WriteLine("\nExpenses saved successfully.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\nError saving expenses: {ex.Message}");
                    }
                    break;

                    //

                case 4: //Load Expense
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
                                if (parts.Length == 3 && DateTime.TryParse(parts[0], out DateTime loadedDate) && 
                                    double.TryParse(parts[2], out double loadedAmount))
                                {
                                    expense.Add((loadedDate, parts[1], loadedAmount));
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

                    //

                case 5: //Delete
                    if (expense.Count == 0)
                    {
                        Console.WriteLine("\nNo expense to delete.");
                        break ;
                    }

                    Console.WriteLine("\nExpenses: ");
                    for (int i = 0; i < expense.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {expense[i].Name} - {expense[i].Amount}");
                    }

                    Console.WriteLine("\nEnte the number of the expenses you want to delete: ");
                    string deleteInput = Console.ReadLine();
                    int deleteIndex;

                    if (!int.TryParse(deleteInput, out deleteIndex) || deleteIndex < 1 || deleteIndex > expense.Count)
                    {
                        Console.WriteLine("\nInvalid Choice. Please select a valid number.");
                        break ;
                    }

                    expense.RemoveAt(deleteIndex - 1);
                    Console.WriteLine("\nExpense deleted successfully.");
                    break;

                    //

                case 6:
                    Console.WriteLine("\nExiting the Expense Tracker. Goodbye !");
                    return;

                default:
                    Console.WriteLine("\nPlease choose a valid option.");
                    break;

            }//switch
        }//while
    }//Main
}