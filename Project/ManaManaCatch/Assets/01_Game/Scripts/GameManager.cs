using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum ePlayerType
    {
        Player1,
        Player2,
        Player3,
        Player4,
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.anyKeyDown)
        {
            ePlayerType playerType = GetPlayerType(Input.inputString);
        }

    }
    //���͂��ꂽ�L�[�ɉ����ăv���C���[�̃^�C�v��Ԃ�����(�����Ŏg������)
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
}
