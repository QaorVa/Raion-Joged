using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SongSelectManager : MonoBehaviour
{
    public SongDatabase songDB;
    public TMP_Text nameText;
    public TMP_Text highScoreText;
    public Image songSprite;
    public GameObject playButton;
    public Sprite[] grade;
    public GameObject centerGrade;

    private int selectedOption = 0;
    void Start()
    {
        UpdateSong(selectedOption);
        Time.timeScale = 1f;
    }

    public void NextOption()
    {
        selectedOption++;
        if(selectedOption >= songDB.SongCount)
        {
            selectedOption = 0;
        }

        GetComponent<Animator>().SetTrigger("out");
    }
    public void BackOption()
    {
        selectedOption--;
        if(selectedOption < 0)
        {
            selectedOption = songDB.SongCount - 1;
        }

        GetComponent<Animator>().SetTrigger("out");
    }

    public void UpdateFromAnim()
    {
        UpdateSong(selectedOption);
    }

    public void UpdateSong(int selectedOption)
    {
        Song song = songDB.GetSong(selectedOption);
        songSprite.sprite = song.songSprite;
        nameText.text = song.songName;
        //Tulisan highscore
        if(PlayerPrefs.HasKey(song.songName))
        {
            highScoreText.text = PlayerPrefs.GetInt(song.songName).ToString();
            centerGrade.SetActive(true);

            //highscore sesuai grade blum ya di edit lg ini berapa, grade[0 - 5] -> sprite[S - F]
            Debug.Log(PlayerPrefs.GetInt(song.songName + "Grade"));
            centerGrade.GetComponent<Image>().sprite = grade[PlayerPrefs.GetInt(song.songName + "Grade")];
            

        } 
        else
        {
            highScoreText.text = "???";
            centerGrade.SetActive(false);
        }

        // playe button hilang ketika comingsoon
        if(song.songName == "ComingSoon")
        {
            playButton.SetActive(false);
            centerGrade.SetActive(false);
        } 
        else
        {
            playButton.SetActive(true);
        }

        GetComponent<Animator>().SetTrigger("in");
    }
}
