using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001DE RID: 478
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundRelationalPattern : BoundPattern
	{
		// Token: 0x0600203F RID: 8255 RVA: 0x000A3F11 File Offset: 0x000A2111
		public BoundRelationalPattern(SyntaxNode syntax, BinaryOperatorKind relation, BoundExpression value, ConstantValue constantValue, TypeSymbol inputType, TypeSymbol narrowedType, bool hasErrors = false)
			: base(BoundKind.RelationalPattern, syntax, inputType, narrowedType, hasErrors || value.HasErrors())
		{
			this.Relation = relation;
			this.Value = value;
			this.ConstantValue = constantValue;
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06002040 RID: 8256 RVA: 0x000A3F46 File Offset: 0x000A2146
		public BinaryOperatorKind Relation { get; }

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06002041 RID: 8257 RVA: 0x000A3F4E File Offset: 0x000A214E
		public BoundExpression Value { get; }

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06002042 RID: 8258 RVA: 0x000A3F56 File Offset: 0x000A2156
		public ConstantValue ConstantValue { get; }

		// Token: 0x06002043 RID: 8259 RVA: 0x000A3F5E File Offset: 0x000A215E
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitRelationalPattern(this);
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x000A3F68 File Offset: 0x000A2168
		public BoundRelationalPattern Update(BinaryOperatorKind relation, BoundExpression value, ConstantValue constantValue, TypeSymbol inputType, TypeSymbol narrowedType)
		{
			if (relation != this.Relation || value != this.Value || constantValue != this.ConstantValue || !TypeSymbol.Equals(inputType, base.InputType, TypeCompareKind.ConsiderEverything) || !TypeSymbol.Equals(narrowedType, base.NarrowedType, TypeCompareKind.ConsiderEverything))
			{
				BoundRelationalPattern boundRelationalPattern = new BoundRelationalPattern(this.Syntax, relation, value, constantValue, inputType, narrowedType, base.HasErrors);
				boundRelationalPattern.CopyAttributes(this);
				return boundRelationalPattern;
			}
			return this;
		}
	}
}
