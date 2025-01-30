using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using WebMongoI.Models;

namespace WebMongoI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Inserir : ControllerBase
    {

        //public void InserirDados()
        //{
        //    // Configurar conexão
        //    var connectionString = "mongodb+srv://fernandoxxxxxx@cluster1.iahfv.mongodb.net/?retryWrites=true&w=majority";
        //    var databaseName = "mongodbVSCodePlaygroundDB";
        //    var collectionName = "sales";

        //    var context = new MongoDbContext(connectionString, databaseName);
        //    var salesCollection = context.GetCollection<Sale>(collectionName);

        //    // Criar um novo documento
        //    var newSale = new Sale
        //    {
        //        Item = "Camera Nikon d7100",
        //        Quantity = 1,
        //        Price = 1100.0,
        //        Date = DateTime.UtcNow
        //    };

        //    // Inserir no MongoDB
        //    salesCollection.InsertOne(newSale);

        //    Console.WriteLine("Documento inserido com sucesso!");
        //}
    }


}
