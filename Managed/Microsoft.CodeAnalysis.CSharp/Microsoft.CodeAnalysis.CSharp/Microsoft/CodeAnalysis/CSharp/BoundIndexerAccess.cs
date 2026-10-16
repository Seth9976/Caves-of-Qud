using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Operations;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000092 RID: 146
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundIndexerAccess : BoundExpression, IBoundInvalidNode
	{
		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x000534B9 File Offset: 0x000516B9
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public override Symbol ExpressionSymbol
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return this.Indexer;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x000534C4 File Offset: 0x000516C4
		public override LookupResultKind ResultKind
		{
			get
			{
				if (this.OriginalIndexersOpt.IsDefault)
				{
					return base.ResultKind;
				}
				return LookupResultKind.OverloadResolutionFailure;
			}
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x000534EC File Offset: 0x000516EC
		public static BoundIndexerAccess ErrorAccess(SyntaxNode node, BoundExpression receiverOpt, PropertySymbol indexer, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> namedArguments, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> refKinds, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<PropertySymbol> originalIndexers)
		{
			return new BoundIndexerAccess(node, receiverOpt, indexer, arguments, namedArguments, refKinds, false, default(ImmutableArray<int>), default(BitVector), originalIndexers, indexer.Type, true);
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x00053524 File Offset: 0x00051724
		public BoundIndexerAccess(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, PropertySymbol indexer, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNamesOpt, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> argumentRefKindsOpt, bool expanded, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<int> argsToParamsOpt, BitVector defaultArguments, TypeSymbol type, bool hasErrors = false)
			: this(syntax, receiverOpt, indexer, arguments, argumentNamesOpt, argumentRefKindsOpt, expanded, argsToParamsOpt, defaultArguments, default(ImmutableArray<PropertySymbol>), type, hasErrors)
		{
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x00053554 File Offset: 0x00051754
		public BoundIndexerAccess Update([global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, PropertySymbol indexer, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNamesOpt, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> argumentRefKindsOpt, bool expanded, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<int> argsToParamsOpt, BitVector defaultArguments, TypeSymbol type)
		{
			return this.Update(receiverOpt, indexer, arguments, argumentNamesOpt, argumentRefKindsOpt, expanded, argsToParamsOpt, defaultArguments, this.OriginalIndexersOpt, type);
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x0005357C File Offset: 0x0005177C
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		ImmutableArray<BoundNode> IBoundInvalidNode.InvalidNodeChildren
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return CSharpOperationFactory.CreateInvalidChildrenFromArgumentsExpression(this.ReceiverOpt, this.Arguments, null);
			}
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x00053590 File Offset: 0x00051790
		public BoundIndexerAccess(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, PropertySymbol indexer, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNamesOpt, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> argumentRefKindsOpt, bool expanded, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<int> argsToParamsOpt, BitVector defaultArguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<PropertySymbol> originalIndexersOpt, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.IndexerAccess, syntax, type, hasErrors || receiverOpt.HasErrors() || arguments.HasErrors<BoundExpression>())
		{
			this.ReceiverOpt = receiverOpt;
			this.Indexer = indexer;
			this.Arguments = arguments;
			this.ArgumentNamesOpt = argumentNamesOpt;
			this.ArgumentRefKindsOpt = argumentRefKindsOpt;
			this.Expanded = expanded;
			this.ArgsToParamsOpt = argsToParamsOpt;
			this.DefaultArguments = defaultArguments;
			this.OriginalIndexersOpt = originalIndexersOpt;
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x00053607 File Offset: 0x00051807
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x0005360F File Offset: 0x0005180F
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundExpression ReceiverOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x00053617 File Offset: 0x00051817
		public PropertySymbol Indexer { get; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0005361F File Offset: 0x0005181F
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundExpression> Arguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x00053627 File Offset: 0x00051827
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<string> ArgumentNamesOpt
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x0005362F File Offset: 0x0005182F
		[global::System.Runtime.CompilerServices.Nullable(0)]
		public ImmutableArray<RefKind> ArgumentRefKindsOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get;
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x00053637 File Offset: 0x00051837
		public bool Expanded { get; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x0005363F File Offset: 0x0005183F
		[global::System.Runtime.CompilerServices.Nullable(0)]
		public ImmutableArray<int> ArgsToParamsOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(0)]
			get;
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x00053647 File Offset: 0x00051847
		public BitVector DefaultArguments { get; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x0005364F File Offset: 0x0005184F
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<PropertySymbol> OriginalIndexersOpt
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00053657 File Offset: 0x00051857
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitIndexerAccess(this);
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x00053660 File Offset: 0x00051860
		public BoundIndexerAccess Update([global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression receiverOpt, PropertySymbol indexer, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<string> argumentNamesOpt, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<RefKind> argumentRefKindsOpt, bool expanded, [global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<int> argsToParamsOpt, BitVector defaultArguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<PropertySymbol> originalIndexersOpt, TypeSymbol type)
		{
			if (receiverOpt != this.ReceiverOpt || !SymbolEqualityComparer.ConsiderEverything.Equals(indexer, this.Indexer) || arguments != this.Arguments || argumentNamesOpt != this.ArgumentNamesOpt || argumentRefKindsOpt != this.ArgumentRefKindsOpt || expanded != this.Expanded || argsToParamsOpt != this.ArgsToParamsOpt || defaultArguments != this.DefaultArguments || originalIndexersOpt != this.OriginalIndexersOpt || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundIndexerAccess boundIndexerAccess = new BoundIndexerAccess(this.Syntax, receiverOpt, indexer, arguments, argumentNamesOpt, argumentRefKindsOpt, expanded, argsToParamsOpt, defaultArguments, originalIndexersOpt, type, base.HasErrors);
				boundIndexerAccess.CopyAttributes(this);
				return boundIndexerAccess;
			}
			return this;
		}
	}
}
