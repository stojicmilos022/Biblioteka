using Biblioteka.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.DAO
{
    public class DaoKnjige
    {
        public static List<Knjige> PreuzmiKnjigeIzSql()
        {
            SqlConnection connection = DaoKonekcija.NewConnection();

            List<Knjige> sveKnjige = new List<Knjige>();

            string sQuerry = "select Knjige.id,imeknjige,autor,GodinaIzdavanja,id_clana,ime,prezime from Knjige left join ClanoviBiblioteke on Knjige.Id_Clana =ClanoviBiblioteke.Id";

            SqlCommand cmd = new SqlCommand(sQuerry, connection);

            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                int id = (int)rdr["Id"];
                string imeknjige = (string)rdr["imeknjige"];
                string autor = (string)rdr["autor"];
                int godinaIzd = (int)rdr["godinaIzdavanja"];
                ClanBiblioteke clan;

                if (rdr.IsDBNull(4))
                {
                    clan = null;
                }
                else
                {
                    int id_clana = (int) rdr[4];
                    string ime = (string)rdr[5];
                    string prezime= (string)rdr[6];
                    clan = new ClanBiblioteke(id_clana, ime, prezime);
                }

                Knjige novaKnjiga = new Knjige(id, imeknjige, autor,godinaIzd,clan);
                sveKnjige.Add(novaKnjiga);
            }
            rdr.Close();
            connection.Close();
            return sveKnjige;
        }

        




        public static void DostupneKnjige()
        {
            List<Knjige> sveDostupne = PreuzmiKnjigeIzSql();
            Console.WriteLine("\tSvi knjige koje biblioteka poseduje :");
            Console.WriteLine("\t____________________________________________________________________________________________________");
            Console.WriteLine("\t{0,-4} | {1,-30} | {2,-25} | {3,5} ", "Id", "ImeKnjige", "Autor", "Godina");
            Console.WriteLine("\t____________________________________________________________________________________________________");

            foreach (Knjige k in sveDostupne)
            {
                if (k.Lokacija == null)
                {
                    Console.WriteLine(k.TabelarniPrikazKnjigaSkraceni());
                }
                else { continue; }
            }
        }

        public static void IzdateKnjige()
        {
            List<Knjige> sveDostupne = PreuzmiKnjigeIzSql();
            Console.WriteLine("\tSvi knjige koje biblioteka poseduje :");
            Console.WriteLine("\t____________________________________________________________________________________________________");
            Console.WriteLine("\t{0,-4} | {1,-30} | {2,-25} | {3,5} ", "Id", "ImeKnjige", "Autor", "Godina");
            Console.WriteLine("\t____________________________________________________________________________________________________");

            foreach (Knjige k in sveDostupne)
            {
                if (k.Lokacija != null)
                {
                    Console.WriteLine(k.TabelarniPrikazKnjigaSkraceni());
                }
                else { continue; }
            }
        }

        public static void IzlistajSveKnjige()
        {
            List<Knjige> sveKnjige = DaoKnjige.PreuzmiKnjigeIzSql();
            Console.WriteLine("\tSvi knjige koje biblioteka poseduje :");
            Console.WriteLine("\t____________________________________________________________________________________________________");
            Console.WriteLine("\t{0,-4} | {1,-30} | {2,-25} | {3,5} | {4,-15}","Id","ImeKnjige","Autor","Godina","Trenutno kod clana");
            Console.WriteLine("\t____________________________________________________________________________________________________");
            foreach (Knjige k in sveKnjige)
            {
                Console.WriteLine(k.TabelarniPrikazKnjiga());
            }
            Console.WriteLine();
        }

        public static void KnjigaDodaj()
        {
            Knjige novaKnjiga = KnjigeHelp.ProveraUnosaKnjige();
            if (novaKnjiga != null)
            {
                bool uspesno = KnjigeHelp.TestUnosaKnjige(novaKnjiga);

                if (uspesno == false)
                {
                    Console.WriteLine("Greska pri unosu clana...");
                }
                else
                {
                    Console.WriteLine("Knjiga {0} je uspesno dodata", novaKnjiga.TabelarniPrikazKnjigaSkraceni());
                }
            }
        }



        public static void ObrisiKnjigu()
        {
            Knjige knjigaZaBrisanje = KnjigeHelp.PreuzmiKnjiguAkoPostoji();

            if (knjigaZaBrisanje == null)
            {
                return;
            }

            if (KnjigeHelp.DalJeNekaKnjigaKodClana(knjigaZaBrisanje) == true)
            {
                Console.WriteLine("Nije moguce obrisati knjigu kod koga je trenutno neka knjiga...");
            }

            bool obrisano = KnjigeHelp.TestBrisanja(knjigaZaBrisanje);
            if (obrisano == false)
            {
                Console.WriteLine("Doslo je do greske prilikom brisanja knjige");
            }
            else
            {
                Console.WriteLine("Knjiga  : {0} od autora {1} je obrisana iz baze podataka", knjigaZaBrisanje.ImeKnjige, knjigaZaBrisanje.Autor);
            }
        }

        internal static void IzlistajSveKnjigeZaNekogAutora()
        {
            List<Knjige> sveOdAutora = PreuzmiKnjigeIzSql();
            IzlistajSveKnjige();
            Console.WriteLine();
            Console.WriteLine("Unesi autora za pretragu (unesi deo imena autora ili puno ime):");
            string tempAutor=Console.ReadLine();
            Console.WriteLine("\tSvi knjige koje biblioteka poseduje  od autora koji sadrzi :",tempAutor);
            Console.WriteLine("\t____________________________________________________________________________________________________");
            Console.WriteLine("\t{0,-4} | {1,-30} | {2,-25} | {3,5} ", "Id", "ImeKnjige", "Autor", "Godina");
            Console.WriteLine("\t____________________________________________________________________________________________________");

            foreach (Knjige k in sveOdAutora)
            {
                if (k.Autor.Contains(tempAutor))
                {
                    Console.WriteLine(k.TabelarniPrikazKnjigaSkraceni());
                }
                else { continue; }
            }
        }

        internal static bool DodeliClanu(Knjige dodeliKnjigu, ClanBiblioteke dodeliClanu)
        {
            SqlConnection conn=DaoKonekcija.NewConnection();
            if (dodeliKnjigu == null || dodeliClanu == null)
            {
                return false;
            }

            bool retVal;

            string sqlU = "update knjige set id_clana=@id_Clana where knjige.id=@id";
            SqlCommand cmd=new SqlCommand(sqlU, conn);
            cmd.Parameters.AddWithValue("id_Clana", dodeliClanu.id);
            cmd.Parameters.AddWithValue("id", dodeliKnjigu.Id);

            try
            {
                cmd.ExecuteNonQuery();
                retVal = true;
            }
            catch
            {
                retVal = false;
            }
            conn.Close();
            return retVal;
        }

        internal static bool OslobodiKnjigu(Knjige knjigaPreuzmi)
        {
            SqlConnection conn = DaoKonekcija.NewConnection();

            if(knjigaPreuzmi == null)
            {
                return false; 
            }

            bool retVal;

            string sql = "update knjige set id_clana=NUll where id=@id";
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("id",knjigaPreuzmi.Id);

            try
            {
                cmd.ExecuteNonQuery();

                retVal = true;
            }
            catch
            {
                retVal= false;  
            }
            conn.Close();
            return retVal;
        }
    }
}
