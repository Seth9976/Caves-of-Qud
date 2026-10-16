using System;
using System.Text;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;

namespace XRL.World.Effects
{
	/// This class is not used in the base game.
	// Token: 0x02000C45 RID: 3141
	[RequiresMod(WorkshopID = 2242612659UL)]
	[Serializable]
	public class CardiacArrest : Effect, ITierInitialized
	{
		// Token: 0x06008039 RID: 32825 RVA: 0x002F3A38 File Offset: 0x002F1C38
		public CardiacArrest()
		{
			this.Duration = 1;
			this.DisplayName = "{{W|cardiac arrest}}";
		}

		// Token: 0x0600803A RID: 32826 RVA: 0x002F3A89 File Offset: 0x002F1C89
		public CardiacArrest(int HeartsStopped)
			: this()
		{
			this.HeartsStopped = HeartsStopped;
		}

		// Token: 0x0600803B RID: 32827 RVA: 0x002F3A98 File Offset: 0x002F1C98
		public void Initialize(int Tier)
		{
			Tier = Tier.Constrain(Stat.Random(Tier - 2, Tier + 2));
			this.SaveTarget = 16 + Tier * 3;
		}

		// Token: 0x0600803C RID: 32828 RVA: 0x002F3AB8 File Offset: 0x002F1CB8
		public override int GetEffectType()
		{
			return 100663312;
		}

		// Token: 0x0600803D RID: 32829 RVA: 0x002F3ABF File Offset: 0x002F1CBF
		public override string GetStateDescription()
		{
			return "{{W|in cardiac arrest}}";
		}

		// Token: 0x0600803E RID: 32830 RVA: 0x002F3AC6 File Offset: 0x002F1CC6
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x0600803F RID: 32831 RVA: 0x002F3ACC File Offset: 0x002F1CCC
		public override string GetDetails()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append("-1 to every attribute per turn (current: " + (-this.Progress).ToString() + ").");
			if (this.HeartsStopped < base.Object.GetHeartCount())
			{
				stringBuilder.Compound("Unable to progress until all hearts have stopped (current:", "\n").Append(this.HeartsStopped).Append(").");
			}
			stringBuilder.Compound("Dies when any attribute reaches zero.", "\n").Compound("Doesn't regenerate hit points.", "\n").Compound("Healing effects are only half as effective.", "\n");
			if (!string.IsNullOrEmpty(this.SaveAttribute))
			{
				stringBuilder.Compound("Difficulty ", "\n").Append(this.SaveTarget).Append(' ')
					.Append(this.SaveAttribute.Replace(",", "/"))
					.Append(" save each turn to recover.");
			}
			stringBuilder.Compound("Electrical damage has a percentage chance to cause recovery equal to the damage taken.", "\n");
			if (this.IllFactor > 0)
			{
				stringBuilder.Compound("Will become ill upon recovery for 5 turns per attribute penalty point.", "\n");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06008040 RID: 32832 RVA: 0x002F3BF4 File Offset: 0x002F1DF4
		public override bool Apply(GameObject Object)
		{
			int heartCount = Object.GetHeartCount();
			if (heartCount < 1)
			{
				return false;
			}
			CardiacArrest effect = Object.GetEffect<CardiacArrest>();
			if (effect != null)
			{
				if (effect.HeartsStopped < heartCount)
				{
					base.DidX("go", "into another cardiac arrest", "!", null, null, null, Object, false, false, null, null, false, false, false, false, Object.IsPlayer(), null);
					effect.HeartsStopped += this.HeartsStopped;
				}
				return false;
			}
			base.DidX("go", "into cardiac arrest", "!", null, null, null, Object, false, false, null, null, false, false, false, false, Object.IsPlayer(), null);
			return true;
		}

		// Token: 0x06008041 RID: 32833 RVA: 0x002F3C88 File Offset: 0x002F1E88
		public override void Remove(GameObject Object)
		{
			base.StatShifter.RemoveStatShifts();
			if (Object.IsPlayer())
			{
				if (this.HeartsStopped > 1)
				{
					Popup.Show("{{G|Your hearts restart!}}", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				else
				{
					Popup.Show("{{G|Your heart restarts!}}", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
			else
			{
				base.DidX("look", "less stricken", null, null, null, Object, null, false, false, null, null, false, false, false, false, false, null);
			}
			int num = this.Progress * this.IllFactor;
			if (num > 0)
			{
				Object.ApplyEffect(new Ill(num, 1, "You feel shaken and infirm.", Object.IsPlayer(), false), null);
			}
			base.Remove(Object);
		}

		// Token: 0x06008042 RID: 32834 RVA: 0x002F3D32 File Offset: 0x002F1F32
		public override bool Render(RenderEvent E)
		{
			if (XRLCore.CurrentFrame > 45)
			{
				E.Tile = null;
				E.RenderString = "û";
				E.ColorString = "&W^k";
			}
			return true;
		}

		// Token: 0x06008043 RID: 32835 RVA: 0x002F3D5B File Offset: 0x002F1F5B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == PooledEvent<GetCompanionStatusEvent>.ID || ID == TookDamageEvent.ID;
		}

		// Token: 0x06008044 RID: 32836 RVA: 0x002F3D81 File Offset: 0x002F1F81
		public override bool HandleEvent(GetCompanionStatusEvent E)
		{
			if (E.Object == base.Object)
			{
				E.AddStatus("in cardiac arrest", 30);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008045 RID: 32837 RVA: 0x002F3DA8 File Offset: 0x002F1FA8
		public override bool HandleEvent(TookDamageEvent E)
		{
			if (E.Object == base.Object && E.Damage.IsElectricDamage() && E.Damage.Amount.in100())
			{
				base.Object.RemoveEffect(this, true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008046 RID: 32838 RVA: 0x002F3DF7 File Offset: 0x002F1FF7
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.CardiacArrestProgress();
			return base.HandleEvent(E);
		}

		// Token: 0x06008047 RID: 32839 RVA: 0x002F3E06 File Offset: 0x002F2006
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Recuperating");
			Registrar.Register("Regenerating");
			Registrar.Register("Healing");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008048 RID: 32840 RVA: 0x002F3E34 File Offset: 0x002F2034
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Healing")
			{
				E.SetParameter("Amount", E.GetIntParameter("Amount", 0) / 2);
			}
			else
			{
				if (E.ID == "Regenerating")
				{
					E.SetParameter("Amount", 0);
					return false;
				}
				if (E.ID == "Recuperating")
				{
					base.Object.RemoveEffect(this, true);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x06008049 RID: 32841 RVA: 0x002F3EB8 File Offset: 0x002F20B8
		public void CardiacArrestProgress()
		{
			int heartCount = base.Object.GetHeartCount();
			bool flag = this.HeartsStopped >= heartCount;
			if (flag)
			{
				this.Progress++;
				foreach (string text in Statistic.Attributes)
				{
					if (base.Object.Stat(text, 0) <= 1)
					{
						base.Object.Die(null, "cardiac arrest", "Your " + ((heartCount > 1) ? "hearts" : "heart") + " stopped.", base.Object.Its + " " + ((heartCount > 1) ? "hearts" : "heart") + " stopped.", false, null, null, false, false, null, null, null);
						return;
					}
					base.StatShifter.SetStatShift(text, -this.Progress, false);
				}
			}
			if (!string.IsNullOrEmpty(this.SaveAttribute) && base.Object.MakeSave(this.SaveAttribute, this.SaveTarget, null, null, this.SaveVs, false, false, !flag, false, null))
			{
				base.Object.RemoveEffect(this, true);
			}
		}

		// Token: 0x040038F0 RID: 14576
		public int HeartsStopped = 1;

		// Token: 0x040038F1 RID: 14577
		public int Progress;

		// Token: 0x040038F2 RID: 14578
		public int IllFactor = 5;

		// Token: 0x040038F3 RID: 14579
		public string SaveAttribute = "Toughness,Willpower";

		// Token: 0x040038F4 RID: 14580
		public int SaveTarget = 30;

		// Token: 0x040038F5 RID: 14581
		public string SaveVs = "CardiacArrest CardiacArrestRecovery";
	}
}
