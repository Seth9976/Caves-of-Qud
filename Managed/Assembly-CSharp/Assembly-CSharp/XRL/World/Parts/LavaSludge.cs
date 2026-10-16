using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E7E RID: 3710
	[Serializable]
	public class LavaSludge : IPart
	{
		// Token: 0x060095B6 RID: 38326 RVA: 0x003627E5 File Offset: 0x003609E5
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x060095B7 RID: 38327 RVA: 0x003627E8 File Offset: 0x003609E8
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CheckTemperature();
		}

		// Token: 0x060095B8 RID: 38328 RVA: 0x003627F0 File Offset: 0x003609F0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeDieEvent.ID;
		}

		// Token: 0x060095B9 RID: 38329 RVA: 0x00362808 File Offset: 0x00360A08
		public override bool HandleEvent(BeforeDieEvent E)
		{
			if (E.Reason != "You cooled into a block of shale.")
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell != null)
				{
					base.DidX("cool", "into a block of shale", null, null, null, null, this.ParentObject, false, false, null, null, false, false, false, false, false, null);
					this.ParentObject.Die(null, "phase change", "You cooled into a block of shale.", this.ParentObject.It + " @@cooled into a block of shale.", true, null, null, false, false, null, null, null);
					if (!currentCell.IsSolid())
					{
						currentCell.AddObject(GameObject.Create("Shale"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060095BA RID: 38330 RVA: 0x003628C5 File Offset: 0x00360AC5
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x060095BB RID: 38331 RVA: 0x003628C8 File Offset: 0x00360AC8
		public void CheckTemperature()
		{
			if (!this.ParentObject.IsAflame())
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell != null)
				{
					if (!currentCell.IsSolid())
					{
						currentCell.AddObject(GameObject.Create("Shale"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
					base.DidX("cool", "into a block of shale", null, null, null, null, this.ParentObject, false, false, null, null, false, false, false, false, false, null);
					this.ParentObject.Die(null, "phase change", "You cooled into a block of shale.", this.ParentObject.It + " @@cooled into a block of shale.", true, null, null, false, false, null, null, null);
				}
			}
		}

		// Token: 0x04003DEB RID: 15851
		public const string DEATH_REASON = "You cooled into a block of shale.";

		// Token: 0x04003DEC RID: 15852
		public const string THIRD_PERSON_DEATH_REASON_SUFFIX = " @@cooled into a block of shale.";
	}
}
