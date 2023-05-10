using Biblioteka.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka.Model;
using System.Runtime.InteropServices;

namespace Biblioteka.UI
{
    public class KnjigeUI
    {
        // ispis menija za knjige
        public static void KnjigeMenu()
        {

            int odluka = -1;
            while (odluka != 0)
            {
                Console.WriteLine("\t1. Dodaj novu knjigu");
                Console.WriteLine("\t2. Obrisi knjigu");
                Console.WriteLine("\t3. Izlistaj knjige");
                Console.WriteLine("\t4. Dodeli knjigu clanu");
                Console.WriteLine("\t5. Preuzmi knjigu od clana");

                odluka = int.Parse(Console.ReadLine());
                Console.Clear();

                switch (odluka)
                {
                    case 0:
                        Console.WriteLine("Izlaz iz programa");
                        break;
                    case 1:
                        DaoKnjige.KnjigaDodaj();
                        break;

                    case 2:
                        DaoKnjige.ObrisiKnjigu();
                        break;
                    case 3:
                        KnjigeUI.IzlistajKnjigeMenu();
                        break;

                    case 4:
                        KnjigeUI.DodeljivanjeKnjigeClanu();
                        break;
                    case 5:
                        KnjigeUI.PreuzimanjeKnjigeOdClana();
                        break;


                    default:
                        Console.WriteLine("Nepoznata komanda");
                        break;
                }
            }

        }

        private static void PreuzimanjeKnjigeOdClana()
        {
            Knjige knjigaPreuzmi=KnjigeHelp.PreuzmiKnjiguAkoJeKodClanaH();

            if(knjigaPreuzmi==null)
            {
                return;
            }

            if (KnjigeHelp.DaliJeKnjigaUBiblioteci(knjigaPreuzmi) == true)
            {
                Console.WriteLine("Nije moguce preuzeti knjigu koja nije kod ni jednog clana...");
                Console.WriteLine("Mozete preuzeti samo neku od sledecih knjiga\n");

                DaoKnjige.IzdateKnjige();
                Console.WriteLine("\n\n");
                return;
            }

            bool oslobodjeno = DaoKnjige.OslobodiKnjigu(knjigaPreuzmi);

            if(oslobodjeno==false)
            {
                Console.WriteLine("Greska prilikom oslobadjanja knjige");
            }
            if(oslobodjeno==true)
            {
                Console.WriteLine("Knjiga sa nazivom \"{0}\" je ponovo dostupna",knjigaPreuzmi.ImeKnjige);
            }
        }
        // neki novi komentar
        private static void DodeljivanjeKnjigeClanu()
        {
            //preuzimam id knjige od korisnika
            Knjige dodeliKnjigu = KnjigeHelp.PreuzmiKnjiguAkoJeDostupna();
            // proveravam dali je funkcija vratila nesto ili nista, ako je nista batalim posao
            if (dodeliKnjigu == null)
            {
                return;
            }

            if (KnjigeHelp.DalJeNekaKnjigaKodClana(dodeliKnjigu) == true)
            {
                Console.WriteLine("Nije moguce dodelitiu knjigu koja je vec kod nekog clana...");
                Console.WriteLine("Mozete dodeliti samo neku od sledecih knjiga");
                DaoKnjige.DostupneKnjige();
                return;
            }

            ClanBiblioteke dodeliClanu = ClanoviHelp.PreuzmiClanaAkoPostoji();
            // proveravam dali je funkcija vratila nesto ili nista, ako je nista batalim posao
            if (dodeliClanu == null)
            {
                return; 
            }

            bool slobodnoDodeli = DaoKnjige.DodeliClanu(dodeliKnjigu, dodeliClanu);

            if (slobodnoDodeli == false)
            {
                Console.WriteLine("Doslo je do greske prilikom dodeljivanja knjige");
            }
            else
            {
                Console.WriteLine("Knjiga sa nazivom \"{0}\" je uspesno dodeljenja clanu {1} {2}",dodeliKnjigu.ImeKnjige,dodeliClanu.Ime,dodeliClanu.Prezime);
            }
            
        }

        public static void IzlistajKnjigeMenu()
        {
            int odluka = -1;
            while (odluka != 0)
            {

                Console.WriteLine("Ispis knjiga odaberi opciju");
                Console.WriteLine("\t1. Izlistaj sve knjige");
                Console.WriteLine("\t2. Izlistaj samo dostupne knjige");
                Console.WriteLine("\t3. Izlistaj samo  knjige koje su kod clanova");
                Console.WriteLine("\t4. Islistaj knjige za odredjenog autora");
                Console.WriteLine("\t5. Islistaj knjige koje su kod odredjenog clana");

                odluka = int.Parse(Console.ReadLine());
                Console.Clear();

                switch (odluka)
                {
                    case 0:
                        Console.WriteLine("Izlaz iz programa");
                        break;
                    case 1:
                        DaoKnjige.IzlistajSveKnjige();
                        break;
                    case 2:
                        DaoKnjige.DostupneKnjige();
                        break;
                    case 3:
                        DaoKnjige.IzdateKnjige();
                        break;
                    case 4:
                        DaoKnjige.IzlistajSveKnjigeZaNekogAutora();
                        break;
                    default:
                        Console.WriteLine("Nepoznata komanda");
                        break;
                }
            }
        }
        public static void NekaProbaZaGit()
        {
            Console.WriteLine();
        }


    }
}
