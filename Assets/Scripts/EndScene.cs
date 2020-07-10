using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    Level current;
    Level next;

    private void Start()
    {
        current = Config.Levels.GetLevel(SceneManager.GetActiveScene().name);
        next = Config.Levels.GetLevel(current.order + 1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Config.Tags.Player))
        {
            SceneManager.LoadScene(next.name);
        }
    }

}

