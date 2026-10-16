using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace XRL.World
{
	// Token: 0x0200094D RID: 2381
	public class GamePartBlueprint
	{
		// Token: 0x06006145 RID: 24901 RVA: 0x001DA688 File Offset: 0x001D8888
		public GamePartBlueprint(string Name)
			: this("XRL.World.Parts", Name)
		{
		}

		// Token: 0x06006146 RID: 24902 RVA: 0x001DA698 File Offset: 0x001D8898
		public GamePartBlueprint(string Namespace, string Name)
		{
			this.Namespace = Namespace;
			this.Name = Name;
			this.Reflector = GamePartBlueprint.PartReflectionCache.Get(this.T);
		}

		// Token: 0x06006147 RID: 24903 RVA: 0x001DA6E7 File Offset: 0x001D88E7
		public IEnumerable<KeyValuePair<string, string>> GetParameterStrings()
		{
			if (this._SettersCache != null)
			{
				foreach (KeyValuePair<string, GamePartBlueprint.PartSetter> keyValuePair in this._SettersCache)
				{
					yield return new KeyValuePair<string, string>(keyValuePair.Key, keyValuePair.Value.OriginalValue);
				}
				Dictionary<string, GamePartBlueprint.PartSetter>.Enumerator enumerator = default(Dictionary<string, GamePartBlueprint.PartSetter>.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06006148 RID: 24904 RVA: 0x001DA6F8 File Offset: 0x001D88F8
		// (set) Token: 0x06006149 RID: 24905 RVA: 0x001DA754 File Offset: 0x001D8954
		public Dictionary<string, string> Parameters
		{
			[Obsolete("Parameters are now stored parsed.  Use TryGetParameter<T> to get the parsed value, or GetParameterString() to get the original string.  If you really need a full iterator for Parameters to strings, use GetParameterStrings() -- Will remove Q2 2023")]
			get
			{
				Dictionary<string, GamePartBlueprint.PartSetter> settersCache = this._SettersCache;
				if (settersCache == null)
				{
					return null;
				}
				return settersCache.ToDictionary<KeyValuePair<string, GamePartBlueprint.PartSetter>, string, string>((KeyValuePair<string, GamePartBlueprint.PartSetter> _) => _.Key, (KeyValuePair<string, GamePartBlueprint.PartSetter> _) => _.Value.OriginalValue);
			}
			set
			{
				this._SettersCache = new Dictionary<string, GamePartBlueprint.PartSetter>((value != null) ? value.Count : 0);
				foreach (KeyValuePair<string, string> keyValuePair in value)
				{
					if (keyValuePair.Key == "ChanceOneIn")
					{
						int num;
						if (int.TryParse(keyValuePair.Value, out num))
						{
							this.ChanceOneIn = num;
						}
					}
					else if (keyValuePair.Key == "ChanceIn10000")
					{
						int num2;
						if (int.TryParse(keyValuePair.Value, out num2))
						{
							this.ChanceOneIn = 10000 / num2;
						}
					}
					else
					{
						ValueTuple<Type, GamePartBlueprint.PartReflectionCache.SetValue, ObsoleteAttribute> propertyOrField = this.Reflector.GetPropertyOrField(keyValuePair.Key);
						Type type = propertyOrField.Item1;
						GamePartBlueprint.PartReflectionCache.SetValue item = propertyOrField.Item2;
						ObsoleteAttribute item2 = propertyOrField.Item3;
						if (type == null || item == null)
						{
							type = typeof(string);
						}
						if (!(keyValuePair.Key == "Name"))
						{
							XmlDataHelper.AttributeParser attributeParser = XmlDataHelper.TryGetAttributeParser(type);
							if (attributeParser != null)
							{
								XmlDataHelper.AttributeParser.ParseDelegate parse = attributeParser._Parse;
								object obj = null;
								if (type.IsValueType || type == typeof(string))
								{
									obj = parse(keyValuePair.Value);
								}
								this._SettersCache.Add(keyValuePair.Key, new GamePartBlueprint.PartSetter(item, parse, keyValuePair.Value, obj));
							}
						}
					}
				}
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x0600614A RID: 24906 RVA: 0x001DA8EC File Offset: 0x001D8AEC
		public Type T
		{
			get
			{
				Type type;
				if ((type = this._T) == null)
				{
					type = (this._T = ModManager.ResolveType(this.Namespace, this.Name, false, false, true));
				}
				return type;
			}
		}

		// Token: 0x0600614B RID: 24907 RVA: 0x001DA920 File Offset: 0x001D8B20
		public bool HasParameter(string Parameter)
		{
			return this._SettersCache != null && this._SettersCache.ContainsKey(Parameter);
		}

		// Token: 0x0600614C RID: 24908 RVA: 0x001DA938 File Offset: 0x001D8B38
		public string GetParameterString(string Parameter, string Default = null)
		{
			GamePartBlueprint.PartSetter partSetter;
			if (this._SettersCache != null && this._SettersCache.TryGetValue(Parameter, out partSetter))
			{
				return partSetter.OriginalValue;
			}
			return Default;
		}

		// Token: 0x0600614D RID: 24909 RVA: 0x001DA968 File Offset: 0x001D8B68
		public T GetParameter<T>(string Name, T Default = default(T))
		{
			GamePartBlueprint.PartSetter partSetter;
			if (this._SettersCache != null && this._SettersCache.TryGetValue(Name, out partSetter))
			{
				return (T)((object)partSetter.GetParsedValue());
			}
			return Default;
		}

		// Token: 0x0600614E RID: 24910 RVA: 0x001DA99B File Offset: 0x001D8B9B
		[Obsolete("Use TryGetParameter")]
		public bool TryGetParameterValue<T>(string Name, out T Value)
		{
			return this.TryGetParameter<T>(Name, out Value);
		}

		// Token: 0x0600614F RID: 24911 RVA: 0x001DA9A8 File Offset: 0x001D8BA8
		public bool TryGetParameter<T>(string Name, out T Value)
		{
			Value = default(T);
			GamePartBlueprint.PartSetter partSetter;
			if (this._SettersCache != null && this._SettersCache.TryGetValue(Name, out partSetter))
			{
				Value = (T)((object)partSetter.GetParsedValue());
				return true;
			}
			return false;
		}

		// Token: 0x06006150 RID: 24912 RVA: 0x001DA9EC File Offset: 0x001D8BEC
		public void InitializePartInstance(object NewPart)
		{
			if (this._SettersCache == null)
			{
				return;
			}
			foreach (KeyValuePair<string, GamePartBlueprint.PartSetter> keyValuePair in this._SettersCache)
			{
				GamePartBlueprint.PartSetter value = keyValuePair.Value;
				GamePartBlueprint.PartReflectionCache.SetValue setValue = value.SetValue;
				if (setValue != null)
				{
					setValue(NewPart, value.GetParsedValue());
				}
			}
		}

		// Token: 0x06006151 RID: 24913 RVA: 0x001DAA64 File Offset: 0x001D8C64
		public void CopyFrom(GamePartBlueprint Source)
		{
			this.Name = Source.Name;
			this.Reflector = Source.Reflector;
			this._SettersCache = Source._SettersCache;
		}

		// Token: 0x04002DD0 RID: 11728
		public GamePartBlueprint.PartReflectionCache Reflector;

		// Token: 0x04002DD1 RID: 11729
		public string Name = "";

		// Token: 0x04002DD2 RID: 11730
		public string Namespace = "XRL.World.Parts";

		// Token: 0x04002DD3 RID: 11731
		public int ChanceOneIn = 1;

		// Token: 0x04002DD4 RID: 11732
		private Type _T;

		// Token: 0x04002DD5 RID: 11733
		protected Dictionary<string, GamePartBlueprint.PartSetter> _SettersCache;

		// Token: 0x02001A57 RID: 6743
		public class PartReflectionCache
		{
			/// <summary>Dynamically emit a casting delegate to directly call the setter of a property without reflection overhead.</summary>
			// Token: 0x0600E71A RID: 59162 RVA: 0x00533E1C File Offset: 0x0053201C
			private GamePartBlueprint.PartReflectionCache.SetValue CreateSetterDelegate(PropertyInfo Property)
			{
				MethodInfo setMethod = Property.GetSetMethod(true);
				Type declaringType = Property.DeclaringType;
				Type parameterType = setMethod.GetParameters()[0].ParameterType;
				DynamicMethod dynamicMethod = new DynamicMethod("PropertySetterDelegate_" + Property.Name, typeof(void), GamePartBlueprint.PartReflectionCache.DelegateSignature, declaringType, true);
				ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Castclass, declaringType);
				ilgenerator.Emit(OpCodes.Ldarg_1);
				ilgenerator.Emit(parameterType.IsValueType ? OpCodes.Unbox_Any : OpCodes.Castclass, parameterType);
				ilgenerator.Emit(setMethod.IsVirtual ? OpCodes.Callvirt : OpCodes.Call, setMethod);
				ilgenerator.Emit(OpCodes.Ret);
				return (GamePartBlueprint.PartReflectionCache.SetValue)dynamicMethod.CreateDelegate(typeof(GamePartBlueprint.PartReflectionCache.SetValue));
			}

			/// <summary>Dynamically emit a casting delegate to directly set a field without reflection overhead.</summary>
			// Token: 0x0600E71B RID: 59163 RVA: 0x00533EE8 File Offset: 0x005320E8
			private GamePartBlueprint.PartReflectionCache.SetValue CreateSetterDelegate(FieldInfo Field)
			{
				Type declaringType = Field.DeclaringType;
				Type fieldType = Field.FieldType;
				DynamicMethod dynamicMethod = new DynamicMethod("FieldSetterDelegate_" + Field.Name, typeof(void), GamePartBlueprint.PartReflectionCache.DelegateSignature, declaringType, true);
				ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
				ilgenerator.Emit(OpCodes.Ldarg_0);
				ilgenerator.Emit(OpCodes.Castclass, declaringType);
				ilgenerator.Emit(OpCodes.Ldarg_1);
				ilgenerator.Emit(fieldType.IsValueType ? OpCodes.Unbox_Any : OpCodes.Castclass, fieldType);
				ilgenerator.Emit(OpCodes.Stfld, Field);
				ilgenerator.Emit(OpCodes.Ret);
				return (GamePartBlueprint.PartReflectionCache.SetValue)dynamicMethod.CreateDelegate(typeof(GamePartBlueprint.PartReflectionCache.SetValue));
			}

			/// <summary>Dynamically emit a constructor delegate without reflection overhead.</summary>
			// Token: 0x0600E71C RID: 59164 RVA: 0x00533F98 File Offset: 0x00532198
			private GamePartBlueprint.PartReflectionCache.NewInstance CreateInstanceDelegate(Type Part)
			{
				DynamicMethod dynamicMethod = new DynamicMethod("DefaultConstructorDelegate_" + Part.Name, typeof(IPart), Type.EmptyTypes, Part, true);
				ILGenerator ilgenerator = dynamicMethod.GetILGenerator();
				ilgenerator.Emit(OpCodes.Newobj, Part.GetDefaultConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic));
				ilgenerator.Emit(OpCodes.Ret);
				return (GamePartBlueprint.PartReflectionCache.NewInstance)dynamicMethod.CreateDelegate(typeof(GamePartBlueprint.PartReflectionCache.NewInstance));
			}

			// Token: 0x0600E71D RID: 59165 RVA: 0x00534004 File Offset: 0x00532204
			private PartReflectionCache(Type T)
			{
				this.T = T;
				this.GetNewInstance = this.CreateInstanceDelegate(T);
				FieldInfo[] fields = T.GetFields(BindingFlags.Instance | BindingFlags.Public);
				PropertyInfo[] properties = T.GetProperties(BindingFlags.Instance | BindingFlags.Public);
				Dictionary<string, ValueTuple<Type, GamePartBlueprint.PartReflectionCache.SetValue, ObsoleteAttribute>> dictionary = new Dictionary<string, ValueTuple<Type, GamePartBlueprint.PartReflectionCache.SetValue, ObsoleteAttribute>>();
				foreach (FieldInfo fieldInfo in fields)
				{
					if (!fieldInfo.IsLiteral && !fieldInfo.IsInitOnly && !fieldInfo.Name.StartsWith("_"))
					{
						dictionary.TryAdd(fieldInfo.Name, new ValueTuple<Type, GamePartBlueprint.PartReflectionCache.SetValue, ObsoleteAttribute>(fieldInfo.FieldType, this.CreateSetterDelegate(fieldInfo), fieldInfo.GetCustomAttribute<ObsoleteAttribute>()));
					}
				}
				foreach (PropertyInfo propertyInfo in properties)
				{
					if (propertyInfo.CanWrite && !(propertyInfo.Name == "Name"))
					{
						dictionary.TryAdd(propertyInfo.Name, new ValueTuple<Type, GamePartBlueprint.PartReflectionCache.SetValue, ObsoleteAttribute>(propertyInfo.PropertyType, this.CreateSetterDelegate(propertyInfo), propertyInfo.GetCustomAttribute<ObsoleteAttribute>()));
					}
				}
				this.Settables = new ReadOnlyDictionary<string, ValueTuple<Type, GamePartBlueprint.PartReflectionCache.SetValue, ObsoleteAttribute>>(dictionary);
				if (T.GetProperty("Pool", BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public) != null)
				{
					IPart part = this.GetNewInstance();
					this.Pool = part.Pool;
					this.Pool.Return(part);
				}
			}

			// Token: 0x0600E71E RID: 59166 RVA: 0x0053414C File Offset: 0x0053234C
			[return: TupleElementNames(new string[] { "Type", "SetValue", "obsolete" })]
			public ValueTuple<Type, GamePartBlueprint.PartReflectionCache.SetValue, ObsoleteAttribute> GetPropertyOrField(string name)
			{
				ValueTuple<Type, GamePartBlueprint.PartReflectionCache.SetValue, ObsoleteAttribute> valueTuple;
				if (this.Settables.TryGetValue(name, out valueTuple))
				{
					return valueTuple;
				}
				return new ValueTuple<Type, GamePartBlueprint.PartReflectionCache.SetValue, ObsoleteAttribute>(null, null, null);
			}

			// Token: 0x0600E71F RID: 59167 RVA: 0x00534174 File Offset: 0x00532374
			public IPart GetInstance()
			{
				IPart part;
				if (this.Pool != null && this.Pool.TryGet(out part))
				{
					return part;
				}
				return this.GetNewInstance();
			}

			// Token: 0x0600E720 RID: 59168 RVA: 0x005341A8 File Offset: 0x005323A8
			public static GamePartBlueprint.PartReflectionCache Get(Type T)
			{
				if (T == null)
				{
					return null;
				}
				GamePartBlueprint.PartReflectionCache partReflectionCache;
				if (GamePartBlueprint.PartReflectionCache.CacheByType.TryGetValue(T, out partReflectionCache))
				{
					return partReflectionCache;
				}
				partReflectionCache = new GamePartBlueprint.PartReflectionCache(T);
				GamePartBlueprint.PartReflectionCache.CacheByType.Add(T, partReflectionCache);
				return partReflectionCache;
			}

			// Token: 0x0600E721 RID: 59169 RVA: 0x005341E0 File Offset: 0x005323E0
			public static void ClearOrphaned()
			{
				HashSet<Assembly> assemblies = ModManager.ActiveAssemblies.ToHashSet<Assembly>();
				Dictionary<Type, GamePartBlueprint.PartReflectionCache> cacheByType = GamePartBlueprint.PartReflectionCache.CacheByType;
				int num = ((cacheByType != null) ? cacheByType.RemoveAll<Type, GamePartBlueprint.PartReflectionCache>((KeyValuePair<Type, GamePartBlueprint.PartReflectionCache> x) => !assemblies.Contains(x.Key.Assembly)) : 0);
				MetricsManager.LogInfo(string.Format("Cleared {0} orphaned part reflection caches.", num));
			}

			// Token: 0x040068DD RID: 26845
			public readonly Type T;

			// Token: 0x040068DE RID: 26846
			public readonly GamePartBlueprint.PartReflectionCache.NewInstance GetNewInstance;

			// Token: 0x040068DF RID: 26847
			public IPartPool Pool;

			// Token: 0x040068E0 RID: 26848
			private static readonly Type[] DelegateSignature = new Type[]
			{
				typeof(object),
				typeof(object)
			};

			// Token: 0x040068E1 RID: 26849
			[TupleElementNames(new string[] { "Type", "SetValue", "Obsolete" })]
			public readonly ReadOnlyDictionary<string, ValueTuple<Type, GamePartBlueprint.PartReflectionCache.SetValue, ObsoleteAttribute>> Settables;

			// Token: 0x040068E2 RID: 26850
			public static readonly Dictionary<Type, GamePartBlueprint.PartReflectionCache> CacheByType = new Dictionary<Type, GamePartBlueprint.PartReflectionCache>();

			// Token: 0x02001E90 RID: 7824
			// (Invoke) Token: 0x0600F7D0 RID: 63440
			public delegate void SetValue(object target, object value);

			// Token: 0x02001E91 RID: 7825
			// (Invoke) Token: 0x0600F7D4 RID: 63444
			public delegate IPart NewInstance();
		}

		// Token: 0x02001A58 RID: 6744
		protected struct PartSetter
		{
			// Token: 0x0600E723 RID: 59171 RVA: 0x00534267 File Offset: 0x00532467
			public PartSetter(GamePartBlueprint.PartReflectionCache.SetValue SetValue, XmlDataHelper.AttributeParser.ParseDelegate ParseValue, string OriginalValue, object ParsedValue)
			{
				this.SetValue = SetValue;
				this.ParseValue = ParseValue;
				this.OriginalValue = OriginalValue;
				this.ParsedValue = ParsedValue;
			}

			// Token: 0x0600E724 RID: 59172 RVA: 0x00534286 File Offset: 0x00532486
			public object GetParsedValue()
			{
				return this.ParsedValue ?? this.ParseValue(this.OriginalValue);
			}

			// Token: 0x040068E3 RID: 26851
			public GamePartBlueprint.PartReflectionCache.SetValue SetValue;

			// Token: 0x040068E4 RID: 26852
			public XmlDataHelper.AttributeParser.ParseDelegate ParseValue;

			// Token: 0x040068E5 RID: 26853
			public string OriginalValue;

			// Token: 0x040068E6 RID: 26854
			public object ParsedValue;
		}
	}
}
