using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using Cysharp.Text;
using Newtonsoft.Json;

namespace XRL.UI
{
	// Token: 0x020014BD RID: 5309
	public class GameOption
	{
		// Token: 0x0600D062 RID: 53346 RVA: 0x004CE984 File Offset: 0x004CCB84
		public bool RequiresCapabilityMet()
		{
			return CapabilityManager.HasCapability(this.RequiresCapability);
		}

		// Token: 0x040051CE RID: 20942
		public string ID;

		// Token: 0x040051CF RID: 20943
		public string DisplayText;

		// Token: 0x040051D0 RID: 20944
		public string ConsoleDisplayText;

		// Token: 0x040051D1 RID: 20945
		public string SwitchDisplayText;

		// Token: 0x040051D2 RID: 20946
		public string Category;

		// Token: 0x040051D3 RID: 20947
		public string Type;

		// Token: 0x040051D4 RID: 20948
		public string Default;

		// Token: 0x040051D5 RID: 20949
		public string SearchKeywords;

		// Token: 0x040051D6 RID: 20950
		public CapabilityManager.CapabilityType? RequiresCapability;

		// Token: 0x040051D7 RID: 20951
		public string HelpText;

		// Token: 0x040051D8 RID: 20952
		public MethodInfo OnClick;

		// Token: 0x040051D9 RID: 20953
		public GameOption.RequiresSpec Requires;

		// Token: 0x040051DA RID: 20954
		public string[] DisplayValues = Array.Empty<string>();

		// Token: 0x040051DB RID: 20955
		public string[] Values = Array.Empty<string>();

		// Token: 0x040051DC RID: 20956
		public int Min;

		// Token: 0x040051DD RID: 20957
		public int Max;

		// Token: 0x040051DE RID: 20958
		public int Increment;

		// Token: 0x040051DF RID: 20959
		public bool Restart;

		// Token: 0x02001DD0 RID: 7632
		[JsonConverter(typeof(GameOption.RequiresSpec.JsonConverter))]
		public class RequiresSpec
		{
			// Token: 0x17000FF7 RID: 4087
			// (get) Token: 0x0600F561 RID: 62817 RVA: 0x00555954 File Offset: 0x00553B54
			public bool RequirementsMet
			{
				get
				{
					int i = 0;
					int num = this.Parsed.Length;
					while (i < num)
					{
						GameOption.RequiresSpec.ParsedSpec parsedSpec = this.Parsed[i];
						if (Options.GetOption(parsedSpec.Option, "") == parsedSpec.Value != parsedSpec.Result)
						{
							return false;
						}
						i++;
					}
					return true;
				}
			}

			// Token: 0x0600F562 RID: 62818 RVA: 0x005559A8 File Offset: 0x00553BA8
			public override string ToString()
			{
				string text;
				using (Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder())
				{
					int i = 0;
					int num = this.Parsed.Length;
					while (i < num)
					{
						if (i != 0)
						{
							utf16ValueStringBuilder.Append(',');
						}
						utf16ValueStringBuilder.Append(this.Parsed[i].Option);
						utf16ValueStringBuilder.Append(this.Parsed[i].Result ? "==" : "!=");
						utf16ValueStringBuilder.Append(this.Parsed[i].Value);
						i++;
					}
					text = utf16ValueStringBuilder.ToString();
				}
				return text;
			}

			// Token: 0x0600F563 RID: 62819 RVA: 0x00555A58 File Offset: 0x00553C58
			public string ToString(int Index)
			{
				string text;
				using (Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder())
				{
					utf16ValueStringBuilder.Append(this.Parsed[Index].Option);
					utf16ValueStringBuilder.Append(this.Parsed[Index].Result ? "==" : "!=");
					utf16ValueStringBuilder.Append(this.Parsed[Index].Value);
					text = utf16ValueStringBuilder.ToString();
				}
				return text;
			}

			// Token: 0x0600F564 RID: 62820 RVA: 0x00555AE4 File Offset: 0x00553CE4
			[XmlDataHelper.AttributeParserAttribute(typeof(GameOption.RequiresSpec))]
			public static GameOption.RequiresSpec ParseString(string input)
			{
				if (input.IsNullOrEmpty())
				{
					return null;
				}
				return GameOption.RequiresSpec.ParseInternal(input.Split(',', StringSplitOptions.None));
			}

			// Token: 0x0600F565 RID: 62821 RVA: 0x00555AFE File Offset: 0x00553CFE
			public static GameOption.RequiresSpec ParseArray(string[] input)
			{
				if (input.IsNullOrEmpty<string>())
				{
					return null;
				}
				return GameOption.RequiresSpec.ParseInternal(input);
			}

			// Token: 0x0600F566 RID: 62822 RVA: 0x00555B10 File Offset: 0x00553D10
			private static GameOption.RequiresSpec ParseInternal(string[] Specs)
			{
				GameOption.RequiresSpec.ParsedSpec[] array = new GameOption.RequiresSpec.ParsedSpec[Specs.Length];
				for (int i = 0; i < array.Length; i++)
				{
					Match match = GameOption.RequiresSpec.partParse.Match(Specs[i]);
					if (!match.Success)
					{
						throw new Exception("Could not parse '" + Specs[i] + "' as a valid option state.");
					}
					array[i] = new GameOption.RequiresSpec.ParsedSpec(match.Groups["Option"].Value, match.Groups["Value"].Value, match.Groups["Test"].Value == "==");
				}
				return new GameOption.RequiresSpec
				{
					Parsed = array
				};
			}

			// Token: 0x04007426 RID: 29734
			private GameOption.RequiresSpec.ParsedSpec[] Parsed;

			// Token: 0x04007427 RID: 29735
			private static readonly Regex partParse = new Regex("^\\s*(?<Option>.*?)\\s*(?<Test>[!=]=)\\s*(?<Value>.*?)\\s*$", RegexOptions.Compiled);

			// Token: 0x02001E97 RID: 7831
			private class ParsedSpec : IEquatable<GameOption.RequiresSpec.ParsedSpec>
			{
				// Token: 0x0600F7E0 RID: 63456 RVA: 0x0055D4EB File Offset: 0x0055B6EB
				public ParsedSpec(string Option, string Value, bool Result)
				{
					this.Option = Option;
					this.Value = Value;
					this.Result = Result;
					base..ctor();
				}

				// Token: 0x17001054 RID: 4180
				// (get) Token: 0x0600F7E1 RID: 63457 RVA: 0x0055D508 File Offset: 0x0055B708
				[Nullable(1)]
				[CompilerGenerated]
				protected virtual Type EqualityContract
				{
					[NullableContext(1)]
					[CompilerGenerated]
					get
					{
						return typeof(GameOption.RequiresSpec.ParsedSpec);
					}
				}

				// Token: 0x17001055 RID: 4181
				// (get) Token: 0x0600F7E2 RID: 63458 RVA: 0x0055D514 File Offset: 0x0055B714
				// (set) Token: 0x0600F7E3 RID: 63459 RVA: 0x0055D51C File Offset: 0x0055B71C
				public string Option { get; set; }

				// Token: 0x17001056 RID: 4182
				// (get) Token: 0x0600F7E4 RID: 63460 RVA: 0x0055D525 File Offset: 0x0055B725
				// (set) Token: 0x0600F7E5 RID: 63461 RVA: 0x0055D52D File Offset: 0x0055B72D
				public string Value { get; set; }

				// Token: 0x17001057 RID: 4183
				// (get) Token: 0x0600F7E6 RID: 63462 RVA: 0x0055D536 File Offset: 0x0055B736
				// (set) Token: 0x0600F7E7 RID: 63463 RVA: 0x0055D53E File Offset: 0x0055B73E
				public bool Result { get; set; }

				// Token: 0x0600F7E8 RID: 63464 RVA: 0x0055D548 File Offset: 0x0055B748
				[NullableContext(1)]
				[CompilerGenerated]
				public override string ToString()
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("ParsedSpec");
					stringBuilder.Append(" { ");
					if (this.PrintMembers(stringBuilder))
					{
						stringBuilder.Append(' ');
					}
					stringBuilder.Append('}');
					return stringBuilder.ToString();
				}

				// Token: 0x0600F7E9 RID: 63465 RVA: 0x0055D594 File Offset: 0x0055B794
				[NullableContext(1)]
				[CompilerGenerated]
				protected virtual bool PrintMembers(StringBuilder builder)
				{
					RuntimeHelpers.EnsureSufficientExecutionStack();
					builder.Append("Option = ");
					builder.Append(this.Option);
					builder.Append(", Value = ");
					builder.Append(this.Value);
					builder.Append(", Result = ");
					builder.Append(this.Result.ToString());
					return true;
				}

				// Token: 0x0600F7EA RID: 63466 RVA: 0x0055D600 File Offset: 0x0055B800
				[NullableContext(2)]
				[CompilerGenerated]
				public static bool operator !=(GameOption.RequiresSpec.ParsedSpec left, GameOption.RequiresSpec.ParsedSpec right)
				{
					return !(left == right);
				}

				// Token: 0x0600F7EB RID: 63467 RVA: 0x0055D60C File Offset: 0x0055B80C
				[NullableContext(2)]
				[CompilerGenerated]
				public static bool operator ==(GameOption.RequiresSpec.ParsedSpec left, GameOption.RequiresSpec.ParsedSpec right)
				{
					return left == right || (left != null && left.Equals(right));
				}

				// Token: 0x0600F7EC RID: 63468 RVA: 0x0055D620 File Offset: 0x0055B820
				[CompilerGenerated]
				public override int GetHashCode()
				{
					return ((EqualityComparer<global::System.Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Option>k__BackingField)) * -1521134295 + EqualityComparer<string>.Default.GetHashCode(this.<Value>k__BackingField)) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.<Result>k__BackingField);
				}

				// Token: 0x0600F7ED RID: 63469 RVA: 0x0055D682 File Offset: 0x0055B882
				[NullableContext(2)]
				[CompilerGenerated]
				public override bool Equals(object obj)
				{
					return this.Equals(obj as GameOption.RequiresSpec.ParsedSpec);
				}

				// Token: 0x0600F7EE RID: 63470 RVA: 0x0055D690 File Offset: 0x0055B890
				[NullableContext(2)]
				[CompilerGenerated]
				public virtual bool Equals(GameOption.RequiresSpec.ParsedSpec other)
				{
					return this == other || (other != null && this.EqualityContract == other.EqualityContract && EqualityComparer<string>.Default.Equals(this.<Option>k__BackingField, other.<Option>k__BackingField) && EqualityComparer<string>.Default.Equals(this.<Value>k__BackingField, other.<Value>k__BackingField) && EqualityComparer<bool>.Default.Equals(this.<Result>k__BackingField, other.<Result>k__BackingField));
				}

				// Token: 0x0600F7F0 RID: 63472 RVA: 0x0055D709 File Offset: 0x0055B909
				[CompilerGenerated]
				protected ParsedSpec([Nullable(1)] GameOption.RequiresSpec.ParsedSpec original)
				{
					this.Option = original.<Option>k__BackingField;
					this.Value = original.<Value>k__BackingField;
					this.Result = original.<Result>k__BackingField;
				}

				// Token: 0x0600F7F1 RID: 63473 RVA: 0x0055D735 File Offset: 0x0055B935
				[CompilerGenerated]
				public void Deconstruct(out string Option, out string Value, out bool Result)
				{
					Option = this.Option;
					Value = this.Value;
					Result = this.Result;
				}
			}

			// Token: 0x02001E98 RID: 7832
			public class JsonConverter : JsonConverter<GameOption.RequiresSpec>
			{
				// Token: 0x0600F7F2 RID: 63474 RVA: 0x0055D750 File Offset: 0x0055B950
				public override void WriteJson(JsonWriter writer, GameOption.RequiresSpec value, JsonSerializer serializer)
				{
					int num = value.Parsed.Length;
					if (num == 0)
					{
						writer.WriteValue(null);
						return;
					}
					if (num == 1)
					{
						writer.WriteValue(value.ToString());
						return;
					}
					writer.WriteStartArray();
					for (int i = 0; i < num; i++)
					{
						writer.WriteValue(value.ToString(i));
					}
					writer.WriteEndArray();
				}

				// Token: 0x0600F7F3 RID: 63475 RVA: 0x0055D7A7 File Offset: 0x0055B9A7
				public override GameOption.RequiresSpec ReadJson(JsonReader reader, Type objectType, GameOption.RequiresSpec existingValue, bool hasExistingValue, JsonSerializer serializer)
				{
					if (reader.TokenType == JsonToken.StartArray)
					{
						return GameOption.RequiresSpec.ParseArray(serializer.Deserialize<string[]>(reader));
					}
					return GameOption.RequiresSpec.ParseString((string)reader.Value);
				}
			}
		}
	}
}
