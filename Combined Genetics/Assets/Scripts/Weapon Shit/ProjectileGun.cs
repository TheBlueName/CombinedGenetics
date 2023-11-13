using UnityEngine;
using System.Collections;
using TMPro;

public class ProjectileGun : MonoBehaviour
{
    [Header("Basic Stats")]
    public GameObject bullet;
    public float shootForce, upwardForce;
    public bool IsPistol;
    public bool isRayCast;

    [Header("If Raycast")]
    public int damage = 10;
    [Space]
    [SerializeField] private GameObject bloodParticle;
    [SerializeField] private GameObject wallParticle;

    [Header("Accuracy")]
    public float spread = 1f;
    public float spreadReduce = 0.5f;
    public float shotSpreadIncrease = 0.1f;

    [Space]

    [Range(25f, 55f)]
    public float adsFov = 50f;
    public float lerpTime = 1f;

    [Space]
    public float Xrecoil;
    public float Yrecoil;
    public float Zrecoil;

    [Space]
    public float ADSrecoil;

    [Header("Ammo")]
    public float timeBetweenShooting, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;
    public int totalAmmo = 60;

    [Header("Else")]
    private Vector3 originPos;
    private Sway s;

    bool shooting, readyToShoot, reloading, canReload;

    [Header("Fill")]
    public ParticleSystem muzzleFlash;
    private TextMeshProUGUI ammunitionDisplay;
    public Animator animator;
    public Animator aimAnimator;
    private Camera fpsCam;
    private Transform camTrans;
    public Transform attackPoint;

    public bool allowInvoke = true;

    [Header("Sounds")]
    [SerializeField] private AudioSource shootSound;

    //other shit
    private GameObject CrossHair;
    private AmmoInventory ammoin;
    private Recoil recoil;
    private float originSpread;
    private hit m;
    private modsUI modsStats;


    void Start()
    {
        ammoin = GameObject.Find("Script1").GetComponent<AmmoInventory>();
        modsStats = FindObjectOfType<modsUI>();
        m = GameObject.Find("Hitmarker").GetComponent<hit>();
    } 

    private void Awake()
    {
        canReload =  true;
        bulletsLeft = magazineSize;
        readyToShoot = true;

        originSpread = spread;

        s = GameObject.Find("InputWeapon").GetComponent<Sway>();
        CrossHair = GameObject.Find("CrossHair");
        ammunitionDisplay = GameObject.Find("MagAmmo").GetComponent<TextMeshProUGUI>();
        fpsCam = Camera.main;
        camTrans = GameObject.Find("Main Camera").GetComponent<Transform>();
        recoil = GameObject.Find("CameraPosition").GetComponent<Recoil>();

        //set the recoil values
        recoil.recoilX = Xrecoil;
        recoil.recoilY = Yrecoil;
        recoil.recoilZ = Zrecoil;

    }

    private void Update()
    {
            if(IsPistol && s != null) s.pistolEquipped = true;
            if(!IsPistol && s != null) s.pistolEquipped = false;
            
        MyInput();
         ModsDisplay();

        if (ammunitionDisplay != null)
             ammunitionDisplay.SetText(bulletsLeft / bulletsPerTap + " / " + totalAmmo / bulletsPerTap);



        if (Input.GetButtonDown("Fire2"))
        {   
            //fov
            float t = Mathf.PingPong(Time.time / lerpTime, 1f);
            float currentFov = Mathf.Lerp(60, adsFov, t);
            fpsCam.fieldOfView = currentFov;
            
             aimAnimator.SetBool("ADS", true);
              CrossHair.SetActive(false);
               spread -= spreadReduce;
                recoil.recoilX = ADSrecoil;
        }

        if (Input.GetButtonUp("Fire2"))
        {
            fpsCam.fieldOfView = 60;
             aimAnimator.SetBool("ADS", false);
              CrossHair.SetActive(true);
               spread += spreadReduce;
                recoil.recoilX = Xrecoil;
        }
        
        totalAmmo = ammoin.Ammo;
    }

    private void ModsDisplay()
    {
        //remove the minus with the recoil
        float recVal = -Xrecoil;
        modsStats.spreadText.text = spread.ToString();
         modsStats.recoilText.text = recVal.ToString();

         //fire rate
          if(allowButtonHold) modsStats.rateText.text = "Automatic " + timeBetweenShots.ToString();
          if(!allowButtonHold) modsStats.rateText.text = "Semi Auto";
          if(!allowButtonHold && bulletsPerTap >= 1) modsStats.rateText.text = "Burst Of " + bulletsPerTap.ToString() + " Rate " + timeBetweenShots;
    }

    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        //reloading 
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading && totalAmmo >= magazineSize && canReload) Reload();

        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0 && totalAmmo >= magazineSize && canReload) Reload();

        //shooting
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;

            Shoot();
        }

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            Invoke("spreadReturn", 1.0f);
        }

        //disable shooting on left shift
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
             readyToShoot = false;
              canReload = false;   
               reloading = false;
        }
        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
             readyToShoot = true;
              canReload = true;   
        }
    }

    public void Shoot()
    {

        readyToShoot = false;

        //epic hugo sound gamer design
        shootSound.pitch = Random.Range(0.75f, 1.0f);
        shootSound.PlayOneShot(shootSound.clip);
            

        spread += shotSpreadIncrease;

        animator.SetTrigger("Shooting");

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); 
        RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && isRayCast)
            {
                    Enemy enemy = hit.transform.GetComponent<Enemy>();
                    if(enemy != null)
                    {
                        enemy.TakeDamage(damage);

                        //fx
                        Transform pos = hit.transform;
                        Instantiate(bloodParticle, pos.position, pos.rotation);
                         m.mark();
                    }
                    else Instantiate(wallParticle, hit.transform.position, Quaternion.identity);
            }
            if(!isRayCast)
            { 
                Vector3 targetPoint;
            if (Physics.Raycast(ray, out hit))
                targetPoint = hit.point;
            else
                targetPoint = ray.GetPoint(75);

            Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

            float distanceToTarget = Vector3.Distance(targetPoint, attackPoint.position);

            // Calculate the spread based on the distance to the target
            float modifiedSpread = spread * (distanceToTarget / 100f);

            float x = Random.Range(-modifiedSpread, modifiedSpread);
            float y = Random.Range(-modifiedSpread, modifiedSpread);

            Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

            GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); 
            currentBullet.transform.forward = directionWithSpread.normalized;

            currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
            currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);
            }

            if (muzzleFlash != null)
                muzzleFlash.Play();

            bulletsLeft--;
            bulletsShot++;


            recoil.RecoilShoot();

            if (allowInvoke)
            {
                Invoke("ResetShot", timeBetweenShooting);
                allowInvoke = false;
            }


            if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
                Invoke("Shoot", timeBetweenShots);
    }

        void spreadReturn()
        {
            spread = originSpread;
        }


    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    private void Reload()
    {
        reloading = true;
        s.IsReloading();
        totalAmmo =- magazineSize;
        animator.SetBool("Reloading", true);
        Invoke("ReloadFinished", reloadTime);
    }
    private void ReloadFinished()
    {
        //Fill magazine
        bulletsLeft = magazineSize;
        reloading = false;
        animator.SetBool("Reloading", false);
    }  

}
