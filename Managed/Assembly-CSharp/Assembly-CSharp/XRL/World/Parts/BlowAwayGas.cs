using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000E4F RID: 3663
	[Serializable]
	public class BlowAwayGas : IPart
	{
		// Token: 0x06009462 RID: 37986 RVA: 0x00356824 File Offset: 0x00354A24
		public override bool WantEvent(int ID, int Cascade)
		{
			return ID == SingletonEvent<BeginTakeActionEvent>.ID;
		}

		// Token: 0x06009463 RID: 37987 RVA: 0x0035682E File Offset: 0x00354A2E
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			this.BlowAway();
			return base.HandleEvent(E);
		}

		// Token: 0x06009464 RID: 37988 RVA: 0x00356840 File Offset: 0x00354A40
		public void BlowAway()
		{
			List<GameObject> gasObjects = this.GasObjects;
			List<string> gasNames = this.GasNames;
			bool flag = !this.Message.IsNullOrEmpty();
			try
			{
				foreach (Cell cell in this.ParentObject.CurrentCell.IterateAdjacent(this.Radius, true, true, true))
				{
					foreach (GameObject gameObject in cell.Objects)
					{
						Gas gas;
						if (gameObject.TryGetPart<Gas>(out gas) && gas.Creator != this.ParentObject)
						{
							gasObjects.Add(gameObject);
						}
					}
				}
				foreach (GameObject gameObject2 in gasObjects)
				{
					string text = this.ParentObject.GetDirectionToward(gameObject2, false);
					if (text == "." || text == "?")
					{
						text = Directions.GetRandomDirection();
					}
					Cell cellFromDirection = gameObject2.CurrentCell.GetCellFromDirection(text, true);
					if (cellFromDirection != null)
					{
						cellFromDirection.AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						if (flag && !gasNames.Contains(gameObject2.Render.DisplayName))
						{
							gasNames.Add(gameObject2.Render.DisplayName);
						}
					}
				}
				if (flag)
				{
					string text2 = IComponent<GameObject>.ConsequentialColor(this.ParentObject, null);
					foreach (string text3 in gasNames)
					{
						this.ParentObject.EmitMessage(this.Message.StartReplace().AddObject(this.ParentObject, null).AddReplacer("gas", text3)
							.ToString(), null, text2, false);
					}
				}
			}
			finally
			{
				gasObjects.Clear();
				gasNames.Clear();
			}
		}

		// Token: 0x04003D4E RID: 15694
		public string Message;

		// Token: 0x04003D4F RID: 15695
		public int Radius = 4;

		// Token: 0x04003D50 RID: 15696
		[NonSerialized]
		private List<GameObject> GasObjects = new List<GameObject>();

		// Token: 0x04003D51 RID: 15697
		[NonSerialized]
		private List<string> GasNames = new List<string>();
	}
}
