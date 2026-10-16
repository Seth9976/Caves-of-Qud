using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x020000CE RID: 206
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundBlock : BoundStatementList
	{
		// Token: 0x06001119 RID: 4377 RVA: 0x0005DB63 File Offset: 0x0005BD63
		public BoundBlock(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundStatement> statements, bool hasErrors = false)
			: this(syntax, locals, ImmutableArray<LocalFunctionSymbol>.Empty, statements, hasErrors)
		{
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x0005DB75 File Offset: 0x0005BD75
		public static BoundBlock SynthesizedNoLocals(SyntaxNode syntax, BoundStatement statement)
		{
			return new BoundBlock(syntax, ImmutableArray<LocalSymbol>.Empty, ImmutableArray.Create<BoundStatement>(statement), false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x0005DB90 File Offset: 0x0005BD90
		public static BoundBlock SynthesizedNoLocals(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundStatement> statements)
		{
			return new BoundBlock(syntax, ImmutableArray<LocalSymbol>.Empty, statements, false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x0005DBA6 File Offset: 0x0005BDA6
		public static BoundBlock SynthesizedNoLocals(SyntaxNode syntax, params BoundStatement[] statements)
		{
			return new BoundBlock(syntax, ImmutableArray<LocalSymbol>.Empty, statements.AsImmutableOrNull<BoundStatement>(), false)
			{
				WasCompilerGenerated = true
			};
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x0005DBC1 File Offset: 0x0005BDC1
		public BoundBlock(SyntaxNode syntax, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalFunctionSymbol> localFunctions, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundStatement> statements, bool hasErrors = false)
			: base(BoundKind.Block, syntax, statements, hasErrors || statements.HasErrors<BoundStatement>())
		{
			this.Locals = locals;
			this.LocalFunctions = localFunctions;
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x0600111E RID: 4382 RVA: 0x0005DBEA File Offset: 0x0005BDEA
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<LocalSymbol> Locals
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x0600111F RID: 4383 RVA: 0x0005DBF2 File Offset: 0x0005BDF2
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<LocalFunctionSymbol> LocalFunctions
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0005DBFA File Offset: 0x0005BDFA
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitBlock(this);
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x0005DC04 File Offset: 0x0005BE04
		public BoundBlock Update([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalSymbol> locals, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<LocalFunctionSymbol> localFunctions, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundStatement> statements)
		{
			if (locals != this.Locals || localFunctions != this.LocalFunctions || statements != base.Statements)
			{
				BoundBlock boundBlock = new BoundBlock(this.Syntax, locals, localFunctions, statements, base.HasErrors);
				boundBlock.CopyAttributes(this);
				return boundBlock;
			}
			return this;
		}
	}
}
