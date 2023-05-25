using System.IO;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;

public class JsonConverter : MonoBehaviour
{

    public static void Save<T>(T objectToSave, string fileName)
    {
        using FileStream stream = new FileStream(fileName, FileMode.Create);
        string jsonData = JsonConvert.SerializeObject(objectToSave);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        stream.Write(data, 0, data.Length);
    }

    public static bool Load<T>(out T objectToLoad, string fileName) where T : new() // 인스턴스가 없을떄만 사용가능
    {
        if (File.Exists(fileName)) // 파일이 있으면 읽고 objectToLoad 객체에 담는다.
        {
            using FileStream stream = new FileStream(fileName, FileMode.Open);
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, (int)stream.Length);
            string jsonData = Encoding.UTF8.GetString(data);
            objectToLoad = JsonConvert.DeserializeObject<T>(jsonData);
            stream.Close();
            return true;
        }
        else
        {
            objectToLoad = new T();
            return false;
        }
    }

    public static void DeleteJson(string fileName)
    { 
        if (File.Exists(fileName))
            File.Delete(fileName);
    }
}
