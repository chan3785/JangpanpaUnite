using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class bossThrowingStone : MonoBehaviour
{

    
    public Transform m_Target;
    public Transform m_Target2;
    public float m_InitialAngle = 30f; // 처음 날라가는 각도
    private Rigidbody2D m_Rigidbody;


    private GameObject player,enemy;
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        player = GameObject.Find("playerStone");
        enemy = GameObject.Find("bossStone");
    }


    public void bossThrow()
    {
        
        transform.position = new Vector3(-1, 0, 0);
        Vector3 velocity = GetVelocity(transform.position, m_Target.position, m_InitialAngle);
        m_Rigidbody.velocity = velocity;
        player.SetActive(false);
        enemy.SetActive(true);
    }

    public void playerThrow()
    {
        ;
        transform.position = new Vector3(-7, 0, 0);
        Vector3 velocity = GetVelocity(transform.position, m_Target2.position, m_InitialAngle);
        m_Rigidbody.velocity = velocity;
        player.SetActive(true);
        enemy.SetActive(false);
    }

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            bossThrow();
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerThrow();
        }
        
    }

    public Vector3 GetVelocity(Vector3 player, Vector3 target, float initialAngle)
    {
        float gravity = Physics.gravity.magnitude;
        float angle = initialAngle * Mathf.Deg2Rad;

        Vector3 planarTarget = new Vector3(target.x, 0, target.z);
        Vector3 planarPosition = new Vector3(player.x, 0, player.z);

        float distance = Vector3.Distance(planarTarget, planarPosition);
        float yOffset = player.y - target.y;

        float initialVelocity
            = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));

        Vector3 velocity
            = new Vector3(0f, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));

        float angleBetweenObjects
            = Vector3.Angle(Vector3.forward, planarTarget - planarPosition) * (target.x > player.x ? 1 : -1);
        Vector3 finalVelocity
            = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;

        return finalVelocity;
    
    }
    


}
