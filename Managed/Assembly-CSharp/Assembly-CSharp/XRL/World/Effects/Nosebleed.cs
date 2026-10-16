using System;
using XRL.Language;
using XRL.Liquids;

namespace XRL.World.Effects
{
	// Token: 0x02000D68 RID: 3432
	[Serializable]
	public class Nosebleed : Bleeding, ITierInitialized
	{
		// Token: 0x060087D4 RID: 34772 RVA: 0x0030B36A File Offset: 0x0030956A
		public Nosebleed()
		{
			this.DisplayName = "{{r|nosebleed}}";
			this.Internal = true;
		}

		// Token: 0x060087D5 RID: 34773 RVA: 0x0030B3A5 File Offset: 0x003095A5
		public Nosebleed(string Damage = "1", int SaveTarget = 20, GameObject Owner = null, bool Stack = true, bool StartMessageUsePopup = false, bool StopMessageUsePopup = false)
			: this()
		{
			this.Damage = Damage;
			this.SaveTarget = SaveTarget;
			this.Owner = Owner;
			this.Stack = Stack;
			this.StartMessageUsePopup = StartMessageUsePopup;
			this.StopMessageUsePopup = StopMessageUsePopup;
		}

		// Token: 0x060087D6 RID: 34774 RVA: 0x0030B3DC File Offset: 0x003095DC
		public override string GetStateDescription()
		{
			if (this.Hemorrhage)
			{
				return ("cerebrally " + this.CirculatoryLossTerm).Color(this.Color);
			}
			return ("nasally " + this.CirculatoryLossTerm).Color(this.Color);
		}

		// Token: 0x060087D7 RID: 34775 RVA: 0x0030B428 File Offset: 0x00309628
		public override bool CanApplyBleeding(GameObject Object)
		{
			return Object.FireEvent(Event.New("ApplyBleeding", "Effect", this)) && ApplyEffectEvent.Check(Object, "Bleeding", this, null) && (Object.CanHaveNosebleed() || Object.Brain != null) && base.CanApplyBleeding(Object);
		}

		// Token: 0x060087D8 RID: 34776 RVA: 0x0030B47C File Offset: 0x0030967C
		public override void SyncVersion()
		{
			BaseLiquid primaryLiquidForLiquidSpecification = base.GetPrimaryLiquidForLiquidSpecification();
			if (primaryLiquidForLiquidSpecification != null)
			{
				this.CirculatoryLossTerm = primaryLiquidForLiquidSpecification.CirculatoryLossTerm;
				this.CirculatoryLossNoun = primaryLiquidForLiquidSpecification.CirculatoryLossNoun;
				this.Color = primaryLiquidForLiquidSpecification.GetColor();
				this.DisplayName = ("nose" + this.CirculatoryLossNoun).Color(this.Color);
			}
		}

		// Token: 0x060087D9 RID: 34777 RVA: 0x0030B4D8 File Offset: 0x003096D8
		public override void ApplyingBleeding(GameObject Object)
		{
			if (!Object.CanHaveNosebleed())
			{
				this.DisplayName = "hemorrhaging".Color(this.Color);
				this.Hemorrhage = true;
			}
			base.ApplyingBleeding(Object);
		}

		// Token: 0x060087DA RID: 34778 RVA: 0x0030B508 File Offset: 0x00309708
		public override void StartMessage(GameObject Object)
		{
			if (this.Hemorrhage)
			{
				base.StartMessage(Object);
				return;
			}
			if (Object.GetBodyPartCount("Face") > 1)
			{
				if (Object.HasEffectOtherThan(typeof(Nosebleed), this))
				{
					base.EmitMessage(Object.Poss("noses", true) + " begin " + this.CirculatoryLossTerm + " more heavily.", ' ', false, false, false, null, Object);
					return;
				}
				base.EmitMessage(Object.Poss("noses", true) + " begin " + this.CirculatoryLossTerm + ".", ' ', false, false, false, null, Object);
				return;
			}
			else
			{
				if (Object.HasEffectOtherThan(typeof(Nosebleed), this))
				{
					base.EmitMessage(Object.Poss("nose", true) + " begins " + this.CirculatoryLossTerm + " more heavily.", ' ', false, false, false, null, Object);
					return;
				}
				base.EmitMessage(Object.Poss("nose", true) + " begins " + this.CirculatoryLossTerm + ".", ' ', false, false, false, null, Object);
				return;
			}
		}

		// Token: 0x060087DB RID: 34779 RVA: 0x0030B614 File Offset: 0x00309814
		public override void StopMessage(GameObject Object)
		{
			if (this.Hemorrhage)
			{
				base.StartMessage(Object);
				return;
			}
			if (Object.GetBodyPartCount("Face") > 1)
			{
				if (Object.HasEffectOtherThan(typeof(Nosebleed), this))
				{
					base.EmitMessage(Object.Poss("noses", true) + " stop " + this.CirculatoryLossTerm + " quite so heavily.", ' ', false, false, false, Object, null);
					return;
				}
				base.EmitMessage(Object.Poss("noses", true) + " stop " + this.CirculatoryLossTerm + ".", ' ', false, false, false, Object, null);
				return;
			}
			else
			{
				if (Object.HasEffectOtherThan(typeof(Nosebleed), this))
				{
					base.EmitMessage(Object.Poss("nose", true) + " stops " + this.CirculatoryLossTerm + " quite so heavily.", ' ', false, false, false, Object, null);
					return;
				}
				base.EmitMessage(Object.Poss("nose", true) + " stops " + this.CirculatoryLossTerm + ".", ' ', false, false, false, Object, null);
				return;
			}
		}

		// Token: 0x060087DC RID: 34780 RVA: 0x0030B720 File Offset: 0x00309920
		public override string DamageMessage()
		{
			if (this.Hemorrhage)
			{
				return base.DamageMessage();
			}
			return "from " + Grammar.A(base.DisplayNameStripped, false) + ".";
		}

		// Token: 0x04003A2F RID: 14895
		public string Color = "r";

		// Token: 0x04003A30 RID: 14896
		public string CirculatoryLossTerm = "bleeding";

		// Token: 0x04003A31 RID: 14897
		public string CirculatoryLossNoun = "bleed";

		// Token: 0x04003A32 RID: 14898
		public bool Hemorrhage;
	}
}
