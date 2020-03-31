using UnityEngine;
using UnityEngine.SceneManagement;

public class GUI : MonoBehaviour
{
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
