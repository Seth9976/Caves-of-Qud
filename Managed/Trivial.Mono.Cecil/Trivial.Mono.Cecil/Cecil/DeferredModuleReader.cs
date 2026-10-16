using System;
using Trivial.Mono.Cecil.PE;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000029 RID: 41
	internal sealed class DeferredModuleReader : ModuleReader
	{
		// Token: 0x0600012C RID: 300 RVA: 0x00004AA4 File Offset: 0x00002CA4
		public DeferredModuleReader(Image image)
			: base(image, ReadingMode.Deferred)
		{
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004AAE File Offset: 0x00002CAE
		protected override void ReadModule()
		{
			this.module.Read<ModuleDefinition, ModuleDefinition>(this.module, delegate(ModuleDefinition module, MetadataReader reader)
			{
				base.ReadModuleManifest(reader);
				return module;
			});
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00002921 File Offset: 0x00000B21
		public override void ReadSymbols(ModuleDefinition module)
		{
		}
	}
}
