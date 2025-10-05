namespace Slimulator
{
    public class BMIRecord
    {
        public int Id { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public double BMI { get; set; }
        public DateTime Date { get; set; }

        public BMIRecord(int id, double height, double weight, double bmi, DateTime date)
        {
            Id = id;
            Height = height;
            Weight = weight;
            BMI = bmi;
            Date = date;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Height: {Height} m, Weight: {Weight} kg, BMI: {BMI:F2}, Date: {Date:yyyy-MM-dd}";
        }
    }
}