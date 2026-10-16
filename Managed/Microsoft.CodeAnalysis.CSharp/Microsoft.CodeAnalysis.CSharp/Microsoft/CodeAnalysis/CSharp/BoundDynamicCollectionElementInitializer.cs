using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001C9 RID: 457
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundDynamicCollectionElementInitializer : BoundDynamicInvocableBase
	{
		// Token: 0x06001FCC RID: 8140 RVA: 0x000A3130 File Offset: 0x000A1330
		public BoundDynamicCollectionElementInitializer(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> applicableMethods, BoundExpression expression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.DynamicCollectionElementInitializer, syntax, expression, arguments, type, hasErrors || expression.HasErrors() || arguments.HasErrors<BoundExpression>())
		{
			this.ApplicableMethods = applicableMethods;
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001FCD RID: 8141 RVA: 0x000A3160 File Offset: 0x000A1360
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001FCE RID: 8142 RVA: 0x000A3168 File Offset: 0x000A1368
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<MethodSymbol> ApplicableMethods
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x000A3170 File Offset: 0x000A1370
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitDynamicCollectionElementInitializer(this);
		}

		// Token: 0x06001FD0 RID: 8144 RVA: 0x000A317C File Offset: 0x000A137C
		public BoundDynamicCollectionElementInitializer Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<MethodSymbol> applicableMethods, BoundExpression expression, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, TypeSymbol type)
		{
			if (applicableMethods != this.ApplicableMethods || expression != base.Expression || arguments != base.Arguments || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundDynamicCollectionElementInitializer boundDynamicCollectionElementInitializer = new BoundDynamicCollectionElementInitializer(this.Syntax, applicableMethods, expression, arguments, type, base.HasErrors);
				boundDynamicCollectionElementInitializer.CopyAttributes(this);
				return boundDynamicCollectionElementInitializer;
			}
			return this;
		}
	}
}
