using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerUpType
{
    Grow,
    Shrink,
    Normalize,
    Ghost,
}
public class PowerUp : MonoBehaviour
{
    public PowerUpType powerUpType;
    public Material ghostMaterial;

    Vector3 normalPlayerScale;

    const float scaleMultiplier = 2f;

    static GameObject[] walls;
    static Material playerMaterial;

    private void Start()
    {
        normalPlayerScale = GameObject.FindGameObjectWithTag(Config.PLAYER_TAG).transform.localScale;
        
        if(walls == null)
        {
             walls = GameObject.FindGameObjectsWithTag(Config.WALL_TAG);
        }
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

                    if(playerMaterial)
                    {
                        other.GetComponent<Renderer>().sharedMaterial = playerMaterial;
                    }

                    foreach (var wall in walls)
                    {
                        Collider collider;

                        if(wall.TryGetComponent<Collider>(out collider))
                        {
                            collider.enabled = true;
                        }
                        
                    }
                    break;

                case PowerUpType.Ghost:
                    playerMaterial = other.GetComponent<Material>();
                    other.GetComponent<Renderer>().sharedMaterial = ghostMaterial;

                    foreach (var wall in walls)
                    {
                        Collider collider;

                        if (wall.TryGetComponent<Collider>(out collider))
                        {
                            collider.enabled = false;
                        }
                    }
                    break;

                default:
                    Debug.Log("Implement power up type!");
                    break;
            }
        }
    }
}
