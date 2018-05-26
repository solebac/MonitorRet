using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace MonitorRet
{
    public partial class Monitor : Form
    {
        public Monitor(string verifica)
        {
            if (verifica != null)
            {
                InitializeComponent();
                RequestStop(false);
                carrega_variaveis();
                insertCarga = new instrucaol();
                banco = new dados(alwaysVariables.UserName, alwaysVariables.LocalHost, alwaysVariables.Senha, alwaysVariables.Schema);
            }
        }

        //#############################################################    Variaveis #################################################################
        #region 
        [DllImport("kernel32.dll", EntryPoint = "GetPrivateProfileString")]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);
        [DllImport("kernel32.dll", EntryPoint = "WritePrivateProfileString")]
        private static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);
            private dados banco;
            private conector_full_variable alwaysVariables = new conector_full_variable();
            private string fileSecret = Path.Combine(Directory.GetCurrentDirectory(), "c:\\windows\\soberanu.ini");
            private int auxConsistencia = 0;
            private int countRows, countField;
            //eventos workObject = new eventos();
            Thread servico;
            //##############################Begin
            private volatile bool _flagStop;
            private string[] regra = new string[14] { "nf", "nfce", "nfItem", "nfImposto", "cupom_cabecalho", "cupom_detalhes", "cupom_movimento", "movimentoDia", "detalhe_reducao", "detalhe_reducao_aliquota", "fechamentoCaixa", "cartao", "cheque", "convenioMovimento" };
            private string[] path = new string[14];
            private cupomImport Import;
            private instrucaol insertCarga = new instrucaol();
            string[,] vetorCx;
            string[] vetorStore;
            string[,] vetorStoreRelacionada;
            List<string> leituraPasta = new List<string>();
            List<string> leituraPasta2 = new List<string>();
        #endregion
        //#############################################################End Variaveis #################################################################


            //############################################################Bloco de Funçoes################################################################
            #region

            void conector_import_carga(string folderRepc, string lojas, int liberaCarga)
            {
                string[] dirs = Directory.GetFiles(folderRepc, "*TRANSMISSAO*");
                if (liberaCarga == 1)
                {
                    foreach (string dir in dirs)
                    {

                        if (!File.Exists(alwaysVariables.TransmissaoWindows + "break.txt") &&  File.Exists(dir))
                        {
                            if (dir.ToString().IndexOf("TRANSMISSAO") != -1)
                            {
                                bool valida = false;
                                insertCarga.preparaSql_Import(dir, lojas);
                                insertCarga.carregaSql_Import();
                                insertCarga.executaSqlBat_Import(lojas, ref valida);
                                if (valida == false)
                                {
                                    ltvEventosHistorico.Invoke((MethodInvoker)delegate { ltvEventosHistorico.Items.Add("REJEIÇÃO NA ATUALIZAÇÃO LOJA, VERIFIQUE OS STATUS"); });
                                }
                            }

                            File.Delete(dir);

                        }
                    }
                }
            }
            void conector_start_thread()
            {
                btnEventosStart.Enabled = false;
                if (cmbEventosLoja.Text != "" || (cmbEventosLoja.SelectedIndex == -1 && chkEventosTodasLojas.Checked == true))
                {
                    if (ltvEventosHistorico.Items.Count > 0)
                    {
                        ltvEventosHistorico.Items.Clear();
                        ltvEventosHistorico.Items.Add("REINICIO MONITOR CONECTOR - DATA " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now), 3);
                        ltvEventosHistorico.Items.Add("ELEMINAÇÃO DE LOG's CONSTANTES - AGUARDANDO DADOS", 3);
                    }

                    servico = new Thread(Varrefolder);

                    RequestStop(false);

                    servico.Start();

                    while (!servico.IsAlive) ;

                    Thread.Sleep(1000);
                }
                else
                {
                    btnEventosStart.Enabled = true;
                    RequestStop(true);
                    try
                    {
                        ltvEventosHistorico.Items.Add("STOP MONITOR CONECTOR " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " LOJA NÃO DEFINIDA. ", 3);
                        servico.Join();
                        servico.Abort();
                    }
                    catch (Exception)
                    {

                    }
                    MessageBox.Show("Loja não definida...!");
                    cmbEventosLoja.Select();
                }
            }
            public static string getFile(string localizacao)
            {
                if (localizacao.IndexOf("\\conector") != -1)
                {
                    localizacao = localizacao.Replace("\\conector", "");
                }
                return localizacao;
            }

            public static string getValue(string secao, string chave, string fileName)
            {
                int carateres = 256;
                StringBuilder buffer = new StringBuilder(carateres);
                string sdefault = "";
                if (GetPrivateProfileString(secao, chave, sdefault, buffer, carateres, fileName) != 0)
                {
                    return buffer.ToString();
                }
                else
                {
                    // Verifica o último erro Win32.
                    int err = Marshal.GetLastWin32Error();
                    return null;
                }
            }
            void carrega_variaveis()
            {
                alwaysVariables.Perfil = getValue("banco_smartPDV", "Perfil", fileSecret);
                alwaysVariables.PubKeyFile = getValue("loja", "keyPass", fileSecret);
                alwaysVariables.PubCert = getValue("loja", "keyCert", fileSecret);
                alwaysVariables.Schema = getValue("banco", "schema", fileSecret);
                alwaysVariables.UserName = getValue("banco", "username", fileSecret);
                alwaysVariables.Senha = getValue("banco", "password", fileSecret);
                alwaysVariables.Atualizador = getValue("ftp", "atualizador", fileSecret);
                alwaysVariables.PalavraChave = getValue("ftp", "palavraChave", fileSecret);
                alwaysVariables.StringConector = getValue("ftp","stringConector", fileSecret);
                alwaysVariables.LocalHost = getValue("banco", "server", fileSecret);
                alwaysVariables.Store = getValue("loja", "store", fileSecret);
                alwaysVariables.ImportCarga = Convert.ToInt32(getValue("banco", "ImportCarga", fileSecret));
                alwaysVariables.ECF_UTIL = getValue("Printer", "ecfUtil", fileSecret);
                alwaysVariables.TypeComunicacao = getValue("banco", "typeComunicao", fileSecret);
                alwaysVariables.TransmissaoWindows = getValue("banco", "transmissaoWindows", fileSecret);
                alwaysVariables.TransmissaoLinux = getValue("banco", "transmissaoLinux", fileSecret);
                alwaysVariables.RecepcaoLinux = getValue("banco", "recepcaoLinux", fileSecret);
                alwaysVariables.RecepcaoWindows = getValue("banco", "recepcaoWindows", fileSecret);
                alwaysVariables.EscritaCompatilhada = getValue("banco", "escritaCompartilhada", fileSecret);
                alwaysVariables.EscritaCompatilhadaImport = getValue("banco", "escritaCompartilhadaImport", fileSecret);
            }
            #endregion
        //############################################################# END Bloco de Funçoes #######################################################
        //#############################################################Implementação da Thread######################################################
            #region //Funções e Metodos
            public void RequestStop(bool para)
            {
                _flagStop = para;
            }

            public string renomeiaLog()
            {
                return @"c:\conector\log\" + "conector" + String.Format("{0:yyyyMMdd-HHmmss}", DateTime.Now) + ".log";
            }
            public void Varrefolder()
            {
                using (cupomImport Import = new cupomImport())
                {
                    alwaysVariables.caminhoLog = renomeiaLog();
                    StreamWriter sw = new StreamWriter(alwaysVariables.caminhoLog, false);

                    while (!_flagStop)
                    {
                        string msgErro = "";
                        string[] fileRecepcao = Directory.GetFiles(@"c:\conector\recepcao\");
                        string[] fileTransmissao = Directory.GetFiles(@"C:\conector\Transmissao\");
                        string data = "";
                        string pdv = "";
                        string store = "";
                        leituraPasta.Clear();
                        leituraPasta2.Clear();
                        Import.setVetor();
                        bool valida = false;
                        bool parada = true;

                        for (int p = 0; p < Import._Store.Length; p++)
                        {

                            if (fileRecepcao.GetLength(0) > 0)
                            {

                                conector_import_carga(@"c:\conector\recepcao\", alwaysVariables.Store, alwaysVariables.ImportCarga);

                                for (int i = 0; i < regra.Length; i++)
                                {
                                    if (Import.verifica_arquivo(regra[i]) > 0)
                                    {
                                        path[i] = Import.varredura("MVconector", regra[i], Import._Store[p]);
                                    }
                                }
                                for (int j = 0; j < path.Length; j++)
                                {
                                    if (path != null)
                                    {
                                        if (File.Exists(path[j]))
                                        {
                                            ltvEventosHistorico.Invoke((MethodInvoker)delegate { ltvEventosHistorico.Items.Add("Recepção do Arquivo de Conferencia => " + "Store: " + (store == "" ? alwaysVariables.Store : store) + " TERMINAL: " + pdv + " Atualização " + path[j] + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco, 1); });
                                            sw.Write("Recepção do Arquivo de Conferencia => " + "Store: " + (store == "" ? alwaysVariables.Store : store) + " TERMINAL: " + pdv + " Atualização " + path[j] + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco + "\r\n");
                                            //ltvEventosHistorico.Items.Add(path[j] + "Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now), 1); 
                                            Import.carregaSql(path[j], j, ref leituraPasta);

                                            for (int i = 0; i < leituraPasta.Count; i++)
                                            {
                                                ltvEventosHistorico.Invoke((MethodInvoker)delegate { ltvEventosHistorico.Items.Add("Recepção Arquivos Selecionados => " + "Store: " + (store == "" ? alwaysVariables.Store : store) + " TERMINAL: " + pdv + " Atualização " + leituraPasta[i].ToString() + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco, 1); });
                                                sw.Write("Recepção Arquivos Selecionados => " + "Store: " + (store == "" ? alwaysVariables.Store : store) + " TERMINAL: " + pdv + " Atualização " + leituraPasta[i].ToString() + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco + "\r\n");
                                            }

                                            Import.executaSqlBat(Import._Store[p], ref leituraPasta2);

                                            for (int i = 0; i < leituraPasta2.Count; i++)
                                            {
                                                ltvEventosHistorico.Invoke((MethodInvoker)delegate { ltvEventosHistorico.Items.Add("Recepção Arquivos Selecionados => " + "Store: " + (store == "" ? alwaysVariables.Store : store) + " TERMINAL: " + pdv + " Atualização " + leituraPasta2[i].ToString() + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco, 1); });
                                                sw.Write("Recepção Arquivos Selecionados => " + "Store: " + (store == "" ? alwaysVariables.Store : store) + " TERMINAL: " + pdv + " Atualização " + leituraPasta2[i].ToString() + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco + "\r\n");
                                            }
                                        }
                                    }
                                }
                            }
                            if (fileRecepcao.GetLength(0) > 0)
                            {
                                for (int y = 0; y < fileRecepcao.GetLength(0); y++)
                                {
                                    int pos = fileRecepcao[y].ToString().IndexOf("rar");

                                    if ((pos < 0) && !File.Exists(alwaysVariables.TransmissaoWindows + "break.txt"))
                                    {
                                        File.Delete(fileRecepcao[y]);
                                    }
                                }
                            }
                            Thread.Sleep(6000);
                            if (fileTransmissao.GetLength(0) > 0 && !File.Exists(alwaysVariables.TransmissaoWindows + "break.txt") &&
                                !File.Exists(@"C:\conector\transmissao\semaforo.txt"))
                            {
                                bool aviso = true;
                                for (int i = 0; i < Import._Store.Length; i++)
                                {
                                    if (fileTransmissao.GetLength(0) > 0 && !File.Exists(alwaysVariables.TransmissaoWindows + "break.txt") &&
                                        !File.Exists(@"C:\conector\transmissao\semaforo.txt"))
                                    {
                                        if (i == 0 && aviso == true)
                                        {
                                            ltvEventosHistorico.Invoke((MethodInvoker)delegate { ltvEventosHistorico.Items.Add("Mensagem Servidor => " + "Arquivo lançado na pasta transmissão: " + fileTransmissao[i].ToString() + " RESPOSTA ... AGUARDANDO ENVIO ... "+ " TERMINAL: " + pdv + " RETORNO SERVIDOR " + msgErro.Replace("\r\n", "") + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco, 1); });
                                            aviso = false;
                                        }
                                        #region
                                        //Atualiza Concentrador
                                        for (int b = 0; b < Import._StoreRelacionamento.GetLength(0); b++)
                                        {
                                            if (alwaysVariables.Store != Import._StoreRelacionamento[b, 0])
                                            {

                                                for (int v = 0; v < fileTransmissao.GetLength(0); v++)
                                                {
                                                    if ((fileTransmissao[v].ToString().IndexOf("TRANSMISSAO") != -1 && 
                                                        alwaysVariables.Store != Import._StoreRelacionamento[v, 0] &&
                                                        File.Exists(fileTransmissao[v].ToString())) || (fileTransmissao.GetLength(0) == 1))
                                                    {
                                                        for (int g = 0; g < fileTransmissao.GetLength(0); g++)
                                                        {
                                                            if ( !File.Exists(@"C:\conector\transmissao\semaforo.txt") && !File.Exists(alwaysVariables.TransmissaoWindows + "break.txt") && (fileTransmissao[g].ToString().IndexOf(".bat") != -1 || fileTransmissao[g].ToString().IndexOf("TRexeTwo") != -1))
                                                            {
                                                                parada = false;
                                                            }
                                                        }
                                                        if (parada == true)
                                                        {
                                                            if (!File.Exists(@"C:\conector\transmissao\semaforo.txt") && !File.Exists(alwaysVariables.TransmissaoWindows + "break.txt") && fileTransmissao.GetLength(0) == 1)
                                                            {
                                                                try
                                                                {
                                                                    store = fileTransmissao[0].Substring(fileTransmissao[0].ToString().IndexOf("TRANSMISSAO") - 16, 8);
                                                                    pdv = fileTransmissao[0].Substring(fileTransmissao[0].ToString().IndexOf("TRANSMISSAO") - 8, 8);
                                                                }
                                                                catch (Exception)
                                                                {
                                                                    fileTransmissao = Directory.GetFiles(@"C:\conector\Transmissao\");
                                                                }

                                                                for (int z = 0; z < Import._StoreRelacionamento.GetLength(0); z++)
                                                                {
                                                                    if (!File.Exists(@"C:\conector\transmissao\semaforo.txt") && !File.Exists(alwaysVariables.TransmissaoWindows + "break.txt") && Convert.ToInt32(Import._StoreRelacionamento[z, 0]) == Convert.ToInt32(store == "" ? "0" : store) && Convert.ToString(Import._StoreRelacionamento[z, 1]) != "localhost")
                                                                    {
                                                                        if (!File.Exists(@"C:\conector\transmissao\semaforo.txt") && !File.Exists(alwaysVariables.TransmissaoWindows + "break.txt") && ((BllSocketClient.EnviarArquivo(fileTransmissao[0], Import._StoreRelacionamento[z, 1], 1) == true) /*Import.transmissao(fileTransmissao[0], Import._StoreRelacionamento[z, 0], Import._StoreRelacionamento[z, 1], ref msgErro, false)*/))//Alter recepcao)
                                                                        {
                                                                            valida = true;
                                                                            ltvEventosHistorico.Invoke((MethodInvoker)delegate { ltvEventosHistorico.Items.Add("Mensagem Servidor => " + "Store: " + Import._StoreRelacionamento[z, 0] + " TERMINAL: " + Import._StoreRelacionamento[z, 1] + " RETORNO SERVIDOR " + msgErro.Replace("\r\n", "") + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco, 1); });
                                                                            ltvEventosHistorico.Invoke((MethodInvoker)delegate { ltvEventosHistorico.Items.Add("Recepção de Arquivo => " + "Store: " + Import._StoreRelacionamento[z, 0] + " TERMINAL: " + Import._StoreRelacionamento[z, 1] + " Envio: " + fileTransmissao[0].ToString() + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco, 1); });
                                                                            sw.Write("Transmissao de Arquivo => " + "Host: " + Import._StoreRelacionamento[v, 1] + "Store: " + Import._StoreRelacionamento[z, 0] + " CONCENTRADOR: " + Import._StoreRelacionamento[z, 1] + " Envio: " + fileTransmissao[0].ToString() + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco + "\r\n");
                                                                        }
                                                                        else
                                                                        {
                                                                            if (valida == true)
                                                                            {
                                                                                ltvEventosHistorico.Invoke((MethodInvoker)delegate { ltvEventosHistorico.Items.Add("ERRO Transmissao => " + "Store: " + Import._StoreRelacionamento[v, 0] + " CONCENTRADOR: " + Import._StoreRelacionamento[z, 1] + " Envio: " + msgErro + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco, 1); });
                                                                            }
                                                                            sw.Write("ERRO Transmissao => " + "Store: " + Import._StoreRelacionamento[z, 0] + " TERMINAL: " + Import._StoreRelacionamento[z, 1] + " Envio: " + msgErro + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco + "\r\n");
                                                                            valida = false;
                                                                        }
                                                                        if (!File.Exists(alwaysVariables.TransmissaoWindows + "break.txt") && pdv != "" && Convert.ToInt32(pdv == "" ? "0" : pdv) == 0)
                                                                        {
                                                                            vetorCx = Import.conector_find_pdv(store);
                                                                            if (vetorCx.GetLength(0) == 0)
                                                                            {
                                                                                if (!File.Exists(@"C:\conector\transmissao\semaforo.txt"))
                                                                                {
                                                                                    File.Delete(fileTransmissao[0]);
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                               // ltvEventosHistorico.Invoke((MethodInvoker)delegate { ltvEventosHistorico.Items.Add("Transmissao Persistente=> " + "Store: " + Import._StoreRelacionamento[v, 0] + " CONCENTRADOR: " + Import._StoreRelacionamento[z, 1] + " Envio: " + msgErro + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco, 1); });
                                                                            }
                                                                            vetorCx = null;
                                                                            fileTransmissao = Directory.GetFiles(@"C:\conector\Transmissao\");
                                                                        }
                                                                    }
                                                                }
                                                            }else
                                                                if (!File.Exists(alwaysVariables.TransmissaoWindows + "break.txt") && ((BllSocketClient.EnviarArquivo(fileTransmissao[v], Import._StoreRelacionamento[v, 1], 1) == true)/*Import.transmissao(fileTransmissao[v], Import._StoreRelacionamento[v, 0], Import._StoreRelacionamento[v, 1], ref msgErro, false)*/))//Alter recepcao)
                                                            {
                                                                valida = true;
                                                                ltvEventosHistorico.Invoke((MethodInvoker)delegate { ltvEventosHistorico.Items.Add("Mensagem Servidor => " + "Store: " + Import._StoreRelacionamento[v, 0] + " TERMINAL: " + Import._StoreRelacionamento[v, 1] + " RETORNO SERVIDOR " + msgErro.Replace("\r\n", "") + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco, 1); });
                                                                ltvEventosHistorico.Invoke((MethodInvoker)delegate { ltvEventosHistorico.Items.Add("Recepção de Arquivo => " + "Store: " + Import._StoreRelacionamento[v, 0] + " TERMINAL: " + Import._StoreRelacionamento[v, 1] + " Envio: " + fileTransmissao[v].ToString() + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco, 1); });
                                                                sw.Write("Transmissao de Arquivo => " + "Host: " + Import._StoreRelacionamento[v, 1] + "Store: " + Import._StoreRelacionamento[v, 0] + " CONCENTRADOR: " + Import._StoreRelacionamento[v, 1] + " Envio: " + fileTransmissao[v].ToString() + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco + "\r\n");
                                                            }
                                                            else
                                                            {
                                                                if (valida == true)
                                                                {
                                                                    ltvEventosHistorico.Invoke((MethodInvoker)delegate { ltvEventosHistorico.Items.Add("ERRO Transmissao => " + "Store: " + Import._StoreRelacionamento[v, 0] + " CONCENTRADOR: " + Import._StoreRelacionamento[v, 1] + " Envio: " + msgErro + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco, 1); });
                                                                }
                                                                sw.Write("ERRO Transmissao => " + "Store: " + Import._StoreRelacionamento[v, 0] + " TERMINAL: " + Import._StoreRelacionamento[v, 1] + " Envio: " + msgErro + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco + "\r\n");
                                                                valida = false;
                                                            }
                                                            parada = true;
                                                        }
                                                        if (!File.Exists(alwaysVariables.TransmissaoWindows + "break.txt") && ((v + 1) == fileTransmissao.GetLength(0)) && alwaysVariables.ImportCarga == 1)
                                                        {
                                                            b = v;//Trata saida do loop Loja.
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        #endregion

                                        for (int y = 0; y < fileTransmissao.GetLength(0); y++)
                                        {
                                            if (!File.Exists(alwaysVariables.TransmissaoWindows + "break.txt") && (fileTransmissao[y].ToString().IndexOf("TRANSMISSAO") != -1 && File.Exists(fileTransmissao[y].ToString())))
                                            {
                                                try
                                                {
                                                    pdv = fileTransmissao[y].Substring(fileTransmissao[y].ToString().IndexOf("TRANSMISSAO") - 8, 8);
                                                    store = fileTransmissao[y].Substring(fileTransmissao[y].ToString().IndexOf("TRANSMISSAO") - 16, 8);
                                                    data = fileTransmissao[y].Substring(fileTransmissao[y].ToString().IndexOf("TRANSMISSAO") - 24, 8);
                                                    vetorStore = Import.vetorStore;
                                                    vetorCx = Import.conector_find_pdv(store);
                                                }
                                                catch (Exception)
                                                {
                                                    fileTransmissao = Directory.GetFiles(@"C:\conector\Transmissao\");
                                                }

                                                if (store == "0")
                                                {
                                                    for (int x = 0; x < vetorStore.Length; x++)
                                                    {
                                                        vetorCx = Import.conector_find_pdv(vetorStore[x]); //Carrega terminais

                                                        for (int e = 0; e < vetorCx.GetLength(0); e++)
                                                        {
                                                            for (int v = 0; v < fileTransmissao.GetLength(0); v++)
                                                            {
                                                                if (!File.Exists(alwaysVariables.TransmissaoWindows + "break.txt") && (fileTransmissao[v].ToString().IndexOf("TRANSMISSAO") != -1))
                                                                {
                                                                    for (int g = 0; g < fileTransmissao.GetLength(0); g++)
                                                                    {
                                                                        if (fileTransmissao[g].ToString().IndexOf(".bat") != -1 || fileTransmissao[g].ToString().IndexOf("TRexeTwo") != -1)
                                                                        {
                                                                            parada = false;
                                                                        }
                                                                    }
                                                                    if (parada == true)
                                                                    {
                                                                        //if (!File.Exists(alwaysVariables.TransmissaoWindows + "break.txt") && (/*Import.transmissao(fileTransmissao[v], vetorCx[e, 0], vetorCx[e, 1], ref msgErro, true)*/(BllSocketClient.EnviarArquivo(fileTransmissao[v], vetorCx[e, 1], 1) == true)))
                                                                        if (!File.Exists(alwaysVariables.TransmissaoWindows + "break.txt") && (Import.transmissao(fileTransmissao[v], vetorCx[e, 0], vetorCx[e, 1], ref msgErro, true)))
                                                                        {
                                                                            valida = true;
                                                                            ltvEventosHistorico.Invoke((MethodInvoker)delegate { ltvEventosHistorico.Items.Add("Mensagem Servidor => " + "Store: " + store + " TERMINAL: " + pdv + " RETORNO SERVIDOR " + msgErro.Replace("\r\n", "") + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco, 1); });
                                                                            ltvEventosHistorico.Invoke((MethodInvoker)delegate { ltvEventosHistorico.Items.Add("Recepção de Arquivo => " + "Store: " + store + " TERMINAL: " + pdv + " Envio: " + fileTransmissao[y].ToString() + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco, 1); });
                                                                            sw.Write("Transmissao de Arquivo => " + "Store: " + store + " TERMINAL: " + pdv + " Envio: " + fileTransmissao[v].ToString() + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco + "\r\n");
                                                                        }
                                                                        else
                                                                        {
                                                                            sw.Write("ERRO Transmissao => " + "Store: " + store + " TERMINAL: " + pdv + " Envio: " + msgErro + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco + "\r\n");
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                            fileTransmissao = Directory.GetFiles(@"C:\conector\Transmissao\");
                                                            /*if (vetorCx[e, 0] != null)
                                                            {
                                                                Import.transmissao(fileRecepcao[y], vetorCx[e, 0], vetorCx[e, 1], ref msgErro);
                                                            }*/
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    vetorCx = Import.conector_find_pdv(store);
                                                    //Atualiza Caixas
                                                    for (int b = 0; b < vetorCx.GetLength(0); b++)
                                                    {
                                                        if (vetorCx[b, 0] != null)
                                                        {
                                                            for (int v = 0; v < fileTransmissao.GetLength(0); v++)
                                                            {
                                                                string varStore = fileTransmissao[v].Substring(fileTransmissao[y].ToString().IndexOf("TRANSMISSAO") - 16, 8);

                                                                if (varStore == store && !File.Exists(alwaysVariables.TransmissaoWindows + "break.txt") && (fileTransmissao[v].ToString().IndexOf("TRANSMISSAO") != -1))
                                                                {
                                                                    for (int g = 0; g < fileTransmissao.GetLength(0); g++)
                                                                    {
                                                                        if (fileTransmissao[g].ToString().IndexOf(".bat") != -1 || fileTransmissao[g].ToString().IndexOf("TRexeTwo") != -1)
                                                                        {
                                                                            parada = false;
                                                                        }
                                                                    }
                                                                    if (parada == true)
                                                                    {
                                                                        //if (!File.Exists(alwaysVariables.TransmissaoWindows + "break.txt") &&(BllSocketClient.EnviarArquivo(fileTransmissao[v], vetorCx[b, 1], 1) == true))
                                                                        if (!File.Exists(alwaysVariables.TransmissaoWindows + "break.txt") && (Import.transmissao(fileTransmissao[v], vetorCx[b, 0], vetorCx[b, 1], ref msgErro, true)))//Alter recepcao)
                                                                        {
                                                                            valida = true;
                                                                            ltvEventosHistorico.Invoke((MethodInvoker)delegate { ltvEventosHistorico.Items.Add("Mensagem Servidor => " + "Store: " + store + " TERMINAL: " + pdv + " RETORNO SERVIDOR " + msgErro.Replace("\r\n", "") + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco, 1); });
                                                                            ltvEventosHistorico.Invoke((MethodInvoker)delegate { ltvEventosHistorico.Items.Add("Recepção de Arquivo => " + "Store: " + store + " TERMINAL: " + pdv + " Envio: " + fileTransmissao[y].ToString() + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco, 1); });
                                                                            sw.Write("Transmissao de Arquivo => " + "Host envio: " + Import._StoreRelacionamento[v, 1]  + "Store: " + store + " TERMINAL: " + pdv + " Envio: " + fileTransmissao[v].ToString() + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco + "\r\n");
                                                                        }
                                                                        else
                                                                        {
                                                                            if (valida == true)
                                                                            {
                                                                                ltvEventosHistorico.Invoke((MethodInvoker)delegate { ltvEventosHistorico.Items.Add("ERRO Transmissao => " + "Store: " + store + " TERMINAL: " + pdv + " Envio: " + msgErro + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco, 1); });
                                                                            }
                                                                            else
                                                                            {
                                                                                ltvEventosHistorico.Invoke((MethodInvoker)delegate { ltvEventosHistorico.Items.Add("ERRO Transmissao => " + "Store: " + store + " MAQUINA: " + vetorCx[b, 1] + " Envio: " + msgErro + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco, 1); });
                                                                            }
                                                                            sw.Write("ERRO Transmissao => " + "Store: " + store + " TERMINAL: " + pdv + " Envio: " + msgErro + " Processamento: " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now) + " Versao EXE." + alwaysVariables.VersaoSystem + " Versao Banco" + alwaysVariables.VersaoBanco + "\r\n");
                                                                            valida = false;
                                                                        }
                                                                        parada = true;
                                                                    }
                                                                }
                                                            }
                                                            fileTransmissao = Directory.GetFiles(@"C:\conector\Transmissao\");
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        for (int y = 0; y < fileTransmissao.GetLength(0); y++)
                                        {
                                            if (!File.Exists(alwaysVariables.TransmissaoWindows + "break.txt") && File.Exists(fileTransmissao[y].ToString()) && valida == true)
                                            {
                                                if (!File.Exists(@"C:\conector\transmissao\semaforo.txt"))
                                                {
                                                    File.Delete(fileTransmissao[y].ToString());
                                                }
                                            }
                                            fileTransmissao = Directory.GetFiles(@"C:\conector\Transmissao\");   
                                        }
                                        
                                    }
                                }
                            }
                        }

                        Thread.Sleep(6000);
                        if (fileTransmissao.GetLength(0) > 0 && !File.Exists(@"C:\conector\transmissao\semaforo.txt"))
                        {
                            for (int y = 0; y < fileTransmissao.GetLength(0); y++)
                            {
                                try
                                {
                                    int pos = fileTransmissao[y].ToString().IndexOf("rar");
                                    if (!File.Exists(alwaysVariables.TransmissaoWindows + "break.txt") && pos < 0)
                                    {
                                        if (!File.Exists(@"C:\conector\transmissao\semaforo.txt"))
                                        {
                                            File.Delete(fileTransmissao[y]);
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }
                            fileTransmissao = Directory.GetFiles(@"C:\conector\Transmissao\");
                        }
                    }//End While

                    sw.Close();

                    Import.compactaLog(alwaysVariables.caminhoLog);
                }
            }

            #endregion
        //#############################################################End Implementacao############################################################
        //############################################################Regra de negocio Banco########################################################
        #region
            public void carrega_infor(string store)
            {
                int retorno = 0;
                try
                {
                    banco.abreConexao();
                    banco.iniciarTransacao();
                    banco.singleTransaction("select versaoBanco, versaoSystem from system");
                    banco.procedimentoRead();
                    if (banco.retornaRead().Read() == true)
                    {
                        alwaysVariables.VersaoBanco = banco.retornaRead().GetString(0);
                        alwaysVariables.VersaoSystem = banco.retornaRead().GetString(1);
                    }
                    else retorno = 0;
                    banco.fechaRead();
                    banco.commit();
                }
                catch (Exception erro)
                {
                    banco.rollback();
                    MessageBox.Show(erro.Message, "Caro Usúario", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                finally
                {
                    banco.fechaConexao();
                }
            }
            public void conector_find_store()
            {
                cmbEventosLoja.Items.Clear();
                countRows = countField = 0;
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
                    MessageBox.Show(erro.Message, "Erro não identificado, entre contato como revendedor"); auxConsistencia = 1;
                }
                finally
                {
                    if (auxConsistencia == 0)
                    {
                        countField = banco.retornaSet().Tables[0].Columns.Count;
                        countRows = banco.retornaSet().Tables[0].DefaultView.Count;
                        if (countField > 0)
                        {
                            for (int i = 0; i < countRows; i++)
                            {
                                cmbEventosLoja.Items.Add(banco.retornaSet().Tables[0].Rows[i][1]);
                            }
                        }
                    }
                    banco.fechaConexao();
                }
            }
            public void conector_find_loja(string id)
            {
                auxConsistencia = 0;
                try
                {
                    banco.abreConexao();
                    banco.iniciarTransacao();
                    banco.startTransaction("conector_find_loja");
                    banco.addParametro("tipo", "1");
                    banco.addParametro("find_loja", id);
                    banco.procedimentoRead();
                    if (banco.retornaRead().Read() == true)
                    {
                        alwaysVariables.RazaoStore = banco.retornaRead().GetString(1);
                        alwaysVariables.TypeComissao = banco.retornaRead().GetString(37);
                        alwaysVariables.CNPJ = banco.retornaRead().GetString(3);
                        alwaysVariables.RamoAtuacao = banco.retornaRead().GetString(24);
                    }
                    else
                    {
                        MessageBox.Show("Loja não possui razão, ou não existe, verifique a messagem.", "IMPOSSÍVEL PROSSEGUIR...!"); 
                        auxConsistencia = 1;
                    }
                    banco.fechaRead();
                    banco.commit();
                }
                catch (Exception erro)
                {
                    banco.rollback();
                    MessageBox.Show("Loja não possui razão, ou não existe, verifique a messagem! '" + erro.Message + "'.", "Erro não identificado, entre contato como revendedor"); auxConsistencia = 1;
                }
                finally
                {
                    banco.fechaConexao();
                    if (auxConsistencia == 1)
                    {
                        string[] fileRecepcao = Directory.GetFiles(@"c:\conector\recepcao\");
                        if (fileRecepcao.GetLength(0) > 0 && fileRecepcao[0].ToString().IndexOf("TRANSMISSAO") != -1)
                        {
                            if (MessageBox.Show("Um arquivo de atualização foi encontrado pelo sistema, deseja analisa-ló para uma possivel atualização de carga de dados!", "Caro Usúario", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                conector_import_carga(@"c:\conector\recepcao\", alwaysVariables.Store, alwaysVariables.ImportCarga);
                            }
                            else
                            {
                                Environment.Exit(0);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Loja indefinida para o uso do conector, esse programa será encerrado!"); 
                            Environment.Exit(0);
                        }
                    }
                }
            }
        #endregion

            private void Monitor_Load(object sender, EventArgs e)
            {
                conector_find_loja(alwaysVariables.Store);
                carrega_infor(alwaysVariables.Store);
                ltvEventosHistorico.Columns.Add("INICIANDO MONITOR CONECTOR - DATA " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now), 800, HorizontalAlignment.Left);
                Import = new cupomImport();
                conector_find_store();
                lblEventosVersaoBancoServer.Text = alwaysVariables.VersaoBanco;
                lblEventosVersaoSistema.Text = alwaysVariables.VersaoSystem;
                lblEventosServidor.Text = alwaysVariables.LocalHost;
                cmbEventosLoja.Text = alwaysVariables.RazaoStore;
                WindowState = FormWindowState.Minimized;
                conector_start_thread();
                if (sockets.IsBusy != true)
                {
                    // Start the asynchronous operation.
                    sockets.RunWorkerAsync();
                }
            }

            private void btnThreadStart_Click(object sender, EventArgs e)
            {
                conector_start_thread();
                
            }

            private void btnThreadWait_Click(object sender, EventArgs e)
            {
                Thread.Sleep(60000);
            }

            private void btnThreadStop_Click(object sender, EventArgs e)
            {
                btnEventosStart.Enabled = true;
                RequestStop(true);
                
                try
                {
                    ltvEventosHistorico.Items.Add("STOP MONITOR CONECTOR - DATA " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now), 3);
                    Thread.Sleep(10000);
                    servico.Join();
                    servico.Abort();
                }
                catch (Exception)
                {
                    
                }
            }

            private void Monitor_KeyDown(object sender, KeyEventArgs e)
            {
                if (e.KeyCode == Keys.F10)
                {
                    WindowState = FormWindowState.Minimized;
                }
                else if (e.KeyCode == Keys.F11)
                {
                    RequestStop(true);
                    try
                    {
                        ltvEventosHistorico.Items.Add("STOP MONITOR CONECTOR - DATA " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now), 3);
                        servico.Abort();
                    }
                    catch (Exception)
                    {

                    }
                    //this.Dispose();
                    Environment.Exit(1);

                } else if (e.KeyCode == Keys.F2)
                {
                    conector_start_thread();
                }
                else if (e.KeyCode == Keys.F3) { Thread.Sleep(60000); }
                else if (e.KeyCode == Keys.F5)
                {
                    btnEventosStart.Enabled = true;
                    RequestStop(true);

                    try
                    {
                        ltvEventosHistorico.Items.Add("STOP MONITOR CONECTOR - DATA " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now), 3);
                        Thread.Sleep(10000);
                        servico.Join();
                        servico.Abort();
                    }
                    catch (Exception)
                    {

                    }
                }
            }

            private void label6_Click(object sender, EventArgs e)
            {
                
            }

            private void chkEventosTodasLojas_CheckedChanged(object sender, EventArgs e)
            {
                if (chkEventosTodasLojas.Checked == true)
                {
                    cmbEventosLoja.SelectedIndex = -1;
                    cmbEventosLoja.Enabled = false;
                    alwaysVariables.Store = getValue("loja", "store", fileSecret);
                }
                else
                {
                    cmbEventosLoja.Enabled = true;
                }
            }

            private void cmbEventosLoja_SelectedIndexChanged(object sender, EventArgs e)
            {
                if (cmbEventosLoja.Text != "")
                {
                    auxConsistencia = 0;
                    try
                    {
                        banco.abreConexao();
                        banco.iniciarTransacao();
                        banco.startTransaction("conector_find_loja");
                        banco.addParametro("tipo", "2");
                        banco.addParametro("find_loja", cmbEventosLoja.Text);
                        banco.procedimentoSet();
                        banco.commit();
                    }
                    catch (Exception erro)
                    {
                        banco.rollback();
                        MessageBox.Show(erro.Message, "Erro não identificado, entre contato como revendedor"); auxConsistencia = 1;
                    }
                    finally
                    {
                        if (auxConsistencia == 0)
                        {
                            alwaysVariables.Store = Convert.ToString(banco.retornaSet().Tables[0].Rows[0][0]);
                        }
                        else
                        {
                            alwaysVariables.Store = getValue("loja", "store", fileSecret);
                        }
                        banco.fechaConexao();
                    }
                }//end if
            }

            private void button1_Click(object sender, EventArgs e)
            {
                RequestStop(true);
                try
                {
                    ltvEventosHistorico.Items.Add("STOP MONITOR CONECTOR - DATA " + String.Format("{0:dd-MM-yyyy HH:mm:ss}", DateTime.Now), 3);
                    servico.Abort();
                }
                catch (Exception)
                {

                }
                //this.Dispose();
                Environment.Exit(0);
            }

            private void Monitor_Resize(object sender, EventArgs e)
            {
                //notifyIcon1.BalloonTipTitle = "Vai para o canto da tela";
                //notifyIcon1.BalloonTipText = "Sucesso ao minimizar o form";

                if (FormWindowState.Minimized == this.WindowState)
                {
                    notifyIcon1.Visible = true;
                    notifyIcon1.ShowBalloonTip(500);
                    this.Hide();
                }
                else if (FormWindowState.Normal == this.WindowState)
                {
                    notifyIcon1.Visible = false;
                }
            }

            private void Monitor_MouseDoubleClick(object sender, MouseEventArgs e)
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
            }

            private void btnMininizaMonitor_Click(object sender, EventArgs e)
            {
                WindowState = FormWindowState.Minimized;
            }

            private void sockets_DoWork(object sender, DoWorkEventArgs e)
            {
                BllSocketServer.IniciarServidor(@"C:\conector\Recepcao\", alwaysVariables.IP_MQ);
            }
        //############################################################END Regra de negocio Banco####################################################
    }
}
