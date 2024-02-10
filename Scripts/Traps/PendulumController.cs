using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumController : MonoBehaviour
{
    private List<FixedJoystick> joysticks = new List<FixedJoystick>();

    private void Start() 
    {
        var joy = FindObjectsOfType<FixedJoystick>();
        foreach (FixedJoystick fj in joy)
        {
            joysticks.Add(fj);
        }
    }
    private void OnTriggerEnter(Collider col) 
    {
        if (col.tag == "Player")
        {
            foreach (FixedJoystick fj in joysticks)
            {
                fj.gameObject.SetActive(false);
            }
            PlayerController playerController = col.GetComponent<PlayerController>();
            PlayerStatus playerStatus = col.GetComponent<PlayerStatus>();
            playerController.ReceiveDamage((int)playerStatus.maxLife);
        }
    }
}
