using System;
using System.Reflection.Metadata;
using Microsoft.CodeAnalysis;

namespace Microsoft.Cci
{
	// Token: 0x02000097 RID: 151
	internal static class CallingConventionUtils
	{
		// Token: 0x060003F6 RID: 1014 RVA: 0x0000DFE3 File Offset: 0x0000C1E3
		internal static CallingConvention FromSignatureConvention(this SignatureCallingConvention convention)
		{
			if (!convention.IsValid())
			{
				throw new UnsupportedSignatureContent();
			}
			return (CallingConvention)(convention & (SignatureCallingConvention)15);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x0000DFF7 File Offset: 0x0000C1F7
		internal static bool IsValid(this SignatureCallingConvention convention)
		{
			return convention <= SignatureCallingConvention.VarArgs || convention == SignatureCallingConvention.Unmanaged;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000E004 File Offset: 0x0000C204
		internal static SignatureCallingConvention ToSignatureConvention(this CallingConvention convention)
		{
			return (SignatureCallingConvention)convention & (SignatureCallingConvention)15;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000E00B File Offset: 0x0000C20B
		internal static bool IsCallingConvention(this CallingConvention original, CallingConvention compare)
		{
			return (original & (CallingConvention)15) == compare;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000E014 File Offset: 0x0000C214
		internal static bool HasUnknownCallingConventionAttributeBits(this CallingConvention convention)
		{
			return (convention & (CallingConvention)(-128)) > CallingConvention.Default;
		}

		// Token: 0x040001AA RID: 426
		private const SignatureCallingConvention SignatureCallingConventionMask = (SignatureCallingConvention)15;

		// Token: 0x040001AB RID: 427
		private const SignatureAttributes SignatureAttributesMask = SignatureAttributes.Generic | SignatureAttributes.Instance | SignatureAttributes.ExplicitThis;
	}
}
