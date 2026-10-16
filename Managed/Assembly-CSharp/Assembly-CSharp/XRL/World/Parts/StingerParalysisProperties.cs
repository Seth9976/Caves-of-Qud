using System;
using System.Text;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x0200119F RID: 4511
	[Serializable]
	public class StingerParalysisProperties : IStingerProperties
	{
		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x0600B370 RID: 45936 RVA: 0x00417873 File Offset: 0x00415A73
		public override string SaveVs
		{
			get
			{
				return "Stinger Injected Poison Paralysis";
			}
		}

		// Token: 0x0600B371 RID: 45937 RVA: 0x0041787A File Offset: 0x00415A7A
		public override Effect CreateEffect(GameObject Attacker, GameObject Defender, int Level)
		{
			return new Paralyzed(this.GetDuration(Level).RollCached(), -1);
		}

		// Token: 0x0600B372 RID: 45938 RVA: 0x0041788E File Offset: 0x00415A8E
		public override void AppendLevelText(StringBuilder SB, int Level)
		{
			SB.Append("Venom paralyzes opponents for {{rules|").Append(this.GetDuration(Level)).Append("}} rounds\n");
		}

		// Token: 0x0600B373 RID: 45939 RVA: 0x004178B2 File Offset: 0x00415AB2
		public override void VisualEffect(GameObject Attacker, GameObject Defender)
		{
			Defender.Splatter("&m.");
		}

		// Token: 0x0600B374 RID: 45940 RVA: 0x004178BF File Offset: 0x00415ABF
		public override string GetAdjective()
		{
			return "paralyzing";
		}

		// Token: 0x0600B375 RID: 45941 RVA: 0x004178C8 File Offset: 0x00415AC8
		public override string GetDuration(int Level)
		{
			if (Level >= 3)
			{
				return "1d3+" + Math.Min(7, Level / 3 + 1).ToString();
			}
			return "1d3+1";
		}
	}
}
