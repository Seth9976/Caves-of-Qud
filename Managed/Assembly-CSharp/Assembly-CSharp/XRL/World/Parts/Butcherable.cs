using System;
using System.Collections.Generic;
using XRL.Language;
using XRL.Rules;
using XRL.UI;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x02001004 RID: 4100
	[Serializable]
	public class Butcherable : IPart
	{
		// Token: 0x0600A365 RID: 41829 RVA: 0x003BB4CC File Offset: 0x003B96CC
		public override bool SameAs(IPart p)
		{
			Butcherable butcherable = p as Butcherable;
			return !(butcherable.OnSuccessAmount != this.OnSuccessAmount) && !(butcherable.OnSuccess != this.OnSuccess) && base.SameAs(p);
		}

		// Token: 0x0600A366 RID: 41830 RVA: 0x003BB511 File Offset: 0x003B9711
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == ObjectEnteringCellEvent.ID;
		}

		// Token: 0x0600A367 RID: 41831 RVA: 0x003BB538 File Offset: 0x003B9738
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.IsButcherable() && E.Actor.HasSkill("CookingAndGathering_Butchery"))
			{
				E.AddAction("Butcher", "butcher", "Butcher", null, 'b', false, 20, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A368 RID: 41832 RVA: 0x003BB58C File Offset: 0x003B978C
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Butcher" && this.AttemptButcher(E.Actor, E.Auto, false, false, null, E.FromCell, E.Generated))
			{
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A369 RID: 41833 RVA: 0x003BB5DB File Offset: 0x003B97DB
		public override bool HandleEvent(ObjectEnteringCellEvent E)
		{
			this.AttemptButcher(E.Object, true, false, false, null, E.Cell, null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A36A RID: 41834 RVA: 0x003BB5FC File Offset: 0x003B97FC
		public bool AttemptButcher(GameObject Actor, bool Automatic = false, bool SkipSkill = false, bool IntoInventory = false, string Verb = null, Cell FromCell = null, List<GameObject> Tracking = null)
		{
			if (!this.IsButcherable())
			{
				return false;
			}
			if (!SkipSkill && !Actor.HasSkill("CookingAndGathering_Butchery"))
			{
				return false;
			}
			if (Automatic)
			{
				if (this.ParentObject.IsImportant())
				{
					return false;
				}
				if (Actor.IsPlayer())
				{
					if (Actor.ArePerceptibleHostilesNearby(false, false, null, null, null, Options.AutoexploreIgnoreEasyEnemies, Options.AutoexploreIgnoreDistantEnemies, false, false))
					{
						return false;
					}
				}
				else if (Actor.Target != null)
				{
					return false;
				}
				CookingAndGathering_Butchery part = Actor.GetPart<CookingAndGathering_Butchery>();
				if (part != null && !part.IsMyActivatedAbilityToggledOn(part.ActivatedAbilityID, null))
				{
					return false;
				}
				if (this.ParentObject.HasTagOrProperty("QuestItem"))
				{
					return false;
				}
			}
			if (!Actor.CheckFrozen(false, false, false, null))
			{
				return false;
			}
			if (!this.ParentObject.ConfirmUseImportant(Actor, "butcher", null, -1))
			{
				return false;
			}
			Cell currentCell = this.ParentObject.GetCurrentCell();
			Cell cell = FromCell ?? Actor.GetCurrentCell();
			string text = null;
			if (currentCell != null && currentCell != cell)
			{
				text = Directions.GetDirectionDescription(Actor, cell.GetDirectionFromCell(currentCell, false));
			}
			bool storedByPlayer = this.ParentObject.GetIntProperty("StoredByPlayer", 0) > 0;
			Action<GameObject> action = delegate(GameObject o)
			{
				if (storedByPlayer)
				{
					o.SetIntProperty("FromStoredByPlayer", 1, false);
				}
				Event @event = Event.New("ObjectExtracted", 0, 0, 0);
				@event.SetParameter("Object", o);
				@event.SetParameter("Source", this.ParentObject);
				@event.SetParameter("Actor", Actor);
				@event.SetParameter("Action", "Butcher");
				o.FireEvent(@event);
			};
			bool flag = false;
			GameObject gameObject = this.ParentObject.RemoveOne();
			int num = (this.ParentObject.IsTemporary ? 0 : this.OnSuccessAmount.RollCached());
			int num2 = 1000;
			if (num > 0)
			{
				num2 /= Math.Max(Actor.GetIntProperty("ButcheryToolEquipped", 0) + 1, 1);
				GameObject gameObject2;
				if (this.OnSuccess[0] == '@')
				{
					gameObject2 = GameObject.Create(PopulationManager.RollOneFrom(this.OnSuccess.Substring(1), null, null).Blueprint, 0, 0, null, null, action, null, null);
				}
				else
				{
					gameObject2 = GameObject.Create(this.OnSuccess, 0, 0, null, null, action, null, null);
				}
				if (!IntoInventory && Actor.IsPlayer() && gameObject2.ShouldAutoget())
				{
					IntoInventory = true;
				}
				if (this.OnSuccess[0] != '@' && num > 1)
				{
					IComponent<GameObject>.XDidYToZ(Actor, Verb ?? "butcher", gameObject, string.Concat(new string[]
					{
						text.IsNullOrEmpty() ? "" : (text + " "),
						"into ",
						Grammar.Cardinal(num),
						" ",
						Grammar.Pluralize(gameObject2.ShortDisplayName)
					}), null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
					if (IntoInventory)
					{
						Actor.TakeObject(gameObject2, false, true, new int?(0), null, Tracking);
						GameObject actor = Actor;
						string onSuccess = this.OnSuccess;
						int num3 = num - 1;
						bool flag2 = false;
						bool flag3 = true;
						Action<GameObject> action2 = action;
						actor.TakeObject(onSuccess, num3, flag2, flag3, new int?(0), null, 0, 0, null, Tracking, null, action2);
					}
					else
					{
						currentCell.AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						currentCell.AddObject(this.OnSuccess, num - 1, null, null, action);
					}
				}
				else
				{
					IComponent<GameObject>.WDidXToYWithZ(Actor, Verb ?? "butcher", gameObject, ((text == null) ? "" : (text + " ")) + "into", gameObject2, null, null, null, null, null, null, false, false, false, true, false, false, false, false, null, null, null, null, false, false, false, false, false, null);
					if (IntoInventory)
					{
						Actor.TakeObject(gameObject2, false, true, new int?(0), null, Tracking);
					}
					else
					{
						currentCell.AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
				}
				flag = true;
			}
			else
			{
				IComponent<GameObject>.XDidYToZ(Actor, "fail", "to " + (Verb ?? "butcher") + " anything useful from", gameObject, text, null, null, null, null, Actor, false, false, false, false, false, null, null, null, false, false, false, false, false, null);
			}
			gameObject.PlayWorldSound("Sounds/Interact/sfx_interact_corpse_butcher", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			gameObject.Bloodsplatter(false);
			gameObject.Destroy(null, false, false, null);
			Actor.UseEnergy(num2, "Skill", null, null, false);
			return flag;
		}

		// Token: 0x0600A36B RID: 41835 RVA: 0x003BBA71 File Offset: 0x003B9C71
		public bool IsButcherable()
		{
			return !string.IsNullOrEmpty(this.OnSuccess) && this.ParentObject.Render.Visible && !this.ParentObject.HasPart<HologramMaterial>();
		}

		// Token: 0x040043B4 RID: 17332
		public string OnSuccessAmount = "1";

		// Token: 0x040043B5 RID: 17333
		public string OnSuccess = "";
	}
}
