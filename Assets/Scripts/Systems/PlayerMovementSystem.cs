using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class PlayerMovementSystem : ComponentSystem
{

    protected override void OnUpdate()
    {
        var deltaTime = Time.deltaTime;

        Entities.ForEach((Rigidbody rigidbody, InputComponent inputComponent) => {

                var moveVector = new Vector3(inputComponent.horizontal, 0, inputComponent.vertical);
                var movePosition = rigidbody.position + moveVector.normalized * 3 * deltaTime; //3=speed?

                rigidbody.MovePosition(movePosition);

            });
    }
}
