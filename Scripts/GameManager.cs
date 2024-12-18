
/*
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public bool isGameOver = false;
    
    [SerializeField]
    private TextMeshProUGUI textGoal;

    public int goal;

    [SerializeField]
    private GameObject btnRetry;
    
    [SerializeField]
    private Color green;

    [SerializeField]
    private Color red;
    // [NEW] PinLauncher를 GameManager에 연결하여 초기화 제어
    [SerializeField]
    private PinLauncher pinLauncher;

    // [NEW] TargetCircle의 참조를 인스펙터에서 설정
    [SerializeField]
    private TargetCircle targetCircle;
    private void Awake() {
        if(instance == null){
            instance = this;
        }
        
    }

       // [NEW] 스테이지 데이터를 관리하기 위한 배열
    [System.Serializable]
    public class StageData
    {
        public int goal;            // 목표 개수
        public float rotateSpeed;   // 목표 원의 회전 속도
    }
    [SerializeField]
    private StageData[] stages;    // 모든 스테이지 데이터
    private int currentStage = 0;  // 현재 스테이지 인덱스


    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Start()
    {
        SetStage(currentStage); // [NEW] 첫 스테이지 설정
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseGoal(){
        goal -= 1;
        textGoal.SetText(goal.ToString());

        if(goal <=0){
            SetGameOver(true);
            Invoke("NextStage", 2f); // [NEW] 다음 스테이지로 전환
        }
    }

    public void SetGameOver(bool success) {
        if(isGameOver==false){
            isGameOver = true;

            Camera.main.backgroundColor = success ? green : red;
            Invoke("ShowRetryButton", 1f);
        }
    }

    void ShowRetryButton(){
        btnRetry.SetActive(true);
    }

    public void Retry(){
       SceneManager.LoadScene("SampleScene");
    }

    // [NEW] 현재 스테이지 데이터를 설정
    private void SetStage(int stageIndex)
    {
        if (stageIndex >= stages.Length)
        {
            Debug.Log("모든 스테이지를 클리어했습니다!");
            return;
        }

        currentStage = stageIndex;
        goal = stages[stageIndex].goal;
        textGoal.SetText(goal.ToString());
        isGameOver = false;
        btnRetry.SetActive(false);

        // [UPDATED] TargetCircle의 회전 속도를 직접 설정
        if (targetCircle != null)
        {
            targetCircle.SetRotateSpeed(stages[stageIndex].rotateSpeed);
        }
        // [NEW] PinLauncher 초기화
        if (pinLauncher != null)
        {
            pinLauncher.ResetLauncher();
        }
 
    }

    // [NEW] 다음 스테이지로 이동
    private void NextStage()
    {
        SetStage(currentStage + 1);
    }

}
*/


/*
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public bool isGameOver = false;

    [SerializeField]
    private TextMeshProUGUI textGoal;

    public int goal;

    [SerializeField]
    private GameObject btnRetry;

    [SerializeField]
    private Color green;

    [SerializeField]
    private Color red;

    [SerializeField]
    private PinLauncher pinLauncher; // PinLauncher 연결

    [SerializeField]
    private TargetCircle targetCircle; // TargetCircle 연결

    [System.Serializable]
    public class StageData
    {
        public int goal;            // 목표 핀 개수
        public float rotateSpeed;   // 목표 원의 회전 속도
    }

    [SerializeField]
    private StageData[] stages; // 스테이지 데이터 배열

    private int currentStage = 0; // 현재 스테이지 인덱스

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    
    void Start()
    {
        
        SetStage(currentStage); // 첫 스테이지 설정
    }

    public void DecreaseGoal()
    {
        if (isGameOver) return; // 게임 오버 상태라면 실행하지 않음

        goal -= 1;
        textGoal.SetText(goal.ToString()); // 목표 UI 업데이트

        if (goal <= 0) // 목표를 모두 달성한 경우
        {
            Debug.Log("목표 달성! 다음 스테이지로 이동합니다.");
            isGameOver = true; // 게임 오버 상태 설정
            Invoke("NextStage", 2f); // 2초 후 다음 스테이지로 이동
        }
    }

    public void SetGameOver(bool success)
    {
        if (isGameOver == false)
        {
            isGameOver = true;
            Camera.main.backgroundColor = success ? green : red;

            if (!success) // 실패한 경우에만 재시작 버튼 표시
            {
                Invoke("ShowRetryButton", 1f);
            }
        }
    }

    void ShowRetryButton()
    {
        btnRetry.SetActive(true);
    }

    public void Retry()
    {
        SceneManager.LoadScene("SampleScene");
    }



    private void SetStage(int stageIndex)
    {
        if (stages == null || stages.Length == 0)
        {
            Debug.LogError("stages 배열이 비어 있습니다! Unity 에디터에서 데이터를 설정하세요.");
            return;
        }

        if (stageIndex >= stages.Length)
        {
            Debug.Log("모든 스테이지를 클리어했습니다!");
            return;
        }

        currentStage = stageIndex;

        goal = stages[stageIndex].goal;
        if (goal <= 0) // 잘못된 goal 값 확인
        {
            Debug.LogError($"스테이지 {stageIndex + 1}의 goal 값이 0이거나 비정상적입니다!");
            return;
        }

        textGoal.SetText(goal.ToString()); // 목표 UI 업데이트
        isGameOver = false; // 게임 오버 상태를 초기화
        btnRetry.SetActive(false); // Retry 버튼 숨김

        if (targetCircle != null)
        {
            targetCircle.SetRotateSpeed(stages[stageIndex].rotateSpeed);
        }

        if (pinLauncher != null)
        {
            pinLauncher.ResetLauncher();
        }

        Debug.Log($"스테이지 {stageIndex + 1} 시작: 목표 {goal}개, 속도 {stages[stageIndex].rotateSpeed}");
    }

    private void NextStage()
    {
        if (currentStage + 1 >= stages.Length)
        {
            Debug.Log("모든 스테이지를 클리어했습니다!");
            return;
        }

        SetStage(currentStage + 1); // 다음 스테이지 설정
    }
}

*/

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public bool isGameOver = false;

    [SerializeField]
    private TextMeshProUGUI textGoal;

    public int goal;

    [SerializeField]
    private GameObject btnRetry;

    [SerializeField]
    private Color green;

    [SerializeField]
    private Color red;
    


    //사운드
    private AudioSource audioSource; // AudioSource 컴포넌트 가져오기
    [Header("Audio Settings")]
    [SerializeField]
    private AudioClip gameOverClip; // 게임 오버 사운드 파일
    [SerializeField]
    private AudioClip nextStageClip; // [NEW] 다음 스테이지로 넘어갈 때 사운드 파일
    [SerializeField]
    private AudioClip retryClip; // [NEW] 리트라이 버튼 클릭 시 사운드 파일
    
    


    [SerializeField]
    private PinLauncher pinLauncher; // PinLauncher 연결

    [SerializeField]
    private TargetCircle targetCircle; // TargetCircle 연결

    [System.Serializable]
    public class StageData
    {
        public int goal;            // 목표 핀 개수
        public float rotateSpeed;   // 목표 원의 회전 속도
    }

    [SerializeField]
    private StageData[] stages; // 스테이지 데이터 배열

    private int currentStage = 0; // 현재 스테이지 인덱스

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }



        // AudioSource 자동으로 가져오기
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource 컴포넌트가 GameManager 오브젝트에 추가되지 않았습니다!");
        }



    }

    void Start()
    {
        isGameOver = false; // 게임 오버 상태 초기화
        btnRetry.SetActive(false); // Retry 버튼 비활성화

        // 기존 핀 제거
        foreach (GameObject pin in GameObject.FindGameObjectsWithTag("Pin"))
        {
            Destroy(pin);
        }

        SetStage(currentStage); // 첫 스테이지 설정
    }

    /*

    public void DecreaseGoal()
    {
        if (isGameOver) return;

        goal -= 1;
        textGoal.SetText(goal.ToString());

        if (goal <= 0)
        {
            Debug.Log("목표 달성! 다음 스테이지로 이동합니다.");
            isGameOver = true;
            Invoke("NextStage", 2f);
        }
    }
    */

    public void DecreaseGoal()
    {
        if (isGameOver) return; // 게임 종료 상태라면 실행하지 않음

        goal -= 1;
        textGoal.SetText(goal.ToString());

        if (goal <= 0)
        {
            Debug.Log("목표 달성! 다음 스테이지로 이동합니다.");
            isGameOver = true; // 게임 종료 상태 설정
            Invoke("NextStage", 2f);
        }
    }



    public void SetGameOver(bool success)
    {
        if (isGameOver) return;

        isGameOver = true;




        // 게임 오버 사운드 재생
        if (!success && audioSource != null && gameOverClip != null)
        {
            audioSource.PlayOneShot(gameOverClip);
        }
        


        
        Debug.Log($"SetGameOver 호출됨. 성공 여부: {success}");

        Camera.main.backgroundColor = success ? green : red;

        if (!success)
        {
            Invoke("ShowRetryButton", 1f);
        }
    }

    void ShowRetryButton()
    {
        btnRetry.SetActive(true);
    }

    public void Retry()
    {
        // [NEW] 리트라이 사운드 재생
        if (audioSource != null && retryClip != null)
        {
            audioSource.PlayOneShot(retryClip);
        }


        SceneManager.LoadScene("SampleScene");
    }

    private void SetStage(int stageIndex)
    {
        if (stages == null || stages.Length == 0)
        {
            Debug.LogError("stages 배열이 비어 있습니다!");
            return;
        }

        if (stageIndex >= stages.Length)
        {
            Debug.Log("모든 스테이지를 클리어했습니다!");
            return;
        }

        currentStage = stageIndex;
        goal = stages[stageIndex].goal;

        if (goal <= 0)
        {
            Debug.LogError($"스테이지 {stageIndex + 1}의 goal 값이 비정상적입니다!");
            return;
        }

        textGoal.SetText(goal.ToString());
        isGameOver = false;
        btnRetry.SetActive(false);

        if (targetCircle != null)
        {
            targetCircle.SetRotateSpeed(stages[stageIndex].rotateSpeed);
        }

        if (pinLauncher != null)
        {
            pinLauncher.ResetLauncher();
        }
    }
    
    /*
    private void NextStage()
    {
        if (currentStage + 1 >= stages.Length)
        {
            Debug.Log("모든 스테이지를 클리어했습니다!");
            return;
        }

        SetStage(currentStage + 1);
    }

    */
    private void NextStage()
    {
        if (currentStage + 1 >= stages.Length)
        {
            Debug.Log("모든 스테이지를 클리어했습니다!");
            return;
        }


        // [NEW] 다음 스테이지 사운드 재생
        if (audioSource != null && nextStageClip != null)
        {
            audioSource.PlayOneShot(nextStageClip);
        }

        // [NEW] 기존 핀 제거
        foreach (GameObject pin in GameObject.FindGameObjectsWithTag("Pin"))
        {
            Destroy(pin);
        }

        currentStage++; // 스테이지 인덱스 증가
        SetStage(currentStage); // 다음 스테이지 설정
    }



}
