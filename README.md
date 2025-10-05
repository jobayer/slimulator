# Slimulator

A simple C# console application for tracking BMI (Body Mass Index), and health progress over time. This tool helps users monitor their weight and BMI trends through an easy-to-use menu-driven interface.

## Downloads

Pre-built executable files are available in the [Releases](https://github.com/jobayer/slimulator/releases) section of this repository.

- **slimulator-x64.exe**: For 64-bit Windows systems.
- **slimulator-x86.exe**: For 32-bit Windows systems.

Download the appropriate version for your system, place it in a folder, and run it directly. No installation required!

## Features

- **BMI Calculation**: Calculate your BMI based on height and weight inputs.
- **Record Management**: Add, view, and delete BMI records with timestamps.
- **Data Visualization**: Display an ASCII chart to visualize BMI progress over time.
- **Unit Conversion**: Support for multiple units (height: meters, centimeters, feet, inches; weight: kilograms, grams, pounds, ounces).
- **Persistent Storage**: Records are saved to a CSV file, and user preferences (units) are stored in a JSON config file.
- **Health Categories**: Automatic BMI categorization (Underweight, Healthy, Overweight, Obesity).

## Requirements

- Windows (x64 or x86)
- No additional dependencies (self-contained exe)

## Building from Source

If you prefer to build from source:

1. Clone the repository:
   ```bash
   git clone https://github.com/jobayer/slimulator.git
   cd slimulator
   ```

2. Build the project:
   ```bash
   dotnet build
   ```

3. Run the application:
   ```bash
   dotnet run
   ```

## Usage

Upon running the application, you'll see a main menu with the following options:

1. **Calculate BMI**: Enter your height and weight to calculate BMI. Optionally save the record.
2. **Add New Record**: Add a new BMI records.
3. **List All Records**: View all saved BMI records.
4. **Delete Records**: Remove specific records or delete all records.
5. **Show ASCII Chart**: Visualize BMI trends as a simple ASCII chart.
6. **Change Unit**: Switch between different units for height and weight.
7. **Exit**: Close the application.

### Example Interaction

```
BMI Health Tracker
==================
1. Calculate BMI
2. Add New Record
3. List All Records (0 records)
4. Delete Records
5. Show ASCII Chart
6. Change Unit
7. Exit
Choose an option: 1

Calculate BMI
=============
Enter height (ft): 5.8
Enter weight (kg): 70
Your BMI is: 22.49 (Healthy)
Do you want to save this record? (y/n): y
Record saved!
Press any key to continue...
```

## Configuration

- Units are saved in `config.json` (created automatically in the same directory as the exe).
- Records are stored in `bmi_records.csv` (created automatically in the same directory as the exe).

## BMI Categories

- **Underweight**: BMI < 18.5
- **Healthy**: 18.5 ≤ BMI < 25
- **Overweight**: 25 ≤ BMI < 30
- **Obesity**: BMI ≥ 30

*Reference: [Calculate Your BMI - NHLBI, NIH](https://www.nhlbi.nih.gov/calculate-your-bmi)*

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request for any improvements or bug fixes.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Attribution

<a href="https://www.flaticon.com/free-icons/bmi" title="bmi icons">Bmi icons created by Flat Icons - Flaticon</a>

## Disclaimer

This application is for informational purposes only and should not replace professional medical advice. Consult a healthcare provider for personalized health recommendations.
