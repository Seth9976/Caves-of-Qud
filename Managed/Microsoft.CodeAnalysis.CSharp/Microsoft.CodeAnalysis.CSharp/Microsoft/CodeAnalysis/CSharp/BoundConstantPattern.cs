using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001D5 RID: 469
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundConstantPattern : BoundPattern
	{
		// Token: 0x0600200B RID: 8203 RVA: 0x000A3843 File Offset: 0x000A1A43
		public BoundConstantPattern(SyntaxNode syntax, BoundExpression value, ConstantValue constantValue, TypeSymbol inputType, TypeSymbol narrowedType, bool hasErrors = false)
			: base(BoundKind.ConstantPattern, syntax, inputType, narrowedType, hasErrors || value.HasErrors())
		{
			this.Value = value;
			this.ConstantValue = constantValue;
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x0600200C RID: 8204 RVA: 0x000A3870 File Offset: 0x000A1A70
		public BoundExpression Value { get; }

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x0600200D RID: 8205 RVA: 0x000A3878 File Offset: 0x000A1A78
		public ConstantValue ConstantValue { get; }

		// Token: 0x0600200E RID: 8206 RVA: 0x000A3880 File Offset: 0x000A1A80
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitConstantPattern(this);
		}

		// Token: 0x0600200F RID: 8207 RVA: 0x000A388C File Offset: 0x000A1A8C
		public BoundConstantPattern Update(BoundExpression value, ConstantValue constantValue, TypeSymbol inputType, TypeSymbol narrowedType)
		{
			if (value != this.Value || constantValue != this.ConstantValue || !TypeSymbol.Equals(inputType, base.InputType, TypeCompareKind.ConsiderEverything) || !TypeSymbol.Equals(narrowedType, base.NarrowedType, TypeCompareKind.ConsiderEverything))
			{
				BoundConstantPattern boundConstantPattern = new BoundConstantPattern(this.Syntax, value, constantValue, inputType, narrowedType, base.HasErrors);
				boundConstantPattern.CopyAttributes(this);
				return boundConstantPattern;
			}
			return this;
		}
	}
}
