using System;

namespace UnityEngine.Purchasing
{
	// Token: 0x02000006 RID: 6
	internal static class VersionCheck
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000020D6 File Offset: 0x000002D6
		public static bool GreaterThanOrEqual(string versionA, string versionB)
		{
			return !VersionCheck.LessThan(versionA, versionB);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000020E2 File Offset: 0x000002E2
		public static bool GreaterThan(string versionA, string versionB)
		{
			return !VersionCheck.LessThanOrEqual(versionA, versionB);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000020F0 File Offset: 0x000002F0
		public static bool LessThan(string versionA, string versionB)
		{
			VersionCheck.Version version = VersionCheck.Parse(versionA);
			VersionCheck.Version version2 = VersionCheck.Parse(versionB);
			return version.major <= version2.major && (version.major < version2.major || (version.minor <= version2.minor && (version.minor < version2.minor || (version.patch <= version2.patch && version.patch < version2.patch))));
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000216C File Offset: 0x0000036C
		public static bool LessThanOrEqual(string versionA, string versionB)
		{
			return VersionCheck.LessThan(versionA, versionB) || !VersionCheck.LessThan(versionB, versionA);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002183 File Offset: 0x00000383
		public static bool Equal(string versionA, string versionB)
		{
			return !VersionCheck.LessThan(versionA, versionB) && !VersionCheck.LessThan(versionB, versionA);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000219A File Offset: 0x0000039A
		public static int MajorVersion(string version)
		{
			return VersionCheck.PartialVersion(version, 0);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021A3 File Offset: 0x000003A3
		public static int MinorVersion(string version)
		{
			return VersionCheck.PartialVersion(version, 1);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000021AC File Offset: 0x000003AC
		public static int PatchVersion(string version)
		{
			return VersionCheck.PartialVersion(version, 2);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000021B8 File Offset: 0x000003B8
		public static VersionCheck.Version Parse(string version)
		{
			return new VersionCheck.Version
			{
				major = VersionCheck.MajorVersion(version),
				minor = VersionCheck.MinorVersion(version),
				patch = VersionCheck.PatchVersion(version)
			};
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021F8 File Offset: 0x000003F8
		private static int PartialVersion(string version, int index)
		{
			string text = version.Split(new char[] { 'a', 'b', 'f', 'p' })[0];
			int num = 0;
			string[] array = text.Split('.', StringSplitOptions.None);
			if (array.Length < index + 1)
			{
				return num;
			}
			int.TryParse(array[index], out num);
			return num;
		}

		// Token: 0x0200000B RID: 11
		internal struct Version
		{
			// Token: 0x04000009 RID: 9
			public int major;

			// Token: 0x0400000A RID: 10
			public int minor;

			// Token: 0x0400000B RID: 11
			public int patch;
		}
	}
}
