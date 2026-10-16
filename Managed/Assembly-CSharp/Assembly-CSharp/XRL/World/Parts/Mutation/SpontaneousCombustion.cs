using System;
using XRL.Rules;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013DC RID: 5084
	[Serializable]
	public class SpontaneousCombustion : BaseMutation
	{
		// Token: 0x0600C46F RID: 50287 RVA: 0x00478420 File Offset: 0x00476620
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C470 RID: 50288 RVA: 0x00478423 File Offset: 0x00476623
		public override string GetDescription()
		{
			return "You spontaneously erupt into flames.\n\nSmall chance each round you're in combat that you spontaneously erupt into flames.";
		}

		// Token: 0x0600C471 RID: 50289 RVA: 0x0047842A File Offset: 0x0047662A
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C472 RID: 50290 RVA: 0x00478431 File Offset: 0x00476631
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600C473 RID: 50291 RVA: 0x00478434 File Offset: 0x00476634
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (!this.ParentObject.OnWorldMap() && 2.in1000() && this.ParentObject.AreHostilesNearby())
			{
				this.ParentObject.TemperatureChange(400 + Stat.Random(1, 300), this.ParentObject, false, false, false, false, 0, null, null);
				if (this.ParentObject.IsAflame())
				{
					this.ParentObject.StopMoving();
					base.DidX("erupt", "into flames", "!", null, null, null, this.ParentObject, false, false, null, null, false, false, false, false, false, null);
				}
			}
			base.TurnTick(TimeTick, Amount);
		}

		// Token: 0x0600C474 RID: 50292 RVA: 0x004784EB File Offset: 0x004766EB
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}
	}
}
