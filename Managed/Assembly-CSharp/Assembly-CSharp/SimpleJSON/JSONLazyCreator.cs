using System;

namespace SimpleJSON
{
	// Token: 0x0200035D RID: 861
	internal class JSONLazyCreator : JSONNode
	{
		// Token: 0x06002012 RID: 8210 RVA: 0x0009D8DB File Offset: 0x0009BADB
		public JSONLazyCreator(JSONNode aNode)
		{
			this.m_Node = aNode;
			this.m_Key = null;
		}

		// Token: 0x06002013 RID: 8211 RVA: 0x0009D8F1 File Offset: 0x0009BAF1
		public JSONLazyCreator(JSONNode aNode, string aKey)
		{
			this.m_Node = aNode;
			this.m_Key = aKey;
		}

		// Token: 0x06002014 RID: 8212 RVA: 0x0009D907 File Offset: 0x0009BB07
		private void Set(JSONNode aVal)
		{
			if (this.m_Key == null)
			{
				this.m_Node.Add(aVal);
			}
			else
			{
				aVal.Key = this.m_Key;
				this.m_Node.Add(this.m_Key, aVal);
			}
			this.m_Node = null;
		}

		// Token: 0x17000452 RID: 1106
		public override JSONNode this[int aIndex]
		{
			get
			{
				return new JSONLazyCreator(this);
			}
			set
			{
				this.Set(new JSONArray { value });
			}
		}

		// Token: 0x17000453 RID: 1107
		public override JSONNode this[string aKey]
		{
			get
			{
				return new JSONLazyCreator(this, aKey);
			}
			set
			{
				JSONClass jsonclass = new JSONClass();
				value.Key = aKey;
				jsonclass.Add(aKey, value);
				this.Set(jsonclass);
			}
		}

		// Token: 0x06002019 RID: 8217 RVA: 0x0009D9A4 File Offset: 0x0009BBA4
		public override void Add(JSONNode aItem)
		{
			this.Set(new JSONArray { aItem });
		}

		// Token: 0x0600201A RID: 8218 RVA: 0x0009D9C8 File Offset: 0x0009BBC8
		public override void Add(string aKey, JSONNode aItem)
		{
			JSONClass jsonclass = new JSONClass();
			aItem.Key = aKey;
			jsonclass.Add(aKey, aItem);
			this.Set(jsonclass);
		}

		// Token: 0x0600201B RID: 8219 RVA: 0x0009D9F1 File Offset: 0x0009BBF1
		public static bool operator ==(JSONLazyCreator a, object b)
		{
			return b == null || a == b;
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x0009D9FC File Offset: 0x0009BBFC
		public static bool operator !=(JSONLazyCreator a, object b)
		{
			return !(a == b);
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x0009DA08 File Offset: 0x0009BC08
		public override bool Equals(object obj)
		{
			return obj == null || this == obj;
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x0009DA13 File Offset: 0x0009BC13
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x0009DA1B File Offset: 0x0009BC1B
		public override string ToString()
		{
			return null;
		}

		// Token: 0x06002020 RID: 8224 RVA: 0x0009DA1E File Offset: 0x0009BC1E
		public override string ToString(string aPrefix)
		{
			return "";
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06002021 RID: 8225 RVA: 0x0009DA28 File Offset: 0x0009BC28
		// (set) Token: 0x06002022 RID: 8226 RVA: 0x0009DA44 File Offset: 0x0009BC44
		public override int AsInt
		{
			get
			{
				JSONData jsondata = new JSONData(0);
				this.Set(jsondata);
				return 0;
			}
			set
			{
				JSONData jsondata = new JSONData(value);
				this.Set(jsondata);
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06002023 RID: 8227 RVA: 0x0009DA60 File Offset: 0x0009BC60
		// (set) Token: 0x06002024 RID: 8228 RVA: 0x0009DA84 File Offset: 0x0009BC84
		public override float AsFloat
		{
			get
			{
				JSONData jsondata = new JSONData(0f);
				this.Set(jsondata);
				return 0f;
			}
			set
			{
				JSONData jsondata = new JSONData(value);
				this.Set(jsondata);
			}
		}

		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06002025 RID: 8229 RVA: 0x0009DAA0 File Offset: 0x0009BCA0
		// (set) Token: 0x06002026 RID: 8230 RVA: 0x0009DACC File Offset: 0x0009BCCC
		public override double AsDouble
		{
			get
			{
				JSONData jsondata = new JSONData(0.0);
				this.Set(jsondata);
				return 0.0;
			}
			set
			{
				JSONData jsondata = new JSONData(value);
				this.Set(jsondata);
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06002027 RID: 8231 RVA: 0x0009DAE8 File Offset: 0x0009BCE8
		// (set) Token: 0x06002028 RID: 8232 RVA: 0x0009DB04 File Offset: 0x0009BD04
		public override bool AsBool
		{
			get
			{
				JSONData jsondata = new JSONData(false);
				this.Set(jsondata);
				return false;
			}
			set
			{
				JSONData jsondata = new JSONData(value);
				this.Set(jsondata);
			}
		}

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06002029 RID: 8233 RVA: 0x0009DB20 File Offset: 0x0009BD20
		public override JSONArray AsArray
		{
			get
			{
				JSONArray jsonarray = new JSONArray();
				this.Set(jsonarray);
				return jsonarray;
			}
		}

		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x0600202A RID: 8234 RVA: 0x0009DB3C File Offset: 0x0009BD3C
		public override JSONClass AsObject
		{
			get
			{
				JSONClass jsonclass = new JSONClass();
				this.Set(jsonclass);
				return jsonclass;
			}
		}

		// Token: 0x04001063 RID: 4195
		private JSONNode m_Node;

		// Token: 0x04001064 RID: 4196
		private string m_Key;
	}
}
