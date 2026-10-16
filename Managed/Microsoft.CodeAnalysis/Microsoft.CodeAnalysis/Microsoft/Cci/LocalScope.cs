using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;

namespace Microsoft.Cci
{
	// Token: 0x02000093 RID: 147
	internal struct LocalScope
	{
		// Token: 0x060003E8 RID: 1000 RVA: 0x0000DD18 File Offset: 0x0000BF18
		internal LocalScope(int offset, int endOffset, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ILocalDefinition> constants, [Nullable(new byte[] { 0, 1 })] ImmutableArray<ILocalDefinition> locals)
		{
			this.StartOffset = offset;
			this.EndOffset = endOffset;
			this._constants = constants;
			this._locals = locals;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000DD37 File Offset: 0x0000BF37
		public int Length
		{
			get
			{
				return this.EndOffset - this.StartOffset;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0000DD46 File Offset: 0x0000BF46
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ILocalDefinition> Constants
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._constants.NullToEmpty<ILocalDefinition>();
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000DD53 File Offset: 0x0000BF53
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ILocalDefinition> Variables
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._locals.NullToEmpty<ILocalDefinition>();
			}
		}

		// Token: 0x04000195 RID: 405
		public readonly int StartOffset;

		// Token: 0x04000196 RID: 406
		public readonly int EndOffset;

		// Token: 0x04000197 RID: 407
		[Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<ILocalDefinition> _constants;

		// Token: 0x04000198 RID: 408
		[Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<ILocalDefinition> _locals;
	}
}
