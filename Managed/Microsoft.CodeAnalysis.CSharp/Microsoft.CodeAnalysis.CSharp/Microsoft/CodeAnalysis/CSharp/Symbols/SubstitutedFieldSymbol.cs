using System;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Emit;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000562 RID: 1378
	internal sealed class SubstitutedFieldSymbol : WrappedFieldSymbol
	{
		// Token: 0x06007452 RID: 29778 RVA: 0x001B2839 File Offset: 0x001B0A39
		internal SubstitutedFieldSymbol(SubstitutedNamedTypeSymbol containingType, FieldSymbol substitutedFrom)
			: base(substitutedFrom.OriginalDefinition)
		{
			this._containingType = containingType;
		}

		// Token: 0x06007453 RID: 29779 RVA: 0x001B2850 File Offset: 0x001B0A50
		internal override TypeWithAnnotations GetFieldType(ConsList<FieldSymbol> fieldsBeingBound)
		{
			if (this._lazyType == null)
			{
				TypeWithAnnotations typeWithAnnotations = this._containingType.TypeSubstitution.SubstituteType(this.OriginalDefinition.GetFieldType(fieldsBeingBound));
				Interlocked.CompareExchange<TypeWithAnnotations.Boxed>(ref this._lazyType, new TypeWithAnnotations.Boxed(typeWithAnnotations), null);
			}
			return this._lazyType.Value;
		}

		// Token: 0x17001FF7 RID: 8183
		// (get) Token: 0x06007454 RID: 29780 RVA: 0x001B28A0 File Offset: 0x001B0AA0
		public override Symbol ContainingSymbol
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x17001FF8 RID: 8184
		// (get) Token: 0x06007455 RID: 29781 RVA: 0x001B28A8 File Offset: 0x001B0AA8
		public override NamedTypeSymbol ContainingType
		{
			get
			{
				return this._containingType;
			}
		}

		// Token: 0x17001FF9 RID: 8185
		// (get) Token: 0x06007456 RID: 29782 RVA: 0x001B28B0 File Offset: 0x001B0AB0
		public override FieldSymbol OriginalDefinition
		{
			get
			{
				return this._underlyingField;
			}
		}

		// Token: 0x17001FFA RID: 8186
		// (get) Token: 0x06007457 RID: 29783 RVA: 0x001B28B8 File Offset: 0x001B0AB8
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return (this.ContainingType.IsTupleType && this.IsDefaultTupleElement) || base.IsImplicitlyDeclared;
			}
		}

		// Token: 0x06007458 RID: 29784 RVA: 0x001B28D7 File Offset: 0x001B0AD7
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			return this.OriginalDefinition.GetAttributes();
		}

		// Token: 0x17001FFB RID: 8187
		// (get) Token: 0x06007459 RID: 29785 RVA: 0x001B28E4 File Offset: 0x001B0AE4
		public override Symbol AssociatedSymbol
		{
			get
			{
				Symbol associatedSymbol = this.OriginalDefinition.AssociatedSymbol;
				if (associatedSymbol == null)
				{
					return null;
				}
				return associatedSymbol.SymbolAsMember(this.ContainingType);
			}
		}

		// Token: 0x0600745A RID: 29786 RVA: 0x001B2910 File Offset: 0x001B0B10
		internal override NamedTypeSymbol FixedImplementationType(PEModuleBuilder emitModule)
		{
			return (NamedTypeSymbol)this._containingType.TypeSubstitution.SubstituteType(this.OriginalDefinition.FixedImplementationType(emitModule)).Type;
		}

		// Token: 0x0600745B RID: 29787 RVA: 0x001B2948 File Offset: 0x001B0B48
		public override bool Equals(Symbol obj, TypeCompareKind compareKind)
		{
			if (this == obj)
			{
				return true;
			}
			FieldSymbol fieldSymbol = obj as FieldSymbol;
			return fieldSymbol != null && TypeSymbol.Equals(this._containingType, fieldSymbol.ContainingType, compareKind) && this.OriginalDefinition == fieldSymbol.OriginalDefinition;
		}

		// Token: 0x0600745C RID: 29788 RVA: 0x001B298C File Offset: 0x001B0B8C
		public override int GetHashCode()
		{
			int num = this.OriginalDefinition.GetHashCode();
			int hashCode = this._containingType.GetHashCode();
			if (hashCode != this.OriginalDefinition.ContainingType.GetHashCode())
			{
				num = Hash.Combine(hashCode, num);
			}
			return num;
		}

		// Token: 0x04001C43 RID: 7235
		private readonly SubstitutedNamedTypeSymbol _containingType;

		// Token: 0x04001C44 RID: 7236
		private TypeWithAnnotations.Boxed _lazyType;
	}
}
