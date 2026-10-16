using System;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;
using XRL.World.Parts.Skill;

namespace XRL.World.Effects
{
	// Token: 0x02000D36 RID: 3382
	[Serializable]
	public class Famished : Effect, ITierInitialized
	{
		// Token: 0x06008576 RID: 34166 RVA: 0x00302D12 File Offset: 0x00300F12
		public Famished()
		{
			this.Duration = 1;
		}

		// Token: 0x06008577 RID: 34167 RVA: 0x00302D2C File Offset: 0x00300F2C
		public override int GetEffectType()
		{
			return 33554436;
		}

		// Token: 0x06008578 RID: 34168 RVA: 0x00302D33 File Offset: 0x00300F33
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008579 RID: 34169 RVA: 0x00302D36 File Offset: 0x00300F36
		public override string GetDescription()
		{
			if (this.mode == "wilted")
			{
				return "{{R|wilted}}";
			}
			return "{{R|famished}}";
		}

		// Token: 0x0600857A RID: 34170 RVA: 0x00302D55 File Offset: 0x00300F55
		public override string GetDetails()
		{
			if (this.mode == "wilted")
			{
				return "-5 Quickness\n-10% to natural healing rate";
			}
			return "-10 Quickness";
		}

		// Token: 0x0600857B RID: 34171 RVA: 0x00302D74 File Offset: 0x00300F74
		public override bool Apply(GameObject Object)
		{
			if (!Object.HasEffect(typeof(Famished)) && !Object.HasPart(typeof(Discipline_MindOverBody)) && Object.FireEvent("ApplyFamished"))
			{
				if (Object != null)
				{
					Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_negativeVitality", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
				if (Object.HasPart(typeof(PhotosyntheticSkin)))
				{
					this.mode = "wilted";
					this.Penalty = 5;
				}
				else
				{
					this.Penalty = 10;
				}
				base.StatShifter.SetStatShift("Speed", -this.Penalty, false);
				if (Object.IsPlayer())
				{
					if (this.mode == "wilted")
					{
						Popup.Show("You have wilted! You'll move and regenerate slower until you eat or bask in the sunlight again.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					else
					{
						Popup.Show("You are famished! You'll act more slowly until you eat again.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					if (AutoAct.IsInterruptable())
					{
						AutoAct.Interrupt(null, null, null, false);
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600857C RID: 34172 RVA: 0x00302E88 File Offset: 0x00301088
		public override void Applied(GameObject Object)
		{
			Stomach stomach;
			if (Object.TryGetPart<Stomach>(out stomach))
			{
				int num = stomach.CalculateCookingIncrement() * 2;
				if (stomach.CookingCounter < num)
				{
					stomach.CookingCounter = num;
				}
			}
		}

		// Token: 0x0600857D RID: 34173 RVA: 0x00302EB8 File Offset: 0x003010B8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Regenerating2" && this.mode == "wilted")
			{
				E.SetParameter("Amount", E.GetIntParameter("Amount", 0) * 9 / 10);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600857E RID: 34174 RVA: 0x00302F0E File Offset: 0x0030110E
		public override void Remove(GameObject Object)
		{
			base.StatShifter.RemoveStatShifts();
			this.Penalty = 0;
			base.Remove(Object);
		}

		// Token: 0x040039B8 RID: 14776
		public string mode = "famished";

		// Token: 0x040039B9 RID: 14777
		public int Penalty;
	}
}
