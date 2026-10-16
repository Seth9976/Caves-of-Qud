using System;
using System.Collections;
using System.Globalization;
using System.IO;

namespace NUnit.Framework.Constraints
{
	// Token: 0x02000117 RID: 279
	public abstract class MessageWriter : StringWriter
	{
		// Token: 0x060008C6 RID: 2246 RVA: 0x000143B7 File Offset: 0x000125B7
		protected MessageWriter()
			: base(CultureInfo.InvariantCulture)
		{
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060008C7 RID: 2247
		// (set) Token: 0x060008C8 RID: 2248
		public abstract int MaxLineLength { get; set; }

		// Token: 0x060008C9 RID: 2249 RVA: 0x000143C4 File Offset: 0x000125C4
		public void WriteMessageLine(string message, params object[] args)
		{
			this.WriteMessageLine(0, message, args);
		}

		// Token: 0x060008CA RID: 2250
		public abstract void WriteMessageLine(int level, string message, params object[] args);

		// Token: 0x060008CB RID: 2251
		public abstract void DisplayDifferences(ConstraintResult result);

		// Token: 0x060008CC RID: 2252
		public abstract void DisplayDifferences(object expected, object actual);

		// Token: 0x060008CD RID: 2253
		public abstract void DisplayDifferences(object expected, object actual, Tolerance tolerance);

		// Token: 0x060008CE RID: 2254
		public abstract void DisplayStringDifferences(string expected, string actual, int mismatch, bool ignoreCase, bool clipping);

		// Token: 0x060008CF RID: 2255
		public abstract void WriteActualValue(object actual);

		// Token: 0x060008D0 RID: 2256
		public abstract void WriteValue(object val);

		// Token: 0x060008D1 RID: 2257
		public abstract void WriteCollectionElements(IEnumerable collection, long start, int max);
	}
}
