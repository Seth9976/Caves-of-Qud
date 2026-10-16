using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using Mono.Cecil;
using Mono.Cecil.Cil;

namespace MonoMod.Utils
{
	// Token: 0x0200043C RID: 1084
	internal static class DynamicMethodHelper
	{
		// Token: 0x060016C7 RID: 5831 RVA: 0x0004C2DE File Offset: 0x0004A4DE
		public static object GetReference(int id)
		{
			return DynamicMethodHelper.References[id];
		}

		// Token: 0x060016C8 RID: 5832 RVA: 0x0004C2EB File Offset: 0x0004A4EB
		public static void SetReference(int id, object obj)
		{
			DynamicMethodHelper.References[id] = obj;
		}

		// Token: 0x060016C9 RID: 5833 RVA: 0x0004C2FC File Offset: 0x0004A4FC
		private static int AddReference(object obj)
		{
			List<object> references = DynamicMethodHelper.References;
			int num;
			lock (references)
			{
				DynamicMethodHelper.References.Add(obj);
				num = DynamicMethodHelper.References.Count - 1;
			}
			return num;
		}

		// Token: 0x060016CA RID: 5834 RVA: 0x0004C350 File Offset: 0x0004A550
		public static void FreeReference(int id)
		{
			DynamicMethodHelper.References[id] = null;
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x0004C360 File Offset: 0x0004A560
		public static DynamicMethod Stub(this DynamicMethod dm)
		{
			ILGenerator ilgenerator = dm.GetILGenerator();
			for (int i = 0; i < 32; i++)
			{
				ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Nop);
			}
			if (dm.ReturnType != typeof(void))
			{
				ilgenerator.DeclareLocal(dm.ReturnType);
				ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldloca_S, 0);
				ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Initobj, dm.ReturnType);
				ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ldloc_0);
			}
			ilgenerator.Emit(global::System.Reflection.Emit.OpCodes.Ret);
			return dm;
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x0004C3E4 File Offset: 0x0004A5E4
		public static DynamicMethodDefinition Stub(this DynamicMethodDefinition dmd)
		{
			ILProcessor ilprocessor = dmd.GetILProcessor();
			for (int i = 0; i < 32; i++)
			{
				ilprocessor.Emit(Mono.Cecil.Cil.OpCodes.Nop);
			}
			if (dmd.Definition.ReturnType != dmd.Definition.Module.TypeSystem.Void)
			{
				ilprocessor.Body.Variables.Add(new VariableDefinition(dmd.Definition.ReturnType));
				ilprocessor.Emit(Mono.Cecil.Cil.OpCodes.Ldloca_S, 0);
				ilprocessor.Emit(Mono.Cecil.Cil.OpCodes.Initobj, dmd.Definition.ReturnType);
				ilprocessor.Emit(Mono.Cecil.Cil.OpCodes.Ldloc_0);
			}
			ilprocessor.Emit(Mono.Cecil.Cil.OpCodes.Ret);
			return dmd;
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x0004C48C File Offset: 0x0004A68C
		public static int EmitReference<T>(this ILGenerator il, T obj)
		{
			Type typeFromHandle = typeof(T);
			int num = DynamicMethodHelper.AddReference(obj);
			il.Emit(global::System.Reflection.Emit.OpCodes.Ldc_I4, num);
			il.Emit(global::System.Reflection.Emit.OpCodes.Call, DynamicMethodHelper._GetReference);
			if (typeFromHandle.IsValueType)
			{
				il.Emit(global::System.Reflection.Emit.OpCodes.Unbox_Any, typeFromHandle);
			}
			return num;
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x0004C4E4 File Offset: 0x0004A6E4
		public static int EmitReference<T>(this ILProcessor il, T obj)
		{
			ModuleDefinition module = il.Body.Method.Module;
			Type typeFromHandle = typeof(T);
			int num = DynamicMethodHelper.AddReference(obj);
			il.Emit(Mono.Cecil.Cil.OpCodes.Ldc_I4, num);
			il.Emit(Mono.Cecil.Cil.OpCodes.Call, module.ImportReference(DynamicMethodHelper._GetReference));
			if (typeFromHandle.IsValueType)
			{
				il.Emit(Mono.Cecil.Cil.OpCodes.Unbox_Any, module.ImportReference(typeFromHandle));
			}
			return num;
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x0004C558 File Offset: 0x0004A758
		public static int EmitGetReference<T>(this ILGenerator il, int id)
		{
			Type typeFromHandle = typeof(T);
			il.Emit(global::System.Reflection.Emit.OpCodes.Ldc_I4, id);
			il.Emit(global::System.Reflection.Emit.OpCodes.Call, DynamicMethodHelper._GetReference);
			if (typeFromHandle.IsValueType)
			{
				il.Emit(global::System.Reflection.Emit.OpCodes.Unbox_Any, typeFromHandle);
			}
			return id;
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x0004C5A4 File Offset: 0x0004A7A4
		public static int EmitGetReference<T>(this ILProcessor il, int id)
		{
			ModuleDefinition module = il.Body.Method.Module;
			Type typeFromHandle = typeof(T);
			il.Emit(Mono.Cecil.Cil.OpCodes.Ldc_I4, id);
			il.Emit(Mono.Cecil.Cil.OpCodes.Call, module.ImportReference(DynamicMethodHelper._GetReference));
			if (typeFromHandle.IsValueType)
			{
				il.Emit(Mono.Cecil.Cil.OpCodes.Unbox_Any, module.ImportReference(typeFromHandle));
			}
			return id;
		}

		// Token: 0x04000FF2 RID: 4082
		private static List<object> References = new List<object>();

		// Token: 0x04000FF3 RID: 4083
		private static readonly MethodInfo _GetMethodFromHandle = typeof(MethodBase).GetMethod("GetMethodFromHandle", new Type[] { typeof(RuntimeMethodHandle) });

		// Token: 0x04000FF4 RID: 4084
		private static readonly MethodInfo _GetReference = typeof(DynamicMethodHelper).GetMethod("GetReference");
	}
}
