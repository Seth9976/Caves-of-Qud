using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200013F RID: 319
	public abstract class ControlFlowAnalysis
	{
		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000CF0 RID: 3312
		[Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<SyntaxNode> EntryPoints
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000CF1 RID: 3313
		[Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<SyntaxNode> ExitPoints
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000CF2 RID: 3314
		public abstract bool EndPointIsReachable { get; }

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000CF3 RID: 3315
		public abstract bool StartPointIsReachable { get; }

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000CF4 RID: 3316
		[Nullable(new byte[] { 0, 1 })]
		public abstract ImmutableArray<SyntaxNode> ReturnStatements
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000CF5 RID: 3317
		public abstract bool Succeeded { get; }
	}
}
