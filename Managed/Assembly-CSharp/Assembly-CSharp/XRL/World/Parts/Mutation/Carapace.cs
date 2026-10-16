using System;
using System.Text;
using XRL.Rules;
using XRL.UI;
using XRL.World.Anatomy;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200142E RID: 5166
	[Serializable]
	public class Carapace : BaseDefaultEquipmentMutation
	{
		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x0600C96B RID: 51563 RVA: 0x00495868 File Offset: 0x00493A68
		public string BlueprintName
		{
			get
			{
				return this.Variant.Coalesce("Carapace");
			}
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x0600C96C RID: 51564 RVA: 0x0049587A File Offset: 0x00493A7A
		public GameObjectBlueprint Blueprint
		{
			get
			{
				if (this._Blueprint == null)
				{
					this._Blueprint = GameObjectFactory.Factory.GetBlueprint(this.BlueprintName);
				}
				return this._Blueprint;
			}
		}

		// Token: 0x0600C96D RID: 51565 RVA: 0x004958A0 File Offset: 0x00493AA0
		public override IPart DeepCopy(GameObject Parent, Func<GameObject, GameObject> MapInv)
		{
			Carapace carapace = base.DeepCopy(Parent, MapInv) as Carapace;
			carapace.CarapaceObject = null;
			return carapace;
		}

		// Token: 0x0600C96F RID: 51567 RVA: 0x004958C9 File Offset: 0x00493AC9
		public override bool GeneratesEquipment()
		{
			return true;
		}

		// Token: 0x0600C970 RID: 51568 RVA: 0x004958CC File Offset: 0x00493ACC
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("CarapaceName", base.GetDisplayName(true).ToLower(), false, 0);
		}

		// Token: 0x0600C971 RID: 51569 RVA: 0x004958E7 File Offset: 0x00493AE7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetDefensiveAbilityListEvent.ID || ID == SingletonEvent<UseEnergyEvent>.ID;
		}

		// Token: 0x0600C972 RID: 51570 RVA: 0x00495908 File Offset: 0x00493B08
		public override bool HandleEvent(AIGetDefensiveAbilityListEvent E)
		{
			int num = Math.Max(this.ParentObject.baseHitpoints - E.Distance, 1);
			if (!this.Tight && this.ACModifier >= 1 && E.Actor.HasStat("Hitpoints") && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && Stat.Random(0, num) > E.Actor.hitpoints)
			{
				E.Add("CommandTightenCarapace", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C973 RID: 51571 RVA: 0x0049598C File Offset: 0x00493B8C
		public override bool HandleEvent(UseEnergyEvent E)
		{
			if (this.Tight && (E.Type == null || (!E.Type.Contains("Pass") && !E.Type.Contains("Mental") && !E.Type.Contains("Carapace"))))
			{
				this.Loosen(true);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C974 RID: 51572 RVA: 0x004959ED File Offset: 0x00493BED
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginMove");
			Registrar.Register("CommandTightenCarapace");
			Registrar.Register("IsMobile");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C975 RID: 51573 RVA: 0x00495A18 File Offset: 0x00493C18
		public override string GetDescription()
		{
			return this.Blueprint.GetTag("VariantDescription", "").Coalesce("You are protected by a durable carapace.");
		}

		// Token: 0x0600C976 RID: 51574 RVA: 0x00495A39 File Offset: 0x00493C39
		public static int GetAVModifier(int Level)
		{
			return 3 + (int)Math.Floor((double)Level / 2.0);
		}

		// Token: 0x0600C977 RID: 51575 RVA: 0x00495A4F File Offset: 0x00493C4F
		public static int GetDVModifier(int Level)
		{
			return -2;
		}

		// Token: 0x0600C978 RID: 51576 RVA: 0x00495A53 File Offset: 0x00493C53
		public static int GetHeatResistance(int Level)
		{
			return 5 + 5 * Level;
		}

		// Token: 0x0600C979 RID: 51577 RVA: 0x00495A5A File Offset: 0x00493C5A
		public static int GetColdResistance(int Level)
		{
			return 5 + 5 * Level;
		}

		// Token: 0x0600C97A RID: 51578 RVA: 0x00495A64 File Offset: 0x00493C64
		public override string GetLevelText(int Level)
		{
			string cachedDisplayNameStripped = this.Blueprint.CachedDisplayNameStripped;
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.AppendSigned(Carapace.GetAVModifier(Level), "rules").Append(" AV\n").AppendSigned(Carapace.GetDVModifier(Level), "rules")
				.Append(" DV\n")
				.AppendSigned(Carapace.GetHeatResistance(Level), "rules")
				.Append(" Heat Resistance\n")
				.AppendSigned(Carapace.GetColdResistance(Level), "rules")
				.Append(" Cold Resistance");
			string text;
			int num;
			if (this.Blueprint.TryGetPartParameter<string>("AddsRep", "Faction", out text) && this.Blueprint.TryGetPartParameter<int>("AddsRep", "Value", out num))
			{
				AddsRep.AppendDescription(stringBuilder, text, num, null, null, false, false);
			}
			stringBuilder.Append("\nYou may tighten your ").Append(cachedDisplayNameStripped).Append(" to receive double the AV bonus at a -2 DV penalty as long as you remain still.")
				.Append("\nCannot wear body armor.");
			return Event.FinalizeString(stringBuilder);
		}

		// Token: 0x0600C97B RID: 51579 RVA: 0x00495B58 File Offset: 0x00493D58
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandTightenCarapace")
			{
				this.Loosen(false);
				if (this.ACModifier < 1)
				{
					return this.ParentObject.Fail("You fail to tighten " + ((this.CarapaceObject == null) ? "your carapace" : this.ParentObject.poss(this.CarapaceObject, true, null)) + ".");
				}
				base.UseEnergy(1000, "Physical Mutation Tighten Carapace");
				this.Tighten(true);
				The.Core.RenderBase(true, false);
			}
			else if (E.ID == "BeginMove" && this.Tight && !E.HasFlag("Forced") && E.GetStringParameter("Type", null) != "Teleporting")
			{
				this.Loosen(true);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C97C RID: 51580 RVA: 0x00495C40 File Offset: 0x00493E40
		public void Tighten(bool Message = false)
		{
			if (!this.Tight)
			{
				this.Tight = true;
				this.TightFactor = this.ACModifier;
				this.ParentObject.Statistics["AV"].Bonus += this.TightFactor;
				this.ParentObject.Statistics["DV"].Penalty += 2;
				this.ParentObject.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_positiveVitality", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				if (Message)
				{
					if (this.CarapaceObject == null)
					{
						MetricsManager.LogError(this.ParentObject.DebugName + " had no CarapaceObject for Carapace tighten message");
						if (this.ParentObject.IsPlayer())
						{
							Popup.Show("You tighten your carapace. Your AV increases by {{G|" + this.TightFactor.ToString() + "}}.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
							return;
						}
						base.DidX("tighten", this.ParentObject.its + " carapace", null, null, null, null, null, false, false, null, null, false, false, false, true, false, null);
						return;
					}
					else
					{
						if (this.ParentObject.IsPlayer())
						{
							Popup.Show(string.Concat(new string[]
							{
								"You tighten ",
								this.ParentObject.poss(this.CarapaceObject, true, null),
								". Your AV increases by {{G|",
								this.TightFactor.ToString(),
								"}}."
							}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
							return;
						}
						base.DidXToY("tighten", this.CarapaceObject, null, null, null, null, null, null, false, false, false, false, false, null, this.ParentObject, null, false, false, false, true, false, null);
					}
				}
			}
		}

		// Token: 0x0600C97D RID: 51581 RVA: 0x00495E10 File Offset: 0x00494010
		public void Loosen(bool Message = false)
		{
			if (this.Tight)
			{
				this.ParentObject.Statistics["AV"].Bonus -= this.TightFactor;
				this.ParentObject.Statistics["DV"].Penalty -= 2;
				this.Tight = false;
				this.TightFactor = 0;
				this.ParentObject.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_negativeVitality", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				if (Message)
				{
					if (this.CarapaceObject == null)
					{
						MetricsManager.LogError(this.ParentObject.DebugName + " had no CarapaceObject for Carapace loosen message");
						if (this.ParentObject.IsPlayer())
						{
							Popup.Show(this.ParentObject.Poss("carapace", true) + " loosens. Your AV decreases by {{R|" + this.ACModifier.ToString() + "}}.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
							return;
						}
						IComponent<GameObject>.EmitMessage(this.ParentObject, this.ParentObject.Poss("carapace", true) + " loosens.", ' ', false, false, false, null, null);
						return;
					}
					else
					{
						if (this.ParentObject.IsPlayer())
						{
							Popup.Show(this.CarapaceObject.Does("loosen", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + ". Your AV decreases by {{R|" + this.ACModifier.ToString() + "}}.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
							return;
						}
						IComponent<GameObject>.EmitMessage(this.ParentObject, this.CarapaceObject.Does("loosen", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + ".", ' ', false, false, false, null, null);
					}
				}
			}
		}

		// Token: 0x0600C97E RID: 51582 RVA: 0x00496008 File Offset: 0x00494208
		public override bool ChangeLevel(int NewLevel)
		{
			this.Loosen(false);
			this.ACModifier = 3 + (int)Math.Floor(base.Level / 2);
			this.DVModifier = -2;
			if (this.ResistanceMod > 0)
			{
				if (this.ParentObject.HasStat("HeatResistance"))
				{
					this.ParentObject.GetStat("HeatResistance").Bonus -= this.ResistanceMod;
				}
				if (this.ParentObject.HasStat("ColdResistance"))
				{
					this.ParentObject.GetStat("ColdResistance").Bonus -= this.ResistanceMod;
				}
				this.ResistanceMod = 0;
			}
			this.ResistanceMod = 5 + 5 * base.Level;
			if (this.ParentObject.HasStat("HeatResistance"))
			{
				this.ParentObject.GetStat("HeatResistance").Bonus += this.ResistanceMod;
			}
			if (this.ParentObject.HasStat("ColdResistance"))
			{
				this.ParentObject.GetStat("ColdResistance").Bonus += this.ResistanceMod;
			}
			if (this.CarapaceObject != null)
			{
				Armor part = this.CarapaceObject.GetPart<Armor>();
				part.AV = this.ACModifier;
				part.DV = this.DVModifier;
			}
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C97F RID: 51583 RVA: 0x00496164 File Offset: 0x00494364
		public override void OnRegenerateDefaultEquipment(Body body)
		{
			this.AddCarapaceTo(body.GetPartByID(this.bodyID, false));
		}

		// Token: 0x0600C980 RID: 51584 RVA: 0x00496179 File Offset: 0x00494379
		public override void SetVariant(string Variant)
		{
			base.SetVariant(Variant);
			this._Blueprint = null;
		}

		// Token: 0x0600C981 RID: 51585 RVA: 0x0049618C File Offset: 0x0049438C
		public void AddCarapaceTo(BodyPart body)
		{
			if (body == null)
			{
				return;
			}
			if (this.CarapaceObject == null)
			{
				this.CarapaceObject = GameObjectFactory.Factory.CreateObject(this.Blueprint, 0, 0, null, null, null, null, null);
			}
			if (body.Equipped == this.CarapaceObject)
			{
				return;
			}
			body.ForceUnequip(true, false, false);
			body.ParentBody.ParentObject.ForceEquipObject(this.CarapaceObject, body, true, new int?(0));
		}

		// Token: 0x0600C982 RID: 51586 RVA: 0x004961FC File Offset: 0x004943FC
		public override bool Mutate(GameObject GO, int Level)
		{
			Body body = GO.Body;
			if (body != null)
			{
				BodyPart body2 = body.GetBody();
				this.bodyID = body2.ID;
				this.AddCarapaceTo(body2);
				this.ActivatedAbilityID = base.AddMyActivatedAbility("Tighten " + base.GetDisplayName(true), "CommandTightenCarapace", "Physical Mutations", null, "ï", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			}
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C983 RID: 51587 RVA: 0x00496278 File Offset: 0x00494478
		public override bool Unmutate(GameObject GO)
		{
			this.Loosen(false);
			if (this.ResistanceMod > 0)
			{
				if (this.ParentObject.HasStat("HeatResistance"))
				{
					this.ParentObject.GetStat("HeatResistance").Bonus -= this.ResistanceMod;
				}
				if (this.ParentObject.HasStat("ColdResistance"))
				{
					this.ParentObject.GetStat("ColdResistance").Bonus -= this.ResistanceMod;
				}
				this.ResistanceMod = 0;
			}
			base.CleanUpMutationEquipment(GO, ref this.CarapaceObject);
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004EB8 RID: 20152
		public int ACModifier;

		// Token: 0x04004EB9 RID: 20153
		public int DVModifier;

		// Token: 0x04004EBA RID: 20154
		public int ResistanceMod;

		// Token: 0x04004EBB RID: 20155
		public bool Tight;

		// Token: 0x04004EBC RID: 20156
		public int TightFactor;

		// Token: 0x04004EBD RID: 20157
		public GameObject CarapaceObject;

		// Token: 0x04004EBE RID: 20158
		[NonSerialized]
		protected GameObjectBlueprint _Blueprint;

		// Token: 0x04004EBF RID: 20159
		public int bodyID = int.MinValue;
	}
}
