using System.Collections.Generic;
using Source.ActorSupports;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Services
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private Transform startUi;
        [SerializeField] private Transform gameUi;
        [SerializeField] private Transform settingsUi;
        [SerializeField] private Transform loseUi;
        
        [SerializeField] private Image healthIcon;
        [SerializeField] private Transform healthAnchor;
        [SerializeField] private Text currentScoreText;
        [SerializeField] private Text maxScoreTextInStart;
        [SerializeField] private Text maxScoreTextInGame;
        [SerializeField] private Text currentRoundText;
        [SerializeField] private Text turnMusicText;
        [SerializeField] private Text turnEffectsText;

        private List<Transform> healths = new List<Transform>();

        private void Update()
        {
            SetUiTexts();
            CheckSettingsButtons();
            HighScore.CheckNewScore();
        }

        private void SetUiTexts()
        {
            currentScoreText.text = $"Score: {HighScore.CurrentScore}";
            maxScoreTextInGame.text = $"HighScore: {HighScore.MaxScore}";
            maxScoreTextInStart.text = $"HighScore: {HighScore.MaxScore}";
        }

        public void StartLoseScreen()
        {
            loseUi.gameObject.SetActive(true);
            HighScore.SavePoints();
        }
        
        public void AddHealth()
        {
            var health = Instantiate(healthIcon, healthAnchor, true);
            healths.Add(health.transform);
            var padding = 120 + (healths.Count - 1) * 30; //todo
            var position = new Vector2(padding, 0);
                
            healths[healths.Count - 1].localPosition = position;
        }

        public void RemoveHealth()
        {
            if (healths.Count < 1) return;
            
            Destroy(healths[healths.Count - 1].gameObject);
            healths.Remove(healths[healths.Count - 1]);
        }

        public void SetCurrentRound(int round)
        {
            currentRoundText.text = $"Round: {round}";
        }
        
        private void CheckSettingsButtons()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                
                TurnSettingsMenu();
            }
        }

        public void TurnSettingsMenu()
        {
            SoundsComponent.Sounds.PlayClickSound();
            
            var isActive = settingsUi.gameObject.activeInHierarchy;

            if (isActive)
                Time.timeScale = 1;
            else
                Time.timeScale = 0;

            settingsUi.gameObject.SetActive(!isActive);
        }
        
        public void QuiteGame()
        {
            SoundsComponent.Sounds.PlayClickSound();
            Application.Quit();
        }
        
        public void TurnMusic(bool isPlayMusic)
        {
            SoundsComponent.Sounds.PlayClickSound();
            SoundsComponent.Sounds.PlayMainThemeMusic(!isPlayMusic);

            if (isPlayMusic)
                turnMusicText.text = "Music on";
            else
                turnMusicText.text = "Music off";
        }

        public void TurnEffects(bool isPlayEffects)
        {
            SoundsComponent.Sounds.PlayClickSound();
            SoundsComponent.Sounds.PlayEffectsSound(isPlayEffects);

            if (isPlayEffects)
                turnEffectsText.text = "Effects on";
            else
                turnEffectsText.text = "Effects off";
        }

        public void StartGameScreen(int startNumHealth)
        {
            HighScore.CurrentScore = 0;
            
            for (int i = 0; i < startNumHealth; i++)
            {
                AddHealth();
            }
        }
        
        public void ResetScore()
        {
            SoundsComponent.Sounds.PlayClickSound();
            HighScore.ResetHighScore();
        }
    }
}