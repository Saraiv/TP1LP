using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TP1LP
{
    struct dados
    {
        public string nome;
        public float dinheiro;
        public int quantidade;
    }

    class SuperMercado
    {
        private string nome;
        private List<Cliente> clientes;
        private List<Funcionarios> funcionarios;
        private List<Setor> sectores;
        private List<Produto> produtos;
        private float dinheiro;
        private SortedList<string, Produto> stock;
        private Queue<Cliente> fila;
        private List<dados> dados;

        public List<Cliente> Clientes
        {
            get
            {
                return clientes;
            }

            set
            {
                clientes = value;
            }
        }

        public List<Funcionarios> Funcionarios
        {
            get
            {
                return funcionarios;
            }

            set
            {
                funcionarios = value;
            }
        }

        public List<Setor> Sectores
        {
            get
            {
                return sectores;
            }

            set
            {
                sectores = value;
            }
        }

        public List<Produto> Produtos
        {
            get
            {
                return produtos;
            }

            set
            {
                produtos = value;
            }
        }

        public float Dinheiro
        {
            get
            {
                return dinheiro;
            }

            set
            {
                dinheiro = value;
            }
        }

        public SortedList<string, Produto> Stock
        {
            get
            {
                return stock;
            }

            set
            {
                stock = value;
            }
        }

        public Queue<Cliente> Fila
        {
            get
            {
                return fila;
            }

            set
            {
                fila = value;
            }
        }

        public string Nome
        {
            get
            {
                return nome;
            }

            set
            {
                nome = value;
            }
        }

        internal List<dados> Dados
        {
            get
            {
                return dados;
            }

            set
            {
                dados = value;
            }
        }

        public SuperMercado(string nome, List<Cliente> clientes, List<Funcionarios> funcionarios, List<Setor> sectores, List<Produto> produtos, float dinheiro, Queue<Cliente> fila)
        {
            this.Clientes = clientes;
            this.Funcionarios = funcionarios;
            this.Sectores = sectores;
            this.Produtos = produtos;
            this.Dinheiro = dinheiro;
            this.Fila = fila;
            this.Nome = nome;
        }

        public void writeFile(string filename)
        {
            try
            {
                StreamWriter f = File.CreateText(filename);
                f.WriteLine("Dados:");
                foreach (Produto p in produtos)
                {
                    f.WriteLine(p.Nome);
                    f.WriteLine(p.Preco);
                    f.WriteLine(p.Quantidade);
                }
                f.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void readFile(string filename)
        {
            StreamReader sr;
            string aux;
            try
            {
                sr = new StreamReader(filename);
                aux = sr.ReadLine();
                while ((aux = sr.ReadLine()) != null)
                {
                    aux = sr.ReadLine();
                    float d = float.Parse(aux);
                    dados newd = new dados();
                    string n = newd.nome;
                    newd.dinheiro = d;
                    int q = newd.quantidade;
                    Dados.Add(newd);
                }
                sr.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }


        public void maisUm(Cliente c)
        {
            fila.Enqueue(c);
        }

        public void menosUm(Cliente c)
        {
            fila.Dequeue();
        }

        public bool existeStock(string n, int q)
        {
            bool res = false;
            if (Stock.ContainsKey(n))
            {
                Produto p = getDoStock(n);
                if (p.Quantidade > q)
                {
                    res = false;
                }
            }
            return res;
        }

        public Produto getDoStock(string n)
        {
            int i = Stock.IndexOfKey(n);
            Produto p;
            Stock.TryGetValue(n, out p);
            return p;
        }

        public float valorAPagar(string n, int q)
        {
            float res = 0.0f;
            Produto p = getSeExiste(n);
            if (p != null)
            {
                if (p.Quantidade >= q)
                {
                    res = p.Preco * q;
                }
            }
            return res;
        }

        public Produto getSeExiste(string n)
        {
            Produto res = null;
            foreach (Produto p in produtos)
            {
                if (p.Nome == n)
                {
                    res = p;
                }
            }
            return res;
        }

        public bool existeProduto(string n)
        {
            bool res = false;
            foreach (Produto p in Produtos)
            {
                if (n == p.Nome)
                    res = true;
            }
            return res;
        }

        public bool existeQuantidade(string n, int q)
        {
            bool res = false;
            if (existeProduto(n))
            {
                foreach (Produto p in produtos)
                {
                    if (p.Quantidade >= q && p.Nome == n)
                    {
                        res = true;
                    }

                }
            }

            return res;
        }

        public string procurarP(string n)
        {
            string res = null;
            foreach (Produto p in produtos)
            {
                if (p.Nome == n)
                {
                    res = p.Setor;
                }
            }
            return res;
        }

        public int numeroEmpregados()
        {
            return funcionarios.Count;
        }

    }

    

class Cliente
    {
        private string nome;
        private float dinheiro;

        public Cliente(string nome, float dinheiro)
            
        {
            this.nome = nome;
            this.dinheiro = dinheiro;
        }

        public string Nome
        {
            get
            {
                return nome;
            }

            set
            {
                nome = value;
            }
        }

        public float Dinheiro
        {
            get
            {
                return dinheiro;
            }

            set
            {
                dinheiro = value;
            }
        }

        public void pagar(float v, SuperMercado sw)
        {
            dinheiro -= v;
            sw.Dinheiro += v;
            if(dinheiro < v)
            {
                Console.WriteLine("Não tem dinheiro suficiente!");
            }
        }

    }

    class Funcionarios
    {
        private string nome;

        public Funcionarios(string nome)
        {
            this.nome = nome;
        }

        public string Nome
        {
            get
            {
                return nome;
            }

            set
            {
                nome = value;
            }
        }
        
    }

    class Setor
    {
        private List<Funcionarios> funcionarios;
        private string nome;

        public Setor(List<Funcionarios> funcionarios, string nome)
            
        {
            this.funcionarios = funcionarios;
            this.nome = nome;
          

        }

        public string Nome
        {
            get
            {
                return nome;
            }

            set
            {
                nome = value;
            }
        }

        public List<Funcionarios> Funcionarios
        {
            get
            {
                return funcionarios;
            }

            set
            {
                funcionarios = value;
            }
        }
    }

    class Produto
    {
        protected string nome;
        protected float quantidade;
        protected float preco;
        protected int iva;
        protected string setor;
        protected int ano;
        protected int mes;
        protected int dia;

        public string Nome
        {
            get
            {
                return nome;
            }

            set
            {
                nome = value;
            }
        }

        public float Quantidade
        {
            get
            {
                return quantidade;
            }

            set
            {
                quantidade = value;
            }
        }

        public float Preco
        {
            get
            {
                return preco;
            }

            set
            {
                preco = value;
            }
        }

        public string Setor
        {
            get
            {
                return setor;
            }

            set
            {
                setor = value;
            }
        }

        public int Ano
        {
            get
            {
                return ano;
            }

            set
            {
                ano = value;
            }
        }

        public int Mes
        {
            get
            {
                return mes;
            }

            set
            {
                mes = value;
            }
        }

        public int Dia
        {
            get
            {
                return dia;
            }

            set
            {
                dia = value;
            }
        }

        public virtual float valorAPagar(int quantidade)
        {
            return (quantidade * preco) * iva;
        }

        public Produto(string nome, int quantidade, float preco, string setor, int ano, int mes, int dia)
        {
            this.nome = nome;
            this.quantidade = quantidade;
            this.preco = preco;
            this.setor = setor;
            this.ano = ano;
            this.mes = mes;
            this.dia = dia;
        }

        public bool compararData(int a, int m, int d)
        {
            bool res = false;
            int ano = 2018;
            int mes = 9;
            int dia = 30;
            if (ano < a)
            {
                Console.WriteLine("Dentro de validade");
                res = false;
            }
            else if (ano > a)
            {
                Console.WriteLine("Fora de validade");
                res = true;
            }
            else if (ano == a)
            {
                if (mes < m)
                {
                    Console.WriteLine("Dentro de validade");
                    res = false;
                }
                else if (mes > m)
                {
                    Console.WriteLine("Fora de validade");
                    res = true;
                }
                else if (mes == m)
                {
                    if (dia < d)
                    {
                        Console.WriteLine("Dentro de validade");
                    }
                    res = false;
                }
                else if (dia >= d)
                {
                    Console.WriteLine("Fora de validade");
                    res = true;
                }
            }
            return res;
            }
/*
        class Vinho : Produto
        {
            private float grau;



        }*/

    class Carrinho
    {
        private List<Produto> produtos;
        private float dinheiro;
        private int quantidade;

        public List<Produto> Produtos
        {
            get
            {
                return produtos;
            }

            set
            {
                produtos = value;
            }
        }

        public float Dinheiro
        {
            get
            {
                return dinheiro;
            }

            set
            {
                dinheiro = value;
            }
        }

        public int Quantidade
        {
            get
            {
                return quantidade;
            }

            set
            {
                quantidade = value;
            }
        }

        public Carrinho(List<Produto> produtos, float dinheiro, int quantidade)
        {
            this.Produtos = produtos;
            this.Dinheiro = dinheiro;
            this.Quantidade = quantidade;
        }


        public void retirarProduto(List<Produto> c, Produto p, int q, string n)
        {
            foreach (Produto pr in c)
            {
                if (n == pr.Nome)
                {
                    pr.Quantidade += q;
                }
            }
                c.Remove(p);
            }

        }


    class Program
    {
            public static void readFromFile(string path, List<Produto> p)
            {
                string l;
                try
                {
                    StreamReader r = new StreamReader(path);
                    Produto aux;
                    for (int i = 0; i < p.Count; i++)
                    {
                        aux = p.ElementAt(i);
                        l = r.ReadLine();//Produto
                        l = r.ReadLine();//nome
                        aux.nome = l;
                        l = r.ReadLine();//preço
                        aux.preco = float.Parse(l);
                        l = r.ReadLine();//quantidade
                        aux.quantidade = float.Parse(l);
                        l = r.ReadLine();//ano
                        aux.ano = int.Parse(l);
                        l = r.ReadLine();//mes
                        aux.mes = int.Parse(l);
                        l = r.ReadLine();//dia
                        aux.dia = int.Parse(l);
                    }
                    r.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            static void Main(string[] args)
        {
            float v = 0.0f;
            
            Cliente c1 = new Cliente("Tó", 50.0f);
            Cliente c2 = new Cliente("Nha", 20.0f);

            List<Cliente> clientes = new List<Cliente>();
            clientes.Add(c1);
            clientes.Add(c2);
            

            Funcionarios f1 = new Funcionarios("Marilia");
            Funcionarios f2 = new Funcionarios("Quimzé");
            Funcionarios f3 = new Funcionarios("Jaquim");
            Funcionarios f4 = new Funcionarios("Zézoca");
            Funcionarios f5 = new Funcionarios("André");

            List<Funcionarios> funcionario1 = new List<Funcionarios>();
            funcionario1.Add(f1);

            List<Funcionarios> funcionario2 = new List<Funcionarios>();
            funcionario2.Add(f2);

            List<Funcionarios> funcionario3 = new List<Funcionarios>();
            funcionario3.Add(f3);

            List<Funcionarios> funcionario4 = new List<Funcionarios>();
            funcionario4.Add(f4);

            List<Funcionarios> funcionario5 = new List<Funcionarios>();
            funcionario5.Add(f5);

            List<Funcionarios> funcionarios = new List<Funcionarios>();
            funcionarios.Add(f1);
            funcionarios.Add(f2);
            funcionarios.Add(f3);
            funcionarios.Add(f4);
            funcionarios.Add(f5);

            Produto p1 = new Produto("Frango", 20, 5.0f, "Talho", 2018, 10, 10);
            Produto p2 = new Produto("Pão", 10, 1.0f, "Padaria", 1920, 10, 10);
            Produto p3 = new Produto("Peru", 5, 5.0f, "Talho", 2018, 10, 10);
            Produto p4 = new Produto("Salmão", 15, 5.0f, "Peixaria", 2018, 10, 10);
            Produto p5 = new Produto("Cabaça", 20, 2.0f, "Frutaria", 2018, 10, 10);
            Produto p6 = new Produto("Linguado", 20, 15.0f, "Charcutaria", 2018, 10, 10);

            List<Produto> produtos = new List<Produto>();
            produtos.Add(p1);
            produtos.Add(p2);
            produtos.Add(p3);
            produtos.Add(p4);
            produtos.Add(p5);
            produtos.Add(p6);

            Queue<Cliente> fila = new Queue<Cliente>();

            // Setor s1 = new Setor(funcionario1, "Talho");
            Setor s1 = new Setor(funcionario1, "Talho");
            //Setor s2 = new Setor(funcionario2, "Padaria");
            Setor s2 = new Setor(funcionario2, "Padaria");
            //Setor s3 = new Setor(funcionario3, "Peixaria");
            Setor s3 = new Setor(funcionario3, "Peixaria");
            //Setor s4 = new Setor(funcionario4, "Charcutaria");
            Setor s4 = new Setor(funcionario4, "Charcutaria");
            //Setor s5 = new Setor(funcionario5, "Frutaria");
            Setor s5 = new Setor(funcionario5, "Frutaria");

                List<Setor> sectores = new List<Setor>();
            sectores.Add(s1);
            sectores.Add(s2);
            sectores.Add(s3);
            
           

            

            SuperMercado sw = new SuperMercado("Continente" ,clientes, funcionarios, sectores, produtos, 50.5f, fila);

               

            string ondeestou = Directory.GetCurrentDirectory();
            string filename = ondeestou + "/../../../dadosMB.txt";

                //sw.writeFile(filename);
                sw.readFile(filename);

            sw.maisUm(c1);
            sw.maisUm(c2);

            bool acabar = false;
            while (!acabar)
            {
                Console.Clear();
                Cliente ccc = sw.Fila.Peek();
                Console.Write("Bem vindo ao super mercado " + sw.Nome + ":");
                Console.WriteLine("\t\t\t\t\t Cliente: " + ccc.Nome);
                Console.WriteLine("1 - Procurar o produto");
                Console.WriteLine("2 - Comprar produtos");
                Console.WriteLine("3 - Pagar na caixa");
                Console.WriteLine("4 - Número de empregados");
                Console.WriteLine("5 - Verificar validade");
                Console.WriteLine("0 - Sair\n");
                string escolha = Console.ReadLine();
                Console.Clear();
                switch (escolha)
                {
                    case ("1"):
                    {
                        Console.Clear();
                        bool nada = false;
                        while (nada == false)
                        {
                            Console.WriteLine("Procurar o produto");
                            foreach (Produto p in sw.Produtos)
                            {
                                Console.WriteLine(p.Nome);
                            }
                            Console.WriteLine("\n");
                            Console.WriteLine("Escreva o nome do produto: ");
                            string produto = Console.ReadLine();
                            Console.WriteLine("Produto encontrado no/a: " + sw.procurarP(produto));
                            Console.WriteLine("Deseja continuar? (s / n) ");
                            string r = Console.ReadLine();
                            if (r == "s" || r == "S")
                            {
                                nada = false;
                            }
                            if (r == "n" || r == "N")
                            {
                                nada = true;
                            }
                        }
                    }break;
                  
                    case ("2"):
                        {
                            bool cheio = false;
                            while (!cheio)
                            {
                                Console.Clear();
                                Console.WriteLine("Comprar produtos");
                                //string comprar = Console.ReadLine();
                                foreach (Produto p in sw.Produtos)
                                {
                                    if (p.Quantidade > 0)
                                    {
                                        Console.WriteLine(p.Nome + " preço -> " + p.Preco + "euros");
                                    }
                                }
                                Console.WriteLine("Nada");
                                Console.WriteLine("\n");
                                bool nada = false;
                                while (!nada)
                                {
                                    Console.WriteLine("O que deseja?");
                                    string s = Console.ReadLine();
                                    if(s == "Nada")
                                    {
                                        nada = true;
                                        cheio = true;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Quantos quer?");
                                        string n = Console.ReadLine();
                                        int q = Int32.Parse(n);
                                        if (sw.existeQuantidade(s, q))
                                        {
                                            Console.WriteLine("Aqui tem. Bom proveito.");
                                            v += sw.valorAPagar(s, q);
                                        }
                                        Console.WriteLine("Deseja continuar a compra? (s / n) ");
                                        string r = Console.ReadLine();
                                        if (r == "s" || r == "S")
                                        {
                                            nada = false;
                                        }
                                        if (r == "n" || r == "N")
                                        {
                                            cheio = true;
                                            nada = true;
                                        }
                                        
                                        else
                                        {
                                            Console.WriteLine("Não existe o artigo ou as quantidades que pediu.");
                                        }
                                    }
                                    nada = true;
                                    }
                                }
                                Console.Clear();
                                cheio = true;
                            }break;
                    case ("3"):
                        {
                            bool cheio = false;
                            while (!cheio)
                            {
                                Console.Clear();
                                Console.WriteLine("Pagar na caixa");
                                bool nada = false;
                                while (nada == false)
                                {
                                    Console.WriteLine("Preço total a pagar: " + v);
                                    System.Console.ReadKey();
                                    if (ccc.Dinheiro >= v)
                                    {
                                        ccc.pagar(v, sw);
                                        Console.WriteLine("Bom proveito!");
                                        sw.Fila.Dequeue();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Não tem dinheiro suficiente!");
                                    }
                                    System.Console.ReadKey();
                                    nada = true;
                                }
                                
                                Console.Clear();
                                cheio = true;
                            }
                        }
                        break;

                    case ("4"):
                        {
                            Console.Clear();
                            bool nada = false;
                            while (nada == false)
                            {
                                Console.WriteLine("Número de empregados: " + sw.numeroEmpregados());
                                Console.WriteLine("Voltar ao menu principal? (s / n) ");
                                string r = Console.ReadLine();
                                if (r == "s" || r == "S")
                                {
                                    nada = true;
                                }
                                if (r == "n" || r == "N")
                                {
                                    nada = false;
                                }
                            }
                        }break;

                        case ("5"):
                            {
                                Console.Clear();
                                bool nada = false;
                                while (nada == false)
                                {
                                    Console.WriteLine("Insira o produto em questão: ");
                                    string prod = Console.ReadLine();

                                    if (sw.existeProduto(prod) == true)
                                    {
                                        Produto p = sw.getSeExiste(prod);
                                        p.compararData(p.ano, p.mes, p.dia);
                                    }

                                    Console.WriteLine("Voltar ao menu principal? (s / n) ");
                                    string r = Console.ReadLine();
                                    if (r == "s" || r == "S")
                                    {
                                        nada = true;
                                    }
                                    if (r == "n" || r == "N")
                                    {
                                        nada = false;
                                    }
                                }
                            }
                            break;

                        case ("0"):
                        {
                            acabar = true;
                            sw.Fila.Dequeue();
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("\nErro!!\n");
                            sw.Fila.Dequeue();
                            System.Console.ReadKey();
                        }
                        break;
                }
            }
        }
    }
}
}
