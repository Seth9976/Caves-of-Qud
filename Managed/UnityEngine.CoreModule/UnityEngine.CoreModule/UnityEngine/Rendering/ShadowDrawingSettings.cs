using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000558 RID: 1368
	[UsedByNativeCode]
	public struct ShadowDrawingSettings : IEquatable<ShadowDrawingSettings>
	{
		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x060034C9 RID: 13513 RVA: 0x000679A8 File Offset: 0x00065BA8
		// (set) Token: 0x060034CA RID: 13514 RVA: 0x000679C0 File Offset: 0x00065BC0
		public CullingResults cullingResults
		{
			get
			{
				return this.m_CullingResults;
			}
			set
			{
				this.m_CullingResults = value;
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x060034CB RID: 13515 RVA: 0x000679CC File Offset: 0x00065BCC
		// (set) Token: 0x060034CC RID: 13516 RVA: 0x000679E4 File Offset: 0x00065BE4
		public int lightIndex
		{
			get
			{
				return this.m_LightIndex;
			}
			set
			{
				this.m_LightIndex = value;
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x060034CD RID: 13517 RVA: 0x000679F0 File Offset: 0x00065BF0
		// (set) Token: 0x060034CE RID: 13518 RVA: 0x00067A08 File Offset: 0x00065C08
		public int splitIndex
		{
			get
			{
				return this.m_SplitIndex;
			}
			set
			{
				this.m_SplitIndex = value;
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x060034CF RID: 13519 RVA: 0x00067A14 File Offset: 0x00065C14
		// (set) Token: 0x060034D0 RID: 13520 RVA: 0x00067A2F File Offset: 0x00065C2F
		public bool useRenderingLayerMaskTest
		{
			get
			{
				return this.m_UseRenderingLayerMaskTest != 0;
			}
			set
			{
				this.m_UseRenderingLayerMaskTest = (value ? 1 : 0);
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x060034D1 RID: 13521 RVA: 0x00067A40 File Offset: 0x00065C40
		// (set) Token: 0x060034D2 RID: 13522 RVA: 0x00067A58 File Offset: 0x00065C58
		public uint batchLayerMask
		{
			get
			{
				return this.m_BatchLayerMask;
			}
			set
			{
				this.m_BatchLayerMask = value;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x060034D3 RID: 13523 RVA: 0x00067A64 File Offset: 0x00065C64
		// (set) Token: 0x060034D4 RID: 13524 RVA: 0x00067A7C File Offset: 0x00065C7C
		[Obsolete("ShadowDrawingSettings.splitData is deprecated. The equivalent data must be passed to ScriptableRenderContext.CullShadowCasters.")]
		public ShadowSplitData splitData
		{
			get
			{
				return this.m_SplitData;
			}
			set
			{
				this.m_SplitData = value;
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x060034D5 RID: 13525 RVA: 0x00067A88 File Offset: 0x00065C88
		// (set) Token: 0x060034D6 RID: 13526 RVA: 0x00067AA0 File Offset: 0x00065CA0
		public ShadowObjectsFilter objectsFilter
		{
			get
			{
				return this.m_ObjectsFilter;
			}
			set
			{
				this.m_ObjectsFilter = value;
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x060034D7 RID: 13527 RVA: 0x00067AAC File Offset: 0x00065CAC
		// (set) Token: 0x060034D8 RID: 13528 RVA: 0x00067AC4 File Offset: 0x00065CC4
		[Obsolete("ShadowDrawingSettings.projectionType is deprecated. There is no replacement for this parameter. You don't need to set it anymore.")]
		public BatchCullingProjectionType projectionType
		{
			get
			{
				return this.m_ProjectionType;
			}
			set
			{
				this.m_ProjectionType = value;
			}
		}

		// Token: 0x060034D9 RID: 13529 RVA: 0x00067AD0 File Offset: 0x00065CD0
		public ShadowDrawingSettings(CullingResults cullingResults, int lightIndex)
		{
			this.m_CullingResults = cullingResults;
			this.m_LightIndex = lightIndex;
			this.m_SplitIndex = -1;
			this.m_UseRenderingLayerMaskTest = 0;
			this.m_BatchLayerMask = uint.MaxValue;
			this.m_SplitData = default(ShadowSplitData);
			this.m_SplitData.shadowCascadeBlendCullingFactor = 1f;
			this.m_ObjectsFilter = ShadowObjectsFilter.AllObjects;
			this.m_ProjectionType = BatchCullingProjectionType.Unknown;
		}

		// Token: 0x060034DA RID: 13530 RVA: 0x00067B2C File Offset: 0x00065D2C
		[Obsolete("ShadowDrawingSettings(CullingResults, int, BatchCullingProjectionType) is deprecated. Use ShadowDrawingSettings(CullingResults, int) instead.")]
		public ShadowDrawingSettings(CullingResults cullingResults, int lightIndex, BatchCullingProjectionType projectionType)
		{
			this = new ShadowDrawingSettings(cullingResults, lightIndex);
			this.m_ProjectionType = projectionType;
		}

		// Token: 0x060034DB RID: 13531 RVA: 0x00067B40 File Offset: 0x00065D40
		public bool Equals(ShadowDrawingSettings other)
		{
			return this.m_CullingResults.Equals(other.m_CullingResults) && this.m_LightIndex == other.m_LightIndex && this.m_SplitIndex == other.m_SplitIndex && this.m_SplitData.Equals(other.m_SplitData) && this.m_UseRenderingLayerMaskTest.Equals(other.m_UseRenderingLayerMaskTest) && this.m_BatchLayerMask == other.m_BatchLayerMask && this.m_ObjectsFilter.Equals(other.m_ObjectsFilter);
		}

		// Token: 0x060034DC RID: 13532 RVA: 0x00067BD4 File Offset: 0x00065DD4
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is ShadowDrawingSettings && this.Equals((ShadowDrawingSettings)obj);
		}

		// Token: 0x060034DD RID: 13533 RVA: 0x00067C0C File Offset: 0x00065E0C
		public override int GetHashCode()
		{
			int num = this.m_CullingResults.GetHashCode();
			num = (num * 397) ^ this.m_LightIndex;
			num = (num * 397) ^ this.m_SplitIndex;
			num = (num * 397) ^ this.m_UseRenderingLayerMaskTest;
			num = (num * 397) ^ (int)this.m_BatchLayerMask;
			num = (num * 397) ^ this.m_SplitData.GetHashCode();
			return (num * 397) ^ (int)this.m_ObjectsFilter;
		}

		// Token: 0x060034DE RID: 13534 RVA: 0x00067C98 File Offset: 0x00065E98
		public static bool operator ==(ShadowDrawingSettings left, ShadowDrawingSettings right)
		{
			return left.Equals(right);
		}

		// Token: 0x060034DF RID: 13535 RVA: 0x00067CB4 File Offset: 0x00065EB4
		public static bool operator !=(ShadowDrawingSettings left, ShadowDrawingSettings right)
		{
			return !left.Equals(right);
		}

		// Token: 0x040011B0 RID: 4528
		private CullingResults m_CullingResults;

		// Token: 0x040011B1 RID: 4529
		private int m_LightIndex;

		// Token: 0x040011B2 RID: 4530
		private int m_SplitIndex;

		// Token: 0x040011B3 RID: 4531
		private int m_UseRenderingLayerMaskTest;

		// Token: 0x040011B4 RID: 4532
		private uint m_BatchLayerMask;

		// Token: 0x040011B5 RID: 4533
		private ShadowSplitData m_SplitData;

		// Token: 0x040011B6 RID: 4534
		private ShadowObjectsFilter m_ObjectsFilter;

		// Token: 0x040011B7 RID: 4535
		private BatchCullingProjectionType m_ProjectionType;
	}
}
