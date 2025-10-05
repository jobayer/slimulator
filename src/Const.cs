using System;
using System.IO;

namespace Slimulator
{
    public static class Const
    {
        public static readonly string DATA_PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bmi_records.csv");
        public static readonly string CONFIG_PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.json");
    }
}