using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ChangeGunHandle(WeaponBehavior weapon);
public class WeaponControl : MonoBehaviour
{
    private int index = -1;
    public Transform parentGun;
    public List<int> id_guns;
    public List<WeaponBehavior> guns;
    [SerializeField]
    private WeaponBehavior cur_Weapon;
    [SerializeField]
    private CharacterDataBinding dataBinding;
    public event ChangeGunHandle OnChangeGun;
    [SerializeField]
    private CharacterControl characterControl;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
        id_guns = DataAPIController.instance.GetSlotEquip();
        foreach (int wp_id in id_guns)
        {
            ConfigWeaponRecord configWeapon = ConfigManager.instance.configWeapon.GetRecordByKeySearch(wp_id);
            GameObject wp_object = Instantiate(Resources.Load("Weapon/"+configWeapon.Prefab,typeof(GameObject))) as GameObject;
            wp_object.transform.SetParent(parentGun, false);
            WeaponBehavior wp = wp_object.GetComponent<WeaponBehavior>();
            ConfigWeaponRecord cf_wp = ConfigManager.instance.configWeapon.GetRecordByKeySearch(wp_id);
            wp.Setup(new GunData{dataBinding = this.dataBinding, cf_weapon = cf_wp, characterControl = characterControl});
            wp.gameObject.SetActive(false);
            guns.Add(wp);
        }
        ChangeGun();
    }
    public void ChangeGun()
    {
        index++;
        if(index >= guns.Count) 
        {
            index = 0;
        }
        cur_Weapon?.OnHideGun();
        cur_Weapon?.gameObject.SetActive(false);
        cur_Weapon = guns[index];
        cur_Weapon.gameObject.SetActive(true);
        cur_Weapon.OnDrawGun();
        if (OnChangeGun != null)
        {
            OnChangeGun(cur_Weapon);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeGun();
        }
    }
}
