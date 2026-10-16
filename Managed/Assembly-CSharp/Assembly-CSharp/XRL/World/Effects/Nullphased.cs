using System;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000D69 RID: 3433
	[Serializable]
	public class Nullphased : Effect
	{
		// Token: 0x060087DD RID: 34781 RVA: 0x0030B74C File Offset: 0x0030994C
		public Nullphased()
		{
			this.DisplayName = "{{g|nullphased}}";
			this.FrameOffset = Stat.Random(1, 60);
		}

		// Token: 0x060087DE RID: 34782 RVA: 0x0030B76D File Offset: 0x0030996D
		public Nullphased(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x060087DF RID: 34783 RVA: 0x0030B77C File Offset: 0x0030997C
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x060087E0 RID: 34784 RVA: 0x0030B77F File Offset: 0x0030997F
		public override int GetEffectType()
		{
			return 256;
		}

		// Token: 0x060087E1 RID: 34785 RVA: 0x0030B786 File Offset: 0x00309986
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x060087E2 RID: 34786 RVA: 0x0030B79C File Offset: 0x0030999C
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (this.Duration != 9999 && E.Cell.OnWorldMap())
			{
				base.Object.RemoveEffect(this, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060087E3 RID: 34787 RVA: 0x0030B7CD File Offset: 0x003099CD
		public override string GetDetails()
		{
			return "Can't physically interact with creatures and objects.";
		}

		// Token: 0x060087E4 RID: 34788 RVA: 0x0030B7D4 File Offset: 0x003099D4
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect<Nullphased>())
			{
				return false;
			}
			Object.ModIntProperty("Nullphase", 1, true);
			Object.PlayWorldSound("Sounds/Misc/sfx_phase_shiftOut", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			base.FlushNavigationCaches();
			return true;
		}

		// Token: 0x060087E5 RID: 34789 RVA: 0x0030B82C File Offset: 0x00309A2C
		public override void Remove(GameObject Object)
		{
			Object.ModIntProperty("Nullphase", -1, true);
			Object.PlayWorldSound("Sounds/Misc/sfx_phase_shiftIn", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			base.FlushNavigationCaches();
			Object.Gravitate();
		}

		// Token: 0x060087E6 RID: 34790 RVA: 0x0030B880 File Offset: 0x00309A80
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				int num = (XRLCore.CurrentFrame + this.FrameOffset) % 60 / 2 % 6;
				if (num == 0)
				{
					E.ApplyColors("&k", 100);
				}
				if (num == 1)
				{
					E.ApplyColors("&K", 100);
				}
				if (num == 2)
				{
					E.ApplyColors("&c", 100);
				}
				if (num == 4)
				{
					E.ApplyColors("&y", 100);
				}
			}
			return true;
		}

		// Token: 0x060087E7 RID: 34791 RVA: 0x0030B8EC File Offset: 0x00309AEC
		public override bool allowCopyOnNoEffectDeepCopy()
		{
			return true;
		}

		// Token: 0x04003A33 RID: 14899
		[NonSerialized]
		private int FrameOffset;
	}
}
