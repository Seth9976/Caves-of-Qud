using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000167 RID: 359
	internal abstract class AnalyzerAssemblyLoader : IAnalyzerAssemblyLoader
	{
		// Token: 0x06000E78 RID: 3704
		protected abstract Assembly LoadFromPathImpl(string fullPath);

		// Token: 0x06000E79 RID: 3705 RVA: 0x0002BED0 File Offset: 0x0002A0D0
		public void AddDependencyLocation(string fullPath)
		{
			CompilerPathUtilities.RequireAbsolutePath(fullPath, "fullPath");
			string fileName = PathUtilities.GetFileName(fullPath, false);
			object guard = this._guard;
			lock (guard)
			{
				HashSet<string> hashSet;
				if (!this._knownAssemblyPathsBySimpleName.TryGetValue(fileName, ref hashSet))
				{
					hashSet = new HashSet<string>(PathUtilities.Comparer);
					this._knownAssemblyPathsBySimpleName.Add(fileName, hashSet);
				}
				hashSet.Add(fullPath);
			}
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x0002BF50 File Offset: 0x0002A150
		public Assembly LoadFromPath(string fullPath)
		{
			CompilerPathUtilities.RequireAbsolutePath(fullPath, "fullPath");
			return this.LoadFromPathUnchecked(fullPath);
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0002BF64 File Offset: 0x0002A164
		private Assembly LoadFromPathUnchecked(string fullPath)
		{
			return this.LoadFromPathUncheckedCore(fullPath, null);
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x0002BF70 File Offset: 0x0002A170
		private Assembly LoadFromPathUncheckedCore(string fullPath, AssemblyIdentity identity = null)
		{
			Assembly assembly = null;
			object guard = this._guard;
			lock (guard)
			{
				Assembly assembly2;
				if (this._loadedAssembliesByPath.TryGetValue(fullPath, ref assembly2))
				{
					assembly = assembly2;
				}
				else
				{
					if (identity == null)
					{
						identity = this.GetOrAddAssemblyIdentity(fullPath);
					}
					if (identity != null && this._loadedAssembliesByIdentity.TryGetValue(identity, ref assembly2))
					{
						assembly = assembly2;
					}
				}
			}
			if (assembly == null)
			{
				assembly = this.LoadFromPathImpl(fullPath);
			}
			return this.AddToCache(assembly, fullPath, identity);
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x0002C004 File Offset: 0x0002A204
		private Assembly AddToCache(Assembly assembly, string fullPath, AssemblyIdentity identity)
		{
			identity = this.AddToCache(fullPath, identity ?? AssemblyIdentity.FromAssemblyDefinition(assembly));
			object guard = this._guard;
			Assembly assembly3;
			lock (guard)
			{
				Assembly assembly2;
				if (this._loadedAssembliesByIdentity.TryGetValue(identity, ref assembly2))
				{
					assembly = assembly2;
				}
				else
				{
					this._loadedAssembliesByIdentity.Add(identity, assembly);
				}
				this._loadedAssembliesByPath[fullPath] = assembly;
				assembly3 = assembly;
			}
			return assembly3;
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x0002C084 File Offset: 0x0002A284
		private AssemblyIdentity GetOrAddAssemblyIdentity(string fullPath)
		{
			object guard = this._guard;
			lock (guard)
			{
				AssemblyIdentity assemblyIdentity;
				if (this._loadedAssemblyIdentitiesByPath.TryGetValue(fullPath, ref assemblyIdentity))
				{
					return assemblyIdentity;
				}
			}
			AssemblyIdentity assemblyIdentity2 = AssemblyIdentityUtils.TryGetAssemblyIdentity(fullPath);
			return this.AddToCache(fullPath, assemblyIdentity2);
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0002C0E8 File Offset: 0x0002A2E8
		private AssemblyIdentity AddToCache(string fullPath, AssemblyIdentity identity)
		{
			object guard = this._guard;
			lock (guard)
			{
				AssemblyIdentity assemblyIdentity;
				if (this._loadedAssemblyIdentitiesByPath.TryGetValue(fullPath, ref assemblyIdentity) && assemblyIdentity != null)
				{
					identity = assemblyIdentity;
				}
				else
				{
					this._loadedAssemblyIdentitiesByPath[fullPath] = identity;
				}
			}
			return identity;
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x0002C150 File Offset: 0x0002A350
		public Assembly Load(string displayName)
		{
			AssemblyIdentity assemblyIdentity;
			if (!AssemblyIdentity.TryParseDisplayName(displayName, out assemblyIdentity))
			{
				return null;
			}
			object guard = this._guard;
			ImmutableArray<string> immutableArray;
			lock (guard)
			{
				Assembly assembly;
				if (this._loadedAssembliesByIdentity.TryGetValue(assemblyIdentity, ref assembly))
				{
					return assembly;
				}
				HashSet<string> hashSet;
				if (!this._knownAssemblyPathsBySimpleName.TryGetValue(assemblyIdentity.Name, ref hashSet))
				{
					return null;
				}
				immutableArray = hashSet.ToImmutableArray<string>();
			}
			foreach (string text in immutableArray)
			{
				AssemblyIdentity orAddAssemblyIdentity = this.GetOrAddAssemblyIdentity(text);
				if (assemblyIdentity.Equals(orAddAssemblyIdentity))
				{
					return this.LoadFromPathUncheckedCore(text, orAddAssemblyIdentity);
				}
			}
			return null;
		}

		// Token: 0x04000419 RID: 1049
		private readonly object _guard = new object();

		// Token: 0x0400041A RID: 1050
		private readonly Dictionary<string, Assembly> _loadedAssembliesByPath = new Dictionary<string, Assembly>();

		// Token: 0x0400041B RID: 1051
		private readonly Dictionary<string, AssemblyIdentity> _loadedAssemblyIdentitiesByPath = new Dictionary<string, AssemblyIdentity>();

		// Token: 0x0400041C RID: 1052
		private readonly Dictionary<AssemblyIdentity, Assembly> _loadedAssembliesByIdentity = new Dictionary<AssemblyIdentity, Assembly>();

		// Token: 0x0400041D RID: 1053
		private readonly Dictionary<string, HashSet<string>> _knownAssemblyPathsBySimpleName = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
	}
}
