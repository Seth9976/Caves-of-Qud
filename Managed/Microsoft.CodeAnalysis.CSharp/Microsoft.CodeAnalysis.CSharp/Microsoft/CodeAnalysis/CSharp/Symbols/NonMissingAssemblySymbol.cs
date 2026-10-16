using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp.Symbols
{
	// Token: 0x02000501 RID: 1281
	internal abstract class NonMissingAssemblySymbol : AssemblySymbol
	{
		// Token: 0x17001C02 RID: 7170
		// (get) Token: 0x06006C57 RID: 27735 RVA: 0x001903D9 File Offset: 0x0018E5D9
		internal sealed override bool IsMissing
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001C03 RID: 7171
		// (get) Token: 0x06006C58 RID: 27736 RVA: 0x001903DC File Offset: 0x0018E5DC
		public sealed override NamespaceSymbol GlobalNamespace
		{
			get
			{
				if (this._globalNamespace == null)
				{
					IEnumerable<NamespaceSymbol> enumerable = this.Modules.Select<ModuleSymbol, NamespaceSymbol>((ModuleSymbol m) => m.GlobalNamespace);
					NamespaceSymbol namespaceSymbol = MergedNamespaceSymbol.Create(new NamespaceExtent(this), null, enumerable.AsImmutable<NamespaceSymbol>(), null);
					Interlocked.CompareExchange<NamespaceSymbol>(ref this._globalNamespace, namespaceSymbol, null);
				}
				return this._globalNamespace;
			}
		}

		// Token: 0x06006C59 RID: 27737 RVA: 0x00190444 File Offset: 0x0018E644
		internal sealed override NamedTypeSymbol LookupTopLevelMetadataTypeWithCycleDetection(ref MetadataTypeName emittedName, ConsList<AssemblySymbol> visitedAssemblies, bool digThroughForwardedTypes)
		{
			NamedTypeSymbol namedTypeSymbol = this.LookupTopLevelMetadataTypeInCache(ref emittedName);
			if (namedTypeSymbol == null)
			{
				ImmutableArray<ModuleSymbol> modules = this.Modules;
				int length = modules.Length;
				int i = 0;
				namedTypeSymbol = modules[i].LookupTopLevelMetadataType(ref emittedName);
				if (namedTypeSymbol is MissingMetadataTypeSymbol)
				{
					for (i = 1; i < length; i++)
					{
						NamedTypeSymbol namedTypeSymbol2 = modules[i].LookupTopLevelMetadataType(ref emittedName);
						if (!(namedTypeSymbol2 is MissingMetadataTypeSymbol))
						{
							namedTypeSymbol = namedTypeSymbol2;
							break;
						}
					}
				}
				bool flag = i < length;
				if (!flag && digThroughForwardedTypes)
				{
					NamedTypeSymbol namedTypeSymbol3 = this.TryLookupForwardedMetadataTypeWithCycleDetection(ref emittedName, visitedAssemblies);
					if (namedTypeSymbol3 != null)
					{
						namedTypeSymbol = namedTypeSymbol3;
					}
				}
				if (digThroughForwardedTypes || flag)
				{
					this.CacheTopLevelMetadataType(ref emittedName, namedTypeSymbol);
				}
				return namedTypeSymbol;
			}
			if (digThroughForwardedTypes || (!namedTypeSymbol.IsErrorType() && namedTypeSymbol.ContainingAssembly == this))
			{
				return namedTypeSymbol;
			}
			return new MissingMetadataTypeSymbol.TopLevel(this.Modules[0], ref emittedName, null);
		}

		// Token: 0x06006C5A RID: 27738
		internal abstract override NamedTypeSymbol TryLookupForwardedMetadataTypeWithCycleDetection(ref MetadataTypeName emittedName, ConsList<AssemblySymbol> visitedAssemblies);

		// Token: 0x06006C5B RID: 27739 RVA: 0x00190514 File Offset: 0x0018E714
		private NamedTypeSymbol LookupTopLevelMetadataTypeInCache(ref MetadataTypeName emittedName)
		{
			NamedTypeSymbol namedTypeSymbol = null;
			if (this._emittedNameToTypeMap.TryGetValue(emittedName.ToKey(), ref namedTypeSymbol))
			{
				return namedTypeSymbol;
			}
			return null;
		}

		// Token: 0x06006C5C RID: 27740 RVA: 0x0019053C File Offset: 0x0018E73C
		internal NamedTypeSymbol CachedTypeByEmittedName(string emittedname)
		{
			MetadataTypeName metadataTypeName = MetadataTypeName.FromFullName(emittedname, false, -1);
			return this._emittedNameToTypeMap[metadataTypeName.ToKey()];
		}

		// Token: 0x17001C04 RID: 7172
		// (get) Token: 0x06006C5D RID: 27741 RVA: 0x00190564 File Offset: 0x0018E764
		internal int EmittedNameToTypeMapCount
		{
			get
			{
				return this._emittedNameToTypeMap.Count;
			}
		}

		// Token: 0x06006C5E RID: 27742 RVA: 0x00190571 File Offset: 0x0018E771
		private void CacheTopLevelMetadataType(ref MetadataTypeName emittedName, NamedTypeSymbol result)
		{
			this._emittedNameToTypeMap.GetOrAdd(emittedName.ToKey(), result);
		}

		// Token: 0x04001ACB RID: 6859
		private readonly ConcurrentDictionary<MetadataTypeName.Key, NamedTypeSymbol> _emittedNameToTypeMap = new ConcurrentDictionary<MetadataTypeName.Key, NamedTypeSymbol>();

		// Token: 0x04001ACC RID: 6860
		private NamespaceSymbol _globalNamespace;
	}
}
