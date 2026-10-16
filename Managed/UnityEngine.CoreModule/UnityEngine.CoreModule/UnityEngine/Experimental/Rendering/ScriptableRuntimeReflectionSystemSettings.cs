using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x02000590 RID: 1424
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Camera/ScriptableRuntimeReflectionSystem.h")]
	public static class ScriptableRuntimeReflectionSystemSettings
	{
		// Token: 0x170008FF RID: 2303
		// (get) Token: 0x060036FC RID: 14076 RVA: 0x0006C7E4 File Offset: 0x0006A9E4
		// (set) Token: 0x060036FD RID: 14077 RVA: 0x0006C7FC File Offset: 0x0006A9FC
		public static IScriptableRuntimeReflectionSystem system
		{
			get
			{
				return ScriptableRuntimeReflectionSystemSettings.Internal_ScriptableRuntimeReflectionSystemSettings_system;
			}
			set
			{
				bool flag = value == null || value.Equals(null);
				if (flag)
				{
					Debug.LogError("'null' cannot be assigned to ScriptableRuntimeReflectionSystemSettings.system");
				}
				else
				{
					bool flag2 = !(ScriptableRuntimeReflectionSystemSettings.system is BuiltinRuntimeReflectionSystem) && !(value is BuiltinRuntimeReflectionSystem) && ScriptableRuntimeReflectionSystemSettings.system != value;
					if (flag2)
					{
						Debug.LogWarningFormat("ScriptableRuntimeReflectionSystemSettings.system is assigned more than once. Only a the last instance will be used. (Last instance {0}, New instance {1})", new object[]
						{
							ScriptableRuntimeReflectionSystemSettings.system,
							value
						});
					}
					ScriptableRuntimeReflectionSystemSettings.Internal_ScriptableRuntimeReflectionSystemSettings_system = value;
				}
			}
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x060036FE RID: 14078 RVA: 0x0006C874 File Offset: 0x0006AA74
		// (set) Token: 0x060036FF RID: 14079 RVA: 0x0006C890 File Offset: 0x0006AA90
		private static IScriptableRuntimeReflectionSystem Internal_ScriptableRuntimeReflectionSystemSettings_system
		{
			get
			{
				return ScriptableRuntimeReflectionSystemSettings.s_Instance.implementation;
			}
			[RequiredByNativeCode]
			set
			{
				bool flag = ScriptableRuntimeReflectionSystemSettings.s_Instance.implementation != value;
				if (flag)
				{
					bool flag2 = ScriptableRuntimeReflectionSystemSettings.s_Instance.implementation != null;
					if (flag2)
					{
						ScriptableRuntimeReflectionSystemSettings.s_Instance.implementation.Dispose();
					}
				}
				ScriptableRuntimeReflectionSystemSettings.s_Instance.implementation = value;
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06003700 RID: 14080 RVA: 0x0006C8E4 File Offset: 0x0006AAE4
		private static ScriptableRuntimeReflectionSystemWrapper Internal_ScriptableRuntimeReflectionSystemSettings_instance
		{
			[RequiredByNativeCode]
			get
			{
				return ScriptableRuntimeReflectionSystemSettings.s_Instance;
			}
		}

		// Token: 0x06003701 RID: 14081
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		[StaticAccessor("ScriptableRuntimeReflectionSystem", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ScriptingDirtyReflectionSystemInstance();

		// Token: 0x040012E0 RID: 4832
		private static ScriptableRuntimeReflectionSystemWrapper s_Instance = new ScriptableRuntimeReflectionSystemWrapper();
	}
}
