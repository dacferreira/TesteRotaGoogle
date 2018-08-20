using Newtonsoft.Json.Linq;
using System;

namespace TesteRotaGoogle
{
    class Program
    {
        const string baseUri = "http://maps.googleapis.com/maps/api/directions/";
        const string apikey = "AIzaSyBx3Ad5i1QbW56s5c1n_mN_5lc6BIBdpyg";
        static void Main(string[] args)
        {
            var distancia = ObterDistancia("Blumenau", "Florianópolis");

            Console.Write(distancia / 1000.0f);
            Console.Write(" distância em km");

            Console.ReadKey();
        }

        public static int ObterDistancia(string origem, string destino)
        {
            int distancia = 0;
            var url = $"{baseUri}json?origin={origem}&destination={destino}&sensor=false&key{apikey}";

            string resultado = ObterDadosApi(url);
            JObject objeto = JObject.Parse(resultado);
            try
            {
                distancia = (int)objeto.SelectToken("routes[0].legs[0].distance.value");
                return distancia;
            }
            catch
            {
                return distancia;
            }
        }

        protected static string ObterDadosApi(string url)
        {
            string resultado = string.Empty;
            try
            {
                if (url.ToLower().IndexOf("http:") > -1)
                {
                    System.Net.WebClient webClient = new System.Net.WebClient();
                    byte[] resposta = webClient.DownloadData(url);
                    resultado = System.Text.Encoding.ASCII.GetString(resposta);
                }
                else
                {
                    System.IO.StreamReader streamReader = new System.IO.StreamReader(url);
                    resultado = streamReader.ReadToEnd();
                    streamReader.Close();
                }
            }
            catch { resultado = "falha ao acessar o servidor."; }
            return resultado;
        }
    }
}
