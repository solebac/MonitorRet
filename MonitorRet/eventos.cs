using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace MonitorRet
{
    class eventos
    {
        public eventos()
        {
            RequestStop(false);
        }

        #region //Variaveis

            private volatile bool _flagStop;
            private string[] regra = new string[14] { "nf", "nfce", "nfItem", "nfImposto", "cupom_cabecalho", "cupom_detalhes", "cupom_movimento", "movimentoDia", "detalhe_reducao", "detalhe_reducao_aliquota", "fechamentoCaixa", "cartao", "cheque", "convenioMovimento" };
            private string[] path = new string[14];
            private cupomImport Import = new cupomImport();
            string[,] vetorCx;
            string[] vetorStore;

        #endregion

        #region //Funções e Metodos
        public void RequestStop(bool para)
        {
            _flagStop = para;
        }

        public void Varrefolder()
        {
            Import = new cupomImport();

            while (!_flagStop)
            {
                string[] fileRecepcao = Directory.GetFiles(@"c:\conector\recepcao\");
                string[] fileTransmissao = Directory.GetFiles(@"C:\conector\Transmissao\");
                string data = "";
                string pdv = "";
                string store = "";
                string msgErro = "";
                for (int p = 0; p < Import._Store.Length; p++)
                {
                    if (fileRecepcao.GetLength(0) > 0)
                    {
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
                                    //Import.carregaSql(path[j], j);
                                    //Import.executaSqlBat(Import._Store[p]);
                                }
                            }
                        }
                    }
                    
                    if (fileTransmissao.GetLength(0) > 0)
                    {
                        for (int i = 0; i < Import._Store.Length; i++)
                        {
                            if (fileTransmissao.GetLength(0) > 0)
                            {
                                for (int y = 0; y < fileTransmissao.GetLength(0); y++)
                                {
                                    if (fileTransmissao[y].ToString().IndexOf("TRANSMISSAO") != -1)
                                    {
                                        pdv = fileTransmissao[y].Substring(fileTransmissao[y].ToString().IndexOf("TRANSMISSAO") - 8, 8);
                                        store = fileTransmissao[y].Substring(fileTransmissao[y].ToString().IndexOf("TRANSMISSAO") - 16, 8);
                                        data = fileTransmissao[y].Substring(fileTransmissao[y].ToString().IndexOf("TRANSMISSAO") - 24, 8);
                                        vetorStore = Import.vetorStore;
                                        vetorCx = Import.conector_find_pdv(store);
                                        
                                        if (store == "0")
                                        {
                                            for (int x = 0; x < vetorStore.Length; x++)
                                            {
                                                Import.conector_find_pdv(vetorStore[x]);
                                                for (int v = 0; v < vetorCx.GetLength(0); v++)
                                                {
                                                    if (vetorCx[v, 0] != null)
                                                    {
                                                        Import.transmissao(fileRecepcao[y], vetorCx[v, 0], vetorCx[v, 1], ref msgErro, true);//Alter recepcao
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            for (int b = 0; b < vetorCx.GetLength(0); b++)
                                            {
                                                if (vetorCx[b, 0] != null)
                                                {
                                                    Import.transmissao(fileRecepcao[y], vetorCx[b, 0], vetorCx[b, 1], ref msgErro, true); //Alter recepcao
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                
            }
        }

        #endregion


    }
}
