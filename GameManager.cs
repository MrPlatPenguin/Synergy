using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    CharacterManager CM;

    static bool checkpointHit;

    static Vector3 lightPostion, darkPositon;
    static Quaternion lightRotation, darkRotation;

    static bool _isPaused;

    [SerializeField] GameObject pauseMenu, deathScreen, winScreen;

    public bool canPause = true;
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) && canPause)
        {
            Pause();
        }
    }

    private void Awake()
    {
        CM = FindObjectOfType<CharacterManager>();
        if (canPause)
        {
            if (checkpointHit)
                LoadFromCheckpoint();
        }
        isPaused = false;
    }

    public static void SaveCheckpoint(Transform lightLocation, Transform darkLocation)
    {
        checkpointHit = true;
        lightPostion = lightLocation.position;
        lightRotation = lightLocation.rotation;
        darkPositon = darkLocation.position;
        darkRotation = darkLocation.rotation;
    }

    void LoadFromCheckpoint()
    {
        print("Loaded from checkpoint");
        CM.characterOne.GetComponent<CharacterMovement>().warpPos = lightPostion;
        CM.characterTwo.GetComponent<CharacterMovement>().warpPos = darkPositon;
        CM.characterOne.position = lightPostion;
        CM.characterOne.rotation = lightRotation;
        CM.characterTwo.position = darkPositon;
        CM.characterTwo.rotation = darkRotation;
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public static bool isPaused
    {
        get { return _isPaused; }
        set
        {
            _isPaused = value;
            Time.timeScale = value ? 0 : 1;
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Death()
    {
        Cursor.lockState = CursorLockMode.None;
        deathScreen.SetActive(true);
        isPaused = true;
    }

    public void Pause()
    {
        isPaused = !isPaused;
        if (pauseMenu != null) pauseMenu.SetActive(isPaused);
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void Win()
    {
        winScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        canPause = false;
    }
}
