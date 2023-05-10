using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.UI
{
    public class MainMenuUI
    {
        // ispis glavnog menija
        
        public static void MMenuUI()
        {
            int odluka = -1;
            while (odluka!=0)
            {
                Console.WriteLine("Biblioteka informacioni sistem \n");
                Console.WriteLine("Odaberi opciju za rad :\n");
                Console.WriteLine("\t1. Rad sa clanovima biblioteke :");
                Console.WriteLine("\t2. Rad sa Knjigama :\n");
                Console.WriteLine("Izaberi jednu od opcija :");
                odluka=int.Parse(Console.ReadLine());
                Console.Clear();

                switch (odluka)
                {
                    case 0:
                        Console.WriteLine("Izlaz iz programa");
                        break;
                    case 1:
                        ClanoviUI.ClanoviMenu();
                        break;
                    case 2:
                        KnjigeUI.KnjigeMenu();
                        break;
                    default:
                        Console.WriteLine("Nepoznata komanda");
                        break;
                }
            }
        }
    }
}
