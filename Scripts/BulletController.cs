using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controlls bullet behaviour

public class BulletController : MonoBehaviour
{
    #region Fields and Properties
    public float BulletSpeed = 25;
    //Emits vibration for the Cinemachine Camera
    private Cinemachine.CinemachineImpulseSource impulseSource;
    #endregion

    #region Execution
    //On Trigger will cause a random damage to the collider object and calls blood vfx.

    private void Update() 
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.MovePosition(transform.position + transform.forward * BulletSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider col)
    {
        PlayerStatus ps = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
        Quaternion oppositeRotation = Quaternion.LookRotation(-transform.forward);
        switch (col.tag)
        {
            case "Enemy":
                ZombieController zc = col.GetComponent<ZombieController>();
                
                ps.damage = Random.Range(ps.minDamage, ps.maxDamage);
                
                zc.CallBloodVFX(transform.position, oppositeRotation);
                zc.ReceiveDamage(ps.damage);
                break;
            case "Boss":
                BossController bc = col.GetComponent<BossController>();

                ps.damage = Random.Range(ps.minDamage, ps.maxDamage);
                
                bc.CallBloodVFX(transform.position, oppositeRotation);
                bc.ReceiveDamage(ps.damage);
                break;
        }
        

        Destroy(gameObject);
    }
    #endregion

    #region Methods
    //Adds impulse to the bullet and generates bullet shake on camera
    public void Shoot()
    {
        //GameObject reticle = GameObject.FindWithTag("Reticle");

        GetComponent<Rigidbody>().AddForce(transform.forward * (BulletSpeed), ForceMode.Impulse);
        impulseSource = GetComponent<Cinemachine.CinemachineImpulseSource>();

        impulseSource.GenerateImpulse(-Camera.main.transform.forward);
    }
    #endregion
}
