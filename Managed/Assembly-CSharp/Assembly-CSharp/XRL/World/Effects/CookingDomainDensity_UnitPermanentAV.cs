using System;
using Qud.API;

namespace XRL.World.Effects
{
	// Token: 0x02000C92 RID: 3218
	[Serializable]
	public class CookingDomainDensity_UnitPermanentAV : ProceduralCookingEffectUnit
	{
		// Token: 0x06008188 RID: 33160 RVA: 0x002F6645 File Offset: 0x002F4845
		public override string GetDescription()
		{
			if (this.Collapse)
			{
				return "{{R|CRITICAL GRAVITATIONAL COLLAPSE}}";
			}
			return this.Bonus.Signed(false) + " AV permanently";
		}

		// Token: 0x06008189 RID: 33161 RVA: 0x002F666B File Offset: 0x002F486B
		public override string GetTemplatedDescription()
		{
			return "+1 AV permanently. Small chance of gravitational collapse.";
		}

		// Token: 0x0600818A RID: 33162 RVA: 0x002F6672 File Offset: 0x002F4872
		public override void Init(GameObject target)
		{
			this.Collapse = 10.in100();
			this.Bonus = 1;
		}

		// Token: 0x0600818B RID: 33163 RVA: 0x002F6688 File Offset: 0x002F4888
		public override void Apply(GameObject Object, Effect parent)
		{
			if (this.Collapse)
			{
				Object.Explode(15000, null, "10d10+250", 1f, true, false, false, 0, null);
				return;
			}
			Object.GetStat("AV").BaseValue += this.Bonus;
			if (Object.IsPlayer())
			{
				JournalAPI.AddAccomplishment("You became denser.", string.Concat(new string[]
				{
					"On the ",
					Calendar.GetDay(),
					" of ",
					Calendar.GetMonth(),
					", =name= uncorked the cosmic wine and imbibed the forbidden starjuice. O, the mass within!"
				}), "Somewhere in " + The.Player.CurrentZone.GetTerrainDisplayName(true) + ", =name= had a dream that <entity.subjectPronoun> imbibed a dram of starjuice. <spice.commonPhrases.fromThenOn.!random.capitalize>, <entity.subjectPronoun> always kept some stars in a bottle on <entity.possessivePronoun> person.", null, "general", MuralCategory.BodyExperienceGood, MuralWeight.Medium, null, -1L, true);
				Achievement.COOKED_FLUX.Unlock();
			}
		}

		// Token: 0x0600818C RID: 33164 RVA: 0x002F6753 File Offset: 0x002F4953
		public override void Remove(GameObject Object, Effect parent)
		{
		}

		// Token: 0x0400391F RID: 14623
		public int Bonus = 1;

		// Token: 0x04003920 RID: 14624
		public bool Collapse;
	}
}
