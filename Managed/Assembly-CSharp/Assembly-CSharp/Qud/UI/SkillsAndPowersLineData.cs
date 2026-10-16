using System;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World;

namespace Qud.UI
{
	// Token: 0x02000476 RID: 1142
	public class SkillsAndPowersLineData : PooledFrameworkDataElement<SkillsAndPowersLineData>
	{
		// Token: 0x06002B81 RID: 11137 RVA: 0x000E8439 File Offset: 0x000E6639
		public SkillsAndPowersLineData set(SPNode entry, SkillsAndPowersStatusScreen screen, GameObject go)
		{
			this.entry = entry;
			this.screen = screen;
			this.go = go;
			return this;
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x000E8451 File Offset: 0x000E6651
		public override void free()
		{
			this.go = null;
			this.entry = null;
			this.screen = null;
			base.free();
		}

		// Token: 0x040017A6 RID: 6054
		public GameObject go;

		// Token: 0x040017A7 RID: 6055
		public SPNode entry;

		// Token: 0x040017A8 RID: 6056
		public SkillsAndPowersStatusScreen screen;
	}
}
