using System;
using System.Reflection;

namespace HarmonyLib
{
	// Token: 0x02000193 RID: 403
	public static class MethodBaseExtensions
	{
		// Token: 0x0600068D RID: 1677 RVA: 0x000168CC File Offset: 0x00014ACC
		public static bool HasMethodBody(this MethodBase member)
		{
			MethodBody methodBody = member.GetMethodBody();
			int? num;
			if (methodBody == null)
			{
				num = null;
			}
			else
			{
				byte[] ilasByteArray = methodBody.GetILAsByteArray();
				num = ((ilasByteArray != null) ? new int?(ilasByteArray.Length) : null);
			}
			int? num2 = num;
			return num2.GetValueOrDefault() > 0;
		}
	}
}
