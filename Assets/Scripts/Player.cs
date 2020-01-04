using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float m_speed = 10f;
    Rigidbody2D m_rigidbody;
    PlayerTriggerCollider m_playerTrigger;

    bool m_stillTime = true;
    public event Action Parked;

    [SerializeField]
    AudioClip[] m_obstacleHitAudio;
    AudioSource m_audioSource;

    [SerializeField]
    GameObject m_obstacleHitEfx;

    Animator m_Animator;

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_playerTrigger = GetComponentInChildren<PlayerTriggerCollider>();

        m_playerTrigger.Parked += () => { Parked.Invoke(); };

        m_audioSource = GetComponent<AudioSource>();
        m_Animator = gameObject.GetComponent<Animator>();
    }

    public void OutOfTime()
    {
        m_stillTime = false;
    }

    private void Update()
    {
        if (m_stillTime)
        {
            float YMovement = Input.GetAxis("Vertical");
            float XMovement = Input.GetAxis("Horizontal");

            m_rigidbody.AddForce(new Vector2(XMovement, YMovement) * m_speed);
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            int clipIndex = UnityEngine.Random.Range(0,2);
            m_audioSource.clip = m_obstacleHitAudio[clipIndex];
            m_audioSource.Play();

            int contactsCount = other.contactCount;
            int centerPoint = contactsCount / 2;

            GameObject particleEffect = Instantiate(m_obstacleHitEfx, new Vector3(other.GetContact(centerPoint).point.x, other.GetContact(centerPoint).point.y, -10), Quaternion.identity);

            Destroy(particleEffect, 1f);

            m_Animator.SetTrigger("Hit");
        }
    }

}
