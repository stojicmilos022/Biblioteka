using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteka.Model
{
    public class ClanBiblioteke
    {
        public int id { get; set; }
        public string Ime { get; set; }

        public string Prezime { get; set; }
        /*
        public ClanBiblioteke(int id, string ime, string prezime)
        {
            this.id = id;
            this.Ime = ime;
            this.Prezime = prezime;
        }
        */
        public ClanBiblioteke( string ime, string prezime)
        {
            this.Ime = ime;
            this.Prezime = prezime;
        }
        public ClanBiblioteke(int id,string ime,string prezime):this(ime,prezime)
        {
            this.id = id;
        }

        public override string ToString()
        {
            return "\tId : " + id + "\tIme : " + Ime + "\tPrezime : " + Prezime;
        }

        public string TabelarniPrikazClanova()
        {
            return string.Format("\t{0,-4} | {1,-15} | {2,-15}", id, Ime, Prezime);
        }

        public string SkracenoClan()
        {
            return Ime + " " + Prezime;
        }

    }
}
