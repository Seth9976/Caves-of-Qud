using System;
using System.Collections.Generic;

namespace Mono.Cecil
{
	// Token: 0x020001FC RID: 508
	internal class DefaultAssemblyResolver : BaseAssemblyResolver
	{
		// Token: 0x06000A58 RID: 2648 RVA: 0x000267D2 File Offset: 0x000249D2
		public DefaultAssemblyResolver()
		{
			this.cache = new Dictionary<string, AssemblyDefinition>(StringComparer.Ordinal);
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x000267EC File Offset: 0x000249EC
		public override AssemblyDefinition Resolve(AssemblyNameReference name)
		{
			Mixin.CheckName(name);
			AssemblyDefinition assemblyDefinition;
			if (this.cache.TryGetValue(name.FullName, out assemblyDefinition))
			{
				return assemblyDefinition;
			}
			assemblyDefinition = base.Resolve(name);
			this.cache[name.FullName] = assemblyDefinition;
			return assemblyDefinition;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00026834 File Offset: 0x00024A34
		protected void RegisterAssembly(AssemblyDefinition assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			string fullName = assembly.Name.FullName;
			if (this.cache.ContainsKey(fullName))
			{
				return;
			}
			this.cache[fullName] = assembly;
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00026878 File Offset: 0x00024A78
		protected override void Dispose(bool disposing)
		{
			foreach (AssemblyDefinition assemblyDefinition in this.cache.Values)
			{
				assemblyDefinition.Dispose();
			}
			this.cache.Clear();
			base.Dispose(disposing);
		}

		// Token: 0x040002F1 RID: 753
		private readonly IDictionary<string, AssemblyDefinition> cache;
	}
}
