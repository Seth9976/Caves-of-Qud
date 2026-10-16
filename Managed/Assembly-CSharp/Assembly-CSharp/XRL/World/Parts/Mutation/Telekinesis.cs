using System;
using System.Collections.Generic;
using Genkit;
using Qud.API;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts.Mutation
{
	/// This part is not used in the base game.
	// Token: 0x020013FD RID: 5117
	[Serializable]
	public class Telekinesis : BaseMutation
	{
		// Token: 0x0600C676 RID: 50806 RVA: 0x00484EFC File Offset: 0x004830FC
		public Telekinesis()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C677 RID: 50807 RVA: 0x00484F25 File Offset: 0x00483125
		public static int GetTelekineticRange(int Level)
		{
			return Math.Max((Level + 1) * 5, 1);
		}

		// Token: 0x0600C678 RID: 50808 RVA: 0x00484F32 File Offset: 0x00483132
		public int GetTelekineticRange()
		{
			return Telekinesis.GetTelekineticRange(base.Level);
		}

		// Token: 0x0600C679 RID: 50809 RVA: 0x00484F3F File Offset: 0x0048313F
		public static int GetTelekineticStrength(int Level)
		{
			return Level * 4;
		}

		// Token: 0x0600C67A RID: 50810 RVA: 0x00484F44 File Offset: 0x00483144
		public int GetTelekineticStrength()
		{
			return Telekinesis.GetTelekineticStrength(base.Level);
		}

		// Token: 0x0600C67B RID: 50811 RVA: 0x00484F51 File Offset: 0x00483151
		public int GetTelekineticWeightCapacity()
		{
			return this.GetTelekineticStrength() * RuleSettings.MAXIMUM_CARRIED_WEIGHT_PER_STRENGTH;
		}

		// Token: 0x0600C67C RID: 50812 RVA: 0x00484F60 File Offset: 0x00483160
		public override string GetDescription()
		{
			string text = "You can move things with your mind.";
			if (Options.AnySifrah)
			{
				text += "\nUseful in many tinkering Sifrah games.";
			}
			return text;
		}

		// Token: 0x0600C67D RID: 50813 RVA: 0x00484F88 File Offset: 0x00483188
		public override string GetLevelText(int Level)
		{
			string text = "You can manipulate objects at a distance and perform some physical tasks with your mind while immobilized.\n";
			int telekineticRange = Telekinesis.GetTelekineticRange(Level);
			text = string.Concat(new string[]
			{
				text,
				"Range: ",
				telekineticRange.ToString(),
				" ",
				(telekineticRange == 1) ? "square" : "squares",
				"\n"
			});
			return text + "Telekinetic Strength: " + Telekinesis.GetTelekineticStrength(Level).ToString() + "\n";
		}

		// Token: 0x0600C67E RID: 50814 RVA: 0x00485007 File Offset: 0x00483207
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<GetThrowProfileEvent>.ID || ID == InventoryActionEvent.ID || ID == OwnerGetInventoryActionsEvent.ID;
		}

		// Token: 0x0600C67F RID: 50815 RVA: 0x00485038 File Offset: 0x00483238
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (GameObject.Validate(E.Target) && base.IsMyActivatedAbilityVoluntarilyUsable(this.TelekinesisActivatedAbilityID, null) && E.Actor.HasLOSTo(E.Target, false, false, false, true, null))
			{
				if (this.FindExplosiveToDetonateNear(E.Target) != null)
				{
					E.Add("CommandAIDetonateExplosive", 2, null, false, false, null, null);
				}
				if (this.FindHurlAtObjectNear(E.Target) != null)
				{
					E.Add("CommandAIHurlAt", 1, null, false, false, null, null);
				}
				if (this.FindHurlIntoObjectNear(E.Target) != null)
				{
					E.Add("CommandAIHurlInto", 1, null, false, false, null, null);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C680 RID: 50816 RVA: 0x004850E0 File Offset: 0x004832E0
		public override bool HandleEvent(GetThrowProfileEvent E)
		{
			if (this.IsTelekineticThrowingActive())
			{
				int telekineticRange = this.GetTelekineticRange();
				int telekineticStrength = this.GetTelekineticStrength();
				E.Strength = Math.Max(E.Strength, telekineticStrength);
				E.Range += telekineticRange;
				E.Telekinetic = true;
				if (E.Distance <= telekineticRange)
				{
					E.AimVariance = 0;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C681 RID: 50817 RVA: 0x00485144 File Offset: 0x00483344
		public override bool HandleEvent(OwnerGetInventoryActionsEvent E)
		{
			if (E.Object.IsReal)
			{
				if (E.Object.CurrentCell != null)
				{
					if (E.Object.DistanceTo(this.ParentObject) > 1 && (E.Object.IsTakeable() || E.Object.IsCreature))
					{
						E.AddAction("Telekinetic Pull", "telekinetically pull toward you", "TelekineticPull", null, 'p', true, -1, 0, false, false, true, false, true, null, false);
						if (E.Object.Count > 1)
						{
							E.AddAction("Telekinetic Pull One", "telekinetically pull one toward you", "TelekineticPullOne", null, 'P', true, -1, 0, false, false, true, false, true, null, false);
						}
					}
					if (E.Object.IsTakeable())
					{
						E.AddAction("Telekinetic Pull And Take", "telekinetically pull towards you and take", "TelekineticPullAndTake", "take", 't', true, -1, 0, false, false, true, false, true, null, false);
						if (E.Object.Count > 1)
						{
							E.AddAction("Telekinetic Pull And Take One", "telekinetically pull one towards you and take", "TelekineticPullAndTakeOne", "take", 'T', true, -1, 0, false, false, true, false, true, null, false);
						}
					}
				}
				if ((E.Object.CurrentCell != null || E.Object.CanBeUnequipped(null, this.ParentObject, false, true)) && (E.Object.IsTakeable() || E.Object.IsCreature))
				{
					E.AddAction("Telekinetic Hurl", "telekinetically hurl", "TelekineticHurl", null, 'h', true, -1, 0, false, false, true, false, true, null, false);
					E.AddAction("Telekinetic Move", "telekinetically move", "TelekineticMove", null, 'm', true, -1, 0, false, false, true, false, true, null, false);
					if (E.Object.Count > 1)
					{
						E.AddAction("Telekinetic Hurl One", "telekinetically hurl one", "TelekineticHurlOne", null, 'H', true, -1, 0, false, false, true, false, true, null, false);
						E.AddAction("Telekinetic Move One", "telekinetically move one", "TelekineticMove", null, 'M', true, -1, 0, false, false, true, false, true, null, false);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C682 RID: 50818 RVA: 0x00485340 File Offset: 0x00483540
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "TelekineticPullAndTake" || E.Command == "TelekineticPullAndTakeOne" || E.Command == "TelekineticPull" || E.Command == "TelekineticPullOne")
			{
				if (!this.Activate(true))
				{
					return false;
				}
				if (!(E.Command == "TelekineticPullAndTake"))
				{
					E.Command == "TelekineticPullAndTakeOne";
				}
				bool flag = E.Command == "TelekineticPullAndTakeOne" || E.Command == "TelekineticPullOne";
				GameObject item = E.Item;
				if (flag)
				{
					item.SplitFromStack();
				}
				Cell currentCell = item.CurrentCell;
				double num = currentCell.RealDistanceTo(this.ParentObject);
				int num2 = 0;
				if (num > 1.0)
				{
					num2 = item.Accelerate(Math.Min(this.GetTelekineticWeightCapacity(), (int)((num + 1.0) * (double)item.GetKineticResistance())), null, this.ParentObject.CurrentCell, null, "Telekinetic", this.ParentObject, true, null, null, 1.0, true, true, false, true, true, true);
				}
				bool flag2 = false;
				if (GameObject.Validate(item))
				{
					if (E.Command == "TelekineticPullAndTake" && item.CurrentCell.DistanceTo(this.ParentObject) <= 1 && this.ParentObject.ReceiveObject(item, false, null))
					{
						flag2 = true;
					}
					else if (num2 > 0)
					{
						flag2 = true;
					}
					else if (item.CurrentCell == currentCell && E.Actor.IsPlayer())
					{
						if (item.IsPlayer())
						{
							Popup.ShowFail("You do not budge.", true, true, true);
						}
						else
						{
							Popup.ShowFail(item.The + item.ShortDisplayName + item.GetVerb("do", true, false, false, true, false) + " not budge.", true, true, true);
						}
					}
				}
				else
				{
					flag2 = true;
				}
				if (flag2)
				{
					this.ParentObject.UseEnergy(1000, "Mental Mutation Telekinesis Pull", null, null, false);
					E.RequestInterfaceExit();
				}
				if (GameObject.Validate(ref item) && item.CurrentCell == currentCell && num2 == 0)
				{
					item.CheckStack();
				}
			}
			else if (E.Command == "TelekineticHurl" || E.Command == "TelekineticHurlOne" || E.Command == "TelekineticMove" || E.Command == "TelekineticMoveOne")
			{
				GameObject item2 = E.Item;
				bool flag3 = E.Command == "TelekineticHurl" || E.Command == "TelekineticHurlOne";
				bool flag4 = E.Command == "TelekineticMove" || E.Command == "TelekineticMoveOne";
				bool flag5 = E.Command == "TelekineticHurlOne" || E.Command == "TelekineticMoveOne";
				Cell currentCell2 = item2.GetCurrentCell();
				if (currentCell2 != null)
				{
					Cell cell = PickTarget.ShowPicker(PickTarget.PickStyle.Line, 999, 999, currentCell2.X, currentCell2.Y, false, AllowVis.Any, null, null, null, new Point2D?(currentCell2.Pos2D), "Telekinesis", false, true);
					if (cell != null)
					{
						if (flag5)
						{
							item2.SplitFromStack();
						}
						if (item2.CurrentCell != null || item2.TryRemoveFromContext(null))
						{
							if (item2.CurrentCell == null)
							{
								currentCell2.AddObject(item2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							}
							int num3 = this.GetTelekineticWeightCapacity();
							if (!flag3)
							{
								num3 = Math.Min(num3, ((int)currentCell2.RealDistanceTo(cell, true) + 1) * item2.GetKineticResistance());
							}
							int num4 = item2.Accelerate(num3, null, cell, null, "Telekinetic", this.ParentObject, false, null, null, 1.0, true, false, flag3, true, true, true);
							if (!GameObject.Validate(item2) || num4 > 0)
							{
								if (flag3)
								{
									this.ParentObject.UseEnergy(1000, "Mental Mutation Telekinesis Hurl", null, null, false);
								}
								else if (flag4)
								{
									this.ParentObject.UseEnergy(1000, "Mental Mutation Telekinesis Move", null, null, false);
								}
								else
								{
									this.ParentObject.UseEnergy(1000, "Mental Mutation Telekinesis", null, null, false);
								}
								E.RequestInterfaceExit();
							}
							else if (item2.CurrentCell == currentCell2)
							{
								if (E.Actor.IsPlayer())
								{
									if (item2.IsPlayer())
									{
										Popup.ShowFail("You do not budge.", true, true, true);
									}
									else
									{
										Popup.ShowFail(item2.The + item2.ShortDisplayName + item2.GetVerb("do", true, false, false, true, false) + " not budge.", true, true, true);
									}
								}
							}
							else
							{
								E.RequestInterfaceExit();
							}
							if (GameObject.Validate(ref item2) && item2.CurrentCell == currentCell2 && num4 == 0)
							{
								item2.CheckStack();
							}
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C683 RID: 50819 RVA: 0x00485844 File Offset: 0x00483A44
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandAIDetonateExplosive");
			Registrar.Register("CommandAIHurlAt");
			Registrar.Register("CommandAIHurlInto");
			Registrar.Register("CommandTelekinesis");
			Registrar.Register("CommandToggleTelekineticThrowing");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C684 RID: 50820 RVA: 0x00485890 File Offset: 0x00483A90
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandAIHurlAt")
			{
				if (!this.Activate(false))
				{
					return false;
				}
				GameObject target = this.ParentObject.Target;
				GameObject gameObject = this.FindHurlAtObjectNear(target);
				if (gameObject == null || this.ParentObject.DistanceTo(gameObject) > this.GetTelekineticRange())
				{
					return false;
				}
				Cell currentCell = target.CurrentCell;
				if (currentCell == null)
				{
					return false;
				}
				this.ParentObject.UseEnergy(1000, "Mental Mutation Telekinesis Hurl", null, null, false);
				gameObject.Accelerate(this.GetTelekineticWeightCapacity(), null, currentCell, null, "Telekinetic", this.ParentObject, true, target, null, 1.0, true, false, true, true, true, true);
			}
			else if (E.ID == "CommandAIHurlInto")
			{
				if (!this.Activate(false))
				{
					return false;
				}
				GameObject target2 = this.ParentObject.Target;
				GameObject gameObject2 = this.FindHurlIntoObjectNear(target2);
				if (gameObject2 == null || this.ParentObject.DistanceTo(target2) > this.GetTelekineticRange())
				{
					return false;
				}
				Cell currentCell2 = gameObject2.CurrentCell;
				if (currentCell2 == null)
				{
					return false;
				}
				this.ParentObject.UseEnergy(1000, "Mental Mutation Telekinesis Hurl", null, null, false);
				target2.Accelerate(this.GetTelekineticWeightCapacity(), null, currentCell2, null, "Telekinetic", this.ParentObject, true, target2, null, 1.0, true, false, true, true, true, true);
			}
			else if (E.ID == "CommandAIDetonateExplosive")
			{
				if (!this.Activate(false))
				{
					return false;
				}
				InventoryAction inventoryAction;
				GameObject gameObject3 = this.FindExplosiveToDetonateNear(this.ParentObject.Target, out inventoryAction);
				if (gameObject3 == null || inventoryAction == null || this.ParentObject.DistanceTo(gameObject3) > this.GetTelekineticRange())
				{
					return false;
				}
				gameObject3.TelekinesisBlip();
				inventoryAction.Process(gameObject3, this.ParentObject, true);
			}
			else if (E.ID == "CommandTelekinesis")
			{
				if (!this.AttemptTelekinesis())
				{
					return false;
				}
			}
			else if (E.ID == "CommandToggleTelekineticThrowing")
			{
				base.ToggleMyActivatedAbility(this.TelekineticThrowingActivatedAbilityID, null, false, null);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C685 RID: 50821 RVA: 0x00485AB9 File Offset: 0x00483CB9
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C686 RID: 50822 RVA: 0x00485AC4 File Offset: 0x00483CC4
		public override bool Mutate(GameObject GO, int Level)
		{
			this.TelekinesisActivatedAbilityID = GO.AddActivatedAbility("Telekinesis", "CommandTelekinesis", "Mental Mutations", "You can use this ability to choose an object to interact with telekinetically. You can also use telekinesis directly by interacting with an object via Look or Interact Nearby.\n\nWhen moving things telekinetically, \"move\" or \"pull\" indicates moving something gently, with as little force as needed, minimizing any injuries or damage from accidents. \"Hurl\" indicates moving something with maximum possible force.", "\a", null, false, false, false, false, false, false, false, false, true, true, false, false, -1, null, null, null, null, null);
			this.TelekineticThrowingActivatedAbilityID = GO.AddActivatedAbility("Telekinetic Throwing", "CommandToggleTelekineticThrowing", "Mental Mutations", "Normally, one uses one's telekinesis to augment throwing. Toggle this ability to control this behavior.", "\a", null, true, true, false, false, false, true, false, false, true, true, false, false, -1, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C687 RID: 50823 RVA: 0x00485B49 File Offset: 0x00483D49
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.TelekinesisActivatedAbilityID, null);
			base.RemoveMyActivatedAbility(ref this.TelekineticThrowingActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x0600C688 RID: 50824 RVA: 0x00485B6E File Offset: 0x00483D6E
		public bool IsTelekineticThrowingActive()
		{
			return base.IsMyActivatedAbilityVoluntarilyUsable(this.TelekineticThrowingActivatedAbilityID, null);
		}

		// Token: 0x0600C689 RID: 50825 RVA: 0x00485B7D File Offset: 0x00483D7D
		public bool Activate(bool AllowMessage = true)
		{
			if (!base.IsMyActivatedAbilityUsable(this.TelekinesisActivatedAbilityID, null))
			{
				if (AllowMessage && this.ParentObject.IsPlayer())
				{
					Popup.ShowFail("Your psyche is too exhausted.", true, true, true);
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600C68A RID: 50826 RVA: 0x00485BB0 File Offset: 0x00483DB0
		public bool AttemptTelekinesis()
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return false;
			}
			if (!this.Activate(true))
			{
				return false;
			}
			int telekineticRange = this.GetTelekineticRange();
			int num = currentCell.X;
			int num2 = currentCell.Y;
			List<GameObject> list = Event.NewGameObjectList();
			for (;;)
			{
				Cell cell = PickTarget.ShowPicker(PickTarget.PickStyle.EmptyCell, telekineticRange, telekineticRange, num, num2, false, AllowVis.OnlyVisible, null, null, null, null, "Telekinesis", false, true);
				if (cell == null)
				{
					break;
				}
				num = cell.X;
				num2 = cell.Y;
				foreach (GameObject gameObject in cell.Objects)
				{
					if (EquipmentAPI.CanBeTwiddled(gameObject, this.ParentObject, true))
					{
						list.Add(gameObject);
					}
				}
				if (list.Count > 0)
				{
					goto IL_00E7;
				}
				if (this.ParentObject.IsPlayer())
				{
					Popup.ShowFail("There is nothing you can telekinetically manipulate there.", true, true, true);
				}
			}
			return false;
			IL_00E7:
			GameObject gameObject2;
			if (list.Count == 1)
			{
				gameObject2 = list[0];
			}
			else
			{
				if (this.ParentObject.IsPlayer())
				{
					gameObject2 = PickItem.ShowPicker(list, null, PickItem.PickItemDialogStyle.SelectItemDialog, this.ParentObject, null, null, null, false, null, false, true, false);
				}
				else
				{
					gameObject2 = list.GetRandomElement<GameObject>(null);
				}
				if (gameObject2 == null)
				{
					return false;
				}
			}
			return gameObject2.TelekineticTwiddle(null);
		}

		// Token: 0x0600C68B RID: 50827 RVA: 0x00485D10 File Offset: 0x00483F10
		public GameObject FindExplosiveToDetonateNear(GameObject target, out InventoryAction action)
		{
			action = null;
			if (target == null)
			{
				return null;
			}
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null || currentCell.ParentZone == null)
			{
				return null;
			}
			Cell currentCell2 = target.CurrentCell;
			if (currentCell2 == null || currentCell2.ParentZone == null)
			{
				return null;
			}
			List<GameObject> list = currentCell2.ParentZone.FastFloodVisibility(currentCell2.X, currentCell2.Y, 3, "Physics", this.ParentObject, null, null);
			list.Remove(this.ParentObject);
			if (list.Count <= 0)
			{
				return null;
			}
			int telekineticRange = this.GetTelekineticRange();
			List<GameObject> list2 = Event.NewGameObjectList();
			List<InventoryAction> list3 = new List<InventoryAction>();
			foreach (GameObject gameObject in list)
			{
				InventoryAction usefulDetonateAction = Telekinesis.GetUsefulDetonateAction(gameObject, this.ParentObject, target, telekineticRange);
				if (usefulDetonateAction != null)
				{
					list2.Add(gameObject);
					list3.Add(usefulDetonateAction);
				}
			}
			if (list2.Count <= 0)
			{
				return null;
			}
			int num = Stat.Random(0, list2.Count - 1);
			action = list3[num];
			return list2[num];
		}

		// Token: 0x0600C68C RID: 50828 RVA: 0x00485E3C File Offset: 0x0048403C
		public GameObject FindExplosiveToDetonateNear(GameObject target)
		{
			InventoryAction inventoryAction;
			return this.FindExplosiveToDetonateNear(target, out inventoryAction);
		}

		// Token: 0x0600C68D RID: 50829 RVA: 0x00485E54 File Offset: 0x00484054
		private static InventoryAction GetUsefulDetonateAction(GameObject obj, GameObject who, GameObject target, int range)
		{
			if (who == null || target == null)
			{
				return null;
			}
			if (obj.DistanceTo(target) > 3)
			{
				return null;
			}
			if (obj.DistanceTo(who) <= 3)
			{
				return null;
			}
			if (who.DistanceTo(obj) > range)
			{
				return null;
			}
			if (obj.HasPropertyOrTag("NoAIEquip"))
			{
				return null;
			}
			if (!who.HasPart<Clairvoyance>() && !who.HasLOSTo(obj, true, false, false, true, null))
			{
				return null;
			}
			List<InventoryAction> inventoryActions = EquipmentAPI.GetInventoryActions(obj, who, true, true);
			if (inventoryActions == null)
			{
				return null;
			}
			foreach (InventoryAction inventoryAction in inventoryActions)
			{
				if (inventoryAction.Name == "Detonate")
				{
					return inventoryAction;
				}
			}
			return null;
		}

		// Token: 0x0600C68E RID: 50830 RVA: 0x00485F18 File Offset: 0x00484118
		public GameObject FindHurlIntoObjectNear(GameObject target)
		{
			if (target == null)
			{
				return null;
			}
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null || currentCell.ParentZone == null)
			{
				return null;
			}
			Cell currentCell2 = target.CurrentCell;
			if (currentCell2 == null || currentCell2.ParentZone == null)
			{
				return null;
			}
			if (target.HasTag("ExcavatoryTerrainFeature"))
			{
				return null;
			}
			if (!Physics.IsMoveable(target, true))
			{
				return null;
			}
			if (this.ParentObject.DistanceTo(target) > this.GetTelekineticRange())
			{
				return null;
			}
			int num = this.GetTelekineticWeightCapacity() / Math.Max(target.GetKineticResistance(), 1);
			if (num < 1)
			{
				return null;
			}
			List<GameObject> list = currentCell2.ParentZone.FastFloodVisibility(currentCell2.X, currentCell2.Y, num, "Physics", this.ParentObject, null, null);
			list.Remove(this.ParentObject);
			if (list.Count <= 0)
			{
				return null;
			}
			List<GameObject> list2 = Event.NewGameObjectList();
			foreach (GameObject gameObject in list)
			{
				if (Telekinesis.IsUsefulHurlIntoObject(gameObject, this.ParentObject, target, num))
				{
					list2.Add(gameObject);
				}
			}
			return list2.GetRandomElement<GameObject>(null);
		}

		// Token: 0x0600C68F RID: 50831 RVA: 0x00486044 File Offset: 0x00484244
		private static bool IsUsefulHurlIntoObject(GameObject obj, GameObject who, GameObject target, int hurlRange)
		{
			if (who == null || target == null)
			{
				return false;
			}
			if (!obj.IsReal)
			{
				return false;
			}
			if (target.DistanceTo(obj) > hurlRange)
			{
				return false;
			}
			if (who.DistanceTo(target) <= 1 && obj.DistanceTo(who) <= 1)
			{
				return false;
			}
			if (obj.GetMatterPhase() >= 2)
			{
				return false;
			}
			if (obj.IsCombat)
			{
				if (!who.IsHostileTowards(obj))
				{
					return false;
				}
				int combatDV = Stats.GetCombatDV(obj);
				if (Stat.Random(1, 30) <= combatDV || Stat.Random(1, 30) <= combatDV)
				{
					return false;
				}
			}
			else if (!obj.ConsiderSolid())
			{
				return false;
			}
			return (obj.PhaseMatches(target) || Stat.Random(1, 30) > who.Stat("Intelligence", 0)) && (who.HasPart<Clairvoyance>() || who.HasLOSTo(obj, true, false, false, true, null));
		}

		// Token: 0x0600C690 RID: 50832 RVA: 0x00486108 File Offset: 0x00484308
		public GameObject FindHurlAtObjectNear(GameObject target)
		{
			if (target == null)
			{
				return null;
			}
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null || currentCell.ParentZone == null)
			{
				return null;
			}
			Cell currentCell2 = target.CurrentCell;
			if (currentCell2 == null || currentCell2.ParentZone == null)
			{
				return null;
			}
			int telekineticRange = this.GetTelekineticRange();
			List<GameObject> list = currentCell2.ParentZone.FastFloodVisibility(currentCell.X, currentCell.Y, telekineticRange, "Physics", this.ParentObject, null, null);
			list.Remove(this.ParentObject);
			if (list.Count <= 0)
			{
				return null;
			}
			int telekineticWeightCapacity = this.GetTelekineticWeightCapacity();
			List<GameObject> list2 = Event.NewGameObjectList();
			foreach (GameObject gameObject in list)
			{
				if (Telekinesis.IsUsefulHurlAtObject(gameObject, this.ParentObject, target, telekineticRange, telekineticWeightCapacity))
				{
					list2.Add(gameObject);
				}
			}
			return list2.GetRandomElement<GameObject>(null);
		}

		// Token: 0x0600C691 RID: 50833 RVA: 0x004861FC File Offset: 0x004843FC
		private static bool IsUsefulHurlAtObject(GameObject obj, GameObject who, GameObject target, int range, int weightCapacity)
		{
			if (who == null || target == null)
			{
				return false;
			}
			if (!obj.IsReal)
			{
				return false;
			}
			int num = obj.DistanceTo(target);
			if (num < 1)
			{
				return false;
			}
			int num2 = who.DistanceTo(target);
			if (num2 > range)
			{
				return false;
			}
			if (num2 <= 1 && obj.DistanceTo(who) <= 1)
			{
				return false;
			}
			if (obj.GetMatterPhase() >= 2)
			{
				return false;
			}
			int num3 = Math.Max(obj.GetKineticResistance(), 1);
			int num4 = weightCapacity / num3;
			if (num > num4)
			{
				return false;
			}
			if (obj.HasTag("ExcavatoryTerrainFeature"))
			{
				return false;
			}
			if (!Physics.IsMoveable(obj, true))
			{
				return false;
			}
			int combatDV = Stats.GetCombatDV(target);
			if (Stat.Random(1, 30) <= combatDV || Stat.Random(1, 30) <= combatDV)
			{
				return false;
			}
			if (num > 1 && !obj.HasLOSTo(target, true, false, false, true, null))
			{
				return false;
			}
			if (obj.IsCombat)
			{
				if (!who.IsHostileTowards(obj))
				{
					return false;
				}
			}
			else if (!obj.ConsiderSolid() && (!obj.IsTakeable() || !50.in100()))
			{
				return false;
			}
			return (obj.PhaseMatches(target) || Stat.Random(1, 30) > who.Stat("Intelligence", 0)) && (who.HasPart<Clairvoyance>() || who.HasLOSTo(obj, true, false, false, true, null));
		}

		// Token: 0x04004E40 RID: 20032
		public Guid TelekinesisActivatedAbilityID = Guid.Empty;

		// Token: 0x04004E41 RID: 20033
		public Guid TelekineticThrowingActivatedAbilityID = Guid.Empty;
	}
}
