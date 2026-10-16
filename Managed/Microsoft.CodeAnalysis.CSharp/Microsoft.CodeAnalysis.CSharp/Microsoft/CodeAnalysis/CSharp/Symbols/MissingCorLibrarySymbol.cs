using System;
using System.Collections.Immutable;
using System.Threading;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x020004F4 RID: 1268
	internal sealed class MissingCorLibrarySymbol : MissingAssemblySymbol
	{
		// Token: 0x06006B90 RID: 27536 RVA: 0x0018F2BC File Offset: 0x0018D4BC
		private MissingCorLibrarySymbol()
			: base(new AssemblyIdentity("<Missing Core Assembly>", null, null, default(ImmutableArray<byte>), false, false, 0))
		{
			base.SetCorLibrary(this);
		}

		// Token: 0x06006B91 RID: 27537 RVA: 0x0018F2F0 File Offset: 0x0018D4F0
		internal override NamedTypeSymbol GetDeclaredSpecialType(SpecialType type)
		{
			if (this._lazySpecialTypes == null)
			{
				Interlocked.CompareExchange<NamedTypeSymbol[]>(ref this._lazySpecialTypes, new NamedTypeSymbol[46], null);
			}
			if (this._lazySpecialTypes[(int)type] == null)
			{
				MetadataTypeName metadataTypeName = MetadataTypeName.FromFullName(type.GetMetadataName(), true, -1);
				NamedTypeSymbol namedTypeSymbol = new MissingMetadataTypeSymbol.TopLevel(this.moduleSymbol, ref metadataTypeName, type, null);
				Interlocked.CompareExchange<NamedTypeSymbol>(ref this._lazySpecialTypes[(int)type], namedTypeSymbol, null);
			}
			return this._lazySpecialTypes[(int)type];
		}

		// Token: 0x04001AB2 RID: 6834
		internal static readonly MissingCorLibrarySymbol Instance = new MissingCorLibrarySymbol();

		// Token: 0x04001AB3 RID: 6835
		private NamedTypeSymbol[] _lazySpecialTypes;
	}
}
