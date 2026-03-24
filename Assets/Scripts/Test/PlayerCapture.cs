using UnityEngine;
using System.Collections;

public class PlayerCapture : MonoBehaviour
{
    public GameObject captureAreaPrefab;
    public Transform spawnPoint;
    public KeyCode captureKey = KeyCode.E;

    public Animator animator;

    public float captureDelay = 0.3f; 

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
        yield return new WaitForSeconds(captureDelay);

        Instantiate(
            captureAreaPrefab,
            spawnPoint.position,
            Quaternion.identity
        );
    }
}