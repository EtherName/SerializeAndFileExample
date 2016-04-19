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
        static string path = "DataInJson.json";
        static string sharedPath = @"S:\C-16-01\DATA\VasiliyJson.gz";
        static void Main(string[] args)
        {
            Person p = new Person() { FirstName = "Petr", LastName = "Ivanov" };
            SerializableToJson(p);
            //WriteToZipAsync();
            var p2 = DeSerializableToJson();
            Console.WriteLine(p2.FirstName + " " + p2.LastName);
            var files = GetArchives();
            foreach (var item in files)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
        static void SerializableToJson(Person person)
        {
            using (FileStream fs= new FileStream(path, FileMode.Create))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Person));
                ser.WriteObject(fs, person);
            }
        }

        static Person DeSerializableToJson()
        {
            using (FileStream fs = File.OpenRead(path))// sharedPath or path
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Person));
                return (Person)ser.ReadObject(fs);
            }
        }
        static string[] GetArchives()
        {
            return Directory.GetFiles(@"S:\C-16-01\DATA\");
        }
        static void WriteToZipAsync()
        {
            using (FileStream fsr = File.OpenRead(path))
            using (FileStream file = File.Create(sharedPath))
            using (GZipStream GZipStream = new GZipStream(file, CompressionMode.Compress))
            {
                int oneByte = 0;
                do
                {
                    oneByte = fsr.ReadByte();
                    GZipStream.WriteByte((byte)oneByte);
                }
                while (oneByte != -1);
            }
        }
    }
}
