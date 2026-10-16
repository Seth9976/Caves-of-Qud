using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000E4 RID: 228
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundIndexOrRangePatternIndexerAccess : BoundExpression
	{
		// Token: 0x1700032D RID: 813
		// (get) Token: 0x060011BB RID: 4539 RVA: 0x0005EC95 File Offset: 0x0005CE95
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		protected override ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				return ImmutableArray.Create<BoundNode>(this.Receiver, this.Argument);
			}
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0005ECA8 File Offset: 0x0005CEA8
		public BoundIndexOrRangePatternIndexerAccess(SyntaxNode syntax, BoundExpression receiver, PropertySymbol lengthOrCountProperty, Symbol patternSymbol, BoundExpression argument, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.IndexOrRangePatternIndexerAccess, syntax, type, hasErrors || receiver.HasErrors() || argument.HasErrors())
		{
			this.Receiver = receiver;
			this.LengthOrCountProperty = lengthOrCountProperty;
			this.PatternSymbol = patternSymbol;
			this.Argument = argument;
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x0005ECF7 File Offset: 0x0005CEF7
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x060011BE RID: 4542 RVA: 0x0005ECFF File Offset: 0x0005CEFF
		public BoundExpression Receiver { get; }

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x060011BF RID: 4543 RVA: 0x0005ED07 File Offset: 0x0005CF07
		public PropertySymbol LengthOrCountProperty { get; }

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x0005ED0F File Offset: 0x0005CF0F
		public Symbol PatternSymbol { get; }

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x0005ED17 File Offset: 0x0005CF17
		public BoundExpression Argument { get; }

		// Token: 0x060011C2 RID: 4546 RVA: 0x0005ED1F File Offset: 0x0005CF1F
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitIndexOrRangePatternIndexerAccess(this);
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x0005ED28 File Offset: 0x0005CF28
		public BoundIndexOrRangePatternIndexerAccess Update(BoundExpression receiver, PropertySymbol lengthOrCountProperty, Symbol patternSymbol, BoundExpression argument, TypeSymbol type)
		{
			if (receiver != this.Receiver || !SymbolEqualityComparer.ConsiderEverything.Equals(lengthOrCountProperty, this.LengthOrCountProperty) || !SymbolEqualityComparer.ConsiderEverything.Equals(patternSymbol, this.PatternSymbol) || argument != this.Argument || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundIndexOrRangePatternIndexerAccess boundIndexOrRangePatternIndexerAccess = new BoundIndexOrRangePatternIndexerAccess(this.Syntax, receiver, lengthOrCountProperty, patternSymbol, argument, type, base.HasErrors);
				boundIndexOrRangePatternIndexerAccess.CopyAttributes(this);
				return boundIndexOrRangePatternIndexerAccess;
			}
			return this;
		}
	}
}
