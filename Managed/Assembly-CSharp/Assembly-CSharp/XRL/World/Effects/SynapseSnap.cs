using System;
using XRL.Core;

namespace XRL.World.Effects
{
	// Token: 0x02000D9B RID: 3483
	[Serializable]
	public class SynapseSnap : Effect
	{
		// Token: 0x06008AEF RID: 35567 RVA: 0x00317F87 File Offset: 0x00316187
		public SynapseSnap()
		{
			this.DisplayName = "synapse snap";
		}

		// Token: 0x06008AF0 RID: 35568 RVA: 0x00317F9A File Offset: 0x0031619A
		public SynapseSnap(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06008AF1 RID: 35569 RVA: 0x00317FA9 File Offset: 0x003161A9
		public override int GetEffectType()
		{
			return 67108866;
		}

		// Token: 0x06008AF2 RID: 35570 RVA: 0x00317FB0 File Offset: 0x003161B0
		public override string GetStateDescription()
		{
			return "synaptically snappy";
		}

		// Token: 0x06008AF3 RID: 35571 RVA: 0x00317FB7 File Offset: 0x003161B7
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008AF4 RID: 35572 RVA: 0x00317FBA File Offset: 0x003161BA
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008AF5 RID: 35573 RVA: 0x00317FBD File Offset: 0x003161BD
		public override bool Apply(GameObject Object)
		{
			Object.ParticleText("*synapse snap*", 'W', false, 1.5f, -8f);
			this.ApplyChanges();
			return true;
		}

		// Token: 0x06008AF6 RID: 35574 RVA: 0x00317FDE File Offset: 0x003161DE
		public override void Remove(GameObject Object)
		{
			Object.ParticleText("*synapse snap wore off*", 'r', false, 1.5f, -8f);
			this.UnapplyChanges();
		}

		// Token: 0x06008AF7 RID: 35575 RVA: 0x00317FFE File Offset: 0x003161FE
		private void ApplyChanges()
		{
			base.StatShifter.SetStatShift("Agility", 4, false);
			base.StatShifter.SetStatShift("Intelligence", 4, false);
		}

		// Token: 0x06008AF8 RID: 35576 RVA: 0x00318026 File Offset: 0x00316226
		private void UnapplyChanges()
		{
			base.StatShifter.RemoveStatShifts();
		}

		// Token: 0x06008AF9 RID: 35577 RVA: 0x00318033 File Offset: 0x00316233
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("AfterDeepCopyWithoutEffects");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008AFA RID: 35578 RVA: 0x00318053 File Offset: 0x00316253
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeDeepCopyWithoutEffects")
			{
				this.UnapplyChanges();
			}
			else if (E.ID == "AfterDeepCopyWithoutEffects")
			{
				this.ApplyChanges();
			}
			return base.FireEvent(E);
		}

		// Token: 0x06008AFB RID: 35579 RVA: 0x00318090 File Offset: 0x00316290
		public override bool Render(RenderEvent E)
		{
			int num = XRLCore.CurrentFrame % 60;
			if (num > 25 && num < 35)
			{
				E.Tile = null;
				E.RenderString = "\u0018";
				E.ColorString = "&W";
			}
			return true;
		}
	}
}
