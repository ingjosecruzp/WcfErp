using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Net;
using System.IO;

namespace WcfErp.Reportes
{
    public class ReportesPFD
    {

        //private System.Collections.ObjectModel.ObservableCollection<Property> _Properties;

        enum Days { Sun = 1, Mon = 2, Tue = 3, Wed = 4, Thu = 5, Fri = 6, Sat = 10 };
        //private string requestUrl = System.Diagnostics.Debugger.IsAttached == true ? "http://10.10.1.171:8080/jasperserver/" : "http://10.10.1.171:8080/jasperserver/";
        //private string requestUrl = System.Diagnostics.Debugger.IsAttached == true ? "http://172.16.5.78:8080/jasperserver/" : "http://172.16.5.78:8080/jasperserver/";
        private string requestUrl = System.Diagnostics.Debugger.IsAttached == true ? "http://localhost:8080/jasperserver/" : "http://localhost:8080/jasperserver/";
        private string token;

        private string _UrlReporte;
        private List<reportParameter> _LstParametros;

        private string _PanelFiltrosState;
        private string _PanelFiltrosVisibility;

        private string _Formato;
        private string _nombreArchivo;

        public string Token
        {
            get
            {
                return token;
            }

            set
            {
                token = value;
            }
        }

        public string UrlReporte
        {
            get
            {
                return _UrlReporte;
            }

            set
            {
                _UrlReporte = value;
            }
        }

        public List<reportParameter> LstParametros
        {
            get
            {
                return _LstParametros;
            }

            set
            {
                _LstParametros = value;
            }
        }

        public string PanelFiltrosState
        {
            get
            {
                return _PanelFiltrosState;
            }

            set
            {
                _PanelFiltrosState = value;
            }
        }

        public string PanelFiltrosVisibility
        {
            get
            {
                return _PanelFiltrosVisibility;
            }

            set
            {
                _PanelFiltrosVisibility = value;
            }
        }

        public string Formato
        {
            get
            {
                return _Formato;
            }

            set
            {
                _Formato = value;
            }
        }

        public string NombreArchivo
        {
            get
            {
                return _nombreArchivo;
            }

            set
            {
                _nombreArchivo = value;
            }
        }

        public ReportesPFD(string urlreporte, List<reportParameter> parametros, string formato, string Archivo)
        {
            LstParametros = new List<reportParameter>();
            UrlReporte = urlreporte;
            LstParametros = parametros;
            Formato = formato;
            NombreArchivo = Archivo;
            GenerarReporte();
        }
        public ReportesPFD(string urlreporte, reportParameter parametro, string formato)
        {
            LstParametros = new List<reportParameter>();
            UrlReporte = urlreporte;
            LstParametros.Add(parametro);
            Formato = formato;

            GenerarReporte();
        }
        public ReportesPFD(string urlreporte, string formato)
        {
            LstParametros = new List<reportParameter>();
            UrlReporte = urlreporte;
            Formato = formato;
            PanelFiltrosVisibility = "Hidden";
            GenerarReporte();
        }
        private void GenerarReporte()
        {
            try
            {
                //Paso 1
                //Adquire un idetificado con jaspersoft
                token = null;
                //token = (string)Peticion("POST", "j_username=" + VariablesGlobales.BdEmpresa + "&j_password=" + VariablesGlobales.PasswordReportes, "application/x-www-form-urlencoded", "", typeof(string), "rest/login");
                token = (string)Peticion("POST", "j_username=jasperadmin&j_password=jasperadmin", "application/x-www-form-urlencoded", "", typeof(string), "rest/login");
                //token = (string)Peticion("POST", "j_username=locmex&j_password=locmex", "application/x-www-form-urlencoded", "", typeof(string), "rest/login");
                //token = (string)Peticion("GET", null , "application/x-www-form-urlencoded", "", typeof(string), "j_spring_security_check?j_username=locmex&j_password=locmex");
                var Extension = "pdf";

                //Genera caracterizticas del reporte
                ReporteModel rpt = new ReporteModel();
                rpt.reportUnitUri = UrlReporte;
                rpt.async = false;
                rpt.freshData = false;
                rpt.saveDataSnapshot = false;
                rpt.outputFormat = Extension;
                rpt.interactive = true;
                rpt.ignorePagination = false;

                //Agrega los parametros al reporte
                reportParameters parametros = new reportParameters();
                parametros.reportParameter = LstParametros;

                rpt.parameters = parametros;

                //Paso 2
                //Mandas la orden para que genere el reporte y retorne el identificador del mismo
                ResponseReportModel RespuestaReporte = new ResponseReportModel();
                JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings { DateFormatHandling = DateFormatHandling.MicrosoftDateFormat };
                //RespuestaReporte = (ResponseReportModel)Peticion("POST", JsonConvert.SerializeObject(rpt, microsoftDateFormatSettings), "application/json", "application/json", typeof(ResponseReportModel), "rest_v2/reportExecutions/?j_username=jasperadmin&j_password=jasperadmin");
                RespuestaReporte = (ResponseReportModel)Peticion("POST", JsonConvert.SerializeObject(rpt, microsoftDateFormatSettings), "application/json", "application/json", typeof(ResponseReportModel), "rest_v2/reportExecutions");

                if (RespuestaReporte.exports[0].status != "ready")
                    throw new Exception(RespuestaReporte.exports[0].status);

                //Paso 3
                //Una vez con el id generado mandas a descargar el reporte
                //Nota se utiliza la clase WebCliente por que la clase HttpWebRequest solo es compatible con texto
                string url = requestUrl + "rest_v2/reportExecutions/" + RespuestaReporte.requestId + "/exports/" + RespuestaReporte.exports[0].id + "/outputResource";


                if (Extension == "pdf")
                {
                    //string rutaTemporal = "c:\\CLG\\" + NombreArchivo + "." + Extension;
                    string rutaTemporal = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "\\webReports\\" + NombreArchivo + "." + Extension;


                    WebClient Cliente = new WebClient();
                    Cliente.Headers.Add("Cookie", token);
                    Cliente.DownloadFile(url, rutaTemporal);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public object Peticion(string Method, string Request, string ContentType, string Accept, Type ResponseType, string URL)
        {

            object result;
            try
            {

                HttpWebRequest httpWebRequest = WebRequest.Create(requestUrl + URL) as HttpWebRequest;
                httpWebRequest.Method = Method;
                httpWebRequest.ContentType = ContentType;
                httpWebRequest.Accept = Accept;

                if (!String.IsNullOrEmpty(token))
                    httpWebRequest.Headers.Add("Cookie", token);

                if (Method == "POST")
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(Request);
                    Stream requestStream = httpWebRequest.GetRequestStream();
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }
                using (HttpWebResponse httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse)
                {
                    if (httpWebResponse.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception(string.Format("Server error (HTTP {0}: {1}).", httpWebResponse.StatusCode, httpWebResponse.StatusDescription));
                    }

                    Stream responseStream = httpWebResponse.GetResponseStream();
                    StreamReader streamReader = new StreamReader(responseStream);

                    string text2 = streamReader.ReadToEnd();

                    object obj;
                    /*if (String.IsNullOrEmpty(token))
                        if (httpWebResponse.ResponseUri.Query == "?error=1")
                            throw new Exception("Login error");
                        else
                        {
                            // obj = httpWebResponse.Headers["Set-Cookie"];
                            string[] cadena = httpWebResponse.ResponseUri.AbsolutePath.Split('=');
                            obj = "JSESSIONID=" + cadena[1];
                        }
                    else
                        obj = JsonConvert.DeserializeObject(text2, ResponseType);*/
                    if (String.IsNullOrEmpty(token))
                        obj = httpWebResponse.Headers["Set-Cookie"];
                    else
                        obj = JsonConvert.DeserializeObject(text2, ResponseType);

                    result = obj;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}