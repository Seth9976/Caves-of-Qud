using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Config;

namespace AiUnity.NLog.Core.Internal
{
	// Token: 0x0200026A RID: 618
	internal class ObjectGraphScanner
	{
		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x00078127 File Offset: 0x00076327
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x00078130 File Offset: 0x00076330
		public static T[] FindReachableObjects<T>(params object[] rootObjects) where T : class
		{
			ObjectGraphScanner.Logger.Trace("FindReachableObject<{0}>:", new object[] { typeof(T) });
			List<T> list = new List<T>();
			Dictionary<object, int> dictionary = new Dictionary<object, int>();
			foreach (object obj in rootObjects)
			{
				ObjectGraphScanner.ScanProperties<T>(list, obj, 0, dictionary);
			}
			return list.ToArray();
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x00078190 File Offset: 0x00076390
		private static void ScanProperties<T>(List<T> result, object o, int level, Dictionary<object, int> visitedObjects) where T : class
		{
			if (o == null)
			{
				return;
			}
			if (!o.GetType().IsDefined(typeof(NLogConfigurationItemAttribute), true))
			{
				return;
			}
			if (visitedObjects.ContainsKey(o))
			{
				return;
			}
			visitedObjects.Add(o, 0);
			T t = o as T;
			if (t != null)
			{
				result.Add(t);
			}
			if (ObjectGraphScanner.Logger.IsTraceEnabled)
			{
				ObjectGraphScanner.Logger.Trace("{0}Scanning {1} '{2}'", new object[]
				{
					new string(' ', level),
					o.GetType().Name,
					o
				});
			}
			foreach (PropertyInfo propertyInfo in PropertyHelper.GetAllReadableProperties(o.GetType()))
			{
				if (!propertyInfo.PropertyType.IsPrimitive && !propertyInfo.PropertyType.IsEnum && !(propertyInfo.PropertyType == typeof(string)) && !propertyInfo.IsDefined(typeof(NLogConfigurationIgnorePropertyAttribute), true))
				{
					object value = propertyInfo.GetValue(o, null);
					if (value != null)
					{
						IEnumerable enumerable = value as IEnumerable;
						if (enumerable != null)
						{
							using (IEnumerator enumerator2 = enumerable.GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									object obj = enumerator2.Current;
									ObjectGraphScanner.ScanProperties<T>(result, obj, level + 1, visitedObjects);
								}
								continue;
							}
						}
						ObjectGraphScanner.ScanProperties<T>(result, value, level + 1, visitedObjects);
					}
				}
			}
		}
	}
}
