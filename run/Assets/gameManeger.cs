using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class gameManeger : MonoBehaviour
{
    //Oyuncunun ba�lama noktas�
    public Transform startingPoint;

    //Lerp h�z�
    public float lerpSpeed;

    //Oyuncunun y�ksek skoru
    public float highScore = 0;

    //Oyuncunun anl�k skoru
    public float score = 0;

    //Skoru g�steren metin nesneleri
    public TextMeshProUGUI TotalScoreText;
    public TextMeshProUGUI HighScoreText;

    PlayerController playerControllerScript;

    private void Start()
    {
        //Y�ksek skoru oyuncu tercihlerinden al
        highScore = PlayerPrefs.GetFloat("EnYuksekSkorum", 0);

        //Skoru ve y�ksek skoru ekranda g�ster
        TotalScoreText.text = "SCORE: " + score;
        HighScoreText.text = "H�GHSCORE: " + highScore;

        //PlayerController al ve oyunu ba�lat
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        playerControllerScript.gameOver = false;
        StartCoroutine(PlayerIntro());
    }

    private void Update()
    {
        //Y�ksek skoru g�ncelle
        HighScoreText.text = "H�GHSCORE: " + highScore.ToString();
    }

    IEnumerator PlayerIntro()
    {
        //Intro animasyonu i�in ba�lang�� ve biti� pozisyonlar�
        Vector3 startPos = playerControllerScript.transform.position;
        Vector3 endPos = startingPoint.position;

        //Intro animasyonunun uzunlu�u
        float journeyLength = Vector3.Distance(startPos, endPos);

        //Intro animasyonunun ba�lang�c zaman�
        float startTime = Time.time;

        //Intro animasyonunun ne kadar s�rede tamamland���
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

        //Intro sona erdi�inde oyunu devam ettir
        playerControllerScript.gameOver = false;
    }

    //Skor eklemek i�in metod
    public void AddScore(int value)
    {
        //Skoru g�ncelle ve y�ksek skoru kontrol et
        score += value;
        TotalScoreText.text = "SCORE: " + score;

        if (score >= highScore)
        {
            highScore = score;
            HighScoreText.text = "H�GHSCORE: " + highScore;

            //yeni y�ksek skoru dataya kaydet
            PlayerPrefs.SetFloat("EnYuksekSkorum", highScore);
        }
        else
        {
            HighScoreText.text = "H�GHSCORE: " + highScore;
        }
    }
}