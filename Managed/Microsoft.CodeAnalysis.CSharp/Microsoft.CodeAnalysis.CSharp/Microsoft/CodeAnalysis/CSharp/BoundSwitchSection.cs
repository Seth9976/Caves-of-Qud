using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020001B9 RID: 441
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundSwitchSection : BoundStatementList
	{
		// Token: 0x06001F76 RID: 8054 RVA: 0x000A2784 File Offset: 0x000A0984
		public BoundSwitchSection(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundSwitchLabel> switchLabels, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundStatement> statements, bool hasErrors = false)
			: base(BoundKind.SwitchSection, syntax, statements, hasErrors || switchLabels.HasErrors<BoundSwitchLabel>() || statements.HasErrors<BoundStatement>())
		{
			this.Locals = locals;
			this.SwitchLabels = switchLabels;
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001F77 RID: 8055 RVA: 0x000A27B8 File Offset: 0x000A09B8
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<LocalSymbol> Locals
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001F78 RID: 8056 RVA: 0x000A27C0 File Offset: 0x000A09C0
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundSwitchLabel> SwitchLabels
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x000A27C8 File Offset: 0x000A09C8
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitSwitchSection(this);
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x000A27D4 File Offset: 0x000A09D4
		public BoundSwitchSection Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundSwitchLabel> switchLabels, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundStatement> statements)
		{
			if (locals != this.Locals || switchLabels != this.SwitchLabels || statements != base.Statements)
			{
				BoundSwitchSection boundSwitchSection = new BoundSwitchSection(this.Syntax, locals, switchLabels, statements, base.HasErrors);
				boundSwitchSection.CopyAttributes(this);
				return boundSwitchSection;
			}
			return this;
		}
	}
}
