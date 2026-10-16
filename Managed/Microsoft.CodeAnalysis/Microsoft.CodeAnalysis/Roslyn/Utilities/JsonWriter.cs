using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Roslyn.Utilities
{
	// Token: 0x0200004E RID: 78
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class JsonWriter : IDisposable
	{
		// Token: 0x06000199 RID: 409 RVA: 0x000063CD File Offset: 0x000045CD
		public JsonWriter(TextWriter output)
		{
			this._output = output;
			this._pending = JsonWriter.Pending.None;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000063E3 File Offset: 0x000045E3
		public void WriteObjectStart()
		{
			this.WriteStart('{');
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000063ED File Offset: 0x000045ED
		public void WriteObjectStart(string key)
		{
			this.WriteKey(key);
			this.WriteObjectStart();
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000063FC File Offset: 0x000045FC
		public void WriteObjectEnd()
		{
			this.WriteEnd('}');
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00006406 File Offset: 0x00004606
		public void WriteArrayStart()
		{
			this.WriteStart('[');
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00006410 File Offset: 0x00004610
		public void WriteArrayStart(string key)
		{
			this.WriteKey(key);
			this.WriteArrayStart();
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000641F File Offset: 0x0000461F
		public void WriteArrayEnd()
		{
			this.WriteEnd(']');
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x00006429 File Offset: 0x00004629
		public void WriteKey(string key)
		{
			this.Write(key);
			this._output.Write(": ");
			this._pending = JsonWriter.Pending.None;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00006449 File Offset: 0x00004649
		public void Write(string key, [Nullable(2)] string value)
		{
			this.WriteKey(key);
			this.Write(value);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00006459 File Offset: 0x00004659
		public void Write(string key, int value)
		{
			this.WriteKey(key);
			this.Write(value);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00006469 File Offset: 0x00004669
		public void Write(string key, bool value)
		{
			this.WriteKey(key);
			this.Write(value);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00006479 File Offset: 0x00004679
		[NullableContext(2)]
		public void Write(string value)
		{
			this.WritePending();
			this._output.Write('"');
			this._output.Write(JsonWriter.EscapeString(value));
			this._output.Write('"');
			this._pending = JsonWriter.Pending.CommaNewLineAndIndent;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000064B3 File Offset: 0x000046B3
		public void Write(int value)
		{
			this.WritePending();
			this._output.Write(value.ToString(CultureInfo.InvariantCulture));
			this._pending = JsonWriter.Pending.CommaNewLineAndIndent;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000064D9 File Offset: 0x000046D9
		public void Write(bool value)
		{
			this.WritePending();
			this._output.Write(value ? "true" : "false");
			this._pending = JsonWriter.Pending.CommaNewLineAndIndent;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00006504 File Offset: 0x00004704
		private void WritePending()
		{
			if (this._pending == JsonWriter.Pending.None)
			{
				return;
			}
			if (this._pending == JsonWriter.Pending.CommaNewLineAndIndent)
			{
				this._output.Write(',');
			}
			this._output.WriteLine();
			for (int i = 0; i < this._indent; i++)
			{
				this._output.Write("  ");
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000655C File Offset: 0x0000475C
		private void WriteStart(char c)
		{
			this.WritePending();
			this._output.Write(c);
			this._pending = JsonWriter.Pending.NewLineAndIndent;
			this._indent++;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00006585 File Offset: 0x00004785
		private void WriteEnd(char c)
		{
			this._pending = JsonWriter.Pending.NewLineAndIndent;
			this._indent--;
			this.WritePending();
			this._output.Write(c);
			this._pending = JsonWriter.Pending.CommaNewLineAndIndent;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000065B5 File Offset: 0x000047B5
		public void Dispose()
		{
			this._output.Dispose();
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000065C4 File Offset: 0x000047C4
		private static string EscapeString([Nullable(2)] string value)
		{
			PooledStringBuilder pooledStringBuilder = null;
			StringBuilder stringBuilder = null;
			if (RoslynString.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < value.Length; i++)
			{
				char c = value.get_Chars(i);
				if (c == '"' || c == '\\' || JsonWriter.ShouldAppendAsUnicode(c))
				{
					if (stringBuilder == null)
					{
						pooledStringBuilder = PooledStringBuilder.GetInstance();
						stringBuilder = pooledStringBuilder.Builder;
					}
					if (num2 > 0)
					{
						stringBuilder.Append(value, num, num2);
					}
					num = i + 1;
					num2 = 0;
					if (c != '"')
					{
						if (c != '\\')
						{
							JsonWriter.AppendCharAsUnicode(stringBuilder, c);
						}
						else
						{
							stringBuilder.Append("\\\\");
						}
					}
					else
					{
						stringBuilder.Append("\\\"");
					}
				}
				else
				{
					num2++;
				}
			}
			if (stringBuilder == null)
			{
				return value;
			}
			if (num2 > 0)
			{
				stringBuilder.Append(value, num, num2);
			}
			return pooledStringBuilder.ToStringAndFree();
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00006697 File Offset: 0x00004897
		private static void AppendCharAsUnicode(StringBuilder builder, char c)
		{
			builder.Append("\\u");
			builder.AppendFormat(CultureInfo.InvariantCulture, "{0:x4}", (int)c);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000066BC File Offset: 0x000048BC
		private static bool ShouldAppendAsUnicode(char c)
		{
			return c < ' ' || c >= '\ufffe' || (c >= '\ud800' && c <= '\udfff') || c == '\u0085' || c == '\u2028' || c == '\u2029';
		}

		// Token: 0x04000074 RID: 116
		private readonly TextWriter _output;

		// Token: 0x04000075 RID: 117
		private int _indent;

		// Token: 0x04000076 RID: 118
		private JsonWriter.Pending _pending;

		// Token: 0x04000077 RID: 119
		private const string Indentation = "  ";

		// Token: 0x02000500 RID: 1280
		[NullableContext(0)]
		private enum Pending
		{
			// Token: 0x04001405 RID: 5125
			None,
			// Token: 0x04001406 RID: 5126
			NewLineAndIndent,
			// Token: 0x04001407 RID: 5127
			CommaNewLineAndIndent
		}
	}
}
