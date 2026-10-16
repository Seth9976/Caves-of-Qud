using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000190 RID: 400
	[global::System.Runtime.CompilerServices.NullableContext(2)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundUsingLocalDeclarations : BoundMultipleLocalDeclarationsBase
	{
		// Token: 0x06001E95 RID: 7829 RVA: 0x000A0FA7 File Offset: 0x0009F1A7
		public BoundUsingLocalDeclarations([global::System.Runtime.CompilerServices.Nullable(1)] SyntaxNode syntax, MethodArgumentInfo patternDisposeInfoOpt, Conversion iDisposableConversion, BoundAwaitableInfo awaitOpt, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundLocalDeclaration> localDeclarations, bool hasErrors = false)
			: base(BoundKind.UsingLocalDeclarations, syntax, localDeclarations, hasErrors || awaitOpt.HasErrors() || localDeclarations.HasErrors<BoundLocalDeclaration>())
		{
			this.PatternDisposeInfoOpt = patternDisposeInfoOpt;
			this.IDisposableConversion = iDisposableConversion;
			this.AwaitOpt = awaitOpt;
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06001E96 RID: 7830 RVA: 0x000A0FE1 File Offset: 0x0009F1E1
		public MethodArgumentInfo PatternDisposeInfoOpt { get; }

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06001E97 RID: 7831 RVA: 0x000A0FE9 File Offset: 0x0009F1E9
		public Conversion IDisposableConversion { get; }

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001E98 RID: 7832 RVA: 0x000A0FF1 File Offset: 0x0009F1F1
		public BoundAwaitableInfo AwaitOpt { get; }

		// Token: 0x06001E99 RID: 7833 RVA: 0x000A0FF9 File Offset: 0x0009F1F9
		[global::System.Runtime.CompilerServices.NullableContext(1)]
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitUsingLocalDeclarations(this);
		}

		// Token: 0x06001E9A RID: 7834 RVA: 0x000A1004 File Offset: 0x0009F204
		[return: global::System.Runtime.CompilerServices.Nullable(1)]
		public BoundUsingLocalDeclarations Update(MethodArgumentInfo patternDisposeInfoOpt, Conversion iDisposableConversion, BoundAwaitableInfo awaitOpt, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundLocalDeclaration> localDeclarations)
		{
			if (patternDisposeInfoOpt != this.PatternDisposeInfoOpt || iDisposableConversion != this.IDisposableConversion || awaitOpt != this.AwaitOpt || localDeclarations != base.LocalDeclarations)
			{
				BoundUsingLocalDeclarations boundUsingLocalDeclarations = new BoundUsingLocalDeclarations(this.Syntax, patternDisposeInfoOpt, iDisposableConversion, awaitOpt, localDeclarations, base.HasErrors);
				boundUsingLocalDeclarations.CopyAttributes(this);
				return boundUsingLocalDeclarations;
			}
			return this;
		}
	}
}
