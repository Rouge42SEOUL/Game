using System.IO;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;
public class JsonSaveLoder : MonoBehaviour
{

    public void Save(InfoToJson infoToJson)
    {
        FileStream stream = new FileStream(Application.dataPath + "/test.json", FileMode.OpenOrCreate);
        string jsonData = JsonConvert.SerializeObject(infoToJson);
        byte[] data = Encoding.UTF8.GetBytes(jsonData);
        stream.Write(data, 0, data.Length);
        stream.Close();
    }

    public bool Load(out InfoToJson infoToJson)
    {
        string filePath = Application.dataPath + "/test.json";

        if (File.Exists(filePath))
        {
            Debug.LogError("이전 데이터를 불러오는중..");
            FileStream stream = new FileStream(filePath, FileMode.Open);
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, (int)stream.Length);
            string jsonData = Encoding.UTF8.GetString(data);
            infoToJson = JsonConvert.DeserializeObject<InfoToJson>(jsonData);
            return false;
        }
        else
        {
            Debug.LogError("새로운 게임 생성중..");
            FileStream stream = new FileStream(filePath, FileMode.CreateNew);
            // 파일 작업 수행...
            stream.Close();
            infoToJson = new InfoToJson();
            return true;
        }
    }
}
