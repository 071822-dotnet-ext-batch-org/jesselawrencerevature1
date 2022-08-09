using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyApplication
{
    class Program
    {
        HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            Program program = new Program();
            await program.GetTodoItems();

        }         

        private async Task GetTodoItems()
        {
            string response = await client.GetStringAsync(
                "http://api.icndb.com/jokes/random/3?firstName=Mark&lastName=Moore");
                //http://api.icndb.com/jokes/15?firstName=Sally&lastName=Hednrix");

            Console.WriteLine(response);
        }



    }                       
}

