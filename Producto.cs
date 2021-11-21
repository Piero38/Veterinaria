using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Veterinaria
{
    class Producto
    {
        public Producto() { }
        public string nombre { get; set; }
        public double costo { get; set; }
        public string especie { get; set; }
        
        public static char SEPARADOR = '-';
        public string DataArchivo {
            get 
            {
                return nombre + SEPARADOR + costo +
                    SEPARADOR + especie.ToString();
            }
        }
        public Producto(string linea)
        {
            string[] campos = linea.Split(SEPARADOR);
            nombre = campos[0];
            costo = double.Parse(campos[1]);
            especie = campos[2];
        }
    }
}
