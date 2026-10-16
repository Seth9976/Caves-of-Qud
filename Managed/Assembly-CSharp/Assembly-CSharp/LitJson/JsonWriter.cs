using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace LitJson
{
	// Token: 0x0200032C RID: 812
	public class JsonWriter
	{
		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06001E29 RID: 7721 RVA: 0x00093084 File Offset: 0x00091284
		// (set) Token: 0x06001E2A RID: 7722 RVA: 0x0009308C File Offset: 0x0009128C
		public int IndentValue
		{
			get
			{
				return this.indent_value;
			}
			set
			{
				this.indentation = this.indentation / this.indent_value * value;
				this.indent_value = value;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06001E2B RID: 7723 RVA: 0x000930AA File Offset: 0x000912AA
		// (set) Token: 0x06001E2C RID: 7724 RVA: 0x000930B2 File Offset: 0x000912B2
		public bool PrettyPrint
		{
			get
			{
				return this.pretty_print;
			}
			set
			{
				this.pretty_print = value;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001E2D RID: 7725 RVA: 0x000930BB File Offset: 0x000912BB
		public TextWriter TextWriter
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001E2E RID: 7726 RVA: 0x000930C3 File Offset: 0x000912C3
		// (set) Token: 0x06001E2F RID: 7727 RVA: 0x000930CB File Offset: 0x000912CB
		public bool Validate
		{
			get
			{
				return this.validate;
			}
			set
			{
				this.validate = value;
			}
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x000930E0 File Offset: 0x000912E0
		public JsonWriter()
		{
			this.inst_string_builder = new StringBuilder();
			this.writer = new StringWriter(this.inst_string_builder);
			this.Init();
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x0009310A File Offset: 0x0009130A
		public JsonWriter(StringBuilder sb)
			: this(new StringWriter(sb))
		{
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x00093118 File Offset: 0x00091318
		public JsonWriter(TextWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			this.writer = writer;
			this.Init();
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x0009313C File Offset: 0x0009133C
		private void DoValidation(Condition cond)
		{
			if (!this.context.ExpectingValue)
			{
				this.context.Count++;
			}
			if (!this.validate)
			{
				return;
			}
			if (this.has_reached_end)
			{
				throw new JsonException("A complete JSON symbol has already been written");
			}
			switch (cond)
			{
			case Condition.InArray:
				if (!this.context.InArray)
				{
					throw new JsonException("Can't close an array here");
				}
				break;
			case Condition.InObject:
				if (!this.context.InObject || this.context.ExpectingValue)
				{
					throw new JsonException("Can't close an object here");
				}
				break;
			case Condition.NotAProperty:
				if (this.context.InObject && !this.context.ExpectingValue)
				{
					throw new JsonException("Expected a property");
				}
				break;
			case Condition.Property:
				if (!this.context.InObject || this.context.ExpectingValue)
				{
					throw new JsonException("Can't add a property here");
				}
				break;
			case Condition.Value:
				if (!this.context.InArray && (!this.context.InObject || !this.context.ExpectingValue))
				{
					throw new JsonException("Can't add a value here");
				}
				break;
			default:
				return;
			}
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x00093260 File Offset: 0x00091460
		private void Init()
		{
			this.has_reached_end = false;
			this.hex_seq = new char[4];
			this.indentation = 0;
			this.indent_value = 4;
			this.pretty_print = false;
			this.validate = true;
			this.ctx_stack = new Stack<WriterContext>();
			this.context = new WriterContext();
			this.ctx_stack.Push(this.context);
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x000932C4 File Offset: 0x000914C4
		private static void IntToHex(int n, char[] hex)
		{
			for (int i = 0; i < 4; i++)
			{
				int num = n % 16;
				if (num < 10)
				{
					hex[3 - i] = (char)(48 + num);
				}
				else
				{
					hex[3 - i] = (char)(65 + (num - 10));
				}
				n >>= 4;
			}
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x00093305 File Offset: 0x00091505
		private void Indent()
		{
			if (this.pretty_print)
			{
				this.indentation += this.indent_value;
			}
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x00093324 File Offset: 0x00091524
		private void Put(string str)
		{
			if (this.pretty_print && !this.context.ExpectingValue)
			{
				for (int i = 0; i < this.indentation; i++)
				{
					this.writer.Write(' ');
				}
			}
			this.writer.Write(str);
		}

		// Token: 0x06001E39 RID: 7737 RVA: 0x00093370 File Offset: 0x00091570
		private void PutNewline()
		{
			this.PutNewline(true);
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x0009337C File Offset: 0x0009157C
		private void PutNewline(bool add_comma)
		{
			if (add_comma && !this.context.ExpectingValue && this.context.Count > 1)
			{
				this.writer.Write(',');
			}
			if (this.pretty_print && !this.context.ExpectingValue)
			{
				this.writer.Write('\n');
			}
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x000933D8 File Offset: 0x000915D8
		private void PutString(string str)
		{
			this.Put(string.Empty);
			this.writer.Write('"');
			int length = str.Length;
			int i = 0;
			while (i < length)
			{
				char c = str[i];
				switch (c)
				{
				case '\b':
					this.writer.Write("\\b");
					break;
				case '\t':
					this.writer.Write("\\t");
					break;
				case '\n':
					this.writer.Write("\\n");
					break;
				case '\v':
					goto IL_00E4;
				case '\f':
					this.writer.Write("\\f");
					break;
				case '\r':
					this.writer.Write("\\r");
					break;
				default:
					if (c != '"' && c != '\\')
					{
						goto IL_00E4;
					}
					this.writer.Write('\\');
					this.writer.Write(str[i]);
					break;
				}
				IL_0141:
				i++;
				continue;
				IL_00E4:
				if (str[i] >= ' ' && str[i] <= '~')
				{
					this.writer.Write(str[i]);
					goto IL_0141;
				}
				JsonWriter.IntToHex((int)str[i], this.hex_seq);
				this.writer.Write("\\u");
				this.writer.Write(this.hex_seq);
				goto IL_0141;
			}
			this.writer.Write('"');
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x0009353E File Offset: 0x0009173E
		private void Unindent()
		{
			if (this.pretty_print)
			{
				this.indentation -= this.indent_value;
			}
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x0009355B File Offset: 0x0009175B
		public override string ToString()
		{
			if (this.inst_string_builder == null)
			{
				return string.Empty;
			}
			return this.inst_string_builder.ToString();
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x00093578 File Offset: 0x00091778
		public void Reset()
		{
			this.has_reached_end = false;
			this.ctx_stack.Clear();
			this.context = new WriterContext();
			this.ctx_stack.Push(this.context);
			if (this.inst_string_builder != null)
			{
				this.inst_string_builder.Remove(0, this.inst_string_builder.Length);
			}
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x000935D3 File Offset: 0x000917D3
		public void Write(bool boolean)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			this.Put(boolean ? "true" : "false");
			this.context.ExpectingValue = false;
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x00093603 File Offset: 0x00091803
		public void Write(decimal number)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			this.Put(Convert.ToString(number, JsonWriter.number_format));
			this.context.ExpectingValue = false;
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x00093630 File Offset: 0x00091830
		public void Write(double number)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			string text = Convert.ToString(number, JsonWriter.number_format);
			this.Put(text);
			if (text.IndexOf('.') == -1 && text.IndexOf('E') == -1)
			{
				this.writer.Write(".0");
			}
			this.context.ExpectingValue = false;
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x0009368F File Offset: 0x0009188F
		public void Write(int number)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			this.Put(Convert.ToString(number, JsonWriter.number_format));
			this.context.ExpectingValue = false;
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x000936BB File Offset: 0x000918BB
		public void Write(long number)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			this.Put(Convert.ToString(number, JsonWriter.number_format));
			this.context.ExpectingValue = false;
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x000936E7 File Offset: 0x000918E7
		public void Write(string str)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			if (str == null)
			{
				this.Put("null");
			}
			else
			{
				this.PutString(str);
			}
			this.context.ExpectingValue = false;
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x00093719 File Offset: 0x00091919
		[CLSCompliant(false)]
		public void Write(ulong number)
		{
			this.DoValidation(Condition.Value);
			this.PutNewline();
			this.Put(Convert.ToString(number, JsonWriter.number_format));
			this.context.ExpectingValue = false;
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x00093748 File Offset: 0x00091948
		public void WriteArrayEnd()
		{
			this.DoValidation(Condition.InArray);
			this.PutNewline(false);
			this.ctx_stack.Pop();
			if (this.ctx_stack.Count == 1)
			{
				this.has_reached_end = true;
			}
			else
			{
				this.context = this.ctx_stack.Peek();
				this.context.ExpectingValue = false;
			}
			this.Unindent();
			this.Put("]");
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x000937B4 File Offset: 0x000919B4
		public void WriteArrayStart()
		{
			this.DoValidation(Condition.NotAProperty);
			this.PutNewline();
			this.Put("[");
			this.context = new WriterContext();
			this.context.InArray = true;
			this.ctx_stack.Push(this.context);
			this.Indent();
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x00093808 File Offset: 0x00091A08
		public void WriteObjectEnd()
		{
			this.DoValidation(Condition.InObject);
			this.PutNewline(false);
			this.ctx_stack.Pop();
			if (this.ctx_stack.Count == 1)
			{
				this.has_reached_end = true;
			}
			else
			{
				this.context = this.ctx_stack.Peek();
				this.context.ExpectingValue = false;
			}
			this.Unindent();
			this.Put("}");
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x00093874 File Offset: 0x00091A74
		public void WriteObjectStart()
		{
			this.DoValidation(Condition.NotAProperty);
			this.PutNewline();
			this.Put("{");
			this.context = new WriterContext();
			this.context.InObject = true;
			this.ctx_stack.Push(this.context);
			this.Indent();
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x000938C8 File Offset: 0x00091AC8
		public void WritePropertyName(string property_name)
		{
			this.DoValidation(Condition.Property);
			this.PutNewline();
			this.PutString(property_name);
			if (this.pretty_print)
			{
				if (property_name.Length > this.context.Padding)
				{
					this.context.Padding = property_name.Length;
				}
				for (int i = this.context.Padding - property_name.Length; i >= 0; i--)
				{
					this.writer.Write(' ');
				}
				this.writer.Write(": ");
			}
			else
			{
				this.writer.Write(':');
			}
			this.context.ExpectingValue = true;
		}

		// Token: 0x04000FC3 RID: 4035
		private static NumberFormatInfo number_format = NumberFormatInfo.InvariantInfo;

		// Token: 0x04000FC4 RID: 4036
		private WriterContext context;

		// Token: 0x04000FC5 RID: 4037
		private Stack<WriterContext> ctx_stack;

		// Token: 0x04000FC6 RID: 4038
		private bool has_reached_end;

		// Token: 0x04000FC7 RID: 4039
		private char[] hex_seq;

		// Token: 0x04000FC8 RID: 4040
		private int indentation;

		// Token: 0x04000FC9 RID: 4041
		private int indent_value;

		// Token: 0x04000FCA RID: 4042
		private StringBuilder inst_string_builder;

		// Token: 0x04000FCB RID: 4043
		private bool pretty_print;

		// Token: 0x04000FCC RID: 4044
		private bool validate;

		// Token: 0x04000FCD RID: 4045
		private TextWriter writer;
	}
}
