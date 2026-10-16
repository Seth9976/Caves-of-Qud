using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000A0 RID: 160
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundAnonymousPropertyDeclaration : BoundExpression
	{
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x00054AE7 File Offset: 0x00052CE7
		public override Symbol ExpressionSymbol
		{
			get
			{
				return this.Property;
			}
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x00054AEF File Offset: 0x00052CEF
		public BoundAnonymousPropertyDeclaration(SyntaxNode syntax, PropertySymbol property, TypeSymbol type, bool hasErrors)
			: base(BoundKind.AnonymousPropertyDeclaration, syntax, type, hasErrors)
		{
			this.Property = property;
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x00054B07 File Offset: 0x00052D07
		public BoundAnonymousPropertyDeclaration(SyntaxNode syntax, PropertySymbol property, TypeSymbol type)
			: base(BoundKind.AnonymousPropertyDeclaration, syntax, type)
		{
			this.Property = property;
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x00054B1D File Offset: 0x00052D1D
		public new TypeSymbol Type
		{
			get
			{
				return base.Type;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x00054B25 File Offset: 0x00052D25
		public PropertySymbol Property { get; }

		// Token: 0x06000C86 RID: 3206 RVA: 0x00054B2D File Offset: 0x00052D2D
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitAnonymousPropertyDeclaration(this);
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x00054B36 File Offset: 0x00052D36
		public BoundAnonymousPropertyDeclaration Update(PropertySymbol property, TypeSymbol type)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(property, this.Property) || !TypeSymbol.Equals(type, this.Type, TypeCompareKind.ConsiderEverything))
			{
				BoundAnonymousPropertyDeclaration boundAnonymousPropertyDeclaration = new BoundAnonymousPropertyDeclaration(this.Syntax, property, type, base.HasErrors);
				boundAnonymousPropertyDeclaration.CopyAttributes(this);
				return boundAnonymousPropertyDeclaration;
			}
			return this;
		}
	}
}
