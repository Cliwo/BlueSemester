using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem skillParticle;

    [SerializeField]
    private ParticleSystem hitParticle;

    private List<ParticleCollisionEvent> collisionEvents;

    // Use this for initialization
    private void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void OnSkill()
    {
        skillParticle.Emit(3);
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Hit!");
        ParticlePhysicsExtensions.GetCollisionEvents(skillParticle, other, collisionEvents);
        for (int i = 0; i < collisionEvents.Count; i++)
        {
            EmitAtLocation(collisionEvents[i]);
        }
    }

    private void EmitAtLocation(ParticleCollisionEvent particleCollisionEvent)
    {
        hitParticle.transform.position = particleCollisionEvent.intersection;
        hitParticle.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal);
        hitParticle.Emit(1);
    }
}