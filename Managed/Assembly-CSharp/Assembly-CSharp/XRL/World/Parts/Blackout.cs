using System;

namespace XRL.World.Parts
{
	// Token: 0x0200124D RID: 4685
	[Serializable]
	public class Blackout : IPart
	{
		// Token: 0x0600BA2C RID: 47660 RVA: 0x0043FB9C File Offset: 0x0043DD9C
		public override bool SameAs(IPart p)
		{
			Blackout blackout = p as Blackout;
			return blackout.OnlyIfCharged == this.OnlyIfCharged && blackout.Lit == this.Lit && blackout.ConfusionSuspends == this.ConfusionSuspends && blackout.Radius == this.Radius && base.SameAs(p);
		}

		// Token: 0x0600BA2D RID: 47661 RVA: 0x0043FBF7 File Offset: 0x0043DDF7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeRenderEvent.ID;
		}

		// Token: 0x0600BA2E RID: 47662 RVA: 0x0043FC10 File Offset: 0x0043DE10
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			if (E.Pass == 1)
			{
				E.AfterHandlers.Add(this);
			}
			else if (E.Pass == 2)
			{
				Cell currentCell = this.ParentObject.GetCurrentCell();
				if (currentCell != null && this.IsActive())
				{
					if (this.Radius == 999)
					{
						currentCell.ParentZone.BlackoutAll();
					}
					else
					{
						currentCell.ParentZone.RemoveLight(currentCell.X, currentCell.Y, this.Radius, LightLevel.Blackout, LightLevel.Interpolight);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BA2F RID: 47663 RVA: 0x0043FC98 File Offset: 0x0043DE98
		public bool IsActive()
		{
			return this.Lit && (!this.OnlyIfCharged || this.ParentObject.TestCharge(1, false, 0L, true, true)) && (!this.ConfusionSuspends || !this.ParentObject.IsConfused);
		}

		// Token: 0x04004AD4 RID: 19156
		public bool OnlyIfCharged;

		// Token: 0x04004AD5 RID: 19157
		public bool Lit = true;

		// Token: 0x04004AD6 RID: 19158
		public bool ConfusionSuspends = true;

		// Token: 0x04004AD7 RID: 19159
		public int Radius = 5;
	}
}
