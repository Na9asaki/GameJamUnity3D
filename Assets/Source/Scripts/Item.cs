using UnityEngine;

public class Item : MonoBehaviour, IPlaceable
{
    [SerializeField] private Tile _tile;

    private void Start()
    {
        _tile.ItemOccupied = this;
        transform.position = _tile.Root;
    }

    public void Place(Tile tileApplicationForce)
    {
        _tile.ItemOccupied = null;
        _tile = tileApplicationForce;
        _tile.ItemOccupied = this;
        transform.position = _tile.Root;
    }
}
