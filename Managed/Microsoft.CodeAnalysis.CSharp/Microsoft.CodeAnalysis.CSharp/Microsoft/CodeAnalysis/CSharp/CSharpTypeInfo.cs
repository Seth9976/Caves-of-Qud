using System;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000114 RID: 276
	internal readonly struct CSharpTypeInfo : IEquatable<CSharpTypeInfo>
	{
		// Token: 0x060016D4 RID: 5844 RVA: 0x0007B0CC File Offset: 0x000792CC
		internal CSharpTypeInfo(TypeSymbol type, TypeSymbol convertedType, NullabilityInfo nullability, NullabilityInfo convertedNullability, Conversion implicitConversion)
		{
			this.Type = type.GetNonErrorGuess() ?? type;
			this.ConvertedType = convertedType.GetNonErrorGuess() ?? convertedType;
			this.Nullability = nullability;
			this.ConvertedNullability = convertedNullability;
			this.ImplicitConversion = implicitConversion;
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x0007B108 File Offset: 0x00079308
		public static implicit operator TypeInfo(CSharpTypeInfo info)
		{
			TypeSymbol type = info.Type;
			ITypeSymbol typeSymbol = ((type != null) ? type.GetITypeSymbol(info.Nullability.FlowState.ToAnnotation()) : null);
			TypeSymbol convertedType = info.ConvertedType;
			return new TypeInfo(typeSymbol, (convertedType != null) ? convertedType.GetITypeSymbol(info.ConvertedNullability.FlowState.ToAnnotation()) : null, info.Nullability, info.ConvertedNullability);
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x0007B16C File Offset: 0x0007936C
		public override bool Equals(object obj)
		{
			return obj is CSharpTypeInfo && this.Equals((CSharpTypeInfo)obj);
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x0007B184 File Offset: 0x00079384
		public bool Equals(CSharpTypeInfo other)
		{
			return this.ImplicitConversion.Equals(other.ImplicitConversion) && TypeSymbol.Equals(this.Type, other.Type, TypeCompareKind.ConsiderEverything) && TypeSymbol.Equals(this.ConvertedType, other.ConvertedType, TypeCompareKind.ConsiderEverything) && this.Nullability.Equals(other.Nullability) && this.ConvertedNullability.Equals(other.ConvertedNullability);
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x0007B1F8 File Offset: 0x000793F8
		public override int GetHashCode()
		{
			return Hash.Combine<TypeSymbol>(this.ConvertedType, Hash.Combine<TypeSymbol>(this.Type, Hash.Combine(this.Nullability.GetHashCode(), Hash.Combine(this.ConvertedNullability.GetHashCode(), this.ImplicitConversion.GetHashCode()))));
		}

		// Token: 0x04000651 RID: 1617
		internal static readonly CSharpTypeInfo None = new CSharpTypeInfo(null, null, default(NullabilityInfo), default(NullabilityInfo), Conversion.Identity);

		// Token: 0x04000652 RID: 1618
		public readonly TypeSymbol Type;

		// Token: 0x04000653 RID: 1619
		public readonly NullabilityInfo Nullability;

		// Token: 0x04000654 RID: 1620
		public readonly TypeSymbol ConvertedType;

		// Token: 0x04000655 RID: 1621
		public readonly NullabilityInfo ConvertedNullability;

		// Token: 0x04000656 RID: 1622
		public readonly Conversion ImplicitConversion;
	}
}
