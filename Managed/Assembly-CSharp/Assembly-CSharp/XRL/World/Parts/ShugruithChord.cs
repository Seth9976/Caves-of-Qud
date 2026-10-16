using System;
using System.Text;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000EE5 RID: 3813
	public class ShugruithChord : INephalChord
	{
		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06009885 RID: 39045 RVA: 0x003765CD File Offset: 0x003747CD
		public override string Source
		{
			get
			{
				return "Shugruith";
			}
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06009886 RID: 39046 RVA: 0x003765D4 File Offset: 0x003747D4
		public virtual int AVBonus
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06009887 RID: 39047 RVA: 0x003765D8 File Offset: 0x003747D8
		public virtual int DVBOnus
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x06009888 RID: 39048 RVA: 0x003765DC File Offset: 0x003747DC
		public override void Initialize()
		{
			base.StatShifter.SetStatShift("AV", this.AVBonus, false);
			base.StatShifter.SetStatShift("DV", this.DVBOnus, false);
			this.ParentObject.ApplyEffect(new Omniphase("ShugruithChord"), null);
		}

		// Token: 0x06009889 RID: 39049 RVA: 0x00376630 File Offset: 0x00374830
		public override void Remove()
		{
			base.StatShifter.RemoveStatShifts();
			this.ParentObject.RemoveEffect(delegate(Effect x)
			{
				Omniphase omniphase = x as Omniphase;
				return omniphase != null && omniphase.SourceKey == "ShugruithChord";
			});
		}

		// Token: 0x0600988A RID: 39050 RVA: 0x00376668 File Offset: 0x00374868
		public override void AppendRules(StringBuilder Postfix)
		{
			Postfix.Append("\n• ");
			Statistic.AppendStatAdjustDescription(Postfix, "AV", this.AVBonus, false, false);
			Postfix.Append("\n• ");
			Statistic.AppendStatAdjustDescription(Postfix, "DV", this.DVBOnus, false, false);
			Postfix.Append("\n• Omniphase");
		}
	}
}
