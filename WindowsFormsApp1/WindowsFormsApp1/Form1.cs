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
    public partial class Form1 : Form
    {
        private List<Pokemon> listaPokemon;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            load();
        }

       private void load()
        {
            PokemonNegocio negocio = new PokemonNegocio();
            listaPokemon = negocio.listar();
            dataGridView1.DataSource = listaPokemon;
            cargarImagen(listaPokemon[0].UrlImagen);
            dataGridView1.Columns["UrlImagen"].Visible = false;
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            Pokemon seleccionado = (Pokemon)dataGridView1.CurrentRow.DataBoundItem;
            cargarImagen(seleccionado.UrlImagen);
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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            frmAltaPokemon alta = new frmAltaPokemon();
            alta.ShowDialog();
            load();
        }
    }
}
