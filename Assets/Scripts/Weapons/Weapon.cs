using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_Weapon", menuName = "Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField]
    protected GameObject modelPrefab;
    protected GameObject model;

    [SerializeField]
    protected int ammo;
    [SerializeField]
    protected int maxAmmo;
    [SerializeField]
    protected string ammoName;
    [SerializeField]
    protected float reloadTime;
    public bool reloading = false;

    [SerializeField]
    protected int damage;

    private Transform gunPos;


    protected void Start()
    {
        gunPos = GameObject.Find("/Player/Weapon").transform;
        model = Instantiate(modelPrefab, gunPos); 
    }



    public bool Fire()
    {
        if (reloading)
            return false;

        if (ammo == 0)
            return false;

        Debug.Log("Firing weapon!");

        int layerMask = 1 << 8;
        Ray ray = new Ray(gunPos.position, gunPos.TransformDirection(Vector3.forward));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            hit.transform.gameObject.GetComponent<EnemyHitbox>().HitArea(damage);
        }

        ammo--;
        Debug.Log("Ammo: " + ammo + "/" + maxAmmo);

        return true;
    }

    public void Equip()
    {
        if (name == "None")
            return;
        gunPos = GameObject.Find("Weapon").transform;
        model = Instantiate(modelPrefab, gunPos);
        model.SetActive(false);
    }


    public void Toggle()
    {
        model.SetActive(!model.activeSelf);
    }

    public IEnumerator Reload()
    {
        int ammoNeeded = maxAmmo - ammo;

        Item item = Inventory.Instance.SearchItem(ammoName);

        if (!item)
            yield return null;

        reloading = true;

        if (ammoNeeded == 0)
            yield return null;

        yield return new WaitForSeconds(reloadTime);

        if (item.quantity >= ammoNeeded)
        {
            ammo += ammoNeeded;
            item.quantity -= ammoNeeded;
        }
        else
        {
            ammo += item.quantity;
            item.quantity -= item.quantity;
        }

        if(item.quantity == 0)
            Inventory.Instance.RemoveItem(item);

        reloading = false;
    }

    public int GetAmmo()
    {
        return ammo;
    }

}
