using System;
using System.Collections.Generic;
using UnityEngine;
using XRL.UI;
using XRL.World.AI;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts
{
	// Token: 0x02000FB6 RID: 4022
	[Serializable]
	public class Shrine : IPart
	{
		// Token: 0x06009FDA RID: 40922 RVA: 0x003A1E67 File Offset: 0x003A0067
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009FDB RID: 40923 RVA: 0x003A1E6A File Offset: 0x003A006A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsEvent.ID || ID == PooledEvent<GetPointsOfInterestEvent>.ID || ID == PooledEvent<IdleQueryEvent>.ID || ID == InventoryActionEvent.ID || ID == TookDamageEvent.ID;
		}

		// Token: 0x06009FDC RID: 40924 RVA: 0x003A1EA0 File Offset: 0x003A00A0
		public override bool HandleEvent(GetPointsOfInterestEvent E)
		{
			if (E.StandardChecks(this, E.Actor, null))
			{
				string baseDisplayName = this.ParentObject.BaseDisplayName;
				string text = "Shrine " + (this.ParentObject.GetPropertyOrTag("WorshippedAs", null) ?? baseDisplayName);
				string text2 = null;
				bool flag = true;
				PointOfInterest pointOfInterest = E.Find(text);
				if (pointOfInterest != null)
				{
					if (this.ParentObject.DistanceTo(E.Actor) < pointOfInterest.GetDistanceTo(E.Actor))
					{
						E.Remove(pointOfInterest);
						text2 = "nearest";
					}
					else
					{
						flag = false;
						pointOfInterest.Explanation = "nearest";
					}
				}
				if (flag)
				{
					E.Add(this.ParentObject, baseDisplayName, text2, text, null, null, null, 1, null, 0);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009FDD RID: 40925 RVA: 0x003A1F5C File Offset: 0x003A015C
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Pray", "pray", "Pray", null, 'y', false, -10, 0, false, false, false, false, true, null, false);
			if (!this.ParentObject.HasPart<ModDesecrated>())
			{
				E.AddAction("Desecrate", "desecrate", "Desecrate", null, 'D', false, -10, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009FDE RID: 40926 RVA: 0x003A1FC8 File Offset: 0x003A01C8
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Pray")
			{
				if (this.PrayAtShrine(E.Actor, true, false, false))
				{
					E.RequestInterfaceExit();
				}
			}
			else if (E.Command == "Desecrate" && this.DesecrateShrine(E.Actor, true))
			{
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009FDF RID: 40927 RVA: 0x003A2030 File Offset: 0x003A0230
		public override bool HandleEvent(IdleQueryEvent E)
		{
			if (E.Actor.HasPart<Brain>() && 1.in10000())
			{
				GameObject actor = E.Actor;
				if (actor.GetPrimaryFaction(false) == this.ParentObject.Owner)
				{
					actor.Brain.PushGoal(new DelegateGoal(delegate(GoalHandler h)
					{
						if (actor.isAdjacentTo(this.ParentObject))
						{
							this.PrayAtShrine(actor, false, false, false);
						}
						h.FailToParent();
					}, null, null));
					actor.Brain.PushGoal(new MoveTo(this.ParentObject, false, false, 0, false, false, false, -1, -1, 95));
				}
				else if (this.ParentObject.Owner != null && Factions.GetFeelingFactionToObject(this.ParentObject.Owner, actor, null) < 0)
				{
					actor.Brain.PushGoal(new DelegateGoal(delegate(GoalHandler h)
					{
						if (actor.isAdjacentTo(this.ParentObject))
						{
							this.DesecrateShrine(actor, false);
						}
						h.FailToParent();
					}, null, null));
					actor.Brain.PushGoal(new MoveTo(this.ParentObject, false, false, 0, false, false, false, -1, -1, 95));
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009FE0 RID: 40928 RVA: 0x003A2158 File Offset: 0x003A0358
		public override bool HandleEvent(TookDamageEvent E)
		{
			if (GameObject.Validate(E.Actor) && E.Actor != this.ParentObject)
			{
				bool isCreature = this.ParentObject.IsCreature;
				if (!isCreature)
				{
					this.ParentObject.Physics.BroadcastForHelp(E.Actor);
				}
				if (!this.ParentObject.HasPart<ModDesecrated>() && this.ParentObject.isDamaged(0.75, false))
				{
					this.PerformDesecration(E.Actor, false, E.Actor.IsPlayer(), isCreature, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009FE1 RID: 40929 RVA: 0x003A21EA File Offset: 0x003A03EA
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009FE2 RID: 40930 RVA: 0x003A21F0 File Offset: 0x003A03F0
		public bool PrayAtShrine(GameObject Actor, bool FromDialog = false, bool UsePopup = false, bool Silent = false)
		{
			Actor.PlayWorldSound("Sounds/Interact/sfx_interact_shrine_pray", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			if (!Silent)
			{
				IComponent<GameObject>.XDidYToZ(Actor, "voice", "a short prayer beneath", this.ParentObject, null, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, FromDialog, UsePopup, null);
			}
			Actor.UseEnergy(1000, "Item", null, null, false);
			Event @event = Event.New("Prayed", "Actor", Actor, "Object", this.ParentObject);
			this.ParentObject.FireEvent(@event);
			Actor.FireEvent(@event);
			WorshipPerformedEvent.Send(Actor, this.ParentObject, null, null);
			return true;
		}

		// Token: 0x06009FE3 RID: 40931 RVA: 0x003A22B4 File Offset: 0x003A04B4
		public void PerformDesecration(GameObject Actor, bool FromDialog = false, bool UsePopup = false, bool Silent = false, bool DoHelpBroadcast = true)
		{
			base.PlayWorldSound("Sounds/Interact/sfx_interact_shrine_desecrate", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			if (!Silent)
			{
				IComponent<GameObject>.XDidYToZ(Actor, "desecrate", this.ParentObject, null, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, FromDialog, UsePopup, null);
			}
			this.ParentObject.AddPart<ModDesecrated>(new ModDesecrated(), true, false);
			if (DoHelpBroadcast)
			{
				this.ParentObject.Physics.BroadcastForHelp(Actor);
			}
			Event @event = Event.New("Desecrated", "Actor", Actor, "Object", this.ParentObject);
			this.ParentObject.FireEvent(@event);
			Actor.FireEvent(@event);
			BlasphemyPerformedEvent.Send(Actor, this.ParentObject, null, null);
		}

		// Token: 0x06009FE4 RID: 40932 RVA: 0x003A237C File Offset: 0x003A057C
		public bool DesecrateShrine(GameObject Actor, bool FromDialog = false)
		{
			if (!GameObject.Validate(ref Actor))
			{
				return false;
			}
			if (Actor.IsPlayer())
			{
				int freeDrams = Actor.GetFreeDrams("blood", null, null, null, false);
				int freeDrams2 = Actor.GetFreeDrams("putrid", null, null, null, false);
				if (freeDrams <= 0 && freeDrams2 <= 0)
				{
					return Actor.Fail("You need a dram of either {{r|blood}} or {{putrid|putrescence}} to desecrate a shrine.");
				}
				List<string> list = new List<string>(2);
				List<string> list2 = new List<string>(2);
				if (freeDrams > 0)
				{
					list.Add("blood");
					list2.Add("blood");
				}
				if (freeDrams2 > 0)
				{
					list.Add("putrescence");
					list2.Add("putrid");
				}
				if (list.Count == 0)
				{
					Debug.LogError("internal inconsistency");
					return Actor.Fail("You need a dram of either {{r|blood}} or {{putrid|putrescence}} to desecrate a shrine.");
				}
				int num = Popup.PickOption("Choose a desecration liquid", null, "", "Sounds/UI/ui_notification", list.ToArray(), null, null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
				if (num < 0)
				{
					return false;
				}
				Actor.UseDrams(1, list2[num], null, null, null, null, false);
			}
			this.PerformDesecration(Actor, FromDialog, false, false, true);
			Actor.UseEnergy(1000, "Item", null, null, false);
			return true;
		}
	}
}
