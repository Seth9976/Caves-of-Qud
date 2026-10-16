using System;
using XRL.Language;

namespace XRL.World.Parts
{
	/// <remarks>
	/// <para>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, glimmer alteration is increased in magnitude
	/// by a percentage equal to ((power load - 100) / 10), i.e. 30% for the
	/// standard overload power load of 400.
	/// </para>
	/// </remarks>
	// Token: 0x0200103F RID: 4159
	[Serializable]
	public class GlimmerAlteration : IPoweredPart
	{
		// Token: 0x0600A59F RID: 42399 RVA: 0x003CA2AC File Offset: 0x003C84AC
		public GlimmerAlteration()
		{
			this.WorksOnEquipper = true;
			this.IsPowerLoadSensitive = true;
		}

		// Token: 0x0600A5A0 RID: 42400 RVA: 0x003CA2C4 File Offset: 0x003C84C4
		public override bool SameAs(IPart p)
		{
			GlimmerAlteration glimmerAlteration = p as GlimmerAlteration;
			return glimmerAlteration.Amount == this.Amount && glimmerAlteration.ShowInShortDescription == this.ShowInShortDescription && base.SameAs(p);
		}

		// Token: 0x0600A5A1 RID: 42401 RVA: 0x003CA2FF File Offset: 0x003C84FF
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A5A2 RID: 42402 RVA: 0x003CA304 File Offset: 0x003C8504
		public override void TurnTick(long TimeTick, int Amount1)
		{
			if (!base.OnWorldMap)
			{
				base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, 1, null, false, 0, false, null);
			}
		}

		// Token: 0x0600A5A3 RID: 42403 RVA: 0x003CA340 File Offset: 0x003C8540
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BootSequenceDoneEvent.ID || ID == EquippedEvent.ID || (ID == PooledEvent<GetPsychicGlimmerEvent>.ID && this.Amount != 0) || (ID == GetShortDescriptionEvent.ID && this.ShowInShortDescription) || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A5A4 RID: 42404 RVA: 0x003CA394 File Offset: 0x003C8594
		public override bool HandleEvent(GetPsychicGlimmerEvent E)
		{
			if (this.Amount != 0 && this.IsObjectActivePartSubject(E.Subject) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.Level += this.GetEffectiveAmount();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5A5 RID: 42405 RVA: 0x003CA3FC File Offset: 0x003C85FC
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.ShowInShortDescription)
			{
				string text = null;
				int effectiveAmount = this.GetEffectiveAmount();
				if (effectiveAmount > 0)
				{
					text = "Increases psychic glimmer in the " + Grammar.Ordinal(effectiveAmount) + " degree.";
				}
				else if (effectiveAmount < 0)
				{
					text = "Reduces psychic glimmer in the " + Grammar.Ordinal(-effectiveAmount) + " degree.";
				}
				if (!string.IsNullOrEmpty(text))
				{
					E.Postfix.AppendRules(text);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5A6 RID: 42406 RVA: 0x003CA46D File Offset: 0x003C866D
		public override bool HandleEvent(EquippedEvent E)
		{
			if (this.Amount != 0)
			{
				E.Actor.SyncMutationLevelAndGlimmer();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5A7 RID: 42407 RVA: 0x003CA489 File Offset: 0x003C8689
		public override bool HandleEvent(UnequippedEvent E)
		{
			if (this.Amount != 0)
			{
				E.Actor.SyncMutationLevelAndGlimmer();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5A8 RID: 42408 RVA: 0x003CA4A8 File Offset: 0x003C86A8
		public override bool HandleEvent(BootSequenceDoneEvent E)
		{
			if (this.Amount != 0)
			{
				foreach (GameObject gameObject in this.GetActivePartSubjects())
				{
					gameObject.SyncMutationLevelAndGlimmer();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5A9 RID: 42409 RVA: 0x003CA508 File Offset: 0x003C8708
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A5AA RID: 42410 RVA: 0x003CA50C File Offset: 0x003C870C
		public int GetEffectiveAmount()
		{
			int num = this.Amount;
			int num2 = this.MyPowerLoadBonus(int.MinValue, 100, 10);
			if (num2 != 0)
			{
				num = num * (100 + num2) / 100;
			}
			return num;
		}

		// Token: 0x0400446D RID: 17517
		public int Amount;

		// Token: 0x0400446E RID: 17518
		public bool ShowInShortDescription;
	}
}
