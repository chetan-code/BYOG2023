using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class PlayerModel{
    //public float speed = 5;
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private PlayerModel data;
    [SerializeField] private Transform gunCenter;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float raycastDist = 2f;

    private Camera mainCam;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main; 
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.currentGameState != GameState.GAMEPLAY) {
            return;
        }


        Aim(Input.mousePosition);
        if (Physics.Raycast(player.transform.position, player.transform.forward, raycastDist, obstacleLayer)) {
            Debug.Log("Jump"); 
        }
        Debug.DrawLine(player.transform.position, player.transform.position + player.transform.forward * raycastDist, Color.green);
    }


    private void Aim(Vector3 mousePos) {
        float distanceFromCamera = 10f;
        Vector3 worldPosition = mainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, distanceFromCamera));
        Debug.DrawLine(gunCenter.position, worldPosition, Color.red);
        gunCenter.LookAt(worldPosition, Vector3.up);
    }

    private void Jump() { 
    }
}
