using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x02000011 RID: 17
	[NativeType(Header = "Modules/VFX/Public/VFXEventAttribute.h")]
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class VFXEventAttribute : IDisposable
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private VFXEventAttribute(IntPtr ptr, bool owner, VisualEffectAsset vfxAsset)
		{
			this.m_Ptr = ptr;
			this.m_Owner = owner;
			this.m_VfxAsset = vfxAsset;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000206F File Offset: 0x0000026F
		private VFXEventAttribute(IntPtr ptr)
		{
			this.m_Ptr = ptr;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002080 File Offset: 0x00000280
		private VFXEventAttribute()
			: this(IntPtr.Zero, false, null)
		{
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002094 File Offset: 0x00000294
		internal static VFXEventAttribute CreateEventAttributeWrapper()
		{
			return new VFXEventAttribute(IntPtr.Zero, false, null);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020B4 File Offset: 0x000002B4
		internal void SetWrapValue(IntPtr ptrToEventAttribute)
		{
			bool owner = this.m_Owner;
			if (owner)
			{
				throw new Exception("VFXSpawnerState : SetWrapValue is reserved to CreateWrapper object");
			}
			this.m_Ptr = ptrToEventAttribute;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020E0 File Offset: 0x000002E0
		public VFXEventAttribute(VFXEventAttribute original)
		{
			bool flag = original == null;
			if (flag)
			{
				throw new ArgumentNullException("VFXEventAttribute expect a non null attribute");
			}
			this.m_Ptr = VFXEventAttribute.Internal_Create();
			this.m_VfxAsset = original.m_VfxAsset;
			this.Internal_InitFromEventAttribute(original);
		}

		// Token: 0x06000007 RID: 7
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr Internal_Create();

		// Token: 0x06000008 RID: 8 RVA: 0x00002128 File Offset: 0x00000328
		internal static VFXEventAttribute Internal_InstanciateVFXEventAttribute(VisualEffectAsset vfxAsset)
		{
			VFXEventAttribute vfxeventAttribute = new VFXEventAttribute(VFXEventAttribute.Internal_Create(), true, vfxAsset);
			vfxeventAttribute.Internal_InitFromAsset(vfxAsset);
			return vfxeventAttribute;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002150 File Offset: 0x00000350
		internal void Internal_InitFromAsset(VisualEffectAsset vfxAsset)
		{
			IntPtr intPtr = VFXEventAttribute.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VFXEventAttribute.Internal_InitFromAsset_Injected(intPtr, Object.MarshalledUnityObject.Marshal<VisualEffectAsset>(vfxAsset));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002178 File Offset: 0x00000378
		internal void Internal_InitFromEventAttribute(VFXEventAttribute vfxEventAttribute)
		{
			IntPtr intPtr = VFXEventAttribute.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VFXEventAttribute.Internal_InitFromEventAttribute_Injected(intPtr, (vfxEventAttribute == null) ? ((IntPtr)0) : VFXEventAttribute.BindingsMarshaller.ConvertToNative(vfxEventAttribute));
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000021AC File Offset: 0x000003AC
		internal VisualEffectAsset vfxAsset
		{
			get
			{
				return this.m_VfxAsset;
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021C4 File Offset: 0x000003C4
		private void Release()
		{
			bool flag = this.m_Owner && this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				VFXEventAttribute.Internal_Destroy(this.m_Ptr);
			}
			this.m_Ptr = IntPtr.Zero;
			this.m_VfxAsset = null;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002214 File Offset: 0x00000414
		~VFXEventAttribute()
		{
			this.Release();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002244 File Offset: 0x00000444
		public void Dispose()
		{
			this.Release();
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600000F RID: 15
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_Destroy(IntPtr ptr);

		// Token: 0x06000010 RID: 16 RVA: 0x00002258 File Offset: 0x00000458
		[NativeName("HasValueFromScript<bool>")]
		public bool HasBool(int nameID)
		{
			IntPtr intPtr = VFXEventAttribute.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VFXEventAttribute.HasBool_Injected(intPtr, nameID);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000227C File Offset: 0x0000047C
		[NativeName("HasValueFromScript<int>")]
		public bool HasInt(int nameID)
		{
			IntPtr intPtr = VFXEventAttribute.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VFXEventAttribute.HasInt_Injected(intPtr, nameID);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000022A0 File Offset: 0x000004A0
		[NativeName("HasValueFromScript<UInt32>")]
		public bool HasUint(int nameID)
		{
			IntPtr intPtr = VFXEventAttribute.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VFXEventAttribute.HasUint_Injected(intPtr, nameID);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022C4 File Offset: 0x000004C4
		[NativeName("HasValueFromScript<float>")]
		public bool HasFloat(int nameID)
		{
			IntPtr intPtr = VFXEventAttribute.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VFXEventAttribute.HasFloat_Injected(intPtr, nameID);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022E8 File Offset: 0x000004E8
		[NativeName("HasValueFromScript<Vector2f>")]
		public bool HasVector2(int nameID)
		{
			IntPtr intPtr = VFXEventAttribute.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VFXEventAttribute.HasVector2_Injected(intPtr, nameID);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000230C File Offset: 0x0000050C
		[NativeName("HasValueFromScript<Vector3f>")]
		public bool HasVector3(int nameID)
		{
			IntPtr intPtr = VFXEventAttribute.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VFXEventAttribute.HasVector3_Injected(intPtr, nameID);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002330 File Offset: 0x00000530
		[NativeName("HasValueFromScript<Vector4f>")]
		public bool HasVector4(int nameID)
		{
			IntPtr intPtr = VFXEventAttribute.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VFXEventAttribute.HasVector4_Injected(intPtr, nameID);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002354 File Offset: 0x00000554
		[NativeName("HasValueFromScript<Matrix4x4f>")]
		public bool HasMatrix4x4(int nameID)
		{
			IntPtr intPtr = VFXEventAttribute.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VFXEventAttribute.HasMatrix4x4_Injected(intPtr, nameID);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002378 File Offset: 0x00000578
		[NativeName("SetValueFromScript<bool>")]
		public void SetBool(int nameID, bool b)
		{
			IntPtr intPtr = VFXEventAttribute.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VFXEventAttribute.SetBool_Injected(intPtr, nameID, b);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000239C File Offset: 0x0000059C
		[NativeName("SetValueFromScript<int>")]
		public void SetInt(int nameID, int i)
		{
			IntPtr intPtr = VFXEventAttribute.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VFXEventAttribute.SetInt_Injected(intPtr, nameID, i);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023C0 File Offset: 0x000005C0
		[NativeName("SetValueFromScript<UInt32>")]
		public void SetUint(int nameID, uint i)
		{
			IntPtr intPtr = VFXEventAttribute.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VFXEventAttribute.SetUint_Injected(intPtr, nameID, i);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023E4 File Offset: 0x000005E4
		[NativeName("SetValueFromScript<float>")]
		public void SetFloat(int nameID, float f)
		{
			IntPtr intPtr = VFXEventAttribute.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VFXEventAttribute.SetFloat_Injected(intPtr, nameID, f);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002408 File Offset: 0x00000608
		[NativeName("SetValueFromScript<Vector2f>")]
		public void SetVector2(int nameID, Vector2 v)
		{
			IntPtr intPtr = VFXEventAttribute.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VFXEventAttribute.SetVector2_Injected(intPtr, nameID, ref v);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002430 File Offset: 0x00000630
		[NativeName("SetValueFromScript<Vector3f>")]
		public void SetVector3(int nameID, Vector3 v)
		{
			IntPtr intPtr = VFXEventAttribute.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VFXEventAttribute.SetVector3_Injected(intPtr, nameID, ref v);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002458 File Offset: 0x00000658
		[NativeName("SetValueFromScript<Vector4f>")]
		public void SetVector4(int nameID, Vector4 v)
		{
			IntPtr intPtr = VFXEventAttribute.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VFXEventAttribute.SetVector4_Injected(intPtr, nameID, ref v);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002480 File Offset: 0x00000680
		[NativeName("SetValueFromScript<Matrix4x4f>")]
		public void SetMatrix4x4(int nameID, Matrix4x4 v)
		{
			IntPtr intPtr = VFXEventAttribute.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VFXEventAttribute.SetMatrix4x4_Injected(intPtr, nameID, ref v);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024A8 File Offset: 0x000006A8
		[NativeName("GetValueFromScript<bool>")]
		public bool GetBool(int nameID)
		{
			IntPtr intPtr = VFXEventAttribute.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VFXEventAttribute.GetBool_Injected(intPtr, nameID);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024CC File Offset: 0x000006CC
		[NativeName("GetValueFromScript<int>")]
		public int GetInt(int nameID)
		{
			IntPtr intPtr = VFXEventAttribute.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VFXEventAttribute.GetInt_Injected(intPtr, nameID);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024F0 File Offset: 0x000006F0
		[NativeName("GetValueFromScript<UInt32>")]
		public uint GetUint(int nameID)
		{
			IntPtr intPtr = VFXEventAttribute.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VFXEventAttribute.GetUint_Injected(intPtr, nameID);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002514 File Offset: 0x00000714
		[NativeName("GetValueFromScript<float>")]
		public float GetFloat(int nameID)
		{
			IntPtr intPtr = VFXEventAttribute.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return VFXEventAttribute.GetFloat_Injected(intPtr, nameID);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002538 File Offset: 0x00000738
		[NativeName("GetValueFromScript<Vector2f>")]
		public Vector2 GetVector2(int nameID)
		{
			IntPtr intPtr = VFXEventAttribute.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector2 vector;
			VFXEventAttribute.GetVector2_Injected(intPtr, nameID, out vector);
			return vector;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002560 File Offset: 0x00000760
		[NativeName("GetValueFromScript<Vector3f>")]
		public Vector3 GetVector3(int nameID)
		{
			IntPtr intPtr = VFXEventAttribute.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			VFXEventAttribute.GetVector3_Injected(intPtr, nameID, out vector);
			return vector;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002588 File Offset: 0x00000788
		[NativeName("GetValueFromScript<Vector4f>")]
		public Vector4 GetVector4(int nameID)
		{
			IntPtr intPtr = VFXEventAttribute.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector4 vector;
			VFXEventAttribute.GetVector4_Injected(intPtr, nameID, out vector);
			return vector;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025B0 File Offset: 0x000007B0
		[NativeName("GetValueFromScript<Matrix4x4f>")]
		public Matrix4x4 GetMatrix4x4(int nameID)
		{
			IntPtr intPtr = VFXEventAttribute.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Matrix4x4 matrix4x;
			VFXEventAttribute.GetMatrix4x4_Injected(intPtr, nameID, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025D8 File Offset: 0x000007D8
		public bool HasBool(string name)
		{
			return this.HasBool(Shader.PropertyToID(name));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000025F8 File Offset: 0x000007F8
		public bool HasInt(string name)
		{
			return this.HasInt(Shader.PropertyToID(name));
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002618 File Offset: 0x00000818
		public bool HasUint(string name)
		{
			return this.HasUint(Shader.PropertyToID(name));
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002638 File Offset: 0x00000838
		public bool HasFloat(string name)
		{
			return this.HasFloat(Shader.PropertyToID(name));
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002658 File Offset: 0x00000858
		public bool HasVector2(string name)
		{
			return this.HasVector2(Shader.PropertyToID(name));
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002678 File Offset: 0x00000878
		public bool HasVector3(string name)
		{
			return this.HasVector3(Shader.PropertyToID(name));
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002698 File Offset: 0x00000898
		public bool HasVector4(string name)
		{
			return this.HasVector4(Shader.PropertyToID(name));
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000026B8 File Offset: 0x000008B8
		public bool HasMatrix4x4(string name)
		{
			return this.HasMatrix4x4(Shader.PropertyToID(name));
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000026D6 File Offset: 0x000008D6
		public void SetBool(string name, bool b)
		{
			this.SetBool(Shader.PropertyToID(name), b);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000026E7 File Offset: 0x000008E7
		public void SetInt(string name, int i)
		{
			this.SetInt(Shader.PropertyToID(name), i);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000026F8 File Offset: 0x000008F8
		public void SetUint(string name, uint i)
		{
			this.SetUint(Shader.PropertyToID(name), i);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002709 File Offset: 0x00000909
		public void SetFloat(string name, float f)
		{
			this.SetFloat(Shader.PropertyToID(name), f);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000271A File Offset: 0x0000091A
		public void SetVector2(string name, Vector2 v)
		{
			this.SetVector2(Shader.PropertyToID(name), v);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000272B File Offset: 0x0000092B
		public void SetVector3(string name, Vector3 v)
		{
			this.SetVector3(Shader.PropertyToID(name), v);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000273C File Offset: 0x0000093C
		public void SetVector4(string name, Vector4 v)
		{
			this.SetVector4(Shader.PropertyToID(name), v);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000274D File Offset: 0x0000094D
		public void SetMatrix4x4(string name, Matrix4x4 v)
		{
			this.SetMatrix4x4(Shader.PropertyToID(name), v);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002760 File Offset: 0x00000960
		public bool GetBool(string name)
		{
			return this.GetBool(Shader.PropertyToID(name));
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002780 File Offset: 0x00000980
		public int GetInt(string name)
		{
			return this.GetInt(Shader.PropertyToID(name));
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000027A0 File Offset: 0x000009A0
		public uint GetUint(string name)
		{
			return this.GetUint(Shader.PropertyToID(name));
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000027C0 File Offset: 0x000009C0
		public float GetFloat(string name)
		{
			return this.GetFloat(Shader.PropertyToID(name));
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000027E0 File Offset: 0x000009E0
		public Vector2 GetVector2(string name)
		{
			return this.GetVector2(Shader.PropertyToID(name));
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002800 File Offset: 0x00000A00
		public Vector3 GetVector3(string name)
		{
			return this.GetVector3(Shader.PropertyToID(name));
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002820 File Offset: 0x00000A20
		public Vector4 GetVector4(string name)
		{
			return this.GetVector4(Shader.PropertyToID(name));
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002840 File Offset: 0x00000A40
		public Matrix4x4 GetMatrix4x4(string name)
		{
			return this.GetMatrix4x4(Shader.PropertyToID(name));
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002860 File Offset: 0x00000A60
		public void CopyValuesFrom([NotNull] VFXEventAttribute eventAttibute)
		{
			if (eventAttibute == null)
			{
				ThrowHelper.ThrowArgumentNullException(eventAttibute, "eventAttibute");
			}
			IntPtr intPtr = VFXEventAttribute.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = VFXEventAttribute.BindingsMarshaller.ConvertToNative(eventAttibute);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(eventAttibute, "eventAttibute");
			}
			VFXEventAttribute.CopyValuesFrom_Injected(intPtr, intPtr2);
		}

		// Token: 0x06000041 RID: 65
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_InitFromAsset_Injected(IntPtr _unity_self, IntPtr vfxAsset);

		// Token: 0x06000042 RID: 66
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_InitFromEventAttribute_Injected(IntPtr _unity_self, IntPtr vfxEventAttribute);

		// Token: 0x06000043 RID: 67
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasBool_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x06000044 RID: 68
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasInt_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x06000045 RID: 69
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasUint_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x06000046 RID: 70
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasFloat_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x06000047 RID: 71
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasVector2_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x06000048 RID: 72
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasVector3_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x06000049 RID: 73
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasVector4_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x0600004A RID: 74
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasMatrix4x4_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x0600004B RID: 75
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBool_Injected(IntPtr _unity_self, int nameID, bool b);

		// Token: 0x0600004C RID: 76
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetInt_Injected(IntPtr _unity_self, int nameID, int i);

		// Token: 0x0600004D RID: 77
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetUint_Injected(IntPtr _unity_self, int nameID, uint i);

		// Token: 0x0600004E RID: 78
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFloat_Injected(IntPtr _unity_self, int nameID, float f);

		// Token: 0x0600004F RID: 79
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVector2_Injected(IntPtr _unity_self, int nameID, [In] ref Vector2 v);

		// Token: 0x06000050 RID: 80
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVector3_Injected(IntPtr _unity_self, int nameID, [In] ref Vector3 v);

		// Token: 0x06000051 RID: 81
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVector4_Injected(IntPtr _unity_self, int nameID, [In] ref Vector4 v);

		// Token: 0x06000052 RID: 82
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMatrix4x4_Injected(IntPtr _unity_self, int nameID, [In] ref Matrix4x4 v);

		// Token: 0x06000053 RID: 83
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetBool_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x06000054 RID: 84
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetInt_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x06000055 RID: 85
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetUint_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x06000056 RID: 86
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetFloat_Injected(IntPtr _unity_self, int nameID);

		// Token: 0x06000057 RID: 87
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetVector2_Injected(IntPtr _unity_self, int nameID, out Vector2 ret);

		// Token: 0x06000058 RID: 88
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetVector3_Injected(IntPtr _unity_self, int nameID, out Vector3 ret);

		// Token: 0x06000059 RID: 89
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetVector4_Injected(IntPtr _unity_self, int nameID, out Vector4 ret);

		// Token: 0x0600005A RID: 90
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMatrix4x4_Injected(IntPtr _unity_self, int nameID, out Matrix4x4 ret);

		// Token: 0x0600005B RID: 91
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyValuesFrom_Injected(IntPtr _unity_self, IntPtr eventAttibute);

		// Token: 0x0400010C RID: 268
		private IntPtr m_Ptr;

		// Token: 0x0400010D RID: 269
		private bool m_Owner;

		// Token: 0x0400010E RID: 270
		private VisualEffectAsset m_VfxAsset;

		// Token: 0x02000012 RID: 18
		internal static class BindingsMarshaller
		{
			// Token: 0x0600005C RID: 92 RVA: 0x000028A6 File Offset: 0x00000AA6
			public static IntPtr ConvertToNative(VFXEventAttribute eventAttibute)
			{
				return eventAttibute.m_Ptr;
			}

			// Token: 0x0600005D RID: 93 RVA: 0x000028AE File Offset: 0x00000AAE
			public static VFXEventAttribute ConvertToManaged(IntPtr ptr)
			{
				return new VFXEventAttribute(ptr);
			}
		}
	}
}
