using System;
using System.Collections.Generic;
using System.Text;
using XRL.UI;

namespace XRL.World
{
	// Token: 0x0200092A RID: 2346
	[Serializable]
	public class DynamicQuestRewardElement_ChoiceFromPopulation : DynamicQuestRewardElement
	{
		// Token: 0x06005A0C RID: 23052 RVA: 0x001B49AF File Offset: 0x001B2BAF
		public DynamicQuestRewardElement_ChoiceFromPopulation()
		{
		}

		// Token: 0x06005A0D RID: 23053 RVA: 0x001B49BE File Offset: 0x001B2BBE
		public DynamicQuestRewardElement_ChoiceFromPopulation(string table, int nChoices)
			: this()
		{
			this.table = table;
			this.nChoices = nChoices;
		}

		// Token: 0x06005A0E RID: 23054 RVA: 0x001B49D4 File Offset: 0x001B2BD4
		public override string getRewardConversationType()
		{
			return null;
		}

		// Token: 0x06005A0F RID: 23055 RVA: 0x001B49D7 File Offset: 0x001B2BD7
		public override string getRewardAcceptQuestText()
		{
			return null;
		}

		// Token: 0x06005A10 RID: 23056 RVA: 0x001B49DC File Offset: 0x001B2BDC
		public override void award()
		{
			List<List<PopulationResult>> list = PopulationManager.RollDistinctFrom(this.table, this.nChoices, null, null);
			if (list.Count > 0)
			{
				List<string> list2 = new List<string>();
				List<char> list3 = new List<char>();
				char c = 'a';
				List<List<GameObject>> list4 = new List<List<GameObject>>();
				foreach (List<PopulationResult> list5 in list)
				{
					StringBuilder stringBuilder = new StringBuilder();
					List<GameObject> list6 = new List<GameObject>();
					foreach (PopulationResult populationResult in list5)
					{
						GameObject gameObject = GameObjectFactory.Factory.CreateObject(populationResult.Blueprint);
						for (int i = 0; i < populationResult.Number; i++)
						{
							list6.Add(gameObject.DeepCopy(false, true, null));
						}
						string text = (gameObject.HasTagOrProperty("DisplayFullNameAsReward") ? gameObject.GetDisplayName(int.MaxValue, null, null, true, false, false, false, false, false, true, false, false, false, false, false, false, null, false, false, false, false, null, false, null, false, true) : gameObject.DisplayNameOnlyDirect);
						if (stringBuilder.Length > 0)
						{
							stringBuilder.Append(", ");
						}
						if (populationResult.Number == 1)
						{
							stringBuilder.Append(text);
						}
						else
						{
							stringBuilder.Append(text + " &Wx" + populationResult.Number.ToString());
						}
					}
					list4.Add(list6);
					list2.Add(stringBuilder.ToString());
					list3.Add(c);
					c += '\u0001';
				}
				int num = Popup.PickOption("Choose a reward", null, "", "Sounds/UI/ui_notification", list2.ToArray(), list3.ToArray(), null, null, null, null, null, 1, 60, 0, -1, false, false, false, true, false, null, null);
				foreach (GameObject gameObject2 in list4[num])
				{
					gameObject2.MakeUnderstood(false);
					The.Player.ReceiveObject(gameObject2, false, null);
				}
			}
		}

		// Token: 0x04002C97 RID: 11415
		public string table;

		// Token: 0x04002C98 RID: 11416
		public int nChoices = 3;
	}
}
