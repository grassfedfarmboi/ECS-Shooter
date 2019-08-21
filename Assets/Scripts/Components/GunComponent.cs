using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public struct Gun : IComponentData
{

}

public class GunComponent : ComponentDataProxy<Gun>
{
    
}
