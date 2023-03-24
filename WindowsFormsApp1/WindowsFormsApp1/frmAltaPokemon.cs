using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;
namespace WindowsFormsApp1
{
    public partial class frmAltaPokemon : Form
    {
        public frmAltaPokemon()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                Pokemon pokemon= new Pokemon();
                pokemon.Numero = int.Parse(txbNumero.Text);
                pokemon.Nombre = txbNombre.Text;
                pokemon.Descripcion = txbDescripcion.Text;

                PokemonNegocio negocio = new PokemonNegocio();
                negocio.agregar(pokemon);
                MessageBox.Show("Pokemon agregado");
                Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
    }
}
