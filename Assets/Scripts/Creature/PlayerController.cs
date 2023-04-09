using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;
public class PlayerController : CreatureBase
{
    CharacterController controller;
    public UISystem uisystem;
    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }
    void Start()
    {

    }
    void Update()
    {
        GravityMove();
        PlayerMove();
    }
    public void GravityMove()
    {
        //玩家移动不移动都需要重力，所以没写一块
        Vector3 dir = new Vector3(0, -0.1f, 0);
        controller.Move(dir);
    }
    public void PlayerMove()
    {
        Vector3 dir = new Vector3(uisystem.HandleDirection.x, 0, uisystem.HandleDirection.y);
        controller.Move(dir * MoveSpeed * Time.deltaTime);
        transform.eulerAngles = new Vector3(0, -uisystem.HandleAngle, 0);
    }
}
