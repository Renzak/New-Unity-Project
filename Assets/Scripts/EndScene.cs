using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{

    readonly string PLAYER_TAG = "Player";
    public string nextSceneName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PLAYER_TAG))
        {
            SceneManager.LoadScene(nextSceneName);
        }
    }

}

