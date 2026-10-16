using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using UnityEngine;

namespace RoslynCSharp
{
	// Token: 0x0200000F RID: 15
	public sealed class ScriptType
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000401B File Offset: 0x0000221B
		public Type RawType
		{
			get
			{
				return this.rawType;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004023 File Offset: 0x00002223
		public string Name
		{
			get
			{
				return this.rawType.Name;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00004030 File Offset: 0x00002230
		public string Namespace
		{
			get
			{
				return this.rawType.Namespace;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000403D File Offset: 0x0000223D
		public string FullName
		{
			get
			{
				return this.rawType.FullName;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000404A File Offset: 0x0000224A
		public bool IsPublic
		{
			get
			{
				return this.rawType.IsPublic;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00004057 File Offset: 0x00002257
		public ScriptAssembly Assembly
		{
			get
			{
				return this.assembly;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x0000405F File Offset: 0x0000225F
		public ScriptType Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004067 File Offset: 0x00002267
		public bool IsNestedType
		{
			get
			{
				return this.parent != null;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00004072 File Offset: 0x00002272
		public ScriptType[] NestedTypes
		{
			get
			{
				return this.nestedTypes;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000407A File Offset: 0x0000227A
		public bool HasNestedTypes
		{
			get
			{
				return this.nestedTypes.Length != 0;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00004086 File Offset: 0x00002286
		public IScriptMemberProxy FieldsStatic
		{
			get
			{
				this.fields.throwOnError = true;
				return this.fields;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000409A File Offset: 0x0000229A
		public IScriptMemberProxy SafeFieldsStatic
		{
			get
			{
				this.fields.throwOnError = false;
				return this.fields;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000040AE File Offset: 0x000022AE
		public IScriptMemberProxy PropertiesStatic
		{
			get
			{
				this.properies.throwOnError = true;
				return this.properies;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000BE RID: 190 RVA: 0x000040C2 File Offset: 0x000022C2
		public IScriptMemberProxy SafePropertiesStatic
		{
			get
			{
				this.properies.throwOnError = false;
				return this.properies;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000040D6 File Offset: 0x000022D6
		public bool IsUnityObject
		{
			get
			{
				return this.IsSubTypeOf<global::UnityEngine.Object>();
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000040DE File Offset: 0x000022DE
		public bool IsMonoBehaviour
		{
			get
			{
				return this.IsSubTypeOf<MonoBehaviour>();
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x000040E6 File Offset: 0x000022E6
		public bool IsScriptableObject
		{
			get
			{
				return this.IsSubTypeOf<ScriptableObject>();
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000040EE File Offset: 0x000022EE
		public ICollection<object> CustomAttributes
		{
			get
			{
				this.GenerateAttributeInformation();
				return this.typeAttributes;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000040FC File Offset: 0x000022FC
		public ScriptType(Type type)
		{
			this.assembly = null;
			this.rawType = type;
			this.fields = new ScriptFieldProxy(true, this, null);
			this.properies = new ScriptPropertyProxy(true, this, null);
			Type[] array = type.GetNestedTypes(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			this.nestedTypes = new ScriptType[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				this.nestedTypes[i] = new ScriptType(array[i]);
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004190 File Offset: 0x00002390
		internal ScriptType(ScriptAssembly assembly, ScriptType parent, Type type)
		{
			this.assembly = assembly;
			this.parent = parent;
			this.rawType = type;
			this.fields = new ScriptFieldProxy(true, this, null);
			this.properies = new ScriptPropertyProxy(true, this, null);
			Type[] array = type.GetNestedTypes(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			this.nestedTypes = new ScriptType[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				this.nestedTypes[i] = new ScriptType(assembly, this, array[i]);
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000422C File Offset: 0x0000242C
		public ScriptProxy CreateInstance(GameObject parent = null)
		{
			if (this.IsMonoBehaviour)
			{
				return this.CreateBehaviourInstance(parent);
			}
			if (this.IsScriptableObject)
			{
				return this.CreateScriptableInstance();
			}
			return this.CreateCSharpInstance(Array.Empty<object>());
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004258 File Offset: 0x00002458
		public ScriptProxy CreateInstance(GameObject parent = null, params object[] parameters)
		{
			if (this.IsMonoBehaviour)
			{
				return this.CreateBehaviourInstance(parent);
			}
			if (this.IsScriptableObject)
			{
				return this.CreateScriptableInstance();
			}
			return this.CreateCSharpInstance(parameters);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004280 File Offset: 0x00002480
		public object CreateRawInstance(GameObject parent = null)
		{
			ScriptProxy scriptProxy = this.CreateInstance(parent);
			if (scriptProxy == null)
			{
				return null;
			}
			return scriptProxy.Instance;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000042A0 File Offset: 0x000024A0
		public object CreateRawInstance(GameObject parent = null, params object[] parameters)
		{
			ScriptProxy scriptProxy = this.CreateInstance(parent, parameters);
			if (scriptProxy == null)
			{
				return null;
			}
			return scriptProxy.Instance;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000042C4 File Offset: 0x000024C4
		public T CreateRawInstance<T>(GameObject parent = null)
		{
			ScriptProxy scriptProxy = this.CreateInstance(parent);
			if (scriptProxy == null)
			{
				return default(T);
			}
			return scriptProxy.GetInstanceAs<T>(false, default(T));
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000042F8 File Offset: 0x000024F8
		public T CreateRawInstance<T>(GameObject parent = null, params object[] parameters)
		{
			ScriptProxy scriptProxy = this.CreateInstance(parent);
			if (scriptProxy == null)
			{
				return default(T);
			}
			return scriptProxy.GetInstanceAs<T>(false, default(T));
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000432C File Offset: 0x0000252C
		private ScriptProxy CreateBehaviourInstance(GameObject parent)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent");
			}
			MonoBehaviour monoBehaviour = parent.AddComponent(this.rawType) as MonoBehaviour;
			if (monoBehaviour != null)
			{
				return new ScriptProxy(this, monoBehaviour);
			}
			return null;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004374 File Offset: 0x00002574
		private ScriptProxy CreateScriptableInstance()
		{
			ScriptableObject scriptableObject = ScriptableObject.CreateInstance(this.rawType);
			if (scriptableObject != null)
			{
				return new ScriptProxy(this, scriptableObject);
			}
			return null;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000043A0 File Offset: 0x000025A0
		private ScriptProxy CreateCSharpInstance(params object[] args)
		{
			object obj = null;
			try
			{
				obj = Activator.CreateInstance(this.rawType, BindingFlags.Default, null, args, null);
			}
			catch (MissingMethodException)
			{
				if (args.Length != 0)
				{
					return null;
				}
				obj = FormatterServices.GetUninitializedObject(this.rawType);
			}
			if (obj != null)
			{
				return new ScriptProxy(this, obj);
			}
			return null;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000043F8 File Offset: 0x000025F8
		public bool IsSubTypeOf(Type baseClass)
		{
			return baseClass.IsAssignableFrom(this.rawType);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004406 File Offset: 0x00002606
		public bool IsSubTypeOf<T>()
		{
			return this.IsSubTypeOf(typeof(T));
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004418 File Offset: 0x00002618
		public FieldInfo FindCachedField(string name, bool isStatic)
		{
			if (this.fieldCache.ContainsKey(name))
			{
				return this.fieldCache[name];
			}
			BindingFlags bindingFlags = (isStatic ? ScriptType.memberStaticFlags : ScriptType.memberInstanceFlags);
			FieldInfo field = this.rawType.GetField(name, bindingFlags);
			if (field == null)
			{
				return null;
			}
			this.fieldCache.Add(name, field);
			return field;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004478 File Offset: 0x00002678
		public PropertyInfo FindCachedProperty(string name, bool isStatic)
		{
			if (this.propertyCache.ContainsKey(name))
			{
				return this.propertyCache[name];
			}
			BindingFlags bindingFlags = (isStatic ? ScriptType.memberStaticFlags : ScriptType.memberInstanceFlags);
			PropertyInfo property = this.rawType.GetProperty(name, bindingFlags);
			if (property == null)
			{
				return null;
			}
			this.propertyCache.Add(name, property);
			return property;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000044D8 File Offset: 0x000026D8
		public MethodInfo FindCachedMethod(string name, bool isStatic)
		{
			if (this.methodCache.ContainsKey(name))
			{
				return this.methodCache[name];
			}
			BindingFlags bindingFlags = (isStatic ? ScriptType.memberStaticFlags : ScriptType.memberInstanceFlags);
			MethodInfo method = this.rawType.GetMethod(name, bindingFlags);
			if (method == null)
			{
				return null;
			}
			this.methodCache.Add(name, method);
			return method;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004538 File Offset: 0x00002738
		public object CallStatic(string methodName)
		{
			MethodInfo methodInfo = this.FindCachedMethod(methodName, true);
			if (methodInfo == null)
			{
				throw new TargetException(string.Format("Type '{0}' does not define a static method called '{1}'", this, methodName));
			}
			if (!methodInfo.IsStatic)
			{
				throw new TargetException(string.Format("The target method '{0}' is not marked as static and must be called on an object", methodName));
			}
			return methodInfo.Invoke(null, null);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004588 File Offset: 0x00002788
		public object CallStatic(string methodName, params object[] arguments)
		{
			MethodInfo methodInfo = this.FindCachedMethod(methodName, true);
			if (methodInfo == null)
			{
				throw new TargetException(string.Format("Type '{0}' does not define a static method called '{1}'", this, methodName));
			}
			if (!methodInfo.IsStatic)
			{
				throw new TargetException(string.Format("The target method '{0}' is not marked as static and must be called on an object", methodName));
			}
			return methodInfo.Invoke(null, arguments);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000045D8 File Offset: 0x000027D8
		public object SafeCallStatic(string method)
		{
			object obj;
			try
			{
				obj = this.CallStatic(method);
			}
			catch
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004608 File Offset: 0x00002808
		public object SafeCallStatic(string method, params object[] arguments)
		{
			object obj;
			try
			{
				obj = this.CallStatic(method, arguments);
			}
			catch
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00004638 File Offset: 0x00002838
		public bool HasAttribute(Type type, bool includeSubTypes = false)
		{
			foreach (object obj in this.typeAttributes)
			{
				if (!includeSubTypes)
				{
					if (obj.GetType() == type)
					{
						return true;
					}
				}
				else if (type.IsAssignableFrom(obj.GetType()))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000046B0 File Offset: 0x000028B0
		public bool HasAttribute<T>(bool includeSubTypes = false) where T : Attribute
		{
			return this.HasAttribute(typeof(T), includeSubTypes);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000046C4 File Offset: 0x000028C4
		public object GetAttribute(Type type, bool includeSubTypes = false)
		{
			foreach (object obj in this.typeAttributes)
			{
				if (!includeSubTypes)
				{
					if (obj.GetType() == type)
					{
						return obj;
					}
				}
				else if (type.IsAssignableFrom(obj.GetType()))
				{
					return obj;
				}
			}
			return null;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000473C File Offset: 0x0000293C
		public T GetAttribute<T>(bool includeSubTypes = false) where T : Attribute
		{
			return this.GetAttribute(typeof(T), includeSubTypes) as T;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000475C File Offset: 0x0000295C
		public object[] GetAttributes(Type type, bool includeSubTypes = false)
		{
			ScriptType.matchedAttributes.Clear();
			foreach (object obj in this.typeAttributes)
			{
				if (!includeSubTypes)
				{
					if (obj.GetType() == type)
					{
						ScriptType.matchedAttributes.Add(obj);
					}
				}
				else if (type.IsAssignableFrom(obj.GetType()))
				{
					ScriptType.matchedAttributes.Add(obj);
				}
			}
			return ScriptType.matchedAttributes.ToArray();
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000047F4 File Offset: 0x000029F4
		public T[] GetAttributes<T>(bool includeSubTypes = false) where T : Attribute
		{
			return this.GetAttributes(typeof(T), includeSubTypes) as T[];
		}

		// Token: 0x060000DD RID: 221 RVA: 0x0000480C File Offset: 0x00002A0C
		public override string ToString()
		{
			return string.Format("ScriptType({0})", this.rawType.Name);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004824 File Offset: 0x00002A24
		private void GenerateAttributeInformation()
		{
			if (this.typeAttributes == null)
			{
				this.typeAttributes = new HashSet<object>();
				foreach (object obj in this.rawType.GetCustomAttributes(false))
				{
					this.typeAttributes.Add(obj);
				}
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004870 File Offset: 0x00002A70
		public ScriptType FindNestedType(string nestedTypeName)
		{
			return Array.Find<ScriptType>(this.nestedTypes, (ScriptType t) => t.Name == nestedTypeName);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000048A4 File Offset: 0x00002AA4
		public ScriptType FindNestedTypeFullName(string nestedTypeFullName)
		{
			return Array.Find<ScriptType>(this.nestedTypes, (ScriptType t) => t.FullName == nestedTypeFullName);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000048D8 File Offset: 0x00002AD8
		public static ScriptType FindType(string typeName, ScriptDomain searchDomain = null)
		{
			if (!ScriptType.ResolveSearchDomain(ref searchDomain))
			{
				return null;
			}
			ScriptAssembly[] assemblies = searchDomain.Assemblies;
			for (int i = 0; i < assemblies.Length; i++)
			{
				ScriptType scriptType = assemblies[i].FindType(typeName);
				if (scriptType != null)
				{
					return scriptType;
				}
			}
			return null;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004918 File Offset: 0x00002B18
		public static ScriptType FindSubTypeOf(string typeName, Type subType, ScriptDomain searchDomain = null)
		{
			if (!ScriptType.ResolveSearchDomain(ref searchDomain))
			{
				return null;
			}
			ScriptAssembly[] assemblies = searchDomain.Assemblies;
			for (int i = 0; i < assemblies.Length; i++)
			{
				ScriptType scriptType = assemblies[i].FindSubTypeOf(subType, typeName);
				if (scriptType != null)
				{
					return scriptType;
				}
			}
			return null;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004958 File Offset: 0x00002B58
		public static ScriptType FindSubTypeOf<T>(string typeName, ScriptDomain searchDomain = null)
		{
			if (!ScriptType.ResolveSearchDomain(ref searchDomain))
			{
				return null;
			}
			ScriptAssembly[] assemblies = searchDomain.Assemblies;
			for (int i = 0; i < assemblies.Length; i++)
			{
				ScriptType scriptType = assemblies[i].FindSubTypeOf<T>(typeName, true);
				if (scriptType != null)
				{
					return scriptType;
				}
			}
			return null;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004998 File Offset: 0x00002B98
		public static ScriptType FindSubTypeOf(Type subType, ScriptDomain searchDomain = null)
		{
			if (!ScriptType.ResolveSearchDomain(ref searchDomain))
			{
				return null;
			}
			ScriptAssembly[] assemblies = searchDomain.Assemblies;
			for (int i = 0; i < assemblies.Length; i++)
			{
				ScriptType scriptType = assemblies[i].FindSubTypeOf(subType, true, true);
				if (scriptType != null)
				{
					return scriptType;
				}
			}
			return null;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000049D8 File Offset: 0x00002BD8
		public static ScriptType FindSubTypeOf<T>(ScriptDomain searchDomain = null)
		{
			if (!ScriptType.ResolveSearchDomain(ref searchDomain))
			{
				return null;
			}
			ScriptAssembly[] assemblies = searchDomain.Assemblies;
			for (int i = 0; i < assemblies.Length; i++)
			{
				ScriptType scriptType = assemblies[i].FindSubTypeOf<T>(true, true);
				if (scriptType != null)
				{
					return scriptType;
				}
			}
			return null;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004A18 File Offset: 0x00002C18
		public static ScriptType[] FindAllSubTypesOf(Type subType, bool includeNonPublic = true, ScriptDomain searchDomain = null)
		{
			if (!ScriptType.ResolveSearchDomain(ref searchDomain))
			{
				return new ScriptType[0];
			}
			ScriptType.matchedTypes.Clear();
			ScriptAssembly[] assemblies = searchDomain.Assemblies;
			for (int i = 0; i < assemblies.Length; i++)
			{
				ScriptType[] array = assemblies[i].FindAllSubTypesOf(subType, includeNonPublic, true);
				ScriptType.matchedTypes.AddRange(array);
			}
			return ScriptType.matchedTypes.ToArray();
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004A78 File Offset: 0x00002C78
		public static ScriptType[] FindAllSubTypesOf<T>(bool includeNonPublic = true, ScriptDomain searchDomain = null)
		{
			if (!ScriptType.ResolveSearchDomain(ref searchDomain))
			{
				return new ScriptType[0];
			}
			ScriptType.matchedTypes.Clear();
			ScriptAssembly[] assemblies = searchDomain.Assemblies;
			for (int i = 0; i < assemblies.Length; i++)
			{
				ScriptType[] array = assemblies[i].FindAllSubTypesOf<T>(includeNonPublic, true);
				ScriptType.matchedTypes.AddRange(array);
			}
			return ScriptType.matchedTypes.ToArray();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004AD4 File Offset: 0x00002CD4
		public static ScriptType[] FindAllTypes(bool includeNonPublic = true, ScriptDomain searchDomain = null)
		{
			if (!ScriptType.ResolveSearchDomain(ref searchDomain))
			{
				return new ScriptType[0];
			}
			ScriptType.matchedTypes.Clear();
			ScriptAssembly[] assemblies = searchDomain.Assemblies;
			for (int i = 0; i < assemblies.Length; i++)
			{
				ScriptType[] array = assemblies[i].FindAllTypes(includeNonPublic, true);
				ScriptType.matchedTypes.AddRange(array);
			}
			return ScriptType.matchedTypes.ToArray();
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004B30 File Offset: 0x00002D30
		public static ScriptType[] FindAllUnityTypes(bool includeNonPublic = true, ScriptDomain searchDomain = null)
		{
			if (!ScriptType.ResolveSearchDomain(ref searchDomain))
			{
				return new ScriptType[0];
			}
			ScriptType.matchedTypes.Clear();
			ScriptAssembly[] assemblies = searchDomain.Assemblies;
			for (int i = 0; i < assemblies.Length; i++)
			{
				ScriptType[] array = assemblies[i].FindAllUnityTypes(includeNonPublic, true);
				ScriptType.matchedTypes.AddRange(array);
			}
			return ScriptType.matchedTypes.ToArray();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004B8C File Offset: 0x00002D8C
		public static ScriptType[] FindAllMonoBehaviourTypes(bool includeNonPublic = true, ScriptDomain searchDomain = null)
		{
			if (!ScriptType.ResolveSearchDomain(ref searchDomain))
			{
				return new ScriptType[0];
			}
			ScriptType.matchedTypes.Clear();
			ScriptAssembly[] assemblies = searchDomain.Assemblies;
			for (int i = 0; i < assemblies.Length; i++)
			{
				ScriptType[] array = assemblies[i].FindAllMonoBehaviourTypes(includeNonPublic, true);
				ScriptType.matchedTypes.AddRange(array);
			}
			return ScriptType.matchedTypes.ToArray();
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004BE8 File Offset: 0x00002DE8
		public static ScriptType[] FindAllScriptableObjectTypes(bool includeNonPublic = true, ScriptDomain searchDomain = null)
		{
			if (!ScriptType.ResolveSearchDomain(ref searchDomain))
			{
				return new ScriptType[0];
			}
			ScriptType.matchedTypes.Clear();
			ScriptAssembly[] assemblies = searchDomain.Assemblies;
			for (int i = 0; i < assemblies.Length; i++)
			{
				ScriptType[] array = assemblies[i].FindAllScriptableObjectTypes(includeNonPublic, true);
				ScriptType.matchedTypes.AddRange(array);
			}
			return ScriptType.matchedTypes.ToArray();
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004C44 File Offset: 0x00002E44
		public static IEnumerable<ScriptType> EnumerateAllSubTypesOf(Type subType, bool includeNonPublic = true, ScriptDomain searchDomain = null)
		{
			if (!ScriptType.ResolveSearchDomain(ref searchDomain))
			{
				yield break;
			}
			foreach (ScriptAssembly scriptAssembly in searchDomain.Assemblies)
			{
				foreach (ScriptType scriptType in scriptAssembly.EnumerateAllSubTypesOf(subType, includeNonPublic, true))
				{
					yield return scriptType;
				}
				IEnumerator<ScriptType> enumerator = null;
			}
			ScriptAssembly[] array = null;
			yield break;
			yield break;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004C62 File Offset: 0x00002E62
		public static IEnumerable<ScriptType> EnumerateAllSubTypesOf<T>(bool includeNonPublic = true, ScriptDomain searchDomain = null)
		{
			if (!ScriptType.ResolveSearchDomain(ref searchDomain))
			{
				yield break;
			}
			foreach (ScriptAssembly scriptAssembly in searchDomain.Assemblies)
			{
				foreach (ScriptType scriptType in scriptAssembly.EnumerateAllSubTypesOf<T>(includeNonPublic, true))
				{
					yield return scriptType;
				}
				IEnumerator<ScriptType> enumerator = null;
			}
			ScriptAssembly[] array = null;
			yield break;
			yield break;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004C79 File Offset: 0x00002E79
		public static IEnumerable<ScriptType> EnumerateAllTypes(bool includeNonPublic = true, ScriptDomain searchDomain = null)
		{
			if (!ScriptType.ResolveSearchDomain(ref searchDomain))
			{
				yield break;
			}
			foreach (ScriptAssembly scriptAssembly in searchDomain.Assemblies)
			{
				foreach (ScriptType scriptType in scriptAssembly.EnumerateAllTypes(includeNonPublic, true))
				{
					yield return scriptType;
				}
				IEnumerator<ScriptType> enumerator = null;
			}
			ScriptAssembly[] array = null;
			yield break;
			yield break;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004C90 File Offset: 0x00002E90
		public static IEnumerable<ScriptType> EnumerateAllUnityTypes(bool includeNonPublic = true, ScriptDomain searchDomain = null)
		{
			if (!ScriptType.ResolveSearchDomain(ref searchDomain))
			{
				yield break;
			}
			foreach (ScriptAssembly scriptAssembly in searchDomain.Assemblies)
			{
				foreach (ScriptType scriptType in scriptAssembly.EnumerateAllUnityTypes(includeNonPublic, true))
				{
					yield return scriptType;
				}
				IEnumerator<ScriptType> enumerator = null;
			}
			ScriptAssembly[] array = null;
			yield break;
			yield break;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004CA7 File Offset: 0x00002EA7
		public static IEnumerable<ScriptType> EnumerateAllMonoBehaviourTypes(bool includeNonPublic = true, ScriptDomain searchDomain = null)
		{
			if (!ScriptType.ResolveSearchDomain(ref searchDomain))
			{
				yield break;
			}
			foreach (ScriptAssembly scriptAssembly in searchDomain.Assemblies)
			{
				foreach (ScriptType scriptType in scriptAssembly.EnumerateAllMonoBehaviourTypes(includeNonPublic, true))
				{
					yield return scriptType;
				}
				IEnumerator<ScriptType> enumerator = null;
			}
			ScriptAssembly[] array = null;
			yield break;
			yield break;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004CBE File Offset: 0x00002EBE
		public static IEnumerable<ScriptType> EnumerateAllScriptableObjectTypes(bool includeNonPublic = true, ScriptDomain searchDomain = null)
		{
			if (!ScriptType.ResolveSearchDomain(ref searchDomain))
			{
				yield break;
			}
			foreach (ScriptAssembly scriptAssembly in searchDomain.Assemblies)
			{
				foreach (ScriptType scriptType in scriptAssembly.EnumerateAllScriptableObjectTypes(includeNonPublic, true))
				{
					yield return scriptType;
				}
				IEnumerator<ScriptType> enumerator = null;
			}
			ScriptAssembly[] array = null;
			yield break;
			yield break;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00004CD5 File Offset: 0x00002ED5
		private static bool ResolveSearchDomain(ref ScriptDomain searchDomain)
		{
			if (searchDomain == null)
			{
				searchDomain = ScriptDomain.Active;
				if (searchDomain == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0400004E RID: 78
		private static BindingFlags memberFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;

		// Token: 0x0400004F RID: 79
		private static BindingFlags memberInstanceFlags = BindingFlags.Instance | ScriptType.memberFlags;

		// Token: 0x04000050 RID: 80
		private static BindingFlags memberStaticFlags = BindingFlags.Static | ScriptType.memberFlags;

		// Token: 0x04000051 RID: 81
		private static List<ScriptType> matchedTypes = new List<ScriptType>();

		// Token: 0x04000052 RID: 82
		private static List<object> matchedAttributes = new List<object>();

		// Token: 0x04000053 RID: 83
		private HashSet<object> typeAttributes;

		// Token: 0x04000054 RID: 84
		private Dictionary<string, FieldInfo> fieldCache = new Dictionary<string, FieldInfo>();

		// Token: 0x04000055 RID: 85
		private Dictionary<string, PropertyInfo> propertyCache = new Dictionary<string, PropertyInfo>();

		// Token: 0x04000056 RID: 86
		private Dictionary<string, MethodInfo> methodCache = new Dictionary<string, MethodInfo>();

		// Token: 0x04000057 RID: 87
		private Type rawType;

		// Token: 0x04000058 RID: 88
		private ScriptAssembly assembly;

		// Token: 0x04000059 RID: 89
		private ScriptType parent;

		// Token: 0x0400005A RID: 90
		private ScriptType[] nestedTypes;

		// Token: 0x0400005B RID: 91
		private ScriptFieldProxy fields;

		// Token: 0x0400005C RID: 92
		private ScriptPropertyProxy properies;
	}
}
