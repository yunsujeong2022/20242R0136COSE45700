
/*
using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;

    private bool isPinned = false;
    private bool isLaunched = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isPinned==false&&isLaunched==true){
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        isPinned = true;
        if(other.gameObject.tag=="Target"){
            GameObject childObject = transform.Find("Square").gameObject;
            //GameObject childObject = transform.GetChild(0).gameObject;
            SpriteRenderer childSprite = childObject.GetComponent<SpriteRenderer>();
            childSprite.enabled = true;

            transform.SetParent(other.gameObject.transform);

            GameManager.instance.DecreaseGoal();
        }else if (other.gameObject.tag=="Pin"){
            GameManager.instance.SetGameOver(false);
        }
    }


    public void Launch(){
       isLaunched = true;
    }
}

*/


/*
using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f; // 핀 이동 속도

    private bool isPinned = false; // 핀이 고정되었는지 여부
    private bool isLaunched = false; // 핀이 발사되었는지 여부

    void FixedUpdate()
    {
        if (!isPinned && isLaunched) // 발사된 상태에서만 위로 이동
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        isPinned = true;

        if (other.gameObject.tag == "Target")
        {
            GameObject childObject = transform.Find("Square").gameObject;
            SpriteRenderer childSprite = childObject.GetComponent<SpriteRenderer>();
            childSprite.enabled = true;

            transform.SetParent(other.gameObject.transform); // 목표에 핀 고정
            GameManager.instance.DecreaseGoal(); // 목표 감소
        }
        else if (other.gameObject.tag == "Pin")
        {
            GameManager.instance.SetGameOver(false); // 다른 핀과 충돌 시 실패 처리
        }
    }

    public void Launch()
    {
        isLaunched = true;
    }
}
*/

using UnityEngine;

public class Pin : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;

    private bool isPinned = false;
    private bool isLaunched = false;
    
    //오디오
    private AudioSource audioSource; // AudioSource 컴포넌트를 가져오기 위한 변수

    void Awake()
    {
        // AudioSource 자동으로 가져오기
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource 컴포넌트가 Pin에 추가되지 않았습니다!");
        }
    }

    void FixedUpdate()
    {
        if (!isPinned && isLaunched)
        {
            transform.position += Vector3.up * moveSpeed * Time.deltaTime;
        }
    }


    /*
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isPinned || GameManager.instance.isGameOver) return;

        isPinned = true;

        if (other.gameObject.tag == "Target")
        {
            GameObject childObject = transform.Find("Square").gameObject;
            SpriteRenderer childSprite = childObject.GetComponent<SpriteRenderer>();
            childSprite.enabled = true;

            transform.SetParent(other.gameObject.transform);
            GameManager.instance.DecreaseGoal();
        }
        else if (other.gameObject.tag == "Pin")
        {
            GameManager.instance.SetGameOver(false);
        }
    }
    */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isPinned || GameManager.instance.isGameOver) return; // 이미 고정되었거나 게임 종료 상태라면 실행하지 않음

        if (other.gameObject.tag == "Target")
        {
            isPinned = true; // 고정 상태 설정


            // 사운드 재생
            if (audioSource != null)
            {
                audioSource.Play();
            }



            Debug.Log("핀 고정됨: Target에 충돌");

            GameObject childObject = transform.Find("Square").gameObject;
            SpriteRenderer childSprite = childObject.GetComponent<SpriteRenderer>();
            childSprite.enabled = true;

            transform.SetParent(other.gameObject.transform); // 목표에 핀 고정
            GameManager.instance.DecreaseGoal(); // 목표 감소
        }
        else if (other.gameObject.tag == "Pin")
        {
            if (!isPinned) // 핀이 아직 고정되지 않은 경우에만 게임 종료 처리
            {
                Debug.Log("핀끼리 충돌, 게임 종료");
                GameManager.instance.SetGameOver(false);
            }
        }
    }



    public void Launch()
    {
        isLaunched = true;
    }
}
