using System;
using System.Collections.Generic;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004D9 RID: 1241
	internal sealed class ConversionSignatureComparer : IEqualityComparer<SourceUserDefinedConversionSymbol>
	{
		// Token: 0x17001A71 RID: 6769
		// (get) Token: 0x060068F2 RID: 26866 RVA: 0x0018A4CC File Offset: 0x001886CC
		public static ConversionSignatureComparer Comparer
		{
			get
			{
				return ConversionSignatureComparer.s_comparer;
			}
		}

		// Token: 0x060068F3 RID: 26867 RVA: 0x0018A4D3 File Offset: 0x001886D3
		private ConversionSignatureComparer()
		{
		}

		// Token: 0x060068F4 RID: 26868 RVA: 0x0018A4DC File Offset: 0x001886DC
		public bool Equals(SourceUserDefinedConversionSymbol member1, SourceUserDefinedConversionSymbol member2)
		{
			return member1 == member2 || (member1 != null && member2 != null && member1.ParameterCount == 1 && member2.ParameterCount == 1 && member1.ReturnType.Equals(member2.ReturnType, TypeCompareKind.IgnoreDynamic | TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes) && member1.ParameterTypesWithAnnotations[0].Equals(member2.ParameterTypesWithAnnotations[0], TypeCompareKind.IgnoreDynamic | TypeCompareKind.IgnoreTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes));
		}

		// Token: 0x060068F5 RID: 26869 RVA: 0x0018A54C File Offset: 0x0018874C
		public int GetHashCode(SourceUserDefinedConversionSymbol member)
		{
			if (member == null)
			{
				return 0;
			}
			int num = 1;
			num = Hash.Combine(member.ReturnType.GetHashCode(), num);
			if (member.ParameterCount != 1)
			{
				return num;
			}
			return Hash.Combine(member.GetParameterType(0).GetHashCode(), num);
		}

		// Token: 0x04001A3C RID: 6716
		private static readonly ConversionSignatureComparer s_comparer = new ConversionSignatureComparer();
	}
}
