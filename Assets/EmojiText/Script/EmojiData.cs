/*
*   修改于 东方VS清扬 的项目
*   https://github.com/DFVSQY/EmojiText
*/
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;

public class EmojiData : MonoBehaviour {

    public enum EmojiSize
    {
        Image32=0,Image64=1,Image128=2
    }

    public EmojiSize emojiSize = EmojiSize.Image64 ;

    private string[] imageSize = new string[3]{"32/","64/","128/"} ;
    private EmojiCode emojiCode ;

    private List<EmojiTexture> emojiTexture = new List<EmojiTexture>();


    void Awake()
    {
        TextAsset asset = (TextAsset)Resources.Load("emoji");
		if(asset != null){
			emojiCode = JsonUtility.FromJson<EmojiCode>(asset.text);
		}
    }

    public Texture2D getEmojiTexture(string unicode){
        Texture2D tex = null ;
        for (int i = 0; i < emojiTexture.Count; i++)
        {
            if (emojiTexture[i].unicode.Equals(unicode))
            {
                tex = emojiTexture[i].texture;
                break ;
            }
        }
        if(tex == null){
            unicode = emojiCode.GetUnified4SoftBankUnicode(unicode);
            tex = Resources.Load<Texture2D>("Icon/Unified/Emojione/" +imageSize[(int)emojiSize] + unicode);
        }
        return tex ;
    }
    


    [System.Serializable]
    public struct EmojiTexture{
        public string unicode;
        public Texture2D texture;

        public EmojiTexture(string u, Texture2D t)
        {
            this.unicode = u;
            this.texture = t;
        }
    }

    

}
