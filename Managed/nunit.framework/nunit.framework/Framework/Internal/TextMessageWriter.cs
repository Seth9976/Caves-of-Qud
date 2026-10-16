using System;
using System.Collections;
using NUnit.Framework.Constraints;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000067 RID: 103
	public class TextMessageWriter : MessageWriter
	{
		// Token: 0x06000376 RID: 886 RVA: 0x00007D98 File Offset: 0x00005F98
		public TextMessageWriter()
		{
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00007DAB File Offset: 0x00005FAB
		public TextMessageWriter(string userMessage, params object[] args)
		{
			if (userMessage != null && userMessage != string.Empty)
			{
				base.WriteMessageLine(userMessage, args);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000378 RID: 888 RVA: 0x00007DD6 File Offset: 0x00005FD6
		// (set) Token: 0x06000379 RID: 889 RVA: 0x00007DDE File Offset: 0x00005FDE
		public override int MaxLineLength
		{
			get
			{
				return this.maxLineLength;
			}
			set
			{
				this.maxLineLength = value;
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00007DE7 File Offset: 0x00005FE7
		public override void WriteMessageLine(int level, string message, params object[] args)
		{
			if (message != null)
			{
				while (level-- >= 0)
				{
					this.Write("  ");
				}
				if (args != null && args.Length != 0)
				{
					message = string.Format(message, args);
				}
				this.WriteLine(MsgUtils.EscapeNullCharacters(message));
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00007E1E File Offset: 0x0000601E
		public override void DisplayDifferences(ConstraintResult result)
		{
			this.WriteExpectedLine(result);
			this.WriteActualLine(result);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00007E2E File Offset: 0x0000602E
		public override void DisplayDifferences(object expected, object actual)
		{
			this.WriteExpectedLine(expected);
			this.WriteActualLine(actual);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00007E3E File Offset: 0x0000603E
		public override void DisplayDifferences(object expected, object actual, Tolerance tolerance)
		{
			this.WriteExpectedLine(expected, tolerance);
			this.WriteActualLine(actual);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00007E50 File Offset: 0x00006050
		public override void DisplayStringDifferences(string expected, string actual, int mismatch, bool ignoreCase, bool clipping)
		{
			int num = this.MaxLineLength - TextMessageWriter.PrefixLength - 2;
			if (clipping)
			{
				MsgUtils.ClipExpectedAndActual(ref expected, ref actual, num, mismatch);
			}
			expected = MsgUtils.EscapeControlChars(expected);
			actual = MsgUtils.EscapeControlChars(actual);
			mismatch = MsgUtils.FindMismatchPosition(expected, actual, 0, ignoreCase);
			this.Write(TextMessageWriter.Pfx_Expected);
			this.Write(MsgUtils.FormatValue(expected));
			if (ignoreCase)
			{
				this.Write(", ignoring case");
			}
			this.WriteLine();
			this.WriteActualLine(actual);
			if (mismatch >= 0)
			{
				this.WriteCaretLine(mismatch);
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00007ED5 File Offset: 0x000060D5
		public override void WriteActualValue(object actual)
		{
			this.WriteValue(actual);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00007EDE File Offset: 0x000060DE
		public override void WriteValue(object val)
		{
			this.Write(MsgUtils.FormatValue(val));
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00007EEC File Offset: 0x000060EC
		public override void WriteCollectionElements(IEnumerable collection, long start, int max)
		{
			this.Write(MsgUtils.FormatCollection(collection, start, max));
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00007EFC File Offset: 0x000060FC
		private void WriteExpectedLine(ConstraintResult result)
		{
			this.Write(TextMessageWriter.Pfx_Expected);
			this.WriteLine(result.Description);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00007F15 File Offset: 0x00006115
		private void WriteExpectedLine(object expected)
		{
			this.WriteExpectedLine(expected, null);
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00007F20 File Offset: 0x00006120
		private void WriteExpectedLine(object expected, Tolerance tolerance)
		{
			this.Write(TextMessageWriter.Pfx_Expected);
			this.Write(MsgUtils.FormatValue(expected));
			if (tolerance != null && !tolerance.IsUnsetOrDefault)
			{
				this.Write(" +/- ");
				this.Write(MsgUtils.FormatValue(tolerance.Value));
				if (tolerance.Mode != ToleranceMode.Linear)
				{
					this.Write(" {0}", tolerance.Mode);
				}
			}
			this.WriteLine();
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00007F90 File Offset: 0x00006190
		private void WriteActualLine(ConstraintResult result)
		{
			this.Write(TextMessageWriter.Pfx_Actual);
			result.WriteActualValueTo(this);
			this.WriteLine();
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00007FAA File Offset: 0x000061AA
		private void WriteActualLine(object actual)
		{
			this.Write(TextMessageWriter.Pfx_Actual);
			this.WriteActualValue(actual);
			this.WriteLine();
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00007FC4 File Offset: 0x000061C4
		private void WriteCaretLine(int mismatch)
		{
			this.WriteLine("  {0}^", new string('-', TextMessageWriter.PrefixLength + mismatch - 2 + 1));
		}

		// Token: 0x04000071 RID: 113
		private static readonly int DEFAULT_LINE_LENGTH = 78;

		// Token: 0x04000072 RID: 114
		public static readonly string Pfx_Expected = "  Expected: ";

		// Token: 0x04000073 RID: 115
		public static readonly string Pfx_Actual = "  But was:  ";

		// Token: 0x04000074 RID: 116
		public static readonly int PrefixLength = TextMessageWriter.Pfx_Expected.Length;

		// Token: 0x04000075 RID: 117
		private int maxLineLength = TextMessageWriter.DEFAULT_LINE_LENGTH;
	}
}
