using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020002A7 RID: 679
	[RequiredByNativeCode]
	[ExtensionOfNativeClass]
	[NativeClass(null)]
	[NativeHeader("Runtime/Mono/MonoBehaviour.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class ScriptableObject : Object
	{
		// Token: 0x0600218E RID: 8590 RVA: 0x000413C6 File Offset: 0x0003F5C6
		public ScriptableObject()
		{
			ScriptableObject.CreateScriptableObject(this);
		}

		// Token: 0x0600218F RID: 8591 RVA: 0x000413D8 File Offset: 0x0003F5D8
		[NativeConditional("ENABLE_MONO")]
		[Obsolete("Use EditorUtility.SetDirty instead")]
		public void SetDirty()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ScriptableObject>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ScriptableObject.SetDirty_Injected(intPtr);
		}

		// Token: 0x06002190 RID: 8592 RVA: 0x000413FC File Offset: 0x0003F5FC
		public static ScriptableObject CreateInstance(string className)
		{
			return ScriptableObject.CreateScriptableObjectInstanceFromName(className);
		}

		// Token: 0x06002191 RID: 8593 RVA: 0x00041414 File Offset: 0x0003F614
		public static ScriptableObject CreateInstance(Type type)
		{
			return ScriptableObject.CreateScriptableObjectInstanceFromType(type, true);
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x00041430 File Offset: 0x0003F630
		public static T CreateInstance<T>() where T : ScriptableObject
		{
			return (T)((object)ScriptableObject.CreateInstance(typeof(T)));
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x00041458 File Offset: 0x0003F658
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal static ScriptableObject CreateInstance(Type type, Action<ScriptableObject> initialize)
		{
			bool flag = !typeof(ScriptableObject).IsAssignableFrom(type);
			if (flag)
			{
				throw new ArgumentException("Type must inherit ScriptableObject.", "type");
			}
			ScriptableObject scriptableObject = ScriptableObject.CreateScriptableObjectInstanceFromType(type, false);
			try
			{
				initialize(scriptableObject);
			}
			finally
			{
				ScriptableObject.ResetAndApplyDefaultInstances(scriptableObject);
			}
			return scriptableObject;
		}

		// Token: 0x06002194 RID: 8596
		[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateScriptableObject([Writable] ScriptableObject self);

		// Token: 0x06002195 RID: 8597 RVA: 0x000414C0 File Offset: 0x0003F6C0
		[FreeFunction("Scripting::CreateScriptableObject")]
		private unsafe static ScriptableObject CreateScriptableObjectInstanceFromName(string className)
		{
			ScriptableObject scriptableObject;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(className, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = className.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr = ScriptableObject.CreateScriptableObjectInstanceFromName_Injected(ref managedSpanWrapper);
			}
			finally
			{
				IntPtr intPtr;
				scriptableObject = Unmarshal.UnmarshalUnityObject<ScriptableObject>(intPtr);
				char* ptr = null;
			}
			return scriptableObject;
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x00041520 File Offset: 0x0003F720
		[NativeMethod(Name = "Scripting::CreateScriptableObjectWithType", IsFreeFunction = true, ThrowsException = true)]
		internal static ScriptableObject CreateScriptableObjectInstanceFromType(Type type, bool applyDefaultsAndReset)
		{
			return Unmarshal.UnmarshalUnityObject<ScriptableObject>(ScriptableObject.CreateScriptableObjectInstanceFromType_Injected(type, applyDefaultsAndReset));
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x0004153C File Offset: 0x0003F73C
		[FreeFunction("Scripting::ResetAndApplyDefaultInstances")]
		internal static void ResetAndApplyDefaultInstances([NotNull] Object obj)
		{
			if (obj == null)
			{
				ThrowHelper.ThrowArgumentNullException(obj, "obj");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Object>(obj);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(obj, "obj");
			}
			ScriptableObject.ResetAndApplyDefaultInstances_Injected(intPtr);
		}

		// Token: 0x06002198 RID: 8600
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetDirty_Injected(IntPtr _unity_self);

		// Token: 0x06002199 RID: 8601
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateScriptableObjectInstanceFromName_Injected(ref ManagedSpanWrapper className);

		// Token: 0x0600219A RID: 8602
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateScriptableObjectInstanceFromType_Injected(Type type, bool applyDefaultsAndReset);

		// Token: 0x0600219B RID: 8603
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetAndApplyDefaultInstances_Injected(IntPtr obj);
	}
}
