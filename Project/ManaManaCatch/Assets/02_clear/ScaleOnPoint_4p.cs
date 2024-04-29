using UnityEngine;

public class ScaleOnPoint_4p : MonoBehaviour
{
    public float minHeight = 1.0f; // Y���̍ŏ��̑傫��
    public float maxHeight = 5.0f; // Y���̍ő�̑傫��
    public float lastpoint_4p;
    public float point_4p = 40;

    private int scorePoint = 0;
    private int scoreMax = 0;

    private void Awake()
    {
        scorePoint = GameData.scoreList[3];

        scoreMax = GameData.scoreMax;
    }


    void Update()
    {
        if (point_4p < scorePoint)
        {
            point_4p += 0.007f;
        }
        lastpoint_4p = point_4p / scoreMax;
        
        // Y���̑傫�����v�Z����
        float scaleY = Mathf.Lerp(minHeight, maxHeight, lastpoint_4p);

        // �I�u�W�F�N�g�̑傫����ݒ肷��
        transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
    }
}