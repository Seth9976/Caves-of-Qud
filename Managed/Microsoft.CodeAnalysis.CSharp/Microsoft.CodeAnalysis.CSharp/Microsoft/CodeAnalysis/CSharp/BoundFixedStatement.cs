using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000F3 RID: 243
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundFixedStatement : BoundStatement
	{
		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x00063BB4 File Offset: 0x00061DB4
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
		protected override ImmutableArray<BoundNode> Children
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 2 })]
			get
			{
				return ImmutableArray.Create<BoundNode>(this.Declarations, this.Body);
			}
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x00063BC7 File Offset: 0x00061DC7
		public BoundFixedStatement(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, BoundMultipleLocalDeclarations declarations, BoundStatement body, bool hasErrors = false)
			: base(BoundKind.FixedStatement, syntax, hasErrors || declarations.HasErrors() || body.HasErrors())
		{
			this.Locals = locals;
			this.Declarations = declarations;
			this.Body = body;
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x00063BFE File Offset: 0x00061DFE
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<LocalSymbol> Locals
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060012C5 RID: 4805 RVA: 0x00063C06 File Offset: 0x00061E06
		public BoundMultipleLocalDeclarations Declarations { get; }

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060012C6 RID: 4806 RVA: 0x00063C0E File Offset: 0x00061E0E
		public BoundStatement Body { get; }

		// Token: 0x060012C7 RID: 4807 RVA: 0x00063C16 File Offset: 0x00061E16
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitFixedStatement(this);
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x00063C1F File Offset: 0x00061E1F
		public BoundFixedStatement Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, BoundMultipleLocalDeclarations declarations, BoundStatement body)
		{
			if (locals != this.Locals || declarations != this.Declarations || body != this.Body)
			{
				BoundFixedStatement boundFixedStatement = new BoundFixedStatement(this.Syntax, locals, declarations, body, base.HasErrors);
				boundFixedStatement.CopyAttributes(this);
				return boundFixedStatement;
			}
			return this;
		}
	}
}
