using Biblioteka.DAO;
using Biblioteka.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka
{
    public class KnjigeHelp
    {
        internal static Knjige ProveraUnosaKnjige()
        {
            Knjige novaKnjiga = null;
            string imeKnjige, Autor;
            int godinaIzdavanja;

            Console.WriteLine("Unesi ime nove knjige ");
            imeKnjige = Console.ReadLine();
            Console.WriteLine("Unesi autora knjige");
            Autor = Console.ReadLine();
            Console.WriteLine("Unesi godinu izdavanja knjige");
            string tempGod=Console.ReadLine();

            if (int.TryParse(tempGod, out godinaIzdavanja) == false)
            {
                Console.WriteLine("Id nije integer");
            }
            else
            {
                godinaIzdavanja = int.Parse(tempGod);
            }
            if (!string.IsNullOrEmpty(imeKnjige) && !string.IsNullOrEmpty(Autor))
            {
                novaKnjiga = new Knjige(imeKnjige, Autor, godinaIzdavanja);
            }
            else
            {
                Console.WriteLine("Los unos podataka");
            }

            return novaKnjiga;
        }

        internal static bool TestUnosaKnjige(Knjige novaKnjiga)
        {
            SqlConnection connection = DaoKonekcija.NewConnection();

            bool Izvrseno;

            string sQuerry = "insert into Knjige (imeKnjige,Autor,GodinaIzdavanja) values (@imeKnjige,@Autor,@Godina)";

            SqlCommand cmd = new SqlCommand(sQuerry, connection);
            cmd.Parameters.AddWithValue("imeKnjige", novaKnjiga.ImeKnjige);
            cmd.Parameters.AddWithValue("Autor", novaKnjiga.Autor);
            cmd.Parameters.AddWithValue("Godina", novaKnjiga.GodinaIzdavanja);
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

        public static Knjige KnjigaPreuzmiPoId(int id)
        {
            SqlConnection conn = DaoKonekcija.NewConnection();

            Knjige knjigaK;
            string sQuerry = "select id,imeknjige,autor,GodinaIzdavanja from knjige where  id=@id";
            SqlCommand cmd = new SqlCommand(sQuerry, conn);
            cmd.Parameters.AddWithValue("id", id);

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                //int Clan_Id = (int)dr[0];
                string imeKnjige = (string)dr[1];
                string Autor = (string)dr[2];
                int godina = (int)dr[3];
                Knjige k = new Knjige(id, imeKnjige, Autor,godina);
                knjigaK = k;

            }
            else
            {
                knjigaK = null;
            }
            conn.Close();
            dr.Close();
            return knjigaK;
        }

        public static Knjige PreuzmiKnjiguAkoPostoji()
        {
            Knjige knjiga = null;
            int userInput;
            
            Console.Clear();
            DaoKnjige.IzlistajSveKnjige();
            Console.WriteLine("Unesite id knjige :");
            string unetiTekst = Console.ReadLine();
            if (int.TryParse(unetiTekst, out userInput) == false)
            {
                Console.WriteLine("Id nije integer");
            }
            else
            {
                userInput = int.Parse(unetiTekst);

                knjiga = KnjigaPreuzmiPoId(userInput);

                if (knjiga == null)
                {
                    Console.WriteLine("Nepostojeca knjiga");
                }
            }

            return knjiga;
        }
        public static Knjige PreuzmiKnjiguAkoJeDostupna()
        {
            Knjige knjiga = null;
            int userInput;

            Console.Clear();
            DaoKnjige.DostupneKnjige();
            Console.WriteLine("Unesite id knjige :");
            string unetiTekst = Console.ReadLine();
            if (int.TryParse(unetiTekst, out userInput) == false)
            {
                Console.WriteLine("Id nije integer");
            }
            else
            {
                userInput = int.Parse(unetiTekst);

                knjiga = KnjigaPreuzmiPoId(userInput);

                if (knjiga == null)
                {
                    Console.WriteLine("Nepostojeca knjiga");
                }
            }

            return knjiga;
        }

        public static Knjige PreuzmiKnjiguAkoJeKodClanaH()
        {
            Knjige knjiga = null;
            int userInput;

            Console.Clear();
            DaoKnjige.IzdateKnjige();
            Console.WriteLine("Unesite id knjige :");
            string unetiTekst = Console.ReadLine();
            if (int.TryParse(unetiTekst, out userInput) == false)
            {
                Console.WriteLine("Id nije integer");
            }
            else
            {
                userInput = int.Parse(unetiTekst);

                knjiga = KnjigaPreuzmiPoId(userInput);

                if (knjiga == null)
                {
                    Console.WriteLine("Nepostojeca knjiga");
                }
            }

            return knjiga;
        }
        public static bool DalJeNekaKnjigaKodClana(Knjige knjiga)
        {
            SqlConnection conn = DaoKonekcija.NewConnection();

            bool izvrseno;

            string sQuerry = "select id from knjige where id_clana is not null and id=@id";
            SqlCommand cmd = new SqlCommand(sQuerry, conn);
            cmd.Parameters.AddWithValue("id", knjiga.Id);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
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

        public static bool DaliJeKnjigaUBiblioteci(Knjige knjiga)
        {
            SqlConnection conn = DaoKonekcija.NewConnection();

            bool izvrseno;

            string sQuerry = "select id from knjige where id_clana is null and id=@id";
            SqlCommand cmd = new SqlCommand(sQuerry, conn);
            cmd.Parameters.AddWithValue("id", knjiga.Id);
            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
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

        public static bool TestBrisanja(Knjige obrisi)
        {
            //POKUSAVAM DA OBRISEM KNJIGU
            SqlConnection conn = DaoKonekcija.NewConnection();

            bool izvrseno = false;
            string sQuerry = "delete Knjige where  id=@id";
            SqlCommand cmd = new SqlCommand(sQuerry, conn);
            cmd.Parameters.AddWithValue("id", obrisi.Id);

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


    }
}
