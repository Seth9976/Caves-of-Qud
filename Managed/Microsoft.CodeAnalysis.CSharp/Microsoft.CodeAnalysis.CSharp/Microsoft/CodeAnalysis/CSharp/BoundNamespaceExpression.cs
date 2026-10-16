using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200008E RID: 142
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundNamespaceExpression : BoundExpression
	{
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x00052FC1 File Offset: 0x000511C1
		public override Symbol ExpressionSymbol
		{
			get
			{
				return this.AliasOpt ?? this.NamespaceSymbol;
			}
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00052FD3 File Offset: 0x000511D3
		public BoundNamespaceExpression(SyntaxNode syntax, NamespaceSymbol namespaceSymbol, bool hasErrors = false)
			: this(syntax, namespaceSymbol, null, hasErrors)
		{
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00052FDF File Offset: 0x000511DF
		public BoundNamespaceExpression(SyntaxNode syntax, NamespaceSymbol namespaceSymbol)
			: this(syntax, namespaceSymbol, null)
		{
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00052FEA File Offset: 0x000511EA
		public BoundNamespaceExpression Update(NamespaceSymbol namespaceSymbol)
		{
			return this.Update(namespaceSymbol, this.AliasOpt);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00052FF9 File Offset: 0x000511F9
		public BoundNamespaceExpression(SyntaxNode syntax, NamespaceSymbol namespaceSymbol, [global::System.Runtime.CompilerServices.Nullable(2)] AliasSymbol aliasOpt, bool hasErrors)
			: base(BoundKind.NamespaceExpression, syntax, null, hasErrors)
		{
			this.NamespaceSymbol = namespaceSymbol;
			this.AliasOpt = aliasOpt;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00053015 File Offset: 0x00051215
		public BoundNamespaceExpression(SyntaxNode syntax, NamespaceSymbol namespaceSymbol, [global::System.Runtime.CompilerServices.Nullable(2)] AliasSymbol aliasOpt)
			: base(BoundKind.NamespaceExpression, syntax, null)
		{
			this.NamespaceSymbol = namespaceSymbol;
			this.AliasOpt = aliasOpt;
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000B9D RID: 2973 RVA: 0x0005302F File Offset: 0x0005122F
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public new TypeSymbol Type
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return base.Type;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x00053037 File Offset: 0x00051237
		public NamespaceSymbol NamespaceSymbol { get; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000B9F RID: 2975 RVA: 0x0005303F File Offset: 0x0005123F
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public AliasSymbol AliasOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00053047 File Offset: 0x00051247
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitNamespaceExpression(this);
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x00053050 File Offset: 0x00051250
		public BoundNamespaceExpression Update(NamespaceSymbol namespaceSymbol, [global::System.Runtime.CompilerServices.Nullable(2)] AliasSymbol aliasOpt)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(namespaceSymbol, this.NamespaceSymbol) || !SymbolEqualityComparer.ConsiderEverything.Equals(aliasOpt, this.AliasOpt))
			{
				BoundNamespaceExpression boundNamespaceExpression = new BoundNamespaceExpression(this.Syntax, namespaceSymbol, aliasOpt, base.HasErrors);
				boundNamespaceExpression.CopyAttributes(this);
				return boundNamespaceExpression;
			}
			return this;
		}
	}
}
