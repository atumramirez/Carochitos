using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine.InputSystem;
public class WaterDash : MonoBehaviour
{
    public float fireRate = 0.2f;
    private float nextFireTime;

    public bool canBuff;
    public float buff;
    private float buffTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= nextFireTime && canBuff)
        {
            StartCoroutine(Buff());
            nextFireTime = Time.time + fireRate;
        }
    }

    public IEnumerator Buff()
    {
        if (transform.TryGetComponent<MonsterController>(out var car))
        {
            canBuff = false;

            float ogspeed = car.playerSpeed;
            car.playerSpeed *= buff;

            yield return new WaitForSeconds(buffTime);

            car.playerSpeed = ogspeed;

            yield return new WaitForSeconds(nextFireTime);

            canBuff = true;
        }
    }
}
