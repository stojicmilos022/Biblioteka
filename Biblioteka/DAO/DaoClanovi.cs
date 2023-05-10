using Biblioteka.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.DAO
{
    public static class DaoClanovi
    {
        public static List<ClanBiblioteke> PreuzmiClanoveIzSql()
        {
            SqlConnection connection=DaoKonekcija.NewConnection();

            List<ClanBiblioteke> sviClanovi = new List<ClanBiblioteke>();

            string sQuerry = "select id,ime,prezime from ClanoviBiblioteke";

            SqlCommand cmd = new SqlCommand(sQuerry, connection);

            SqlDataReader rdr=cmd.ExecuteReader();

            while (rdr.Read())
            {
                int id = (int)rdr["Id"];
                string ime = (string)rdr["ime"];
                string prezime = (string)rdr["prezime"];
                ClanBiblioteke noviClan=new ClanBiblioteke(id, ime, prezime);
                sviClanovi.Add(noviClan);
            }
            rdr.Close();
            connection.Close();
            return sviClanovi;
        }






        public static void ClanObrisi()
        {
            ClanBiblioteke clanZaBrisanje = ClanoviHelp.PreuzmiClanaAkoPostoji();

            if (clanZaBrisanje == null)
            {
                return;
            }

            if(ClanoviHelp.DalJeNekaKnjigaKodClana(clanZaBrisanje)==true)
            {
                Console.WriteLine("Nije moguce obrisati clana kod koga je trenutno neka knjiga...");
            }

            bool obrisano = ClanoviHelp.TestBrisanja(clanZaBrisanje);
            if (obrisano == false) 
            {
                Console.WriteLine("Doslo je do greske prilikom brisanja");
            }
            else
            {
                Console.WriteLine("Clan  : {0} {1} je obrisan iz baze podataka", clanZaBrisanje.Ime, clanZaBrisanje.Prezime); 
            }
        }



        public static void ClanDodaj()
        {
            ClanBiblioteke novi = ClanoviHelp.ProveraUnosaClana();
            if (novi != null)
            {
                bool uspesno = ClanoviHelp.TestDodavanjaClana(novi);

                if(uspesno==false)
                {
                    Console.WriteLine("Greska pri unosu clana...");
                }
                else
                {
                    Console.WriteLine("Clan {0} je uspesno dodat",novi);
                }
            }

        }

        public static void IspisSvihClanova()
        {
            List<ClanBiblioteke> sviClanovi = DaoClanovi.PreuzmiClanoveIzSql();
            Console.WriteLine("\tSvi clanovi biblioteke :");
            Console.WriteLine("\t________________________________________________");
            Console.WriteLine("\t{0,-4} | {1,-15} | {2,-15}", "ID", "Ime", "Prezime");
            Console.WriteLine("\t________________________________________________");
            foreach (ClanBiblioteke c in sviClanovi)
            {
                Console.WriteLine(c.TabelarniPrikazClanova());
            }
            Console.WriteLine();
        }


    }
}
