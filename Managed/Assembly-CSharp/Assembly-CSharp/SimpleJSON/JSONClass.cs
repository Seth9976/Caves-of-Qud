using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SimpleJSON
{
	// Token: 0x0200035B RID: 859
	public class JSONClass : JSONNode, IEnumerable
	{
		// Token: 0x06001FF7 RID: 8183 RVA: 0x0009D250 File Offset: 0x0009B450
		public List<string> GetKeys()
		{
			List<string> list = new List<string>();
			foreach (string text in this.m_Dict.Keys)
			{
				list.Add(text.Replace("\"", ""));
			}
			return list;
		}

		// Token: 0x1700044C RID: 1100
		public override JSONNode this[string aKey]
		{
			get
			{
				if (this.m_Dict.ContainsKey(aKey))
				{
					return this.m_Dict[aKey];
				}
				return new JSONLazyCreator(this, aKey);
			}
			set
			{
				if (this.m_Dict.ContainsKey(aKey))
				{
					value.Key = aKey;
					this.m_Dict[aKey] = value;
					return;
				}
				value.Key = aKey;
				this.m_Dict.Add(aKey, value);
			}
		}

		// Token: 0x1700044D RID: 1101
		public override JSONNode this[int aIndex]
		{
			get
			{
				if (aIndex < 0 || aIndex >= this.m_Dict.Count)
				{
					return null;
				}
				return this.m_Dict.ElementAt<KeyValuePair<string, JSONNode>>(aIndex).Value;
			}
			set
			{
				if (aIndex < 0 || aIndex >= this.m_Dict.Count)
				{
					return;
				}
				string key = this.m_Dict.ElementAt<KeyValuePair<string, JSONNode>>(aIndex).Key;
				this.m_Dict[key] = value;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001FFC RID: 8188 RVA: 0x0009D39A File Offset: 0x0009B59A
		public override int Count
		{
			get
			{
				return this.m_Dict.Count;
			}
		}

		// Token: 0x06001FFD RID: 8189 RVA: 0x0009D3A8 File Offset: 0x0009B5A8
		public override void Add(string aKey, JSONNode aItem)
		{
			if (string.IsNullOrEmpty(aKey))
			{
				string text = Guid.NewGuid().ToString();
				aItem.Key = text;
				this.m_Dict.Add(text, aItem);
				return;
			}
			if (this.m_Dict.ContainsKey(aKey))
			{
				aItem.Key = aKey;
				this.m_Dict[aKey] = aItem;
				return;
			}
			aItem.Key = aKey;
			this.m_Dict.Add(aKey, aItem);
		}

		// Token: 0x06001FFE RID: 8190 RVA: 0x0009D41D File Offset: 0x0009B61D
		public override JSONNode Remove(string aKey)
		{
			if (!this.m_Dict.ContainsKey(aKey))
			{
				return null;
			}
			JSONNode jsonnode = this.m_Dict[aKey];
			this.m_Dict.Remove(aKey);
			return jsonnode;
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x0009D448 File Offset: 0x0009B648
		public override JSONNode Remove(int aIndex)
		{
			if (aIndex < 0 || aIndex >= this.m_Dict.Count)
			{
				return null;
			}
			KeyValuePair<string, JSONNode> keyValuePair = this.m_Dict.ElementAt<KeyValuePair<string, JSONNode>>(aIndex);
			this.m_Dict.Remove(keyValuePair.Key);
			return keyValuePair.Value;
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x0009D490 File Offset: 0x0009B690
		public override JSONNode Remove(JSONNode aNode)
		{
			JSONNode jsonnode;
			try
			{
				KeyValuePair<string, JSONNode> keyValuePair = this.m_Dict.Where<KeyValuePair<string, JSONNode>>((KeyValuePair<string, JSONNode> k) => k.Value == aNode).First<KeyValuePair<string, JSONNode>>();
				this.m_Dict.Remove(keyValuePair.Key);
				jsonnode = aNode;
			}
			catch
			{
				jsonnode = null;
			}
			return jsonnode;
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06002001 RID: 8193 RVA: 0x0009D4FC File Offset: 0x0009B6FC
		public IEnumerable<KeyValuePair<string, JSONNode>> ChildNodes
		{
			get
			{
				foreach (KeyValuePair<string, JSONNode> keyValuePair in this.m_Dict)
				{
					yield return keyValuePair;
				}
				Dictionary<string, JSONNode>.Enumerator enumerator = default(Dictionary<string, JSONNode>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06002002 RID: 8194 RVA: 0x0009D50C File Offset: 0x0009B70C
		public override IEnumerable<JSONNode> Childs
		{
			get
			{
				foreach (KeyValuePair<string, JSONNode> keyValuePair in this.m_Dict)
				{
					yield return keyValuePair.Value;
				}
				Dictionary<string, JSONNode>.Enumerator enumerator = default(Dictionary<string, JSONNode>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x06002003 RID: 8195 RVA: 0x0009D51C File Offset: 0x0009B71C
		public IEnumerator GetEnumerator()
		{
			foreach (KeyValuePair<string, JSONNode> keyValuePair in this.m_Dict)
			{
				yield return keyValuePair;
			}
			Dictionary<string, JSONNode>.Enumerator enumerator = default(Dictionary<string, JSONNode>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06002004 RID: 8196 RVA: 0x0009D52C File Offset: 0x0009B72C
		public override string ToString()
		{
			string text = "{";
			foreach (KeyValuePair<string, JSONNode> keyValuePair in this.m_Dict)
			{
				if (text.Length > 2)
				{
					text += ", ";
				}
				text = string.Concat(new string[]
				{
					text,
					"\"",
					JSONNode.Escape(keyValuePair.Key),
					"\":",
					keyValuePair.Value.ToString()
				});
			}
			text += "}";
			return text;
		}

		// Token: 0x06002005 RID: 8197 RVA: 0x0009D5E0 File Offset: 0x0009B7E0
		public override string ToString(string aPrefix)
		{
			string text = "{ ";
			foreach (KeyValuePair<string, JSONNode> keyValuePair in this.m_Dict)
			{
				if (text.Length > 3)
				{
					text += ", ";
				}
				text = text + "\n" + aPrefix + "   ";
				text = string.Concat(new string[]
				{
					text,
					"\"",
					JSONNode.Escape(keyValuePair.Key),
					"\" : ",
					keyValuePair.Value.ToString(aPrefix + "   ")
				});
			}
			text = text + "\n" + aPrefix + "}";
			return text;
		}

		// Token: 0x06002006 RID: 8198 RVA: 0x0009D6B8 File Offset: 0x0009B8B8
		public override void Serialize(BinaryWriter aWriter)
		{
			aWriter.Write(2);
			aWriter.Write(this.m_Dict.Count);
			foreach (string text in this.m_Dict.Keys)
			{
				aWriter.Write(text);
				this.m_Dict[text].Serialize(aWriter);
			}
		}

		// Token: 0x04001061 RID: 4193
		private Dictionary<string, JSONNode> m_Dict = new Dictionary<string, JSONNode>();
	}
}
