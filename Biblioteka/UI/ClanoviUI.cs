using Biblioteka.DAO;
using Biblioteka.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.UI
{
     class ClanoviUI
    {
        public static void ClanoviMenu()
        {
            int odluka = -1;
            while (odluka != 0)
            {
                Console.WriteLine("Meni za rad sa clanovima biblioteke :\n");
                Console.WriteLine("\t1. Dodaj novog clana");
                Console.WriteLine("\t2. Obrisi clana");
                Console.WriteLine("\t3. Izlistaj sve clanove");

                odluka = int.Parse(Console.ReadLine());
                Console.Clear();

                switch (odluka)
                {
                    case 0:
                        Console.WriteLine("Izlaz iz programa");
                        break;
                    case 1:
                        DaoClanovi.ClanDodaj();
                        break;
                    case 2:
                        DaoClanovi.ClanObrisi();
                        break;
                    case 3:
                        DaoClanovi.IspisSvihClanova();
                            break;
                    default:
                        Console.WriteLine("Nepoznata komanda");
                        break;
                }
            }
        }



       
    }
}
