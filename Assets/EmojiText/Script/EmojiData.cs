/*
*   修改于 东方VS清扬 的项目
*   https://github.com/DFVSQY/EmojiText
*/
using System.Collections.Generic;
using UnityEngine;

public class EmojiData : MonoBehaviour {

    




    /// <summary>
    /// 表情内容
    /// </summary>
    public List<EmojiSprite> emojiSprites = new List<EmojiSprite>();

    

    /// <summary>
    /// 获取图片
    /// </summary>
    /// <param name="unicode"></param>
    /// <returns></returns>
    public Sprite getEmojiSprite(string unicode)
    {
        string z = "";
        for (int x = 1; x < unicode.Length; x++)
        {
            z += unicode[x];
        }
        Sprite sprite = GetEmojiSpriteForList(z);
        if (sprite == null)
        {
            sprite = Resources.Load<Sprite>("Icon/SBUnicode/" + z);
            EmojiSprite eSprite = new EmojiSprite(z, sprite);
            emojiSprites.Add(eSprite);
        }
        return sprite;
    }

    /// <summary>
    /// 从列表中获取
    /// </summary>
    /// <param name="unicode"></param>
    /// <returns></returns>
    private Sprite GetEmojiSpriteForList(string unicode)
    {
        for (int i = 0; i < emojiSprites.Count; i++)
        {
            if (emojiSprites[i].unicode.Equals(unicode))
            {
                return emojiSprites[i].sprite;
            }
        }
        return null;
    }

  

    [System.Serializable]
    public struct EmojiSprite
    {
        public string unicode;
        public Sprite sprite;

        public EmojiSprite(string u, Sprite s)
        {
            this.unicode = u;
            this.sprite = s;
        }
    }

    

}
