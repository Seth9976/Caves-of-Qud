using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200056A RID: 1386
	[UsedByNativeCode]
	[NativeHeader("Runtime/Shaders/PassIdentifier.h")]
	public readonly struct PassIdentifier : IEquatable<PassIdentifier>
	{
		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x060035E9 RID: 13801 RVA: 0x00069C38 File Offset: 0x00067E38
		public uint SubshaderIndex
		{
			get
			{
				return this.m_SubShaderIndex;
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x060035EA RID: 13802 RVA: 0x00069C50 File Offset: 0x00067E50
		public uint PassIndex
		{
			get
			{
				return this.m_PassIndex;
			}
		}

		// Token: 0x060035EB RID: 13803 RVA: 0x00069C68 File Offset: 0x00067E68
		public PassIdentifier(uint subshaderIndex, uint passIndex)
		{
			this.m_SubShaderIndex = subshaderIndex;
			this.m_PassIndex = passIndex;
		}

		// Token: 0x060035EC RID: 13804 RVA: 0x00069C7C File Offset: 0x00067E7C
		public override bool Equals(object o)
		{
			bool flag;
			if (o is PassIdentifier)
			{
				PassIdentifier passIdentifier = (PassIdentifier)o;
				flag = this.Equals(passIdentifier);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060035ED RID: 13805 RVA: 0x00069CA8 File Offset: 0x00067EA8
		public bool Equals(PassIdentifier rhs)
		{
			return this.m_SubShaderIndex == rhs.m_SubShaderIndex && this.m_PassIndex == rhs.m_PassIndex;
		}

		// Token: 0x060035EE RID: 13806 RVA: 0x00069CDC File Offset: 0x00067EDC
		public static bool operator ==(PassIdentifier lhs, PassIdentifier rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x060035EF RID: 13807 RVA: 0x00069CF8 File Offset: 0x00067EF8
		public static bool operator !=(PassIdentifier lhs, PassIdentifier rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060035F0 RID: 13808 RVA: 0x00069D14 File Offset: 0x00067F14
		public override int GetHashCode()
		{
			return this.m_SubShaderIndex.GetHashCode() ^ this.m_PassIndex.GetHashCode();
		}

		// Token: 0x0400122B RID: 4651
		internal readonly uint m_SubShaderIndex;

		// Token: 0x0400122C RID: 4652
		internal readonly uint m_PassIndex;
	}
}
