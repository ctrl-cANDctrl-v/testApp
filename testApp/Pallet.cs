using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testApp
{
    public class Pallet : Item
    {
        public double Volume { get; set; }
        public List<Box> Boxes { get; set; } = new List<Box>();
        public DateTime? ExpirationDate 
        {
            get
            {
                var earliestExpirationDate = Boxes.Select(b => b.ExpirationDate).Min();
                if (earliestExpirationDate.HasValue)
                    return earliestExpirationDate.Value;
                else
                    return null;
            }
        }

        // Проверка на случай изменения входных данных для random при генерации коробок
        public bool CanContain(Box box)
        {
            return Width >= box.Width && Depth >= box.Depth;
        }

        public double CalcVolume()
        {
            Volume = Height * Width * Depth;
            foreach (var box in Boxes)
            {
                Volume += box.Volume;
            }
            return Volume;
        }

        public double CalcWeight()
        {
            Weight = 30;
            foreach (var box in Boxes)
            {
                Weight += box.Weight;
            }
            return Weight;
        }

      
    }
}
