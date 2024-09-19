using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json; // Adicione o pacote Newtonsoft.Json para manipulação de JSON

namespace ConsoleApp
{
    class Program
    {
        private static readonly HttpClient client = new HttpClient();
        private const string BaseAddress = "http://10.254.22.106:5186/api/clientes"; // Altere conforme a URL da sua API

        static async Task Main(string[] args)
        {
            client.BaseAddress = new Uri(BaseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("Menu de Funções: ");
                Console.WriteLine("1- Cadastrar");
                Console.WriteLine("2- Listar");
                Console.WriteLine("3- Deletar");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        await CadastrarCliente();
                        break;

                    case "2":
                        Console.Clear();
                        await ListarClientes();
                        break;

                    case "3":
                        Console.Clear();
                        await DeletarClientes();
                        break;


                    default:
                        Console.WriteLine("Opção Inválida!");
                        break;
                }
            }
        }

        private static async Task DeletarClientes()
        {
            Console.Write("Digite o ID: ");
            var id = ObterNum(); 

            var url = $"{BaseAddress}/{id}";

            HttpResponseMessage response = await client.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Cliente excluído com sucesso!");
            }
            else
            {
                Console.WriteLine("Erro ao excluir cliente.");
            }

            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }


        private static async Task CadastrarCliente()
        {
            Console.Write("Digite o Nome: ");
            var nome = Console.ReadLine();

            Console.Write("Digite o SobreNome: ");
            var sobreNome = Console.ReadLine();

            var cliente = new { Nome = nome, SobreNome = sobreNome };
            var json = JsonConvert.SerializeObject(cliente);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("", content);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Cliente cadastrado com sucesso!");
            }
            else
            {
                Console.WriteLine("Erro ao cadastrar cliente.");
            }
        }

        private static async Task ListarClientes()
        {
            HttpResponseMessage response = await client.GetAsync("");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var clientes = JsonConvert.DeserializeObject<dynamic>(jsonString);
                foreach (var cliente in clientes)
                {
                    Console.WriteLine($"ID: {cliente.id}, Nome: {cliente.nome}, SobreNome: {cliente.sobreNome}");
                }
            }
            else
            {
                Console.WriteLine("Erro ao listar clientes.");
            }
        }

        private static int ObterNum()
        {
            while (true)
            {
                try 
                { 
                    var num = Console.ReadLine();
                    return Convert.ToInt32(num);
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Digite um numero valido!");
                }
            }
        }
    }
}
