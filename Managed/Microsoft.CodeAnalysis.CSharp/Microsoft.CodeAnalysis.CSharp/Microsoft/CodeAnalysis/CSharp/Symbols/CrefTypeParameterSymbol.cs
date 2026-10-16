using System;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000519 RID: 1305
	internal sealed class CrefTypeParameterSymbol : TypeParameterSymbol
	{
		// Token: 0x06006D96 RID: 28054 RVA: 0x00193520 File Offset: 0x00191720
		public CrefTypeParameterSymbol(string name, int ordinal, IdentifierNameSyntax declaringSyntax)
		{
			this._name = name;
			this._ordinal = ordinal;
			this._declaringSyntax = declaringSyntax.GetReference();
		}

		// Token: 0x17001CBB RID: 7355
		// (get) Token: 0x06006D97 RID: 28055 RVA: 0x00193542 File Offset: 0x00191742
		public override TypeParameterKind TypeParameterKind
		{
			get
			{
				return TypeParameterKind.Cref;
			}
		}

		// Token: 0x17001CBC RID: 7356
		// (get) Token: 0x06006D98 RID: 28056 RVA: 0x00193545 File Offset: 0x00191745
		public override string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17001CBD RID: 7357
		// (get) Token: 0x06006D99 RID: 28057 RVA: 0x0019354D File Offset: 0x0019174D
		public override int Ordinal
		{
			get
			{
				return this._ordinal;
			}
		}

		// Token: 0x06006D9A RID: 28058 RVA: 0x00193558 File Offset: 0x00191758
		internal override bool Equals(TypeSymbol t2, TypeCompareKind comparison)
		{
			if (this == t2)
			{
				return true;
			}
			if (t2 == null)
			{
				return false;
			}
			CrefTypeParameterSymbol crefTypeParameterSymbol = t2 as CrefTypeParameterSymbol;
			return crefTypeParameterSymbol != null && crefTypeParameterSymbol._name == this._name && crefTypeParameterSymbol._ordinal == this._ordinal && crefTypeParameterSymbol._declaringSyntax.GetSyntax(default(CancellationToken)) == this._declaringSyntax.GetSyntax(default(CancellationToken));
		}

		// Token: 0x06006D9B RID: 28059 RVA: 0x001935C7 File Offset: 0x001917C7
		public override int GetHashCode()
		{
			return Hash.Combine<string>(this._name, this._ordinal);
		}

		// Token: 0x17001CBE RID: 7358
		// (get) Token: 0x06006D9C RID: 28060 RVA: 0x001935DA File Offset: 0x001917DA
		public override VarianceKind Variance
		{
			get
			{
				return VarianceKind.None;
			}
		}

		// Token: 0x17001CBF RID: 7359
		// (get) Token: 0x06006D9D RID: 28061 RVA: 0x001935DD File Offset: 0x001917DD
		public override bool HasValueTypeConstraint
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001CC0 RID: 7360
		// (get) Token: 0x06006D9E RID: 28062 RVA: 0x001935E0 File Offset: 0x001917E0
		public override bool IsValueTypeFromConstraintTypes
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001CC1 RID: 7361
		// (get) Token: 0x06006D9F RID: 28063 RVA: 0x001935E3 File Offset: 0x001917E3
		public override bool HasReferenceTypeConstraint
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001CC2 RID: 7362
		// (get) Token: 0x06006DA0 RID: 28064 RVA: 0x001935E6 File Offset: 0x001917E6
		public override bool IsReferenceTypeFromConstraintTypes
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001CC3 RID: 7363
		// (get) Token: 0x06006DA1 RID: 28065 RVA: 0x001935E9 File Offset: 0x001917E9
		internal override bool? ReferenceTypeConstraintIsNullable
		{
			get
			{
				return new bool?(false);
			}
		}

		// Token: 0x17001CC4 RID: 7364
		// (get) Token: 0x06006DA2 RID: 28066 RVA: 0x001935F1 File Offset: 0x001917F1
		public override bool HasNotNullConstraint
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001CC5 RID: 7365
		// (get) Token: 0x06006DA3 RID: 28067 RVA: 0x001935F4 File Offset: 0x001917F4
		internal override bool? IsNotNullable
		{
			get
			{
				return default(bool?);
			}
		}

		// Token: 0x17001CC6 RID: 7366
		// (get) Token: 0x06006DA4 RID: 28068 RVA: 0x0019360A File Offset: 0x0019180A
		public override bool HasUnmanagedTypeConstraint
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001CC7 RID: 7367
		// (get) Token: 0x06006DA5 RID: 28069 RVA: 0x0019360D File Offset: 0x0019180D
		public override bool HasConstructorConstraint
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001CC8 RID: 7368
		// (get) Token: 0x06006DA6 RID: 28070 RVA: 0x00193610 File Offset: 0x00191810
		public override Symbol ContainingSymbol
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001CC9 RID: 7369
		// (get) Token: 0x06006DA7 RID: 28071 RVA: 0x00193613 File Offset: 0x00191813
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return ImmutableArray.Create<Location>(this._declaringSyntax.GetLocation());
			}
		}

		// Token: 0x17001CCA RID: 7370
		// (get) Token: 0x06006DA8 RID: 28072 RVA: 0x00193625 File Offset: 0x00191825
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray.Create<SyntaxReference>(this._declaringSyntax);
			}
		}

		// Token: 0x06006DA9 RID: 28073 RVA: 0x00193632 File Offset: 0x00191832
		internal override void EnsureAllConstraintsAreResolved()
		{
		}

		// Token: 0x06006DAA RID: 28074 RVA: 0x00193634 File Offset: 0x00191834
		internal override ImmutableArray<TypeWithAnnotations> GetConstraintTypes(ConsList<TypeParameterSymbol> inProgress)
		{
			return ImmutableArray<TypeWithAnnotations>.Empty;
		}

		// Token: 0x06006DAB RID: 28075 RVA: 0x0019363B File Offset: 0x0019183B
		internal override ImmutableArray<NamedTypeSymbol> GetInterfaces(ConsList<TypeParameterSymbol> inProgress)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x06006DAC RID: 28076 RVA: 0x00193642 File Offset: 0x00191842
		internal override NamedTypeSymbol GetEffectiveBaseClass(ConsList<TypeParameterSymbol> inProgress)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x06006DAD RID: 28077 RVA: 0x00193649 File Offset: 0x00191849
		internal override TypeSymbol GetDeducedBaseType(ConsList<TypeParameterSymbol> inProgress)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17001CCB RID: 7371
		// (get) Token: 0x06006DAE RID: 28078 RVA: 0x00193650 File Offset: 0x00191850
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001B18 RID: 6936
		private readonly string _name;

		// Token: 0x04001B19 RID: 6937
		private readonly int _ordinal;

		// Token: 0x04001B1A RID: 6938
		private readonly SyntaxReference _declaringSyntax;
	}
}
