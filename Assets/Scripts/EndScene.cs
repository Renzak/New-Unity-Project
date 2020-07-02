using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    public string nextSceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Config.PLAYER_TAG))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

}

