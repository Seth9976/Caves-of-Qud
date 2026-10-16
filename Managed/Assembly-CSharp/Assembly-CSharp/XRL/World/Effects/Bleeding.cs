using System;
using XRL.Core;
using XRL.Liquids;
using XRL.Rules;
using XRL.Wish;
using XRL.World.Capabilities;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000C3B RID: 3131
	[HasWishCommand]
	[Serializable]
	public class Bleeding : Effect, ITierInitialized
	{
		// Token: 0x06007FA4 RID: 32676 RVA: 0x002F1309 File Offset: 0x002EF509
		public Bleeding()
		{
			this.DisplayName = "{{r|bleeding}}";
			this.Duration = 1;
		}

		// Token: 0x06007FA5 RID: 32677 RVA: 0x002F133D File Offset: 0x002EF53D
		public Bleeding(string Damage = "1", int SaveTarget = 20, GameObject Owner = null, bool Stack = true, bool Internal = false, bool StartMessageUsePopup = false, bool StopMessageUsePopup = false)
			: this()
		{
			this.Damage = Damage;
			this.SaveTarget = SaveTarget;
			this.Owner = Owner;
			this.Stack = Stack;
			this.Internal = Internal;
			this.StartMessageUsePopup = StartMessageUsePopup;
			this.StopMessageUsePopup = StopMessageUsePopup;
		}

		// Token: 0x06007FA6 RID: 32678 RVA: 0x002F137C File Offset: 0x002EF57C
		public void Initialize(int Tier)
		{
			Tier = Tier.Constrain(Stat.Random(Tier - 2, Tier + 2));
			this.SaveTarget = Math.Max(25, (Tier + 1) * 5);
			if (Tier >= 7)
			{
				this.Damage = "3-4";
				return;
			}
			if (Tier >= 5)
			{
				this.Damage = "2-3";
				return;
			}
			if (Tier >= 3)
			{
				this.Damage = "1-2";
				return;
			}
			if (Tier >= 1)
			{
				this.Damage = "0-1";
			}
		}

		// Token: 0x06007FA7 RID: 32679 RVA: 0x002F13EC File Offset: 0x002EF5EC
		public override int GetEffectType()
		{
			return 117440528;
		}

		// Token: 0x06007FA8 RID: 32680 RVA: 0x002F13F3 File Offset: 0x002EF5F3
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			GameObject.Validate(ref this.Owner);
			base.Write(Basis, Writer);
		}

		// Token: 0x06007FA9 RID: 32681 RVA: 0x002F1409 File Offset: 0x002EF609
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06007FAA RID: 32682 RVA: 0x002F140C File Offset: 0x002EF60C
		public override string GetDescription()
		{
			return this.DisplayName;
		}

		// Token: 0x06007FAB RID: 32683 RVA: 0x002F1414 File Offset: 0x002EF614
		public override string GetDetails()
		{
			string text = this.Damage + " damage per turn.";
			if (this.Internal)
			{
				text += " Internal.";
			}
			if (this.Bandaged)
			{
				text += " Has been bandaged.";
			}
			return text;
		}

		// Token: 0x06007FAC RID: 32684 RVA: 0x002F145C File Offset: 0x002EF65C
		public override bool Apply(GameObject Object)
		{
			if (Object.GetIntProperty("Bleeds", 0) <= 0)
			{
				return false;
			}
			if (this.Stack)
			{
				foreach (Effect effect in Object.Effects)
				{
					Bleeding bleeding = effect as Bleeding;
					if (bleeding != null && bleeding.GetType() == base.GetType() && bleeding.Stack)
					{
						if (bleeding.SaveTarget < this.SaveTarget)
						{
							bleeding.SaveTarget = this.SaveTarget;
							bleeding.Bandaged = false;
						}
						if (bleeding.Damage.GetCachedDieRoll().Average() < this.Damage.GetCachedDieRoll().Average())
						{
							bleeding.Damage = this.Damage;
							bleeding.Bandaged = false;
						}
						return false;
					}
				}
			}
			if (!Object.FireEvent(Event.New("Apply" + base.ClassName, "Effect", this)))
			{
				return false;
			}
			if (!ApplyEffectEvent.Check(Object, base.ClassName, this, null))
			{
				return false;
			}
			this.SyncVersion();
			this.ApplyingBleeding(Object);
			this.StartMessage(Object);
			return true;
		}

		// Token: 0x06007FAD RID: 32685 RVA: 0x002F1598 File Offset: 0x002EF798
		public override void Remove(GameObject Object)
		{
			this.StopMessage(Object);
			base.Remove(Object);
		}

		// Token: 0x06007FAE RID: 32686 RVA: 0x002F15A8 File Offset: 0x002EF7A8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == SingletonEvent<GeneralAmnestyEvent>.ID || ID == PooledEvent<GetCompanionStatusEvent>.ID;
		}

		// Token: 0x06007FAF RID: 32687 RVA: 0x002F15CE File Offset: 0x002EF7CE
		public override bool HandleEvent(GetCompanionStatusEvent E)
		{
			if (E.Object == base.Object)
			{
				E.AddStatus("bleeding", 20);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007FB0 RID: 32688 RVA: 0x002F15F2 File Offset: 0x002EF7F2
		public override bool HandleEvent(GeneralAmnestyEvent E)
		{
			this.Owner = null;
			return base.HandleEvent(E);
		}

		// Token: 0x06007FB1 RID: 32689 RVA: 0x002F1602 File Offset: 0x002EF802
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.ProcessBleeding();
			return base.HandleEvent(E);
		}

		// Token: 0x06007FB2 RID: 32690 RVA: 0x002F1611 File Offset: 0x002EF811
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Recuperating");
			base.Register(Object, Registrar);
		}

		// Token: 0x06007FB3 RID: 32691 RVA: 0x002F1628 File Offset: 0x002EF828
		public override bool Render(RenderEvent E)
		{
			Render render = base.Object.Render;
			int num = XRLCore.CurrentFrame % 60;
			if (num > 45 && num < 55)
			{
				E.RenderString = "\u0003";
				E.ApplyColors("&r", Effect.ICON_COLOR_PRIORITY);
				return false;
			}
			return true;
		}

		// Token: 0x06007FB4 RID: 32692 RVA: 0x002F1672 File Offset: 0x002EF872
		public override bool FireEvent(Event E)
		{
			if (E.ID == "Recuperating")
			{
				base.Object.RemoveEffect(this, true);
			}
			return base.FireEvent(E);
		}

		// Token: 0x06007FB5 RID: 32693 RVA: 0x002F169B File Offset: 0x002EF89B
		public virtual string DamageAttributes()
		{
			return "Bleeding Unavoidable";
		}

		// Token: 0x06007FB6 RID: 32694 RVA: 0x002F16A2 File Offset: 0x002EF8A2
		public virtual string SaveAttribute()
		{
			return "Toughness";
		}

		// Token: 0x06007FB7 RID: 32695 RVA: 0x002F16A9 File Offset: 0x002EF8A9
		public virtual string SaveVs()
		{
			return "Bleeding";
		}

		// Token: 0x06007FB8 RID: 32696 RVA: 0x002F16B0 File Offset: 0x002EF8B0
		public virtual bool CanApplyBleeding(GameObject Object)
		{
			return true;
		}

		// Token: 0x06007FB9 RID: 32697 RVA: 0x002F16B3 File Offset: 0x002EF8B3
		public virtual void ApplyingBleeding(GameObject Object)
		{
		}

		// Token: 0x06007FBA RID: 32698 RVA: 0x002F16B8 File Offset: 0x002EF8B8
		public virtual bool RecoveryChance(int ReduceSaveTargetBy = 1, bool ReduceFirst = false)
		{
			if (ReduceFirst)
			{
				this.SaveTarget -= ReduceSaveTargetBy;
			}
			if (base.Object.MakeSave(this.SaveAttribute(), this.SaveTarget, null, null, this.SaveVs(), false, false, false, false, null))
			{
				base.Object.RemoveEffect(this, true);
				return true;
			}
			if (!ReduceFirst)
			{
				this.SaveTarget -= ReduceSaveTargetBy;
			}
			return false;
		}

		// Token: 0x06007FBB RID: 32699 RVA: 0x002F1720 File Offset: 0x002EF920
		public void ProcessBleeding()
		{
			GameObject.Validate(ref this.Owner);
			if (this.Duration <= 0)
			{
				return;
			}
			if (!GameObject.Validate(base.Object))
			{
				return;
			}
			if (this.RecoveryChance(1, false))
			{
				return;
			}
			GameObject @object = base.Object;
			int num = this.Damage.RollCached();
			string text = this.DamageAttributes();
			GameObject owner = this.Owner;
			@object.TakeDamage(num, this.DamageMessage(), text, null, null, null, owner, null, null, null, false, false, true, false, false, false, false, false, false, 5, null);
			if (!this.Internal)
			{
				Cell currentCell = base.Object.CurrentCell;
				if (currentCell != null && !currentCell.OnWorldMap())
				{
					bool flag = false;
					if (50.in100())
					{
						foreach (GameObject gameObject in currentCell.GetObjectsWithPartReadonly("Render"))
						{
							LiquidVolume liquidVolume = gameObject.LiquidVolume;
							if (liquidVolume != null && liquidVolume.IsOpenVolume())
							{
								liquidVolume.MixWith(new LiquidVolume
								{
									InitialLiquid = base.Object.GetBleedLiquid("blood-1000"),
									Volume = 2
								}, null, null, null, false);
								flag = true;
							}
							else
							{
								gameObject.MakeBloody(base.Object.GetBleedLiquid("blood-1000"), Stat.Random(1, 3), 1);
							}
						}
					}
					if (!flag && 5.in100())
					{
						GameObject gameObject2 = GameObject.Create("BloodSplash");
						LiquidVolume liquidVolume2 = gameObject2.LiquidVolume;
						if (liquidVolume2 != null)
						{
							liquidVolume2.InitialLiquid = base.Object.GetBleedLiquid("blood-1000");
							this.SplashCreated(gameObject2);
							currentCell.AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							return;
						}
						MetricsManager.LogError("generated " + gameObject2.Blueprint + " with no LiquidVolume");
						gameObject2.Obliterate(null, false, null);
					}
				}
			}
		}

		// Token: 0x06007FBC RID: 32700 RVA: 0x002F1918 File Offset: 0x002EFB18
		public virtual void SplashCreated(GameObject Object)
		{
		}

		// Token: 0x06007FBD RID: 32701 RVA: 0x002F191A File Offset: 0x002EFB1A
		public virtual void SyncVersion()
		{
			this.DisplayName = this.GetVersionOfBleedingForLiquidSpecification();
		}

		// Token: 0x06007FBE RID: 32702 RVA: 0x002F1928 File Offset: 0x002EFB28
		public virtual void StartMessage(GameObject Object)
		{
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_physicalRupture", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			if (Object.HasEffectOtherThan((Effect fx) => fx is Bleeding, this))
			{
				base.DidX("begin", base.DisplayNameStripped + " from another wound", "!", null, null, null, Object, false, false, null, null, false, false, false, false, this.StartMessageUsePopup, null);
				return;
			}
			base.DidX("begin", base.DisplayNameStripped, "!", null, null, null, Object, false, false, null, null, false, false, false, false, this.StartMessageUsePopup, null);
		}

		// Token: 0x06007FBF RID: 32703 RVA: 0x002F19E8 File Offset: 0x002EFBE8
		public virtual void StopMessage(GameObject Object)
		{
			if (Object.HasEffectOtherThan((Effect fx) => fx is Bleeding, this))
			{
				base.EmitMessage(string.Concat(new string[]
				{
					"One of ",
					Object.poss("wounds", true),
					" stops ",
					base.DisplayNameStripped,
					"."
				}), ' ', false, this.StopMessageUsePopup, false, Object, null);
				return;
			}
			base.DidX("stop", base.DisplayNameStripped, null, null, null, Object, null, false, false, null, null, false, false, false, false, this.StopMessageUsePopup, null);
		}

		// Token: 0x06007FC0 RID: 32704 RVA: 0x002F1A92 File Offset: 0x002EFC92
		public virtual string DamageMessage()
		{
			return "from " + base.DisplayNameStripped + ".";
		}

		// Token: 0x06007FC1 RID: 32705 RVA: 0x002F1AAC File Offset: 0x002EFCAC
		public static string GetPrimaryLiquidNameForLiquidSpecification(string Spec)
		{
			if (!Spec.Contains(","))
			{
				if (!Spec.Contains("-"))
				{
					return Spec;
				}
				if (Spec.EndsWith("-1000"))
				{
					return Spec.Substring(0, Spec.Length - 5);
				}
			}
			return new LiquidVolume(Spec, 1).GetPrimaryLiquidID();
		}

		// Token: 0x06007FC2 RID: 32706 RVA: 0x002F1AFE File Offset: 0x002EFCFE
		public static BaseLiquid GetPrimaryLiquidForLiquidSpecification(string Spec)
		{
			return LiquidVolume.GetLiquid(Bleeding.GetPrimaryLiquidNameForLiquidSpecification(Spec));
		}

		// Token: 0x06007FC3 RID: 32707 RVA: 0x002F1B10 File Offset: 0x002EFD10
		public static string GetVersionOfBleedingForLiquidSpecification(string Spec)
		{
			return Bleeding.GetPrimaryLiquidForLiquidSpecification(Spec).ColoredCirculatoryLossTerm;
		}

		// Token: 0x06007FC4 RID: 32708 RVA: 0x002F1B1D File Offset: 0x002EFD1D
		public BaseLiquid GetPrimaryLiquidForLiquidSpecification()
		{
			return Bleeding.GetPrimaryLiquidForLiquidSpecification(base.Object.GetBleedLiquid("blood-1000"));
		}

		// Token: 0x06007FC5 RID: 32709 RVA: 0x002F1B34 File Offset: 0x002EFD34
		public string GetPrimaryLiquidNameForLiquidSpecification()
		{
			return Bleeding.GetPrimaryLiquidNameForLiquidSpecification(base.Object.GetBleedLiquid("blood-1000"));
		}

		// Token: 0x06007FC6 RID: 32710 RVA: 0x002F1B4B File Offset: 0x002EFD4B
		public string GetVersionOfBleedingForLiquidSpecification()
		{
			return Bleeding.GetVersionOfBleedingForLiquidSpecification(base.Object.GetBleedLiquid("blood-1000"));
		}

		// Token: 0x06007FC7 RID: 32711 RVA: 0x002F1B62 File Offset: 0x002EFD62
		[WishCommand(null, null, Command = "bleed")]
		public void HandleBleedWish()
		{
			IComponent<GameObject>.ThePlayer.ApplyEffect(new Bleeding("1d2-1", 20, null, true, false, false, false), null);
		}

		// Token: 0x040038D7 RID: 14551
		public string Damage = "1";

		// Token: 0x040038D8 RID: 14552
		public int SaveTarget = 20;

		// Token: 0x040038D9 RID: 14553
		public GameObject Owner;

		// Token: 0x040038DA RID: 14554
		public bool Stack = true;

		// Token: 0x040038DB RID: 14555
		public bool Internal;

		// Token: 0x040038DC RID: 14556
		public bool StartMessageUsePopup;

		// Token: 0x040038DD RID: 14557
		public bool StopMessageUsePopup;

		// Token: 0x040038DE RID: 14558
		public bool Bandaged;
	}
}
