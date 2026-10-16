using System;
using System.Reflection;

namespace NUnit.Compatibility
{
	// Token: 0x02000007 RID: 7
	public static class AssemblyExtensions
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020B4 File Offset: 0x000002B4
		public static T GetCustomAttribute<T>(this Assembly assembly) where T : Attribute
		{
			T[] array = (T[])assembly.GetCustomAttributes(typeof(T), false);
			if (array.Length == 0)
			{
				return default(T);
			}
			return array[0];
		}
	}
}
