using System;
using System.Reflection;
using System.Reflection.Emit;
using MonoMod.RuntimeDetour;
using MonoMod.Utils;

namespace HarmonyLib
{
	// Token: 0x0200001E RID: 30
	public static class Memory
	{
		// Token: 0x060000A4 RID: 164 RVA: 0x00005318 File Offset: 0x00003518
		public unsafe static void MarkForNoInlining(MethodBase method)
		{
			if (AccessTools.IsMonoRuntime)
			{
				byte* ptr = (byte*)(void*)method.MethodHandle.Value + 2;
				*(short*)ptr = (short)(*(ushort*)ptr | 8);
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00005348 File Offset: 0x00003548
		public static string DetourMethod(MethodBase original, MethodBase replacement)
		{
			Exception ex;
			long methodStart = Memory.GetMethodStart(original, out ex);
			if (methodStart == 0L)
			{
				return ex.Message;
			}
			Memory.PadShortMethods(original);
			long methodStart2 = Memory.GetMethodStart(replacement, out ex);
			if (methodStart2 == 0L)
			{
				return ex.Message;
			}
			return Memory.WriteJump(methodStart, methodStart2);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00005388 File Offset: 0x00003588
		internal static void DetourCompiledMethod(IntPtr originalCodeStart, MethodBase replacement)
		{
			Exception ex;
			long methodStart = Memory.GetMethodStart(replacement, out ex);
			if (methodStart != 0L && ex == null)
			{
				Memory.WriteJump((long)originalCodeStart, methodStart);
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000053B4 File Offset: 0x000035B4
		internal static void DetourMethodAndPersist(MethodBase original, MethodBase replacement)
		{
			string text = Memory.DetourMethod(original, replacement);
			if (text != null)
			{
				throw new FormatException("Method " + original.FullDescription() + " cannot be patched. Reason: " + text);
			}
			PatchTools.RememberObject(original, replacement);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000053F0 File Offset: 0x000035F0
		internal static void PadShortMethods(MethodBase method)
		{
			if (Tools.isWindows)
			{
				return;
			}
			MethodBody methodBody = method.GetMethodBody();
			int? num;
			if (methodBody == null)
			{
				num = null;
			}
			else
			{
				byte[] ilasByteArray = methodBody.GetILAsByteArray();
				num = ((ilasByteArray != null) ? new int?(ilasByteArray.Length) : null);
			}
			int? num2 = num;
			int valueOrDefault = num2.GetValueOrDefault();
			if (valueOrDefault == 0)
			{
				return;
			}
			if (valueOrDefault >= 16)
			{
				return;
			}
			DynamicMethodDefinition dynamicMethodDefinition = new DynamicMethodDefinition(string.Format("PadMethod-{0}", Guid.NewGuid()), typeof(void), new Type[0]);
			dynamicMethodDefinition.GetILGenerator().Emit(OpCodes.Ret);
			dynamicMethodDefinition.Generate().Invoke(null, null);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00005490 File Offset: 0x00003690
		public static string WriteJump(long memory, long destination)
		{
			NativeDetourData nativeDetourData = DetourHelper.Native.Create((IntPtr)memory, (IntPtr)destination, null);
			DetourHelper.Native.MakeWritable(nativeDetourData);
			DetourHelper.Native.Apply(nativeDetourData);
			DetourHelper.Native.MakeExecutable(nativeDetourData);
			DetourHelper.Native.FlushICache(nativeDetourData);
			DetourHelper.Native.Free(nativeDetourData);
			return null;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000054F8 File Offset: 0x000036F8
		public static long GetMethodStart(MethodBase method, out Exception exception)
		{
			long num;
			try
			{
				exception = null;
				num = method.Pin<MethodBase>().GetNativeStart().ToInt64();
			}
			catch (Exception ex)
			{
				exception = ex;
				num = 0L;
			}
			return num;
		}
	}
}
