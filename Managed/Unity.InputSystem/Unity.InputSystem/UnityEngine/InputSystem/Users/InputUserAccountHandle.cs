using System;

namespace UnityEngine.InputSystem.Users
{
	// Token: 0x02000081 RID: 129
	public struct InputUserAccountHandle : IEquatable<InputUserAccountHandle>
	{
		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000AE6 RID: 2790 RVA: 0x0003B6FB File Offset: 0x000398FB
		public string apiName
		{
			get
			{
				return this.m_ApiName;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x0003B703 File Offset: 0x00039903
		public ulong handle
		{
			get
			{
				return this.m_Handle;
			}
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0003B70B File Offset: 0x0003990B
		public InputUserAccountHandle(string apiName, ulong handle)
		{
			if (string.IsNullOrEmpty(apiName))
			{
				throw new ArgumentNullException("apiName");
			}
			this.m_ApiName = apiName;
			this.m_Handle = handle;
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0003B72E File Offset: 0x0003992E
		public override string ToString()
		{
			if (this.m_ApiName == null)
			{
				return base.ToString();
			}
			return string.Format("{0}({1})", this.m_ApiName, this.m_Handle);
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0003B764 File Offset: 0x00039964
		public bool Equals(InputUserAccountHandle other)
		{
			return string.Equals(this.apiName, other.apiName) && object.Equals(this.handle, other.handle);
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0003B798 File Offset: 0x00039998
		public override bool Equals(object obj)
		{
			return obj != null && obj is InputUserAccountHandle && this.Equals((InputUserAccountHandle)obj);
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0003B7B5 File Offset: 0x000399B5
		public static bool operator ==(InputUserAccountHandle left, InputUserAccountHandle right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0003B7BF File Offset: 0x000399BF
		public static bool operator !=(InputUserAccountHandle left, InputUserAccountHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x0003B7CC File Offset: 0x000399CC
		public override int GetHashCode()
		{
			return (((this.apiName != null) ? this.apiName.GetHashCode() : 0) * 397) ^ this.handle.GetHashCode();
		}

		// Token: 0x040003C7 RID: 967
		private string m_ApiName;

		// Token: 0x040003C8 RID: 968
		private ulong m_Handle;
	}
}
