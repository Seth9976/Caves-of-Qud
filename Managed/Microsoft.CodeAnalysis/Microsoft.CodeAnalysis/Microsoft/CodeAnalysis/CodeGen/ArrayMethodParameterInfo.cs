using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.CodeAnalysis.CodeGen
{
	// Token: 0x020004BC RID: 1212
	[NullableContext(1)]
	[Nullable(0)]
	internal class ArrayMethodParameterInfo : IParameterTypeInformation, IParameterListEntry
	{
		// Token: 0x0600308C RID: 12428 RVA: 0x0008A663 File Offset: 0x00088863
		protected ArrayMethodParameterInfo(ushort index)
		{
			this._index = index;
		}

		// Token: 0x0600308D RID: 12429 RVA: 0x0008A672 File Offset: 0x00088872
		public static ArrayMethodParameterInfo GetIndexParameter(ushort index)
		{
			switch (index)
			{
			case 0:
				return ArrayMethodParameterInfo.s_index0;
			case 1:
				return ArrayMethodParameterInfo.s_index1;
			case 2:
				return ArrayMethodParameterInfo.s_index2;
			case 3:
				return ArrayMethodParameterInfo.s_index3;
			default:
				return new ArrayMethodParameterInfo(index);
			}
		}

		// Token: 0x17000DE9 RID: 3561
		// (get) Token: 0x0600308E RID: 12430 RVA: 0x0008A6AA File Offset: 0x000888AA
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ICustomModifier> RefCustomModifiers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<ICustomModifier>.Empty;
			}
		}

		// Token: 0x17000DEA RID: 3562
		// (get) Token: 0x0600308F RID: 12431 RVA: 0x0008A6B1 File Offset: 0x000888B1
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<ICustomModifier> CustomModifiers
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<ICustomModifier>.Empty;
			}
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06003090 RID: 12432 RVA: 0x0008A6B8 File Offset: 0x000888B8
		public bool IsByReference
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x0008A6BB File Offset: 0x000888BB
		public virtual ITypeReference GetType(EmitContext context)
		{
			return context.Module.GetPlatformType(PlatformType.SystemInt32, context);
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06003092 RID: 12434 RVA: 0x0008A6CB File Offset: 0x000888CB
		public ushort Index
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x040012D1 RID: 4817
		private readonly ushort _index;

		// Token: 0x040012D2 RID: 4818
		private static readonly ArrayMethodParameterInfo s_index0 = new ArrayMethodParameterInfo(0);

		// Token: 0x040012D3 RID: 4819
		private static readonly ArrayMethodParameterInfo s_index1 = new ArrayMethodParameterInfo(1);

		// Token: 0x040012D4 RID: 4820
		private static readonly ArrayMethodParameterInfo s_index2 = new ArrayMethodParameterInfo(2);

		// Token: 0x040012D5 RID: 4821
		private static readonly ArrayMethodParameterInfo s_index3 = new ArrayMethodParameterInfo(3);
	}
}
