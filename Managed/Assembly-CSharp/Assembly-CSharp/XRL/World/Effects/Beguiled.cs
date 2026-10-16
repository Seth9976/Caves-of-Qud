using System;
using Qud.API;
using XRL.World.AI;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000C38 RID: 3128
	[Serializable]
	public class Beguiled : Effect
	{
		// Token: 0x06007F74 RID: 32628 RVA: 0x002F0534 File Offset: 0x002EE734
		public Beguiled()
		{
			this.DisplayName = "{{m|beguiled}}";
			this.Duration = 1;
		}

		// Token: 0x06007F75 RID: 32629 RVA: 0x002F054E File Offset: 0x002EE74E
		public Beguiled(GameObject Beguiler)
			: this()
		{
			this.Beguiler = Beguiler;
		}

		// Token: 0x06007F76 RID: 32630 RVA: 0x002F055D File Offset: 0x002EE75D
		public Beguiled(GameObject Beguiler, int Level)
			: this(Beguiler)
		{
			this.Level = Level;
		}

		// Token: 0x06007F77 RID: 32631 RVA: 0x002F056D File Offset: 0x002EE76D
		public Beguiled(GameObject Beguiler, int Level, bool Independent)
			: this(Beguiler, Level)
		{
			this.Independent = Independent;
		}

		// Token: 0x06007F78 RID: 32632 RVA: 0x002F057E File Offset: 0x002EE77E
		public override int GetEffectType()
		{
			return 2;
		}

		// Token: 0x06007F79 RID: 32633 RVA: 0x002F0581 File Offset: 0x002EE781
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06007F7A RID: 32634 RVA: 0x002F0584 File Offset: 0x002EE784
		public override string GetDetails()
		{
			string text = "Charmed by another creature into following them.";
			if (this.Level != 0)
			{
				text = text + "\n+" + (this.Level * 5).ToString() + " hit points.";
			}
			return text;
		}

		// Token: 0x06007F7B RID: 32635 RVA: 0x002F05C1 File Offset: 0x002EE7C1
		public override string GetDescription()
		{
			return "{{m|beguiled}}";
		}

		// Token: 0x06007F7C RID: 32636 RVA: 0x002F05C8 File Offset: 0x002EE7C8
		public override bool Apply(GameObject Object)
		{
			if (!GameObject.Validate(ref this.Beguiler))
			{
				return false;
			}
			if (Object.Brain == null)
			{
				return false;
			}
			if (!Object.FireEvent("CanApplyBeguile"))
			{
				return false;
			}
			if (!Object.FireEvent("ApplyBeguile"))
			{
				return false;
			}
			if (!ApplyEffectEvent.Check(Object, "Beguile", this, null))
			{
				return false;
			}
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_charm", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			IComponent<GameObject>.XDidYToZ(Object, "ogle", this.Beguiler, "lovingly", null, null, null, this.Beguiler, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
			if (this.Beguiler.IsPlayer() && !this.Beguiler.HasEffect<Dominated>())
			{
				JournalAPI.AddAccomplishment(Object.An(int.MaxValue, null, null, false, false, false, false, false, true, true, false, false, true, false, null, false, null, false) + " ogled you lovingly after you employed your charm.", "The storied eroticism of =name= became intimately known to " + Object.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + ".", string.Concat(new string[]
				{
					"<spice.elements.",
					The.Player.GetMythicDomain(5, null),
					".weddingConditions.!random.capitalize>, =name= cemented ",
					The.Player.GetPronounProvider(false).PossessiveAdjective,
					" friendship with ",
					Object.GetPrimaryFactionName(true, true, true),
					" by marrying ",
					Object.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
					"."
				}), null, "general", MuralCategory.Trysts, MuralWeight.Medium, null, -1L, true);
			}
			Object.Heartspray("&M", "&R", "&r", "&Y", '\u0003');
			Beguiling.SyncTarget(this.Beguiler, Object, this.Independent);
			Object.SetAlliedLeader<AllyBeguile>(this.Beguiler, 0, false);
			this.ApplyBeguilement();
			return true;
		}

		// Token: 0x06007F7D RID: 32637 RVA: 0x002F07D8 File Offset: 0x002EE9D8
		public override void Remove(GameObject Object)
		{
			if (GameObject.Validate(ref this.Beguiler) && Object.PartyLeader == this.Beguiler && !this.Beguiler.SupportsFollower(Object, 13))
			{
				Object.Brain.PartyLeader = null;
				Object.Brain.Goals.Clear();
				base.DidXToY("lose", "interest in", this.Beguiler, null, null, null, null, null, this.Beguiler, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
			}
			Brain brain = Object.Brain;
			GameObject beguiler = this.Beguiler;
			brain.RemoveAllegiance<AllyBeguile>((beguiler != null) ? beguiler.BaseID : 0);
			this.UnapplyBeguilement();
			this.Beguiler = null;
			base.Remove(Object);
		}

		// Token: 0x06007F7E RID: 32638 RVA: 0x002F088C File Offset: 0x002EEA8C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("BeginTakeAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x06007F7F RID: 32639 RVA: 0x002F08B8 File Offset: 0x002EEAB8
		public Beguiling GetMutation()
		{
			if (!GameObject.Validate(ref this.Beguiler))
			{
				return null;
			}
			Beguiling part = this.Beguiler.GetPart<Beguiling>();
			if (part == null)
			{
				return null;
			}
			if (part.RealityDistortionBased && !base.CheckMyRealityDistortionUsability(null, null, null, null, null, null, 0))
			{
				return null;
			}
			return part;
		}

		// Token: 0x06007F80 RID: 32640 RVA: 0x002F0908 File Offset: 0x002EEB08
		public void ApplyBeguilement()
		{
			int num = this.Level - this.LevelApplied;
			if (base.Object.HasStat("Hitpoints"))
			{
				base.Object.Statistics["Hitpoints"].BaseValue += 5 * num;
			}
			if (base.Object.Brain != null && GameObject.Validate(ref this.Beguiler))
			{
				base.Object.Brain.AddOpinion<OpinionBeguile>(this.Beguiler, 1f);
			}
			this.LevelApplied = this.Level;
		}

		// Token: 0x06007F81 RID: 32641 RVA: 0x002F099C File Offset: 0x002EEB9C
		public void UnapplyBeguilement()
		{
			if (this.LevelApplied != 0)
			{
				if (base.Object.HasStat("Hitpoints"))
				{
					base.Object.Statistics["Hitpoints"].BaseValue -= 5 * this.LevelApplied;
				}
				if (base.Object.Brain != null && GameObject.Validate(ref this.Beguiler))
				{
					base.Object.Brain.RemoveOpinion<OpinionBeguile>(this.Beguiler);
				}
				this.LevelApplied = 0;
			}
		}

		// Token: 0x06007F82 RID: 32642 RVA: 0x002F0A24 File Offset: 0x002EEC24
		public void SyncToMutation()
		{
			if (!this.Independent)
			{
				Beguiling mutation = this.GetMutation();
				if (mutation == null)
				{
					this.Duration = 0;
					return;
				}
				if (mutation.Level != this.Level)
				{
					this.Level = mutation.Level;
					this.ApplyBeguilement();
				}
			}
		}

		// Token: 0x06007F83 RID: 32643 RVA: 0x002F0A6B File Offset: 0x002EEC6B
		public bool IsSupported()
		{
			return GameObject.Validate(ref this.Beguiler) && this.Beguiler.SupportsFollower(base.Object, 2);
		}

		// Token: 0x06007F84 RID: 32644 RVA: 0x002F0A90 File Offset: 0x002EEC90
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction")
			{
				if (!this.IsSupported())
				{
					this.Duration = 0;
				}
				else
				{
					this.SyncToMutation();
				}
			}
			else if (E.ID == "BeforeDeepCopyWithoutEffects")
			{
				this.UnapplyBeguilement();
			}
			else if (E.ID == "AfterDeepCopyWithoutEffects")
			{
				this.ApplyBeguilement();
			}
			return base.FireEvent(E);
		}

		// Token: 0x040038CF RID: 14543
		public GameObject Beguiler;

		// Token: 0x040038D0 RID: 14544
		public int Level;

		// Token: 0x040038D1 RID: 14545
		public int LevelApplied;

		// Token: 0x040038D2 RID: 14546
		public bool Independent;
	}
}
