using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAliveState : PlayerBaseState
{
    public PlayerAliveState(PlayerStateMachine currentContext) : base(currentContext) { }
    public override void UpdateState()
    {
        Look();
        Move();
        Fire();
        FreezeAgility();
    }

    private void Fire()
    {
        if (context.IsFiring)
        {
            context.activeWeapon?.TryUse();
        }
    }

    private void FreezeAgility()
    {
        
        Debug.Log("btnPressed : " + context.IsAbility1 + " delay ok? : " + (context.FreezingDelay > DateTime.Now.Second));
        if (!context.IsAbility1 || context.LastFreezing + context.FreezingDelay > DateTime.Now.Second) return;
        context.LastFreezing = DateTime.Now.Second;

        Debug.Log(context.LastFreezing);
    }

    private void Look()
    {
        Vector2 inputLook = context.InputLook;
        Vector2 lookVelocity = inputLook * context.MouseSensitivity;
        Transform transform = context.transform;
        var mainCamera = context.MainCamera;

        float horizontalLook = lookVelocity.x * Time.deltaTime;
        float verticalLook = lookVelocity.y * Time.deltaTime;

        transform.Rotate(Vector3.up, horizontalLook);

        float cameraRotation = mainCamera.transform.localEulerAngles.x;
        cameraRotation -= verticalLook;
        if (cameraRotation > 180)
        {
            cameraRotation -= 360;
        }

        cameraRotation = Mathf.Clamp(cameraRotation, -90, 90);
        mainCamera.transform.localEulerAngles = new Vector3(cameraRotation, 0, 0);
    }

    protected abstract void Move();

    private void TrySendThroughTime()
    {
        if(context.IsAbility3 && context.Health > .6 * context.MaxHealth && context.IsAbility3Usable)
        {
            Camera camera = context.TrueCamera;

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if(hit.collider.gameObject.tag == "Enemy")
                {
                    context.Health -= .6f * context.MaxHealth;
                    context.IsAbility3Usable = false;
                    EnemyController enemy = hit.collider.gameObject.GetComponentInParent<EnemyController>();
                    context.TimeMachine.SendThroughTime(enemy);
                }
            }
        }
    }




}
