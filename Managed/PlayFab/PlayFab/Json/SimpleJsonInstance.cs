using System;
using System.Globalization;
using PlayFab.Internal;

namespace PlayFab.Json
{
	// Token: 0x02000034 RID: 52
	public class SimpleJsonInstance : ISerializerPlugin, IPlayFabPlugin
	{
		// Token: 0x0600031E RID: 798 RVA: 0x000104EA File Offset: 0x0000E6EA
		public T DeserializeObject<T>(string json)
		{
			return PlayFabSimpleJson.DeserializeObject<T>(json, SimpleJsonInstance.ApiSerializerStrategy);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000104F7 File Offset: 0x0000E6F7
		public T DeserializeObject<T>(string json, object jsonSerializerStrategy)
		{
			return PlayFabSimpleJson.DeserializeObject<T>(json, (IJsonSerializerStrategy)jsonSerializerStrategy);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00010505 File Offset: 0x0000E705
		public object DeserializeObject(string json)
		{
			return PlayFabSimpleJson.DeserializeObject(json, typeof(object), SimpleJsonInstance.ApiSerializerStrategy);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0001051C File Offset: 0x0000E71C
		public string SerializeObject(object json)
		{
			return PlayFabSimpleJson.SerializeObject(json, SimpleJsonInstance.ApiSerializerStrategy);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00010529 File Offset: 0x0000E729
		public string SerializeObject(object json, object jsonSerializerStrategy)
		{
			return PlayFabSimpleJson.SerializeObject(json, (IJsonSerializerStrategy)jsonSerializerStrategy);
		}

		// Token: 0x040002D4 RID: 724
		public static SimpleJsonInstance.PlayFabSimpleJsonCuztomization ApiSerializerStrategy = new SimpleJsonInstance.PlayFabSimpleJsonCuztomization();

		// Token: 0x02000345 RID: 837
		public class PlayFabSimpleJsonCuztomization : PocoJsonSerializerStrategy
		{
			// Token: 0x06000B5F RID: 2911 RVA: 0x00037BFC File Offset: 0x00035DFC
			public override object DeserializeObject(object value, Type type)
			{
				string text = value as string;
				if (text == null)
				{
					return base.DeserializeObject(value, type);
				}
				Type underlyingType = Nullable.GetUnderlyingType(type);
				if (underlyingType != null)
				{
					return this.DeserializeObject(value, underlyingType);
				}
				if (type.GetTypeInfo().IsEnum)
				{
					return Enum.Parse(type, (string)value, true);
				}
				double num;
				if (type == typeof(DateTime))
				{
					DateTime dateTime;
					if (DateTime.TryParseExact(text, PlayFabUtil._defaultDateTimeFormats, CultureInfo.InvariantCulture, PlayFabUtil.DateTimeStyles, out dateTime))
					{
						return dateTime;
					}
				}
				else if (type == typeof(DateTimeOffset))
				{
					DateTimeOffset dateTimeOffset;
					if (DateTimeOffset.TryParseExact(text, PlayFabUtil._defaultDateTimeFormats, CultureInfo.InvariantCulture, PlayFabUtil.DateTimeStyles, out dateTimeOffset))
					{
						return dateTimeOffset;
					}
				}
				else if (type == typeof(TimeSpan) && double.TryParse(text, out num))
				{
					return TimeSpan.FromSeconds(num);
				}
				return base.DeserializeObject(value, type);
			}

			// Token: 0x06000B60 RID: 2912 RVA: 0x00037CE8 File Offset: 0x00035EE8
			protected override bool TrySerializeKnownTypes(object input, out object output)
			{
				if (input.GetType().GetTypeInfo().IsEnum)
				{
					output = input.ToString();
					return true;
				}
				if (input is DateTime)
				{
					output = ((DateTime)input).ToString(PlayFabUtil._defaultDateTimeFormats[2], CultureInfo.InvariantCulture);
					return true;
				}
				if (input is DateTimeOffset)
				{
					output = ((DateTimeOffset)input).ToString(PlayFabUtil._defaultDateTimeFormats[2], CultureInfo.InvariantCulture);
					return true;
				}
				if (input is TimeSpan)
				{
					output = ((TimeSpan)input).TotalSeconds;
					return true;
				}
				return base.TrySerializeKnownTypes(input, out output);
			}
		}
	}
}
