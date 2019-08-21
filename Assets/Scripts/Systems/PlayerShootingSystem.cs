using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

public class PlayerShootingSystem : JobComponentSystem
/// Tags specific entities with the Firing Component
{

    //IJobForEach replaces IJobParallelFor; IJobForEachWithEntity additionally provides a reference to the entity object
    private struct PlayerShootingJob : IJobForEachWithEntity<Gun>
    {
        public EntityCommandBuffer.Concurrent commandBuffer; //Concurrent command buffers should always be used with parallel Jobs
        public bool isFiring;

        // The JobComponentSystem calls your Execute() method for each eligible entity, 
        // passing in the components identified by the IJobForEach signature. Thus, the parameters 
        // of your Execute() function must match the generic arguments you defined for the Job struct. 
        // Additionally, IJobForEachWithEntity passes the entity ref and the index into the extended, parallel arrays of components.
        public void Execute(Entity entity, int index, [ReadOnly] ref Gun gun)
        {
            if (!isFiring) return;
            commandBuffer.AddComponent<Firing>(index, entity);
        }
    }

    BeginSimulationEntityCommandBufferSystem m_beginSimEcbSystem;
    protected override void OnCreate()
    {
        m_beginSimEcbSystem = World.GetOrCreateSystem<BeginSimulationEntityCommandBufferSystem>();
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var playerShootingJobHandle = new PlayerShootingJob
        {
            commandBuffer = m_beginSimEcbSystem.CreateCommandBuffer().ToConcurrent(), // Each system that uses the EntityCommandBuffer provided by a command buffer system must call CreateCommandBuffer() to create its own command buffer instance. 
            isFiring = Input.GetButton("Fire1")
        }.Schedule(this, inputDeps);
        m_beginSimEcbSystem.AddJobHandleForProducer(playerShootingJobHandle); // Dependency ensures buffer system waits for Job to complete before executing the command buffer.

        return playerShootingJobHandle;
    }
}