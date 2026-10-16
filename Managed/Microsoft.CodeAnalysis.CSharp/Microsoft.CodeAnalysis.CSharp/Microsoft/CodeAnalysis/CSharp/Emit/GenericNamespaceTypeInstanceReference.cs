using System;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols;

namespace Microsoft.CodeAnalysis.CSharp.Emit
{
	// Token: 0x02000489 RID: 1161
	internal sealed class GenericNamespaceTypeInstanceReference : GenericTypeInstanceReference
	{
		// Token: 0x060060B8 RID: 24760 RVA: 0x001757EE File Offset: 0x001739EE
		public GenericNamespaceTypeInstanceReference(NamedTypeSymbol underlyingNamedType)
			: base(underlyingNamedType)
		{
		}

		// Token: 0x1700167F RID: 5759
		// (get) Token: 0x060060B9 RID: 24761 RVA: 0x001757F7 File Offset: 0x001739F7
		public override IGenericTypeInstanceReference AsGenericTypeInstanceReference
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001680 RID: 5760
		// (get) Token: 0x060060BA RID: 24762 RVA: 0x001757FA File Offset: 0x001739FA
		public override INamespaceTypeReference AsNamespaceTypeReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001681 RID: 5761
		// (get) Token: 0x060060BB RID: 24763 RVA: 0x001757FD File Offset: 0x001739FD
		public override INestedTypeReference AsNestedTypeReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001682 RID: 5762
		// (get) Token: 0x060060BC RID: 24764 RVA: 0x00175800 File Offset: 0x00173A00
		public override ISpecializedNestedTypeReference AsSpecializedNestedTypeReference
		{
			get
			{
				return null;
			}
		}
	}
}
