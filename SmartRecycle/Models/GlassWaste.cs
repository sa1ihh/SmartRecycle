using System;

namespace SmartRecycle.Models
{
    public class GlassWaste : Waste
    {
        public override WasteType
            Type => WasteType.Glass;
        public override double
            RecyclableRate => 0.90;
        public override double
            EnergySavingKwhPerKg => 0.9;
    }
}