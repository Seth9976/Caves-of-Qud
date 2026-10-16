using System;
using System.Collections.Generic;
using XRL.Language;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02001140 RID: 4416
	[Serializable]
	public class ModGigantic : IModification
	{
		// Token: 0x0600AF2B RID: 44843 RVA: 0x00409F4B File Offset: 0x0040814B
		public ModGigantic()
		{
		}

		// Token: 0x0600AF2C RID: 44844 RVA: 0x00409F53 File Offset: 0x00408153
		public ModGigantic(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600AF2D RID: 44845 RVA: 0x00409F5C File Offset: 0x0040815C
		public ModGigantic(ModGigantic Source)
		{
		}

		// Token: 0x0600AF2E RID: 44846 RVA: 0x00409F64 File Offset: 0x00408164
		public override int GetModificationSlotUsage()
		{
			return 0;
		}

		// Token: 0x0600AF2F RID: 44847 RVA: 0x00409F67 File Offset: 0x00408167
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.NameForStatus = "MassEnhancement";
		}

		// Token: 0x0600AF30 RID: 44848 RVA: 0x00409F7C File Offset: 0x0040817C
		public override void ApplyModification(GameObject Object)
		{
			MeleeWeapon part = Object.GetPart<MeleeWeapon>();
			if (part != null)
			{
				part.AdjustDamage(3);
			}
			EnergyCell part2 = Object.GetPart<EnergyCell>();
			if (part2 != null)
			{
				part2.MaxCharge *= 2;
				part2.Charge *= 2;
			}
			DeploymentGrenade part3 = Object.GetPart<DeploymentGrenade>();
			if (part3 != null)
			{
				part3.Radius *= 2;
			}
			EMPGrenade part4 = Object.GetPart<EMPGrenade>();
			if (part4 != null)
			{
				part4.Radius *= 2;
			}
			FlashbangGrenade part5 = Object.GetPart<FlashbangGrenade>();
			if (part5 != null)
			{
				part5.Radius *= 2;
			}
			GasGrenade part6 = Object.GetPart<GasGrenade>();
			if (part6 != null)
			{
				part6.Density *= 2;
			}
			GravityGrenade part7 = Object.GetPart<GravityGrenade>();
			if (part7 != null)
			{
				part7.Force *= 2;
				part7.Radius *= 2;
			}
			HEGrenade part8 = Object.GetPart<HEGrenade>();
			if (part8 != null)
			{
				part8.Force *= 2;
			}
			PhaseGrenade part9 = Object.GetPart<PhaseGrenade>();
			if (part9 != null)
			{
				part9.Radius *= 2;
			}
			SunderGrenade part10 = Object.GetPart<SunderGrenade>();
			if (part10 != null)
			{
				part10.Radius *= 2;
			}
			ThermalGrenade part11 = Object.GetPart<ThermalGrenade>();
			if (part11 != null)
			{
				part11.Radius *= 2;
			}
			TimeDilationGrenade part12 = Object.GetPart<TimeDilationGrenade>();
			if (part12 != null)
			{
				part12.Range *= 2;
			}
			ModGigantic.CheckLiquidVolume(Object);
			base.IncreaseDifficultyAndComplexityIfComplex(1, 1, null);
		}

		// Token: 0x0600AF31 RID: 44849 RVA: 0x0040A0E8 File Offset: 0x004082E8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetCleaveAmountEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetIntrinsicWeightEvent.ID || ID == GetIntrinsicValueEvent.ID || ID == PooledEvent<GetMissileWeaponPerformanceEvent>.ID || ID == PooledEvent<GetThrownWeaponPerformanceEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == PooledEvent<GetSlotsRequiredEvent>.ID || ID == PooledEvent<GetTonicDosageEvent>.ID || ID == PooledEvent<ModificationAppliedEvent>.ID || ID == WasDerivedFromEvent.ID;
		}

		// Token: 0x0600AF32 RID: 44850 RVA: 0x0040A161 File Offset: 0x00408361
		public override bool HandleEvent(WasDerivedFromEvent E)
		{
			E.ApplyToEach(delegate(GameObject obj)
			{
				obj.RemovePart<ModGigantic>();
			});
			E.ApplyToEach(delegate(GameObject obj)
			{
				obj.AddPart<ModGigantic>(new ModGigantic(this), true, false);
			});
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF33 RID: 44851 RVA: 0x0040A1A4 File Offset: 0x004083A4
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.Object.HasTagOrProperty("ModGiganticNoDisplayName") && (!E.Object.HasTagOrProperty("ModGiganticNoUnknownDisplayName") || E.Understood()) && !E.Object.HasProperName)
			{
				E.ApplySizeAdjective("gigantic", 30, -20);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF34 RID: 44852 RVA: 0x0040A200 File Offset: 0x00408400
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!E.Object.HasTagOrProperty("ModGiganticNoShortDescription"))
			{
				E.Postfix.AppendRules(this.GetInstanceDescription());
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF35 RID: 44853 RVA: 0x0040A22D File Offset: 0x0040842D
		public override bool HandleEvent(GetIntrinsicWeightEvent E)
		{
			E.Weight += (double)this.GetAddedWeight();
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF36 RID: 44854 RVA: 0x0040A24A File Offset: 0x0040844A
		public override bool HandleEvent(GetIntrinsicValueEvent E)
		{
			if (E.Object.GetIntProperty("Currency", 0) > 0)
			{
				E.Value *= 3.333333;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF37 RID: 44855 RVA: 0x0040A280 File Offset: 0x00408480
		public override bool HandleEvent(GetMissileWeaponPerformanceEvent E)
		{
			if (this.IsObjectActivePartSubject(E.Subject) && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				DieRoll damageRoll = E.GetDamageRoll();
				if (damageRoll != null)
				{
					damageRoll.AdjustResult(3);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF38 RID: 44856 RVA: 0x0040A2DC File Offset: 0x004084DC
		public override bool HandleEvent(GetThrownWeaponPerformanceEvent E)
		{
			if (this.IsObjectActivePartSubject(E.Object) && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.DamageModifier += 3;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF39 RID: 44857 RVA: 0x0040A334 File Offset: 0x00408534
		public override bool HandleEvent(GetCleaveAmountEvent E)
		{
			if (this.IsObjectActivePartSubject(E.Object) && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.Amount++;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF3A RID: 44858 RVA: 0x0040A38C File Offset: 0x0040858C
		public override bool HandleEvent(GetSlotsRequiredEvent E)
		{
			if (this.IsObjectActivePartSubject(E.Object) && !E.Actor.IsGiganticCreature && !E.Object.IsNatural())
			{
				E.Increases++;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF3B RID: 44859 RVA: 0x0040A3CB File Offset: 0x004085CB
		public override bool HandleEvent(GetTonicDosageEvent E)
		{
			if (E.Object == this.ParentObject)
			{
				E.Dosage++;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF3C RID: 44860 RVA: 0x0040A3F0 File Offset: 0x004085F0
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("might", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF3D RID: 44861 RVA: 0x0040A413 File Offset: 0x00408613
		public override bool HandleEvent(ModificationAppliedEvent E)
		{
			ModGigantic.CheckLiquidVolume(this.ParentObject);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AF3E RID: 44862 RVA: 0x0040A427 File Offset: 0x00408627
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AF3F RID: 44863 RVA: 0x0040A42A File Offset: 0x0040862A
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("GetSlamMultiplier");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AF40 RID: 44864 RVA: 0x0040A43F File Offset: 0x0040863F
		public override bool FireEvent(Event E)
		{
			if (E.ID == "GetSlamMultiplier")
			{
				E.SetParameter("Multiplier", E.GetIntParameter("Multiplier", 0) + 1);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600AF41 RID: 44865 RVA: 0x0040A474 File Offset: 0x00408674
		public static string GetDescription(int Tier)
		{
			return "Gigantic: This item is much heavier than usual and can only be equipped by gigantic creatures.";
		}

		// Token: 0x0600AF42 RID: 44866 RVA: 0x0040A47C File Offset: 0x0040867C
		public static string GetDescription(int Tier, GameObject Object)
		{
			if (Object == null)
			{
				return ModGigantic.GetDescription(Tier);
			}
			string text = "item";
			List<List<string>> list = new List<List<string>>();
			List<List<string>> list2 = new List<List<string>>();
			if (Object.LiquidVolume != null)
			{
				list2.Add(new List<string> { "hold", "twice as much liquid" });
			}
			if (Object.HasPart<EnergyCell>())
			{
				list2.Add(new List<string> { "have", "twice the energy capacity" });
			}
			if (Object.HasPartDescendedFrom<IGrenade>())
			{
				list2.Add(new List<string> { "have", "twice as large a radius of effect" });
			}
			if (Object.HasPart<Tonic>())
			{
				list2.Add(new List<string> { "contain", "double the tonic dosage" });
			}
			if (Object.GetIntProperty("Currency", 0) > 0)
			{
				list2.Add(new List<string> { null, "much more valuable" });
			}
			bool flag = Object.EquipAsDefaultBehavior();
			if (!flag)
			{
				list2.Add(new List<string> { null, "much heavier than usual" });
			}
			MeleeWeapon part = Object.GetPart<MeleeWeapon>();
			bool flag2 = flag || Object.IsEntirelyFloating();
			if (part != null && Object.HasTagOrProperty("ShowMeleeWeaponStats"))
			{
				text = "weapon";
				list.Add(new List<string> { "have", "+3 damage" });
				if (part.Skill == "Cudgel")
				{
					list.Add(new List<string>
					{
						null,
						"twice as effective when you Slam with " + Object.them
					});
				}
				else if (part.Skill == "Axe")
				{
					list.Add(new List<string> { "cleave", "for -3 AV" });
				}
				if (!flag2)
				{
					if (Object.UsesSlots == null)
					{
						list2.Add(new List<string>
						{
							"",
							Object.it + " must be wielded " + (Object.UsesTwoSlots ? "four" : "two") + "-handed by non-gigantic creatures"
						});
					}
					else
					{
						list2.Add(new List<string> { "", "can only be equipped by gigantic creatures" });
					}
				}
			}
			else if (Object.HasPart<MissileWeapon>())
			{
				list.Add(new List<string> { "have", "+3 damage" });
				if (!flag2)
				{
					if (Object.UsesSlots == null)
					{
						list2.Add(new List<string>
						{
							"",
							Object.it + " must be wielded " + (Object.UsesTwoSlots ? "four" : "two") + "-handed by non-gigantic creatures"
						});
					}
					else
					{
						list2.Add(new List<string> { "", "can only be equipped by gigantic creatures" });
					}
				}
			}
			else if (Object.HasPart<ThrownWeapon>())
			{
				text = "weapon";
				if (!Object.HasPartDescendedFrom<IGrenade>())
				{
					list.Add(new List<string> { "have", "+3 damage" });
				}
				if (!flag2)
				{
					list2.Add(new List<string> { "", "can only be equipped by gigantic creatures" });
				}
			}
			else if ((Object.HasPart<Armor>() || Object.HasPart<Shield>()) && !flag2)
			{
				list2.Add(new List<string> { "", "can only be equipped by gigantic creatures" });
			}
			if (Object.HasPart<DiggingTool>() || Object.HasPart<Drill>())
			{
				list.Add(new List<string> { "dig", "twice as fast" });
			}
			if (list.Count == 0)
			{
				List<string> list3 = new List<string>();
				foreach (List<string> list4 in list2)
				{
					list3.Add(ModGigantic.GetProcessedItem(list4, false, list2, Object));
				}
				return string.Concat(new string[]
				{
					"Gigantic: ",
					Object.IsPlural ? ("These " + Grammar.Pluralize(text)) : ("This " + text),
					" ",
					Grammar.MakeAndList(list3, true),
					"."
				});
			}
			List<string> list5 = new List<string>();
			List<string> list6 = new List<string>();
			foreach (List<string> list7 in list)
			{
				list5.Add(ModGigantic.GetProcessedItem(list7, false, list, Object));
			}
			foreach (List<string> list8 in list2)
			{
				list6.Add(ModGigantic.GetProcessedItem(list8, true, list2, Object));
			}
			return string.Concat(new string[]
			{
				"Gigantic: ",
				Object.IsPlural ? ("These " + Grammar.Pluralize(text)) : ("This " + text),
				" ",
				Grammar.MakeAndList(list5, true),
				". ",
				Grammar.MakeAndList(list6, true),
				"."
			});
		}

		// Token: 0x0600AF43 RID: 44867 RVA: 0x0040AA14 File Offset: 0x00408C14
		private static string GetProcessedItem(List<string> item, bool second, List<List<string>> items, GameObject obj)
		{
			if (item[0] == "")
			{
				if (second && item == items[0])
				{
					return obj.It + " " + item[1];
				}
				return item[1];
			}
			else if (item[0] == null)
			{
				if (second && item == items[0])
				{
					return obj.Itis + " " + item[1];
				}
				if (item != items[0])
				{
					bool flag = true;
					using (List<List<string>>.Enumerator enumerator = items.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current[0] != null)
							{
								flag = false;
								break;
							}
						}
					}
					if (flag)
					{
						return item[1];
					}
				}
				return obj.GetVerb("are", false, false, false, true, false) + " " + item[1];
			}
			else
			{
				if (second && item == items[0])
				{
					return obj.It + obj.GetVerb(item[0], true, false, false, true, false) + " " + item[1];
				}
				return obj.GetVerb(item[0], false, false, false, true, false) + " " + item[1];
			}
		}

		// Token: 0x0600AF44 RID: 44868 RVA: 0x0040AB6C File Offset: 0x00408D6C
		public string GetInstanceDescription()
		{
			return ModGigantic.GetDescription(this.Tier, this.ParentObject);
		}

		// Token: 0x0600AF45 RID: 44869 RVA: 0x0040AB80 File Offset: 0x00408D80
		public int GetAddedWeight()
		{
			if (!this.HaveAddedWeight)
			{
				try
				{
					this.AddedWeight = this.ParentObject.GetBlueprint(true).GetPartParameter<int>("Physics", "Weight", 0) * 4;
				}
				catch
				{
				}
				if (this.AddedWeight < 4)
				{
					this.AddedWeight = 4;
				}
				this.HaveAddedWeight = true;
			}
			return this.AddedWeight;
		}

		// Token: 0x0600AF46 RID: 44870 RVA: 0x0040ABEC File Offset: 0x00408DEC
		private static void CheckLiquidVolume(GameObject Object)
		{
			if (!Object.HasIntProperty("ModGiganticLiquidVolumeDone"))
			{
				LiquidVolume liquidVolume = Object.LiquidVolume;
				if (liquidVolume != null)
				{
					liquidVolume.MaxVolume *= 2;
					if (!Object.HasContext())
					{
						liquidVolume.Volume *= 2;
					}
					Object.SetIntProperty("ModGiganticLiquidVolumeDone", 1, false);
				}
			}
		}

		// Token: 0x040047FD RID: 18429
		[NonSerialized]
		private bool HaveAddedWeight;

		// Token: 0x040047FE RID: 18430
		[NonSerialized]
		private int AddedWeight;
	}
}
