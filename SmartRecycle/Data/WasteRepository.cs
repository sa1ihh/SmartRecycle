using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using SmartRecycle.Models;

namespace SmartRecycle.Data
{
    public class WasteRepository
    {
        private readonly string _filePath;
        private readonly List<Waste> _items = new();

        private class WasteRecord
        {
            public WasteType Type { get; set; }
            public double WeightKg { get; set; }
            public DateTime Date { get; set; }
        }

        public WasteRepository(string filePath = "wastes.json")
        {
            _filePath = Path.GetFullPath(filePath);
            Load();
        }

        public IReadOnlyList<Waste> GetAll()
        {
            return _items.ToList();
        }

        public void Add(Waste waste)
        {
            if (waste == null) throw new ArgumentNullException(nameof(waste));
            _items.Add(waste);
            Save();
        }

        public void Clear()
        {
            _items.Clear();
            Save();
        }

        public void Load()
        {
            _items.Clear();

            if (!File.Exists(_filePath))
                return;

            var json = File.ReadAllText(_filePath);
            if (string.IsNullOrWhiteSpace(json))
                return;

            var records = JsonSerializer.Deserialize<List<WasteRecord>>(json) ?? new List<WasteRecord>();

            foreach (var r in records)
            {
                var w = CreateWaste(r.Type, r.WeightKg, r.Date);
                _items.Add(w);
            }
        }

        public void Save()
        {
            var records = _items.Select(w => new WasteRecord
            {
                Type = w.Type,
                WeightKg = w.WeightKg,
                Date = w.Date
            }).ToList();

            var json = JsonSerializer.Serialize(records, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public static Waste CreateWaste(WasteType type, double weightKg, DateTime date)
        {
            Waste w = type switch
            {
                WasteType.Plastic => new PlasticWaste(),
                WasteType.Paper => new PaperWaste(),
                WasteType.Glass => new GlassWaste(),
                WasteType.Metal => new MetalWaste(),
                WasteType.Organic => new OrganicWaste(),
                _ => new PlasticWaste()
            };

            w.WeightKg = weightKg;
            w.Date = date;
            return w;
        }
    }
}