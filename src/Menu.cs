namespace Slimulator
{
    public class Menu
    {
        private BMIManager manager;

        public Menu(BMIManager manager)
        {
            this.manager = manager;
        }

        public void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("BMI Health Tracker");
                Console.WriteLine("==================");
                Console.WriteLine("1. Calculate BMI");
                Console.WriteLine("2. Add New Record");
                Console.WriteLine($"3. List All Records ({manager.GetAllRecords().Count} records)");
                Console.WriteLine("4. Delete Records");
                Console.WriteLine("5. Show ASCII Chart");
                Console.WriteLine("6. Change Unit");
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option: ");

                string choice = Console.ReadLine() ?? "";
                switch (choice)
                {
                    case "1":
                        CalculateBMI();
                        break;
                    case "2":
                        AddNewRecord();
                        break;
                    case "3":
                        ListAllRecords();
                        break;
                    case "4":
                        DeleteRecords();
                        break;
                    case "5":
                        ShowASCIIChart();
                        break;
                    case "6":
                        ChangeUnit();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Press any key to continue...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void CalculateBMI()
        {
            Console.Clear();
            Console.WriteLine("Calculate BMI");
            Console.WriteLine("=============");

            double? heightNullable = GetHeightInput();
            if (!heightNullable.HasValue) return;
            double height = heightNullable.Value;

            double? weightNullable = GetWeightInput();
            if (!weightNullable.HasValue) return;
            double weight = weightNullable.Value;

            double heightM = UnitConverter.ConvertHeightToMeters(height, manager.Config.HeightUnit);
            double weightKg = UnitConverter.ConvertWeightToKg(weight, manager.Config.WeightUnit);
            double bmi = BMI.CalculateBMI(heightM, weightKg);

            string category = BMI.GetBMICategoryString(BMI.GetBMICategory(bmi));
            Console.WriteLine($"Your BMI is: {bmi:F2} ({category})");

            Console.Write("Do you want to save this record? (y/n): ");
            string save = Console.ReadLine() ?? "";
            if (save.ToLower() == "y")
            {
                manager.AddRecord(heightM, weightKg, bmi);
                Console.WriteLine("Record saved!");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void AddNewRecord()
        {
            bool addAnother = true;
            while (addAnother)
            {
                Console.Clear();
                Console.WriteLine("Add New Record");
                Console.WriteLine("==============");

                double? heightNullable = GetHeightInput();
                if (!heightNullable.HasValue) break;
                double height = heightNullable.Value;

                double? weightNullable = GetWeightInput();
                if (!weightNullable.HasValue) break;
                double weight = weightNullable.Value;

                double heightM = UnitConverter.ConvertHeightToMeters(height, manager.Config.HeightUnit);
                double weightKg = UnitConverter.ConvertWeightToKg(weight, manager.Config.WeightUnit);
                double bmi = BMI.CalculateBMI(heightM, weightKg);

                string category = BMI.GetBMICategoryString(BMI.GetBMICategory(bmi));
                Console.WriteLine($"Your BMI is: {bmi:F2} ({category})");

                manager.AddRecord(heightM, weightKg, bmi);
                Console.WriteLine("Record saved!");

                Console.Write("Do you want to add another record? (y/n): ");
                string another = Console.ReadLine() ?? "";
                addAnother = another.ToLower() == "y";
            }
        }

        private void ListAllRecords()
        {
            Console.Clear();
            Console.WriteLine("All BMI Records");
            Console.WriteLine("===============");

            var records = manager.GetAllRecords();
            if (records.Count == 0)
            {
                Console.WriteLine("No records found.");
            }
            else
            {
                foreach (var record in records)
                {
                    Console.WriteLine(record.ToString());
                }
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void ShowASCIIChart()
        {
            Console.Clear();
            Console.WriteLine("ASCII Chart of Health Progress");
            Console.WriteLine("==============================");
            Console.WriteLine("Y-axis: BMI values | X-axis: Record IDs");

            var records = manager.GetAllRecords();
            if (records.Count < 2)
            {
                Console.WriteLine("Not enough records to plot chart.");
            }
            else
            {
                var sortedRecords = records.OrderBy(r => r.Id).ToList();
                double minBMI = sortedRecords.Min(r => r.BMI);
                double maxBMI = sortedRecords.Max(r => r.BMI);
                int chartHeight = 10;

                for (int i = chartHeight; i >= 0; i--)
                {
                    double level = minBMI + (maxBMI - minBMI) * i / chartHeight;
                    Console.Write($"{level:F1} | ");
                    foreach (var record in sortedRecords)
                    {
                        if (record.BMI >= level)
                        {
                            Console.Write("*");
                        }
                        else
                        {
                            Console.Write(" ");
                        }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine(new string('-', sortedRecords.Count * 3 + 10));
                Console.Write("      ");
                foreach (var record in sortedRecords)
                {
                    Console.Write($"{record.Id}");
                }
                Console.WriteLine();
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void ChangeUnit()
        {
            Console.Clear();
            Console.WriteLine("Change Unit");
            Console.WriteLine("===========");

            Console.WriteLine("Current Height Unit: " + manager.Config.HeightUnit);
            Console.Write("New Height Unit (m, cm, ft, in): ");
            string heightUnit = Console.ReadLine() ?? "";
            if (!string.IsNullOrEmpty(heightUnit))
            {
                manager.Config.HeightUnit = heightUnit;
            }

            Console.WriteLine("Current Weight Unit: " + manager.Config.WeightUnit);
            Console.Write("New Weight Unit (kg, g, lb, oz): ");
            string weightUnit = Console.ReadLine() ?? "";
            if (!string.IsNullOrEmpty(weightUnit))
            {
                manager.Config.WeightUnit = weightUnit;
            }

            manager.SaveConfig();
            Console.WriteLine("Units updated!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void DeleteRecords()
        {
            Console.Clear();
            Console.WriteLine("Delete Records");
            Console.WriteLine("==============");

            var records = manager.GetAllRecords();
            if (records.Count == 0)
            {
                Console.WriteLine("No records found.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Current Records:");
            foreach (var record in records)
            {
                Console.WriteLine(record.ToString());
            }

            Console.WriteLine();
            Console.WriteLine("Hints:");
            Console.WriteLine("- To delete all records, press *");
            Console.WriteLine("- To delete a specific record, press the record ID");
            Console.WriteLine("- To cancel, press any other key");
            Console.Write("Your choice: ");

            string input = Console.ReadLine() ?? "";
            if (input == "*")
            {
                manager.DeleteAllRecords();
                Console.WriteLine("All records deleted!");
            }
            else if (int.TryParse(input, out int id))
            {
                var record = records.FirstOrDefault(r => r.Id == id);
                if (record != null)
                {
                    manager.DeleteRecord(id);
                    Console.WriteLine($"Record with ID {id} deleted!");
                }
                else
                {
                    Console.WriteLine("Record not found.");
                }
            }
            else
            {
                Console.WriteLine("Operation cancelled.");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private double? GetHeightInput()
        {
            while (true)
            {
                Console.Write($"Enter height ({manager.Config.HeightUnit}): ");
                string? input = ReadLineOrEsc();
                if (input == null)
                {
                    return null;
                }
                if (double.TryParse(input, out double value))
                {
                    return value;
                }
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }

        private double? GetWeightInput()
        {
            while (true)
            {
                Console.Write($"Enter weight ({manager.Config.WeightUnit}): ");
                string? input = ReadLineOrEsc();
                if (input == null)
                {
                    return null;
                }
                if (double.TryParse(input, out double value))
                {
                    return value;
                }
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }

        private string? ReadLineOrEsc()
        {
            string input = "";
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    return input;
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine();
                    return null;
                }
                else if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                {
                    input = input.Substring(0, input.Length - 1);
                    Console.Write("\b \b");
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    input += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
            }
        }
    }
}