using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class handsScript : MonoBehaviour
{
    public float distTopPickup = 0.3f;
    bool handClosed = false;
    public LayerMask pickUpLayer;

    public SteamVR_Input_Sources handSource = SteamVR_Input_Sources.LeftHand;

    Rigidbody holdingTarget;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (SteamVR_Actions.shooter_Grab.GetState(handSource))
        {
            handClosed = true;
        }
        else
        {
            handClosed = false;
        }
        if (!handClosed)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, distToPickup, pickUpLayer);
            if(colliders.Length > 0)
            {
                holdingTarget = colliders[0].transform.root.GetComponent<Rigidbody>();
            }
            else
            {
                holdingTarget = null;
            }
        }
        else
        {
            if (holdingTarget)
            {
                holdingTarget.velocity = (transform.position - holdingTarget.transform.position) / Time.fixedDeltaTime;

                holdingTarget.maxAngularVelocity = 20;
                Quarternion deltaRot = transform.rotation * Quarternion.Inverse(holdingTarget.transform.rotation);
                Vector3 eulerRot = new Vector3(Mathf.DeltaAngle(0, deltaRot.eulerAngles.x), Mathf.deltaAngle(0, deltaRot.eulerAngles.y), Mathf.deltaAngle(0, deltaRot.eulerAngles.z));
                eulerRot *= 0.95f;
                eulerRot *= Mathf.Deg2Rad;
                holdingTarget.angularVelocity = eulerRot / Time.fixedDeltaTime;
            }
        }
    }
}
