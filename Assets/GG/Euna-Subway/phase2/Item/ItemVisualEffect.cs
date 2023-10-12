using UnityEngine;

public class ItemVisualEffect : MonoBehaviour
{
    //y�� ���� �ݺ� �̵�
    private float initYPos;
    public const float moveSpeed = 1f;
    public const float offset = 1f;
    private bool moveUp = true; //t: ��� f:�ϰ�

    //y�� ������ ȸ��
    public float rotateSpeed = 1f;
    private void Update()
    {
        rotate();
        updown();
    }


    private void Awake()
    {
        initYPos = this.transform.position.y;
    }


    private void rotate()
    {
        this.transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);
    }

    private void updown()
    {
        float dist = this.transform.position.y - initYPos;

        //��� -> �ϰ� ��ȯ
        if(dist >= offset)
        {
            moveUp = false;
        }
        //�ϰ� -> ��� ��ȯ
        else if (dist <= 0f)
        {
            moveUp = true;
        }

        if(moveUp) //���
        {
            this.transform.position = transform.position + new Vector3(0, 1, 0) * moveSpeed * Time.deltaTime;
        }
        else //�ϰ�
        {
            this.transform.position = transform.position + new Vector3(0, -1, 0) * moveSpeed * Time.deltaTime;
        }
    }
}