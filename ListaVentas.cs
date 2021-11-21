using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Veterinaria
{
    class ListaVentas
    {
        List<Venta> ventas = new List<Venta>();
        public void agregarVenta(Venta ventaNueva)
        {
            ventas.Add(ventaNueva);
        }
        public List<Venta> getLista()
        {
            return ventas;
        }
        public void guardarEnArchivo()
        {
            try
            {
                StreamWriter sw = new StreamWriter("InformeVentas.txt",true);
                foreach (Venta x in ventas)
                {
                    sw.WriteLine(x.DataArchivo);
                }
                sw.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Excepcion: " + e.Message);
            }
        }
        public void cargarDesdeArchivo()
        {
            try
            {
                ventas = new List<Venta>();
                StreamReader sr = new StreamReader("InformeVentas.txt");
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    ventas.Add(new Venta(line));
                }
                sr.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Excepcion: " + e.Message);
            }
        }
    }
}
