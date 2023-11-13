 using UnityEngine;
 using System.Collections;
 
 public class Sway : MonoBehaviour 
 
{
    [SerializeField] private float Smooth;
    [SerializeField] private float SwayMult;
    private Animator animator;
    public bool pistolEquipped;
    private Rigidbody rb;
    bool ads = false;
    [HideInInspector] public float reloadTime;

    private void Start()
    {
        animator = GameObject.Find("WeaponHolder").GetComponent<Animator>();
        rb = GameObject.Find("Player").GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * SwayMult;
        float mouseY = Input.GetAxisRaw("Mouse Y") * SwayMult;

        Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
        Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

        Quaternion TargetRotation = rotationX * rotationY;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, TargetRotation, Smooth * Time.deltaTime);

        if(Input.GetKeyDown(KeyCode.LeftShift) && !pistolEquipped) animator.SetBool("Moving", true);
        if(Input.GetKeyUp(KeyCode.LeftShift) && !pistolEquipped) animator.SetBool("Moving", false);

        if(Input.GetKeyDown(KeyCode.LeftShift) && pistolEquipped) animator.SetBool("pMoving", true);
        if(Input.GetKeyUp(KeyCode.LeftShift) && pistolEquipped) animator.SetBool("pMoving", false);


        if(!ads)
        {
            if(Input.GetKeyDown(KeyCode.W)) animator.SetBool("Forward", true);
            if(Input.GetKeyUp(KeyCode.W)) animator.SetBool("Forward", false);

            if(Input.GetKeyDown(KeyCode.A)) animator.SetBool("Left", true);
            if(Input.GetKeyUp(KeyCode.A)) animator.SetBool("Left", false);

            if(Input.GetKeyDown(KeyCode.D)) animator.SetBool("Right", true);
            if(Input.GetKeyUp(KeyCode.D)) animator.SetBool("Right", false);

            if(Input.GetKeyDown(KeyCode.S)) animator.SetBool("Back", true);
            if(Input.GetKeyUp(KeyCode.S)) animator.SetBool("Back", false);
        }

        if(ads)
        {
            animator.SetBool("Moving", false);
            animator.SetBool("pMoving", false);
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
            animator.SetBool("Back", false);
        }

        if(Input.GetKeyDown(KeyCode.Mouse1)) ads = true;
        if(Input.GetKeyUp(KeyCode.Mouse1)) ads = false;


    }

    public void IsReloading()
    {
        animator.SetBool("Moving", false);
    }

}
