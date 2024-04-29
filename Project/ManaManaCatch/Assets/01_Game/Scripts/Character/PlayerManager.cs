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
        ms_instance = this;
    }

    public void PlayAnim(int no, bool scoop, bool success = false, bool bomb = false)
    {
        if (!(0 <= no && no < m_players.Count)) return;

        var player = m_players[no];

        if (!scoop)
        {
            player.PlayScoopAnim();
            return;
        }
        if (bomb)
        {
            player.PlayBombAnim();
            return;
        }

        player.PlayCatchAnim(success);
    }

    public bool CanScoop(int no)
    {
        if (!(0 <= no && no < m_players.Count)) return false;

        var player = m_players[no];
        return !player.isPlaying;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (Input.GetKey(KeyCode.Space)) PlayAnim(0, true, Random.Range(0, 2) == 0);
            else PlayAnim(0, false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (Input.GetKey(KeyCode.Space)) PlayAnim(1, true, Random.Range(0, 2) == 0);
            else PlayAnim(1, false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (Input.GetKey(KeyCode.Space)) PlayAnim(2, true, Random.Range(0, 2) == 0);
            else PlayAnim(2, false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (Input.GetKey(KeyCode.Space)) PlayAnim(3, true, Random.Range(0, 2) == 0);
            else PlayAnim(3, false);
        }
    }
}
