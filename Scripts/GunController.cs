using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Logic for the current gun and attacks

public class GunController : MonoBehaviour
{
    #region Fields and Properties
    //Bullet prefab and spawn
    public GameObject Bullet;
    public GameObject BulletSpawnMarker;
    //Sounds
    public AudioClip GunfireSound;
    public AudioClip EmptyGunSFX;
    public AudioClip ReloadingSFX;
    //UI
    public GUIController _gui;
    // FireRate, ReloadRate
    [SerializeField] private float GunFireRate = 0.5f;
    [SerializeField] public float ReloadRate = 2f;
    [SerializeField] private bool canShoot = true;
    private string actionLockGunCode_1 = "Shoot";
    private string actionLockGunCode_2 = "Reload";
    // KeyCodes
    private string atkKeyCode = "Attack";
    private string reloadKeyCode = "Reload";
    [SerializeField] private GameObject look;
    private CharacterAnimator animator;

    //New gun system (raycast)
    [HideInInspector]
    public int damage;
    [HideInInspector]
    public float range;
    [HideInInspector]
    public ParticleSystem muzzleFlash;
    //Getting screen center pos
    private Camera cam;
    [SerializeField]
    private LayerMask playerLayerMask;
    
    #endregion

    #region Initialization
    private void Start()
    {
        _gui.UpdateAmmoCount();
        animator = GetComponent<CharacterAnimator>();
        cam = Camera.main;
    }
    #endregion

    #region Execution
    private void Update()
    {
        Debug.DrawRay(cam.transform.position, cam.transform.forward * 1000, Color.red);
        if (EventSystem.current.IsPointerOverGameObject()) return;
    }
    #endregion

    #region Methods


    public void FireGun()
    {
        PlayerStatus playerStatus = GetComponent<PlayerStatus>();

        if (canShoot)
        {
            if (playerStatus.loadedAmmo > 0)
            {
                playerStatus.UpdateLoadedAmmo(-1);

                _gui.UpdateAmmoCount();

                animator.Shoot();

                Shoot();
            
                AudioController.instance.PlayOneShot(GunfireSound);

                StartCoroutine(LockGun(actionLockGunCode_1));
            }
            else if (playerStatus.loadedAmmo <= 0)
            {
                animator.Shoot();
                AudioController.instance.PlayOneShot(EmptyGunSFX);
            }
        }
    }

    public void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward * 1000, out hit, playerLayerMask))
        {
            if (hit.transform.tag == "Enemy")
            {
                HealthComponentZombie hc = hit.transform.GetComponent<HealthComponentZombie>();
                if (hc != null)
                {
                    hc.TakeDamage(damage);
                }
            }
        }
        muzzleFlash.Play();
    }

    public void ReloadGun()
    {
        PlayerStatus playerStatus = GetComponent<PlayerStatus>();
        bool canReload = playerStatus.reloadAmmo > 0 && playerStatus.loadedAmmo < playerStatus.maxAmmoOnChamber;

        if (canReload)
        {
            AudioController.instance.PlayOneShot(ReloadingSFX);

            playerStatus.maxAmmoChamberHelper = playerStatus.CurrentMaxAmmoChamber;
            
            if (playerStatus.reloadAmmo > playerStatus.maxAmmoChamberHelper)
            {
                playerStatus.UpdateLoadedAmmo(playerStatus.maxAmmoChamberHelper);
            }
            else
            {
                if (playerStatus.maxAmmoChamberHelper > playerStatus.reloadAmmo)
                {
                    playerStatus.UpdateLoadedAmmo(playerStatus.reloadAmmo);
                    playerStatus.maxAmmoChamberHelper = playerStatus.reloadAmmo;
                }
                else
                {
                    playerStatus.UpdateLoadedAmmo(playerStatus.maxAmmoChamberHelper);
                }
            }

            playerStatus.UpdateReloadAmmo(-playerStatus.maxAmmoChamberHelper);
            
            StartCoroutine(LockGun(actionLockGunCode_2));
            
            _gui.UpdateAmmoCount();
        }
    }

    public IEnumerator LockGun(string action)
    {
        float timeToWait = 0f;
        canShoot = false;

        if (action == actionLockGunCode_1)
        {
            timeToWait = GunFireRate;
        }
        else if (action == actionLockGunCode_2)
        {
            timeToWait = ReloadRate;
            _gui.FillReloadBar();
        }

        yield return new WaitForSecondsRealtime(timeToWait);
        
        canShoot = true;
    }
    #endregion
}
