using Cliente.Model;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Servidor
{
    class Program
    {
        static void Main(string[] args)
        {
            string data = string.Empty;
            string tipo = string.Empty;
            bool start = true;
            string data2 = string.Empty;
            int tamanho = 0;
            var msg = new Mensagem();


            Socket listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint connecta = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 6300);

            listen.Connect(connecta);

            while(start)
            {
                byte[] buffer = new byte[1024];
                byte[] bufferEnvio = new byte[1024];

                Console.WriteLine("Qual seu cargo?");
            Console.WriteLine("[1]Vendedor [2]Gerente");
            tipo = Console.ReadLine();
           
            if (tipo == "1")
            {
                msg.codigoOperacao = 0;
                Console.WriteLine("Nome do vendedor");
                msg.nomeVendedor = Console.ReadLine();

                Console.WriteLine("Id da loja (numerico)");
                msg.codigoLoja = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("data da venda");
                msg.dataVenda = Convert.ToDateTime(Console.ReadLine());

                Console.WriteLine("valor vendido");
                msg.valorVendido = Convert.ToDouble(Console.ReadLine());
                msg.dataFinal = DateTime.Now;
                msg.dataInicial = DateTime.Now;

            }
            else if(tipo == "2")
            {
                Console.WriteLine("Escolha a operação");
                Console.WriteLine("[1]Total de vendas de um vendedor");
                Console.WriteLine("[2]Total de vendas de uma loja");
                Console.WriteLine("[3]Total de vendas da rede de lojas em um período");
                Console.WriteLine("[4]Melhor vendedor");
                Console.WriteLine("[5]Melhor loja");
                msg.codigoOperacao = Convert.ToInt32(Console.ReadLine());
                switch (msg.codigoOperacao)
                {
                    case 1:
                        {
                            Console.WriteLine("Nome do vendedor");
                            msg.nomeVendedor = Console.ReadLine();
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Identificação da loja");
                            msg.codigoLoja = Convert.ToInt32(Console.ReadLine());
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Data Inicial");
                            msg.dataInicial = Convert.ToDateTime(Console.ReadLine());
                            Console.WriteLine("Data final");
                            msg.dataFinal = Convert.ToDateTime(Console.ReadLine());
                            break;
                        }
                    default:
                        break;
                }
            }
            else
            {
                Console.WriteLine();
            }

            var dataJson = JsonConvert.SerializeObject(msg);
            bufferEnvio = Encoding.Default.GetBytes(dataJson);

            listen.Send(bufferEnvio);


            tamanho = listen.Receive(buffer, 0, buffer.Length, 0);
            Array.Resize(ref buffer, tamanho);
            data2 = Encoding.Default.GetString(buffer);
            Console.WriteLine("----------------------------");
            Console.WriteLine("Resposta: " + data2);
            Console.WriteLine("#############################");
            Console.WriteLine("[1] para nova operação.");
                var op = Console.ReadLine();
                if (!op.Equals("1"))
                {
                    start = false;
                }
            }
        }
    }
}