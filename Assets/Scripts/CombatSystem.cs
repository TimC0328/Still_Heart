using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    [SerializeField] private Camera otsCamera;

    [SerializeField] private Transform gunPos;
    [SerializeField] private LineRenderer lineRender;

    [SerializeField] private float sensitivity = 100f;
    [SerializeField] private float fpsSensitivity = 50f;


    [SerializeField] private Light muzzleFlash;

    [SerializeField] private Weapon currentWeapon;


    private Ray ray;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        lineRender.enabled = false;
        muzzleFlash.enabled = false;
        currentWeapon.Equip();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void TakeAim()
    {


        RotateInFrame();

        DrawLaser();

        if (Input.GetMouseButtonDown(0))
        {
            if (currentWeapon.Fire())
                StartCoroutine(DisplayMuzzleFlash());
        }
        else if(Input.GetKeyDown(KeyCode.R) && !currentWeapon.reloading)
        {
            StartCoroutine(currentWeapon.Reload());
        }

    }

    IEnumerator DisplayMuzzleFlash()
    {
        muzzleFlash.enabled = true;
        yield return new WaitForSeconds(0.075f);
        muzzleFlash.enabled = false;
    }

    void DrawLaser()
    {
        ray = new Ray(gunPos.position, gunPos.TransformDirection(Vector3.forward));

        lineRender.SetPosition(0, gunPos.transform.position);

        if (Physics.Raycast(ray, out hit, 100))
        {
            lineRender.SetPosition(1, hit.point);
        }
        else
        {
            lineRender.SetPosition(1, ray.GetPoint(10));
        }
    }



    float ClampAngle(float angle, float min, float max)
    {
        if (angle < 90 || angle > 270)
        {       // if angle in the critic region...
            if (angle > 180) angle -= 360;  // convert all angles to -180..+180
            if (max > 180) max -= 360;
            if (min > 180) min -= 360;
        }
        angle = Mathf.Clamp(angle, min, max);
        if (angle < 0) angle += 360;  // if angle negative, convert to 0..360
        return angle;
    }


    void RotateInFrame()
    {
        float mx = Input.GetAxis("Mouse X");
        float my = Input.GetAxis("Mouse Y");

        //float xMin = -15 + transform.eulerAngles.x;
        // float xMax = 15 + transform.eulerAngles.x;
        float xMin = -1 + transform.eulerAngles.x;
        float xMax = 1 + transform.eulerAngles.x;


        float yMin = -1f + transform.eulerAngles.y;
        //float yMax = 30f + transform.eulerAngles.y;
        float yMax = 1f + transform.eulerAngles.y;


        Vector3 rot = gunPos.rotation.eulerAngles + new Vector3(-my * Time.deltaTime * sensitivity, mx * Time.deltaTime * sensitivity, 0f);
      
        rot.x = ClampAngle(rot.x, xMin, xMax);
        rot.y = ClampAngle(rot.y, yMin, yMax);

        gunPos.eulerAngles = rot;

        if (rot.x < xMin + 2f || rot.x > xMax - 2f)
        {
            rot = transform.rotation.eulerAngles + new Vector3(-my * Time.deltaTime * sensitivity, 0f, 0f);
            rot.x = ClampAngle(rot.x, -30f, 30f);
            transform.eulerAngles = rot;
        }

        if (rot.y < yMin + 2f || rot.y > yMax - 1f)
        {
            rot = transform.rotation.eulerAngles + new Vector3(0f, mx * Time.deltaTime * sensitivity, 0f);
            transform.eulerAngles = rot;
        }

    }

    public void ToggleWeapon()
    {
        currentWeapon.Toggle();
        lineRender.enabled = !lineRender.enabled;
    }

    public void ResetGun()
    {
        Vector3 temp = transform.rotation.eulerAngles;
        temp.x = 0f;
        transform.rotation = Quaternion.Euler(temp);

        gunPos.localRotation = Quaternion.identity;
    }

    public void ChangeWeapon(Weapon weapon)
    {
        currentWeapon = weapon;
    }

    public Weapon GetCurrentWeapon()
    {
        return currentWeapon;
    }

    public void UpdateFPS()
    {
        float mx = Input.GetAxis("Horizontal");
        float my = Input.GetAxis("Vertical");

        Vector3 rot = transform.rotation.eulerAngles + new Vector3(my * Time.deltaTime * fpsSensitivity, mx * Time.deltaTime * fpsSensitivity, 0f);
        transform.eulerAngles = rot;
    }


}
