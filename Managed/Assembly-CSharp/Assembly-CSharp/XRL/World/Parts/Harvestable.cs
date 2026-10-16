using System;
using System.Collections.Generic;
using XRL.Language;
using XRL.Rules;
using XRL.UI;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x02001049 RID: 4169
	[Serializable]
	public class Harvestable : IPart
	{
		// Token: 0x0600A5EE RID: 42478 RVA: 0x003CB454 File Offset: 0x003C9654
		public void UpdateRipeStatus(bool newRipeStatus)
		{
			if (newRipeStatus)
			{
				this.RegenTimer = int.MaxValue;
			}
			else if (this.Ripe && this.RegenTime != "")
			{
				this.RegenTimer = Stat.Roll(this.RegenTime, null);
			}
			this.Ripe = newRipeStatus;
			if (this.Ripe)
			{
				if (!this.RipeTiles.IsNullOrEmpty())
				{
					List<string> list = this.RipeTiles.CachedCommaExpansion();
					if (this.TileIndex < 0)
					{
						this.TileIndex = Stat.Rand.Next(0, list.Count);
					}
					if (this.TileIndex < list.Count)
					{
						this.ParentObject.Render.Tile = list[this.TileIndex];
					}
				}
				if (!this.RipeRenderString.IsNullOrEmpty())
				{
					this.ParentObject.Render.RenderString = this.RipeRenderString;
				}
				if (!this.RipeColor.IsNullOrEmpty())
				{
					this.ParentObject.Render.ColorString = this.RipeColor;
				}
				if (!this.RipeTileColor.IsNullOrEmpty())
				{
					this.ParentObject.Render.TileColor = this.RipeTileColor;
				}
				if (!this.RipeDetailColor.IsNullOrEmpty())
				{
					this.ParentObject.Render.DetailColor = this.RipeDetailColor;
					return;
				}
			}
			else
			{
				if (!this.UnripeTiles.IsNullOrEmpty())
				{
					List<string> list2 = this.UnripeTiles.CachedCommaExpansion();
					if (this.TileIndex < 0)
					{
						this.TileIndex = Stat.Rand.Next(0, list2.Count);
					}
					if (this.TileIndex < list2.Count)
					{
						this.ParentObject.Render.Tile = list2[this.TileIndex];
					}
				}
				if (!this.UnripeRenderString.IsNullOrEmpty())
				{
					this.ParentObject.Render.RenderString = this.UnripeRenderString;
				}
				if (!this.UnripeColor.IsNullOrEmpty())
				{
					this.ParentObject.Render.ColorString = this.UnripeColor;
				}
				if (!this.UnripeTileColor.IsNullOrEmpty())
				{
					this.ParentObject.Render.TileColor = this.UnripeTileColor;
				}
				if (!this.UnripeDetailColor.IsNullOrEmpty())
				{
					this.ParentObject.Render.DetailColor = this.UnripeDetailColor;
				}
			}
		}

		// Token: 0x0600A5EF RID: 42479 RVA: 0x003CB694 File Offset: 0x003C9894
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AfterObjectCreatedEvent.ID || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == ObjectEnteringCellEvent.ID;
		}

		// Token: 0x0600A5F0 RID: 42480 RVA: 0x003CB6E5 File Offset: 0x003C98E5
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return (!this.IsHarvestable() || !E.Actor.HasPart<CookingAndGathering_Harvestry>()) && base.HandleEvent(E);
		}

		// Token: 0x0600A5F1 RID: 42481 RVA: 0x003CB705 File Offset: 0x003C9905
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			this.AttemptHarvest(E.Actor, false, null, null, null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5F2 RID: 42482 RVA: 0x003CB71F File Offset: 0x003C991F
		public override bool HandleEvent(EndTurnEvent E)
		{
			this.Ripen();
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5F3 RID: 42483 RVA: 0x003CB730 File Offset: 0x003C9930
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.IsHarvestable() && E.Actor.HasPart<CookingAndGathering_Harvestry>())
			{
				E.AddAction("Harvest", "harvest", "Harvest", null, 'h', false, 15, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5F4 RID: 42484 RVA: 0x003CB77D File Offset: 0x003C997D
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Harvest")
			{
				this.AttemptHarvest(E.Actor, E.Auto, null, E.FromCell, E.Generated);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5F5 RID: 42485 RVA: 0x003CB7B8 File Offset: 0x003C99B8
		public override bool HandleEvent(ObjectEnteringCellEvent E)
		{
			if (this.AttemptHarvest(E.Object, true, null, E.Cell, null))
			{
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5F6 RID: 42486 RVA: 0x003CB7DE File Offset: 0x003C99DE
		public override bool HandleEvent(AfterObjectCreatedEvent E)
		{
			if (Stat.Chance(this.StartRipeChance))
			{
				this.UpdateRipeStatus(true);
			}
			else
			{
				this.UpdateRipeStatus(false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5F7 RID: 42487 RVA: 0x003CB804 File Offset: 0x003C9A04
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A5F8 RID: 42488 RVA: 0x003CB807 File Offset: 0x003C9A07
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AccelerateRipening");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A5F9 RID: 42489 RVA: 0x003CB81C File Offset: 0x003C9A1C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AccelerateRipening")
			{
				this.Ripen();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A5FA RID: 42490 RVA: 0x003CB840 File Offset: 0x003C9A40
		public bool AttemptHarvest(GameObject who, bool Automatic = false, string Verb = null, Cell FromCell = null, List<GameObject> Tracking = null)
		{
			if (!this.IsHarvestable() || !who.HasPart<CookingAndGathering_Harvestry>())
			{
				return false;
			}
			if (Automatic)
			{
				if (this.ParentObject.IsImportant())
				{
					return false;
				}
				if (who.IsPlayer())
				{
					if (who.ArePerceptibleHostilesNearby(false, false, null, null, null, Options.AutoexploreIgnoreEasyEnemies, Options.AutoexploreIgnoreDistantEnemies, false, false))
					{
						return false;
					}
				}
				else if (who.Target != null)
				{
					return false;
				}
				CookingAndGathering_Harvestry part = who.GetPart<CookingAndGathering_Harvestry>();
				if (part != null && !part.IsMyActivatedAbilityToggledOn(part.ActivatedAbilityID, null))
				{
					return false;
				}
			}
			if (!who.CheckFrozen(false, false, false, null))
			{
				return false;
			}
			this.ParentObject.SplitFromStack();
			if (!this.ParentObject.ConfirmUseImportant(who, "harvest", null, -1))
			{
				this.ParentObject.CheckStack();
				return false;
			}
			bool StoredByPlayer = this.ParentObject.GetIntProperty("StoredByPlayer", 0) > 0;
			Action<GameObject> action = delegate(GameObject obj)
			{
				if (StoredByPlayer)
				{
					obj.SetIntProperty("FromStoredByPlayer", 1, false);
				}
				Event @event = Event.New("ObjectExtracted", 0, 0, 0);
				@event.SetParameter("Object", obj);
				@event.SetParameter("Source", this.ParentObject);
				@event.SetParameter("Actor", who);
				@event.SetParameter("Action", "Harvest");
				obj.FireEvent(@event);
			};
			Cell currentCell = this.ParentObject.GetCurrentCell();
			Cell cell = FromCell ?? who.GetCurrentCell();
			string text = null;
			if (currentCell != null && currentCell != cell)
			{
				if (who.IsPlayer())
				{
					text = "the " + Directions.GetExpandedDirection(cell.GetDirectionFromCell(currentCell, false));
				}
				else
				{
					text = who.its + " " + Directions.GetExpandedDirection(cell.GetDirectionFromCell(currentCell, false));
				}
			}
			string text2 = Verb ?? this.HarvestVerb;
			if ((this.ParentObject.IsTemporary ? 0 : this.OnSuccessAmount.RollCached()) > 0)
			{
				int num = this.OnSuccessAmount.RollCached();
				this.ParentObject.PlayWorldSound("Sounds/Interact/sfx_interact_plant_harvest", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				if (this.OnSuccess[0] == '@')
				{
					GameObject gameObject = GameObject.Create(PopulationManager.RollOneFrom(this.OnSuccess.Substring(1), null, null).Blueprint, 0, 0, null, null, action, null, null);
					if (!text2.IsNullOrEmpty())
					{
						IComponent<GameObject>.WDidXToYWithZ(who, text2, this.ParentObject, (text == null) ? "into" : ("to " + text + " into"), gameObject, null, null, null, null, who, null, false, false, true, true, false, false, false, false, null, null, null, null, false, false, false, !Automatic, false, null);
					}
					else
					{
						IComponent<GameObject>.XDidYToZ(who, "harvest", gameObject, (text == null) ? null : ("from " + text), null, null, null, who, null, false, false, true, false, false, null, null, null, false, false, false, false, false, null);
					}
					who.TakeObject(gameObject, false, true, new int?(0), null, Tracking);
				}
				else
				{
					GameObject gameObject2 = GameObject.Create(this.OnSuccess, 0, 0, null, null, action, null, null);
					if (!text2.IsNullOrEmpty())
					{
						if (num <= 1)
						{
							IComponent<GameObject>.WDidXToYWithZ(who, text2, this.ParentObject, (text == null) ? "into" : ("to " + text + " into"), gameObject2, null, null, null, null, who, null, false, false, true, true, false, false, false, false, null, null, null, null, false, false, false, !Automatic, false, null);
						}
						else if (gameObject2.IsPlural)
						{
							IComponent<GameObject>.XDidYToZ(who, text2, this.ParentObject, string.Concat(new string[]
							{
								(text == null) ? "" : ("to " + text + " "),
								"into ",
								Grammar.Cardinal(num),
								" ",
								gameObject2.ShortDisplayName
							}), null, null, null, who, null, false, false, true, false, false, null, null, null, false, false, false, Automatic, false, null);
						}
						else
						{
							IComponent<GameObject>.XDidYToZ(who, text2, this.ParentObject, string.Concat(new string[]
							{
								(text == null) ? "" : ("to " + text + " "),
								"into ",
								Grammar.Cardinal(num),
								" ",
								Grammar.Pluralize(gameObject2.ShortDisplayName)
							}), null, null, null, who, null, false, false, true, false, false, null, null, null, false, false, false, Automatic, false, null);
						}
					}
					else if (who.IsPlayer())
					{
						if (num <= 1)
						{
							IComponent<GameObject>.EmitMessage(who, "You harvest " + gameObject2.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + ((text == null) ? "" : (" from " + text)) + ".", ' ', !Automatic, false, false, null, null);
						}
						else if (gameObject2.IsPlural)
						{
							IComponent<GameObject>.EmitMessage(who, string.Concat(new string[]
							{
								"You harvest ",
								Grammar.Cardinal(num),
								" ",
								gameObject2.ShortDisplayName,
								(text == null) ? "" : (" from " + text),
								"."
							}), ' ', !Automatic, false, false, null, null);
						}
						else
						{
							IComponent<GameObject>.EmitMessage(who, string.Concat(new string[]
							{
								"You harvest ",
								Grammar.Cardinal(num),
								" ",
								Grammar.Pluralize(gameObject2.ShortDisplayName),
								(text == null) ? "" : (" from " + text),
								"."
							}), ' ', !Automatic, false, false, null, null);
						}
					}
					else if (who.IsVisible())
					{
						if (num <= 1)
						{
							IComponent<GameObject>.EmitMessage(who, string.Concat(new string[]
							{
								who.Does("harvest", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
								" ",
								gameObject2.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
								(text == null) ? "" : (" from " + text),
								"."
							}), ' ', !Automatic, false, false, null, null);
						}
						else if (gameObject2.IsPlural)
						{
							IComponent<GameObject>.EmitMessage(who, string.Concat(new string[]
							{
								who.Does("harvest", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
								" ",
								Grammar.Cardinal(num),
								" ",
								gameObject2.ShortDisplayName,
								(text == null) ? "" : (" from " + text),
								"."
							}), ' ', !Automatic, false, false, null, null);
						}
						else
						{
							IComponent<GameObject>.EmitMessage(who, string.Concat(new string[]
							{
								who.Does("harvest", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
								" ",
								Grammar.Cardinal(num),
								" ",
								Grammar.Pluralize(gameObject2.ShortDisplayName),
								(text == null) ? "" : (" from " + text),
								"."
							}), ' ', !Automatic, false, false, null, null);
						}
					}
					for (int i = 0; i < num; i++)
					{
						who.TakeObject(this.OnSuccess, false, true, new int?(0), 0, 0, null, null, Tracking, null, action);
					}
				}
				this.UpdateRipeStatus(false);
				if (this.DestroyOnHarvest)
				{
					this.ParentObject.Destroy(null, false, false, null);
				}
				who.UseEnergy(1000, "Skill", null, null, false);
				this.ParentObject.CheckStack();
				return true;
			}
			if (who.IsPlayer())
			{
				IComponent<GameObject>.EmitMessage(who, "There is nothing left to harvest.", ' ', !Automatic, false, false, null, null);
			}
			return false;
		}

		// Token: 0x0600A5FB RID: 42491 RVA: 0x003CC0CC File Offset: 0x003CA2CC
		public void Ripen()
		{
			if (this.RegenTimer < 2147483647)
			{
				this.RegenTimer--;
				if (this.RegenTimer <= 0)
				{
					if (Stat.Chance(this.RipeTimerChance))
					{
						this.UpdateRipeStatus(true);
						return;
					}
					this.RegenTimer = this.RegenTime.RollCached();
				}
			}
		}

		// Token: 0x0600A5FC RID: 42492 RVA: 0x003CC124 File Offset: 0x003CA324
		public bool IsHarvestable()
		{
			return this.Ripe && this.ParentObject.Render.Visible && (!this.ParentObject.HasPart<HologramMaterial>() || this.ParentObject.Physics.CurrentCell.ParentZone.ZoneID.StartsWith("ThinWorld."));
		}

		// Token: 0x04004483 RID: 17539
		public bool DestroyOnHarvest;

		// Token: 0x04004484 RID: 17540
		public string OnSuccess = "Vinewafer";

		// Token: 0x04004485 RID: 17541
		public string OnSuccessAmount = "1";

		// Token: 0x04004486 RID: 17542
		public string RipeTiles = "";

		// Token: 0x04004487 RID: 17543
		public string RipeRenderString = "";

		// Token: 0x04004488 RID: 17544
		public string RipeColor = "";

		// Token: 0x04004489 RID: 17545
		public string RipeTileColor = "";

		// Token: 0x0400448A RID: 17546
		public string RipeDetailColor = "";

		// Token: 0x0400448B RID: 17547
		public string UnripeTiles = "";

		// Token: 0x0400448C RID: 17548
		public string UnripeRenderString = "";

		// Token: 0x0400448D RID: 17549
		public string UnripeColor = "";

		// Token: 0x0400448E RID: 17550
		public string UnripeTileColor = "";

		// Token: 0x0400448F RID: 17551
		public string UnripeDetailColor = "";

		// Token: 0x04004490 RID: 17552
		public int TileIndex = -1;

		// Token: 0x04004491 RID: 17553
		public bool Ripe;

		// Token: 0x04004492 RID: 17554
		public string StartRipeChance = "1:1";

		// Token: 0x04004493 RID: 17555
		public int RegenTimer = int.MaxValue;

		// Token: 0x04004494 RID: 17556
		public string RegenTime = "";

		// Token: 0x04004495 RID: 17557
		public string RipeTimerChance = "1:1";

		// Token: 0x04004496 RID: 17558
		public string HarvestVerb;
	}
}
