using System;
using UnityEngine;

[Serializable]
public class Overview
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _characterTransform;
    [SerializeField] private float _height;

    public void View()
    {   
        //if (_camera.transform.rotation.y < _characterTransform.transform.rotation.y)
            //_camera.transform.RotateAround(_characterTransform.position, Vector3.up, 0);
    }
}
