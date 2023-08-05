using System;
using UnityEngine;

[Serializable]
public class Movement
{
    [SerializeField] private Transform _characterTransform;
    private KeyCode ButtonMoveForward;
    private KeyCode ButtonMoveBack;
    private KeyCode ButtonMoveRight;
    private KeyCode ButtonMoveLeft;

    private Tile Tile { get; set; }
    
    private Vector3 _direction;

    public void ReadKey()
    {
        (bool, bool) moveBackForward = (Input.GetKeyDown(KeyCode.W), Input.GetKeyDown(KeyCode.S));
        (bool, bool) moveRightLeft = (Input.GetKeyDown(KeyCode.A), Input.GetKeyDown(KeyCode.D));
        if (moveBackForward.Item1) _direction = _characterTransform.forward;
        else if (moveBackForward.Item2) _direction = -_characterTransform.forward;
        else if (moveRightLeft.Item1) _direction = -_characterTransform.right;
        else if (moveRightLeft.Item2) _direction = _characterTransform.right;
        else _direction = Vector3.zero;
    }
    
    public void Move()
    {
        ReadKey();
        if (_direction != Vector3.zero)
        {
            var nextTileHit = TryGetNextTile();
            if(nextTileHit.transform != null) {
                Tile = nextTileHit.transform.gameObject.GetComponent<Tile>();
                _characterTransform.transform.position = Tile.Root;
                if (_direction == _characterTransform.right)
                {
                    _characterTransform.Rotate(0, 90, 0);
                } else if (_direction == -_characterTransform.right)
                {
                    _characterTransform.Rotate(0, -90, 0);
                } else if (_direction == -_characterTransform.forward)
                {
                    _characterTransform.Rotate(0, 180, 0);
                }
                _direction = Vector3.zero;
            }
        }
    }

    public void Init(Tile tile)
    {
        Tile = tile;
        UpdateControll();
    }

    public void UpdateControll(KeyCode forward=KeyCode.W, KeyCode back=KeyCode.S, KeyCode right=KeyCode.D, KeyCode left=KeyCode.A)
    {
        ButtonMoveForward = forward;
        ButtonMoveBack = back;
        ButtonMoveRight = right;
        ButtonMoveLeft = left;
    }
    private RaycastHit TryGetNextTile()
    {
        RaycastHit hitTarget;
        Physics.Raycast(Tile.transform.position, _direction, out hitTarget, .7f);
        return hitTarget;
    }
}
