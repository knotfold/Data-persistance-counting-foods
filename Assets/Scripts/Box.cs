using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] ParticleSystem explosionParticleGood;
    [SerializeField] ParticleSystem explosionParticleBad;

    [SerializeField] AudioClip dropSound;
    [SerializeField] AudioClip wrongSound;

    private AudioSource boxAudio;

    // Start is called before the first frame update
    void Start()
    {
        boxAudio = gameObject.GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {

            if (gameManager.currentFood.CompareTag(gameObject.tag))
            {
                Instantiate(gameManager.currentFood, transform.position, gameManager.currentFood.transform.rotation);
                gameManager.AddScore();
                boxAudio.PlayOneShot(dropSound, 1.0f);
                Instantiate(explosionParticleGood, gameObject.transform.position, gameObject.transform.rotation);
                //add score
            }
            else
            {
                Instantiate(explosionParticleBad, gameObject.transform.position, gameObject.transform.rotation);
                boxAudio.PlayOneShot(wrongSound, 1.0f);
                gameManager.wrongBoxResult();
                //reduce score maybe?
            }



            Destroy(gameManager.currentFood);
            gameManager.fruitActive = false;





        }

    }
}
