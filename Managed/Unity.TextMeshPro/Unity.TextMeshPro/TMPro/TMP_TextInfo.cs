using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000062 RID: 98
	[Serializable]
	public class TMP_TextInfo
	{
		// Token: 0x0600050A RID: 1290 RVA: 0x00022874 File Offset: 0x00020A74
		public TMP_TextInfo()
		{
			this.characterInfo = new TMP_CharacterInfo[8];
			this.wordInfo = new TMP_WordInfo[16];
			this.linkInfo = new TMP_LinkInfo[0];
			this.lineInfo = new TMP_LineInfo[2];
			this.pageInfo = new TMP_PageInfo[4];
			this.meshInfo = new TMP_MeshInfo[1];
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x000228D0 File Offset: 0x00020AD0
		internal TMP_TextInfo(int characterCount)
		{
			this.characterInfo = new TMP_CharacterInfo[characterCount];
			this.wordInfo = new TMP_WordInfo[16];
			this.linkInfo = new TMP_LinkInfo[0];
			this.lineInfo = new TMP_LineInfo[2];
			this.pageInfo = new TMP_PageInfo[4];
			this.meshInfo = new TMP_MeshInfo[1];
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0002292C File Offset: 0x00020B2C
		public TMP_TextInfo(TMP_Text textComponent)
		{
			this.textComponent = textComponent;
			this.characterInfo = new TMP_CharacterInfo[8];
			this.wordInfo = new TMP_WordInfo[4];
			this.linkInfo = new TMP_LinkInfo[0];
			this.lineInfo = new TMP_LineInfo[2];
			this.pageInfo = new TMP_PageInfo[4];
			this.meshInfo = new TMP_MeshInfo[1];
			this.meshInfo[0].mesh = textComponent.mesh;
			this.materialCount = 1;
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x000229AC File Offset: 0x00020BAC
		internal void Clear()
		{
			this.characterCount = 0;
			this.spaceCount = 0;
			this.wordCount = 0;
			this.linkCount = 0;
			this.lineCount = 0;
			this.pageCount = 0;
			this.spriteCount = 0;
			for (int i = 0; i < this.meshInfo.Length; i++)
			{
				this.meshInfo[i].vertexCount = 0;
			}
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00022A10 File Offset: 0x00020C10
		internal void ClearAllData()
		{
			this.characterCount = 0;
			this.spaceCount = 0;
			this.wordCount = 0;
			this.linkCount = 0;
			this.lineCount = 0;
			this.pageCount = 0;
			this.spriteCount = 0;
			this.characterInfo = new TMP_CharacterInfo[4];
			this.wordInfo = new TMP_WordInfo[1];
			this.lineInfo = new TMP_LineInfo[1];
			this.pageInfo = new TMP_PageInfo[1];
			this.linkInfo = new TMP_LinkInfo[0];
			this.materialCount = 0;
			this.meshInfo = new TMP_MeshInfo[1];
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00022AA0 File Offset: 0x00020CA0
		public void ClearMeshInfo(bool updateMesh)
		{
			for (int i = 0; i < this.meshInfo.Length; i++)
			{
				this.meshInfo[i].Clear(updateMesh);
			}
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x00022AD4 File Offset: 0x00020CD4
		public void ClearAllMeshInfo()
		{
			for (int i = 0; i < this.meshInfo.Length; i++)
			{
				this.meshInfo[i].Clear(true);
			}
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00022B08 File Offset: 0x00020D08
		public void ResetVertexLayout(bool isVolumetric)
		{
			for (int i = 0; i < this.meshInfo.Length; i++)
			{
				this.meshInfo[i].ResizeMeshInfo(0, isVolumetric);
			}
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00022B3C File Offset: 0x00020D3C
		public void ClearUnusedVertices(MaterialReference[] materials)
		{
			for (int i = 0; i < this.meshInfo.Length; i++)
			{
				int num = 0;
				this.meshInfo[i].ClearUnusedVertices(num);
			}
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00022B70 File Offset: 0x00020D70
		internal void ClearLineInfo()
		{
			if (this.lineInfo == null)
			{
				this.lineInfo = new TMP_LineInfo[1];
			}
			int num = this.lineInfo.Length;
			for (int i = 0; i < num; i++)
			{
				this.lineInfo[i].characterCount = 0;
				this.lineInfo[i].spaceCount = 0;
				this.lineInfo[i].wordCount = 0;
				this.lineInfo[i].controlCharacterCount = 0;
				this.lineInfo[i].visibleCharacterCount = 0;
				this.lineInfo[i].visibleSpaceCount = 0;
				this.lineInfo[i].ascender = TMP_TextInfo.k_InfinityVectorNegative.x;
				this.lineInfo[i].baseline = 0f;
				this.lineInfo[i].descender = TMP_TextInfo.k_InfinityVectorPositive.x;
				this.lineInfo[i].maxAdvance = 0f;
				this.lineInfo[i].marginLeft = 0f;
				this.lineInfo[i].marginRight = 0f;
				this.lineInfo[i].lineExtents.min = TMP_TextInfo.k_InfinityVectorPositive;
				this.lineInfo[i].lineExtents.max = TMP_TextInfo.k_InfinityVectorNegative;
				this.lineInfo[i].width = 0f;
			}
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00022CF4 File Offset: 0x00020EF4
		internal void ClearPageInfo()
		{
			if (this.pageInfo == null)
			{
				this.pageInfo = new TMP_PageInfo[2];
			}
			int num = this.pageInfo.Length;
			for (int i = 0; i < num; i++)
			{
				this.pageInfo[i].firstCharacterIndex = 0;
				this.pageInfo[i].lastCharacterIndex = 0;
				this.pageInfo[i].ascender = -32767f;
				this.pageInfo[i].baseLine = 0f;
				this.pageInfo[i].descender = 32767f;
			}
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00022D90 File Offset: 0x00020F90
		public TMP_MeshInfo[] CopyMeshInfoVertexData()
		{
			if (this.m_CachedMeshInfo == null || this.m_CachedMeshInfo.Length != this.meshInfo.Length)
			{
				this.m_CachedMeshInfo = new TMP_MeshInfo[this.meshInfo.Length];
				for (int i = 0; i < this.m_CachedMeshInfo.Length; i++)
				{
					int num = this.meshInfo[i].vertices.Length;
					this.m_CachedMeshInfo[i].vertices = new Vector3[num];
					this.m_CachedMeshInfo[i].uvs0 = new Vector4[num];
					this.m_CachedMeshInfo[i].uvs2 = new Vector2[num];
					this.m_CachedMeshInfo[i].colors32 = new Color32[num];
				}
			}
			for (int j = 0; j < this.m_CachedMeshInfo.Length; j++)
			{
				int num2 = this.meshInfo[j].vertices.Length;
				if (this.m_CachedMeshInfo[j].vertices.Length != num2)
				{
					this.m_CachedMeshInfo[j].vertices = new Vector3[num2];
					this.m_CachedMeshInfo[j].uvs0 = new Vector4[num2];
					this.m_CachedMeshInfo[j].uvs2 = new Vector2[num2];
					this.m_CachedMeshInfo[j].colors32 = new Color32[num2];
				}
				Array.Copy(this.meshInfo[j].vertices, this.m_CachedMeshInfo[j].vertices, num2);
				Array.Copy(this.meshInfo[j].uvs0, this.m_CachedMeshInfo[j].uvs0, num2);
				Array.Copy(this.meshInfo[j].uvs2, this.m_CachedMeshInfo[j].uvs2, num2);
				Array.Copy(this.meshInfo[j].colors32, this.m_CachedMeshInfo[j].colors32, num2);
			}
			return this.m_CachedMeshInfo;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00022F98 File Offset: 0x00021198
		public static void Resize<T>(ref T[] array, int size)
		{
			int num = ((size > 1024) ? (size + 256) : Mathf.NextPowerOfTwo(size));
			Array.Resize<T>(ref array, num);
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00022FC4 File Offset: 0x000211C4
		public static void Resize<T>(ref T[] array, int size, bool isBlockAllocated)
		{
			if (isBlockAllocated)
			{
				size = ((size > 1024) ? (size + 256) : Mathf.NextPowerOfTwo(size));
			}
			if (size == array.Length)
			{
				return;
			}
			Array.Resize<T>(ref array, size);
		}

		// Token: 0x0400049E RID: 1182
		internal static Vector2 k_InfinityVectorPositive = new Vector2(32767f, 32767f);

		// Token: 0x0400049F RID: 1183
		internal static Vector2 k_InfinityVectorNegative = new Vector2(-32767f, -32767f);

		// Token: 0x040004A0 RID: 1184
		public TMP_Text textComponent;

		// Token: 0x040004A1 RID: 1185
		public int characterCount;

		// Token: 0x040004A2 RID: 1186
		public int spriteCount;

		// Token: 0x040004A3 RID: 1187
		public int spaceCount;

		// Token: 0x040004A4 RID: 1188
		public int wordCount;

		// Token: 0x040004A5 RID: 1189
		public int linkCount;

		// Token: 0x040004A6 RID: 1190
		public int lineCount;

		// Token: 0x040004A7 RID: 1191
		public int pageCount;

		// Token: 0x040004A8 RID: 1192
		public int materialCount;

		// Token: 0x040004A9 RID: 1193
		public TMP_CharacterInfo[] characterInfo;

		// Token: 0x040004AA RID: 1194
		public TMP_WordInfo[] wordInfo;

		// Token: 0x040004AB RID: 1195
		public TMP_LinkInfo[] linkInfo;

		// Token: 0x040004AC RID: 1196
		public TMP_LineInfo[] lineInfo;

		// Token: 0x040004AD RID: 1197
		public TMP_PageInfo[] pageInfo;

		// Token: 0x040004AE RID: 1198
		public TMP_MeshInfo[] meshInfo;

		// Token: 0x040004AF RID: 1199
		private TMP_MeshInfo[] m_CachedMeshInfo;
	}
}
