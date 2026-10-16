using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000538 RID: 1336
	public struct DepthState : IEquatable<DepthState>
	{
		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x0600332A RID: 13098 RVA: 0x00063F5C File Offset: 0x0006215C
		public static DepthState defaultValue
		{
			get
			{
				return new DepthState(true, CompareFunction.Less);
			}
		}

		// Token: 0x0600332B RID: 13099 RVA: 0x00063F75 File Offset: 0x00062175
		public DepthState(bool writeEnabled = true, CompareFunction compareFunction = CompareFunction.Less)
		{
			this.m_WriteEnabled = Convert.ToByte(writeEnabled);
			this.m_CompareFunction = (sbyte)compareFunction;
		}

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x0600332C RID: 13100 RVA: 0x00063F8C File Offset: 0x0006218C
		// (set) Token: 0x0600332D RID: 13101 RVA: 0x00063FA9 File Offset: 0x000621A9
		public bool writeEnabled
		{
			get
			{
				return Convert.ToBoolean(this.m_WriteEnabled);
			}
			set
			{
				this.m_WriteEnabled = Convert.ToByte(value);
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x0600332E RID: 13102 RVA: 0x00063FB8 File Offset: 0x000621B8
		// (set) Token: 0x0600332F RID: 13103 RVA: 0x00063FD0 File Offset: 0x000621D0
		public CompareFunction compareFunction
		{
			get
			{
				return (CompareFunction)this.m_CompareFunction;
			}
			set
			{
				this.m_CompareFunction = (sbyte)value;
			}
		}

		// Token: 0x06003330 RID: 13104 RVA: 0x00063FDC File Offset: 0x000621DC
		public bool Equals(DepthState other)
		{
			return this.m_WriteEnabled == other.m_WriteEnabled && this.m_CompareFunction == other.m_CompareFunction;
		}

		// Token: 0x06003331 RID: 13105 RVA: 0x00064010 File Offset: 0x00062210
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is DepthState && this.Equals((DepthState)obj);
		}

		// Token: 0x06003332 RID: 13106 RVA: 0x00064048 File Offset: 0x00062248
		public override int GetHashCode()
		{
			return (this.m_WriteEnabled.GetHashCode() * 397) ^ this.m_CompareFunction.GetHashCode();
		}

		// Token: 0x06003333 RID: 13107 RVA: 0x00064078 File Offset: 0x00062278
		public static bool operator ==(DepthState left, DepthState right)
		{
			return left.Equals(right);
		}

		// Token: 0x06003334 RID: 13108 RVA: 0x00064094 File Offset: 0x00062294
		public static bool operator !=(DepthState left, DepthState right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04001119 RID: 4377
		private byte m_WriteEnabled;

		// Token: 0x0400111A RID: 4378
		private sbyte m_CompareFunction;
	}
}
