using System;
using XRL.Collections;

namespace XRL.World.AI.Pathfinding
{
	// Token: 0x02000B73 RID: 2931
	internal class NavigationQueue
	{
		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x0600756D RID: 30061 RVA: 0x002C0155 File Offset: 0x002BE355
		public int Count
		{
			get
			{
				return this.Items.Count;
			}
		}

		// Token: 0x0600756E RID: 30062 RVA: 0x002C0162 File Offset: 0x002BE362
		public void Clear()
		{
			this.Items.Clear();
		}

		// Token: 0x0600756F RID: 30063 RVA: 0x002C0170 File Offset: 0x002BE370
		public void Enqueue(GridNode Item)
		{
			int num = this.Items.Count;
			this.Items.Add(Item);
			Item.Index = num;
			GridNode[] array = this.Items.GetArray();
			while (num != 0)
			{
				int num2 = (num - 1) / 2;
				if (array[num].CompareTo(array[num2]) >= 0)
				{
					break;
				}
				GridNode gridNode = array[num];
				array[num] = array[num2];
				array[num].Index = num;
				array[num2] = gridNode;
				array[num2].Index = num2;
				num = num2;
			}
		}

		// Token: 0x06007570 RID: 30064 RVA: 0x002C01E4 File Offset: 0x002BE3E4
		public void Requeue(GridNode Item)
		{
			int i = Item.Index;
			GridNode[] array = this.Items.GetArray();
			while (i > 0)
			{
				int num = (i - 1) / 2;
				if (array[i].CompareTo(array[num]) >= 0)
				{
					break;
				}
				GridNode gridNode = array[i];
				array[i] = array[num];
				array[i].Index = i;
				array[num] = gridNode;
				array[num].Index = num;
				i = num;
			}
		}

		// Token: 0x06007571 RID: 30065 RVA: 0x002C0240 File Offset: 0x002BE440
		public GridNode Dequeue()
		{
			GridNode[] array = this.Items.GetArray();
			GridNode gridNode = array[0];
			int num = this.Items.Count - 1;
			array[0] = array[num];
			array[0].Index = 0;
			this.Items.RemoveAt(num);
			int num2 = 0;
			for (;;)
			{
				int num3 = num2;
				int num4 = 2 * num2 + 1;
				int num5 = num4 + 1;
				if (num4 < num && array[num2].CompareTo(array[num4]) > 0)
				{
					num2 = num4;
				}
				if (num5 < num && array[num2].CompareTo(array[num5]) > 0)
				{
					num2 = num5;
				}
				if (num2 == num3)
				{
					break;
				}
				GridNode gridNode2 = array[num2];
				array[num2] = array[num3];
				array[num2].Index = num2;
				array[num3] = gridNode2;
				array[num3].Index = num3;
			}
			return gridNode;
		}

		// Token: 0x040035B1 RID: 13745
		private Rack<GridNode> Items = new Rack<GridNode>();
	}
}
