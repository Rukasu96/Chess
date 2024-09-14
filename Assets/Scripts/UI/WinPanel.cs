using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    [SerializeField] private GameObject winnerPanel;
    [SerializeField] private TMP_Text winnerTeamText;

    public void ActivePanel()
    {
        winnerPanel.SetActive(true);
    }

    public void SetWinnerTeamText(TeamColor winnerTeam)
    {
        if(winnerTeam == TeamColor.white)
        {
            winnerTeamText.text = "White";
            winnerTeamText.color = Color.white;
        }
        else
        {
            winnerTeamText.text = "Black";
            winnerTeamText.color = Color.black;
        }
    }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
