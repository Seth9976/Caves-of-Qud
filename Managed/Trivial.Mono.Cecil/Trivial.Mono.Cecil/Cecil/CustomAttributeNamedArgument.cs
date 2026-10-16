using System;

namespace Trivial.Mono.Cecil
{
	// Token: 0x02000021 RID: 33
	public struct CustomAttributeNamedArgument
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000113 RID: 275 RVA: 0x000047A2 File Offset: 0x000029A2
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000114 RID: 276 RVA: 0x000047AA File Offset: 0x000029AA
		public CustomAttributeArgument Argument
		{
			get
			{
				return this.argument;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000047B2 File Offset: 0x000029B2
		public CustomAttributeNamedArgument(string name, CustomAttributeArgument argument)
		{
			Mixin.CheckName(name);
			this.name = name;
			this.argument = argument;
		}

		// Token: 0x0400003D RID: 61
		private readonly string name;

		// Token: 0x0400003E RID: 62
		private readonly CustomAttributeArgument argument;
	}
}
