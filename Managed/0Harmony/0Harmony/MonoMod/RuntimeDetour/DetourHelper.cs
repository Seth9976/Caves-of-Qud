using System;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.RuntimeDetour.Platforms;
using MonoMod.Utils;

namespace MonoMod.RuntimeDetour
{
	// Token: 0x02000467 RID: 1127
	internal static class DetourHelper
	{
		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x00054D0C File Offset: 0x00052F0C
		// (set) Token: 0x06001838 RID: 6200 RVA: 0x00054DAC File Offset: 0x00052FAC
		public static IDetourRuntimePlatform Runtime
		{
			get
			{
				if (DetourHelper._Runtime != null)
				{
					return DetourHelper._Runtime;
				}
				object runtimeLock = DetourHelper._RuntimeLock;
				IDetourRuntimePlatform detourRuntimePlatform;
				lock (runtimeLock)
				{
					if (DetourHelper._Runtime != null)
					{
						detourRuntimePlatform = DetourHelper._Runtime;
					}
					else if (DetourHelper._RuntimeInit)
					{
						detourRuntimePlatform = null;
					}
					else
					{
						DetourHelper._RuntimeInit = true;
						if (ReflectionHelper.IsMono)
						{
							DetourHelper._Runtime = new DetourRuntimeMonoPlatform();
						}
						else if (ReflectionHelper.IsCore)
						{
							DetourHelper._Runtime = DetourRuntimeNETCorePlatform.Create();
						}
						else
						{
							DetourHelper._Runtime = new DetourRuntimeNETPlatform();
						}
						detourRuntimePlatform = DetourHelper._Runtime;
					}
				}
				return detourRuntimePlatform;
			}
			set
			{
				DetourHelper._Runtime = value;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x00054DB4 File Offset: 0x00052FB4
		// (set) Token: 0x0600183A RID: 6202 RVA: 0x00054F00 File Offset: 0x00053100
		public static IDetourNativePlatform Native
		{
			get
			{
				if (DetourHelper._Native != null)
				{
					return DetourHelper._Native;
				}
				object nativeLock = DetourHelper._NativeLock;
				IDetourNativePlatform detourNativePlatform;
				lock (nativeLock)
				{
					if (DetourHelper._Native != null)
					{
						detourNativePlatform = DetourHelper._Native;
					}
					else if (DetourHelper._NativeInit)
					{
						detourNativePlatform = null;
					}
					else
					{
						DetourHelper._NativeInit = true;
						IDetourNativePlatform detourNativePlatform2;
						if (PlatformHelper.Is(Platform.ARM))
						{
							detourNativePlatform2 = new DetourNativeARMPlatform();
						}
						else
						{
							detourNativePlatform2 = new DetourNativeX86Platform();
						}
						if (PlatformHelper.Is(Platform.Windows))
						{
							detourNativePlatform = (DetourHelper._Native = new DetourNativeWindowsPlatform(detourNativePlatform2));
						}
						else
						{
							if (ReflectionHelper.IsMono)
							{
								try
								{
									return DetourHelper._Native = new DetourNativeMonoPlatform(detourNativePlatform2, "libmonosgen-2.0." + PlatformHelper.LibrarySuffix);
								}
								catch
								{
								}
							}
							string environmentVariable = Environment.GetEnvironmentVariable("MONOMOD_RUNTIMEDETOUR_MONOPOSIXHELPER");
							if ((ReflectionHelper.IsMono && environmentVariable != "0") || environmentVariable == "1")
							{
								try
								{
									return DetourHelper._Native = new DetourNativeMonoPosixPlatform(detourNativePlatform2);
								}
								catch
								{
								}
							}
							try
							{
								return DetourHelper._Native = new DetourNativeLibcPlatform(detourNativePlatform2);
							}
							catch
							{
							}
							detourNativePlatform = detourNativePlatform2;
						}
					}
				}
				return detourNativePlatform;
			}
			set
			{
				DetourHelper._Native = value;
			}
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x00054F08 File Offset: 0x00053108
		public static void MakeWritable(this IDetourNativePlatform plat, NativeDetourData detour)
		{
			plat.MakeWritable(detour.Method, detour.Size);
		}

		// Token: 0x0600183C RID: 6204 RVA: 0x00054F1C File Offset: 0x0005311C
		public static void MakeExecutable(this IDetourNativePlatform plat, NativeDetourData detour)
		{
			plat.MakeExecutable(detour.Method, detour.Size);
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x00054F30 File Offset: 0x00053130
		public static void FlushICache(this IDetourNativePlatform plat, NativeDetourData detour)
		{
			plat.FlushICache(detour.Method, detour.Size);
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x00054F44 File Offset: 0x00053144
		public unsafe static void Write(this IntPtr to, ref int offs, byte value)
		{
			*(UIntPtr)((long)to + (long)offs) = value;
			offs++;
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x00054F59 File Offset: 0x00053159
		public unsafe static void Write(this IntPtr to, ref int offs, ushort value)
		{
			*(UIntPtr)((long)to + (long)offs) = (short)value;
			offs += 2;
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x00054F6E File Offset: 0x0005316E
		public unsafe static void Write(this IntPtr to, ref int offs, uint value)
		{
			*(UIntPtr)((long)to + (long)offs) = (int)value;
			offs += 4;
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x00054F83 File Offset: 0x00053183
		public unsafe static void Write(this IntPtr to, ref int offs, ulong value)
		{
			*(UIntPtr)((long)to + (long)offs) = (long)value;
			offs += 8;
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x00054F98 File Offset: 0x00053198
		public static MethodBase GetIdentifiable(this MethodBase method)
		{
			return DetourHelper.Runtime.GetIdentifiable(method);
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x00054FA5 File Offset: 0x000531A5
		public static IntPtr GetNativeStart(this MethodBase method)
		{
			return DetourHelper.Runtime.GetNativeStart(method);
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x00054FB2 File Offset: 0x000531B2
		public static IntPtr GetNativeStart(this Delegate method)
		{
			return method.Method.GetNativeStart();
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x00054FBF File Offset: 0x000531BF
		public static IntPtr GetNativeStart(this Expression method)
		{
			return ((MethodCallExpression)method).Method.GetNativeStart();
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x00054FD1 File Offset: 0x000531D1
		public static MethodInfo CreateILCopy(this MethodBase method)
		{
			return DetourHelper.Runtime.CreateCopy(method);
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x00054FDE File Offset: 0x000531DE
		public static bool TryCreateILCopy(this MethodBase method, out MethodInfo dm)
		{
			return DetourHelper.Runtime.TryCreateCopy(method, out dm);
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x00054FEC File Offset: 0x000531EC
		public static T Pin<T>(this T method) where T : MethodBase
		{
			DetourHelper.Runtime.Pin(method);
			return method;
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x00054FFF File Offset: 0x000531FF
		public static T Unpin<T>(this T method) where T : MethodBase
		{
			DetourHelper.Runtime.Unpin(method);
			return method;
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x00055014 File Offset: 0x00053214
		public static MethodInfo GenerateNativeProxy(IntPtr target, MethodBase signature)
		{
			MethodInfo methodInfo = signature as MethodInfo;
			Type type = ((methodInfo != null) ? methodInfo.ReturnType : null) ?? typeof(void);
			ParameterInfo[] parameters = signature.GetParameters();
			Type[] array = new Type[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				array[i] = parameters[i].ParameterType;
			}
			MethodInfo methodInfo2;
			using (DynamicMethodDefinition dynamicMethodDefinition = new DynamicMethodDefinition("Native<" + ((long)target).ToString("X16", CultureInfo.InvariantCulture) + ">", type, array))
			{
				methodInfo2 = dynamicMethodDefinition.StubCriticalDetour().Generate().Pin<MethodInfo>();
			}
			NativeDetourData nativeDetourData = DetourHelper.Native.Create(methodInfo2.GetNativeStart(), target, null);
			DetourHelper.Native.MakeWritable(nativeDetourData);
			DetourHelper.Native.Apply(nativeDetourData);
			DetourHelper.Native.MakeExecutable(nativeDetourData);
			DetourHelper.Native.FlushICache(nativeDetourData);
			DetourHelper.Native.Free(nativeDetourData);
			return methodInfo2;
		}

		// Token: 0x0600184B RID: 6219 RVA: 0x0005512C File Offset: 0x0005332C
		private static NativeDetourData ToNativeDetourData(IntPtr method, IntPtr target, uint size, byte type, IntPtr extra)
		{
			return new NativeDetourData
			{
				Method = method,
				Target = target,
				Size = size,
				Type = type,
				Extra = extra
			};
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x0005516C File Offset: 0x0005336C
		public static DynamicMethodDefinition StubCriticalDetour(this DynamicMethodDefinition dm)
		{
			ILProcessor ilprocessor = dm.GetILProcessor();
			ModuleDefinition module = ilprocessor.Body.Method.Module;
			for (int i = 0; i < 32; i++)
			{
				ilprocessor.Emit(OpCodes.Nop);
			}
			ilprocessor.Emit(OpCodes.Ldstr, dm.Definition.Name + " should've been detoured!");
			ilprocessor.Emit(OpCodes.Newobj, module.ImportReference(DetourHelper._ctor_Exception));
			ilprocessor.Emit(OpCodes.Throw);
			return dm;
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x000551EC File Offset: 0x000533EC
		public static void EmitDetourCopy(this ILProcessor il, IntPtr src, IntPtr dst, byte type)
		{
			ModuleDefinition module = il.Body.Method.Module;
			il.Emit(OpCodes.Ldsfld, module.ImportReference(DetourHelper._f_Native));
			il.Emit(OpCodes.Ldc_I8, (long)src);
			il.Emit(OpCodes.Conv_I);
			il.Emit(OpCodes.Ldc_I8, (long)dst);
			il.Emit(OpCodes.Conv_I);
			il.Emit(OpCodes.Ldc_I4, (int)type);
			il.Emit(OpCodes.Conv_U1);
			il.Emit(OpCodes.Callvirt, module.ImportReference(DetourHelper._m_Copy));
		}

		// Token: 0x0600184E RID: 6222 RVA: 0x00055288 File Offset: 0x00053488
		public static void EmitDetourApply(this ILProcessor il, NativeDetourData data)
		{
			ModuleDefinition module = il.Body.Method.Module;
			il.Emit(OpCodes.Ldsfld, module.ImportReference(DetourHelper._f_Native));
			il.Emit(OpCodes.Ldc_I8, (long)data.Method);
			il.Emit(OpCodes.Conv_I);
			il.Emit(OpCodes.Ldc_I8, (long)data.Target);
			il.Emit(OpCodes.Conv_I);
			il.Emit(OpCodes.Ldc_I4, (int)data.Size);
			il.Emit(OpCodes.Ldc_I4, (int)data.Type);
			il.Emit(OpCodes.Conv_U1);
			il.Emit(OpCodes.Ldc_I8, (long)data.Extra);
			il.Emit(OpCodes.Conv_I);
			il.Emit(OpCodes.Call, module.ImportReference(DetourHelper._m_ToNativeDetourData));
			il.Emit(OpCodes.Callvirt, module.ImportReference(DetourHelper._m_Apply));
		}

		// Token: 0x0400109B RID: 4251
		private static readonly object _RuntimeLock = new object();

		// Token: 0x0400109C RID: 4252
		private static bool _RuntimeInit = false;

		// Token: 0x0400109D RID: 4253
		private static IDetourRuntimePlatform _Runtime;

		// Token: 0x0400109E RID: 4254
		private static readonly object _NativeLock = new object();

		// Token: 0x0400109F RID: 4255
		private static bool _NativeInit = false;

		// Token: 0x040010A0 RID: 4256
		private static IDetourNativePlatform _Native;

		// Token: 0x040010A1 RID: 4257
		private static readonly FieldInfo _f_Native = typeof(DetourHelper).GetField("_Native", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x040010A2 RID: 4258
		private static readonly MethodInfo _m_ToNativeDetourData = typeof(DetourHelper).GetMethod("ToNativeDetourData", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x040010A3 RID: 4259
		private static readonly MethodInfo _m_Copy = typeof(IDetourNativePlatform).GetMethod("Copy");

		// Token: 0x040010A4 RID: 4260
		private static readonly MethodInfo _m_Apply = typeof(IDetourNativePlatform).GetMethod("Apply");

		// Token: 0x040010A5 RID: 4261
		private static readonly ConstructorInfo _ctor_Exception = typeof(Exception).GetConstructor(new Type[] { typeof(string) });
	}
}
