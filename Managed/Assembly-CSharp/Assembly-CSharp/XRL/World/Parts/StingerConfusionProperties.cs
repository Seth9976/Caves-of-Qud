using System;
using System.Text;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x0200119E RID: 4510
	[Serializable]
	public class StingerConfusionProperties : IStingerProperties
	{
		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x0600B369 RID: 45929 RVA: 0x004177D9 File Offset: 0x004159D9
		public override string SaveVs
		{
			get
			{
				return "Stinger Injected Poison Confusion";
			}
		}

		// Token: 0x0600B36A RID: 45930 RVA: 0x004177E0 File Offset: 0x004159E0
		public override Effect CreateEffect(GameObject Attacker, GameObject Defender, int Level)
		{
			return new Confused(this.GetDuration(Level).RollCached(), Level, Level + 2, "Confusion");
		}

		// Token: 0x0600B36B RID: 45931 RVA: 0x004177FC File Offset: 0x004159FC
		public override void AppendLevelText(StringBuilder SB, int Level)
		{
			SB.Append("Venom confuses opponents for {{rules|").Append(this.GetDuration(Level)).Append("}} rounds\n");
		}

		// Token: 0x0600B36C RID: 45932 RVA: 0x00417820 File Offset: 0x00415A20
		public override void VisualEffect(GameObject Attacker, GameObject Defender)
		{
			Defender.Splatter("&B.");
		}

		// Token: 0x0600B36D RID: 45933 RVA: 0x0041782D File Offset: 0x00415A2D
		public override string GetAdjective()
		{
			return "confusing";
		}

		// Token: 0x0600B36E RID: 45934 RVA: 0x00417834 File Offset: 0x00415A34
		public override string GetDuration(int Level)
		{
			if (Level >= 3)
			{
				return "2d3+" + Math.Min(14, Level * 2 / 3 + 2).ToString();
			}
			return "2d3+2";
		}
	}
}
