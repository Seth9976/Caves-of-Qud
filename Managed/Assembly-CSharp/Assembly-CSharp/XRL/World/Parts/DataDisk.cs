using System;
using System.Collections.Generic;
using System.Text;
using XRL.Language;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Parts.Skill;
using XRL.World.Tinkering;

namespace XRL.World.Parts
{
	// Token: 0x02001012 RID: 4114
	[Serializable]
	public class DataDisk : IPart
	{
		// Token: 0x0600A3DB RID: 41947 RVA: 0x003BDAF4 File Offset: 0x003BBCF4
		public override bool SameAs(IPart p)
		{
			DataDisk dataDisk = p as DataDisk;
			return dataDisk.Data == this.Data && dataDisk.TargetTier == this.TargetTier && dataDisk.TargetTechTier == this.TargetTechTier && dataDisk.TargetTinkerTier == this.TargetTinkerTier && !(dataDisk.TargetBlueprint != this.TargetBlueprint) && !(dataDisk.TargetTinkerCategory != this.TargetTinkerCategory) && !(dataDisk.TargetType != this.TargetType) && base.SameAs(p);
		}

		// Token: 0x0600A3DC RID: 41948 RVA: 0x003BDB8E File Offset: 0x003BBD8E
		public override bool CanGenerateStacked()
		{
			return false;
		}

		// Token: 0x0600A3DD RID: 41949 RVA: 0x003BDB91 File Offset: 0x003BBD91
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == GetShortDescriptionEvent.ID || ID == InventoryActionEvent.ID || ID == ObjectCreatedEvent.ID;
		}

		// Token: 0x0600A3DE RID: 41950 RVA: 0x003BDBC8 File Offset: 0x003BBDC8
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			DataDisk.SB.Clear();
			if (this.Data == null)
			{
				if (this.TargetBlueprint.IsNullOrEmpty())
				{
					this.ObjectName = "invalid data";
				}
				else
				{
					this.ObjectName = "invalid blueprint: " + this.TargetBlueprint;
				}
			}
			else if (this.Data.Type == "Build")
			{
				try
				{
					if (this.ObjectName == null)
					{
						if (this.Data.Blueprint == null)
						{
							this.ObjectName = "invalid blueprint: " + this.Data.Blueprint;
						}
						else
						{
							this.ObjectName = TinkeringHelpers.TinkeredItemShortDisplayName(this.Data.Blueprint);
						}
					}
				}
				catch (Exception)
				{
					this.ObjectName = "error:" + this.Data.Blueprint;
				}
				if (E.AsIfKnown || (E.Understood() && The.Player != null && (The.Player.HasSkill("Tinkering") || Scanning.HasScanningFor(The.Player, Scanning.Scan.Tech))))
				{
					DataDisk.SB.Append(": {{C|").Append(this.ObjectName).Append("}} <");
					DataDisk.Cost.Clear();
					DataDisk.Cost.Import(TinkerItem.GetBitCostFor(this.Data.Blueprint));
					ModifyBitCostEvent.Process(The.Player, DataDisk.Cost, "DataDisk");
					DataDisk.Cost.ToStringBuilder(DataDisk.SB);
					DataDisk.SB.Append('>');
				}
			}
			else if (this.Data.Type == "Mod")
			{
				this.ObjectName = "[{{W|Item mod}}] - {{C|" + this.Data.DisplayName + "}}";
				if (E.AsIfKnown || (E.Understood() && The.Player != null && (The.Player.HasSkill("Tinkering") || Scanning.HasScanningFor(The.Player, Scanning.Scan.Tech))))
				{
					DataDisk.SB.Append(": ").Append(this.ObjectName);
				}
			}
			if (DataDisk.SB.Length > 0)
			{
				E.AddBase(DataDisk.SB.ToString(), 5, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3DF RID: 41951 RVA: 0x003BDE1C File Offset: 0x003BC01C
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.Data != null)
			{
				if (this.Data.Type == "Mod")
				{
					E.Postfix.Append("\nAdds item modification: ").Append(ItemModding.GetModificationDescription(this.Data.Blueprint, 0));
				}
				else
				{
					GameObject gameObject = GameObject.CreateSample(this.Data.Blueprint);
					if (gameObject != null)
					{
						TinkeringHelpers.StripForTinkering(gameObject);
						TinkerItem part = gameObject.GetPart<TinkerItem>();
						Description part2 = gameObject.GetPart<Description>();
						E.Postfix.Append("\n{{rules|Creates:}} ");
						if (part != null && part.NumberMade > 1)
						{
							E.Postfix.Append(Grammar.Cardinal(part.NumberMade)).Append(' ').Append(Grammar.Pluralize(gameObject.DisplayNameOnlyDirect));
						}
						else
						{
							E.Postfix.Append(gameObject.DisplayNameOnlyDirect);
						}
						E.Postfix.Append("\n");
						if (part2 != null)
						{
							E.Postfix.Append('\n').Append(part2._Short);
						}
						gameObject.Obliterate(null, false, null);
					}
				}
				E.Postfix.Append("\n\n{{rules|Requires:}} ").Append(this.GetRequiredSkillHumanReadable());
				if (TinkerData.RecipeKnown(this.Data))
				{
					E.Postfix.Append("\n\n{{rules|You already know this recipe.}}");
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3E0 RID: 41952 RVA: 0x003BDF7C File Offset: 0x003BC17C
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.Data != null && this.ParentObject.Understood())
			{
				if (this.Data.Type == "Build")
				{
					E.AddAction("Build", "build", "BuildFromDataDisk", null, 'b', false, (this.ParentObject.InInventory == E.Actor) ? 200 : 0, 0, false, false, false, false, true, null, false);
				}
				E.AddAction("Learn", "learn", "LearnFromDataDisk", null, 'n', false, TinkerData.RecipeKnown(this.Data) ? 0 : 300, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3E1 RID: 41953 RVA: 0x003BE038 File Offset: 0x003BC238
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "LearnFromDataDisk")
			{
				if (TinkerData.RecipeKnown(this.Data))
				{
					E.Actor.Fail("You already know that recipe!");
				}
				else if (!E.Actor.HasSkill(this.GetRequiredSkill()))
				{
					E.Actor.Fail("You don't have the required skill: " + this.GetRequiredSkillHumanReadable() + "!");
				}
				else
				{
					E.Actor.PlayWorldOrUISound("Sounds/Interact/sfx_interact_dataDisk_learn", null);
					if (this.Data.Type == "Mod")
					{
						Popup.Show("You learn the item modification {{W|" + this.Data.DisplayName + "}}.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
					else
					{
						GameObject gameObject = GameObject.CreateSample(this.Data.Blueprint);
						gameObject.MakeUnderstood(false);
						if (E.Actor.IsPlayer())
						{
							Popup.Show("You learn to build " + (gameObject.IsPlural ? gameObject.DisplayNameOnlyDirect : Grammar.Pluralize(gameObject.DisplayNameOnlyDirect)) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
						gameObject.Destroy(null, false, false, null);
					}
					TinkerData.KnownRecipes.Add(this.Data);
					this.ParentObject.Destroy(null, false, false, null);
				}
			}
			else if (E.Command == "BuildFromDataDisk")
			{
				E.Actor.GetPart<Tinkering>();
				BitLocker bitLocker = E.Actor.RequirePart<BitLocker>(false);
				if (!(this.Data.Type != "Build"))
				{
					if (!E.Actor.HasSkill(this.GetRequiredSkill()))
					{
						E.Actor.Fail("You don't have the required skill: " + this.GetRequiredSkillHumanReadable() + "!");
					}
					else
					{
						string bitCostFor = TinkerItem.GetBitCostFor(this.Data.Blueprint);
						if (!bitLocker.HasBits(bitCostFor))
						{
							E.Actor.Fail("You don't have the required <" + BitType.GetString(bitCostFor) + "> bits! You have:\n\n" + bitLocker.GetBitsString());
						}
						else
						{
							bool flag = E.Actor.AreHostilesNearby();
							if (flag && E.Actor.FireEvent("CombatPreventsTinkering"))
							{
								E.Actor.Fail("You can't tinker with hostiles nearby!");
								return false;
							}
							if (!E.Actor.CanMoveExtremities(null, true, false, true))
							{
								return false;
							}
							Inventory inventory = E.Actor.Inventory;
							GameObject gameObject2 = null;
							if (!this.Data.Ingredient.IsNullOrEmpty())
							{
								foreach (string text in this.Data.Ingredient.Split(',', StringSplitOptions.None))
								{
									gameObject2 = inventory.FindObjectByBlueprint(text);
									if (gameObject2 != null)
									{
										break;
									}
								}
								if (gameObject2 == null)
								{
									string text2 = "";
									foreach (string text3 in this.Data.Ingredient.Split(',', StringSplitOptions.None))
									{
										if (text2 != "")
										{
											text2 += " or ";
										}
										GameObject gameObject3 = GameObject.CreateSample(text3);
										text2 += gameObject3.GetDisplayName(int.MaxValue, null, null, false, false, false, false, true, false, true, true, false, true, false, false, false, null, false, false, false, false, new bool?(true), false, null, false, true);
										gameObject3.Obliterate(null, false, null);
									}
									E.Actor.Fail("You don't have the required ingredient: " + text2 + "!");
									goto IL_055A;
								}
							}
							if (!this.Data.Ingredient.IsNullOrEmpty())
							{
								gameObject2.SplitStack(1, E.Actor, false);
								if (!inventory.FireEvent(Event.New("CommandRemoveObject", "Object", gameObject2)))
								{
									E.Actor.Fail("You cannot use the ingredient!");
									goto IL_055A;
								}
							}
							bitLocker.UseBits(TinkerItem.GetBitCostFor(this.Data.Blueprint));
							GameObject gameObject4 = GameObject.CreateSample(this.Data.Blueprint);
							TinkeringHelpers.ProcessTinkeredItem(gameObject4, E.Actor);
							TinkerItem part = gameObject4.GetPart<TinkerItem>();
							GameObject gameObject5 = null;
							int j = 0;
							int num = Math.Max(part.NumberMade, 1);
							while (j < num)
							{
								gameObject5 = GameObject.Create(this.Data.Blueprint);
								TinkeringHelpers.ProcessTinkeredItem(gameObject5, E.Actor);
								inventory.AddObject(gameObject5, null, false, false, true, null);
								j++;
							}
							IComponent<GameObject>.PlayUISound("Sounds/Interact/sfx_interact_dataDisk_build", 1f, false, SoundRequest.SoundEffectType.None);
							if (part.NumberMade > 1)
							{
								string displayName = gameObject4.GetDisplayName(int.MaxValue, null, null, false, false, false, false, true, false, true, true, false, true, false, false, false, null, false, false, false, false, new bool?(true), false, null, false, true);
								base.DidX("tinker", "up " + Grammar.Cardinal(part.NumberMade) + " " + Grammar.Pluralize(displayName), "!", null, null, null, null, false, false, null, null, false, false, false, true, false, null);
							}
							else
							{
								base.DidXToY("tinker", "up", gameObject5, null, "!", null, null, null, null, false, false, true, false, false, null, null, null, false, false, false, true, false, null);
							}
							E.Actor.UseEnergy(1000, "Skill Tinkering Data Disk Build", null, null, false);
							if (flag)
							{
								E.RequestInterfaceExit();
							}
							gameObject4.Obliterate(null, false, null);
						}
					}
				}
			}
			IL_055A:
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3E2 RID: 41954 RVA: 0x003BE5A8 File Offset: 0x003BC7A8
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (this.Data == null)
			{
				if (!this.TargetBlueprint.IsNullOrEmpty())
				{
					using (List<TinkerData>.Enumerator enumerator = TinkerData.TinkerRecipes.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							TinkerData tinkerData = enumerator.Current;
							if (tinkerData.Blueprint == this.TargetBlueprint)
							{
								this.Data = tinkerData;
							}
						}
						goto IL_0115;
					}
				}
				int num = 0;
				TinkerData tinkerData2 = null;
				int num2 = 0;
				int maximumDataScore = this.GetMaximumDataScore();
				int num3 = maximumDataScore * 10;
				do
				{
					TinkerData randomElement = TinkerData.TinkerRecipes.GetRandomElement<TinkerData>(null);
					int num4 = this.GetDataScore(randomElement);
					if (num4 > num2)
					{
						tinkerData2 = randomElement;
						num2 = num4;
					}
				}
				while (num2 < maximumDataScore && ++num < num3);
				if (num2 < maximumDataScore)
				{
					foreach (TinkerData tinkerData3 in new List<TinkerData>(TinkerData.TinkerRecipes).ShuffleInPlace<TinkerData>(null))
					{
						int num4 = this.GetDataScore(tinkerData3);
						if (num4 > num2)
						{
							tinkerData2 = tinkerData3;
							num2 = num4;
							if (num2 >= maximumDataScore)
							{
								break;
							}
						}
					}
				}
				this.Data = tinkerData2;
			}
			IL_0115:
			if (this.Data != null)
			{
				Commerce part = this.ParentObject.GetPart<Commerce>();
				if (part != null)
				{
					if (this.Data.Cost.Contains("M"))
					{
						part.Value = 450.0;
					}
					else if (this.Data.Cost.Contains("Y"))
					{
						part.Value = 400.0;
					}
					else if (this.Data.Cost.Contains("W"))
					{
						part.Value = 350.0;
					}
					else if (this.Data.Cost.Contains("K"))
					{
						part.Value = 300.0;
					}
					else if (this.Data.Cost.Contains("c"))
					{
						part.Value = 250.0;
					}
					else if (this.Data.Cost.Contains("b"))
					{
						part.Value = 200.0;
					}
					else if (this.Data.Cost.Contains("g"))
					{
						part.Value = 150.0;
					}
					else if (this.Data.Cost.Contains("r"))
					{
						part.Value = 100.0;
					}
					else
					{
						part.Value = 50.0;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A3E3 RID: 41955 RVA: 0x003BE874 File Offset: 0x003BCA74
		public static string GetRequiredSkill(int Tier)
		{
			if (Tier <= 3)
			{
				return "Tinkering_Tinker1";
			}
			if (Tier <= 6)
			{
				return "Tinkering_Tinker2";
			}
			return "Tinkering_Tinker3";
		}

		// Token: 0x0600A3E4 RID: 41956 RVA: 0x003BE88F File Offset: 0x003BCA8F
		public string GetRequiredSkill()
		{
			TinkerData data = this.Data;
			return DataDisk.GetRequiredSkill((data != null) ? data.Tier : 0);
		}

		// Token: 0x0600A3E5 RID: 41957 RVA: 0x003BE8A8 File Offset: 0x003BCAA8
		public static string GetRequiredSkillHumanReadable(int Tier)
		{
			if (Tier <= 3)
			{
				return "Tinker I";
			}
			if (Tier <= 6)
			{
				return "Tinker II";
			}
			return "Tinker III";
		}

		// Token: 0x0600A3E6 RID: 41958 RVA: 0x003BE8C3 File Offset: 0x003BCAC3
		public string GetRequiredSkillHumanReadable()
		{
			TinkerData data = this.Data;
			return DataDisk.GetRequiredSkillHumanReadable((data != null) ? data.Tier : 0);
		}

		// Token: 0x0600A3E7 RID: 41959 RVA: 0x003BE8DC File Offset: 0x003BCADC
		private int GetDataScore(TinkerData Data)
		{
			int num = 0;
			if (Data.Type == "Build")
			{
				GameObjectBlueprint blueprintIfExists = GameObjectFactory.Factory.GetBlueprintIfExists(Data.Blueprint);
				if (blueprintIfExists == null)
				{
					return num;
				}
				if (this.TargetTier > 0)
				{
					num += 8 - Math.Abs(blueprintIfExists.Tier - this.TargetTier);
				}
				if (this.TargetTechTier > 0)
				{
					num += 8 - Math.Abs(blueprintIfExists.TechTier - this.TargetTechTier);
				}
			}
			if (this.TargetTinkerTier > 0 && Data.Tier == this.TargetTinkerTier)
			{
				num += 8 - Math.Abs(Data.Tier - this.TargetTinkerTier);
			}
			if (!this.TargetTinkerCategory.IsNullOrEmpty() && Data.Category == this.TargetTinkerCategory)
			{
				num += 16;
			}
			if (!this.TargetType.IsNullOrEmpty() && this.TargetType == Data.Type)
			{
				num += 32;
			}
			return num + 1;
		}

		// Token: 0x0600A3E8 RID: 41960 RVA: 0x003BE9D0 File Offset: 0x003BCBD0
		private int GetMaximumDataScore()
		{
			int num = 1;
			if (this.TargetTier > 0)
			{
				num += 8;
			}
			if (this.TargetTechTier > 0)
			{
				num += 8;
			}
			if (this.TargetTinkerTier > 0)
			{
				num += 8;
			}
			if (!this.TargetTinkerCategory.IsNullOrEmpty())
			{
				num += 16;
			}
			if (!this.TargetType.IsNullOrEmpty())
			{
				num += 32;
			}
			return num;
		}

		// Token: 0x040043D3 RID: 17363
		public TinkerData Data;

		// Token: 0x040043D4 RID: 17364
		public int TargetTier;

		// Token: 0x040043D5 RID: 17365
		public int TargetTechTier;

		// Token: 0x040043D6 RID: 17366
		public int TargetTinkerTier;

		// Token: 0x040043D7 RID: 17367
		public string TargetBlueprint;

		// Token: 0x040043D8 RID: 17368
		public string TargetTinkerCategory;

		// Token: 0x040043D9 RID: 17369
		public string TargetType;

		// Token: 0x040043DA RID: 17370
		[NonSerialized]
		public string ObjectName;

		// Token: 0x040043DB RID: 17371
		[NonSerialized]
		private static StringBuilder SB = new StringBuilder();

		// Token: 0x040043DC RID: 17372
		[NonSerialized]
		private static BitCost Cost = new BitCost();
	}
}
