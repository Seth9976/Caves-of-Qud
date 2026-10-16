using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000E4E RID: 3662
	[Serializable]
	public class BloodRingWeakness : IPart
	{
		// Token: 0x0600945F RID: 37983 RVA: 0x00356726 File Offset: 0x00354926
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeforeBeginTakeActionEvent>.ID;
		}

		// Token: 0x06009460 RID: 37984 RVA: 0x0035673C File Offset: 0x0035493C
		public override bool HandleEvent(BeforeBeginTakeActionEvent E)
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell != null && this.ParentObject.Brain != null && !this.ParentObject.HasEffect<Terrified>())
			{
				foreach (GameObject gameObject in currentCell.ParentZone.FastSquareVisibility(currentCell.X, currentCell.Y, 5, "Body", this.ParentObject, false, false, null))
				{
					if (gameObject.IsPlayer() && this.ParentObject.IsHostileTowards(gameObject) && gameObject.HasEquippedItem("Blood-stained neck-ring"))
					{
						this.ParentObject.ApplyEffect(new Terrified(2, gameObject, false, true), null);
						break;
					}
				}
			}
			return base.HandleEvent(E);
		}
	}
}
