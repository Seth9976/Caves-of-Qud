using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Utils;

namespace MonoMod.RuntimeDetour.Platforms
{
	// Token: 0x0200048A RID: 1162
	public class DetourRuntimeNETCore30Platform : DetourRuntimeNETCorePlatform
	{
		// Token: 0x06001901 RID: 6401 RVA: 0x000570A8 File Offset: 0x000552A8
		protected unsafe override void DisableInlining(MethodBase method, RuntimeMethodHandle handle)
		{
			ushort* ptr = (ushort*)((byte*)(void*)handle.Value + 6);
			ushort* ptr2 = ptr;
			*ptr2 |= 8192;
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x000570CF File Offset: 0x000552CF
		private IntPtr GetCompileMethod(IntPtr jit)
		{
			return DetourRuntimeNETCorePlatform.ReadObjectVTable(jit, this.VTableIndex_ICorJitCompiler_compileMethod);
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001903 RID: 6403 RVA: 0x00018941 File Offset: 0x00016B41
		public override bool OnMethodCompiledWillBeCalled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x000570DD File Offset: 0x000552DD
		protected unsafe virtual DetourRuntimeNETCore30Platform.CorJitResult InvokeRealCompileMethod(IntPtr thisPtr, IntPtr corJitInfo, in DetourRuntimeNETCore30Platform.CORINFO_METHOD_INFO methodInfo, uint flags, out byte* nativeEntry, out uint nativeSizeOfCode)
		{
			nativeEntry = (IntPtr)((UIntPtr)0);
			nativeSizeOfCode = 0U;
			if (this.real_compileMethod == null)
			{
				return DetourRuntimeNETCore30Platform.CorJitResult.CORJIT_OK;
			}
			return this.real_compileMethod(thisPtr, corJitInfo, in methodInfo, flags, out nativeEntry, out nativeSizeOfCode);
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x00057108 File Offset: 0x00055308
		protected unsafe virtual IntPtr GetCompileMethodHook(IntPtr real)
		{
			this.real_compileMethod = real.AsDelegate<DetourRuntimeNETCore30Platform.d_compileMethod>();
			this.our_compileMethod = new DetourRuntimeNETCore30Platform.d_compileMethod(this.CompileMethodHook);
			IntPtr functionPointerForDelegate = Marshal.GetFunctionPointerForDelegate<DetourRuntimeNETCore30Platform.d_compileMethod>(this.our_compileMethod);
			NativeDetourData nativeDetourData = DetourRuntimeNETCore30Platform.CreateNativeTrampolineTo(functionPointerForDelegate);
			DetourRuntimeNETCore30Platform.d_compileMethod d_compileMethod = nativeDetourData.Method.AsDelegate<DetourRuntimeNETCore30Platform.d_compileMethod>();
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			DetourRuntimeNETCore30Platform.CORINFO_METHOD_INFO corinfo_METHOD_INFO = default(DetourRuntimeNETCore30Platform.CORINFO_METHOD_INFO);
			byte* ptr;
			uint num;
			d_compileMethod(zero, zero2, in corinfo_METHOD_INFO, 0U, out ptr, out num);
			DetourRuntimeNETCore30Platform.FreeNativeTrampoline(nativeDetourData);
			return functionPointerForDelegate;
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x00057174 File Offset: 0x00055374
		protected unsafe override void InstallJitHooks(IntPtr jit)
		{
			this.SetupJitHookHelpers();
			IntPtr zero = IntPtr.Zero;
			IntPtr zero2 = IntPtr.Zero;
			DetourRuntimeNETCore30Platform.CORINFO_METHOD_INFO corinfo_METHOD_INFO = default(DetourRuntimeNETCore30Platform.CORINFO_METHOD_INFO);
			byte* ptr;
			uint num;
			this.InvokeRealCompileMethod(zero, zero2, in corinfo_METHOD_INFO, 0U, out ptr, out num);
			IntPtr compileMethodHook = this.GetCompileMethodHook(this.GetCompileMethod(jit));
			int num2 = DetourRuntimeNETCore30Platform.hookEntrancy;
			IntPtr* vtableEntry = DetourRuntimeNETCorePlatform.GetVTableEntry(jit, this.VTableIndex_ICorJitCompiler_compileMethod);
			DetourHelper.Native.MakeWritable((IntPtr)((void*)vtableEntry), (uint)IntPtr.Size);
			this.real_compileMethodPtr = *vtableEntry;
			*vtableEntry = compileMethodHook;
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x000571E8 File Offset: 0x000553E8
		protected static NativeDetourData CreateNativeTrampolineTo(IntPtr target)
		{
			IntPtr intPtr = DetourHelper.Native.MemAlloc(64U);
			NativeDetourData nativeDetourData = DetourHelper.Native.Create(intPtr, target, null);
			DetourHelper.Native.MakeWritable(nativeDetourData);
			DetourHelper.Native.Apply(nativeDetourData);
			DetourHelper.Native.MakeExecutable(nativeDetourData);
			DetourHelper.Native.FlushICache(nativeDetourData);
			return nativeDetourData;
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x00057245 File Offset: 0x00055445
		protected static void FreeNativeTrampoline(NativeDetourData data)
		{
			DetourHelper.Native.MakeWritable(data);
			DetourHelper.Native.MemFree(data.Method);
			DetourHelper.Native.Free(data);
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x00057270 File Offset: 0x00055470
		protected unsafe DetourRuntimeNETCore30Platform.CorJitResult CompileMethodHook(IntPtr jit, IntPtr corJitInfo, in DetourRuntimeNETCore30Platform.CORINFO_METHOD_INFO methodInfo, uint flags, out byte* nativeEntry, out uint nativeSizeOfCode)
		{
			nativeEntry = (IntPtr)((UIntPtr)0);
			nativeSizeOfCode = 0U;
			if (jit == IntPtr.Zero)
			{
				return DetourRuntimeNETCore30Platform.CorJitResult.CORJIT_OK;
			}
			DetourRuntimeNETCore30Platform.hookEntrancy++;
			DetourRuntimeNETCore30Platform.CorJitResult corJitResult2;
			try
			{
				DetourRuntimeNETCore30Platform.CorJitResult corJitResult = this.InvokeRealCompileMethod(jit, corJitInfo, in methodInfo, flags, out nativeEntry, out nativeSizeOfCode);
				if (DetourRuntimeNETCore30Platform.hookEntrancy == 1)
				{
					try
					{
						RuntimeTypeHandle[] array = null;
						RuntimeTypeHandle[] array2 = null;
						if (methodInfo.args.sigInst.classInst != null)
						{
							array = new RuntimeTypeHandle[methodInfo.args.sigInst.classInstCount];
							for (int i = 0; i < array.Length; i++)
							{
								array[i] = this.GetTypeFromNativeHandle(methodInfo.args.sigInst.classInst[(IntPtr)i * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)]).TypeHandle;
							}
						}
						if (methodInfo.args.sigInst.methInst != null)
						{
							array2 = new RuntimeTypeHandle[methodInfo.args.sigInst.methInstCount];
							for (int j = 0; j < array2.Length; j++)
							{
								array2[j] = this.GetTypeFromNativeHandle(methodInfo.args.sigInst.methInst[(IntPtr)j * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)]).TypeHandle;
							}
						}
						RuntimeTypeHandle typeHandle = this.GetDeclaringTypeOfMethodHandle(methodInfo.ftn).TypeHandle;
						RuntimeMethodHandle runtimeMethodHandle = this.CreateHandleForHandlePointer(methodInfo.ftn);
						this.JitHookCore(typeHandle, runtimeMethodHandle, (IntPtr)nativeEntry, (ulong)nativeSizeOfCode, array, array2);
					}
					catch
					{
					}
				}
				corJitResult2 = corJitResult;
			}
			finally
			{
				DetourRuntimeNETCore30Platform.hookEntrancy--;
			}
			return corJitResult2;
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x0005742C File Offset: 0x0005562C
		protected RuntimeMethodHandle CreateHandleForHandlePointer(IntPtr handle)
		{
			return this.CreateRuntimeMethodHandle(this.CreateRuntimeMethodInfoStub(handle, this.MethodHandle_GetLoaderAllocator(handle)));
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x00057454 File Offset: 0x00055654
		protected virtual void SetupJitHookHelpers()
		{
			MethodInfo methodInfo = typeof(object).Assembly.GetType("Internal.Runtime.CompilerServices.Unsafe").GetMethods().First<MethodInfo>((MethodInfo m) => m.Name == "As" && m.ReturnType.IsByRef);
			MethodInfo method = typeof(RuntimeMethodHandle).GetMethod("GetLoaderAllocator", BindingFlags.Static | BindingFlags.NonPublic);
			MethodInfo methodInfo2;
			using (DynamicMethodDefinition dynamicMethodDefinition = new DynamicMethodDefinition("MethodHandle_GetLoaderAllocator", typeof(object), new Type[] { typeof(IntPtr) }))
			{
				ILProcessor ilprocessor = dynamicMethodDefinition.GetILProcessor();
				ModuleDefinition module = ilprocessor.Body.Method.Module;
				Type parameterType = method.GetParameters().First<ParameterInfo>().ParameterType;
				ilprocessor.Emit(OpCodes.Ldarga_S, ilprocessor.Body.Method.Parameters[0]);
				ilprocessor.Emit(OpCodes.Call, module.ImportReference(methodInfo.MakeGenericMethod(new Type[]
				{
					typeof(IntPtr),
					parameterType
				})));
				ilprocessor.Emit(OpCodes.Ldobj, module.ImportReference(parameterType));
				ilprocessor.Emit(OpCodes.Call, module.ImportReference(method));
				ilprocessor.Emit(OpCodes.Ret);
				methodInfo2 = dynamicMethodDefinition.Generate();
			}
			this.MethodHandle_GetLoaderAllocator = methodInfo2.CreateDelegate<DetourRuntimeNETCore30Platform.d_MethodHandle_GetLoaderAllocator>();
			MethodInfo orCreateGetTypeFromHandleUnsafe = this.GetOrCreateGetTypeFromHandleUnsafe();
			this.GetTypeFromNativeHandle = orCreateGetTypeFromHandleUnsafe.CreateDelegate<DetourRuntimeNETCore30Platform.d_GetTypeFromNativeHandle>();
			Type type = typeof(RuntimeMethodHandle).Assembly.GetType("System.RuntimeMethodHandleInternal");
			MethodInfo method2 = typeof(RuntimeMethodHandle).GetMethod("GetDeclaringType", BindingFlags.Static | BindingFlags.NonPublic, null, new Type[] { type }, null);
			MethodInfo methodInfo3;
			using (DynamicMethodDefinition dynamicMethodDefinition2 = new DynamicMethodDefinition("GetDeclaringTypeOfMethodHandle", typeof(Type), new Type[] { typeof(IntPtr) }))
			{
				ILProcessor ilprocessor2 = dynamicMethodDefinition2.GetILProcessor();
				ModuleDefinition module2 = ilprocessor2.Body.Method.Module;
				ilprocessor2.Emit(OpCodes.Ldarga_S, ilprocessor2.Body.Method.Parameters[0]);
				ilprocessor2.Emit(OpCodes.Call, module2.ImportReference(methodInfo.MakeGenericMethod(new Type[]
				{
					typeof(IntPtr),
					type
				})));
				ilprocessor2.Emit(OpCodes.Ldobj, module2.ImportReference(type));
				ilprocessor2.Emit(OpCodes.Call, module2.ImportReference(method2));
				ilprocessor2.Emit(OpCodes.Ret);
				methodInfo3 = dynamicMethodDefinition2.Generate();
			}
			this.GetDeclaringTypeOfMethodHandle = methodInfo3.CreateDelegate<DetourRuntimeNETCore30Platform.d_GetDeclaringTypeOfMethodHandle>();
			Type[] array = new Type[]
			{
				typeof(IntPtr),
				typeof(object)
			};
			Type type2 = typeof(RuntimeMethodHandle).Assembly.GetType("System.RuntimeMethodInfoStub");
			ConstructorInfo constructor = type2.GetConstructor(array);
			MethodInfo methodInfo4;
			using (DynamicMethodDefinition dynamicMethodDefinition3 = new DynamicMethodDefinition("new RuntimeMethodInfoStub", type2, array))
			{
				ILProcessor ilprocessor3 = dynamicMethodDefinition3.GetILProcessor();
				ModuleDefinition module3 = ilprocessor3.Body.Method.Module;
				ilprocessor3.Emit(OpCodes.Ldarg_0);
				ilprocessor3.Emit(OpCodes.Ldarg_1);
				ilprocessor3.Emit(OpCodes.Newobj, module3.ImportReference(constructor));
				ilprocessor3.Emit(OpCodes.Ret);
				methodInfo4 = dynamicMethodDefinition3.Generate();
			}
			this.CreateRuntimeMethodInfoStub = methodInfo4.CreateDelegate<DetourRuntimeNETCore30Platform.d_CreateRuntimeMethodInfoStub>();
			ConstructorInfo constructorInfo = typeof(RuntimeMethodHandle).GetConstructors(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).First<ConstructorInfo>();
			MethodInfo methodInfo5;
			using (DynamicMethodDefinition dynamicMethodDefinition4 = new DynamicMethodDefinition("new RuntimeMethodHandle", typeof(RuntimeMethodHandle), new Type[] { typeof(object) }))
			{
				ILProcessor ilprocessor4 = dynamicMethodDefinition4.GetILProcessor();
				ModuleDefinition module4 = ilprocessor4.Body.Method.Module;
				ilprocessor4.Emit(OpCodes.Ldarg_0);
				ilprocessor4.Emit(OpCodes.Newobj, module4.ImportReference(constructorInfo));
				ilprocessor4.Emit(OpCodes.Ret);
				methodInfo5 = dynamicMethodDefinition4.Generate();
			}
			this.CreateRuntimeMethodHandle = methodInfo5.CreateDelegate<DetourRuntimeNETCore30Platform.d_CreateRuntimeMethodHandle>();
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x000578A4 File Offset: 0x00055AA4
		private MethodInfo GetOrCreateGetTypeFromHandleUnsafe()
		{
			if (this._getTypeFromHandleUnsafeMethod != null)
			{
				return this._getTypeFromHandleUnsafeMethod;
			}
			Assembly assembly;
			using (ModuleDefinition moduleDefinition = ModuleDefinition.CreateModule("MonoMod.RuntimeDetour.Runtime.NETCore3+Helpers", new ModuleParameters
			{
				Kind = ModuleKind.Dll
			}))
			{
				TypeDefinition typeDefinition = new TypeDefinition("System", "Type", Mono.Cecil.TypeAttributes.Public | Mono.Cecil.TypeAttributes.Abstract)
				{
					BaseType = moduleDefinition.TypeSystem.Object
				};
				moduleDefinition.Types.Add(typeDefinition);
				MethodDefinition methodDefinition = new MethodDefinition("GetTypeFromHandleUnsafe", Mono.Cecil.MethodAttributes.FamANDAssem | Mono.Cecil.MethodAttributes.Family | Mono.Cecil.MethodAttributes.Static, moduleDefinition.ImportReference(typeof(Type)))
				{
					IsInternalCall = true
				};
				methodDefinition.Parameters.Add(new ParameterDefinition(moduleDefinition.ImportReference(typeof(IntPtr))));
				typeDefinition.Methods.Add(methodDefinition);
				assembly = ReflectionHelper.Load(moduleDefinition);
			}
			this.MakeAssemblySystemAssembly(assembly);
			return this._getTypeFromHandleUnsafeMethod = assembly.GetType("System.Type").GetMethod("GetTypeFromHandleUnsafe");
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x000579AC File Offset: 0x00055BAC
		protected unsafe virtual void MakeAssemblySystemAssembly(Assembly assembly)
		{
			IntPtr intPtr = (IntPtr)DetourRuntimeNETCore30Platform._runtimeAssemblyPtrField.GetValue(assembly);
			int num = IntPtr.Size + IntPtr.Size + IntPtr.Size + IntPtr.Size + IntPtr.Size + 4 + IntPtr.Size + IntPtr.Size + 4 + 4 + IntPtr.Size + IntPtr.Size + 4 + 4 + IntPtr.Size;
			if (IntPtr.Size == 8)
			{
				num += 4;
			}
			IntPtr intPtr2 = *(IntPtr*)((byte*)(void*)intPtr + num);
			int num2 = IntPtr.Size + IntPtr.Size + IntPtr.Size + IntPtr.Size;
			IntPtr intPtr3 = *(IntPtr*)((byte*)(void*)intPtr2 + num2);
			int num3 = IntPtr.Size + IntPtr.Size + IntPtr.Size + 4 + 4 + IntPtr.Size + IntPtr.Size + IntPtr.Size + IntPtr.Size + 4;
			int* ptr = (int*)((byte*)(void*)intPtr3 + num3);
			*ptr |= 1;
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x00057A81 File Offset: 0x00055C81
		protected void HookPermanent(MethodBase from, MethodBase to)
		{
			this.Pin(from);
			this.Pin(to);
			this.HookPermanent(this.GetNativeStart(from), this.GetNativeStart(to));
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x00057AA8 File Offset: 0x00055CA8
		protected void HookPermanent(IntPtr from, IntPtr to)
		{
			NativeDetourData nativeDetourData = DetourHelper.Native.Create(from, to, null);
			DetourHelper.Native.MakeWritable(nativeDetourData);
			DetourHelper.Native.Apply(nativeDetourData);
			DetourHelper.Native.MakeExecutable(nativeDetourData);
			DetourHelper.Native.FlushICache(nativeDetourData);
			DetourHelper.Native.Free(nativeDetourData);
		}

		// Token: 0x04001276 RID: 4726
		public static readonly Guid JitVersionGuid = new Guid("d609bed1-7831-49fc-bd49-b6f054dd4d46");

		// Token: 0x04001277 RID: 4727
		private DetourRuntimeNETCore30Platform.d_compileMethod our_compileMethod;

		// Token: 0x04001278 RID: 4728
		private IntPtr real_compileMethodPtr;

		// Token: 0x04001279 RID: 4729
		private DetourRuntimeNETCore30Platform.d_compileMethod real_compileMethod;

		// Token: 0x0400127A RID: 4730
		[ThreadStatic]
		private static int hookEntrancy = 0;

		// Token: 0x0400127B RID: 4731
		protected DetourRuntimeNETCore30Platform.d_MethodHandle_GetLoaderAllocator MethodHandle_GetLoaderAllocator;

		// Token: 0x0400127C RID: 4732
		protected DetourRuntimeNETCore30Platform.d_CreateRuntimeMethodInfoStub CreateRuntimeMethodInfoStub;

		// Token: 0x0400127D RID: 4733
		protected DetourRuntimeNETCore30Platform.d_CreateRuntimeMethodHandle CreateRuntimeMethodHandle;

		// Token: 0x0400127E RID: 4734
		protected DetourRuntimeNETCore30Platform.d_GetDeclaringTypeOfMethodHandle GetDeclaringTypeOfMethodHandle;

		// Token: 0x0400127F RID: 4735
		protected DetourRuntimeNETCore30Platform.d_GetTypeFromNativeHandle GetTypeFromNativeHandle;

		// Token: 0x04001280 RID: 4736
		private MethodInfo _getTypeFromHandleUnsafeMethod;

		// Token: 0x04001281 RID: 4737
		private static FieldInfo _runtimeAssemblyPtrField = Type.GetType("System.Reflection.RuntimeAssembly").GetField("m_assembly", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x0200048B RID: 1163
		protected enum CorJitResult
		{
			// Token: 0x04001283 RID: 4739
			CORJIT_OK
		}

		// Token: 0x0200048C RID: 1164
		protected struct CORINFO_SIG_INST
		{
			// Token: 0x04001284 RID: 4740
			public uint classInstCount;

			// Token: 0x04001285 RID: 4741
			public unsafe IntPtr* classInst;

			// Token: 0x04001286 RID: 4742
			public uint methInstCount;

			// Token: 0x04001287 RID: 4743
			public unsafe IntPtr* methInst;
		}

		// Token: 0x0200048D RID: 1165
		protected struct CORINFO_SIG_INFO
		{
			// Token: 0x04001288 RID: 4744
			public int callConv;

			// Token: 0x04001289 RID: 4745
			public IntPtr retTypeClass;

			// Token: 0x0400128A RID: 4746
			public IntPtr retTypeSigClass;

			// Token: 0x0400128B RID: 4747
			public byte retType;

			// Token: 0x0400128C RID: 4748
			public byte flags;

			// Token: 0x0400128D RID: 4749
			public ushort numArgs;

			// Token: 0x0400128E RID: 4750
			public DetourRuntimeNETCore30Platform.CORINFO_SIG_INST sigInst;

			// Token: 0x0400128F RID: 4751
			public IntPtr args;

			// Token: 0x04001290 RID: 4752
			public IntPtr pSig;

			// Token: 0x04001291 RID: 4753
			public uint sbSig;

			// Token: 0x04001292 RID: 4754
			public IntPtr scope;

			// Token: 0x04001293 RID: 4755
			public uint token;
		}

		// Token: 0x0200048E RID: 1166
		protected struct CORINFO_METHOD_INFO
		{
			// Token: 0x04001294 RID: 4756
			public IntPtr ftn;

			// Token: 0x04001295 RID: 4757
			public IntPtr scope;

			// Token: 0x04001296 RID: 4758
			public unsafe byte* ILCode;

			// Token: 0x04001297 RID: 4759
			public uint ILCodeSize;

			// Token: 0x04001298 RID: 4760
			public uint maxStack;

			// Token: 0x04001299 RID: 4761
			public uint EHcount;

			// Token: 0x0400129A RID: 4762
			public int options;

			// Token: 0x0400129B RID: 4763
			public int regionKind;

			// Token: 0x0400129C RID: 4764
			public DetourRuntimeNETCore30Platform.CORINFO_SIG_INFO args;

			// Token: 0x0400129D RID: 4765
			public DetourRuntimeNETCore30Platform.CORINFO_SIG_INFO locals;
		}

		// Token: 0x0200048F RID: 1167
		// (Invoke) Token: 0x06001913 RID: 6419
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private unsafe delegate DetourRuntimeNETCore30Platform.CorJitResult d_compileMethod(IntPtr thisPtr, IntPtr corJitInfo, in DetourRuntimeNETCore30Platform.CORINFO_METHOD_INFO methodInfo, uint flags, out byte* nativeEntry, out uint nativeSizeOfCode);

		// Token: 0x02000490 RID: 1168
		// (Invoke) Token: 0x06001917 RID: 6423
		protected delegate object d_MethodHandle_GetLoaderAllocator(IntPtr methodHandle);

		// Token: 0x02000491 RID: 1169
		// (Invoke) Token: 0x0600191B RID: 6427
		protected delegate object d_CreateRuntimeMethodInfoStub(IntPtr methodHandle, object loaderAllocator);

		// Token: 0x02000492 RID: 1170
		// (Invoke) Token: 0x0600191F RID: 6431
		protected delegate RuntimeMethodHandle d_CreateRuntimeMethodHandle(object runtimeMethodInfo);

		// Token: 0x02000493 RID: 1171
		// (Invoke) Token: 0x06001923 RID: 6435
		protected delegate Type d_GetDeclaringTypeOfMethodHandle(IntPtr methodHandle);

		// Token: 0x02000494 RID: 1172
		// (Invoke) Token: 0x06001927 RID: 6439
		protected delegate Type d_GetTypeFromNativeHandle(IntPtr handle);
	}
}
