using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using WcfErp.Modelos.Generales;

namespace WcfErp.Modelos
{
    public class UsuarioContext : Context
    {
        public virtual MongoClient client { get; set; }
        public virtual IMongoDatabase db { get; set; }

        public UsuarioContext()
        {
            client = new MongoClient(GetConnectionString());
            db = client.GetDatabase("Usuarios");

            var pack = new ConventionPack();
            pack.Add(new IgnoreIfNullConvention(true));
            ConventionRegistry.Register("ignore nulls",
                            pack,
                            t => true);


            foreach (PropertyInfo prop in typeof(UsuarioContext).GetProperties())
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
        public virtual BDEmpresas BDEmpresas { get; set; }
        public virtual Roles Roles { get; set; }
        public virtual Usuarios Usuarios { get; set; }

    }
}