using System;
using UnityEngine;

public class Mover : MonoBehaviour
{   
    [SerializeField] private int jumpConstant=250;
    [SerializeField] private int rotationCostant=120;
    [SerializeField] private int speed=10;

    [SerializeField] AudioClip  WalkingAudio;
    [SerializeField] AudioClip JumpAudio;


    Rigidbody rb;
    AudioSource AS;


    public static bool isInPlay;
    public static bool canJump;
    

    private void Start() 
    {   
        rb=GetComponent<Rigidbody>();
        AS=GetComponent<AudioSource>();

        isInPlay=false;
        canJump=false;
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag=="Ground")
        {
            isInPlay = true;
            canJump = true; 
        }
        if(other.gameObject.tag=="Finish"){
            AS.Stop();
        }
    }

    private void Update() 
    {
        if(!isInPlay){return;}
        Forward();
        Rotate();
        Jump();
    }

    private void Forward()
    {
        if(Input.GetKey(KeyCode.W))
        {   
            if(speed==0){return;}
            transform.Translate(new Vector3(-1*speed*Time.deltaTime,0,0));
            if(canJump&&!AS.isPlaying){
                AS.PlayOneShot(WalkingAudio);
            }
        }
        else if(Input.GetKey(KeyCode.S))
        {
            if(speed==0){return;}
            transform.Translate(new Vector3(speed*Time.deltaTime,0,0));
            if(canJump&&!AS.isPlaying){
                AS.PlayOneShot(WalkingAudio);
            }
        }
        else if(canJump)
        {
            AS.Stop();
        }
    }

    private void Rotate()
    {
        if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up*rotationCostant*Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(-1*Vector3.up*rotationCostant*Time.deltaTime);
        }
    }

    private void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space)&&canJump)
        {
            rb.AddForce(Vector3.up*jumpConstant);
            canJump=false;
            AS.PlayOneShot(JumpAudio);
        }
    }

}
