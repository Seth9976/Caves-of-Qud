using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200009F RID: 159
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundAnonymousObjectCreationExpression : BoundExpression
	{
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x00054A02 File Offset: 0x00052C02
		public override Symbol ExpressionSymbol
		{
			get
			{
				return this.Constructor;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x00054A0A File Offset: 0x00052C0A
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		protected override ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				return StaticCast<BoundNode>.From<BoundExpression>(this.Arguments);
			}
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x00054A17 File Offset: 0x00052C17
		public BoundAnonymousObjectCreationExpression(SyntaxNode syntax, MethodSymbol constructor, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundAnonymousPropertyDeclaration> declarations, TypeSymbol type, bool hasErrors = false)
			: base(BoundKind.AnonymousObjectCreationExpression, syntax, type, hasErrors || arguments.HasErrors<BoundExpression>() || declarations.HasErrors<BoundAnonymousPropertyDeclaration>())
		{
			this.Constructor = constructor;
			this.Arguments = arguments;
			this.Declarations = declarations;
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x00054A53 File Offset: 0x00052C53
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x00054A5B File Offset: 0x00052C5B
		public MethodSymbol Constructor { get; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x00054A63 File Offset: 0x00052C63
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundExpression> Arguments
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x00054A6B File Offset: 0x00052C6B
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundAnonymousPropertyDeclaration> Declarations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x00054A73 File Offset: 0x00052C73
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitAnonymousObjectCreationExpression(this);
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x00054A7C File Offset: 0x00052C7C
		public BoundAnonymousObjectCreationExpression Update(MethodSymbol constructor, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> arguments, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundAnonymousPropertyDeclaration> declarations, TypeSymbol type)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(constructor, this.Constructor) || arguments != this.Arguments || declarations != this.Declarations || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundAnonymousObjectCreationExpression boundAnonymousObjectCreationExpression = new BoundAnonymousObjectCreationExpression(this.Syntax, constructor, arguments, declarations, type, base.HasErrors);
				boundAnonymousObjectCreationExpression.CopyAttributes(this);
				return boundAnonymousObjectCreationExpression;
			}
			return this;
		}
	}
}
