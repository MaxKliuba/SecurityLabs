using Lab3.Model;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace Lab3.Utils
{
    class CasinoRoyale
    {
        private const string URI = "http://95.217.177.249/casino/";

        public static Account CreateAcc()
        {
            Account account = null;

            int id = 0;

            while (account == null)
            {
                string jsonString = Get(URI + "createacc?id=" + id);
                if (jsonString != null)
                {
                    account = JsonConvert.DeserializeObject<Account>(jsonString);
                }
                id++;
            }

            return account;
        }

        public static Result PlayLcg(int id, long bed, int number)
        {
            return Play("Lcg", id, bed, number);
        }

        public static Result PlayMt(int id, long bed, uint number)
        {
            return Play("Mt", id, bed, number);
        }

        public static Result PlayBetterMt(int id, long bed, uint number)
        {
            return Play("BetterMt", id, bed, number);
        }

        private static Result Play(String mode, int id, long bed, long number)
        {
            Result result = null;

            string jsonString = Get($"{URI}play{mode}?id={id}&bet={bed}&number={number}");
            if (jsonString != null)
            {
                result = JsonConvert.DeserializeObject<Result>(jsonString);
            }

            return result;
        }

        public static string Get(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            string responseString = null;

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    responseString = reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                var response = (HttpWebResponse)ex.Response;
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    string errorResponseString = reader.ReadToEnd();
                    Console.WriteLine($"[{response.StatusCode}]: - {errorResponseString} - {uri}");
                }
            }

            return responseString;
        }
    }
}
