

/*
using UnityEngine;

public class TargetCircle : MonoBehaviour
{
    [SerializeField]

    private float rotateSpeed = -30f; //시계방향 (음수),반시계방향 (양수);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   if(GameManager.instance.isGameOver == false){
        transform.Rotate(0,0,rotateSpeed*Time.deltaTime);
    }
    }

    // [NEW] 회전 속도 설정 메서드 추가
    public void SetRotateSpeed(float speed)
    {
        rotateSpeed = speed;
    }
}

*/

using UnityEngine;

public class TargetCircle : MonoBehaviour
{
    [SerializeField]
    private float rotateSpeed = -30f; // 목표 원 회전 속도

    void Update()
    {
        if (!GameManager.instance.isGameOver)
        {
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime); // 회전
        }
    }

    public void SetRotateSpeed(float speed)
    {
        rotateSpeed = speed; // 회전 속도 설정
    }
}
