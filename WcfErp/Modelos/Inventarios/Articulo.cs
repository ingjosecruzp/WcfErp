using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfErp.Modelos.Compras;
using WcfErp.Modelos.Generales;

namespace WcfErp.Modelos.Inventarios
{
    public class Articulo : ModeloBase<Articulo>
    {
        public string Clave { get; set; }
        public GrupoUnidad GrupoUnidad { get; set; }
        public GrupoComponente GrupoComponente { get; set; }
        public SubgrupoComponente SubGrupoComponente { get; set; }
        public string Activo { get; set; }
        public Marca Marca { get; set; }
        public string Inventariable { get; set; }
        public string TipoSeguimiento { get; set; }
        public Unidad UnidadInventario { get; set; }
        public Unidad UnidadVenta { get; set; }
        public Unidad UnidadCompra { get; set; }
        public string Modelo { get; set; }
        public string NoParte { get; set; }
        public Proveedor Proveedor { get; set; }
        public List<CodigosBarra> CodigosBarra { get; set; }
        public List<ConfiguracionesAlmacen> ConfiguracionesAlmacen { get; set; }

        //Datos Particulares
        public double Pureza { get; set; }
        public double Peso { get; set; }
        public Paises Paises { get; set; }
        public List<Imagen> Imagen { get; set; }

        public Articulo()
        {
            this.CodigosBarra = new List<CodigosBarra>();
            this.ConfiguracionesAlmacen = new List<ConfiguracionesAlmacen>();
            this.Imagen = new List<Imagen>();
        }

        protected override Articulo addValues(Articulo item, EmpresaContext db)
        {
            try
            {
                item.GrupoComponente = db.GrupoComponente.get(item.GrupoComponente._id, db);
                item.GrupoUnidad = db.GrupoUnidad.get(item.GrupoUnidad._id, db);
                item.Marca = item.Marca.id == "" ? null : db.Marca.get(item.Marca._id, db);
                item.SubGrupoComponente = db.SubgrupoComponente.get(item.SubGrupoComponente._id, db);

                item.UnidadCompra = item.GrupoUnidad.GrupoUnidadDetalle.Where(i => i.UnidadEquivalente._id == item.UnidadCompra._id).Select(x => x.UnidadEquivalente).FirstOrDefault();
                item.UnidadVenta = item.GrupoUnidad.GrupoUnidadDetalle.Where(i => i.UnidadEquivalente._id == item.UnidadVenta._id).Select(x => x.UnidadEquivalente).FirstOrDefault();
                item.UnidadInventario = item.GrupoUnidad.GrupoUnidadDetalle.Where(i => i.UnidadEquivalente._id == item.UnidadInventario._id).Select(x => x.UnidadEquivalente).FirstOrDefault();

                //item.Pureza = item.Pureza.id == "" ? null : db.Pureza.get(item.Pureza._id, db);
                item.Paises = item.Paises.id == "" ? null : db.Paises.get(item.Paises._id, db);

                foreach (CodigosBarra codigo in item.CodigosBarra)
                {
                    codigo.Unidad = db.Unidad.get(codigo.Unidad.id, db);
                }
                
                return item;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}