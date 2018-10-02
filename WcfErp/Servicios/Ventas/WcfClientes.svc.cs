using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WcfErp.Modelos.Ventas;

namespace WcfErp.Servicios.Ventas
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "WcfClientes" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select WcfClientes.svc or WcfClientes.svc.cs at the Solution Explorer and start debugging.
    public class WcfClientes :  ServiceBase<Clientes>,IWcfClientes
    {
        /*public Clientes add(Clientes item)
        {
            try
            {
                MongoClient client = new MongoClient();
                IMongoDatabase db = client.GetDatabase("PAMC861025DB7");
                
                IMongoCollection<Clientes> CollectionClientes = db.GetCollection<Clientes>("Clientes");
                //Clientes cliente = CollectionClientes.Find(FilterDefinition<Clientes>.Empty).Single();

                CollectionClientes.InsertOne(item);

                return item;
            }
            catch (Exception ex)
            {

                return null;
            }
        }*/

        public List<Clientes> all()
        {
            try
            {
                MongoClient client = new MongoClient();
                IMongoDatabase db = client.GetDatabase("PAMC861025DB7");

                IMongoCollection<Clientes> CollectionClientes = db.GetCollection<Clientes>("Clientes");


                //List<Clientes> LstCliente = CollectionClientes.Find<Clientes>().
                List<Clientes> LstClientes =CollectionClientes.AsQueryable().ToList();
                //CollectionClientes.findall();

                //List<Clientes> LstCliente=CollectionClientes.Find().ToList();
                return LstClientes;
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        public Clientes delete(string id)
        {
            throw new NotImplementedException();
        }

        public Clientes get(string id)
        {
            try
            {
                //ObjectId ClienteId = ObjectId.Parse(id);

                MongoClient client = new MongoClient();
                IMongoDatabase db = client.GetDatabase("PAMC861025DB7");

                IMongoCollection<Clientes> CollectionClientes = db.GetCollection<Clientes>("Clientes");

                //var filter = Builders<Clientes>.Filter.Eq(x => x.name, "system")

                Clientes cliente = CollectionClientes.Find<Clientes>(d => d._id == id).FirstOrDefault();

                return cliente;

            }
            catch (Exception)
            {

                return null;
            }
        }


        public Clientes update(Clientes item,string id)
        {
            try
            {
                /*ObjectId ClienteId = ObjectId.Parse(id);

                item._id = ClienteId;

                MongoClient client = new MongoClient();
                IMongoDatabase db = client.GetDatabase("PAMC861025DB7");

                IMongoCollection<Clientes> CollectionClientes = db.GetCollection<Clientes>("Clientes");

                var filter = Builders<Clientes>.Filter.Eq(s => s._id, ClienteId);

                var result = CollectionClientes.ReplaceOne(filter, item);*/

                return null;
            }
            catch (Exception ex)
            {

                return null;
            }

        }
    }
}
