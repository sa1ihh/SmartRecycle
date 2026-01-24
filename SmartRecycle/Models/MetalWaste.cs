using System;

namespace SmartRecycle.Models
{
    public class MetalWaste : Waste
    {
        public override WasteType Type => WasteType.Metal;
        public override double RecyclableRate => 0.95;
        public override double EnergySavingKwhPerKg => 8.2;
    }
}