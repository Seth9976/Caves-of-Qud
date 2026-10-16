using System;
using System.Collections.Generic;
using System.IO;

namespace LitJson
{
	// Token: 0x02000329 RID: 809
	public class JsonReader
	{
		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06001E12 RID: 7698 RVA: 0x000927D8 File Offset: 0x000909D8
		// (set) Token: 0x06001E13 RID: 7699 RVA: 0x000927E5 File Offset: 0x000909E5
		public bool AllowComments
		{
			get
			{
				return this.lexer.AllowComments;
			}
			set
			{
				this.lexer.AllowComments = value;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06001E14 RID: 7700 RVA: 0x000927F3 File Offset: 0x000909F3
		// (set) Token: 0x06001E15 RID: 7701 RVA: 0x00092800 File Offset: 0x00090A00
		public bool AllowSingleQuotedStrings
		{
			get
			{
				return this.lexer.AllowSingleQuotedStrings;
			}
			set
			{
				this.lexer.AllowSingleQuotedStrings = value;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06001E16 RID: 7702 RVA: 0x0009280E File Offset: 0x00090A0E
		// (set) Token: 0x06001E17 RID: 7703 RVA: 0x00092816 File Offset: 0x00090A16
		public bool SkipNonMembers
		{
			get
			{
				return this.skip_non_members;
			}
			set
			{
				this.skip_non_members = value;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06001E18 RID: 7704 RVA: 0x0009281F File Offset: 0x00090A1F
		public bool EndOfInput
		{
			get
			{
				return this.end_of_input;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06001E19 RID: 7705 RVA: 0x00092827 File Offset: 0x00090A27
		public bool EndOfJson
		{
			get
			{
				return this.end_of_json;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06001E1A RID: 7706 RVA: 0x0009282F File Offset: 0x00090A2F
		public JsonToken Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06001E1B RID: 7707 RVA: 0x00092837 File Offset: 0x00090A37
		public object Value
		{
			get
			{
				return this.token_value;
			}
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x0009283F File Offset: 0x00090A3F
		static JsonReader()
		{
			JsonReader.PopulateParseTable();
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x00092846 File Offset: 0x00090A46
		public JsonReader(string json_text)
			: this(new StringReader(json_text), true)
		{
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x00092855 File Offset: 0x00090A55
		public JsonReader(TextReader reader)
			: this(reader, false)
		{
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x00092860 File Offset: 0x00090A60
		private JsonReader(TextReader reader, bool owned)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this.parser_in_string = false;
			this.parser_return = false;
			this.read_started = false;
			this.automaton_stack = new Stack<int>();
			this.automaton_stack.Push(65553);
			this.automaton_stack.Push(65543);
			this.lexer = new Lexer(reader);
			this.end_of_input = false;
			this.end_of_json = false;
			this.skip_non_members = true;
			this.reader = reader;
			this.reader_is_owned = owned;
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x000928F0 File Offset: 0x00090AF0
		private static void PopulateParseTable()
		{
			JsonReader.parse_table = new Dictionary<int, IDictionary<int, int[]>>();
			JsonReader.TableAddRow(ParserToken.Array);
			JsonReader.TableAddCol(ParserToken.Array, 91, new int[] { 91, 65549 });
			JsonReader.TableAddRow(ParserToken.ArrayPrime);
			JsonReader.TableAddCol(ParserToken.ArrayPrime, 34, new int[] { 65550, 65551, 93 });
			JsonReader.TableAddCol(ParserToken.ArrayPrime, 91, new int[] { 65550, 65551, 93 });
			JsonReader.TableAddCol(ParserToken.ArrayPrime, 93, new int[] { 93 });
			JsonReader.TableAddCol(ParserToken.ArrayPrime, 123, new int[] { 65550, 65551, 93 });
			JsonReader.TableAddCol(ParserToken.ArrayPrime, 65537, new int[] { 65550, 65551, 93 });
			JsonReader.TableAddCol(ParserToken.ArrayPrime, 65538, new int[] { 65550, 65551, 93 });
			JsonReader.TableAddCol(ParserToken.ArrayPrime, 65539, new int[] { 65550, 65551, 93 });
			JsonReader.TableAddCol(ParserToken.ArrayPrime, 65540, new int[] { 65550, 65551, 93 });
			JsonReader.TableAddRow(ParserToken.Object);
			JsonReader.TableAddCol(ParserToken.Object, 123, new int[] { 123, 65545 });
			JsonReader.TableAddRow(ParserToken.ObjectPrime);
			JsonReader.TableAddCol(ParserToken.ObjectPrime, 34, new int[] { 65546, 65547, 125 });
			JsonReader.TableAddCol(ParserToken.ObjectPrime, 125, new int[] { 125 });
			JsonReader.TableAddRow(ParserToken.Pair);
			JsonReader.TableAddCol(ParserToken.Pair, 34, new int[] { 65552, 58, 65550 });
			JsonReader.TableAddRow(ParserToken.PairRest);
			JsonReader.TableAddCol(ParserToken.PairRest, 44, new int[] { 44, 65546, 65547 });
			JsonReader.TableAddCol(ParserToken.PairRest, 125, new int[] { 65554 });
			JsonReader.TableAddRow(ParserToken.String);
			JsonReader.TableAddCol(ParserToken.String, 34, new int[] { 34, 65541, 34 });
			JsonReader.TableAddRow(ParserToken.Text);
			JsonReader.TableAddCol(ParserToken.Text, 91, new int[] { 65548 });
			JsonReader.TableAddCol(ParserToken.Text, 123, new int[] { 65544 });
			JsonReader.TableAddRow(ParserToken.Value);
			JsonReader.TableAddCol(ParserToken.Value, 34, new int[] { 65552 });
			JsonReader.TableAddCol(ParserToken.Value, 91, new int[] { 65548 });
			JsonReader.TableAddCol(ParserToken.Value, 123, new int[] { 65544 });
			JsonReader.TableAddCol(ParserToken.Value, 65537, new int[] { 65537 });
			JsonReader.TableAddCol(ParserToken.Value, 65538, new int[] { 65538 });
			JsonReader.TableAddCol(ParserToken.Value, 65539, new int[] { 65539 });
			JsonReader.TableAddCol(ParserToken.Value, 65540, new int[] { 65540 });
			JsonReader.TableAddRow(ParserToken.ValueRest);
			JsonReader.TableAddCol(ParserToken.ValueRest, 44, new int[] { 44, 65550, 65551 });
			JsonReader.TableAddCol(ParserToken.ValueRest, 93, new int[] { 65554 });
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x00092C69 File Offset: 0x00090E69
		private static void TableAddCol(ParserToken row, int col, params int[] symbols)
		{
			JsonReader.parse_table[(int)row].Add(col, symbols);
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x00092C7D File Offset: 0x00090E7D
		private static void TableAddRow(ParserToken rule)
		{
			JsonReader.parse_table.Add((int)rule, new Dictionary<int, int[]>());
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x00092C90 File Offset: 0x00090E90
		private void ProcessNumber(string number)
		{
			double num;
			if ((number.IndexOf('.') != -1 || number.IndexOf('e') != -1 || number.IndexOf('E') != -1) && double.TryParse(number, out num))
			{
				this.token = JsonToken.Double;
				this.token_value = num;
				return;
			}
			int num2;
			if (int.TryParse(number, out num2))
			{
				this.token = JsonToken.Int;
				this.token_value = num2;
				return;
			}
			long num3;
			if (long.TryParse(number, out num3))
			{
				this.token = JsonToken.Long;
				this.token_value = num3;
				return;
			}
			this.token = JsonToken.Int;
			this.token_value = 0;
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x00092D2C File Offset: 0x00090F2C
		private void ProcessSymbol()
		{
			if (this.current_symbol == 91)
			{
				this.token = JsonToken.ArrayStart;
				this.parser_return = true;
				return;
			}
			if (this.current_symbol == 93)
			{
				this.token = JsonToken.ArrayEnd;
				this.parser_return = true;
				return;
			}
			if (this.current_symbol == 123)
			{
				this.token = JsonToken.ObjectStart;
				this.parser_return = true;
				return;
			}
			if (this.current_symbol == 125)
			{
				this.token = JsonToken.ObjectEnd;
				this.parser_return = true;
				return;
			}
			if (this.current_symbol == 34)
			{
				if (this.parser_in_string)
				{
					this.parser_in_string = false;
					this.parser_return = true;
					return;
				}
				if (this.token == JsonToken.None)
				{
					this.token = JsonToken.String;
				}
				this.parser_in_string = true;
				return;
			}
			else
			{
				if (this.current_symbol == 65541)
				{
					this.token_value = this.lexer.StringValue;
					return;
				}
				if (this.current_symbol == 65539)
				{
					this.token = JsonToken.Boolean;
					this.token_value = false;
					this.parser_return = true;
					return;
				}
				if (this.current_symbol == 65540)
				{
					this.token = JsonToken.Null;
					this.parser_return = true;
					return;
				}
				if (this.current_symbol == 65537)
				{
					this.ProcessNumber(this.lexer.StringValue);
					this.parser_return = true;
					return;
				}
				if (this.current_symbol == 65546)
				{
					this.token = JsonToken.PropertyName;
					return;
				}
				if (this.current_symbol == 65538)
				{
					this.token = JsonToken.Boolean;
					this.token_value = true;
					this.parser_return = true;
				}
				return;
			}
		}

		// Token: 0x06001E25 RID: 7717 RVA: 0x00092E9E File Offset: 0x0009109E
		private bool ReadToken()
		{
			if (this.end_of_input)
			{
				return false;
			}
			this.lexer.NextToken();
			if (this.lexer.EndOfInput)
			{
				this.Close();
				return false;
			}
			this.current_input = this.lexer.Token;
			return true;
		}

		// Token: 0x06001E26 RID: 7718 RVA: 0x00092EDD File Offset: 0x000910DD
		public void Close()
		{
			if (this.end_of_input)
			{
				return;
			}
			this.end_of_input = true;
			this.end_of_json = true;
			if (this.reader_is_owned)
			{
				this.reader.Close();
			}
			this.reader = null;
		}

		// Token: 0x06001E27 RID: 7719 RVA: 0x00092F10 File Offset: 0x00091110
		public bool Read()
		{
			if (this.end_of_input)
			{
				return false;
			}
			if (this.end_of_json)
			{
				this.end_of_json = false;
				this.automaton_stack.Clear();
				this.automaton_stack.Push(65553);
				this.automaton_stack.Push(65543);
			}
			this.parser_in_string = false;
			this.parser_return = false;
			this.token = JsonToken.None;
			this.token_value = null;
			if (!this.read_started)
			{
				this.read_started = true;
				if (!this.ReadToken())
				{
					return false;
				}
			}
			while (!this.parser_return)
			{
				this.current_symbol = this.automaton_stack.Pop();
				this.ProcessSymbol();
				if (this.current_symbol == this.current_input)
				{
					if (!this.ReadToken())
					{
						if (this.automaton_stack.Peek() != 65553)
						{
							throw new JsonException("Input doesn't evaluate to proper JSON text");
						}
						return this.parser_return;
					}
				}
				else
				{
					int[] array;
					try
					{
						array = JsonReader.parse_table[this.current_symbol][this.current_input];
					}
					catch (KeyNotFoundException ex)
					{
						throw new JsonException((ParserToken)this.current_input, ex);
					}
					if (array[0] != 65554)
					{
						for (int i = array.Length - 1; i >= 0; i--)
						{
							this.automaton_stack.Push(array[i]);
						}
					}
				}
			}
			if (this.automaton_stack.Peek() == 65553)
			{
				this.end_of_json = true;
			}
			return true;
		}

		// Token: 0x04000FA9 RID: 4009
		private static IDictionary<int, IDictionary<int, int[]>> parse_table;

		// Token: 0x04000FAA RID: 4010
		private Stack<int> automaton_stack;

		// Token: 0x04000FAB RID: 4011
		private int current_input;

		// Token: 0x04000FAC RID: 4012
		private int current_symbol;

		// Token: 0x04000FAD RID: 4013
		private bool end_of_json;

		// Token: 0x04000FAE RID: 4014
		private bool end_of_input;

		// Token: 0x04000FAF RID: 4015
		private Lexer lexer;

		// Token: 0x04000FB0 RID: 4016
		private bool parser_in_string;

		// Token: 0x04000FB1 RID: 4017
		private bool parser_return;

		// Token: 0x04000FB2 RID: 4018
		private bool read_started;

		// Token: 0x04000FB3 RID: 4019
		private TextReader reader;

		// Token: 0x04000FB4 RID: 4020
		private bool reader_is_owned;

		// Token: 0x04000FB5 RID: 4021
		private bool skip_non_members;

		// Token: 0x04000FB6 RID: 4022
		private object token_value;

		// Token: 0x04000FB7 RID: 4023
		private JsonToken token;
	}
}
