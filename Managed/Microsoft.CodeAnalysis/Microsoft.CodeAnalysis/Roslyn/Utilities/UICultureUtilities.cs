using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x02000069 RID: 105
	[NullableContext(1)]
	[Nullable(0)]
	internal static class UICultureUtilities
	{
		// Token: 0x06000277 RID: 631 RVA: 0x00008D14 File Offset: 0x00006F14
		private static bool TryGetCurrentUICultureSetter([Nullable(new byte[] { 2, 1 })] [NotNullWhen(true)] out Action<CultureInfo> setter)
		{
			bool flag;
			try
			{
				Type type = Type.GetType("System.Globalization.CultureInfo, System.Globalization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a") ?? IntrospectionExtensions.GetTypeInfo(typeof(object)).Assembly.GetType("System.Globalization.CultureInfo");
				if (type == null)
				{
					setter = null;
					flag = false;
				}
				else
				{
					PropertyInfo declaredProperty = IntrospectionExtensions.GetTypeInfo(type).GetDeclaredProperty("CurrentUICulture");
					MethodInfo methodInfo = ((declaredProperty != null) ? declaredProperty.SetMethod : null);
					if (methodInfo == null || !methodInfo.IsStatic || methodInfo.ContainsGenericParameters || methodInfo.ReturnType != typeof(void))
					{
						setter = null;
						flag = false;
					}
					else
					{
						ParameterInfo[] parameters = methodInfo.GetParameters();
						if (parameters.Length != 1 || parameters[0].ParameterType != typeof(CultureInfo))
						{
							setter = null;
							flag = false;
						}
						else
						{
							setter = (Action<CultureInfo>)methodInfo.CreateDelegate(typeof(Action<CultureInfo>));
							flag = true;
						}
					}
				}
			}
			catch
			{
				setter = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00008E0C File Offset: 0x0000700C
		private static bool TryGetCurrentThreadUICultureSetter([Nullable(new byte[] { 2, 1 })] [NotNullWhen(true)] out Action<CultureInfo> setter)
		{
			bool flag;
			try
			{
				UICultureUtilities.<>c__DisplayClass3_0 CS$<>8__locals1 = new UICultureUtilities.<>c__DisplayClass3_0();
				Type type = IntrospectionExtensions.GetTypeInfo(typeof(object)).Assembly.GetType("System.Threading.Thread");
				if (type == null)
				{
					setter = null;
					flag = false;
				}
				else
				{
					TypeInfo typeInfo = IntrospectionExtensions.GetTypeInfo(type);
					UICultureUtilities.<>c__DisplayClass3_0 CS$<>8__locals2 = CS$<>8__locals1;
					PropertyInfo declaredProperty = typeInfo.GetDeclaredProperty("CurrentThread");
					CS$<>8__locals2.currentThreadGetter = ((declaredProperty != null) ? declaredProperty.GetMethod : null);
					if (CS$<>8__locals1.currentThreadGetter == null || !CS$<>8__locals1.currentThreadGetter.IsStatic || CS$<>8__locals1.currentThreadGetter.ContainsGenericParameters || CS$<>8__locals1.currentThreadGetter.ReturnType != type || CS$<>8__locals1.currentThreadGetter.GetParameters().Length != 0)
					{
						setter = null;
						flag = false;
					}
					else
					{
						UICultureUtilities.<>c__DisplayClass3_0 CS$<>8__locals3 = CS$<>8__locals1;
						PropertyInfo declaredProperty2 = typeInfo.GetDeclaredProperty("CurrentUICulture");
						CS$<>8__locals3.currentUICultureSetter = ((declaredProperty2 != null) ? declaredProperty2.SetMethod : null);
						if (CS$<>8__locals1.currentUICultureSetter == null || CS$<>8__locals1.currentUICultureSetter.IsStatic || CS$<>8__locals1.currentUICultureSetter.ContainsGenericParameters || CS$<>8__locals1.currentUICultureSetter.ReturnType != typeof(void))
						{
							setter = null;
							flag = false;
						}
						else
						{
							ParameterInfo[] parameters = CS$<>8__locals1.currentUICultureSetter.GetParameters();
							if (parameters.Length != 1 || parameters[0].ParameterType != typeof(CultureInfo))
							{
								setter = null;
								flag = false;
							}
							else
							{
								setter = delegate(CultureInfo culture)
								{
									MethodBase currentUICultureSetter = CS$<>8__locals1.currentUICultureSetter;
									object obj = CS$<>8__locals1.currentThreadGetter.Invoke(null, null);
									object[] array = new CultureInfo[] { culture };
									currentUICultureSetter.Invoke(obj, array);
								};
								flag = true;
							}
						}
					}
				}
			}
			catch
			{
				setter = null;
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00008F90 File Offset: 0x00007190
		static UICultureUtilities()
		{
			if (!UICultureUtilities.TryGetCurrentUICultureSetter(out UICultureUtilities.s_setCurrentUICulture) && !UICultureUtilities.TryGetCurrentThreadUICultureSetter(out UICultureUtilities.s_setCurrentUICulture))
			{
				UICultureUtilities.s_setCurrentUICulture = null;
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00008FB0 File Offset: 0x000071B0
		public static Action WithCurrentUICulture(Action action)
		{
			if (UICultureUtilities.s_setCurrentUICulture == null)
			{
				return action;
			}
			CultureInfo savedCulture = CultureInfo.CurrentUICulture;
			return delegate
			{
				CultureInfo currentUICulture = CultureInfo.CurrentUICulture;
				if (currentUICulture != savedCulture)
				{
					UICultureUtilities.s_setCurrentUICulture.Invoke(savedCulture);
					try
					{
						action.Invoke();
						return;
					}
					finally
					{
						UICultureUtilities.s_setCurrentUICulture.Invoke(currentUICulture);
					}
				}
				action.Invoke();
			};
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00008FF0 File Offset: 0x000071F0
		public static Action<T> WithCurrentUICulture<[Nullable(2)] T>(Action<T> action)
		{
			if (UICultureUtilities.s_setCurrentUICulture == null)
			{
				return action;
			}
			CultureInfo savedCulture = CultureInfo.CurrentUICulture;
			return delegate(T param)
			{
				CultureInfo currentUICulture = CultureInfo.CurrentUICulture;
				if (currentUICulture != savedCulture)
				{
					UICultureUtilities.s_setCurrentUICulture.Invoke(savedCulture);
					try
					{
						action.Invoke(param);
						return;
					}
					finally
					{
						UICultureUtilities.s_setCurrentUICulture.Invoke(currentUICulture);
					}
				}
				action.Invoke(param);
			};
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00009030 File Offset: 0x00007230
		public static Func<T> WithCurrentUICulture<[Nullable(2)] T>(Func<T> func)
		{
			if (UICultureUtilities.s_setCurrentUICulture == null)
			{
				return func;
			}
			CultureInfo savedCulture = CultureInfo.CurrentUICulture;
			return delegate
			{
				CultureInfo currentUICulture = CultureInfo.CurrentUICulture;
				if (currentUICulture != savedCulture)
				{
					UICultureUtilities.s_setCurrentUICulture.Invoke(savedCulture);
					try
					{
						return func.Invoke();
					}
					finally
					{
						UICultureUtilities.s_setCurrentUICulture.Invoke(currentUICulture);
					}
				}
				return func.Invoke();
			};
		}

		// Token: 0x040000AF RID: 175
		private const string currentUICultureName = "CurrentUICulture";

		// Token: 0x040000B0 RID: 176
		[Nullable(new byte[] { 2, 1 })]
		private static readonly Action<CultureInfo> s_setCurrentUICulture;
	}
}
