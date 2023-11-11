using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum eScoreEvent
{
    monte,
    mina,
    minaOutro,
    gameVitoria,
    gameDerrota
}


public class ScoreManager : MonoBehaviour
{
    public GameObject victory;
    public GameObject defeat;
    static private ScoreManager S;
    private AudioSource audio;

    static public int SCORE_DA_PARTIDA_ANTERIOR = 0;
    static public int HIGH_SCORE = 0;
    private string filePath = "leaderboard.txt";

    [Header("Set Dynamically")]
    public int serie = 0;
    public int scoreRodada = 0;
    public int score = 0;

    TMP_Text scoreText;

    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("ScoreManager.Awake(): S já existe!");
        }
        if (PlayerPrefs.HasKey("GarimpeiroHighScore"))
        {
            HIGH_SCORE = PlayerPrefs.GetInt("GarimpeiroHighScore");
        }
        score += SCORE_DA_PARTIDA_ANTERIOR;
        SCORE_DA_PARTIDA_ANTERIOR = 0;
        S.scoreText = GetComponent<TMP_Text>();
        audio = GetComponent<AudioSource>();

        victory.SetActive(false);
        defeat.SetActive(false);
    }

    private void Start()
    {
        S.scoreText.text = "Inicio, Record Atual = " + HIGH_SCORE;
    }

    static public void EVENT(eScoreEvent evt)
    {
        try
        {
            S.Event(evt);
        }catch(System.NullReferenceException nre)
        {
            Debug.LogError("ScoreManager:EVENT() Chamado enquanto S = null.\n" + nre);
        }
    }

    void Event(eScoreEvent evt)
    {
        switch (evt)
        {
            case eScoreEvent.monte:
            case eScoreEvent.gameVitoria:
            case eScoreEvent.gameDerrota:
                serie = 0;
                score += scoreRodada;
                scoreRodada = 0;
                break;
            case eScoreEvent.mina:
                audio.Play();
                serie++;
                scoreRodada += serie;
                break;
        }

        switch (evt)
        {
            case eScoreEvent.gameVitoria:
                SCORE_DA_PARTIDA_ANTERIOR = score;
                if(SCORE_DA_PARTIDA_ANTERIOR > HIGH_SCORE)
                {
                    HIGH_SCORE = SCORE_DA_PARTIDA_ANTERIOR;
                    PlayerPrefs.SetInt("GarimpeiroHighScore", score);
                }
                print("VITÓRIA! Pontos desta Partida: " + score);

                filePath = Path.Combine(Application.dataPath, "Data/leaderboard.txt");

                // Abrindo o arquivo em modo de adição e adicionadno novo jogador com pontuação
                using (StreamWriter writer = File.AppendText(filePath))
                {
                    writer.WriteLine("\n" + score);
                }
                victory.SetActive(true);
                break;

            case eScoreEvent.gameDerrota:
                if(HIGH_SCORE <= score)
                {
                    print("Você teve uma pontuação alta! High score: " + score);
                }
                else
                {
                    print("Sua pontuação no game foi: " + score);
                }
                filePath = Path.Combine(Application.dataPath, "Data/leaderboard.txt");

                // Abrindo o arquivo em modo de adição e adicionadno novo jogador com pontuação
                using (StreamWriter writer = File.AppendText(filePath))
                {
                    writer.WriteLine("\n"+score);
                }
                defeat.SetActive(true);
                break;
            default:
                scoreText.text = "Total: " + score.ToString() +
                                    " da rodada:" + scoreRodada.ToString() +
                                    ", série: " + serie.ToString();
                break;


        }
    }

}
