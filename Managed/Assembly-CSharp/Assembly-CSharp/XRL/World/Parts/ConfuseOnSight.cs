using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000E5B RID: 3675
	[Serializable]
	public class ConfuseOnSight : IPart
	{
		// Token: 0x060094B0 RID: 38064 RVA: 0x0035BD06 File Offset: 0x00359F06
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x060094B1 RID: 38065 RVA: 0x0035BD0C File Offset: 0x00359F0C
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.ParentObject.IsNowhere())
			{
				return;
			}
			string primaryFaction = this.ParentObject.GetPrimaryFaction(false);
			foreach (GameObject gameObject in this.ParentObject.CurrentZone.IterateObjects())
			{
				if (gameObject.IsCombatObject() && gameObject != this.ParentObject && !gameObject.HasEffect(typeof(Confused)) && !gameObject.IsMemberOfFaction(primaryFaction) && gameObject.HasLOSTo(this.ParentObject, true, false, false, true, null) && this.Chance.in100() && gameObject.FireEvent("ApplyAttackConfusion") && !gameObject.MakeSave("Willpower", this.Strength, null, null, this.NameForChecks, false, false, false, false, this.ParentObject))
				{
					gameObject.ApplyEffect(new Confused(this.Duration.RollCached(), this.Level, this.Level + 2, this.NameForChecks), null);
				}
			}
		}

		// Token: 0x04003D7D RID: 15741
		public int Chance = 100;

		// Token: 0x04003D7E RID: 15742
		public int Strength = 25;

		// Token: 0x04003D7F RID: 15743
		public string Duration = "10-15";

		// Token: 0x04003D80 RID: 15744
		public int Level = 10;

		// Token: 0x04003D81 RID: 15745
		public string NameForChecks = "Confusion";
	}
}
