using System;

namespace SmartRecycle.Models
{
    public enum WasteType
    {
        Plastic,
        Paper,
        Glass,
        Metal,
        Organic
    }

    public abstract class Waste
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public abstract WasteType Type { get; }
        public double WeightKg { get; set; }
        public DateTime Date { get; set; }

        public abstract double RecyclableRate { get; }
        public abstract double EnergySavingKwhPerKg { get; }

        public double RecyclableKg => WeightKg * RecyclableRate;
        public double EnergySavedKwh => RecyclableKg * EnergySavingKwhPerKg;
    }
}