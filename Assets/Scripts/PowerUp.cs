using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    Grow,
    Shrink,
    Normalize,
}
public class PowerUp : MonoBehaviour
{
    public PowerUpType powerUpType;

    Vector3 normalPlayerScale;

    const float scaleMultiplier = 2f;

    private void Start()
    {
        normalPlayerScale = GameObject.FindGameObjectWithTag(Config.PLAYER_TAG).transform.localScale;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Config.PLAYER_TAG))
        {
            switch (powerUpType)
            {
                case PowerUpType.Grow:
                    other.transform.localScale = normalPlayerScale * scaleMultiplier;
                    break;
                case PowerUpType.Shrink:
                    other.transform.localScale = normalPlayerScale / scaleMultiplier;
                    break;
                case PowerUpType.Normalize:
                    other.transform.localScale = normalPlayerScale;
                    break;
                default:
                    Debug.Log("Implement power up type!");
                    break;
            }
        }
    }
}
