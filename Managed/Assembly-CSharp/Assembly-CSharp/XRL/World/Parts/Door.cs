using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Parts.Mutation;
using XRL.World.Tinkering;

namespace XRL.World.Parts
{
	// Token: 0x02000FA1 RID: 4001
	[Serializable]
	public class Door : IPart, IHackingSifrahHandler
	{
		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06009ECB RID: 40651 RVA: 0x00398793 File Offset: 0x00396993
		// (set) Token: 0x06009ECC RID: 40652 RVA: 0x0039879B File Offset: 0x0039699B
		[Obsolete("mod compat, will be removed after Q2 2024")]
		public bool bOpen
		{
			get
			{
				return this.Open;
			}
			set
			{
				this.Open = value;
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06009ECD RID: 40653 RVA: 0x003987A4 File Offset: 0x003969A4
		// (set) Token: 0x06009ECE RID: 40654 RVA: 0x003987AC File Offset: 0x003969AC
		[Obsolete("mod compat, will be removed after Q2 2024")]
		public bool bLocked
		{
			get
			{
				return this.Locked;
			}
			set
			{
				this.Locked = value;
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06009ED0 RID: 40656 RVA: 0x003987C3 File Offset: 0x003969C3
		// (set) Token: 0x06009ECF RID: 40655 RVA: 0x003987B5 File Offset: 0x003969B5
		public int SecurityClearance
		{
			get
			{
				return XRL.World.Capabilities.SecurityClearance.GetSecurityClearanceByKeySpecification(this.KeyObject);
			}
			set
			{
				XRL.World.Capabilities.SecurityClearance.HandleSecurityClearanceSpecification(value, ref this.KeyObject);
			}
		}

		// Token: 0x06009ED1 RID: 40657 RVA: 0x003987D0 File Offset: 0x003969D0
		public override bool SameAs(IPart p)
		{
			Door door = p as Door;
			return !(door.ClosedDisplay != this.ClosedDisplay) && !(door.OpenDisplay != this.OpenDisplay) && !(door.ClosedTile != this.ClosedTile) && !(door.OpenTile != this.OpenTile) && door.SyncRender == this.SyncRender && !(door.KeyObject != this.KeyObject) && door.SyncAdjacent == this.SyncAdjacent && door.OccludingWhileClosed == this.OccludingWhileClosed && door.Locked == this.Locked && door.WasLocked == this.WasLocked && door.Open == this.Open;
		}

		// Token: 0x06009ED2 RID: 40658 RVA: 0x003988B0 File Offset: 0x00396AB0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AnimateEvent>.ID || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEvent.ID || ID == EnteredCellEvent.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == ObjectCreatedEvent.ID || ID == ObjectEnteredCellEvent.ID;
		}

		// Token: 0x06009ED3 RID: 40659 RVA: 0x00398909 File Offset: 0x00396B09
		public override bool HandleEvent(AnimateEvent E)
		{
			this.SyncAdjacent = false;
			return base.HandleEvent(E);
		}

		// Token: 0x06009ED4 RID: 40660 RVA: 0x0039891C File Offset: 0x00396B1C
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			Cell currentCell = base.currentCell;
			return (this.Open && E.MinPriority > 0) || (currentCell != null && currentCell.HasObjectWithPart("Campfire") && base.HandleEvent(E));
		}

		// Token: 0x06009ED5 RID: 40661 RVA: 0x0039895C File Offset: 0x00396B5C
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			Event @event = Event.New("Open", 0, 0, 0);
			@event.SetParameter("Opener", E.Actor);
			@event.SetParameter("Actor", E.Actor);
			@event.SetParameter("UsePopupsForFailures", true);
			this.ParentObject.FireEvent(@event);
			return base.HandleEvent(E);
		}

		// Token: 0x06009ED6 RID: 40662 RVA: 0x003989C4 File Offset: 0x00396BC4
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (this.SyncAdjacent)
			{
				this.PerformAdjacentSync();
			}
			if (!this.Open)
			{
				int i = 0;
				int count = E.Cell.Objects.Count;
				while (i < count)
				{
					GameObject gameObject = E.Cell.Objects[i];
					if (gameObject != E.Ignore && this.BlocksClosing(gameObject))
					{
						if (gameObject.IsCombatObject())
						{
							this.AttemptOpen(gameObject, false, false, true, true, true, false, E);
							if (this.Open)
							{
								break;
							}
						}
						else if (!this.Locked)
						{
							this.PerformOpen(null, null, true);
							if (this.Open)
							{
								break;
							}
						}
					}
					i++;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009ED7 RID: 40663 RVA: 0x00398A6C File Offset: 0x00396C6C
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			if (!this.Open && this.BlocksClosing(E.Object))
			{
				if (E.Object.IsCombat)
				{
					this.AttemptOpen(E.Object, false, false, true, false, false, false, E);
				}
				else if (!this.Locked)
				{
					this.PerformOpen(null, null, true);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009ED8 RID: 40664 RVA: 0x00398ACC File Offset: 0x00396CCC
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.Open)
			{
				E.AddAction("Close", "close", "Close", null, 'c', false, 10, 0, false, false, true, false, true, null, false);
			}
			else
			{
				E.AddAction("Open", "open", "Open", null, 'o', false, 10, 0, false, false, true, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009ED9 RID: 40665 RVA: 0x00398B34 File Offset: 0x00396D34
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Open" || E.Command == "Close")
			{
				Event @event = Event.New("Open", 0, 0, 0);
				@event.SetParameter("Opener", E.Actor);
				@event.SetParameter("Actor", E.Actor);
				@event.SetFlag("UsePopups", !E.Auto);
				@event.SetFlag("UsePopupsForFailures", true);
				if (!this.ParentObject.FireEvent(@event))
				{
					return false;
				}
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009EDA RID: 40666 RVA: 0x00398BD8 File Offset: 0x00396DD8
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (this.Open)
			{
				if (this.SyncRender)
				{
					this.ParentObject.Render.RenderString = this.OpenDisplay;
					this.ParentObject.Render.Tile = this.OpenTile;
				}
				this.ParentObject.Render.Occluding = false;
				this.ParentObject.Physics.Solid = false;
			}
			else
			{
				if (this.SyncRender)
				{
					this.ParentObject.Render.RenderString = this.ClosedDisplay;
					this.ParentObject.Render.Tile = this.ClosedTile;
				}
				if (this.OccludingWhileClosed)
				{
					this.ParentObject.Render.Occluding = true;
				}
				this.ParentObject.Physics.Solid = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009EDB RID: 40667 RVA: 0x00398CAA File Offset: 0x00396EAA
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009EDC RID: 40668 RVA: 0x00398CAD File Offset: 0x00396EAD
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforePhysicsRejectObjectEntringCell");
			Registrar.Register("Open");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009EDD RID: 40669 RVA: 0x00398CD0 File Offset: 0x00396ED0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforePhysicsRejectObjectEntringCell")
			{
				if (!this.ParentObject.IsCreature)
				{
					if (E.HasFlag("Actual"))
					{
						if (!this.Open)
						{
							GameObject gameObjectParameter = E.GetGameObjectParameter("Object");
							if (gameObjectParameter.Body != null)
							{
								Event @event = Event.New("Open", 0, 0, 0);
								@event.SetParameter("Opener", gameObjectParameter);
								@event.SetParameter("Actor", gameObjectParameter);
								@event.SetFlag("FromMove", true);
								this.ParentObject.FireEvent(@event);
							}
						}
					}
					else
					{
						GameObject gameObjectParameter2 = E.GetGameObjectParameter("Object");
						if (gameObjectParameter2.Body != null && this.CanOpen(gameObjectParameter2, false, false, false))
						{
							return false;
						}
					}
				}
			}
			else if (E.ID == "Open" && !this.AttemptOpen(E.GetGameObjectParameter("Actor") ?? E.GetGameObjectParameter("Opener"), E.HasFlag("UsePopups"), E.HasFlag("UsePopupsForFailures"), false, false, E.HasFlag("FromMove"), false, E))
			{
				return false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x06009EDE RID: 40670 RVA: 0x00398DFC File Offset: 0x00396FFC
		public bool CanOpen(GameObject Actor = null, bool IgnoreMobility = false, bool IgnoreSpecialConditions = false, bool FromMove = false)
		{
			if (this.Open)
			{
				return false;
			}
			if (!IgnoreMobility && Actor != null && !Actor.CanMoveExtremities("Open", false, false, true))
			{
				return false;
			}
			if (Actor != null && !Actor.PhaseMatches(this.ParentObject))
			{
				return false;
			}
			if (Actor != null && this.ParentObject.IsFlying && !Actor.IsFlying)
			{
				return false;
			}
			if (!IgnoreSpecialConditions && Actor != null && !Door.CapableOfOpening(Actor, this.ParentObject))
			{
				return false;
			}
			if (!this.Locked || Actor == null)
			{
				return true;
			}
			Event @event = Event.New("CanAttemptDoorUnlock", 0, 0, 0);
			@event.SetParameter("Opener", Actor);
			@event.SetParameter("Actor", Actor);
			@event.SetParameter("Door", this);
			if (!this.ParentObject.FireEvent(@event))
			{
				return false;
			}
			if (!this.KeyObject.IsNullOrEmpty())
			{
				List<string> list = this.KeyObject.CachedCommaExpansion();
				if ((list.Count > 1 || list[0] != "*Psychometry") && Actor.FindContainedObjectByAnyBlueprint(list) != null)
				{
					return true;
				}
				if (Actor.GetIntProperty("DoorUnlocker", 0) > 0)
				{
					return true;
				}
				if (list.Contains("*Psychometry") && base.UsePsychometry(Actor, this.ParentObject))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06009EDF RID: 40671 RVA: 0x00398F38 File Offset: 0x00397138
		public bool AttemptOpen(GameObject Actor = null, bool UsePopups = false, bool UsePopupsForFailures = false, bool IgnoreMobility = false, bool IgnoreSpecialConditions = false, bool FromMove = false, bool Silent = false, IEvent FromEvent = null)
		{
			if (!IgnoreMobility && Actor != null && !Actor.CanMoveExtremities("Open", !Silent, false, true))
			{
				return false;
			}
			string text;
			if (!IgnoreSpecialConditions && Actor != null && !Door.CapableOfOpening(Actor, this.ParentObject, out text))
			{
				if (!Silent && Actor != null && Actor.IsPlayer())
				{
					IComponent<GameObject>.EmitMessage(Actor, "You cannot open " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + text + ".", ' ', false, UsePopups || UsePopupsForFailures, false, null, null);
				}
				return false;
			}
			if (Actor != null && !Actor.PhaseMatches(this.ParentObject))
			{
				if (!Silent && Actor.IsPlayer())
				{
					IComponent<GameObject>.EmitMessage(Actor, "You are out of phase with " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", ' ', false, UsePopups || UsePopupsForFailures, false, null, null);
				}
				return false;
			}
			if (Actor != null && this.ParentObject.IsFlying && !Actor.IsFlying)
			{
				if (!Silent && Actor.IsPlayer())
				{
					IComponent<GameObject>.EmitMessage(Actor, "You cannot reach " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", ' ', false, UsePopups || UsePopupsForFailures, false, null, null);
				}
				return false;
			}
			if (!this.Open)
			{
				if (!IgnoreSpecialConditions && Actor != null && Actor.HasTag("Grazer"))
				{
					return false;
				}
				if (this.Locked)
				{
					if (Actor != null)
					{
						Event @event = Event.New("AttemptDoorUnlock", 0, 0, 0);
						@event.SetParameter("Actor", Actor);
						@event.SetParameter("Opener", Actor);
						@event.SetParameter("Door", this);
						if (!this.ParentObject.FireEvent(@event))
						{
							return false;
						}
						if (!this.KeyObject.IsNullOrEmpty())
						{
							if (this.ParentObject.DistanceTo(Actor) > 1)
							{
								if (!Silent && Actor.IsPlayer())
								{
									IComponent<GameObject>.EmitMessage(Actor, "You can't unlock " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " from a distance.", ' ', false, UsePopups || UsePopupsForFailures, false, null, null);
								}
								return false;
							}
							List<string> list = this.KeyObject.CachedCommaExpansion();
							if (list.Count > 1 || list[0] != "*Psychometry")
							{
								GameObject obj = Actor.FindContainedObjectByAnyBlueprint(list);
								if (obj != null)
								{
									if (!Silent && Actor.IsPlayer())
									{
										this.PerformOpen(Actor, delegate
										{
											IComponent<GameObject>.EmitMessage(Actor, obj.Does("unlock", int.MaxValue, null, null, null, false, true, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", ' ', false, UsePopups, false, null, null);
										}, false);
									}
									else
									{
										this.PerformOpen(Actor, null, false);
									}
								}
							}
							if (this.Locked && Actor.GetIntProperty("DoorUnlocker", 0) > 0)
							{
								if (!Silent && Actor.IsPlayer())
								{
									this.PerformOpen(Actor, delegate
									{
										IComponent<GameObject>.EmitMessage(Actor, string.Concat(new string[]
										{
											"You interface with ",
											this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
											" and unlock ",
											this.ParentObject.them,
											"."
										}), ' ', false, UsePopups, false, null, null);
									}, false);
								}
								else
								{
									this.PerformOpen(Actor, null, false);
								}
							}
							if (this.Locked && list.Contains("*Psychometry") && base.UsePsychometry(Actor, this.ParentObject))
							{
								if (!Silent && Actor.IsPlayer())
								{
									this.PerformOpen(Actor, delegate
									{
										IComponent<GameObject>.EmitMessage(Actor, string.Concat(new string[]
										{
											"You lay your hand upon ",
											this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
											" and draw forth ",
											this.ParentObject.its,
											" passcode. You enter the code and ",
											this.ParentObject.does("unlock", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
											"."
										}), ' ', false, UsePopups, false, null, null);
									}, false);
								}
								else
								{
									this.PerformOpen(Actor, null, false);
								}
							}
							if (this.Locked && Actor.IsPlayer() && this.IsHackable() && Options.SifrahHacking && this.ParentObject.GetIntProperty("SifrahHack", 0) >= 0)
							{
								int num = XRL.World.Capabilities.SecurityClearance.GetSecurityClearanceByKeySpecification(this.KeyObject);
								if (!this.KeyObject.CachedCommaExpansion().Contains("*Psychometry"))
								{
									num += 2;
								}
								HackingSifrah hackingSifrah = new HackingSifrah(this.ParentObject, num, num, Actor.Stat("Intelligence", 0));
								hackingSifrah.HandlerID = this.ParentObject.ID;
								hackingSifrah.HandlerPartName = base.GetType().Name;
								hackingSifrah.Play(this.ParentObject);
								if (hackingSifrah.InterfaceExitRequested && FromEvent != null)
								{
									FromEvent.RequestInterfaceExit();
								}
								if (this.ParentObject.GetIntProperty("SifrahHack", 0) > 0)
								{
									this.ParentObject.ModIntProperty("SifrahHack", -1, true);
									this.PerformOpen(Actor, null, false);
								}
							}
						}
						if (this.Locked)
						{
							if (!Silent && Actor.IsPlayer() && (!FromMove || !this.ParentObject.IsCreature))
							{
								base.PlayWorldSound("sfx_interact_door_locked_rattle", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
								IComponent<GameObject>.EmitMessage(Actor, "You can't unlock " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", ' ', false, UsePopups || UsePopupsForFailures, false, null, null);
							}
							return false;
						}
					}
				}
				else
				{
					this.PerformOpen(Actor, null, FromMove);
				}
			}
			else if (!this.AttemptClose(Actor, UsePopups, UsePopupsForFailures, IgnoreMobility, IgnoreSpecialConditions, FromMove, Silent))
			{
				return false;
			}
			return true;
		}

		// Token: 0x06009EE0 RID: 40672 RVA: 0x00399589 File Offset: 0x00397789
		private bool ShouldSync(Door d)
		{
			return d != null && d.SyncAdjacent && d.KeyObject == this.KeyObject;
		}

		// Token: 0x06009EE1 RID: 40673 RVA: 0x003995AC File Offset: 0x003977AC
		private void SyncAdjacentCell(Cell C)
		{
			int i = 0;
			int count = C.Objects.Count;
			while (i < count)
			{
				Door part = C.Objects[i].GetPart<Door>();
				if (this.ShouldSync(part))
				{
					if (part.Locked != this.Locked)
					{
						if (this.Locked)
						{
							part.Lock();
						}
						else
						{
							part.Unlock();
						}
					}
					if (part.Open != this.Open)
					{
						if (this.Open)
						{
							part.PerformOpen(null, null, false);
						}
						else
						{
							part.PerformClose(null, false);
						}
					}
				}
				i++;
			}
		}

		// Token: 0x06009EE2 RID: 40674 RVA: 0x00399639 File Offset: 0x00397839
		public void PerformAdjacentSync()
		{
			if (this.ParentObject.CurrentCell == null)
			{
				return;
			}
			this.ParentObject.CurrentCell.ForeachCardinalAdjacentCell(new Action<Cell>(this.SyncAdjacentCell));
		}

		// Token: 0x06009EE3 RID: 40675 RVA: 0x00399668 File Offset: 0x00397868
		public void PerformOpen(GameObject Actor = null, Action Message = null, bool FromMove = false)
		{
			if (this.Locked)
			{
				this.WasLocked = true;
				this.Locked = false;
			}
			this.Open = true;
			if (this.SyncRender)
			{
				this.ParentObject.Render.RenderString = this.OpenDisplay;
				this.ParentObject.Render.Tile = this.OpenTile;
			}
			this.ParentObject.Render.Occluding = false;
			this.ParentObject.Physics.Solid = false;
			Zone.SoundMapDirty = true;
			if (Message != null)
			{
				Message();
			}
			base.PlayWorldSound(this.ParentObject.GetPropertyOrTag("OpenSound", null), 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			if (this.ParentObject.CurrentCell != null)
			{
				this.ParentObject.CurrentCell.ClearOccludeCache();
				this.ParentObject.CurrentCell.FlushNavigationCache();
			}
			if (this.SyncAdjacent)
			{
				this.PerformAdjacentSync();
			}
			if (this.ParentObject.HasRegisteredEvent("Opened"))
			{
				this.ParentObject.FireEvent(Event.New("Opened", "Object", this.ParentObject));
			}
			if (Actor != null)
			{
				if (Actor.HasRegisteredEvent("Opened"))
				{
					Actor.FireEvent(Event.New("Opened", "Object", this.ParentObject));
				}
				if (!FromMove)
				{
					Actor.UseEnergy(1000, "Door Open", null, null, false);
				}
			}
		}

		// Token: 0x06009EE4 RID: 40676 RVA: 0x003997E8 File Offset: 0x003979E8
		public bool AttemptClose(GameObject Actor = null, bool UsePopups = false, bool UsePopupsForFailures = false, bool IgnoreMobility = false, bool IgnoreSpecialConditions = false, bool FromMove = false, bool Silent = false)
		{
			if (this.ParentObject.HasTagOrProperty("NoClose"))
			{
				if (!Silent && Actor != null && Actor.IsPlayer())
				{
					IComponent<GameObject>.EmitMessage(Actor, this.ParentObject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " cannot be closed.", ' ', false, UsePopups || UsePopupsForFailures, false, null, null);
				}
				return false;
			}
			if (!IgnoreMobility && Actor != null && !Actor.CanMoveExtremities("Close", !Silent, false, true))
			{
				return false;
			}
			string text;
			if (!IgnoreSpecialConditions && Actor != null && !Door.CapableOfClosing(Actor, this.ParentObject, out text))
			{
				if (!Silent && Actor != null && Actor.IsPlayer())
				{
					IComponent<GameObject>.EmitMessage(Actor, "You cannot close " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + text + ".", ' ', false, UsePopups || UsePopupsForFailures, false, null, null);
				}
				return false;
			}
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell != null)
			{
				int i = 0;
				int count = currentCell.Objects.Count;
				while (i < count)
				{
					GameObject gameObject = currentCell.Objects[i];
					if (this.BlocksClosing(gameObject))
					{
						if (!Silent && (gameObject.IsPlayer() || (Actor != null && Actor.IsPlayer())))
						{
							IComponent<GameObject>.EmitMessage(Actor, this.ParentObject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " cannot be closed with " + (gameObject.IsPlayer() ? "you" : gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false)) + " in the way.", ' ', false, UsePopups || UsePopupsForFailures, false, null, null);
						}
						return false;
					}
					i++;
				}
				if (this.SyncAdjacent)
				{
					List<Cell> cardinalAdjacentCells = currentCell.GetCardinalAdjacentCells(false, true, false);
					int j = 0;
					int count2 = cardinalAdjacentCells.Count;
					while (j < count2)
					{
						Cell cell = cardinalAdjacentCells[j];
						Door door = null;
						int k = 0;
						int count3 = cell.Objects.Count;
						while (k < count3)
						{
							Door part = cell.Objects[k].GetPart<Door>();
							if (this.ShouldSync(part))
							{
								door = part;
								break;
							}
							k++;
						}
						if (door != null)
						{
							int l = 0;
							int count4 = cell.Objects.Count;
							while (l < count4)
							{
								GameObject gameObject2 = cell.Objects[l];
								if (door.BlocksClosing(gameObject2))
								{
									if (!Silent && (gameObject2.IsPlayer() || (Actor != null && Actor.IsPlayer())))
									{
										IComponent<GameObject>.EmitMessage(Actor, string.Concat(new string[]
										{
											this.ParentObject.T(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
											" cannot be closed with ",
											gameObject2.IsPlayer() ? "you" : gameObject2.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
											" in the way to the ",
											Directions.GetExpandedDirection(currentCell.GetDirectionFromCell(cell, false)),
											"."
										}), ' ', false, UsePopups || UsePopupsForFailures, false, null, null);
									}
									return false;
								}
								l++;
							}
						}
						j++;
					}
				}
			}
			this.PerformClose(Actor, FromMove);
			return !this.Open;
		}

		// Token: 0x06009EE5 RID: 40677 RVA: 0x00399B84 File Offset: 0x00397D84
		public void PerformClose(GameObject Actor = null, bool FromMove = false)
		{
			if (this.ParentObject.HasTagOrProperty("NoClose"))
			{
				return;
			}
			this.Locked = this.WasLocked;
			this.Open = false;
			if (this.SyncRender)
			{
				this.ParentObject.Render.RenderString = this.ClosedDisplay;
				this.ParentObject.Render.Tile = this.ClosedTile;
			}
			this.ParentObject.Render.Occluding = true;
			this.ParentObject.Physics.Solid = true;
			Zone.SoundMapDirty = true;
			base.PlayWorldSound(this.ParentObject.GetPropertyOrTag("CloseSound", null), 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			if (this.ParentObject.CurrentCell != null)
			{
				this.ParentObject.CurrentCell.ClearOccludeCache();
				this.ParentObject.CurrentCell.FlushNavigationCache();
			}
			if (this.SyncAdjacent)
			{
				this.PerformAdjacentSync();
			}
			if (this.ParentObject.HasRegisteredEvent("Closed"))
			{
				this.ParentObject.FireEvent(Event.New("Closed", "Object", this.ParentObject));
			}
			if (Actor != null)
			{
				if (Actor.HasRegisteredEvent("Closed"))
				{
					Actor.FireEvent(Event.New("Closed", "Object", this.ParentObject));
				}
				if (!FromMove)
				{
					Actor.UseEnergy(1000, "Door Close", null, null, false);
				}
			}
		}

		// Token: 0x06009EE6 RID: 40678 RVA: 0x00399D04 File Offset: 0x00397F04
		public void Unlock()
		{
			if (!this.Locked)
			{
				return;
			}
			this.WasLocked = true;
			this.Locked = false;
			base.PlayWorldSound(this.ParentObject.GetPropertyOrTag("UnlockSound", null), 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			if (this.SyncAdjacent)
			{
				this.PerformAdjacentSync();
			}
		}

		// Token: 0x06009EE7 RID: 40679 RVA: 0x00399D70 File Offset: 0x00397F70
		public void Lock()
		{
			if (this.Locked)
			{
				return;
			}
			if (this.Open)
			{
				this.PerformClose(null, false);
			}
			this.Locked = true;
			base.PlayWorldSound(this.ParentObject.GetPropertyOrTag("LockSound", null), 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			if (this.SyncAdjacent)
			{
				this.PerformAdjacentSync();
			}
		}

		// Token: 0x06009EE8 RID: 40680 RVA: 0x00399DE4 File Offset: 0x00397FE4
		public bool CanPathThrough(GameObject who)
		{
			if (this.Open)
			{
				return true;
			}
			if (!Door.CapableOfOpening(who, this.ParentObject))
			{
				return false;
			}
			if (!this.Locked)
			{
				return true;
			}
			if (!this.KeyObject.IsNullOrEmpty() && who != null)
			{
				List<string> list = this.KeyObject.CachedCommaExpansion();
				if ((list.Count > 1 || list[0] != "*Psychometry") && who.ContainsAnyBlueprint(list))
				{
					return true;
				}
				if (who.GetIntProperty("DoorUnlocker", 0) > 0)
				{
					return true;
				}
				if (list.Contains("*Psychometry") && base.ShouldUsePsychometry(who))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06009EE9 RID: 40681 RVA: 0x00399E84 File Offset: 0x00398084
		public bool BlocksClosing(GameObject obj)
		{
			return obj != this.ParentObject && obj.GetMatterPhase() == 1 && obj.HasPart<FungalVision>() == this.ParentObject.HasPart<FungalVision>() && (obj.HasTag("Creature") || obj.HasPropertyOrTag("BlocksDoors") || obj.ConsiderSolidFor(this.ParentObject, false)) && obj.PhaseAndFlightMatches(this.ParentObject) && !obj.HasTag("ExcavatoryTerrainFeature");
		}

		// Token: 0x06009EEA RID: 40682 RVA: 0x00399F07 File Offset: 0x00398107
		public bool IsHackable()
		{
			return !this.KeyObject.IsNullOrEmpty() && XRL.World.Capabilities.SecurityClearance.GetSecurityClearanceByKeySpecification(this.KeyObject) > 0;
		}

		// Token: 0x06009EEB RID: 40683 RVA: 0x00399F28 File Offset: 0x00398128
		public void HackingResultSuccess(GameObject who, GameObject obj, HackingSifrah game)
		{
			if (obj == this.ParentObject)
			{
				this.ParentObject.ModIntProperty("SifrahHack", 1, false);
				if (who.IsPlayer())
				{
					Popup.Show("You hack " + obj.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
		}

		// Token: 0x06009EEC RID: 40684 RVA: 0x00399FA0 File Offset: 0x003981A0
		public void HackingResultExceptionalSuccess(GameObject who, GameObject obj, HackingSifrah game)
		{
			if (obj == this.ParentObject)
			{
				this.ParentObject.ModIntProperty("SifrahHack", 1, false);
				if (!this.KeyObject.IsNullOrEmpty())
				{
					List<string> list = this.KeyObject.CachedCommaExpansion();
					int num = 0;
					while (++num < 10)
					{
						try
						{
							if (70.in100())
							{
								string randomBits = BitType.GetRandomBits("2d4".Roll(), obj.GetTechTier());
								if (!randomBits.IsNullOrEmpty())
								{
									who.RequirePart<BitLocker>(false).AddBits(randomBits);
									if (who.IsPlayer())
									{
										Popup.Show(string.Concat(new string[]
										{
											"You hack ",
											this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
											" and find tinkering bits <{{|",
											BitType.GetDisplayString(randomBits),
											"}}> in ",
											this.ParentObject.them,
											"!"
										}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
									}
									break;
								}
							}
							else
							{
								GameObject gameObject = GameObject.Create(list.GetRandomElement<string>(null));
								if (gameObject != null)
								{
									if (who.IsPlayer())
									{
										Popup.Show(string.Concat(new string[]
										{
											"You hack ",
											this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
											" and find ",
											gameObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
											" stuck in ",
											this.ParentObject.them,
											"!"
										}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
									}
									who.ReceiveObject(gameObject, false, null);
									break;
								}
							}
						}
						catch
						{
						}
					}
				}
			}
		}

		// Token: 0x06009EED RID: 40685 RVA: 0x0039A1B0 File Offset: 0x003983B0
		public void HackingResultPartialSuccess(GameObject who, GameObject obj, HackingSifrah game)
		{
			if (obj == this.ParentObject && who.IsPlayer())
			{
				Popup.Show("You feel like you're making progress on hacking " + obj.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " open.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
		}

		// Token: 0x06009EEE RID: 40686 RVA: 0x0039A214 File Offset: 0x00398414
		public void HackingResultFailure(GameObject who, GameObject obj, HackingSifrah game)
		{
			if (obj == this.ParentObject)
			{
				this.ParentObject.ModIntProperty("SifrahHack", -1, false);
				if (who.IsPlayer())
				{
					Popup.Show("You cannot seem to work out how to hack " + obj.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
		}

		// Token: 0x06009EEF RID: 40687 RVA: 0x0039A28C File Offset: 0x0039848C
		public void HackingResultCriticalFailure(GameObject who, GameObject obj, HackingSifrah game)
		{
			if (obj == this.ParentObject)
			{
				this.ParentObject.ModIntProperty("SifrahHack", -1, false);
				if (who.HasPart<Dystechnia>())
				{
					Dystechnia.CauseExplosion(this.ParentObject, who, null);
					game.RequestInterfaceExit();
					return;
				}
				if (who.IsPlayer())
				{
					Popup.Show("Your attempt to hack " + obj.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " has gone very wrong.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				List<Cell> localAdjacentCells = this.ParentObject.CurrentCell.GetLocalAdjacentCells();
				Cell currentCell = who.CurrentCell;
				this.ParentObject.Discharge((currentCell != null && localAdjacentCells.Contains(currentCell)) ? currentCell : localAdjacentCells.GetRandomElement<Cell>(null), "3d8".RollCached(), 0, "2d4", null, who, obj, null, null, null, null, null, 0, false, false, false);
			}
		}

		// Token: 0x06009EF0 RID: 40688 RVA: 0x0039A378 File Offset: 0x00398578
		public static bool CapableOfOpening(GameObject Object, GameObject Door, out string Reason)
		{
			Reason = "";
			if (Object == null)
			{
				return true;
			}
			if (!GameObject.Validate(ref Object))
			{
				return false;
			}
			if (Object.CanManipulateTelekinetically(Door))
			{
				return true;
			}
			if (Object.Stat("Intelligence", 0) < 7 && !Object.IsPlayer() && !Object.HasPropertyOrTag("Humanoid"))
			{
				Reason = " because you cannot figure out how";
				return false;
			}
			return !Object.HasTagOrProperty("CantOpenDoors") && !Object.HasTagOrProperty("Grazer");
		}

		// Token: 0x06009EF1 RID: 40689 RVA: 0x0039A3F4 File Offset: 0x003985F4
		public static bool CapableOfOpening(GameObject Object, GameObject Door = null)
		{
			string text;
			return Door.CapableOfOpening(Object, Door, out text);
		}

		// Token: 0x06009EF2 RID: 40690 RVA: 0x0039A40C File Offset: 0x0039860C
		public static bool CapableOfClosing(GameObject Object, GameObject Door, out string Reason)
		{
			Reason = "";
			if (Object == null)
			{
				return true;
			}
			if (!GameObject.Validate(ref Object))
			{
				return false;
			}
			if (Object.CanManipulateTelekinetically(Door))
			{
				return true;
			}
			if (Object.Stat("Intelligence", 0) < 7 && !Object.IsPlayer() && !Object.HasPropertyOrTag("Humanoid"))
			{
				Reason = " because you cannot figure out how";
				return false;
			}
			return !Object.HasTagOrProperty("CantCloseDoors");
		}

		// Token: 0x06009EF3 RID: 40691 RVA: 0x0039A478 File Offset: 0x00398678
		public static bool CapableOfClosing(GameObject Object, GameObject Door = null)
		{
			string text;
			return Door.CapableOfClosing(Object, Door, out text);
		}

		// Token: 0x040041B8 RID: 16824
		public bool Open;

		// Token: 0x040041B9 RID: 16825
		public bool Locked;

		// Token: 0x040041BA RID: 16826
		public bool WasLocked;

		// Token: 0x040041BB RID: 16827
		public string ClosedDisplay = "+";

		// Token: 0x040041BC RID: 16828
		public string OpenDisplay = "/";

		// Token: 0x040041BD RID: 16829
		public string ClosedTile = "Tiles/sw_door_basic.bmp";

		// Token: 0x040041BE RID: 16830
		public string OpenTile = "Tiles/sw_door_basic_open.bmp";

		// Token: 0x040041BF RID: 16831
		public bool SyncRender = true;

		// Token: 0x040041C0 RID: 16832
		public string KeyObject;

		// Token: 0x040041C1 RID: 16833
		public bool SyncAdjacent;

		// Token: 0x040041C2 RID: 16834
		public bool OccludingWhileClosed = true;
	}
}
