using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { 
    MENU,
    GAMEPLAY,
    LEVEL_END,
    GAMEOVER
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState currentGameState
    {
        get
        {
            return _currentGameState;
        }
    }
    private GameState _currentGameState;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _currentGameState = GameState.MENU; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeState(GameState newState) { 
        _currentGameState = newState;
    }


}
