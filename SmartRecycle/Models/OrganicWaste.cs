using System;

namespace SmartRecycle.Models
{
    public class OrganicWaste : Waste
    {
        public override WasteType Type => WasteType.Organic;
        public override double RecyclableRate => 0.60;
        public override double EnergySavingKwhPerKg => 0.3;
    }
}