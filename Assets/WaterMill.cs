﻿using UnityEngine;
using System.Collections;

public class WaterMill : MonoBehaviour 
{
	public float Strength = 100f;
	public float distanceS = 10f;	
	public int Direction = 1;

	private Transform trans;
	private Rigidbody thisRd;
	private Transform playerTrans;
    private Vector3 previous;
	private bool InZone;
    private int dir = 1;

    void Awake()
	{
		trans = transform;
		thisRd = trans.GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
        thisRd.AddTorque(new Vector3(0,0,0.1f*dir));
        if (InZone)
		{

            Vector3 directionTo = playerTrans.position - trans.position;
			float distance = Vector3.Distance(playerTrans.position, trans.position);
            int height = -1;
            if (trans.position.y > playerTrans.position.y)
            {
                height = 1;
            }
            if (((((playerTrans.position - previous).x) / Time.deltaTime) / distance) < 0)
            {
                dir = 1;
            }
            else if (((((playerTrans.position - previous).x) / Time.deltaTime) / distance) > 0)
            {
                dir = -1;
            }
            float DistanceStr = (distanceS / distance) * Strength;
            thisRd.AddTorque(DistanceStr * (directionTo * Direction) * ((((playerTrans.position - previous).x) / Time.deltaTime) / distance * height), ForceMode.Force);
            //trans.transform.Rotate(new Vector3(0, 0, 1.5f));
            previous = playerTrans.position;
        }
        
    }

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
		{
			playerTrans = other.transform;
			InZone = true;
		}
	}

	void OnTriggerExit (Collider other)
	{
	//	if (other.tag == "Magnet" && looseMagnet)
//		{
	//		magnetInZone = false;
		//}
	}
}






