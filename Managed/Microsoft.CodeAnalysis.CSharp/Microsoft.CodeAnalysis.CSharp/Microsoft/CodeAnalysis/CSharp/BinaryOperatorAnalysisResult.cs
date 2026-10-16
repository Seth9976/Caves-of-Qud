using System;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000054 RID: 84
	internal struct BinaryOperatorAnalysisResult
	{
		// Token: 0x06000826 RID: 2086 RVA: 0x0004251F File Offset: 0x0004071F
		private BinaryOperatorAnalysisResult(OperatorAnalysisResultKind kind, BinaryOperatorSignature signature, Conversion leftConversion, Conversion rightConversion)
		{
			this.Kind = kind;
			this.Signature = signature;
			this.LeftConversion = leftConversion;
			this.RightConversion = rightConversion;
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x0004253E File Offset: 0x0004073E
		public bool IsValid
		{
			get
			{
				return this.Kind == OperatorAnalysisResultKind.Applicable;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000828 RID: 2088 RVA: 0x00042549 File Offset: 0x00040749
		public bool HasValue
		{
			get
			{
				return this.Kind > OperatorAnalysisResultKind.Undefined;
			}
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x00042554 File Offset: 0x00040754
		public override bool Equals(object obj)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0004255B File Offset: 0x0004075B
		public override int GetHashCode()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00042562 File Offset: 0x00040762
		public static BinaryOperatorAnalysisResult Applicable(BinaryOperatorSignature signature, Conversion leftConversion, Conversion rightConversion)
		{
			return new BinaryOperatorAnalysisResult(OperatorAnalysisResultKind.Applicable, signature, leftConversion, rightConversion);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x0004256D File Offset: 0x0004076D
		public static BinaryOperatorAnalysisResult Inapplicable(BinaryOperatorSignature signature, Conversion leftConversion, Conversion rightConversion)
		{
			return new BinaryOperatorAnalysisResult(OperatorAnalysisResultKind.Inapplicable, signature, leftConversion, rightConversion);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00042578 File Offset: 0x00040778
		public BinaryOperatorAnalysisResult Worse()
		{
			return new BinaryOperatorAnalysisResult(OperatorAnalysisResultKind.Worse, this.Signature, this.LeftConversion, this.RightConversion);
		}

		// Token: 0x04000137 RID: 311
		public readonly Conversion LeftConversion;

		// Token: 0x04000138 RID: 312
		public readonly Conversion RightConversion;

		// Token: 0x04000139 RID: 313
		public readonly BinaryOperatorSignature Signature;

		// Token: 0x0400013A RID: 314
		public readonly OperatorAnalysisResultKind Kind;
	}
}
