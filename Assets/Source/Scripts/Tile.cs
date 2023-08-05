using Source.Scripts;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector3 Root { get; private set; }
    public IPlaceable ItemOccupied { get; set; }
    
    [SerializeField] private Buff _tileBuff;

    private void Awake()
    {
        Root = new Vector3(transform.position.x, transform.position.y + transform.lossyScale.y, transform.position.z); 
    }
}
