﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusController : MonoBehaviour
{

    //Stats
    public int atk; //melee attack stat
    public int matk; //magical attack stat
    public int aspd; // attack speed stat


    public float mvspd; // current movement speed
    public float dmvspd; // default movement speed for an object, set in inspector

    public int crit; //critical strike chance

    private int slows = 0; // counter to reset movement speed only when no slows are applied

    //Status Effects
    public GameObject burnEffect;


    public void Start()
    {
        mvspd = dmvspd;
    }
    public void Update()
    {

    }

    public void Burn(float totalDmg, float time, float slowAmt)
    {

        StartCoroutine(DoTCoroutine(totalDmg, time));
        StartCoroutine(SlowCoroutine(slowAmt, time));
    }

    IEnumerator DoTCoroutine(float dmg, float time)
    {
        float amountDamaged = 0;
        GameObject burn = Instantiate(burnEffect, transform.position, Quaternion.Euler(-90f, 0f,0f));
        burn.transform.parent = transform;

        Destroy(burn, time);
        while (amountDamaged < dmg)
        {
            // apply dmg dmg/time
            this.GetComponent<HealthController>().TakeDamage((int)(dmg / time));
            amountDamaged += dmg / time;
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator SlowCoroutine(float slow, float time)
    {
        slows++;
        float count = 0.0f;
        while (count < time)
        {
            mvspd = slow * dmvspd;
            count += 0.1f;
            yield return new WaitForSeconds(0.1f);
        }
        slows --;
        if(slows==0)mvspd = dmvspd;


    }






}

