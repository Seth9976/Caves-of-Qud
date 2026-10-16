using System;
using System.ComponentModel;

namespace AiUnity.NLog.Core.Common
{
	// Token: 0x020002BA RID: 698
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class TargetAttribute : DisplayNameAttribute
	{
		// Token: 0x06001865 RID: 6245 RVA: 0x0007EF70 File Offset: 0x0007D170
		public TargetAttribute(string name)
			: base(name)
		{
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001866 RID: 6246 RVA: 0x0007EF79 File Offset: 0x0007D179
		// (set) Token: 0x06001867 RID: 6247 RVA: 0x0007EF81 File Offset: 0x0007D181
		public bool IsWrapper { get; set; }

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06001868 RID: 6248 RVA: 0x0007EF8A File Offset: 0x0007D18A
		// (set) Token: 0x06001869 RID: 6249 RVA: 0x0007EF92 File Offset: 0x0007D192
		public bool IsCompound { get; set; }
	}
}
