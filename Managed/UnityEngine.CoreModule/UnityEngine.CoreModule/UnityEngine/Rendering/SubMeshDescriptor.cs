using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020004A1 RID: 1185
	public struct SubMeshDescriptor
	{
		// Token: 0x06002D83 RID: 11651 RVA: 0x0005640C File Offset: 0x0005460C
		public SubMeshDescriptor(int indexStart, int indexCount, MeshTopology topology = MeshTopology.Triangles)
		{
			this.indexStart = indexStart;
			this.indexCount = indexCount;
			this.topology = topology;
			this.bounds = default(Bounds);
			this.baseVertex = 0;
			this.firstVertex = 0;
			this.vertexCount = 0;
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06002D84 RID: 11652 RVA: 0x0005645A File Offset: 0x0005465A
		// (set) Token: 0x06002D85 RID: 11653 RVA: 0x00056462 File Offset: 0x00054662
		public Bounds bounds { readonly get; set; }

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06002D86 RID: 11654 RVA: 0x0005646B File Offset: 0x0005466B
		// (set) Token: 0x06002D87 RID: 11655 RVA: 0x00056473 File Offset: 0x00054673
		public MeshTopology topology { readonly get; set; }

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06002D88 RID: 11656 RVA: 0x0005647C File Offset: 0x0005467C
		// (set) Token: 0x06002D89 RID: 11657 RVA: 0x00056484 File Offset: 0x00054684
		public int indexStart { readonly get; set; }

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06002D8A RID: 11658 RVA: 0x0005648D File Offset: 0x0005468D
		// (set) Token: 0x06002D8B RID: 11659 RVA: 0x00056495 File Offset: 0x00054695
		public int indexCount { readonly get; set; }

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06002D8C RID: 11660 RVA: 0x0005649E File Offset: 0x0005469E
		// (set) Token: 0x06002D8D RID: 11661 RVA: 0x000564A6 File Offset: 0x000546A6
		public int baseVertex { readonly get; set; }

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06002D8E RID: 11662 RVA: 0x000564AF File Offset: 0x000546AF
		// (set) Token: 0x06002D8F RID: 11663 RVA: 0x000564B7 File Offset: 0x000546B7
		public int firstVertex { readonly get; set; }

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x06002D90 RID: 11664 RVA: 0x000564C0 File Offset: 0x000546C0
		// (set) Token: 0x06002D91 RID: 11665 RVA: 0x000564C8 File Offset: 0x000546C8
		public int vertexCount { readonly get; set; }

		// Token: 0x06002D92 RID: 11666 RVA: 0x000564D4 File Offset: 0x000546D4
		public override string ToString()
		{
			return string.Format("(topo={0} indices={1},{2} vertices={3},{4} basevtx={5} bounds={6})", new object[] { this.topology, this.indexStart, this.indexCount, this.firstVertex, this.vertexCount, this.baseVertex, this.bounds });
		}
	}
}
