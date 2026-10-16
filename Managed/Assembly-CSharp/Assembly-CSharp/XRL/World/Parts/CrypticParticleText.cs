using System;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x02000E61 RID: 3681
	[Serializable]
	public class CrypticParticleText : IPart
	{
		// Token: 0x17000A88 RID: 2696
		// (set) Token: 0x060094E4 RID: 38116 RVA: 0x0035DA59 File Offset: 0x0035BC59
		public string InitialTimer
		{
			set
			{
				this.Timer = ((value != null) ? value.RollCached() : this.Timer);
			}
		}

		// Token: 0x060094E5 RID: 38117 RVA: 0x0035DA72 File Offset: 0x0035BC72
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x060094E6 RID: 38118 RVA: 0x0035DA78 File Offset: 0x0035BC78
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.Timer > 0)
			{
				this.Timer = Math.Max(this.Timer - Amount, 0);
				if (this.Timer == 0)
				{
					this.Preach();
					return;
				}
			}
			else if (this.Chance.in100())
			{
				this.Preach();
			}
		}

		// Token: 0x060094E7 RID: 38119 RVA: 0x0035DAC4 File Offset: 0x0035BCC4
		public void Preach()
		{
			string text = TextFilters.GenerateCrypticWord();
			this.ParentObject.ParticleText("{{c|'" + text + "'}}", true);
		}

		// Token: 0x04003DAD RID: 15789
		public int Chance = 8;

		// Token: 0x04003DAE RID: 15790
		public int Timer;
	}
}
