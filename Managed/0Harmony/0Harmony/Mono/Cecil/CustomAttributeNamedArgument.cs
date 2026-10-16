using System;

namespace Mono.Cecil
{
	// Token: 0x020001F8 RID: 504
	public struct CustomAttributeNamedArgument
	{
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x000264FB File Offset: 0x000246FB
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x00026503 File Offset: 0x00024703
		public CustomAttributeArgument Argument
		{
			get
			{
				return this.argument;
			}
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0002650B File Offset: 0x0002470B
		public CustomAttributeNamedArgument(string name, CustomAttributeArgument argument)
		{
			Mixin.CheckName(name);
			this.name = name;
			this.argument = argument;
		}

		// Token: 0x040002E5 RID: 741
		private readonly string name;

		// Token: 0x040002E6 RID: 742
		private readonly CustomAttributeArgument argument;
	}
}
