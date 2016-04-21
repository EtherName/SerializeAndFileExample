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
    internal class WorkHelper
    {

        internal static void SerializableToJson(Person person, string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Person));
                ser.WriteObject(fs, person);
            }
        }
        internal static Person DeSerializableToJson(string path)
        {
            using (FileStream fs = File.OpenRead(path))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Person));
                return (Person)ser.ReadObject(fs);
            }
        }
        internal static FileInfo[] GetArchives(string allSharedPath)
        {
            DirectoryInfo directorySelected = new DirectoryInfo(allSharedPath);
            return directorySelected.GetFiles("*.gz");
        }
        internal static void Compress(string pathFrom, string pathInto)
        {
            using (FileStream fsr = File.OpenRead(pathFrom))
            using (FileStream file = File.Create(pathInto))
            using (GZipStream gZipStream = new GZipStream(file, CompressionMode.Compress))
            {
                int oneByte = 0;
                do
                {
                    oneByte = fsr.ReadByte();
                    gZipStream.WriteByte((byte)oneByte);
                }
                while (oneByte != -1);
            }
        }
        internal static string Decompress(FileInfo fileToDecompress)
        {
            using (FileStream originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = string.Concat(currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length), ".json");

                using (FileStream decompressedFileStream = File.Create(newFileName))
                {//41-54
                    using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                        return newFileName;
                    }
                }
            }
        }

    }
}
