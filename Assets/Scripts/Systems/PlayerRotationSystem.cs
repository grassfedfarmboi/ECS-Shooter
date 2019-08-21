using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class PlayerRotationSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        var mousePosition = Input.mousePosition;
        var cameraRay = Camera.main.ScreenPointToRay(mousePosition);
        var layerMask = LayerMask.GetMask("Floor");

        if (Physics.Raycast(cameraRay, out var hit, 100, layerMask))
        {
            Entities.ForEach((Transform transform, RotationComponent rotationComponent) => {

                var forward = hit.point - transform.position;
                var rotation = Quaternion.LookRotation(forward);

                rotationComponent.value = new Quaternion(0, rotation.y, 0, rotation.w).normalized;

            });
        }
        
    }
}
