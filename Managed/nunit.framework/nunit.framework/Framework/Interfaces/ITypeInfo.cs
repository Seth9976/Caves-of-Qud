using System;
using System.Reflection;

namespace NUnit.Framework.Interfaces
{
	// Token: 0x020000DB RID: 219
	public interface ITypeInfo : IReflectionInfo
	{
		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060006EE RID: 1774
		Type Type { get; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060006EF RID: 1775
		ITypeInfo BaseType { get; }

		// Token: 0x060006F0 RID: 1776
		bool IsType(Type type);

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060006F1 RID: 1777
		string Name { get; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060006F2 RID: 1778
		string FullName { get; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060006F3 RID: 1779
		Assembly Assembly { get; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060006F4 RID: 1780
		string Namespace { get; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060006F5 RID: 1781
		bool IsAbstract { get; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060006F6 RID: 1782
		bool IsGenericType { get; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060006F7 RID: 1783
		bool ContainsGenericParameters { get; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060006F8 RID: 1784
		bool IsGenericTypeDefinition { get; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060006F9 RID: 1785
		bool IsSealed { get; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060006FA RID: 1786
		bool IsStaticClass { get; }

		// Token: 0x060006FB RID: 1787
		string GetDisplayName();

		// Token: 0x060006FC RID: 1788
		string GetDisplayName(object[] args);

		// Token: 0x060006FD RID: 1789
		Type GetGenericTypeDefinition();

		// Token: 0x060006FE RID: 1790
		ITypeInfo MakeGenericType(Type[] typeArgs);

		// Token: 0x060006FF RID: 1791
		bool HasMethodWithAttribute(Type attrType);

		// Token: 0x06000700 RID: 1792
		IMethodInfo[] GetMethods(BindingFlags flags);

		// Token: 0x06000701 RID: 1793
		ConstructorInfo GetConstructor(Type[] argTypes);

		// Token: 0x06000702 RID: 1794
		bool HasConstructor(Type[] argTypes);

		// Token: 0x06000703 RID: 1795
		object Construct(object[] args);
	}
}
