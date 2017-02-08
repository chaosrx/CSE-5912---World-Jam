﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SlideMulti2 : NetworkBehaviour
{

    private Rigidbody rb;
    public PlayerController Player;
    private CapsuleCollider col;
    public Transform Scaler;
    private float DefaultSize;
    public float SlideDampenFactor;
    public float SlideDampenAmount;
    public bool IsSliding;



    private SoundPlayer SFXPlayer;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        Scaler = GetComponent<Transform>();
        Player = GetComponent<PlayerController>();
        SFXPlayer = GetComponent<SoundPlayer>();

        DefaultSize = col.height;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
            return;

        if (IsGrounded())
        {

            if (Input.GetButton("Fire2"))
            {
                SlideDampenAmount = SlideDampenFactor;
                IsSliding = true;

                col.height = 0.1f;
                GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity * SlideDampenFactor;


            }
            else
            {
                SlideDampenAmount = 1.0f;
                IsSliding = false;

                col.height = DefaultSize;
                //Scaler.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
                //GetComponent<CapsuleCollider>().height = 1.0f;

            }
        }
    }

    public bool IsGrounded()
    {
        RaycastHit hit;
        float distance = 2f;
        Vector3 dir = new Vector3(0, -1);

        if (Physics.Raycast(transform.position, dir, out hit, distance))
        {
            return true;

        }
        else
        {
            return false;
        }
    }


}
