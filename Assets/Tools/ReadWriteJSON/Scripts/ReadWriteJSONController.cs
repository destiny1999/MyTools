using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReadWriteJSONController : MonoBehaviour
{
    [SerializeField] string jsonFileName;
    TestJsonClass jsonClass;
    // Start is called before the first frame update
    void Start()
    {

        jsonClass = ReadJsonData();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            WriteJsonData(jsonClass);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            jsonClass = ReadJsonData();
            print(jsonClass.test1);
        }
    }
    public TestJsonClass ReadJsonData()
    {
        string path = $"Assets/Resources/{jsonFileName}.json";
        string content = File.ReadAllText(path);
        TestJsonClass jsonData = JsonUtility.FromJson<TestJsonClass>(content);
        return jsonData;
    }
    public void WriteJsonData(TestJsonClass jsonClass)
    {
        jsonClass.test1 = "test write";
        string json = JsonUtility.ToJson(jsonClass);

        string path = $"Assets/Resources/{jsonFileName}.json";

        StreamWriter writer = new StreamWriter(path);
        writer.WriteLine(json);
        writer.Close();
        Debug.Log("Write ok");
    }
}
public class TestJsonClass
{
    public string test1;
    public string test2;
    public int[] test3;
}
