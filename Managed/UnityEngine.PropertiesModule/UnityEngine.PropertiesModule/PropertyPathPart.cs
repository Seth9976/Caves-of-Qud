using System;
using System.Runtime.CompilerServices;

namespace Unity.Properties
{
	// Token: 0x0200002A RID: 42
	public readonly struct PropertyPathPart : IEquatable<PropertyPathPart>
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000035C7 File Offset: 0x000017C7
		public bool IsName
		{
			get
			{
				return this.Kind == PropertyPathPartKind.Name;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000035D2 File Offset: 0x000017D2
		public bool IsIndex
		{
			get
			{
				return this.Kind == PropertyPathPartKind.Index;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000035DD File Offset: 0x000017DD
		public bool IsKey
		{
			get
			{
				return this.Kind == PropertyPathPartKind.Key;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000035E8 File Offset: 0x000017E8
		public PropertyPathPartKind Kind
		{
			get
			{
				return this.m_Kind;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000035F0 File Offset: 0x000017F0
		public string Name
		{
			get
			{
				this.CheckKind(PropertyPathPartKind.Name);
				return this.m_Name;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x00003610 File Offset: 0x00001810
		public int Index
		{
			get
			{
				this.CheckKind(PropertyPathPartKind.Index);
				return this.m_Index;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003630 File Offset: 0x00001830
		public object Key
		{
			get
			{
				this.CheckKind(PropertyPathPartKind.Key);
				return this.m_Key;
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003650 File Offset: 0x00001850
		public PropertyPathPart(string name)
		{
			this.m_Kind = PropertyPathPartKind.Name;
			this.m_Name = name;
			this.m_Index = -1;
			this.m_Key = null;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000366F File Offset: 0x0000186F
		public PropertyPathPart(int index)
		{
			this.m_Kind = PropertyPathPartKind.Index;
			this.m_Name = string.Empty;
			this.m_Index = index;
			this.m_Key = null;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003692 File Offset: 0x00001892
		public PropertyPathPart(object key)
		{
			this.m_Kind = PropertyPathPartKind.Key;
			this.m_Name = string.Empty;
			this.m_Index = -1;
			this.m_Key = key;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000036B8 File Offset: 0x000018B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void CheckKind(PropertyPathPartKind type)
		{
			bool flag = type != this.Kind;
			if (flag)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000036DC File Offset: 0x000018DC
		public override string ToString()
		{
			PropertyPathPartKind kind = this.Kind;
			if (!true)
			{
			}
			string text;
			switch (kind)
			{
			case PropertyPathPartKind.Name:
				text = this.m_Name;
				break;
			case PropertyPathPartKind.Index:
				text = "[" + this.m_Index.ToString() + "]";
				break;
			case PropertyPathPartKind.Key:
			{
				string text2 = "[\"";
				object key = this.m_Key;
				text = text2 + ((key != null) ? key.ToString() : null) + "\"]";
				break;
			}
			default:
				throw new ArgumentOutOfRangeException();
			}
			if (!true)
			{
			}
			return text;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003764 File Offset: 0x00001964
		public bool Equals(PropertyPathPart other)
		{
			return this.m_Kind == other.m_Kind && this.m_Name == other.m_Name && this.m_Index == other.m_Index && object.Equals(this.m_Key, other.m_Key);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000037BC File Offset: 0x000019BC
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is PropertyPathPart)
			{
				PropertyPathPart propertyPathPart = (PropertyPathPart)obj;
				flag = this.Equals(propertyPathPart);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000037E8 File Offset: 0x000019E8
		public override int GetHashCode()
		{
			int kind = (int)this.m_Kind;
			PropertyPathPartKind kind2 = this.m_Kind;
			if (!true)
			{
			}
			int num;
			switch (kind2)
			{
			case PropertyPathPartKind.Name:
				num = (kind * 397) ^ ((this.m_Name != null) ? this.m_Name.GetHashCode() : 0);
				break;
			case PropertyPathPartKind.Index:
				num = (kind * 397) ^ this.m_Index;
				break;
			case PropertyPathPartKind.Key:
				num = (kind * 397) ^ ((this.m_Key != null) ? this.m_Key.GetHashCode() : 0);
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			if (!true)
			{
			}
			return num;
		}

		// Token: 0x0400003E RID: 62
		private readonly PropertyPathPartKind m_Kind;

		// Token: 0x0400003F RID: 63
		private readonly string m_Name;

		// Token: 0x04000040 RID: 64
		private readonly int m_Index;

		// Token: 0x04000041 RID: 65
		private readonly object m_Key;
	}
}
