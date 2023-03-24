using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;

namespace Negocio

{
    public class PokemonNegocio
    {
        public void agregar(Pokemon pokemon)
        {   
            AccesoDatos data = new AccesoDatos();

            try
            {
                data.setQuery("insert into POKEMONS(Numero, nombre,descripcion,activo,idTipo,IdDebilidad) values (" + pokemon.Numero + ",'"+pokemon.Nombre+ "','"+ pokemon.Descripcion+ "',1, @idTipo, @idDebilidad)");
                data.setearParametro("@idTipo", pokemon.Tipo.Id);
                data.setearParametro("@idDebilidad", pokemon.Debilidad.Id);
                data.ejecutarAccion();
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {
                data.close();
            }   
        }

        public List<Pokemon> listar()
        {
            List<Pokemon> lista = new List<Pokemon>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=POKEDEX_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select Numero, Nombre, P.Descripcion, UrlImagen, e.Descripcion as Tipo, D.Descripcion Debilidad from POKEMONS P , ELEMENTOS E , ELEMENTOS D where P.IdTipo = E.Id and D.Id = P.IdDebilidad";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Numero = lector.GetInt32(0);
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    aux.UrlImagen = (string)lector["UrlImagen"];
                    aux.Tipo = new Elemento();
                    aux.Tipo.Descripcion = (string)lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Descripcion = (string)lector["Debilidad"];
                    lista.Add(aux);
                }

                conexion.Close();
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
