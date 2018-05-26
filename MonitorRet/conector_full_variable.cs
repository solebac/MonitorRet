using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Implementações lib's
using System.IO;
using System.Net;
using System.Security.Cryptography;

namespace MonitorRet
{
    class conector_full_variable
    {
        public conector_full_variable()
        {
            IPHostEntry ipEntry = Dns.GetHostByName(nome);
            IPAddress[] ip = ipEntry.AddressList;
            
            try
            {
                _IP = ip[1].ToString();
            }
            catch (Exception)
            {
                _IP = ip[0].ToString();
            }

        }
        //###############################################################Declaracao Variaveis e Metodos Globais#################################################
        // Declare CspParmeters and RsaCryptoServiceProvider
        // objects with global scope of your Form class.
        private CspParameters cspp = new CspParameters();
        private RSACryptoServiceProvider rsa;
        private dados banco;
        private int auxConsistencia = 0;
        private static string _keyName = "F1AV10 R063R10 D@ 51LV4 - C0N3CT0R";
        private string nome = Dns.GetHostName();
        public string keyName
        {
            get { return _keyName; }
        }
        private static string _PubKeyFile = @"c:\\temp";
        public string PubKeyFile
        {
            get { return _PubKeyFile; }
            set { _PubKeyFile = value; }
        }

        private static string _RazaoHomoloNfe = "NF-E EMITIDA EM AMBIENTE DE HOMOLOGACAO - SEM VALOR FISCAL";
        public string RazaoHomoloNfe
        {
            get { return _RazaoHomoloNfe; }
            set { _RazaoHomoloNfe = value; }
        }
        private static string _log;
        public string caminhoLog
        {
            get { return _log; }
            set { _log = value; }
        }
        private static string _tipoAmbiente = "n";
        public string TipoAmbiente
        {
            get { return _tipoAmbiente; }
            set { _tipoAmbiente = value; }
        }
        private static string _IP;
        public string IP_MQ
        {
            get { return _IP; }
            set { _IP = value; }
        }
        private static string _PubCert;
        public string PubCert
        {
            get { return _PubCert; }
            set { _PubCert = value; }
        }


        private static string _copyLocal = "0";
        public string CopyLocal
        {
            get { return _copyLocal; }
            set { _copyLocal = value; }
        }

        private static string _flagHomologacao;
        public string Homologacao
        {
            get { return _flagHomologacao; }
            set { _flagHomologacao = value; }
        }

        private static string _ramoAtuacao;
        public string RamoAtuacao
        {
            get { return _ramoAtuacao; }
            set { _ramoAtuacao = value; }
        }

        private static string _liberacaocaoCredito;
        public string LiberacaocaoCredito
        {
            get { return _liberacaocaoCredito; }
            set { _liberacaocaoCredito = value; }
        }

        private static string _pis = "";
        public string alquitoPis
        {
            get { return _pis; }
            set { _pis = value; }
        }

        private static string _Cofins = "";
        public string alquitoCofins
        {
            get { return _Cofins; }
            set { _Cofins = value; }
        }

        private static string _limiteRendaBase = "0";
        public string LimiteRendaBase
        {
            get { return _limiteRendaBase; }
            set { _limiteRendaBase = value; }
        }

        private static string _categoriaLimite;
        public string CategoriaLimite
        {
            get { return _categoriaLimite; }
            set { _categoriaLimite = value; }
        }

        private static string _variacaoLimite;
        public string VariacaoLimite
        {
            get { return _variacaoLimite; }
            set { _variacaoLimite = value; }
        }

        private static string _optionFatorCheque = "0";
        public string FatorCheque
        {
            get { return _optionFatorCheque; }
            set { _optionFatorCheque = value; }
        }
        private static string _optionAutorizaChequeJuridica = "n";
        public string AutorizaChequeJuridica
        {
            get { return _optionAutorizaChequeJuridica; }
            set { _optionAutorizaChequeJuridica = value; }
        }

        private static string _Periodo_Liberacao = "";
        public string Periodo_Liberacao
        {
            get { return _Periodo_Liberacao; }
            set { _Periodo_Liberacao = value; }
        }

        private static string _Instituicao = "";
        public string Instituicao
        {
            get { return _Instituicao; }
            set { _Instituicao = value; }
        }

        private static string _caminhoFiliacao1 = "";
        public string CaminhoFilial1
        {
            get { return _caminhoFiliacao1; }
            set { _caminhoFiliacao1 = value; }
        }

        private static string _caminhoFiliacao2 = "";
        public string CaminhoFilial2
        {
            get { return _caminhoFiliacao2; }
            set { _caminhoFiliacao2 = value; }
        }

        private static string _caminhoFiliacao3 = "";
        public string CaminhoFilial3
        {
            get { return _caminhoFiliacao3; }
            set { _caminhoFiliacao3 = value; }
        }

        private static string _PortaSocketFiliacao2 = "";
        public string PortaSocketFiliacao2
        {
            get { return _PortaSocketFiliacao2; }
            set { _PortaSocketFiliacao2 = value; }
        }

        private static string _filiacaoSPCSerasa = "";
        public string filiacaoSPCSerasa
        {
            get { return _filiacaoSPCSerasa; }
            set { _filiacaoSPCSerasa = value; }
        }

        private static string _autorizacaoMd5 = "";
        public string MD5VALIDO
        {
            get { return _autorizacaoMd5; }
            set { _autorizacaoMd5 = value; }
        }

        private static string _perfil = "";
        public string Perfil
        {
            get { return _perfil; }
            set { _perfil = value; }
        }
        private static string _schema = "";
        public string Schema
        {
            get { return _schema; }
            set { _schema = value; }
        }

        private static string _userName = "";
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }

        private static string _Atualizador = "";
        public string Atualizador
        {
            get { return _Atualizador; }
            set { _Atualizador = value; }
        }

        private static string _palavraChave = "";
        public string PalavraChave
        {
            get { return _palavraChave; }
            set { _palavraChave = value; }
        }

        private static string _stringConector = "";
        public string StringConector
        {
            get { return _stringConector; }
            set { _stringConector = value; }
        }

        private static string _compatilhamento = "";
        public string EscritaCompatilhada
        {
            get { return _compatilhamento; }
            set { _compatilhamento = value; }
        }

        private static string _compatilhamentoImport = "";
        public string EscritaCompatilhadaImport
        {
            get { return _compatilhamentoImport; }
            set { _compatilhamentoImport = value; }
        }

        private static string _transmissaoWindows = "";
        public string TransmissaoWindows
        {
            get { return _transmissaoWindows; }
            set { _transmissaoWindows = value; }
        }

        private static string _transmissaoLinux = "";
        public string TransmissaoLinux
        {
            get { return _transmissaoLinux; }
            set { _transmissaoLinux = value; }
        }

        private static string _recepcaoLinux = "";
        public string RecepcaoLinux
        {
            get { return _recepcaoLinux; }
            set { _recepcaoLinux = value; }
        }

        private static string _recepcaoWindows = "";
        public string RecepcaoWindows
        {
            get { return _recepcaoWindows; }
            set { _recepcaoWindows = value; }
        }

        private static string _typeComunicacao = "";
        public string TypeComunicacao
        {
            get { return _typeComunicacao; }
            set { _typeComunicacao = value; }
        }

        private static string _senha = "";
        public string Senha
        {
            get { return _senha; }
            set { _senha = value; }
        }

        private static string _localHost = "";
        public string LocalHost
        {
            get { return _localHost; }
            set { _localHost = value; }
        }

        private static string _port = "";
        public string Port
        {
            get { return _port; }
            set { _port = value; }
        }
        private static string _terminal = "";
        public string Terminal
        {
            get { return _terminal; }
            set { _terminal = value; }
        }

        private static string _store = "";
        public string Store
        {
            get { return _store; }
            set { _store = value; }
        }

        private static int _ImportCarga = 0;
        public int ImportCarga
        {
            get { return _ImportCarga; }
            set { _ImportCarga = value; }
        }

        private static string _razaoStore = "";
        public string RazaoStore
        {
            get { return _razaoStore; }
            set { _razaoStore = value; }
        }
        private static string _user = "";
        public string Usuario
        {
            get { return _user; }
            set { _user = value; }
        }
        private static string _login = "";
        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }
        private static string _cnpj = "";
        public string CNPJ
        {
            get { return _cnpj; }
            set { _cnpj = value; }
        }
        private static string _supervisor = "0";
        public string Supervisor
        {
            get { return _supervisor; }
            set { _supervisor = value; }
        }
        private static Int32 _prazoQuitacaoAfter = 30;
        public Int32 dayQuitacaoAfter
        {
            get { return _prazoQuitacaoAfter; }
            set { _prazoQuitacaoAfter = value; }
        }

        private static Int32 _prazoQuitacaoBefore = 365;
        public Int32 dayQuitacaoBefore
        {
            get { return _prazoQuitacaoBefore; }
            set { _prazoQuitacaoBefore = value; }
        }

        private static decimal _indiceSingleday = Convert.ToDecimal("0.02");
        public decimal IndiceSingleday
        {
            get { return _indiceSingleday; }
            set { _indiceSingleday = value; }
        }
        private static decimal _indiceAtrasoMora = 2;
        public decimal IndiceAtrasoMora
        {
            get { return _indiceAtrasoMora; }
            set { _indiceAtrasoMora = value; }
        }
        private static string _defaultBanco;
        public string BancoDefault
        {
            get { return _defaultBanco; }
            set { _defaultBanco = value; }
        }

            private static string _versaoSystem;
            public string VersaoSystem
            {
                get { return _versaoNfce; }
                set { _versaoNfce = value; }
            }

            private static string _versaoBanco;
            public string VersaoBanco
            {
                get { return _versaoBanco; }
                set { _versaoBanco = value; }
            }

        private static string _typeComissao;
        public string TypeComissao
        {
            get { return _typeComissao; }
            set { _typeComissao = value; }
        }
        private static Int32 _carenciaSingleDay = 0;
        public Int32 CarenciaSingleDay
        {
            get { return _carenciaSingleDay; }
            set { _carenciaSingleDay = value; }
        }
        private static Int32 _carenciaSingleMora = 0;
        public Int32 CarenciaSingleMora
        {
            get { return _carenciaSingleMora; }
            set { _carenciaSingleMora = value; }
        }
        private static string _idadeSpc = "18";
        public string IdadeSpc
        {
            get { return _idadeSpc; }
            set { _idadeSpc = value; }
        }
        private static string _altValuePrestacao = "1";
        public string AltValuePrestacao
        {
            get { return _altValuePrestacao; }
            set { _altValuePrestacao = value; }
        }
        private static string _altValueEntrada = "1";
        public string AltValueEntrada
        {
            get { return _altValueEntrada; }
            set { _altValueEntrada = value; }
        }
        private static string _logicaCredito = "0";
        public string LogicaCredito
        {
            get { return _logicaCredito; }
            set { _logicaCredito = value; }
        }
        private static decimal _descontoMax = 10;
        public decimal DescontoMaximoPrestacao
        {
            get { return _descontoMax; }
            set { _descontoMax = value; }
        }

        private static string _ECF_UTIL = "";
        public string ECF_UTIL
        {
            get { return _ECF_UTIL; }
            set { _ECF_UTIL = value; }
        }

        private static string _chaveNfce = "";
        public string chaveNfce
        {
            get { return _chaveNfce; }
            set { _chaveNfce = value; }
        }

        private static string _protocoloNfce = "";
        public string ProtocoloNfce
        {
            get { return _protocoloNfce; }
            set { _protocoloNfce = value; }
        }

        private static string _motivoNfce = "";
        public string MotivoNfce
        {
            get { return _motivoNfce; }
            set { _motivoNfce = value; }
        }

        private static string _versaoNfce = "";
        public string versaoNfce
        {
            get { return _versaoNfce; }
            set { _versaoNfce = value; }
        }

        private static string _dataAutorizaNfce = "";
        public string dataAutorizaNfce
        {
            get { return _dataAutorizaNfce; }
            set { _dataAutorizaNfce = value; }
        }

        private static string _dataHoraRecbNfe = "";
        public string DataHoraRecbNfe
        {
            get { return _dataHoraRecbNfe; }
            set { _dataHoraRecbNfe = value; }
        }
        public Boolean descryptFileLiberacao(string path) //Flavio
        {
            Boolean valida = false;
            cspp.KeyContainerName = keyName;
            rsa = new RSACryptoServiceProvider(cspp);
            rsa.PersistKeyInCsp = true;
            // Create instance of Rijndael for
            // symetric decryption of the data.
            RijndaelManaged rjndl = new RijndaelManaged();
            rjndl.KeySize = 256;
            rjndl.BlockSize = 256;
            rjndl.Mode = CipherMode.CBC; //Obtém ou define o modo de operação do algoritmo simétrico. 

            // Create byte arrays to get the length of
            // the encrypted key and IV.
            // These values were stored as 4 bytes each
            // at the beginning of the encrypted package.
            byte[] LenK = new byte[4];
            byte[] LenIV = new byte[4];

            // Consruct the file name for the decrypted file.
            string outFile = path.Substring(0, path.LastIndexOf(".")) + ".txt";

            // Use FileStream objects to read the encrypted
            // file (inFs) and save the decrypted file (outFs).
            using (FileStream inFs = new FileStream(path.Substring(0, path.LastIndexOf(".")) + ".enc", FileMode.Open))
            {

                inFs.Seek(0, SeekOrigin.Begin);
                inFs.Seek(0, SeekOrigin.Begin);
                inFs.Read(LenK, 0, 3);
                inFs.Seek(4, SeekOrigin.Begin);
                inFs.Read(LenIV, 0, 3);

                // Convert the lengths to integer values.
                int lenK = BitConverter.ToInt32(LenK, 0);
                int lenIV = BitConverter.ToInt32(LenIV, 0);

                // Determine the start postition of
                // the ciphter text (startC)
                // and its length(lenC).
                int startC = lenK + lenIV + 8;
                int lenC = (int)inFs.Length - startC;

                // Create the byte arrays for
                // the encrypted Rijndael key,
                // the IV, and the cipher text.
                byte[] KeyEncrypted = new byte[lenK];
                byte[] IV = new byte[lenIV];

                // Extract the key and IV
                // starting from index 8
                // after the length values.
                inFs.Seek(8, SeekOrigin.Begin);
                inFs.Read(KeyEncrypted, 0, lenK);
                inFs.Seek(8 + lenK, SeekOrigin.Begin);
                inFs.Read(IV, 0, lenIV);
                //Directory.CreateDirectory(path.Substring(0, path.LastIndexOf(".")));
                // Use RSACryptoServiceProvider
                // to decrypt the Rijndael key.
                byte[] KeyDecrypted = rsa.Decrypt(KeyEncrypted, false);

                // Decrypt the key.
                ICryptoTransform transform = rjndl.CreateDecryptor(KeyDecrypted, IV);

                // Decrypt the cipher text from
                // from the FileSteam of the encrypted
                // file (inFs) into the FileStream
                // for the decrypted file (outFs).
                using (FileStream outFs = new FileStream(outFile, FileMode.Create))
                {

                    int count = 0;
                    int offset = 0;

                    // blockSizeBytes can be any arbitrary size.
                    int blockSizeBytes = rjndl.BlockSize / 8;
                    byte[] data = new byte[blockSizeBytes];


                    // By decrypting a chunk a time,
                    // you can save memory and
                    // accommodate large files.

                    // Start at the beginning
                    // of the cipher text.
                    inFs.Seek(startC, SeekOrigin.Begin);
                    using (CryptoStream outStreamDecrypted = new CryptoStream(outFs, transform, CryptoStreamMode.Write))
                    {
                        do
                        {
                            count = inFs.Read(data, 0, blockSizeBytes);
                            offset += count;
                            outStreamDecrypted.Write(data, 0, count);
                        }
                        while (count > 0);

                        outStreamDecrypted.FlushFinalBlock();
                        outStreamDecrypted.Close();
                    }
                    outFs.Close();
                }
                //Open the stream and read it back.
                using (FileStream fs = File.OpenRead(outFile))
                {
                    string tem = "";
                    string aux1, aux2, aux3, aux5, aux6, aux7, aux8, retorno;
                    int teste = 0;
                    banco = new dados();
                    byte[] b = new byte[1024];
                    UTF8Encoding temp = new UTF8Encoding(true);
                    while (fs.Read(b, 0, b.Length) > 0)
                    {
                        tem += temp.GetString(b);
                        aux1 = tem.Substring(0, tem.IndexOf("|"));
                        if (aux1.Length == 14)
                        {
                            aux2 = tem.Substring(tem.IndexOf("|") + 1, 1);
                            aux3 = tem.Substring(tem.IndexOf("|") + 3, 8);
                            _Periodo_Liberacao = tem.Substring(tem.IndexOf("|") + 12, 8);
                            aux5 = tem.Substring(tem.IndexOf("|") + 21, 8);
                            aux6 = tem.Substring(tem.IndexOf("|") + 30, 1);
                            aux7 = tem.Substring(tem.IndexOf("|") + 32, 1);
                            aux8 = tem.Substring(tem.IndexOf("|") + 34, 34);
                            teste = count_passwd(aux1);
                            if (aux1 == CNPJ)
                            {
                                if (teste > 0)
                                {
                                    retorno = verifica_passwd(keyName, aux1);
                                    if (retorno != null)
                                    {
                                        if (Convert.ToDouble(retorno) < Convert.ToDouble(_Periodo_Liberacao))
                                        {
                                            conector_passwd(aux1, aux3, _Periodo_Liberacao, aux2, _store, aux5, keyName);
                                            if (auxConsistencia == 0)
                                            {
                                                valida = true;
                                            }
                                        }
                                    }
                                }
                                else if ((teste = count_store(aux1)) == 1 && aux5 != "" && aux1.Length == 14)//Existe Loja ligada a senha e senha
                                {
                                    conector_passwd(aux1, aux3, _Periodo_Liberacao, aux2, _store, aux5, keyName);
                                    if (auxConsistencia == 0)
                                    {
                                        valida = true;
                                    }
                                }
                            }
                            else
                            {
                                valida = false;
                            }
                        }
                        else
                        {
                            valida = false;
                        }

                    }
                }
                if (File.Exists(outFile))
                {
                    File.Delete(outFile);
                }
                inFs.Close();

                return valida;
            }

        }
        //###############################################################End Variaveis e Metodos Globais########################################################
        //###############################################################Procedimento de banco de dados ########################################################
        public int count_passwd(string cnpj)
        {
            int retorno = 0;
            try
            {
                banco.abreConexao();
                banco.iniciarTransacao();
                banco.singleTransaction("select count(*) FROM `conector`.`licenca_adm` where cnpj=?str");
                banco.addParametro("?str", cnpj);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    retorno = Convert.ToInt32(banco.retornaRead().GetString(0));
                }
                else retorno = 0;
                banco.fechaRead();
                banco.commit();
            }
            catch (Exception erro)
            {
                banco.rollback();

            }
            finally
            {
                banco.fechaConexao();
            }
            return retorno;
        }
        
        public int count_store(string cnpj)
        {
            int retorno = 0;
            try
            {
                banco.abreConexao();
                banco.iniciarTransacao();
                banco.singleTransaction("select count(cnpj) from loja where cnpj=?str");
                banco.addParametro("?str", cnpj);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    retorno = Convert.ToInt32(banco.retornaRead().GetString(0));
                }
                else retorno = 0;
                banco.fechaRead();
                banco.commit();
            }
            catch (Exception erro)
            {
                banco.rollback();

            }
            finally
            {
                banco.fechaConexao();
            }
            return retorno;
        }

        public string verifica_passwd(string hash, string cnpj)
        {
            banco = new dados();
            string retorno = null;
            try
            {
                banco.abreConexao();
                banco.iniciarTransacao();
                banco.singleTransaction("select AES_DECRYPT(`licenca_adm`.`password`,?passwd) from `conector`.`licenca_adm` where cnpj=?str");
                banco.addParametro("?passwd", hash);
                banco.addParametro("?str", cnpj);
                banco.procedimentoRead();
                if (banco.retornaRead().Read() == true)
                {
                    retorno = Convert.ToString(banco.retornaRead().GetString(0));
                }
                else retorno = null;
                banco.fechaRead();
                banco.commit();
            }
            catch (Exception erro)
            {
                banco.rollback();
                retorno = null;
            }
            finally
            {
                banco.fechaConexao();

            }
            return retorno;
        }
        public void conector_passwd(string VarAux1, string VarAux2, string VarAux3, string VarAux5, string VarAux6, string passwd, string newpasswd)
        {
            int temp = 0;
            string select = "";
            select = "replace INTO `conector`.`licenca_adm`";
            select += "     ( ";
            select += "        `cnpj`, ";
            select += "        `aquisicao`, ";
            select += "        `periodoUso`, ";
            select += "        `password`, ";
            select += "        `statusPgto`, ";
            select += "        `idloja` ";
            select += "    ) ";
            select += "    VALUES ";
            select += "( ";
            select += "        ?VarAux1, ";
            select += "        ?VarAux2, ";
            select += "        ?VarAux3, ";
            select += "        AES_ENCRYPT(?Passwd,?newPasswd), ";
            select += "        ?VarAux5, ";
            select += "        ?VarAux6 ";
            select += ")";
            try
            {
                banco.abreConexao();
                banco.iniciarTransacao();
                banco.singleTransaction(select);
                banco.addParametro("?VarAux1", VarAux1);
                banco.addParametro("?VarAux2", VarAux2);
                banco.addParametro("?VarAux3", VarAux3);
                banco.addParametro("?passwd", passwd);
                banco.addParametro("?newpasswd", newpasswd);
                banco.addParametro("?VarAux5", VarAux5);
                banco.addParametro("?VarAux6", VarAux6);
                banco.procedimentoText();
                banco.commit();
            }
            catch (Exception erro)
            {
                banco.rollback();
                temp = 1;
            }
            finally
            {
                banco.fechaConexao();
                if (temp == 0)
                {

                }
            }
        }
        //###########################################################End Procedimento de banco de dados ########################################################
    }
}
