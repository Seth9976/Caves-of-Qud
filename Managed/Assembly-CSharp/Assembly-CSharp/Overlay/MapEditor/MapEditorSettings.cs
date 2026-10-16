using System;
using XRL.Collections;

namespace Overlay.MapEditor
{
	// Token: 0x020003DF RID: 991
	public class MapEditorSettings
	{
		// Token: 0x0400127B RID: 4731
		public string DefaultPath;

		// Token: 0x0400127C RID: 4732
		public Rack<string> RecentFiles = new Rack<string>();
	}
}
