using System;
using UnityEngine;

namespace XRL.UI.Framework
{
	/// <summary>
	///   Data Structure for various UI elements to use.
	/// </summary>
	// Token: 0x02001520 RID: 5408
	[Serializable]
	public class SummaryBlockData : FrameworkDataElement
	{
		// Token: 0x04005372 RID: 21362
		public string Title;

		// Token: 0x04005373 RID: 21363
		public string IconPath;

		// Token: 0x04005374 RID: 21364
		public Color IconForegroundColor;

		// Token: 0x04005375 RID: 21365
		public Color IconDetailColor;

		// Token: 0x04005376 RID: 21366
		public int SortOrder;
	}
}
