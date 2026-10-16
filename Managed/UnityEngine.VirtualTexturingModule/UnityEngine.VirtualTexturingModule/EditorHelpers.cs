using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.VirtualTexturing
{
	// Token: 0x02000003 RID: 3
	[NativeConditional("UNITY_EDITOR")]
	[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
	[StaticAccessor("VirtualTexturing::Editor", StaticAccessorType.DoubleColon)]
	public static class EditorHelpers
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10
		[NativeThrows]
		internal static extern int tileSize
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002104 File Offset: 0x00000304
		[NativeThrows]
		public static bool ValidateTextureStack([Unmarshalled] [NotNull] Texture[] textures, out string errorMessage)
		{
			if (textures == null)
			{
				ThrowHelper.ThrowArgumentNullException(textures, "textures");
			}
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				flag = EditorHelpers.ValidateTextureStack_Injected(textures, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				errorMessage = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return flag;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002148 File Offset: 0x00000348
		[NativeThrows]
		internal static EditorHelpers.StackValidationResult[] ValidateMaterialTextureStacks([NotNull] Material mat)
		{
			if (mat == null)
			{
				ThrowHelper.ThrowArgumentNullException(mat, "mat");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Material>(mat);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(mat, "mat");
			}
			return EditorHelpers.ValidateMaterialTextureStacks_Injected(intPtr);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002180 File Offset: 0x00000380
		[NativeConditional("UNITY_EDITOR")]
		[NativeThrows]
		public static GraphicsFormat[] QuerySupportedFormats()
		{
			GraphicsFormat[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				EditorHelpers.QuerySupportedFormats_Injected(out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				GraphicsFormat[] array;
				blittableArrayWrapper.Unmarshal<GraphicsFormat>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600000E RID: 14
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ValidateTextureStack_Injected(Texture[] textures, out ManagedSpanWrapper errorMessage);

		// Token: 0x0600000F RID: 15
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern EditorHelpers.StackValidationResult[] ValidateMaterialTextureStacks_Injected(IntPtr mat);

		// Token: 0x06000010 RID: 16
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void QuerySupportedFormats_Injected(out BlittableArrayWrapper ret);

		// Token: 0x02000004 RID: 4
		[NativeHeader("Runtime/Shaders/SharedMaterialData.h")]
		internal struct StackValidationResult
		{
			// Token: 0x04000002 RID: 2
			public string stackName;

			// Token: 0x04000003 RID: 3
			public string errorMessage;
		}
	}
}
