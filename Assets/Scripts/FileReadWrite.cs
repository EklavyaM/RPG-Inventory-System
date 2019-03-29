using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;

public static class FileReadWrite
{
    public static void WriteToBinaryFile<T>(string filePath, T data)
    {
        using (Stream stream = File.Open(filePath, FileMode.Create))
        {
            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(stream, data);
        }
    }

    public static T ReadFromBinaryFile<T>(string filePath)
    {
        using (Stream stream = File.Open(filePath, FileMode.Open))
        {
            var binaryFormatter = new BinaryFormatter();
            return (T)binaryFormatter.Deserialize(stream);
        }
    }
}
