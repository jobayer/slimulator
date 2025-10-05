namespace Slimulator
{
    public enum BMICategory
    {
        Underweight,
        Healthy,
        Overweight,
        Obesity
    }

    public static class BMI
    {
        public static double CalculateBMI(double heightInMeters, double weightInKg)
        {
            return weightInKg / (heightInMeters * heightInMeters);
        }

        public static BMICategory GetBMICategory(double bmi)
        {
            if (bmi < 18.5)
                return BMICategory.Underweight;
            else if (bmi < 25)
                return BMICategory.Healthy;
            else if (bmi < 30)
                return BMICategory.Overweight;
            else
                return BMICategory.Obesity;
        }

        public static string GetBMICategoryString(BMICategory category)
        {
            return category.ToString();
        }
    }
}
