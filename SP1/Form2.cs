using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SP1
{
    public partial class Form2 : Form
    {
        // variable para guardar el nombre del archivo de repuestos
        private string PATH_ARCHIVO;

        public Form2(string Path) // el constructor se modifica para que reciba el nombre del archivo
        {
            InitializeComponent();
            PATH_ARCHIVO = Path;
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            // controlar si el archivo existe
            if(!File.Exists (Application.StartupPath + "\\" + PATH_ARCHIVO))
            {
                MessageBox.Show("No hay datos para mostrar", "Consulta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            // leer el contenido del archivo
            Archivo Repuestos = new Archivo();
            Repuestos.NombreArchivo = PATH_ARCHIVO;
            List<Repuesto> repuestos = Repuestos.ObtenerRepuestosOrdenados();

            // limpiar la grilla
            dgvRepuestos.Rows.Clear();
            // recorrer la lista de repuestos
            foreach (Repuesto repuesto in repuestos)
            {
                // controlar el valor de la Marca de cada repuesto comparado con 
                // el valor seleccionado en el comboBox de las marcas
                if (repuesto.Marca == cmbMarca.SelectedItem.ToString())
                {
                    // controlar el tipo de Origen y los botones de opción activos
                    if (optImportado.Checked && repuesto.Origen == "Importado")
                    {
                        // agregar el repuesto a la grilla
                        dgvRepuestos.Rows.Add(repuesto.Codigo, repuesto.Nombre,
                            repuesto.Marca, repuesto.Origen,
                            repuesto.Precio.ToString());
                    }
                    else
                    {
                        if (optNacional.Checked && repuesto.Origen == "Nacional")
                        {
                            // agregar el repuesto a la grilla
                            dgvRepuestos.Rows.Add(repuesto.Codigo, repuesto.Nombre,
                                repuesto.Marca, repuesto.Origen,
                                repuesto.Precio.ToString());
                        }
                        else
                        {
                            if (optAmbos.Checked) // si se quiere mostrar los 2 orígenes
                            {
                                // agregar el repuesto a la grilla
                                dgvRepuestos.Rows.Add(repuesto.Codigo, repuesto.Nombre,
                                    repuesto.Marca, repuesto.Origen, 
                                    repuesto.Precio.ToString());
                            }
                        }
                    }
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Inicializar(); // estado inicial del formulario
        }

        private void Inicializar()
        {
            // carga de los items en el comboBox de Marcas
            cmbMarca.Items.Clear();
            cmbMarca.Items.Add("Marca A");
            cmbMarca.Items.Add("Marca B");
            cmbMarca.Items.Add("Marca C");
            cmbMarca.SelectedIndex = 0;
            // opción de Origen inicial
            optNacional.Checked = true;
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
