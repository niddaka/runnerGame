using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class gameManeger : MonoBehaviour
{
    //Oyuncunun baþlama noktasý
    public Transform startingPoint;

    //Lerp hýzý
    public float lerpSpeed;

    //Oyuncunun yüksek skoru
    public float highScore = 0;

    //Oyuncunun anlýk skoru
    public float score = 0;

    //Skoru gösteren metin nesneleri
    public TextMeshProUGUI TotalScoreText;
    public TextMeshProUGUI HighScoreText;

    PlayerController playerControllerScript;

    private void Start()
    {
        //Yüksek skoru oyuncu tercihlerinden al
        highScore = PlayerPrefs.GetFloat("EnYuksekSkorum", 0);

        //Skoru ve yüksek skoru ekranda göster
        TotalScoreText.text = "SCORE: " + score;
        HighScoreText.text = "HÝGHSCORE: " + highScore;

        //PlayerController al ve oyunu baþlat
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        playerControllerScript.gameOver = false;
        StartCoroutine(PlayerIntro());
    }

    private void Update()
    {
        //Yüksek skoru güncelle
        HighScoreText.text = "HÝGHSCORE: " + highScore.ToString();
    }

    IEnumerator PlayerIntro()
    {
        //Intro animasyonu için baþlangýç ve bitiþ pozisyonlarý
        Vector3 startPos = playerControllerScript.transform.position;
        Vector3 endPos = startingPoint.position;

        //Intro animasyonunun uzunluðu
        float journeyLength = Vector3.Distance(startPos, endPos);

        //Intro animasyonunun baþlangýc zamaný
        float startTime = Time.time;

        //Intro animasyonunun ne kadar sürede tamamlandýðý
        float distanceCov = (Time.time - startTime) / lerpSpeed;
        float fractionJourney = distanceCov / journeyLength;

        //Intro animasyonunu lerp ile oynat
        while (fractionJourney < 1)
        {
            distanceCov = (Time.time - startTime) * lerpSpeed;
            fractionJourney= distanceCov / journeyLength;
            playerControllerScript.transform.position = Vector3.Lerp(startPos, endPos, fractionJourney);
            yield return null;
        }

        //Intro sona erdiðinde oyunu devam ettir
        playerControllerScript.gameOver = false;
    }

    //Skor eklemek için metod
    public void AddScore(int value)
    {
        //Skoru güncelle ve yüksek skoru kontrol et
        score += value;
        TotalScoreText.text = "SCORE: " + score;

        if (score >= highScore)
        {
            highScore = score;
            HighScoreText.text = "HÝGHSCORE: " + highScore;

            //yeni yüksek skoru dataya kaydet
            PlayerPrefs.SetFloat("EnYuksekSkorum", highScore);
        }
        else
        {
            HighScoreText.text = "HÝGHSCORE: " + highScore;
        }
    }
}