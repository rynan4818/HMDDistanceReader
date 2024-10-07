using System;
using HMDDistanceReader.Databases;
using Newtonsoft.Json;
using static HMDDistanceReader.Databases.HMDDistance;

namespace HMDDistanceReader
{
    internal class Program
    {
        public class ErrorJson
        {
            public string Error { get; set; }
        }
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: HMDDistanceReader <path to database>");
                return;
            }
            using (var db = new LiteDbReader(args[0]))
            {
                if (!db.IsConnected)
                {
                    var errorData = new ErrorJson { Error = db.ErrorMessage };
                    Console.WriteLine(JsonConvert.SerializeObject(errorData, Formatting.Indented));
                    return;
                }
                var hmdDistance = new HMDDistance(db);
                Console.WriteLine(hmdDistance.HMDDistanceJsonGet());
            }
        }
    }
}
