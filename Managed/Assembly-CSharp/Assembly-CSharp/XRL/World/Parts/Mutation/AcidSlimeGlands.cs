using System;
using System.Collections.Generic;
using XRL.UI;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001404 RID: 5124
	[Serializable]
	public class AcidSlimeGlands : BaseMutation
	{
		// Token: 0x0600C6FB RID: 50939 RVA: 0x00488B40 File Offset: 0x00486D40
		public AcidSlimeGlands()
		{
			base.Type = "Physical";
		}

		// Token: 0x0600C6FC RID: 50940 RVA: 0x00488B53 File Offset: 0x00486D53
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandSpitAcid");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C6FD RID: 50941 RVA: 0x00488B68 File Offset: 0x00486D68
		public override string GetDescription()
		{
			return "You spit a puddle of corrosive acid.";
		}

		// Token: 0x0600C6FE RID: 50942 RVA: 0x00488B6F File Offset: 0x00486D6F
		public override string GetLevelText(int Level)
		{
			return "" + "Covers the area in acidic slime.\n" + "Area: 3x3\n" + "Range: 8\n" + "Cooldown: 10 rounds\n";
		}

		// Token: 0x0600C6FF RID: 50943 RVA: 0x00488B9E File Offset: 0x00486D9E
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("Area", "3x3", false, 0);
			stats.Set("Range", "8", false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), 10, 0);
		}

		// Token: 0x0600C700 RID: 50944 RVA: 0x00488BDA File Offset: 0x00486DDA
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == BeforeApplyDamageEvent.ID;
		}

		// Token: 0x0600C701 RID: 50945 RVA: 0x00488BF8 File Offset: 0x00486DF8
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= 8 && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && GameObject.Validate(E.Target) && E.Actor.HasLOSTo(E.Target, true, false, true, true, null))
			{
				E.Add("CommandSpitAcid", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C702 RID: 50946 RVA: 0x00488C5C File Offset: 0x00486E5C
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if (E.Object == this.ParentObject && E.Damage.IsAcidDamage())
			{
				NotifyTargetImmuneEvent.Send(E.Weapon, E.Object, E.Actor, E.Damage, this);
				E.Damage.Amount = 0;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C703 RID: 50947 RVA: 0x00488CB8 File Offset: 0x00486EB8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandSpitAcid")
			{
				List<Cell> list = base.PickBurst(1, 8, false, AllowVis.OnlyVisible, "Spit Acid");
				if (list == null)
				{
					return false;
				}
				using (List<Cell>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.DistanceTo(this.ParentObject) > 9)
						{
							if (this.ParentObject.IsPlayer())
							{
								Popup.ShowFail("That is out of range! (8 squares)", true, true, true);
							}
							return false;
						}
					}
				}
				this.ParentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_creature_liquid_spit", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				SlimeGlands.SlimeAnimation("&G", this.ParentObject.CurrentCell, list[0]);
				int num = 0;
				foreach (Cell cell in list)
				{
					if (num == 0 || 80.in100())
					{
						cell.AddObject("SaltyAcidPool", null, null, null, null);
					}
					num++;
				}
				base.DidX("spit", "a stream of corrosive acid", null, null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
				base.UseEnergy(1000, "Physical Mutation Acid Slime Glands");
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, 40, null, null, false);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C704 RID: 50948 RVA: 0x00488E4C File Offset: 0x0048704C
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C705 RID: 50949 RVA: 0x00488E58 File Offset: 0x00487058
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Spit Acid", "CommandSpitAcid", "Physical Mutations", null, "\u00ad", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C706 RID: 50950 RVA: 0x00488EA1 File Offset: 0x004870A1
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}
	}
}
