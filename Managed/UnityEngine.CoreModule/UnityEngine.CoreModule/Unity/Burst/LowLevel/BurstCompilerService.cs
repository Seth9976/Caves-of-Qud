using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Bindings;

namespace Unity.Burst.LowLevel
{
	// Token: 0x020000C8 RID: 200
	[NativeHeader("Runtime/Burst/BurstDelegateCache.h")]
	[NativeHeader("Runtime/Burst/Burst.h")]
	[StaticAccessor("BurstCompilerService::Get()", StaticAccessorType.Arrow)]
	internal static class BurstCompilerService
	{
		// Token: 0x06000429 RID: 1065 RVA: 0x00008308 File Offset: 0x00006508
		[NativeMethod("Initialize")]
		private unsafe static string InitializeInternal(string path, BurstCompilerService.ExtractCompilerFlags extractCompilerFlags)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(path, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = path.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				BurstCompilerService.InitializeInternal_Injected(ref managedSpanWrapper, extractCompilerFlags, out managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				ManagedSpanWrapper managedSpanWrapper2;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper2);
			}
			return stringAndDispose;
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0000836C File Offset: 0x0000656C
		[ThreadSafe]
		public unsafe static string GetDisassembly(MethodInfo m, string compilerOptions)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(compilerOptions, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = compilerOptions.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ManagedSpanWrapper managedSpanWrapper2;
				BurstCompilerService.GetDisassembly_Injected(m, ref managedSpanWrapper, out managedSpanWrapper2);
			}
			finally
			{
				char* ptr = null;
				ManagedSpanWrapper managedSpanWrapper2;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper2);
			}
			return stringAndDispose;
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000083D0 File Offset: 0x000065D0
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static int CompileAsyncDelegateMethod(object delegateMethod, string compilerOptions)
		{
			int num;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(compilerOptions, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = compilerOptions.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				num = BurstCompilerService.CompileAsyncDelegateMethod_Injected(delegateMethod, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x0600042C RID: 1068
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void* GetAsyncCompiledAsyncDelegateMethod(int userID);

		// Token: 0x0600042D RID: 1069
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void* GetOrCreateSharedMemory(ref Hash128 key, uint size_of, uint alignment);

		// Token: 0x0600042E RID: 1070 RVA: 0x00008428 File Offset: 0x00006628
		[ThreadSafe]
		public static string GetMethodSignature(MethodInfo method)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				BurstCompilerService.GetMethodSignature_Injected(method, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600042F RID: 1071
		public static extern bool IsInitialized
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000430 RID: 1072
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetCurrentExecutionMode(uint environment);

		// Token: 0x06000431 RID: 1073
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern uint GetCurrentExecutionMode();

		// Token: 0x06000432 RID: 1074
		[FreeFunction("DefaultBurstLogCallback", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Log(void* userData, BurstCompilerService.BurstLogType logType, byte* message, byte* filename, int lineNumber);

		// Token: 0x06000433 RID: 1075
		[FreeFunction("DefaultBurstRuntimeLogCallback", true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void RuntimeLog(void* userData, BurstCompilerService.BurstLogType logType, byte* message, byte* filename, int lineNumber);

		// Token: 0x06000434 RID: 1076 RVA: 0x00008458 File Offset: 0x00006658
		public unsafe static bool LoadBurstLibrary(string fullPathToLibBurstGenerated)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(fullPathToLibBurstGenerated, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = fullPathToLibBurstGenerated.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = BurstCompilerService.LoadBurstLibrary_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000084B0 File Offset: 0x000066B0
		public static void Initialize(string folderRuntime, BurstCompilerService.ExtractCompilerFlags extractCompilerFlags)
		{
			bool flag = folderRuntime == null;
			if (flag)
			{
				throw new ArgumentNullException("folderRuntime");
			}
			bool flag2 = extractCompilerFlags == null;
			if (flag2)
			{
				throw new ArgumentNullException("extractCompilerFlags");
			}
			bool flag3 = !Directory.Exists(folderRuntime);
			if (flag3)
			{
				Debug.LogError("Unable to initialize the burst JIT compiler. The folder `" + folderRuntime + "` does not exist");
			}
			else
			{
				string text = BurstCompilerService.InitializeInternal(folderRuntime, extractCompilerFlags);
				bool flag4 = !string.IsNullOrEmpty(text);
				if (flag4)
				{
					Debug.LogError("Unexpected error while trying to initialize the burst JIT compiler: " + text);
				}
			}
		}

		// Token: 0x06000436 RID: 1078
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InitializeInternal_Injected(ref ManagedSpanWrapper path, BurstCompilerService.ExtractCompilerFlags extractCompilerFlags, out ManagedSpanWrapper ret);

		// Token: 0x06000437 RID: 1079
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetDisassembly_Injected(MethodInfo m, ref ManagedSpanWrapper compilerOptions, out ManagedSpanWrapper ret);

		// Token: 0x06000438 RID: 1080
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int CompileAsyncDelegateMethod_Injected(object delegateMethod, ref ManagedSpanWrapper compilerOptions);

		// Token: 0x06000439 RID: 1081
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMethodSignature_Injected(MethodInfo method, out ManagedSpanWrapper ret);

		// Token: 0x0600043A RID: 1082
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool LoadBurstLibrary_Injected(ref ManagedSpanWrapper fullPathToLibBurstGenerated);

		// Token: 0x020000C9 RID: 201
		// (Invoke) Token: 0x0600043C RID: 1084
		public delegate bool ExtractCompilerFlags(Type jobType, out string flags);

		// Token: 0x020000CA RID: 202
		public enum BurstLogType
		{
			// Token: 0x04000292 RID: 658
			Info,
			// Token: 0x04000293 RID: 659
			Warning,
			// Token: 0x04000294 RID: 660
			Error
		}
	}
}
