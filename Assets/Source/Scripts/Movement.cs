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

    private Tile CurrentTile { get; set; }
    
    private Vector3 _direction;

    public void ReadKey()
    {
        (bool, bool) moveBackForward = (Input.GetKeyDown(ButtonMoveForward), Input.GetKeyDown(ButtonMoveBack));
        (bool, bool) moveRightLeft = (Input.GetKeyDown(ButtonMoveLeft), Input.GetKeyDown(ButtonMoveRight));
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
            if(nextTileHit.transform != null)
            {
                var temporaryTile = nextTileHit.transform.gameObject.GetComponent<Tile>();
                if (temporaryTile.ItemOccupied != null)
                {
                    if (TryPushItem(temporaryTile.ItemOccupied, out temporaryTile))
                    {
                        CurrentTile = temporaryTile;
                    }
                }
                else
                {
                    CurrentTile = nextTileHit.transform.gameObject.GetComponent<Tile>();
                }
                RotateCharacter();
                _characterTransform.transform.position = CurrentTile.Root;
                _direction = Vector3.zero;
            }
        }
    }

    public void Init(Tile tile)
    {
        CurrentTile = tile;
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
        Physics.Raycast(CurrentTile.transform.position, _direction, out hitTarget, .7f);
        return hitTarget;
    }

    private bool TryPushItem(IPlaceable item, out Tile tileBack)
    {
        _direction = -_direction;
        var nextTileHit = TryGetNextTile();
        _direction = -_direction;
        if (nextTileHit.transform != null)
        {   
            item.Place(CurrentTile);
            tileBack = nextTileHit.transform.gameObject.GetComponent<Tile>();
            return true;
        }
        tileBack = null;
        return false;
    }

    private void RotateCharacter()
    {
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
    }
}
