using UnityEngine;
using UnityEngine.UI;
public class TimerUI : MonoBehaviour
{
    [SerializeField] private Text timeText;

    private void OnEnable()
    {
        GameSignals.SurvivalTimeUpdated += HandleTimeUpdated;
    }

    private void OnDisable()
    {
        GameSignals.SurvivalTimeUpdated -= HandleTimeUpdated;
    }

    private void HandleTimeUpdated(float time)
    {
        int totalSeconds = Mathf.FloorToInt(time);

        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        timeText.text = $"{minutes:00}:{seconds:00}";
    }
}
