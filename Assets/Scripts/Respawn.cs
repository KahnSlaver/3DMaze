using UnityEngine;
using UnityEngine.SceneManagement;


public class Respawn : MonoBehaviour
{
    Vector3 startingPosition;
    Quaternion startingRotation;

    [SerializeField] ParticleSystem DeathParticles;
    [SerializeField] ParticleSystem SuccessParticles;
    [SerializeField] AudioClip DeathAudio;
    [SerializeField] AudioClip SuccessAudio;
    [SerializeField] Rigidbody Player;

    AudioSource AS;
    
    
    private void Start() 
    {
        startingPosition = Player.transform.position;
        startingRotation = Player.transform.rotation;

        AS = GetComponent<AudioSource>();
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.R)&&Mover.canJump)
        {
            Death();
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0); //Back To Main Screen
        }
        if(Player.transform.position.y<-5)
        {   
            Death();
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player")
        {   
            if(!SuccessParticles.isPlaying){
                SuccessParticles.Play();
                AS.PlayOneShot(SuccessAudio);
            }
            Player.GetComponent<Mover>().enabled=false;
            Invoke("NextLevel",2f);
        }
    }
    
    private void Death()
    {   
        Player.GetComponent<AudioSource>().Stop();
        if(!DeathParticles.isPlaying&&Mover.isInPlay){
                DeathParticles.Play();
                AS.PlayOneShot(DeathAudio);
            }
        Mover.isInPlay = false;
        Mover.canJump = false;
        Invoke("Teleportation",1.5f);
    }

    private void Teleportation()
    {   
        AS.Stop();
        Player.transform.position = startingPosition;
        Player.transform.rotation = startingRotation;
    }

    private void NextLevel()
    {   
        SceneManager.LoadScene(2);
    }
}
