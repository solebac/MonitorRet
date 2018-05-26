using System;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.IO;

public static class BllSocketClient
{
    /// <summary>
    /// Cliente socket que irá enviar arquivo.
    /// </summary>
    /// <param name="nomeArquivo"></param>
    /// <param name="enderecoIp"></param>
    /// <param name="tamanhoArquivoMb"></param>
    /// <returns></returns>
    public static bool EnviarArquivo(string nomeArquivo, string enderecoIp, byte tamanhoArquivoMb)
    {
        try
        {
            IPEndPoint enderecoCliente = new IPEndPoint(IPAddress.Parse(enderecoIp), 5656);
            using (Socket socketCliente = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP))
            {
                string[] fileTransmissao = Directory.GetFiles(@"C:\conector\Transmissao\");
                string caminhoArquivo = string.Empty;
                string destroi = nomeArquivo;
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

                if ((fileTransmissao.GetLength(0) > 1))
                {
                    System.Threading.Thread.Sleep(10000);
                }
                if (File.Exists(destroi))
                {
                    File.Delete(destroi);
                }
                return true;
            }
        }
        catch (System.IO.IOException)
        {
            throw;
            return false;
        }
        catch (Exception err)
        {
            throw;
            return false;
        }
    }
}
