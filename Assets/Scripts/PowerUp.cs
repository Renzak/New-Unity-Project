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

    Vector3 normalPlayerScale;

    const float scaleMultiplier = 2f;

    GameObject[] walls = null;
    static Material playerMaterial = null;
    static Material ghostMaterial = null;

    private void Start()
    {
        normalPlayerScale = GameObject.FindGameObjectWithTag(Config.Tags.Player).transform.localScale;

        if(ghostMaterial == null)
            ghostMaterial = Resources.Load<Material>(Config.MaterialPaths.ghostPowerup);

        if(playerMaterial == null)
            playerMaterial = Resources.Load<Material>(Config.MaterialPaths.player);

        walls = GameObject.FindGameObjectsWithTag(Config.Tags.Wall);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Config.Tags.Player))
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
                    other.GetComponent<Renderer>().material = playerMaterial;
                    
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
                    playerMaterial = other.GetComponent<Renderer>().sharedMaterial;
                    other.GetComponent<Renderer>().material = ghostMaterial;

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
                    Debug.Log("Power up type not found!");
                    break;
            }
        }
    }
}
