using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace UnityEngine.Purchasing.MiniJSON
{
	// Token: 0x02000007 RID: 7
	public static class Json
	{
		// Token: 0x06000017 RID: 23 RVA: 0x0000223F File Offset: 0x0000043F
		public static object Deserialize(string json)
		{
			if (json == null)
			{
				return null;
			}
			return Json.Parser.Parse(json);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000224C File Offset: 0x0000044C
		public static string Serialize(object obj)
		{
			return Json.Serializer.Serialize(obj);
		}

		// Token: 0x0200000C RID: 12
		private sealed class Parser : IDisposable
		{
			// Token: 0x06000025 RID: 37 RVA: 0x000023FE File Offset: 0x000005FE
			public static bool IsWordBreak(char c)
			{
				return char.IsWhiteSpace(c) || "{}[],:\"".IndexOf(c) != -1;
			}

			// Token: 0x06000026 RID: 38 RVA: 0x0000241B File Offset: 0x0000061B
			private Parser(string jsonString)
			{
				this.json = new StringReader(jsonString);
			}

			// Token: 0x06000027 RID: 39 RVA: 0x00002430 File Offset: 0x00000630
			public static object Parse(string jsonString)
			{
				object obj;
				using (Json.Parser parser = new Json.Parser(jsonString))
				{
					obj = parser.ParseValue();
				}
				return obj;
			}

			// Token: 0x06000028 RID: 40 RVA: 0x00002468 File Offset: 0x00000668
			public void Dispose()
			{
				this.json.Dispose();
				this.json = null;
			}

			// Token: 0x06000029 RID: 41 RVA: 0x0000247C File Offset: 0x0000067C
			private Dictionary<string, object> ParseObject()
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				this.json.Read();
				for (;;)
				{
					Json.Parser.TOKEN nextToken = this.NextToken;
					if (nextToken == Json.Parser.TOKEN.NONE)
					{
						break;
					}
					if (nextToken == Json.Parser.TOKEN.CURLY_CLOSE)
					{
						return dictionary;
					}
					if (nextToken != Json.Parser.TOKEN.COMMA)
					{
						string text = this.ParseString();
						if (text == null)
						{
							goto Block_4;
						}
						if (this.NextToken != Json.Parser.TOKEN.COLON)
						{
							goto Block_5;
						}
						this.json.Read();
						dictionary[text] = this.ParseValue();
					}
				}
				return null;
				Block_4:
				return null;
				Block_5:
				return null;
			}

			// Token: 0x0600002A RID: 42 RVA: 0x000024E4 File Offset: 0x000006E4
			private List<object> ParseArray()
			{
				List<object> list = new List<object>();
				this.json.Read();
				bool flag = true;
				while (flag)
				{
					Json.Parser.TOKEN nextToken = this.NextToken;
					if (nextToken == Json.Parser.TOKEN.NONE)
					{
						return null;
					}
					if (nextToken != Json.Parser.TOKEN.SQUARED_CLOSE)
					{
						if (nextToken != Json.Parser.TOKEN.COMMA)
						{
							object obj = this.ParseByToken(nextToken);
							list.Add(obj);
						}
					}
					else
					{
						flag = false;
					}
				}
				return list;
			}

			// Token: 0x0600002B RID: 43 RVA: 0x00002534 File Offset: 0x00000734
			private object ParseValue()
			{
				Json.Parser.TOKEN nextToken = this.NextToken;
				return this.ParseByToken(nextToken);
			}

			// Token: 0x0600002C RID: 44 RVA: 0x00002550 File Offset: 0x00000750
			private object ParseByToken(Json.Parser.TOKEN token)
			{
				switch (token)
				{
				case Json.Parser.TOKEN.CURLY_OPEN:
					return this.ParseObject();
				case Json.Parser.TOKEN.SQUARED_OPEN:
					return this.ParseArray();
				case Json.Parser.TOKEN.STRING:
					return this.ParseString();
				case Json.Parser.TOKEN.NUMBER:
					return this.ParseNumber();
				case Json.Parser.TOKEN.TRUE:
					return true;
				case Json.Parser.TOKEN.FALSE:
					return false;
				case Json.Parser.TOKEN.NULL:
					return null;
				}
				return null;
			}

			// Token: 0x0600002D RID: 45 RVA: 0x000025C0 File Offset: 0x000007C0
			private string ParseString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				this.json.Read();
				bool flag = true;
				while (flag && this.json.Peek() != -1)
				{
					char c = this.NextChar;
					if (c != '"')
					{
						if (c != '\\')
						{
							stringBuilder.Append(c);
						}
						else if (this.json.Peek() == -1)
						{
							flag = false;
						}
						else
						{
							c = this.NextChar;
							if (c <= '\\')
							{
								if (c == '"' || c == '/' || c == '\\')
								{
									stringBuilder.Append(c);
								}
							}
							else if (c <= 'f')
							{
								if (c != 'b')
								{
									if (c == 'f')
									{
										stringBuilder.Append('\f');
									}
								}
								else
								{
									stringBuilder.Append('\b');
								}
							}
							else if (c != 'n')
							{
								switch (c)
								{
								case 'r':
									stringBuilder.Append('\r');
									break;
								case 't':
									stringBuilder.Append('\t');
									break;
								case 'u':
								{
									char[] array = new char[4];
									for (int i = 0; i < 4; i++)
									{
										array[i] = this.NextChar;
									}
									stringBuilder.Append((char)Convert.ToInt32(new string(array), 16));
									break;
								}
								}
							}
							else
							{
								stringBuilder.Append('\n');
							}
						}
					}
					else
					{
						flag = false;
					}
				}
				return stringBuilder.ToString();
			}

			// Token: 0x0600002E RID: 46 RVA: 0x0000270C File Offset: 0x0000090C
			private object ParseNumber()
			{
				string nextWord = this.NextWord;
				if (nextWord.IndexOf('.') == -1 && nextWord.IndexOf('e') == -1 && nextWord.IndexOf('E') == -1)
				{
					long num;
					long.TryParse(nextWord, NumberStyles.Any, CultureInfo.InvariantCulture, out num);
					return num;
				}
				double num2;
				double.TryParse(nextWord, NumberStyles.Any, CultureInfo.InvariantCulture, out num2);
				return num2;
			}

			// Token: 0x0600002F RID: 47 RVA: 0x00002774 File Offset: 0x00000974
			private void EatWhitespace()
			{
				while (char.IsWhiteSpace(this.PeekChar))
				{
					this.json.Read();
					if (this.json.Peek() == -1)
					{
						break;
					}
				}
			}

			// Token: 0x17000001 RID: 1
			// (get) Token: 0x06000030 RID: 48 RVA: 0x0000279F File Offset: 0x0000099F
			private char PeekChar
			{
				get
				{
					return Convert.ToChar(this.json.Peek());
				}
			}

			// Token: 0x17000002 RID: 2
			// (get) Token: 0x06000031 RID: 49 RVA: 0x000027B1 File Offset: 0x000009B1
			private char NextChar
			{
				get
				{
					return Convert.ToChar(this.json.Read());
				}
			}

			// Token: 0x17000003 RID: 3
			// (get) Token: 0x06000032 RID: 50 RVA: 0x000027C4 File Offset: 0x000009C4
			private string NextWord
			{
				get
				{
					StringBuilder stringBuilder = new StringBuilder();
					while (!Json.Parser.IsWordBreak(this.PeekChar))
					{
						stringBuilder.Append(this.NextChar);
						if (this.json.Peek() == -1)
						{
							break;
						}
					}
					return stringBuilder.ToString();
				}
			}

			// Token: 0x17000004 RID: 4
			// (get) Token: 0x06000033 RID: 51 RVA: 0x00002808 File Offset: 0x00000A08
			private Json.Parser.TOKEN NextToken
			{
				get
				{
					this.EatWhitespace();
					if (this.json.Peek() == -1)
					{
						return Json.Parser.TOKEN.NONE;
					}
					char peekChar = this.PeekChar;
					if (peekChar <= '[')
					{
						switch (peekChar)
						{
						case '"':
							return Json.Parser.TOKEN.STRING;
						case '#':
						case '$':
						case '%':
						case '&':
						case '\'':
						case '(':
						case ')':
						case '*':
						case '+':
						case '.':
						case '/':
							break;
						case ',':
							this.json.Read();
							return Json.Parser.TOKEN.COMMA;
						case '-':
						case '0':
						case '1':
						case '2':
						case '3':
						case '4':
						case '5':
						case '6':
						case '7':
						case '8':
						case '9':
							return Json.Parser.TOKEN.NUMBER;
						case ':':
							return Json.Parser.TOKEN.COLON;
						default:
							if (peekChar == '[')
							{
								return Json.Parser.TOKEN.SQUARED_OPEN;
							}
							break;
						}
					}
					else
					{
						if (peekChar == ']')
						{
							this.json.Read();
							return Json.Parser.TOKEN.SQUARED_CLOSE;
						}
						if (peekChar == '{')
						{
							return Json.Parser.TOKEN.CURLY_OPEN;
						}
						if (peekChar == '}')
						{
							this.json.Read();
							return Json.Parser.TOKEN.CURLY_CLOSE;
						}
					}
					string nextWord = this.NextWord;
					if (nextWord == "false")
					{
						return Json.Parser.TOKEN.FALSE;
					}
					if (nextWord == "true")
					{
						return Json.Parser.TOKEN.TRUE;
					}
					if (!(nextWord == "null"))
					{
						return Json.Parser.TOKEN.NONE;
					}
					return Json.Parser.TOKEN.NULL;
				}
			}

			// Token: 0x0400000C RID: 12
			private const string WORD_BREAK = "{}[],:\"";

			// Token: 0x0400000D RID: 13
			private StringReader json;

			// Token: 0x02000010 RID: 16
			private enum TOKEN
			{
				// Token: 0x04000010 RID: 16
				NONE,
				// Token: 0x04000011 RID: 17
				CURLY_OPEN,
				// Token: 0x04000012 RID: 18
				CURLY_CLOSE,
				// Token: 0x04000013 RID: 19
				SQUARED_OPEN,
				// Token: 0x04000014 RID: 20
				SQUARED_CLOSE,
				// Token: 0x04000015 RID: 21
				COLON,
				// Token: 0x04000016 RID: 22
				COMMA,
				// Token: 0x04000017 RID: 23
				STRING,
				// Token: 0x04000018 RID: 24
				NUMBER,
				// Token: 0x04000019 RID: 25
				TRUE,
				// Token: 0x0400001A RID: 26
				FALSE,
				// Token: 0x0400001B RID: 27
				NULL
			}
		}

		// Token: 0x0200000D RID: 13
		private sealed class Serializer
		{
			// Token: 0x06000034 RID: 52 RVA: 0x0000292A File Offset: 0x00000B2A
			private Serializer()
			{
				this.builder = new StringBuilder();
			}

			// Token: 0x06000035 RID: 53 RVA: 0x0000293D File Offset: 0x00000B3D
			public static string Serialize(object obj)
			{
				Json.Serializer serializer = new Json.Serializer();
				serializer.SerializeValue(obj);
				return serializer.builder.ToString();
			}

			// Token: 0x06000036 RID: 54 RVA: 0x00002958 File Offset: 0x00000B58
			private void SerializeValue(object value)
			{
				if (value == null)
				{
					this.builder.Append("null");
					return;
				}
				string text;
				if ((text = value as string) != null)
				{
					this.SerializeString(text);
					return;
				}
				if (value is bool)
				{
					this.builder.Append(((bool)value) ? "true" : "false");
					return;
				}
				IList list;
				if ((list = value as IList) != null)
				{
					this.SerializeArray(list);
					return;
				}
				IDictionary dictionary;
				if ((dictionary = value as IDictionary) != null)
				{
					this.SerializeObject(dictionary);
					return;
				}
				if (value is char)
				{
					this.SerializeString(new string((char)value, 1));
					return;
				}
				this.SerializeOther(value);
			}

			// Token: 0x06000037 RID: 55 RVA: 0x000029FC File Offset: 0x00000BFC
			private void SerializeObject(IDictionary obj)
			{
				bool flag = true;
				this.builder.Append('{');
				foreach (object obj2 in obj.Keys)
				{
					if (!flag)
					{
						this.builder.Append(',');
					}
					this.SerializeString(obj2.ToString());
					this.builder.Append(':');
					this.SerializeValue(obj[obj2]);
					flag = false;
				}
				this.builder.Append('}');
			}

			// Token: 0x06000038 RID: 56 RVA: 0x00002AA4 File Offset: 0x00000CA4
			private void SerializeArray(IList anArray)
			{
				this.builder.Append('[');
				bool flag = true;
				foreach (object obj in anArray)
				{
					if (!flag)
					{
						this.builder.Append(',');
					}
					this.SerializeValue(obj);
					flag = false;
				}
				this.builder.Append(']');
			}

			// Token: 0x06000039 RID: 57 RVA: 0x00002B24 File Offset: 0x00000D24
			private void SerializeString(string str)
			{
				this.builder.Append('"');
				char[] array = str.ToCharArray();
				int i = 0;
				while (i < array.Length)
				{
					char c = array[i];
					switch (c)
					{
					case '\b':
						this.builder.Append("\\b");
						break;
					case '\t':
						this.builder.Append("\\t");
						break;
					case '\n':
						this.builder.Append("\\n");
						break;
					case '\v':
						goto IL_00E0;
					case '\f':
						this.builder.Append("\\f");
						break;
					case '\r':
						this.builder.Append("\\r");
						break;
					default:
						if (c != '"')
						{
							if (c != '\\')
							{
								goto IL_00E0;
							}
							this.builder.Append("\\\\");
						}
						else
						{
							this.builder.Append("\\\"");
						}
						break;
					}
					IL_0129:
					i++;
					continue;
					IL_00E0:
					int num = Convert.ToInt32(c);
					if (num >= 32 && num <= 126)
					{
						this.builder.Append(c);
						goto IL_0129;
					}
					this.builder.Append("\\u");
					this.builder.Append(num.ToString("x4"));
					goto IL_0129;
				}
				this.builder.Append('"');
			}

			// Token: 0x0600003A RID: 58 RVA: 0x00002C78 File Offset: 0x00000E78
			private void SerializeOther(object value)
			{
				if (value is float)
				{
					this.builder.Append(((float)value).ToString("R", CultureInfo.InvariantCulture));
					return;
				}
				if (value is int || value is uint || value is long || value is sbyte || value is byte || value is short || value is ushort || value is ulong)
				{
					this.builder.Append(value);
					return;
				}
				if (value is double || value is decimal)
				{
					this.builder.Append(Convert.ToDouble(value).ToString("R", CultureInfo.InvariantCulture));
					return;
				}
				this.SerializeString(value.ToString());
			}

			// Token: 0x0400000E RID: 14
			private readonly StringBuilder builder;
		}
	}
}
