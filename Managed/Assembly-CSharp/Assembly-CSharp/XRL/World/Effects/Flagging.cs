using System;
using XRL.Core;
using XRL.Language;

namespace XRL.World.Effects
{
	// Token: 0x02000D37 RID: 3383
	[Serializable]
	public class Flagging : Effect
	{
		// Token: 0x0600857F RID: 34175 RVA: 0x00302F29 File Offset: 0x00301129
		public Flagging()
		{
			this.DisplayName = "{{r|flagging}}";
		}

		// Token: 0x06008580 RID: 34176 RVA: 0x00302F3C File Offset: 0x0030113C
		public Flagging(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06008581 RID: 34177 RVA: 0x00302F4B File Offset: 0x0030114B
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008582 RID: 34178 RVA: 0x00302F4E File Offset: 0x0030114E
		public override int GetEffectType()
		{
			return 117440516;
		}

		// Token: 0x06008583 RID: 34179 RVA: 0x00302F55 File Offset: 0x00301155
		public override string GetDetails()
		{
			return "Will fall asleep soon.";
		}

		// Token: 0x06008584 RID: 34180 RVA: 0x00302F5C File Offset: 0x0030115C
		public override bool Apply(GameObject Object)
		{
			return base.Apply(Object);
		}

		// Token: 0x06008585 RID: 34181 RVA: 0x00302F65 File Offset: 0x00301165
		public override void Remove(GameObject Object)
		{
			Object.ForceApplyEffect(new Asleep(5, true, false, false, true), null);
			base.Remove(Object);
		}

		// Token: 0x06008586 RID: 34182 RVA: 0x00302F80 File Offset: 0x00301180
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID;
		}

		// Token: 0x06008587 RID: 34183 RVA: 0x00302F98 File Offset: 0x00301198
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.Duration > 0)
			{
				this.Duration--;
				if (base.Object.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
					{
						"You're going to collapse from exhaustion in ",
						Grammar.Cardinal(this.Duration),
						" ",
						(this.Duration == 1) ? "round" : "rounds",
						"."
					}), null, true);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008588 RID: 34184 RVA: 0x00303020 File Offset: 0x00301220
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0 && XRLCore.CurrentFrame % 20 > 10)
			{
				E.RenderString = "Z";
				E.ColorString = "&r";
			}
			return true;
		}

		// Token: 0x040039BA RID: 14778
		public int Bonus;

		// Token: 0x040039BB RID: 14779
		public int Counter;
	}
}
