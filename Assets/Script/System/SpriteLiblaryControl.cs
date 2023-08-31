using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteLiblaryControl : BYSingletonMono<SpriteLiblaryControl> 
{
    [SerializeField]
    private List<Sprite> ls_sprites;
    private Dictionary<string, Sprite> dic = new Dictionary<string, Sprite>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (Sprite sp in ls_sprites)
        {
            dic.Add(sp.name, sp);
        }
    }

    public Sprite GetSpriteByName(string name)
    {
        return dic[name];
    }
}
