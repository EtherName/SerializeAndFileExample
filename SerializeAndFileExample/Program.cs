using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace SerializeAndFileExample
{
    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            string path = "DataInJson.json";
            string sharedPath = @"S:\C-16-01\DATA\VasiliyJson.gz";
            string allSharedPath = @"S:\C-16-01\DATA\";


            Person p = new Person() { FirstName = "Petr", LastName = "Ivanov" };
            Person p2 = null;
            // serialize Person to .json
            WorkHelper.SerializableToJson(p, path);
            // compress .json to .gz
            //WorkHelper.Compress(path, sharedPath);
            // deserialize from .json to Person
            //p2 = WorkHelper.DeSerializableToJson(path);
            
            #region GetArchives
            // get .gz archive in shared directory
            FileInfo[] allCompressedFile = WorkHelper.GetArchives(@"D:\C# work\");
            foreach (FileInfo file in allCompressedFile)
            {
                Console.WriteLine(count++ + ". " + file.Name);
            }
            #endregion

            #region Decompress
            Console.WriteLine("Input number file to decompresed");
            int number = 0;
            bool parse = int.TryParse(Console.ReadLine(), out number);
            string decompressedFile = WorkHelper.Decompress(allCompressedFile[number]);
            #endregion

            // deserialize decompressedFile .json to Person
            //p2 = WorkHelper.DeSerializableToJson(decompressedFile);

            // print p2 Person
            //Console.WriteLine($"firstName is {p2.FirstName}\nLastName is {p2.LastName}");
            
            Console.ReadKey();
        }
    }
}
