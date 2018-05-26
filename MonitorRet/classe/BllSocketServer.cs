using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
//using DTO;
//using DAL;

public static class BllSocketServer
{
    /// <summary>
    /// Servidor socket que irá recepcionar arquivos.
    /// </summary>
    /// <param name="caminhoRecepcaoArquivos"></param>
    /// <param name="enderecoIP"></param>
    /// <returns></returns>
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
        catch (SocketException erro)
        {
            throw;
        }
    }

    /*public static async Task<IEnumerable<DtoSistema>> GetDataFromBase(DtoSistema dados)
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
}
