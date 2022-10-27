using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static bool gameIsPaused = false;
    public GameObject pauseMenu;
    public GameObject scoreManager;
    public AudioSource audioSource;
    public LoadSceneManager loadManager;

    public static GameManager instance;

    public int currentScore;
    public int perfectScore = 300;
    public int goodScore = 200;
    public int okayScore = 100;

    public int currentMultiplier;
    public int comboTracker;
    public int[] multiplierThresholds;

    public TMP_Text scoreText;
    public TMP_Text mulText;
    public TMP_Text comboText;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        scoreText.text = "SCORE: 0";
        currentMultiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {

        // mulai saat mencet any key 
        if(Input.anyKeyDown && !gameIsPaused)
        {
           scoreManager.GetComponent<SongManager>().enabled = true;         
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // method yang dijalankan saat note berhasil dihit
    public void noteHit()
    {
        //cek di konsol apakah note berhasil dihit
        Debug.Log("Note Hit");

        //iterasi nilai combo
        comboTracker++;

        //cek jika multiplier lebih kecil dari maks multiplier
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            //memunculkan ui teks combo jika combo > 0
            if (comboTracker > 0)
            {
                comboText.enabled = true;
            }

            //menambahkan multiplier saat jumlah combo sudah mencapai angka tertentu untuk multiplier
            if (comboTracker == multiplierThresholds[currentMultiplier - 1])
            {
                currentMultiplier++;
            }
        }

        //menampilkan teks score, combo, dan multiplier
        comboText.text = comboTracker + "x";

        //currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "SCORE: " + currentScore;

        mulText.text = "MULTIPLIER: x" + currentMultiplier;
    }

    public void OkayHit()
    {
        Debug.Log("Okay Hit");

        currentScore += okayScore * currentMultiplier;
        noteHit();
    }

    public void GoodHit()
    {
        Debug.Log("Good Hit");

        currentScore += goodScore * currentMultiplier;
        noteHit();
    }

    public void PerfectHit()
    {
        Debug.Log("Perfect Hit");

        currentScore += perfectScore * currentMultiplier;
        noteHit();
    }

    // method yang dijalankan saat note gagal untuk dihit
    public void noteMissed()
    {
        Debug.Log("Note Missed");

        //mereset nilai multiplier
        currentMultiplier = 1;

        //mereset nilai combo
        comboTracker = 0;

        //menghilangkan teks combo jika combo = 0
        if (comboTracker == 0)
        {
            comboText.enabled = false;
        }

        //menampilkan teks multiplier
        mulText.text = "MULTIPLIER: x" + currentMultiplier;


    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
        audioSource.Pause();
    }
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
        audioSource.Play();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    public void Back()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        pauseMenu.SetActive(false);
        loadManager.BackScene();
    }
}
