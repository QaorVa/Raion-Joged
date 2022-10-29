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
    public Animator charAnim;

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

    public GameObject resultScreen, resultsScreen2, failScreen, pauseScreen;
    public TMP_Text percentHitText, okaysText, goodsText, perfectsText, missesText, rankText, finalScoreText, accText;

    public ParticleSystem x2Particle, x3Particle, x4Particle;

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

        // mengambil data highscore, di editor save-an kesimpen juga jadi klo mau reset jalanin kode diatas 
        if(PlayerPrefs.HasKey("Bullet"))
        {
            highScore = PlayerPrefs.GetInt("Bullet");
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
                resultScreen.GetComponent<AudioSource>().Play();

                LeanTween.moveLocalY(resultsScreen2, 0f, 1f)
                .setDelay(0.5f)
                .setIgnoreTimeScale(true)
                .setEase(LeanTweenType.easeOutBounce);

                if (currentScore >= maxScore * 0.75f)
                {
                    S.SetActive(true);
                    if(currentScore > highScore)
                    {
                        highScore = currentScore;
                        PlayerPrefs.SetInt("Bullet", highScore);
                        PlayerPrefs.SetInt("BulletGrade", 0);
                    }
                } else if (currentScore >= maxScore * 0.45f)
                {
                    A.SetActive(true);
                    if(currentScore > highScore)
                    {
                        highScore = currentScore;
                        PlayerPrefs.SetInt("Bullet", highScore);
                        PlayerPrefs.SetInt("BulletGrade", 1);
                    }
                } else if (currentScore >= maxScore * 0.25f)
                {
                    B.SetActive(true);
                    if(currentScore > highScore)
                    {
                        highScore = currentScore;
                        PlayerPrefs.SetInt("Bullet", highScore);
                        PlayerPrefs.SetInt("BulletGrade", 2);
                    }
                } else if (currentScore >= maxScore * 0.19f)
                {
                    C.SetActive(true);
                    if(currentScore > highScore)
                    {
                        highScore = currentScore;
                        PlayerPrefs.SetInt("Bullet", highScore);
                        PlayerPrefs.SetInt("BulletGrade", 3);
                    }
                } else if (currentScore >= maxScore * 0.08f)
                {
                    D.SetActive(true);
                    if(currentScore > highScore)
                    {
                        highScore = currentScore;
                        PlayerPrefs.SetInt("Bullet", highScore);
                        PlayerPrefs.SetInt("BulletGrade", 4);
                    }
                } else
                {
                    F.SetActive(true);
                    if(currentScore > highScore)
                    {
                        highScore = currentScore;
                        PlayerPrefs.SetInt("Bullet", highScore);
                        PlayerPrefs.SetInt("BulletGrade", 5);
                    }
                }

                okaysText.text = okayHits.ToString();
                goodsText.text = goodHits.ToString();
                perfectsText.text = perfectHits.ToString();
                missesText.text = missedHits.ToString();
                finalScoreText.text = currentScore.ToString();

                float totalHit = okayHits + goodHits + perfectHits;
                float percentHit = (totalHit / totalNotes) * 100f;

                percentHitText.text = percentHit.ToString("F1") + "%";


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

        mulText.text = "x" + currentMultiplier;

        Accuracy();

        maxScore += perfectScore * 4;

        CharParticle();
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
        charAnim.SetTrigger("Miss");

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
        mulText.text = "x" + currentMultiplier;

        Accuracy();

        maxScore += perfectScore * 4;

        CharParticle();
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

    public void CharParticle()
    {
        if(currentMultiplier == 2)
        {
            mulText.fontSize = 60;
            mulText.color = Color.yellow;

            x2Particle.emissionRate = 50f;
        } else if (currentMultiplier == 3)
        {
            mulText.fontSize = 70;
            mulText.color = Color.green;

            x3Particle.emissionRate = 50f;
            x2Particle.emissionRate = 0f;
        } else if (currentMultiplier == 4)
        {
            mulText.fontSize = 80;
            mulText.color = Color.cyan;

            x4Particle.emissionRate = 50f;
            x3Particle.emissionRate = 0f;
        } else
        {
            mulText.fontSize = 50;
            mulText.color = Color.white;

            x4Particle.emissionRate = 0f;
            x3Particle.emissionRate = 0f;
            x2Particle.emissionRate = 0f;
        }
    }
}
