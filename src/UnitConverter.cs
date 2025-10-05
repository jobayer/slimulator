namespace Slimulator
{
    public static class UnitConverter
    {
        public static double ConvertHeightToMeters(double value, string unit)
        {
            switch (unit.ToLower())
            {
                case "m":
                    return value;
                case "cm":
                    return value / 100;
                case "ft":
                    return value * 0.3048;
                case "in":
                    return value * 0.0254;
                default:
                    throw new ArgumentException("Invalid height unit");
            }
        }

        public static double ConvertWeightToKg(double value, string unit)
        {
            switch (unit.ToLower())
            {
                case "kg":
                    return value;
                case "g":
                    return value / 1000;
                case "lb":
                    return value * 0.453592;
                case "oz":
                    return value * 0.0283495;
                default:
                    throw new ArgumentException("Invalid weight unit");
            }
        }

    }
}