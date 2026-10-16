using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.CodeAnalysis.CSharp.Emit
{
	// Token: 0x02000488 RID: 1160
	internal sealed class GenericMethodInstanceReference : MethodReference, IGenericMethodInstanceReference, IMethodReference, ISignature, ITypeMemberReference, IReference, INamedEntity
	{
		// Token: 0x060060B3 RID: 24755 RVA: 0x00175792 File Offset: 0x00173992
		public GenericMethodInstanceReference(MethodSymbol underlyingMethod)
			: base(underlyingMethod)
		{
		}

		// Token: 0x060060B4 RID: 24756 RVA: 0x0017579B File Offset: 0x0017399B
		public override void Dispatch(MetadataVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060060B5 RID: 24757 RVA: 0x001757A4 File Offset: 0x001739A4
		IEnumerable<ITypeReference> IGenericMethodInstanceReference.GetGenericArguments(EmitContext context)
		{
			PEModuleBuilder moduleBeingBuilt = (PEModuleBuilder)context.Module;
			foreach (TypeWithAnnotations typeWithAnnotations in this.UnderlyingMethod.TypeArgumentsWithAnnotations)
			{
				yield return moduleBeingBuilt.Translate(typeWithAnnotations.Type, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics);
			}
			ImmutableArray<TypeWithAnnotations>.Enumerator enumerator = default(ImmutableArray<TypeWithAnnotations>.Enumerator);
			yield break;
		}

		// Token: 0x060060B6 RID: 24758 RVA: 0x001757BB File Offset: 0x001739BB
		IMethodReference IGenericMethodInstanceReference.GetGenericMethod(EmitContext context)
		{
			return ((PEModuleBuilder)context.Module).Translate(this.UnderlyingMethod.OriginalDefinition, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics, null, true);
		}

		// Token: 0x1700167E RID: 5758
		// (get) Token: 0x060060B7 RID: 24759 RVA: 0x001757EB File Offset: 0x001739EB
		public override IGenericMethodInstanceReference AsGenericMethodInstanceReference
		{
			get
			{
				return this;
			}
		}
	}
}
