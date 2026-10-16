using System;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Symbols;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000234 RID: 564
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class UnmanagedCallersOnlyAttributeData
	{
		// Token: 0x060015F6 RID: 5622 RVA: 0x000453E0 File Offset: 0x000435E0
		internal static UnmanagedCallersOnlyAttributeData Create([Nullable(new byte[] { 2, 1 })] ImmutableHashSet<INamedTypeSymbolInternal> callingConventionTypes)
		{
			UnmanagedCallersOnlyAttributeData unmanagedCallersOnlyAttributeData;
			if (callingConventionTypes == null || callingConventionTypes.IsEmpty)
			{
				unmanagedCallersOnlyAttributeData = UnmanagedCallersOnlyAttributeData.PlatformDefault;
			}
			else
			{
				unmanagedCallersOnlyAttributeData = new UnmanagedCallersOnlyAttributeData(callingConventionTypes);
			}
			return unmanagedCallersOnlyAttributeData;
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x00045408 File Offset: 0x00043608
		private UnmanagedCallersOnlyAttributeData(ImmutableHashSet<INamedTypeSymbolInternal> callingConventionTypes)
		{
			this.CallingConventionTypes = callingConventionTypes;
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x00045418 File Offset: 0x00043618
		internal static bool IsCallConvsTypedConstant(string key, bool isField, in TypedConstant value)
		{
			if (isField && key == "CallConvs")
			{
				TypedConstant typedConstant = value;
				if (typedConstant.Kind == TypedConstantKind.Array)
				{
					typedConstant = value;
					if (!typedConstant.Values.IsDefaultOrEmpty)
					{
						typedConstant = value;
						return typedConstant.Values.All<TypedConstant>((TypedConstant v) => v.Kind == TypedConstantKind.Type);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000915 RID: 2325
		internal static readonly UnmanagedCallersOnlyAttributeData Uninitialized = new UnmanagedCallersOnlyAttributeData(ImmutableHashSet<INamedTypeSymbolInternal>.Empty);

		// Token: 0x04000916 RID: 2326
		internal static readonly UnmanagedCallersOnlyAttributeData AttributePresentDataNotBound = new UnmanagedCallersOnlyAttributeData(ImmutableHashSet<INamedTypeSymbolInternal>.Empty);

		// Token: 0x04000917 RID: 2327
		private static readonly UnmanagedCallersOnlyAttributeData PlatformDefault = new UnmanagedCallersOnlyAttributeData(ImmutableHashSet<INamedTypeSymbolInternal>.Empty);

		// Token: 0x04000918 RID: 2328
		public const string CallConvsPropertyName = "CallConvs";

		// Token: 0x04000919 RID: 2329
		public readonly ImmutableHashSet<INamedTypeSymbolInternal> CallingConventionTypes;
	}
}
