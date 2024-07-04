using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;

namespace ConsoleApp1
{
    public class Program
    {
        private static readonly HttpClient client = new HttpClient();
        public static async Task Main(string[] args)
        {

            //Muestra en la consola la información de los primeros 5 usuarios(id, nombre,correo electrónico, etc.).
            var users = await GetUserAsync();
            for (int i =0;  i < users.Length; i++)
            {
                //usuario
                Console.WriteLine($"{users[i].id}, {users[i].name}, {users[i].email}, {users[i].username},{users[i].phone},{users[i].website}");
                //address
                Console.WriteLine($"{users[i].address.street},{users[i].address.suite},{users[i].address.city},{users[i].address.zipcode}");
                //geo
                Console.WriteLine($"{users[i].address.geo.lat},{users[i].address.geo.lng}");
                //company
                Console.WriteLine($"{users[i].company.name},{users[i].company.catchPhrase},{users[i].company.bs}");
            }

            //Implementa una función que, dado el id de un usuario, muestre en la conso-la la información completa de ese usuario.
            printUser();

        }

        public static async Task<User[]> GetUserAsync()
        {
            try
            {
                var response = await client.GetStringAsync("https://jsonplaceholder.typicode.com/users");

                var users = JsonSerializer.Deserialize<User[]>(response);

                // Verificar si la deserialización fue exitosa
                if (users == null)
                {
                    Console.WriteLine("La deserialización del JSON devolvió null.");
                    return null;
                }

                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null; // O maneja el error según sea necesario
            }

        }

        public static async Task<User> GetUserById(int id)
        {
            var response = await client.GetStringAsync($"https://jsonplaceholder.typicode.com/users/{id}");
            return JsonSerializer.Deserialize<User>(response);
        }

        public static async Task printUser()
        {
            var users = await GetUserById(1);

            //usuario
            Console.WriteLine($"{users.id}, {users.name}, {users.email}, {users.username},{users.phone},{users.website}");
            //address
            Console.WriteLine($"{users.address.street},{users.address.suite},{users.address.city},{users.address.zipcode}");
            //geo
            Console.WriteLine($"{users.address.geo.lat},{users.address.geo.lng}");
            //company
            Console.WriteLine($"{users.company.name},{users.company.catchPhrase},{users.company.bs}");
        }
    }
}
