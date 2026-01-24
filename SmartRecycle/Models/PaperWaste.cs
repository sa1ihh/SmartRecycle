using System;

namespace SmartRecycle.Models
{
    public class PaperWaste : Waste
    {
        public override WasteType Type => WasteType.Paper;
        public override double RecyclableRate => 0.85;
        public override double EnergySavingKwhPerKg => 4.0;
    }
}