using System;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200005D RID: 93
	internal struct UnaryOperatorAnalysisResult
	{
		// Token: 0x060008E9 RID: 2281 RVA: 0x00048A27 File Offset: 0x00046C27
		private UnaryOperatorAnalysisResult(OperatorAnalysisResultKind kind, UnaryOperatorSignature signature, Conversion conversion)
		{
			this.Kind = kind;
			this.Signature = signature;
			this.Conversion = conversion;
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x00048A3E File Offset: 0x00046C3E
		public bool IsValid
		{
			get
			{
				return this.Kind == OperatorAnalysisResultKind.Applicable;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x00048A49 File Offset: 0x00046C49
		public bool HasValue
		{
			get
			{
				return this.Kind > OperatorAnalysisResultKind.Undefined;
			}
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00048A54 File Offset: 0x00046C54
		public static UnaryOperatorAnalysisResult Applicable(UnaryOperatorSignature signature, Conversion conversion)
		{
			return new UnaryOperatorAnalysisResult(OperatorAnalysisResultKind.Applicable, signature, conversion);
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00048A5E File Offset: 0x00046C5E
		public static UnaryOperatorAnalysisResult Inapplicable(UnaryOperatorSignature signature, Conversion conversion)
		{
			return new UnaryOperatorAnalysisResult(OperatorAnalysisResultKind.Inapplicable, signature, conversion);
		}

		// Token: 0x060008EE RID: 2286 RVA: 0x00048A68 File Offset: 0x00046C68
		public UnaryOperatorAnalysisResult Worse()
		{
			return new UnaryOperatorAnalysisResult(OperatorAnalysisResultKind.Worse, this.Signature, this.Conversion);
		}

		// Token: 0x040003FC RID: 1020
		public readonly UnaryOperatorSignature Signature;

		// Token: 0x040003FD RID: 1021
		public readonly Conversion Conversion;

		// Token: 0x040003FE RID: 1022
		public readonly OperatorAnalysisResultKind Kind;
	}
}
