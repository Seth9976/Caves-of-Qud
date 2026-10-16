using System;
using System.Text;
using UnityEngine;

namespace XRL.World.Parts
{
	// Token: 0x02000EDE RID: 3806
	public class BethsaidaChord : INephalChord
	{
		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06009849 RID: 38985 RVA: 0x00375262 File Offset: 0x00373462
		public override string Source
		{
			get
			{
				return "Bethsaida";
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x0600984A RID: 38986 RVA: 0x00375269 File Offset: 0x00373469
		public virtual float HPMult
		{
			get
			{
				return 0.5f;
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x0600984B RID: 38987 RVA: 0x00375270 File Offset: 0x00373470
		public virtual int HPBonus
		{
			get
			{
				return Mathf.RoundToInt((float)this.ParentObject.GetStat("Hitpoints").BaseValue * this.HPMult);
			}
		}

		// Token: 0x0600984C RID: 38988 RVA: 0x00375294 File Offset: 0x00373494
		public override void Initialize()
		{
			base.StatShifter.SetStatShift("Hitpoints", this.HPBonus, false);
		}

		// Token: 0x0600984D RID: 38989 RVA: 0x003752AE File Offset: 0x003734AE
		public override void Remove()
		{
			base.StatShifter.RemoveStatShifts();
		}

		// Token: 0x0600984E RID: 38990 RVA: 0x003752BC File Offset: 0x003734BC
		public override void AppendRules(StringBuilder Postfix)
		{
			Postfix.Append("\n• ");
			if (this.HPMult > 0f)
			{
				Postfix.Append('+');
			}
			Postfix.Append(Mathf.RoundToInt(this.HPMult * 100f)).Append("% ").Append(Statistic.GetStatDisplayName("Hitpoints"));
		}
	}
}
