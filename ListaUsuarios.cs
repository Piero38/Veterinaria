using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Veterinaria
{
    class ListaUsuarios
    {
        List<Usuario> usuarios = new List<Usuario>();
        public void agregarUsuario(Usuario nuevoUsuario)
        {
            usuarios.Add(nuevoUsuario);
        }
        public void eliminarUsuario(int indice)
        {
            usuarios.RemoveAt(indice);
        }
        public List<Usuario> getLista()
        {
            return usuarios;
        }
        public void guardarEnArchivo()
        {
            try
            {
                StreamWriter sw = new StreamWriter("Usuarios.txt");
                foreach (Usuario x in usuarios)
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

                usuarios = new List<Usuario>();
                StreamReader sr = new StreamReader("Usuarios.txt");
                string linea;
                while ((linea = sr.ReadLine()) != null)
                {
                    usuarios.Add(new Usuario(linea));
                }
                sr.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Excepcion: " + e.Message);
            }
        }
        public bool existeUsuario(Usuario usuarioComprobar)
        {
            foreach (Usuario aux in usuarios)
            {
                if (aux.usuario == usuarioComprobar.usuario)
                {
                    if (aux.contraseña == usuarioComprobar.contraseña)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
