using System;
using System.Collections.Generic;
using XRL.Language;
using XRL.UI;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x02000EB3 RID: 3763
	[Serializable]
	public class FabricateFromSelf : IPoweredPart
	{
		// Token: 0x060096FA RID: 38650 RVA: 0x0036C428 File Offset: 0x0036A628
		public FabricateFromSelf()
		{
			this.ChargeUse = 1000;
			this.IsBootSensitive = false;
			this.IsEMPSensitive = false;
			this.WorksOnSelf = true;
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x060096FB RID: 38651 RVA: 0x0036C4A8 File Offset: 0x0036A6A8
		private GameObject Sample
		{
			get
			{
				if (this._Sample == null)
				{
					this._Sample = GameObject.CreateSample(this.FabricateBlueprint);
				}
				return this._Sample;
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x060096FC RID: 38652 RVA: 0x0036C4CC File Offset: 0x0036A6CC
		private string AbilityDescription
		{
			get
			{
				if (this._AbilityDescription == null)
				{
					this._AbilityDescription = "Fabricate " + Grammar.Pluralize(this.Sample.GetDisplayName(int.MaxValue, null, null, true, false, false, false, false, false, true, false, false, true, false, false, false, null, false, false, false, false, null, false, null, false, true));
				}
				return this._AbilityDescription;
			}
		}

		// Token: 0x060096FD RID: 38653 RVA: 0x0036C530 File Offset: 0x0036A730
		public override bool SameAs(IPart p)
		{
			FabricateFromSelf fabricateFromSelf = p as FabricateFromSelf;
			return !(fabricateFromSelf.FabricateBlueprint != this.FabricateBlueprint) && !(fabricateFromSelf.BatchSize != this.BatchSize) && !(fabricateFromSelf.HitpointsPer != this.HitpointsPer) && !(fabricateFromSelf.Cooldown != this.Cooldown) && !(fabricateFromSelf.FabricateVerb != this.FabricateVerb) && !(fabricateFromSelf.FabricateAlternateSource != this.FabricateAlternateSource) && fabricateFromSelf.EnergyCost == this.EnergyCost && fabricateFromSelf.HitpointsThreshold == this.HitpointsThreshold && fabricateFromSelf.AIHitpointsThreshold == this.AIHitpointsThreshold && fabricateFromSelf.AIUseForAmmo == this.AIUseForAmmo && fabricateFromSelf.AIUseOffensively == this.AIUseOffensively && fabricateFromSelf.AIUseDefensively == this.AIUseDefensively && fabricateFromSelf.AIUsePassively == this.AIUsePassively && fabricateFromSelf.AIUseForThrowing == this.AIUseForThrowing && fabricateFromSelf.Continuous == this.Continuous && base.SameAs(p);
		}

		// Token: 0x060096FE RID: 38654 RVA: 0x0036C659 File Offset: 0x0036A859
		public override bool WantTurnTick()
		{
			return this.Continuous;
		}

		// Token: 0x060096FF RID: 38655 RVA: 0x0036C664 File Offset: 0x0036A864
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.Continuous && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.Activate(true);
			}
		}

		// Token: 0x06009700 RID: 38656 RVA: 0x0036C6AC File Offset: 0x0036A8AC
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.Set("FabricateVerb", this.FabricateVerb, false, 0);
			string text = this.Sample.GetDisplayName(int.MaxValue, null, null, true, false, false, false, false, false, true, false, false, true, false, false, false, null, false, false, false, false, null, false, null, false, true);
			if (this.BatchSize != "1")
			{
				text = Grammar.Pluralize(text);
				stats.Set("An", "", false, 0);
			}
			else
			{
				stats.Set("An", Grammar.IndefiniteArticleShouldBeAn(text) ? "an " : "a ", false, 0);
			}
			stats.Set("FabricateBlueprint", text, false, 0);
			stats.Set("FabricateSource", string.IsNullOrEmpty(this.FabricateAlternateSource) ? "the substance of your body" : this.FabricateAlternateSource, false, 0);
			stats.Set("BatchSize", this.BatchSize, false, 0);
			stats.Set("HitpointsPer", this.HitpointsPer, false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.Cooldown);
		}

		// Token: 0x06009701 RID: 38657 RVA: 0x0036C7C0 File Offset: 0x0036A9C0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetDefensiveAbilityListEvent.ID || ID == AIGetOffensiveAbilityListEvent.ID || ID == AIGetPassiveAbilityListEvent.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x06009702 RID: 38658 RVA: 0x0036C7EE File Offset: 0x0036A9EE
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009703 RID: 38659 RVA: 0x0036C814 File Offset: 0x0036AA14
		public override bool HandleEvent(AIGetPassiveAbilityListEvent E)
		{
			if (this.AIUsePassively && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && (this.AIHitpointsThreshold <= 0 || E.Actor.Stat("Hitpoints", 0) >= this.AIHitpointsThreshold) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.Add("ActivateFabricateFromSelf", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009704 RID: 38660 RVA: 0x0036C89C File Offset: 0x0036AA9C
		public override bool HandleEvent(AIGetDefensiveAbilityListEvent E)
		{
			if (this.AIUseDefensively && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && (this.AIHitpointsThreshold <= 0 || E.Actor.Stat("Hitpoints", 0) >= this.AIHitpointsThreshold) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.Add("ActivateFabricateFromSelf", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009705 RID: 38661 RVA: 0x0036C924 File Offset: 0x0036AB24
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if ((this.AIUseOffensively || this.AIUseForAmmo || this.AIUseForThrowing) && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && (this.AIHitpointsThreshold <= 0 || E.Actor.Stat("Hitpoints", 0) >= this.AIHitpointsThreshold) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				bool flag = false;
				if (!flag && this.AIUseOffensively)
				{
					flag = true;
				}
				if (!flag && this.AIUseForAmmo)
				{
					List<GameObject> missileWeapons = E.Actor.GetMissileWeapons(null, null);
					if (missileWeapons != null && missileWeapons.Count > 0)
					{
						bool flag2 = false;
						int num = 0;
						foreach (GameObject gameObject in missileWeapons)
						{
							MagazineAmmoLoader part = gameObject.GetPart<MagazineAmmoLoader>();
							if (part != null && !part.AmmoPart.IsNullOrEmpty() && this.Sample.HasPart(part.AmmoPart))
							{
								flag2 = true;
								int num2 = ((part.Ammo == null) ? 100 : (100 - part.Ammo.Count * 100 / part.MaxAmmo));
								if (num2 > num)
								{
									num = num2;
								}
							}
						}
						if (flag2 && num > 0)
						{
							flag = true;
						}
					}
				}
				if (!flag && this.AIUseForThrowing)
				{
					BodyPart firstBodyPart = E.Actor.GetFirstBodyPart("Thrown Weapon");
					if (firstBodyPart != null && firstBodyPart.Equipped == null)
					{
						flag = true;
					}
				}
				if (flag)
				{
					E.Add("ActivateFabricateFromSelf", 1, null, false, false, null, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009706 RID: 38662 RVA: 0x0036CAE0 File Offset: 0x0036ACE0
		public override void Initialize()
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility(this.AbilityDescription, "ActivateFabricateFromSelf", "Tinkering", null, "ö", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
		}

		// Token: 0x06009707 RID: 38663 RVA: 0x0036CB22 File Offset: 0x0036AD22
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ActivateFabricateFromSelf");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009708 RID: 38664 RVA: 0x0036CB38 File Offset: 0x0036AD38
		private bool Activate(bool Automatic = false)
		{
			if (base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				if (this.ParentObject.IsPlayer() && !Automatic)
				{
					Popup.ShowFail("Nothing happens.", true, true, true);
				}
				return false;
			}
			if (this.HitpointsThreshold > 0 && this.ParentObject.Stat("Hitpoints", 0) < this.HitpointsThreshold)
			{
				if (this.ParentObject.IsPlayer() && !Automatic)
				{
					Popup.ShowFail("Your health is too weak to do that.", true, true, true);
				}
				return false;
			}
			int num = this.BatchSize.RollCached();
			if (num < 1)
			{
				if (this.ParentObject.IsPlayer() && !Automatic)
				{
					Popup.ShowFail("Nothing happens.", true, true, true);
				}
				return false;
			}
			GameObject gameObject = GameObject.CreateUnmodified(this.FabricateBlueprint);
			if (num > 1 && gameObject.Stacker != null && gameObject.CanGenerateStacked())
			{
				gameObject.Stacker.StackCount = num;
				this.ParentObject.ReceiveObject(gameObject, false, null);
			}
			else
			{
				this.ParentObject.ReceiveObject(gameObject, false, null);
				for (int i = 1; i < num; i++)
				{
					gameObject = GameObject.CreateUnmodified(this.FabricateBlueprint);
					this.ParentObject.ReceiveObject(gameObject, false, null);
				}
			}
			this.ParentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_fabricate", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			if (base.Visible())
			{
				IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
				{
					this.ParentObject.Does(this.FabricateVerb, int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
					" ",
					(num == 1) ? gameObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) : (Grammar.Cardinal(num) + " " + this.Sample.GetPluralName(false, false, false, false)),
					" from ",
					this.FabricateAlternateSource.IsNullOrEmpty() ? ("the substance of " + this.ParentObject.its + " body") : this.FabricateAlternateSource,
					"."
				}), null, true);
			}
			if (this.AIUseForThrowing && !this.ParentObject.IsPlayer())
			{
				BodyPart firstBodyPart = this.ParentObject.GetFirstBodyPart("Thrown Weapon");
				if (firstBodyPart != null && firstBodyPart.Equipped == null)
				{
					this.ParentObject.FireEvent(Event.New("CommandEquipObject", "Object", gameObject, "BodyPart", firstBodyPart));
				}
			}
			int num2 = 0;
			for (int j = 0; j < num; j++)
			{
				num2 += this.HitpointsPer.RollCached();
			}
			if (num2 > 0)
			{
				GameObject parentObject = this.ParentObject;
				int num3 = num2;
				GameObject parentObject2 = this.ParentObject;
				parentObject.TakeDamage(num3, "from using " + (this.FabricateAlternateSource.IsNullOrEmpty() ? (this.ParentObject.GetPronounProvider(false).PossessiveAdjective + "body") : this.FabricateAlternateSource) + " as raw materials.", "Fabrication", null, null, parentObject2, null, null, null, null, false, false, false, false, false, false, false, false, false, 5, null);
			}
			base.ConsumeCharge(null, null);
			if (this.EnergyCost > 0)
			{
				this.ParentObject.UseEnergy(this.EnergyCost, "Physical Ability Fabricate", null, null, false);
			}
			if (!this.Cooldown.IsNullOrEmpty())
			{
				int num4 = this.Cooldown.RollCached();
				if (num4 > 0)
				{
					base.CooldownMyActivatedAbility(this.ActivatedAbilityID, num4, null, null, false);
				}
			}
			return true;
		}

		// Token: 0x06009709 RID: 38665 RVA: 0x0036CEE9 File Offset: 0x0036B0E9
		public override bool FireEvent(Event E)
		{
			return (!(E.ID == "ActivateFabricateFromSelf") || this.Activate(false)) && base.FireEvent(E);
		}

		// Token: 0x04003EB1 RID: 16049
		public string FabricateBlueprint = "PhysicalObject";

		// Token: 0x04003EB2 RID: 16050
		public string BatchSize = "1d6";

		// Token: 0x04003EB3 RID: 16051
		public string HitpointsPer = "2d4";

		// Token: 0x04003EB4 RID: 16052
		public string Cooldown = "5d10";

		// Token: 0x04003EB5 RID: 16053
		public string FabricateVerb = "fabricate";

		// Token: 0x04003EB6 RID: 16054
		public string FabricateAlternateSource;

		// Token: 0x04003EB7 RID: 16055
		public int EnergyCost = 1000;

		// Token: 0x04003EB8 RID: 16056
		public int HitpointsThreshold;

		// Token: 0x04003EB9 RID: 16057
		public int AIHitpointsThreshold;

		// Token: 0x04003EBA RID: 16058
		public bool AIUseForAmmo;

		// Token: 0x04003EBB RID: 16059
		public bool AIUseOffensively;

		// Token: 0x04003EBC RID: 16060
		public bool AIUseDefensively;

		// Token: 0x04003EBD RID: 16061
		public bool AIUsePassively;

		// Token: 0x04003EBE RID: 16062
		public bool AIUseForThrowing;

		// Token: 0x04003EBF RID: 16063
		public bool Continuous;

		// Token: 0x04003EC0 RID: 16064
		public Guid ActivatedAbilityID = Guid.Empty;

		// Token: 0x04003EC1 RID: 16065
		[NonSerialized]
		private string _AbilityDescription;

		// Token: 0x04003EC2 RID: 16066
		[NonSerialized]
		private GameObject _Sample;
	}
}
