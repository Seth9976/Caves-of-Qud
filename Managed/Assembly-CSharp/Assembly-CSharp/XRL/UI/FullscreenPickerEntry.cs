using System;
using System.Collections.Generic;

namespace XRL.UI
{
	// Token: 0x0200147F RID: 5247
	public class FullscreenPickerEntry
	{
		// Token: 0x0600CE93 RID: 52883 RVA: 0x004B2354 File Offset: 0x004B0554
		public FullscreenPickerEntry(string title, List<string> Lines)
		{
			this.Title = title;
			this.Lines.AddRange(Lines);
		}

		// Token: 0x04005027 RID: 20519
		public string Title = "";

		// Token: 0x04005028 RID: 20520
		public List<string> Lines = new List<string>();

		// Token: 0x04005029 RID: 20521
		public List<string> Details = new List<string>();
	}
}
