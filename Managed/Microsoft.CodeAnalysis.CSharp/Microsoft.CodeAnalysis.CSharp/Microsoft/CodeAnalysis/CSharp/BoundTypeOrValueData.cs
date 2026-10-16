using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000AD RID: 173
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal struct BoundTypeOrValueData : IEquatable<BoundTypeOrValueData>
	{
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00055B70 File Offset: 0x00053D70
		public readonly Symbol ValueSymbol { get; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x00055B78 File Offset: 0x00053D78
		public readonly BoundExpression ValueExpression { get; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x00055B80 File Offset: 0x00053D80
		public readonly BindingDiagnosticBag ValueDiagnostics { get; }

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x00055B88 File Offset: 0x00053D88
		public readonly BoundExpression TypeExpression { get; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x00055B90 File Offset: 0x00053D90
		public readonly BindingDiagnosticBag TypeDiagnostics { get; }

		// Token: 0x06000D11 RID: 3345 RVA: 0x00055B98 File Offset: 0x00053D98
		public BoundTypeOrValueData(Symbol valueSymbol, BoundExpression valueExpression, BindingDiagnosticBag valueDiagnostics, BoundExpression typeExpression, BindingDiagnosticBag typeDiagnostics)
		{
			this.ValueSymbol = valueSymbol;
			this.ValueExpression = valueExpression;
			this.ValueDiagnostics = valueDiagnostics;
			this.TypeExpression = typeExpression;
			this.TypeDiagnostics = typeDiagnostics;
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x00055BC0 File Offset: 0x00053DC0
		public static bool operator ==(BoundTypeOrValueData a, BoundTypeOrValueData b)
		{
			return a.ValueSymbol == b.ValueSymbol && a.ValueExpression == b.ValueExpression && a.ValueDiagnostics == b.ValueDiagnostics && a.TypeExpression == b.TypeExpression && a.TypeDiagnostics == b.TypeDiagnostics;
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x00055C1F File Offset: 0x00053E1F
		public static bool operator !=(BoundTypeOrValueData a, BoundTypeOrValueData b)
		{
			return !(a == b);
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x00055C2B File Offset: 0x00053E2B
		[global::System.Runtime.CompilerServices.NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is BoundTypeOrValueData && (BoundTypeOrValueData)obj == this;
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00055C48 File Offset: 0x00053E48
		public override int GetHashCode()
		{
			return Hash.Combine(this.ValueSymbol.GetHashCode(), Hash.Combine(this.ValueExpression.GetHashCode(), Hash.Combine(this.ValueDiagnostics.GetHashCode(), Hash.Combine(this.TypeExpression.GetHashCode(), this.TypeDiagnostics.GetHashCode()))));
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x00055CA0 File Offset: 0x00053EA0
		bool IEquatable<BoundTypeOrValueData>.Equals(BoundTypeOrValueData b)
		{
			return b == this;
		}
	}
}
