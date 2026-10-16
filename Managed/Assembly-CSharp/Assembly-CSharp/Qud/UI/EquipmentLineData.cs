using System;
using XRL.UI.Framework;
using XRL.World.Anatomy;

namespace Qud.UI
{
	// Token: 0x02000467 RID: 1127
	public class EquipmentLineData : PooledFrameworkDataElement<EquipmentLineData>
	{
		// Token: 0x06002AB9 RID: 10937 RVA: 0x000E1B88 File Offset: 0x000DFD88
		public EquipmentLineData set(bool showCybernetics, BodyPart bodyPart, InventoryAndEquipmentStatusScreen screen, HotkeySpread spread)
		{
			this.showCybernetics = showCybernetics;
			this.bodyPart = bodyPart;
			this.screen = screen;
			this.spread = spread;
			return this;
		}

		// Token: 0x06002ABA RID: 10938 RVA: 0x000E1BA8 File Offset: 0x000DFDA8
		public override void free()
		{
			this.screen = null;
			this.bodyPart = null;
			this.showCybernetics = false;
			this.line = null;
			this.spread = null;
			base.free();
		}

		// Token: 0x040016B7 RID: 5815
		public bool showCybernetics;

		// Token: 0x040016B8 RID: 5816
		public BodyPart bodyPart;

		// Token: 0x040016B9 RID: 5817
		public InventoryAndEquipmentStatusScreen screen;

		// Token: 0x040016BA RID: 5818
		public EquipmentLine line;

		// Token: 0x040016BB RID: 5819
		public HotkeySpread spread;
	}
}
