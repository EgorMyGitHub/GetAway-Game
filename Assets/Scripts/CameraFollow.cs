using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float damping;

    [SerializeField] private Vector2 offset;
    
    private Player m_FollowPlayer;

    private void Start()
    {
        m_FollowPlayer = ComponentRoot.Resolve<Player>();

        transform.position = GetPlayerPos();
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, GetPlayerPos(), damping * Time.deltaTime);
    }

    private Vector3 GetPlayerPos()
    {
        return new Vector3(m_FollowPlayer.transform.position.x - offset.x, m_FollowPlayer.transform.position.y - offset.y, -10);
    }
}
