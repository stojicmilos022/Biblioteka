using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Model
{
    public class Knjige
    {
        public int Id { get; set; }
        public string ImeKnjige { get; set; }
        public string Autor { get; set; }
        public int GodinaIzdavanja { get; set; }
        public ClanBiblioteke  Lokacija { get; set; }

        public Knjige(int id, string imeKnjige, string autor, int godinaIzdavanja, ClanBiblioteke lokacija=null)
        {
            Id = id;
            ImeKnjige = imeKnjige;
            Autor = autor;
            GodinaIzdavanja = godinaIzdavanja;
            Lokacija = lokacija;
        }

        public Knjige(string imeKnjige, string autor, int godinaIzdavanja, ClanBiblioteke lokacija = null)
        {
            
            ImeKnjige = imeKnjige;
            Autor = autor;
            GodinaIzdavanja = godinaIzdavanja;
            Lokacija = lokacija;
        }


        public override string ToString()
        {
            return "Id : " + Id + " ,naziv : " + ImeKnjige + " ,autor : " + Autor + " ,godina izdavanja :  " 
                + GodinaIzdavanja + " trenutno se nalazi kod : " + Lokacija;
        }

        public string TabelarniPrikazKnjiga()
        {
            string trenutnokod = (Lokacija == null) ? "dostupna" : (Lokacija.Ime + " " + Lokacija.Prezime);
            return string.Format("\t{0,-4} | {1,-30} | {2,-25} | {3,-5} | {4,-15}", Id, ImeKnjige, Autor,GodinaIzdavanja,trenutnokod);
        }

        public string TabelarniPrikazKnjigaSkraceni()
        {
            string trenutnokod = (Lokacija == null) ? "dostupna" : "zauzeta";
            return string.Format("\t{0,-4} | {1,-30} | {2,-25} | {3,-5} ", Id, ImeKnjige, Autor, GodinaIzdavanja,trenutnokod);
        }

    }
}
