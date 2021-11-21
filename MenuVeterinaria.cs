using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Veterinaria
{
    public partial class MenuVeterinaria : Form
    {
        public MenuVeterinaria()
        {
            InitializeComponent();
        }
        ListaProductos lista = new ListaProductos();
        ListaVentas lista_ = new ListaVentas();
        ListaUsuarios lista2 = new ListaUsuarios();
        private void MenuVeterinaria_Load(object sender, EventArgs e)
        {
            lista2.cargarDesdeArchivo();
            actualizarComboBoxUsuarios();
            LimpiaryCentrarBoleta();
            panelInforme.Location = panelProductos.Location;
            panelRegistro.Location = panelProductos.Location;
            panelBoleta.Location = panelProductos.Location;
            panelInforme.Size = panelProductos.Size;
            panelRegistro.Size = panelProductos.Size;
            panelBoleta.Size = panelProductos.Size;
            panelBoleta.Visible = false;
            panelInforme.Visible = false;
            panelRegistro.Visible = false;
            panelProductos.Visible = false;
            panelMenus.Visible = false;
            
            panelLogin.Size = this.Size;
            panelLogin.Location = new Point(0, 0);
            panelLogin.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }
        public bool vacio;
        private void validar(Panel panel)
        {
            vacio = false;
            foreach(Control TextBox in panel.Controls)
            {
                if(TextBox is TextBox & TextBox.Text == String.Empty)
                    vacio = true;
            }
            if(vacio==true)
                MessageBox.Show("Falta llenar todos los campos");
        }
        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            validar(panelProductos);
            if (vacio != true)
            {
                Producto nuevoProducto = new Producto();
                nuevoProducto.nombre = txtNombre.Text;
                nuevoProducto.costo = double.Parse(txtCosto.Text);
                nuevoProducto.especie = txtEspecie.Text;
                lista.agregarProducto(nuevoProducto);
                actualizarGrilla();
                actualizarListBox();
                LimpiarYCentrar();
            }
            else
            {
                LimpiarYCentrar();
            }
        }
        
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            lista.eliminarProducto(dGVListProServ.CurrentRow.Index);
            actualizarGrilla();
            actualizarListBox();
        }
        private void actualizarGrilla()
        {
            List<Producto> productos = lista.getLista();
            dGVListProServ.Rows.Clear();
            foreach (Producto p in productos)
            {
                int indiceNuevaFila = dGVListProServ.Rows.Add();
                DataGridViewRow filaNueva = dGVListProServ.Rows[indiceNuevaFila];
                filaNueva.Cells[0].Value = p.nombre;
                filaNueva.Cells[1].Value = "S/ " + p.costo;
                filaNueva.Cells[2].Value = p.especie;
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            lista.guardarEnArchivo();

        }
        private void btnCargar_Click(object sender, EventArgs e)
        {
            lista.cargarDesdeArchivo();
            actualizarGrilla();
            actualizarListBox();
            LimpiarYCentrar();
        }
        private void LimpiarYCentrar()
        {
            txtNombre.Clear();
            txtCosto.Clear();
            txtEspecie.Clear();
            txtNombre.Focus();
        }
        private void actualizarListBox()
        {
            lstProdoServi.Items.Clear();
            List<Producto> productos = lista.getLista();
            foreach (Producto p in productos)
            {
                lstProdoServi.Items.Add(p.nombre);
            }
        }
        /*-----------------------------------------------------------------------------------------*/
        Producto productoSeleccionado;
        private void lstProdoServi_SelectedIndexChanged(object sender, EventArgs e)
        {
            productoSeleccionado = lista.obtenerProducto(lstProdoServi.SelectedIndex);
            lblPrecio.Text = "S/ " + productoSeleccionado.costo.ToString();
        }
        private void btnAgregarVenta_Click(object sender, EventArgs e)
        {
            lbxCantidad.Items.Add(txtCantidad.Text);
            lbxDescripcion.Items.Add(productoSeleccionado.nombre);
            lbxPrecioUnit.Items.Add(productoSeleccionado.costo.ToString());
            lbxSubTotal.Items.Add(SubTotalRecursividad(productoSeleccionado.costo, int.Parse(txtCantidad.Text)).ToString());
            actualizarTotal();
            txtCantidad.Clear();
        }    
        private double SubTotalRecursividad(double costo, int  cantidad)
        {
            if(cantidad == 1)
            {
                return costo;
            }
            else
            {
                return costo + SubTotalRecursividad(costo, cantidad - 1);
            }
            
        }
        private void actualizarTotal()
        {
            double total = 0;
            foreach(object item in lbxSubTotal.Items)
            {
                total += double.Parse(item.ToString());
            }
            txtTotal.Text = total.ToString();
        }
        private void btnNuevo_Click(object sender, EventArgs e)
        {
            LimpiaryCentrarBoleta();
        }
        private void LimpiaryCentrarBoleta()
        {
            cboVendedor.Text = "Elegir un vendedor";
            //Datos del Cliente
            txtNomDue.Clear();
            txtTeleDue.Clear();
            cboDisitrito.Text = cboDisitrito.Items[0].ToString();
            txtNomDue.Focus();
            //Datos de la Mascota
            txtNomMasc.Clear();
            txtEdadMasc.Clear();
            cboEspecie.Text = cboEspecie.Items[0].ToString();
            cboRaza.Items.Clear();
            cboRaza.Enabled = false;
            cboRaza.Text = "";
            //Datos de la Venta
            lbxCantidad.Items.Clear();
            lbxPrecioUnit.Items.Clear();
            lbxDescripcion.Items.Clear();
            lbxSubTotal.Items.Clear();
            txtTotal.Clear();
        }
        private void cboEspecie_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEspecie.SelectedIndex == 0)
            {
                cboRaza.Items.Clear();
                cboRaza.Enabled = false;
            }
            if (cboEspecie.SelectedIndex == 1)
            {
                cboRaza.Enabled = true;
                cboRaza.Items.Clear();
                cboRaza.Items.Add("Labrador");
                cboRaza.Items.Add("Shitzu");
                cboRaza.Items.Add("Salchica");
                cboRaza.Items.Add("Bulldog");
                cboRaza.Items.Add("Pitbull");
                cboRaza.Items.Add("Schnauzer");
                cboRaza.Items.Add("Beagle");
                cboRaza.Items.Add("Poodle");
                cboRaza.Items.Add("Pastor Aleman");
                cboRaza.Items.Add("Chihuahua");
                cboRaza.Items.Add("Otros");
            }
            if (cboEspecie.SelectedIndex == 2)
            {
                cboRaza.Enabled = true;
                cboRaza.Items.Clear();
                cboRaza.Items.Add("Persa");
                cboRaza.Items.Add("Maine Coon");
                cboRaza.Items.Add("Bengala");
                cboRaza.Items.Add("Siames");
                cboRaza.Items.Add("Esfinge");
                cboRaza.Items.Add("Siberiano");
                cboRaza.Items.Add("Azul Ruso");
                cboRaza.Items.Add("Otros");
            }
            if (cboEspecie.SelectedIndex == 3)
            {
                cboRaza.Enabled = true;
                cboRaza.Items.Clear();
                cboRaza.Items.Add("Loro");
                cboRaza.Items.Add("Ninfa");
                cboRaza.Items.Add("Agaporni");
                cboRaza.Items.Add("Perico");
                cboRaza.Items.Add("Canario");
                cboRaza.Items.Add("Paloma");
                cboRaza.Items.Add("Diamante");
                cboRaza.Items.Add("Otros");
            }
        }
        private void btnEliminarVenta_Click(object sender, EventArgs e)
        {
            int indice = lbxDescripcion.SelectedIndex;
            lbxCantidad.Items.RemoveAt(indice);
            lbxDescripcion.Items.RemoveAt(indice);
            lbxPrecioUnit.Items.RemoveAt(indice);
            lbxSubTotal.Items.RemoveAt(indice);
            actualizarTotal();
            txtCantidad.Clear();
        }
        private void btnGuardarVenta_Click(object sender, EventArgs e)
        {
            validar(panelDatos);
            if (vacio != true)
            {
                if (txtTeleDue.TextLength == 9)
                {
                    Venta nuevaVenta = new Venta();
                    nuevaVenta.fechaVenta = DateTime.Now;
                    nuevaVenta.vendedor = cboVendedor.Text;
                    nuevaVenta.nombDue = txtNomDue.Text;
                    nuevaVenta.nomMasc = txtNomMasc.Text;
                    nuevaVenta.Total = double.Parse(txtTotal.Text);
                    lista_.agregarVenta(nuevaVenta);
                    actualizarGrillaVentas();
                    lista_.guardarEnArchivo();
                }
                else
                    MessageBox.Show("Solo se permiten 9 digitos para el Celular");
            }
            
        }
        private void actualizarGrillaVentas()
        {
            List<Venta> ventas= lista_.getLista();
            dgvInformeVentas.Rows.Clear();
            foreach(Venta p in ventas)
            {
                int indiceNuevaFila = dgvInformeVentas.Rows.Add();
                DataGridViewRow filaNueva = dgvInformeVentas.Rows[indiceNuevaFila];
                filaNueva.Cells[0].Value = p.fechaVenta;
                filaNueva.Cells[1].Value = p.vendedor;
                filaNueva.Cells[2].Value = p.nombDue;
                filaNueva.Cells[3].Value = p.nomMasc;
                filaNueva.Cells[4].Value = "S/ " + p.Total;
            }
        }
        public void actualizarComboBoxUsuarios()
        {
            List<Usuario> usuarios = lista2.getLista();
            cboVendedor.Items.Clear();
            foreach(Usuario p in usuarios)
            {
                cboVendedor.Items.Add(p.usuario);
            }
        }
        private void btnImprimir_Click(object sender, EventArgs e)
        {
           
        }
        private void Imprimir(object sender, PrintPageEventArgs e)
        {
            
        }
        /*-----------------------------------------------------------------------------------------*/
        private void btnCargarVentas_Click(object sender, EventArgs e)
        {
            lista_.cargarDesdeArchivo();
            actualizarGrillaVentas();
        }
        /*-----------------------------------------------------------------------------------------*/
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if(txtNuevoUsuario.Text != "" && txtNuevaContraseña.Text != "")
            {
                Usuario nuevoUsuario = new Usuario();
                nuevoUsuario.usuario = txtNuevoUsuario.Text;
                nuevoUsuario.contraseña = txtNuevaContraseña.Text;
                lista2.agregarUsuario(nuevoUsuario);
                actualizarListaUsuarios();
                MessageBox.Show("Usuario Registrado");
                lista2.guardarEnArchivo();
            }
            else
            {
                MessageBox.Show("Rellenar todos los Campos");
            }
        }
        private void btnEliminarRegistro_Click(object sender, EventArgs e)
        {
            int indice = lstUsuarios.SelectedIndex;
            lista2.eliminarUsuario(indice);
            actualizarListaUsuarios();
            lista2.guardarEnArchivo();
        }

        private void actualizarListaUsuarios()
        {
            List<Usuario> usuarios = lista2.getLista();
            lstUsuarios.Items.Clear();
            foreach (Usuario p in usuarios)
            {
                lstUsuarios.Items.Add(p.usuario);                
            }
        }
        private void btnCargarUsuarios_Click(object sender, EventArgs e)
        {
            lista2.cargarDesdeArchivo();
            actualizarListaUsuarios();
        }
        /*-----------------------------------------------------------------------------------------*/
        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuarioIngreso.Text;
            string contraseña = txtContraseñaIngreso.Text;
            Usuario consultar = new Usuario();
            consultar.usuario = usuario;
            consultar.contraseña = contraseña;
            if (lista2.existeUsuario(consultar))
            {   
                panelLogin.Visible = false;
                panelMenus.Visible = true;
            }
            else
            {
                MessageBox.Show("Ingrese un usuario y contraseña correcta");
            }
            txtUsuarioIngreso.Clear();
            txtContraseñaIngreso.Clear();
            txtUsuarioIngreso.Focus();
        }

        private void lblMenuProductos_Click(object sender, EventArgs e)
        {
            panelProductos.Visible = true;
            panelBoleta.Visible = false;
            panelInforme.Visible = false;
            panelRegistro.Visible = false;
        }

        private void lblMenuBoleta_Click(object sender, EventArgs e)
        {
            panelProductos.Visible = false;
            panelBoleta.Visible = true;
            panelInforme.Visible = false;
            panelRegistro.Visible = false;
        }

        private void lblMenuVentas_Click(object sender, EventArgs e)
        {
            panelProductos.Visible = false;
            panelBoleta.Visible = false;
            panelInforme.Visible = true;
            panelRegistro.Visible = false;
        }

        private void lblMenuUsuarios_Click(object sender, EventArgs e)
        {
            panelProductos.Visible = false;
            panelBoleta.Visible = false;
            panelInforme.Visible = false;
            panelRegistro.Visible = true;
        }

        private void lblCerrar_Click(object sender, EventArgs e)
        {
            panelProductos.Visible = false;
            panelBoleta.Visible = false;
            panelInforme.Visible = false;
            panelRegistro.Visible = false;
            panelLogin.Visible = true;
            panelMenus.Visible = false;
        }
    }
}

