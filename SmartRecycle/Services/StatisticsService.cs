using System;
using System.Collections.Generic;

namespace SmartRecycle.Services
{
    public static class StatisticsService
    {
        // Recyclable oran (kg başına geri dönüştürülebilir kısmı)
        private static readonly Dictionary<string, double> RecyclableRate = new(StringComparer.OrdinalIgnoreCase)
        {
            ["Paper"] = 0.85,
            ["Plastic"] = 0.70,
            ["Glass"] = 0.90,
            ["Metal"] = 0.95,
            ["Organic"] = 0.30
        };

        // Enerji tasarrufu (kWh / kg) - örnek katsayılar
        private static readonly Dictionary<string, double> EnergyKwhPerKg = new(StringComparer.OrdinalIgnoreCase)
        {
            ["Paper"] = 4.0,
            ["Plastic"] = 6.0,
            ["Glass"] = 0.3,
            ["Metal"] = 14.0,
            ["Organic"] = 0.2
        };

        // Birim değer ($ / kg) - senin istediğin “dolar cinsinden”
        private static readonly Dictionary<string, double> UnitUsdPerKg = new(StringComparer.OrdinalIgnoreCase)
        {
            ["Paper"] = 2.0,
            ["Plastic"] = 3.5,
            ["Glass"] = 1.2,
            ["Metal"] = 7.0,
            ["Organic"] = 5.0
        };

        public static double GetRecyclableKg(string type, double weightKg)
        {
            var rate = RecyclableRate.TryGetValue(type ?? "", out var r) ? r : 0.0;
            return Round2(weightKg * rate);
        }

        public static double GetEnergySavedKwh(string type, double weightKg)
        {
            var kwh = EnergyKwhPerKg.TryGetValue(type ?? "", out var e) ? e : 0.0;
            return Round2(weightKg * kwh);
        }

        public static double GetUnitUsd(string type)
        {
            return UnitUsdPerKg.TryGetValue(type ?? "", out var u) ? u : 0.0;
        }

        public static double GetTotalUsd(string type, double weightKg)
        {
            return Round2(weightKg * GetUnitUsd(type));
        }

        // İstersen “profit” gibi göster: geri dönüştürülebilir kısım üzerinden para
        public static double GetProfitUsd(string type, double weightKg)
        {
            var recyclableKg = GetRecyclableKg(type, weightKg);
            var unitUsd = GetUnitUsd(type);
            return Round2(recyclableKg * unitUsd);
        }

        private static double Round2(double v) => Math.Round(v, 2, MidpointRounding.AwayFromZero);
    }
}