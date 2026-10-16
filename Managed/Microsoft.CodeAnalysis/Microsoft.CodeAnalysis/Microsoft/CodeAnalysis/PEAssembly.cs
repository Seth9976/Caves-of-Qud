using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using System.Threading;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x020001AA RID: 426
	internal sealed class PEAssembly
	{
		// Token: 0x0600110D RID: 4365 RVA: 0x000337B8 File Offset: 0x000319B8
		internal PEAssembly(AssemblyMetadata owner, ImmutableArray<PEModule> modules)
		{
			this._identity = modules[0].ReadAssemblyIdentityOrThrow();
			ArrayBuilder<AssemblyIdentity> instance = ArrayBuilder<AssemblyIdentity>.GetInstance();
			int[] array = new int[modules.Length];
			for (int i = 0; i < modules.Length; i++)
			{
				ImmutableArray<AssemblyIdentity> referencedAssemblies = modules[i].ReferencedAssemblies;
				array[i] = referencedAssemblies.Length;
				instance.AddRange(referencedAssemblies);
			}
			this._modules = modules;
			this.AssemblyReferences = instance.ToImmutableAndFree();
			this.ModuleReferenceCounts = array.AsImmutableOrNull<int>();
			this._owner = owner;
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x0600110E RID: 4366 RVA: 0x00033848 File Offset: 0x00031A48
		internal EntityHandle Handle
		{
			get
			{
				return EntityHandle.AssemblyDefinition;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x0600110F RID: 4367 RVA: 0x00033854 File Offset: 0x00031A54
		internal PEModule ManifestModule
		{
			get
			{
				return this.Modules[0];
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001110 RID: 4368 RVA: 0x00033870 File Offset: 0x00031A70
		internal ImmutableArray<PEModule> Modules
		{
			get
			{
				return this._modules;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001111 RID: 4369 RVA: 0x00033878 File Offset: 0x00031A78
		internal AssemblyIdentity Identity
		{
			get
			{
				return this._identity;
			}
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x00033880 File Offset: 0x00031A80
		internal bool ContainsNoPiaLocalTypes()
		{
			if (this._lazyContainsNoPiaLocalTypes == ThreeState.Unknown)
			{
				ImmutableArray<PEModule>.Enumerator enumerator = this.Modules.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.ContainsNoPiaLocalTypes())
					{
						this._lazyContainsNoPiaLocalTypes = ThreeState.True;
						return true;
					}
				}
				this._lazyContainsNoPiaLocalTypes = ThreeState.False;
			}
			return this._lazyContainsNoPiaLocalTypes == ThreeState.True;
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x000338D8 File Offset: 0x00031AD8
		private Dictionary<string, List<ImmutableArray<byte>>> BuildInternalsVisibleToMap()
		{
			Dictionary<string, List<ImmutableArray<byte>>> dictionary = new Dictionary<string, List<ImmutableArray<byte>>>(StringComparer.OrdinalIgnoreCase);
			ImmutableArray<string>.Enumerator enumerator = this.Modules[0].GetInternalsVisibleToAttributeValues(this.Handle).GetEnumerator();
			while (enumerator.MoveNext())
			{
				AssemblyIdentity assemblyIdentity;
				if (AssemblyIdentity.TryParseDisplayName(enumerator.Current, out assemblyIdentity))
				{
					List<ImmutableArray<byte>> list;
					if (dictionary.TryGetValue(assemblyIdentity.Name, ref list))
					{
						list.Add(assemblyIdentity.PublicKey);
					}
					else
					{
						list = new List<ImmutableArray<byte>>();
						list.Add(assemblyIdentity.PublicKey);
						dictionary[assemblyIdentity.Name] = list;
					}
				}
			}
			return dictionary;
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x00033974 File Offset: 0x00031B74
		internal IEnumerable<ImmutableArray<byte>> GetInternalsVisibleToPublicKeys(string simpleName)
		{
			if (this._lazyInternalsVisibleToMap == null)
			{
				Interlocked.CompareExchange<Dictionary<string, List<ImmutableArray<byte>>>>(ref this._lazyInternalsVisibleToMap, this.BuildInternalsVisibleToMap(), null);
			}
			List<ImmutableArray<byte>> list;
			this._lazyInternalsVisibleToMap.TryGetValue(simpleName, ref list);
			IEnumerable<ImmutableArray<byte>> enumerable = list;
			return enumerable ?? SpecializedCollections.EmptyEnumerable<ImmutableArray<byte>>();
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001115 RID: 4373 RVA: 0x000339B8 File Offset: 0x00031BB8
		internal bool DeclaresTheObjectClass
		{
			get
			{
				if (this._lazyDeclaresTheObjectClass == ThreeState.Unknown)
				{
					bool flag = this._modules[0].MetadataReader.DeclaresTheObjectClass();
					this._lazyDeclaresTheObjectClass = flag.ToThreeState();
				}
				return this._lazyDeclaresTheObjectClass == ThreeState.True;
			}
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x000339FC File Offset: 0x00031BFC
		public AssemblyMetadata GetNonDisposableMetadata()
		{
			return this._owner.Copy();
		}

		// Token: 0x04000542 RID: 1346
		internal readonly ImmutableArray<AssemblyIdentity> AssemblyReferences;

		// Token: 0x04000543 RID: 1347
		internal readonly ImmutableArray<int> ModuleReferenceCounts;

		// Token: 0x04000544 RID: 1348
		private readonly ImmutableArray<PEModule> _modules;

		// Token: 0x04000545 RID: 1349
		private readonly AssemblyIdentity _identity;

		// Token: 0x04000546 RID: 1350
		private ThreeState _lazyContainsNoPiaLocalTypes;

		// Token: 0x04000547 RID: 1351
		private ThreeState _lazyDeclaresTheObjectClass;

		// Token: 0x04000548 RID: 1352
		private readonly AssemblyMetadata _owner;

		// Token: 0x04000549 RID: 1353
		private Dictionary<string, List<ImmutableArray<byte>>> _lazyInternalsVisibleToMap;
	}
}
