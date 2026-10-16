using System;

namespace Mono.Cecil
{
	// Token: 0x02000226 RID: 550
	internal sealed class LinkedResource : Resource
	{
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x00029765 File Offset: 0x00027965
		public byte[] Hash
		{
			get
			{
				return this.hash;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x0002976D File Offset: 0x0002796D
		// (set) Token: 0x06000BF0 RID: 3056 RVA: 0x00029775 File Offset: 0x00027975
		public string File
		{
			get
			{
				return this.file;
			}
			set
			{
				this.file = value;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x00018318 File Offset: 0x00016518
		public override ResourceType ResourceType
		{
			get
			{
				return ResourceType.Linked;
			}
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0001AE86 File Offset: 0x00019086
		public LinkedResource(string name, ManifestResourceAttributes flags)
			: base(name, flags)
		{
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0002977E File Offset: 0x0002797E
		public LinkedResource(string name, ManifestResourceAttributes flags, string file)
			: base(name, flags)
		{
			this.file = file;
		}

		// Token: 0x04000355 RID: 853
		internal byte[] hash;

		// Token: 0x04000356 RID: 854
		private string file;
	}
}
