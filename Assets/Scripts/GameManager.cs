using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static bool gameIsPaused = false;
    public static bool isFailed = false;

    public GameObject scoreManager;
    public AudioSource audioSource;
    public LoadSceneManager loadManager;

    public GameObject startingScreen;

    public static GameManager instance;

    public int currentScore;
    public int perfectScore = 300;
    public int goodScore = 200;
    public int okayScore = 100;

    // -----------------------------baru--------------------------------
    public int highScore = 0;

    // nyari max score buat ranking
    public int maxScore;

    public int currentMultiplier;
    public int comboTracker;
    public int[] multiplierThresholds;

    public TMP_Text scoreText;
    public TMP_Text mulText;
    public TMP_Text comboText;

    public float totalNotes;
    public float okayHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    public GameObject A, B, C, D, S, F;

    public GameObject resultScreen, failScreen, pauseScreen;
    public TMP_Text percentHitText, okaysText, goodsText, perfectsText, missesText, rankText, finalScoreText, accText;

    // Start is called before the first frame update
    void Start()
    {   
        instance = this;

        // Reset semua pause
        gameIsPaused = false;
        Time.timeScale = 1;
        isFailed = false;

        scoreText.text = "SCORE: 0";
        currentMultiplier = 1;

        PlayerPrefs.DeleteAll();
        // mengambil data highscore, di editor save-an kesimpen juga jadi klo mau reset jalanin kode diatas 
        if(PlayerPrefs.HasKey("Bullet"))
        {
            highScore = PlayerPrefs.GetInt("Bullet");
            Debug.Log(highScore);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Escape) && !resultScreen.activeInHierarchy && !failScreen.activeInHierarchy && !startingScreen.activeInHierarchy)
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

        // mulai saat mencet any key 
        if(Input.anyKeyDown && !gameIsPaused && !isFailed)
        {
            scoreManager.GetComponent<SongManager>().enabled = true;
            startingScreen.SetActive(false);
        }

        if(!gameIsPaused)
        {
            if (!Health.instance.alive && !resultScreen.activeInHierarchy)
            {
                Failed();
            }
            
            else if (audioSource.time >= audioSource.clip.length && !resultScreen.activeInHierarchy && Health.instance.alive)
            {
                resultScreen.SetActive(true);

                if (currentScore >= maxScore * 0.75f)
                {
                    S.SetActive(true);
                } else if (currentScore >= maxScore * 0.45f)
                {
                    A.SetActive(true);
                } else if (currentScore >= maxScore * 0.25f)
                {
                    B.SetActive(true);
                } else if (currentScore >= maxScore * 0.19f)
                {
                    C.SetActive(true);
                } else if (currentScore >= maxScore * 0.08f)
                {
                    D.SetActive(true);
                } else
                {
                    F.SetActive(true);
                }

                okaysText.text = okayHits.ToString();
                goodsText.text = goodHits.ToString();
                perfectsText.text = perfectHits.ToString();
                missesText.text = missedHits.ToString();
                finalScoreText.text = currentScore.ToString();

                float totalHit = okayHits + goodHits + perfectHits;
                float percentHit = (totalHit / totalNotes) * 100f;

                percentHitText.text = percentHit.ToString("F1") + "%";

                // save highscore
                if(currentScore > highScore)
                {
                    highScore = currentScore;
                    PlayerPrefs.SetInt("Bullet", highScore);
                }
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

        totalNotes++;

        //menampilkan teks score, combo, dan multiplier
        comboText.text = comboTracker + "x";

        //currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "SCORE: " + currentScore;

        mulText.text = "MULTIPLIER: x" + currentMultiplier;

        Accuracy();

        maxScore += perfectScore * 4;
    }

    public void OkayHit()
    {
        Debug.Log("Okay Hit");

        okayHits++;

        currentScore += okayScore * currentMultiplier;
        noteHit();

        Health.instance.Heal(1f);
    }

    public void GoodHit()
    {
        Debug.Log("Good Hit");

        goodHits++;

        currentScore += goodScore * currentMultiplier;
        noteHit();

        Health.instance.Heal(2f);
    }

    public void PerfectHit()
    {
        Debug.Log("Perfect Hit");

        perfectHits++;

        currentScore += perfectScore * currentMultiplier;
        noteHit();

        Health.instance.Heal(5f);
    }

    // method yang dijalankan saat note gagal untuk dihit
    public void noteMissed()
    {
        Debug.Log("Note Missed");

        //mereset nilai multiplier
        currentMultiplier = 1;

        //mereset nilai combo
        comboTracker = 0;

        Health.instance.TakeDamage(4f);

        //menghilangkan teks combo jika combo = 0
        if (comboTracker == 0)
        {
            comboText.enabled = false;
        }

        totalNotes++;
        missedHits++;

        //menampilkan teks multiplier
        mulText.text = "MULTIPLIER: x" + currentMultiplier;

        Accuracy();

        maxScore += perfectScore * 4;
    }

    public void Pause()
    {
        pauseScreen.SetActive(true);
        //Time.timeScale = 0f;
        gameIsPaused = true;
        audioSource.Pause();
    }
    public void Resume()
    {
        pauseScreen.GetComponent<Animator>().SetTrigger("PopEnd");
        Time.timeScale = 1f;
        gameIsPaused = false;
        audioSource.Play();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        gameIsPaused = false;
        isFailed = false;
    }
    public void Back()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        pauseScreen.SetActive(false);
        loadManager.BackScene();
        isFailed = false;
    }

    public void Failed()
    {
        isFailed = true;

        failScreen.SetActive(true);
        scoreText.enabled = false;
        mulText.enabled = false;
        //scoreManager.GetComponent<SongManager>().enabled = false;

        okaysText.text = okayHits.ToString();
        goodsText.text = goodHits.ToString();
        perfectsText.text = perfectHits.ToString();
        missesText.text = missedHits.ToString();

        float totalHit = okayHits + goodHits + perfectHits;
        float percentHit = (totalHit / totalNotes) * 100f;

        percentHitText.text = percentHit.ToString("F1") + "%";
        finalScoreText.text = currentScore.ToString();

        // save highscore
        if(currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("Bullet", highScore);
        }

        //Time.timeScale = 0f;
        gameIsPaused = true;
        audioSource.Pause();
    }

    public void Accuracy()
    {
        float totalHit = okayHits + goodHits + perfectHits;
        float percentHit = (totalHit / totalNotes) * 100f;
        accText.text = percentHit.ToString("F0");
    }
}
