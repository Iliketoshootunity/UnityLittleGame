using UnityEngine;
using System.Collections;
using EasyFrameWork;

public partial class SpriteDBModel
{

    public SpriteEntity GetSpriteEntityById(int spriteId)
    {
        return m_List.Find(x => x.Id == spriteId);
    }
}
