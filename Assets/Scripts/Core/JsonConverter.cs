using System.IO;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;

public class JsonConverter : MonoBehaviour
{

    public static void Save<T>(T objectToSave, string fileName)
    {
        FileStream stream = new FileStream(fileName, FileMode.OpenOrCreate);
        string jsonData = JsonConvert.SerializeObject(objectToSave);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        stream.Write(data, 0, data.Length);
        stream.Close();
    }

    public static bool Load<T>(out T objectToLoad, string fileName) where T : new() // 인스턴스가 없을떄만 사용가능
    {
        objectToLoad = new T();
        if (File.Exists(fileName)) // 파일이 있으면 읽고 objectToLoad 객체에 담는다.
        {
            FileStream stream = new FileStream(fileName, FileMode.Open);
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, (int)stream.Length);
            string jsonData = Encoding.UTF8.GetString(data);
            objectToLoad = JsonConvert.DeserializeObject<T>(jsonData);
            stream.Close();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void DeleteJson(string fileName)
    { 
        if (File.Exists(Application.dataPath + fileName))
            File.Delete(Application.dataPath + fileName);
    }
}
