using System;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200005F RID: 95
	internal struct UnaryOperatorSignature
	{
		// Token: 0x060008F7 RID: 2295 RVA: 0x00048BB7 File Offset: 0x00046DB7
		public UnaryOperatorSignature(UnaryOperatorKind kind, TypeSymbol operandType, TypeSymbol returnType, MethodSymbol method = null)
		{
			this.Kind = kind;
			this.OperandType = operandType;
			this.ReturnType = returnType;
			this.Method = method;
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x00048BD6 File Offset: 0x00046DD6
		public override string ToString()
		{
			return string.Format("kind: {0} operandType: {1} operandRefKind: {2} return: {3}", new object[] { this.Kind, this.OperandType, this.RefKind, this.ReturnType });
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x00048C18 File Offset: 0x00046E18
		public RefKind RefKind
		{
			get
			{
				if (this.Method != null && !this.Method.ParameterRefKinds.IsDefaultOrEmpty)
				{
					return this.Method.ParameterRefKinds.Single<RefKind>();
				}
				return RefKind.None;
			}
		}

		// Token: 0x04000401 RID: 1025
		public static UnaryOperatorSignature Error;

		// Token: 0x04000402 RID: 1026
		public readonly MethodSymbol Method;

		// Token: 0x04000403 RID: 1027
		public readonly TypeSymbol OperandType;

		// Token: 0x04000404 RID: 1028
		public readonly TypeSymbol ReturnType;

		// Token: 0x04000405 RID: 1029
		public readonly UnaryOperatorKind Kind;
	}
}
