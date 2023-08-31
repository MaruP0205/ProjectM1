using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBufferControl : MonoBehaviour
{
    public Animator anim;
    public Transform slot_1;
    public Transform slot_2_SR;
    public Transform slot_2_LR;
    private List<int> lsGun = new List<int>();
    private Dictionary<int, GameObject> dic_wp = new Dictionary<int, GameObject>();
    // Start is called before the first frame update


    private void OnEnable()
    {
        DataTrigger.RegisterValueChange(DataPath.SLOT, UpdateSlot);

    }
    private void OnDisable()
    {
        DataTrigger.UnRegisterValueChange(DataPath.SLOT, UpdateSlot);

    }

    void Start()
    {
        lsGun = DataAPIController.instance.GetSlotEquip();
        SetGun();
    }


    private void UpdateSlot(object val)
    {
        lsGun = DataAPIController.instance.GetSlotEquip();
        SetGun();
    }
    private void SetGun()
    {
        int slot_1_ID = lsGun[0];
        int slot_2_ID = lsGun[1];
        if (!dic_wp.ContainsKey(slot_1_ID))
        {
            CreateWP(slot_1_ID);
        }
        if (!dic_wp.ContainsKey(slot_2_ID))
        {
            CreateWP(slot_2_ID);
        }
        foreach (KeyValuePair<int, GameObject> kp in dic_wp)
        {
            kp.Value.SetActive(false);
        }
        // slot 2
        GameObject slot_2_wp = dic_wp[slot_2_ID];
        if (slot_2_wp.GetComponent<WeaponBufferUI>().weaponType == WeaponType.HAND_GUN)
        {
            slot_2_wp.transform.SetParent(slot_2_SR, false);
        }
        else
        {
            slot_2_wp.transform.SetParent(slot_2_LR, false);
        }
        // slot 1
        GameObject slot_1_wp = dic_wp[slot_1_ID];
        slot_1_wp.transform.SetParent(slot_1, false);
        anim.runtimeAnimatorController = slot_1_wp.GetComponent<WeaponBufferUI>().overrideController;
        slot_2_wp.SetActive(true);
        slot_1_wp.SetActive(true);

        anim.Play("Draw");
    }
    private void CreateWP(int id)
    {
        ConfigWeaponRecord cf = ConfigManager.instance.configWeapon.GetRecordByKeySearch(id);
        GameObject wp_ob = Instantiate(Resources.Load("WeaponUI/" + cf.Prefab, typeof(GameObject))) as GameObject;
        dic_wp[id] = wp_ob;
        wp_ob.SetActive(false);
    }
}

