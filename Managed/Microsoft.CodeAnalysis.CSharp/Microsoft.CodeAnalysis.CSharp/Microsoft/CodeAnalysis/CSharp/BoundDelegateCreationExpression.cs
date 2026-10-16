using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001CC RID: 460
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundDelegateCreationExpression : BoundExpression
	{
		// Token: 0x06001FDB RID: 8155 RVA: 0x000A32AB File Offset: 0x000A14AB
		public BoundDelegateCreationExpression(SyntaxNode syntax, BoundExpression argument, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol methodOpt, bool isExtensionMethod, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.DelegateCreationExpression, syntax, type, hasErrors || argument.HasErrors())
		{
			this.Argument = argument;
			this.MethodOpt = methodOpt;
			this.IsExtensionMethod = isExtensionMethod;
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001FDC RID: 8156 RVA: 0x000A32DE File Offset: 0x000A14DE
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001FDD RID: 8157 RVA: 0x000A32E6 File Offset: 0x000A14E6
		public BoundExpression Argument { get; }

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001FDE RID: 8158 RVA: 0x000A32EE File Offset: 0x000A14EE
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public MethodSymbol MethodOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x06001FDF RID: 8159 RVA: 0x000A32F6 File Offset: 0x000A14F6
		public bool IsExtensionMethod { get; }

		// Token: 0x06001FE0 RID: 8160 RVA: 0x000A32FE File Offset: 0x000A14FE
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDelegateCreationExpression(this);
		}

		// Token: 0x06001FE1 RID: 8161 RVA: 0x000A3308 File Offset: 0x000A1508
		public BoundDelegateCreationExpression Update(BoundExpression argument, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol methodOpt, bool isExtensionMethod, TypeSymbol type)
		{
			if (argument != this.Argument || !SymbolEqualityComparer.ConsiderEverything.Equals(methodOpt, this.MethodOpt) || isExtensionMethod != this.IsExtensionMethod || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundDelegateCreationExpression boundDelegateCreationExpression = new BoundDelegateCreationExpression(this.Syntax, argument, methodOpt, isExtensionMethod, type, base.HasErrors);
				boundDelegateCreationExpression.CopyAttributes(this);
				return boundDelegateCreationExpression;
			}
			return this;
		}
	}
}
