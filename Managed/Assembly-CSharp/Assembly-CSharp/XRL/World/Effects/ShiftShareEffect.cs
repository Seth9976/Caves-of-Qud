using System;
using System.Collections.Generic;

namespace XRL.World.Effects
{
	// Token: 0x02000D8A RID: 3466
	[Serializable]
	public class ShiftShareEffect : Effect
	{
		// Token: 0x060089EA RID: 35306 RVA: 0x00313DB3 File Offset: 0x00311FB3
		public override string GetDescription()
		{
			return null;
		}

		// Token: 0x060089EB RID: 35307 RVA: 0x00313DB8 File Offset: 0x00311FB8
		public void TakeShifts(GameObject Item)
		{
			GameObject equipped = Item.Equipped;
			string id = equipped.ID;
			foreach (IPart part in Item.PartsList)
			{
				Dictionary<string, Guid> dictionary;
				if (part.HasStatShifts() && part.StatShifter.ActiveShifts.TryGetValue(id, out dictionary))
				{
					foreach (KeyValuePair<string, Guid> keyValuePair in dictionary)
					{
						Statistic statistic;
						if (equipped.Statistics.TryGetValue(keyValuePair.Key, out statistic))
						{
							Statistic.StatShift shift = statistic.GetShift(keyValuePair.Value);
							base.StatShifter.SetStatShift(statistic.Name, shift.Amount, false);
						}
					}
				}
			}
		}

		// Token: 0x060089EC RID: 35308 RVA: 0x00313EB4 File Offset: 0x003120B4
		public override bool Apply(GameObject Object)
		{
			this.TakeShifts(this.Source);
			return base.Apply(Object);
		}

		// Token: 0x060089ED RID: 35309 RVA: 0x00313EC9 File Offset: 0x003120C9
		public override void Remove(GameObject Object)
		{
			base.StatShifter.RemoveStatShifts();
			base.Remove(Object);
		}

		// Token: 0x060089EE RID: 35310 RVA: 0x00313EDD File Offset: 0x003120DD
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID;
		}

		// Token: 0x060089EF RID: 35311 RVA: 0x00313EF4 File Offset: 0x003120F4
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.Duration != 9999)
			{
				this.Duration--;
			}
			if (!GameObject.Validate(ref this.Source) || !GameObject.Validate(this.Source.Equipped) || this.Source.Equipped.PartyLeader != base.Object)
			{
				this.Duration = 0;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04003A88 RID: 14984
		public GameObject Source;
	}
}
