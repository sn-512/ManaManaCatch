using UnityEngine;

public class ScaleOnPoint_2p : MonoBehaviour
{
    public float minHeight = 1.0f; // Y���̍ŏ��̑傫��
    public float maxHeight = 5.0f; // Y���̍ő�̑傫��
    public float lastpoint_2p;
    public float point_2p = 0;

    private int scorePoint = 0;
    private int scoreMax = 0;

    private void Awake()
    {
        scorePoint = GameData.scoreList[1];

        scoreMax = GameData.scoreMax;
    }





    void Update()
    {
        if (point_2p < scorePoint)
        {
            point_2p += 0.007f;
        }
        lastpoint_2p = point_2p / scoreMax;
        
        // Y���̑傫�����v�Z����
        float scaleY = Mathf.Lerp(minHeight, maxHeight, lastpoint_2p);

        // �I�u�W�F�N�g�̑傫����ݒ肷��
        transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
    }
    
}