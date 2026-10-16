using System;
using System.Collections.Generic;

namespace XRL.UI.Framework
{
	// Token: 0x02001501 RID: 5377
	public class StartingLocationData : FrameworkDataElement, IFrameworkDataHotkey
	{
		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x0600D1D4 RID: 53716 RVA: 0x004DED43 File Offset: 0x004DCF43
		// (set) Token: 0x0600D1D5 RID: 53717 RVA: 0x004DED4B File Offset: 0x004DCF4B
		public string Hotkey { get; set; }

		// Token: 0x040052FB RID: 21243
		public string Name;

		// Token: 0x040052FC RID: 21244
		public string Location;

		// Token: 0x040052FD RID: 21245
		public string Set;

		// Token: 0x040052FE RID: 21246
		public string ExcludeFromDaily;

		// Token: 0x04005300 RID: 21248
		public List<StartingLocationSkill> skills = new List<StartingLocationSkill>();

		// Token: 0x04005301 RID: 21249
		public List<StartingLocationItem> items = new List<StartingLocationItem>();

		// Token: 0x04005302 RID: 21250
		public List<StartingLocationReputation> reputations = new List<StartingLocationReputation>();

		// Token: 0x04005303 RID: 21251
		public Dictionary<string, StartingLocationGridElement> grid = new Dictionary<string, StartingLocationGridElement>();

		// Token: 0x04005304 RID: 21252
		public Dictionary<string, string> stringGameStates = new Dictionary<string, string>();
	}
}
