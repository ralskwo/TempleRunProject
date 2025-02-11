using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] ParticleSystem collisionParticleSystem;
    [SerializeField] AudioSource boulderSmashAudioSource;
    [SerializeField] float shakeModifier = 10f;
    [SerializeField] float collisionCoolDown = 1f;

    CinemachineImpulseSource cinemachineImpulseSource;

    float collisionTimer = 1f;

    private void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
        boulderSmashAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        collisionTimer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collisionTimer < collisionCoolDown) return;

        FireImpulse();
        CollisionFX(collision);
        collisionTimer = 0f;
    }

    private void FireImpulse()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeIntensity = (1f / distance) * shakeModifier;
        shakeIntensity = Mathf.Min(shakeIntensity, 1f);

        cinemachineImpulseSource.GenerateImpulse(shakeIntensity);
    }

    void CollisionFX(Collision other)
    {
        ContactPoint contactPoint = other.contacts[0];
        collisionParticleSystem.transform.position = contactPoint.point;
        collisionParticleSystem.Play();
        boulderSmashAudioSource.Play();
    }
}
