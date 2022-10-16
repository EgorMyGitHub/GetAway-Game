using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float liveTime;
    [SerializeField] private float checkDistance;
    
    private void Awake()
    {
        Destroy(gameObject, liveTime);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed);
        
        CheckCollider();
    }

    private void CheckCollider()
    {
        var hitbox = Physics2D.Raycast(transform.position, 
            Vector2.right, checkDistance);
        
        if(hitbox.collider != null && hitbox.collider.TryGetComponent<Player>(out var player))
            Kill(player.gameObject);
    }

    private void Kill(GameObject killObject)
    {
        Destroy(killObject);
        Destroy(gameObject);
    }

    public void InitializedAndStart(Transform gunTransform)
    {
        transform.rotation = gunTransform.rotation;
    }
}
