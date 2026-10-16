using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000E3 RID: 227
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter, AllowMultiple = true)]
	public sealed class MacroAttribute : Attribute
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x000086FB File Offset: 0x000068FB
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x00008703 File Offset: 0x00006903
		[CanBeNull]
		public string Expression { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x0000870C File Offset: 0x0000690C
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x00008714 File Offset: 0x00006914
		public int Editable { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0000871D File Offset: 0x0000691D
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x00008725 File Offset: 0x00006925
		[CanBeNull]
		public string Target { get; set; }
	}
}
