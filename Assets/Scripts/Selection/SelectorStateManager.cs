using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectorStateManager : MonoBehaviour
{
    [SerializeField] private Board board;
    
    private IRayProvider rayProvider;
    private IHoveredSelector hoveredSelector;

    private SelectorBaseState currentState;
    public SelectorChooseChessmanState ChooseChessmanState = new SelectorChooseChessmanState();
    public SelectorChooseTileState ChooseTileState = new SelectorChooseTileState();
    public SelectorChangingPlayerState ChangingPlayerState = new SelectorChangingPlayerState();
    public Chessman SelectedChessman;

    public IRayProvider RayProvider => rayProvider;
    public IHoveredSelector HoveredSelector => hoveredSelector;
    public Board Board => board;
    private void Awake()
    {
        rayProvider = GetComponent<IRayProvider>();
        hoveredSelector = GetComponent<IHoveredSelector>();
    }
    private void Start()
    {
        currentState = ChooseChessmanState;
        currentState.EnterState(this);
    }
    private void Update()
    {
        currentState.UpdateState(this);
    }
    public void SwitchState(SelectorBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

}
