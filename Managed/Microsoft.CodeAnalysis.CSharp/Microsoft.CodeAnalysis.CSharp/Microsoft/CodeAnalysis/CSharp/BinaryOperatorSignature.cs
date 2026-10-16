using System;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000057 RID: 87
	internal struct BinaryOperatorSignature : IEquatable<BinaryOperatorSignature>
	{
		// Token: 0x060008B4 RID: 2228 RVA: 0x00047C3F File Offset: 0x00045E3F
		public BinaryOperatorSignature(BinaryOperatorKind kind, TypeSymbol leftType, TypeSymbol rightType, TypeSymbol returnType, MethodSymbol method = null)
		{
			this.Kind = kind;
			this.LeftType = leftType;
			this.RightType = rightType;
			this.ReturnType = returnType;
			this.Method = method;
			this.Priority = default(int?);
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00047C74 File Offset: 0x00045E74
		public override string ToString()
		{
			return string.Format("kind: {0} leftType: {1} leftRefKind: {2} rightType: {3} rightRefKind: {4} return: {5}", new object[] { this.Kind, this.LeftType, this.LeftRefKind, this.RightType, this.RightRefKind, this.ReturnType });
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00047CD8 File Offset: 0x00045ED8
		public bool Equals(BinaryOperatorSignature other)
		{
			return this.Kind == other.Kind && TypeSymbol.Equals(this.LeftType, other.LeftType, TypeCompareKind.ConsiderEverything) && TypeSymbol.Equals(this.RightType, other.RightType, TypeCompareKind.ConsiderEverything) && TypeSymbol.Equals(this.ReturnType, other.ReturnType, TypeCompareKind.ConsiderEverything) && this.Method == other.Method;
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00047D42 File Offset: 0x00045F42
		public static bool operator ==(BinaryOperatorSignature x, BinaryOperatorSignature y)
		{
			return x.Equals(y);
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00047D4C File Offset: 0x00045F4C
		public static bool operator !=(BinaryOperatorSignature x, BinaryOperatorSignature y)
		{
			return !x.Equals(y);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00047D59 File Offset: 0x00045F59
		public override bool Equals(object obj)
		{
			return obj is BinaryOperatorSignature && this.Equals((BinaryOperatorSignature)obj);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00047D71 File Offset: 0x00045F71
		public override int GetHashCode()
		{
			return Hash.Combine<TypeSymbol>(this.ReturnType, Hash.Combine<TypeSymbol>(this.LeftType, Hash.Combine<TypeSymbol>(this.RightType, Hash.Combine<MethodSymbol>(this.Method, (int)this.Kind))));
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060008BB RID: 2235 RVA: 0x00047DA8 File Offset: 0x00045FA8
		public RefKind LeftRefKind
		{
			get
			{
				if (this.Method != null && !this.Method.ParameterRefKinds.IsDefaultOrEmpty)
				{
					return this.Method.ParameterRefKinds[0];
				}
				return RefKind.None;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x00047DE8 File Offset: 0x00045FE8
		public RefKind RightRefKind
		{
			get
			{
				if (this.Method != null && !this.Method.ParameterRefKinds.IsDefaultOrEmpty)
				{
					return this.Method.ParameterRefKinds[1];
				}
				return RefKind.None;
			}
		}

		// Token: 0x04000140 RID: 320
		public static BinaryOperatorSignature Error;

		// Token: 0x04000141 RID: 321
		public readonly TypeSymbol LeftType;

		// Token: 0x04000142 RID: 322
		public readonly TypeSymbol RightType;

		// Token: 0x04000143 RID: 323
		public readonly TypeSymbol ReturnType;

		// Token: 0x04000144 RID: 324
		public readonly MethodSymbol Method;

		// Token: 0x04000145 RID: 325
		public readonly BinaryOperatorKind Kind;

		// Token: 0x04000146 RID: 326
		public int? Priority;
	}
}
