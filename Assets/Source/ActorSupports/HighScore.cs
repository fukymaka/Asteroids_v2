﻿using Asteroids.Source;
using Source.Interfaces;
using UnityEngine;

namespace Source.ActorSupports
{
    public class HighScore : MonoBehaviour
    {
        public static int CurrentScore { get; set; }
        public static int MaxScore { get; private set; }

        public  static void LoadHighScore()
        {
            if (PlayerPrefs.HasKey("HighScore"))
                MaxScore = PlayerPrefs.GetInt("HighScore");
            else
                PlayerPrefs.SetInt("HighScore", MaxScore);
        }

        public static void CheckNewScore()
        {
            if (CurrentScore > MaxScore)
                MaxScore = CurrentScore;
        }

        public static void AddPoints(IMovableObject enemy)
        {
            switch (enemy.Type)
            {
                case PossibleCollisions.Asteroid:
                    // CurrentScore += 10 + 10 * (int) enemy.Generation;
                    break;
                case PossibleCollisions.Ufo:
                    // CurrentScore += 50 + 50 * (int) enemy.Generation;
                    break;
            }
        }

        public static void SavePoints()
        {
            PlayerPrefs.SetInt("HighScore", MaxScore);
        }

        public static void ResetHighScore()
        {
            PlayerPrefs.SetInt("HighScore", 0);
            MaxScore = PlayerPrefs.GetInt("HighScore");
        }
    }
}