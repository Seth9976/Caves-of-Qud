using System;

// Token: 0x020000F4 RID: 244
public struct exUtility
{
	// Token: 0x0600095D RID: 2397 RVA: 0x0003B31C File Offset: 0x0003951C
	public static void Swap<T>(ref T lhs, ref T rhs)
	{
		T t = lhs;
		lhs = rhs;
		rhs = t;
	}
}
