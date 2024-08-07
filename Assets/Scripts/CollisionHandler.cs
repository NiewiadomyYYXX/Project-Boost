using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] float ReloadDelay = 1f;
    [SerializeField] float NextLvlDelay = 1.25f;

    void OnCollisionEnter(Collision collision)
    {
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
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", ReloadDelay);
    }

    void StartNext()
    {
        GetComponent<Movement>().enabled = false;
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
