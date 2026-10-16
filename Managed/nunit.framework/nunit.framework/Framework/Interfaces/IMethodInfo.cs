using System;
using System.Reflection;

namespace NUnit.Framework.Interfaces
{
	// Token: 0x020000CA RID: 202
	public interface IMethodInfo : IReflectionInfo
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600069F RID: 1695
		ITypeInfo TypeInfo { get; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060006A0 RID: 1696
		MethodInfo MethodInfo { get; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060006A1 RID: 1697
		string Name { get; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060006A2 RID: 1698
		bool IsAbstract { get; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060006A3 RID: 1699
		bool IsPublic { get; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060006A4 RID: 1700
		bool ContainsGenericParameters { get; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060006A5 RID: 1701
		bool IsGenericMethod { get; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060006A6 RID: 1702
		bool IsGenericMethodDefinition { get; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060006A7 RID: 1703
		ITypeInfo ReturnType { get; }

		// Token: 0x060006A8 RID: 1704
		IParameterInfo[] GetParameters();

		// Token: 0x060006A9 RID: 1705
		Type[] GetGenericArguments();

		// Token: 0x060006AA RID: 1706
		IMethodInfo MakeGenericMethod(params Type[] typeArguments);

		// Token: 0x060006AB RID: 1707
		object Invoke(object fixture, params object[] args);
	}
}
