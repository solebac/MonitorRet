using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

namespace MonitorRet
{
    class instrucaol : conector_full_variable //Exemplo de Herança
    {
        public instrucaol()
        {
            InitializeComponent();

            if (myPass != "")
            {
                if (myUsers != "")
                {
                    if (alwaysVariables.TypeComunicacao != "")
                    {
                        if (alwaysVariables.TypeComunicacao == "0")
                        {
                            transmissaoCaminho = alwaysVariables.TransmissaoWindows;
                            recepcaoCaminho = alwaysVariables.RecepcaoWindows;
                        }
                        else
                        {
                            transmissaoCaminho = alwaysVariables.TransmissaoLinux;
                            recepcaoCaminho = alwaysVariables.RecepcaoLinux;
                        }
                        compartilhamento = alwaysVariables.EscritaCompatilhada;
                        setConfig(myUsers, myPass);
                        setCadastro(myUsers, myPass);
                        setFiscal(myUsers, myPass);
                        setAdministradora(myUsers, myPass);
                        setCorreio(myUsers, myPass);
                        setFinalizadora(myUsers, myPass);
                        setProduto(myUsers, myPass, Store);
                        setPessoa(myUsers, myPass, Store);
                        setCrediario(myUsers, myPass, Store);
                    }
                }
            }
        }
        
        //#########################################################Variavel Enpsulada########################################################
        private string myUsers;
        private string myPass;
        ProcessStartInfo ProcessInfo;
        Process myProcess;
        //#########################################################End Variavel Enpsulada####################################################

        //#########################################################Variavel & Constantes#######################################################
        private dados banco1 = new dados();
        const string folderMaster = @"c:\conector\";
        const string folderSlave = @"c:\conector\Transmissao";
        const string folderLog = @"c:\conector\Log";
        private conector_full_variable alwaysVariables = new conector_full_variable();
        string[] vetorConfig = new string[48];
        string[] vetorCadastro = new string[48];
        string[] vetorAdministradora = new string[48];
        string[] vetorFiscal = new string[48];
        string[] vetorProduto = new string[48];
        string[] vetorPessoa = new string[48];
        string[] vetorBalanca = new string[48];
        string[] vetorFinalizadora = new string[48];
        string[] vetorCorreio = new string[48];
        string[] vetorCrediario = new string[48];
        string[] vetorPromocao = new string[10];
        int countConfig = 0;
        int countCadastro = 0;
        int countFiscal=0;
        int countFinalizadora = 0;
        int countBalanca = 0;
        int countAdm = 0;
        int countPessoa = 0;
        int countProduto = 0;
        int countCorreio = 0;
        int countCrediario = 0;
        int countPromocao = 0;
        //##########Cliente
        protected string tipoPessoa;
        protected string representante;
        protected string cliente;
        protected string avalista;
        protected string dependente;
        protected string juridica;
        protected string rural;
        protected string fisica;
        protected string clienteCobranca;
        protected string clienteEntrega;
        protected string clienteProfissional;
        protected string clienteReferencia;
        protected string clienteRisco;
        protected string endereco;
        protected string enderecoType;
        protected string comprador;
        protected string fone;
        protected string foneType;
        protected string funcionario;
        protected string funcionario_endereco;
        protected string funcionario_fone;
        protected string tipoFornecedor;
        //##########End Cliente
        //##########Finalizadora
        protected string finalizadora;
        protected string metodo;
        protected string metodoparcelas;
        protected string metodoStore;
        //##########End Finalizadora
        //##########Config
        private string compartilhamento = @"c:\conector\Transmissao";
        private string typeComunicacao = "0";// 0 - Windows 1 - linux
        private string transmissaoCaminho;
        private string recepcaoCaminho;
        private string mensagem;
        List<string> mensagemLinha = new List<string>();
        private string profile;
        protected string spedGeneroItem;
        protected string system;
        protected string paramentro_faturamento;
        protected string table_codigo;
        protected string typeItem;
        protected string loja;
        protected string lojaecf;
        protected string loja_card;
        protected string hardware_ecf;
        protected string typeComissao;
        protected string carteira;
        protected string conectCard;
        protected string networkCard;
        protected string historico;
        protected string tipoVeiculo;
        protected string spedPlanoContas;
        protected string statusPedido;
        protected string typeCartao;
        protected string administradora;
        protected string banco;
        protected string spedNcm;
        protected string terminal;
        protected string terminalecfconfig;
        protected string typeTerminal;
        protected string escolaridade;
        protected string funcao;
        protected string typeReferencia;
        protected string contacorrente;
        protected string profissao;
        protected string estado;
        protected string cepCity;
        protected string typeHistorico;
        protected string spedMunicipio;
        protected string cepBairro;
        protected string atividade;
        protected string pais;
        protected string cargoFuncao;
        protected string civil;
        protected string usuario;
        protected string convenio;
        protected string configuracao;
        protected string feriado;
        protected string pisCofins;
        protected string sexo;
        protected string classMotivo;
        protected string motivo;
        protected string licenca_ecf;
        protected string licenca_ecf_ok;
        protected string statusPDV;
        //##########End Config
        //##########Fiscal
        protected string cst;
        protected string cstPis;
        protected string cstIpi;
        protected string cstCofins;
        protected string tributacao;
        protected string aliquota;
        protected string modeloFiscal;
        protected string modelo_ecf;
        protected string situacaoFiscal;
        protected string table_type_codigo;
        protected string cfop;
        protected string pisCofinsAnexo;
        //##########End Fiscal
        //##########Produto
        protected string unidadeMedida;
        protected string produto;
        protected string produtoStore;
        protected string produtoEmbalagem;
        protected string produtoEstoques;
        protected string produtoImpostos;
        protected string produtoMovimento;
        protected string produtoPrice;
        protected string setor;
        protected string grupo;
        protected string categoria;
        //##########End Produto
        //##########Crediario
        private string crediario;
        private string parcela;
        //##########End Crediario
        private string promocao;
        private string tipoPromocao;
        //#########################################################End Variavel & Constantes###################################################
        //#########################################################Propertes###################################################################
        public string[] _crediario
        {
            get
            {
                return vetorCrediario;
            }
            set
            {
                vetorCrediario = value;
            }
        }
        public string[] _promocao
        {
            get
            {
                return vetorPromocao;
            }
            set
            {
                vetorPromocao = value;
            }
        }
        public string[] _config
        {
            get
            {
                return vetorConfig;
            }
            set
            {
                vetorConfig = value;
            }
        }
        public string[] _balanca
        {
            get
            {
                return vetorBalanca;
            }
            set
            {
                vetorBalanca = value;
            }
        }
        public string[] _correio
        {
            get
            {
                return vetorCorreio;
            }
            set
            {
                vetorCorreio = value;
            }
        }
        public string[] _finalizadora
        {
            get
            {
                return vetorFinalizadora;
            }
            set
            {
                vetorFinalizadora = value;
            }
        }
        public string[] _cadastro
        {
            get
            {
                return vetorCadastro;
            }
            set
            {
                vetorCadastro = value;
            }
        }
        public string[] _administradora
        {
            get
            {
                return vetorAdministradora;
            }
            set
            {
                vetorAdministradora = value;
            }
        }
        public string[] _fiscal
        {
            get
            {
                return vetorFiscal;
            }
            set
            {
                vetorFiscal = value;
            }
        }
        public string[] _produto
        {
            get
            {
                return vetorProduto;
            }
            set
            {
                vetorProduto = value;
            }
        }
        public string[] _pessoa
        {
            get
            {
                return vetorPessoa;
            }
            set
            {
                vetorPessoa = value;
            }
        }
        //#########################################################End Propertes###############################################################
        //#########################################################Metodos, Funçoes e Propart #################################################
        private void InitializeComponent()
        {
            Directory.CreateDirectory(folderMaster);
            Directory.CreateDirectory(folderSlave);
            Directory.CreateDirectory(folderLog);
            myUsers = UserName;
            myPass = Senha;
        }
       public void zeraCounts()
        {
            countConfig = 0;
            countCadastro = 0;
            countFiscal = 0;
            countFinalizadora = 0;
            countBalanca = 0;
            countAdm = 0;
            countPessoa = 0;
            countProduto = 0;
            countCorreio = 0;
            countCrediario = 0;
            countPromocao = 0;
        }
        protected void exeProcesso(string stringExe)
        {
            ProcessInfo = new ProcessStartInfo("cmd.exe", "/K " + stringExe);
            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.UseShellExecute = true;
            ProcessInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProcess = Process.Start(ProcessInfo);
            myProcess.WaitForExit();
            if (myProcess != null)
            {
                myProcess.Close();
            }
            if (File.Exists(stringExe))
            {
                File.Delete(stringExe);
                /*string[] fileRecepcao = Directory.GetFiles(@"c:\conector\recepcao\");
                int pos = stringExe.IndexOf("SQL");
                if (fileRecepcao.GetLength(0) <= 0 && pos == 21 )
                {
                    File.Delete(stringExe);
                }
                else if (pos == -1)
                {
                    File.Delete(stringExe);
                }*/
            }
        }
        public static string Right(string param, int length)
        {
            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }
        private void setCadastro(string users, string acess)
        {
            contacorrente = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "contacorrente.sql" + "' fields terminated by '|' from conector.contacorrente\"";
            vetorCadastro[countCadastro++] = contacorrente;
            typeReferencia = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "typeReferencia.sql" + "' fields terminated by '|' from conector.typeReferencia\"";
            vetorCadastro[countCadastro++] = typeReferencia;
            typeComissao = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "typeComissao.sql" + "' fields terminated by '|' from conector.typeComissao\"";
            vetorCadastro[countCadastro++] = typeComissao;
            carteira = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "carteira.sql" + "' fields terminated by '|' from conector.carteira\"";
            vetorCadastro[countCadastro++] = carteira;
            conectCard = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "conectCard.sql" + "' fields terminated by '|' from conector.conectCard\"";
            vetorCadastro[countCadastro++] = conectCard;
            banco = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "banco.sql" + "' fields terminated by '|' from conector.banco\"";
            vetorCadastro[countCadastro++] = banco;
            spedNcm = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "spedNcm.sql" + "' fields terminated by '|' from conector.spedNcm\"";
            vetorCadastro[countCadastro++] = spedNcm;
            terminal = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "terminal.sql" + "' fields terminated by '|' from conector.terminal\"";
            vetorCadastro[countCadastro++] = terminal;
            terminalecfconfig = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "terminalecfconfig.sql" + "' fields terminated by '|' from conector.terminalecfconfig\"";
            vetorCadastro[countCadastro++] = terminalecfconfig;
            profissao = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "profissao.sql" + "' fields terminated by '|' from conector.profissao\"";
            vetorCadastro[countCadastro++] = profissao;
            historico = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "historico.sql" + "' fields terminated by '|' from conector.historico\"";
            vetorCadastro[countCadastro++] = historico;
            escolaridade = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "escolaridade.sql" + "' fields terminated by '|' from conector.escolaridade\"";
            vetorCadastro[countCadastro++] = escolaridade;
            estado = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "estado.sql" + "' fields terminated by '|' from conector.estado\"";
            vetorCadastro[countCadastro++] = estado;
            spedMunicipio = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "spedMunicipio.sql" + "' fields terminated by '|' from conector.spedMunicipio\"";
            vetorCadastro[countCadastro++] = spedMunicipio;
            loja = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "loja.sql" + "' fields terminated by '|' from conector.loja\"";
            vetorCadastro[countCadastro++] = loja;
            loja_card = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "loja_card.sql" + "' fields terminated by '|' from conector.loja_card\"";
            vetorCadastro[countCadastro++] = loja_card;
            lojaecf = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "lojaecf.sql" + "' fields terminated by '|' from conector.lojaecf\"";
            vetorCadastro[countCadastro++] = lojaecf;
            cepCity = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cepCity.sql" + "' fields terminated by '|' from conector.cepCity\"";
            vetorCadastro[countCadastro++] = cepCity;
            pais = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "pais.sql" + "' fields terminated by '|' from conector.pais\"";
            vetorCadastro[countCadastro++] = pais;
            cargoFuncao = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cargoFuncao.sql" + "' fields terminated by '|' from conector.cargoFuncao\"";
            vetorCadastro[countCadastro++] = cargoFuncao;
            civil = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "civil.sql" + "' fields terminated by '|' from conector.civil\"";
            vetorCadastro[countCadastro++] = civil;
            sexo = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "sexo.sql" + "' fields terminated by '|' from conector.sexo\"";
            vetorCadastro[countCadastro++] = sexo;
            motivo = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "motivo.sql" + "' fields terminated by '|' from conector.motivo\"";
            vetorCadastro[countCadastro++] = motivo;
            atividade = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "atividade.sql" + "' fields terminated by '|' from conector.atividade\"";
            vetorCadastro[countCadastro++] = atividade;
            feriado = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "feriado.sql" + "' fields terminated by '|' from conector.feriado\"";
            vetorCadastro[countCadastro++] = feriado;
            usuario = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "usuario.sql" + "' fields terminated by '|' from conector.usuario\"";
            vetorCadastro[countCadastro++] = usuario;
            convenio = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "convenio.sql" + "' fields terminated by '|' from conector.convenio\"";
            vetorCadastro[countCadastro++] = convenio;
            convenio = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "typeLancamento.sql" + "' fields terminated by '|' from conector.typeLancamento\"";
            vetorCadastro[countCadastro++] = convenio;
            //configuracao = "mysql --user=" + users + "  -X --execute=\"SELECT * FROM conector.configuracao\" --password=" + acess + "> c://conector//Transmissao//TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "configuracao.xml";
            //vetorCadastro[countCadastro++] = configuracao; layout do conector.Adm

        }
        private void setCorreio(string users, string acess)
        {
            cepBairro = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cepBairro.sql" + "' fields terminated by '|' from conector.cepBairro\"";
            vetorCorreio[countCorreio++] = cepBairro;
        }
        private void setCrediario(string users, string acess, string store)
        {
            crediario = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "crediario.sql" + "' fields terminated by '|' from conector.crediario where idLoja=" + store + "\"";
            vetorCrediario[countCrediario++] = crediario;
            parcela = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "parcela.sql" + "' fields terminated by '|' from conector.parcela\"";
            vetorCrediario[countCrediario++] = parcela;
        }
        public void setPessoa(string users, string acess, string store)
        {
            countPessoa = 0;
            cliente = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cliente.sql" + "' fields terminated by '|' from conector.cliente where idLoja=" + store + "\"";
            vetorPessoa[countPessoa++] = cliente;
            avalista = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "avalista.sql" + "' fields terminated by '|' from conector.avalista\"";
            vetorPessoa[countPessoa++] = avalista;
            dependente = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "dependente.sql" + "' fields terminated by '|' from conector.dependente\"";
            vetorPessoa[countPessoa++] = dependente;
            tipoPessoa = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "tipopessoa.sql" + "' fields terminated by '|' from conector.tipopessoa\"";
            vetorPessoa[countPessoa++] = tipoPessoa;
            juridica = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select tab1.* into outFile 'c://conector//Transmissao//TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "juridica.sql" + "' fields terminated by '|' from conector.cliente tab, conector.juridica tab1 where tab.idCliente=tab1.idCliente and tab.idLoja=" + store + "\"";
            vetorPessoa[countPessoa++] = juridica;
            rural = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select tab1.* into outFile 'c://conector//Transmissao//TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "rural.sql" + "' fields terminated by '|' from conector.cliente tab, conector.rural tab1 where tab.idCliente=tab1.idCliente and tab.idLoja=" + store + "\"";
            vetorPessoa[countPessoa++] = rural;
            fisica = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select tab1.* into outFile 'c://conector//Transmissao//TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "fisica.sql" + "' fields terminated by '|' from conector.cliente tab, conector.fisica tab1 where tab.idCliente=tab1.idCliente and tab.idLoja=" + store + "\"";
            vetorPessoa[countPessoa++] = fisica;
            clienteCobranca = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select tab1.* into outFile 'c://conector//Transmissao//TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "clienteCobranca.sql" + "' fields terminated by '|' from conector.cliente tab, conector.clientecobranca tab1 where tab.idCliente=tab1.idCliente and tab.idLoja=" + store + "\"";
            vetorPessoa[countPessoa++] =clienteCobranca;
            clienteEntrega = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select tab1.* into outFile 'c://conector//Transmissao//TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "clienteEntrega.sql" + "' fields terminated by '|' from conector.cliente tab, conector.clienteentrega tab1 where tab.idCliente=tab1.idCliente and tab.idLoja=" + store + "\"";
            vetorPessoa[countPessoa++] = clienteEntrega;
            clienteProfissional = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select tab1.* into outFile 'c://conector//Transmissao//TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "clienteProfissional.sql" + "' fields terminated by '|' from conector.cliente tab, conector.clienteprofissional tab1 where tab.idCliente=tab1.idCliente  and tab.idLoja=" + store + "\"";
            vetorPessoa[countPessoa++] = clienteProfissional;
    	    clienteReferencia = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select tab1.* into outFile 'c://conector//Transmissao//TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "clienteReferencia.sql" + "' fields terminated by '|' FROM conector.clientereferencia tab1, conector.cliente tab where tab.idcliente=tab1.idCliente and tab.idLoja=" + store + "\"";
            vetorPessoa[countPessoa++] = clienteReferencia;
            clienteRisco = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select tab1.* into outFile 'c://conector//Transmissao//TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "clienteRisco.sql" + "' fields terminated by '|' from conector.cliente tab, conector.clienterisco tab1 where tab.idCliente=tab1.idCliente and tab.idLoja=" + store +  "\"";
            vetorPessoa[countPessoa++] = clienteRisco;
            enderecoType = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "enderecoType.sql" + "' fields terminated by '|' from conector.enderecoType\"";
            vetorPessoa[countPessoa++] = enderecoType;
            endereco = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select tab1.* into outFile 'c://conector//Transmissao//TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "endereco.sql" + "' fields terminated by '|' from conector.cliente tab, conector.endereco tab1 where tab.idCliente=tab1.idCliente and tab.idLoja=" + store + "\"";
            vetorPessoa[countPessoa++] = endereco;
            comprador = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "comprador.sql" + "' fields terminated by '|' from conector.comprador\"";
            vetorPessoa[countPessoa++] = comprador;
            foneType = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "foneType.sql" + "' fields terminated by '|' from conector.foneType\"";
            vetorPessoa[countPessoa++] = foneType;
            fone = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "fone.sql" + "' fields terminated by '|' from conector.fone\"";
            vetorPessoa[countPessoa++] = fone;
            funcionario = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "funcionario.sql" + "' fields terminated by '|' from conector.funcionario\"";
            vetorPessoa[countPessoa++] = funcionario;
            funcionario_endereco = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "funcionario_endereco.sql" + "' fields terminated by '|' from conector.funcionario_endereco\"";
            vetorPessoa[countPessoa++] = funcionario_endereco;
            funcionario_fone = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "funcionario_fone.sql" + "' fields terminated by '|' from conector.funcionario_fone\"";
            vetorPessoa[countPessoa++] = funcionario_fone;
            tipoFornecedor = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "tipoFornecedor.sql" + "' fields terminated by '|' from conector.tipoFornecedor\"";
            vetorPessoa[countPessoa++] = tipoFornecedor;
            representante = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "representante.sql" + "' fields terminated by '|' from conector.representante\"";
            vetorPessoa[countPessoa++] = representante;

        }
        public void setPromocao(string users, string acess, string store)
        {
            countPromocao = 0;
            tipoPromocao = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "tipoPromocao.sql" + "' fields terminated by '|' from conector.tipoPromocao\"";
            vetorPromocao[countPromocao++] = tipoPromocao;
            promocao = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "promocao.sql" + "' fields terminated by '|' from conector.promocao\"";
            vetorPromocao[countPromocao++] = promocao;
        }
        public void setProduto(string users, string acess, string store)
        {
            countProduto = 0;
            setor = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "setor.sql" + "' fields terminated by '|' from conector.setor\"";
            vetorProduto[countProduto++] = setor;
            grupo = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "grupo.sql" + "' fields terminated by '|' from conector.grupo\"";
            vetorProduto[countProduto++] = grupo;
            categoria = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "categoria.sql" + "' fields terminated by '|' from conector.categoria\"";
            vetorProduto[countProduto++] = categoria;
            unidadeMedida = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "unidadeMedida.sql" + "' fields terminated by '|' from conector.unidadeMedida\"";
            vetorProduto[countProduto++] = unidadeMedida;
            produto = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "produto.sql" + "' fields terminated by '|' from conector.produto\"";
            vetorProduto[countProduto++] = produto;
            produtoStore = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select tab1.* into outFile 'c://conector//Transmissao//TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "produtoStore.sql" + "' fields terminated by '|' FROM conector.produto tab, conector.produtostore tab1 where tab.idProduto= tab1.idProduto and (tab1.idLoja=" + store + " or " + store + "=0) \"";
            vetorProduto[countProduto++] = produtoStore;
            produtoEmbalagem = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select tab2.* into outFile 'c://conector//Transmissao//TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "produtoEmbalagem.sql" + "' fields terminated by '|' FROM conector.produto tab, conector.produtostore tab1, conector.produtoEmbalagem tab2 where tab.idProduto = tab1.idProduto and (tab1.idLoja=" + store + " or " + store + "=0) and tab.idProduto=tab2.idProduto and tab1.idProduto=tab2.idProduto\"";
            vetorProduto[countProduto++] = produtoEmbalagem;
            produtoEstoques = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select tab2.* into outFile 'c://conector//Transmissao//TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "produtoEstoques.sql" + "' fields terminated by '|' FROM conector.produto tab, conector.produtostore tab1, conector.produtoEstoques tab2 where tab.idProduto= tab1.idProduto and (tab1.idLoja=" + store + " or " + store + "=0) and (tab2.idLoja=" + store + " or " + store + "=0)and tab.idProduto=tab2.idProduto and tab1.idProduto=tab2.idProduto\"";
            vetorProduto[countProduto++] = produtoEstoques;
            produtoImpostos = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select tab2.* into outFile 'c://conector//Transmissao//TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "produtoImpostos.sql" + "' fields terminated by '|' FROM conector.produto tab, conector.produtostore tab1, conector.produtoimpostos tab2 where tab.idProduto= tab1.idProduto and (tab1.idLoja=" + store + " or " + store + "=0) and (tab2.idLoja=" + store + " or " + store + "=0)and tab.idProduto=tab2.idProduto and tab1.idProduto=tab2.idProduto\"";
            vetorProduto[countProduto++] = produtoImpostos;
            produtoPrice = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select tab2.* into outFile 'c://conector//Transmissao//TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "produtoPrice.sql" + "' fields terminated by '|' FROM conector.produto tab, conector.produtostore tab1, conector.produtoprice tab2 where tab.idProduto= tab1.idProduto and (tab1.idLoja=" + store + " or " + store + "=0) and (tab2.idLoja=" + store + " or " + store + "=0)and tab.idProduto=tab2.idProduto and tab1.idProduto=tab2.idProduto\"";
            vetorProduto[countProduto++] = produtoPrice;
        }
        private void setConfig(string users, string acess)
        {
            licenca_ecf = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select caixa,aquisicao,liberacao into outFile 'c://conector//Transmissao//TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "liberacao.sql" + "' fields terminated by '|' from conector.licenca_ecf\"";
            vetorConfig[countConfig++] = licenca_ecf;
            licenca_ecf_ok = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile 'c://conector//Transmissao//TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "liberacao_ok.sql" + "' fields terminated by '|' from conector.licenca_ecf\"";
            vetorConfig[countConfig++] = licenca_ecf_ok;
            system = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "system.sql" + "' fields terminated by '|' from conector.system\"";;
            vetorConfig[countConfig++] = system;
            typeItem = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "typeItem.sql" + "' fields terminated by '|' from conector.typeItem\"";
            vetorConfig[countConfig++] = typeItem;
            hardware_ecf = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "hardware_ecf.sql" + "' fields terminated by '|' from conector.hardware_ecf\"";
            vetorConfig[countConfig++] = hardware_ecf;
            tipoVeiculo = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "tipoVeiculo.sql" + "' fields terminated by '|' from conector.tipoVeiculo\"";
            vetorConfig[countConfig++] = tipoVeiculo;
            spedPlanoContas = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "spedPlanoContas.sql" + "' fields terminated by '|' from conector.spedPlanoContas\"";
            vetorConfig[countConfig++] = spedPlanoContas;
            table_type_codigo = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "table_type_codigo.sql" + "' fields terminated by '|' from conector.table_type_codigo\"";
            vetorConfig[countConfig++] = table_type_codigo;
            statusPedido = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "statusPedido.sql" + "' fields terminated by '|' from conector.statusPedido\"";
            vetorConfig[countConfig++] = statusPedido;
            typeTerminal = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "typeTerminal.sql" + "' fields terminated by '|' from conector.typeTerminal\"";
            vetorConfig[countConfig++] = typeTerminal;
            funcao = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "funcao.sql" + "' fields terminated by '|' from conector.funcao\"";
            vetorConfig[countConfig++] = funcao;
            typeHistorico = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "typeHistorico.sql" + "' fields terminated by '|' from conector.typeHistorico\"";
            vetorConfig[countConfig++] = typeHistorico;
            classMotivo = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "classMotivo.sql" + "' fields terminated by '|' from conector.classMotivo\"";
            vetorConfig[countConfig++] = classMotivo;
            statusPDV = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "statusPDV.sql" + "' fields terminated by '|' from conector.statusPDV\"";
            vetorConfig[countConfig++] = statusPDV;
            paramentro_faturamento = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "paramentro_faturamento.sql" + "' fields terminated by '|' from conector.paramentro_faturamento\"";
            vetorConfig[countConfig++] = paramentro_faturamento;
            table_codigo = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "table_codigo.sql" + "' fields terminated by '|' from conector.table_codigo\"";
            vetorConfig[countConfig++] = table_codigo;
        }
        private void setAdministradora(string users, string acess)
        {
            networkCard = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "networkCard.sql" + "' fields terminated by '|' from conector.networkCard\"";
            vetorAdministradora[countAdm++] = networkCard;
            typeCartao = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "typeCartao.sql" + "' fields terminated by '|' from conector.typeCartao\"";
            vetorAdministradora[countAdm++] = typeCartao;
            administradora = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "administradora.sql" + "' fields terminated by '|' from conector.administradora\"";
            vetorAdministradora[countAdm++] = administradora;
        }
        private void setFiscal(string users, string acess)
        {
            cst = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cst.sql" + "' fields terminated by '|' from conector.cst\"";
            vetorFiscal[countFiscal++] = cst;
            tributacao = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "tributacao.sql" + "' fields terminated by '|' from conector.tributacao\"";
            vetorFiscal[countFiscal++] = tributacao;
            aliquota = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "aliquota.sql" + "' fields terminated by '|' from conector.aliquota\"";
            vetorFiscal[countFiscal++] = aliquota;
            spedGeneroItem = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "spedGeneroItem.sql" + "' fields terminated by '|' from conector.spedGeneroItem\"";
            vetorFiscal[countFiscal++] = spedGeneroItem;
            modeloFiscal = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "modeloFiscal.sql" + "' fields terminated by '|' from conector.modeloFiscal\"";
            vetorFiscal[countFiscal++] = modeloFiscal;
            modelo_ecf = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "modelo_ecf.sql" + "' fields terminated by '|' from conector.modelo_ecf\"";
            vetorFiscal[countFiscal++] = modelo_ecf;
            situacaoFiscal = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "situacaoFiscal.sql" + "' fields terminated by '|' from conector.situacaoFiscal\"";
            vetorFiscal[countFiscal++] = situacaoFiscal;
            pisCofins = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "pisCofins.sql" + "' fields terminated by '|' from conector.pisCofins\"";
            vetorFiscal[countFiscal++] = pisCofins;
            pisCofinsAnexo = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "pisCofinsAnexo.sql" + "' fields terminated by '|' from conector.pisCofinsAnexo\"";
            vetorFiscal[countFiscal++] = pisCofinsAnexo;
            cfop = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cfop.sql" + "' fields terminated by '|' from conector.cfop\"";
            vetorFiscal[countFiscal++] = cfop;
            cstIpi = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cstIpi.sql" + "' fields terminated by '|' from conector.cstIpi\"";
            vetorFiscal[countFiscal++] = cstIpi;
            cstPis = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cstPis.sql" + "' fields terminated by '|' from conector.cstPis\"";
            vetorFiscal[countFiscal++] = cstPis;
            cstCofins = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cstCofins.sql" + "' fields terminated by '|' from conector.cstCofins\"";
            vetorFiscal[countFiscal++] = cstCofins;

        }
        private void setFinalizadora(string users, string acess)
        {
            finalizadora = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "finalizadora.sql" + "' fields terminated by '|' from conector.finalizadora\"";
            vetorFinalizadora[countFinalizadora++] = finalizadora;
            metodo = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "metodo.sql" + "' fields terminated by '|' from conector.metodo\"";
            vetorFinalizadora[countFinalizadora++] = metodo;
            metodoparcelas = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "metodoparcelas.sql" + "' fields terminated by '|' from conector.metodoparcelas\"";
            vetorFinalizadora[countFinalizadora++] = metodoparcelas;
            metodoStore = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost  + " -A conector --execute=\"select * into outFile '" + transmissaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "metodoStore.sql" + "' fields terminated by '|' from conector.metodoStore\"";
            vetorFinalizadora[countFinalizadora++] = metodoStore;

        }

        #region -- SqlImport
        public void preparaSql_Import(string exe, string store)
        {
            if (File.Exists(exe))
            {
                StreamWriter sw = new StreamWriter(recepcaoCaminho + "RCexe" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(store)) + ".bat", false);
                //                    sw.Write("exit");
                sw.Write(" cd " + recepcaoCaminho + " && ");
                sw.Write(" del " + recepcaoCaminho + "*.sql && ");
                sw.Write(" del " + recepcaoCaminho + "TRconector* && ");
                sw.Write("\"" + alwaysVariables.ECF_UTIL + "\"" + " e -ibck  -kb \"" + exe + "\"" + " \"" + recepcaoCaminho + "\" " + " && ");

                sw.Write(" del " + exe + " && ");
                sw.Write("\"" + alwaysVariables.ECF_UTIL + "\"" + " e -ibck -kb \"" + recepcaoCaminho + "*.rar\" " + " \"" + recepcaoCaminho + "\" " + "  && ");
                sw.Write(" dir /b /s " + recepcaoCaminho + "*.sql  >  " + recepcaoCaminho + "%date:~0,2%%date:~3,2%%date:~6,4%.txt && ");
                sw.Write(" del " + recepcaoCaminho + "*.rar && exit");
                sw.Close();
                profile = recepcaoCaminho + String.Format("{0:ddMMyyyy}", DateTime.Now) + ".txt";
                //exeProcesso(recepcaoCaminho + "RCexe" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + ".bat", "#");
                exeProcesso(recepcaoCaminho + "RCexe" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(store)) + ".bat");
            }
        }

        public void carregaSql_Import()
        {
            if (File.Exists(profile))
            {
                using (StreamReader texto = new StreamReader(profile))
                {

                    while ((mensagem = texto.ReadLine()) != null)
                    {
                        mensagemLinha.Add(mensagem.Replace("\\", "//"));
                        //mensagemLinha.Add(mensagem);
                    }
                }
                File.Delete(profile);
            }
        }

        public void executaSqlBat_Import(string store,ref bool valida)
        {
            if (mensagemLinha.Count > 0)
            {
                string strTroca = "";
                StreamWriter sw = new StreamWriter(recepcaoCaminho + "SQLexe" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(store)) + ".bat", false);
                sw.Write(" cd " + recepcaoCaminho + " && ");

                for (int i = 0; i < mensagemLinha.Count; i++)
                {
                    strTroca = mensagemLinha[i].Substring(50, mensagemLinha[i].IndexOf(".") - 50) == "licenca_ecf" ? "liberacao" : mensagemLinha[i].Substring(50, mensagemLinha[i].IndexOf(".") - 50) == "liberacao_ok" ? "licenca_ecf" : mensagemLinha[i].Substring(50, mensagemLinha[i].IndexOf(".") - 50);
                    if (strTroca == "usuario")
                    {
                        sw.Write(" mysql --user=" + alwaysVariables.UserName + " -A conector  --execute=\"SET foreign_key_checks=0; LOAD DATA LOCAL INFILE '" + mensagemLinha[i] + "' REPLACE INTO TABLE conector." + strTroca + " FIELDS TERMINATED BY '|' " + "\" --password=" + alwaysVariables.Senha + " && ");
                        sw.Write(" del " + recepcaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(store)) + mensagemLinha[i].Substring(50, mensagemLinha[i].IndexOf(".") - 50) + ".sql && ");
                        break;
                    }
                }

                for (int i = 0; i < mensagemLinha.Count; i++)
                {
                    strTroca = mensagemLinha[i].Substring(50, mensagemLinha[i].IndexOf(".") - 50) == "licenca_ecf" ? "liberacao" : mensagemLinha[i].Substring(50, mensagemLinha[i].IndexOf(".") - 50) == "liberacao_ok" ? "licenca_ecf" : mensagemLinha[i].Substring(50, mensagemLinha[i].IndexOf(".") - 50);
                    if (strTroca == "system")
                    {
                        sw.Write(" mysql --user=" + alwaysVariables.UserName + " -A conector  --execute=\"SET foreign_key_checks=0; LOAD DATA LOCAL INFILE '" + mensagemLinha[i] + "' REPLACE INTO TABLE conector." + strTroca + " FIELDS TERMINATED BY '|' " + "\" --password=" + alwaysVariables.Senha + " && ");
                        sw.Write(" del " + recepcaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(store)) + mensagemLinha[i].Substring(50, mensagemLinha[i].IndexOf(".") - 50) + ".sql && ");
                        break;
                    }
                }

                for (int i = 0; i < mensagemLinha.Count; i++)
                {
                    strTroca = mensagemLinha[i].Substring(50, mensagemLinha[i].IndexOf(".") - 50) == "licenca_ecf" ? "liberacao" : mensagemLinha[i].Substring(50, mensagemLinha[i].IndexOf(".") - 50) == "liberacao_ok" ? "licenca_ecf" : mensagemLinha[i].Substring(50, mensagemLinha[i].IndexOf(".") - 50);
                    if (strTroca == "loja")
                    {
                        sw.Write(" mysql --user=" + alwaysVariables.UserName + " -A conector  --execute=\"SET foreign_key_checks=0; LOAD DATA LOCAL INFILE '" + mensagemLinha[i] + "' REPLACE INTO TABLE conector." + strTroca + " FIELDS TERMINATED BY '|' " + "\" --password=" + alwaysVariables.Senha + " && ");
                        sw.Write(" del " + recepcaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(store)) + mensagemLinha[i].Substring(50, mensagemLinha[i].IndexOf(".") - 50) + ".sql && ");
                        break;
                    }
                }

                for (int i = 0; i < mensagemLinha.Count; i++)
                {
                    strTroca = mensagemLinha[i].Substring(50, mensagemLinha[i].IndexOf(".") - 50) == "licenca_ecf" ? "liberacao" : mensagemLinha[i].Substring(50, mensagemLinha[i].IndexOf(".") - 50) == "liberacao_ok" ? "licenca_ecf" : mensagemLinha[i].Substring(50, mensagemLinha[i].IndexOf(".") - 50);
                    if ((strTroca != "liberacao") && (strTroca != "usuario") && (strTroca != "loja") && (strTroca != "system"))
                    {
                        sw.Write(" mysql --user=" + alwaysVariables.UserName + " -A conector  --execute=\"SET foreign_key_checks=0; LOAD DATA LOCAL INFILE '" + mensagemLinha[i] + "' REPLACE INTO TABLE conector." + strTroca + " FIELDS TERMINATED BY '|' " + "\" --password=" + alwaysVariables.Senha + " && ");
                        sw.Write(" del " + recepcaoCaminho + "TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(store)) + mensagemLinha[i].Substring(50, mensagemLinha[i].IndexOf(".") - 50) + ".sql && ");
                    }
                    else
                    {
                        if ((strTroca == "liberacao"))
                        {
                            File.Delete(mensagemLinha[i].ToString());
                        }
                    }
                }
                sw.Write(" SET foreign_key_checks=1; && exit ");
                sw.Close();

                string[] fileRecepcao = Directory.GetFiles(recepcaoCaminho);
                if (fileRecepcao.GetLength(0) > 1)
                {
                    int pos = fileRecepcao[1].IndexOf("sql");
                    if (pos != 1)
                    {
                        for (int i = 1; i < fileRecepcao.GetLength(0); i++)
                        {
                            pos = fileRecepcao[i].IndexOf("TRconector");
                            if (pos != -1)
                            {
                                string lojas = fileRecepcao[i].Substring(pos + 18, 8).ToString();
                                if (lojas != "" && Convert.ToInt32(lojas) == Convert.ToInt32(store))
                                {
                                    valida = true;
                                    break;
                                }
                                else
                                {
                                    valida = false;
                                }
                            }
                            else
                            {
                                valida = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        valida = true;
                    }
                }
                else
                {
                    valida = true;
                }
                if (valida == true)
                {
                    exeProcesso(recepcaoCaminho + "SQLexe" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(store)) + ".bat");
                }
                else
                {
                    File.Delete(recepcaoCaminho + "SQLexe" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(store)) + ".bat");
                }
                if(File.Exists("break.txt"))
                {
                    File.Delete("break.txt");
                }
            }
        }
        #endregion

        ////Trecho de desenvolvimento XML
   /*     private void setCadastro(string users, string acess)
        {
            typeReferencia = "mysql --user=" + users + "  -X --execute=\"select * from conector.typeReferencia\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "typeReferencia.xml";
             string Testliente = "mysql --user=" + users + " --password=" + acess + " -h " + alwaysVariables.LocalHost + " -A conector --execute=\"select * into outFile 'c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cliente.sql" + "' fields terminated by '|' from conector.cliente where idLoja=" + Store + "\"";
            vetorCadastro[countCadastro++] = typeReferencia;
            typeComissao = "mysql --user=" + users + "  -X --execute=\"select * from conector.typeComissao\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "typeComissao.xml";
            vetorCadastro[countCadastro++] = typeComissao;
            carteira = "mysql --user=" + users + "  -X --execute=\"select * from conector.carteira\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "carteira.xml";
            vetorCadastro[countCadastro++] = carteira;
            conectCard = "mysql --user=" + users + "  -X --execute=\"select * from conector.conectCard\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "conectCard.xml";
            vetorCadastro[countCadastro++] = conectCard;
            banco = "mysql --user=" + users + "  -X --execute=\"select * from conector.banco\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "banco.xml";
            vetorCadastro[countCadastro++] = banco;
            spedNcm = "mysql --user=" + users + "  -X --execute=\"select * from conector.spedNcm\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "spedNcm.xml";
            vetorCadastro[countCadastro++] = spedNcm;
            terminal = "mysql --user=" + users + "  -X --execute=\"select * from `conector`.`Terminal`\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "terminal.xml";
            vetorCadastro[countCadastro++] = terminal;
            profissao = "mysql --user=" + users + "  -X --execute=\"select * from conector.profissao order by 1\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "profissao.xml";
            vetorCadastro[countCadastro++] = profissao;
            historico = "mysql --user=" + users + "  -X --execute=\"select * from conector.historico\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "historico.xml";
            vetorCadastro[countCadastro++] = historico;
            escolaridade = "mysql --user=" + users + "  -X --execute=\"select * from conector.escolaridade\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "escolaridade.xml";
            vetorCadastro[countCadastro++] = escolaridade;
            estado = "mysql --user=" + users + "  -X --execute=\"select * from conector.estado\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "estado.xml";
            vetorCadastro[countCadastro++] = estado;
            spedMunicipio = "mysql --user=" + users + "  -X --execute=\"select * from conector.spedMunicipio\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "spedMunicipio.xml";
            vetorCadastro[countCadastro++] = spedMunicipio;
            loja = "mysql --user=" + users + "  -X --execute=\"select * from conector.loja\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "loja.xml";
            vetorCadastro[countCadastro++] = loja;
            loja_card = "mysql --user=" + users + "  -X --execute=\"select * from conector.loja_card\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "loja_card.xml";
            vetorCadastro[countCadastro++] = loja_card;
            lojaecf = "mysql --user=" + users + "  -X --execute=\"select * from conector.lojaecf\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "lojaecf.xml";
            vetorCadastro[countCadastro++] = lojaecf;
            cepCity = "mysql --user=" + users + "  -X --execute=\"select * from conector.cepcity\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cepcity.xml";
            vetorCadastro[countCadastro++] = cepCity;
            pais = "mysql --user=" + users + "  -X --execute=\"select * from conector.pais\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "pais.xml";
            vetorCadastro[countCadastro++] = pais;
            cargoFuncao = "mysql --user=" + users + "  -X --execute=\"select * from conector.cargofuncao\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cargoFuncao.xml";
            vetorCadastro[countCadastro++] = cargoFuncao;
            civil = "mysql --user=" + users + "  -X --execute=\"select * from conector.civil\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "civil.xml";
            vetorCadastro[countCadastro++] = civil;
            sexo = "mysql --user=" + users + "  -X --execute=\"select * from conector.SEXO\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "sexo.xml";
            vetorCadastro[countCadastro++] = sexo;
            motivo = "mysql --user=" + users + "  -X --execute=\"select * from conector.motivo\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "motivo.xml";
            vetorCadastro[countCadastro++] = motivo;
            atividade = "mysql --user=" + users + "  -X --execute=\"select * from conector.atividade\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "atividade.xml";
            vetorCadastro[countCadastro++] = atividade;
            feriado = "mysql --user=" + users + "  -X --execute=\"SELECT * FROM conector.feriado\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "feriado.xml";
            vetorCadastro[countCadastro++] = feriado;
            usuario = "mysql --user=" + users + "  -X --execute=\"select * from conector.usuario\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "usuario.xml";
            vetorCadastro[countCadastro++] = usuario;
            //configuracao = "mysql --user=" + users + "  -X --execute=\"SELECT * FROM conector.configuracao\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "configuracao.xml";
            //vetorCadastro[countCadastro++] = configuracao; layout do conector.Adm

        }
        private void setCorreio(string users, string acess)
        {
            //cepCity = "mysql --user=" + users + "  -X --execute=\"select * from conector.cepcity\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cepCity.xml";
            //vetorCorreio[countCorreio++] = cepCity;
            cepBairro = "mysql --user=" + users + "  -X --execute=\"select * from conector.cepbairro\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cepBairro.xml";
            vetorCorreio[countCorreio++] = cepBairro;
        }
        public void setPessoa(string users, string acess, string store)
        {
            countPessoa = 0;
            cliente = "mysql --user=" + users + "  -X --execute=\"select * from conector.cliente where idLoja="+store+"\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cliente.xml";
            vetorPessoa[countPessoa++] = cliente;
            tipoPessoa = "mysql --user=" + users + "  -X --execute=\"SELECT * FROM conector.tipopessoa\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "tipoPessoa.xml";
            vetorPessoa[countPessoa++] = tipoPessoa;
            juridica = "mysql --user=" + users + "  -X --execute=\"select tab1.* /*juridica*/ /*from conector.cliente tab, conector.juridica tab1 where tab.idCliente=tab1.idCliente and tab.idLoja=" + store + "\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "juridica.xml";
            /*vetorPessoa[countPessoa++] = juridica;
            rural = "mysql --user=" + users + "  -X --execute=\"select tab1.* /*rural*/ /*from conector.cliente tab, conector.rural tab1 where tab.idCliente=tab1.idCliente and tab.idLoja=" + store + "\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "rural.xml";
            /*vetorPessoa[countPessoa++] = rural;
            fisica = "mysql --user=" + users + "  -X --execute=\"select tab1.* /*fisica*/ /*from conector.cliente tab, conector.fisica tab1 where tab.idCliente=tab1.idCliente and tab.idLoja=" + store +"\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "fisica.xml";
            /*vetorPessoa[countPessoa++] = fisica;
            clienteCobranca = "mysql --user=" + users + "  -X --execute=\"select tab1.* /*clientecobranca*/ /*from conector.cliente tab, conector.clientecobranca tab1 where tab.idCliente=tab1.idCliente\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "clienteCobranca.xml";
            /*vetorPessoa[countPessoa++] =clienteCobranca;
            clienteEntrega = "mysql --user=" + users + "  -X --execute=\"select tab1.* /*clienteEntrega*/ /*from conector.cliente tab, conector.clienteentrega tab1 where tab.idCliente=tab1.idCliente\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "clienteEntrega.xml";
            /*vetorPessoa[countPessoa++] = clienteEntrega;
            clienteProfissional = "mysql --user=" + users + "  -X --execute=\"select tab1.* /*clienteprofissional*/ /*from conector.cliente tab, conector.clienteprofissional tab1 where tab.idCliente=tab1.idCliente\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "clienteProfissional.xml";
            /*vetorPessoa[countPessoa++] = clienteProfissional;
            /*clienteReferencia = "mysql --user=" + users + "  -X --execute=\"SELECT tab1.* FROM conector.clientereferencia tab1, conector.cliente tab where tab.idcliente=tab1.idCliente and tab.idLoja=" + store + "\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "clienteReferencia.xml";
            vetorPessoa[countPessoa++] = clienteReferencia;*/
            /*clienteRisco = "mysql --user=" + users + "  -X --execute=\"select tab1.* /*clienteRisco*/ /*from conector.cliente tab, conector.clienterisco tab1 where tab.idCliente=tab1.idCliente\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "clienteRisco.xml";
            /*vetorPessoa[countPessoa++] = clienteRisco;
            enderecoType = "mysql --user=" + users + "  -X --execute=\"SELECT * FROM conector.enderecotype\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "enderecoType.xml";
            vetorPessoa[countPessoa++] = enderecoType;
            endereco = "mysql --user=" + users + "  -X --execute=\"select tab1.* /*endereco*/ /*from conector.cliente tab, conector.endereco tab1 where tab.idCliente=tab1.idCliente and tab.idLoja=" + store + "\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "endereco.xml";
            /*vetorPessoa[countPessoa++] = endereco;
            comprador = "mysql --user=" + users + "  -X --execute=\"SELECT * FROM conector.comprador\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "comprador.xml";
            vetorPessoa[countPessoa++] = comprador;
            foneType = "mysql --user=" + users + "  -X --execute=\"SELECT * FROM conector.foneType\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "foneType.xml";
            vetorPessoa[countPessoa++] = foneType;
            fone = "mysql --user=" + users + "  -X --execute=\"SELECT * FROM conector.fone tab, conector.cliente tab1 where tab.idCliente=tab1.idCliente and tab1.idLoja="+store+"\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "fone.xml";
            vetorPessoa[countPessoa++] = fone;
            funcionario = "mysql --user=" + users + "  -X --execute=\"SELECT * FROM conector.funcionario tab, conector.loja tab1 where tab.idLoja=tab1.idLoja and tab1.idLoja=" + store + "\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "funcionario.xml";
            vetorPessoa[countPessoa++] = funcionario;
            funcionario_endereco = "mysql --user=" + users + "  -X --execute=\"SELECT * FROM conector.funcionario_endereco\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "funcionario_endereco.xml";
            vetorPessoa[countPessoa++] = funcionario_endereco;
            /*funcionario_fone = "mysql --user=" + users + "  -X --execute=\"SELECT * FROM conector.funcionario_fone\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "funcionario_fone.xml";
            /*vetorPessoa[countPessoa++] = funcionario_fone;
            /*tipoFornecedor = "mysql --user=" + users + "  -X --execute=\"SELECT * FROM conector.tipoFornecedor\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "tipoFornecedor.xml";
            vetorPessoa[countPessoa++] = tipoFornecedor;
            //profissao = "mysql --user=" + users + "  -X --execute=\"SELECT * FROM conector.profissao\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "profissao.xml";
            //vetorPessoa[countPessoa++] = profissao;
           /* representante = "mysql --user=" + users + "  -X --execute=\"SELECT * FROM conector.representante\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "representante.xml";
            vetorPessoa[countPessoa++] = representante;

        }/*
        /*public void setProduto(string users, string acess, string store)
        {
            countProduto = 0;
            setor = "mysql --user=" + users + "  -X --execute=\"select * from conector.setor\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "setor.xml";
            vetorProduto[countProduto++] = setor;
            grupo = "mysql --user=" + users + "  -X --execute=\"select * FROM conector.grupo\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "grupo.xml";
            vetorProduto[countProduto++] = grupo;
            categoria = "mysql --user=" + users + "  -X --execute=\"select * FROM conector.categoria\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "categoria.xml";
            vetorProduto[countProduto++] = categoria;
            unidadeMedida = "mysql --user=" + users + "  -X --execute=\"select * from conector.unidadeMedida\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "unidadeMedida.xml";
            vetorProduto[countProduto++] = unidadeMedida;
            produto = "mysql --user=" + users + "  -X --execute=\"SELECT * FROM conector.produto\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "produto.xml";
            vetorProduto[countProduto++] = produto;
            produtoStore = "mysql --user=" + users + "  -X --execute=\"SELECT tab1.* /*Loja*/ /*FROM conector.produto tab, conector.produtostore tab1 where tab.idProduto= tab1.idProduto and (tab1.idLoja=" + store + " or " + store + "=0)\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "produtoStore.xml";
          /*  vetorProduto[countProduto++] = produtoStore;
            produtoEmbalagem = "mysql --user=" + users + "  -X --execute=\"SELECT tab2.* /*Embalagem*//* FROM conector.produto tab, conector.produtostore tab1, conector.produtoEmbalagem tab2 where tab.idProduto = tab1.idProduto and (tab1.idLoja=" + store + " or " + store + "=0) and tab.idProduto=tab2.idProduto and tab1.idProduto=tab2.idProduto\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "produtoEmbalagem.xml";
            /*vetorProduto[countProduto++] = produtoEmbalagem;
            produtoEstoques = "mysql --user=" + users + "  -X --execute=\"SELECT tab2.* /*Estoques*/ /*FROM conector.produto tab, conector.produtostore tab1, conector.produtoEstoques tab2 where tab.idProduto= tab1.idProduto and (tab1.idLoja=" + store + " or " + store + "=0) and (tab2.idLoja=" + store + " or " + store + "=0)and tab.idProduto=tab2.idProduto and tab1.idProduto=tab2.idProduto\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "produtoEstoques.xml";
            /*vetorProduto[countProduto++] = produtoEstoques;
            produtoImpostos = "mysql --user=" + users + "  -X --execute=\"SELECT tab2.* /*Impostos*/ /*FROM conector.produto tab, conector.produtostore tab1, conector.produtoimpostos tab2 where tab.idProduto= tab1.idProduto and (tab1.idLoja=" + store + " or " + store + "=0) and (tab2.idLoja=" + store + " or " + store + "=0)and tab.idProduto=tab2.idProduto and tab1.idProduto=tab2.idProduto\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "produtoImpostos.xml";
            //vetorProduto[countProduto++] = produtoImpostos;/*
            //produtoMovimento = "mysql --user=" + users + "  -X --execute=\"SELECT tab2.* /*Movimentação*//* FROM conector.produto tab, conector.produtostore tab1, conector.produtomovimento tab2 where tab.idProduto= tab1.idProduto and (tab1.idLoja=" + store + " or " + store + "=0) and (tab2.idLoja=" + store + " or " + store + "=0) and tab.idProduto=tab2.idProduto and tab1.idProduto=tab2.idProduto\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "produtoMovimento.xml";
            //vetorProduto[countProduto++] = produtoMovimento; not found
            /*produtoPrice = "mysql --user=" + users + "  -X --execute=\"SELECT tab2.* /*Preço de Venda*/
        /*FROM conector.produto tab, conector.produtostore tab1, conector.produtoprice tab2 where tab.idProduto= tab1.idProduto and (tab1.idLoja=" + store + " or " + store + "=0) and (tab2.idLoja=" + store + " or " + store + "=0)and tab.idProduto=tab2.idProduto and tab1.idProduto=tab2.idProduto\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "produtoPrice.xml";
//vetorProduto[countProduto++] = produtoPrice;/*
}*/
        /*private void setConfig(string users, string acess)
        {
            licenca_ecf = "mysql --user=" + users + "  -X --execute=\"select caixa,aquisicao,liberacao from conector.licenca_ecf where cnpj=" + CNPJ + "\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "liberacao.xml";
            vetorConfig[countConfig++] = licenca_ecf;
            system = "mysql --user=" + users + "  -X --execute=\"select * from conector.system\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "system.xml";
            vetorConfig[countConfig++] = system;
            typeItem = "mysql --user=" + users + "  -X --execute=\"select * from conector.typeItem\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "typeItem.xml";
            vetorConfig[countConfig++] = typeItem;
            hardware_ecf = "mysql --user=" + users + "  -X --execute=\"select * from  `conector`.`hardware_ecf`\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "hardware_ecf.xml";
            vetorConfig[countConfig++] = hardware_ecf;
            tipoVeiculo = "mysql --user=" + users + "  -X --execute=\"select * from conector.tipoVeiculo\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "tipoVeiculo.xml";
            vetorConfig[countConfig++] = tipoVeiculo;
            spedPlanoContas = "mysql --user=" + users + "  -X --execute=\"select * from conector.spedPlanoContas\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "spedPlanoContas.xml";
            vetorConfig[countConfig++] = spedPlanoContas;
            table_type_codigo = "mysql --user=" + users + "  -X --execute=\"select * from conector.table_type_codigo\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "table_type_codigo.xml";
            vetorConfig[countConfig++] = table_type_codigo;
            statusPedido = "mysql --user=" + users + "  -X --execute=\"select * from conector.statusPedido\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "statusPedido.xml";
            vetorConfig[countConfig++] = statusPedido;
            typeTerminal = "mysql --user=" + users + "  -X --execute=\"select * from conector.typeTerminal\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "typeTerminal.xml";
            vetorConfig[countConfig++] = typeTerminal;
            funcao = "mysql --user=" + users + "  -X --execute=\"select * from conector.funcao\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "funcao.xml";
            vetorConfig[countConfig++] = funcao;
            //typeMovimento = "mysql --user=" + users + "  -X --execute=\"select * from conector.TYPEMOVIMENTACAO\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "TYPEMOVIMENTACAO.xml";
            //vetorConfig[countConfig++] = typeMovimento;
            typeHistorico = "mysql --user=" + users + "  -X --execute=\"select * from conector.typeHistorico \" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "typeHistorico.xml";
            vetorConfig[countConfig++] = typeHistorico;
            classMotivo = "mysql --user=" + users + "  -X --execute=\"select * from conector.classMotivo\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "classMotivo.xml";
            vetorConfig[countConfig++] = classMotivo;
        }
        private void setAdministradora(string users, string acess)
        {
            networkCard = "mysql --user=" + users + "  -X --execute=\"select * from conector.networkCard\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "networkCard.xml";
            vetorAdministradora[countAdm++] = networkCard;
            typeCartao = "mysql --user=" + users + "  -X --execute=\"select * from conector.typeCartao\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "typeCartao.xml";
            vetorAdministradora[countAdm++] = typeCartao;
            administradora = "mysql --user=" + users + "  -X --execute=\"select * from conector.administradora\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "administradora.xml";
            vetorAdministradora[countAdm++] = administradora;
        }*/
        /*private void setFiscal(string users, string acess)
        {
            cst = "mysql --user=" + users + "  -X --execute=\"select * from conector.cst\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cst.xml";
            vetorFiscal[countFiscal++] = cst;
            tributacao = "mysql --user=" + users + "  -X --execute=\"select * from conector.tributacao\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "tributacao.xml";
            vetorFiscal[countFiscal++] = tributacao;
            aliquota = "mysql --user=" + users + "  -X --execute=\"select * from conector.aliquota\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "aliquota.xml";
            vetorFiscal[countFiscal] = aliquota;
            spedGeneroItem = "mysql --user=" + users + "  -X --execute=\"select * from conector.spedGeneroItem\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "spedGeneroItem.xml";
            vetorFiscal[countFiscal++] = spedGeneroItem;
            modeloFiscal = "mysql --user=" + users + "  -X --execute=\"select * from conector.modeloFiscal\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "modeloFiscal.xml";
            vetorFiscal[countFiscal++] = modeloFiscal;
            situacaoFiscal = "mysql --user=" + users + "  -X --execute=\"select * from conector.situacaoFiscal\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "situacaoFiscal.xml";
            vetorFiscal[countFiscal++] = situacaoFiscal;
            pisCofins = "mysql --user=" + users + "  -X --execute=\"select * from conector.piscofins\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "pisCofins.xml";
            vetorFiscal[countFiscal++] = pisCofins;
            cfop = "mysql --user=" + users + "  -X --execute=\"select * from conector.cfop\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cfop.xml";
            vetorFiscal[countFiscal++] = cfop;
            cstIpi = "mysql --user=" + users + "  -X --execute=\"SELECT * FROM conector.cstipi\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cstIpi.xml";
            vetorFiscal[countFiscal++] = cstIpi;
            cstPis = "mysql --user=" + users + "  -X --execute=\"SELECT * FROM conector.cstpis\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cstPis.xml";
            vetorFiscal[countFiscal++] = cstPis;
            cstCofins = "mysql --user=" + users + "  -X --execute=\"SELECT * FROM conector.cstcofins\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "cstPis.xml";
            vetorFiscal[countFiscal++] = cstCofins;

        }*/
/*
        private void setFinalizadora(string users, string acess)
        {
            finalizadora = "mysql --user=" + users + "  -X --execute=\"SELECT * FROM finalizadora\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "finalizadora.xml";
            vetorFinalizadora[countFinalizadora++] = finalizadora;
            metodo = "mysql --user=" + users + "  -X --execute=\"SELECT * FROM conector.metodo\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "metodo.xml";
            vetorFinalizadora[countFinalizadora++] = metodo;
            metodoparcelas = "mysql --user=" + users + "  -X --execute=\"SELECT * FROM conector.metodoparcelas\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "metodoparcelas.xml";
            vetorFinalizadora[countFinalizadora++] = metodoparcelas;
            metodoStore = "mysql --user=" + users + "  -X --execute=\"SELECT * FROM conector.metodostore\" --password=" + acess + "> c:\\conector\\Transmissao\\TRconector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "metodoStore.xml";
            vetorFinalizadora[countFinalizadora++] = metodoStore;

        }
*/
        public void clearDirector(string type)
        {
            StreamWriter sw = new StreamWriter(compartilhamento + "ClearDirector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + type + ".bat", false);

            sw.Write("del " + compartilhamento + "*.sql && ");
            sw.Write("del " + compartilhamento + "*.rar && ");
            sw.Write("del " + compartilhamento + "*.txt && ");
            sw.Write("del " + compartilhamento + "*.bat && exit");

            sw.Close();
            exeProcesso(compartilhamento + "ClearDirector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + type + ".bat");
        }
        public void exeXml(string[] vetor, string type)
        {
            StreamWriter sw = new StreamWriter(compartilhamento + "TRexeOne" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + type + ".bat", false);
            //sw.Write("set data=CONFIG%date:~0,2%%date:~3,2%%date:~6,4% && ");
            if (vetor[0] == null)
            {
                sw.Write("exit");
            }
            else
            {
                sw.Write("cd " + compartilhamento + " && ");
                sw.Write("del "+ compartilhamento + type + "%date:~0,2%%date:~3,2%%date:~6,4%" + ".rar " + " &&");
                sw.Write("\"" + alwaysVariables.ECF_UTIL + "\" a -ibck -o+ -r -V1500000 \"" + compartilhamento + "" + type + "%date:~0,2%%date:~3,2%%date:~6,4%" + String.Format("{0:00000000}", Convert.ToDouble(Store)) + "\"  \"" + compartilhamento + "*.sql\" && ");
                sw.Write("del " + compartilhamento + "*.sql && exit");
            }
            
            sw.Close();
            exeProcesso(compartilhamento + "TRexeOne" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + type + ".bat");
        }
        public void preparaXml(string type)
        {
            StreamWriter sw = new StreamWriter(compartilhamento + "TRexeTwo" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + ".bat", false);

            sw.Write("cd " + compartilhamento + " && ");
            sw.Write("del " + compartilhamento + "%date:~0,2%%date:~3,2%%date:~6,4%" + String.Format("{0:00000000}", Convert.ToDouble(Store)) + type + ".rar " + " &&");
            //sw.Write("\"C:\\Arquivos de programas\\WinRAR\\WINRAR.EXE\" a -ibck -o+ -r -V1500000 \"" + compartilhamento + "%date:~0,2%%date:~3,2%%date:~6,4%" + String.Format("{0:00000000}", Convert.ToDouble(Store)) + type + "\"  \"" + compartilhamento + "*.rar\" && ");
            sw.Write("\"" + alwaysVariables.ECF_UTIL + "\" a -ibck -o+ -r -V1500000 \"" + compartilhamento + "%date:~0,2%%date:~3,2%%date:~6,4%" + String.Format("{0:00000000}", Convert.ToDouble(Store)) + type + "\"  \"" + compartilhamento + "*.rar\" && ");
            sw.Write("del " + compartilhamento + "TRconector* && ");
            sw.Write("del " + compartilhamento + "*%date:~0,2%%date:~3,2%%date:~6,4%" + String.Format("{0:00000000}", Convert.ToDouble(Store)) + ".rar && exit");
            sw.Close();
            exeProcesso(compartilhamento + "TRexeTwo" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + ".bat");
        }
        public void carregaInstrucao(string[] vetor, string type)
        {
            if (vetor.Length >= 0)
            {
                StreamWriter sw = new StreamWriter(compartilhamento + "conector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + type + ".bat", false);
                for (int i = 0; i < vetor.Length; i++)
                {
                    if (vetor[i] == null)
                    {
                        sw.Write("exit" + "&&");
                    }
                    else
                    {
                        sw.Write(vetor[i] + " \r\n");
                    }
                }
                
                sw.Close();
                exeProcesso(compartilhamento + "conector" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + type + ".bat");
            }
        }
        //#########################################################End Metodos, Funçoes e Propart #######################################################################################################################################################################################################################
        //#########################################################Procedimento de Banco#########################################################################################
        private string conector_import_resource(string caminho, string table)
        {
            string retorno = "Not found...!";
            //SET foreign_key_checks = 0;
            string test = "SET foreign_key_checks=0; ";
            test += " LOAD XML LOCAL INFILE '";
            test += caminho;
            test += "' INTO TABLE " + table + "; ";
            test += "SET foreign_key_checks=1; ";
            int auxConsistencia = 0;
            try
            {
                banco1.abreConexao();
                banco1.iniciarTransacao();
                banco1.singleTransaction(test);
                if (File.Exists(caminho))
                {
                    banco1.procedimentoRead();
                }
                else
                {
                    retorno = "Caro Usuário: A instrução não foi executada.";
                }
                banco1.fechaRead();
                banco1.commit();

            }
            catch (Exception erro)
            {
                banco1.rollback();
                retorno = "Caro Usuário: MSG Suporte => " + erro.ToString();
                auxConsistencia = 1;
            }
            finally
            {
                banco1.fechaConexao();
                if (auxConsistencia == 0)
                {
                    retorno = "Concluído...!";
                    if (File.Exists(caminho))
                    {
                        File.Delete(caminho);
                    }
                }
            }
            return retorno;
        }
        //#########################################################Procedimento de Banco#########################################################################################
    }
}
