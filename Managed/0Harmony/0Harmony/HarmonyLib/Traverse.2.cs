using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace HarmonyLib
{
	// Token: 0x02000199 RID: 409
	public class Traverse
	{
		// Token: 0x060006AF RID: 1711 RVA: 0x00017320 File Offset: 0x00015520
		[MethodImpl(MethodImplOptions.Synchronized)]
		static Traverse()
		{
			if (Traverse.Cache == null)
			{
				Traverse.Cache = new AccessCache();
			}
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x00017348 File Offset: 0x00015548
		public static Traverse Create(Type type)
		{
			return new Traverse(type);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00017350 File Offset: 0x00015550
		public static Traverse Create<T>()
		{
			return Traverse.Create(typeof(T));
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00017361 File Offset: 0x00015561
		public static Traverse Create(object root)
		{
			return new Traverse(root);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00017369 File Offset: 0x00015569
		public static Traverse CreateWithType(string name)
		{
			return new Traverse(AccessTools.TypeByName(name));
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00002AED File Offset: 0x00000CED
		private Traverse()
		{
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00017376 File Offset: 0x00015576
		public Traverse(Type type)
		{
			this._type = type;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00017385 File Offset: 0x00015585
		public Traverse(object root)
		{
			this._root = root;
			this._type = ((root != null) ? root.GetType() : null);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x000173A6 File Offset: 0x000155A6
		private Traverse(object root, MemberInfo info, object[] index)
		{
			this._root = root;
			this._type = ((root != null) ? root.GetType() : null) ?? info.GetUnderlyingType();
			this._info = info;
			this._params = index;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x000173DF File Offset: 0x000155DF
		private Traverse(object root, MethodInfo method, object[] parameter)
		{
			this._root = root;
			this._type = method.ReturnType;
			this._method = method;
			this._params = parameter;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00017408 File Offset: 0x00015608
		public object GetValue()
		{
			if (this._info is FieldInfo)
			{
				return ((FieldInfo)this._info).GetValue(this._root);
			}
			if (this._info is PropertyInfo)
			{
				return ((PropertyInfo)this._info).GetValue(this._root, AccessTools.all, null, this._params, CultureInfo.CurrentCulture);
			}
			if (this._method != null)
			{
				return this._method.Invoke(this._root, this._params);
			}
			if (this._root == null && this._type != null)
			{
				return this._type;
			}
			return this._root;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x000174AC File Offset: 0x000156AC
		public T GetValue<T>()
		{
			object value = this.GetValue();
			if (value == null)
			{
				return default(T);
			}
			return (T)((object)value);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x000174D3 File Offset: 0x000156D3
		public object GetValue(params object[] arguments)
		{
			if (this._method == null)
			{
				throw new Exception("cannot get method value without method");
			}
			return this._method.Invoke(this._root, arguments);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x000174FA File Offset: 0x000156FA
		public T GetValue<T>(params object[] arguments)
		{
			if (this._method == null)
			{
				throw new Exception("cannot get method value without method");
			}
			return (T)((object)this._method.Invoke(this._root, arguments));
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00017528 File Offset: 0x00015728
		public Traverse SetValue(object value)
		{
			if (this._info is FieldInfo)
			{
				((FieldInfo)this._info).SetValue(this._root, value, AccessTools.all, null, CultureInfo.CurrentCulture);
			}
			if (this._info is PropertyInfo)
			{
				((PropertyInfo)this._info).SetValue(this._root, value, AccessTools.all, null, this._params, CultureInfo.CurrentCulture);
			}
			if (this._method != null)
			{
				throw new Exception("cannot set value of method " + this._method.FullDescription());
			}
			return this;
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x000175BD File Offset: 0x000157BD
		public Type GetValueType()
		{
			if (this._info is FieldInfo)
			{
				return ((FieldInfo)this._info).FieldType;
			}
			if (this._info is PropertyInfo)
			{
				return ((PropertyInfo)this._info).PropertyType;
			}
			return null;
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x000175FC File Offset: 0x000157FC
		private Traverse Resolve()
		{
			if (this._root == null)
			{
				FieldInfo fieldInfo = this._info as FieldInfo;
				if (fieldInfo != null && fieldInfo.IsStatic)
				{
					return new Traverse(this.GetValue());
				}
				PropertyInfo propertyInfo = this._info as PropertyInfo;
				if (propertyInfo != null && propertyInfo.GetGetMethod().IsStatic)
				{
					return new Traverse(this.GetValue());
				}
				if (this._method != null && this._method.IsStatic)
				{
					return new Traverse(this.GetValue());
				}
				if (this._type != null)
				{
					return this;
				}
			}
			return new Traverse(this.GetValue());
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00017694 File Offset: 0x00015894
		public Traverse Type(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (this._type == null)
			{
				return new Traverse();
			}
			Type type = AccessTools.Inner(this._type, name);
			if (type == null)
			{
				return new Traverse();
			}
			return new Traverse(type);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x000176DC File Offset: 0x000158DC
		public Traverse Field(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Traverse traverse = this.Resolve();
			if (traverse._type == null)
			{
				return new Traverse();
			}
			FieldInfo fieldInfo = Traverse.Cache.GetFieldInfo(traverse._type, name, AccessCache.MemberType.Any, false);
			if (fieldInfo == null)
			{
				return new Traverse();
			}
			if (!fieldInfo.IsStatic && traverse._root == null)
			{
				return new Traverse();
			}
			return new Traverse(traverse._root, fieldInfo, null);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001774C File Offset: 0x0001594C
		public Traverse<T> Field<T>(string name)
		{
			return new Traverse<T>(this.Field(name));
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001775A File Offset: 0x0001595A
		public List<string> Fields()
		{
			return AccessTools.GetFieldNames(this.Resolve()._type);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001776C File Offset: 0x0001596C
		public Traverse Property(string name, object[] index = null)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Traverse traverse = this.Resolve();
			if (traverse._type == null)
			{
				return new Traverse();
			}
			PropertyInfo propertyInfo = Traverse.Cache.GetPropertyInfo(traverse._type, name, AccessCache.MemberType.Any, false);
			if (propertyInfo == null)
			{
				return new Traverse();
			}
			return new Traverse(traverse._root, propertyInfo, index);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x000177C6 File Offset: 0x000159C6
		public Traverse<T> Property<T>(string name, object[] index = null)
		{
			return new Traverse<T>(this.Property(name, index));
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x000177D5 File Offset: 0x000159D5
		public List<string> Properties()
		{
			return AccessTools.GetPropertyNames(this.Resolve()._type);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x000177E8 File Offset: 0x000159E8
		public Traverse Method(string name, params object[] arguments)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Traverse traverse = this.Resolve();
			if (traverse._type == null)
			{
				return new Traverse();
			}
			Type[] types = AccessTools.GetTypes(arguments);
			MethodBase methodInfo = Traverse.Cache.GetMethodInfo(traverse._type, name, types, AccessCache.MemberType.Any, false);
			if (methodInfo == null)
			{
				return new Traverse();
			}
			return new Traverse(traverse._root, (MethodInfo)methodInfo, arguments);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00017850 File Offset: 0x00015A50
		public Traverse Method(string name, Type[] paramTypes, object[] arguments = null)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Traverse traverse = this.Resolve();
			if (traverse._type == null)
			{
				return new Traverse();
			}
			MethodBase methodInfo = Traverse.Cache.GetMethodInfo(traverse._type, name, paramTypes, AccessCache.MemberType.Any, false);
			if (methodInfo == null)
			{
				return new Traverse();
			}
			return new Traverse(traverse._root, (MethodInfo)methodInfo, arguments);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x000178B0 File Offset: 0x00015AB0
		public List<string> Methods()
		{
			return AccessTools.GetMethodNames(this.Resolve()._type);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x000178C2 File Offset: 0x00015AC2
		public bool FieldExists()
		{
			return this._info != null && this._info is FieldInfo;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x000178DC File Offset: 0x00015ADC
		public bool PropertyExists()
		{
			return this._info != null && this._info is PropertyInfo;
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x000178F6 File Offset: 0x00015AF6
		public bool MethodExists()
		{
			return this._method != null;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00017901 File Offset: 0x00015B01
		public bool TypeExists()
		{
			return this._type != null;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0001790C File Offset: 0x00015B0C
		public static void IterateFields(object source, Action<Traverse> action)
		{
			Traverse sourceTrv = Traverse.Create(source);
			AccessTools.GetFieldNames(source).ForEach(delegate(string f)
			{
				action(sourceTrv.Field(f));
			});
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001794C File Offset: 0x00015B4C
		public static void IterateFields(object source, object target, Action<Traverse, Traverse> action)
		{
			Traverse sourceTrv = Traverse.Create(source);
			Traverse targetTrv = Traverse.Create(target);
			AccessTools.GetFieldNames(source).ForEach(delegate(string f)
			{
				action(sourceTrv.Field(f), targetTrv.Field(f));
			});
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00017998 File Offset: 0x00015B98
		public static void IterateFields(object source, object target, Action<string, Traverse, Traverse> action)
		{
			Traverse sourceTrv = Traverse.Create(source);
			Traverse targetTrv = Traverse.Create(target);
			AccessTools.GetFieldNames(source).ForEach(delegate(string f)
			{
				action(f, sourceTrv.Field(f), targetTrv.Field(f));
			});
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x000179E4 File Offset: 0x00015BE4
		public static void IterateProperties(object source, Action<Traverse> action)
		{
			Traverse sourceTrv = Traverse.Create(source);
			AccessTools.GetPropertyNames(source).ForEach(delegate(string f)
			{
				action(sourceTrv.Property(f, null));
			});
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00017A24 File Offset: 0x00015C24
		public static void IterateProperties(object source, object target, Action<Traverse, Traverse> action)
		{
			Traverse sourceTrv = Traverse.Create(source);
			Traverse targetTrv = Traverse.Create(target);
			AccessTools.GetPropertyNames(source).ForEach(delegate(string f)
			{
				action(sourceTrv.Property(f, null), targetTrv.Property(f, null));
			});
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00017A70 File Offset: 0x00015C70
		public static void IterateProperties(object source, object target, Action<string, Traverse, Traverse> action)
		{
			Traverse sourceTrv = Traverse.Create(source);
			Traverse targetTrv = Traverse.Create(target);
			AccessTools.GetPropertyNames(source).ForEach(delegate(string f)
			{
				action(f, sourceTrv.Property(f, null), targetTrv.Property(f, null));
			});
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00017AB9 File Offset: 0x00015CB9
		public override string ToString()
		{
			MethodBase methodBase = this._method ?? this.GetValue();
			if (methodBase == null)
			{
				return null;
			}
			return methodBase.ToString();
		}

		// Token: 0x04000219 RID: 537
		private static readonly AccessCache Cache;

		// Token: 0x0400021A RID: 538
		private readonly Type _type;

		// Token: 0x0400021B RID: 539
		private readonly object _root;

		// Token: 0x0400021C RID: 540
		private readonly MemberInfo _info;

		// Token: 0x0400021D RID: 541
		private readonly MethodBase _method;

		// Token: 0x0400021E RID: 542
		private readonly object[] _params;

		// Token: 0x0400021F RID: 543
		public static Action<Traverse, Traverse> CopyFields = delegate(Traverse from, Traverse to)
		{
			to.SetValue(from.GetValue());
		};
	}
}
