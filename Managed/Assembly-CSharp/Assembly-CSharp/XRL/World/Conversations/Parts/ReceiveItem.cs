using System;
using System.Collections.Generic;
using XRL.Language;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C11 RID: 3089
	public class ReceiveItem : IConversationPart
	{
		// Token: 0x06007BD5 RID: 31701 RVA: 0x002E88FD File Offset: 0x002E6AFD
		public ReceiveItem()
		{
		}

		// Token: 0x06007BD6 RID: 31702 RVA: 0x002E8910 File Offset: 0x002E6B10
		public ReceiveItem(string Blueprints)
		{
			this.Blueprints = Blueprints;
		}

		// Token: 0x06007BD7 RID: 31703 RVA: 0x002E892A File Offset: 0x002E6B2A
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == EnteredElementEvent.ID;
		}

		// Token: 0x06007BD8 RID: 31704 RVA: 0x002E8940 File Offset: 0x002E6B40
		public override bool HandleEvent(EnteredElementEvent E)
		{
			List<PopulationResult> list = (this.Table.IsNullOrEmpty() ? new List<PopulationResult>() : PopulationManager.Generate(this.Table, null, null, null, null, null));
			if (!this.Blueprints.IsNullOrEmpty())
			{
				foreach (string text in this.Blueprints.Split(',', StringSplitOptions.None))
				{
					int num = text.IndexOf(':');
					PopulationResult populationResult;
					if (num < 0)
					{
						populationResult = new PopulationResult(text, 1, null, null);
					}
					else
					{
						string text2 = text.Substring(num + 1);
						string text3 = text.Substring(0, num);
						if (text2.Contains(":"))
						{
							populationResult = new PopulationResult(text3, 1, text2, null);
						}
						else
						{
							bool flag;
							DieRoll dieRoll = new DieRoll(text2, out flag);
							if (flag)
							{
								populationResult = new PopulationResult(text3, 1, text2, null);
							}
							else
							{
								populationResult = new PopulationResult(text3, dieRoll.Resolve(), null, null);
							}
						}
					}
					list.Add(populationResult);
				}
			}
			List<GameObject> list2 = Event.NewGameObjectList();
			bool flag2 = this.Identify == "*" || this.Identify.EqualsNoCase("all");
			for (int j = 0; j < list.Count; j++)
			{
				bool flag3 = false;
				for (int k = list[j].Number; k > 0; k--)
				{
					int num2;
					int num3;
					bool flag4;
					this.ParseItemHints(list[j], !flag3, out num2, out num3, out flag4);
					if (!flag4)
					{
						if (!flag3 && num2 != 0)
						{
							k = num2;
							flag3 = true;
						}
						int num4 = this.Mods.RollCached();
						GameObject gameObject = (this.FromSpeaker ? The.Speaker.HasItemWithBlueprint(list[j].Blueprint) : null) ?? GameObject.Create(list[j].Blueprint, num3, num4, null, null, null, null, null);
						if (flag2 || (!this.Identify.IsNullOrEmpty() && this.Identify.CachedCommaExpansion().Contains(list[j].Blueprint)))
						{
							gameObject.MakeUnderstood(false);
						}
						if (k > 1 && gameObject.CanGenerateStacked())
						{
							gameObject.Count = k;
							k = 0;
						}
						list2.Add(gameObject);
					}
				}
			}
			if (this.Pick && list2.Count > 1)
			{
				GameObject gameObject2 = Popup.PickGameObject("Choose a reward", list2, false, false, true, null, 0, false);
				for (int l = list2.Count - 1; l >= 0; l--)
				{
					if (list2[l] != gameObject2)
					{
						list2[l].Obliterate(null, false, null);
						list2.RemoveAt(l);
					}
				}
			}
			if (list2.Count > 0)
			{
				List<string> list3 = new List<string>(list.Count);
				for (int m = 0; m < list2.Count; m++)
				{
					list3.Add(list2[m].an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false));
					The.Player.ReceiveObject(list2[m], false, null);
				}
				Popup.Show("You receive " + Grammar.MakeAndList(list3, true) + "!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007BD9 RID: 31705 RVA: 0x002E8C84 File Offset: 0x002E6E84
		private void ParseItemHints(PopulationResult Result, bool AllowCount, out int Count, out int ModChance, out bool Skip)
		{
			Count = 0;
			ModChance = 0;
			Skip = false;
			if (Result.Hint == null)
			{
				return;
			}
			int num;
			if (AllowCount)
			{
				num = Result.Hint.IndexOf("Count:", StringComparison.Ordinal);
				if (num >= 0)
				{
					num += 6;
					int num2 = Result.Hint.IndexOf(',', num);
					string text = ((num2 < 0) ? Result.Hint.Substring(num) : Result.Hint.Substring(num, num2 - num));
					Count = text.RollCached();
				}
			}
			num = Result.Hint.IndexOf("SetBonusModChance:", StringComparison.Ordinal);
			if (num >= 0)
			{
				num += 18;
				int num3 = Result.Hint.IndexOf(',', num);
				string text2 = ((num3 < 0) ? Result.Hint.Substring(num) : Result.Hint.Substring(num, num3 - num));
				ModChance = text2.RollCached();
			}
			num = Result.Hint.IndexOf("IfSkillDominant:", StringComparison.Ordinal);
			if (num >= 0)
			{
				num += 16;
				int num4 = Result.Hint.IndexOf(',', num);
				string[] array = ((num4 < 0) ? Result.Hint.Substring(num) : Result.Hint.Substring(num, num4 - num)).Split('/', StringSplitOptions.None);
				if (array.Length < 2 || The.Player.GetSkillAndPowerCountInSkill(array[0]) <= The.Player.GetSkillAndPowerCountInSkill(array[1]))
				{
					Skip = true;
				}
			}
			num = Result.Hint.IndexOf("IfNotSkillDominant:", StringComparison.Ordinal);
			if (num >= 0)
			{
				num += 19;
				int num5 = Result.Hint.IndexOf(',', num);
				string[] array2 = ((num5 < 0) ? Result.Hint.Substring(num) : Result.Hint.Substring(num, num5 - num)).Split('/', StringSplitOptions.None);
				if (array2.Length < 2 || The.Player.GetSkillAndPowerCountInSkill(array2[0]) > The.Player.GetSkillAndPowerCountInSkill(array2[1]))
				{
					Skip = true;
				}
			}
		}

		// Token: 0x04003846 RID: 14406
		public string Table;

		// Token: 0x04003847 RID: 14407
		public string Blueprints;

		// Token: 0x04003848 RID: 14408
		public string Identify;

		// Token: 0x04003849 RID: 14409
		public string Mods = "0";

		// Token: 0x0400384A RID: 14410
		public bool Pick;

		// Token: 0x0400384B RID: 14411
		public bool FromSpeaker;
	}
}
