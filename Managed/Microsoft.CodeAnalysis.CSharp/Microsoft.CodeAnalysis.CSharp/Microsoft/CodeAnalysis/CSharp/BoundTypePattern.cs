using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001DB RID: 475
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundTypePattern : BoundPattern
	{
		// Token: 0x06002030 RID: 8240 RVA: 0x000A3D1E File Offset: 0x000A1F1E
		public BoundTypePattern(SyntaxNode syntax, BoundTypeExpression declaredType, bool isExplicitNotNullTest, TypeSymbol inputType, TypeSymbol narrowedType, bool hasErrors = false)
			: base(BoundKind.TypePattern, syntax, inputType, narrowedType, hasErrors || declaredType.HasErrors())
		{
			this.DeclaredType = declaredType;
			this.IsExplicitNotNullTest = isExplicitNotNullTest;
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06002031 RID: 8241 RVA: 0x000A3D4B File Offset: 0x000A1F4B
		public BoundTypeExpression DeclaredType { get; }

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06002032 RID: 8242 RVA: 0x000A3D53 File Offset: 0x000A1F53
		public bool IsExplicitNotNullTest { get; }

		// Token: 0x06002033 RID: 8243 RVA: 0x000A3D5B File Offset: 0x000A1F5B
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitTypePattern(this);
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x000A3D64 File Offset: 0x000A1F64
		public BoundTypePattern Update(BoundTypeExpression declaredType, bool isExplicitNotNullTest, TypeSymbol inputType, TypeSymbol narrowedType)
		{
			if (declaredType != this.DeclaredType || isExplicitNotNullTest != this.IsExplicitNotNullTest || !TypeSymbol.Equals(inputType, base.InputType, TypeCompareKind.ConsiderEverything) || !TypeSymbol.Equals(narrowedType, base.NarrowedType, TypeCompareKind.ConsiderEverything))
			{
				BoundTypePattern boundTypePattern = new BoundTypePattern(this.Syntax, declaredType, isExplicitNotNullTest, inputType, narrowedType, base.HasErrors);
				boundTypePattern.CopyAttributes(this);
				return boundTypePattern;
			}
			return this;
		}
	}
}
