using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DayNightManager : MonoBehaviour
{
    public static DayNightManager instance;

    [HideInInspector] public bool isGameOver;

    public GameObject directional;
    
    [Header("Menu")]
    public Text gameOverTxt;
    public Button reloadBtn;
    public Button exitBtn;

    public float totalSecondsTo16hours = 91;
    public float startDayRotationX = 0;
    public float endDayRotationX = 270;
    public float secondsLeft = 90;
    public Text timer;

    private float elapsedTime = 0;
    bool isPaused;

    private void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        Reset();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        secondsLeft = totalSecondsTo16hours - elapsedTime;
        if (secondsLeft < 0) GameOver();
        SetText();
        float rot = (endDayRotationX - startDayRotationX) / totalSecondsTo16hours;
        directional.transform.Rotate(rot * Time.deltaTime, 0, 0);

        if (Input.GetKeyDown(KeyCode.R) || secondsLeft <= 0)
        {
            Reset();
        }
        if (Input.GetKeyDown(KeyCode.Escape) && !isGameOver)
        {
            PauseGame();
        }
    }

    public void Reset()
    {
        secondsLeft = 90;
        totalSecondsTo16hours--;
        elapsedTime = 0;
        directional.transform.rotation = Quaternion.Euler(startDayRotationX, 0, 0);
    }

    void SetText()
    {
        float secondsXHour = totalSecondsTo16hours / 16.0f;
        int hour = (int)((totalSecondsTo16hours - secondsLeft) / secondsXHour);

        int minutes = (int)((((totalSecondsTo16hours - secondsLeft) / secondsXHour) - hour) * 60);

        string label;

        if ((hour + 8) > 12)
        {
            label = (hour + 8 - 12).ToString("00") + ":" + minutes.ToString("00") + "F G";
        }
        else
        {
            label = (hour + 8).ToString("00") + ":" + minutes.ToString("00") + "E G";
        }
        
        timer.text = label;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOverTxt.gameObject.SetActive(true);
        reloadBtn.gameObject.SetActive(true);
        exitBtn.gameObject.SetActive(true);
    }

    void PauseGame()
    {
        reloadBtn.gameObject.SetActive(!isPaused);
        exitBtn.gameObject.SetActive(!isPaused);

        if (isPaused)
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        isPaused = !isPaused;
    }

    public void ReloadGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
