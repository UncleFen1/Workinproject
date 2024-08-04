using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class LoadAsset : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(InitList());
    }

    private IEnumerator InitList()
    {
        yield return StartCoroutine(LoadFile());
    }

    private IEnumerator LoadFile()
    {
        string pathToFile = Path.Combine(Application.streamingAssetsPath, "qwe.json");
        string temp;
        if (pathToFile.Contains("://") || pathToFile.Contains(":///"))
        {
            using (UnityWebRequest request = UnityWebRequest.Get(pathToFile))
            {
                yield return request.SendWebRequest();

                temp = request.downloadHandler.text;
            }
        }
        else
        {
            temp = System.IO.File.ReadAllText(pathToFile);
            yield return null;  // To keep the coroutine pattern consistent
        }
        Debug.Log("_j text: " + temp);
    }
}
