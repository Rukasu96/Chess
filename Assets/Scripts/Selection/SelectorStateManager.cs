using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SelectorStateManager : MonoBehaviour
{
    [SerializeField] private BoardManager boardManager;
    [SerializeField] private TurnManager turnManager;
    [SerializeField] private MoveManager moveManager;

    private IHoveredSelector hoveredSelector;

    private SelectorBaseState currentState;
    private Dictionary<Type, SelectorBaseState> typeByState;

    private void Awake()
    {
        hoveredSelector = GetComponent<IHoveredSelector>();

        typeByState = new Dictionary<Type, SelectorBaseState>
        {
            { typeof(SelectorChooseChessmanState), new SelectorChooseChessmanState(boardManager, hoveredSelector) },
            { typeof(SelectorChooseTileState), new SelectorChooseTileState(boardManager, hoveredSelector) },
            { typeof(SelectorChangingPlayerState), new SelectorChangingPlayerState(boardManager, hoveredSelector) }
        };

        foreach (var state in typeByState.Values)
        {
            state.OnSelectChessman += State_OnSelectChessman;
            state.OnSelectTile += State_OnSelectTile;
            state.OnBackToDefaultState += State_BackToDefault;
        }
    }

    private void State_OnSelectChessman(Chessman selectedChessman)
    {
        moveManager.SetCurrentChessman(selectedChessman);
    }

    private void State_OnSelectTile(Tile selectedTile)
    {
        moveManager.MoveChessman(selectedTile);
        turnManager.UpdatePlayer();
    }

    private void State_BackToDefault()
    {
        moveManager.BackToDefault();
    }

    private void Start()
    {
        SwitchState<SelectorChooseChessmanState>();
    }

    private void Update()
    {
        if (currentState == null)
        {
            return;
        }

        var stateType = currentState.UpdateState();
        if (stateType == null)
        {
            return;
        }

        SwitchState(stateType);
    }

    public void SwitchState(SelectorBaseState state)
    {
        if(currentState == state) 
            return;
        currentState = state;
        currentState.EnterState();
    }

    private void SwitchState(Type stateType)
    {
        if (typeByState.TryGetValue(stateType, out var selectorBaseState))
        {
            SwitchState(selectorBaseState);
        }
        else
        {
            Debug.LogError("State type doesn't exist" + stateType.Name);
        }
    }

    private void SwitchState<T>() where T : SelectorBaseState
    {
        var stateType = typeof(T);
        SwitchState(stateType);
    }
}
