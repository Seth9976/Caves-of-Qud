using System;
using Unity.Properties;

namespace UnityEngine.UIElements
{
	// Token: 0x020005B5 RID: 1461
	public readonly struct BindingId : IEquatable<BindingId>
	{
		// Token: 0x06002B71 RID: 11121 RVA: 0x0009CE46 File Offset: 0x0009B046
		public BindingId(string path)
		{
			this.m_PropertyPath = new PropertyPath(path);
			this.m_Path = path;
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x0009CE5C File Offset: 0x0009B05C
		public BindingId(in PropertyPath path)
		{
			this.m_PropertyPath = path;
			this.m_Path = path.ToString();
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x0009CE80 File Offset: 0x0009B080
		public static implicit operator PropertyPath(in BindingId vep)
		{
			return vep.m_PropertyPath;
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x0009CE98 File Offset: 0x0009B098
		public static implicit operator string(in BindingId vep)
		{
			return vep.m_Path;
		}

		// Token: 0x06002B75 RID: 11125 RVA: 0x0009CEB0 File Offset: 0x0009B0B0
		public static implicit operator BindingId(string name)
		{
			return new BindingId(name);
		}

		// Token: 0x06002B76 RID: 11126 RVA: 0x0009CEC8 File Offset: 0x0009B0C8
		public static implicit operator BindingId(in PropertyPath path)
		{
			return new BindingId(in path);
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x0009CEE0 File Offset: 0x0009B0E0
		public override string ToString()
		{
			return this.m_Path;
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x0009CEF8 File Offset: 0x0009B0F8
		public bool Equals(BindingId other)
		{
			return this.m_PropertyPath == other.m_PropertyPath;
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x0009CF1C File Offset: 0x0009B11C
		public override bool Equals(object obj)
		{
			bool flag;
			if (obj is BindingId)
			{
				BindingId bindingId = (BindingId)obj;
				flag = this.Equals(bindingId);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06002B7A RID: 11130 RVA: 0x0009CF48 File Offset: 0x0009B148
		public override int GetHashCode()
		{
			return this.m_PropertyPath.GetHashCode();
		}

		// Token: 0x06002B7B RID: 11131 RVA: 0x0009CF6C File Offset: 0x0009B16C
		public static bool operator ==(in BindingId lhs, in BindingId rhs)
		{
			return lhs.m_PropertyPath == rhs.m_PropertyPath;
		}

		// Token: 0x06002B7C RID: 11132 RVA: 0x0009CF90 File Offset: 0x0009B190
		public static bool operator !=(in BindingId lhs, in BindingId rhs)
		{
			return !((in lhs) == (in rhs));
		}

		// Token: 0x040013AD RID: 5037
		public static readonly BindingId Invalid = default(BindingId);

		// Token: 0x040013AE RID: 5038
		private readonly PropertyPath m_PropertyPath;

		// Token: 0x040013AF RID: 5039
		private readonly string m_Path;
	}
}
