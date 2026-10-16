using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using MonoMod.Utils;

namespace MonoMod.RuntimeDetour.Platforms
{
	// Token: 0x02000496 RID: 1174
	public class DetourRuntimeNETCorePlatform : DetourRuntimeNETPlatform
	{
		// Token: 0x0600192D RID: 6445 RVA: 0x00057B69 File Offset: 0x00055D69
		public DetourRuntimeNETCorePlatform()
		{
			this.GlueThiscallInStructRetPtr = this.GlueThiscallStructRetPtr;
		}

		// Token: 0x0600192E RID: 6446 RVA: 0x00057B80 File Offset: 0x00055D80
		protected static IntPtr GetJitObject()
		{
			if (DetourRuntimeNETCorePlatform.getJit == null)
			{
				ProcessModule processModule = Process.GetCurrentProcess().Modules.Cast<ProcessModule>().FirstOrDefault<ProcessModule>((ProcessModule m) => Path.GetFileNameWithoutExtension(m.FileName).EndsWith("clrjit", StringComparison.Ordinal));
				if (processModule == null)
				{
					throw new PlatformNotSupportedException();
				}
				IntPtr intPtr;
				if (!DynDll.TryOpenLibrary(processModule.FileName, out intPtr, false, null))
				{
					throw new PlatformNotSupportedException();
				}
				if (PlatformHelper.Is(Platform.Windows))
				{
					DetourRuntimeNETCorePlatform.isNet5Jit = processModule.FileVersionInfo.ProductMajorPart >= 5;
				}
				else
				{
					DetourRuntimeNETCorePlatform.isNet5Jit = typeof(object).Assembly.GetName().Version.Major >= 5;
				}
				try
				{
					DetourRuntimeNETCorePlatform.getJit = intPtr.GetFunction("getJit").AsDelegate<DetourRuntimeNETCorePlatform.d_getJit>();
				}
				catch
				{
					DynDll.CloseLibrary(intPtr);
					throw;
				}
			}
			return DetourRuntimeNETCorePlatform.getJit();
		}

		// Token: 0x0600192F RID: 6447 RVA: 0x00057C7C File Offset: 0x00055E7C
		protected static Guid GetJitGuid(IntPtr jit)
		{
			int num = (DetourRuntimeNETCorePlatform.isNet5Jit ? 2 : 4);
			Guid guid;
			DetourRuntimeNETCorePlatform.ReadObjectVTable(jit, num).AsDelegate<DetourRuntimeNETCorePlatform.d_getVersionIdentifier>()(jit, out guid);
			return guid;
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001930 RID: 6448 RVA: 0x00018318 File Offset: 0x00016518
		protected virtual int VTableIndex_ICorJitCompiler_compileMethod
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06001931 RID: 6449 RVA: 0x00057CAA File Offset: 0x00055EAA
		protected unsafe static IntPtr* GetVTableEntry(IntPtr @object, int index)
		{
			return *(IntPtr*)(void*)@object / (IntPtr)sizeof(IntPtr) + index * sizeof(IntPtr);
		}

		// Token: 0x06001932 RID: 6450 RVA: 0x00057CBD File Offset: 0x00055EBD
		protected unsafe static IntPtr ReadObjectVTable(IntPtr @object, int index)
		{
			return *DetourRuntimeNETCorePlatform.GetVTableEntry(@object, index);
		}

		// Token: 0x06001933 RID: 6451 RVA: 0x00018659 File Offset: 0x00016859
		protected override void DisableInlining(MethodBase method, RuntimeMethodHandle handle)
		{
		}

		// Token: 0x06001934 RID: 6452 RVA: 0x00057CC7 File Offset: 0x00055EC7
		protected virtual void InstallJitHooks(IntPtr jitObject)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001935 RID: 6453 RVA: 0x00018318 File Offset: 0x00016518
		public override bool OnMethodCompiledWillBeCalled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06001936 RID: 6454 RVA: 0x00057CD0 File Offset: 0x00055ED0
		// (remove) Token: 0x06001937 RID: 6455 RVA: 0x00057D08 File Offset: 0x00055F08
		public override event OnMethodCompiledEvent OnMethodCompiled;

		// Token: 0x06001938 RID: 6456 RVA: 0x00057D40 File Offset: 0x00055F40
		protected virtual void JitHookCore(RuntimeTypeHandle declaringType, RuntimeMethodHandle methodHandle, IntPtr methodBodyStart, ulong methodBodySize, RuntimeTypeHandle[] genericClassArguments, RuntimeTypeHandle[] genericMethodArguments)
		{
			try
			{
				Type type = Type.GetTypeFromHandle(declaringType);
				if (genericClassArguments != null && type.IsGenericTypeDefinition)
				{
					type = type.MakeGenericType(genericClassArguments.Select<RuntimeTypeHandle, Type>(new Func<RuntimeTypeHandle, Type>(Type.GetTypeFromHandle)).ToArray<Type>());
				}
				MethodBase methodBase = MethodBase.GetMethodFromHandle(methodHandle, type.TypeHandle);
				if (methodBase == null)
				{
					methodBase = this.GetPin(methodHandle).Method;
				}
				try
				{
					OnMethodCompiledEvent onMethodCompiled = this.OnMethodCompiled;
					if (onMethodCompiled != null)
					{
						onMethodCompiled(methodBase, methodBodyStart, methodBodySize);
					}
				}
				catch (Exception ex)
				{
					MMDbgLog.Log(string.Format("Error executing OnMethodCompiled event: {0}", ex));
				}
			}
			catch (Exception ex2)
			{
				MMDbgLog.Log(string.Format("Error in JitHookCore: {0}", ex2));
			}
		}

		// Token: 0x06001939 RID: 6457 RVA: 0x00057E00 File Offset: 0x00056000
		public static DetourRuntimeNETCorePlatform Create()
		{
			try
			{
				IntPtr jitObject = DetourRuntimeNETCorePlatform.GetJitObject();
				Guid jitGuid = DetourRuntimeNETCorePlatform.GetJitGuid(jitObject);
				DetourRuntimeNETCorePlatform detourRuntimeNETCorePlatform = null;
				if (jitGuid == DetourRuntimeNET60Platform.JitVersionGuid)
				{
					detourRuntimeNETCorePlatform = new DetourRuntimeNET60Platform();
				}
				else if (jitGuid == DetourRuntimeNET50Platform.JitVersionGuid)
				{
					detourRuntimeNETCorePlatform = new DetourRuntimeNET50Platform();
				}
				else if (jitGuid == DetourRuntimeNETCore30Platform.JitVersionGuid)
				{
					detourRuntimeNETCorePlatform = new DetourRuntimeNETCore30Platform();
				}
				if (detourRuntimeNETCorePlatform == null)
				{
					return new DetourRuntimeNETCorePlatform();
				}
				if (detourRuntimeNETCorePlatform != null)
				{
					detourRuntimeNETCorePlatform.InstallJitHooks(jitObject);
				}
				return detourRuntimeNETCorePlatform;
			}
			catch (Exception ex)
			{
				MMDbgLog.Log("Could not get JIT information for the runtime, falling out to the version without JIT hooks");
				MMDbgLog.Log(string.Format("Error: {0}", ex));
			}
			return new DetourRuntimeNETCorePlatform();
		}

		// Token: 0x040012A0 RID: 4768
		private static DetourRuntimeNETCorePlatform.d_getJit getJit;

		// Token: 0x040012A1 RID: 4769
		private static bool isNet5Jit;

		// Token: 0x040012A2 RID: 4770
		private const int vtableIndex_ICorJitCompiler_getVersionIdentifier = 4;

		// Token: 0x040012A3 RID: 4771
		private const int vtableIndex_ICorJitCompiler_getVersionIdentifier_net5 = 2;

		// Token: 0x02000497 RID: 1175
		// (Invoke) Token: 0x0600193B RID: 6459
		[UnmanagedFunctionPointer(CallingConvention.StdCall)]
		private delegate IntPtr d_getJit();

		// Token: 0x02000498 RID: 1176
		// (Invoke) Token: 0x0600193F RID: 6463
		[UnmanagedFunctionPointer(CallingConvention.ThisCall)]
		private delegate void d_getVersionIdentifier(IntPtr thisPtr, out Guid versionIdentifier);
	}
}
