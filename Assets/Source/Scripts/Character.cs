using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] private Overview _overview;
    [SerializeField] private Tile _startTile;
    
    private void Start()
    {
        transform.position = _startTile.Root;
        _movement.Init(_startTile);
    }

    private void Update()
    {
        _movement.Move();
        _overview.View();
    }
}
