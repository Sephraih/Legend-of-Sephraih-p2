﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class positioning : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.localPosition += new Vector3(0, 1, 0) * Time.deltaTime;
    }
}
