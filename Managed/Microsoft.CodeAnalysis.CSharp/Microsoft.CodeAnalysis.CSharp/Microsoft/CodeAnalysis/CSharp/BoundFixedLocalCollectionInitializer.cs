using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000DC RID: 220
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundFixedLocalCollectionInitializer : BoundExpression
	{
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06001185 RID: 4485 RVA: 0x0005E65B File Offset: 0x0005C85B
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		protected override ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				return ImmutableArray.Create<BoundNode>(this.Expression);
			}
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x0005E668 File Offset: 0x0005C868
		public BoundFixedLocalCollectionInitializer(SyntaxNode syntax, TypeSymbol elementPointerType, Conversion elementPointerTypeConversion, BoundExpression expression, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol getPinnableOpt, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.FixedLocalCollectionInitializer, syntax, type, hasErrors || expression.HasErrors())
		{
			this.ElementPointerType = elementPointerType;
			this.ElementPointerTypeConversion = elementPointerTypeConversion;
			this.Expression = expression;
			this.GetPinnableOpt = getPinnableOpt;
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x0005E6A1 File Offset: 0x0005C8A1
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x0005E6A9 File Offset: 0x0005C8A9
		public TypeSymbol ElementPointerType { get; }

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x0005E6B1 File Offset: 0x0005C8B1
		public Conversion ElementPointerTypeConversion { get; }

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x0600118A RID: 4490 RVA: 0x0005E6B9 File Offset: 0x0005C8B9
		public BoundExpression Expression { get; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x0005E6C1 File Offset: 0x0005C8C1
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public MethodSymbol GetPinnableOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x0005E6C9 File Offset: 0x0005C8C9
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitFixedLocalCollectionInitializer(this);
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x0005E6D4 File Offset: 0x0005C8D4
		public BoundFixedLocalCollectionInitializer Update(TypeSymbol elementPointerType, Conversion elementPointerTypeConversion, BoundExpression expression, [global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol getPinnableOpt, TypeSymbol type)
		{
			if (!TypeSymbol.Equals(elementPointerType, this.ElementPointerType, TypeCompareKind.ConsiderEverything) || elementPointerTypeConversion != this.ElementPointerTypeConversion || expression != this.Expression || !SymbolEqualityComparer.ConsiderEverything.Equals(getPinnableOpt, this.GetPinnableOpt) || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundFixedLocalCollectionInitializer boundFixedLocalCollectionInitializer = new BoundFixedLocalCollectionInitializer(this.Syntax, elementPointerType, elementPointerTypeConversion, expression, getPinnableOpt, type, base.HasErrors);
				boundFixedLocalCollectionInitializer.CopyAttributes(this);
				return boundFixedLocalCollectionInitializer;
			}
			return this;
		}
	}
}
