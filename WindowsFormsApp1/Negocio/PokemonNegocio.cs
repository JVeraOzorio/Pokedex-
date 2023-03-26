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
                data.setQuery("insert into POKEMONS(Numero, nombre,descripcion,activo,idTipo,IdDebilidad,UrlImagen) values (" + pokemon.Numero + ",'"+pokemon.Nombre+ "','"+ pokemon.Descripcion+ "',1, @idTipo, @idDebilidad, @UrlImagen)");
                data.setearParametro("@idTipo", pokemon.Tipo.Id);
                data.setearParametro("@idDebilidad", pokemon.Debilidad.Id);
                data.setearParametro("@UrlImagen", pokemon.UrlImagen);
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
                comando.CommandText = "select Numero, Nombre, P.Descripcion, UrlImagen, e.Descripcion as Tipo, D.Descripcion Debilidad, P.IdTipo, P.IdDebilidad, P.Id  from POKEMONS P , ELEMENTOS E , ELEMENTOS D where P.IdTipo = E.Id and D.Id = P.IdDebilidad and P.Activo = 1";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Pokemon aux = new Pokemon();
                    aux.Id = (int)lector["Id"];
                    aux.Numero = lector.GetInt32(0);
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    if (!(lector["UrlImagen"] is DBNull))
                    aux.UrlImagen = (string)lector["UrlImagen"];
                    aux.Tipo = new Elemento();
                    aux.Tipo.Id = (int)lector["IdTipo"];
                    aux.Tipo.Descripcion = (string)lector["Tipo"];
                    aux.Debilidad = new Elemento();
                    aux.Debilidad.Id = (int)lector["IdDebilidad"];
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

        public void modificar(Pokemon pokemon)
        {   
            AccesoDatos data = new AccesoDatos();

            try
            {
                data.setQuery("update Pokemons set Numero = @Numero , Nombre = @Nombre , Descripcion = @Descripcion, UrlImagen = @UrlImagen, IdTipo = @IdTipo, IdDebilidad = @IdDebilidad  where id = @Id");
                data.setearParametro("@Numero", pokemon.Numero);
                data.setearParametro("@Nombre", pokemon.Nombre);
                data.setearParametro("@Descripcion", pokemon.Descripcion);
                data.setearParametro("@UrlImagen", pokemon.UrlImagen);
                data.setearParametro("@IdTipo", pokemon.Tipo.Id);
                data.setearParametro("@IdDebilidad", pokemon.Debilidad.Id);
                data.setearParametro("@Id", pokemon.Id);

                data.executeQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { data.close(); }
        }

        public void eliminar(int iD)
        {
            try
            {
                AccesoDatos data = new AccesoDatos();
                data.setQuery("delete from pokemons where id = @Id");
                data.setearParametro("@Id", iD);
                data.executeQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void elimnarLogica(int Id)
        {
            try
            {
                AccesoDatos data = new AccesoDatos();
                data.setQuery("update POKEMONS set Activo = 0 where id = @Id");
                data.setearParametro("@Id", Id);
                data.ejecutarAccion();

            }
            catch (Exception ex)
            {
                throw ex;
               
            }
        }
    }
}
