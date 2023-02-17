using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playercontroller : MonoBehaviour
{

    public CharacterController controller;
    public Camera cam;

    public float speed = 12f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;   
    bool isGrounded; 

    PhotonView PV;
    private Animator _animator;
    Animator lightBoxAnimator;

    Ray ray;
    public GameObject interactInterface;
    public mouseLock mouseLock;
    public bool canMove = true;
    public bool lightBoxOpened = false;

    // Start is called before the first frame update
    void Awake()
    {
         PV = GetComponent<PhotonView>();
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        GameObject lightBox = GameObject.FindWithTag("lightBox");
        lightBoxAnimator = lightBox.GetComponent<Animator>();
        

        if(!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (lightBoxOpened == true)
        {
            transform.position = new Vector3(-3.536096f, -4.5f, 36.59479f);
            
            
        }
        
        if (!canMove)
            return;
        if(PV.IsMine)
        {
            ray = new Ray(cam.transform.position, cam.transform.forward);
            
        }

        

        CheckForColliders();

        if(!PV.IsMine)
            return;
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical"); 

        UpdateMovingBoolean((x != 0f || z != 0F));

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        
    }

    private void UpdateMovingBoolean(bool moving)
    {
        _animator.SetBool("moving", moving);
    }


    void CheckForColliders()
    {
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if(hit.collider.gameObject.name == "lightBox")
            {
                interactInterface.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    lightBoxOpened = true;
                    canMove = false;
                    mouseLock.canTurn = false;
                    transform.rotation = Quaternion.Euler(0, -100, 0);
                    
                    lightBoxAnimator.SetBool("opened?", true);
                    cam.transform.rotation = Quaternion.Euler(0, 0, 0);
                }
            } else {
                interactInterface.SetActive(false);
            }
        }
    }
}
