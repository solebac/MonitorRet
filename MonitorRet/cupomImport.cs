using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Examples.System.Net;
namespace MonitorRet
{   
    class cupomImport : IDisposable
    {
        public cupomImport()
        {
            Directory.CreateDirectory(folderBackup);
            if (alwaysVariables.TypeComunicacao == "1")
            {
                recepcaoCaminho = alwaysVariables.EscritaCompatilhadaImport;
            }
            else
            {
                recepcaoCaminho = alwaysVariables.RecepcaoWindows;
            }
            conector_find_loja();
            conector_find_loja_relacionamento();
        }

        //#########################################################Variavel Enpsulada########################################################
        AsynchronousFtpUpLoader ftp;
        ProcessStartInfo ProcessInfo;
        Process myProcess;
        private dados banco = new dados();
        List<string> leituraPasta = new List<string>();
        List<string> countComponentes = new List<string>();
        //string[,] vetorCupom = new string[2, 15];
        string[,] tables = new string[14, 2] { { "nf", "0" }, { "nfce", "0" }, { "nfItem", "0" }, { "nfImposto", "0" }, { "cupom_cabecalho", "0" }, { "cupom_detalhes", "0" }, { "cupom_movimento", "0" }, { "movimentoDia", "0" }, { "detalhe_reducao", "0" }, { "detalhe_reducao_aliquota", "0" }, { "fechamentoCaixa", "0" }, { "cartao", "0" }, { "cheque", "0" }, { "convenioMovimento", "0" } };
        public string[] vetorStore;
        private string path;// path = folderRepc + String.Format("{0:ddMMyyyy}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(alwaysVariables.Store)) + "TRANSMISSAO.rar";
        private string strCupom;
        private string profile;
        private conector_full_variable alwaysVariables = new conector_full_variable();
        private string mensagem;
        private int countField;
        private int countRows;
        private int auxConsistencia;
        private string recepcaoCaminho;
        /*private string auxIdLoja;
        private string auxTypeSintegra;
        private string retorno;
        private string transmissaoCaminho;
        protected string cupom_cabecalho;
        protected string cupom_detalhes;
        protected string cupom_movimento;
        protected string mapa_cabecalho;
        protected string mapa_movimento;
        protected string mapa_aliquota;
        protected string fechamentoCaixa;*/
        protected int count = 0;
        private string folderBackup = @"c:\conector\movimento\" + String.Format("{0:ddMMyy}", DateTime.Now);
        const string folderTrans = @"c:\conector\Transmissao\";
        const string folderRepc = @"c:\conector\Recepcao\";
        const string folderTef = @"c:\conector\tef\";
        int countCupom = 0;
        int countMapa = 0;
        //#########################################################Vetor Variavel
        string[] vetorCupomCabecalho = new string[48];
        string[] vetorCupomMovimento = new string[48];
        string[] vetorCupomDetalhes = new string[1000];
        string[] vetorMapa = new string[10];
        string[] vetorMapaMovimento = new string[26];
        string[] vetorMapaAliquota = new string[39];
        string[] vetorFechamentoCaixa = new string[10];
        string[,] vetorCx;
        string[,] vetorStoreRelacionada;
        //#########################################################End Variavel Enpsulada####################################################
        //#########################################################Vetor Variavel Nfce/Nfe
        string[] vetorNfe = new string[48];
        string[] vetorNfce = new string[48];
        string[] vetorNfceItem = new string[1000];
        //#########################################################End Variavel & Constantes###################################################

        //#########################################################  Procedimento de Banco  #################################################
        public string[,] conector_find_pdv(string store)
        {
            countField = 0;
            countRows = 0;
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.iniciarTransacao();
                banco.startTransaction("conector_find_terminal");
                banco.addParametro("tipo", "4");
                banco.addParametro("find", "");
                banco.addParametro("find_loja", store);
                banco.addParametro("find_type", "5");
                banco.procedimentoSet();
                banco.commit();
            }
            catch (Exception erro)
            {
                banco.rollback();
            }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = banco.retornaSet().Tables[0].Columns.Count;
                    countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                    vetorCx = new string[countRows, countField];
                    if (countRows > 0)
                    {
                        for (int i = 0; i < countRows; i++)
                        {
                            for (int j = 0; j < countField; j++)
                            {
                                if (j == 0)
                                {
                                    vetorCx[i, 0] = banco.retornaSet().Tables[0].Rows[i][0].ToString();
                                }
                                else if (j == 8)
                                {
                                    vetorCx[i, 1] = banco.retornaSet().Tables[0].Rows[i][8].ToString();
                                }
                            }
                        }
                    }
                }
                banco.fechaConexao();
            }
            return vetorCx;
        }
        public void conector_find_loja()
        {
            countRows = 0;
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.iniciarTransacao();
                banco.startTransaction("conector_find_loja");
                banco.addParametro("tipo", "3");
                banco.addParametro("find_loja", "0");
                banco.procedimentoSet();
                banco.commit();
            }
            catch (Exception erro)
            {
                banco.rollback();
                auxConsistencia = 1; }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        vetorStore = new string[countRows];
                        for (int i = 0; i < countRows; i++)
                        {
                            vetorStore[i] = (banco.retornaSet().Tables[0].Rows[i][0]).ToString();
                        }
                    }
                }
                banco.fechaConexao();
            }
        }

        public void conector_find_loja_relacionamento()
        {
            countRows = 0;
            countField = 0;
            auxConsistencia = 0;
            try
            {
                banco.abreConexao();
                banco.iniciarTransacao();
                banco.startTransaction("conector_find_loja");
                banco.addParametro("tipo", "6");
                banco.addParametro("find_loja", alwaysVariables.Store);
                banco.procedimentoSet();
                banco.commit();
            }
            catch (Exception erro)
            {
                banco.rollback();
                auxConsistencia = 1;
            }
            finally
            {
                if (auxConsistencia == 0)
                {
                    countField = banco.retornaSet().Tables[0].Columns.Count;
                    countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                    if (countRows > 0)
                    {
                        vetorStoreRelacionada = new string[countRows, countField];

                        for (int i = 0; i < countRows; i++)//Linha
                        {
                            for (int j = 0; j < countField; j++) //Coluna
                            {
                                vetorStoreRelacionada[i, j] = Convert.ToString(banco.retornaSet().Tables[0].Rows[i][j]);
                            }
                        }
                    }
                }
                banco.fechaConexao();
            }
        }

        //######################################################### End Procedimento de Banco  ##############################################

        //#########################################################  Proparteis     #########################################################
        #region
        public string[] _Store
        {
            get
            {
                return vetorStore;
            }
            set
            {
                vetorStore = value;
            }
        }

        public string[,] _StoreRelacionamento
        {
            get
            {
                return vetorStoreRelacionada;
            }
            set
            {
                vetorStoreRelacionada = value;
            }
        }
        public string[] _Mapa
        {
            get
            {
                return vetorMapa;
            }
            set
            {
                vetorMapa = value;
            }
        }

        public string[] _MapaMovimento
        {
            get
            {
                return vetorMapaMovimento;
            }
            set
            {
                vetorMapaMovimento = value;
            }
        }
        public string[] _MapaAliquota
        {
            get
            {
                return vetorMapaAliquota;
            }
            set
            {
                vetorMapaAliquota = value;
            }
        }

        public string[] _FechamentoCaixa
        {
            get
            {
                return vetorFechamentoCaixa;
            }
            set
            {
                vetorFechamentoCaixa = value;
            }
        }
        public string _strCupom
        {
            get
            {
                return strCupom;
            }
            set
            {
                strCupom = value;
            }
        }
        public string[] _cabecalho
        {
            get
            {
                return vetorCupomCabecalho;
            }
            set
            {
                vetorCupomCabecalho = value;
            }
        }
        public string[] _detalhes
        {
            get
            {
                return vetorCupomDetalhes;
            }
            set
            {
                vetorCupomDetalhes = value;
            }
        }

        public string[] _movimento
        {
            get
            {
                return vetorCupomMovimento;
            }
            set
            {
                vetorCupomMovimento = value;
            }
        }

        public string[] _nfe
        {
            get
            {
                return vetorNfe;
            }
            set
            {
                vetorNfe = value;
            }
        }

        public string[] _nfce
        {
            get
            {
                return vetorNfce;
            }
            set
            {
                vetorNfce = value;
            }
        }

        public string[] _nfceItem
        {
            get
            {
                return vetorNfceItem;
            }
            set
            {
                vetorNfceItem = value;
            }
        }
        #endregion
        //######################################################### END Proparteis     #####################################################

        //######################################################### Metodos e Funçoes #######################################################
        public void setVetor()
        {
            vetorCupomCabecalho = new string[48];
            vetorCupomMovimento = new string[48];
            vetorCupomDetalhes = new string[1000];
            vetorMapa = new string[10];
            vetorMapaMovimento = new string[26];
            vetorMapaAliquota = new string[39];
            vetorFechamentoCaixa = new string[10];
            vetorNfe = new string[48];
            vetorNfce = new string[48];
            vetorNfceItem = new string[1000];
            leituraPasta = new List<string>();
            countComponentes = new List<string>();
        }
        protected void exeProcesso(string stringExe, Int16 priori, double time)
        {
            ProcessInfo = new ProcessStartInfo("cmd.exe", "/K " + stringExe);
            ProcessInfo.CreateNoWindow = true;
            ProcessInfo.UseShellExecute = true;
            ProcessInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProcess = Process.Start(ProcessInfo);

            /*foreach (Process pr in Process.GetProcessesByName(ProcessInfo.Arguments))
            {
                if (!pr.HasExited) pr.Kill();
            }*/
            if (priori == 0)
            {
                if (myProcess != null)
                {
                    count = 0;
                    do
                    {
                        if (!myProcess.HasExited)
                        {
                            try
                            {
                                if (myProcess.Responding)
                                {
                                    Console.WriteLine("Status = Running");
                                    if (count == time)
                                    {
                                        myProcess.Kill();
                                    }
                                }
                                else
                                {
                                    myProcess.Kill();
                                }
                            }
                            catch (Exception)
                            {

                            }
                        }
                        count++;
                    } while (!myProcess.WaitForExit(1000));
                }
            }
            else
            {
                myProcess.WaitForExit();//Aguarde a conclusao
            }
            if (myProcess != null)
            {
                myProcess.Close();
            }

            if (File.Exists(stringExe))
            {
                try
                {
                    File.Delete(stringExe);
                }
                catch (Exception)
                {
                }
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

        [DllImport("wininet.dll")]
        private extern static Boolean InternetGetConnectedState(out int Description, int ReservedValue);
        // Um método que verifica se esta conectado
        public static Boolean IsConnected()
        {
            int Description;
            return InternetGetConnectedState(out Description, 0);
        }

        public static string PingHost(string args)//RECEBE COMO PARAMENTRO A URL SERVICE
        {
            HttpWebResponse res = null;

            try
            {
                // Create a request to the passed URI.  
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(args);
                req.Credentials = CredentialCache.DefaultNetworkCredentials;

                // Get the response object.  
                res = (HttpWebResponse)req.GetResponse();

                return "Service Up";
            }
            catch (Exception e)
            {
                if (e.Message.ToString() == "Impossível conectar-se ao servidor remoto")
                {
                    return "Host Unavailable";
                }
                //MessageBox.Show("Source : " + e.Source, "Exception Source", MessageBoxButtons.OK);
                //MessageBox.Show("Message : " + e.Message, "Exception Message", MessageBoxButtons.OK);
                return "Host Unavailable";
            }
        }
        public int verifica_arquivo(string table)
        {
            folderBackup = @"c:\conector\movimento\" + String.Format("{0:ddMMyy}", DateTime.Now);
            
            Directory.CreateDirectory(folderBackup); 

            countComponentes.Clear();
            StreamWriter sw = new StreamWriter(recepcaoCaminho + "RColetaexe" + "-" + table + ".bat");
            sw.Write(" cd " + recepcaoCaminho + " && ");
            /*if (alwaysVariables.TypeComunicacao == "1")
            {
                sw.Write("IF EXIST " + recepcaoCaminho + "*" + table + "*.rar " + " ( dir /b " + recepcaoCaminho + "*" + table + ".rar  >  " + recepcaoCaminho + "coleta-" + table + "-" + "%date:~0,2%%date:~3,2%%date:~6,4%.txt ) ELSE (ECHO \"###\" > " + recepcaoCaminho + "coleta-" + table + "-" + "%date:~0,2%%date:~3,2%%date:~6,4%.txt" + ") && ");
            }
            else
            {
                sw.Write("IF EXIST " + recepcaoCaminho + "*" + table + "*.rar " + " ( dir /w /b /s " + recepcaoCaminho + "*" + table + ".rar  >  " + recepcaoCaminho + "coleta-" + table + "-" + "%date:~0,2%%date:~3,2%%date:~6,4%.txt ) ELSE (ECHO \"###\" > " + recepcaoCaminho + "coleta-" + table + "-" + "%date:~0,2%%date:~3,2%%date:~6,4%.txt" + ") && ");   
            }*/
            sw.Write("IF EXIST " + recepcaoCaminho + "*" + table + "*.rar " + " ( dir /w /b /s " + recepcaoCaminho + "*" + table + ".rar  >  " + recepcaoCaminho + "coleta-" + table + "-" + "%date:~0,2%%date:~3,2%%date:~6,4%.txt ) ELSE (ECHO \"###\" > " + recepcaoCaminho + "coleta-" + table + "-" + "%date:~0,2%%date:~3,2%%date:~6,4%.txt" + ") && ");   
            //"IF EXIST C:\conector\Recepcao\*cupom_cabecalho.ra (dir /w /b /s c:\conector\Recepcao\*cupom_cabecalho.ra  >  c:\conector\Recepcao\coleta-cupom_detalhes-31072014.txt)" 
            sw.Write(" exit");
            sw.Close();
            profile = recepcaoCaminho + "coleta-" + table + "-"  + String.Format("{0:ddMMyyyy}", DateTime.Now) + ".txt";
            exeProcesso(recepcaoCaminho + "RColetaexe" + "-" + table + ".bat", 0 , 5);

            if (File.Exists(profile))
            {
                countCupom = 0;
                using (StreamReader texto = new StreamReader(profile))
                {

                    while ((mensagem = texto.ReadLine()) != null)
                    {
                        countComponentes.Add(mensagem);
                        int t = mensagem.IndexOf(".");
                        if (mensagem.Substring((mensagem.IndexOf(".") + 1),3) == "rar")
                        {
                            countCupom++;
                        }
                        else
                        {
                            countCupom = 0;
                            break;
                        }
                        //leituraPasta.Add(mensagem);
                    }
                }
                File.Delete(profile);
            }
            else
            {
                countCupom = 0;
            }
            return countCupom;
        }

        public void carregaSql(string caminho, int indice, ref List<string> leitura)
        {
            leituraPasta.Clear();
            if (File.Exists(caminho))
            {
                countCupom = 0;
                using (StreamReader texto = new StreamReader(caminho))
                {

                    while ((mensagem = texto.ReadLine()) != null)
                    {
                        /* if (alwaysVariables.TypeComunicacao ==  "1")
                         {
                             leituraPasta.Add(alwaysVariables.RecepcaoLinux + mensagem);
                         }
                         else
                         {
                             leituraPasta.Add(mensagem.Replace("\\", "//"));
                         }*/
                        leituraPasta.Add(mensagem.Replace("\\", "//"));
                        countCupom++;
                        //leituraPasta.Add(mensagem);
                    }
                }
                File.Delete(caminho);
                tables[indice, 1] = countCupom.ToString();
            }
            leitura = leituraPasta;
        }
        public string varredura(string tipo, string table, string Store)
        {
                StreamWriter sw = new StreamWriter(recepcaoCaminho + "RCupomexe" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + ".bat", false);
                sw.Write(" cd " + recepcaoCaminho + " && ");
                sw.Write(" del " + recepcaoCaminho + "*" + table + ".sql && ");
                sw.Write(" copy /Y " + recepcaoCaminho + tipo + "*" + table + ".rar " + " \"" + folderBackup + "\" " + " && ");
                sw.Write("\"" + alwaysVariables.ECF_UTIL + "\"" + " e -ibck  -kb \"" + recepcaoCaminho + tipo + "*" + table + ".rar\"" + " \"" + recepcaoCaminho + "\" " + " && ");
                /*if (alwaysVariables.TypeComunicacao == "1")
                {
                    sw.Write(" dir /b  " + recepcaoCaminho + tipo + "*" + table + "*.sql  >  " + recepcaoCaminho + table + "-" + "%date:~0,2%%date:~3,2%%date:~6,4%.txt && ");
                }
                else
                {
                    sw.Write(" dir /b /s " + recepcaoCaminho + tipo + "*" + table + "*.sql  >  " + recepcaoCaminho + table + "-" + "%date:~0,2%%date:~3,2%%date:~6,4%.txt && ");
                }*/
                sw.Write(" dir /b /s " + recepcaoCaminho + tipo + "*" + table + "*.sql  >  " + recepcaoCaminho + table + "-" + "%date:~0,2%%date:~3,2%%date:~6,4%.txt && ");
                sw.Write(" del " + recepcaoCaminho + "*" +table + ".rar && exit");
                sw.Close();
                profile = recepcaoCaminho + table + "-" + String.Format("{0:ddMMyyyy}", DateTime.Now) + ".txt";
                double time = 15;
                if (countCupom <= 10)
                {
                    time = 5;
                }
                else if(countCupom >= 100)
                {
                    time = countCupom / 10;
                }
                exeProcesso(recepcaoCaminho + "RCupomexe" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + ".bat", 0, time);//Segundos
                /*if (alwaysVariables.TypeComunicacao == "1")
                {
                    StreamWriter sw2 = new StreamWriter(recepcaoCaminho + "RCupomexe" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + ".bat", false);
                    sw2.Write(" cd " + recepcaoCaminho + " && ");
                    sw2.Write(" dir /b " + recepcaoCaminho + tipo + "*" + table + "*.sql  >  " + recepcaoCaminho + table + "-" + "%date:~0,2%%date:~3,2%%date:~6,4%.txt && ");
                    sw2.Write(" exit");
                    sw2.Close();
                    exeProcesso(recepcaoCaminho + "RCupomexe" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + ".bat", 0, 10);//Segundos
                }*/
                //File.Delete(profile);
                return profile;
        }
        private bool isValidConnection(string url, string user, string password)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(user, password);
                request.GetResponse();
            }
            catch (WebException ex)
            {
                return false;
            }
            return true;
        }

        //Desenvolvimento Via Sockets Alexandre Wilian

        #region //Sockets Client
        /// <summary>
        /// Cliente socket que irá enviar arquivo.
        /// </summary>
        /// <param name="nomeArquivo"></param>
        /// <param name="enderecoIp"></param>
        /// <param name="tamanhoArquivoMb"></param>
        /// <returns></returns>
        public bool EnviarArquivo(string nomeArquivo, string enderecoIp, byte tamanhoArquivoMb)
        {
            try
            {
                IPEndPoint enderecoCliente = new IPEndPoint(IPAddress.Parse(enderecoIp), 5656);
                using (Socket socketCliente = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP))
                {
                    string caminhoArquivo = string.Empty;
                    nomeArquivo = nomeArquivo.Replace("\\", "/");
                    while (nomeArquivo.IndexOf("/") > -1)
                    {
                        caminhoArquivo += nomeArquivo.Substring(0, nomeArquivo.IndexOf("/") + 1);
                        nomeArquivo = nomeArquivo.Substring(nomeArquivo.IndexOf("/") + 1);
                    }
                    byte[] nomeArquivoByte = Encoding.UTF8.GetBytes(nomeArquivo);
                    if (nomeArquivoByte.Length > (tamanhoArquivoMb * 1000) * 1024)
                    {
                        return false;
                    }
                    string caminhoCompleto = caminhoArquivo + nomeArquivo;
                    byte[] fileData = File.ReadAllBytes(caminhoCompleto);
                    byte[] clientData = new byte[4 + nomeArquivoByte.Length + fileData.Length];
                    byte[] nomeArquivoLen = BitConverter.GetBytes(nomeArquivoByte.Length);
                    nomeArquivoLen.CopyTo(clientData, 0);
                    nomeArquivoByte.CopyTo(clientData, 4);
                    fileData.CopyTo(clientData, 4 + nomeArquivoByte.Length);
                    socketCliente.Connect(enderecoCliente);
                    socketCliente.Send(clientData, 0, clientData.Length, 0);
                    socketCliente.Close();
                    return true;
                }
            }
            catch (IOException)
            {
                throw;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region //Sockets Server
        public static void IniciarServidor(string caminhoRecepcaoArquivos, string enderecoIP)
        {
            try
            {
                IPEndPoint enderecoServidor = new IPEndPoint(IPAddress.Parse(enderecoIP), 5656);
                using (Socket socketServidor = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP))
                {
                    Socket socketCliente;
                    socketServidor.Bind(enderecoServidor);
                    socketServidor.Listen(100);
                    while (true)
                    {
                        socketCliente = socketServidor.Accept();
                        socketCliente.ReceiveBufferSize = 16384;

                        var socketThread = new Thread(() =>
                        {
                            byte[] dadosCliente = new byte[1024 * 50000];
                            int tamanhoBytesRecebidos = socketCliente.Receive(dadosCliente, dadosCliente.Length, 0);
                            int tamnhoNomeArquivo = BitConverter.ToInt32(dadosCliente, 0);
                            string nomeArquivo = Encoding.UTF8.GetString(dadosCliente, 4, tamnhoNomeArquivo);
                            BinaryWriter bWrite = new BinaryWriter(File.Open(caminhoRecepcaoArquivos + nomeArquivo, FileMode.Append));
                            bWrite.Write(dadosCliente, 4 + tamnhoNomeArquivo, tamanhoBytesRecebidos - 4 - tamnhoNomeArquivo);

                            while (tamanhoBytesRecebidos > 0)
                            {
                                tamanhoBytesRecebidos = socketCliente.Receive(dadosCliente, dadosCliente.Length, 0);
                                if (tamanhoBytesRecebidos == 0)
                                {
                                    bWrite.Close();
                                }
                                else
                                {
                                    bWrite.Write(dadosCliente, 0, tamanhoBytesRecebidos);
                                }
                            }
                            bWrite.Close();
                            socketCliente.Shutdown(SocketShutdown.Both);
                            socketCliente.Close();
                        });
                        socketThread.Start();
                    }

                }
            }
            catch (SocketException)
            {
                throw;
            }
        }
        /*
        public static async Task<IEnumerable<DtoSistema>> GetDataFromBase(DtoSistema dados)
        {
            await Task.Delay(0);

            using (DalSistema restricao = new DalSistema())
            {
                try
                {
                    return await restricao.SelectObjectsFull(restricao.ComandoSelectFull, dados);
                }
                catch (Exception)
                {
                    throw;
                    //await LogSystem.WriteErrorLog($"Erro! Mensagem: {e.Message} | StackTrace: {e.StackTrace}", "BLLUsuario");
                }
            }
            //return null;
        }*/
        #endregion

        public bool transmissao(string arquivo, string pdv, string ip, ref string msg, bool modo)
        {
            #region
            ftp = new AsynchronousFtpUpLoader();
            string filename = arquivo;
            FileInfo objFile = new FileInfo(filename);
            msg = "";

            if (ftp.Main("ftp://" + ip + "/" + objFile.Name, objFile.ToString(), alwaysVariables.Atualizador, alwaysVariables.PalavraChave, ref msg, modo))
            {
                return true;
            }
            else
            {
                return false;
            }
            
            /*
            msg = "";

                // Create FtpWebRequest object 
                objFTPRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ip + "/" + objFile.Name));
                
                // Set Credintials
                objFTPRequest.Credentials = new NetworkCredential(alwaysVariables.Atualizador, alwaysVariables.PalavraChave);



                if (isValidConnection("ftp://" + ip + "/" + objFile.Name, alwaysVariables.Atualizador, alwaysVariables.PalavraChave))
                {

                    // By default KeepAlive is true, where the control connection is 
                    // not closed after a command is executed.
                    objFTPRequest.KeepAlive = false;

                    // Set the data transfer type.
                    objFTPRequest.UseBinary = true;

                    // Set content length
                    objFTPRequest.ContentLength = objFile.Length;

                    // Set request method
                    objFTPRequest.Method = WebRequestMethods.Ftp.UploadFile;

                    // Set buffer size
                    int intBufferLength = 16 * 1024;
                    byte[] objBuffer = new byte[intBufferLength];

                    // Opens a file to read
                    FileStream objFileStream = objFile.OpenRead();
                    /*using (FileStream objFileStream = objFile.OpenRead())
                    {
                        objFileStream.Read(objBuffer, 0, Convert.ToInt32(objFile.Length));
                    }*/
            /*
                    try
                    {
                        using (FtpWebResponse response =
                                (FtpWebResponse)objFTPRequest.
                                                   GetResponse())
                        {
                            // Get Stream of the file
                            using (Stream objStream = objFTPRequest.GetRequestStream())
                            {

                                int len = 0;

                                while ((len = objFileStream.Read(objBuffer, 0, intBufferLength)) != 0)
                                {
                                    // Write file Content 
                                    objStream.Write(objBuffer, 0, len);
                                }
                                objStream.Flush();
                                objStream.Close();
                                objFileStream.Close();
                                objFileStream = null;
                            }
                        }
                        /*FtpWebResponse ftpResponse = (FtpWebResponse)objFTPRequest.GetResponse();
                        string test = (ftpResponse.StatusDescription);
                        objFTPRequest.Abort();
                        ftpResponse.Close();*/
            /*
                        if(File.Exists(arquivo))
                        {
                            File.Delete(arquivo);
                        }
                    }
                    catch (Exception ex)
                    {
                        msg = ex.ToString();
                        return false;//throw ex;
                    }
                    return true;
                }
                else
                {
                    msg = "ERRO DE CONEXÃO COM SERVIDOR - ANALISE DE PARAMETROS: " + "ftp://" + ip + "/" + objFile.Name;
                    return false;//throw ex;
                }
            */
            #endregion
        }
        public void executaSqlBat(string Store, ref List<string> leitura)
        {
            if (leituraPasta.Count > 0)
            {
                StreamWriter sw;

                sw = new StreamWriter(recepcaoCaminho + "SQLexe" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + ".bat", false);
                sw.Write(" cd " + recepcaoCaminho + " && ");

                for (int i = 0; i < leituraPasta.Count; i++)
                {
                    try
                    {
                        if (!File.Exists(recepcaoCaminho + "SQLexe" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + ".bat"))
                        {
                            sw = new StreamWriter(recepcaoCaminho + "SQLexe" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + ".bat", false);
                            sw.Write(" cd " + recepcaoCaminho + " && ");
                        }

                        //string tabela = alwaysVariables.TypeComunicacao == "0" ? leituraPasta[i].Substring(65, leituraPasta[i].IndexOf(".") - 65) : leituraPasta[i].Substring(62, leituraPasta[i].IndexOf(".") - 62);
                        string tabela = alwaysVariables.TypeComunicacao == "0" ? leituraPasta[i].Substring(65, leituraPasta[i].IndexOf(".") - 65) : leituraPasta[i].Substring(55, leituraPasta[i].IndexOf(".") - 55);
                        //sw.Write("mysql --user=" + alwaysVariables.UserName + " --password=" + alwaysVariables.Senha + " -h " + alwaysVariables.LocalHost + " -A conector  --execute=\"SET foreign_key_checks=0; LOAD DATA LOCAL INFILE '" + leituraPasta[i] + "' REPLACE INTO TABLE conector." + tabela + " FIELDS TERMINATED BY '|' " + "\" --password=" + alwaysVariables.Senha + " && ");
                        //sw.Write("mysql --user=" + alwaysVariables.UserName + " --password=" + alwaysVariables.Senha + " -h " + alwaysVariables.LocalHost + " -A conector  --execute=\"SET foreign_key_checks=0; LOAD DATA LOCAL INFILE '" + leituraPasta[i] + "' REPLACE INTO TABLE conector." + tabela + " FIELDS TERMINATED BY '|' " + " && ");
                        sw.Write("mysql --user=" + alwaysVariables.UserName + " --password=" + alwaysVariables.Senha + " -h " + alwaysVariables.LocalHost + " -A conector  --execute=\"SET foreign_key_checks=0; LOAD DATA LOCAL INFILE '" + leituraPasta[i] + "' REPLACE INTO TABLE conector." + tabela + " FIELDS TERMINATED BY '|' " + "\"  && ");
                        sw.Write(" del " + leituraPasta[i].Replace("//", "\\") + " && ");

                        if (i != 0 && (i % 2) == 0)
                        {
                            sw.Write(" SET foreign_key_checks=1; && exit ");
                            sw.Close();
                            exeProcesso(recepcaoCaminho + "SQLexe" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + ".bat", 1, 0);//Not Time   
                        }

                    }
                    catch (Exception erro)
                    {
                        File.Delete(recepcaoCaminho + "SQLexe" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + ".bat");
                    }
                }
                if (File.Exists(recepcaoCaminho + "SQLexe" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + ".bat"))
                {
                    sw.Write(" SET foreign_key_checks=1; && exit ");
                    sw.Close();
                    exeProcesso(recepcaoCaminho + "SQLexe" + String.Format("{0:yyyyMMdd}", DateTime.Now) + String.Format("{0:00000000}", Convert.ToDouble(Store)) + ".bat", 1, 0);//Not Time   
                }
                leitura = leituraPasta;
            }
        }
        public void compactaLog(string strLog)
        {
            strLog = strLog.Replace(".log","");
            string newstr = strLog.Substring(16, 23);
            StreamWriter sw = new StreamWriter(@"c:\conector\log\" + "TRexeOne" + newstr + ".bat", false);
            //sw.Write("set data=CONFIG%date:~0,2%%date:~3,2%%date:~6,4% && ");
            sw.Write("cd " + @"c:\conector\log\" + " && ");
            sw.Write("\"" + alwaysVariables.ECF_UTIL + "\"" + " a -ibck -o+ -r -V1500000 \"" + strLog + ".rar" + "\"  \"" + @"c:\conector\log\" + newstr + ".log\" && ");
            sw.Write("del "+ strLog +".log && exit");
            sw.Close();
            exeProcesso(@"c:\conector\log\" + "TRexeOne" + newstr + ".bat", 1, 0);//Not Time
        }

        #region IDisposable Members

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        
        #endregion
    }
}


namespace Examples.System.Net
{
    public class FtpState
    {
        private ManualResetEvent wait;
        private FtpWebRequest request;
        private string fileName;
        private Exception operationException = null;
        string status;

        public FtpState()
        {
            wait = new ManualResetEvent(false);
        }

        public ManualResetEvent OperationComplete
        {
            get {return wait;}
        }

        public FtpWebRequest Request
        {
            get {return request;}
            set {request = value;}
        }

        public string FileName
        {
            get {return fileName;}
            set {fileName = value;}
        }
        public Exception OperationException
        {
            get {return operationException;}
            set {operationException = value;}
        }
        public string StatusDescription
        {
            get {return status;}
            set {status = value;}
        }
    }
    public class AsynchronousFtpUpLoader
    {  
        // Command line arguments are two strings:
        // 1. The url that is the name of the file being uploaded to the server.
        // 2. The name of the file on the local machine.
        //
        public AsynchronousFtpUpLoader()
        { }
        public bool Main(string url, string caminho, string user, string palavra, ref string msg, bool modo)
        {
            msg = "";
            // Create a Uri instance with the specified URI string.
            // If the URI is not correctly formed, the Uri constructor
            // will throw an exception.
            ManualResetEvent waitObject;

            Uri target = new Uri (url);
            string fileName = caminho;
            FtpState state = new FtpState();
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(target);
            request.Method = WebRequestMethods.Ftp.UploadFile;

            // This example uses anonymous logon.
            // The request is anonymous by default; the credential does not have to be specified. 
            // The example specifies the credential only to
            // control how actions are logged on the server.

            request.Credentials = new NetworkCredential(user,palavra);

            // Store the request in the object that we pass into the
            // asynchronous operations.
            state.Request = request;
            state.FileName = fileName;

            // Get the event to wait on.
            waitObject = state.OperationComplete;

            // Asynchronously get the stream for the file contents.
            request.BeginGetRequestStream(
                new AsyncCallback (EndGetStreamCallback),
                state
            );

            // Block the current thread until all operations are complete.
            waitObject.WaitOne();

            // The operations either completed or threw an exception.
            if (state.OperationException != null)
            {
                msg = state.OperationException.ToString();
                return false;//throw ex;
            }
            else
            {
                if (File.Exists(state.FileName))
                {
                    if (modo == true)
                    {
                        File.Delete(state.FileName);
                    }
                }
                msg = "The operation completed - " +  state.StatusDescription;
                return true;//throw ex;
            }
        }
        private static void EndGetStreamCallback(IAsyncResult ar)
        {
            FtpState state = (FtpState) ar.AsyncState;

            Stream requestStream = null;
            // End the asynchronous call to get the request stream.
            try
            {
                requestStream = state.Request.EndGetRequestStream(ar);
                // Copy the file contents to the request stream.
                const int bufferLength = 2048;
                byte[] buffer = new byte[bufferLength];
                int count = 0;
                int readBytes = 0;
                using (FileStream stream = File.OpenRead(state.FileName))
                {
                    do
                    {
                        readBytes = stream.Read(buffer, 0, bufferLength);
                        requestStream.Write(buffer, 0, readBytes);
                        count += readBytes;
                    }
                    while (readBytes != 0);
                }

                // IMPORTANT: Close the request stream before sending the request.
                requestStream.Close();

                /*if (File.Exists(state.FileName))
                {
                        File.Delete(state.FileName);
                }*/
                // Asynchronously get the response to the upload request.
                state.Request.BeginGetResponse(
                    new AsyncCallback (EndGetResponseCallback), 
                    state
                );

                
            } 
            // Return exceptions to the main application thread.
            catch (Exception e)
            {
                state.OperationException = e;
                state.OperationComplete.Set();
                return;
            }

        }

        // The EndGetResponseCallback method  
        // completes a call to BeginGetResponse.
        private static void EndGetResponseCallback(IAsyncResult ar)
        {
            FtpState state = (FtpState) ar.AsyncState;
            FtpWebResponse response = null;
            try 
            {
                response = (FtpWebResponse) state.Request.EndGetResponse(ar);
                response.Close();
                state.StatusDescription = response.StatusDescription;
                // Signal the main application thread that 
                // the operation is complete.
                state.OperationComplete.Set();
            }
            // Return exceptions to the main application thread.
            catch (Exception e)
            {
                state.OperationException = e;
                state.OperationComplete.Set();
            }
        }
    }
}