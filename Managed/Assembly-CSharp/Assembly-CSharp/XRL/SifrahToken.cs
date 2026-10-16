using System;
using XRL.World;

namespace XRL
{
	/// This class is not used in the base game.
	// Token: 0x02000516 RID: 1302
	[Serializable]
	public class SifrahToken : SifrahRenderable
	{
		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x060037E6 RID: 14310 RVA: 0x00111533 File Offset: 0x0010F733
		public bool Eliminated
		{
			get
			{
				return this.EliminatedAt != -1;
			}
		}

		// Token: 0x060037E7 RID: 14311 RVA: 0x00111541 File Offset: 0x0010F741
		public virtual string GetDescription(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			return this.Description;
		}

		// Token: 0x060037E8 RID: 14312 RVA: 0x00111549 File Offset: 0x0010F749
		public virtual bool GetDisabled(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			return this.DisabledThisTurn;
		}

		// Token: 0x060037E9 RID: 14313 RVA: 0x00111551 File Offset: 0x0010F751
		public virtual bool CheckTokenUse(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			return true;
		}

		// Token: 0x060037EA RID: 14314 RVA: 0x00111554 File Offset: 0x0010F754
		public virtual void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
		}

		// Token: 0x060037EB RID: 14315 RVA: 0x00111556 File Offset: 0x0010F756
		public virtual int GetPowerup(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			return 0;
		}

		// Token: 0x04001C05 RID: 7173
		public string Description;

		// Token: 0x04001C06 RID: 7174
		public int EliminatedAt = -1;

		// Token: 0x04001C07 RID: 7175
		public bool UsabilityCheckedThisTurn;

		// Token: 0x04001C08 RID: 7176
		public bool DisabledThisTurn;
	}
}
