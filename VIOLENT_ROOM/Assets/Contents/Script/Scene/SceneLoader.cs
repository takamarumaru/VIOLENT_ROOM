using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string path)
    {
        CatchLog.Instance.HandleLog(path+"を読み込みます");
        SceneManager.LoadScene(0);
    }
}
