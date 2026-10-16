using System;
using XRL.UI;
using XRL.World.Effects;
using XRL.World.Tinkering;

namespace XRL.World.Parts.Skill
{
	// Token: 0x020013BC RID: 5052
	[Serializable]
	public class Tinkering_Repair : BaseSkill, IRepairSifrahHandler
	{
		// Token: 0x0600C2FF RID: 49919 RVA: 0x004731AB File Offset: 0x004713AB
		public static void Init()
		{
		}

		// Token: 0x0600C300 RID: 49920 RVA: 0x004731AD File Offset: 0x004713AD
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == InventoryActionEvent.ID || ID == OwnerGetInventoryActionsEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C301 RID: 49921 RVA: 0x004731D3 File Offset: 0x004713D3
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("scholarship", 2);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C302 RID: 49922 RVA: 0x004731F8 File Offset: 0x004713F8
		public override bool HandleEvent(OwnerGetInventoryActionsEvent E)
		{
			if (this.ParentObject.HasSkill("Tinkering_Repair") && IsRepairableEvent.Check(E.Actor, E.Object, null, this, null))
			{
				E.AddAction("Repair", "repair", "Repair", null, 'R', true, 25, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C303 RID: 49923 RVA: 0x00473260 File Offset: 0x00471460
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Repair")
			{
				GameObject actor = E.Actor;
				GameObject item = E.Item;
				actor.GetPart<Tinkering>();
				BitLocker bitLocker = actor.RequirePart<BitLocker>(false);
				if (actor.GetTotalConfusion() > 0)
				{
					actor.ShowFailure("You're too confused to do that.");
					return false;
				}
				if (item.CurrentCell != null)
				{
					if (!actor.FlightMatches(item))
					{
						actor.ShowFailure(string.Concat(new string[]
						{
							"You cannot reach ",
							item.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							" to repair ",
							item.them,
							"."
						}));
						return false;
					}
					if (!actor.PhaseMatches(item))
					{
						actor.ShowFailure(string.Concat(new string[]
						{
							"You are out of phase with ",
							item.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							" and cannot repair ",
							item.them,
							"."
						}));
						return false;
					}
				}
				if (actor.AreHostilesNearby() && actor.FireEvent("CombatPreventsRepair"))
				{
					actor.ShowFailure("You can't repair with hostile creatures nearby.");
					return false;
				}
				if (actor.IsPlayer() && !item.Understood())
				{
					Popup.ShowFail(string.Concat(new string[]
					{
						"You cannot repair ",
						item.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
						" until you understand ",
						item.them,
						"."
					}), true, true, true);
					return false;
				}
				if (item.HasTagOrProperty("NoRepair"))
				{
					Popup.ShowFail("You cannot repair " + item.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", true, true, true);
					return false;
				}
				if (Options.SifrahRepair && actor.IsPlayer())
				{
					if (!string.IsNullOrEmpty(item.Owner) && !item.HasPropertyOrTag("DontWarnOnRepair") && Popup.ShowYesNoCancel(string.Concat(new string[]
					{
						item.Does("are", 2147483647, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
						" not owned by you, and trying to repair ",
						item.them,
						" risks damaging ",
						item.them,
						". Are you sure you want to do so?"
					}), "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
					{
						return false;
					}
					GameObject inInventory = item.InInventory;
					if (inInventory != null && inInventory != actor && !string.IsNullOrEmpty(inInventory.Owner) && inInventory.Owner != item.Owner && !inInventory.HasPropertyOrTag("DontWarnOnRepair") && Popup.ShowYesNoCancel(string.Concat(new string[]
					{
						inInventory.Does("are", 2147483647, null, null, null, false, true, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
						" not owned by you, and trying to repair ",
						item.an(2147483647, null, null, false, true, false, false, false, false, true, false, false, true, false, null, false, null, false),
						" inside ",
						inInventory.them,
						" risks causing damage. Are you sure you want to do so?"
					}), "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
					{
						return false;
					}
					RepairSifrah repairSifrah = new RepairSifrah(item, item.GetComplexity(), item.GetTier() / 2, actor.Stat("Intelligence", 0));
					repairSifrah.HandlerID = this.ParentObject.ID;
					repairSifrah.HandlerPartName = base.Name;
					repairSifrah.Play(item);
					if (repairSifrah.InterfaceExitRequested)
					{
						E.RequestInterfaceExit();
					}
				}
				else
				{
					bool flag = false;
					BitCost bitCost = new BitCost(Tinkering_Repair.GetRepairCost(item));
					ModifyBitCostEvent.Process(actor, bitCost, "Repair");
					if (actor.IsPlayer())
					{
						if (!bitLocker.HasBits(bitCost))
						{
							Popup.ShowFail(string.Concat(new string[]
							{
								"You don't have <",
								bitCost.ToString(),
								"> to repair ",
								item.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								". You have:\n\n",
								bitLocker.GetBitsString()
							}), true, true, true);
							return false;
						}
						if (Popup.ShowYesNoCancel(string.Concat(new string[]
						{
							"Do you want to spend <",
							bitCost.ToString(),
							"> to repair ",
							item.t(2147483647, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							"? You have:\n\n",
							bitLocker.GetBitsString()
						}), "Sounds/UI/ui_notification", true, DialogResult.Cancel) == DialogResult.Yes)
						{
							flag = true;
							bitLocker.UseBits(bitCost);
						}
					}
					else if (Tinkering_Repair.IsRepairableBy(item, actor, bitCost, null, null))
					{
						flag = true;
					}
					else
					{
						actor.FireEvent(Event.New("UnableToRepair", "Object", item));
					}
					if (flag)
					{
						this.RepairResultSuccess(actor, item);
					}
				}
				actor.UseEnergy(1000, "Skill Tinkering Repair", null, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C304 RID: 49924 RVA: 0x004737C4 File Offset: 0x004719C4
		public static bool IsRepairableBy(GameObject obj, GameObject actor, BitCost RepairCost = null, Tinkering_Repair Skill = null, int? MaxRepairTier = null)
		{
			if (MaxRepairTier == null && Skill == null)
			{
				Skill = actor.GetPart<Tinkering_Repair>();
				if (Skill == null)
				{
					return false;
				}
			}
			if (!IsRepairableEvent.Check(actor, obj, null, Skill, MaxRepairTier))
			{
				return false;
			}
			int num = ((RepairCost != null) ? Tinkering_Repair.GetRepairTier(RepairCost) : Tinkering_Repair.GetRepairTier(Tinkering_Repair.GetRepairCost(obj)));
			Repair part = obj.GetPart<Repair>();
			if (part != null)
			{
				int num2 = Math.Min(Math.Max(part.Difficulty / 2, 0), 8);
				if (num2 > num)
				{
					num = num2;
				}
			}
			if (num > 0)
			{
				if (MaxRepairTier != null)
				{
					int? num3 = MaxRepairTier;
					int num4 = num;
					if ((num3.GetValueOrDefault() < num4) & (num3 != null))
					{
						return false;
					}
				}
				else if (!actor.HasSkill(DataDisk.GetRequiredSkill(num)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600C305 RID: 49925 RVA: 0x00473870 File Offset: 0x00471A70
		public bool IsRepairableBy(GameObject actor, string RepairCost)
		{
			return Tinkering_Repair.IsRepairableBy(this.ParentObject, actor, new BitCost(RepairCost), null, null);
		}

		// Token: 0x0600C306 RID: 49926 RVA: 0x0047389C File Offset: 0x00471A9C
		public static int GetRepairTier(BitCost RepairCost)
		{
			int num = 0;
			foreach (char c in RepairCost.Keys)
			{
				int bitTier = BitType.GetBitTier(c);
				if (bitTier > num)
				{
					num = bitTier;
				}
			}
			return num;
		}

		// Token: 0x0600C307 RID: 49927 RVA: 0x004738F8 File Offset: 0x00471AF8
		public static int GetRepairTier(string RepairCost)
		{
			int num = 0;
			int i = 0;
			int length = RepairCost.Length;
			while (i < length)
			{
				int bitTier = BitType.GetBitTier(RepairCost[i]);
				if (bitTier > num)
				{
					num = bitTier;
				}
				i++;
			}
			return num;
		}

		// Token: 0x0600C308 RID: 49928 RVA: 0x00473930 File Offset: 0x00471B30
		public static string GetRepairCost(GameObject obj)
		{
			TinkerItem part = obj.GetPart<TinkerItem>();
			bool flag = obj.HasEffect<Rusted>();
			string text = (flag ? "RustedRepairCost" : "RepairCost");
			string text2 = ((part == null) ? obj.GetTagOrStringProperty(text, null) : (flag ? part.RustedRepairCost : part.RepairCost));
			if (!string.IsNullOrEmpty(text2))
			{
				return text2;
			}
			text2 = "";
			string text3 = ((part == null) ? BitType.ToRealBits("BC", obj.Blueprint) : part.Bits);
			int num = 0;
			while (text2 == "")
			{
				Random random = new Random(The.Game.GetWorldSeed(obj.Blueprint + "RepairCost"));
				int num2 = (flag ? 75 : 50);
				int num3 = (flag ? (text3.Length - 1) : random.Next(0, text3.Length));
				for (int i = 0; i < text3.Length; i++)
				{
					if (num3 == i || random.Next(0, 100) < num2)
					{
						text2 += text3[i].ToString();
					}
				}
				num++;
				if (text2 == "" && num >= 10)
				{
					text2 = "BC";
				}
			}
			if (part == null)
			{
				obj.SetStringProperty(text, text2, false);
			}
			else if (flag)
			{
				part.RustedRepairCost = text2;
			}
			else
			{
				part.RepairCost = text2;
			}
			return text2;
		}

		// Token: 0x0600C309 RID: 49929 RVA: 0x00473A8E File Offset: 0x00471C8E
		public override bool AddSkill(GameObject GO)
		{
			return true;
		}

		// Token: 0x0600C30A RID: 49930 RVA: 0x00473A91 File Offset: 0x00471C91
		public override bool RemoveSkill(GameObject GO)
		{
			return true;
		}

		// Token: 0x0600C30B RID: 49931 RVA: 0x00473A94 File Offset: 0x00471C94
		public void RepairResultSuccess(GameObject who, GameObject obj)
		{
			who.PlayWorldOrUISound("Sounds/Misc/sfx_interact_artifact_repair", null);
			if (who.IsPlayer())
			{
				Popup.ShowBlock("You repair " + obj.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			RepairedEvent.Send(who, obj, null, this, 0);
		}

		// Token: 0x0600C30C RID: 49932 RVA: 0x00473B0C File Offset: 0x00471D0C
		public void RepairResultExceptionalSuccess(GameObject who, GameObject obj)
		{
			this.RepairResultSuccess(who, obj);
			string randomBits = BitType.GetRandomBits("3d6".Roll(), obj.GetComplexity());
			if (!string.IsNullOrEmpty(randomBits))
			{
				who.RequirePart<BitLocker>(false).AddBits(randomBits);
				if (who.IsPlayer())
				{
					Popup.Show("You receive tinkering bits <{{|" + BitType.GetDisplayString(randomBits) + "}}>", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
		}

		// Token: 0x0600C30D RID: 49933 RVA: 0x00473B7C File Offset: 0x00471D7C
		public void RepairResultPartialSuccess(GameObject who, GameObject obj)
		{
			if (who.IsPlayer())
			{
				Popup.Show("You make some progress repairing " + obj.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}

		// Token: 0x0600C30E RID: 49934 RVA: 0x00473BD8 File Offset: 0x00471DD8
		public void RepairResultFailure(GameObject who, GameObject obj)
		{
			if (who.IsPlayer())
			{
				Popup.Show("You can't figure out how to fix " + obj.t(int.MaxValue, null, null, false, true, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}

		// Token: 0x0600C30F RID: 49935 RVA: 0x00473C34 File Offset: 0x00471E34
		public void RepairResultCriticalFailure(GameObject who, GameObject obj)
		{
			if (RepairCriticalFailureEvent.Check(who, obj))
			{
				if (obj.IsBroken())
				{
					if (!obj.HasPropertyOrTag("CantDestroyOnRepair"))
					{
						obj.PotentiallyAngerOwner(who, "DontWarnOnRepair");
						IComponent<GameObject>.XDidYToZ(who, "accidentally", "destroy", obj, null, "!", null, null, null, who, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
						obj.Destroy(null, false, false, null);
						return;
					}
					this.RepairResultFailure(who, obj);
					return;
				}
				else
				{
					string text = "You think you broke " + obj.them + "...";
					if (obj.ApplyEffect(new Broken(), null) && obj.IsBroken())
					{
						if (who.IsPlayer())
						{
							Popup.Show(text, null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
						obj.PotentiallyAngerOwner(who, "DontWarnOnRepair");
						return;
					}
					this.RepairResultFailure(who, obj);
				}
			}
		}
	}
}
