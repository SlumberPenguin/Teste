using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//*集中管理UI的地方*
public class UISystem : MonoBehaviour
{
    public Vector3 HandleDirection;//玩家推手柄的方向，x水平，y垂直，三个值范围0-1
    public float HandleDistance;//玩家推手柄的距离，范围0-1
    public float HandleAngle;//手柄的角度
}
