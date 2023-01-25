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
        TrySendThroughTime();
        FreezeAbility();
    }

    private void Fire()
    {
        if (context.IsFiring)
        {
            context.activeWeapon?.TryUse();
        }
    }

    private void FreezeAbility()
    {
        if (!context.IsAbility1 || context.IsFreezeReloading) return;

        Debug.Log("Attempting to freeze");
        
        Ray ray = context.TrueCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        Vector3 endPoint = new Vector3(0, 0, 0);
        if (Physics.Raycast(ray, out hit, 100f))
        {
            Debug.Log("Raycast hit");
            IFreezable freezable = hit.collider.GetComponentInParent<IFreezable>();
            if (freezable != null)
            {
                freezable.Freeze();
                context.StartCoroutine(FreezingDelay());
            }
        }
    }

    private void Look()
    {
        Vector2 inputLook = context.InputLook;
        Vector2 lookVelocity = inputLook * context.MouseSensitivity;
        Transform transform = context.transform;
        var mainCamera = context.VirtualCamera;

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
            Debug.Log("Condition met");
            Camera camera = context.TrueCamera;

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if(hit.collider.gameObject.tag == "Enemy")
                {
                    Debug.Log("Enemy hit");
                    context.Health -= .6f * context.MaxHealth;
                    context.IsAbility3Usable = false;
                    EnemyController enemy = hit.collider.gameObject.GetComponentInParent<EnemyController>();
                    context.TimeMachine.SendThroughTime(enemy);
                }
            }
        }
    }

    private IEnumerator FreezingDelay()
    {
        context.IsFreezeReloading = true;
        yield return new WaitForSeconds(10f);
        context.IsFreezeReloading = false;
    }


}
