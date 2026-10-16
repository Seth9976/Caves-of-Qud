using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;

namespace LitJson
{
	// Token: 0x0200031B RID: 795
	public class JsonData : IJsonWrapper, IList, ICollection, IEnumerable, IOrderedDictionary, IDictionary, IEquatable<JsonData>
	{
		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06001D36 RID: 7478 RVA: 0x000902DF File Offset: 0x0008E4DF
		public int Count
		{
			get
			{
				return this.EnsureCollection().Count;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001D37 RID: 7479 RVA: 0x000902EC File Offset: 0x0008E4EC
		public bool IsArray
		{
			get
			{
				return this.type == JsonType.Array;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06001D38 RID: 7480 RVA: 0x000902F7 File Offset: 0x0008E4F7
		public bool IsBoolean
		{
			get
			{
				return this.type == JsonType.Boolean;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06001D39 RID: 7481 RVA: 0x00090302 File Offset: 0x0008E502
		public bool IsDouble
		{
			get
			{
				return this.type == JsonType.Double;
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06001D3A RID: 7482 RVA: 0x0009030D File Offset: 0x0008E50D
		public bool IsInt
		{
			get
			{
				return this.type == JsonType.Int;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06001D3B RID: 7483 RVA: 0x00090318 File Offset: 0x0008E518
		public bool IsLong
		{
			get
			{
				return this.type == JsonType.Long;
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06001D3C RID: 7484 RVA: 0x00090323 File Offset: 0x0008E523
		public bool IsObject
		{
			get
			{
				return this.type == JsonType.Object;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06001D3D RID: 7485 RVA: 0x0009032E File Offset: 0x0008E52E
		public bool IsString
		{
			get
			{
				return this.type == JsonType.String;
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06001D3E RID: 7486 RVA: 0x00090339 File Offset: 0x0008E539
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001D3F RID: 7487 RVA: 0x00090341 File Offset: 0x0008E541
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.EnsureCollection().IsSynchronized;
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001D40 RID: 7488 RVA: 0x0009034E File Offset: 0x0008E54E
		object ICollection.SyncRoot
		{
			get
			{
				return this.EnsureCollection().SyncRoot;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001D41 RID: 7489 RVA: 0x0009035B File Offset: 0x0008E55B
		bool IDictionary.IsFixedSize
		{
			get
			{
				return this.EnsureDictionary().IsFixedSize;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001D42 RID: 7490 RVA: 0x00090368 File Offset: 0x0008E568
		bool IDictionary.IsReadOnly
		{
			get
			{
				return this.EnsureDictionary().IsReadOnly;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001D43 RID: 7491 RVA: 0x00090378 File Offset: 0x0008E578
		ICollection IDictionary.Keys
		{
			get
			{
				this.EnsureDictionary();
				IList<string> list = new List<string>();
				foreach (KeyValuePair<string, JsonData> keyValuePair in this.object_list)
				{
					list.Add(keyValuePair.Key);
				}
				return (ICollection)list;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001D44 RID: 7492 RVA: 0x000903E0 File Offset: 0x0008E5E0
		ICollection IDictionary.Values
		{
			get
			{
				this.EnsureDictionary();
				IList<JsonData> list = new List<JsonData>();
				foreach (KeyValuePair<string, JsonData> keyValuePair in this.object_list)
				{
					list.Add(keyValuePair.Value);
				}
				return (ICollection)list;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001D45 RID: 7493 RVA: 0x00090448 File Offset: 0x0008E648
		bool IJsonWrapper.IsArray
		{
			get
			{
				return this.IsArray;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001D46 RID: 7494 RVA: 0x00090450 File Offset: 0x0008E650
		bool IJsonWrapper.IsBoolean
		{
			get
			{
				return this.IsBoolean;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001D47 RID: 7495 RVA: 0x00090458 File Offset: 0x0008E658
		bool IJsonWrapper.IsDouble
		{
			get
			{
				return this.IsDouble;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001D48 RID: 7496 RVA: 0x00090460 File Offset: 0x0008E660
		bool IJsonWrapper.IsInt
		{
			get
			{
				return this.IsInt;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001D49 RID: 7497 RVA: 0x00090468 File Offset: 0x0008E668
		bool IJsonWrapper.IsLong
		{
			get
			{
				return this.IsLong;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001D4A RID: 7498 RVA: 0x00090470 File Offset: 0x0008E670
		bool IJsonWrapper.IsObject
		{
			get
			{
				return this.IsObject;
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001D4B RID: 7499 RVA: 0x00090478 File Offset: 0x0008E678
		bool IJsonWrapper.IsString
		{
			get
			{
				return this.IsString;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001D4C RID: 7500 RVA: 0x00090480 File Offset: 0x0008E680
		bool IList.IsFixedSize
		{
			get
			{
				return this.EnsureList().IsFixedSize;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001D4D RID: 7501 RVA: 0x0009048D File Offset: 0x0008E68D
		bool IList.IsReadOnly
		{
			get
			{
				return this.EnsureList().IsReadOnly;
			}
		}

		// Token: 0x170003FC RID: 1020
		object IDictionary.this[object key]
		{
			get
			{
				return this.EnsureDictionary()[key];
			}
			set
			{
				if (!(key is string))
				{
					throw new ArgumentException("The key has to be a string");
				}
				JsonData jsonData = this.ToJsonData(value);
				this[(string)key] = jsonData;
			}
		}

		// Token: 0x170003FD RID: 1021
		object IOrderedDictionary.this[int idx]
		{
			get
			{
				this.EnsureDictionary();
				return this.object_list[idx].Value;
			}
			set
			{
				this.EnsureDictionary();
				JsonData jsonData = this.ToJsonData(value);
				KeyValuePair<string, JsonData> keyValuePair = this.object_list[idx];
				this.inst_object[keyValuePair.Key] = jsonData;
				KeyValuePair<string, JsonData> keyValuePair2 = new KeyValuePair<string, JsonData>(keyValuePair.Key, jsonData);
				this.object_list[idx] = keyValuePair2;
			}
		}

		// Token: 0x170003FE RID: 1022
		object IList.this[int index]
		{
			get
			{
				return this.EnsureList()[index];
			}
			set
			{
				this.EnsureList();
				JsonData jsonData = this.ToJsonData(value);
				this[index] = jsonData;
			}
		}

		// Token: 0x170003FF RID: 1023
		public JsonData this[string prop_name]
		{
			get
			{
				this.EnsureDictionary();
				return this.inst_object[prop_name];
			}
			set
			{
				this.EnsureDictionary();
				KeyValuePair<string, JsonData> keyValuePair = new KeyValuePair<string, JsonData>(prop_name, value);
				if (this.inst_object.ContainsKey(prop_name))
				{
					for (int i = 0; i < this.object_list.Count; i++)
					{
						if (this.object_list[i].Key == prop_name)
						{
							this.object_list[i] = keyValuePair;
							break;
						}
					}
				}
				else
				{
					this.object_list.Add(keyValuePair);
				}
				this.inst_object[prop_name] = value;
				this.json = null;
			}
		}

		// Token: 0x17000400 RID: 1024
		public JsonData this[int index]
		{
			get
			{
				this.EnsureCollection();
				if (this.type == JsonType.Array)
				{
					return this.inst_array[index];
				}
				return this.object_list[index].Value;
			}
			set
			{
				this.EnsureCollection();
				if (this.type == JsonType.Array)
				{
					this.inst_array[index] = value;
				}
				else
				{
					KeyValuePair<string, JsonData> keyValuePair = this.object_list[index];
					KeyValuePair<string, JsonData> keyValuePair2 = new KeyValuePair<string, JsonData>(keyValuePair.Key, value);
					this.object_list[index] = keyValuePair2;
					this.inst_object[keyValuePair.Key] = value;
				}
				this.json = null;
			}
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x000906EB File Offset: 0x0008E8EB
		public JsonData()
		{
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x000906F3 File Offset: 0x0008E8F3
		public JsonData(bool boolean)
		{
			this.type = JsonType.Boolean;
			this.inst_boolean = boolean;
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x00090709 File Offset: 0x0008E909
		public JsonData(double number)
		{
			this.type = JsonType.Double;
			this.inst_double = number;
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x0009071F File Offset: 0x0008E91F
		public JsonData(int number)
		{
			this.type = JsonType.Int;
			this.inst_int = number;
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x00090735 File Offset: 0x0008E935
		public JsonData(long number)
		{
			this.type = JsonType.Long;
			this.inst_long = number;
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x0009074C File Offset: 0x0008E94C
		public JsonData(object obj)
		{
			if (obj is bool)
			{
				this.type = JsonType.Boolean;
				this.inst_boolean = (bool)obj;
				return;
			}
			if (obj is double)
			{
				this.type = JsonType.Double;
				this.inst_double = (double)obj;
				return;
			}
			if (obj is int)
			{
				this.type = JsonType.Int;
				this.inst_int = (int)obj;
				return;
			}
			if (obj is long)
			{
				this.type = JsonType.Long;
				this.inst_long = (long)obj;
				return;
			}
			if (obj is string)
			{
				this.type = JsonType.String;
				this.inst_string = (string)obj;
				return;
			}
			throw new ArgumentException("Unable to wrap the given object with JsonData");
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x000907F5 File Offset: 0x0008E9F5
		public JsonData(string str)
		{
			this.type = JsonType.String;
			this.inst_string = str;
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x0009080B File Offset: 0x0008EA0B
		public static implicit operator JsonData(bool data)
		{
			return new JsonData(data);
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x00090813 File Offset: 0x0008EA13
		public static implicit operator JsonData(double data)
		{
			return new JsonData(data);
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x0009081B File Offset: 0x0008EA1B
		public static implicit operator JsonData(int data)
		{
			return new JsonData(data);
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x00090823 File Offset: 0x0008EA23
		public static implicit operator JsonData(long data)
		{
			return new JsonData(data);
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x0009082B File Offset: 0x0008EA2B
		public static implicit operator JsonData(string data)
		{
			return new JsonData(data);
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x00090833 File Offset: 0x0008EA33
		public static explicit operator bool(JsonData data)
		{
			if (data.type != JsonType.Boolean)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold a double");
			}
			return data.inst_boolean;
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x0009084F File Offset: 0x0008EA4F
		public static explicit operator double(JsonData data)
		{
			if (data.type != JsonType.Double)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold a double");
			}
			return data.inst_double;
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x0009086B File Offset: 0x0008EA6B
		public static explicit operator int(JsonData data)
		{
			if (data.type != JsonType.Int)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold an int");
			}
			return data.inst_int;
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x00090887 File Offset: 0x0008EA87
		public static explicit operator long(JsonData data)
		{
			if (data.type != JsonType.Long)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold an int");
			}
			return data.inst_long;
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x000908A3 File Offset: 0x0008EAA3
		public static explicit operator string(JsonData data)
		{
			if (data.type != JsonType.String)
			{
				throw new InvalidCastException("Instance of JsonData doesn't hold a string");
			}
			return data.inst_string;
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x000908BF File Offset: 0x0008EABF
		void ICollection.CopyTo(Array array, int index)
		{
			this.EnsureCollection().CopyTo(array, index);
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x000908D0 File Offset: 0x0008EAD0
		void IDictionary.Add(object key, object value)
		{
			JsonData jsonData = this.ToJsonData(value);
			this.EnsureDictionary().Add(key, jsonData);
			KeyValuePair<string, JsonData> keyValuePair = new KeyValuePair<string, JsonData>((string)key, jsonData);
			this.object_list.Add(keyValuePair);
			this.json = null;
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x00090913 File Offset: 0x0008EB13
		void IDictionary.Clear()
		{
			this.EnsureDictionary().Clear();
			this.object_list.Clear();
			this.json = null;
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x00090932 File Offset: 0x0008EB32
		bool IDictionary.Contains(object key)
		{
			return this.EnsureDictionary().Contains(key);
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x00090940 File Offset: 0x0008EB40
		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return ((IOrderedDictionary)this).GetEnumerator();
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x00090948 File Offset: 0x0008EB48
		void IDictionary.Remove(object key)
		{
			this.EnsureDictionary().Remove(key);
			for (int i = 0; i < this.object_list.Count; i++)
			{
				if (this.object_list[i].Key == (string)key)
				{
					this.object_list.RemoveAt(i);
					break;
				}
			}
			this.json = null;
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x000909AD File Offset: 0x0008EBAD
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.EnsureCollection().GetEnumerator();
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x000909BA File Offset: 0x0008EBBA
		bool IJsonWrapper.GetBoolean()
		{
			if (this.type != JsonType.Boolean)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a boolean");
			}
			return this.inst_boolean;
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x000909D6 File Offset: 0x0008EBD6
		double IJsonWrapper.GetDouble()
		{
			if (this.type != JsonType.Double)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a double");
			}
			return this.inst_double;
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x000909F2 File Offset: 0x0008EBF2
		int IJsonWrapper.GetInt()
		{
			if (this.type != JsonType.Int)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold an int");
			}
			return this.inst_int;
		}

		// Token: 0x06001D73 RID: 7539 RVA: 0x00090A0E File Offset: 0x0008EC0E
		long IJsonWrapper.GetLong()
		{
			if (this.type != JsonType.Long)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a long");
			}
			return this.inst_long;
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x00090A2A File Offset: 0x0008EC2A
		string IJsonWrapper.GetString()
		{
			if (this.type != JsonType.String)
			{
				throw new InvalidOperationException("JsonData instance doesn't hold a string");
			}
			return this.inst_string;
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x00090A46 File Offset: 0x0008EC46
		void IJsonWrapper.SetBoolean(bool val)
		{
			this.type = JsonType.Boolean;
			this.inst_boolean = val;
			this.json = null;
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x00090A5D File Offset: 0x0008EC5D
		void IJsonWrapper.SetDouble(double val)
		{
			this.type = JsonType.Double;
			this.inst_double = val;
			this.json = null;
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x00090A74 File Offset: 0x0008EC74
		void IJsonWrapper.SetInt(int val)
		{
			this.type = JsonType.Int;
			this.inst_int = val;
			this.json = null;
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x00090A8B File Offset: 0x0008EC8B
		void IJsonWrapper.SetLong(long val)
		{
			this.type = JsonType.Long;
			this.inst_long = val;
			this.json = null;
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x00090AA2 File Offset: 0x0008ECA2
		void IJsonWrapper.SetString(string val)
		{
			this.type = JsonType.String;
			this.inst_string = val;
			this.json = null;
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x00090AB9 File Offset: 0x0008ECB9
		string IJsonWrapper.ToJson()
		{
			return this.ToJson();
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x00090AC1 File Offset: 0x0008ECC1
		void IJsonWrapper.ToJson(JsonWriter writer)
		{
			this.ToJson(writer);
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x00090ACA File Offset: 0x0008ECCA
		int IList.Add(object value)
		{
			return this.Add(value);
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x00090AD3 File Offset: 0x0008ECD3
		void IList.Clear()
		{
			this.EnsureList().Clear();
			this.json = null;
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x00090AE7 File Offset: 0x0008ECE7
		bool IList.Contains(object value)
		{
			return this.EnsureList().Contains(value);
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x00090AF5 File Offset: 0x0008ECF5
		int IList.IndexOf(object value)
		{
			return this.EnsureList().IndexOf(value);
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x00090B03 File Offset: 0x0008ED03
		void IList.Insert(int index, object value)
		{
			this.EnsureList().Insert(index, value);
			this.json = null;
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x00090B19 File Offset: 0x0008ED19
		void IList.Remove(object value)
		{
			this.EnsureList().Remove(value);
			this.json = null;
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x00090B2E File Offset: 0x0008ED2E
		void IList.RemoveAt(int index)
		{
			this.EnsureList().RemoveAt(index);
			this.json = null;
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x00090B43 File Offset: 0x0008ED43
		IDictionaryEnumerator IOrderedDictionary.GetEnumerator()
		{
			this.EnsureDictionary();
			return new OrderedDictionaryEnumerator(this.object_list.GetEnumerator());
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x00090B5C File Offset: 0x0008ED5C
		void IOrderedDictionary.Insert(int idx, object key, object value)
		{
			string text = (string)key;
			JsonData jsonData = this.ToJsonData(value);
			this[text] = jsonData;
			KeyValuePair<string, JsonData> keyValuePair = new KeyValuePair<string, JsonData>(text, jsonData);
			this.object_list.Insert(idx, keyValuePair);
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x00090B98 File Offset: 0x0008ED98
		void IOrderedDictionary.RemoveAt(int idx)
		{
			this.EnsureDictionary();
			this.inst_object.Remove(this.object_list[idx].Key);
			this.object_list.RemoveAt(idx);
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x00090BD8 File Offset: 0x0008EDD8
		private ICollection EnsureCollection()
		{
			if (this.type == JsonType.Array)
			{
				return (ICollection)this.inst_array;
			}
			if (this.type == JsonType.Object)
			{
				return (ICollection)this.inst_object;
			}
			throw new InvalidOperationException("The JsonData instance has to be initialized first");
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x00090C10 File Offset: 0x0008EE10
		private IDictionary EnsureDictionary()
		{
			if (this.type == JsonType.Object)
			{
				return (IDictionary)this.inst_object;
			}
			if (this.type != JsonType.None)
			{
				throw new InvalidOperationException("Instance of JsonData is not a dictionary");
			}
			this.type = JsonType.Object;
			this.inst_object = new Dictionary<string, JsonData>();
			this.object_list = new List<KeyValuePair<string, JsonData>>();
			return (IDictionary)this.inst_object;
		}

		// Token: 0x06001D88 RID: 7560 RVA: 0x00090C70 File Offset: 0x0008EE70
		private IList EnsureList()
		{
			if (this.type == JsonType.Array)
			{
				return (IList)this.inst_array;
			}
			if (this.type != JsonType.None)
			{
				throw new InvalidOperationException("Instance of JsonData is not a list");
			}
			this.type = JsonType.Array;
			this.inst_array = new List<JsonData>();
			return (IList)this.inst_array;
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x00090CC2 File Offset: 0x0008EEC2
		private JsonData ToJsonData(object obj)
		{
			if (obj == null)
			{
				return null;
			}
			if (obj is JsonData)
			{
				return (JsonData)obj;
			}
			return new JsonData(obj);
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x00090CE0 File Offset: 0x0008EEE0
		private static void WriteJson(IJsonWrapper obj, JsonWriter writer)
		{
			if (obj == null)
			{
				writer.Write(null);
				return;
			}
			if (obj.IsString)
			{
				writer.Write(obj.GetString());
				return;
			}
			if (obj.IsBoolean)
			{
				writer.Write(obj.GetBoolean());
				return;
			}
			if (obj.IsDouble)
			{
				writer.Write(obj.GetDouble());
				return;
			}
			if (obj.IsInt)
			{
				writer.Write(obj.GetInt());
				return;
			}
			if (obj.IsLong)
			{
				writer.Write(obj.GetLong());
				return;
			}
			if (obj.IsArray)
			{
				writer.WriteArrayStart();
				foreach (object obj2 in obj)
				{
					JsonData.WriteJson((JsonData)obj2, writer);
				}
				writer.WriteArrayEnd();
				return;
			}
			if (obj.IsObject)
			{
				writer.WriteObjectStart();
				foreach (object obj3 in obj)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj3;
					writer.WritePropertyName((string)dictionaryEntry.Key);
					JsonData.WriteJson((JsonData)dictionaryEntry.Value, writer);
				}
				writer.WriteObjectEnd();
				return;
			}
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x00090E30 File Offset: 0x0008F030
		public int Add(object value)
		{
			JsonData jsonData = this.ToJsonData(value);
			this.json = null;
			return this.EnsureList().Add(jsonData);
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x00090E58 File Offset: 0x0008F058
		public void Clear()
		{
			if (this.IsObject)
			{
				((IDictionary)this).Clear();
				return;
			}
			if (this.IsArray)
			{
				((IList)this).Clear();
				return;
			}
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x00090E78 File Offset: 0x0008F078
		public bool Equals(JsonData x)
		{
			if (x == null)
			{
				return false;
			}
			if (x.type != this.type)
			{
				return false;
			}
			switch (this.type)
			{
			case JsonType.None:
				return true;
			case JsonType.Object:
				return this.inst_object.Equals(x.inst_object);
			case JsonType.Array:
				return this.inst_array.Equals(x.inst_array);
			case JsonType.String:
				return this.inst_string.Equals(x.inst_string);
			case JsonType.Int:
				return this.inst_int.Equals(x.inst_int);
			case JsonType.Long:
				return this.inst_long.Equals(x.inst_long);
			case JsonType.Double:
				return this.inst_double.Equals(x.inst_double);
			case JsonType.Boolean:
				return this.inst_boolean.Equals(x.inst_boolean);
			default:
				return false;
			}
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x00090F4D File Offset: 0x0008F14D
		public JsonType GetJsonType()
		{
			return this.type;
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x00090F58 File Offset: 0x0008F158
		public void SetJsonType(JsonType type)
		{
			if (this.type == type)
			{
				return;
			}
			switch (type)
			{
			case JsonType.Object:
				this.inst_object = new Dictionary<string, JsonData>();
				this.object_list = new List<KeyValuePair<string, JsonData>>();
				break;
			case JsonType.Array:
				this.inst_array = new List<JsonData>();
				break;
			case JsonType.String:
				this.inst_string = null;
				break;
			case JsonType.Int:
				this.inst_int = 0;
				break;
			case JsonType.Long:
				this.inst_long = 0L;
				break;
			case JsonType.Double:
				this.inst_double = 0.0;
				break;
			case JsonType.Boolean:
				this.inst_boolean = false;
				break;
			}
			this.type = type;
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x00090FF8 File Offset: 0x0008F1F8
		public string ToJson()
		{
			if (this.json != null)
			{
				return this.json;
			}
			StringWriter stringWriter = new StringWriter();
			JsonData.WriteJson(this, new JsonWriter(stringWriter)
			{
				Validate = false
			});
			this.json = stringWriter.ToString();
			return this.json;
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x00091044 File Offset: 0x0008F244
		public void ToJson(JsonWriter writer)
		{
			bool validate = writer.Validate;
			writer.Validate = false;
			JsonData.WriteJson(this, writer);
			writer.Validate = validate;
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x00091070 File Offset: 0x0008F270
		public override string ToString()
		{
			switch (this.type)
			{
			case JsonType.Object:
				return "JsonData object";
			case JsonType.Array:
				return "JsonData array";
			case JsonType.String:
				return this.inst_string;
			case JsonType.Int:
				return this.inst_int.ToString();
			case JsonType.Long:
				return this.inst_long.ToString();
			case JsonType.Double:
				return this.inst_double.ToString();
			case JsonType.Boolean:
				return this.inst_boolean.ToString();
			default:
				return "Uninitialized JsonData";
			}
		}

		// Token: 0x04000F78 RID: 3960
		private IList<JsonData> inst_array;

		// Token: 0x04000F79 RID: 3961
		private bool inst_boolean;

		// Token: 0x04000F7A RID: 3962
		private double inst_double;

		// Token: 0x04000F7B RID: 3963
		private int inst_int;

		// Token: 0x04000F7C RID: 3964
		private long inst_long;

		// Token: 0x04000F7D RID: 3965
		private IDictionary<string, JsonData> inst_object;

		// Token: 0x04000F7E RID: 3966
		private string inst_string;

		// Token: 0x04000F7F RID: 3967
		private string json;

		// Token: 0x04000F80 RID: 3968
		private JsonType type;

		// Token: 0x04000F81 RID: 3969
		private IList<KeyValuePair<string, JsonData>> object_list;
	}
}
