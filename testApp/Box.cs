using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testApp
{
    public class Box : Item
    {
        public static Random random = new Random();
        public double Volume => Width * Height * Depth;
        public DateTime? ProductionDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
       

        public void CalcExpDate()
        {
            if (!ProductionDate.HasValue)
            {
                ExpirationDate = DateTime.Now.AddDays(random.Next(10, 50));
            }
            else
                ExpirationDate = ProductionDate.Value.AddDays(100);
        }
    }
}
