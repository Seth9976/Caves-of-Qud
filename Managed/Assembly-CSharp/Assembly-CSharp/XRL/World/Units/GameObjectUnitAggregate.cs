using System;
using System.Collections.Generic;
using System.Text;

namespace XRL.World.Units
{
	// Token: 0x020009B8 RID: 2488
	[Serializable]
	public class GameObjectUnitAggregate : GameObjectUnit
	{
		// Token: 0x06006B16 RID: 27414 RVA: 0x00227A40 File Offset: 0x00225C40
		public GameObjectUnitAggregate()
		{
			this.Units = new List<GameObjectUnit>();
		}

		// Token: 0x06006B17 RID: 27415 RVA: 0x00227A53 File Offset: 0x00225C53
		public GameObjectUnitAggregate(string Description, params GameObjectUnit[] Units)
		{
			this.Description = Description;
			this.Units = new List<GameObjectUnit>(Units);
		}

		// Token: 0x06006B18 RID: 27416 RVA: 0x00227A6E File Offset: 0x00225C6E
		public GameObjectUnitAggregate(params GameObjectUnit[] Units)
		{
			this.Units = new List<GameObjectUnit>(Units);
		}

		// Token: 0x06006B19 RID: 27417 RVA: 0x00227A82 File Offset: 0x00225C82
		public GameObjectUnitAggregate(IEnumerable<GameObjectUnit> Units)
		{
			this.Units = new List<GameObjectUnit>(Units);
		}

		// Token: 0x06006B1A RID: 27418 RVA: 0x00227A98 File Offset: 0x00225C98
		public override void Apply(GameObject Object)
		{
			foreach (GameObjectUnit gameObjectUnit in this.Units)
			{
				gameObjectUnit.Apply(Object);
			}
		}

		// Token: 0x06006B1B RID: 27419 RVA: 0x00227AEC File Offset: 0x00225CEC
		public override void Remove(GameObject Object)
		{
			foreach (GameObjectUnit gameObjectUnit in this.Units)
			{
				gameObjectUnit.Remove(Object);
			}
		}

		// Token: 0x06006B1C RID: 27420 RVA: 0x00227B40 File Offset: 0x00225D40
		public override string GetDescription(bool Inscription = false)
		{
			if (this.Description != null)
			{
				return this.Description;
			}
			if (this.Units.IsNullOrEmpty<GameObjectUnit>())
			{
				return "";
			}
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			foreach (GameObjectUnit gameObjectUnit in this.Units)
			{
				stringBuilder.Compound(gameObjectUnit.GetDescription(false), '\n');
			}
			return this.Description = Event.FinalizeString(stringBuilder);
		}

		// Token: 0x04003108 RID: 12552
		public List<GameObjectUnit> Units;

		// Token: 0x04003109 RID: 12553
		public string Description;
	}
}
