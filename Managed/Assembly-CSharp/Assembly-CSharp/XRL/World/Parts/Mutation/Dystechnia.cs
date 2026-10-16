using System;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013CE RID: 5070
	[Serializable]
	public class Dystechnia : BaseMutation
	{
		// Token: 0x0600C3E1 RID: 50145 RVA: 0x00476AD1 File Offset: 0x00474CD1
		public Dystechnia()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C3E2 RID: 50146 RVA: 0x00476AE4 File Offset: 0x00474CE4
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C3E3 RID: 50147 RVA: 0x00476AE7 File Offset: 0x00474CE7
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ExamineCriticalFailureEvent.ID || ID == GetTinkeringBonusEvent.ID || ID == RepairCriticalFailureEvent.ID;
		}

		// Token: 0x0600C3E4 RID: 50148 RVA: 0x00476B10 File Offset: 0x00474D10
		public override bool HandleEvent(GetTinkeringBonusEvent E)
		{
			if (E.Type == "Inspect")
			{
				E.Bonus -= 20;
			}
			else if (E.Type == "Examine" || E.Type == "Repair" || E.Type == "ReverseEngineerTurns" || E.Type == "ItemModding" || E.Type == "Disarming")
			{
				E.Bonus--;
			}
			else if (E.Type == "Hacking")
			{
				E.Bonus -= 2;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C3E5 RID: 50149 RVA: 0x00476BD4 File Offset: 0x00474DD4
		public override bool HandleEvent(ExamineCriticalFailureEvent E)
		{
			if (E.Pass == ExamineCriticalFailureEvent.PASSES)
			{
				Dystechnia.CauseExplosion(E.Item, E.Actor, E);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C3E6 RID: 50150 RVA: 0x00476BFE File Offset: 0x00474DFE
		public override bool HandleEvent(RepairCriticalFailureEvent E)
		{
			Dystechnia.CauseExplosion(E.Item, E.Actor, E);
			return false;
		}

		// Token: 0x0600C3E7 RID: 50151 RVA: 0x00476C14 File Offset: 0x00474E14
		public static void CauseExplosion(GameObject Object, GameObject Actor = null, IEvent Event = null)
		{
			int complexity = Object.GetComplexity();
			Object.PotentiallyAngerOwner(Actor, "DontWarnOnExamine");
			IComponent<GameObject>.XDidY(Object, "explode", null, "!", null, null, null, Actor, false, false, null, null, false, false, false, true, Actor.IsPlayer(), null);
			Object.Explode((int)((float)complexity * 3000f * Stat.Random(0.8f, 1.2f)), Actor, null, 1f, complexity >= 8, false, false, 0, null);
			if (Event != null)
			{
				Event.RequestInterfaceExit();
			}
		}

		// Token: 0x0600C3E8 RID: 50152 RVA: 0x00476C94 File Offset: 0x00474E94
		public override string GetDescription()
		{
			string text = "";
			text += "You are befuddled by technological complexity.\n\n";
			text += "You're much worse at examining artifacts.\n";
			text += "You can't have artifacts identified for you because you don't understand their explanations.\n";
			text += "When you fail severely during artifact examination, the artifact explodes.\n";
			if (Options.AnySifrah)
			{
				text += "You lose a turn in most tinkering Sifrah games, and two turns in hacking Sifrah games.";
			}
			return text;
		}

		// Token: 0x0600C3E9 RID: 50153 RVA: 0x00476CEB File Offset: 0x00474EEB
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C3EA RID: 50154 RVA: 0x00476CF2 File Offset: 0x00474EF2
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C3EB RID: 50155 RVA: 0x00476CFB File Offset: 0x00474EFB
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C3EC RID: 50156 RVA: 0x00476D05 File Offset: 0x00474F05
		public override bool Unmutate(GameObject GO)
		{
			return base.Unmutate(GO);
		}
	}
}
