using System.Text.Json;

namespace Slimulator
{
    public class BMIManager
    {
        private static readonly string CsvFilePath = Const.DATA_PATH;
        private static readonly string ConfigFilePath = Const.CONFIG_PATH;

        public List<BMIRecord> Records { get; private set; } = new List<BMIRecord>();
        public UnitConfig Config { get; private set; } = new UnitConfig();

        public BMIManager()
        {
            LoadConfig();
            LoadRecords();
        }

        private void LoadConfig()
        {
            if (File.Exists(ConfigFilePath))
            {
                string json = File.ReadAllText(ConfigFilePath);
                var loaded = JsonSerializer.Deserialize<UnitConfig>(json);
                if (loaded != null)
                {
                    Config = loaded;
                }
                else
                {
                    Config = new UnitConfig();
                }
            }
            else
            {
                Config = new UnitConfig();
                SaveConfig();
            }
        }

        public void SaveConfig()
        {
            string json = JsonSerializer.Serialize(Config, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ConfigFilePath, json);
        }

        private void LoadRecords()
        {
            if (File.Exists(CsvFilePath))
            {
                var lines = File.ReadAllLines(CsvFilePath);
                foreach (var line in lines.Skip(1))
                {
                    var parts = line.Split(',');
                    if (parts.Length == 5)
                    {
                        int id = int.Parse(parts[0]);
                        double height = double.Parse(parts[1]);
                        double weight = double.Parse(parts[2]);
                        double bmi = double.Parse(parts[3]);
                        DateTime date = DateTime.Parse(parts[4]);
                        Records.Add(new BMIRecord(id, height, weight, bmi, date));
                    }
                }
            }
        }

        public void SaveRecords()
        {
            using (var writer = new StreamWriter(CsvFilePath))
            {
                writer.WriteLine("id,height (m),weight (kg),bmi,date");
                foreach (var record in Records)
                {
                    writer.WriteLine($"{record.Id},{record.Height:F2},{record.Weight:F2},{record.BMI:F2},{record.Date:yyyy-MM-dd HH:mm:ss}");
                }
            }
        }

        public void AddRecord(double height, double weight, double bmi)
        {
            int id = Records.Count > 0 ? Records.Max(r => r.Id) + 1 : 1;
            var record = new BMIRecord(id, height, weight, bmi, DateTime.Now);
            Records.Add(record);
            SaveRecords();
        }

        public void DeleteRecord(int id)
        {
            var record = Records.FirstOrDefault(r => r.Id == id);
            if (record != null)
            {
                Records.Remove(record);
                SaveRecords();
            }
        }

        public void DeleteAllRecords()
        {
            Records.Clear();
            SaveRecords();
        }

        public List<BMIRecord> GetAllRecords()
        {
            return Records.OrderBy(r => r.Date).ToList();
        }
    }
}