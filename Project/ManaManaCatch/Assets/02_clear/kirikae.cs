using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // �ǉ�

public class kirikae : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(LoadSceneAfterDelay());
    }

    IEnumerator LoadSceneAfterDelay()
    {
        yield return new WaitForSeconds(12f); // 12�b�ҋ@

        // "rodo"�V�[�������[�h
        SceneManager.LoadScene("rodo");
    }
}