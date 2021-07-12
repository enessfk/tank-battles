using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : Sense
{
    string TankName="unknown";
    public override void InitializeSense()
    {
        
    }

    public override void UpdateSense()
    {
        if (TankName.Equals("playerTank"))
        {
            Debug.Log("Player has been detected");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        TankName = other.name;
    }
    private void OnTriggerExit(Collider other)
    {
        TankName = "unknown";
    }

}
