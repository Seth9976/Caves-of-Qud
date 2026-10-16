using System;
using System.Collections.Generic;
using System.Linq;
using Qud.API;
using Qud.UI;
using UnityEngine;
using XRL.Language;
using XRL.UI;
using XRL.World.AI;
using XRL.World.Quests.GolemQuest;

namespace XRL.World.Parts
{
	// Token: 0x02001310 RID: 4880
	[Serializable]
	public class GolemQuestMound : IPart
	{
		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x0600BEE6 RID: 48870 RVA: 0x0045E4FD File Offset: 0x0045C6FD
		public int CompleteDays
		{
			get
			{
				return Mathf.RoundToInt((float)(this.CompleteTurn - The.Game.TimeTicks) * 1f / 1200f);
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x0600BEE7 RID: 48871 RVA: 0x0045E524 File Offset: 0x0045C724
		public GolemQuestSystem System
		{
			get
			{
				GolemQuestSystem golemQuestSystem;
				if ((golemQuestSystem = this._System) == null)
				{
					golemQuestSystem = (this._System = GolemQuestSystem.Get());
				}
				return golemQuestSystem;
			}
		}

		// Token: 0x0600BEE9 RID: 48873 RVA: 0x0045E559 File Offset: 0x0045C759
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BEEA RID: 48874 RVA: 0x0045E55C File Offset: 0x0045C75C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEvent.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == ZoneActivatedEvent.ID;
		}

		// Token: 0x0600BEEB RID: 48875 RVA: 0x0045E592 File Offset: 0x0045C792
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return false;
		}

		// Token: 0x0600BEEC RID: 48876 RVA: 0x0045E595 File Offset: 0x0045C795
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			return !this.TryDisplayOptionsFor(E.Actor) && base.HandleEvent(E);
		}

		// Token: 0x0600BEED RID: 48877 RVA: 0x0045E5B0 File Offset: 0x0045C7B0
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Interface", "interface", "ActivateGolemMound", null, 'i', false, 0, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BEEE RID: 48878 RVA: 0x0045E5E7 File Offset: 0x0045C7E7
		public override bool HandleEvent(InventoryActionEvent E)
		{
			return (!(E.Command == "ActivateGolemMound") || !this.TryDisplayOptionsFor(E.Actor)) && base.HandleEvent(E);
		}

		// Token: 0x0600BEEF RID: 48879 RVA: 0x0045E612 File Offset: 0x0045C812
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			this.CheckCompletion();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BEF0 RID: 48880 RVA: 0x0045E621 File Offset: 0x0045C821
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600BEF1 RID: 48881 RVA: 0x0045E624 File Offset: 0x0045C824
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CheckCompletion();
		}

		// Token: 0x0600BEF2 RID: 48882 RVA: 0x0045E62C File Offset: 0x0045C82C
		public void CheckCompletion()
		{
			if (this.CompleteTurn > 0L && The.Game.TimeTicks >= this.CompleteTurn && this.ParentObject.CurrentZone == The.ActiveZone)
			{
				this.CompleteTurn = -1L;
				Popup.Show("{{C|Your golem is ready for use.}}", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				this.Place(GameObject.FindByID(this.BuilderID) ?? The.Player);
			}
		}

		// Token: 0x0600BEF3 RID: 48883 RVA: 0x0045E6A0 File Offset: 0x0045C8A0
		public bool TryDisplayOptionsFor(GameObject Actor)
		{
			if (Actor != The.Player)
			{
				return false;
			}
			if (!Actor.CheckFrozen(false, false, false, null))
			{
				return false;
			}
			if (this.CompleteTurn > 0L)
			{
				int completeDays = this.CompleteDays;
				string text = "soon";
				if (completeDays >= 1)
				{
					text = "in " + Grammar.Cardinal(completeDays) + ((completeDays == 1) ? " day" : " days");
				}
				return Actor.ShowFailure("The creature is being shaped. It will be finished " + text + ".");
			}
			if (Actor.AreHostilesNearby())
			{
				return Actor.ShowFailure("You can't do that with hostile creatures nearby.");
			}
			if (this.System == null)
			{
				this._System = GolemQuestSystem.Require();
			}
			base.PlayWorldSound("Sounds/Interact/sfx_interact_scrapClayMound_activate", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			this.DisplayOptions(Actor);
			return true;
		}

		// Token: 0x0600BEF4 RID: 48884 RVA: 0x0045E774 File Offset: 0x0045C974
		public void DisplayOptions(GameObject Actor)
		{
			this.System.Mound = this;
			GolemQuestSelection[] array = this.System.Selections.Values.ToArray<GolemQuestSelection>();
			string[] array2 = new string[array.Length];
			char[] array3 = new char[array.Length];
			bool flag = The.Game.HasFinishedQuest("The Golem") || this.ParentObject.HasIntProperty("Wish");
			object obj;
			if (!flag)
			{
				obj = null;
			}
			else
			{
				(obj = new QudMenuItem[1])[0] = new QudMenuItem
				{
					command = "option:-2",
					hotkey = "CmdDelete"
				};
			}
			QudMenuItem[] array4 = obj;
			int num = 0;
			for (;;)
			{
				array2.Fill<string>(array.Select<GolemQuestSelection, string>((GolemQuestSelection x) => x.GetOptionText()));
				array3.Fill<char>(array.Select<GolemQuestSelection, char>((GolemQuestSelection x) => x.Key));
				bool flag2 = false;
				if (flag)
				{
					flag2 = array.All<GolemQuestSelection>((GolemQuestSelection x) => x.IsValid());
					array4[0].text = (flag2 ? "{{W|[Backspace]}} {{y|Build}}" : "{{K|Build}}");
				}
				num = Popup.PickOption("", this.ParentObject.GetPart<Description>().Short + "\n\n", "", "Sounds/UI/ui_notification", array2, array3, null, array4, this.ParentObject, null, null, 0, 60, num, -1, true, true, false, true, false, null, null);
				if (num < 0 || num >= array.Length)
				{
					this.System.UpdateQuest();
					if (num != -2)
					{
						goto IL_01BF;
					}
					if (flag2)
					{
						break;
					}
				}
				else
				{
					array[num].Pick();
					Event.ResetPool(true);
				}
			}
			this.Build(Actor, 0);
			IL_01BF:
			if (this.System != null)
			{
				this.System.Mound = null;
			}
		}

		// Token: 0x0600BEF5 RID: 48885 RVA: 0x0045E954 File Offset: 0x0045CB54
		public void Build(GameObject Actor, int DurationTicks = 0)
		{
			GameObject gameObject = Vehicle.CreateOwnedBy(this.System.Body.BodyBlueprint.Name, Actor, new bool?(true), new bool?(true), true);
			gameObject.SetGender(this.System.Body.Material.GetGender(true));
			foreach (KeyValuePair<string, GolemQuestSelection> keyValuePair in this.System.Selections)
			{
				try
				{
					keyValuePair.Value.Apply(gameObject);
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("GolemMound", ex, "game_exception");
				}
			}
			GolemQuestSelection.ProcessDescription(gameObject, null);
			The.Game.FinishQuest("The Golem");
			IComponent<GameObject>.ThePlayer.AwardXP(40000, -1, 0, int.MaxValue, null, gameObject, null, null, null, null);
			The.ZoneManager.CachedObjects[gameObject.ID] = gameObject;
			this.BuilderID = Actor.ID;
			this.BodyID = gameObject.ID;
			if (DurationTicks <= 0)
			{
				this.Place(Actor);
				return;
			}
			this.CompleteTurn = The.Game.TimeTicks + (long)DurationTicks;
		}

		// Token: 0x0600BEF6 RID: 48886 RVA: 0x0045EA98 File Offset: 0x0045CC98
		public void Place(GameObject Actor)
		{
			Dictionary<string, GameObject> cachedObjects = The.ZoneManager.CachedObjects;
			GameObject gameObject;
			if (!this.BodyID.IsNullOrEmpty() && cachedObjects.TryGetValue(this.BodyID, out gameObject))
			{
				cachedObjects.Remove(this.BodyID);
				gameObject.SetAlliedLeader<AllyConstructed>(Actor, 0, false);
				this.ParentObject.CurrentZone.GetObjectsWithTagOrProperty("GolemChassis", true).ForEach(delegate(GameObject x)
				{
					x.Obliterate(null, true, null);
				});
				this.ParentObject.ReplaceWith(gameObject);
				this.BodyID = null;
				gameObject.MakeActive(false);
				gameObject.Brain.PerformReequip(false, true, false, null);
				The.Game.FlagSystemForRemoval(this.System);
				The.Game.RemoveBooleanGameState("GolemBuilding");
				this._System = null;
				JournalAPI.AddAccomplishment("With the help of Pax Klanq and the Barathrumites, you constructed " + gameObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, true, false, true, false, null, false, null, false) + " to climb the Spindle.", null, null, null, "general", MuralCategory.Generic, MuralWeight.Medium, null, -1L, true);
			}
		}

		// Token: 0x04004CF9 RID: 19705
		public string BuilderID;

		// Token: 0x04004CFA RID: 19706
		public string BodyID;

		// Token: 0x04004CFB RID: 19707
		public long CompleteTurn = -1L;

		// Token: 0x04004CFC RID: 19708
		public const int GolemQuestXPAward = 40000;

		// Token: 0x04004CFD RID: 19709
		[NonSerialized]
		private GolemQuestSystem _System;
	}
}
