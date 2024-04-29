using UnityEngine;

public class ScaleOnPoint_1p : MonoBehaviour
{
    public float minHeight = 1.0f; // Y���̍ŏ��̑傫��
    public float maxHeight = 5.0f; // Y���̍ő�̑傫��
    public float lastpoint_1p;
    public float point_1p = 40;

    private int scorePoint = 0;
    private int scoreMax = 0;

    private void Awake()
    {
        scorePoint = GameData.scoreList[0];

        scoreMax = GameData.scoreMax;
    }


    void Update()
    {
        if (point_1p < scorePoint)
        {
            point_1p += 0.007f;
        }
        lastpoint_1p = point_1p / scoreMax;
        
        
        // Y���̑傫�����v�Z����
        float scaleY = Mathf.Lerp(minHeight, maxHeight, lastpoint_1p);

        // �I�u�W�F�N�g�̑傫����ݒ肷��
        transform.localScale = new Vector3(transform.localScale.x, scaleY, transform.localScale.z);
    }
}