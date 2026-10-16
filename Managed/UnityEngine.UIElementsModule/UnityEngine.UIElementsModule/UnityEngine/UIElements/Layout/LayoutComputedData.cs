using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x0200069C RID: 1692
	internal struct LayoutComputedData
	{
		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x060031C2 RID: 12738 RVA: 0x000BBBE8 File Offset: 0x000B9DE8
		public unsafe static LayoutComputedData Default
		{
			get
			{
				LayoutComputedData layoutComputedData = new LayoutComputedData
				{
					Direction = LayoutDirection.Inherit,
					ComputedFlexBasisGeneration = 0U,
					ComputedFlexBasis = float.NaN,
					HadOverflow = false,
					GenerationCount = 0U,
					LastParentDirection = (LayoutDirection)(-1),
					LastPointScaleFactor = 1f
				};
				layoutComputedData.Dimensions.FixedElementField = LayoutDefaults.DimensionValues[0];
				*((ref layoutComputedData.Dimensions.FixedElementField) + 4) = LayoutDefaults.DimensionValues[1];
				layoutComputedData.MeasuredDimensions.FixedElementField = LayoutDefaults.DimensionValues[0];
				*((ref layoutComputedData.MeasuredDimensions.FixedElementField) + 4) = LayoutDefaults.DimensionValues[1];
				return layoutComputedData;
			}
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x060031C3 RID: 12739 RVA: 0x000BBC9C File Offset: 0x000B9E9C
		public unsafe float* MarginBuffer
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				fixed (float* ptr = &this.Margin.FixedElementField)
				{
					return ptr;
				}
			}
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x060031C4 RID: 12740 RVA: 0x000BBCC0 File Offset: 0x000B9EC0
		public unsafe float* BorderBuffer
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				fixed (float* ptr = &this.Border.FixedElementField)
				{
					return ptr;
				}
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x060031C5 RID: 12741 RVA: 0x000BBCE4 File Offset: 0x000B9EE4
		public unsafe float* PaddingBuffer
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				fixed (float* ptr = &this.Padding.FixedElementField)
				{
					return ptr;
				}
			}
		}

		// Token: 0x04001849 RID: 6217
		[FixedBuffer(typeof(float), 4)]
		public LayoutComputedData.<Position>e__FixedBuffer Position;

		// Token: 0x0400184A RID: 6218
		[FixedBuffer(typeof(float), 2)]
		public LayoutComputedData.<Dimensions>e__FixedBuffer Dimensions;

		// Token: 0x0400184B RID: 6219
		[FixedBuffer(typeof(float), 6)]
		public LayoutComputedData.<Margin>e__FixedBuffer Margin;

		// Token: 0x0400184C RID: 6220
		[FixedBuffer(typeof(float), 6)]
		public LayoutComputedData.<Border>e__FixedBuffer Border;

		// Token: 0x0400184D RID: 6221
		[FixedBuffer(typeof(float), 6)]
		public LayoutComputedData.<Padding>e__FixedBuffer Padding;

		// Token: 0x0400184E RID: 6222
		public LayoutDirection Direction;

		// Token: 0x0400184F RID: 6223
		public uint ComputedFlexBasisGeneration;

		// Token: 0x04001850 RID: 6224
		public float ComputedFlexBasis;

		// Token: 0x04001851 RID: 6225
		public bool HadOverflow;

		// Token: 0x04001852 RID: 6226
		public uint GenerationCount;

		// Token: 0x04001853 RID: 6227
		public LayoutDirection LastParentDirection;

		// Token: 0x04001854 RID: 6228
		public float LastPointScaleFactor;

		// Token: 0x04001855 RID: 6229
		[FixedBuffer(typeof(float), 2)]
		public LayoutComputedData.<MeasuredDimensions>e__FixedBuffer MeasuredDimensions;

		// Token: 0x0200069D RID: 1693
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 24)]
		public struct <Border>e__FixedBuffer
		{
			// Token: 0x04001856 RID: 6230
			public float FixedElementField;
		}

		// Token: 0x0200069E RID: 1694
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 8)]
		public struct <Dimensions>e__FixedBuffer
		{
			// Token: 0x04001857 RID: 6231
			public float FixedElementField;
		}

		// Token: 0x0200069F RID: 1695
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 24)]
		public struct <Margin>e__FixedBuffer
		{
			// Token: 0x04001858 RID: 6232
			public float FixedElementField;
		}

		// Token: 0x020006A0 RID: 1696
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 8)]
		public struct <MeasuredDimensions>e__FixedBuffer
		{
			// Token: 0x04001859 RID: 6233
			public float FixedElementField;
		}

		// Token: 0x020006A1 RID: 1697
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 24)]
		public struct <Padding>e__FixedBuffer
		{
			// Token: 0x0400185A RID: 6234
			public float FixedElementField;
		}

		// Token: 0x020006A2 RID: 1698
		[UnsafeValueType]
		[CompilerGenerated]
		[StructLayout(LayoutKind.Sequential, Size = 16)]
		public struct <Position>e__FixedBuffer
		{
			// Token: 0x0400185B RID: 6235
			public float FixedElementField;
		}
	}
}
