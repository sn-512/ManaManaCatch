using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    enum ePlayerType
    {
        Player1,
        Player2,
        Player3,
        Player4,
    }

    private void Awake()
    {
        SceneManager.LoadScene("Field", LoadSceneMode.Additive);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameStart();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.anyKeyDown)
        {
            ePlayerType playerType = GetPlayerType(Input.inputString);
        }

    }
    //入力されたキーに応じてプレイヤーのタイプを返す処理(何かで使うかも)
    private ePlayerType GetPlayerType(string key) => key switch
    {
        "1" => ePlayerType.Player1,
        "2" => ePlayerType.Player2,
        "3" => ePlayerType.Player3,
        "4" => ePlayerType.Player4,
        _ => throw new System.InvalidOperationException()
    };
    private void Hoge(ePlayerType type)
    {

    }

    public void GameStart()
    {
        StartCoroutine(Co_GameStart());
    }

    private IEnumerator Co_GameStart()
    {
        yield return new WaitForSeconds(1f);

        SoundManager.instance.PlayBGM();

        while (!FishManager.instance.isEnd)
        {
            yield return null;
        }

        yield return new WaitForSeconds(3f);

        GameEnd();
    }

    public void GameEnd()
    {
        GameData.scoreMax = FishManager.instance.scoreMax;
        for (int i = 0; i < 4; i++)
        {
            GameData.scoreList[i] = LaneManager.instance.GetLaneScore(i);
        }
        SceneManager.LoadScene("clear");
    }
}
