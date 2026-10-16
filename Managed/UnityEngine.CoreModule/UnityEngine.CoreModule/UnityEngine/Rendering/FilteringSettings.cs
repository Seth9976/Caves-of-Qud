using System;
using UnityEngine.Internal;

namespace UnityEngine.Rendering
{
	// Token: 0x0200053C RID: 1340
	public struct FilteringSettings : IEquatable<FilteringSettings>
	{
		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x06003352 RID: 13138 RVA: 0x000645FE File Offset: 0x000627FE
		public static FilteringSettings defaultValue
		{
			get
			{
				return new FilteringSettings(new RenderQueueRange?(RenderQueueRange.all), -1, uint.MaxValue, 0);
			}
		}

		// Token: 0x06003353 RID: 13139 RVA: 0x00064614 File Offset: 0x00062814
		public FilteringSettings([DefaultValue("RenderQueueRange.all")] RenderQueueRange? renderQueueRange = null, int layerMask = -1, uint renderingLayerMask = 4294967295U, int excludeMotionVectorObjects = 0)
		{
			this = default(FilteringSettings);
			this.m_RenderQueueRange = renderQueueRange ?? RenderQueueRange.all;
			this.m_LayerMask = layerMask;
			this.m_RenderingLayerMask = renderingLayerMask;
			this.m_BatchLayerMask = uint.MaxValue;
			this.m_ExcludeMotionVectorObjects = excludeMotionVectorObjects;
			this.m_ForceAllMotionVectorObjects = 0;
			this.m_SortingLayerRange = SortingLayerRange.all;
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x06003354 RID: 13140 RVA: 0x00064678 File Offset: 0x00062878
		// (set) Token: 0x06003355 RID: 13141 RVA: 0x00064690 File Offset: 0x00062890
		public RenderQueueRange renderQueueRange
		{
			get
			{
				return this.m_RenderQueueRange;
			}
			set
			{
				this.m_RenderQueueRange = value;
			}
		}

		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06003356 RID: 13142 RVA: 0x0006469C File Offset: 0x0006289C
		// (set) Token: 0x06003357 RID: 13143 RVA: 0x000646B4 File Offset: 0x000628B4
		public int layerMask
		{
			get
			{
				return this.m_LayerMask;
			}
			set
			{
				this.m_LayerMask = value;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06003358 RID: 13144 RVA: 0x000646C0 File Offset: 0x000628C0
		// (set) Token: 0x06003359 RID: 13145 RVA: 0x000646D8 File Offset: 0x000628D8
		public uint renderingLayerMask
		{
			get
			{
				return this.m_RenderingLayerMask;
			}
			set
			{
				this.m_RenderingLayerMask = value;
			}
		}

		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x0600335A RID: 13146 RVA: 0x000646E4 File Offset: 0x000628E4
		// (set) Token: 0x0600335B RID: 13147 RVA: 0x000646FC File Offset: 0x000628FC
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

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x0600335C RID: 13148 RVA: 0x00064708 File Offset: 0x00062908
		// (set) Token: 0x0600335D RID: 13149 RVA: 0x00064723 File Offset: 0x00062923
		public bool excludeMotionVectorObjects
		{
			get
			{
				return this.m_ExcludeMotionVectorObjects != 0;
			}
			set
			{
				this.m_ExcludeMotionVectorObjects = (value ? 1 : 0);
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x0600335E RID: 13150 RVA: 0x00064734 File Offset: 0x00062934
		// (set) Token: 0x0600335F RID: 13151 RVA: 0x0006474F File Offset: 0x0006294F
		public bool forceAllMotionVectorObjects
		{
			get
			{
				return this.m_ForceAllMotionVectorObjects != 0;
			}
			set
			{
				this.m_ForceAllMotionVectorObjects = (value ? 1 : 0);
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06003360 RID: 13152 RVA: 0x00064760 File Offset: 0x00062960
		// (set) Token: 0x06003361 RID: 13153 RVA: 0x00064778 File Offset: 0x00062978
		public SortingLayerRange sortingLayerRange
		{
			get
			{
				return this.m_SortingLayerRange;
			}
			set
			{
				this.m_SortingLayerRange = value;
			}
		}

		// Token: 0x06003362 RID: 13154 RVA: 0x00064784 File Offset: 0x00062984
		public bool Equals(FilteringSettings other)
		{
			return this.m_RenderQueueRange.Equals(other.m_RenderQueueRange) && this.m_LayerMask == other.m_LayerMask && this.m_RenderingLayerMask == other.m_RenderingLayerMask && this.m_BatchLayerMask == other.m_BatchLayerMask && this.m_ExcludeMotionVectorObjects == other.m_ExcludeMotionVectorObjects && this.m_ForceAllMotionVectorObjects == other.m_ForceAllMotionVectorObjects;
		}

		// Token: 0x06003363 RID: 13155 RVA: 0x000647F4 File Offset: 0x000629F4
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is FilteringSettings && this.Equals((FilteringSettings)obj);
		}

		// Token: 0x06003364 RID: 13156 RVA: 0x0006482C File Offset: 0x00062A2C
		public override int GetHashCode()
		{
			int num = this.m_RenderQueueRange.GetHashCode();
			num = (num * 397) ^ this.m_LayerMask;
			num = (num * 397) ^ (int)this.m_RenderingLayerMask;
			num = (num * 397) ^ (int)this.m_BatchLayerMask;
			num = (num * 397) ^ this.m_ExcludeMotionVectorObjects;
			return (num * 397) ^ this.m_ForceAllMotionVectorObjects;
		}

		// Token: 0x06003365 RID: 13157 RVA: 0x000648A0 File Offset: 0x00062AA0
		public static bool operator ==(FilteringSettings left, FilteringSettings right)
		{
			return left.Equals(right);
		}

		// Token: 0x06003366 RID: 13158 RVA: 0x000648BC File Offset: 0x00062ABC
		public static bool operator !=(FilteringSettings left, FilteringSettings right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0400112D RID: 4397
		private RenderQueueRange m_RenderQueueRange;

		// Token: 0x0400112E RID: 4398
		private int m_LayerMask;

		// Token: 0x0400112F RID: 4399
		private uint m_RenderingLayerMask;

		// Token: 0x04001130 RID: 4400
		private uint m_BatchLayerMask;

		// Token: 0x04001131 RID: 4401
		private int m_ExcludeMotionVectorObjects;

		// Token: 0x04001132 RID: 4402
		private int m_ForceAllMotionVectorObjects;

		// Token: 0x04001133 RID: 4403
		private SortingLayerRange m_SortingLayerRange;
	}
}
