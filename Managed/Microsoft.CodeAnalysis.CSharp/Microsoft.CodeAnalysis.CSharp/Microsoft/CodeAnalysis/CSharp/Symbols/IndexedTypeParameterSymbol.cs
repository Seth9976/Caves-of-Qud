using System;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000520 RID: 1312
	internal sealed class IndexedTypeParameterSymbol : TypeParameterSymbol
	{
		// Token: 0x06006E0C RID: 28172 RVA: 0x00194B06 File Offset: 0x00192D06
		private IndexedTypeParameterSymbol(int index)
		{
			this._index = index;
		}

		// Token: 0x17001CF7 RID: 7415
		// (get) Token: 0x06006E0D RID: 28173 RVA: 0x00194B15 File Offset: 0x00192D15
		public override TypeParameterKind TypeParameterKind
		{
			get
			{
				return TypeParameterKind.Method;
			}
		}

		// Token: 0x06006E0E RID: 28174 RVA: 0x00194B18 File Offset: 0x00192D18
		internal static TypeParameterSymbol GetTypeParameter(int index)
		{
			if (index >= IndexedTypeParameterSymbol.s_parameterPool.Length)
			{
				IndexedTypeParameterSymbol.GrowPool(index + 1);
			}
			return IndexedTypeParameterSymbol.s_parameterPool[index];
		}

		// Token: 0x06006E0F RID: 28175 RVA: 0x00194B34 File Offset: 0x00192D34
		private static void GrowPool(int count)
		{
			TypeParameterSymbol[] array = IndexedTypeParameterSymbol.s_parameterPool;
			while (count > array.Length)
			{
				TypeParameterSymbol[] array2 = new TypeParameterSymbol[(count + 15) & -16];
				Array.Copy(array, array2, array.Length);
				for (int i = array.Length; i < array2.Length; i++)
				{
					array2[i] = new IndexedTypeParameterSymbol(i);
				}
				Interlocked.CompareExchange<TypeParameterSymbol[]>(ref IndexedTypeParameterSymbol.s_parameterPool, array2, array);
				array = IndexedTypeParameterSymbol.s_parameterPool;
			}
		}

		// Token: 0x06006E10 RID: 28176 RVA: 0x00194B94 File Offset: 0x00192D94
		internal static ImmutableArray<TypeParameterSymbol> TakeSymbols(int count)
		{
			if (count > IndexedTypeParameterSymbol.s_parameterPool.Length)
			{
				IndexedTypeParameterSymbol.GrowPool(count);
			}
			ArrayBuilder<TypeParameterSymbol> instance = ArrayBuilder<TypeParameterSymbol>.GetInstance();
			for (int i = 0; i < count; i++)
			{
				instance.Add(IndexedTypeParameterSymbol.GetTypeParameter(i));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06006E11 RID: 28177 RVA: 0x00194BD8 File Offset: 0x00192DD8
		internal static ImmutableArray<TypeWithAnnotations> Take(int count)
		{
			if (count > IndexedTypeParameterSymbol.s_parameterPool.Length)
			{
				IndexedTypeParameterSymbol.GrowPool(count);
			}
			ArrayBuilder<TypeWithAnnotations> instance = ArrayBuilder<TypeWithAnnotations>.GetInstance();
			for (int i = 0; i < count; i++)
			{
				instance.Add(TypeWithAnnotations.Create(IndexedTypeParameterSymbol.GetTypeParameter(i), NullableAnnotation.Ignored, default(ImmutableArray<CustomModifier>)));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x17001CF8 RID: 7416
		// (get) Token: 0x06006E12 RID: 28178 RVA: 0x00194C28 File Offset: 0x00192E28
		public override int Ordinal
		{
			get
			{
				return this._index;
			}
		}

		// Token: 0x06006E13 RID: 28179 RVA: 0x00194C30 File Offset: 0x00192E30
		internal override bool Equals(TypeSymbol t2, TypeCompareKind comparison)
		{
			return this == t2;
		}

		// Token: 0x06006E14 RID: 28180 RVA: 0x00194C36 File Offset: 0x00192E36
		public override int GetHashCode()
		{
			return this._index;
		}

		// Token: 0x17001CF9 RID: 7417
		// (get) Token: 0x06006E15 RID: 28181 RVA: 0x00194C3E File Offset: 0x00192E3E
		public override VarianceKind Variance
		{
			get
			{
				return VarianceKind.None;
			}
		}

		// Token: 0x17001CFA RID: 7418
		// (get) Token: 0x06006E16 RID: 28182 RVA: 0x00194C41 File Offset: 0x00192E41
		public override bool HasValueTypeConstraint
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001CFB RID: 7419
		// (get) Token: 0x06006E17 RID: 28183 RVA: 0x00194C44 File Offset: 0x00192E44
		public override bool IsValueTypeFromConstraintTypes
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001CFC RID: 7420
		// (get) Token: 0x06006E18 RID: 28184 RVA: 0x00194C47 File Offset: 0x00192E47
		public override bool HasReferenceTypeConstraint
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001CFD RID: 7421
		// (get) Token: 0x06006E19 RID: 28185 RVA: 0x00194C4A File Offset: 0x00192E4A
		public override bool IsReferenceTypeFromConstraintTypes
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001CFE RID: 7422
		// (get) Token: 0x06006E1A RID: 28186 RVA: 0x00194C4D File Offset: 0x00192E4D
		internal override bool? ReferenceTypeConstraintIsNullable
		{
			get
			{
				return new bool?(false);
			}
		}

		// Token: 0x17001CFF RID: 7423
		// (get) Token: 0x06006E1B RID: 28187 RVA: 0x00194C55 File Offset: 0x00192E55
		public override bool HasNotNullConstraint
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D00 RID: 7424
		// (get) Token: 0x06006E1C RID: 28188 RVA: 0x00194C58 File Offset: 0x00192E58
		internal override bool? IsNotNullable
		{
			get
			{
				return default(bool?);
			}
		}

		// Token: 0x17001D01 RID: 7425
		// (get) Token: 0x06006E1D RID: 28189 RVA: 0x00194C6E File Offset: 0x00192E6E
		public override bool HasUnmanagedTypeConstraint
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D02 RID: 7426
		// (get) Token: 0x06006E1E RID: 28190 RVA: 0x00194C71 File Offset: 0x00192E71
		public override bool HasConstructorConstraint
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001D03 RID: 7427
		// (get) Token: 0x06006E1F RID: 28191 RVA: 0x00194C74 File Offset: 0x00192E74
		public override Symbol ContainingSymbol
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001D04 RID: 7428
		// (get) Token: 0x06006E20 RID: 28192 RVA: 0x00194C77 File Offset: 0x00192E77
		public override ImmutableArray<Location> Locations
		{
			get
			{
				return ImmutableArray<Location>.Empty;
			}
		}

		// Token: 0x17001D05 RID: 7429
		// (get) Token: 0x06006E21 RID: 28193 RVA: 0x00194C7E File Offset: 0x00192E7E
		public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences
		{
			get
			{
				return ImmutableArray<SyntaxReference>.Empty;
			}
		}

		// Token: 0x06006E22 RID: 28194 RVA: 0x00194C85 File Offset: 0x00192E85
		internal override void EnsureAllConstraintsAreResolved()
		{
		}

		// Token: 0x06006E23 RID: 28195 RVA: 0x00194C87 File Offset: 0x00192E87
		internal override ImmutableArray<TypeWithAnnotations> GetConstraintTypes(ConsList<TypeParameterSymbol> inProgress)
		{
			return ImmutableArray<TypeWithAnnotations>.Empty;
		}

		// Token: 0x06006E24 RID: 28196 RVA: 0x00194C8E File Offset: 0x00192E8E
		internal override ImmutableArray<NamedTypeSymbol> GetInterfaces(ConsList<TypeParameterSymbol> inProgress)
		{
			return ImmutableArray<NamedTypeSymbol>.Empty;
		}

		// Token: 0x06006E25 RID: 28197 RVA: 0x00194C95 File Offset: 0x00192E95
		internal override NamedTypeSymbol GetEffectiveBaseClass(ConsList<TypeParameterSymbol> inProgress)
		{
			return null;
		}

		// Token: 0x06006E26 RID: 28198 RVA: 0x00194C98 File Offset: 0x00192E98
		internal override TypeSymbol GetDeducedBaseType(ConsList<TypeParameterSymbol> inProgress)
		{
			return null;
		}

		// Token: 0x17001D06 RID: 7430
		// (get) Token: 0x06006E27 RID: 28199 RVA: 0x00194C9B File Offset: 0x00192E9B
		public override bool IsImplicitlyDeclared
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04001B1F RID: 6943
		private static TypeParameterSymbol[] s_parameterPool = Array.Empty<TypeParameterSymbol>();

		// Token: 0x04001B20 RID: 6944
		private readonly int _index;
	}
}
