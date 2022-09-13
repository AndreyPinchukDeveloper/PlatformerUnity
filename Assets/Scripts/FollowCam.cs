using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    private Transform _target;
    private float _smoothTime = 0.2f;
    private Vector3 _velocity = Vector3.zero;

    [SerializeField] private float _minX, _maxX;

    private void LateUpdate()
        //LU calls after all calculation in Update has been done
        //we'll have conflicts if we write thisd code in Update, like "slide show"
    {
        _velocity = transform.position;
        _velocity.x = _target.position.x;
        transform.position = _velocity;
    }
    // Start is called before the first frame update
    void Start()
    {
        _target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
