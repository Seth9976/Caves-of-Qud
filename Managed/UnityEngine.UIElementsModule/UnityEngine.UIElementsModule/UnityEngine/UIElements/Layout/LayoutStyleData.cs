using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.UIElements.Layout
{
	// Token: 0x020006CA RID: 1738
	[RequiredByNativeCode]
	[NativeHeader("Modules/UIElements/Core/Layout/Native/LayoutModel.h")]
	internal struct LayoutStyleData
	{
		// Token: 0x04001914 RID: 6420
		public static LayoutStyleData Default = new LayoutStyleData
		{
			Direction = LayoutDirection.Inherit,
			FlexDirection = LayoutFlexDirection.Column,
			JustifyContent = LayoutJustify.FlexStart,
			AlignContent = LayoutAlign.Auto,
			AlignItems = LayoutAlign.Stretch,
			AlignSelf = LayoutAlign.Auto,
			PositionType = LayoutPositionType.Relative,
			AspectRatio = float.NaN,
			FlexWrap = LayoutWrap.NoWrap,
			Overflow = LayoutOverflow.Visible,
			Display = LayoutDisplay.Flex,
			FlexGrow = float.NaN,
			FlexShrink = float.NaN,
			FlexBasis = LayoutValue.Auto(),
			border = LayoutDefaults.EdgeValuesUnit,
			position = LayoutDefaults.EdgeValuesUnit,
			margin = LayoutDefaults.EdgeValuesUnit,
			padding = LayoutDefaults.EdgeValuesUnit,
			dimensions = LayoutDefaults.DimensionValuesAutoUnit,
			minDimensions = LayoutDefaults.DimensionValuesUnit
		};

		// Token: 0x04001915 RID: 6421
		public LayoutDirection Direction;

		// Token: 0x04001916 RID: 6422
		public LayoutFlexDirection FlexDirection;

		// Token: 0x04001917 RID: 6423
		public LayoutJustify JustifyContent;

		// Token: 0x04001918 RID: 6424
		public LayoutAlign AlignContent;

		// Token: 0x04001919 RID: 6425
		public LayoutAlign AlignItems;

		// Token: 0x0400191A RID: 6426
		public LayoutAlign AlignSelf;

		// Token: 0x0400191B RID: 6427
		public LayoutPositionType PositionType;

		// Token: 0x0400191C RID: 6428
		public float AspectRatio;

		// Token: 0x0400191D RID: 6429
		public LayoutWrap FlexWrap;

		// Token: 0x0400191E RID: 6430
		public LayoutOverflow Overflow;

		// Token: 0x0400191F RID: 6431
		public LayoutDisplay Display;

		// Token: 0x04001920 RID: 6432
		public float FlexGrow;

		// Token: 0x04001921 RID: 6433
		public float FlexShrink;

		// Token: 0x04001922 RID: 6434
		public LayoutValue FlexBasis;

		// Token: 0x04001923 RID: 6435
		public FixedBuffer9<LayoutValue> border;

		// Token: 0x04001924 RID: 6436
		public FixedBuffer9<LayoutValue> position;

		// Token: 0x04001925 RID: 6437
		public FixedBuffer9<LayoutValue> margin;

		// Token: 0x04001926 RID: 6438
		public FixedBuffer9<LayoutValue> padding;

		// Token: 0x04001927 RID: 6439
		public FixedBuffer2<LayoutValue> maxDimensions;

		// Token: 0x04001928 RID: 6440
		public FixedBuffer2<LayoutValue> minDimensions;

		// Token: 0x04001929 RID: 6441
		public FixedBuffer2<LayoutValue> dimensions;
	}
}
