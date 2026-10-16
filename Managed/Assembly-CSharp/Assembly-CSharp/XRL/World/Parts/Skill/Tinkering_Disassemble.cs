using System;
using System.Collections.Generic;
using System.Text;
using XRL.UI;
using XRL.World.Tinkering;

namespace XRL.World.Parts.Skill
{
	// Token: 0x020013B9 RID: 5049
	[Serializable]
	public class Tinkering_Disassemble : BaseSkill
	{
		// Token: 0x0600C2D9 RID: 49881 RVA: 0x0047263E File Offset: 0x0047083E
		public static void Init()
		{
		}

		// Token: 0x0600C2DA RID: 49882 RVA: 0x00472640 File Offset: 0x00470840
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AllowInventoryStackEvent.ID || ID == AutoexploreObjectEvent.ID || ID == InventoryActionEvent.ID || ID == OwnerGetInventoryActionsEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == TookEvent.ID;
		}

		// Token: 0x0600C2DB RID: 49883 RVA: 0x0047267E File Offset: 0x0047087E
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("scholarship", 2);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C2DC RID: 49884 RVA: 0x004726A4 File Offset: 0x004708A4
		public override bool HandleEvent(TookEvent E)
		{
			if (E.Context != "Tinkering" && this.WantToDisassemble(E.Item))
			{
				InventoryActionEvent.Check(E.Item, this.ParentObject, E.Item, "DisassembleAll", true, false, false, false, false, 0, 0, 0, null, null, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C2DD RID: 49885 RVA: 0x00472701 File Offset: 0x00470901
		public override bool HandleEvent(AllowInventoryStackEvent E)
		{
			return !this.WantToDisassemble(E.Item) && base.HandleEvent(E);
		}

		// Token: 0x0600C2DE RID: 49886 RVA: 0x0047271A File Offset: 0x0047091A
		public override bool HandleEvent(AutoexploreObjectEvent E)
		{
			if (!E.AutogetOnlyMode && E.Command == null && this.WantToDisassemble(E.Item))
			{
				E.Command = "DisassembleAll";
				E.AllowRetry = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C2DF RID: 49887 RVA: 0x00472754 File Offset: 0x00470954
		public override bool HandleEvent(OwnerGetInventoryActionsEvent E)
		{
			if (Tinkering_Disassemble.CanBeConsideredScrap(E.Object) && !TinkeringHelpers.ConsiderStandardScrap(E.Object) && E.Actor.IsPlayer() && E.Object.Understood())
			{
				if (Tinkering_Disassemble.CheckScrapToggle(E.Object))
				{
					E.AddAction("Toggle Scrap", "stop treating these as scrap", "ToggleScrap", "scrap", 'S', true, 0, 0, false, true, false, false, true, null, false);
				}
				else
				{
					E.AddAction("Toggle Scrap", "treat these as scrap", "ToggleScrap", "scrap", 'S', true, 0, 0, false, true, false, false, true, null, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C2E0 RID: 49888 RVA: 0x004727FC File Offset: 0x004709FC
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "ToggleScrap" && E.Actor.IsPlayer())
			{
				Tinkering_Disassemble.ToggleScrap(E.Item);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C2E1 RID: 49889 RVA: 0x00472830 File Offset: 0x00470A30
		public static string ModProfile(GameObject obj)
		{
			Tinkering_Disassemble.ModNames.Clear();
			int i = 0;
			int count = obj.PartsList.Count;
			while (i < count)
			{
				IModification modification = obj.PartsList[i] as IModification;
				if (modification != null)
				{
					Tinkering_Disassemble.ModNames.Add(modification.Name);
				}
				i++;
			}
			if (Tinkering_Disassemble.ModNames.Count <= 0)
			{
				return "";
			}
			if (Tinkering_Disassemble.ModNames.Count > 1)
			{
				Tinkering_Disassemble.ModNames.Sort();
				Tinkering_Disassemble.ModNamesSB.Clear();
				foreach (string text in Tinkering_Disassemble.ModNames)
				{
					Tinkering_Disassemble.ModNamesSB.Append('+').Append(text);
				}
				return Tinkering_Disassemble.ModNamesSB.ToString();
			}
			return "+" + Tinkering_Disassemble.ModNames[0];
		}

		// Token: 0x0600C2E2 RID: 49890 RVA: 0x00472930 File Offset: 0x00470B30
		public static string ToggleKey(GameObject obj)
		{
			string text = "ScrapToggle_" + obj.GetTinkeringBlueprint() + Tinkering_Disassemble.ModProfile(obj);
			Tinkering_Mine part = obj.GetPart<Tinkering_Mine>();
			if (part != null)
			{
				if (part.Timer <= 0)
				{
					text += "/AsMine";
				}
				else
				{
					text += "/AsBomb";
				}
			}
			return text;
		}

		// Token: 0x0600C2E3 RID: 49891 RVA: 0x00472984 File Offset: 0x00470B84
		public static bool CanBeConsideredScrap(GameObject obj)
		{
			if (!obj.IsReal)
			{
				return false;
			}
			TinkerItem part = obj.GetPart<TinkerItem>();
			if (part == null)
			{
				return false;
			}
			if (!part.CanDisassemble)
			{
				return false;
			}
			if (obj.IsCreature)
			{
				return false;
			}
			Tinkering_Mine part2 = obj.GetPart<Tinkering_Mine>();
			return (part2 == null || !part2.Armed) && !obj.HasTag("BaseObject");
		}

		// Token: 0x0600C2E4 RID: 49892 RVA: 0x004729DF File Offset: 0x00470BDF
		public static bool CheckScrapToggle(GameObject obj)
		{
			return The.Game.GetBooleanGameState(Tinkering_Disassemble.ToggleKey(obj), false) && obj.Understood();
		}

		// Token: 0x0600C2E5 RID: 49893 RVA: 0x004729FC File Offset: 0x00470BFC
		public static void SetScrapToggle(GameObject obj, bool flag)
		{
			if (flag)
			{
				The.Game.SetBooleanGameState(Tinkering_Disassemble.ToggleKey(obj), true);
				return;
			}
			The.Game.RemoveBooleanGameState(Tinkering_Disassemble.ToggleKey(obj));
		}

		// Token: 0x0600C2E6 RID: 49894 RVA: 0x00472A23 File Offset: 0x00470C23
		public static void ToggleScrap(GameObject obj)
		{
			Tinkering_Disassemble.SetScrapToggle(obj, !Tinkering_Disassemble.CheckScrapToggle(obj));
		}

		// Token: 0x0600C2E7 RID: 49895 RVA: 0x00472A34 File Offset: 0x00470C34
		public static bool ConsiderScrap(GameObject obj)
		{
			return TinkeringHelpers.ConsiderStandardScrap(obj) || Tinkering_Disassemble.CheckScrapToggle(obj);
		}

		// Token: 0x0600C2E8 RID: 49896 RVA: 0x00472A46 File Offset: 0x00470C46
		public bool WantToDisassemble(GameObject Object)
		{
			return Tinkering_Disassemble.WantToDisassemble(this.ParentObject, Object);
		}

		// Token: 0x0600C2E9 RID: 49897 RVA: 0x00472A54 File Offset: 0x00470C54
		public static bool WantToDisassemble(GameObject Actor, GameObject Object)
		{
			return Actor.IsPlayer() && Options.AutoDisassembleScrap && GameObject.Validate(ref Object) && Tinkering_Disassemble.CanBeConsideredScrap(Object) && Tinkering_Disassemble.ConsiderScrap(Object) && Object.Owner.IsNullOrEmpty() && !Object.IsImportant() && !Object.IsInStasis() && (!Object.HasPart<LiquidVolume>() || Object.GetPart<LiquidVolume>().Volume <= 0);
		}

		// Token: 0x04004DAA RID: 19882
		private static List<string> ModNames = new List<string>();

		// Token: 0x04004DAB RID: 19883
		private static StringBuilder ModNamesSB = new StringBuilder();
	}
}
