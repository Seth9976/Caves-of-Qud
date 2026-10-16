using System;
using System.Collections.Immutable;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.Emit;

namespace Microsoft.CodeAnalysis.CSharp.Emit
{
	// Token: 0x02000495 RID: 1173
	internal sealed class SpecializedFieldReference : TypeMemberReference, ISpecializedFieldReference, IFieldReference, ITypeMemberReference, IReference, INamedEntity
	{
		// Token: 0x06006193 RID: 24979 RVA: 0x00178036 File Offset: 0x00176236
		public SpecializedFieldReference(FieldSymbol underlyingField)
		{
			this._underlyingField = underlyingField;
		}

		// Token: 0x170016BB RID: 5819
		// (get) Token: 0x06006194 RID: 24980 RVA: 0x00178045 File Offset: 0x00176245
		protected override Symbol UnderlyingSymbol
		{
			get
			{
				return this._underlyingField;
			}
		}

		// Token: 0x06006195 RID: 24981 RVA: 0x0017804D File Offset: 0x0017624D
		public override void Dispatch(MetadataVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x170016BC RID: 5820
		// (get) Token: 0x06006196 RID: 24982 RVA: 0x00178056 File Offset: 0x00176256
		IFieldReference ISpecializedFieldReference.UnspecializedVersion
		{
			get
			{
				return this._underlyingField.OriginalDefinition.GetCciAdapter();
			}
		}

		// Token: 0x170016BD RID: 5821
		// (get) Token: 0x06006197 RID: 24983 RVA: 0x00178068 File Offset: 0x00176268
		ISpecializedFieldReference IFieldReference.AsSpecializedFieldReference
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06006198 RID: 24984 RVA: 0x0017806C File Offset: 0x0017626C
		ITypeReference IFieldReference.GetType(EmitContext context)
		{
			TypeWithAnnotations typeWithAnnotations = this._underlyingField.TypeWithAnnotations;
			ImmutableArray<CustomModifier> customModifiers = typeWithAnnotations.CustomModifiers;
			ITypeReference typeReference = ((PEModuleBuilder)context.Module).Translate(typeWithAnnotations.Type, (CSharpSyntaxNode)context.SyntaxNode, context.Diagnostics);
			if (customModifiers.Length == 0)
			{
				return typeReference;
			}
			return new ModifiedTypeReference(typeReference, ImmutableArray<ICustomModifier>.CastUp<CustomModifier>(customModifiers));
		}

		// Token: 0x06006199 RID: 24985 RVA: 0x001780CD File Offset: 0x001762CD
		IFieldDefinition IFieldReference.GetResolvedField(EmitContext context)
		{
			return null;
		}

		// Token: 0x170016BE RID: 5822
		// (get) Token: 0x0600619A RID: 24986 RVA: 0x001780D0 File Offset: 0x001762D0
		bool IFieldReference.IsContextualNamedEntity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001975 RID: 6517
		private readonly FieldSymbol _underlyingField;
	}
}
