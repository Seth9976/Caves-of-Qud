using System;
using System.Collections.Immutable;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Emit;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Emit
{
	// Token: 0x02000490 RID: 1168
	internal sealed class ArgListParameterTypeInformation : IParameterTypeInformation, IParameterListEntry
	{
		// Token: 0x06006104 RID: 24836 RVA: 0x00175C1F File Offset: 0x00173E1F
		public ArgListParameterTypeInformation(int ordinal, bool isByRef, ITypeReference type)
		{
			this._ordinal = (ushort)ordinal;
			this._isByRef = isByRef;
			this._type = type;
		}

		// Token: 0x170016A7 RID: 5799
		// (get) Token: 0x06006105 RID: 24837 RVA: 0x00175C3D File Offset: 0x00173E3D
		ImmutableArray<ICustomModifier> IParameterTypeInformation.CustomModifiers
		{
			get
			{
				return ImmutableArray<ICustomModifier>.Empty;
			}
		}

		// Token: 0x170016A8 RID: 5800
		// (get) Token: 0x06006106 RID: 24838 RVA: 0x00175C44 File Offset: 0x00173E44
		bool IParameterTypeInformation.IsByReference
		{
			get
			{
				return this._isByRef;
			}
		}

		// Token: 0x170016A9 RID: 5801
		// (get) Token: 0x06006107 RID: 24839 RVA: 0x00175C4C File Offset: 0x00173E4C
		ImmutableArray<ICustomModifier> IParameterTypeInformation.RefCustomModifiers
		{
			get
			{
				return ImmutableArray<ICustomModifier>.Empty;
			}
		}

		// Token: 0x06006108 RID: 24840 RVA: 0x00175C53 File Offset: 0x00173E53
		ITypeReference IParameterTypeInformation.GetType(EmitContext context)
		{
			return this._type;
		}

		// Token: 0x170016AA RID: 5802
		// (get) Token: 0x06006109 RID: 24841 RVA: 0x00175C5B File Offset: 0x00173E5B
		ushort IParameterListEntry.Index
		{
			get
			{
				return this._ordinal;
			}
		}

		// Token: 0x0600610A RID: 24842 RVA: 0x00175C63 File Offset: 0x00173E63
		public sealed override bool Equals(object obj)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0600610B RID: 24843 RVA: 0x00175C6A File Offset: 0x00173E6A
		public sealed override int GetHashCode()
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x0400195C RID: 6492
		private readonly ushort _ordinal;

		// Token: 0x0400195D RID: 6493
		private readonly bool _isByRef;

		// Token: 0x0400195E RID: 6494
		private readonly ITypeReference _type;
	}
}
