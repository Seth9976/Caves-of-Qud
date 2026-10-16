using System;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Internal;

namespace Microsoft.CodeAnalysis.Collections.Internal
{
	// Token: 0x020004BA RID: 1210
	internal static class SR
	{
		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x06003065 RID: 12389 RVA: 0x0008A3EC File Offset: 0x000885EC
		internal static ResourceManager ResourceManager
		{
			get
			{
				ResourceManager resourceManager;
				if ((resourceManager = SR.s_resourceManager) == null)
				{
					resourceManager = (SR.s_resourceManager = new ResourceManager(typeof(Strings)));
				}
				return resourceManager;
			}
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x06003066 RID: 12390 RVA: 0x0008A40C File Offset: 0x0008860C
		// (set) Token: 0x06003067 RID: 12391 RVA: 0x0008A413 File Offset: 0x00088613
		internal static CultureInfo Culture { get; set; }

		// Token: 0x06003068 RID: 12392 RVA: 0x0008A41B File Offset: 0x0008861B
		[MethodImpl(256)]
		internal static string GetResourceString(string resourceKey, string defaultValue = null)
		{
			return SR.ResourceManager.GetString(resourceKey, SR.Culture);
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x06003069 RID: 12393 RVA: 0x0008A42D File Offset: 0x0008862D
		internal static string Arg_ArrayPlusOffTooSmall
		{
			get
			{
				return SR.GetResourceString("Arg_ArrayPlusOffTooSmall", null);
			}
		}

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x0600306A RID: 12394 RVA: 0x0008A43A File Offset: 0x0008863A
		internal static string Arg_HTCapacityOverflow
		{
			get
			{
				return SR.GetResourceString("Arg_HTCapacityOverflow", null);
			}
		}

		// Token: 0x17000DCF RID: 3535
		// (get) Token: 0x0600306B RID: 12395 RVA: 0x0008A447 File Offset: 0x00088647
		internal static string Arg_KeyNotFoundWithKey
		{
			get
			{
				return SR.GetResourceString("Arg_KeyNotFoundWithKey", null);
			}
		}

		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x0600306C RID: 12396 RVA: 0x0008A454 File Offset: 0x00088654
		internal static string Arg_LongerThanDestArray
		{
			get
			{
				return SR.GetResourceString("Arg_LongerThanDestArray", null);
			}
		}

		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x0600306D RID: 12397 RVA: 0x0008A461 File Offset: 0x00088661
		internal static string Arg_LongerThanSrcArray
		{
			get
			{
				return SR.GetResourceString("Arg_LongerThanSrcArray", null);
			}
		}

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x0600306E RID: 12398 RVA: 0x0008A46E File Offset: 0x0008866E
		internal static string Arg_NonZeroLowerBound
		{
			get
			{
				return SR.GetResourceString("Arg_NonZeroLowerBound", null);
			}
		}

		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x0600306F RID: 12399 RVA: 0x0008A47B File Offset: 0x0008867B
		internal static string Arg_RankMultiDimNotSupported
		{
			get
			{
				return SR.GetResourceString("Arg_RankMultiDimNotSupported", null);
			}
		}

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x06003070 RID: 12400 RVA: 0x0008A488 File Offset: 0x00088688
		internal static string Arg_WrongType
		{
			get
			{
				return SR.GetResourceString("Arg_WrongType", null);
			}
		}

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x06003071 RID: 12401 RVA: 0x0008A495 File Offset: 0x00088695
		internal static string Argument_AddingDuplicateWithKey
		{
			get
			{
				return SR.GetResourceString("Argument_AddingDuplicateWithKey", null);
			}
		}

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x06003072 RID: 12402 RVA: 0x0008A4A2 File Offset: 0x000886A2
		internal static string Argument_InvalidArrayType
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidArrayType", null);
			}
		}

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x06003073 RID: 12403 RVA: 0x0008A4AF File Offset: 0x000886AF
		internal static string Argument_InvalidOffLen
		{
			get
			{
				return SR.GetResourceString("Argument_InvalidOffLen", null);
			}
		}

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x06003074 RID: 12404 RVA: 0x0008A4BC File Offset: 0x000886BC
		internal static string ArgumentOutOfRange_ArrayLB
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_ArrayLB", null);
			}
		}

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x06003075 RID: 12405 RVA: 0x0008A4C9 File Offset: 0x000886C9
		internal static string ArgumentOutOfRange_BiggerThanCollection
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_BiggerThanCollection", null);
			}
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x06003076 RID: 12406 RVA: 0x0008A4D6 File Offset: 0x000886D6
		internal static string ArgumentOutOfRange_Count
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_Count", null);
			}
		}

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x06003077 RID: 12407 RVA: 0x0008A4E3 File Offset: 0x000886E3
		internal static string ArgumentOutOfRange_Index
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_Index", null);
			}
		}

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x06003078 RID: 12408 RVA: 0x0008A4F0 File Offset: 0x000886F0
		internal static string ArgumentOutOfRange_ListInsert
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_ListInsert", null);
			}
		}

		// Token: 0x17000DDD RID: 3549
		// (get) Token: 0x06003079 RID: 12409 RVA: 0x0008A4FD File Offset: 0x000886FD
		internal static string ArgumentOutOfRange_NeedNonNegNum
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_NeedNonNegNum", null);
			}
		}

		// Token: 0x17000DDE RID: 3550
		// (get) Token: 0x0600307A RID: 12410 RVA: 0x0008A50A File Offset: 0x0008870A
		internal static string ArgumentOutOfRange_SmallCapacity
		{
			get
			{
				return SR.GetResourceString("ArgumentOutOfRange_SmallCapacity", null);
			}
		}

		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x0600307B RID: 12411 RVA: 0x0008A517 File Offset: 0x00088717
		internal static string InvalidOperation_ConcurrentOperationsNotSupported
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_ConcurrentOperationsNotSupported", null);
			}
		}

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x0600307C RID: 12412 RVA: 0x0008A524 File Offset: 0x00088724
		internal static string InvalidOperation_EnumFailedVersion
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_EnumFailedVersion", null);
			}
		}

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x0600307D RID: 12413 RVA: 0x0008A531 File Offset: 0x00088731
		internal static string InvalidOperation_EnumOpCantHappen
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_EnumOpCantHappen", null);
			}
		}

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x0600307E RID: 12414 RVA: 0x0008A53E File Offset: 0x0008873E
		internal static string InvalidOperation_IComparerFailed
		{
			get
			{
				return SR.GetResourceString("InvalidOperation_IComparerFailed", null);
			}
		}

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x0600307F RID: 12415 RVA: 0x0008A54B File Offset: 0x0008874B
		internal static string NotSupported_KeyCollectionSet
		{
			get
			{
				return SR.GetResourceString("NotSupported_KeyCollectionSet", null);
			}
		}

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x06003080 RID: 12416 RVA: 0x0008A558 File Offset: 0x00088758
		internal static string NotSupported_ValueCollectionSet
		{
			get
			{
				return SR.GetResourceString("NotSupported_ValueCollectionSet", null);
			}
		}

		// Token: 0x17000DE5 RID: 3557
		// (get) Token: 0x06003081 RID: 12417 RVA: 0x0008A565 File Offset: 0x00088765
		internal static string Rank_MustMatch
		{
			get
			{
				return SR.GetResourceString("Rank_MustMatch", null);
			}
		}

		// Token: 0x17000DE6 RID: 3558
		// (get) Token: 0x06003082 RID: 12418 RVA: 0x0008A572 File Offset: 0x00088772
		internal static string NotSupported_FixedSizeCollection
		{
			get
			{
				return SR.GetResourceString("NotSupported_FixedSizeCollection", null);
			}
		}

		// Token: 0x17000DE7 RID: 3559
		// (get) Token: 0x06003083 RID: 12419 RVA: 0x0008A57F File Offset: 0x0008877F
		internal static string ArgumentException_OtherNotArrayOfCorrectLength
		{
			get
			{
				return SR.GetResourceString("ArgumentException_OtherNotArrayOfCorrectLength", null);
			}
		}

		// Token: 0x17000DE8 RID: 3560
		// (get) Token: 0x06003084 RID: 12420 RVA: 0x0008A58C File Offset: 0x0008878C
		internal static string Arg_BogusIComparer
		{
			get
			{
				return SR.GetResourceString("Arg_BogusIComparer", null);
			}
		}

		// Token: 0x040012CE RID: 4814
		private static ResourceManager s_resourceManager;
	}
}
