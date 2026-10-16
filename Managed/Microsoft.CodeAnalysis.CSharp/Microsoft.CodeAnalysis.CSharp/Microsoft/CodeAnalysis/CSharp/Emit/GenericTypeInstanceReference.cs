using System;
using System.Collections.Immutable;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Emit
{
	// Token: 0x0200048B RID: 1163
	internal abstract class GenericTypeInstanceReference : NamedTypeReference, IGenericTypeInstanceReference, ITypeReference, IReference
	{
		// Token: 0x060060C3 RID: 24771 RVA: 0x00175848 File Offset: 0x00173A48
		public GenericTypeInstanceReference(NamedTypeSymbol underlyingNamedType)
			: base(underlyingNamedType)
		{
		}

		// Token: 0x060060C4 RID: 24772 RVA: 0x00175851 File Offset: 0x00173A51
		public sealed override void Dispatch(MetadataVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060060C5 RID: 24773 RVA: 0x0017585C File Offset: 0x00173A5C
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

		// Token: 0x060060C6 RID: 24774 RVA: 0x001758CD File Offset: 0x00173ACD
		INamedTypeReference IGenericTypeInstanceReference.GetGenericType(EmitContext context)
		{
			return ((PEModuleBuilder)context.Module).Translate(this.UnderlyingNamedType.OriginalDefinition, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics, false, true);
		}
	}
}
