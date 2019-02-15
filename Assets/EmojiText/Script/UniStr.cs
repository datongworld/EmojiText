/// <summary>
/// 字符串与Unicode 之间的转换
/// author : datongworld@hotmail.com
/// </summary>

using System.Collections;
using UnityEngine;
using System.Text;
using System;
using System.Globalization;

public static class UniStr {

	/// <summary>
	/// 字符串转Unicode码
	/// </summary>
	/// <param name="str">字符串</param>
	/// <returns>Unicode码 字符串</returns>
	public static string String2Unicode(string str){
		char[] charBuffers = str.ToCharArray();
		byte[] buffers ;
		StringBuilder stringBuilder = new StringBuilder();
		for(int i=0; i <charBuffers.Length;i++){
			buffers = System.Text.Encoding.Unicode.GetBytes(charBuffers[i].ToString());
			if(i==0){
				stringBuilder.Append(String.Format("u{0:X2}{1:X2}",buffers[1],buffers[0]));
			}else{
				stringBuilder.Append(String.Format("/u{0:X2}{1:X2}", buffers[1], buffers[0]));
			}
		}
		return stringBuilder.ToString();
	}

	/// <summary>
	/// Unicode码转字符串
	/// </summary>
	/// <param name="uni">uni码字符串</param>
	/// <returns>字符串</returns>
	public static string Unicode2String(string uni){
		string dst = "";
		string src = uni;
		int len = uni.Length/6;
		for(int i=0; i < len-1;i++){
			string str = "";
			str = src.Substring(0, 6).Substring(2);  
            src = src.Substring(6);  
            byte[] bytes = new byte[2];  
            bytes[1] = byte.Parse(int.Parse(str.Substring(0, 2), NumberStyles.HexNumber).ToString());  
            bytes[0] = byte.Parse(int.Parse(str.Substring(2, 2), NumberStyles.HexNumber).ToString());  
            dst += Encoding.Unicode.GetString(bytes); 
		}
		return dst;
	}

	/// <summary>
	/// 字体大小
	/// </summary>
	/// <param name="str"></param>
	/// <param name="font"></param>
	/// <param name="fontStyle"></param>
	/// <param name="fontSize"></param>
	/// <returns></returns>
	public static float FontSize(string str,Font font,FontStyle fontStyle, float fontSize){
		float totalLength = 0 ;
		int fs = (int)(fontSize);
		font.RequestCharactersInTexture(str, fs, fontStyle);
		CharacterInfo characterInfo = new CharacterInfo();
		char[] arr = str.ToCharArray();
		foreach (char c in arr){
    		font.GetCharacterInfo(c, out characterInfo, fs);
    		totalLength += characterInfo.advance;
		}
		return totalLength ;
	}
	
}
