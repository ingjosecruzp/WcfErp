using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using MongoDB.Driver;
using WcfErp.Modelos.Inventarios;
using WcfErp.Modelos.Generales;
using WcfErp.Modelos;

namespace WcfErp.Servicios.Inventarios
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfArticulos" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfArticulos.svc or WcfArticulos.svc.cs at the Solution Explorer and start debugging.
    public class WcfArticulos : ServiceBase<Articulo>, IWcfArticulos
    {
        public override Articulo add(Articulo item)
        {
            try
            {
                EmpresaContext db = new EmpresaContext();

                using (var session = db.client.StartSession())
                {

                    session.StartTransaction();

                    GrabarImagen(item);

                    //Genera la clave del articulo
                    string ArticuloId = AutoIncrement("Articulos",db.db,session).ToString().PadLeft(5, '0');

                    item.Clave = item.GrupoComponente.Clave + item.SubGrupoComponente.Clave + ArticuloId;

                    db.Articulo.add(item, db, session);

                    session.CommitTransaction();
                }
                
                return item;
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            } 
        }
        public List<Articulo> searchLimitIds(string busqueda, string ids)
        {
            try
            {
                /*Este metodo se usa en la el inventario fisico para descartar los elementos que 
                 * ya fueron seleccionados  en el grid
                */
                EmpresaContext db = new EmpresaContext();

                List<Articulo> LstArticulos=db.Articulo.searchLimitIds(busqueda, ids, "_id", db);


                return LstArticulos;
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }
        public override Articulo update(Articulo item, string id)
        {
            try
            {
                GrabarImagen(item);
                return base.update(item, id);
            }
            catch (Exception ex)
            {
                Error(ex, "");
                return null;
            }
        }
        public Articulo delete(string id)
        {
            throw new NotImplementedException();
        }
        public void GrabarImagen(Articulo item)
        {
            try
            {
                foreach (Imagen img in item.Imagen)
                {
                    string[] base64 = img.Source.Split(',');

                    if (base64.Length > 1)
                    {

                        //string path = System.Web.HttpContext.Current.Server.MapPath("/WcfErp/img/");
                        //string path = System.Web.HttpContext.Current.Server.MapPath("/img/");
                        //string path = "c:\\inetpub\\wwwroot\\img\\";
                        string path = System.Diagnostics.Debugger.IsAttached == true ? System.Web.HttpContext.Current.Server.MapPath("/img/") : "c:\\inetpub\\wwwroot\\img\\";

                        string filename = DateTime.Now.ToString("yyyyMMddHHmmssff") + GetFileExtension(base64[1]);

                        File.WriteAllBytes(path + filename, Convert.FromBase64String(base64[1]));

                        img.Source = filename;
                    }
                    else
                    {
                        string path = System.Diagnostics.Debugger.IsAttached == true ? "http://localhost:60493/img/" : "http://localhost/img/";

                        img.Source = img.Source.Replace(path, "");
                        if (item._id == null)
                            img.Source = "NoImagen.jpg";
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string GetFileExtension(string base64String)
        {
            try
            {
                var data = base64String.Substring(0, 5);

                switch (data.ToUpper())
                {
                    case "IVBOR":
                        return ".png";
                    case "/9J/4":
                        return ".jpg";
                    case "AAAAF":
                        return ".mp4";
                    case "JVBER":
                        return ".pdf";
                    case "AAABA":
                        return ".ico";
                    case "UMFYI":
                        return ".rar";
                    case "E1XYD":
                        return ".rtf";
                    case "U1PKC":
                        return ".txt";
                    case "MQOWM":
                    case "77U/M":
                        return ".srt";
                    default:
                        return string.Empty;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string path()
        {
            try
            {
                string path = System.Web.HttpContext.Current.Server.MapPath("/WcfErp/img/");
                return path;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
