using System;

namespace SmartRecycle.Models
{
    public class PlasticWaste : Waste
    {
        public override WasteType Type => WasteType.Plastic;
        public override double RecyclableRate => 0.75;
        public override double EnergySavingKwhPerKg => 5.8;
    }
}