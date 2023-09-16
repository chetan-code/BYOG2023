using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject startText;
    [SerializeField] private PlatformController platformController;
    [SerializeField] private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.currentGameState == GameState.MENU) {
            if (Input.GetKeyDown(KeyCode.Space)) { 
                GameManager.Instance.ChangeState(GameState.GAMEPLAY);
                startText.SetActive(false);
            }
        }

        if (GameManager.Instance.currentGameState == GameState.GAMEPLAY)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (playerController.isJumping) { return; }
                platformController.MovePlatform(Time.deltaTime);
                playerController.Run(1);
            }
            else { 
                playerController.Run(0);
            }
        }
    }
}
