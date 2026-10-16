using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200059A RID: 1434
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class SynthesizedParameterSymbol : SynthesizedParameterSymbolBase
	{
		// Token: 0x0600780A RID: 30730 RVA: 0x001B8A90 File Offset: 0x001B6C90
		private SynthesizedParameterSymbol([global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol container, TypeWithAnnotations type, int ordinal, RefKind refKind, string name)
			: base(container, type, ordinal, refKind, name)
		{
		}

		// Token: 0x0600780B RID: 30731 RVA: 0x001B8A9F File Offset: 0x001B6C9F
		public static ParameterSymbol Create([global::System.Runtime.CompilerServices.Nullable(2)] MethodSymbol container, TypeWithAnnotations type, int ordinal, RefKind refKind, string name = "", [global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<CustomModifier> refCustomModifiers = default(ImmutableArray<CustomModifier>), [global::System.Runtime.CompilerServices.Nullable(2)] SourceComplexParameterSymbol baseParameterForAttributes = null)
		{
			if (refCustomModifiers.IsDefaultOrEmpty && baseParameterForAttributes == null)
			{
				return new SynthesizedParameterSymbol(container, type, ordinal, refKind, name);
			}
			return new SynthesizedComplexParameterSymbol(container, type, ordinal, refKind, name, refCustomModifiers.NullToEmpty<CustomModifier>(), baseParameterForAttributes);
		}

		// Token: 0x0600780C RID: 30732 RVA: 0x001B8AD0 File Offset: 0x001B6CD0
		[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		internal static ImmutableArray<ParameterSymbol> DeriveParameters(MethodSymbol sourceMethod, MethodSymbol destinationMethod)
		{
			ArrayBuilder<ParameterSymbol> instance = ArrayBuilder<ParameterSymbol>.GetInstance();
			foreach (ParameterSymbol parameterSymbol in sourceMethod.Parameters)
			{
				instance.Add(SynthesizedParameterSymbol.Create(destinationMethod, parameterSymbol.TypeWithAnnotations, parameterSymbol.Ordinal, parameterSymbol.RefKind, parameterSymbol.Name, parameterSymbol.RefCustomModifiers, null));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x17002219 RID: 8729
		// (get) Token: 0x0600780D RID: 30733 RVA: 0x001B8B36 File Offset: 0x001B6D36
		[global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
		public override ImmutableArray<CustomModifier> RefCustomModifiers
		{
			[return: global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })]
			get
			{
				return ImmutableArray<CustomModifier>.Empty;
			}
		}

		// Token: 0x1700221A RID: 8730
		// (get) Token: 0x0600780E RID: 30734 RVA: 0x001B8B3D File Offset: 0x001B6D3D
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override MarshalPseudoCustomAttributeData MarshallingInformation
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}
	}
}
