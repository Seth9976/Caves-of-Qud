using System;
using XRL.Core;

namespace XRL.World.Effects
{
	// Token: 0x02000D1A RID: 3354
	[Serializable]
	public class CorpseTethered : Effect
	{
		// Token: 0x06008406 RID: 33798 RVA: 0x002FBE07 File Offset: 0x002FA007
		public CorpseTethered()
		{
			this.DisplayName = "{{Y|tomb-tethered}}";
			this.Duration = 1;
		}

		// Token: 0x06008407 RID: 33799 RVA: 0x002FBE21 File Offset: 0x002FA021
		public override int GetEffectType()
		{
			return 64;
		}

		// Token: 0x06008408 RID: 33800 RVA: 0x002FBE25 File Offset: 0x002FA025
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008409 RID: 33801 RVA: 0x002FBE28 File Offset: 0x002FA028
		public void CheckCell()
		{
			if (base.Object == null)
			{
				this.Duration = 0;
				return;
			}
			if (!base.Object.HasMarkOfDeath(null))
			{
				this.Duration = 0;
				return;
			}
			if (base.Object.GetCurrentCell() == null)
			{
				this.Duration = 0;
				return;
			}
			if (!base.Object.GetCurrentCell().HasObject("AnchorRoomTile"))
			{
				this.Duration = 0;
			}
		}

		// Token: 0x0600840A RID: 33802 RVA: 0x002FBE8E File Offset: 0x002FA08E
		public override string GetDescription()
		{
			this.CheckCell();
			if (this.Duration <= 0)
			{
				return null;
			}
			return "{{Y|tomb-tethered}}";
		}

		// Token: 0x0600840B RID: 33803 RVA: 0x002FBEA6 File Offset: 0x002FA0A6
		public override bool SuppressInLookDisplay()
		{
			this.CheckCell();
			return this.Duration <= 0;
		}

		// Token: 0x0600840C RID: 33804 RVA: 0x002FBEBA File Offset: 0x002FA0BA
		public override string GetDetails()
		{
			return "Currently occupying a tile safe from the tolling teleportation of the Bell of Rest.";
		}

		// Token: 0x0600840D RID: 33805 RVA: 0x002FBEC1 File Offset: 0x002FA0C1
		public override bool Apply(GameObject Object)
		{
			return true;
		}

		// Token: 0x0600840E RID: 33806 RVA: 0x002FBEC4 File Offset: 0x002FA0C4
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 5 && num < 15)
				{
					E.Tile = null;
					E.RenderString = "\u0019";
					E.ColorString = "&Y^k";
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600840F RID: 33807 RVA: 0x002FBF0C File Offset: 0x002FA10C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == SingletonEvent<EndTurnEvent>.ID;
		}

		// Token: 0x06008410 RID: 33808 RVA: 0x002FBF2A File Offset: 0x002FA12A
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.CheckCell();
			return base.HandleEvent(E);
		}

		// Token: 0x06008411 RID: 33809 RVA: 0x002FBF39 File Offset: 0x002FA139
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.Reference)
			{
				this.CheckCell();
				if (this.Duration > 0)
				{
					E.AddTag("{{y|[{{Y|tomb-tethered}}]}}", 0);
				}
			}
			return base.HandleEvent(E);
		}
	}
}
