using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020005A5 RID: 1445
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class TypeParameterBounds
	{
		// Token: 0x060078C5 RID: 30917 RVA: 0x001B9C13 File Offset: 0x001B7E13
		public TypeParameterBounds([global::System.Runtime.CompilerServices.Nullable(0)] ImmutableArray<TypeWithAnnotations> constraintTypes, [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<NamedTypeSymbol> interfaces, NamedTypeSymbol effectiveBaseClass, TypeSymbol deducedBaseType)
		{
			this.ConstraintTypes = constraintTypes;
			this.Interfaces = interfaces;
			this.EffectiveBaseClass = effectiveBaseClass;
			this.DeducedBaseType = deducedBaseType;
		}

		// Token: 0x060078C6 RID: 30918 RVA: 0x001B9C38 File Offset: 0x001B7E38
		private TypeParameterBounds()
		{
			this.EffectiveBaseClass = null;
			this.DeducedBaseType = null;
		}

		// Token: 0x04001D10 RID: 7440
		public static readonly TypeParameterBounds Unset = new TypeParameterBounds();

		// Token: 0x04001D11 RID: 7441
		[global::System.Runtime.CompilerServices.Nullable(0)]
		public readonly ImmutableArray<TypeWithAnnotations> ConstraintTypes;

		// Token: 0x04001D12 RID: 7442
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public readonly ImmutableArray<NamedTypeSymbol> Interfaces;

		// Token: 0x04001D13 RID: 7443
		public readonly NamedTypeSymbol EffectiveBaseClass;

		// Token: 0x04001D14 RID: 7444
		public readonly TypeSymbol DeducedBaseType;
	}
}
