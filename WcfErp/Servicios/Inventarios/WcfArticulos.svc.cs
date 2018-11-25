﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Inventarios;

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
                GrabarImagen(item);
                return base.add(item);
            }
            catch (Exception ex)
            {

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
            catch (Exception)
            {

                throw;
            }
        }
        public Articulo delete(string id)
        {
            throw new NotImplementedException();
        }
        public void GrabarImagen(Articulo item)
        {
            foreach (Imagen img in item.Imagen)
            {
                string[] base64 = img.Source.Split(',');

                if (base64.Length > 1)
                {

                    string path = System.Web.HttpContext.Current.Server.MapPath("/WcfErp/img/");

                    string filename = DateTime.Now.ToString("yyyyMMddHHmmssff") + GetFileExtension(base64[1]);

                    File.WriteAllBytes(path + filename, Convert.FromBase64String(base64[1]));

                    img.Source = filename;
                }
                else
                    if(item._id == null)
                        img.Source = "NoImagen.jpg";
            }
        }
        public string GetFileExtension(string base64String)
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
