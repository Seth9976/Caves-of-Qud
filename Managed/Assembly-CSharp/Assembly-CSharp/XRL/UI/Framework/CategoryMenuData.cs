using System;
using System.Collections.Generic;

namespace XRL.UI.Framework
{
	/// <summary>
	///   Data Structure for various UI elements to use.
	/// </summary>
	// Token: 0x02001506 RID: 5382
	[Serializable]
	public class CategoryMenuData : FrameworkDataElement
	{
		// Token: 0x04005317 RID: 21271
		public string Title;

		// Token: 0x04005318 RID: 21272
		public List<PrefixMenuOption> menuOptions;
	}
}
