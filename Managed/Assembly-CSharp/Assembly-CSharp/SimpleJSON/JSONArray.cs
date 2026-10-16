using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace SimpleJSON
{
	// Token: 0x0200035A RID: 858
	public class JSONArray : JSONNode, IEnumerable
	{
		// Token: 0x17000448 RID: 1096
		public override JSONNode this[int aIndex]
		{
			get
			{
				if (aIndex < 0 || aIndex >= this.m_List.Count)
				{
					return new JSONLazyCreator(this);
				}
				return this.m_List[aIndex];
			}
			set
			{
				if (aIndex < 0 || aIndex >= this.m_List.Count)
				{
					this.m_List.Add(value);
					return;
				}
				this.m_List[aIndex] = value;
			}
		}

		// Token: 0x17000449 RID: 1097
		public override JSONNode this[string aKey]
		{
			get
			{
				return new JSONLazyCreator(this);
			}
			set
			{
				this.m_List.Add(value);
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001FED RID: 8173 RVA: 0x0009D04D File Offset: 0x0009B24D
		public override int Count
		{
			get
			{
				return this.m_List.Count;
			}
		}

		// Token: 0x06001FEE RID: 8174 RVA: 0x0009D05A File Offset: 0x0009B25A
		public override void Add(string aKey, JSONNode aItem)
		{
			this.m_List.Add(aItem);
		}

		// Token: 0x06001FEF RID: 8175 RVA: 0x0009D068 File Offset: 0x0009B268
		public override JSONNode Remove(int aIndex)
		{
			if (aIndex < 0 || aIndex >= this.m_List.Count)
			{
				return null;
			}
			JSONNode jsonnode = this.m_List[aIndex];
			this.m_List.RemoveAt(aIndex);
			return jsonnode;
		}

		// Token: 0x06001FF0 RID: 8176 RVA: 0x0009D096 File Offset: 0x0009B296
		public override JSONNode Remove(JSONNode aNode)
		{
			this.m_List.Remove(aNode);
			return aNode;
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001FF1 RID: 8177 RVA: 0x0009D0A6 File Offset: 0x0009B2A6
		public override IEnumerable<JSONNode> Childs
		{
			get
			{
				foreach (JSONNode jsonnode in this.m_List)
				{
					yield return jsonnode;
				}
				List<JSONNode>.Enumerator enumerator = default(List<JSONNode>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x06001FF2 RID: 8178 RVA: 0x0009D0B6 File Offset: 0x0009B2B6
		public IEnumerator GetEnumerator()
		{
			foreach (JSONNode jsonnode in this.m_List)
			{
				yield return jsonnode;
			}
			List<JSONNode>.Enumerator enumerator = default(List<JSONNode>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06001FF3 RID: 8179 RVA: 0x0009D0C8 File Offset: 0x0009B2C8
		public override string ToString()
		{
			string text = "[ ";
			foreach (JSONNode jsonnode in this.m_List)
			{
				if (text.Length > 2)
				{
					text += ", ";
				}
				text += jsonnode.ToString();
			}
			text += " ]";
			return text;
		}

		// Token: 0x06001FF4 RID: 8180 RVA: 0x0009D14C File Offset: 0x0009B34C
		public override string ToString(string aPrefix)
		{
			string text = "[ ";
			foreach (JSONNode jsonnode in this.m_List)
			{
				if (text.Length > 3)
				{
					text += ", ";
				}
				text = text + "\n" + aPrefix + "   ";
				text += jsonnode.ToString(aPrefix + "   ");
			}
			text = text + "\n" + aPrefix + "]";
			return text;
		}

		// Token: 0x06001FF5 RID: 8181 RVA: 0x0009D1F0 File Offset: 0x0009B3F0
		public override void Serialize(BinaryWriter aWriter)
		{
			aWriter.Write(1);
			aWriter.Write(this.m_List.Count);
			for (int i = 0; i < this.m_List.Count; i++)
			{
				this.m_List[i].Serialize(aWriter);
			}
		}

		// Token: 0x04001060 RID: 4192
		private List<JSONNode> m_List = new List<JSONNode>();
	}
}
