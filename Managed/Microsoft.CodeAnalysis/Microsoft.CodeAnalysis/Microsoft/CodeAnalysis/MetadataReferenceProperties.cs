using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001BC RID: 444
	[NullableContext(1)]
	[Nullable(0)]
	public struct MetadataReferenceProperties : IEquatable<MetadataReferenceProperties>
	{
		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x0003977C File Offset: 0x0003797C
		public static MetadataReferenceProperties Module
		{
			get
			{
				return new MetadataReferenceProperties(MetadataImageKind.Module, default(ImmutableArray<string>), false);
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001299 RID: 4761 RVA: 0x0003979C File Offset: 0x0003799C
		public static MetadataReferenceProperties Assembly
		{
			get
			{
				return new MetadataReferenceProperties(MetadataImageKind.Assembly, default(ImmutableArray<string>), false);
			}
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x000397BC File Offset: 0x000379BC
		public MetadataReferenceProperties(MetadataImageKind kind = MetadataImageKind.Assembly, [Nullable(new byte[] { 0, 1 })] ImmutableArray<string> aliases = default(ImmutableArray<string>), bool embedInteropTypes = false)
		{
			if (!kind.IsValid())
			{
				throw new ArgumentOutOfRangeException("kind");
			}
			if (kind == MetadataImageKind.Module)
			{
				if (embedInteropTypes)
				{
					throw new ArgumentException(CodeAnalysisResources.CannotEmbedInteropTypesFromModule, "embedInteropTypes");
				}
				if (!aliases.IsDefaultOrEmpty)
				{
					throw new ArgumentException(CodeAnalysisResources.CannotAliasModule, "aliases");
				}
			}
			if (!aliases.IsDefaultOrEmpty)
			{
				ImmutableArray<string>.Enumerator enumerator = aliases.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.IsValidClrTypeName())
					{
						throw new ArgumentException(CodeAnalysisResources.InvalidAlias, "aliases");
					}
				}
			}
			this._kind = kind;
			this._aliases = aliases;
			this._embedInteropTypes = embedInteropTypes;
			this.HasRecursiveAliases = false;
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x00039862 File Offset: 0x00037A62
		internal MetadataReferenceProperties(MetadataImageKind kind, [Nullable(new byte[] { 0, 1 })] ImmutableArray<string> aliases, bool embedInteropTypes, bool hasRecursiveAliases)
		{
			this = new MetadataReferenceProperties(kind, aliases, embedInteropTypes);
			this.HasRecursiveAliases = hasRecursiveAliases;
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00039875 File Offset: 0x00037A75
		public MetadataReferenceProperties WithAliases(IEnumerable<string> aliases)
		{
			return this.WithAliases(aliases.AsImmutableOrEmpty<string>());
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00039883 File Offset: 0x00037A83
		public MetadataReferenceProperties WithAliases([Nullable(new byte[] { 0, 1 })] ImmutableArray<string> aliases)
		{
			return new MetadataReferenceProperties(this._kind, aliases, this._embedInteropTypes, this.HasRecursiveAliases);
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0003989D File Offset: 0x00037A9D
		public MetadataReferenceProperties WithEmbedInteropTypes(bool embedInteropTypes)
		{
			return new MetadataReferenceProperties(this._kind, this._aliases, embedInteropTypes, this.HasRecursiveAliases);
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x000398B7 File Offset: 0x00037AB7
		internal MetadataReferenceProperties WithRecursiveAliases(bool value)
		{
			return new MetadataReferenceProperties(this._kind, this._aliases, this._embedInteropTypes, value);
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x060012A0 RID: 4768 RVA: 0x000398D1 File Offset: 0x00037AD1
		public MetadataImageKind Kind
		{
			get
			{
				return this._kind;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x060012A1 RID: 4769 RVA: 0x000398D9 File Offset: 0x00037AD9
		public static string GlobalAlias
		{
			get
			{
				return "global";
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x060012A2 RID: 4770 RVA: 0x000398E0 File Offset: 0x00037AE0
		[Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<string> Aliases
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._aliases.NullToEmpty<string>();
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x060012A3 RID: 4771 RVA: 0x000398ED File Offset: 0x00037AED
		public bool EmbedInteropTypes
		{
			get
			{
				return this._embedInteropTypes;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x060012A4 RID: 4772 RVA: 0x000398F5 File Offset: 0x00037AF5
		// (set) Token: 0x060012A5 RID: 4773 RVA: 0x000398FD File Offset: 0x00037AFD
		internal bool HasRecursiveAliases { readonly get; private set; }

		// Token: 0x060012A6 RID: 4774 RVA: 0x00039906 File Offset: 0x00037B06
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is MetadataReferenceProperties && this.Equals((MetadataReferenceProperties)obj);
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x00039920 File Offset: 0x00037B20
		public bool Equals(MetadataReferenceProperties other)
		{
			return this.Aliases.SequenceEqual<string, string>(other.Aliases, null) && this._embedInteropTypes == other._embedInteropTypes && this._kind == other._kind && this.HasRecursiveAliases == other.HasRecursiveAliases;
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0003996F File Offset: 0x00037B6F
		public override int GetHashCode()
		{
			return Hash.Combine(Hash.CombineValues<string>(this.Aliases, int.MaxValue), Hash.Combine(this._embedInteropTypes, Hash.Combine(this.HasRecursiveAliases, this._kind.GetHashCode())));
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x000399AD File Offset: 0x00037BAD
		public static bool operator ==(MetadataReferenceProperties left, MetadataReferenceProperties right)
		{
			return left.Equals(right);
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x000399B7 File Offset: 0x00037BB7
		public static bool operator !=(MetadataReferenceProperties left, MetadataReferenceProperties right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000595 RID: 1429
		private readonly MetadataImageKind _kind;

		// Token: 0x04000596 RID: 1430
		[Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<string> _aliases;

		// Token: 0x04000597 RID: 1431
		private readonly bool _embedInteropTypes;
	}
}
