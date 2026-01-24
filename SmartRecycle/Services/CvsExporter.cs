using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace SmartRecycle.Services
{
    public static class CsvExporter
    {
        // Buraya MainForm’dan LISTE gönderiyoruz (grid değil)
        public static void ExportWastes(IEnumerable<dynamic> wastes, string filePath)
        {
            var sb = new StringBuilder();

            sb.AppendLine("Type,WeightKg,Date,RecyclableKg,EnergySavedKwh,UnitUsdPerKg,TotalUsd,ProfitUsd");

            foreach (var w in wastes)
            {
                // dynamic: w.Type / w.WeightKg / w.Date alanlarını okur (sende farklı isimse söylerim)
                string type = (w?.Type ?? "").ToString();
                double weightKg = ToDouble(w?.WeightKg);
                string date = "";

                // DateTime ise düzgün yaz
                if (w?.Date is DateTime dt) date = dt.ToString("yyyy-MM-dd");
                else date = (w?.Date ?? "").ToString();

                var recyclableKg = StatisticsService.GetRecyclableKg(type, weightKg);
                var energyKwh = StatisticsService.GetEnergySavedKwh(type, weightKg);
                var unitUsd = StatisticsService.GetUnitUsd(type);
                var totalUsd = StatisticsService.GetTotalUsd(type, weightKg);
                var profitUsd = StatisticsService.GetProfitUsd(type, weightKg);

                sb.Append(Csv(type)).Append(",");
                sb.Append(ToInv(weightKg)).Append(",");
                sb.Append(Csv(date)).Append(",");
                sb.Append(ToInv(recyclableKg)).Append(",");
                sb.Append(ToInv(energyKwh)).Append(",");
                sb.Append(ToInv(unitUsd)).Append(",");
                sb.Append(ToInv(totalUsd)).Append(",");
                sb.Append(ToInv(profitUsd)).AppendLine();
            }

            File.WriteAllText(filePath, sb.ToString(), new UTF8Encoding(true));
        }

        private static double ToDouble(object? v)
        {
            if (v == null) return 0.0;
            if (v is double d) return d;
            if (v is float f) return f;
            if (v is decimal m) return (double)m;
            if (v is int i) return i;
            if (v is long l) return l;

            var s = v.ToString() ?? "0";
            if (double.TryParse(s, NumberStyles.Any, CultureInfo.CurrentCulture, out var tr)) return tr;

            s = s.Replace(",", ".");
            if (double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out var inv)) return inv;

            return 0.0;
        }

        private static string ToInv(double v) => v.ToString("0.##", CultureInfo.InvariantCulture);

        private static string Csv(string s)
        {
            s ??= "";
            if (s.Contains(",") || s.Contains("\"") || s.Contains("\n") || s.Contains("\r"))
                return "\"" + s.Replace("\"", "\"\"") + "\"";
            return s;
        }
    }
}