using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace testApp
{
    internal class Program
    {
        public static Random random = new Random();
        public Box boxDB = new Box();
       

        static void Main(string[] args)
        {
            TemplateOut();
            Console.ReadLine();

        }

        public static void TemplateOut()
        {
            Console.WriteLine("Введите количесвто паллет для генерации");
            int reqAmount = Convert.ToInt32(Console.ReadLine());
            GenerateAndShowPallets(reqAmount);
        }


        public static void GenerateAndShowPallets(int reqAmPall)
        {
            #region Генерация паллетов и коробок
            var pallets = new List<Pallet>();
            for (int i = 0; i <= reqAmPall - 1; i++)
            {

                var pallet = new Pallet
                {
                    Id = pallets.Count + 1,
                    Width = random.Next(6, 15),
                    Height = random.Next(1, 15),
                    Depth = random.Next(6, 15),
                };

                var boxes = new List<Box>();
                for (int j = 0; j < random.Next(1,6); j++)
                {
                    var box = new Box
                    {
                        Id = boxes.Count + 1,
                        Width = random.Next(1, 5),
                        Height = random.Next(1, 5),
                        Depth = random.Next(1, 5),
                        Weight = random.Next(1, 11)
                    };
                    if (random.Next(2) == 0)
                    {
                         box.ProductionDate = DateTime.Now.AddDays(-random.Next(10,50));
                         box.CalcExpDate();
                    }
                    else
                    {
                         box.ProductionDate = null;
                         box.CalcExpDate();
                    }

                    if (pallet.CanContain(box))
                        boxes.Add(box);
                    
                }

                pallet.Boxes = boxes;
                pallet.Volume = pallet.CalcVolume();
                pallet.Weight = pallet.CalcWeight();
                pallets.Add(pallet);
            }
            #endregion

            Console.WriteLine("\n--------\nВсе паллеты с сортировкой по сроку годности и весу\n--------");

            #region Сортировка по сроку годности и весу
            var groups = pallets.GroupBy(x => x.ExpirationDate).OrderBy(y => y.Key);
            foreach (var group in groups)
            {
          
                Console.WriteLine($"\nСрок годности: {group.Key:d}");
                foreach (var pallet in group.OrderBy(p => p.Weight))
                {
                    Console.WriteLine($"Номер паллеты: {pallet.Id}, Вес = {pallet.Weight} кг, Объём = {pallet.Volume} см3, Срок годности {pallet.ExpirationDate:d}");
                    foreach (var box in pallet.Boxes)
                        Console.WriteLine($"Номер коробки: {box.Id}, Ширина {box.Width} см, Высота {box.Height} см, Глубина {box.Depth} см, Вес {box.Weight} кг, Срок годности {box.ExpirationDate:d}");
                }
            }
            #endregion

            Console.WriteLine("\n--------\nТоп-3 коробки с наибольшим сроком годности\n--------");
         
            #region Вывод топ-3 паллетов с наибольшим сроком годности
            var threePall = pallets.OrderByDescending(x => x.Boxes.Max(y => y.ExpirationDate)).Take(3).OrderBy(c => c.Volume);
            foreach (var pallet in threePall)
            {
                Console.WriteLine($"\nПаллета № {pallet.Id}, Срок годности - {pallet.ExpirationDate:d}, Объём - {pallet.Volume} см3");
                foreach(var box in pallet.Boxes)
                {
                    Console.WriteLine($"Номер коробки {box.Id}, Срок годности {box.ExpirationDate}, Объём {box.Volume} см3");
                }
            }
            #endregion
        }

    }
}
