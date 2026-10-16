using System;
using Qud.API;
using XRL.Messages;
using XRL.UI;
using XRL.World.AI;

namespace XRL.World.Capabilities
{
	// Token: 0x020009DF RID: 2527
	public static class Cloning
	{
		// Token: 0x06006BF2 RID: 27634 RVA: 0x0022D39D File Offset: 0x0022B59D
		public static bool IsCloning(string ReplicationContext)
		{
			return ReplicationContext == "CloningDraught" || ReplicationContext == "Cloneling" || ReplicationContext == "Budding";
		}

		// Token: 0x06006BF3 RID: 27635 RVA: 0x0022D3C6 File Offset: 0x0022B5C6
		public static bool CanBeCloned(GameObject Object, GameObject Actor = null, string Context = null, bool Temporary = false)
		{
			return !Object.HasPropertyOrTag("Noclone") && Object.IsAlive && Effect.CanEffectTypeBeAppliedTo(16, Object) && Object.CanBeReplicated(Actor, Context, Temporary);
		}

		// Token: 0x06006BF4 RID: 27636 RVA: 0x0022D3FC File Offset: 0x0022B5FC
		private static void PostprocessClone(GameObject Original, GameObject Clone, GameObject Actor, bool DuplicateGear = false, bool BecomesCompanion = true, bool Budded = false, string Context = null)
		{
			if (!DuplicateGear)
			{
				Clone.StripContents(true, true);
			}
			Clone.RestorePristineHealth(false, false);
			Clone.HasProperName = false;
			Clone.RemoveIntProperty("Renamed");
			Clone.RemoveStringProperty("OriginalPlayerBody");
			Clone.ModIntProperty("IsClone", 1, false);
			Clone.SetStringProperty("CloneOf", Original.ID, false);
			Clone.SetStringProperty("CloneOfGenes", Original.GeneID, false);
			if (Budded)
			{
				Clone.ModIntProperty("IsBuddedClone", 1, false);
			}
			if (Clone.Render != null && !Original.HasPropertyOrTag("CloneNoNameChange") && !Original.BaseDisplayName.Contains("clone of"))
			{
				if (Original.HasProperName)
				{
					Clone.Render.DisplayName = "clone of " + Original.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, true, false, false, true, false, null, false);
				}
				else
				{
					Clone.Render.DisplayName = Clone.GetBlueprint(true).DisplayName();
				}
			}
			if (BecomesCompanion)
			{
				Clone.SetAlliedLeader<AllyClone>(Original, 0, false);
			}
			WasReplicatedEvent.Send(Original, Actor, Clone, Context, false);
			ReplicaCreatedEvent.Send(Clone, Actor, Original, Context, false);
		}

		// Token: 0x06006BF5 RID: 27637 RVA: 0x0022D518 File Offset: 0x0022B718
		public static GameObject GenerateClone(GameObject Original, GameObject Actor = null, Cell Cell = null, bool DuplicateGear = false, bool BecomesCompanion = true, bool Budded = false, string Context = null, Func<GameObject, GameObject> MapInv = null)
		{
			GameObject gameObject = null;
			try
			{
				Original.FireEvent("BeforeBeingCloned");
				string geneID = Original.GeneID;
				gameObject = Original.DeepCopy(false, false, MapInv);
			}
			finally
			{
				Original.FireEvent("AfterBeingCloned");
			}
			if (gameObject == null)
			{
				return null;
			}
			Cloning.PostprocessClone(Original, gameObject, Actor, DuplicateGear, BecomesCompanion, Budded, Context);
			if (Cell != null)
			{
				Cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				gameObject.MakeActive(false);
				if (!Achievement.CLONES_30.Achieved && Original.IsPlayer())
				{
					Cloning.QueueAchievementCheck();
				}
			}
			return gameObject;
		}

		// Token: 0x06006BF6 RID: 27638 RVA: 0x0022D5B4 File Offset: 0x0022B7B4
		public static GameObject GenerateClone(GameObject Original, GameObject Actor = null, bool DuplicateGear = false, bool BecomesCompanion = true, bool Budded = false, string Context = null, int MaxRadius = 1)
		{
			if (Original.CurrentCell != null && !Original.OnWorldMap())
			{
				Cell firstEmptyAdjacentCell = Original.CurrentCell.GetFirstEmptyAdjacentCell(1, MaxRadius);
				if (firstEmptyAdjacentCell != null)
				{
					return Cloning.GenerateClone(Original, Actor, firstEmptyAdjacentCell, DuplicateGear, BecomesCompanion, Budded, Context, null);
				}
			}
			return null;
		}

		// Token: 0x06006BF7 RID: 27639 RVA: 0x0022D5F4 File Offset: 0x0022B7F4
		public static GameObject GenerateBuddedClone(GameObject Original, Cell Cell, GameObject Actor = null, bool DuplicateGear = false, bool BecomesCompanion = true, string Context = "Budding")
		{
			return Cloning.StartBuddedClone(Original, Cloning.GenerateClone(Original, Actor, Cell, DuplicateGear, BecomesCompanion, true, Context, null));
		}

		// Token: 0x06006BF8 RID: 27640 RVA: 0x0022D616 File Offset: 0x0022B816
		public static GameObject GenerateBuddedClone(GameObject Original, GameObject Actor = null, bool DuplicateGear = false, bool BecomesCompanion = true, int MaxRadius = 1, string Context = "Budding")
		{
			return Cloning.StartBuddedClone(Original, Cloning.GenerateClone(Original, Actor, DuplicateGear, BecomesCompanion, true, Context, MaxRadius));
		}

		// Token: 0x06006BF9 RID: 27641 RVA: 0x0022D62C File Offset: 0x0022B82C
		private static GameObject StartBuddedClone(GameObject Original, GameObject Clone)
		{
			Original.Bloodsplatter();
			if (Original.IsPlayer())
			{
				Popup.Show(Clone.Does("detach", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, true, null, false, false, true, null, true, null, false) + " from you!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				JournalAPI.AddAccomplishment(string.Concat(new string[]
				{
					"On the ",
					Calendar.GetDay(),
					" of ",
					Calendar.GetMonth(),
					", you multiplied."
				}), string.Concat(new string[]
				{
					"In the month of ",
					Calendar.GetMonth(),
					" of ",
					Calendar.GetYear().ToString(),
					" AR, =name= immaculately birthed ",
					The.Player.GetPronounProvider(false).Reflexive,
					"."
				}), string.Concat(new string[]
				{
					"In =year=, while traveling near ",
					JournalAPI.GetLandmarkNearestPlayer().Text,
					", =name= created a simulacrum of ",
					The.Player.GetPronounProvider(false).Reflexive,
					" for the purpose of faking chariot accidents."
				}), null, "general", MuralCategory.WeirdThingHappens, MuralWeight.High, null, -1L, true);
			}
			else if (Original.IsVisible() || Clone.IsVisible())
			{
				MessageQueue.AddPlayerMessage(Clone.Does("detach", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, true, null, false, false, true, null, true, null, false) + " from " + Original.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + "!", null, true);
			}
			return Clone;
		}

		// Token: 0x06006BFA RID: 27642 RVA: 0x0022D7E8 File Offset: 0x0022B9E8
		public static void QueueAchievementCheck()
		{
			GameManager.Instance.gameQueue.queueSingletonTask("CloningAchievements", new Action(Cloning.CheckAchievements), false);
		}

		// Token: 0x06006BFB RID: 27643 RVA: 0x0022D80C File Offset: 0x0022BA0C
		public static void CheckAchievements()
		{
			int num = 0;
			string id = The.Player.ID;
			Zone currentZone = The.Player.CurrentZone;
			for (int i = 0; i < currentZone.Height; i++)
			{
				for (int j = 0; j < currentZone.Width; j++)
				{
					int k = 0;
					int count = currentZone.Map[j][i].Objects.Count;
					while (k < count)
					{
						string text;
						if (currentZone.Map[j][i].Objects[k].Property.TryGetValue("CloneOf", out text) && text == id)
						{
							num++;
						}
						k++;
					}
				}
			}
			if (num >= 10)
			{
				Achievement.CLONES_10.Unlock();
			}
			if (num >= 30)
			{
				Achievement.CLONES_30.Unlock();
			}
		}
	}
}
