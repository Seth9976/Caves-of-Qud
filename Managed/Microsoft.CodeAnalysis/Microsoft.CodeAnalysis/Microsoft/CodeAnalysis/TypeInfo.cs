using System;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000159 RID: 345
	[NullableContext(2)]
	[Nullable(0)]
	public readonly struct TypeInfo : IEquatable<TypeInfo>
	{
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x00027AD7 File Offset: 0x00025CD7
		public ITypeSymbol Type { get; }

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x00027ADF File Offset: 0x00025CDF
		public NullabilityInfo Nullability { get; }

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x00027AE7 File Offset: 0x00025CE7
		public ITypeSymbol ConvertedType { get; }

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x00027AEF File Offset: 0x00025CEF
		public NullabilityInfo ConvertedNullability { get; }

		// Token: 0x06000DD7 RID: 3543 RVA: 0x00027AF7 File Offset: 0x00025CF7
		internal TypeInfo(ITypeSymbol type, ITypeSymbol convertedType, NullabilityInfo nullability, NullabilityInfo convertedNullability)
		{
			this = default(TypeInfo);
			this.Type = type;
			this.Nullability = nullability;
			this.ConvertedType = convertedType;
			this.ConvertedNullability = convertedNullability;
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x00027B20 File Offset: 0x00025D20
		public bool Equals(TypeInfo other)
		{
			return object.Equals(this.Type, other.Type) && object.Equals(this.ConvertedType, other.ConvertedType) && this.Nullability.Equals(other.Nullability) && this.ConvertedNullability.Equals(other.ConvertedNullability);
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x00027B83 File Offset: 0x00025D83
		public override bool Equals(object obj)
		{
			return obj is TypeInfo && this.Equals((TypeInfo)obj);
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x00027B9C File Offset: 0x00025D9C
		public override int GetHashCode()
		{
			return Hash.Combine<ITypeSymbol>(this.ConvertedType, Hash.Combine<ITypeSymbol>(this.Type, Hash.Combine(this.Nullability.GetHashCode(), this.ConvertedNullability.GetHashCode())));
		}

		// Token: 0x040003CA RID: 970
		internal static readonly TypeInfo None = new TypeInfo(null, null, default(NullabilityInfo), default(NullabilityInfo));
	}
}
