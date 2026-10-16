using System;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.CodeAnalysis.CSharp.Emit
{
	// Token: 0x02000499 RID: 1177
	internal class SpecializedNestedTypeReference : NamedTypeReference, ISpecializedNestedTypeReference, INestedTypeReference, INamedTypeReference, ITypeReference, IReference, INamedEntity, ITypeMemberReference
	{
		// Token: 0x060061AC RID: 25004 RVA: 0x001781FC File Offset: 0x001763FC
		public SpecializedNestedTypeReference(NamedTypeSymbol underlyingNamedType)
			: base(underlyingNamedType)
		{
		}

		// Token: 0x060061AD RID: 25005 RVA: 0x00178205 File Offset: 0x00176405
		INestedTypeReference ISpecializedNestedTypeReference.GetUnspecializedVersion(EmitContext context)
		{
			return ((PEModuleBuilder)context.Module).Translate(this.UnderlyingNamedType.OriginalDefinition, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics, false, true).AsNestedTypeReference;
		}

		// Token: 0x060061AE RID: 25006 RVA: 0x0017823A File Offset: 0x0017643A
		public override void Dispatch(MetadataVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060061AF RID: 25007 RVA: 0x00178243 File Offset: 0x00176443
		ITypeReference ITypeMemberReference.GetContainingType(EmitContext context)
		{
			return ((PEModuleBuilder)context.Module).Translate(this.UnderlyingNamedType.ContainingType, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics, false, false);
		}

		// Token: 0x170016C6 RID: 5830
		// (get) Token: 0x060061B0 RID: 25008 RVA: 0x00178273 File Offset: 0x00176473
		public override IGenericTypeInstanceReference AsGenericTypeInstanceReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170016C7 RID: 5831
		// (get) Token: 0x060061B1 RID: 25009 RVA: 0x00178276 File Offset: 0x00176476
		public override INamespaceTypeReference AsNamespaceTypeReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170016C8 RID: 5832
		// (get) Token: 0x060061B2 RID: 25010 RVA: 0x00178279 File Offset: 0x00176479
		public override INestedTypeReference AsNestedTypeReference
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170016C9 RID: 5833
		// (get) Token: 0x060061B3 RID: 25011 RVA: 0x0017827C File Offset: 0x0017647C
		public override ISpecializedNestedTypeReference AsSpecializedNestedTypeReference
		{
			get
			{
				return this;
			}
		}
	}
}
