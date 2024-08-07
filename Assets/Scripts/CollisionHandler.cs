using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float ReloadDelay = 1f;
    [SerializeField] float NextLvlDelay = 1.25f;
    [SerializeField] AudioClip DeathSound;
    [SerializeField] AudioClip SuccessSound;
    [SerializeField] ParticleSystem DeathParticle;
    [SerializeField] ParticleSystem SuccessParticle;

    AudioSource a;

    bool isTransitioning = false;
    bool cheatCollision = false;

    void Start()
    {
        a = GetComponent<AudioSource>();
    }

    void Update()
    {
        CheatSkip();
        CheatCollision();
    }

    void CheatSkip()
    {
        if (Input.GetKey(KeyCode.L))
        {
            if (Input.GetKey(KeyCode.PageDown))
            {
                StartNext();
            }
        }
    }

    void CheatCollision()
    {
        if (Input.GetKey(KeyCode.C))
        {
            if (Input.GetKey(KeyCode.PageUp))
            {
                cheatCollision = true;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {

        if(isTransitioning == true)
        {
            return;
        }

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartNext();
                break;
            default:
                if (cheatCollision == true)
                {
                    break;
                }
                StartCrash();
                break;
        }
    }

    void StartCrash()
    {
        isTransitioning = true;
        a.Stop();
        GetComponent<Movement>().enabled = false;
        a.PlayOneShot(DeathSound);
        DeathParticle.Play();
        Invoke("ReloadLevel", ReloadDelay);
    }

    void StartNext()
    {
        isTransitioning = true;
        a.Stop();
        GetComponent<Movement>().enabled = false;
        a.PlayOneShot(SuccessSound);
        SuccessParticle.Play();
        Invoke("NextLevel", NextLvlDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene("Congratulations");
        }
        SceneManager.LoadScene(currentSceneIndex);
    }

    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

}
