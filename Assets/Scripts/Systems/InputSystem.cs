using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class InputSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        Entities.ForEach((InputComponent inputComponent) => {

                inputComponent.horizontal = horizontal;
                inputComponent.vertical = vertical;

            });
    }
}
