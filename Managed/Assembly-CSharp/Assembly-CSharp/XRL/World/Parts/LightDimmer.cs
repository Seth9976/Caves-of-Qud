using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x020011AA RID: 4522
	[Serializable]
	public class LightDimmer : IPart
	{
		// Token: 0x0600B3B5 RID: 46005 RVA: 0x00418FDC File Offset: 0x004171DC
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600B3B6 RID: 46006 RVA: 0x00418FDF File Offset: 0x004171DF
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.Tick(Amount);
		}

		// Token: 0x0600B3B7 RID: 46007 RVA: 0x00418FE8 File Offset: 0x004171E8
		public void Tick(int Increment = 1)
		{
			if (Stat.Random(1, this.Chance) > Increment)
			{
				return;
			}
			LightSource lightSource = this.ParentObject.GetPart<LightSource>();
			if (lightSource == null)
			{
				lightSource = new LightSource
				{
					Radius = 4
				};
				this.ParentObject.AddPart<LightSource>(lightSource, true, false);
			}
			if (!50.in100())
			{
				lightSource.Radius++;
				base.DidX("brighten", null, null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
				return;
			}
			if (lightSource.Radius <= 1)
			{
				return;
			}
			lightSource.Radius--;
			base.DidX("dim", null, null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
		}

		// Token: 0x0400487F RID: 18559
		public int Chance = 100;
	}
}
