using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000206 RID: 518
	public class DropdownMenuSeparator : DropdownMenuItem
	{
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x0004684A File Offset: 0x00044A4A
		public string subMenuPath { get; }

		// Token: 0x06000FDC RID: 4060 RVA: 0x00046852 File Offset: 0x00044A52
		public DropdownMenuSeparator(string subMenuPath)
		{
			this.subMenuPath = subMenuPath;
		}
	}
}
