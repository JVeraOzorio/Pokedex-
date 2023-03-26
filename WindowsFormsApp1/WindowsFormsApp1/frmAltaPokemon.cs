using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominio;
using Negocio;
namespace WindowsFormsApp1
{
    public partial class frmAltaPokemon : Form
    {   

        private Pokemon pokemon = null;
        public frmAltaPokemon()
        {
            InitializeComponent();
        }
        public frmAltaPokemon(Pokemon pokemon)
        {
            InitializeComponent();
            this.pokemon = pokemon;
            Text = "Modificar Pokemon";
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
                PokemonNegocio negocio = new PokemonNegocio();

            try
            {
                if (pokemon == null)
                {
                    pokemon = new Pokemon();
                }

                pokemon.Numero = int.Parse(txbNumero.Text);
                pokemon.Nombre = txbNombre.Text;
                pokemon.UrlImagen = txbUrlImagen.Text;
                pokemon.Descripcion = txbDescripcion.Text;
                pokemon.Tipo = (Elemento)cmbxTipo.SelectedItem;
                pokemon.Debilidad = (Elemento)cmbxDebilidad.SelectedItem;

                if(pokemon.Id != 0)
                {
                negocio.modificar(pokemon);
                MessageBox.Show("Pokemon modificado");
                Close();
                }
                else
                {
                negocio.agregar(pokemon);
                MessageBox.Show("Pokemon agregado");
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void frmAltaPokemon_Load(object sender, EventArgs e)
        {
            ElementoNegocio element = new ElementoNegocio();
            try
            {
                cmbxTipo.DataSource = element.List();
                cmbxTipo.ValueMember = "Id";
                cmbxTipo.DisplayMember = "Descripcion";
                cmbxDebilidad.DataSource = element.List();
                cmbxDebilidad.ValueMember = "Id";
                cmbxDebilidad.DisplayMember = "Descripcion";
                if(pokemon != null)
                {
                    txbNumero.Text = pokemon.Numero.ToString();
                    txbNombre.Text = pokemon.Nombre;
                    txbDescripcion.Text = pokemon.Descripcion;
                    txbUrlImagen.Text = pokemon.UrlImagen;
                    cargarImagen(pokemon.UrlImagen);
                    cmbxDebilidad.SelectedValue = pokemon.Debilidad.Id;
                    cmbxTipo.SelectedValue = pokemon.Tipo.Id;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                
            }
        }

        private void txbUrlImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txbUrlImagen.Text);
        }
        private void cargarImagen(string imagen)
        {
            try
            {
                pbPokemon.Load(imagen);
            }
            catch (Exception e)
            {
                pbPokemon.Load("https://imgs.search.brave.com/TUFTCoFWtmURY10plUx9jAcbSGjNUNfCMRuGXiHK1TM/rs:fit:474:225:1/g:ce/aHR0cHM6Ly90c2U0/Lm1tLmJpbmcubmV0/L3RoP2lkPU9JUC5Q/bmVoMjJ1eVh3X3JK/OGxlQmFlOFZ3SGFI/YSZwaWQ9QXBp");
            }
        }

       
    }
}
