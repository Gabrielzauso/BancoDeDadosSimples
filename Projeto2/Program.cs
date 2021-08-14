using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;



namespace Projeto2
{

    class Program
    {
        

    [Serializable]
        struct cliente

        {

            public string nome;
            public string email;
            public string cpf;

        }

        static void Remover()
        {
            listagem();
            Console.WriteLine("Digite o ID do cliente que deseja excluir:");
            int id = int.Parse(Console.ReadLine());
            if (id >= 0 && id < Clientes.Count)
            {
                Clientes.RemoveAt(id);
                Salvar();
            }
            else
            {
                Console.WriteLine("ID digitado é invalido, tente novamente!");
                Console.ReadLine();
            }
        }

        static void Adicionar()
        {
            cliente cliente = new cliente();
            Console.WriteLine("Cadastro de clientes");
            Console.WriteLine("Digite o nome do cliente: ");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("Digite o email do cliente");
            cliente.email = Console.ReadLine();
            Console.WriteLine("Digite o CPF:");
            cliente.cpf = Console.ReadLine();

            Clientes.Add(cliente);
            Salvar();

            Console.WriteLine("Cadastro concluido!");
            Console.ReadLine();


        }


        static void Carregar()
        {

            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);

            try
            {
                BinaryFormatter enconder = new BinaryFormatter();

                Clientes = (List<cliente>)enconder.Deserialize(stream);

                if (Clientes == null)
                {
                    Clientes = new List<cliente>();
                }

            }
            catch (Exception e)
            {
                Clientes = new List<cliente>();



            }

            stream.Close();
        }
        static void listagem()
        {
            if (Clientes.Count > 0)
            {
                Console.WriteLine("Lista de clientes");
                int i = 0;
                foreach (cliente cliente in Clientes)
                {
                    Console.WriteLine($"ID: {i}");
                    Console.WriteLine($"Nome:{cliente.nome}");
                    Console.WriteLine($"E-mail: {cliente.email}");
                    Console.WriteLine($"CPF: {cliente.cpf}");
                    Console.WriteLine("==================================");
                    i++;

                }

            }
            else
            {
                Console.WriteLine("Não existe nenhum cadastro!");
            }

            Console.ReadLine();
        }
       
        static void Salvar()
        {
            FileStream stream = new FileStream("clients.dat", FileMode.OpenOrCreate);
            BinaryFormatter enconder = new BinaryFormatter();

            enconder.Serialize(stream, Clientes);

            stream.Close();
        }

             

        static List<cliente> Clientes = new List<cliente>(); 
        
        
        
        
        enum Menu { Listagem = 1, Adicionar, Remover, Sair}

        static void Main(string[] args)
        {
            Carregar();
            bool escolheuSair = false;
            while (!escolheuSair)
            {
                Console.WriteLine("Sistema de Clientes - Bem vindo!");
                Console.WriteLine("1-Listagem\n2-Adicionar\n3-Remover\n4-Sair");
                int OpcaoSelecionada = int.Parse(Console.ReadLine());
                Menu Opcao = (Menu)OpcaoSelecionada;

                switch(Opcao)
                {
                    case Menu.Listagem:
                        listagem();
                        break;
                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Sair:

                        escolheuSair = true;
                        break;


                }

                Console.Clear();
                

            }


            Console.WriteLine("Aperte ENTER para sair");
            Console.ReadLine();
        }
    }
}
