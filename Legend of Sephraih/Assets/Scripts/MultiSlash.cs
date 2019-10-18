﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSlash : MonoBehaviour
{
    public int damage;
    public float startDelay;
    private float delay;
    private int maxCombo = 5;
    private int comboCount = 1;
    private float comboDelay = 0.1f;


    public LayerMask whatIsEnemy;

    private Transform attackPos;
    private float attackRangeX = 2.5f;
    private float attackRangeY = 1.5f;

    private GameObject slashEffect;



    private void Start()
    {
        attackPos = transform.GetChild(0);
        slashEffect = Resources.Load("Prefabs/ParticleSlashPrefab") as GameObject;
    }
    void Update()
    {
        if (delay >= 0)
        {
            delay -= Time.deltaTime;
        }
    }

    public void Attack()
    {
        if (delay <= 0)
        {
            // instantiate slash prefab
            GameObject slash = Instantiate(slashEffect, transform.position + attackPos.localPosition, Quaternion.identity);

            //get particle system to set it's color
            var a = slash.GetComponent<ParticleSystem>().main;

            //set color depending on combo
            if (comboCount == 2)
            {
                a.startColor = Color.cyan;

            }
            if (comboCount == 3)
            {
                a.startColor = Color.blue;

            }
            if (comboCount == 4)
            {
                a.startColor = Color.magenta;

            }
            if (comboCount == 5)
            {
                a.startColor = Color.red;

            }

            //effect
            slash.transform.parent = transform;
            slash.transform.Rotate(Mathf.Atan2(attackPos.localPosition.x, attackPos.localPosition.y) * Mathf.Rad2Deg, +90, 0);
            Destroy(slash, 0.2f);

            //determine damaged enemies, apply damage
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position, new Vector2(attackRangeX, attackRangeY), attackPos.localPosition.x * 90, whatIsEnemy);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<HealthController>().TakeDamage(damage * comboCount);
            }
            comboCount++;
            delay = comboDelay;

            if (comboCount > maxCombo)
            {
                delay = startDelay;
                comboCount = 1;
            }

        }
    }

}



/*void OnDrawGizmosSelected(){
      Gizmos.color = Color.red;
      Gizmos.DrawWireCube(attackPos.position, new Vector3(attackRangeX,attackRangeY,1));

  }*/

/*
  
                float x = Mathf.Pow(2,comboCount-1);
                int z = Mathf.RoundToInt(x);
*/
