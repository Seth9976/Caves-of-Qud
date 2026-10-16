using System;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	/// This part is not used in the base game.
	// Token: 0x020013DF RID: 5087
	[Serializable]
	public class WeakHeart : BaseMutation
	{
		// Token: 0x0600C487 RID: 50311 RVA: 0x004787A7 File Offset: 0x004769A7
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C488 RID: 50312 RVA: 0x004787AA File Offset: 0x004769AA
		public override string GetDescription()
		{
			return "Your heart is weak.\n\n-5 to save vs. poison, disease, and cardiac arrest.\nSmall chance per turn of entering cardiac arrest.";
		}

		// Token: 0x0600C489 RID: 50313 RVA: 0x004787B1 File Offset: 0x004769B1
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C48A RID: 50314 RVA: 0x004787B8 File Offset: 0x004769B8
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600C48B RID: 50315 RVA: 0x004787BB File Offset: 0x004769BB
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (!this.ParentObject.OnWorldMap() && 1.in100() && 1.in100())
			{
				this.ParentObject.ApplyEffect(new CardiacArrest(), null);
			}
		}

		// Token: 0x0600C48C RID: 50316 RVA: 0x004787EC File Offset: 0x004769EC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ModifyDefendingSaveEvent.ID;
		}

		// Token: 0x0600C48D RID: 50317 RVA: 0x00478802 File Offset: 0x00476A02
		public override bool HandleEvent(ModifyDefendingSaveEvent E)
		{
			if (SavingThrows.Applicable("Poison,Disease,CardiacArrest", E))
			{
				E.Roll -= 5;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C48E RID: 50318 RVA: 0x00478826 File Offset: 0x00476A26
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C48F RID: 50319 RVA: 0x0047882F File Offset: 0x00476A2F
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C490 RID: 50320 RVA: 0x00478839 File Offset: 0x00476A39
		public override bool Unmutate(GameObject GO)
		{
			return base.Unmutate(GO);
		}
	}
}
