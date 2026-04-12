using System;
using System.Collections;
using UnityEngine;

public class PlayerCapture : MonoBehaviour
{
    public GameObject captureAreaPrefab;
    public Transform spawnPoint;
    public KeyCode captureKey = KeyCode.E;

    public Animator animator;
    public int captureDelay = 10;

    void Update()
    {
        if (Input.GetKeyDown(captureKey))
        {
            StartCoroutine(CaptureRoutine());
        }
    }

    IEnumerator CaptureRoutine()
    {

        animator.SetTrigger("Murro");
        yield return new WaitForSeconds(2);

        Instantiate(captureAreaPrefab, spawnPoint.position, Quaternion.identity);
    }
}