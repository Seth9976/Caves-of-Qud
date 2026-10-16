using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace Ionic.Zip
{
	// Token: 0x02000024 RID: 36
	internal static class SharedUtilities
	{
		// Token: 0x060000A1 RID: 161 RVA: 0x00003C48 File Offset: 0x00001E48
		public static long GetFileLength(string fileName)
		{
			if (!File.Exists(fileName))
			{
				throw new FileNotFoundException(fileName);
			}
			long num = 0L;
			FileShare fileShare = 3;
			fileShare |= 4;
			using (FileStream fileStream = File.Open(fileName, 3, 1, fileShare))
			{
				num = fileStream.Length;
			}
			return num;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003C9C File Offset: 0x00001E9C
		[Conditional("NETCF")]
		public static void Workaround_Ladybug318918(Stream s)
		{
			s.Flush();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003CA4 File Offset: 0x00001EA4
		private static string SimplifyFwdSlashPath(string path)
		{
			if (path.StartsWith("./"))
			{
				path = path.Substring(2);
			}
			path = path.Replace("/./", "/");
			path = SharedUtilities.doubleDotRegex1.Replace(path, "$1$3");
			return path;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003CE4 File Offset: 0x00001EE4
		public static string NormalizePathForUseInZipFile(string pathName)
		{
			if (string.IsNullOrEmpty(pathName))
			{
				return pathName;
			}
			if (pathName.Length >= 2 && pathName.get_Chars(1) == ':' && pathName.get_Chars(2) == '\\')
			{
				pathName = pathName.Substring(3);
			}
			pathName = pathName.Replace('\\', '/');
			while (pathName.StartsWith("/"))
			{
				pathName = pathName.Substring(1);
			}
			return SharedUtilities.SimplifyFwdSlashPath(pathName);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003D50 File Offset: 0x00001F50
		internal static byte[] StringToByteArray(string value, Encoding encoding)
		{
			return encoding.GetBytes(value);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003D66 File Offset: 0x00001F66
		internal static byte[] StringToByteArray(string value)
		{
			return SharedUtilities.StringToByteArray(value, SharedUtilities.ibm437);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003D73 File Offset: 0x00001F73
		internal static string Utf8StringFromBuffer(byte[] buf)
		{
			return SharedUtilities.StringFromBuffer(buf, SharedUtilities.utf8);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003D80 File Offset: 0x00001F80
		internal static string StringFromBuffer(byte[] buf, Encoding encoding)
		{
			return encoding.GetString(buf, 0, buf.Length);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003D9C File Offset: 0x00001F9C
		internal static int ReadSignature(Stream s)
		{
			int num = 0;
			try
			{
				num = SharedUtilities._ReadFourBytes(s, "n/a");
			}
			catch (BadReadException)
			{
			}
			return num;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003DD0 File Offset: 0x00001FD0
		internal static int ReadEntrySignature(Stream s)
		{
			int num = 0;
			try
			{
				num = SharedUtilities._ReadFourBytes(s, "n/a");
				if (num == 134695760)
				{
					s.Seek(12L, 1);
					num = SharedUtilities._ReadFourBytes(s, "n/a");
					if (num != 67324752)
					{
						s.Seek(8L, 1);
						num = SharedUtilities._ReadFourBytes(s, "n/a");
						if (num != 67324752)
						{
							s.Seek(-24L, 1);
							num = SharedUtilities._ReadFourBytes(s, "n/a");
						}
					}
				}
			}
			catch (BadReadException)
			{
			}
			return num;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003E60 File Offset: 0x00002060
		internal static int ReadInt(Stream s)
		{
			return SharedUtilities._ReadFourBytes(s, "Could not read block - no data!  (position 0x{0:X8})");
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003E70 File Offset: 0x00002070
		private static int _ReadFourBytes(Stream s, string message)
		{
			byte[] array = new byte[4];
			int num = s.Read(array, 0, array.Length);
			if (num != array.Length)
			{
				throw new BadReadException(string.Format(message, s.Position));
			}
			return (((int)array[3] * 256 + (int)array[2]) * 256 + (int)array[1]) * 256 + (int)array[0];
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003ED4 File Offset: 0x000020D4
		internal static long FindSignature(Stream stream, int SignatureToFind)
		{
			long position = stream.Position;
			int num = 65536;
			byte[] array = new byte[]
			{
				(byte)(SignatureToFind >> 24),
				(byte)((SignatureToFind & 16711680) >> 16),
				(byte)((SignatureToFind & 65280) >> 8),
				(byte)(SignatureToFind & 255)
			};
			byte[] array2 = new byte[num];
			bool flag = false;
			do
			{
				int num2 = stream.Read(array2, 0, array2.Length);
				if (num2 == 0)
				{
					break;
				}
				for (int i = 0; i < num2; i++)
				{
					if (array2[i] == array[3])
					{
						long position2 = stream.Position;
						stream.Seek((long)(i - num2), 1);
						int num3 = SharedUtilities.ReadSignature(stream);
						flag = num3 == SignatureToFind;
						if (flag)
						{
							break;
						}
						stream.Seek(position2, 0);
					}
				}
			}
			while (!flag);
			if (!flag)
			{
				stream.Seek(position, 0);
				return -1L;
			}
			return stream.Position - position - 4L;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003FB4 File Offset: 0x000021B4
		internal static DateTime AdjustTime_Reverse(DateTime time)
		{
			if (time.Kind == 1)
			{
				return time;
			}
			DateTime dateTime = time;
			if (DateTime.Now.IsDaylightSavingTime() && !time.IsDaylightSavingTime())
			{
				dateTime = time - new TimeSpan(1, 0, 0);
			}
			else if (!DateTime.Now.IsDaylightSavingTime() && time.IsDaylightSavingTime())
			{
				dateTime = time + new TimeSpan(1, 0, 0);
			}
			return dateTime;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004020 File Offset: 0x00002220
		internal static DateTime PackedToDateTime(int packedDateTime)
		{
			if (packedDateTime == 65535 || packedDateTime == 0)
			{
				return new DateTime(1995, 1, 1, 0, 0, 0, 0);
			}
			short num = (short)(packedDateTime & 65535);
			short num2 = (short)(((long)packedDateTime & (long)((ulong)(-65536))) >> 16);
			int i = 1980 + (((int)num2 & 65024) >> 9);
			int j = (num2 & 480) >> 5;
			int k = (int)(num2 & 31);
			int num3 = ((int)num & 63488) >> 11;
			int l = (num & 2016) >> 5;
			int m = (int)((num & 31) * 2);
			if (m >= 60)
			{
				l++;
				m = 0;
			}
			if (l >= 60)
			{
				num3++;
				l = 0;
			}
			if (num3 >= 24)
			{
				k++;
				num3 = 0;
			}
			DateTime dateTime = DateTime.Now;
			bool flag = false;
			try
			{
				dateTime = new DateTime(i, j, k, num3, l, m, 0);
				flag = true;
			}
			catch (ArgumentOutOfRangeException)
			{
				if (i == 1980)
				{
					if (j != 0)
					{
						if (k != 0)
						{
							goto IL_0111;
						}
					}
					try
					{
						dateTime = new DateTime(1980, 1, 1, num3, l, m, 0);
						flag = true;
						goto IL_01AD;
					}
					catch (ArgumentOutOfRangeException)
					{
						try
						{
							dateTime = new DateTime(1980, 1, 1, 0, 0, 0, 0);
							flag = true;
						}
						catch (ArgumentOutOfRangeException)
						{
						}
						goto IL_01AD;
					}
				}
				try
				{
					IL_0111:
					while (i < 1980)
					{
						i++;
					}
					while (i > 2030)
					{
						i--;
					}
					while (j < 1)
					{
						j++;
					}
					while (j > 12)
					{
						j--;
					}
					while (k < 1)
					{
						k++;
					}
					while (k > 28)
					{
						k--;
					}
					while (l < 0)
					{
						l++;
					}
					while (l > 59)
					{
						l--;
					}
					while (m < 0)
					{
						m++;
					}
					while (m > 59)
					{
						m--;
					}
					dateTime = new DateTime(i, j, k, num3, l, m, 0);
					flag = true;
				}
				catch (ArgumentOutOfRangeException)
				{
				}
				IL_01AD:;
			}
			if (!flag)
			{
				string text = string.Format("y({0}) m({1}) d({2}) h({3}) m({4}) s({5})", new object[] { i, j, k, num3, l, m });
				throw new ZipException(string.Format("Bad date/time format in the zip file. ({0})", text));
			}
			dateTime = DateTime.SpecifyKind(dateTime, 2);
			return dateTime;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004288 File Offset: 0x00002488
		internal static int DateTimeToPacked(DateTime time)
		{
			time = time.ToLocalTime();
			ushort num = (ushort)((time.Day & 31) | ((time.Month << 5) & 480) | ((time.Year - 1980 << 9) & 65024));
			ushort num2 = (ushort)(((time.Second / 2) & 31) | ((time.Minute << 5) & 2016) | ((time.Hour << 11) & 63488));
			return ((int)num << 16) | (int)num2;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004308 File Offset: 0x00002508
		public static void CreateAndOpenUniqueTempFile(string dir, out Stream fs, out string filename)
		{
			for (int i = 0; i < 3; i++)
			{
				try
				{
					filename = Path.Combine(dir, SharedUtilities.InternalGetTempFileName());
					fs = new FileStream(filename, 1);
					return;
				}
				catch (IOException)
				{
					if (i == 2)
					{
						throw;
					}
				}
			}
			throw new IOException();
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000435C File Offset: 0x0000255C
		public static string InternalGetTempFileName()
		{
			return "DotNetZip-" + SharedUtilities.GenerateRandomStringImpl(8, 0) + ".tmp";
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004374 File Offset: 0x00002574
		internal static string GenerateRandomStringImpl(int length, int delta)
		{
			bool flag = delta == 0;
			Random random = new Random();
			char[] array = new char[length];
			for (int i = 0; i < length; i++)
			{
				if (flag)
				{
					delta = ((random.Next(2) == 0) ? 65 : 97);
				}
				array[i] = (char)(random.Next(26) + delta);
			}
			return new string(array);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000043D4 File Offset: 0x000025D4
		internal static int ReadWithRetry(Stream s, byte[] buffer, int offset, int count, string FileName)
		{
			int num = 0;
			bool flag = false;
			do
			{
				try
				{
					num = s.Read(buffer, offset, count);
					flag = true;
				}
				catch (IOException)
				{
					throw;
				}
			}
			while (!flag);
			return num;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000440C File Offset: 0x0000260C
		private static uint _HRForException(Exception ex1)
		{
			return (uint)Marshal.GetHRForException(ex1);
		}

		// Token: 0x0400005D RID: 93
		private static Regex doubleDotRegex1 = new Regex("^(.*/)?([^/\\\\.]+/\\\\.\\\\./)(.+)$");

		// Token: 0x0400005E RID: 94
		private static Encoding ibm437 = Encoding.GetEncoding("UTF-8");

		// Token: 0x0400005F RID: 95
		private static Encoding utf8 = Encoding.GetEncoding("UTF-8");
	}
}
