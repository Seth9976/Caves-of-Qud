using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI
{
	/// <summary>Provides support for rapid application development (RAD) designers to generate and parse data-binding expression syntax. This class cannot be inherited.</summary>
	// Token: 0x020001B9 RID: 441
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DataBinder
	{
		// Token: 0x060011DE RID: 4574 RVA: 0x0002F66A File Offset: 0x0002D86A
		internal static string FormatResult(object result, string format)
		{
			if (result == null)
			{
				return string.Empty;
			}
			if (format == null || format.Length == 0)
			{
				return result.ToString();
			}
			return string.Format(format, result);
		}

		/// <summary>Evaluates data-binding expressions at run time.</summary>
		/// <returns>An <see cref="T:System.Object" /> instance that results from the evaluation of the data-binding expression.</returns>
		/// <param name="container">The object reference against which the expression is evaluated. This must be a valid object identifier in the page's specified language. </param>
		/// <param name="expression">The navigation path from the <paramref name="container" /> object to the public property value to be placed in the bound control property. This must be a string of property or field names separated by periods, such as Tables[0].DefaultView.[0].Price in C# or Tables(0).DefaultView.(0).Price in Visual Basic. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="expression" /> is null or is an empty string after trimming.</exception>
		// Token: 0x060011DF RID: 4575 RVA: 0x0002F690 File Offset: 0x0002D890
		public static object Eval(object container, string expression)
		{
			expression = ((expression != null) ? expression.Trim() : null);
			if (expression == null || expression.Length == 0)
			{
				throw new ArgumentNullException("expression");
			}
			object obj = container;
			while (obj != null)
			{
				int num = expression.IndexOf('.');
				int num2 = ((num == -1) ? expression.Length : num);
				string text = expression.Substring(0, num2);
				if (text.IndexOf('[') != -1)
				{
					obj = DataBinder.GetIndexedPropertyValue(obj, text);
				}
				else
				{
					obj = DataBinder.GetPropertyValue(obj, text);
				}
				if (num == -1)
				{
					break;
				}
				expression = expression.Substring(text.Length + 1);
			}
			return obj;
		}

		/// <summary>Evaluates data-binding expressions at run time and formats the result as a string.</summary>
		/// <returns>A <see cref="T:System.String" /> object that results from evaluating the data-binding expression and converting it to a string type.</returns>
		/// <param name="container">The object reference against which the expression is evaluated. This must be a valid object identifier in the page's specified language. </param>
		/// <param name="expression">The navigation path from the <paramref name="container" /> object to the public property value to be placed in the bound control property. This must be a string of property or field names separated by periods, such as Tables[0].DefaultView.[0].Price in C# or Tables(0).DefaultView.(0).Price in Visual Basic. </param>
		/// <param name="format">A .NET Framework format string (like those used by <see cref="M:System.String.Format(System.String,System.Object)" />) that converts the <see cref="T:System.Object" /> instance returned by the data-binding expression to a <see cref="T:System.String" /> object. </param>
		// Token: 0x060011E0 RID: 4576 RVA: 0x0002F71A File Offset: 0x0002D91A
		public static string Eval(object container, string expression, string format)
		{
			return DataBinder.FormatResult(DataBinder.Eval(container, expression), format);
		}

		/// <summary>Retrieves the value of a property of the specified container and navigation path.</summary>
		/// <returns>An object that results from the evaluation of the data-binding expression.</returns>
		/// <param name="container">The object reference against which <paramref name="expr" /> is evaluated. This must be a valid object identifier in the specified language for the page.</param>
		/// <param name="expr">The navigation path from the <paramref name="container" /> object to the public property value to place in the bound control property. This must be a string of property or field names separated by periods, such as Tables[0].DefaultView.[0].Price in C# or Tables(0).DefaultView.(0).Price in Visual Basic.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="container" /> is null.- or -<paramref name="expr" /> is null or an empty string ("").</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="expr" /> is not a valid indexed expression.- or -<paramref name="expr" /> does not allow indexed access.</exception>
		// Token: 0x060011E1 RID: 4577 RVA: 0x0002F72C File Offset: 0x0002D92C
		public static object GetIndexedPropertyValue(object container, string expr)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			if (expr == null || expr.Length == 0)
			{
				throw new ArgumentNullException("expr");
			}
			int num = expr.IndexOf('[');
			int num2 = expr.IndexOf(']');
			if (num < 0 || num2 < 0 || num2 - num <= 1)
			{
				throw new ArgumentException(expr + " is not a valid indexed expression.");
			}
			string text = expr.Substring(num + 1, num2 - num - 1);
			text = text.Trim();
			if (text.Length == 0)
			{
				throw new ArgumentException(expr + " is not a valid indexed expression.");
			}
			bool flag = false;
			if ((text[0] == '\'' && text[text.Length - 1] == '\'') || (text[0] == '"' && text[text.Length - 1] == '"'))
			{
				flag = true;
				text = text.Substring(1, text.Length - 2);
			}
			else
			{
				for (int i = 0; i < text.Length; i++)
				{
					if (!char.IsDigit(text[i]))
					{
						flag = true;
						break;
					}
				}
			}
			int num3 = 0;
			if (!flag)
			{
				try
				{
					num3 = int.Parse(text);
				}
				catch
				{
					throw new ArgumentException(expr + " is not a valid indexed expression.");
				}
			}
			if (num > 0)
			{
				string text2 = expr.Substring(0, num);
				if (text2 != null && text2.Length > 0)
				{
					container = DataBinder.GetPropertyValue(container, text2);
				}
			}
			if (container == null)
			{
				return null;
			}
			if (container is IList)
			{
				if (flag)
				{
					throw new ArgumentException(expr + " cannot be indexed with a string.");
				}
				return ((IList)container)[num3];
			}
			else
			{
				Type type = container.GetType();
				object[] customAttributes = type.GetCustomAttributes(typeof(DefaultMemberAttribute), false);
				string text2;
				if (customAttributes.Length != 1)
				{
					text2 = "Item";
				}
				else
				{
					text2 = ((DefaultMemberAttribute)customAttributes[0]).MemberName;
				}
				Type[] array = new Type[] { flag ? typeof(string) : typeof(int) };
				PropertyInfo property = type.GetProperty(text2, array);
				if (property == null)
				{
					throw new ArgumentException(expr + " indexer not found.");
				}
				object[] array2 = new object[1];
				if (flag)
				{
					array2[0] = text;
				}
				else
				{
					array2[0] = num3;
				}
				return property.GetValue(container, array2);
			}
		}

		/// <summary>Retrieves the value of the specified property for the specified container, and then formats the results.</summary>
		/// <returns>The value of the specified property in the format specified by <paramref name="format" />.</returns>
		/// <param name="container">The object reference against which the expression is evaluated. This must be a valid object identifier in the specified language for the page.</param>
		/// <param name="propName">The name of the property that contains the value to retrieve.</param>
		/// <param name="format">A string that specifies the format in which to display the results.</param>
		// Token: 0x060011E2 RID: 4578 RVA: 0x0002F96C File Offset: 0x0002DB6C
		public static string GetIndexedPropertyValue(object container, string propName, string format)
		{
			return DataBinder.FormatResult(DataBinder.GetIndexedPropertyValue(container, propName), format);
		}

		/// <summary>Retrieves the value of the specified property of the specified object.</summary>
		/// <returns>The value of the specified property.</returns>
		/// <param name="container">The object that contains the property. </param>
		/// <param name="propName">The name of the property that contains the value to retrieve. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="container" /> is null.-or- <paramref name="propName" /> is null or an empty string (""). </exception>
		/// <exception cref="T:System.Web.HttpException">The object in <paramref name="container" /> does not have the property specified by <paramref name="propName" />. </exception>
		// Token: 0x060011E3 RID: 4579 RVA: 0x0002F97C File Offset: 0x0002DB7C
		public static object GetPropertyValue(object container, string propName)
		{
			if (container == null)
			{
				throw new ArgumentNullException("container");
			}
			if (propName == null || propName.Length == 0)
			{
				throw new ArgumentNullException("propName");
			}
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(container).Find(propName, true);
			if (propertyDescriptor == null)
			{
				string text = "Property ";
				string text2 = " not found in ";
				Type type = container.GetType();
				throw new HttpException(text + propName + text2 + ((type != null) ? type.ToString() : null));
			}
			return propertyDescriptor.GetValue(container);
		}

		/// <summary>Retrieves the value of the specified property of the specified object, and then formats the results.</summary>
		/// <returns>The value of the specified property in the format specified by <paramref name="format" />.</returns>
		/// <param name="container">The object that contains the property. </param>
		/// <param name="propName">The name of the property that contains the value to retrieve. </param>
		/// <param name="format">A string that specifies the format in which to display the results. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="container" /> is null.- or - <paramref name="propName" /> is null or an empty string (""). </exception>
		/// <exception cref="T:System.Web.HttpException">The object in <paramref name="container" /> does not have the property specified by <paramref name="propName" />. </exception>
		// Token: 0x060011E4 RID: 4580 RVA: 0x0002F9EB File Offset: 0x0002DBEB
		public static string GetPropertyValue(object container, string propName, string format)
		{
			return DataBinder.FormatResult(DataBinder.GetPropertyValue(container, propName), format);
		}

		/// <summary>Retrieves an object's declared data item, indicating success or failure.</summary>
		/// <returns>An object that represents the container's declared data item. Returns null if no data item is found or if the container evaluates to null.</returns>
		/// <param name="container">The object reference against which the expression is evaluated. This must be a valid object identifier in the page's specified language.</param>
		/// <param name="foundDataItem">A Boolean value that indicates whether the data item was successfully resolved and returned. This parameter is passed uninitialized.</param>
		// Token: 0x060011E5 RID: 4581 RVA: 0x0002F9FC File Offset: 0x0002DBFC
		public static object GetDataItem(object container, out bool foundDataItem)
		{
			foundDataItem = false;
			if (container == null)
			{
				return null;
			}
			if (container is IDataItemContainer)
			{
				foundDataItem = true;
				return ((IDataItemContainer)container).DataItem;
			}
			PropertyInfo propertyInfo = null;
			if (DataBinder.dataItemCache == null)
			{
				DataBinder.dataItemCache = new Dictionary<Type, PropertyInfo>();
			}
			Type type = container.GetType();
			if (!DataBinder.dataItemCache.TryGetValue(type, out propertyInfo))
			{
				propertyInfo = type.GetProperty("DataItem", BindingFlags.Instance | BindingFlags.Public);
				DataBinder.dataItemCache[type] = propertyInfo;
			}
			if (propertyInfo == null)
			{
				return null;
			}
			foundDataItem = true;
			return propertyInfo.GetValue(container, null);
		}

		/// <summary>Retrieves an object's declared data item.</summary>
		/// <returns>An object that represents the container's declared data item. Returns null if no data item is found or if the container evaluates to null.</returns>
		/// <param name="container">The object reference against which the expression is evaluated. This must be a valid object identifier in the page's specified language.</param>
		// Token: 0x060011E6 RID: 4582 RVA: 0x0002FA84 File Offset: 0x0002DC84
		public static object GetDataItem(object container)
		{
			bool flag;
			return DataBinder.GetDataItem(container, out flag);
		}

		/// <summary>Gets or sets a value that indicates whether data caching is enabled at run time.</summary>
		/// <returns>true if caching is enabled for the <see cref="T:System.Web.UI.DataBinder" /> class; otherwise, false.</returns>
		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x0002FA9C File Offset: 0x0002DC9C
		// (set) Token: 0x060011E8 RID: 4584 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public static bool EnableCaching
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Determines whether the specified data type can be bound.</summary>
		/// <returns>true for types that can be automatically data bound in controls; otherwise, false.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the data type to test.</param>
		// Token: 0x060011E9 RID: 4585 RVA: 0x0002FAB8 File Offset: 0x0002DCB8
		public static bool IsBindableType(Type type)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		// Token: 0x0400140A RID: 5130
		[ThreadStatic]
		private static Dictionary<Type, PropertyInfo> dataItemCache;
	}
}
