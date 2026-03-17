using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class CollectableScript : MonoBehaviour
{
    [SerializeField]
    GameObject Particles;
    [SerializeField]
    GameObject Sprite;
    [SerializeField]
    AudioSource audioSource;

    Collider myCollider;
    private void Start()
    {
        // Set myCollider to this obj's collider
        myCollider = GetComponent<Collider>();
        // Set the coin to be collectable
        SetCollectOn();
    }
    private void OnTriggerEnter(Collider other)
    {

        //if the other's gameObject has a CartScript (Which would ideally only be the player) run the collected methdod
        if (other.gameObject.GetComponent<CartScript>() != null)
        {
            collected();
        }
    }
    //here is where we'd handle everything from telling the game manager that the item was collected and to doing all the fancy SFX and audio crap
    void collected()
    {
        setCollectOff();
        Debug.Log("Collect");
        StartCoroutine(respawn(5f));
    }

    //probably could think of a better name for this method but this makes the coin invisible and plays all the sfx and audio crap
    void setCollectOff()
    {
        myCollider.enabled = false;
        Particles.SetActive(true);
        Sprite.SetActive(false);
        audioSource.pitch = Random.Range(1f,1.15f);
        audioSource.Play();
    }
    //Reset the collectable to be able to be collected again
    void SetCollectOn()
    {
        myCollider.enabled = true;
        Particles.SetActive(false);
        Sprite.SetActive(true);
    }
    //after respawnTime has passed, set the coin ready to be collected
    IEnumerator respawn(float respawnTime)
    {
        yield return new WaitForSeconds(respawnTime);
        SetCollectOn();
    }
}
