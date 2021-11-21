using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria
{
    class Usuario
    {
        public Usuario() { }
        public string usuario { set; get; }
        public string contraseña { set; get; }
        public string DataArchivo
        {
            get
            {
                return usuario + "-" + contraseña;
            }
        }
        public Usuario(string linea)
        {
            string[] campos = linea.Split('-');
            usuario = campos[0];
            contraseña = campos[1];
        }    
    }
}
