using System;
using Mono.Cecil;

// Token: 0x0200042A RID: 1066
internal static class MultiTargetShims
{
	// Token: 0x06001670 RID: 5744 RVA: 0x000488F4 File Offset: 0x00046AF4
	public static string Replace(this string self, string oldValue, string newValue, StringComparison comparison)
	{
		return self.Replace(oldValue, newValue);
	}

	// Token: 0x06001671 RID: 5745 RVA: 0x000488FE File Offset: 0x00046AFE
	public static bool Contains(this string self, string value, StringComparison comparison)
	{
		return self.Contains(value);
	}

	// Token: 0x06001672 RID: 5746 RVA: 0x00048907 File Offset: 0x00046B07
	public static int GetHashCode(this string self, StringComparison comparison)
	{
		return self.GetHashCode();
	}

	// Token: 0x06001673 RID: 5747 RVA: 0x0004890F File Offset: 0x00046B0F
	public static int IndexOf(this string self, char value, StringComparison comparison)
	{
		return self.IndexOf(value);
	}

	// Token: 0x06001674 RID: 5748 RVA: 0x00048918 File Offset: 0x00046B18
	public static int IndexOf(this string self, string value, StringComparison comparison)
	{
		return self.IndexOf(value);
	}

	// Token: 0x06001675 RID: 5749 RVA: 0x00048921 File Offset: 0x00046B21
	public static TypeReference GetConstraintType(this GenericParameterConstraint constraint)
	{
		return constraint.ConstraintType;
	}

	// Token: 0x04000FAC RID: 4012
	private static readonly object[] _NoArgs = new object[0];
}
