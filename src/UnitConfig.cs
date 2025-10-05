namespace Slimulator
{
    public class UnitConfig
    {
        public string HeightUnit { get; set; }
        public string WeightUnit { get; set; }

        public UnitConfig()
        {
            HeightUnit = "m";
            WeightUnit = "kg";
        }

        public UnitConfig(string heightUnit, string weightUnit)
        {
            HeightUnit = heightUnit;
            WeightUnit = weightUnit;
        }
    }
}