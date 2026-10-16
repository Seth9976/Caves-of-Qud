using System;
using System.Collections.Generic;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000028 RID: 40
	public class DefaultAssemblyResolver : BaseAssemblyResolver
	{
		// Token: 0x06000128 RID: 296 RVA: 0x0000499C File Offset: 0x00002B9C
		public DefaultAssemblyResolver()
		{
			this.cache = new Dictionary<string, AssemblyDefinition>(StringComparer.Ordinal);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000049B4 File Offset: 0x00002BB4
		public override AssemblyDefinition Resolve(AssemblyNameReference name)
		{
			Mixin.CheckName(name);
			AssemblyDefinition assemblyDefinition;
			if (this.cache.TryGetValue(name.FullName, ref assemblyDefinition))
			{
				return assemblyDefinition;
			}
			assemblyDefinition = base.Resolve(name);
			this.cache[name.FullName] = assemblyDefinition;
			return assemblyDefinition;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000049FC File Offset: 0x00002BFC
		protected void RegisterAssembly(AssemblyDefinition assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			string fullName = assembly.Name.FullName;
			if (!this.cache.ContainsKey(fullName))
			{
				this.cache[fullName] = assembly;
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00004A40 File Offset: 0x00002C40
		protected override void Dispose(bool disposing)
		{
			foreach (AssemblyDefinition assemblyDefinition in this.cache.Values)
			{
				assemblyDefinition.Dispose();
			}
			this.cache.Clear();
			base.Dispose(disposing);
		}

		// Token: 0x0400004B RID: 75
		private readonly IDictionary<string, AssemblyDefinition> cache;
	}
}
