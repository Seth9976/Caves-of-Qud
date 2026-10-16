using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace PlayFab.Json
{
	// Token: 0x02000039 RID: 57
	[GeneratedCode("simple-json", "1.0.0")]
	public static class PlayFabSimpleJson
	{
		// Token: 0x0600033F RID: 831 RVA: 0x000107C8 File Offset: 0x0000E9C8
		static PlayFabSimpleJson()
		{
			PlayFabSimpleJson.EscapeTable[34] = '"';
			PlayFabSimpleJson.EscapeTable[92] = '\\';
			PlayFabSimpleJson.EscapeTable[8] = 'b';
			PlayFabSimpleJson.EscapeTable[12] = 'f';
			PlayFabSimpleJson.EscapeTable[10] = 'n';
			PlayFabSimpleJson.EscapeTable[13] = 'r';
			PlayFabSimpleJson.EscapeTable[9] = 't';
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00010908 File Offset: 0x0000EB08
		public static object DeserializeObject(string json)
		{
			object obj;
			if (PlayFabSimpleJson.TryDeserializeObject(json, out obj))
			{
				return obj;
			}
			throw new SerializationException("Invalid JSON string");
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0001092C File Offset: 0x0000EB2C
		public static bool TryDeserializeObject(string json, out object obj)
		{
			bool flag = true;
			if (json != null)
			{
				int num = 0;
				obj = PlayFabSimpleJson.ParseValue(json, ref num, ref flag);
			}
			else
			{
				obj = null;
			}
			return flag;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00010954 File Offset: 0x0000EB54
		public static object DeserializeObject(string json, Type type, IJsonSerializerStrategy jsonSerializerStrategy = null)
		{
			object obj = PlayFabSimpleJson.DeserializeObject(json);
			if (type == null || (obj != null && ReflectionUtils.IsAssignableFrom(obj.GetType(), type)))
			{
				return obj;
			}
			return (jsonSerializerStrategy ?? PlayFabSimpleJson.CurrentJsonSerializerStrategy).DeserializeObject(obj, type);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00010995 File Offset: 0x0000EB95
		public static T DeserializeObject<T>(string json, IJsonSerializerStrategy jsonSerializerStrategy = null)
		{
			return (T)((object)PlayFabSimpleJson.DeserializeObject(json, typeof(T), jsonSerializerStrategy));
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000109B0 File Offset: 0x0000EBB0
		public static string SerializeObject(object json, IJsonSerializerStrategy jsonSerializerStrategy = null)
		{
			if (PlayFabSimpleJson._serializeObjectBuilder == null)
			{
				PlayFabSimpleJson._serializeObjectBuilder = new StringBuilder(2000);
			}
			PlayFabSimpleJson._serializeObjectBuilder.Length = 0;
			if (jsonSerializerStrategy == null)
			{
				jsonSerializerStrategy = PlayFabSimpleJson.CurrentJsonSerializerStrategy;
			}
			if (!PlayFabSimpleJson.SerializeValue(jsonSerializerStrategy, json, PlayFabSimpleJson._serializeObjectBuilder))
			{
				return null;
			}
			return PlayFabSimpleJson._serializeObjectBuilder.ToString();
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00010A04 File Offset: 0x0000EC04
		public static string EscapeToJavascriptString(string jsonString)
		{
			if (string.IsNullOrEmpty(jsonString))
			{
				return jsonString;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int i = 0;
			while (i < jsonString.Length)
			{
				char c = jsonString[i++];
				if (c == '\\')
				{
					if (jsonString.Length - i >= 2)
					{
						char c2 = jsonString[i];
						if (c2 == '\\')
						{
							stringBuilder.Append('\\');
							i++;
						}
						else if (c2 == '"')
						{
							stringBuilder.Append("\"");
							i++;
						}
						else if (c2 == 't')
						{
							stringBuilder.Append('\t');
							i++;
						}
						else if (c2 == 'b')
						{
							stringBuilder.Append('\b');
							i++;
						}
						else if (c2 == 'n')
						{
							stringBuilder.Append('\n');
							i++;
						}
						else if (c2 == 'r')
						{
							stringBuilder.Append('\r');
							i++;
						}
					}
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00010AE8 File Offset: 0x0000ECE8
		private static IDictionary<string, object> ParseObject(string json, ref int index, ref bool success)
		{
			IDictionary<string, object> dictionary = new JsonObject();
			PlayFabSimpleJson.NextToken(json, ref index);
			bool flag = false;
			while (!flag)
			{
				PlayFabSimpleJson.TokenType tokenType = PlayFabSimpleJson.LookAhead(json, index);
				if (tokenType == PlayFabSimpleJson.TokenType.NONE)
				{
					success = false;
					return null;
				}
				if (tokenType == PlayFabSimpleJson.TokenType.COMMA)
				{
					PlayFabSimpleJson.NextToken(json, ref index);
				}
				else
				{
					if (tokenType == PlayFabSimpleJson.TokenType.CURLY_CLOSE)
					{
						PlayFabSimpleJson.NextToken(json, ref index);
						return dictionary;
					}
					string text = PlayFabSimpleJson.ParseString(json, ref index, ref success);
					if (!success)
					{
						success = false;
						return null;
					}
					tokenType = PlayFabSimpleJson.NextToken(json, ref index);
					if (tokenType != PlayFabSimpleJson.TokenType.COLON)
					{
						success = false;
						return null;
					}
					object obj = PlayFabSimpleJson.ParseValue(json, ref index, ref success);
					if (!success)
					{
						success = false;
						return null;
					}
					dictionary[text] = obj;
				}
			}
			return dictionary;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00010B78 File Offset: 0x0000ED78
		private static JsonArray ParseArray(string json, ref int index, ref bool success)
		{
			JsonArray jsonArray = new JsonArray();
			PlayFabSimpleJson.NextToken(json, ref index);
			bool flag = false;
			while (!flag)
			{
				PlayFabSimpleJson.TokenType tokenType = PlayFabSimpleJson.LookAhead(json, index);
				if (tokenType == PlayFabSimpleJson.TokenType.NONE)
				{
					success = false;
					return null;
				}
				if (tokenType == PlayFabSimpleJson.TokenType.COMMA)
				{
					PlayFabSimpleJson.NextToken(json, ref index);
				}
				else
				{
					if (tokenType == PlayFabSimpleJson.TokenType.SQUARED_CLOSE)
					{
						PlayFabSimpleJson.NextToken(json, ref index);
						break;
					}
					object obj = PlayFabSimpleJson.ParseValue(json, ref index, ref success);
					if (!success)
					{
						return null;
					}
					jsonArray.Add(obj);
				}
			}
			return jsonArray;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00010BE0 File Offset: 0x0000EDE0
		private static object ParseValue(string json, ref int index, ref bool success)
		{
			switch (PlayFabSimpleJson.LookAhead(json, index))
			{
			case PlayFabSimpleJson.TokenType.CURLY_OPEN:
				return PlayFabSimpleJson.ParseObject(json, ref index, ref success);
			case PlayFabSimpleJson.TokenType.SQUARED_OPEN:
				return PlayFabSimpleJson.ParseArray(json, ref index, ref success);
			case PlayFabSimpleJson.TokenType.STRING:
				return PlayFabSimpleJson.ParseString(json, ref index, ref success);
			case PlayFabSimpleJson.TokenType.NUMBER:
				return PlayFabSimpleJson.ParseNumber(json, ref index, ref success);
			case PlayFabSimpleJson.TokenType.TRUE:
				PlayFabSimpleJson.NextToken(json, ref index);
				return true;
			case PlayFabSimpleJson.TokenType.FALSE:
				PlayFabSimpleJson.NextToken(json, ref index);
				return false;
			case PlayFabSimpleJson.TokenType.NULL:
				PlayFabSimpleJson.NextToken(json, ref index);
				return null;
			}
			success = false;
			return null;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00010C80 File Offset: 0x0000EE80
		private static string ParseString(string json, ref int index, ref bool success)
		{
			if (PlayFabSimpleJson._parseStringBuilder == null)
			{
				PlayFabSimpleJson._parseStringBuilder = new StringBuilder(2000);
			}
			PlayFabSimpleJson._parseStringBuilder.Length = 0;
			PlayFabSimpleJson.EatWhitespace(json, ref index);
			int num = index;
			index = num + 1;
			char c = json[num];
			bool flag = false;
			while (!flag && index != json.Length)
			{
				num = index;
				index = num + 1;
				c = json[num];
				if (c == '"')
				{
					flag = true;
					break;
				}
				if (c == '\\')
				{
					if (index == json.Length)
					{
						break;
					}
					num = index;
					index = num + 1;
					c = json[num];
					if (c == '"')
					{
						PlayFabSimpleJson._parseStringBuilder.Append('"');
					}
					else if (c == '\\')
					{
						PlayFabSimpleJson._parseStringBuilder.Append('\\');
					}
					else if (c == '/')
					{
						PlayFabSimpleJson._parseStringBuilder.Append('/');
					}
					else if (c == 'b')
					{
						PlayFabSimpleJson._parseStringBuilder.Append('\b');
					}
					else if (c == 'f')
					{
						PlayFabSimpleJson._parseStringBuilder.Append('\f');
					}
					else if (c == 'n')
					{
						PlayFabSimpleJson._parseStringBuilder.Append('\n');
					}
					else if (c == 'r')
					{
						PlayFabSimpleJson._parseStringBuilder.Append('\r');
					}
					else if (c == 't')
					{
						PlayFabSimpleJson._parseStringBuilder.Append('\t');
					}
					else if (c == 'u')
					{
						if (json.Length - index < 4)
						{
							break;
						}
						uint num2;
						if (!(success = uint.TryParse(json.Substring(index, 4), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num2)))
						{
							return "";
						}
						if (55296U <= num2 && num2 <= 56319U)
						{
							index += 4;
							uint num3;
							if (json.Length - index < 6 || !(json.Substring(index, 2) == "\\u") || !uint.TryParse(json.Substring(index + 2, 4), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num3) || 56320U > num3 || num3 > 57343U)
							{
								success = false;
								return "";
							}
							PlayFabSimpleJson._parseStringBuilder.Append((char)num2);
							PlayFabSimpleJson._parseStringBuilder.Append((char)num3);
							index += 6;
						}
						else
						{
							PlayFabSimpleJson._parseStringBuilder.Append(PlayFabSimpleJson.ConvertFromUtf32((int)num2));
							index += 4;
						}
					}
				}
				else
				{
					PlayFabSimpleJson._parseStringBuilder.Append(c);
				}
			}
			if (!flag)
			{
				success = false;
				return null;
			}
			return PlayFabSimpleJson._parseStringBuilder.ToString();
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00010EE8 File Offset: 0x0000F0E8
		private static string ConvertFromUtf32(int utf32)
		{
			if (utf32 < 0 || utf32 > 1114111)
			{
				throw new ArgumentOutOfRangeException("utf32", "The argument must be from 0 to 0x10FFFF.");
			}
			if (55296 <= utf32 && utf32 <= 57343)
			{
				throw new ArgumentOutOfRangeException("utf32", "The argument must not be in surrogate pair range.");
			}
			if (utf32 < 65536)
			{
				return new string((char)utf32, 1);
			}
			utf32 -= 65536;
			return new string(new char[]
			{
				(char)((utf32 >> 10) + 55296),
				(char)(utf32 % 1024 + 56320)
			});
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00010F78 File Offset: 0x0000F178
		private static object ParseNumber(string json, ref int index, ref bool success)
		{
			PlayFabSimpleJson.EatWhitespace(json, ref index);
			int lastIndexOfNumber = PlayFabSimpleJson.GetLastIndexOfNumber(json, index);
			int num = lastIndexOfNumber - index + 1;
			string text = json.Substring(index, num);
			object obj;
			if (text.IndexOf(".", StringComparison.OrdinalIgnoreCase) != -1 || text.IndexOf("e", StringComparison.OrdinalIgnoreCase) != -1)
			{
				double num2;
				success = double.TryParse(json.Substring(index, num), NumberStyles.Any, CultureInfo.InvariantCulture, out num2);
				obj = num2;
			}
			else if (text.IndexOf("-", StringComparison.OrdinalIgnoreCase) == -1)
			{
				ulong num3;
				success = ulong.TryParse(json.Substring(index, num), NumberStyles.Any, CultureInfo.InvariantCulture, out num3);
				obj = num3;
			}
			else
			{
				long num4;
				success = long.TryParse(json.Substring(index, num), NumberStyles.Any, CultureInfo.InvariantCulture, out num4);
				obj = num4;
			}
			index = lastIndexOfNumber + 1;
			return obj;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0001104C File Offset: 0x0000F24C
		private static int GetLastIndexOfNumber(string json, int index)
		{
			int num = index;
			while (num < json.Length && "0123456789+-.eE".IndexOf(json[num]) != -1)
			{
				num++;
			}
			return num - 1;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x00011081 File Offset: 0x0000F281
		private static void EatWhitespace(string json, ref int index)
		{
			while (index < json.Length && " \t\n\r\b\f".IndexOf(json[index]) != -1)
			{
				index++;
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x000110AC File Offset: 0x0000F2AC
		private static PlayFabSimpleJson.TokenType LookAhead(string json, int index)
		{
			int num = index;
			return PlayFabSimpleJson.NextToken(json, ref num);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x000110C4 File Offset: 0x0000F2C4
		private static PlayFabSimpleJson.TokenType NextToken(string json, ref int index)
		{
			PlayFabSimpleJson.EatWhitespace(json, ref index);
			if (index == json.Length)
			{
				return PlayFabSimpleJson.TokenType.NONE;
			}
			char c = json[index];
			index++;
			if (c <= '[')
			{
				switch (c)
				{
				case '"':
					return PlayFabSimpleJson.TokenType.STRING;
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
					return PlayFabSimpleJson.TokenType.COMMA;
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
					return PlayFabSimpleJson.TokenType.NUMBER;
				case ':':
					return PlayFabSimpleJson.TokenType.COLON;
				default:
					if (c == '[')
					{
						return PlayFabSimpleJson.TokenType.SQUARED_OPEN;
					}
					break;
				}
			}
			else
			{
				if (c == ']')
				{
					return PlayFabSimpleJson.TokenType.SQUARED_CLOSE;
				}
				if (c == '{')
				{
					return PlayFabSimpleJson.TokenType.CURLY_OPEN;
				}
				if (c == '}')
				{
					return PlayFabSimpleJson.TokenType.CURLY_CLOSE;
				}
			}
			index--;
			int num = json.Length - index;
			if (num >= 5 && json[index] == 'f' && json[index + 1] == 'a' && json[index + 2] == 'l' && json[index + 3] == 's' && json[index + 4] == 'e')
			{
				index += 5;
				return PlayFabSimpleJson.TokenType.FALSE;
			}
			if (num >= 4 && json[index] == 't' && json[index + 1] == 'r' && json[index + 2] == 'u' && json[index + 3] == 'e')
			{
				index += 4;
				return PlayFabSimpleJson.TokenType.TRUE;
			}
			if (num >= 4 && json[index] == 'n' && json[index + 1] == 'u' && json[index + 2] == 'l' && json[index + 3] == 'l')
			{
				index += 4;
				return PlayFabSimpleJson.TokenType.NULL;
			}
			return PlayFabSimpleJson.TokenType.NONE;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00011278 File Offset: 0x0000F478
		private static bool SerializeValue(IJsonSerializerStrategy jsonSerializerStrategy, object value, StringBuilder builder)
		{
			bool flag = true;
			string text = value as string;
			if (value == null)
			{
				builder.Append("null");
			}
			else if (text != null)
			{
				flag = PlayFabSimpleJson.SerializeString(text, builder);
			}
			else
			{
				IDictionary<string, object> dictionary = value as IDictionary<string, object>;
				Type type = value.GetType();
				Type[] genericTypeArguments = ReflectionUtils.GetGenericTypeArguments(type);
				if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Dictionary<, >) && genericTypeArguments[0] == typeof(string))
				{
					IDictionary dictionary2 = value as IDictionary;
					flag = PlayFabSimpleJson.SerializeObject(jsonSerializerStrategy, dictionary2.Keys, dictionary2.Values, builder);
				}
				else if (dictionary != null)
				{
					flag = PlayFabSimpleJson.SerializeObject(jsonSerializerStrategy, dictionary.Keys, dictionary.Values, builder);
				}
				else
				{
					IDictionary<string, string> dictionary3 = value as IDictionary<string, string>;
					if (dictionary3 != null)
					{
						flag = PlayFabSimpleJson.SerializeObject(jsonSerializerStrategy, dictionary3.Keys, dictionary3.Values, builder);
					}
					else
					{
						IEnumerable enumerable = value as IEnumerable;
						if (enumerable != null)
						{
							flag = PlayFabSimpleJson.SerializeArray(jsonSerializerStrategy, enumerable, builder);
						}
						else if (PlayFabSimpleJson.IsNumeric(value))
						{
							flag = PlayFabSimpleJson.SerializeNumber(value, builder);
						}
						else if (value is bool)
						{
							builder.Append(((bool)value) ? "true" : "false");
						}
						else
						{
							object obj;
							flag = jsonSerializerStrategy.TrySerializeNonPrimitiveObject(value, out obj);
							if (flag)
							{
								PlayFabSimpleJson.SerializeValue(jsonSerializerStrategy, obj, builder);
							}
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000113D0 File Offset: 0x0000F5D0
		private static bool SerializeObject(IJsonSerializerStrategy jsonSerializerStrategy, IEnumerable keys, IEnumerable values, StringBuilder builder)
		{
			builder.Append("{");
			IEnumerator enumerator = keys.GetEnumerator();
			IEnumerator enumerator2 = values.GetEnumerator();
			bool flag = true;
			while (enumerator.MoveNext() && enumerator2.MoveNext())
			{
				object obj = enumerator.Current;
				object obj2 = enumerator2.Current;
				if (!flag)
				{
					builder.Append(",");
				}
				string text = obj as string;
				if (text != null)
				{
					PlayFabSimpleJson.SerializeString(text, builder);
				}
				else if (!PlayFabSimpleJson.SerializeValue(jsonSerializerStrategy, obj2, builder))
				{
					return false;
				}
				builder.Append(":");
				if (!PlayFabSimpleJson.SerializeValue(jsonSerializerStrategy, obj2, builder))
				{
					return false;
				}
				flag = false;
			}
			builder.Append("}");
			return true;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00011470 File Offset: 0x0000F670
		private static bool SerializeArray(IJsonSerializerStrategy jsonSerializerStrategy, IEnumerable anArray, StringBuilder builder)
		{
			builder.Append("[");
			bool flag = true;
			foreach (object obj in anArray)
			{
				if (!flag)
				{
					builder.Append(",");
				}
				if (!PlayFabSimpleJson.SerializeValue(jsonSerializerStrategy, obj, builder))
				{
					return false;
				}
				flag = false;
			}
			builder.Append("]");
			return true;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x000114F8 File Offset: 0x0000F6F8
		private static bool SerializeString(string aString, StringBuilder builder)
		{
			if (aString.IndexOfAny(PlayFabSimpleJson.EscapeCharacters) == -1)
			{
				builder.Append('"');
				builder.Append(aString);
				builder.Append('"');
				return true;
			}
			builder.Append('"');
			int num = 0;
			char[] array = aString.ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				char c = array[i];
				if ((int)c >= PlayFabSimpleJson.EscapeTable.Length || PlayFabSimpleJson.EscapeTable[(int)c] == '\0')
				{
					num++;
				}
				else
				{
					if (num > 0)
					{
						builder.Append(array, i - num, num);
						num = 0;
					}
					builder.Append('\\');
					builder.Append(PlayFabSimpleJson.EscapeTable[(int)c]);
				}
			}
			if (num > 0)
			{
				builder.Append(array, array.Length - num, num);
			}
			builder.Append('"');
			return true;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x000115B4 File Offset: 0x0000F7B4
		private static bool SerializeNumber(object number, StringBuilder builder)
		{
			if (number is decimal)
			{
				builder.Append(((decimal)number).ToString("R", CultureInfo.InvariantCulture));
			}
			else if (number is double)
			{
				builder.Append(((double)number).ToString("R", CultureInfo.InvariantCulture));
			}
			else if (number is float)
			{
				builder.Append(((float)number).ToString("R", CultureInfo.InvariantCulture));
			}
			else if (PlayFabSimpleJson.NumberTypes.IndexOf(number.GetType()) != -1)
			{
				builder.Append(number);
			}
			return true;
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00011658 File Offset: 0x0000F858
		private static bool IsNumeric(object value)
		{
			return value is sbyte || value is byte || value is short || value is ushort || value is int || value is uint || value is long || value is ulong || value is float || value is double || value is decimal;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000356 RID: 854 RVA: 0x000116D4 File Offset: 0x0000F8D4
		// (set) Token: 0x06000357 RID: 855 RVA: 0x000116EA File Offset: 0x0000F8EA
		public static IJsonSerializerStrategy CurrentJsonSerializerStrategy
		{
			get
			{
				IJsonSerializerStrategy jsonSerializerStrategy;
				if ((jsonSerializerStrategy = PlayFabSimpleJson._currentJsonSerializerStrategy) == null)
				{
					jsonSerializerStrategy = (PlayFabSimpleJson._currentJsonSerializerStrategy = PlayFabSimpleJson.PocoJsonSerializerStrategy);
				}
				return jsonSerializerStrategy;
			}
			set
			{
				PlayFabSimpleJson._currentJsonSerializerStrategy = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000358 RID: 856 RVA: 0x000116F2 File Offset: 0x0000F8F2
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static PocoJsonSerializerStrategy PocoJsonSerializerStrategy
		{
			get
			{
				PocoJsonSerializerStrategy pocoJsonSerializerStrategy;
				if ((pocoJsonSerializerStrategy = PlayFabSimpleJson._pocoJsonSerializerStrategy) == null)
				{
					pocoJsonSerializerStrategy = (PlayFabSimpleJson._pocoJsonSerializerStrategy = new PocoJsonSerializerStrategy());
				}
				return pocoJsonSerializerStrategy;
			}
		}

		// Token: 0x040002DC RID: 732
		private const int BUILDER_INIT = 2000;

		// Token: 0x040002DD RID: 733
		private static readonly char[] EscapeTable = new char[93];

		// Token: 0x040002DE RID: 734
		private static readonly char[] EscapeCharacters = new char[] { '"', '\\', '\b', '\f', '\n', '\r', '\t' };

		// Token: 0x040002DF RID: 735
		internal static readonly List<Type> NumberTypes = new List<Type>
		{
			typeof(bool),
			typeof(byte),
			typeof(ushort),
			typeof(uint),
			typeof(ulong),
			typeof(sbyte),
			typeof(short),
			typeof(int),
			typeof(long),
			typeof(double),
			typeof(float),
			typeof(decimal)
		};

		// Token: 0x040002E0 RID: 736
		[ThreadStatic]
		private static StringBuilder _serializeObjectBuilder;

		// Token: 0x040002E1 RID: 737
		[ThreadStatic]
		private static StringBuilder _parseStringBuilder;

		// Token: 0x040002E2 RID: 738
		private static IJsonSerializerStrategy _currentJsonSerializerStrategy;

		// Token: 0x040002E3 RID: 739
		private static PocoJsonSerializerStrategy _pocoJsonSerializerStrategy;

		// Token: 0x02000346 RID: 838
		private enum TokenType : byte
		{
			// Token: 0x04001255 RID: 4693
			NONE,
			// Token: 0x04001256 RID: 4694
			CURLY_OPEN,
			// Token: 0x04001257 RID: 4695
			CURLY_CLOSE,
			// Token: 0x04001258 RID: 4696
			SQUARED_OPEN,
			// Token: 0x04001259 RID: 4697
			SQUARED_CLOSE,
			// Token: 0x0400125A RID: 4698
			COLON,
			// Token: 0x0400125B RID: 4699
			COMMA,
			// Token: 0x0400125C RID: 4700
			STRING,
			// Token: 0x0400125D RID: 4701
			NUMBER,
			// Token: 0x0400125E RID: 4702
			TRUE,
			// Token: 0x0400125F RID: 4703
			FALSE,
			// Token: 0x04001260 RID: 4704
			NULL
		}
	}
}
