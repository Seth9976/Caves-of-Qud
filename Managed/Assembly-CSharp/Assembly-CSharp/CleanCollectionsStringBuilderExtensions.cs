using System;
using System.Text;

// Token: 0x0200017D RID: 381
public static class CleanCollectionsStringBuilderExtensions
{
	// Token: 0x06000EA4 RID: 3748 RVA: 0x0005D5AD File Offset: 0x0005B7AD
	public static bool Contains(this StringBuilder haystack, char needle)
	{
		return haystack.IndexOf(needle, 0) != -1;
	}

	// Token: 0x06000EA5 RID: 3749 RVA: 0x0005D5C0 File Offset: 0x0005B7C0
	public static int IndexOf(this StringBuilder haystack, char needle, int startIndex = 0)
	{
		if (haystack == null)
		{
			throw new ArgumentNullException();
		}
		for (int i = startIndex; i < haystack.Length; i++)
		{
			if (haystack[i] == needle)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x06000EA6 RID: 3750 RVA: 0x0005D5F4 File Offset: 0x0005B7F4
	public static bool Contains(this StringBuilder haystack, string needle)
	{
		return haystack.IndexOf(needle) != -1;
	}

	// Token: 0x06000EA7 RID: 3751 RVA: 0x0005D604 File Offset: 0x0005B804
	public static int IndexOf(this StringBuilder haystack, string needle)
	{
		if (haystack == null || needle == null)
		{
			throw new ArgumentNullException();
		}
		if (needle.Length == 0)
		{
			return 0;
		}
		int num = haystack.IndexOf(needle[0], 0);
		if (num == -1 || needle.Length == 1)
		{
			return num;
		}
		int num2 = 0;
		int num3 = 0;
		int[] array = CleanCollectionsStringBuilderExtensions.KMPTable(needle);
		while (num2 + num3 < haystack.Length)
		{
			if (needle[num3] == haystack[num2 + num3])
			{
				if (num3 == needle.Length - 1)
				{
					return num2;
				}
				num3++;
			}
			else
			{
				num2 = num2 + num3 - array[num3];
				num3 = ((array[num3] > -1) ? array[num3] : 0);
			}
		}
		return -1;
	}

	// Token: 0x06000EA8 RID: 3752 RVA: 0x0005D698 File Offset: 0x0005B898
	private static int[] KMPTable(string sought)
	{
		int[] array = new int[sought.Length];
		int i = 2;
		int num = 0;
		array[0] = -1;
		array[1] = 0;
		while (i < array.Length)
		{
			if (sought[i - 1] == sought[num])
			{
				num = (array[i++] = num + 1);
			}
			else if (num > 0)
			{
				num = array[num];
			}
			else
			{
				array[i++] = 0;
			}
		}
		return array;
	}

	// Token: 0x06000EA9 RID: 3753 RVA: 0x0005D6F8 File Offset: 0x0005B8F8
	public static bool CleanContainsStartFrom(this StringBuilder builder, int Start, char C)
	{
		for (int i = Start; i < builder.Length; i++)
		{
			if (builder[i] == C)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000EAA RID: 3754 RVA: 0x0005D724 File Offset: 0x0005B924
	public static bool CleanContainsAnyStartFrom(this StringBuilder builder, int Start, char[] C)
	{
		for (int i = Start; i < builder.Length; i++)
		{
			for (int j = 0; j < C.Length; j++)
			{
				if (builder[i] == C[j])
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000EAB RID: 3755 RVA: 0x0005D760 File Offset: 0x0005B960
	public static int CleanContainsStartFrom(this StringBuilder builder, int Start, char[] C)
	{
		for (int i = Start; i < builder.Length; i++)
		{
			for (int j = 0; j < C.Length; j++)
			{
				if (builder[i] == C[j])
				{
					return i;
				}
			}
		}
		return -1;
	}

	// Token: 0x06000EAC RID: 3756 RVA: 0x0005D79C File Offset: 0x0005B99C
	public static void Substring(this StringBuilder builder, int Start, StringBuilder Output)
	{
		Output.Length = 0;
		for (int i = Start; i < builder.Length; i++)
		{
			Output.Append(builder[i]);
		}
	}

	// Token: 0x06000EAD RID: 3757 RVA: 0x0005D7D0 File Offset: 0x0005B9D0
	public static void Substring(this StringBuilder builder, int Start, int Length, StringBuilder Output)
	{
		Output.Length = 0;
		int num = Start;
		while (num < builder.Length && num < Start + Length)
		{
			Output.Append(builder[num]);
			num++;
		}
	}
}
