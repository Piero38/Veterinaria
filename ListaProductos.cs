using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Veterinaria
{
    class ListaProductos
    {
        List<Producto> productos = new List<Producto>();

        public List<Producto> getLista()
        {
            return productos;
        }
        public void agregarProducto(Producto nuevoProducto)
        {
            productos.Add(nuevoProducto);
        }
        public void eliminarProducto(int posicion)
        {
            productos.RemoveAt(posicion);
        }
        public void guardarEnArchivo()
        {
            try
            {
                StreamWriter sw = new StreamWriter("AlmacenVeterinaria.txt");
                foreach (Producto x in productos)
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
                productos = new List<Producto>();
                StreamReader sr = new StreamReader("AlmacenVeterinaria.txt");
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    productos.Add(new Producto(line));
                }
                sr.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Excepcion: " + e.Message);
            }
        }
        public Producto obtenerProducto(int posicion)
        {
            return productos[posicion];
        }
    }
}
