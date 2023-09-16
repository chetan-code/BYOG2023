using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private Transform platformParent;
    [SerializeField] private Platform[] platformPrefabs;
    [SerializeField] private float size = 25;
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float count = 5;
    [SerializeField] private float zThreshold = -25;
    [SerializeField] private Vector3 offset = Vector3.zero;
    private List<Transform> platforms = new();

    // Start is called before the first frame update
    void Start()
    {
        InitPool();
    }



    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.currentGameState != GameState.GAMEPLAY)
        {
            return;
        }
    }

    public void MovePlatform(float deltaTime) {
        for (int i = 0; i < platforms.Count; i++)
        {
            if (platforms[i].position.z < zThreshold) {
                RemovePlatform(platforms[i]);
            }
            if (platforms[i] == null) { continue; }
            Vector3 newPos = platforms[i].position - (Vector3.forward * moveSpeed * deltaTime);
            platforms[i].position = newPos;
        }
    }


    private void InitPool() {
        for (int i = 0; i < count; i++) {
            Platform platform = Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Length)]);
            platform.transform.position = (Vector3.forward * size * i) + offset;
            platform.transform.parent = platformParent;
            platforms.Add(platform.transform);
        }
    }

    private void RemovePlatform(Transform platform) {
        platforms.Remove(platform);
        Destroy(platform.gameObject);
        AddNewPlatform();
    }

    private void AddNewPlatform() {
        Platform platform = Instantiate(platformPrefabs[Random.Range(0, platformPrefabs.Length)]);
        platform.transform.position = platforms[platforms.Count - 1].position + (Vector3.forward * size);
        platform.transform.parent = platformParent;
        platforms.Add(platform.transform);
    }
}
