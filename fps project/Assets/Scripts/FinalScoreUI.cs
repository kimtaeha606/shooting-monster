using UnityEngine;
using UnityEngine.UI;

public class FinalScoreUI : MonoBehaviour
{
    [SerializeField] Text fianltimeText;

    private void Awake()
    {
        int totalSeconds = Mathf.FloorToInt(RunResult.LastSurvivalTime);

        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        fianltimeText.text = $"{minutes:00}:{seconds:00}";

    }
}
