using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    /*
     * Software para um Bar Alternativo
    -> Funcionários
    -> Produtos
    -> Clientes
    -> Quartos
    -> Serviços
        -> Balcão: cafés e derivados, bebidas espirituosas ...;
        -> Palco: striptease, stand ups, circo;
        -> Quartos: confessionário, 1 vs 1, 1 vs M, orgia, BDSM, exóticas;
        -> Funcionários: lapdances, alimentação, limpeza, segurança, DJ;
*/

    struct Palco
    {
        public int maximo;
        public bool varão;
        public int nPessoas;
        public List<Funcionarios> dançarinos;
    }


    class BAR
    {
        #region Variaveis
        //---------------------------------------- ( Variaveis ) ----------------------------------------\\

        private float dinheiroBar;
        private List<Cliente> clientes;
        private List<Funcionarios> funcionarios;
        private List<Produto> produtos;
        private Quarto[] quartos;
        private Palco palco;
        #endregion

        //---------------------------------------- ( Métodos ) ----------------------------------------\\
        #region GETS & SETS
        public float DinheiroBar
        {
            get { return dinheiroBar; }
            set { dinheiroBar = value; }

        }
        private List<Cliente> Clientes
        {
            get { return clientes; }
            set { clientes = value; }
        }

        private List<Funcionarios> Funcionários
        {
            get { return funcionarios; }
            set { funcionarios = value; }
        }

        private List<Produto> Produtos
        {
            get { return produtos; }
            set { produtos = value; }
        }

        private Quarto[] Quartos
        {
            get { return quartos; }
            set { quartos = value; }
        }

        public void furaha(Cliente c, BAR b) //1v1
        {
            bool jaEscolheu = false;
            foreach (Quarto q in b.Quartos)
            {
                if (q.Disponiblidade && jaEscolheu = false && c.DinheiroCli>=q.Preço)
                {
                    Console.WriteLine("\n Nic Nic avec moi? Hapa? \n");
                    String resposta = Console.ReadLine();
                    if(resposta == "sim")
                    {
                        q.Funcionarios.Add(this);
                        q.Clientes.Add(c);
                        q.Disponiblidade = false;
                        jaEscolheu = true;
                        c.pagar(q.Preço, b);
                    }
                }
            }
        }

        #endregion

        //---------------------------------------- ( Construtores ) ----------------------------------------\\



        public BAR(float dinheiroBar, List<Cliente> clientes, List<Funcionarios> funcionarios, List<Produto> produtos, Quarto[] quartos)
        {
            this.dinheiroBar = dinheiroBar;
            this.clientes = clientes;
            this.funcionarios = funcionarios;
            this.produtos = produtos;
            this.quartos = quartos;
        }

        /// <summary>
        /// Verifica se um produto com nome "n" existe em stock
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>

        public bool existeProduto(string n)
        {
            bool res = false;

            foreach (Produto p in produtos)
            {
                if (n == p.NomeProd)
                {
                    res = true;
                }
            }

            return res;
        }

        /// <summary>
        /// Verifica se um produto com nome "n" existe nas quantidades q
        /// </summary>
        /// <param name="n"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public bool existeQuantidade(string n, int q)
        {
            bool res = false;

            if (existeProduto(n) == true)
            {
                foreach (Produto p in produtos)
                {
                    if (q <= p.Quantidade && p.NomeProd == n)
                    {
                        res = true;
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// Retira do stock a quantidade q do produto n
        /// </summary>
        /// <param name="n"></param>
        /// <param name="q"></param>
        public void daProduto(string n, int q)
        {
            if (existeQuantidade(n, q) == true)
            {
                foreach (Produto p in produtos)
                {
                    if (n == p.NomeProd)
                    {
                        p.Quantidade -= q;
                    }
                }
            }
        }

        /// <summary>
        /// Calcula o valor a pagar de um produto n com quantidades q
        /// </summary>
        /// <param name="n"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public float valorAPagar(string n, int q)
        {
            float res =0.0F;

            if(existeQuantidade(n,q))
            {
                foreach(Produto p in produtos)
                {
                    if(p.NomeProd == n)
                    {
                        res = p.Preço * q;
                    }
                }
            }
            return res;
        }

        /// <summary>
        /// Metodo que da o nome do produto n se ele existir
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public Produto getSeExiste(string n)
        {
            Produto res = null;

            foreach(Produto p in produtos)
            {
                if (p.NomeProd == n)
                {
                    res = p;
                }
            }
            return res;
        }

        /// <summary>
        /// Alternativa ao metodo valorAPagar
        /// </summary>
        /// <param name="n"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        public float valorAPagar2(string n, int q)
        {
            float res = 0.0F;
            Produto p = getSeExiste(n);
            
            if(p != null)
            {
                if(p.Quantidade >= q)
                {
                    res = p.Preço * q;
                }
            }
            return res;
        }

        public void pagamento(float dC, float dP)
        {

        }

        //serviços do palco
        public void chamada(string n)
        {
            foreach (Funcionarios f in funcionarios)
            {
                if(f.Nome == n && palco.dançarinos.Count <Palco.maximo)
                {
                    foreach(string c in f.Categoria)
                    {
                        if (c == "strip")
                        {
                            palco.dançarinos.Add(f);
                        }
                    }
                    
                }
            }
        }
    }
        class Funcionarios
        {
            #region Variaveis
            //---------------------------------------- ( Variaveis ) ----------------------------------------\\

            private string nome;
            private char sexo;      //(M,F,T)
            private List<string> categoria; //{exotico, limpeza, strip, bar, 1v1, hardcore, bdsm}
            private int idade;
            private float rendimento;
            private int NIF;
            private int IBAN;
        private float rendimento;
            #endregion

            //---------------------------------------- ( Métodos ) ----------------------------------------\\
            #region GETS & SETS
            public string NomeFunc { get => nomeFunc; set => nomeFunc = value; }
            public char SexoFunc { get => sexoFunc; set => sexoFunc = value; }
            public List<string> Categoria { get => categoria; set => categoria = value; }
            public int IdadeFunc { get => idadeFunc; set => idadeFunc = value; }
            public float Rendimento { get => rendimento; set => rendimento = value; }
            public int NIFFunc1 { get => NIFFunc; set => NIFFunc = value; }
            public int IBANFunc1 { get => IBANFunc; set => IBANFunc = value; }
            #endregion
            //---------------------------------------- ( Construtores ) ----------------------------------------\\

            public Funcionarios(string nomeFunc, char sexoFunc, List<string> categoria, int idadeFunc, float rendimento, int nIFFunc, int iBANFunc)
            {
                this.nomeFunc = nomeFunc;
                this.sexoFunc = sexoFunc;
                this.categoria = categoria;
                this.idadeFunc = idadeFunc;
                this.rendimento = rendimento;
                NIFFunc = nIFFunc;
                IBANFunc = iBANFunc;
            }
        }

        class Produto
        {
            #region Variaveis
            //---------------------------------------- ( Variaveis ) ----------------------------------------\\

            private string nomeProd;
            private float preço;
            private int quantidade;
            #endregion

            //---------------------------------------- ( Métodos ) ----------------------------------------\\
            #region GETS & SETS 
            public string NomeProd { get => nomeProd; set => nomeProd = value; }
            public float Preço { get => preço; set => preço = value; }
            public int Quantidade { get => quantidade; set => quantidade = value; }
            #endregion
            //---------------------------------------- ( Construtores ) ----------------------------------------\\

            public Produto(string nomeProd, float preço, int quantidade)
            {
                this.nomeProd = nomeProd;
                this.preço = preço;
                this.quantidade = quantidade;
            }

        }
        class Quarto
        {
            #region Variaveis
            //---------------------------------------- ( Variaveis ) ----------------------------------------\\

            private int capacidade;
            private string tipo; //simple, suites, dungeons, japonices, banhos, jacuzzis, porcherie
            private bool disponiblidade;
            private List<Cliente> clientes;
            private List<Funcionarios> funcionarios;
            private float preço;
            private Palco hatua;//suaili
        #endregion

        //---------------------------------------- ( Métodos ) ----------------------------------------\\
        public int Capacidade { get => capacidade; set => capacidade = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public bool Disponiblidade { get => disponiblidade; set => disponiblidade = value; }
        public float Preço { get => preço; set => preço = value; }
        internal List<Cliente> Clientes { get => clientes; set => clientes = value; }
        internal List<Funcionarios> Funcionarios { get => funcionarios; set => funcionarios = value; }
        internal Palco Hatua { get => hatua; set => hatua = value; }
        //---------------------------------------- ( Construtores ) ----------------------------------------\\

        public Quarto(int lotação, List<string> tipo, bool disponiblidade)
            {
                this.lotação = lotação;
                this.tipo = tipo;
                this.disponiblidade = disponiblidade;
            }

        public int Capacidade { get => capacidade; set => capacidade = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public bool Disponiblidade { get => disponiblidade; set => disponiblidade = value; }
        public float Preço { get => preço; set => preço = value; }
        public List<Cliente> Clientes { get => clientes; set => clientes = value; }
        public List<Funcionarios> Funcionarios { get => funcionarios; set => funcionarios = value; }
        public Palco Hatua { get => hatua; set => hatua = value; }
    }

        class Cliente
        {
            #region Variaveis
            //---------------------------------------- ( Variaveis ) ----------------------------------------\\

            private string nomeCli;
            private float dinheiroCli;
            private int idadeCli;
            private char sexoCli;     //(M,F,T)  
            private char orientação;  //(D,M,A,T)
            private int NIFCli;
            private int IBANCli;
            #endregion

            //---------------------------------------- ( Métodos ) ----------------------------------------\\
            #region GETS & SETS
            public string NomeCli { get => nomeCli; set => nomeCli = value; }
            public float DinheiroCli { get => dinheiroCli; set => dinheiroCli = value; }
            public int IdadeCli { get => idadeCli; set => idadeCli = value; }
            public char SexoCli { get => sexoCli; set => sexoCli = value; }
            public char Orientação { get => orientação; set => orientação = value; }
            public int NIFCli1 { get => NIFCli; set => NIFCli = value; }
            public int IBANCli1 { get => IBANCli; set => IBANCli = value; }
            #endregion

            public void pedir(string n, int q,BAR b)
            {
                float valor=b.valorAPagar2(n, q);

            if (b.existeQuantidade(n,q) && valor <= dinheiroCli)
                {
                    pagar(valor,b);
                }

            }

            public void pagar(float v,BAR b)
            {
                dinheiroCli -= v;
                b.DinheiroBar += v;
            }

            public void atirarDinheiro(float d, BAR b)
            {
            if (d <= dinheiroCli)
            {
                float tip = d / (float)b.Palco.dançarinos.Count;
                dinheiroCli -= d;
                foreach (Funcionarios d in b.Palco.dançarinos)
                {
                    f.Rendimento += tip;
                }
            }

            }
            //---------------------------------------- ( Construtores ) ----------------------------------------\\

            public Cliente(string nomeCli, float dinheiroCli, int idadeCli, char sexoCli, char orientação, int nIFCli, int iBANCli)
            {
                this.nomeCli = nomeCli;
                this.dinheiroCli = dinheiroCli;
                this.idadeCli = idadeCli;
                this.sexoCli = sexoCli;
                this.orientação = orientação;
                this.NIFCli = nIFCli;
                this.IBANCli = iBANCli;
            }

        }

        struct Pessoa
        {
            private string nome;
            private int iBAN;
            private int nIF;
            private float dinheiro;


            #region GETS & SETS
            string Nome { get => nome; set => nome = value; }
            public int IBAN { get => iBAN; set => iBAN = value; }
            public int NIF { get => nIF; set => nIF = value; }
            public float Dinheiro { get => dinheiro; set => dinheiro = value; }
            #endregion

        }


        class MB
        {
            #region Variaveis
            //---------------------------------------- ( Variaveis ) ----------------------------------------\\
            private List<Pessoa> Pessoas;
            private float dinheiroMB;
            #endregion

            //---------------------------------------- ( Métodos ) ----------------------------------------\\
            #region GETS & SETS
            public float DinheiroMB { get => dinheiroMB; set => dinheiroMB = value; }
            public List<Pessoa> Pessoas1 { get => Pessoas; set => Pessoas = value; }
            #endregion


            //---------------------------------------- ( Construtores ) ----------------------------------------\\

            public MB(List<Pessoa> pessoas, float dinheiroMB)
            {
                Pessoas = pessoas;
                this.dinheiroMB = dinheiroMB;
            }

        }

        class IRS
        {
            #region Variaveis
            //---------------------------------------- ( Variaveis ) ----------------------------------------\\
            private List<Pessoa> pessoas;
            #endregion

            //---------------------------------------- ( Métodos ) ----------------------------------------\\
            #region GETS & SETS
            private List<Pessoa> Pessoas { get => Pessoas; set => Pessoas = value; }
            #endregion

            //---------------------------------------- ( Construtores ) ----------------------------------------\\

            public IRS(List<Pessoa> pessoas)
            {
                Pessoas = pessoas;
            }


        }

        class Program
        {

            

            static void Main(string[] args)
            {

            //criar objetos/dados
            BAR b = null;
            Cliente c1 = new Cliente("Tó", 18, 50.0F, 'M', 'D', 0001, 0011);
            Cliente c2 = new Cliente("Tó", 18, 50.0F, 'M', 'D', 0001, 0011);
            //menu
            bool acabou = false;
            Console.WriteLine("Bem vindo ao melhor bar alternativo de sempre");
            while (!acabou)
            {
                
                //opçoes
                //1-bar
                //2-hatua/palco
                //3-quarto
                Console.Clear();
                Console.WriteLine("Escolha uma das opções (número)");
                Console.WriteLine("1 - Bar ");
                Console.WriteLine("2 - Hatua");
                Console.WriteLine("3 - Quarto");
                Console.WriteLine("0 - Sair :D");
                string escolha = Console.ReadLine();
                
                switch (escolha)
                {
                    case ("1"): {
                            bool cheio = false;
                            while (!cheio)
                            {
                                Console.Clear();
                                Console.WriteLine("Indique nome do Cliente");
                                string nomeXXX = Console.ReadLine();
                                Console.WriteLine("1 - Comes e bebes");
                                Console.WriteLine("2 - Take Away");
                                Console.WriteLine("3 - Kushiriki/engatar");
                                Console.WriteLine("4 - Voltar a tras");
                                Console.WriteLine("0 - Sair");
                                escolha = Console.ReadLine();
                                Cliente xxx = null;
                                foreach(Cliente c in b.Clientes)
                                {
                                    if (c.NomeCli == nomeXXX)
                                    {
                                        xxx = c; 
                                    }
                                }
                                switch (escolha)
                                {
                                    case ("1"):
                                        {
                                            Console.Clear();
                                            Console.WriteLine("1 - O que há?");
                                            Console.WriteLine("2 - Sugestões?");
                                            Console.WriteLine("3 - Voltar atrás");
                                            escolha = Console.ReadLine();
                                            bool kunywa = false;
                                            while (!kunywa) {
                                                switch (escolha)
                                                {
                                                    case ("1"):
                                                        {
                                                            foreach (Produto p in b.Produtos)
                                                            {
                                                                if (p.Quantidade > 0)
                                                                {
                                                                    Console.WriteLine(p.NomeProd + " preço: " + p.Preço);
                                                                }
                                                            }
                                                            Console.WriteLine("O que deseja");
                                                            string s = Console.ReadLine();
                                                            Console.WriteLine("Quantos queres");
                                                            string n = Console.ReadLine();
                                                            int q = Int32.Parse(n);
                                                            if(b.existeQuantidade(s, q))
                                                            {
                                                                Console.WriteLine("Aqui tem! Bom proveito!");
                                                                float v = b.valorAPagar2(s, q);
                                                                xxx.pagar(v,b);
                                                            }
                                                            else
                                                            {
                                                                Console.WriteLine("Não existe o artigo ou as quantidades que pediu");
                                                            }
                                                        } break;
                                                    case ("2"):
                                                        {
                                                            Console.WriteLine("Bellyshot de whisky e terras de Ucrânia");
                                                        } break;
                                                    case ("3"):
                                                        {
                                                            kunywa = true;
                                                        } break;
                                                }
                                            }    
                                        } break;
                                    case ("2"): { } break;
                                    case ("3"): { } break;
                                    case ("4"):
                                        {
                                            cheio = true;
                                        } break;
                                    case("0"):
                                        {
                                            cheio = true;
                                            acabou = true;
                                        } break;
                                }
                            } break;
                        }
                    case ("2"): { } break;
                    case ("3"): { } break;
                    case ("0"): {
                            acabou = true;
                        } break;
                    default: {
                            Console.WriteLine("Say WHAAAAAAAAAAAAAAT");
                        } break;
                }
            }

            

            }
        }
}


