using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PM2E13977.Models
{
    public class Sitio
    {
        [PrimaryKey,AutoIncrement]  
        public int Id { get; set; }

        [MaxLength(100)]
        public string latitud { get; set; }
        public string longitud { get; set; }
        public string descrip { get; set; }

        public byte[] foto { get; set; }
    }
}
