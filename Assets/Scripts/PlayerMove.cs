﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 floor;
    [SerializeField] private float Grav;
    [SerializeField] private float Thrust;
    [SerializeField] private float Frottement;
    [SerializeField] private float Mass = 1.0f;

    [SerializeField] private GameObject PlayerSprite = null;

    private Vector2 Force;
    private bool isInFloor = false;
    private Vector2 Velocity;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerSprite.transform.Rotate(new Vector3(0,0,-Velocity.magnitude*Time.deltaTime*50));
    }

    public void Compute(Vector2 floorPoint, Vector2 floorPrime)
    {
        Force = new Vector2(0,0);
        Vector2 TanVec = new Vector2(1,floorPrime.y);
        TanVec.Normalize();
        Vector2 NormalVec = new Vector2(-floorPrime.y,1);
        NormalVec.Normalize();
        Force += new Vector2(0,  -Grav);


        Vector2 Fro = new Vector2(0,0);
        Vector2 Thru = new Vector2(0,0);
        Vector2 nPos = new Vector2(transform.position.x, transform.position.y);
        if (nPos.y +(Velocity.y * Time.deltaTime) - 0.1f < floorPoint.y )
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Thru = Thrust * TanVec;
                Force += Thru;
            }
            Force += -NormalVec * Vector2.Dot(NormalVec, Velocity);
            float t = Mass * Grav;
            t *= Frottement;
            float d = -Vector2.Dot(TanVec, Velocity);
            Fro = TanVec * Mathf.Clamp(d, -t, t);
            Force +=  Fro;

            if(Velocity.y <= 0.0f)
            {
                if(Vector2.Dot(Velocity.normalized, TanVec) >= 0.8f)
                    Velocity = TanVec * Velocity.magnitude * 0.995f;
                //else
                {
                    //Velocity = TanVec * Time.deltaTime;
                }
                
            }
            if(Vector2.Dot(Velocity.normalized, TanVec) <= 0.8f)
                Velocity = TanVec * Velocity.magnitude * (1.0f-Frottement);
            




        }

        {
            float Dist = transform.position.y - floorPoint.y;
            if (Dist <= 0.0f)
                Dist = 0.0001f;
            GetComponentInChildren<Camera>().orthographicSize = 10.0f + Mathf.Sqrt(Dist);
        }
        
        Vector2 accel = Force / Mass;
        Velocity += accel * Time.deltaTime;
        nPos += Velocity* Time.deltaTime;
        if (nPos.y < floorPoint.y)
            nPos.y = floorPoint.y;
        Vector2 End = new Vector2(transform.position.x + Force.x, transform.position.y + Force.y);
        Debug.DrawLine(transform.position, End, Color.red);
        End = new Vector2(transform.position.x + Velocity.x, transform.position.y + Velocity.y);

        Debug.DrawLine(transform.position, End, Color.blue);
        
        End = new Vector2(transform.position.x + Thru.x, transform.position.y + Thru.y);
        Debug.DrawLine(transform.position, End, Color.yellow);

        
        transform.position = nPos;
        floor = floorPoint;
    }
}
