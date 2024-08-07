using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Start line");
                break;
            case "Finish":
                Debug.Log("Win");
                break;
            default:
                Debug.Log("Dead");
                break;
        }
    }
}
