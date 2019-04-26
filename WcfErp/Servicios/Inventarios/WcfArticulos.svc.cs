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

namespace WcfErp.Servicios.Inventarios
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfArticulos" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfArticulos.svc or WcfArticulos.svc.cs at the Solution Explorer and start debugging.
    public class WcfArticulos : ServiceBase<Articulo>, IWcfArticulos
    {
        public Articulo agregarValores(Articulo item)
        {
            try
            {
                MongoClient client = new MongoClient(getConnection());
                IMongoDatabase db = client.GetDatabase(getKeyToken("empresa","token"));

                IMongoCollection<GrupoComponente> Collection_GrupoComponente = db.GetCollection<GrupoComponente>("GrupoComponente");
                IMongoCollection<GrupoUnidad> Collection_GrupoUnidad = db.GetCollection<GrupoUnidad>("GrupoUnidad");
                IMongoCollection<Marca> Collection_Marca = db.GetCollection<Marca>("Marca");
                IMongoCollection<SubgrupoComponente> Collection_SubGrupoComponente = db.GetCollection<SubgrupoComponente>("SubgrupoComponente");
                IMongoCollection<Unidad> Collection_Unidad = db.GetCollection<Unidad>("Unidad");

                IMongoCollection<Pureza> Collection_Pureza = db.GetCollection<Pureza>("Pureza");
                IMongoCollection<Peso> Collection_Peso = db.GetCollection<Peso>("Peso");
                IMongoCollection<Paises> Collection_Paises = db.GetCollection<Paises>("Paises");

                item.GrupoComponente = Collection_GrupoComponente.Find<GrupoComponente>(d => d._id == item.GrupoComponente.id).FirstOrDefault();
                item.GrupoUnidad = Collection_GrupoUnidad.Find<GrupoUnidad>(d => d._id == item.GrupoUnidad.id).FirstOrDefault();
                item.Marca = item.Marca.id=="" ? null : Collection_Marca.Find<Marca>(d => d._id == item.Marca.id).FirstOrDefault();
                item.SubGrupoComponente = Collection_SubGrupoComponente.Find<SubgrupoComponente>(d => d._id == item.SubGrupoComponente.id).FirstOrDefault();

                item.UnidadCompra = item.GrupoUnidad.GrupoUnidadDetalle.Where(i => i.UnidadEquivalente._id == item.UnidadCompra._id).Select(x => x.UnidadEquivalente).FirstOrDefault();
                item.UnidadVenta = item.GrupoUnidad.GrupoUnidadDetalle.Where(i => i.UnidadEquivalente._id == item.UnidadVenta._id).Select(x => x.UnidadEquivalente).FirstOrDefault();
                item.UnidadInventario = item.GrupoUnidad.GrupoUnidadDetalle.Where(i => i.UnidadEquivalente._id == item.UnidadInventario._id).Select(x => x.UnidadEquivalente).FirstOrDefault();

                item.Pureza = item.Pureza.id == "" ? null :Collection_Pureza.Find<Pureza>(d => d._id == item.Pureza.id).FirstOrDefault();
                item.Peso = item.Peso.id == "" ? null : Collection_Peso.Find<Peso>(d => d._id == item.Peso.id).FirstOrDefault();
                item.Paises = item.Paises.id == "" ? null : Collection_Paises.Find<Paises>(d => d._id == item.Paises.id).FirstOrDefault();

                foreach (CodigosBarra codigo in item.CodigosBarra)
                {
                    codigo.Unidad = Collection_Unidad.Find<Unidad>(d => d._id == codigo.Unidad.id).FirstOrDefault();
                }

                GrabarImagen(item);
                return item;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public override Articulo add(Articulo item)
        {
            try
            {
                item = agregarValores(item);
                return base.add(item);
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
                //GrabarImagen(item);
                item = agregarValores(item);
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
