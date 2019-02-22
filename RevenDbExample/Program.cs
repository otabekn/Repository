using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Linq;

namespace RevenDbExample
{
    class Program
    {
        static void Main(string[] args)
        {
            using (IDocumentStore store = new DocumentStore
            {
                Urls = new[]                        // URL to the Server,
     {                                   // or list of URLs 
        "http://localhost:8080"  // to all Cluster Servers (Nodes)
    },
                Database = "Northwind",             // Default database that DocumentStore will interact with
                Conventions = { }                   // DocumentStore customizations
            })
            {
                store.Initialize();                 // Each DocumentStore needs to be initialized before use.
                using (IDocumentSession session = store.OpenSession())  // Open a session for a default 'Database'
                {
                    var result = session.Query<Product>().Where(mbox => mbox.UnitsInStock == 10).ToList() ;
                    Console.WriteLine(result[0].Name);
                    return;
                    Category category = new Category
                    {
                     Id= Guid.NewGuid().ToString(),                     
                        Name = "Database Category"
                    };

                    session.Store(category);                            // Assign an 'Id' and collection (Categories)
                                                                        // and start tracking an entity

                    Product product = new Product
                    {
                        Id= Guid.NewGuid().ToString(),
                        Name = "RavenDB Database",
                        Category = category.Id,
                        UnitsInStock = 10
                    };

                    session.Store(product);                             // Assign an 'Id' and collection (Products)
                                                                        // and start tracking an entity

                    session.SaveChanges();                              // Send to the Server
                                                                        // one request processed in one transaction
                }                                // This process establishes the connection with the Server
                                                 // and downloads various configurations
                                                 // e.g. cluster topology or client configuration
            }
        }
    }
}
