using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace PlayFab.Json
{
	// Token: 0x0200003C RID: 60
	[GeneratedCode("reflection-utils", "1.0.0")]
	internal class ReflectionUtils
	{
		// Token: 0x06000367 RID: 871 RVA: 0x00012233 File Offset: 0x00010433
		public static Type GetTypeInfo(Type type)
		{
			return type;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00012236 File Offset: 0x00010436
		public static Attribute GetAttribute(MemberInfo info, Type type)
		{
			if (info == null || type == null || !Attribute.IsDefined(info, type))
			{
				return null;
			}
			return Attribute.GetCustomAttribute(info, type);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0001225C File Offset: 0x0001045C
		public static Type GetGenericListElementType(Type type)
		{
			if (type == typeof(object))
			{
				return type;
			}
			foreach (Type type2 in ((IEnumerable<Type>)type.GetInterfaces()))
			{
				if (ReflectionUtils.IsTypeGeneric(type2) && type2.GetGenericTypeDefinition() == typeof(IList<>))
				{
					return ReflectionUtils.GetGenericTypeArguments(type2)[0];
				}
			}
			return ReflectionUtils.GetGenericTypeArguments(type)[0];
		}

		// Token: 0x0600036A RID: 874 RVA: 0x000122EC File Offset: 0x000104EC
		public static Attribute GetAttribute(Type objectType, Type attributeType)
		{
			if (objectType == null || attributeType == null || !Attribute.IsDefined(objectType, attributeType))
			{
				return null;
			}
			return Attribute.GetCustomAttribute(objectType, attributeType);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00012312 File Offset: 0x00010512
		public static Type[] GetGenericTypeArguments(Type type)
		{
			return type.GetGenericArguments();
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0001231A File Offset: 0x0001051A
		public static bool IsTypeGeneric(Type type)
		{
			return ReflectionUtils.GetTypeInfo(type).IsGenericType;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00012328 File Offset: 0x00010528
		public static bool IsTypeGenericeCollectionInterface(Type type)
		{
			if (!ReflectionUtils.IsTypeGeneric(type))
			{
				return false;
			}
			Type genericTypeDefinition = type.GetGenericTypeDefinition();
			return genericTypeDefinition == typeof(IList<>) || genericTypeDefinition == typeof(ICollection<>) || genericTypeDefinition == typeof(IEnumerable<>);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0001237C File Offset: 0x0001057C
		public static bool IsAssignableFrom(Type type1, Type type2)
		{
			return ReflectionUtils.GetTypeInfo(type1).IsAssignableFrom(ReflectionUtils.GetTypeInfo(type2));
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00012390 File Offset: 0x00010590
		public static bool IsTypeDictionary(Type type)
		{
			if (typeof(IDictionary).IsAssignableFrom(type))
			{
				return true;
			}
			if (!ReflectionUtils.GetTypeInfo(type).IsGenericType)
			{
				return false;
			}
			Type genericTypeDefinition = type.GetGenericTypeDefinition();
			return genericTypeDefinition == typeof(IDictionary<, >) || genericTypeDefinition == typeof(Dictionary<, >);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x000123EB File Offset: 0x000105EB
		public static bool IsNullableType(Type type)
		{
			return ReflectionUtils.GetTypeInfo(type).IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00012411 File Offset: 0x00010611
		public static object ToNullableType(object obj, Type nullableType)
		{
			if (obj != null)
			{
				return Convert.ChangeType(obj, Nullable.GetUnderlyingType(nullableType), CultureInfo.InvariantCulture);
			}
			return null;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00012429 File Offset: 0x00010629
		public static bool IsValueType(Type type)
		{
			return ReflectionUtils.GetTypeInfo(type).IsValueType;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00012436 File Offset: 0x00010636
		public static IEnumerable<ConstructorInfo> GetConstructors(Type type)
		{
			return type.GetConstructors();
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00012440 File Offset: 0x00010640
		public static ConstructorInfo GetConstructorInfo(Type type, params Type[] argsType)
		{
			foreach (ConstructorInfo constructorInfo in ReflectionUtils.GetConstructors(type))
			{
				ParameterInfo[] parameters = constructorInfo.GetParameters();
				if (argsType.Length == parameters.Length)
				{
					int num = 0;
					bool flag = true;
					ParameterInfo[] parameters2 = constructorInfo.GetParameters();
					for (int i = 0; i < parameters2.Length; i++)
					{
						if (parameters2[i].ParameterType != argsType[num])
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						return constructorInfo;
					}
				}
			}
			return null;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000124DC File Offset: 0x000106DC
		public static IEnumerable<PropertyInfo> GetProperties(Type type)
		{
			return type.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x000124E6 File Offset: 0x000106E6
		public static IEnumerable<FieldInfo> GetFields(Type type)
		{
			return type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06000377 RID: 887 RVA: 0x000124F0 File Offset: 0x000106F0
		public static MethodInfo GetGetterMethodInfo(PropertyInfo propertyInfo)
		{
			return propertyInfo.GetGetMethod(true);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x000124F9 File Offset: 0x000106F9
		public static MethodInfo GetSetterMethodInfo(PropertyInfo propertyInfo)
		{
			return propertyInfo.GetSetMethod(true);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00012502 File Offset: 0x00010702
		public static ReflectionUtils.ConstructorDelegate GetContructor(ConstructorInfo constructorInfo)
		{
			return ReflectionUtils.GetConstructorByReflection(constructorInfo);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0001250A File Offset: 0x0001070A
		public static ReflectionUtils.ConstructorDelegate GetContructor(Type type, params Type[] argsType)
		{
			return ReflectionUtils.GetConstructorByReflection(type, argsType);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00012513 File Offset: 0x00010713
		public static ReflectionUtils.ConstructorDelegate GetConstructorByReflection(ConstructorInfo constructorInfo)
		{
			return (object[] args) => constructorInfo.Invoke(args);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0001252C File Offset: 0x0001072C
		public static ReflectionUtils.ConstructorDelegate GetConstructorByReflection(Type type, params Type[] argsType)
		{
			ConstructorInfo constructorInfo = ReflectionUtils.GetConstructorInfo(type, argsType);
			if (!(constructorInfo == null))
			{
				return ReflectionUtils.GetConstructorByReflection(constructorInfo);
			}
			return null;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00012552 File Offset: 0x00010752
		public static ReflectionUtils.GetDelegate GetGetMethod(PropertyInfo propertyInfo)
		{
			return ReflectionUtils.GetGetMethodByReflection(propertyInfo);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0001255A File Offset: 0x0001075A
		public static ReflectionUtils.GetDelegate GetGetMethod(FieldInfo fieldInfo)
		{
			return ReflectionUtils.GetGetMethodByReflection(fieldInfo);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00012562 File Offset: 0x00010762
		public static ReflectionUtils.GetDelegate GetGetMethodByReflection(PropertyInfo propertyInfo)
		{
			MethodInfo methodInfo = ReflectionUtils.GetGetterMethodInfo(propertyInfo);
			return (object source) => methodInfo.Invoke(source, ReflectionUtils.EmptyObjects);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00012580 File Offset: 0x00010780
		public static ReflectionUtils.GetDelegate GetGetMethodByReflection(FieldInfo fieldInfo)
		{
			return (object source) => fieldInfo.GetValue(source);
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00012599 File Offset: 0x00010799
		public static ReflectionUtils.SetDelegate GetSetMethod(PropertyInfo propertyInfo)
		{
			return ReflectionUtils.GetSetMethodByReflection(propertyInfo);
		}

		// Token: 0x06000382 RID: 898 RVA: 0x000125A1 File Offset: 0x000107A1
		public static ReflectionUtils.SetDelegate GetSetMethod(FieldInfo fieldInfo)
		{
			return ReflectionUtils.GetSetMethodByReflection(fieldInfo);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x000125A9 File Offset: 0x000107A9
		public static ReflectionUtils.SetDelegate GetSetMethodByReflection(PropertyInfo propertyInfo)
		{
			MethodInfo methodInfo = ReflectionUtils.GetSetterMethodInfo(propertyInfo);
			return delegate(object source, object value)
			{
				if (ReflectionUtils._1ObjArray == null)
				{
					ReflectionUtils._1ObjArray = new object[1];
				}
				ReflectionUtils._1ObjArray[0] = value;
				methodInfo.Invoke(source, ReflectionUtils._1ObjArray);
			};
		}

		// Token: 0x06000384 RID: 900 RVA: 0x000125C7 File Offset: 0x000107C7
		public static ReflectionUtils.SetDelegate GetSetMethodByReflection(FieldInfo fieldInfo)
		{
			return delegate(object source, object value)
			{
				fieldInfo.SetValue(source, value);
			};
		}

		// Token: 0x040002EA RID: 746
		private static readonly object[] EmptyObjects = new object[0];

		// Token: 0x040002EB RID: 747
		[ThreadStatic]
		private static object[] _1ObjArray;

		// Token: 0x02000347 RID: 839
		// (Invoke) Token: 0x06000B63 RID: 2915
		public delegate object GetDelegate(object source);

		// Token: 0x02000348 RID: 840
		// (Invoke) Token: 0x06000B67 RID: 2919
		public delegate void SetDelegate(object source, object value);

		// Token: 0x02000349 RID: 841
		// (Invoke) Token: 0x06000B6B RID: 2923
		public delegate object ConstructorDelegate(params object[] args);

		// Token: 0x0200034A RID: 842
		// (Invoke) Token: 0x06000B6F RID: 2927
		public delegate TValue ThreadSafeDictionaryValueFactory<TKey, TValue>(TKey key);

		// Token: 0x0200034B RID: 843
		public sealed class ThreadSafeDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
		{
			// Token: 0x06000B72 RID: 2930 RVA: 0x00037D8C File Offset: 0x00035F8C
			public ThreadSafeDictionary(ReflectionUtils.ThreadSafeDictionaryValueFactory<TKey, TValue> valueFactory)
			{
				this._valueFactory = valueFactory;
			}

			// Token: 0x06000B73 RID: 2931 RVA: 0x00037DA8 File Offset: 0x00035FA8
			private TValue Get(TKey key)
			{
				if (this._dictionary == null)
				{
					return this.AddValue(key);
				}
				TValue tvalue;
				if (!this._dictionary.TryGetValue(key, out tvalue))
				{
					return this.AddValue(key);
				}
				return tvalue;
			}

			// Token: 0x06000B74 RID: 2932 RVA: 0x00037DE0 File Offset: 0x00035FE0
			private TValue AddValue(TKey key)
			{
				TValue tvalue = this._valueFactory(key);
				object @lock = this._lock;
				lock (@lock)
				{
					if (this._dictionary == null)
					{
						this._dictionary = new Dictionary<TKey, TValue>();
						this._dictionary[key] = tvalue;
					}
					else
					{
						TValue tvalue2;
						if (this._dictionary.TryGetValue(key, out tvalue2))
						{
							return tvalue2;
						}
						Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(this._dictionary);
						dictionary[key] = tvalue;
						this._dictionary = dictionary;
					}
				}
				return tvalue;
			}

			// Token: 0x06000B75 RID: 2933 RVA: 0x00037E80 File Offset: 0x00036080
			public void Add(TKey key, TValue value)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000B76 RID: 2934 RVA: 0x00037E87 File Offset: 0x00036087
			public bool ContainsKey(TKey key)
			{
				return this._dictionary.ContainsKey(key);
			}

			// Token: 0x17000038 RID: 56
			// (get) Token: 0x06000B77 RID: 2935 RVA: 0x00037E95 File Offset: 0x00036095
			public ICollection<TKey> Keys
			{
				get
				{
					return this._dictionary.Keys;
				}
			}

			// Token: 0x06000B78 RID: 2936 RVA: 0x00037EA2 File Offset: 0x000360A2
			public bool Remove(TKey key)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000B79 RID: 2937 RVA: 0x00037EA9 File Offset: 0x000360A9
			public bool TryGetValue(TKey key, out TValue value)
			{
				value = this[key];
				return true;
			}

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x06000B7A RID: 2938 RVA: 0x00037EB9 File Offset: 0x000360B9
			public ICollection<TValue> Values
			{
				get
				{
					return this._dictionary.Values;
				}
			}

			// Token: 0x1700003A RID: 58
			public TValue this[TKey key]
			{
				get
				{
					return this.Get(key);
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x06000B7D RID: 2941 RVA: 0x00037ED6 File Offset: 0x000360D6
			public void Add(KeyValuePair<TKey, TValue> item)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000B7E RID: 2942 RVA: 0x00037EDD File Offset: 0x000360DD
			public void Clear()
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000B7F RID: 2943 RVA: 0x00037EE4 File Offset: 0x000360E4
			public bool Contains(KeyValuePair<TKey, TValue> item)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000B80 RID: 2944 RVA: 0x00037EEB File Offset: 0x000360EB
			public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
			{
				throw new NotImplementedException();
			}

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x06000B81 RID: 2945 RVA: 0x00037EF2 File Offset: 0x000360F2
			public int Count
			{
				get
				{
					return this._dictionary.Count;
				}
			}

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x06000B82 RID: 2946 RVA: 0x00037EFF File Offset: 0x000360FF
			public bool IsReadOnly
			{
				get
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x06000B83 RID: 2947 RVA: 0x00037F06 File Offset: 0x00036106
			public bool Remove(KeyValuePair<TKey, TValue> item)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06000B84 RID: 2948 RVA: 0x00037F0D File Offset: 0x0003610D
			public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
			{
				return this._dictionary.GetEnumerator();
			}

			// Token: 0x06000B85 RID: 2949 RVA: 0x00037F1F File Offset: 0x0003611F
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this._dictionary.GetEnumerator();
			}

			// Token: 0x04001261 RID: 4705
			private readonly object _lock = new object();

			// Token: 0x04001262 RID: 4706
			private readonly ReflectionUtils.ThreadSafeDictionaryValueFactory<TKey, TValue> _valueFactory;

			// Token: 0x04001263 RID: 4707
			private Dictionary<TKey, TValue> _dictionary;
		}
	}
}
