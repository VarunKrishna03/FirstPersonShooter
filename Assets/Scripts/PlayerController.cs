using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float movespeed;
    public CharacterController characterController;
    private Vector3 moveinput;
    public Transform camtransform;
    public float mousesensitivity;
    public float running;
    public bool invertX, invertY;
    public float gravitymodify;
    public float jumpspeed;
    public bool isgrounded,doublejump;
    public Transform groundcheckpoint;
    public LayerMask ground;
    public Animator anim;
    public GameObject Bullet;
    public Transform FirePoint;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        float ystore = moveinput.y;
        //moveinput.x = Input.GetAxis("Horizontal")*movespeed*Time.deltaTime;
        //moveinput.z = Input.GetAxis("Vertical")*movespeed*Time.deltaTime;
        Vector3 horizmove = transform.right * Input.GetAxis("Horizontal");
        Vector3 verticalmove = transform.forward * Input.GetAxis("Vertical");
        
        moveinput = horizmove + verticalmove;
        moveinput = moveinput * movespeed;
        //moveinput.Normalize();
        if(Input.GetKey(KeyCode.LeftShift)&&isgrounded)
        {
            moveinput = moveinput * running;
        }
        else
        {
            moveinput = moveinput*movespeed;
        }
        moveinput.y = ystore;
        moveinput.y += Physics.gravity.y * gravitymodify*Time.deltaTime;
        if(characterController.isGrounded)
        {
            moveinput.y = Physics.gravity.y * gravitymodify * Time.deltaTime;
        }
        isgrounded = Physics.OverlapSphere(groundcheckpoint.position, 0.25f, ground).Length > 0;
       
        if (Input.GetKeyDown(KeyCode.Space) && isgrounded)
        {
            moveinput.y = jumpspeed;
            doublejump = true;
        }   
         else if (doublejump && Input.GetKeyDown(KeyCode.Space))    
        {
                moveinput.y = jumpspeed;
                doublejump = false;
        }
         characterController.Move(moveinput*Time.deltaTime);
        //control the camera rotation
        Vector2 mouseinput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")*mousesensitivity);
        
        if(invertX)
        {
            mouseinput.x = -mouseinput.x;
        }
        if(invertY)
        {
            mouseinput.y = -mouseinput.y;
        }
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseinput.x, transform.rotation.eulerAngles.z);
        //cam rotation
        camtransform.rotation = Quaternion.Euler(camtransform.rotation.eulerAngles + new Vector3(-mouseinput.y, 0, 0));
        
        //BulletFiring
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(camtransform.position, camtransform.forward, out hit,50.0f))
            {
                FirePoint.LookAt(hit.point);
            }
            else
            {
                FirePoint.LookAt(camtransform.position + (camtransform.forward * 30.0f));
            }
                
            Instantiate(Bullet, FirePoint.position,FirePoint.rotation)  ;
        }

        anim.SetFloat("Movespeed", moveinput.magnitude);
        anim.SetBool("Onground", camtransform);
    }
}
