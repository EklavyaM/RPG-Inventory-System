using UnityEngine;

public static class ItemSaveIO
{
    private static readonly string baseSavePath;

    static ItemSaveIO()
    {
        baseSavePath = Application.persistentDataPath;
    }

    /// <summary>
    /// Saves Items to a file to the application path.
    /// </summary>
    /// <param name="items"></param>
    /// <param name="fileName"></param>
    public static void SaveItems(ItemContainerSaveData items, string fileName)
    {
        FileReadWrite.WriteToBinaryFile(baseSavePath + "/" + fileName + ".dat", items);
    }

    /// <summary>
    /// Loads Items from a file at the given path.
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static ItemContainerSaveData LoadItems(string fileName)
    {
        string filePath = baseSavePath + "/" + fileName + ".dat";
        if (System.IO.File.Exists(filePath))
        {
            return FileReadWrite.ReadFromBinaryFile<ItemContainerSaveData>(filePath);
        }
        return null;
    }
}
