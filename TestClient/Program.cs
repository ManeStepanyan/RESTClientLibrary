using System;
using RestClientLibrary;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        { 
            Client<Product> myClient = new Client<Product>("http://localhost:52004/","application/json");
            myClient.parameters = "api/Products/3";          
            var items = myClient.Get();
            if (items != null)
            {
                foreach (var item in items)
                {
                    Console.WriteLine(item.ID+" "+ item.Name+" "+ item.Price);
                }
            }  
        
           // myClient.Post(pr);
        
            Console.ReadLine();
        }
    }
}
