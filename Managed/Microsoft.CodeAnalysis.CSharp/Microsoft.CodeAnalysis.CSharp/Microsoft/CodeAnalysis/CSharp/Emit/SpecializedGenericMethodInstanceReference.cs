using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.CodeAnalysis.CSharp.Emit
{
	// Token: 0x02000496 RID: 1174
	internal sealed class SpecializedGenericMethodInstanceReference : SpecializedMethodReference, IGenericMethodInstanceReference, IMethodReference, ISignature, ITypeMemberReference, IReference, INamedEntity
	{
		// Token: 0x0600619B RID: 24987 RVA: 0x001780D3 File Offset: 0x001762D3
		public SpecializedGenericMethodInstanceReference(MethodSymbol underlyingMethod)
			: base(underlyingMethod)
		{
			this._genericMethod = new SpecializedMethodReference(underlyingMethod);
		}

		// Token: 0x0600619C RID: 24988 RVA: 0x001780E8 File Offset: 0x001762E8
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

		// Token: 0x0600619D RID: 24989 RVA: 0x001780FF File Offset: 0x001762FF
		IMethodReference IGenericMethodInstanceReference.GetGenericMethod(EmitContext context)
		{
			return this._genericMethod;
		}

		// Token: 0x170016BF RID: 5823
		// (get) Token: 0x0600619E RID: 24990 RVA: 0x00178107 File Offset: 0x00176307
		public override IGenericMethodInstanceReference AsGenericMethodInstanceReference
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600619F RID: 24991 RVA: 0x0017810A File Offset: 0x0017630A
		public override void Dispatch(MetadataVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x04001976 RID: 6518
		private readonly SpecializedMethodReference _genericMethod;
	}
}
