using System;

namespace XRL.World.Capabilities
{
	// Token: 0x020009F7 RID: 2551
	public static class SmartUse
	{
		// Token: 0x06006CD0 RID: 27856 RVA: 0x00236824 File Offset: 0x00234A24
		public static GameObject FindSmartUseObject(Cell TargetCell, GameObject who)
		{
			return TargetCell.GetHighestRenderLayerInteractableObjectFor(who, (GameObject o) => SmartUse.CanSmartUse(o, who, 0));
		}

		// Token: 0x06006CD1 RID: 27857 RVA: 0x00236858 File Offset: 0x00234A58
		public static GameObject FindPlayerSmartUseObject(Cell TargetCell, int MinPriority = 0)
		{
			return TargetCell.GetHighestRenderLayerInteractableObjectFor(The.Player, (GameObject o) => SmartUse.CanPlayerSmartUse(o, MinPriority));
		}

		// Token: 0x06006CD2 RID: 27858 RVA: 0x0023688C File Offset: 0x00234A8C
		public static bool CanSmartUse(GameObject GO, GameObject Actor, int MinPriority = 0)
		{
			if (GO == Actor || GO.HasTag("NoSmartUse"))
			{
				return false;
			}
			if (GO.Render != null && !GO.Render.Visible)
			{
				return false;
			}
			if (GO.HasTag("ForceSmartUse"))
			{
				return true;
			}
			bool flag = false;
			if (GO.HasRegisteredEvent(SmartUse.eCanSmartUse.ID))
			{
				SmartUse.eCanSmartUse.SetParameter("User", Actor);
				SmartUse.eCanSmartUse.SetParameter("MinPriority", MinPriority);
				if (!GO.FireEvent(SmartUse.eCanSmartUse))
				{
					flag = true;
				}
			}
			bool flag2 = false;
			if (GO.WantEvent(CanSmartUseEvent.ID, MinEvent.CascadeLevel) && !GO.HandleEvent(CanSmartUseEvent.FromPool(Actor, GO, MinPriority)))
			{
				flag2 = true;
			}
			if (!flag && !flag2)
			{
				return false;
			}
			if (GO.HasRegisteredEvent(SmartUse.ePreventSmartUse.ID))
			{
				SmartUse.ePreventSmartUse.SetParameter("User", Actor);
				if (!GO.FireEvent(SmartUse.ePreventSmartUse))
				{
					return false;
				}
			}
			if (GO.WantEvent(PreventSmartUseEvent.ID, MinEvent.CascadeLevel) && !GO.HandleEvent(PreventSmartUseEvent.FromPool(Actor, GO)))
			{
				return false;
			}
			Cell currentCell = GO.CurrentCell;
			return currentCell == null || Actor.CurrentCell == currentCell || GO.CanInteractInCellWithSolid(Actor) || !currentCell.IsSolidFor(Actor, false);
		}

		// Token: 0x06006CD3 RID: 27859 RVA: 0x002369C3 File Offset: 0x00234BC3
		public static bool CanPlayerSmartUse(GameObject GO, int MinPriority = 0)
		{
			return SmartUse.CanSmartUse(GO, The.Player, MinPriority);
		}

		// Token: 0x06006CD4 RID: 27860 RVA: 0x002369D1 File Offset: 0x00234BD1
		public static bool CanTake(GameObject GO, GameObject Actor)
		{
			return GO != Actor && (GO.Render == null || GO.Render.Visible) && GO.IsTakeable();
		}

		// Token: 0x06006CD5 RID: 27861 RVA: 0x002369F6 File Offset: 0x00234BF6
		public static bool CanPlayerTake(GameObject GO)
		{
			return SmartUse.CanTake(GO, The.Player);
		}

		// Token: 0x06006CD6 RID: 27862 RVA: 0x00236A04 File Offset: 0x00234C04
		public static bool PerformSmartUse(GameObject GO, GameObject Actor, int MinPriority = 0)
		{
			if (GO == null)
			{
				return false;
			}
			if (GO.HasRegisteredEvent(SmartUse.eCommandSmartUseEarly.ID))
			{
				SmartUse.eCommandSmartUseEarly.SetParameter("User", Actor);
				if (!GO.FireEvent(SmartUse.eCommandSmartUseEarly))
				{
					return false;
				}
			}
			if (GO.WantEvent(CommandSmartUseEarlyEvent.ID, MinEvent.CascadeLevel) && !GO.HandleEvent(CommandSmartUseEarlyEvent.FromPool(Actor, GO)))
			{
				return false;
			}
			if (GO.HasRegisteredEvent(SmartUse.eCommandSmartUse.ID))
			{
				SmartUse.eCommandSmartUse.SetParameter("User", Actor);
				if (!GO.FireEvent(SmartUse.eCommandSmartUse))
				{
					return false;
				}
			}
			return !GO.WantEvent(CommandSmartUseEvent.ID, MinEvent.CascadeLevel) || GO.HandleEvent(CommandSmartUseEvent.FromPool(Actor, GO, MinPriority));
		}

		// Token: 0x06006CD7 RID: 27863 RVA: 0x00236AC2 File Offset: 0x00234CC2
		public static bool PlayerPerformSmartUse(GameObject GO, int MinPriority = 0)
		{
			return SmartUse.PerformSmartUse(GO, The.Player, MinPriority);
		}

		// Token: 0x06006CD8 RID: 27864 RVA: 0x00236AD0 File Offset: 0x00234CD0
		public static bool PlayerPerformSmartUse(Cell TargetCell)
		{
			return SmartUse.PlayerPerformSmartUse(SmartUse.FindPlayerSmartUseObject(TargetCell, 0), 0);
		}

		// Token: 0x040031D6 RID: 12758
		[NonSerialized]
		private static Event eCanSmartUse = new Event("CanSmartUse", "User", null);

		// Token: 0x040031D7 RID: 12759
		[NonSerialized]
		private static Event ePreventSmartUse = new Event("PreventSmartUse", "User", null);

		// Token: 0x040031D8 RID: 12760
		[NonSerialized]
		private static Event eCommandSmartUseEarly = new Event("CommandSmartUseEarly", "User", null);

		// Token: 0x040031D9 RID: 12761
		[NonSerialized]
		private static Event eCommandSmartUse = new Event("CommandSmartUse", "User", null);
	}
}
