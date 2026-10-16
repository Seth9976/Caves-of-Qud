using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using XRL.Language;
using XRL.UI;
using XRL.World.AI;
using XRL.World.AI.GoalHandlers;
using XRL.World.Capabilities;
using XRL.World.Parts.Skill;
using XRL.World.Tinkering;

namespace XRL.World.Parts
{
	// Token: 0x02001238 RID: 4664
	[HasGameBasedStaticCache]
	[Serializable]
	public class TinkerItem : IPart
	{
		// Token: 0x0600B96E RID: 47470 RVA: 0x0043B210 File Offset: 0x00439410
		public static string GetBitCostFor(string Blueprint)
		{
			string text;
			if (!TinkerItem.BitCostMap.TryGetValue(Blueprint, out text))
			{
				GameObjectBlueprint blueprint = GameObjectFactory.Factory.GetBlueprint(Blueprint);
				text = ((blueprint != null) ? blueprint.GetPartParameter<string>("TinkerItem", "Bits", null) : null);
				if (text.IsNullOrEmpty())
				{
					MetricsManager.LogError("Obtaining bit cost for invalid blueprint:" + Blueprint);
					return "1";
				}
				text = BitType.ToRealBits(text, Blueprint);
				TinkerItem.BitCostMap.Add(Blueprint, text);
			}
			if (text == null || text.Contains("0"))
			{
				GameObjectBlueprint blueprint2 = GameObjectFactory.Factory.GetBlueprint(Blueprint);
				text = ((blueprint2 != null) ? blueprint2.GetPartParameter<string>("TinkerItem", "Bits", null) : null);
				if (text.IsNullOrEmpty())
				{
					MetricsManager.LogError("Obtaining bit cost for invalid blueprint:" + Blueprint);
					return "1";
				}
				text = BitType.ToRealBits(text, Blueprint);
				TinkerItem.BitCostMap[Blueprint] = text;
			}
			return text;
		}

		// Token: 0x0600B96F RID: 47471 RVA: 0x0043B2EC File Offset: 0x004394EC
		private static TinkerData GetModRecipe(ModEntry Mod)
		{
			TinkerData tinkerData;
			if (!TinkerItem.ModRecipes.TryGetValue(Mod.Part, out tinkerData))
			{
				int i = 0;
				int count = TinkerData.TinkerRecipes.Count;
				while (i < count)
				{
					TinkerData tinkerData2 = TinkerData.TinkerRecipes[i];
					if (tinkerData2.DisplayName == Mod.TinkerDisplayName && tinkerData2.Type == "Mod")
					{
						tinkerData = tinkerData2;
						TinkerItem.ModRecipes[Mod.Part] = tinkerData;
						break;
					}
					i++;
				}
			}
			return tinkerData;
		}

		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x0600B971 RID: 47473 RVA: 0x0043B384 File Offset: 0x00439584
		// (set) Token: 0x0600B970 RID: 47472 RVA: 0x0043B36B File Offset: 0x0043956B
		public string Bits
		{
			get
			{
				string text;
				if (TinkerItem.BitCostMap.TryGetValue(this.ActiveBlueprint, out text))
				{
					TinkerItem.Cost.Clear();
					TinkerItem.Cost.Import(text);
					if (GlobalConfig.GetBoolSetting("IncludeModBitsInItemBits", false))
					{
						ModifyBitCostEvent.Process(The.Player, TinkerItem.Cost, "Disassemble");
						int modificationSlotsUsed = this.ParentObject.GetModificationSlotsUsed();
						if (modificationSlotsUsed > 0)
						{
							List<IModification> partsDescendedFrom = this.ParentObject.GetPartsDescendedFrom<IModification>();
							if (!partsDescendedFrom.IsNullOrEmpty<IModification>())
							{
								int num = modificationSlotsUsed - this.ParentObject.GetIntProperty("NoCostMods", 0) + this.ParentObject.GetTechTier();
								int i = 0;
								int count = partsDescendedFrom.Count;
								while (i < count)
								{
									IModification modification = partsDescendedFrom[i];
									ModEntry modEntry;
									if (modification.GetModificationSlotUsage() > 0 && ModificationFactory.ModsByPart.TryGetValue(modification.Name, out modEntry))
									{
										TinkerData modRecipe = TinkerItem.GetModRecipe(modEntry);
										if (modRecipe != null)
										{
											TinkerItem.ModBitCost.Clear();
											TinkerItem.ModBitCost.Increment(BitType.TierBits[Tier.Constrain(modRecipe.Tier)]);
											int num2 = Tier.Constrain(num);
											num--;
											TinkerItem.ModBitCost.Increment(BitType.TierBits[num2]);
											ModifyBitCostEvent.Process(The.Player, TinkerItem.ModBitCost, "DisassembleMod");
											TinkerItem.ModBitCost.AddTo(TinkerItem.Cost);
										}
									}
									i++;
								}
							}
						}
					}
					else
					{
						ModifyBitCostEvent.Process(The.Player, TinkerItem.Cost, "Disassemble");
					}
					return TinkerItem.Cost.ToBits();
				}
				return "0";
			}
			set
			{
				TinkerItem.BitSpecMap.TryAdd(this.ParentObject.Blueprint, value);
			}
		}

		// Token: 0x17000B37 RID: 2871
		// (get) Token: 0x0600B973 RID: 47475 RVA: 0x0043B541 File Offset: 0x00439741
		// (set) Token: 0x0600B972 RID: 47474 RVA: 0x0043B51D File Offset: 0x0043971D
		public string ActiveBlueprint
		{
			get
			{
				if (!this.SubstituteBlueprint.IsNullOrEmpty())
				{
					return this.SubstituteBlueprint;
				}
				return this.ParentObject.Blueprint;
			}
			set
			{
				if (value == this.ParentObject.Blueprint)
				{
					this.SubstituteBlueprint = null;
					return;
				}
				this.SubstituteBlueprint = value;
			}
		}

		// Token: 0x0600B974 RID: 47476 RVA: 0x0043B564 File Offset: 0x00439764
		public override void Initialize()
		{
			string text;
			if (!TinkerItem.BitCostMap.ContainsKey(this.ActiveBlueprint) && TinkerItem.BitSpecMap.TryGetValue(this.ParentObject.Blueprint, out text))
			{
				TinkerItem.BitCostMap.Add(this.ActiveBlueprint, BitType.ToRealBits(text, this.ActiveBlueprint));
			}
			for (int i = 0; i < this.Bits.Length; i++)
			{
				BitType bitType;
				if (BitType.BitMap.TryGetValue(this.Bits[i], out bitType))
				{
					if (bitType.Level > this.BuildTier)
					{
						this.BuildTier = bitType.Level;
					}
				}
				else
				{
					MetricsManager.LogWarning(string.Format("Bit '{0}' unrecognized from {1} on Bits: \"{2}\" char {3} ", new object[]
					{
						this.Bits[i],
						this.ParentObject.DebugName,
						this.Bits,
						i
					}));
				}
			}
		}

		// Token: 0x0600B975 RID: 47477 RVA: 0x0043B654 File Offset: 0x00439854
		public static TinkerData LoadBlueprint(GameObjectBlueprint Blueprint)
		{
			if (Blueprint.HasTag("BaseObject", false) || Blueprint.HasTag("NoDataDisk", false))
			{
				return null;
			}
			GamePartBlueprint part = Blueprint.GetPart("TinkerItem");
			if (part == null)
			{
				return null;
			}
			if (!part.GetParameter<bool>("CanBuild", false))
			{
				return null;
			}
			if (!part.GetParameter<string>("SubstituteBlueprint", "").IsNullOrEmpty())
			{
				return null;
			}
			TinkerData tinkerData = new TinkerData();
			tinkerData.Blueprint = Blueprint.Name;
			tinkerData.Cost = TinkerItem.GetBitCostFor(Blueprint.Name);
			int num = 0;
			for (int i = 0; i < tinkerData.Cost.Length; i++)
			{
				if (BitType.BitMap[tinkerData.Cost[i]].Level > num)
				{
					num = BitType.BitMap[tinkerData.Cost[i]].Level;
				}
			}
			tinkerData.Tier = part.GetParameter<int>("BuildTier", num);
			tinkerData.Type = "Build";
			tinkerData.Category = Blueprint.GetTag("TinkerCategory", "utility");
			tinkerData.Ingredient = part.GetParameter<string>("Ingredient", null);
			tinkerData.DisplayName = TinkeringHelpers.TinkeredItemDisplayName(Blueprint.Name);
			return tinkerData;
		}

		// Token: 0x0600B976 RID: 47478 RVA: 0x0043B788 File Offset: 0x00439988
		public static void SaveGlobals(SerializationWriter Writer)
		{
			Writer.Write<string, string>(TinkerItem.BitCostMap);
			Writer.WriteOptimized(TinkerData.KnownRecipes.Count);
			foreach (TinkerData tinkerData in TinkerData.KnownRecipes)
			{
				Writer.WriteComposite<TinkerData>(tinkerData);
			}
		}

		// Token: 0x0600B977 RID: 47479 RVA: 0x0043B7F8 File Offset: 0x004399F8
		public static void LoadGlobals(SerializationReader Reader)
		{
			TinkerItem.BitCostMap = Reader.ReadDictionary<string, string>();
			int num = Reader.ReadOptimizedInt32();
			TinkerData.KnownRecipes = new List<TinkerData>(num);
			for (int i = 0; i < num; i++)
			{
				TinkerData.KnownRecipes.Add(Reader.ReadComposite<TinkerData>());
			}
		}

		// Token: 0x0600B978 RID: 47480 RVA: 0x0043B83E File Offset: 0x00439A3E
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600B979 RID: 47481 RVA: 0x0043B841 File Offset: 0x00439A41
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B97A RID: 47482 RVA: 0x0043B844 File Offset: 0x00439A44
		public void Awake()
		{
			this.objectsToEnable.FirstOrDefault<GameObject>(delegate(GameObject o)
			{
				o.SetActive(true);
				return false;
			});
		}

		// Token: 0x0600B97B RID: 47483 RVA: 0x0043B871 File Offset: 0x00439A71
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetInventoryActionsAlwaysEvent.ID || ID == PooledEvent<GetScanTypeEvent>.ID || ID == PooledEvent<IdleQueryEvent>.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600B97C RID: 47484 RVA: 0x0043B8A7 File Offset: 0x00439AA7
		public override bool HandleEvent(GetScanTypeEvent E)
		{
			if (E.Object == this.ParentObject && (this.CanBuild || this.CanDisassemble))
			{
				E.ScanType = Scanning.Scan.Tech;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B97D RID: 47485 RVA: 0x0043B8D8 File Offset: 0x00439AD8
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if ((E.AsIfKnown || (The.Player != null && The.Player.HasSkill("Tinkering_Disassemble"))) && this.CanDisassemble && 1160 < E.Cutoff && this.ParentObject != null && !this.ParentObject.IsCombat && E.Context != "Tinkering" && E.Understood() && E.Object != null && !E.Object.HasProperName)
			{
				E.AddTag("{{y|<{{|" + BitType.GetString(this.Bits) + "}}>}}", 60);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B97E RID: 47486 RVA: 0x0043B98C File Offset: 0x00439B8C
		public override bool HandleEvent(GetInventoryActionsAlwaysEvent E)
		{
			if (E.Actor.IsPlayer() && this.CanBeDisassembled(E.Actor))
			{
				int num = -1;
				if ((base.HasTag("DefaultDisassemble") || TinkeringHelpers.ConsiderScrap(this.ParentObject, E.Actor)) && !E.Object.IsImportant())
				{
					num = 200;
				}
				E.AddAction("Disassemble", "disassemble", "Disassemble", null, 'm', false, num, 0, false, false, false, false, true, null, false);
				E.AddAction("Disassemble All", "disassemble all", "DisassembleAll", null, 'm', false, -1, -1, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B97F RID: 47487 RVA: 0x0043BA3C File Offset: 0x00439C3C
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if ((E.Command == "Disassemble" || E.Command == "DisassembleAll") && E.Actor.IsPlayer() && this.CanBeDisassembled(E.Actor))
			{
				int count = this.ParentObject.Count;
				bool flag = E.Command == "DisassembleAll";
				bool flag2 = flag && count > 1;
				List<Action<GameObject>> list = null;
				if (!E.Actor.InSameOrAdjacentCellTo(this.ParentObject))
				{
					E.Actor.ShowFailure(string.Concat(new string[]
					{
						"You need be near ",
						this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
						" in order to disassemble ",
						this.ParentObject.them,
						"."
					}));
					return false;
				}
				if (E.Actor.IsPlayer())
				{
					if (flag && flag2 && AutoAct.ShouldHostilesInterrupt("o", null, false, false, true))
					{
						Popup.ShowFail("You cannot use disassemble all with hostiles nearby.", true, true, true);
						return false;
					}
					if (this.ParentObject.IsInStasis())
					{
						Popup.ShowFail("You cannot seem to affect " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " in any way.", true, true, true);
						return false;
					}
					if (!this.ParentObject.Owner.IsNullOrEmpty() && !this.ParentObject.HasPropertyOrTag("DontWarnOnDisassemble"))
					{
						if (Popup.ShowYesNoCancel(string.Concat(new string[]
						{
							this.ParentObject.The,
							this.ParentObject.DisplayNameOnly,
							flag2 ? "are" : this.ParentObject.Is,
							" not owned by you. Are you sure you want to disassemble ",
							flag2 ? "them" : this.ParentObject.them,
							"?"
						}), "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
						{
							return false;
						}
						if (list == null)
						{
							list = new List<Action<GameObject>>();
						}
						list.Add(new Action<GameObject>(this.ParentObject.Physics.BroadcastForHelp));
					}
					if (E.Item.IsImportant())
					{
						if (!E.Item.ConfirmUseImportant(null, "disassemble", null, flag2 ? count : 1))
						{
							return false;
						}
					}
					else if (TinkerItem.ConfirmBeforeDisassembling(this.ParentObject) && Popup.ShowYesNoCancel("Are you sure you want to disassemble " + (flag2 ? ("all the " + (this.ParentObject.GetGender(false).Plural ? this.ParentObject.ShortDisplayName : Grammar.Pluralize(this.ParentObject.ShortDisplayName))) : this.ParentObject.t(2147483647, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false)) + "?", "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
					{
						return false;
					}
					GameObject inInventory = this.ParentObject.InInventory;
					if (inInventory != null && inInventory != E.Actor && !inInventory.Owner.IsNullOrEmpty() && inInventory.Owner != this.ParentObject.Owner && !inInventory.HasPropertyOrTag("DontWarnOnDisassemble"))
					{
						if (Popup.ShowYesNoCancel(string.Concat(new string[]
						{
							inInventory.Does("are", 2147483647, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
							" not owned by you. Are you sure you want to disassemble ",
							flag2 ? "items" : this.ParentObject.an(2147483647, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
							" inside ",
							inInventory.them,
							"?"
						}), "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
						{
							return false;
						}
						if (list == null)
						{
							list = new List<Action<GameObject>>();
						}
						list.Add(new Action<GameObject>(inInventory.Physics.BroadcastForHelp));
					}
				}
				Disassembly disassembly = AutoAct.Action as Disassembly;
				if (disassembly != null)
				{
					disassembly.Enqueue(this.ParentObject, new int?(flag2 ? count : 1), E.Auto, list);
				}
				else
				{
					Disassembly disassembly2 = new Disassembly(this.ParentObject, flag2 ? count : 1, E.Auto, list, 1000);
					if (flag2 || E.Auto)
					{
						AutoAct.Action = disassembly2;
						E.RequestInterfaceExit();
						E.Actor.ForfeitTurn(true);
					}
					else
					{
						disassembly2.EnergyCostPer = 0;
						disassembly2.Continue();
						if (disassembly2.CanComplete())
						{
							disassembly2.Complete();
						}
						else
						{
							disassembly2.Interrupt();
						}
						disassembly2.End();
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B980 RID: 47488 RVA: 0x0043BF20 File Offset: 0x0043A120
		public override bool HandleEvent(IdleQueryEvent E)
		{
			if (this.ParentObject.CurrentCell != null && E.Actor.Owns(this.ParentObject) && Tinkering_Repair.IsRepairableBy(this.ParentObject, E.Actor, null, null, null))
			{
				GameObject who = E.Actor;
				who.Brain.PushGoal(new DelegateGoal(delegate(GoalHandler h)
				{
					if (who.DistanceTo(this.ParentObject) <= 1)
					{
						InventoryActionEvent.Check(who, who, this.ParentObject, "Repair", false, false, false, false, false, 0, 0, 0, null, null, null, null);
					}
					h.FailToParent();
				}, null, null));
				who.Brain.PushGoal(new MoveTo(this.ParentObject, false, false, 1, false, false, false, -1, -1, 95));
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B981 RID: 47489 RVA: 0x0043BFDC File Offset: 0x0043A1DC
		public bool CanBeDisassembled(GameObject who = null)
		{
			bool flag = false;
			if (who != null && who.HasSkill("Tinkering_ReverseEngineer"))
			{
				foreach (TinkerData tinkerData in TinkerData.TinkerRecipes)
				{
					if (tinkerData.Type == "Mod" && this.ParentObject.HasPart(tinkerData.PartName))
					{
						flag = true;
						break;
					}
				}
			}
			return (this.CanDisassemble || flag) && !this.ParentObject.IsCombat && (who == null || who.HasSkill("Tinkering_Disassemble")) && (!this.ParentObject.HasRegisteredEvent("CanBeDisassembled") || this.ParentObject.FireEvent("CanBeDisassembled"));
		}

		// Token: 0x0600B982 RID: 47490 RVA: 0x0043C0B8 File Offset: 0x0043A2B8
		public static bool ConfirmBeforeDisassembling(GameObject obj)
		{
			return obj.Equipped != null || obj.Implantee != null || obj.GetIntProperty("Renamed", 0) == 1 || !obj.FireEvent("ConfirmBeforeDisassembling");
		}

		// Token: 0x04004A89 RID: 19081
		public bool CanDisassemble = true;

		// Token: 0x04004A8A RID: 19082
		public bool CanBuild;

		// Token: 0x04004A8B RID: 19083
		public int BuildTier = 1;

		// Token: 0x04004A8C RID: 19084
		public int NumberMade = 1;

		// Token: 0x04004A8D RID: 19085
		public string Ingredient = "";

		// Token: 0x04004A8E RID: 19086
		public string SubstituteBlueprint;

		// Token: 0x04004A8F RID: 19087
		public string RepairCost;

		// Token: 0x04004A90 RID: 19088
		public string RustedRepairCost;

		// Token: 0x04004A91 RID: 19089
		[GameBasedStaticCache(true, false)]
		[NonSerialized]
		public static Dictionary<string, string> BitCostMap = new Dictionary<string, string>();

		// Token: 0x04004A92 RID: 19090
		[GameBasedStaticCache(true, false)]
		[NonSerialized]
		public static Dictionary<string, string> BitSpecMap = new Dictionary<string, string>();

		// Token: 0x04004A93 RID: 19091
		[NonSerialized]
		private static BitCost ModBitCost = new BitCost();

		// Token: 0x04004A94 RID: 19092
		[NonSerialized]
		private static Dictionary<string, TinkerData> ModRecipes = new Dictionary<string, TinkerData>();

		// Token: 0x04004A95 RID: 19093
		[NonSerialized]
		private static BitCost Cost = new BitCost();

		// Token: 0x04004A96 RID: 19094
		public GameObject[] objectsToDisable;

		// Token: 0x04004A97 RID: 19095
		public GameObject[] objectsToEnable;
	}
}
