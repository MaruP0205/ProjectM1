using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class QuestViewItem : MonoBehaviour
{
    public Image icon;
    public Text name_lb;
    public Text des_lb;
    public Text reward_lb;
    public Text number_lb;
    public GameObject check_obj;
    public Button btn_claim;
    private ConfigQuestRecord configQuest;
    private QuestData questData;

    public void Setup(ConfigQuestRecord configQuest)
    {
        this.configQuest = configQuest;
        questData = DataAPIController.instance.GetQuestData(configQuest.ID);
        icon.overrideSprite = SpriteLiblaryControl.instance.GetSpriteByName("Quest_"+configQuest.ID);
        name_lb.text = configQuest.Name;
        des_lb.text = configQuest.Description;
        reward_lb.text = configQuest.Reward.ToString();
        int number = 0;
        check_obj.SetActive(false);
        btn_claim.gameObject.SetActive(true);
        btn_claim.interactable = false;
        if (questData != null) 
        {
            number = questData.number;
            if(number >= configQuest.Number)
            {
                if (!questData.is_claimed)
                {
                    btn_claim.interactable=true;
                    check_obj.SetActive(false);

                }
                else
                {
                    check_obj.SetActive(true);
                    btn_claim.gameObject.SetActive(false);

                }
            }
        }
        number = Mathf.Clamp(number, 0, configQuest.Number);
        number_lb.text = number.ToString() +"/"+configQuest.Number.ToString();
    }

    public void ClaimQuest()
    {
        DataAPIController.instance.ClaimQuest(configQuest, (res) =>
        {
            if (res)
            {
                check_obj.SetActive(true);
                btn_claim.gameObject.SetActive(false);
            }
        });
    }
}
