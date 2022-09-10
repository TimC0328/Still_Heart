using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    private enum HitboxTypes { HEAD, U_BODY, L_BODY, L_ARM, R_ARM, L_LEG, R_LEG};

    private HitboxTypes hitboxType;

    private Enemy self;

    private void Start()
    {
        self = transform.parent.parent.gameObject.GetComponent<Enemy>();
        GetHitBoxType();
    }

    private void GetHitBoxType()
    {
        switch (name)
        {
            case "Head":
                hitboxType = HitboxTypes.HEAD;
                break;
            case "Upper Body":
                hitboxType = HitboxTypes.U_BODY;
                break;
            case "Lower Body":
                hitboxType = HitboxTypes.L_BODY;
                break;
            case "Left Arm":
                hitboxType = HitboxTypes.L_ARM;
                break;
            case "Right Arm":
                hitboxType = HitboxTypes.R_ARM;
                break;
            case "Left Leg":
                hitboxType = HitboxTypes.L_LEG;
                break;
            case "Right Leg":
                hitboxType = HitboxTypes.R_LEG;
                break;
            default:
                break;
        }
    }

    // Pass weapon damage through function and increase or decrease based on area
    // Call damage function on enemy after damage is calculated
    public void HitArea(int damage)
    {
        switch(hitboxType)
        {
            case HitboxTypes.HEAD:
                Debug.Log("Hit head!");
                damage *= 2;
                break;
            case HitboxTypes.U_BODY:
                Debug.Log("Hit upper body!");
                break;
            case HitboxTypes.L_BODY:
                Debug.Log("Hit lower body!");
                break;
            case HitboxTypes.L_ARM:
                Debug.Log("Hit left arm!");
                break;
            case HitboxTypes.R_ARM:
                Debug.Log("Hit right arn!");
                break;
            case HitboxTypes.L_LEG:
                Debug.Log("Hit left leg!");
                break;
            case HitboxTypes.R_LEG:
                Debug.Log("Hit right leg!");
                break;
            default:
                break;
        }

        self.Damage(damage);
    }
}
