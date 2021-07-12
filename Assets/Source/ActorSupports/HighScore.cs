using Source.EnemySource;
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

        public static void AddAsteroidPoints(AsteroidActor asteroid)
        {
            CurrentScore += 10 + 10 * (int) asteroid.AsteroidGeneration;
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