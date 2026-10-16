using System;
using Mono.Cecil.PE;

namespace Mono.Cecil
{
	// Token: 0x020001BB RID: 443
	internal sealed class DeferredModuleReader : ModuleReader
	{
		// Token: 0x06000813 RID: 2067 RVA: 0x0001BC20 File Offset: 0x00019E20
		public DeferredModuleReader(Image image)
			: base(image, ReadingMode.Deferred)
		{
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001BC2A File Offset: 0x00019E2A
		protected override void ReadModule()
		{
			this.module.Read<ModuleDefinition>(this.module, delegate(ModuleDefinition _, MetadataReader reader)
			{
				base.ReadModuleManifest(reader);
			});
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00018659 File Offset: 0x00016859
		public override void ReadSymbols(ModuleDefinition module)
		{
		}
	}
}
