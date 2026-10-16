using System;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.CodeAnalysis.CSharp.Emit
{
	// Token: 0x0200048A RID: 1162
	internal sealed class GenericNestedTypeInstanceReference : GenericTypeInstanceReference, INestedTypeReference, INamedTypeReference, ITypeReference, IReference, INamedEntity, ITypeMemberReference
	{
		// Token: 0x060060BD RID: 24765 RVA: 0x00175803 File Offset: 0x00173A03
		public GenericNestedTypeInstanceReference(NamedTypeSymbol underlyingNamedType)
			: base(underlyingNamedType)
		{
		}

		// Token: 0x060060BE RID: 24766 RVA: 0x0017580C File Offset: 0x00173A0C
		ITypeReference ITypeMemberReference.GetContainingType(EmitContext context)
		{
			return ((PEModuleBuilder)context.Module).Translate(this.UnderlyingNamedType.ContainingType, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics, false, false);
		}

		// Token: 0x17001683 RID: 5763
		// (get) Token: 0x060060BF RID: 24767 RVA: 0x0017583C File Offset: 0x00173A3C
		public override IGenericTypeInstanceReference AsGenericTypeInstanceReference
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001684 RID: 5764
		// (get) Token: 0x060060C0 RID: 24768 RVA: 0x0017583F File Offset: 0x00173A3F
		public override INamespaceTypeReference AsNamespaceTypeReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001685 RID: 5765
		// (get) Token: 0x060060C1 RID: 24769 RVA: 0x00175842 File Offset: 0x00173A42
		public override INestedTypeReference AsNestedTypeReference
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001686 RID: 5766
		// (get) Token: 0x060060C2 RID: 24770 RVA: 0x00175845 File Offset: 0x00173A45
		public override ISpecializedNestedTypeReference AsSpecializedNestedTypeReference
		{
			get
			{
				return null;
			}
		}
	}
}
