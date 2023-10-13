using UnityEngine;

public class ItemVisualEffect : MonoBehaviour
{
    //y축 상하 반복 이동
    private float initYPos;
    public float moveSpeed = 0.4f;
    public float offset = 0.3f;
    private bool moveUp = true; //t: 상승 f:하강

    private float f = 0f;
    private Vector3 vOriginPos;

    //y축 오른쪽 회전
    public float rotateSpeed = 50f;

    private void Start()
    {
        vOriginPos = transform.position;
    }
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
        this.transform.Rotate(new Vector3(0,1,0) * rotateSpeed * Time.deltaTime);
    }

    private void updown()
    {
        f += Time.deltaTime;
        this.transform.position = vOriginPos  + new Vector3(0, 1, 0) * offset * Mathf.Sin(moveSpeed * f);       
    }
}