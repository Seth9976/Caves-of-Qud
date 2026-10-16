using System;
using System.Collections.Generic;

namespace XRL.UI
{
	// Token: 0x020014B8 RID: 5304
	public abstract class ConsoleTreeNode<T> where T : ConsoleTreeNode<T>
	{
		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x0600D047 RID: 53319 RVA: 0x004CD908 File Offset: 0x004CBB08
		public virtual bool Visible
		{
			get
			{
				T t = this.ParentNode;
				return t == null || t.Expand;
			}
		}

		// Token: 0x0600D048 RID: 53320 RVA: 0x004CD920 File Offset: 0x004CBB20
		public ConsoleTreeNode(string Category = "", bool Expand = false, T ParentNode = default(T))
		{
			this.Category = Category;
			this.Expand = Expand;
			this.ParentNode = ParentNode;
		}

		// Token: 0x0600D049 RID: 53321 RVA: 0x004CD940 File Offset: 0x004CBB40
		public static int NextVisible(List<T> Nodes, ref int Index, int Mod = 0)
		{
			int num = Index + Mod;
			while (num < Nodes.Count && !Nodes[num].Visible)
			{
				num++;
			}
			if (num != Nodes.Count)
			{
				return Index = num;
			}
			return Index;
		}

		// Token: 0x0600D04A RID: 53322 RVA: 0x004CD988 File Offset: 0x004CBB88
		public static int PrevVisible(List<T> Nodes, ref int Index, int Mod = 0)
		{
			int num = Index + Mod;
			while (num >= 0 && !Nodes[num].Visible)
			{
				num--;
			}
			if (num != -1)
			{
				return Index = num;
			}
			return Index;
		}

		// Token: 0x040051BF RID: 20927
		public string Category;

		// Token: 0x040051C0 RID: 20928
		public bool Expand;

		// Token: 0x040051C1 RID: 20929
		public T ParentNode;
	}
}
