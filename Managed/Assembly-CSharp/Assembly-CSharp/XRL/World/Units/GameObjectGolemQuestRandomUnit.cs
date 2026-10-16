using System;
using System.Collections.Generic;
using System.Text;
using XRL.World.Quests.GolemQuest;

namespace XRL.World.Units
{
	// Token: 0x020009AC RID: 2476
	[Serializable]
	public class GameObjectGolemQuestRandomUnit : GameObjectUnit
	{
		// Token: 0x06006AD4 RID: 27348 RVA: 0x002267D0 File Offset: 0x002249D0
		public override void Apply(GameObject Object)
		{
			GolemQuestSystem golemQuestSystem = GolemQuestSystem.Get();
			GolemQuestSelection golemQuestSelection;
			if (golemQuestSystem == null)
			{
				golemQuestSelection = null;
			}
			else
			{
				Dictionary<string, GolemQuestSelection> selections = golemQuestSystem.Selections;
				golemQuestSelection = ((selections != null) ? selections.GetValue(this.SelectionID, null) : null);
			}
			GolemQuestSelection golemQuestSelection2 = golemQuestSelection;
			if (golemQuestSelection2 == null)
			{
				return;
			}
			StringBuilder stringBuilder = (this.Description.IsNullOrEmpty() ? Event.NewStringBuilder(null) : null);
			int num = 0;
			int num2 = 0;
			while (num2 < 100 && num < this.Amount)
			{
				IEnumerable<GameObjectUnit> enumerable = golemQuestSelection2.YieldRandomEffects();
				int num3 = num;
				foreach (GameObjectUnit gameObjectUnit in enumerable)
				{
					if (num3 == num)
					{
						num++;
					}
					gameObjectUnit.Apply(Object);
					if (gameObjectUnit.CanInscribe() && stringBuilder != null)
					{
						stringBuilder.Compound(gameObjectUnit.GetDescription(true), '\n');
					}
				}
				num2++;
			}
			if (stringBuilder != null)
			{
				this.Description = stringBuilder.ToString();
			}
		}

		// Token: 0x06006AD5 RID: 27349 RVA: 0x002268B4 File Offset: 0x00224AB4
		public override void Reset()
		{
			base.Reset();
			this.SelectionID = null;
			this.Amount = 0;
		}

		// Token: 0x06006AD6 RID: 27350 RVA: 0x002268CA File Offset: 0x00224ACA
		public override string GetDescription(bool Inscription = false)
		{
			if (Inscription)
			{
				return this.Description;
			}
			string text;
			if ((text = this.Description) == null)
			{
				text = string.Format("{0} random effect{1}", this.Amount, (this.Amount == 1) ? "" : "s");
			}
			return text;
		}

		// Token: 0x040030EB RID: 12523
		public string SelectionID;

		// Token: 0x040030EC RID: 12524
		public string Description;

		// Token: 0x040030ED RID: 12525
		public int Amount;
	}
}
