using System;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000051 RID: 81
	internal sealed class UserDefinedConversionAnalysis
	{
		// Token: 0x0600081D RID: 2077 RVA: 0x00042485 File Offset: 0x00040685
		public static UserDefinedConversionAnalysis Normal(MethodSymbol op, Conversion sourceConversion, Conversion targetConversion, TypeSymbol fromType, TypeSymbol toType)
		{
			return new UserDefinedConversionAnalysis(UserDefinedConversionAnalysisKind.ApplicableInNormalForm, op, sourceConversion, targetConversion, fromType, toType);
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00042493 File Offset: 0x00040693
		public static UserDefinedConversionAnalysis Lifted(MethodSymbol op, Conversion sourceConversion, Conversion targetConversion, TypeSymbol fromType, TypeSymbol toType)
		{
			return new UserDefinedConversionAnalysis(UserDefinedConversionAnalysisKind.ApplicableInLiftedForm, op, sourceConversion, targetConversion, fromType, toType);
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x000424A1 File Offset: 0x000406A1
		private UserDefinedConversionAnalysis(UserDefinedConversionAnalysisKind kind, MethodSymbol op, Conversion sourceConversion, Conversion targetConversion, TypeSymbol fromType, TypeSymbol toType)
		{
			this.Kind = kind;
			this.Operator = op;
			this.SourceConversion = sourceConversion;
			this.TargetConversion = targetConversion;
			this.FromType = fromType;
			this.ToType = toType;
		}

		// Token: 0x04000128 RID: 296
		public readonly TypeSymbol FromType;

		// Token: 0x04000129 RID: 297
		public readonly TypeSymbol ToType;

		// Token: 0x0400012A RID: 298
		public readonly MethodSymbol Operator;

		// Token: 0x0400012B RID: 299
		public readonly Conversion SourceConversion;

		// Token: 0x0400012C RID: 300
		public readonly Conversion TargetConversion;

		// Token: 0x0400012D RID: 301
		public readonly UserDefinedConversionAnalysisKind Kind;
	}
}
