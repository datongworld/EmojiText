/*
*
* 	Used
*	string str = JsonUtility.ToJson(new Serialization<Enemy>(enemies));
*	输出 : {"target":[{"name":"怪物1,"skills":["攻击"]},{"name":"怪物2","skills":["攻击","恢复"]}]}
*	string str = JsonUtility.ToJson(new Serialization<int, Enemy>(enemies));
*	输出 : {"keys":[1000,2000],"values":[{"name":"怪物1","skills":["攻击"]},{"name":"怪物2","skills":["攻击","恢复"]}]}
*
*	List<Enemy> enemies = JsonUtility.FromJson<Serialization<Enemy>>(str).ToList();
*	Dictionary<int, Enemy> enemies = JsonUtility.FromJson<Serialization<int, Enemy>>(str).ToDictionary();
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class JsonSerialization <T> {

	[SerializeField]
	List<T> target;
	public List<T> ToList() { return target ; }
	public JsonSerialization(List<T> target){
		this.target = target ;
	}
}

[Serializable]
public class JsonSerialization<TKey,TValue> : ISerializationCallbackReceiver{
	[SerializeField]
    List<TKey> keys;
    [SerializeField]
    List<TValue> values;
 
    Dictionary<TKey, TValue> target;
    public Dictionary<TKey, TValue> ToDictionary() { return target; }
 
    public JsonSerialization(Dictionary<TKey, TValue> target)
    {
        this.target = target;
    }
 
    public void OnBeforeSerialize()
    {
        keys = new List<TKey>(target.Keys);
        values = new List<TValue>(target.Values);
    }
 
    public void OnAfterDeserialize()
    {
        var count = Math.Min(keys.Count, values.Count);
        target = new Dictionary<TKey, TValue>(count);
        for (var i = 0; i < count; ++i)
        {
            target.Add(keys[i], values[i]);
        }
	}
}