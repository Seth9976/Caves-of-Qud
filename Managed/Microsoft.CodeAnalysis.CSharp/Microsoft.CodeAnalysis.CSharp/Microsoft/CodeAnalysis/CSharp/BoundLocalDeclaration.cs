using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x0200018D RID: 397
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class BoundLocalDeclaration : BoundStatement
	{
		// Token: 0x06001E88 RID: 7816 RVA: 0x000A0E48 File Offset: 0x0009F048
		public BoundLocalDeclaration(SyntaxNode syntax, LocalSymbol localSymbol, [global::System.Runtime.CompilerServices.Nullable(2)] BoundTypeExpression declaredTypeOpt, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression initializerOpt, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> argumentsOpt, bool inferredType, bool hasErrors = false)
			: base(BoundKind.LocalDeclaration, syntax, hasErrors || declaredTypeOpt.HasErrors() || initializerOpt.HasErrors() || argumentsOpt.HasErrors<BoundExpression>())
		{
			this.LocalSymbol = localSymbol;
			this.DeclaredTypeOpt = declaredTypeOpt;
			this.InitializerOpt = initializerOpt;
			this.ArgumentsOpt = argumentsOpt;
			this.InferredType = inferredType;
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x000A0EA3 File Offset: 0x0009F0A3
		public LocalSymbol LocalSymbol { get; }

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001E8A RID: 7818 RVA: 0x000A0EAB File Offset: 0x0009F0AB
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundTypeExpression DeclaredTypeOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06001E8B RID: 7819 RVA: 0x000A0EB3 File Offset: 0x0009F0B3
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public BoundExpression InitializerOpt
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get;
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001E8C RID: 7820 RVA: 0x000A0EBB File Offset: 0x0009F0BB
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<BoundExpression> ArgumentsOpt
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001E8D RID: 7821 RVA: 0x000A0EC3 File Offset: 0x0009F0C3
		public bool InferredType { get; }

		// Token: 0x06001E8E RID: 7822 RVA: 0x000A0ECB File Offset: 0x0009F0CB
		[DebuggerStepThrough]
		[return: global::System.Runtime.CompilerServices.Nullable(2)]
		public override BoundNode Accept(BoundTreeVisitor visitor)
		{
			return visitor.VisitLocalDeclaration(this);
		}

		// Token: 0x06001E8F RID: 7823 RVA: 0x000A0ED4 File Offset: 0x0009F0D4
		public BoundLocalDeclaration Update(LocalSymbol localSymbol, [global::System.Runtime.CompilerServices.Nullable(2)] BoundTypeExpression declaredTypeOpt, [global::System.Runtime.CompilerServices.Nullable(2)] BoundExpression initializerOpt, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<BoundExpression> argumentsOpt, bool inferredType)
		{
			if (!SymbolEqualityComparer.ConsiderEverything.Equals(localSymbol, this.LocalSymbol) || declaredTypeOpt != this.DeclaredTypeOpt || initializerOpt != this.InitializerOpt || argumentsOpt != this.ArgumentsOpt || inferredType != this.InferredType)
			{
				BoundLocalDeclaration boundLocalDeclaration = new BoundLocalDeclaration(this.Syntax, localSymbol, declaredTypeOpt, initializerOpt, argumentsOpt, inferredType, base.HasErrors);
				boundLocalDeclaration.CopyAttributes(this);
				return boundLocalDeclaration;
			}
			return this;
		}
	}
}
