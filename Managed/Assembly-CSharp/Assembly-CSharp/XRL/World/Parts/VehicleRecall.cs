using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x020012B8 RID: 4792
	[Serializable]
	public class VehicleRecall : IPoweredPart
	{
		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x0600BD5E RID: 48478 RVA: 0x0045507C File Offset: 0x0045327C
		public string VehicleTerm
		{
			get
			{
				if (this._VehicleTerm == null)
				{
					if (this.ParentObject.Property.TryGetValue("VehicleTerm", out this._VehicleTerm))
					{
						return this._VehicleTerm;
					}
					GameObjectBlueprint gameObjectBlueprint;
					if (!this.RequiredBlueprint.IsNullOrEmpty() && GameObjectFactory.Factory.Blueprints.TryGetValue(this.RequiredBlueprint, out gameObjectBlueprint))
					{
						this._VehicleTerm = gameObjectBlueprint.CachedDisplayNameStripped;
					}
					else
					{
						this._VehicleTerm = "vehicle";
					}
				}
				return this._VehicleTerm;
			}
		}

		// Token: 0x0600BD5F RID: 48479 RVA: 0x004550FA File Offset: 0x004532FA
		public VehicleRecall()
		{
			this.WorksOnAdjacentCellContents = true;
		}

		// Token: 0x0600BD60 RID: 48480 RVA: 0x00455114 File Offset: 0x00453314
		public override bool WorksForEveryone()
		{
			return false;
		}

		// Token: 0x0600BD61 RID: 48481 RVA: 0x00455117 File Offset: 0x00453317
		public override bool WorksFor(GameObject Object)
		{
			return Object.Blueprint == this.AdjacentBlueprint;
		}

		// Token: 0x0600BD62 RID: 48482 RVA: 0x0045512A File Offset: 0x0045332A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEarlyEvent.ID;
		}

		// Token: 0x0600BD63 RID: 48483 RVA: 0x00455158 File Offset: 0x00453358
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return !E.Actor.IsPlayer() && base.HandleEvent(E);
		}

		// Token: 0x0600BD64 RID: 48484 RVA: 0x00455170 File Offset: 0x00453370
		public override bool HandleEvent(CommandSmartUseEarlyEvent E)
		{
			if (E.Actor.IsPlayer())
			{
				this.ParentObject.Twiddle(false);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD65 RID: 48485 RVA: 0x00455198 File Offset: 0x00453398
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("RecallVehicle", "recall " + this.VehicleTerm, "RecallVehicle", null, 'c', false, 0, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BD66 RID: 48486 RVA: 0x004551DC File Offset: 0x004533DC
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "RecallVehicle")
			{
				ActivePartStatus activePartStatus = base.GetActivePartStatus(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null);
				if (activePartStatus != ActivePartStatus.Operational)
				{
					if (activePartStatus == ActivePartStatus.Unpowered)
					{
						return E.Actor.ShowFailure(this.ParentObject.Does("do", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " not have enough charge to operate.");
					}
					return E.Actor.ShowFailure(this.ParentObject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " merely" + this.ParentObject.GetVerb("click", true, false, false, true, false) + ".");
				}
				else
				{
					GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
					Cell cell = ((activePartFirstSubject != null) ? activePartFirstSubject.CurrentCell : null);
					if (cell == null)
					{
						return E.Actor.ShowFailure("There is nowhere for the " + this.VehicleTerm + " to be recalled to.");
					}
					if (!cell.IsPassable(null, true))
					{
						return E.Actor.ShowFailure(activePartFirstSubject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " is obstructed.");
					}
					List<GameObject> list = VehicleRecord.ResolveRecordsFor(E.Actor, this.RequiredBlueprint, this.RequiredType);
					if (list.IsNullOrEmpty<GameObject>())
					{
						return E.Actor.ShowFailure("You have no " + this.VehicleTerm + " to recall.");
					}
					GameObject gameObject = list.First<GameObject>();
					if (list.Count > 1 || gameObject.CurrentCell == null)
					{
						string[] array = new string[list.Count];
						IRenderable[] array2 = new IRenderable[list.Count];
						char[] array3 = new char[list.Count];
						char c = 'a';
						for (int i = 0; i < list.Count; i++)
						{
							gameObject = list[i];
							array[i] = gameObject.ShortDisplayName;
							array2[i] = gameObject.RenderForUI(null, false);
							char[] array4 = array3;
							int num = i;
							int num2;
							if (c > 'z')
							{
								num2 = 32;
							}
							else
							{
								c = (char)((num2 = (int)c) + 1);
							}
							array4[num] = num2;
							if (gameObject.CurrentCell == null)
							{
								array[i] = string.Concat(new string[]
								{
									"{{R|destroyed}} ",
									array[i],
									"\nÿÿÿÿ{{C|1 dram of ",
									LiquidVolume.GetLiquid("sunslag").Name,
									" to reshape}}"
								});
							}
						}
						int num3 = Popup.PickOption("Choose a " + this.VehicleTerm + " to recall", null, "", "Sounds/UI/ui_notification", array, array3, array2, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
						if (num3 < 0 || num3 >= list.Count)
						{
							return false;
						}
						gameObject = list[num3];
					}
					bool flag = gameObject.CurrentCell == null;
					if (flag && !E.Actor.UseDrams(1, "sunslag", null, null, null, null, false))
					{
						return E.Actor.ShowFailure("You do not have 1 dram of " + LiquidVolume.GetLiquid("sunslag").GetName() + ".");
					}
					if (base.ConsumeCharge(null, null) && gameObject.TeleportTo(cell, new int?(0), true, false, false, false, "disappear", null))
					{
						The.ZoneManager.CachedObjects.Remove(gameObject.ID);
						gameObject.Physics.DidXToY("are", flag ? "reshaped at" : "transported to", activePartFirstSubject, null, "!", null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, E.Actor.IsPlayer(), null);
						gameObject.TeleportSwirl(null, "&C", true, this.Sound, 'ù', false);
						Vehicle part = gameObject.GetPart<Vehicle>();
						if (!gameObject.IsPlayer() && part != null && part.Follower)
						{
							gameObject.PartyLeader = E.Actor;
							gameObject.Brain.Stay(cell);
						}
						if (flag)
						{
							gameObject.RestorePristineHealth(true, false);
							gameObject.FireEvent(Event.New("Regenera", "Level", 10, "Source", this.ParentObject));
							gameObject.DustPuff("&y", 15);
							RepairedEvent.Send(E.Actor, gameObject, this.ParentObject, null, 0);
						}
						TetheredOnboardRecoilerTeleporter part2 = gameObject.GetPart<TetheredOnboardRecoilerTeleporter>();
						if (part2 != null)
						{
							part2.SetDestination(cell);
						}
						E.RequestInterfaceExit();
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004BE6 RID: 19430
		public string AdjacentBlueprint;

		// Token: 0x04004BE7 RID: 19431
		public string RequiredBlueprint;

		// Token: 0x04004BE8 RID: 19432
		public string RequiredType;

		// Token: 0x04004BE9 RID: 19433
		public string Sound = "Sounds/Interact/sfx_interact_vehicle_recall";

		// Token: 0x04004BEA RID: 19434
		[NonSerialized]
		private string _VehicleTerm;
	}
}
