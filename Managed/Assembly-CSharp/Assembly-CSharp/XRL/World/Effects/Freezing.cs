using System;
using XRL.Core;

namespace XRL.World.Effects
{
	// Token: 0x02000D3A RID: 3386
	[Serializable]
	public class Freezing : Effect
	{
		// Token: 0x0600859E RID: 34206 RVA: 0x00303336 File Offset: 0x00301536
		public Freezing()
		{
			this.DisplayName = "{{freezing|freezing}}";
		}

		// Token: 0x0600859F RID: 34207 RVA: 0x00303349 File Offset: 0x00301549
		public Freezing(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x060085A0 RID: 34208 RVA: 0x00303358 File Offset: 0x00301558
		public override string GetDescription()
		{
			return null;
		}

		// Token: 0x060085A1 RID: 34209 RVA: 0x0030335B File Offset: 0x0030155B
		public static bool CanApply(GameObject Object)
		{
			return CanApplyEffectEvent.Check<Freezing>(Object, 0) && !Object.HasEffect<Freezing>();
		}

		// Token: 0x060085A2 RID: 34210 RVA: 0x00303373 File Offset: 0x00301573
		public static bool ApplyTo(GameObject Object)
		{
			return Freezing.CanApply(Object) && Object.ApplyEffect(new Freezing(1), null);
		}

		// Token: 0x060085A3 RID: 34211 RVA: 0x0030338C File Offset: 0x0030158C
		public override bool Render(RenderEvent E)
		{
			if (base.Object.Brain != null)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 5 && num < 15)
				{
					E.ApplyColors("&C", 200);
					E.RenderString = 'ø'.GetString();
					return false;
				}
			}
			return true;
		}
	}
}
