using System;
using System.Globalization;

namespace System
{
	// Token: 0x0200011C RID: 284
	public abstract class FormattableString : IFormattable
	{
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000AE6 RID: 2790
		public abstract string Format { get; }

		// Token: 0x06000AE7 RID: 2791
		public abstract object[] GetArguments();

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000AE8 RID: 2792
		public abstract int ArgumentCount { get; }

		// Token: 0x06000AE9 RID: 2793
		public abstract object GetArgument(int index);

		// Token: 0x06000AEA RID: 2794
		public abstract string ToString(IFormatProvider formatProvider);

		// Token: 0x06000AEB RID: 2795 RVA: 0x000288E6 File Offset: 0x00026AE6
		string IFormattable.ToString(string ignored, IFormatProvider formatProvider)
		{
			return this.ToString(formatProvider);
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x000288EF File Offset: 0x00026AEF
		public static string Invariant(FormattableString formattable)
		{
			if (formattable == null)
			{
				throw new ArgumentNullException("formattable");
			}
			return formattable.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0002890A File Offset: 0x00026B0A
		public override string ToString()
		{
			return this.ToString(CultureInfo.CurrentCulture);
		}
	}
}
