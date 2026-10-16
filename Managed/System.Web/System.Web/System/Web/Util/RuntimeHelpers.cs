using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Configuration;

namespace System.Web.Util
{
	// Token: 0x0200013F RID: 319
	internal static class RuntimeHelpers
	{
		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x000274DF File Offset: 0x000256DF
		// (set) Token: 0x06000E7F RID: 3711 RVA: 0x000274E6 File Offset: 0x000256E6
		public static bool CaseInsensitive { get; private set; }

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x000274F0 File Offset: 0x000256F0
		public static bool DebuggingEnabled
		{
			get
			{
				CompilationSection compilationSection = WebConfigurationManager.GetSection("system.web/compilation") as CompilationSection;
				return compilationSection != null && compilationSection.Debug;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x00027518 File Offset: 0x00025718
		// (set) Token: 0x06000E82 RID: 3714 RVA: 0x0002751F File Offset: 0x0002571F
		public static IEqualityComparer<string> StringEqualityComparer { get; private set; }

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x06000E83 RID: 3715 RVA: 0x00027527 File Offset: 0x00025727
		// (set) Token: 0x06000E84 RID: 3716 RVA: 0x0002752E File Offset: 0x0002572E
		public static IEqualityComparer<string> StringEqualityComparerCulture { get; private set; }

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x00027536 File Offset: 0x00025736
		// (set) Token: 0x06000E86 RID: 3718 RVA: 0x0002753D File Offset: 0x0002573D
		public static bool IsUncShare { get; private set; }

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x00027545 File Offset: 0x00025745
		// (set) Token: 0x06000E88 RID: 3720 RVA: 0x0002754C File Offset: 0x0002574C
		public static string MonoVersion { get; private set; }

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06000E89 RID: 3721 RVA: 0x00027554 File Offset: 0x00025754
		// (set) Token: 0x06000E8A RID: 3722 RVA: 0x0002755B File Offset: 0x0002575B
		public static bool RunningOnWindows { get; private set; }

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x00027563 File Offset: 0x00025763
		// (set) Token: 0x06000E8C RID: 3724 RVA: 0x0002756A File Offset: 0x0002576A
		public static StringComparison StringComparison { get; private set; }

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x00027572 File Offset: 0x00025772
		// (set) Token: 0x06000E8E RID: 3726 RVA: 0x00027579 File Offset: 0x00025779
		public static StringComparison StringComparisonCulture { get; private set; }

		// Token: 0x06000E8F RID: 3727 RVA: 0x00027584 File Offset: 0x00025784
		static RuntimeHelpers()
		{
			PlatformID platform = Environment.OSVersion.Platform;
			RuntimeHelpers.RunningOnWindows = platform != (PlatformID)128 && platform != PlatformID.Unix && platform != PlatformID.MacOSX;
			if (RuntimeHelpers.RunningOnWindows)
			{
				RuntimeHelpers.CaseInsensitive = true;
				string text = AppDomain.CurrentDomain.GetData(".appPath") as string;
				if (string.IsNullOrEmpty(text))
				{
					goto IL_00D9;
				}
				try
				{
					RuntimeHelpers.IsUncShare = new Uri(text).IsUnc;
					goto IL_00D9;
				}
				catch
				{
					goto IL_00D9;
				}
			}
			string environmentVariable = Environment.GetEnvironmentVariable("MONO_IOMAP");
			if (!string.IsNullOrEmpty(environmentVariable))
			{
				if (environmentVariable == "all")
				{
					RuntimeHelpers.CaseInsensitive = true;
				}
				else
				{
					foreach (string text2 in environmentVariable.Split(':', StringSplitOptions.None))
					{
						if (text2 == "all" || text2 == "case")
						{
							RuntimeHelpers.CaseInsensitive = true;
							break;
						}
					}
				}
			}
			IL_00D9:
			if (RuntimeHelpers.CaseInsensitive)
			{
				RuntimeHelpers.StringEqualityComparer = StringComparer.OrdinalIgnoreCase;
				RuntimeHelpers.StringEqualityComparerCulture = StringComparer.CurrentCultureIgnoreCase;
				RuntimeHelpers.StringComparison = StringComparison.OrdinalIgnoreCase;
				RuntimeHelpers.StringComparisonCulture = StringComparison.CurrentCultureIgnoreCase;
			}
			else
			{
				RuntimeHelpers.StringEqualityComparer = StringComparer.Ordinal;
				RuntimeHelpers.StringEqualityComparerCulture = StringComparer.CurrentCulture;
				RuntimeHelpers.StringComparison = StringComparison.Ordinal;
				RuntimeHelpers.StringComparisonCulture = StringComparison.CurrentCulture;
			}
			string text3 = null;
			try
			{
				Type type = Type.GetType("Mono.Runtime", false);
				if (type != null)
				{
					MethodInfo method = type.GetMethod("GetDisplayName", BindingFlags.Static | BindingFlags.NonPublic);
					if (method != null)
					{
						text3 = method.Invoke(null, new object[0]) as string;
					}
				}
			}
			catch
			{
			}
			if (text3 == null)
			{
				text3 = Environment.Version.ToString();
			}
			RuntimeHelpers.MonoVersion = text3;
		}
	}
}
