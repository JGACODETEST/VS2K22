using MongoDB.Bson;
using MongoDB.Driver;
using NET8.Service.Model.Dto;
using System;
using System.Linq;
using NET8.Repository.Entities;
using Microsoft.Extensions.Hosting;

namespace NET8.Service
{
    public class MongoDBService : IService<TestTable1Dto>
    {
        public MongoDBService()
        { }

        public TestTable1 ConvertDtoToTestTable1(TestTable1Dto dto)
        {
            var result = new TestTable1();

            result.Id = dto.Id;
            result.Descripcion = dto.Descripcion;

            return result;
        }

        public TestTable1Dto ConvertTestTable1ToDto(TestTable1 entity)
        {
            var result = new TestTable1Dto();

            result.Id = entity.Id;
            result.Descripcion = entity.Descripcion;

            return result;
        }

        public void Listar(HostApplicationBuilder builder)
        {
            Console.WriteLine("ITEMS MONGODB: ");

            // Replace with your connection string
            const string connectionString = "mongodb://localhost:27017";

            // Create a MongoClient object
            var client = new MongoClient(connectionString);

            // Use the MongoClient to access the server
            var database = client.GetDatabase("TESTMONGODB44");

            // For example, to get a collection from the database
            var collection = database.GetCollection<BsonDocument>("TESTCOLLECTION1");

            var filter = Builders<BsonDocument>.Filter.Empty;
            var documents = collection.Find(filter).ToList();

            foreach (var document in documents)
            {
                Console.WriteLine(document.ToString());
            }
        }

        public TestTable1Dto Grabar(HostApplicationBuilder builder, TestTable1Dto dto)
        {
            TestTable1Dto result = new TestTable1Dto();

            try
            {
                // Replace with your connection string
                const string connectionString = "mongodb://localhost:27017";

                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("TESTMONGODB44");
                var collection = database.GetCollection<BsonDocument>("TESTCOLLECTION1");



                if (dto != null && dto.Id == 0) // Save
                {
                    Console.WriteLine("ITEM MONGODB - SAVE : " + dto.ToString());

                    // TODO Logging info save

                    var newDocument = new BsonDocument {
                                        { "id", dto.Id },
                                        { "descripcion", dto.Descripcion }
                                    };

                    collection.InsertOne(newDocument);

                    //Console.WriteLine("ITEM MONGODB - AFTER SAVE : " + ConvertTestTable1ToDto(testTable1Tmp).ToString());

                    // TODO Logging info save
                }
                else if (dto != null && dto.Id != 0) // Update
                {
                    var testTable1Tmp = Builders<BsonDocument>.Filter.Eq("id", dto.Id.ToString());

                    if (testTable1Tmp != null)
                    {
                        Console.WriteLine("ITEM MONGODB - BEFORE UPDATE : " + testTable1Tmp.ToString());

                        // TODO Logging info before update

                        
                        var update = Builders<BsonDocument>.Update
                            .Set("id", dto.Id)
                            .Set("descripcion", dto.Descripcion);

                        var options = new UpdateOptions { IsUpsert = true };
                        collection.UpdateOne(testTable1Tmp, update, options);

                        Console.WriteLine("ITEM MONGODB - AFTER UPDATE : " + dto.ToString());

                        // TODO Logging info after update
                    }
                    else
                    {
                        result = null;
                        // TODO Logging error
                    }
                }
                else
                {
                    result = null;
                    // TODO Logging error
                }
                



                

            }
            catch (Exception ex)
            {
                result = null;
                // TODO Logging error
            }

            return result;
        }

        public bool Eliminar(HostApplicationBuilder builder, TestTable1Dto dto)
        {
            bool result = false;

            try
            {
                // Replace with your connection string
                const string connectionString = "mongodb://localhost:27017";

                var client = new MongoClient(connectionString);
                var database = client.GetDatabase("TESTMONGODB44");
                var collection = database.GetCollection<BsonDocument>("TESTCOLLECTION1");

                Console.WriteLine("ITEM MONGODB - BEFORE DELETE : " + dto.ToString());

                // TODO Logging info save

                var testTable1Tmp = Builders<BsonDocument>.Filter.Eq("id", dto.Id);

                if (testTable1Tmp != null)
                {
                    Console.WriteLine("ITEM MONGODB - BEFORE DELETE : " + testTable1Tmp.ToString());

                    // TODO Logging info before update


                    // Delete the document.
                    var resultDelete = collection.DeleteOne(testTable1Tmp);
                    //var resultDelete = collection.DeleteMany(testTable1Tmp);

                    if (resultDelete.DeletedCount > 0)
                    {
                        result = true;

                        Console.WriteLine("Document deleted successfully.");
                    }
                    else
                    {
                        result = false;
                        Console.WriteLine("No documents matched the query for deletion.");
                    }                    
                }
                else
                {
                    result = false;
                    // TODO Logging error
                }

                


            }
            catch (Exception ex)
            {
                result = false;
                // TODO Logging error
            }

            return result;
        }
    }
}