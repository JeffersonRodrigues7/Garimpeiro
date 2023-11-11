using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class SetRecord : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI first;
    [SerializeField] private TextMeshProUGUI second;
    [SerializeField] private TextMeshProUGUI third;

    private List<int> playerScores = new List<int>();

    private void Start()
    {
        // Define o caminho do arquivo de leaderboard
        string filePath = Path.Combine(Application.dataPath, "Data/leaderboard.txt");

        if (File.Exists(filePath))
        {
            // Lê todas as linhas do arquivo de leaderboard
            string[] rows = File.ReadAllLines(filePath);

            foreach (string row in rows)
            {
                try
                {
                    int _ponctuation = int.Parse(row);
                    playerScores.Add(_ponctuation);
                }
                catch (Exception e)
                {
                    continue;
                }

            }
        }

        playerScores.Sort();
        playerScores.Reverse();


        if(playerScores.Count > 0)
        {
            first.text = playerScores[0].ToString();
        }
        if (playerScores.Count > 1)
        {
            second.text = playerScores[1].ToString();
        }
        if (playerScores.Count > 2)
        {
            third.text = playerScores[2].ToString();
        }


    }
}
