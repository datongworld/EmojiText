using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EmojiCode  {

	public List<Code> codes ;

	public EmojiCode(){
		this.codes = new List<Code>();
		/*
		TextAsset asset = (TextAsset)Resources.Load("emojicode");
		if(asset != null){
			this.codes = JsonUtility.FromJson<JsonSerialization<Code>>(asset.text).ToList();
		}
		*/
	}

	public string GetUnified4SoftBankUnicode(string sb_unicode){
		if(this.codes!=null && this.codes.Count > 0){
			foreach(Code c in codes){
				if(sb_unicode.Equals(c.softbank)){
					return c.unified ;
				}
			}
		}
		return "";
	}


	[System.Serializable]
	public class Code{
		public int id;
		public string cls;
		public string name;
		public string unified;
		//public string docomo;
		//public string kddi;
		public string softbank;
		//public string google;
		//public string softb_unicode;
	}
}
