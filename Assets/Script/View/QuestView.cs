using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestView : View
{
    public QuestViewItem prefab_;
    public Transform parent_item;
    private List<QuestViewItem> items = new List<QuestViewItem>();
    public override void Setup(ViewParam param)
    {
        base.Setup(param);
        List<ConfigQuestRecord> cf_records = ConfigManager.instance.configQuest.GetAllRecords();

        if (items.Count <= 0)
        {
            for (int i = 0; i < cf_records.Count; i++)
            {
                QuestViewItem item = Instantiate(prefab_);
                items.Add(item);
                item.transform.SetParent(parent_item, false);
            }
        }
        for (int i = 0; i < cf_records.Count; i++)
        {
            items[i].Setup(cf_records[i]);

        }
    }
}
