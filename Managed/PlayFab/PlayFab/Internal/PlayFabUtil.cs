using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using UnityEngine;

namespace PlayFab.Internal
{
	// Token: 0x020001AD RID: 429
	public static class PlayFabUtil
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x00015750 File Offset: 0x00013950
		public static string timeStamp
		{
			get
			{
				return DateTime.Now.ToString(PlayFabUtil._defaultDateTimeFormats[9]);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x00015774 File Offset: 0x00013974
		public static string utcTimeStamp
		{
			get
			{
				return DateTime.UtcNow.ToString(PlayFabUtil._defaultDateTimeFormats[2]);
			}
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00015795 File Offset: 0x00013995
		public static string Format(string text, params object[] args)
		{
			if (args.Length == 0)
			{
				return text;
			}
			return string.Format(text, args);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x000157A4 File Offset: 0x000139A4
		public static string ReadAllFileText(string filename)
		{
			if (!File.Exists(filename))
			{
				return string.Empty;
			}
			if (PlayFabUtil._sb == null)
			{
				PlayFabUtil._sb = new StringBuilder();
			}
			PlayFabUtil._sb.Length = 0;
			using (FileStream fileStream = new FileStream(filename, FileMode.Open))
			{
				using (BinaryReader binaryReader = new BinaryReader(fileStream))
				{
					while (binaryReader.BaseStream.Position != binaryReader.BaseStream.Length)
					{
						PlayFabUtil._sb.Append(binaryReader.ReadChar());
					}
				}
			}
			return PlayFabUtil._sb.ToString();
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00015854 File Offset: 0x00013A54
		public static T TryEnumParse<T>(string value, T defaultValue)
		{
			T t;
			try
			{
				t = (T)((object)Enum.Parse(typeof(T), value));
			}
			catch (InvalidCastException)
			{
				t = defaultValue;
			}
			catch (Exception ex)
			{
				Debug.LogError("Enum cast failed with unknown error: " + ex.Message);
				t = defaultValue;
			}
			return t;
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x000158B8 File Offset: 0x00013AB8
		internal static string GetLocalSettingsFileProperty(string propertyKey)
		{
			string text = null;
			string text2 = Path.Combine(Directory.GetCurrentDirectory(), PlayFabUtil._localSettingsFileName);
			if (File.Exists(text2))
			{
				text = PlayFabUtil.ReadAllFileText(text2);
			}
			else
			{
				string text3 = Path.Combine(Path.GetTempPath(), PlayFabUtil._localSettingsFileName);
				if (File.Exists(text3))
				{
					text = PlayFabUtil.ReadAllFileText(text3);
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				Dictionary<string, object> dictionary = PluginManager.GetPlugin<ISerializerPlugin>(PluginContract.PlayFab_Serializer, "").DeserializeObject<Dictionary<string, object>>(text);
				try
				{
					object obj;
					if (dictionary.TryGetValue(propertyKey, out obj))
					{
						return (obj == null) ? null : obj.ToString();
					}
					return null;
				}
				catch (KeyNotFoundException)
				{
					return string.Empty;
				}
			}
			return string.Empty;
		}

		// Token: 0x0400094E RID: 2382
		private static string _localSettingsFileName = "playfab.local.settings.json";

		// Token: 0x0400094F RID: 2383
		public static readonly string[] _defaultDateTimeFormats = new string[]
		{
			"yyyy-MM-ddTHH:mm:ss.FFFFFFZ", "yyyy-MM-ddTHH:mm:ss.FFFFZ", "yyyy-MM-ddTHH:mm:ss.FFFZ", "yyyy-MM-ddTHH:mm:ss.FFZ", "yyyy-MM-ddTHH:mm:ssZ", "yyyy-MM-dd HH:mm:ssZ", "yyyy-MM-dd HH:mm:ss.FFFFFF", "yyyy-MM-dd HH:mm:ss.FFFF", "yyyy-MM-dd HH:mm:ss.FFF", "yyyy-MM-dd HH:mm:ss.FF",
			"yyyy-MM-dd HH:mm:ss", "yyyy-MM-dd HH:mm.ss.FFFF", "yyyy-MM-dd HH:mm.ss.FFF", "yyyy-MM-dd HH:mm.ss.FF", "yyyy-MM-dd HH:mm.ss"
		};

		// Token: 0x04000950 RID: 2384
		public const int DEFAULT_UTC_OUTPUT_INDEX = 2;

		// Token: 0x04000951 RID: 2385
		public const int DEFAULT_LOCAL_OUTPUT_INDEX = 9;

		// Token: 0x04000952 RID: 2386
		public static DateTimeStyles DateTimeStyles = DateTimeStyles.RoundtripKind;

		// Token: 0x04000953 RID: 2387
		[ThreadStatic]
		private static StringBuilder _sb;
	}
}
