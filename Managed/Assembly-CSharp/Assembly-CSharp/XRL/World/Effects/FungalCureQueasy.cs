using System;

namespace XRL.World.Effects
{
	// Token: 0x02000D3D RID: 3389
	[Serializable]
	public class FungalCureQueasy : Effect
	{
		// Token: 0x060085C2 RID: 34242 RVA: 0x003039CE File Offset: 0x00301BCE
		public FungalCureQueasy()
		{
			this.DisplayName = "{{W|queasy}}";
		}

		// Token: 0x060085C3 RID: 34243 RVA: 0x003039E1 File Offset: 0x00301BE1
		public FungalCureQueasy(int Duration = 100)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x060085C4 RID: 34244 RVA: 0x003039F0 File Offset: 0x00301BF0
		public override int GetEffectType()
		{
			return 4;
		}

		// Token: 0x060085C5 RID: 34245 RVA: 0x003039F3 File Offset: 0x00301BF3
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x060085C6 RID: 34246 RVA: 0x003039F6 File Offset: 0x00301BF6
		public override string GetDescription()
		{
			return null;
		}

		// Token: 0x060085C7 RID: 34247 RVA: 0x003039F9 File Offset: 0x00301BF9
		public override string GetDetails()
		{
			return "";
		}

		// Token: 0x060085C8 RID: 34248 RVA: 0x00303A00 File Offset: 0x00301C00
		public override bool Apply(GameObject Object)
		{
			FungalCureQueasy fungalCureQueasy;
			if (Object.TryGetEffect<FungalCureQueasy>(out fungalCureQueasy))
			{
				fungalCureQueasy.Duration = Math.Max(fungalCureQueasy.Duration, this.Duration);
				return false;
			}
			if (Object.IsPlayerControlled())
			{
				IComponent<GameObject>.EmitMessage(Object, (Object.IsPlayer() ? "You" : Object.T(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false)) + Object.GetVerb("feel", true, false, false, true, false) + " a little queasy.", ' ', false, true, false, null, null);
			}
			return true;
		}

		// Token: 0x060085C9 RID: 34249 RVA: 0x00303A94 File Offset: 0x00301C94
		public override void Remove(GameObject Object)
		{
			if (Object.IsPlayerControlled())
			{
				IComponent<GameObject>.EmitMessage(Object, Object.Poss("queasiness passes.", true), ' ', true, false, false, null, null);
			}
			base.Remove(Object);
		}
	}
}
