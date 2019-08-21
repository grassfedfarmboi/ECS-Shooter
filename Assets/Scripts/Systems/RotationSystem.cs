using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class RotationSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((RotationComponent rotationComponent, Rigidbody rigidbody) => {

                var rotation = rotationComponent.value;
                rigidbody.MoveRotation(rotation);

            });
    }
}
