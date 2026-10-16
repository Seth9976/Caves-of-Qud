using System;
using System.Collections.Immutable;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Emit
{
	// Token: 0x02000497 RID: 1175
	internal sealed class SpecializedGenericNestedTypeInstanceReference : SpecializedNestedTypeReference, IGenericTypeInstanceReference, ITypeReference, IReference
	{
		// Token: 0x060061A0 RID: 24992 RVA: 0x00178113 File Offset: 0x00176313
		public SpecializedGenericNestedTypeInstanceReference(NamedTypeSymbol underlyingNamedType)
			: base(underlyingNamedType)
		{
		}

		// Token: 0x060061A1 RID: 24993 RVA: 0x0017811C File Offset: 0x0017631C
		public sealed override void Dispatch(MetadataVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060061A2 RID: 24994 RVA: 0x00178128 File Offset: 0x00176328
		ImmutableArray<ITypeReference> IGenericTypeInstanceReference.GetGenericArguments(EmitContext context)
		{
			PEModuleBuilder pemoduleBuilder = (PEModuleBuilder)context.Module;
			ArrayBuilder<ITypeReference> instance = ArrayBuilder<ITypeReference>.GetInstance();
			foreach (TypeWithAnnotations typeWithAnnotations in this.UnderlyingNamedType.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics)
			{
				instance.Add(pemoduleBuilder.Translate(typeWithAnnotations.Type, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x060061A3 RID: 24995 RVA: 0x00178199 File Offset: 0x00176399
		INamedTypeReference IGenericTypeInstanceReference.GetGenericType(EmitContext context)
		{
			return ((PEModuleBuilder)context.Module).Translate(this.UnderlyingNamedType.OriginalDefinition, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics, false, true);
		}

		// Token: 0x170016C0 RID: 5824
		// (get) Token: 0x060061A4 RID: 24996 RVA: 0x001781C9 File Offset: 0x001763C9
		public override IGenericTypeInstanceReference AsGenericTypeInstanceReference
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170016C1 RID: 5825
		// (get) Token: 0x060061A5 RID: 24997 RVA: 0x001781CC File Offset: 0x001763CC
		public override INamespaceTypeReference AsNamespaceTypeReference
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170016C2 RID: 5826
		// (get) Token: 0x060061A6 RID: 24998 RVA: 0x001781CF File Offset: 0x001763CF
		public override INestedTypeReference AsNestedTypeReference
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170016C3 RID: 5827
		// (get) Token: 0x060061A7 RID: 24999 RVA: 0x001781D2 File Offset: 0x001763D2
		public override ISpecializedNestedTypeReference AsSpecializedNestedTypeReference
		{
			get
			{
				return null;
			}
		}
	}
}
