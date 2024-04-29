using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // 追加

public class kirikae : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadSceneAfterDelay());
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(12f); // 12秒待機

        // "rodo"シーンをロード
        SceneManager.LoadScene("rodo");
    }
}