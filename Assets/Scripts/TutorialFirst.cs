using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialFirst : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
    }
}
