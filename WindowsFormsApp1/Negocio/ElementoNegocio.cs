using Dominio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
   public class ElementoNegocio
    {
        public List<Elemento> List()
        {   
            List<Elemento> list = new List<Elemento>();
            AccesoDatos data = new AccesoDatos();

            try
            {
                data.setQuery("select id, descripcion from ELEMENTOS");
                data.executeQuery();

                while (data.Lector.Read()) 
                {
                    Elemento aux = new Elemento();
                    aux.Id = (int)data.Lector["id"];
                    aux.Descripcion = (String)data.Lector["descripcion"];
                    list.Add(aux);
                }
                return list;
            }
            catch (Exception ex) 
            {
                throw ex;
            }
            finally 
            {
                data.close();
            }

        }
    }
}
