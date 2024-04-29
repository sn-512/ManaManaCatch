using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager ms_instance = null;
    public static PlayerManager instance { get { return ms_instance; } }

    [SerializeField]
    private List<Player> m_players;

    private void Awake()
    {
        m_players = new List<Player>(GetComponentsInChildren<Player>());
    }

    public void PlayCatch(int no)
    {
    }

    public void PlayBomb(int no)
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                m_players[0].PlayCatchAnim(Random.Range(0, 2) == 0);
            }
            else
            {
                m_players[0].PlayScoopAnim();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                m_players[1].PlayCatchAnim(Random.Range(0, 2) == 0);
            }
            else
            {
                m_players[1].PlayScoopAnim();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                m_players[2].PlayCatchAnim(Random.Range(0, 2) == 0);
            }
            else
            {
                m_players[2].PlayScoopAnim();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (Input.GetKey(KeyCode.Space))
            {
                m_players[3].PlayCatchAnim(Random.Range(0, 2) == 0);
            }
            else
            {
                m_players[3].PlayScoopAnim();
            }
        }
    }
}
