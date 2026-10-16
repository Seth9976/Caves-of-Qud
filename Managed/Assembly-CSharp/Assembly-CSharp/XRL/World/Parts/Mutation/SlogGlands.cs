using System;
using System.Collections.Generic;
using XRL.UI;
using XRL.World.Anatomy;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001423 RID: 5155
	[Serializable]
	public class SlogGlands : BaseMutation
	{
		// Token: 0x0600C8CE RID: 51406 RVA: 0x00492829 File Offset: 0x00490A29
		public SlogGlands()
		{
			base.Type = "Physical";
		}

		// Token: 0x0600C8CF RID: 51407 RVA: 0x0049283C File Offset: 0x00490A3C
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C8D0 RID: 51408 RVA: 0x0049283F File Offset: 0x00490A3F
		public override bool GeneratesEquipment()
		{
			return true;
		}

		// Token: 0x0600C8D1 RID: 51409 RVA: 0x00492844 File Offset: 0x00490A44
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("Range", SlogGlands.RANGE, false, 0);
			stats.Set("Area", "3x3", false, 0);
			stats.Set("KnockdownChance", "Strength / Agility vs. character level", false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), SlogGlands.COOLDOWN, 0);
		}

		// Token: 0x0600C8D2 RID: 51410 RVA: 0x004928A0 File Offset: 0x00490AA0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C8D3 RID: 51411 RVA: 0x004928C0 File Offset: 0x00490AC0
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= 10 && this.FindSpitVolume() != null && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && this.HasBilgeSphincter() && GameObject.Validate(E.Target) && E.Actor.HasLOSTo(E.Target, true, false, true, true, null))
			{
				E.Add("CommandSlog", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C8D4 RID: 51412 RVA: 0x00492933 File Offset: 0x00490B33
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("might", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C8D5 RID: 51413 RVA: 0x00492956 File Offset: 0x00490B56
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeApplyDamage");
			Registrar.Register("CommandSlog");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C8D6 RID: 51414 RVA: 0x00492976 File Offset: 0x00490B76
		public override string GetDescription()
		{
			return "You bear a sphincter-choked bilge hose that you use to slurp up nearby liquids and spew them at enemies, occasionally knocking them down.";
		}

		// Token: 0x0600C8D7 RID: 51415 RVA: 0x00492980 File Offset: 0x00490B80
		public override string GetLevelText(int Level)
		{
			return "" + "+6 Strength\n" + "+1 AV\n" + "+100 Acid Resistance\n" + "+300 reputation with mollusks\n" + "Bilge sphincter acts as a melee weapon.\n" + "+50 move speed when moving through tiles with 200+ drams of liquid\n" + "You can spew liquid from your tile into a nearby area.\n" + "Spew range: 10\n" + "Spew area: 3x3\n" + "Spew chance to knock the targets down: Strength/Agility save vs. character level\n" + "Spew cooldown: 10 rounds\n";
		}

		// Token: 0x0600C8D8 RID: 51416 RVA: 0x00492A00 File Offset: 0x00490C00
		private LiquidVolume FindSpitVolume()
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			GameObject gameObject = ((currentCell != null) ? currentCell.GetFirstObjectWithPart("LiquidVolume") : null);
			if (gameObject != null)
			{
				LiquidVolume liquidVolume = gameObject.LiquidVolume;
				if (liquidVolume.MaxVolume == -1 && liquidVolume.Volume > 0)
				{
					return liquidVolume;
				}
			}
			return null;
		}

		// Token: 0x0600C8D9 RID: 51417 RVA: 0x00492A4A File Offset: 0x00490C4A
		private GameObject FindBilgeSphincter()
		{
			Body body = this.ParentObject.Body;
			if (body == null)
			{
				return null;
			}
			return body.FindEquipmentOrDefaultByBlueprint("Bilge Sphincter");
		}

		// Token: 0x0600C8DA RID: 51418 RVA: 0x00492A67 File Offset: 0x00490C67
		private bool HasBilgeSphincter()
		{
			return this.FindBilgeSphincter() != null;
		}

		// Token: 0x0600C8DB RID: 51419 RVA: 0x00492A74 File Offset: 0x00490C74
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandSlog")
			{
				if (!this.HasBilgeSphincter())
				{
					if (this.ParentObject.IsPlayer())
					{
						Popup.ShowFail("Your bilge sphincter is missing.", true, true, true);
					}
					return false;
				}
				if (!this.ParentObject.CheckFrozen(false, false, false, null))
				{
					return false;
				}
				LiquidVolume liquidVolume = this.FindSpitVolume();
				if (liquidVolume == null)
				{
					if (this.ParentObject.IsPlayer())
					{
						Popup.ShowFail("There is no liquid here for you to spew.", true, true, true);
					}
					return false;
				}
				int statValue = this.ParentObject.GetStatValue("Level", 15);
				List<Cell> list = base.PickBurst(1, 10, false, AllowVis.OnlyVisible, "Spew");
				if (list == null)
				{
					return false;
				}
				using (List<Cell>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.DistanceTo(this.ParentObject) > 10)
						{
							if (this.ParentObject.IsPlayer())
							{
								Popup.ShowFail("That is out of range! (10 squares)", true, true, true);
							}
							return false;
						}
					}
				}
				base.UseEnergy(1000, "Physical Mutation Bilge Sphincter");
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, 10, null, null, false);
				SlimeGlands.SlimeAnimation("&w", this.ParentObject.CurrentCell, list[0]);
				List<LiquidVolume> list2 = new List<LiquidVolume>();
				int num = 0;
				foreach (Cell cell in list)
				{
					if (num == 0 || 80.in100())
					{
						GameObject gameObject = GameObject.Create("Water");
						LiquidVolume liquidVolume2 = gameObject.LiquidVolume;
						liquidVolume2.ComponentLiquids.Clear();
						foreach (KeyValuePair<string, int> keyValuePair in liquidVolume.ComponentLiquids)
						{
							liquidVolume2.ComponentLiquids.Add(keyValuePair.Key, keyValuePair.Value);
						}
						list2.Add(liquidVolume2);
						cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						num++;
					}
				}
				if (liquidVolume.Volume < list2.Count)
				{
					liquidVolume.MixWith(new LiquidVolume("slime", list2.Count - liquidVolume.Volume), null, null, null, false);
				}
				foreach (LiquidVolume liquidVolume3 in list2)
				{
					liquidVolume3.Volume = Math.Max(Math.Min(1000, liquidVolume.Volume) / num, 1);
					liquidVolume3.Update();
				}
				liquidVolume.UseDrams(1000);
				foreach (Cell cell2 in list)
				{
					foreach (GameObject gameObject2 in cell2.GetObjectsWithPart("Combat"))
					{
						if (gameObject2 != this.ParentObject && !gameObject2.MakeSave("Agility,Strength", statValue, null, null, "SlogGlands Knockdown", false, false, false, false, null))
						{
							gameObject2.ApplyEffect(new Prone(), null);
						}
					}
				}
				base.DidX("spew", "a pool of stinking liquid", "!", null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C8DC RID: 51420 RVA: 0x00492E38 File Offset: 0x00491038
		public void AddSphincterTo(BodyPart Part)
		{
			GameObject gameObject = Part.FindEquipmentOrDefaultByBlueprint("Bilge Sphincter");
			if (gameObject == null)
			{
				gameObject = GameObject.Create("Bilge Sphincter");
				gameObject.GetPart<Armor>().WornOn = Part.Type;
				gameObject.RequirePart<SlogGladsItem>(false);
			}
			bool flag = gameObject.EquipAsDefaultBehavior();
			if (flag && Part.DefaultBehavior != null)
			{
				if (Part.DefaultBehavior == gameObject)
				{
					return;
				}
				Part.DefaultBehavior = null;
			}
			if (!flag && Part.Equipped != null)
			{
				if (Part.Equipped == gameObject)
				{
					return;
				}
				if (Part.Equipped.CanBeUnequipped(null, null, false, true))
				{
					Part.ForceUnequip(true, false, false);
				}
			}
			if (!Part.Equip(gameObject, new int?(0), true, false, false, true))
			{
				base.CleanUpMutationEquipment(this.ParentObject, ref gameObject);
			}
		}

		// Token: 0x0600C8DD RID: 51421 RVA: 0x00492EEC File Offset: 0x004910EC
		public override bool Mutate(GameObject GO, int Level)
		{
			if (this.ParentObject.Body != null)
			{
				BodyPart bodyPart = Stinger.AddTail(GO, "Mutation::SlogGlands", this.ParentObject.Body.Anatomy.StartsWith("Slug"), true);
				if (bodyPart != null)
				{
					this.AddSphincterTo(bodyPart);
					this.ActivatedAbilityID = base.AddMyActivatedAbility("Spew", "CommandSlog", "Physical Mutations", "You slurp up nearby liquids and spew them with your bilge sphincter, occasionally knocking enemies prone.", "\u00ad", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
				}
			}
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C8DE RID: 51422 RVA: 0x00492F77 File Offset: 0x00491177
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			base.CleanUpMutationEquipment(GO, this.FindBilgeSphincter());
			Stinger.RemoveTail(GO, "Mutation::SlogGlands");
			return base.Unmutate(GO);
		}

		// Token: 0x04004E99 RID: 20121
		public const string EQUIPMENT_BLUEPRINT = "Bilge Sphincter";

		// Token: 0x04004E9A RID: 20122
		public const string MANAGER_ID = "Mutation::SlogGlands";

		// Token: 0x04004E9B RID: 20123
		public static readonly int COOLDOWN = 10;

		// Token: 0x04004E9C RID: 20124
		public static readonly int RANGE = 10;
	}
}
