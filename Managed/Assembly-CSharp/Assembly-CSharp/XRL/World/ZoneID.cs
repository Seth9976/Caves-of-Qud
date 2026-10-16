using System;
using System.Globalization;
using System.Text;
using Genkit;
using UnityEngine;

namespace XRL.World
{
	// Token: 0x02000993 RID: 2451
	public static class ZoneID
	{
		// Token: 0x060068EE RID: 26862 RVA: 0x0020ECCC File Offset: 0x0020CECC
		public static string Assemble(string World, Location2D Location, int ZoneZ = 10)
		{
			return ZoneID.SB.Clear().Append(World).Append('.')
				.Append(Location.X / 3)
				.Append('.')
				.Append(Location.Y / 3)
				.Append('.')
				.Append(Location.X % 3)
				.Append('.')
				.Append(Location.Y % 3)
				.Append('.')
				.Append(ZoneZ)
				.ToString();
		}

		// Token: 0x060068EF RID: 26863 RVA: 0x0020ED4C File Offset: 0x0020CF4C
		public static string Assemble(string World, int ParasangX, int ParasangY, int ZoneX, int ZoneY, int ZoneZ)
		{
			return ZoneID.SB.Clear().Append(World).Append('.')
				.Append(ParasangX)
				.Append('.')
				.Append(ParasangY)
				.Append('.')
				.Append(ZoneX)
				.Append('.')
				.Append(ZoneY)
				.Append('.')
				.Append(ZoneZ)
				.ToString();
		}

		// Token: 0x060068F0 RID: 26864 RVA: 0x0020EDB4 File Offset: 0x0020CFB4
		public static string Assemble(string World, int ParasangX, int ParasangY, string ZoneX, string ZoneY, int ZoneZ)
		{
			return ZoneID.SB.Clear().Append(World).Append('.')
				.Append(ParasangX)
				.Append('.')
				.Append(ParasangY)
				.Append('.')
				.Append(ZoneX)
				.Append('.')
				.Append(ZoneY)
				.Append('.')
				.Append(ZoneZ)
				.ToString();
		}

		// Token: 0x060068F1 RID: 26865 RVA: 0x0020EE1C File Offset: 0x0020D01C
		private static int Parse(string ID, out string World, out string Blueprint, out string Instance)
		{
			int num = ID.IndexOf('.');
			int num2 = ((num == -1) ? ID.Length : ID.IndexOf('.'));
			int num3 = ID.IndexOf('@', 0, num2);
			if (num3 == -1)
			{
				World = ID.Substring(0, num2);
				Blueprint = null;
				Instance = null;
				return num;
			}
			World = ID.Substring(0, num3);
			int num4 = ID.IndexOf('@', num3 + 1);
			if (num4 == -1)
			{
				Blueprint = ID.Substring(num3 + 1, num2 - num3 - 1);
				Instance = null;
				return num;
			}
			Blueprint = ID.Substring(num3 + 1, num4 - num3 - 1);
			Instance = ID.Substring(num4 + 1, num2 - num4 - 1);
			return num;
		}

		// Token: 0x060068F2 RID: 26866 RVA: 0x0020EEB4 File Offset: 0x0020D0B4
		public static bool Parse(string ID, out string World, out string Blueprint, out string Instance, out int ParasangX, out int ParasangY, out int ZoneX, out int ZoneY, out int ZoneZ)
		{
			ParasangX = -1;
			ParasangY = -1;
			ZoneX = -1;
			ZoneY = -1;
			ZoneZ = -1;
			int num = ZoneID.Parse(ID, out World, out Blueprint, out Instance);
			if (num == -1)
			{
				return false;
			}
			int num2 = ID.IndexOf('.', num + 1);
			if (num2 == -1)
			{
				return false;
			}
			int num3 = ID.IndexOf('.', num2 + 1);
			if (num3 == -1)
			{
				return false;
			}
			int num4 = ID.IndexOf('.', num3 + 1);
			if (num4 == -1)
			{
				return false;
			}
			int num5 = ID.IndexOf('.', num4 + 1);
			if (num5 == -1)
			{
				return false;
			}
			try
			{
				ParasangX = int.Parse(ID.AsSpan(num + 1, num2 - num - 1), NumberStyles.Integer, null);
				ParasangY = int.Parse(ID.AsSpan(num2 + 1, num3 - num2 - 1), NumberStyles.Integer, null);
				ZoneX = int.Parse(ID.AsSpan(num3 + 1, num4 - num3 - 1), NumberStyles.Integer, null);
				ZoneY = int.Parse(ID.AsSpan(num4 + 1, num5 - num4 - 1), NumberStyles.Integer, null);
				ZoneZ = int.Parse(ID.AsSpan(num5 + 1, ID.Length - num5 - 1), NumberStyles.Integer, null);
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("error parsing \"" + ID + "\"", ex);
			}
			return true;
		}

		// Token: 0x060068F3 RID: 26867 RVA: 0x0020EFE0 File Offset: 0x0020D1E0
		public static bool Parse(string ID, out string World, out int ParasangX, out int ParasangY, out int ZoneX, out int ZoneY, out int ZoneZ)
		{
			string text;
			string text2;
			return ZoneID.Parse(ID, out World, out text, out text2, out ParasangX, out ParasangY, out ZoneX, out ZoneY, out ZoneZ);
		}

		// Token: 0x060068F4 RID: 26868 RVA: 0x0020F000 File Offset: 0x0020D200
		public static bool Parse(string ID, out string World, out int ParasangX, out int ParasangY)
		{
			int num;
			int num2;
			int num3;
			return ZoneID.Parse(ID, out World, out ParasangX, out ParasangY, out num, out num2, out num3);
		}

		// Token: 0x060068F5 RID: 26869 RVA: 0x0020F01C File Offset: 0x0020D21C
		public static bool Parse(string ID, out int ParasangX, out int ParasangY, out int ZoneX, out int ZoneY, out int ZoneZ)
		{
			string text;
			return ZoneID.Parse(ID, out text, out ParasangX, out ParasangY, out ZoneX, out ZoneY, out ZoneZ);
		}

		// Token: 0x060068F6 RID: 26870 RVA: 0x0020F038 File Offset: 0x0020D238
		public static bool Parse(string ID, out int ParasangX, out int ParasangY, out int ZoneX, out int ZoneY)
		{
			string text;
			int num;
			return ZoneID.Parse(ID, out text, out ParasangX, out ParasangY, out ZoneX, out ZoneY, out num);
		}

		/// <summary>Check match level of two Zone IDs.</summary>
		/// <returns>-1 for no match, 0 for world match, 1 for parasang match, 2 for zone match.</returns>
		// Token: 0x060068F7 RID: 26871 RVA: 0x0020F054 File Offset: 0x0020D254
		public static int Match(string ID, string Other)
		{
			int num = 0;
			int num2 = 0;
			int length = ID.Length;
			int length2 = Other.Length;
			int num3 = Mathf.Min(length, length2);
			while (num < num3 && ID[num] == Other[num])
			{
				if (ID[num] == '.')
				{
					num2++;
				}
				num++;
			}
			if (num == length && num == length2)
			{
				num2++;
			}
			if (num2 <= 0)
			{
				return -1;
			}
			if (num2 <= 2)
			{
				return 0;
			}
			if (num2 <= 5)
			{
				return 1;
			}
			return 2;
		}

		// Token: 0x060068F8 RID: 26872 RVA: 0x0020F0C8 File Offset: 0x0020D2C8
		public static string GetWorldID(string ID)
		{
			string text;
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			ZoneID.Parse(ID, out text, out num, out num2, out num3, out num4, out num5);
			return text;
		}

		// Token: 0x0400304D RID: 12365
		private static StringBuilder SB = new StringBuilder();
	}
}
