using System;
using System.Collections.Generic;
using XRL.UI;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013DA RID: 5082
	[Serializable]
	public class Skittish : BaseMutation
	{
		// Token: 0x0600C45E RID: 50270 RVA: 0x00478201 File Offset: 0x00476401
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C45F RID: 50271 RVA: 0x00478204 File Offset: 0x00476404
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginTakeAction");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C460 RID: 50272 RVA: 0x00478219 File Offset: 0x00476419
		public override string GetDescription()
		{
			return "You startle easily and engage your defense mechanisms.";
		}

		// Token: 0x0600C461 RID: 50273 RVA: 0x00478220 File Offset: 0x00476420
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C462 RID: 50274 RVA: 0x00478228 File Offset: 0x00476428
		public bool LoseControl()
		{
			if (this.ParentObject.Target != null)
			{
				this.ParentObject.DistanceTo(this.ParentObject.Target);
				List<AICommandList> @for = AIGetOffensiveAbilityListEvent.GetFor(this.ParentObject, null, -1);
				@for.AddRange(AIGetPassiveAbilityListEvent.GetFor(this.ParentObject, null, -1));
				@for.AddRange(AIGetMovementAbilityListEvent.GetFor(this.ParentObject, null, null, -1, 0));
				if (@for.Count > 0)
				{
					this.ParentObject.SetStringProperty("Skittishing", "1", false);
					try
					{
						if (AICommandList.HandleCommandList(@for, this.ParentObject, this.ParentObject.Target, null, 0) && this.ParentObject.IsPlayer())
						{
							Popup.Show("You are startled!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
					}
					finally
					{
						this.ParentObject.RemoveStringProperty("Skittishing");
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600C463 RID: 50275 RVA: 0x00478314 File Offset: 0x00476514
		public override bool FireEvent(Event E)
		{
			return !(E.ID == "BeginTakeAction") || !3.in1000() || !this.LoseControl();
		}

		// Token: 0x0600C464 RID: 50276 RVA: 0x0047833B File Offset: 0x0047653B
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C465 RID: 50277 RVA: 0x00478344 File Offset: 0x00476544
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C466 RID: 50278 RVA: 0x0047834E File Offset: 0x0047654E
		public override bool Unmutate(GameObject GO)
		{
			return base.Unmutate(GO);
		}
	}
}
