using System;
using System.Collections.Generic;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x0200032B RID: 811
	internal class MeshGenerationNodeManager : IDisposable
	{
		// Token: 0x06001762 RID: 5986 RVA: 0x0005A6D3 File Offset: 0x000588D3
		public MeshGenerationNodeManager(EntryRecorder entryRecorder)
		{
			this.m_EntryRecorder = entryRecorder;
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x0005A6F0 File Offset: 0x000588F0
		public void CreateNode(Entry parentEntry, out MeshGenerationNode node)
		{
			MeshGenerationNodeImpl meshGenerationNodeImpl = this.CreateImpl(parentEntry, true);
			meshGenerationNodeImpl.GetNode(out node);
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x0005A710 File Offset: 0x00058910
		public void CreateUnsafeNode(Entry parentEntry, out UnsafeMeshGenerationNode node)
		{
			MeshGenerationNodeImpl meshGenerationNodeImpl = this.CreateImpl(parentEntry, false);
			meshGenerationNodeImpl.GetUnsafeNode(out node);
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x0005A730 File Offset: 0x00058930
		private MeshGenerationNodeImpl CreateImpl(Entry parentEntry, bool safe)
		{
			bool disposed = this.disposed;
			MeshGenerationNodeImpl meshGenerationNodeImpl;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
				meshGenerationNodeImpl = null;
			}
			else
			{
				bool flag = this.m_Nodes.Count == this.m_UsedCounter;
				if (flag)
				{
					for (int i = 0; i < 200; i++)
					{
						this.m_Nodes.Add(new MeshGenerationNodeImpl());
					}
				}
				List<MeshGenerationNodeImpl> nodes = this.m_Nodes;
				int usedCounter = this.m_UsedCounter;
				this.m_UsedCounter = usedCounter + 1;
				MeshGenerationNodeImpl meshGenerationNodeImpl2 = nodes[usedCounter];
				meshGenerationNodeImpl2.Init(parentEntry, this.m_EntryRecorder, safe);
				meshGenerationNodeImpl = meshGenerationNodeImpl2;
			}
			return meshGenerationNodeImpl;
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x0005A7CC File Offset: 0x000589CC
		public void ResetAll()
		{
			for (int i = 0; i < this.m_UsedCounter; i++)
			{
				this.m_Nodes[i].Reset();
			}
			this.m_UsedCounter = 0;
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x0005A808 File Offset: 0x00058A08
		// (set) Token: 0x06001768 RID: 5992 RVA: 0x0005A810 File Offset: 0x00058A10
		private protected bool disposed { protected get; private set; }

		// Token: 0x06001769 RID: 5993 RVA: 0x0005A819 File Offset: 0x00058A19
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x0005A82C File Offset: 0x00058A2C
		protected void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					int i = 0;
					int count = this.m_Nodes.Count;
					while (i < count)
					{
						this.m_Nodes[i].Dispose();
						i++;
					}
					this.m_Nodes.Clear();
				}
				this.disposed = true;
			}
		}

		// Token: 0x04000CD8 RID: 3288
		private List<MeshGenerationNodeImpl> m_Nodes = new List<MeshGenerationNodeImpl>(8);

		// Token: 0x04000CD9 RID: 3289
		private int m_UsedCounter;

		// Token: 0x04000CDA RID: 3290
		private EntryRecorder m_EntryRecorder;
	}
}
