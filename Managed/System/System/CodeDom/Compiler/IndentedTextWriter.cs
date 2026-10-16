using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace System.CodeDom.Compiler
{
	/// <summary>Provides a text writer that can indent new lines by a tab string token.</summary>
	// Token: 0x02000358 RID: 856
	public class IndentedTextWriter : TextWriter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.IndentedTextWriter" /> class using the specified text writer and default tab string.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to use for output. </param>
		// Token: 0x06001C27 RID: 7207 RVA: 0x00066940 File Offset: 0x00064B40
		public IndentedTextWriter(TextWriter writer)
			: this(writer, "    ")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.IndentedTextWriter" /> class using the specified text writer and tab string.</summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to use for output. </param>
		/// <param name="tabString">The tab string to use for indentation. </param>
		// Token: 0x06001C28 RID: 7208 RVA: 0x0006694E File Offset: 0x00064B4E
		public IndentedTextWriter(TextWriter writer, string tabString)
			: base(CultureInfo.InvariantCulture)
		{
			this._writer = writer;
			this._tabString = tabString;
			this._indentLevel = 0;
			this._tabsPending = false;
		}

		/// <summary>Gets the encoding for the text writer to use.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> that indicates the encoding for the text writer to use.</returns>
		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001C29 RID: 7209 RVA: 0x00066977 File Offset: 0x00064B77
		public override Encoding Encoding
		{
			get
			{
				return this._writer.Encoding;
			}
		}

		/// <summary>Gets or sets the new line character to use.</summary>
		/// <returns>The new line character to use.</returns>
		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001C2A RID: 7210 RVA: 0x00066984 File Offset: 0x00064B84
		// (set) Token: 0x06001C2B RID: 7211 RVA: 0x00066991 File Offset: 0x00064B91
		public override string NewLine
		{
			get
			{
				return this._writer.NewLine;
			}
			set
			{
				this._writer.NewLine = value;
			}
		}

		/// <summary>Gets or sets the number of spaces to indent.</summary>
		/// <returns>The number of spaces to indent.</returns>
		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001C2C RID: 7212 RVA: 0x0006699F File Offset: 0x00064B9F
		// (set) Token: 0x06001C2D RID: 7213 RVA: 0x000669A7 File Offset: 0x00064BA7
		public int Indent
		{
			get
			{
				return this._indentLevel;
			}
			set
			{
				this._indentLevel = Math.Max(value, 0);
			}
		}

		/// <summary>Gets the <see cref="T:System.IO.TextWriter" /> to use.</summary>
		/// <returns>The <see cref="T:System.IO.TextWriter" /> to use.</returns>
		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001C2E RID: 7214 RVA: 0x000669B6 File Offset: 0x00064BB6
		public TextWriter InnerWriter
		{
			get
			{
				return this._writer;
			}
		}

		/// <summary>Closes the document being written to.</summary>
		// Token: 0x06001C2F RID: 7215 RVA: 0x000669BE File Offset: 0x00064BBE
		public override void Close()
		{
			this._writer.Close();
		}

		/// <summary>Flushes the stream.</summary>
		// Token: 0x06001C30 RID: 7216 RVA: 0x000669CB File Offset: 0x00064BCB
		public override void Flush()
		{
			this._writer.Flush();
		}

		/// <summary>Outputs the tab string once for each level of indentation according to the <see cref="P:System.CodeDom.Compiler.IndentedTextWriter.Indent" /> property.</summary>
		// Token: 0x06001C31 RID: 7217 RVA: 0x000669D8 File Offset: 0x00064BD8
		protected virtual void OutputTabs()
		{
			if (this._tabsPending)
			{
				for (int i = 0; i < this._indentLevel; i++)
				{
					this._writer.Write(this._tabString);
				}
				this._tabsPending = false;
			}
		}

		/// <summary>Writes the specified string to the text stream.</summary>
		/// <param name="s">The string to write. </param>
		// Token: 0x06001C32 RID: 7218 RVA: 0x00066A16 File Offset: 0x00064C16
		public override void Write(string s)
		{
			this.OutputTabs();
			this._writer.Write(s);
		}

		/// <summary>Writes the text representation of a Boolean value to the text stream.</summary>
		/// <param name="value">The Boolean value to write. </param>
		// Token: 0x06001C33 RID: 7219 RVA: 0x00066A2A File Offset: 0x00064C2A
		public override void Write(bool value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		/// <summary>Writes a character to the text stream.</summary>
		/// <param name="value">The character to write. </param>
		// Token: 0x06001C34 RID: 7220 RVA: 0x00066A3E File Offset: 0x00064C3E
		public override void Write(char value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		/// <summary>Writes a character array to the text stream.</summary>
		/// <param name="buffer">The character array to write. </param>
		// Token: 0x06001C35 RID: 7221 RVA: 0x00066A52 File Offset: 0x00064C52
		public override void Write(char[] buffer)
		{
			this.OutputTabs();
			this._writer.Write(buffer);
		}

		/// <summary>Writes a subarray of characters to the text stream.</summary>
		/// <param name="buffer">The character array to write data from. </param>
		/// <param name="index">Starting index in the buffer. </param>
		/// <param name="count">The number of characters to write. </param>
		// Token: 0x06001C36 RID: 7222 RVA: 0x00066A66 File Offset: 0x00064C66
		public override void Write(char[] buffer, int index, int count)
		{
			this.OutputTabs();
			this._writer.Write(buffer, index, count);
		}

		/// <summary>Writes the text representation of a Double to the text stream.</summary>
		/// <param name="value">The double to write. </param>
		// Token: 0x06001C37 RID: 7223 RVA: 0x00066A7C File Offset: 0x00064C7C
		public override void Write(double value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		/// <summary>Writes the text representation of a Single to the text stream.</summary>
		/// <param name="value">The single to write. </param>
		// Token: 0x06001C38 RID: 7224 RVA: 0x00066A90 File Offset: 0x00064C90
		public override void Write(float value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		/// <summary>Writes the text representation of an integer to the text stream.</summary>
		/// <param name="value">The integer to write. </param>
		// Token: 0x06001C39 RID: 7225 RVA: 0x00066AA4 File Offset: 0x00064CA4
		public override void Write(int value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		/// <summary>Writes the text representation of an 8-byte integer to the text stream.</summary>
		/// <param name="value">The 8-byte integer to write. </param>
		// Token: 0x06001C3A RID: 7226 RVA: 0x00066AB8 File Offset: 0x00064CB8
		public override void Write(long value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		/// <summary>Writes the text representation of an object to the text stream.</summary>
		/// <param name="value">The object to write. </param>
		// Token: 0x06001C3B RID: 7227 RVA: 0x00066ACC File Offset: 0x00064CCC
		public override void Write(object value)
		{
			this.OutputTabs();
			this._writer.Write(value);
		}

		/// <summary>Writes out a formatted string, using the same semantics as specified.</summary>
		/// <param name="format">The formatting string. </param>
		/// <param name="arg0">The object to write into the formatted string. </param>
		// Token: 0x06001C3C RID: 7228 RVA: 0x00066AE0 File Offset: 0x00064CE0
		public override void Write(string format, object arg0)
		{
			this.OutputTabs();
			this._writer.Write(format, arg0);
		}

		/// <summary>Writes out a formatted string, using the same semantics as specified.</summary>
		/// <param name="format">The formatting string to use. </param>
		/// <param name="arg0">The first object to write into the formatted string. </param>
		/// <param name="arg1">The second object to write into the formatted string. </param>
		// Token: 0x06001C3D RID: 7229 RVA: 0x00066AF5 File Offset: 0x00064CF5
		public override void Write(string format, object arg0, object arg1)
		{
			this.OutputTabs();
			this._writer.Write(format, arg0, arg1);
		}

		/// <summary>Writes out a formatted string, using the same semantics as specified.</summary>
		/// <param name="format">The formatting string to use. </param>
		/// <param name="arg">The argument array to output. </param>
		// Token: 0x06001C3E RID: 7230 RVA: 0x00066B0B File Offset: 0x00064D0B
		public override void Write(string format, params object[] arg)
		{
			this.OutputTabs();
			this._writer.Write(format, arg);
		}

		/// <summary>Writes the specified string to a line without tabs.</summary>
		/// <param name="s">The string to write. </param>
		// Token: 0x06001C3F RID: 7231 RVA: 0x00066B20 File Offset: 0x00064D20
		public void WriteLineNoTabs(string s)
		{
			this._writer.WriteLine(s);
		}

		/// <summary>Writes the specified string, followed by a line terminator, to the text stream.</summary>
		/// <param name="s">The string to write. </param>
		// Token: 0x06001C40 RID: 7232 RVA: 0x00066B2E File Offset: 0x00064D2E
		public override void WriteLine(string s)
		{
			this.OutputTabs();
			this._writer.WriteLine(s);
			this._tabsPending = true;
		}

		/// <summary>Writes a line terminator.</summary>
		// Token: 0x06001C41 RID: 7233 RVA: 0x00066B49 File Offset: 0x00064D49
		public override void WriteLine()
		{
			this.OutputTabs();
			this._writer.WriteLine();
			this._tabsPending = true;
		}

		/// <summary>Writes the text representation of a Boolean, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">The Boolean to write. </param>
		// Token: 0x06001C42 RID: 7234 RVA: 0x00066B63 File Offset: 0x00064D63
		public override void WriteLine(bool value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		/// <summary>Writes a character, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">The character to write. </param>
		// Token: 0x06001C43 RID: 7235 RVA: 0x00066B7E File Offset: 0x00064D7E
		public override void WriteLine(char value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		/// <summary>Writes a character array, followed by a line terminator, to the text stream.</summary>
		/// <param name="buffer">The character array to write. </param>
		// Token: 0x06001C44 RID: 7236 RVA: 0x00066B99 File Offset: 0x00064D99
		public override void WriteLine(char[] buffer)
		{
			this.OutputTabs();
			this._writer.WriteLine(buffer);
			this._tabsPending = true;
		}

		/// <summary>Writes a subarray of characters, followed by a line terminator, to the text stream.</summary>
		/// <param name="buffer">The character array to write data from. </param>
		/// <param name="index">Starting index in the buffer. </param>
		/// <param name="count">The number of characters to write. </param>
		// Token: 0x06001C45 RID: 7237 RVA: 0x00066BB4 File Offset: 0x00064DB4
		public override void WriteLine(char[] buffer, int index, int count)
		{
			this.OutputTabs();
			this._writer.WriteLine(buffer, index, count);
			this._tabsPending = true;
		}

		/// <summary>Writes the text representation of a Double, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">The double to write. </param>
		// Token: 0x06001C46 RID: 7238 RVA: 0x00066BD1 File Offset: 0x00064DD1
		public override void WriteLine(double value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		/// <summary>Writes the text representation of a Single, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">The single to write. </param>
		// Token: 0x06001C47 RID: 7239 RVA: 0x00066BEC File Offset: 0x00064DEC
		public override void WriteLine(float value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		/// <summary>Writes the text representation of an integer, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">The integer to write. </param>
		// Token: 0x06001C48 RID: 7240 RVA: 0x00066C07 File Offset: 0x00064E07
		public override void WriteLine(int value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		/// <summary>Writes the text representation of an 8-byte integer, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">The 8-byte integer to write. </param>
		// Token: 0x06001C49 RID: 7241 RVA: 0x00066C22 File Offset: 0x00064E22
		public override void WriteLine(long value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		/// <summary>Writes the text representation of an object, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">The object to write. </param>
		// Token: 0x06001C4A RID: 7242 RVA: 0x00066C3D File Offset: 0x00064E3D
		public override void WriteLine(object value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		/// <summary>Writes out a formatted string, followed by a line terminator, using the same semantics as specified.</summary>
		/// <param name="format">The formatting string. </param>
		/// <param name="arg0">The object to write into the formatted string. </param>
		// Token: 0x06001C4B RID: 7243 RVA: 0x00066C58 File Offset: 0x00064E58
		public override void WriteLine(string format, object arg0)
		{
			this.OutputTabs();
			this._writer.WriteLine(format, arg0);
			this._tabsPending = true;
		}

		/// <summary>Writes out a formatted string, followed by a line terminator, using the same semantics as specified.</summary>
		/// <param name="format">The formatting string to use. </param>
		/// <param name="arg0">The first object to write into the formatted string. </param>
		/// <param name="arg1">The second object to write into the formatted string. </param>
		// Token: 0x06001C4C RID: 7244 RVA: 0x00066C74 File Offset: 0x00064E74
		public override void WriteLine(string format, object arg0, object arg1)
		{
			this.OutputTabs();
			this._writer.WriteLine(format, arg0, arg1);
			this._tabsPending = true;
		}

		/// <summary>Writes out a formatted string, followed by a line terminator, using the same semantics as specified.</summary>
		/// <param name="format">The formatting string to use. </param>
		/// <param name="arg">The argument array to output. </param>
		// Token: 0x06001C4D RID: 7245 RVA: 0x00066C91 File Offset: 0x00064E91
		public override void WriteLine(string format, params object[] arg)
		{
			this.OutputTabs();
			this._writer.WriteLine(format, arg);
			this._tabsPending = true;
		}

		/// <summary>Writes the text representation of a UInt32, followed by a line terminator, to the text stream.</summary>
		/// <param name="value">A UInt32 to output. </param>
		// Token: 0x06001C4E RID: 7246 RVA: 0x00066CAD File Offset: 0x00064EAD
		[CLSCompliant(false)]
		public override void WriteLine(uint value)
		{
			this.OutputTabs();
			this._writer.WriteLine(value);
			this._tabsPending = true;
		}

		// Token: 0x04000E90 RID: 3728
		private readonly TextWriter _writer;

		// Token: 0x04000E91 RID: 3729
		private readonly string _tabString;

		// Token: 0x04000E92 RID: 3730
		private int _indentLevel;

		// Token: 0x04000E93 RID: 3731
		private bool _tabsPending;

		/// <summary>Specifies the default tab string. This field is constant. </summary>
		// Token: 0x04000E94 RID: 3732
		public const string DefaultTabString = "    ";
	}
}
