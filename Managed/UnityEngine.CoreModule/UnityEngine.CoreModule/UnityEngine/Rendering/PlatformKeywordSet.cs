using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200056B RID: 1387
	[UsedByNativeCode]
	public struct PlatformKeywordSet
	{
		// Token: 0x060035F1 RID: 13809 RVA: 0x00069D40 File Offset: 0x00067F40
		private ulong ComputeKeywordMask(BuiltinShaderDefine define)
		{
			return (ulong)(1L << (int)((define % (BuiltinShaderDefine)64) & BuiltinShaderDefine.SHADER_API_GLES30));
		}

		// Token: 0x060035F2 RID: 13810 RVA: 0x00069D5C File Offset: 0x00067F5C
		public bool IsEnabled(BuiltinShaderDefine define)
		{
			return (this.m_Bits & this.ComputeKeywordMask(define)) > 0UL;
		}

		// Token: 0x060035F3 RID: 13811 RVA: 0x00069D80 File Offset: 0x00067F80
		public void Enable(BuiltinShaderDefine define)
		{
			this.m_Bits |= this.ComputeKeywordMask(define);
		}

		// Token: 0x060035F4 RID: 13812 RVA: 0x00069D97 File Offset: 0x00067F97
		public void Disable(BuiltinShaderDefine define)
		{
			this.m_Bits &= ~this.ComputeKeywordMask(define);
		}

		// Token: 0x0400122D RID: 4653
		private const int k_SizeInBits = 64;

		// Token: 0x0400122E RID: 4654
		internal ulong m_Bits;
	}
}
