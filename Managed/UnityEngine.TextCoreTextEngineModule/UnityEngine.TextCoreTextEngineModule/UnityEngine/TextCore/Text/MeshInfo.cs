using System;
using UnityEngine.Bindings;
using UnityEngine.TextCore.LowLevel;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000027 RID: 39
	[VisibleToOtherModules(new string[] { "UnityEngine.IMGUIModule", "UnityEngine.UIElementsModule" })]
	internal struct MeshInfo
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00005154 File Offset: 0x00003354
		public MeshInfo(int size, bool isIMGUI)
		{
			this = default(MeshInfo);
			this.applySDF = true;
			this.material = null;
			if (isIMGUI)
			{
				size = Mathf.Min(size, 16383);
			}
			int num = size * 4;
			int num2 = size * 6;
			this.vertexCount = 0;
			this.vertexBufferSize = num;
			this.vertexData = new TextCoreVertex[num];
			this.material = null;
			this.glyphRenderMode = GlyphRenderMode.DEFAULT;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000051BC File Offset: 0x000033BC
		internal void ResizeMeshInfo(int size, bool isIMGUI)
		{
			if (isIMGUI)
			{
				size = Mathf.Min(size, 16383);
			}
			int num = size * 4;
			int num2 = size * 6;
			this.vertexBufferSize = num;
			Array.Resize<TextCoreVertex>(ref this.vertexData, num);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000051FC File Offset: 0x000033FC
		internal void Clear(bool uploadChanges)
		{
			bool flag = this.vertexData == null;
			if (!flag)
			{
				Array.Clear(this.vertexData, 0, this.vertexData.Length);
				this.vertexBufferSize = this.vertexData.Length;
				this.vertexCount = 0;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00005248 File Offset: 0x00003448
		internal void ClearUnusedVertices()
		{
			int num = this.vertexData.Length - this.vertexCount;
			bool flag = num > 0;
			if (flag)
			{
				Array.Clear(this.vertexData, this.vertexCount, num);
			}
			this.vertexBufferSize = this.vertexData.Length;
		}

		// Token: 0x040000C3 RID: 195
		public int vertexCount;

		// Token: 0x040000C4 RID: 196
		public TextCoreVertex[] vertexData;

		// Token: 0x040000C5 RID: 197
		public Material material;

		// Token: 0x040000C6 RID: 198
		[Ignore]
		public int vertexBufferSize;

		// Token: 0x040000C7 RID: 199
		[Ignore]
		public bool applySDF;

		// Token: 0x040000C8 RID: 200
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal GlyphRenderMode glyphRenderMode;
	}
}
