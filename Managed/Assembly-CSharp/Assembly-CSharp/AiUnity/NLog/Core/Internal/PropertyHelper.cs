using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using AiUnity.Common.Attributes;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Conditions;
using AiUnity.NLog.Core.Config;
using AiUnity.NLog.Core.Layouts;

namespace AiUnity.NLog.Core.Internal
{
	// Token: 0x0200026B RID: 619
	internal static class PropertyHelper
	{
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06001654 RID: 5716 RVA: 0x0007832C File Offset: 0x0007652C
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x00078334 File Offset: 0x00076534
		internal static void SetPropertyFromString(object o, string name, string value, ConfigurationItemFactory configurationItemFactory)
		{
			PropertyHelper.Logger.Debug("Setting '{0}.{1}' to '{2}'", new object[]
			{
				o.GetType().Name,
				name,
				value
			});
			PropertyInfo propertyInfo;
			if (!PropertyHelper.TryGetPropertyInfo(o, name, out propertyInfo))
			{
				throw new NotSupportedException("Parameter " + name + " not supported on " + o.GetType().Name);
			}
			try
			{
				if (propertyInfo.IsDefined(typeof(ArrayParameterAttribute), false))
				{
					throw new NotSupportedException(string.Concat(new string[]
					{
						"Parameter ",
						name,
						" of ",
						o.GetType().Name,
						" is an array and cannot be assigned a scalar value."
					}));
				}
				Type type = propertyInfo.PropertyType;
				type = Nullable.GetUnderlyingType(type) ?? type;
				object obj;
				if (!PropertyHelper.TryNLogSpecificConversion(type, value, out obj, configurationItemFactory) && !PropertyHelper.TryGetEnumValue(type, value, out obj) && !PropertyHelper.TrySpecialConversion(type, value, out obj) && !PropertyHelper.TryImplicitConversion(type, value, out obj))
				{
					obj = Convert.ChangeType(value, type);
				}
				propertyInfo.SetValue(o, obj, null);
			}
			catch (TargetInvocationException ex)
			{
				throw new NLogConfigurationException("Error when setting property '" + propertyInfo.Name + "' on " + ((o != null) ? o.ToString() : null), ex.InnerException);
			}
			catch (Exception ex2)
			{
				if (ex2.MustBeRethrown())
				{
					throw;
				}
				throw new NLogConfigurationException("Error when setting property '" + propertyInfo.Name + "' on " + ((o != null) ? o.ToString() : null), ex2);
			}
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x000784BC File Offset: 0x000766BC
		internal static bool IsArrayProperty(Type t, string name)
		{
			PropertyInfo propertyInfo;
			if (!PropertyHelper.TryGetPropertyInfo(t, name, out propertyInfo))
			{
				throw new NotSupportedException("Parameter " + name + " not supported on " + t.Name);
			}
			return propertyInfo.IsDefined(typeof(ArrayParameterAttribute), false);
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x00078504 File Offset: 0x00076704
		internal static bool TryGetPropertyInfo(object o, string propertyName, out PropertyInfo result)
		{
			PropertyInfo property = o.GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
			if (property != null)
			{
				result = property;
				return true;
			}
			Dictionary<Type, Dictionary<string, PropertyInfo>> dictionary = PropertyHelper.parameterInfoCache;
			bool flag2;
			lock (dictionary)
			{
				Type type = o.GetType();
				Dictionary<string, PropertyInfo> dictionary2;
				if (!PropertyHelper.parameterInfoCache.TryGetValue(type, out dictionary2))
				{
					dictionary2 = PropertyHelper.BuildPropertyInfoDictionary(type);
					PropertyHelper.parameterInfoCache[type] = dictionary2;
				}
				flag2 = dictionary2.TryGetValue(propertyName, out result);
			}
			return flag2;
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x00078594 File Offset: 0x00076794
		internal static Type GetArrayItemType(PropertyInfo propInfo)
		{
			ArrayParameterAttribute arrayParameterAttribute = (ArrayParameterAttribute)Attribute.GetCustomAttribute(propInfo, typeof(ArrayParameterAttribute));
			if (arrayParameterAttribute != null)
			{
				return arrayParameterAttribute.ItemType;
			}
			return null;
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x000785C2 File Offset: 0x000767C2
		internal static IEnumerable<PropertyInfo> GetAllReadableProperties(Type type)
		{
			return type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x000785CC File Offset: 0x000767CC
		internal static void CheckRequiredParameters(object o)
		{
			foreach (PropertyInfo propertyInfo in PropertyHelper.GetAllReadableProperties(o.GetType()))
			{
				if (propertyInfo.IsDefined(typeof(RequiredParameterAttribute), false) && propertyInfo.GetValue(o, null) == null)
				{
					throw new NLogConfigurationException(string.Concat(new string[]
					{
						"Required parameter '",
						propertyInfo.Name,
						"' on '",
						(o != null) ? o.ToString() : null,
						"' was not specified."
					}));
				}
			}
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x00078678 File Offset: 0x00076878
		private static bool TryImplicitConversion(Type resultType, string value, out object result)
		{
			if (Type.GetTypeCode(resultType) != TypeCode.Object)
			{
				result = null;
				return false;
			}
			MethodInfo method = resultType.GetMethod("op_Implicit", BindingFlags.Static | BindingFlags.Public, null, new Type[] { typeof(string) }, null);
			if (method == null)
			{
				result = null;
				return false;
			}
			result = method.Invoke(null, new object[] { value });
			return true;
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x000786D8 File Offset: 0x000768D8
		private static bool TryNLogSpecificConversion(Type propertyType, string value, out object newValue, ConfigurationItemFactory configurationItemFactory)
		{
			if (propertyType == typeof(Layout) || propertyType == typeof(SimpleLayout))
			{
				newValue = new SimpleLayout(value, configurationItemFactory);
				return true;
			}
			if (propertyType == typeof(ConditionExpression))
			{
				newValue = ConditionParser.ParseExpression(value, configurationItemFactory);
				return true;
			}
			newValue = null;
			return false;
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x00078738 File Offset: 0x00076938
		private static bool TryGetEnumValue(Type resultType, string value, out object result)
		{
			if (!resultType.IsEnum)
			{
				result = null;
				return false;
			}
			if (resultType.IsDefined(typeof(FlagsAttribute), false))
			{
				long num = 0L;
				foreach (string text in value.Split(',', StringSplitOptions.None))
				{
					FieldInfo field = resultType.GetField(text.Trim(), BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
					if (field == null)
					{
						throw new NLogConfigurationException("Invalid enumeration value '" + value + "'.");
					}
					num |= Convert.ToInt64(field.GetValue(null));
				}
				result = Convert.ChangeType(num, Enum.GetUnderlyingType(resultType));
				result = Enum.ToObject(resultType, result);
				return true;
			}
			FieldInfo field2 = resultType.GetField(value, BindingFlags.IgnoreCase | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			if (field2 == null)
			{
				throw new NLogConfigurationException("Invalid enumeration value '" + value + "'.");
			}
			result = field2.GetValue(null);
			return true;
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x0007881C File Offset: 0x00076A1C
		private static bool TrySpecialConversion(Type type, string value, out object newValue)
		{
			if (type == typeof(Uri))
			{
				newValue = new Uri(value, UriKind.RelativeOrAbsolute);
				return true;
			}
			if (type == typeof(Encoding))
			{
				newValue = Encoding.GetEncoding(value);
				return true;
			}
			if (type == typeof(CultureInfo))
			{
				newValue = new CultureInfo(value);
				return true;
			}
			if (type == typeof(Type))
			{
				newValue = Type.GetType(value, true);
				return true;
			}
			newValue = null;
			return false;
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x000788A0 File Offset: 0x00076AA0
		private static bool TryGetPropertyInfo(Type targetType, string propertyName, out PropertyInfo result)
		{
			if (!string.IsNullOrEmpty(propertyName))
			{
				PropertyInfo property = targetType.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public);
				if (property != null)
				{
					result = property;
					return true;
				}
			}
			Dictionary<Type, Dictionary<string, PropertyInfo>> dictionary = PropertyHelper.parameterInfoCache;
			bool flag2;
			lock (dictionary)
			{
				Dictionary<string, PropertyInfo> dictionary2;
				if (!PropertyHelper.parameterInfoCache.TryGetValue(targetType, out dictionary2))
				{
					dictionary2 = PropertyHelper.BuildPropertyInfoDictionary(targetType);
					PropertyHelper.parameterInfoCache[targetType] = dictionary2;
				}
				flag2 = dictionary2.TryGetValue(propertyName, out result);
			}
			return flag2;
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x00078928 File Offset: 0x00076B28
		private static Dictionary<string, PropertyInfo> BuildPropertyInfoDictionary(Type t)
		{
			Dictionary<string, PropertyInfo> dictionary = new Dictionary<string, PropertyInfo>(StringComparer.OrdinalIgnoreCase);
			foreach (PropertyInfo propertyInfo in PropertyHelper.GetAllReadableProperties(t))
			{
				ArrayParameterAttribute arrayParameterAttribute = (ArrayParameterAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(ArrayParameterAttribute));
				if (arrayParameterAttribute != null)
				{
					dictionary[arrayParameterAttribute.ElementName] = propertyInfo;
				}
				else
				{
					dictionary[propertyInfo.Name] = propertyInfo;
				}
				if (propertyInfo.IsDefined(typeof(DefaultParameterAttribute), false))
				{
					dictionary[string.Empty] = propertyInfo;
				}
			}
			return dictionary;
		}

		// Token: 0x04000DD6 RID: 3542
		private static Dictionary<Type, Dictionary<string, PropertyInfo>> parameterInfoCache = new Dictionary<Type, Dictionary<string, PropertyInfo>>();
	}
}
