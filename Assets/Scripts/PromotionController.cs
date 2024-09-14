using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromotionController : MonoBehaviour
{
    [SerializeField] private GameObject whitePromotionPanel;
    [SerializeField] private GameObject blackPromotionPanel;

    private GameObject activePanel;
    IPromotable promotable;

    public void StartPromotion(Chessman promotableChessman, TeamColor teamColor)
    {
        promotable = promotableChessman.GetComponent<IPromotable>();

        if(promotable == null)
        {
            return;
        }    

        if(teamColor == TeamColor.white)
        {
            whitePromotionPanel.SetActive(true);
            activePanel = whitePromotionPanel;
        }
        else
        {
            blackPromotionPanel.SetActive(true);
            activePanel = blackPromotionPanel;
        }
    }

    public void MakePromotionWhite(ChessmanSetting setting)
    {
        promotable.Promotion(setting);
        activePanel.SetActive(false);
    }
}
