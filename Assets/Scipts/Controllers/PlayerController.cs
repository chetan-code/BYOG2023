using DG.Tweening;
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
    [SerializeField] private PlatformController platformController;
    [SerializeField] private float jumpExtraHeight = 2f;
    [SerializeField] private float baseObstacleHeight = 1;
    [SerializeField] private Gun activeGun;
    private Camera mainCam;

    private float jumpDuration = 1f;
    public bool isJumping = false;
    private RaycastHit hit;
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

        if (isJumping) {
            return;
        }

        Aim(Input.mousePosition);
        if (Input.GetMouseButtonDown(0)) {
            activeGun.Shoot();
            Debug.Log("Shoot");
        }
        if (Physics.Raycast(player.transform.position, player.transform.forward, out hit,raycastDist,obstacleLayer)) {
            BoxCollider boxCollider = hit.collider as BoxCollider;
            if (boxCollider != null)
            {
                float height = boxCollider.size.y * hit.transform.lossyScale.y;
                Jump(height);
            }
        }
        Debug.DrawLine(player.transform.position, player.transform.position + player.transform.forward * raycastDist, Color.green);
    }


    private void Aim(Vector3 mousePos) {
        float distanceFromCamera = 10f;
        Vector3 worldPosition = mainCam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, distanceFromCamera));
        Debug.DrawLine(gunCenter.position, worldPosition, Color.red);
        gunCenter.LookAt(worldPosition, Vector3.up);
    }

    private void Shoot() { 
    
    }

    private void Jump(float obstacleHeight) {
        StartCoroutine(StartTimer(obstacleHeight));
    }

    float remainingTime = 0;
    
    private IEnumerator StartTimer(float obstacleHeight)
    {
        isJumping = true;
        jumpDuration = (obstacleHeight/baseObstacleHeight) * jumpDuration;
        remainingTime = jumpDuration;
        player.transform.DOJump(player.transform.position,obstacleHeight + jumpExtraHeight, 1,jumpDuration);
        while (remainingTime > 0f)
        {
            remainingTime -= Time.deltaTime;

            platformController.MovePlatform(Time.deltaTime);

            yield return null;
        }
        jumpDuration = 1f;
        isJumping = false;
    }
}
