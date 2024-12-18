

/*
using UnityEngine;

public class PinLauncher : MonoBehaviour
{
    [SerializeField]
    private GameObject pinObject;

    private Pin currPin;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PreparePin();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)
           && currPin != null
           && GameManager.instance.isGameOver==false){
           currPin.Launch();
           currPin = null;
           Invoke("PreparePin", 0.1f);
        }
    }

    void PreparePin(){
        if(GameManager.instance.isGameOver == false){
           GameObject pin =Instantiate(pinObject,transform.position,Quaternion.identity);
           currPin = pin.GetComponent<Pin>();
        }
    }
    

    // [NEW] PinLauncher 초기화 메서드 추가
    public void ResetLauncher()
    {
        currPin = null; // 현재 핀 초기화
        PreparePin();    // 새로운 핀 준비
    }
}
*/


/*
using UnityEngine;

public class PinLauncher : MonoBehaviour
{
    [SerializeField]
    private GameObject pinObject; // 생성할 핀 프리팹

    private Pin currPin;

    void Start()
    {
        PreparePin(); // 첫 핀 준비
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currPin != null && !GameManager.instance.isGameOver)
        {
            currPin.Launch();
            currPin = null;
            Invoke("PreparePin", 0.1f); // 다음 핀 준비
        }
    }

    void PreparePin()
    {
        if (!GameManager.instance.isGameOver)
        {
            GameObject pin = Instantiate(pinObject, transform.position, Quaternion.identity);
            currPin = pin.GetComponent<Pin>();
        }
    }

    public void ResetLauncher()
    {
        currPin = null; // 현재 핀 초기화
        PreparePin();   // 새로운 핀 준비
    }
}


*/

using UnityEngine;

public class PinLauncher : MonoBehaviour
{
    [SerializeField]
    private GameObject pinObject;

    private Pin currPin;

    void Start()
    {
        foreach (GameObject pin in GameObject.FindGameObjectsWithTag("Pin"))
        {
            Destroy(pin);
        }

        PreparePin();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currPin != null && !GameManager.instance.isGameOver)
        {
            currPin.Launch();
            currPin = null;
            Invoke("PreparePin", 0.1f);
        }
    }

    /*
    void PreparePin()
    {
        if (!GameManager.instance.isGameOver)
        {
            GameObject pin = Instantiate(pinObject, transform.position, Quaternion.identity);
            currPin = pin.GetComponent<Pin>();
        }
    }

    */

    void PreparePin()
    {
        if (!GameManager.instance.isGameOver)
        {
            GameObject pin = Instantiate(pinObject, transform.position, Quaternion.identity);
            pin.transform.position += Vector3.up * 0.5f; // [NEW] 초기 위치를 약간 위로 이동
            currPin = pin.GetComponent<Pin>();
        }
    }


    public void ResetLauncher()
    {
        currPin = null;
        PreparePin();
    }
}
