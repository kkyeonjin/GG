using UnityEngine;

public class ItemVisualEffect : MonoBehaviour
{
    //y축 상하 반복 이동
    private float initYPos;
    public const float moveSpeed = 1f;
    public const float offset = 1f;
    private bool moveUp = true; //t: 상승 f:하강

    //y축 오른쪽 회전
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

        //상승 -> 하강 전환
        if(dist >= offset)
        {
            moveUp = false;
        }
        //하강 -> 상승 전환
        else if (dist <= 0f)
        {
            moveUp = true;
        }

        if(moveUp) //상승
        {
            this.transform.position = transform.position + new Vector3(0, 1, 0) * moveSpeed * Time.deltaTime;
        }
        else //하강
        {
            this.transform.position = transform.position + new Vector3(0, -1, 0) * moveSpeed * Time.deltaTime;
        }
    }
}