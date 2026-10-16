using System;

namespace HarmonyLib
{
	// Token: 0x0200005C RID: 92
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Parameter, AllowMultiple = true)]
	public class HarmonyArgument : Attribute
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000196 RID: 406 RVA: 0x0000AFED File Offset: 0x000091ED
		// (set) Token: 0x06000197 RID: 407 RVA: 0x0000AFF5 File Offset: 0x000091F5
		public string OriginalName { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000AFFE File Offset: 0x000091FE
		// (set) Token: 0x06000199 RID: 409 RVA: 0x0000B006 File Offset: 0x00009206
		public int Index { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0000B00F File Offset: 0x0000920F
		// (set) Token: 0x0600019B RID: 411 RVA: 0x0000B017 File Offset: 0x00009217
		public string NewName { get; private set; }

		// Token: 0x0600019C RID: 412 RVA: 0x0000B020 File Offset: 0x00009220
		public HarmonyArgument(string originalName)
			: this(originalName, null)
		{
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000B02A File Offset: 0x0000922A
		public HarmonyArgument(int index)
			: this(index, null)
		{
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000B034 File Offset: 0x00009234
		public HarmonyArgument(string originalName, string newName)
		{
			this.OriginalName = originalName;
			this.Index = -1;
			this.NewName = newName;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000B051 File Offset: 0x00009251
		public HarmonyArgument(int index, string name)
		{
			this.OriginalName = null;
			this.Index = index;
			this.NewName = name;
		}
	}
}
