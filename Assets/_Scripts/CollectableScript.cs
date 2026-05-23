using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class CollectableScript : MonoBehaviour
{
    [SerializeField]
    GameObject Particles;
    [SerializeField]
    GameObject Sprite;
    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    GameObject UI;
    [SerializeField]
    Animator UIanim;
    [SerializeField]
    GAMEMANAGER gameManager;

    Collider myCollider;
    private void Start()
    {
        gameManager = GAMEMANAGER.Instance;
        // Set myCollider to this obj's collider
        myCollider = GetComponent<Collider>();
        // Set the coin to be collectable
        SetCollectOn();
        UI.SetActive(false);
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

        gameManager.Load_Game();
        SaveData data = gameManager.getSaveData();

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            //Level One Index == 2
            case 2:
                if(data.Level_One_B_Side_Locked == true)
                    data.Level_One_B_Side_Locked = false;
                break;
            case 3:
                if (data.Level_Two_B_Side_Locked == true)
                    data.Level_Two_B_Side_Locked = false;
                break;
            case 4:
                if (data.Level_Three_B_Side_Locked == true)
                    data.Level_Three_B_Side_Locked = false;
                break;
            case 5:
                if (data.Level_Four_B_Side_Locked == true)
                    data.Level_Four_B_Side_Locked = false;
                break;
            case 6:
                if (data.Level_Five_B_Side_Locked == false)
                    data.Level_Five_B_Side_Locked = true;
                break;
            default:
                Debug.LogWarning("Collectable in scene with no level index, cannot set save data for collectable");
            break;
        }
        gameManager.Save_Game(data);

        StartCoroutine(respawn(5f));
    }

    //probably could think of a better name for this method but this makes the coin invisible and plays all the sfx and audio crap
    void setCollectOff()
    {
        myCollider.enabled = false;
        Particles.SetActive(true);
        Sprite.SetActive(false);
        UI.SetActive(true);

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
