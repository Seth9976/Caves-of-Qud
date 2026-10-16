using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x02000013 RID: 19
	[NativeType(Header = "Modules/VFX/Public/VFXExpressionValues.h")]
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class VFXExpressionValues
	{
		// Token: 0x0600005E RID: 94 RVA: 0x000028B6 File Offset: 0x00000AB6
		private VFXExpressionValues()
		{
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000028C0 File Offset: 0x00000AC0
		[RequiredByNativeCode]
		internal static VFXExpressionValues CreateExpressionValuesWrapper(IntPtr ptr)
		{
			return new VFXExpressionValues
			{
				m_Ptr = ptr
			};
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000028E0 File Offset: 0x00000AE0
		[NativeThrows]
		[NativeName("GetValueFromScript<bool>")]
		public bool GetBool(int nameID)
		{
			IntPtr intPtr = VFXExpressionValues.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VFXExpressionValues.GetBool_Injected(intPtr, nameID);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002904 File Offset: 0x00000B04
		[NativeThrows]
		[NativeName("GetValueFromScript<int>")]
		public int GetInt(int nameID)
		{
			IntPtr intPtr = VFXExpressionValues.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VFXExpressionValues.GetInt_Injected(intPtr, nameID);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002928 File Offset: 0x00000B28
		[NativeThrows]
		[NativeName("GetValueFromScript<UInt32>")]
		public uint GetUInt(int nameID)
		{
			IntPtr intPtr = VFXExpressionValues.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VFXExpressionValues.GetUInt_Injected(intPtr, nameID);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000294C File Offset: 0x00000B4C
		[NativeThrows]
		[NativeName("GetValueFromScript<float>")]
		public float GetFloat(int nameID)
		{
			IntPtr intPtr = VFXExpressionValues.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VFXExpressionValues.GetFloat_Injected(intPtr, nameID);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002970 File Offset: 0x00000B70
		[NativeName("GetValueFromScript<Vector2f>")]
		[NativeThrows]
		public Vector2 GetVector2(int nameID)
		{
			IntPtr intPtr = VFXExpressionValues.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector2 vector;
			VFXExpressionValues.GetVector2_Injected(intPtr, nameID, out vector);
			return vector;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002998 File Offset: 0x00000B98
		[NativeThrows]
		[NativeName("GetValueFromScript<Vector3f>")]
		public Vector3 GetVector3(int nameID)
		{
			IntPtr intPtr = VFXExpressionValues.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			VFXExpressionValues.GetVector3_Injected(intPtr, nameID, out vector);
			return vector;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000029C0 File Offset: 0x00000BC0
		[NativeName("GetValueFromScript<Vector4f>")]
		[NativeThrows]
		public Vector4 GetVector4(int nameID)
		{
			IntPtr intPtr = VFXExpressionValues.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector4 vector;
			VFXExpressionValues.GetVector4_Injected(intPtr, nameID, out vector);
			return vector;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000029E8 File Offset: 0x00000BE8
		[NativeName("GetValueFromScript<Matrix4x4f>")]
		[NativeThrows]
		public Matrix4x4 GetMatrix4x4(int nameID)
		{
			IntPtr intPtr = VFXExpressionValues.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Matrix4x4 matrix4x;
			VFXExpressionValues.GetMatrix4x4_Injected(intPtr, nameID, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002A10 File Offset: 0x00000C10
		[NativeThrows]
		[NativeName("GetValueFromScript<Texture*>")]
		public Texture GetTexture(int nameID)
		{
			IntPtr intPtr = VFXExpressionValues.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Texture>(VFXExpressionValues.GetTexture_Injected(intPtr, nameID));
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002A38 File Offset: 0x00000C38
		[NativeName("GetValueFromScript<Mesh*>")]
		[NativeThrows]
		public Mesh GetMesh(int nameID)
		{
			IntPtr intPtr = VFXExpressionValues.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Mesh>(VFXExpressionValues.GetMesh_Injected(intPtr, nameID));
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002A60 File Offset: 0x00000C60
		public AnimationCurve GetAnimationCurve(int nameID)
		{
			AnimationCurve animationCurve = new AnimationCurve();
			this.Internal_GetAnimationCurveFromScript(nameID, animationCurve);
			return animationCurve;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002A84 File Offset: 0x00000C84
		[NativeThrows]
		internal void Internal_GetAnimationCurveFromScript(int nameID, AnimationCurve curve)
		{
			IntPtr intPtr = VFXExpressionValues.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VFXExpressionValues.Internal_GetAnimationCurveFromScript_Injected(intPtr, nameID, (curve == null) ? ((IntPtr)0) : AnimationCurve.BindingsMarshaller.ConvertToNative(curve));
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002AB8 File Offset: 0x00000CB8
		public Gradient GetGradient(int nameID)
		{
			Gradient gradient = new Gradient();
			this.Internal_GetGradientFromScript(nameID, gradient);
			return gradient;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002ADC File Offset: 0x00000CDC
		[NativeThrows]
		internal void Internal_GetGradientFromScript(int nameID, Gradient gradient)
		{
			IntPtr intPtr = VFXExpressionValues.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VFXExpressionValues.Internal_GetGradientFromScript_Injected(intPtr, nameID, (gradient == null) ? ((IntPtr)0) : Gradient.BindingsMarshaller.ConvertToNative(gradient));
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002B10 File Offset: 0x00000D10
		public bool GetBool(string name)
		{
			return this.GetBool(Shader.PropertyToID(name));
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002B30 File Offset: 0x00000D30
		public int GetInt(string name)
		{
			return this.GetInt(Shader.PropertyToID(name));
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002B50 File Offset: 0x00000D50
		public uint GetUInt(string name)
		{
			return this.GetUInt(Shader.PropertyToID(name));
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002B70 File Offset: 0x00000D70
		public float GetFloat(string name)
		{
			return this.GetFloat(Shader.PropertyToID(name));
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002B90 File Offset: 0x00000D90
		public Vector2 GetVector2(string name)
		{
			return this.GetVector2(Shader.PropertyToID(name));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002BB0 File Offset: 0x00000DB0
		public Vector3 GetVector3(string name)
		{
			return this.GetVector3(Shader.PropertyToID(name));
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002BD0 File Offset: 0x00000DD0
		public Vector4 GetVector4(string name)
		{
			return this.GetVector4(Shader.PropertyToID(name));
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002BF0 File Offset: 0x00000DF0
		public Matrix4x4 GetMatrix4x4(string name)
		{
			return this.GetMatrix4x4(Shader.PropertyToID(name));
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002C10 File Offset: 0x00000E10
		public Texture GetTexture(string name)
		{
			return this.GetTexture(Shader.PropertyToID(name));
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002C30 File Offset: 0x00000E30
		public AnimationCurve GetAnimationCurve(string name)
		{
			return this.GetAnimationCurve(Shader.PropertyToID(name));
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002C50 File Offset: 0x00000E50
		public Gradient GetGradient(string name)
		{
			return this.GetGradient(Shader.PropertyToID(name));
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002C70 File Offset: 0x00000E70
		public Mesh GetMesh(string name)
		{
			return this.GetMesh(Shader.PropertyToID(name));
		}

		// Token: 0x0600007A RID: 122
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetBool_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x0600007B RID: 123
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetInt_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x0600007C RID: 124
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetUInt_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x0600007D RID: 125
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetFloat_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x0600007E RID: 126
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetVector2_Injected(IntPtr _unity_self, int nameID, out Vector2 ret);

		// Token: 0x0600007F RID: 127
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetVector3_Injected(IntPtr _unity_self, int nameID, out Vector3 ret);

		// Token: 0x06000080 RID: 128
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetVector4_Injected(IntPtr _unity_self, int nameID, out Vector4 ret);

		// Token: 0x06000081 RID: 129
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMatrix4x4_Injected(IntPtr _unity_self, int nameID, out Matrix4x4 ret);

		// Token: 0x06000082 RID: 130
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetTexture_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x06000083 RID: 131
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetMesh_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x06000084 RID: 132
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetAnimationCurveFromScript_Injected(IntPtr _unity_self, int nameID, IntPtr curve);

		// Token: 0x06000085 RID: 133
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetGradientFromScript_Injected(IntPtr _unity_self, int nameID, IntPtr gradient);

		// Token: 0x0400010F RID: 271
		internal IntPtr m_Ptr;

		// Token: 0x02000014 RID: 20
		internal static class BindingsMarshaller
		{
			// Token: 0x06000086 RID: 134 RVA: 0x00002C8E File Offset: 0x00000E8E
			public static IntPtr ConvertToNative(VFXExpressionValues vFXExpressionValues)
			{
				return vFXExpressionValues.m_Ptr;
			}
		}
	}
}
