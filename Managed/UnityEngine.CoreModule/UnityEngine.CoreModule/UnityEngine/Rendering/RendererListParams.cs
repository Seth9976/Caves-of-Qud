using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.Rendering
{
	// Token: 0x02000547 RID: 1351
	public struct RendererListParams : IEquatable<RendererListParams>
	{
		// Token: 0x06003399 RID: 13209 RVA: 0x00064E58 File Offset: 0x00063058
		public RendererListParams(CullingResults cullingResults, DrawingSettings drawSettings, FilteringSettings filteringSettings)
		{
			this.cullingResults = cullingResults;
			this.drawSettings = drawSettings;
			this.filteringSettings = filteringSettings;
			this.tagName = ShaderTagId.none;
			this.isPassTagName = false;
			this.tagValues = null;
			this.stateBlocks = null;
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x0600339A RID: 13210 RVA: 0x00064EA8 File Offset: 0x000630A8
		internal int numStateBlocks
		{
			get
			{
				bool flag = this.tagValues != null;
				int num;
				if (flag)
				{
					num = this.tagValues.Value.Length;
				}
				else
				{
					num = 0;
				}
				return num;
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x0600339B RID: 13211 RVA: 0x00064EE0 File Offset: 0x000630E0
		internal IntPtr stateBlocksPtr
		{
			get
			{
				bool flag = this.stateBlocks == null;
				IntPtr intPtr;
				if (flag)
				{
					intPtr = IntPtr.Zero;
				}
				else
				{
					intPtr = (IntPtr)this.stateBlocks.Value.GetUnsafeReadOnlyPtr<RenderStateBlock>();
				}
				return intPtr;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x0600339C RID: 13212 RVA: 0x00064F24 File Offset: 0x00063124
		internal IntPtr tagsValuePtr
		{
			get
			{
				bool flag = this.tagValues == null;
				IntPtr intPtr;
				if (flag)
				{
					intPtr = IntPtr.Zero;
				}
				else
				{
					intPtr = (IntPtr)this.tagValues.Value.GetUnsafeReadOnlyPtr<ShaderTagId>();
				}
				return intPtr;
			}
		}

		// Token: 0x0600339D RID: 13213 RVA: 0x00064F68 File Offset: 0x00063168
		internal void Dispose()
		{
			bool flag = this.stateBlocks != null;
			if (flag)
			{
				this.stateBlocks.Value.Dispose();
				this.stateBlocks = null;
			}
			bool flag2 = this.tagValues != null;
			if (flag2)
			{
				this.tagValues.Value.Dispose();
				this.tagValues = null;
			}
		}

		// Token: 0x0600339E RID: 13214 RVA: 0x00064FD8 File Offset: 0x000631D8
		internal void Validate()
		{
			bool flag = this.tagValues != null && this.stateBlocks != null;
			if (flag)
			{
				bool flag2 = this.tagValues.Value.Length != this.stateBlocks.Value.Length;
				if (flag2)
				{
					throw new ArgumentException(string.Format("Arrays {0} and {1} should have same length, but {2} had length {3} while {4} had length {5}.", new object[]
					{
						"tagValues",
						"stateBlocks",
						"tagValues",
						this.tagValues.Value.Length,
						"stateBlocks",
						this.stateBlocks.Value.Length
					}));
				}
			}
			else
			{
				bool flag3 = (this.tagValues != null && this.stateBlocks == null) || (this.tagValues == null && this.stateBlocks != null);
				if (flag3)
				{
					throw new ArgumentException(string.Format("Arrays {0} and {1} should have same length, but one of them is null ({2} : {3}, {4} : {5}).", new object[]
					{
						"tagValues",
						"stateBlocks",
						"tagValues",
						this.tagValues != null,
						"stateBlocks",
						this.stateBlocks != null
					}));
				}
			}
		}

		// Token: 0x0600339F RID: 13215 RVA: 0x00065148 File Offset: 0x00063348
		public bool Equals(RendererListParams other)
		{
			return this.cullingResults == other.cullingResults && this.drawSettings == other.drawSettings && this.filteringSettings == other.filteringSettings && this.tagName == other.tagName && this.isPassTagName == other.isPassTagName && this.tagValues == other.tagValues && this.stateBlocks == other.stateBlocks;
		}

		// Token: 0x060033A0 RID: 13216 RVA: 0x0006524C File Offset: 0x0006344C
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is RendererListParams && this.Equals((RendererListParams)obj);
		}

		// Token: 0x060033A1 RID: 13217 RVA: 0x00065284 File Offset: 0x00063484
		public override int GetHashCode()
		{
			int num = this.cullingResults.GetHashCode();
			num = (num * 397) ^ this.drawSettings.GetHashCode();
			num = (num * 397) ^ this.filteringSettings.GetHashCode();
			num = (num * 397) ^ this.tagName.GetHashCode();
			num = (num * 397) ^ (this.isPassTagName ? 0 : 1);
			num = (num * 397) ^ this.tagValues.GetHashCode();
			return (num * 397) ^ this.stateBlocks.GetHashCode();
		}

		// Token: 0x060033A2 RID: 13218 RVA: 0x00065344 File Offset: 0x00063544
		public static bool operator ==(RendererListParams left, RendererListParams right)
		{
			return left.Equals(right);
		}

		// Token: 0x060033A3 RID: 13219 RVA: 0x00065360 File Offset: 0x00063560
		public static bool operator !=(RendererListParams left, RendererListParams right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04001167 RID: 4455
		public static readonly RendererListParams Invalid = default(RendererListParams);

		// Token: 0x04001168 RID: 4456
		public CullingResults cullingResults;

		// Token: 0x04001169 RID: 4457
		public DrawingSettings drawSettings;

		// Token: 0x0400116A RID: 4458
		public FilteringSettings filteringSettings;

		// Token: 0x0400116B RID: 4459
		public ShaderTagId tagName;

		// Token: 0x0400116C RID: 4460
		public bool isPassTagName;

		// Token: 0x0400116D RID: 4461
		public NativeArray<ShaderTagId>? tagValues;

		// Token: 0x0400116E RID: 4462
		public NativeArray<RenderStateBlock>? stateBlocks;
	}
}
