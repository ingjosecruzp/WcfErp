using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;
using WcfErp.Modelos.Generales;
using WcfErp.Modelos.Inventarios;
using WcfErp.Modelos.PVenta;
using WcfErp.Modelos.PuntoVenta;

namespace WcfErp.Modelos
{
    public class EmpresaContext : Context
    {
        public virtual MongoClient client { get; set; }
        public virtual IMongoDatabase db { get; set; }
        public EmpresaContext()
        {
            client = new MongoClient(GetConnectionString());
            db = client.GetDatabase(getKeyToken("empresa", "token"));

            var pack = new ConventionPack();
            pack.Add(new IgnoreIfNullConvention(true));
            ConventionRegistry.Register("ignore nulls",
                            pack,
                            t => true);


            foreach (PropertyInfo prop in typeof(EmpresaContext).GetProperties())
            {
                if (prop.Name != "client" && prop.Name != "db")
                {
                    //Type type = Type.GetType(prop.Name.ToString(), true);
                    object instance = Activator.CreateInstance(prop.PropertyType);


                    PropertyInfo propdb = prop.PropertyType.GetProperty("dbMongo");
                    propdb.SetValue(instance, db, null);


                    prop.SetValue(this, instance, null);

                }
            }
        }

        //Generales
        public virtual Almacen Almacen { get; set; }
        public virtual BDEmpresas BDEmpresas { get; set; } // Usuarios
        public virtual Cobrador Cobrador { get; set; }
        public virtual CondicionesDePago CondicionesDePago { get; set; }
        public virtual Departamento Departamento { get; set; }
        public virtual GrupoComponente GrupoComponente { get; set; }
        public virtual GrupoUnidad GrupoUnidad { get; set; }
        public virtual Marca Marca { get; set; }
        public virtual Moneda Moneda { get; set; }
        public virtual Paises Paises { get; set; }
        public virtual Puesto Puesto { get; set; }
        public virtual Pureza Pureza { get; set; }
        public virtual Roles Roles { get; set; } // usuarios
        public virtual SubgrupoComponente SubgrupoComponente { get; set; }
        public virtual TipoCliente TipoCliente { get; set; }
        public virtual TipoComponente TipoComponente { get; set; }
        public virtual Unidad Unidad { get; set; }
        public virtual Usuarios Usuarios { get; set; }  //Usuarios
        public virtual ZonaCliente ZonaCliente { get; set; }
        public virtual Municipio Municipio { get; set; }
        public virtual Estado Estado { get; set; }
        public virtual Estado Clientes { get; set; }

        //Fin Generales

        //Inventarios
        public virtual Articulo Articulo { get; set; }
        public virtual Concepto Concepto { get; set; }
        public virtual InventariosCostos InventariosCostos { get; set; }
        public virtual InventariosSaldos InventariosSaldos { get; set; }
        public virtual MovimientosES MovimientosES { get; set; }
        public virtual Procedencia Procedencia { get; set; }
        public virtual TipoConcepto TipoConcepto { get; set; }
        public virtual Vendedor Vendedor { get; set; }
        public virtual InventarioFisico InventarioFisico { get; set; }

        //Fin Inventarios

        //Punto de Venta
        public virtual FormadeCobro FormadeCobro { get; set; }
        public virtual TipodeCambio TipodeCambio { get; set; }
        public virtual PoliticadeComisiones PoliticadeComisiones { get; set; }
        public virtual Cajas Cajas { get; set; }
        public virtual TipoImpuesto TipoImpuesto { get; set; }
        public virtual Impuestos Impuestos { get; set; }
        public virtual Cajeros Cajeros { get; set; }
        //Fin Punto de Venta



    }
}
