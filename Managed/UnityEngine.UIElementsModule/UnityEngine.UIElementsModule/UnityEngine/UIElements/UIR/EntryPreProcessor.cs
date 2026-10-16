using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000638 RID: 1592
	internal class EntryPreProcessor
	{
		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06002EC1 RID: 11969 RVA: 0x000A9845 File Offset: 0x000A7A45
		public int childrenIndex
		{
			get
			{
				return this.m_ChildrenIndex;
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06002EC2 RID: 11970 RVA: 0x000A984D File Offset: 0x000A7A4D
		public List<EntryPreProcessor.AllocSize> headAllocs
		{
			get
			{
				return this.m_HeadAllocs;
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06002EC3 RID: 11971 RVA: 0x000A9855 File Offset: 0x000A7A55
		public List<EntryPreProcessor.AllocSize> tailAllocs
		{
			get
			{
				return this.m_TailAllocs;
			}
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06002EC4 RID: 11972 RVA: 0x000A985D File Offset: 0x000A7A5D
		public List<Entry> flattenedEntries
		{
			get
			{
				return this.m_FlattenedEntries;
			}
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x000A9868 File Offset: 0x000A7A68
		public void PreProcess(Entry root)
		{
			this.m_ChildrenIndex = -1;
			this.m_FlattenedEntries.Clear();
			this.m_HeadAllocs.Clear();
			this.m_TailAllocs.Clear();
			this.m_Allocs = this.m_HeadAllocs;
			this.DoEvaluate(root);
			this.Flush();
			Debug.Assert(!this.m_IsPushingMask);
			Debug.Assert(this.m_Mask.Count == 0);
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x000A98DF File Offset: 0x000A7ADF
		public void ClearReferences()
		{
			this.m_FlattenedEntries.Clear();
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x000A98F0 File Offset: 0x000A7AF0
		private void DoEvaluate(Entry entry)
		{
			while (entry != null)
			{
				bool flag = entry.type != EntryType.DedicatedPlaceholder;
				if (flag)
				{
					this.m_FlattenedEntries.Add(entry);
				}
				switch (entry.type)
				{
				case EntryType.DrawSolidMesh:
				case EntryType.DrawTexturedMesh:
				case EntryType.DrawTexturedMeshSkipAtlas:
				case EntryType.DrawTextMesh:
				case EntryType.DrawGradients:
					Debug.Assert((long)entry.vertices.Length <= (long)((ulong)UIRenderDevice.maxVerticesPerPage));
					this.Add(entry.vertices.Length, entry.indices.Length);
					break;
				case EntryType.DrawImmediate:
				case EntryType.DrawImmediateCull:
				case EntryType.PushClippingRect:
				case EntryType.PopClippingRect:
				case EntryType.PushScissors:
				case EntryType.PopScissors:
				case EntryType.PushGroupMatrix:
				case EntryType.PopGroupMatrix:
				case EntryType.PushRenderTexture:
				case EntryType.BlitAndPopRenderTexture:
				case EntryType.PushDefaultMaterial:
				case EntryType.PopDefaultMaterial:
				case EntryType.CutRenderChain:
				case EntryType.DedicatedPlaceholder:
					break;
				case EntryType.DrawChildren:
					Debug.Assert(!this.m_IsPushingMask);
					Debug.Assert(this.m_ChildrenIndex == -1);
					this.Flush();
					this.m_ChildrenIndex = this.m_FlattenedEntries.Count - 1;
					this.m_Allocs = this.tailAllocs;
					break;
				case EntryType.BeginStencilMask:
					Debug.Assert(!this.m_IsPushingMask);
					this.m_IsPushingMask = true;
					break;
				case EntryType.EndStencilMask:
					Debug.Assert(this.m_IsPushingMask);
					this.m_IsPushingMask = false;
					break;
				case EntryType.PopStencilMask:
					for (;;)
					{
						EntryPreProcessor.AllocSize allocSize;
						bool flag2 = this.m_Mask.TryPop(out allocSize);
						if (!flag2)
						{
							break;
						}
						this.Add(allocSize.vertexCount, allocSize.indexCount);
					}
					break;
				default:
					throw new NotImplementedException();
				}
				bool flag3 = entry.firstChild != null;
				if (flag3)
				{
					this.DoEvaluate(entry.firstChild);
				}
				entry = entry.nextSibling;
			}
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x000A9AA8 File Offset: 0x000A7CA8
		private void Add(int vertexCount, int indexCount)
		{
			bool flag = vertexCount == 0 || indexCount == 0;
			if (!flag)
			{
				int num = this.m_Pending.vertexCount + vertexCount;
				bool flag2 = (long)num <= (long)((ulong)UIRenderDevice.maxVerticesPerPage);
				if (flag2)
				{
					this.m_Pending.vertexCount = num;
					this.m_Pending.indexCount = this.m_Pending.indexCount + indexCount;
				}
				else
				{
					this.Flush();
					this.m_Pending.vertexCount = vertexCount;
					this.m_Pending.indexCount = indexCount;
				}
				bool isPushingMask = this.m_IsPushingMask;
				if (isPushingMask)
				{
					this.m_Mask.Push(new EntryPreProcessor.AllocSize
					{
						vertexCount = vertexCount,
						indexCount = indexCount
					});
				}
			}
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x000A9B5C File Offset: 0x000A7D5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void Flush()
		{
			bool flag = this.m_Pending.vertexCount > 0;
			if (flag)
			{
				this.m_Allocs.Add(this.m_Pending);
				this.m_Pending = default(EntryPreProcessor.AllocSize);
			}
		}

		// Token: 0x040015BB RID: 5563
		private int m_ChildrenIndex;

		// Token: 0x040015BC RID: 5564
		private List<EntryPreProcessor.AllocSize> m_Allocs;

		// Token: 0x040015BD RID: 5565
		private List<EntryPreProcessor.AllocSize> m_HeadAllocs = new List<EntryPreProcessor.AllocSize>(1);

		// Token: 0x040015BE RID: 5566
		private List<EntryPreProcessor.AllocSize> m_TailAllocs = new List<EntryPreProcessor.AllocSize>(1);

		// Token: 0x040015BF RID: 5567
		private List<Entry> m_FlattenedEntries = new List<Entry>(8);

		// Token: 0x040015C0 RID: 5568
		private EntryPreProcessor.AllocSize m_Pending;

		// Token: 0x040015C1 RID: 5569
		private Stack<EntryPreProcessor.AllocSize> m_Mask = new Stack<EntryPreProcessor.AllocSize>(1);

		// Token: 0x040015C2 RID: 5570
		private bool m_IsPushingMask;

		// Token: 0x02000639 RID: 1593
		public struct AllocSize
		{
			// Token: 0x040015C3 RID: 5571
			public int vertexCount;

			// Token: 0x040015C4 RID: 5572
			public int indexCount;
		}
	}
}
