using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
  public List<GameObject> AvaiableWeapons = new List<GameObject>();
  public GameObject WeaponCanvas;
  public GameObject GunController;
  InputHandler ms_InputHandler;
  private GameObject ActiveWeapon;
  private Weapon ActiveWeaponComponent;
  private GameObject ActiveWeaponCanvas;

  // Start is called before the first frame update
  void Start()
  {
    if (AvaiableWeapons.Count > 0)
    {
      ActiveWeapon = Instantiate(AvaiableWeapons[0], GunController.transform);
      ActiveWeaponCanvas = Instantiate(ActiveWeapon.GetComponent<Weapon>().GunViewPrefab, WeaponCanvas.transform);
      ActiveWeaponComponent = ActiveWeapon.GetComponent<Weapon>();
      ActiveWeapon.tag = "ActiveWeapon";
      ActiveWeaponCanvas.tag = "ActiveWeaponCanvas";
    }
  }

  // Update is called once per frame
  void Update()
  {
    HandleActiveWeapon();
  }

  public void HandleActiveWeapon()
  {
    GameObject WeaponInstace = GameObject.FindWithTag("ActiveWeapon");
    GameObject WeaponCanvasInstace = GameObject.FindWithTag("ActiveWeaponCanvas");

    if (WeaponInstace.name != ActiveWeapon.name)
    {
      Destroy(WeaponInstace);
      ActiveWeapon = Instantiate<GameObject>(AvaiableWeapons[0], GunController.transform);
      ActiveWeaponComponent = ActiveWeapon.GetComponent<Weapon>();
      ActiveWeapon.tag = "ActiveWeapon";
    }

    if (WeaponCanvasInstace.name != ActiveWeaponCanvas.name)
    {
      Destroy(WeaponCanvasInstace);
      ActiveWeaponCanvas = Instantiate(ActiveWeapon.GetComponent<Weapon>().GunViewPrefab, WeaponCanvas.transform);
      ActiveWeaponCanvas.tag = "ActiveWeaponCanvas";
    }
  }
}