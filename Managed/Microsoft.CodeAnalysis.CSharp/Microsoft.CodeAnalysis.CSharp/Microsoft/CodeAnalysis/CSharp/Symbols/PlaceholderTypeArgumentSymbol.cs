using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x0200050D RID: 1293
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal sealed class PlaceholderTypeArgumentSymbol : ErrorTypeSymbol
	{
		// Token: 0x06006CB0 RID: 27824 RVA: 0x00191B91 File Offset: 0x0018FD91
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public static ImmutableArray<TypeWithAnnotations> CreateTypeArguments([global::System.Runtime.CompilerServices.Nullable(new byte[] { 0, 1 })] ImmutableArray<TypeParameterSymbol> typeParameters)
		{
			return typeParameters.SelectAsArray<TypeParameterSymbol, TypeWithAnnotations>((TypeParameterSymbol _) => PlaceholderTypeArgumentSymbol.s_instance);
		}

		// Token: 0x06006CB1 RID: 27825 RVA: 0x00191BB8 File Offset: 0x0018FDB8
		private PlaceholderTypeArgumentSymbol()
			: base(null)
		{
		}

		// Token: 0x06006CB2 RID: 27826 RVA: 0x00191BC1 File Offset: 0x0018FDC1
		protected override NamedTypeSymbol WithTupleDataCore(NamedTypeSymbol.TupleExtraData newData)
		{
			throw ExceptionUtilities.Unreachable;
		}

		// Token: 0x17001C18 RID: 7192
		// (get) Token: 0x06006CB3 RID: 27827 RVA: 0x00191BC8 File Offset: 0x0018FDC8
		public override string Name
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17001C19 RID: 7193
		// (get) Token: 0x06006CB4 RID: 27828 RVA: 0x00191BCF File Offset: 0x0018FDCF
		internal override bool MangleName
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001C1A RID: 7194
		// (get) Token: 0x06006CB5 RID: 27829 RVA: 0x00191BD2 File Offset: 0x0018FDD2
		[global::System.Runtime.CompilerServices.Nullable(2)]
		internal override DiagnosticInfo ErrorInfo
		{
			[global::System.Runtime.CompilerServices.NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x06006CB6 RID: 27830 RVA: 0x00191BD5 File Offset: 0x0018FDD5
		internal override bool Equals(TypeSymbol t2, TypeCompareKind comparison)
		{
			return t2 == this;
		}

		// Token: 0x06006CB7 RID: 27831 RVA: 0x00191BDB File Offset: 0x0018FDDB
		public override int GetHashCode()
		{
			return RuntimeHelpers.GetHashCode(this);
		}

		// Token: 0x04001AEA RID: 6890
		private static readonly TypeWithAnnotations s_instance = TypeWithAnnotations.Create(new PlaceholderTypeArgumentSymbol(), NullableAnnotation.Oblivious, default(ImmutableArray<CustomModifier>));
	}
}
