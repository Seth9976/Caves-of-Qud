using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Threading;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020005A2 RID: 1442
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class TupleElementFieldSymbol : WrappedFieldSymbol
	{
		// Token: 0x06007896 RID: 30870 RVA: 0x001B9588 File Offset: 0x001B7788
		public TupleElementFieldSymbol(NamedTypeSymbol container, FieldSymbol underlyingField, int tupleElementIndex, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<Location> locations, bool isImplicitlyDeclared, [global::System.Runtime.CompilerServices.Nullable(2)] FieldSymbol correspondingDefaultFieldOpt = null)
			: base(underlyingField)
		{
			this._containingTuple = container;
			this._tupleElementIndex = ((correspondingDefaultFieldOpt == null) ? (tupleElementIndex << 1) : ((tupleElementIndex << 1) + 1));
			this._locations = locations;
			this._isImplicitlyDeclared = isImplicitlyDeclared;
			this._correspondingDefaultField = correspondingDefaultFieldOpt ?? this;
		}

		// Token: 0x17002279 RID: 8825
		// (get) Token: 0x06007897 RID: 30871 RVA: 0x001B95D4 File Offset: 0x001B77D4
		public sealed override int TupleElementIndex
		{
			get
			{
				return this._tupleElementIndex >> 1;
			}
		}

		// Token: 0x1700227A RID: 8826
		// (get) Token: 0x06007898 RID: 30872 RVA: 0x001B95DE File Offset: 0x001B77DE
		public sealed override bool IsDefaultTupleElement
		{
			get
			{
				return (this._tupleElementIndex & 1) == 0;
			}
		}

		// Token: 0x1700227B RID: 8827
		// (get) Token: 0x06007899 RID: 30873 RVA: 0x001B95EB File Offset: 0x001B77EB
		public sealed override bool IsExplicitlyNamedTupleElement
		{
			get
			{
				return !this._isImplicitlyDeclared;
			}
		}

		// Token: 0x1700227C RID: 8828
		// (get) Token: 0x0600789A RID: 30874 RVA: 0x001B95F6 File Offset: 0x001B77F6
		public sealed override FieldSymbol TupleUnderlyingField
		{
			get
			{
				return this._underlyingField;
			}
		}

		// Token: 0x1700227D RID: 8829
		// (get) Token: 0x0600789B RID: 30875 RVA: 0x001B95FE File Offset: 0x001B77FE
		[global::System.Runtime.CompilerServices.Nullable(2)]
		public sealed override Symbol AssociatedSymbol
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x1700227E RID: 8830
		// (get) Token: 0x0600789C RID: 30876 RVA: 0x001B9604 File Offset: 0x001B7804
		public override FieldSymbol OriginalDefinition
		{
			get
			{
				NamedTypeSymbol originalDefinition = this.ContainingType.OriginalDefinition;
				if (!originalDefinition.IsTupleType)
				{
					return this;
				}
				return originalDefinition.GetTupleMemberSymbolForUnderlyingMember<FieldSymbol>(this._underlyingField.OriginalDefinition);
			}
		}

		// Token: 0x1700227F RID: 8831
		// (get) Token: 0x0600789D RID: 30877 RVA: 0x001B9638 File Offset: 0x001B7838
		public sealed override Symbol ContainingSymbol
		{
			get
			{
				return this._containingTuple;
			}
		}

		// Token: 0x0600789E RID: 30878 RVA: 0x001B9640 File Offset: 0x001B7840
		internal override TypeWithAnnotations GetFieldType(ConsList<FieldSymbol> fieldsBeingBound)
		{
			return this._underlyingField.GetFieldType(fieldsBeingBound);
		}

		// Token: 0x0600789F RID: 30879 RVA: 0x001B964E File Offset: 0x001B784E
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<CSharpAttributeData> GetAttributes()
		{
			return this._underlyingField.GetAttributes();
		}

		// Token: 0x060078A0 RID: 30880 RVA: 0x001B965B File Offset: 0x001B785B
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal override UseSiteInfo<AssemblySymbol> GetUseSiteInfo()
		{
			return this._underlyingField.GetUseSiteInfo();
		}

		// Token: 0x17002280 RID: 8832
		// (get) Token: 0x060078A1 RID: 30881 RVA: 0x001B9668 File Offset: 0x001B7868
		internal override bool RequiresCompletion
		{
			get
			{
				return this._underlyingField.RequiresCompletion;
			}
		}

		// Token: 0x060078A2 RID: 30882 RVA: 0x001B9675 File Offset: 0x001B7875
		internal override bool HasComplete(CompletionPart part)
		{
			return this._underlyingField.HasComplete(part);
		}

		// Token: 0x060078A3 RID: 30883 RVA: 0x001B9683 File Offset: 0x001B7883
		internal override void ForceComplete(SourceLocation locationOpt, CancellationToken cancellationToken)
		{
			this._underlyingField.ForceComplete(locationOpt, cancellationToken);
		}

		// Token: 0x060078A4 RID: 30884 RVA: 0x001B9694 File Offset: 0x001B7894
		public sealed override int GetHashCode()
		{
			return Hash.Combine(this._containingTuple.GetHashCode(), this._tupleElementIndex.GetHashCode());
		}

		// Token: 0x060078A5 RID: 30885 RVA: 0x001B96C0 File Offset: 0x001B78C0
		public sealed override bool Equals(Symbol obj, TypeCompareKind compareKind)
		{
			TupleElementFieldSymbol tupleElementFieldSymbol = obj as TupleElementFieldSymbol;
			return tupleElementFieldSymbol == this || (tupleElementFieldSymbol != null && this._tupleElementIndex == tupleElementFieldSymbol._tupleElementIndex && TypeSymbol.Equals(this._containingTuple, tupleElementFieldSymbol._containingTuple, compareKind));
		}

		// Token: 0x17002281 RID: 8833
		// (get) Token: 0x060078A6 RID: 30886 RVA: 0x001B96FF File Offset: 0x001B78FF
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public sealed override ImmutableArray<Location> Locations
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._locations;
			}
		}

		// Token: 0x17002282 RID: 8834
		// (get) Token: 0x060078A7 RID: 30887 RVA: 0x001B9707 File Offset: 0x001B7907
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public sealed override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				if (!this._isImplicitlyDeclared)
				{
					return Symbol.GetDeclaringSyntaxReferenceHelper<CSharpSyntaxNode>(this._locations);
				}
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x17002283 RID: 8835
		// (get) Token: 0x060078A8 RID: 30888 RVA: 0x001B9722 File Offset: 0x001B7922
		public sealed override bool IsImplicitlyDeclared
		{
			get
			{
				return this._isImplicitlyDeclared;
			}
		}

		// Token: 0x17002284 RID: 8836
		// (get) Token: 0x060078A9 RID: 30889 RVA: 0x001B972A File Offset: 0x001B792A
		public sealed override FieldSymbol CorrespondingTupleField
		{
			get
			{
				return this._correspondingDefaultField;
			}
		}

		// Token: 0x060078AA RID: 30890 RVA: 0x001B9734 File Offset: 0x001B7934
		internal override FieldSymbol AsMember(NamedTypeSymbol newOwner)
		{
			NamedTypeSymbol newUnderlyingOwner = this.GetNewUnderlyingOwner(newOwner);
			return new TupleElementFieldSymbol(newOwner, this._underlyingField.OriginalDefinition.AsMember(newUnderlyingOwner), this.TupleElementIndex, this.Locations, this.IsImplicitlyDeclared, null);
		}

		// Token: 0x060078AB RID: 30891 RVA: 0x001B9774 File Offset: 0x001B7974
		protected NamedTypeSymbol GetNewUnderlyingOwner(NamedTypeSymbol newOwner)
		{
			int i = this.TupleElementIndex;
			NamedTypeSymbol namedTypeSymbol = newOwner;
			while (i >= 7)
			{
				namedTypeSymbol = (NamedTypeSymbol)namedTypeSymbol.TypeArgumentsWithAnnotationsNoUseSiteDiagnostics[7].Type;
				i -= 7;
			}
			return namedTypeSymbol;
		}

		// Token: 0x04001D06 RID: 7430
		private readonly int _tupleElementIndex;

		// Token: 0x04001D07 RID: 7431
		protected readonly NamedTypeSymbol _containingTuple;

		// Token: 0x04001D08 RID: 7432
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<Location> _locations;

		// Token: 0x04001D09 RID: 7433
		protected readonly FieldSymbol _correspondingDefaultField;

		// Token: 0x04001D0A RID: 7434
		private readonly bool _isImplicitlyDeclared;
	}
}
