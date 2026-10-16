using System;
using System.Collections.Immutable;
using System.Runtime.InteropServices;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001A1 RID: 417
	[StructLayout(3)]
	internal struct LocalInfo<TypeSymbol> where TypeSymbol : class
	{
		// Token: 0x06001085 RID: 4229 RVA: 0x0003029E File Offset: 0x0002E49E
		internal LocalInfo(TypeSymbol type, ImmutableArray<ModifierInfo<TypeSymbol>> customModifiers, LocalSlotConstraints constraints, byte[] signatureOpt)
		{
			this.Type = type;
			this.CustomModifiers = customModifiers;
			this.Constraints = constraints;
			this.SignatureOpt = signatureOpt;
		}

		// Token: 0x06001086 RID: 4230 RVA: 0x000302BD File Offset: 0x0002E4BD
		internal LocalInfo<TypeSymbol> WithSignature(byte[] signature)
		{
			return new LocalInfo<TypeSymbol>(this.Type, this.CustomModifiers, this.Constraints, signature);
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x000302D7 File Offset: 0x0002E4D7
		public bool IsByRef
		{
			get
			{
				return (this.Constraints & LocalSlotConstraints.ByRef) > LocalSlotConstraints.None;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x000302E4 File Offset: 0x0002E4E4
		public bool IsPinned
		{
			get
			{
				return (this.Constraints & LocalSlotConstraints.Pinned) > LocalSlotConstraints.None;
			}
		}

		// Token: 0x04000524 RID: 1316
		internal readonly byte[] SignatureOpt;

		// Token: 0x04000525 RID: 1317
		internal readonly TypeSymbol Type;

		// Token: 0x04000526 RID: 1318
		internal readonly ImmutableArray<ModifierInfo<TypeSymbol>> CustomModifiers;

		// Token: 0x04000527 RID: 1319
		internal readonly LocalSlotConstraints Constraints;
	}
}
