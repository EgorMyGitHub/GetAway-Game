using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private int bulletCount;
    [SerializeField] private int reloadTime = 500;
    
    [SerializeField] private GameObject bullet;

    [SerializeField] private Transform spawnBulletPos;

    [SerializeField] private float kDStartTimer;
    
    private float m_KDtimer;
    
    private Player m_Player;
    
    private bool m_IsShot;
    private bool m_Reload;
    private bool m_IsActive;
    
    private Action m_ShotAction;

    private void Awake()
    {
        m_ShotAction += HandleShot;
    }

    private async void Update()
    {
        if(!m_IsActive)
            return;
        
        GetInput();
        UpdateKDTimer();

        if (m_Reload)
        {
            await Reload();
        }
        
        if (m_IsShot)
        {
            Shot();
        }
    }

    private async Task Reload()
    {
        await Task.Delay((int)reloadTime);

        bulletCount = 10;
    }
    
    private void UpdateKDTimer()
    {
        if (m_KDtimer > 0)
            m_KDtimer -= Time.deltaTime;
    }
    
    private void HandleShot()
    {
        bulletCount--;

        m_KDtimer = kDStartTimer;
    }

    private void Shot()
    {
        if(!CheckShot())
            return;
        
        var newBullet = Instantiate(bullet, spawnBulletPos.position, Quaternion.identity);

        var bulletComponent = newBullet.GetComponent<Bullet>();

        bulletComponent.InitializedAndStart(transform);
        
        m_ShotAction.Invoke();
    }

    private bool CheckShot()
    {
        if (bulletCount <= 0)
            return false;

        if (m_KDtimer > 0)
            return false;
        
        return true;
    }
    
    private void GetInput()
    {
        m_IsShot = Input.GetKey(KeyCode.E);
        m_Reload = Input.GetKeyDown(KeyCode.R);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out m_Player))
        {
            transform.position = m_Player.gunPos.position;
            transform.parent = m_Player.gunPos;
            m_IsActive = true;
        }
    }
}
