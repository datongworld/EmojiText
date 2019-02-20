/*
*   修改于 东方VS清扬 的项目
*   https://github.com/DFVSQY/EmojiText
*/

using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class EmojiText : Text {

    private struct EmojiStruct
    {
        public int posIndex;
        public string des;

        public EmojiStruct(int posIndex, string des)
        {
            this.posIndex = posIndex;
            this.des = des;
        }


    }
    private EmojiData emojiData =null;
    /// <summary>
    /// emoji的空格
    /// </summary>
    private static char emSpace = '\u2001';
    private List<UIVertex> verts = new List<UIVertex>();
    private List<EmojiStruct> emojis = new List<EmojiStruct>();

    
    /// <summary>
    /// 重写
    /// </summary>
    public override string text
    {
        get
        {
            return base.text;
        }

        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                value = ParserText(value);
            }
            base.text = value;
            // sometimes when set text, OnPopulateMesh won't be called, 
            // use this statement to force OnPopulateMesh to be called 
            SetVerticesDirty();

            StartCoroutine(ShowEmoji());
        }
    }

    protected override void OnPopulateMesh(VertexHelper toFill)
    {
        base.OnPopulateMesh(toFill);
        if (emojis.Count > 0) toFill.GetUIVertexStream(verts);
    }


    private IEnumerator ShowEmoji()
    {
        if (emojiData == null)
        {
            emojiData = (EmojiData)FindObjectOfType(typeof(EmojiData));
        }
        yield return new WaitUntil(() =>
        {
            return cachedTextGenerator.vertexCount > 0;
        });

        int count = emojis.Count;
        if (count > 0 && verts.Count > 0)
        {
            // 实际显示的数量
            int c = 0 ;
            for (int i = 0; i < count; i++)
            {
                int index = emojis[i].posIndex;
                Image image = null;
                if (i >= transform.childCount)                                  // if emoji gameobject is not enough
                {
                    GameObject go = new GameObject("emoji");
                    image = go.AddComponent<Image>();
                    go.transform.SetParent(transform);
                    go.transform.localScale = Vector3.one;
                }
                else
                {
                    image = transform.GetChild(i).GetComponent<Image>();
                }
                RectTransform rt = image.rectTransform;
                rt.gameObject.SetActive(true);
                rt.sizeDelta = new Vector2(fontSize, fontSize);
                if(index * 6 < verts.Count){
                    float x = verts[index * 6].position.x + fontSize / 2;
                    float y = verts[index * 6].position.y + fontSize / 4;
                    rt.localPosition = new Vector3(x, y, 0f);
                    if (emojiData != null)
                    {
                        //image.texture = emojiData.getEmojiImg(emojis[i].des);
                        image.sprite = emojiData.getEmojiSprite(emojis[i].des);
                    }
                    c += 1 ;
                }
                
            }
            for (int i = c; i < transform.childCount; i++)
            {
                Transform ch = transform.GetChild(i);
                ch.gameObject.SetActive(false);
            }
        }
        else if (count <= 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform ch = transform.GetChild(i);
                ch.gameObject.SetActive(false);
            }
        }
        verts.Clear();
    }

    private string ParserText(string content)
    {
        emojis.Clear();
        StringBuilder sb = new StringBuilder();

        for (int i = 0;i < content.Length; i++)
        {
            string str = content[i].ToString();
            string uni = UniStr.String2Unicode(str);
            if (uni[1].ToString().Equals("E"))
            {
                // 这是emoji
                sb.Append(emSpace);
                emojis.Add(new EmojiStruct(sb.Length - 1, ConversionToLowercase(uni)));
            }
            else
            {
                // 这是文字
                sb.Append(str);
            }
        }
        return sb.ToString();
    }

    /// <summary>
	/// 大写转小写
	/// </summary>
	/// <param name="str"></param>
	/// <returns></returns>
	private string ConversionToLowercase(string str)
    {
        string stringz = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string stringx = "abcdefghijklmnopqrstuvwxyz";
        string reStr = "";
        for (int i = 0; i < str.Length; i++)
        {
            for (int j = 0; j < stringz.Length; j++)
            {
                if (str[i].Equals(stringz[j]))
                {
                    string tmp = str;
                    reStr = "";
                    for (int u = 0; u < tmp.Length; u++)
                    {
                        if (u == i)
                        {
                            reStr += stringx[j];
                        }
                        else
                        {
                            reStr += tmp[u];
                        }
                    }
                }
            }
        }
        return reStr;
    }


}
