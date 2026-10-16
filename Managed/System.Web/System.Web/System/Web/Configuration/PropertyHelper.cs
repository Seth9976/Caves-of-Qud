using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020005CA RID: 1482
	internal static class PropertyHelper
	{
		// Token: 0x040022C5 RID: 8901
		internal static WhiteSpaceTrimStringConverter WhiteSpaceTrimStringConverter = new WhiteSpaceTrimStringConverter();

		// Token: 0x040022C6 RID: 8902
		internal static InfiniteTimeSpanConverter InfiniteTimeSpanConverter = new InfiniteTimeSpanConverter();

		// Token: 0x040022C7 RID: 8903
		internal static InfiniteIntConverter InfiniteIntConverter = new InfiniteIntConverter();

		// Token: 0x040022C8 RID: 8904
		internal static TimeSpanMinutesConverter TimeSpanMinutesConverter = new TimeSpanMinutesConverter();

		// Token: 0x040022C9 RID: 8905
		internal static TimeSpanSecondsOrInfiniteConverter TimeSpanSecondsOrInfiniteConverter = new TimeSpanSecondsOrInfiniteConverter();

		// Token: 0x040022CA RID: 8906
		internal static TimeSpanSecondsConverter TimeSpanSecondsConverter = new TimeSpanSecondsConverter();

		// Token: 0x040022CB RID: 8907
		internal static CommaDelimitedStringCollectionConverter CommaDelimitedStringCollectionConverter = new CommaDelimitedStringCollectionConverter();

		// Token: 0x040022CC RID: 8908
		internal static DefaultValidator DefaultValidator = new DefaultValidator();

		// Token: 0x040022CD RID: 8909
		internal static NullableStringValidator NonEmptyStringValidator = new NullableStringValidator(1);

		// Token: 0x040022CE RID: 8910
		internal static PositiveTimeSpanValidator PositiveTimeSpanValidator = new PositiveTimeSpanValidator();

		// Token: 0x040022CF RID: 8911
		internal static TimeSpanMinutesOrInfiniteConverter TimeSpanMinutesOrInfiniteConverter = new TimeSpanMinutesOrInfiniteConverter();

		// Token: 0x040022D0 RID: 8912
		internal static IntegerValidator IntFromZeroToMaxValidator = new IntegerValidator(0, int.MaxValue);

		// Token: 0x040022D1 RID: 8913
		internal static IntegerValidator IntFromOneToMax_1Validator = new IntegerValidator(1, 2147483646);

		// Token: 0x040022D2 RID: 8914
		internal static VersionConverter VersionConverter = new VersionConverter();
	}
}
