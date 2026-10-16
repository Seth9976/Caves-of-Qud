using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Mono.Cecil;
using Mono.Collections.Generic;

namespace MonoMod.Utils
{
	// Token: 0x02000450 RID: 1104
	public static class ReflectionHelper
	{
		// Token: 0x06001773 RID: 6003 RVA: 0x00051730 File Offset: 0x0004F930
		private static MemberInfo _Cache(string cacheKey, MemberInfo value)
		{
			if (cacheKey != null && value == null)
			{
				MMDbgLog.Log("ResolveRefl failure: " + cacheKey);
			}
			if (cacheKey != null && value != null)
			{
				Dictionary<string, WeakReference> resolveReflectionCache = ReflectionHelper.ResolveReflectionCache;
				lock (resolveReflectionCache)
				{
					ReflectionHelper.ResolveReflectionCache[cacheKey] = new WeakReference(value);
				}
			}
			return value;
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x000517A4 File Offset: 0x0004F9A4
		public static Assembly Load(ModuleDefinition module)
		{
			Assembly assembly;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				module.Write(memoryStream);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				assembly = ReflectionHelper.Load(memoryStream);
			}
			return assembly;
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x000517EC File Offset: 0x0004F9EC
		public static Assembly Load(Stream stream)
		{
			MemoryStream memoryStream = stream as MemoryStream;
			Assembly asm;
			if (memoryStream != null)
			{
				asm = Assembly.Load(memoryStream.GetBuffer());
			}
			else
			{
				using (MemoryStream memoryStream2 = new MemoryStream())
				{
					byte[] array = new byte[4096];
					int num;
					while (0 < (num = stream.Read(array, 0, array.Length)))
					{
						memoryStream2.Write(array, 0, num);
					}
					memoryStream2.Seek(0L, SeekOrigin.Begin);
					asm = Assembly.Load(memoryStream2.GetBuffer());
				}
			}
			AppDomain.CurrentDomain.AssemblyResolve += delegate(object s, ResolveEventArgs e)
			{
				if (!(e.Name == asm.FullName))
				{
					return null;
				}
				return asm;
			};
			return asm;
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x000518A0 File Offset: 0x0004FAA0
		public static Type GetType(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			Type type = Type.GetType(name);
			if (type != null)
			{
				return type;
			}
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			for (int i = 0; i < assemblies.Length; i++)
			{
				type = assemblies[i].GetType(name);
				if (type != null)
				{
					return type;
				}
			}
			return null;
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x000518F8 File Offset: 0x0004FAF8
		public static void ApplyRuntimeHash(this AssemblyNameReference asmRef, Assembly asm)
		{
			byte[] array = new byte[ReflectionHelper.AssemblyHashPrefix.Length + 4];
			Array.Copy(ReflectionHelper.AssemblyHashPrefix, 0, array, 0, ReflectionHelper.AssemblyHashPrefix.Length);
			Array.Copy(BitConverter.GetBytes(asm.GetHashCode()), 0, array, ReflectionHelper.AssemblyHashPrefix.Length, 4);
			asmRef.HashAlgorithm = (AssemblyHashAlgorithm)4294967295U;
			asmRef.Hash = array;
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x00051950 File Offset: 0x0004FB50
		public static string GetRuntimeHashedFullName(this Assembly asm)
		{
			return string.Format("{0}{1}{2}", asm.FullName, ReflectionHelper.AssemblyHashNameTag, asm.GetHashCode());
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x00051974 File Offset: 0x0004FB74
		public static string GetRuntimeHashedFullName(this AssemblyNameReference asm)
		{
			if (asm.HashAlgorithm != (AssemblyHashAlgorithm)4294967295U)
			{
				return asm.FullName;
			}
			byte[] hash = asm.Hash;
			if (hash.Length != ReflectionHelper.AssemblyHashPrefix.Length + 4)
			{
				return asm.FullName;
			}
			for (int i = 0; i < ReflectionHelper.AssemblyHashPrefix.Length; i++)
			{
				if (hash[i] != ReflectionHelper.AssemblyHashPrefix[i])
				{
					return asm.FullName;
				}
			}
			return string.Format("{0}{1}{2}", asm.FullName, ReflectionHelper.AssemblyHashNameTag, BitConverter.ToInt32(hash, ReflectionHelper.AssemblyHashPrefix.Length));
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x000519F9 File Offset: 0x0004FBF9
		public static Type ResolveReflection(this TypeReference mref)
		{
			return ReflectionHelper._ResolveReflection(mref, null) as Type;
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x00051A07 File Offset: 0x0004FC07
		public static MethodBase ResolveReflection(this MethodReference mref)
		{
			return ReflectionHelper._ResolveReflection(mref, null) as MethodBase;
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x00051A15 File Offset: 0x0004FC15
		public static FieldInfo ResolveReflection(this FieldReference mref)
		{
			return ReflectionHelper._ResolveReflection(mref, null) as FieldInfo;
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x00051A23 File Offset: 0x0004FC23
		public static PropertyInfo ResolveReflection(this PropertyReference mref)
		{
			return ReflectionHelper._ResolveReflection(mref, null) as PropertyInfo;
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x00051A31 File Offset: 0x0004FC31
		public static EventInfo ResolveReflection(this EventReference mref)
		{
			return ReflectionHelper._ResolveReflection(mref, null) as EventInfo;
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x00051A3F File Offset: 0x0004FC3F
		public static MemberInfo ResolveReflection(this MemberReference mref)
		{
			return ReflectionHelper._ResolveReflection(mref, null);
		}

		// Token: 0x06001780 RID: 6016 RVA: 0x00051A48 File Offset: 0x0004FC48
		private static MemberInfo _ResolveReflection(MemberReference mref, Module[] modules)
		{
			if (mref == null)
			{
				return null;
			}
			DynamicMethodReference dynamicMethodReference = mref as DynamicMethodReference;
			if (dynamicMethodReference != null)
			{
				return dynamicMethodReference.DynamicMethod;
			}
			MethodReference methodReference = mref as MethodReference;
			string text = ((methodReference != null) ? methodReference.GetID(null, null, true, false) : null) ?? mref.FullName;
			TypeReference typeReference;
			if ((typeReference = mref.DeclaringType) == null)
			{
				typeReference = (mref as TypeReference) ?? null;
			}
			TypeReference typeReference2 = typeReference;
			IMetadataScope metadataScope = ((typeReference2 != null) ? typeReference2.Scope : null);
			AssemblyNameReference assemblyNameReference = metadataScope as AssemblyNameReference;
			string asmName;
			string moduleName;
			if (assemblyNameReference == null)
			{
				ModuleDefinition moduleDefinition = metadataScope as ModuleDefinition;
				if (moduleDefinition == null)
				{
					if (!(metadataScope is ModuleReference))
					{
						if (metadataScope != null)
						{
						}
						asmName = null;
						moduleName = null;
					}
					else
					{
						asmName = typeReference2.Module.Assembly.Name.GetRuntimeHashedFullName();
						moduleName = typeReference2.Module.Name;
					}
				}
				else
				{
					asmName = moduleDefinition.Assembly.Name.GetRuntimeHashedFullName();
					moduleName = moduleDefinition.Name;
				}
			}
			else
			{
				asmName = assemblyNameReference.GetRuntimeHashedFullName();
				moduleName = null;
			}
			text = string.Concat(new string[]
			{
				text,
				" | ",
				asmName ?? "NOASSEMBLY",
				", ",
				moduleName ?? "NOMODULE"
			});
			Dictionary<string, WeakReference> dictionary = ReflectionHelper.ResolveReflectionCache;
			lock (dictionary)
			{
				WeakReference weakReference;
				if (ReflectionHelper.ResolveReflectionCache.TryGetValue(text, out weakReference) && weakReference != null)
				{
					MemberInfo memberInfo = weakReference.SafeGetTarget() as MemberInfo;
					if (memberInfo != null)
					{
						return memberInfo;
					}
				}
			}
			if (mref is GenericParameter)
			{
				throw new NotSupportedException("ResolveReflection on GenericParameter currently not supported");
			}
			MethodReference methodReference2 = mref as MethodReference;
			Type type;
			if (methodReference2 != null && mref.DeclaringType is ArrayType)
			{
				type = ReflectionHelper._ResolveReflection(mref.DeclaringType, modules) as Type;
				string methodID = methodReference2.GetID(null, null, false, false);
				MethodBase methodBase = type.GetMethods((BindingFlags)(-1)).Cast<MethodBase>().Concat<MethodBase>(type.GetConstructors((BindingFlags)(-1)))
					.FirstOrDefault<MethodBase>((MethodBase m) => m.GetID(null, null, false, false, false) == methodID);
				if (methodBase != null)
				{
					return ReflectionHelper._Cache(text, methodBase);
				}
			}
			if (typeReference2 == null)
			{
				throw new ArgumentException("MemberReference hasn't got a DeclaringType / isn't a TypeReference in itself");
			}
			if (asmName == null && moduleName == null)
			{
				throw new NotSupportedException("Unsupported scope type " + typeReference2.Scope.GetType().FullName);
			}
			bool flag2 = true;
			bool flag3 = false;
			bool flag4 = false;
			Func<Type, bool> <>9__20;
			Func<MethodInfo, bool> <>9__21;
			Func<FieldInfo, bool> <>9__22;
			TypeSpecification typeSpecification;
			MemberInfo memberInfo2;
			for (;;)
			{
				if (flag4)
				{
					modules = null;
				}
				flag4 = true;
				if (modules == null)
				{
					Assembly[] array = null;
					if (flag2 && flag3)
					{
						flag3 = false;
						flag2 = false;
					}
					if (flag2)
					{
						dictionary = ReflectionHelper.AssemblyCache;
						lock (dictionary)
						{
							WeakReference weakReference2;
							if (ReflectionHelper.AssemblyCache.TryGetValue(asmName, out weakReference2))
							{
								Assembly assembly = weakReference2.SafeGetTarget() as Assembly;
								if (assembly != null)
								{
									array = new Assembly[] { assembly };
								}
							}
						}
					}
					if (array == null && !flag3)
					{
						Dictionary<string, WeakReference[]> dictionary2 = ReflectionHelper.AssembliesCache;
						lock (dictionary2)
						{
							WeakReference[] array2;
							if (ReflectionHelper.AssembliesCache.TryGetValue(asmName, out array2))
							{
								array = (from asmRef in array2
									select asmRef.SafeGetTarget() as Assembly into asm
									where asm != null
									select asm).ToArray<Assembly>();
							}
						}
					}
					if (array == null)
					{
						int num = asmName.IndexOf(ReflectionHelper.AssemblyHashNameTag, StringComparison.Ordinal);
						int hash;
						if (num != -1 && int.TryParse(asmName.Substring(num + 2), out hash))
						{
							array = (from other in AppDomain.CurrentDomain.GetAssemblies()
								where other.GetHashCode() == hash
								select other).ToArray<Assembly>();
							if (array.Length == 0)
							{
								array = null;
							}
							asmName = asmName.Substring(0, num);
						}
						if (array == null)
						{
							array = (from other in AppDomain.CurrentDomain.GetAssemblies()
								where other.GetName().FullName == asmName
								select other).ToArray<Assembly>();
							if (array.Length == 0)
							{
								array = (from other in AppDomain.CurrentDomain.GetAssemblies()
									where other.GetName().Name == asmName
									select other).ToArray<Assembly>();
							}
							if (array.Length == 0)
							{
								Assembly assembly2 = Assembly.Load(new AssemblyName(asmName));
								if (assembly2 != null)
								{
									array = new Assembly[] { assembly2 };
								}
							}
						}
						if (array.Length != 0)
						{
							Dictionary<string, WeakReference[]> dictionary2 = ReflectionHelper.AssembliesCache;
							lock (dictionary2)
							{
								ReflectionHelper.AssembliesCache[asmName] = array.Select<Assembly, WeakReference>((Assembly asm) => new WeakReference(asm)).ToArray<WeakReference>();
							}
						}
					}
					IEnumerable<Module> enumerable;
					if (!string.IsNullOrEmpty(moduleName))
					{
						enumerable = array.Select<Assembly, Module>((Assembly asm) => asm.GetModule(moduleName));
					}
					else
					{
						enumerable = array.SelectMany<Assembly, Module>((Assembly asm) => asm.GetModules());
					}
					modules = enumerable.Where<Module>((Module mod) => mod != null).ToArray<Module>();
					if (modules.Length == 0)
					{
						break;
					}
				}
				TypeReference typeReference3 = mref as TypeReference;
				if (typeReference3 != null)
				{
					if (typeReference3.FullName == "<Module>")
					{
						goto Block_45;
					}
					typeSpecification = mref as TypeSpecification;
					if (typeSpecification != null)
					{
						goto Block_46;
					}
					type = modules.Select<Module, Type>((Module module) => module.GetType(mref.FullName.Replace("/", "+", StringComparison.Ordinal), false, false)).FirstOrDefault<Type>((Type m) => m != null);
					if (type == null)
					{
						type = modules.Select<Module, Type>(delegate(Module module)
						{
							IEnumerable<Type> types = module.GetTypes();
							Func<Type, bool> func;
							if ((func = <>9__20) == null)
							{
								func = (<>9__20 = (Type m) => mref.Is(m));
							}
							return types.FirstOrDefault<Type>(func);
						}).FirstOrDefault<Type>((Type m) => m != null);
					}
					if (!(type == null) || flag3)
					{
						goto IL_075F;
					}
				}
				else
				{
					bool flag5 = mref.DeclaringType.FullName == "<Module>";
					GenericInstanceMethod genericInstanceMethod = mref as GenericInstanceMethod;
					if (genericInstanceMethod != null)
					{
						memberInfo2 = ReflectionHelper._ResolveReflection(genericInstanceMethod.ElementMethod, modules);
						MethodInfo methodInfo = memberInfo2 as MethodInfo;
						MemberInfo memberInfo3;
						if (methodInfo == null)
						{
							memberInfo3 = null;
						}
						else
						{
							memberInfo3 = methodInfo.MakeGenericMethod(genericInstanceMethod.GenericArguments.Select<TypeReference, Type>((TypeReference arg) => ReflectionHelper._ResolveReflection(arg, null) as Type).ToArray<Type>());
						}
						memberInfo2 = memberInfo3;
					}
					else if (flag5)
					{
						if (mref is MethodReference)
						{
							memberInfo2 = modules.Select<Module, MethodInfo>(delegate(Module module)
							{
								IEnumerable<MethodInfo> methods = module.GetMethods((BindingFlags)(-1));
								Func<MethodInfo, bool> func2;
								if ((func2 = <>9__21) == null)
								{
									func2 = (<>9__21 = (MethodInfo m) => mref.Is(m));
								}
								return methods.FirstOrDefault<MethodInfo>(func2);
							}).FirstOrDefault<MethodInfo>((MethodInfo m) => m != null);
						}
						else
						{
							if (!(mref is FieldReference))
							{
								goto IL_0889;
							}
							memberInfo2 = modules.Select<Module, FieldInfo>(delegate(Module module)
							{
								IEnumerable<FieldInfo> fields = module.GetFields((BindingFlags)(-1));
								Func<FieldInfo, bool> func3;
								if ((func3 = <>9__22) == null)
								{
									func3 = (<>9__22 = (FieldInfo m) => mref.Is(m));
								}
								return fields.FirstOrDefault<FieldInfo>(func3);
							}).FirstOrDefault<FieldInfo>((FieldInfo m) => m != null);
						}
					}
					else
					{
						Type type2 = ReflectionHelper._ResolveReflection(mref.DeclaringType, modules) as Type;
						if (mref is MethodReference)
						{
							memberInfo2 = type2.GetMethods((BindingFlags)(-1)).Cast<MethodBase>().Concat<MethodBase>(type2.GetConstructors((BindingFlags)(-1)))
								.FirstOrDefault<MethodBase>((MethodBase m) => mref.Is(m));
						}
						else if (mref is FieldReference)
						{
							memberInfo2 = type2.GetFields((BindingFlags)(-1)).FirstOrDefault<FieldInfo>((FieldInfo m) => mref.Is(m));
						}
						else
						{
							memberInfo2 = type2.GetMembers((BindingFlags)(-1)).FirstOrDefault<MemberInfo>((MemberInfo m) => mref.Is(m));
						}
					}
					if (!(memberInfo2 == null) || flag3)
					{
						goto IL_0953;
					}
				}
				flag3 = true;
			}
			throw new Exception("Cannot resolve assembly / module " + asmName + " / " + moduleName);
			Block_45:
			throw new ArgumentException("Type <Module> cannot be resolved to a runtime reflection type");
			Block_46:
			type = ReflectionHelper._ResolveReflection(typeSpecification.ElementType, null) as Type;
			if (type == null)
			{
				return null;
			}
			if (typeSpecification.IsByReference)
			{
				return ReflectionHelper._Cache(text, type.MakeByRefType());
			}
			if (typeSpecification.IsPointer)
			{
				return ReflectionHelper._Cache(text, type.MakePointerType());
			}
			if (typeSpecification.IsArray)
			{
				return ReflectionHelper._Cache(text, (typeSpecification as ArrayType).IsVector ? type.MakeArrayType() : type.MakeArrayType((typeSpecification as ArrayType).Dimensions.Count));
			}
			if (typeSpecification.IsGenericInstance)
			{
				return ReflectionHelper._Cache(text, type.MakeGenericType((typeSpecification as GenericInstanceType).GenericArguments.Select<TypeReference, Type>((TypeReference arg) => ReflectionHelper._ResolveReflection(arg, null) as Type).ToArray<Type>()));
			}
			IL_075F:
			return ReflectionHelper._Cache(text, type);
			IL_0889:
			throw new NotSupportedException("Unsupported <Module> member type " + mref.GetType().FullName);
			IL_0953:
			return ReflectionHelper._Cache(text, memberInfo2);
		}

		// Token: 0x06001781 RID: 6017 RVA: 0x000523E8 File Offset: 0x000505E8
		public static SignatureHelper ResolveReflection(this Mono.Cecil.CallSite csite, Module context)
		{
			return csite.ResolveReflectionSignature(context);
		}

		// Token: 0x06001782 RID: 6018 RVA: 0x000523F4 File Offset: 0x000505F4
		public static SignatureHelper ResolveReflectionSignature(this IMethodSignature csite, Module context)
		{
			SignatureHelper signatureHelper;
			switch (csite.CallingConvention)
			{
			case MethodCallingConvention.C:
				signatureHelper = SignatureHelper.GetMethodSigHelper(context, CallingConvention.Cdecl, csite.ReturnType.ResolveReflection());
				break;
			case MethodCallingConvention.StdCall:
				signatureHelper = SignatureHelper.GetMethodSigHelper(context, CallingConvention.StdCall, csite.ReturnType.ResolveReflection());
				break;
			case MethodCallingConvention.ThisCall:
				signatureHelper = SignatureHelper.GetMethodSigHelper(context, CallingConvention.ThisCall, csite.ReturnType.ResolveReflection());
				break;
			case MethodCallingConvention.FastCall:
				signatureHelper = SignatureHelper.GetMethodSigHelper(context, CallingConvention.FastCall, csite.ReturnType.ResolveReflection());
				break;
			case MethodCallingConvention.VarArg:
				signatureHelper = SignatureHelper.GetMethodSigHelper(context, CallingConventions.VarArgs, csite.ReturnType.ResolveReflection());
				break;
			default:
				if (csite.ExplicitThis)
				{
					signatureHelper = SignatureHelper.GetMethodSigHelper(context, CallingConventions.ExplicitThis, csite.ReturnType.ResolveReflection());
				}
				else
				{
					signatureHelper = SignatureHelper.GetMethodSigHelper(context, CallingConventions.Standard, csite.ReturnType.ResolveReflection());
				}
				break;
			}
			if (context != null)
			{
				List<Type> list = new List<Type>();
				List<Type> list2 = new List<Type>();
				using (Collection<ParameterDefinition>.Enumerator enumerator = csite.Parameters.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						ParameterDefinition parameterDefinition = enumerator.Current;
						if (parameterDefinition.ParameterType.IsSentinel)
						{
							signatureHelper.AddSentinel();
						}
						if (parameterDefinition.ParameterType.IsPinned)
						{
							signatureHelper.AddArgument(parameterDefinition.ParameterType.ResolveReflection(), true);
						}
						else
						{
							list2.Clear();
							list.Clear();
							TypeReference typeReference = parameterDefinition.ParameterType;
							for (;;)
							{
								TypeSpecification typeSpecification = typeReference as TypeSpecification;
								if (typeSpecification == null)
								{
									break;
								}
								RequiredModifierType requiredModifierType = typeReference as RequiredModifierType;
								if (requiredModifierType == null)
								{
									OptionalModifierType optionalModifierType = typeReference as OptionalModifierType;
									if (optionalModifierType != null)
									{
										list2.Add(optionalModifierType.ModifierType.ResolveReflection());
									}
								}
								else
								{
									list.Add(requiredModifierType.ModifierType.ResolveReflection());
								}
								typeReference = typeSpecification.ElementType;
							}
							signatureHelper.AddArgument(parameterDefinition.ParameterType.ResolveReflection(), list.ToArray(), list2.ToArray());
						}
					}
					return signatureHelper;
				}
			}
			foreach (ParameterDefinition parameterDefinition2 in csite.Parameters)
			{
				signatureHelper.AddArgument(parameterDefinition2.ParameterType.ResolveReflection());
			}
			return signatureHelper;
		}

		// Token: 0x06001783 RID: 6019 RVA: 0x0005263C File Offset: 0x0005083C
		static ReflectionHelper()
		{
			object[] array = new object[2];
			array[0] = 0;
			ReflectionHelper._CacheGetterArgs = array;
			ReflectionHelper.t_RuntimeType = typeof(Type).Assembly.GetType("System.RuntimeType");
			Type type = ReflectionHelper.t_RuntimeType;
			ReflectionHelper.t_RuntimeTypeCache = ((type != null) ? type.GetNestedType("RuntimeTypeCache", BindingFlags.Public | BindingFlags.NonPublic) : null);
			PropertyInfo propertyInfo;
			if (!(ReflectionHelper.t_RuntimeTypeCache == null))
			{
				Type type2 = ReflectionHelper.t_RuntimeType;
				propertyInfo = ((type2 != null) ? type2.GetProperty("Cache", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, ReflectionHelper.t_RuntimeTypeCache, Type.EmptyTypes, null) : null);
			}
			else
			{
				propertyInfo = null;
			}
			ReflectionHelper.p_RuntimeType_Cache = propertyInfo;
			Type type3 = ReflectionHelper.t_RuntimeTypeCache;
			ReflectionHelper.m_RuntimeTypeCache_GetFieldList = ((type3 != null) ? type3.GetMethod("GetFieldList", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) : null);
			Type type4 = ReflectionHelper.t_RuntimeTypeCache;
			ReflectionHelper.m_RuntimeTypeCache_GetPropertyList = ((type4 != null) ? type4.GetMethod("GetPropertyList", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) : null);
			ReflectionHelper.fmap_CerArrayList_array = new Dictionary<Type, FieldInfo>();
			ReflectionHelper._CacheFixed = new ConditionalWeakTable<Type, ReflectionHelper.CacheFixEntry>();
			ReflectionHelper.t_RuntimeModule = typeof(Module).Assembly.GetType("System.Reflection.RuntimeModule");
			Type type5 = typeof(Module).Assembly.GetType("System.Reflection.RuntimeModule");
			ReflectionHelper.p_RuntimeModule_RuntimeType = ((type5 != null) ? type5.GetProperty("RuntimeType", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) : null);
			Type type6 = typeof(Module).Assembly.GetType("System.Reflection.RuntimeModule");
			ReflectionHelper.f_RuntimeModule__impl = ((type6 != null) ? type6.GetField("_impl", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) : null);
			Type type7 = typeof(Module).Assembly.GetType("System.Reflection.RuntimeModule");
			ReflectionHelper.m_RuntimeModule_GetGlobalType = ((type7 != null) ? type7.GetMethod("GetGlobalType", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic) : null);
			ReflectionHelper.f_SignatureHelper_module = typeof(SignatureHelper).GetField("m_module", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic) ?? typeof(SignatureHelper).GetField("module", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06001784 RID: 6020 RVA: 0x000528AF File Offset: 0x00050AAF
		public static void FixReflectionCacheAuto(this Type type)
		{
			type.FixReflectionCache();
		}

		// Token: 0x06001785 RID: 6021 RVA: 0x000528B8 File Offset: 0x00050AB8
		public static void FixReflectionCache(this Type type)
		{
			if (ReflectionHelper.t_RuntimeType == null || ReflectionHelper.p_RuntimeType_Cache == null || ReflectionHelper.m_RuntimeTypeCache_GetFieldList == null || ReflectionHelper.m_RuntimeTypeCache_GetPropertyList == null)
			{
				return;
			}
			while (type != null)
			{
				if (ReflectionHelper.t_RuntimeType.IsInstanceOfType(type))
				{
					ReflectionHelper.CacheFixEntry value = ReflectionHelper._CacheFixed.GetValue(type, delegate(Type rt)
					{
						ReflectionHelper.CacheFixEntry cacheFixEntry2 = new ReflectionHelper.CacheFixEntry();
						object obj = (cacheFixEntry2.Cache = ReflectionHelper.p_RuntimeType_Cache.GetValue(rt, ReflectionHelper._NoArgs));
						Array array = (cacheFixEntry2.Properties = ReflectionHelper._GetArray(obj, ReflectionHelper.m_RuntimeTypeCache_GetPropertyList));
						Array array2 = (cacheFixEntry2.Fields = ReflectionHelper._GetArray(obj, ReflectionHelper.m_RuntimeTypeCache_GetFieldList));
						ReflectionHelper._FixReflectionCacheOrder<PropertyInfo>(array);
						ReflectionHelper._FixReflectionCacheOrder<FieldInfo>(array2);
						cacheFixEntry2.NeedsVerify = false;
						return cacheFixEntry2;
					});
					if (value.NeedsVerify && !ReflectionHelper._Verify(value, type))
					{
						ReflectionHelper.CacheFixEntry cacheFixEntry = value;
						lock (cacheFixEntry)
						{
							ReflectionHelper._FixReflectionCacheOrder<PropertyInfo>(value.Properties);
							ReflectionHelper._FixReflectionCacheOrder<FieldInfo>(value.Fields);
						}
					}
					value.NeedsVerify = true;
				}
				type = type.DeclaringType;
			}
		}

		// Token: 0x06001786 RID: 6022 RVA: 0x000529A0 File Offset: 0x00050BA0
		private static bool _Verify(ReflectionHelper.CacheFixEntry entry, Type type)
		{
			object value;
			if (entry.Cache != (value = ReflectionHelper.p_RuntimeType_Cache.GetValue(type, ReflectionHelper._NoArgs)))
			{
				entry.Cache = value;
				entry.Properties = ReflectionHelper._GetArray(value, ReflectionHelper.m_RuntimeTypeCache_GetPropertyList);
				entry.Fields = ReflectionHelper._GetArray(value, ReflectionHelper.m_RuntimeTypeCache_GetFieldList);
				return false;
			}
			Array array;
			if (entry.Properties != (array = ReflectionHelper._GetArray(value, ReflectionHelper.m_RuntimeTypeCache_GetPropertyList)))
			{
				entry.Properties = array;
				entry.Fields = ReflectionHelper._GetArray(value, ReflectionHelper.m_RuntimeTypeCache_GetFieldList);
				return false;
			}
			Array array2;
			if (entry.Fields != (array2 = ReflectionHelper._GetArray(value, ReflectionHelper.m_RuntimeTypeCache_GetFieldList)))
			{
				entry.Fields = array2;
				return false;
			}
			return true;
		}

		// Token: 0x06001787 RID: 6023 RVA: 0x00052A40 File Offset: 0x00050C40
		private static Array _GetArray(object cache, MethodInfo getter)
		{
			getter.Invoke(cache, ReflectionHelper._CacheGetterArgs);
			object obj = getter.Invoke(cache, ReflectionHelper._CacheGetterArgs);
			Array array = obj as Array;
			if (array != null)
			{
				return array;
			}
			Type type = obj.GetType();
			if (type.IsGenericType && !type.IsGenericTypeDefinition && type.GetGenericTypeDefinition().FullName == "System.Reflection.CerArrayList`1")
			{
				FieldInfo fieldInfo;
				if (!ReflectionHelper.fmap_CerArrayList_array.TryGetValue(type, out fieldInfo))
				{
					fieldInfo = (ReflectionHelper.fmap_CerArrayList_array[type] = type.GetField("m_array", BindingFlags.Instance | BindingFlags.NonPublic));
				}
				return (Array)fieldInfo.GetValue(obj);
			}
			throw new NotSupportedException("Unsupported reflection cache array type: " + (((obj != null) ? obj.GetType().FullName : null) ?? "null"));
		}

		// Token: 0x06001788 RID: 6024 RVA: 0x00052B00 File Offset: 0x00050D00
		private static void _FixReflectionCacheOrder<T>(Array orig) where T : MemberInfo
		{
			List<T> list = new List<T>(orig.Length);
			for (int i = 0; i < orig.Length; i++)
			{
				object value = orig.GetValue(i);
				if (value == null)
				{
					break;
				}
				list.Add((T)((object)value));
			}
			list.Sort((T a, T b) => a.MetadataToken - b.MetadataToken);
			for (int j = list.Count - 1; j >= 0; j--)
			{
				orig.SetValue(list[j], j);
			}
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x00052B8C File Offset: 0x00050D8C
		public static Type GetModuleType(this Module module)
		{
			if (module == null || ReflectionHelper.t_RuntimeModule == null || !ReflectionHelper.t_RuntimeModule.IsInstanceOfType(module))
			{
				return null;
			}
			if (ReflectionHelper.p_RuntimeModule_RuntimeType != null)
			{
				return (Type)ReflectionHelper.p_RuntimeModule_RuntimeType.GetValue(module, ReflectionHelper._NoArgs);
			}
			if (ReflectionHelper.f_RuntimeModule__impl != null && ReflectionHelper.m_RuntimeModule_GetGlobalType != null)
			{
				return (Type)ReflectionHelper.m_RuntimeModule_GetGlobalType.Invoke(null, new object[] { ReflectionHelper.f_RuntimeModule__impl.GetValue(module) });
			}
			return null;
		}

		// Token: 0x0600178A RID: 6026 RVA: 0x00052C21 File Offset: 0x00050E21
		public static Type GetRealDeclaringType(this MemberInfo member)
		{
			Type type;
			if ((type = member.DeclaringType) == null)
			{
				Module module = member.Module;
				if (module == null)
				{
					return null;
				}
				type = module.GetModuleType();
			}
			return type;
		}

		// Token: 0x0600178B RID: 6027 RVA: 0x00052C3E File Offset: 0x00050E3E
		public static Mono.Cecil.CallSite ImportCallSite(this ModuleDefinition moduleTo, ICallSiteGenerator signature)
		{
			return signature.ToCallSite(moduleTo);
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x00052C47 File Offset: 0x00050E47
		public static Mono.Cecil.CallSite ImportCallSite(this ModuleDefinition moduleTo, SignatureHelper signature)
		{
			return moduleTo.ImportCallSite(ReflectionHelper.f_SignatureHelper_module.GetValue(signature) as Module, signature.GetSignature());
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x00052C65 File Offset: 0x00050E65
		public static Mono.Cecil.CallSite ImportCallSite(this ModuleDefinition moduleTo, Module moduleFrom, int token)
		{
			return moduleTo.ImportCallSite(moduleFrom, moduleFrom.ResolveSignature(token));
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x00052C78 File Offset: 0x00050E78
		public static Mono.Cecil.CallSite ImportCallSite(this ModuleDefinition moduleTo, Module moduleFrom, byte[] data)
		{
			ReflectionHelper.<>c__DisplayClass50_0 CS$<>8__locals1;
			CS$<>8__locals1.moduleTo = moduleTo;
			CS$<>8__locals1.moduleFrom = moduleFrom;
			Mono.Cecil.CallSite callSite = new Mono.Cecil.CallSite(CS$<>8__locals1.moduleTo.TypeSystem.Void);
			Mono.Cecil.CallSite callSite2;
			using (MemoryStream memoryStream = new MemoryStream(data, false))
			{
				ReflectionHelper.<>c__DisplayClass50_1 CS$<>8__locals2;
				CS$<>8__locals2.reader = new BinaryReader(memoryStream);
				try
				{
					ReflectionHelper.<ImportCallSite>g__ReadMethodSignature|50_0(callSite, ref CS$<>8__locals1, ref CS$<>8__locals2);
					callSite2 = callSite;
				}
				finally
				{
					if (CS$<>8__locals2.reader != null)
					{
						((IDisposable)CS$<>8__locals2.reader).Dispose();
					}
				}
			}
			return callSite2;
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x00052D0C File Offset: 0x00050F0C
		[CompilerGenerated]
		internal static void <ImportCallSite>g__ReadMethodSignature|50_0(IMethodSignature method, ref ReflectionHelper.<>c__DisplayClass50_0 A_1, ref ReflectionHelper.<>c__DisplayClass50_1 A_2)
		{
			byte b = A_2.reader.ReadByte();
			if ((b & 32) != 0)
			{
				method.HasThis = true;
				b = (byte)((int)b & -33);
			}
			if ((b & 64) != 0)
			{
				method.ExplicitThis = true;
				b = (byte)((int)b & -65);
			}
			method.CallingConvention = (MethodCallingConvention)b;
			if ((b & 16) != 0)
			{
				ReflectionHelper.<ImportCallSite>g__ReadCompressedUInt32|50_1(ref A_2);
			}
			uint num = ReflectionHelper.<ImportCallSite>g__ReadCompressedUInt32|50_1(ref A_2);
			method.MethodReturnType.ReturnType = ReflectionHelper.<ImportCallSite>g__ReadTypeSignature|50_4(ref A_1, ref A_2);
			int num2 = 0;
			while ((long)num2 < (long)((ulong)num))
			{
				method.Parameters.Add(new ParameterDefinition(ReflectionHelper.<ImportCallSite>g__ReadTypeSignature|50_4(ref A_1, ref A_2)));
				num2++;
			}
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x00052DA0 File Offset: 0x00050FA0
		[CompilerGenerated]
		internal static uint <ImportCallSite>g__ReadCompressedUInt32|50_1(ref ReflectionHelper.<>c__DisplayClass50_1 A_0)
		{
			byte b = A_0.reader.ReadByte();
			if ((b & 128) == 0)
			{
				return (uint)b;
			}
			if ((b & 64) == 0)
			{
				return (((uint)b & 4294967167U) << 8) | (uint)A_0.reader.ReadByte();
			}
			return (uint)((((int)b & -193) << 24) | ((int)A_0.reader.ReadByte() << 16) | ((int)A_0.reader.ReadByte() << 8) | (int)A_0.reader.ReadByte());
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x00052E14 File Offset: 0x00051014
		[CompilerGenerated]
		internal static int <ImportCallSite>g__ReadCompressedInt32|50_2(ref ReflectionHelper.<>c__DisplayClass50_1 A_0)
		{
			byte b = A_0.reader.ReadByte();
			A_0.reader.BaseStream.Seek(-1L, SeekOrigin.Current);
			uint num = ReflectionHelper.<ImportCallSite>g__ReadCompressedUInt32|50_1(ref A_0);
			int num2 = (int)num >> 1;
			if ((num & 1U) == 0U)
			{
				return num2;
			}
			int num3 = (int)(b & 192);
			if (num3 == 0 || num3 == 64)
			{
				return num2 - 64;
			}
			if (num3 != 128)
			{
				return num2 - 268435456;
			}
			return num2 - 8192;
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x00052E80 File Offset: 0x00051080
		[CompilerGenerated]
		internal static TypeReference <ImportCallSite>g__GetTypeDefOrRef|50_3(ref ReflectionHelper.<>c__DisplayClass50_0 A_0, ref ReflectionHelper.<>c__DisplayClass50_1 A_1)
		{
			uint num = ReflectionHelper.<ImportCallSite>g__ReadCompressedUInt32|50_1(ref A_1);
			uint num2 = num >> 2;
			uint num3;
			switch (num & 3U)
			{
			case 0U:
				num3 = 33554432U | num2;
				break;
			case 1U:
				num3 = 16777216U | num2;
				break;
			case 2U:
				num3 = 452984832U | num2;
				break;
			default:
				num3 = 0U;
				break;
			}
			return A_0.moduleTo.ImportReference(A_0.moduleFrom.ResolveType((int)num3));
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x00052EE8 File Offset: 0x000510E8
		[CompilerGenerated]
		internal static TypeReference <ImportCallSite>g__ReadTypeSignature|50_4(ref ReflectionHelper.<>c__DisplayClass50_0 A_0, ref ReflectionHelper.<>c__DisplayClass50_1 A_1)
		{
			MetadataType metadataType = (MetadataType)A_1.reader.ReadByte();
			switch (metadataType)
			{
			case MetadataType.Void:
				return A_0.moduleTo.TypeSystem.Void;
			case MetadataType.Boolean:
				return A_0.moduleTo.TypeSystem.Boolean;
			case MetadataType.Char:
				return A_0.moduleTo.TypeSystem.Char;
			case MetadataType.SByte:
				return A_0.moduleTo.TypeSystem.SByte;
			case MetadataType.Byte:
				return A_0.moduleTo.TypeSystem.Byte;
			case MetadataType.Int16:
				return A_0.moduleTo.TypeSystem.Int16;
			case MetadataType.UInt16:
				return A_0.moduleTo.TypeSystem.UInt16;
			case MetadataType.Int32:
				return A_0.moduleTo.TypeSystem.Int32;
			case MetadataType.UInt32:
				return A_0.moduleTo.TypeSystem.UInt32;
			case MetadataType.Int64:
				return A_0.moduleTo.TypeSystem.Int64;
			case MetadataType.UInt64:
				return A_0.moduleTo.TypeSystem.UInt64;
			case MetadataType.Single:
				return A_0.moduleTo.TypeSystem.Single;
			case MetadataType.Double:
				return A_0.moduleTo.TypeSystem.Double;
			case MetadataType.String:
				return A_0.moduleTo.TypeSystem.String;
			case MetadataType.Pointer:
				return new PointerType(ReflectionHelper.<ImportCallSite>g__ReadTypeSignature|50_4(ref A_0, ref A_1));
			case MetadataType.ByReference:
				return new ByReferenceType(ReflectionHelper.<ImportCallSite>g__ReadTypeSignature|50_4(ref A_0, ref A_1));
			case MetadataType.ValueType:
			case MetadataType.Class:
				return ReflectionHelper.<ImportCallSite>g__GetTypeDefOrRef|50_3(ref A_0, ref A_1);
			case MetadataType.Var:
			case MetadataType.GenericInstance:
			case MetadataType.MVar:
				throw new NotSupportedException(string.Format("Unsupported generic callsite element: {0}", metadataType));
			case MetadataType.Array:
			{
				ArrayType arrayType = new ArrayType(ReflectionHelper.<ImportCallSite>g__ReadTypeSignature|50_4(ref A_0, ref A_1));
				uint num = ReflectionHelper.<ImportCallSite>g__ReadCompressedUInt32|50_1(ref A_1);
				uint[] array = new uint[ReflectionHelper.<ImportCallSite>g__ReadCompressedUInt32|50_1(ref A_1)];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = ReflectionHelper.<ImportCallSite>g__ReadCompressedUInt32|50_1(ref A_1);
				}
				int[] array2 = new int[ReflectionHelper.<ImportCallSite>g__ReadCompressedUInt32|50_1(ref A_1)];
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j] = ReflectionHelper.<ImportCallSite>g__ReadCompressedInt32|50_2(ref A_1);
				}
				arrayType.Dimensions.Clear();
				int num2 = 0;
				while ((long)num2 < (long)((ulong)num))
				{
					int? num3 = null;
					int? num4 = null;
					if (num2 < array2.Length)
					{
						num3 = new int?(array2[num2]);
					}
					if (num2 < array.Length)
					{
						int? num5 = num3;
						int num6 = (int)array[num2];
						num4 = ((num5 != null) ? new int?(num5.GetValueOrDefault() + num6 - 1) : null);
					}
					arrayType.Dimensions.Add(new ArrayDimension(num3, num4));
					num2++;
				}
				return arrayType;
			}
			case MetadataType.TypedByReference:
				return A_0.moduleTo.TypeSystem.TypedReference;
			case (MetadataType)23:
			case (MetadataType)26:
				break;
			case MetadataType.IntPtr:
				return A_0.moduleTo.TypeSystem.IntPtr;
			case MetadataType.UIntPtr:
				return A_0.moduleTo.TypeSystem.UIntPtr;
			case MetadataType.FunctionPointer:
			{
				FunctionPointerType functionPointerType = new FunctionPointerType();
				ReflectionHelper.<ImportCallSite>g__ReadMethodSignature|50_0(functionPointerType, ref A_0, ref A_1);
				return functionPointerType;
			}
			case MetadataType.Object:
				return A_0.moduleTo.TypeSystem.Object;
			case (MetadataType)29:
				return new ArrayType(ReflectionHelper.<ImportCallSite>g__ReadTypeSignature|50_4(ref A_0, ref A_1));
			case MetadataType.RequiredModifier:
				return new RequiredModifierType(ReflectionHelper.<ImportCallSite>g__GetTypeDefOrRef|50_3(ref A_0, ref A_1), ReflectionHelper.<ImportCallSite>g__ReadTypeSignature|50_4(ref A_0, ref A_1));
			case MetadataType.OptionalModifier:
				return new OptionalModifierType(ReflectionHelper.<ImportCallSite>g__GetTypeDefOrRef|50_3(ref A_0, ref A_1), ReflectionHelper.<ImportCallSite>g__ReadTypeSignature|50_4(ref A_0, ref A_1));
			default:
				if (metadataType == MetadataType.Sentinel)
				{
					return new SentinelType(ReflectionHelper.<ImportCallSite>g__ReadTypeSignature|50_4(ref A_0, ref A_1));
				}
				if (metadataType == MetadataType.Pinned)
				{
					return new PinnedType(ReflectionHelper.<ImportCallSite>g__ReadTypeSignature|50_4(ref A_0, ref A_1));
				}
				break;
			}
			throw new NotSupportedException(string.Format("Unsupported callsite element: {0}", metadataType));
		}

		// Token: 0x04001037 RID: 4151
		public static readonly bool IsMono = Type.GetType("Mono.Runtime") != null || Type.GetType("Mono.RuntimeStructs") != null;

		// Token: 0x04001038 RID: 4152
		public static readonly bool IsCore = typeof(object).Assembly.GetName().Name == "System.Private.CoreLib";

		// Token: 0x04001039 RID: 4153
		private static readonly object[] _NoArgs = new object[0];

		// Token: 0x0400103A RID: 4154
		internal static readonly Dictionary<string, WeakReference> AssemblyCache = new Dictionary<string, WeakReference>();

		// Token: 0x0400103B RID: 4155
		internal static readonly Dictionary<string, WeakReference[]> AssembliesCache = new Dictionary<string, WeakReference[]>();

		// Token: 0x0400103C RID: 4156
		internal static readonly Dictionary<string, WeakReference> ResolveReflectionCache = new Dictionary<string, WeakReference>();

		// Token: 0x0400103D RID: 4157
		public static readonly byte[] AssemblyHashPrefix = new UTF8Encoding(false).GetBytes("MonoModRefl").Concat<byte>(new byte[1]).ToArray<byte>();

		// Token: 0x0400103E RID: 4158
		public static readonly string AssemblyHashNameTag = "@#";

		// Token: 0x0400103F RID: 4159
		private const BindingFlags _BindingFlagsAll = (BindingFlags)(-1);

		// Token: 0x04001040 RID: 4160
		private static readonly object[] _CacheGetterArgs;

		// Token: 0x04001041 RID: 4161
		private static Type t_RuntimeType;

		// Token: 0x04001042 RID: 4162
		private static Type t_RuntimeTypeCache;

		// Token: 0x04001043 RID: 4163
		private static PropertyInfo p_RuntimeType_Cache;

		// Token: 0x04001044 RID: 4164
		private static MethodInfo m_RuntimeTypeCache_GetFieldList;

		// Token: 0x04001045 RID: 4165
		private static MethodInfo m_RuntimeTypeCache_GetPropertyList;

		// Token: 0x04001046 RID: 4166
		private static Dictionary<Type, FieldInfo> fmap_CerArrayList_array;

		// Token: 0x04001047 RID: 4167
		private static readonly ConditionalWeakTable<Type, ReflectionHelper.CacheFixEntry> _CacheFixed;

		// Token: 0x04001048 RID: 4168
		private static Type t_RuntimeModule;

		// Token: 0x04001049 RID: 4169
		private static PropertyInfo p_RuntimeModule_RuntimeType;

		// Token: 0x0400104A RID: 4170
		private static FieldInfo f_RuntimeModule__impl;

		// Token: 0x0400104B RID: 4171
		private static MethodInfo m_RuntimeModule_GetGlobalType;

		// Token: 0x0400104C RID: 4172
		private static readonly FieldInfo f_SignatureHelper_module;

		// Token: 0x02000451 RID: 1105
		private class CacheFixEntry
		{
			// Token: 0x0400104D RID: 4173
			public object Cache;

			// Token: 0x0400104E RID: 4174
			public Array Properties;

			// Token: 0x0400104F RID: 4175
			public Array Fields;

			// Token: 0x04001050 RID: 4176
			public bool NeedsVerify;
		}
	}
}
