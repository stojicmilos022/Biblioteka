using Biblioteka.DAO;
using Biblioteka.Model;
using Biblioteka.UI;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public  class ClanoviHelp
    {
        public static ClanBiblioteke ProveraUnosaClana()
        {
            ClanBiblioteke noviClan = null;
            string ime, prezime;

            Console.WriteLine("Unesi ime novog clana ");
            ime = Console.ReadLine();
            Console.WriteLine("Unesi prezime novog clana");
            prezime = Console.ReadLine();
            if (!string.IsNullOrEmpty(ime) && !string.IsNullOrEmpty(prezime))
            {
                noviClan = new ClanBiblioteke(ime, prezime);
            }
            else
            {
                Console.WriteLine("Los unos podataka");
            }

            return noviClan;
        }

        public static bool TestDodavanjaClana(ClanBiblioteke noviClan)
        {
            SqlConnection connection = DaoKonekcija.NewConnection();

            bool Izvrseno;

            string sQuerry = "insert into ClanoviBiblioteke (Ime,Prezime) values (@ime,@prezime)";

            SqlCommand cmd = new SqlCommand(sQuerry, connection);
            cmd.Parameters.AddWithValue("ime", noviClan.Ime);
            cmd.Parameters.AddWithValue("prezime", noviClan.Prezime);
            try
            {
                cmd.ExecuteNonQuery();
                Izvrseno = true;
            }
            catch
            {
                Izvrseno = false;
            }

            connection.Close();
            return Izvrseno;
        }
        
        public static bool DalJeNekaKnjigaKodClana(ClanBiblioteke clan)
        {
            SqlConnection conn = DaoKonekcija.NewConnection();

            bool izvrseno;

            string sQuerry = "select id from ClanoviBiblioteke  a where a.Id in (select id_clana from Knjige) and id=@id";
            SqlCommand cmd = new SqlCommand(sQuerry,conn);
            cmd.Parameters.AddWithValue("id", clan.id);
            SqlDataReader dr = cmd.ExecuteReader();

            if(dr.Read())
            {
                izvrseno = true;
            }
            else
            { 
                izvrseno = false; 
            }
            conn.Close();
            dr.Close();
            return izvrseno;
        }
        public static bool TestBrisanja(ClanBiblioteke obrisi)
        {
            SqlConnection conn = DaoKonekcija.NewConnection();

            bool izvrseno = false;
            string sQuerry = "delete ClanoviBiblioteke where  id=@id";
            SqlCommand cmd = new SqlCommand(sQuerry, conn);
            cmd.Parameters.AddWithValue("id", obrisi.id);

            try
            {
                cmd.ExecuteNonQuery();
                izvrseno = true;
            }
            catch
            {
                izvrseno = false;
            }
            conn.Close();
            return izvrseno;
        }
        
        public static  ClanBiblioteke PreuzmiClanaAkoPostoji()
        {
            ClanBiblioteke drugClan = null;
            int userInput;
            Console.Clear();
            DaoClanovi.IspisSvihClanova();
            Console.WriteLine("Unesite id clana :");
            string unetiTekst = Console.ReadLine();
            if(int.TryParse(unetiTekst, out userInput)==false)
            {
                Console.WriteLine("Id nije integer");
            }
            else
            {
               userInput=int.Parse(unetiTekst);

               drugClan = ClanPreuzmiPoId(userInput);

               if (drugClan == null)
                {
                    Console.WriteLine("Nepostojeci clan");
                }
            }

            return drugClan;
        }
       
        
        public static  ClanBiblioteke ClanPreuzmiPoId(int id)
        {
            SqlConnection conn = DaoKonekcija.NewConnection();

            ClanBiblioteke drugClan;
            string sQuerry = "select id,ime,prezime from clanoviBiblioteke where  id=@id";
            SqlCommand cmd = new SqlCommand(sQuerry, conn);
            cmd.Parameters.AddWithValue("id", id);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                int Clan_Id = (int)dr[0];
                string ime = (string)dr[1];
                string prezime= (string)dr[2];
                ClanBiblioteke clan = new ClanBiblioteke(id, ime, prezime);
                drugClan = clan;
                    
            }
            else
            {
                drugClan=null;
            }
            conn.Close();
            dr.Close();
            return drugClan;
        }
        
    }
}
