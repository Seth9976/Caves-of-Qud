using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000082 RID: 130
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal abstract class BoundDagEvaluation : BoundDagTest
	{
		// Token: 0x06000B1B RID: 2843 RVA: 0x00051CD8 File Offset: 0x0004FED8
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override bool Equals([NotNullWhen(true)] object obj)
		{
			BoundDagEvaluation boundDagEvaluation = obj as BoundDagEvaluation;
			return boundDagEvaluation != null && this.Equals(boundDagEvaluation);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00051CF8 File Offset: 0x0004FEF8
		public virtual bool Equals(BoundDagEvaluation other)
		{
			return this == other || (base.Kind == other.Kind && base.Input.Equals(other.Input) && this.Symbol.Equals(other.Symbol, TypeCompareKind.AllIgnoreOptions));
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x00051D38 File Offset: 0x0004FF38
		private Symbol Symbol
		{
			get
			{
				BoundDagFieldEvaluation boundDagFieldEvaluation = this as BoundDagFieldEvaluation;
				if (boundDagFieldEvaluation != null)
				{
					return boundDagFieldEvaluation.Field.CorrespondingTupleField ?? boundDagFieldEvaluation.Field;
				}
				BoundDagPropertyEvaluation boundDagPropertyEvaluation = this as BoundDagPropertyEvaluation;
				if (boundDagPropertyEvaluation != null)
				{
					return boundDagPropertyEvaluation.Property;
				}
				BoundDagTypeEvaluation boundDagTypeEvaluation = this as BoundDagTypeEvaluation;
				if (boundDagTypeEvaluation != null)
				{
					return boundDagTypeEvaluation.Type;
				}
				BoundDagDeconstructEvaluation boundDagDeconstructEvaluation = this as BoundDagDeconstructEvaluation;
				if (boundDagDeconstructEvaluation != null)
				{
					return boundDagDeconstructEvaluation.DeconstructMethod;
				}
				BoundDagIndexEvaluation boundDagIndexEvaluation = this as BoundDagIndexEvaluation;
				if (boundDagIndexEvaluation == null)
				{
					throw ExceptionUtilities.UnexpectedValue(base.Kind);
				}
				return boundDagIndexEvaluation.Property;
			}
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00051DC6 File Offset: 0x0004FFC6
		public override int GetHashCode()
		{
			int hashCode = base.Input.GetHashCode();
			Symbol symbol = this.Symbol;
			return Hash.Combine(hashCode, (symbol != null) ? symbol.GetHashCode() : 0);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00051DEA File Offset: 0x0004FFEA
		protected BoundDagEvaluation(BoundKind kind, SyntaxNode syntax, BoundDagTemp input, bool hasErrors = false)
			: base(kind, syntax, input, hasErrors)
		{
		}
	}
}
