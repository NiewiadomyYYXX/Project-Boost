using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float ReloadDelay = 1f;
    [SerializeField] float NextLvlDelay = 1.25f;
    [SerializeField] AudioClip DeathSound;
    [SerializeField] AudioClip SuccessSound;

    AudioSource a;

    bool isTransitioning = false;

    void Start()
    {
        a = GetComponent<AudioSource>();
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
                Debug.Log("Start line");
                break;
            case "Finish":
                StartNext();
                break;
            default:
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
        Invoke("ReloadLevel", ReloadDelay);
    }

    void StartNext()
    {
        isTransitioning = true;
        a.Stop();
        GetComponent<Movement>().enabled = false;
        a.PlayOneShot(SuccessSound);
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
