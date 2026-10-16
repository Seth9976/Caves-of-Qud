using System;
using System.Text;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x0200119D RID: 4509
	[Serializable]
	public class StingerPoisonProperties : IStingerProperties
	{
		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x0600B363 RID: 45923 RVA: 0x0041775A File Offset: 0x0041595A
		public override string SaveVs
		{
			get
			{
				return "Stinger Injected Poison Damaging";
			}
		}

		// Token: 0x0600B364 RID: 45924 RVA: 0x00417761 File Offset: 0x00415961
		public override Effect CreateEffect(GameObject Attacker, GameObject Defender, int Level)
		{
			return new StingerPoisoned(this.GetDuration(Level).RollCached(), this.GetIncrement(Level), Level, Attacker);
		}

		// Token: 0x0600B365 RID: 45925 RVA: 0x0041777D File Offset: 0x0041597D
		public override void AppendLevelText(StringBuilder SB, int Level)
		{
			SB.Append("Venom poisons opponents for {{rules|").Append(this.GetDuration(Level)).Append("}} rounds (damage increment {{rules|")
				.Append(this.GetIncrement(Level))
				.Append("}})\n");
		}

		// Token: 0x0600B366 RID: 45926 RVA: 0x004177B7 File Offset: 0x004159B7
		public override string GetDuration(int Level)
		{
			return "8-12";
		}

		// Token: 0x0600B367 RID: 45927 RVA: 0x004177BE File Offset: 0x004159BE
		public virtual string GetIncrement(int Level)
		{
			return Level.ToString() + "d2";
		}
	}
}
