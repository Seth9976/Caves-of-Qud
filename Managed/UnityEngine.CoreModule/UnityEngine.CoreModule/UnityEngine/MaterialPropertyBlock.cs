using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x02000186 RID: 390
	[NativeHeader("Runtime/Math/SphericalHarmonicsL2.h")]
	[NativeHeader("Runtime/Shaders/ShaderPropertySheet.h")]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	[NativeHeader("Runtime/Shaders/ComputeShader.h")]
	public sealed class MaterialPropertyBlock
	{
		// Token: 0x06000F45 RID: 3909 RVA: 0x0001B18C File Offset: 0x0001938C
		[Obsolete("Use SetFloat instead (UnityUpgradable) -> SetFloat(*)", false)]
		public void AddFloat(string name, float value)
		{
			this.SetFloat(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x0001B19D File Offset: 0x0001939D
		[Obsolete("Use SetFloat instead (UnityUpgradable) -> SetFloat(*)", false)]
		public void AddFloat(int nameID, float value)
		{
			this.SetFloat(nameID, value);
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x0001B1A9 File Offset: 0x000193A9
		[Obsolete("Use SetVector instead (UnityUpgradable) -> SetVector(*)", false)]
		public void AddVector(string name, Vector4 value)
		{
			this.SetVector(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x0001B1BA File Offset: 0x000193BA
		[Obsolete("Use SetVector instead (UnityUpgradable) -> SetVector(*)", false)]
		public void AddVector(int nameID, Vector4 value)
		{
			this.SetVector(nameID, value);
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x0001B1C6 File Offset: 0x000193C6
		[Obsolete("Use SetColor instead (UnityUpgradable) -> SetColor(*)", false)]
		public void AddColor(string name, Color value)
		{
			this.SetColor(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x0001B1D7 File Offset: 0x000193D7
		[Obsolete("Use SetColor instead (UnityUpgradable) -> SetColor(*)", false)]
		public void AddColor(int nameID, Color value)
		{
			this.SetColor(nameID, value);
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x0001B1E3 File Offset: 0x000193E3
		[Obsolete("Use SetMatrix instead (UnityUpgradable) -> SetMatrix(*)", false)]
		public void AddMatrix(string name, Matrix4x4 value)
		{
			this.SetMatrix(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x0001B1F4 File Offset: 0x000193F4
		[Obsolete("Use SetMatrix instead (UnityUpgradable) -> SetMatrix(*)", false)]
		public void AddMatrix(int nameID, Matrix4x4 value)
		{
			this.SetMatrix(nameID, value);
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x0001B200 File Offset: 0x00019400
		[Obsolete("Use SetTexture instead (UnityUpgradable) -> SetTexture(*)", false)]
		public void AddTexture(string name, Texture value)
		{
			this.SetTexture(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0001B211 File Offset: 0x00019411
		[Obsolete("Use SetTexture instead (UnityUpgradable) -> SetTexture(*)", false)]
		public void AddTexture(int nameID, Texture value)
		{
			this.SetTexture(nameID, value);
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x0001B220 File Offset: 0x00019420
		[NativeName("GetIntFromScript")]
		[ThreadSafe]
		private int GetIntImpl(int name)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return MaterialPropertyBlock.GetIntImpl_Injected(intPtr, name);
		}

		// Token: 0x06000F50 RID: 3920 RVA: 0x0001B244 File Offset: 0x00019444
		[NativeName("GetFloatFromScript")]
		[ThreadSafe]
		private float GetFloatImpl(int name)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return MaterialPropertyBlock.GetFloatImpl_Injected(intPtr, name);
		}

		// Token: 0x06000F51 RID: 3921 RVA: 0x0001B268 File Offset: 0x00019468
		[NativeName("GetVectorFromScript")]
		[ThreadSafe]
		private Vector4 GetVectorImpl(int name)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector4 vector;
			MaterialPropertyBlock.GetVectorImpl_Injected(intPtr, name, out vector);
			return vector;
		}

		// Token: 0x06000F52 RID: 3922 RVA: 0x0001B290 File Offset: 0x00019490
		[NativeName("GetColorFromScript")]
		[ThreadSafe]
		private Color GetColorImpl(int name)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Color color;
			MaterialPropertyBlock.GetColorImpl_Injected(intPtr, name, out color);
			return color;
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0001B2B8 File Offset: 0x000194B8
		[ThreadSafe]
		[NativeName("GetMatrixFromScript")]
		private Matrix4x4 GetMatrixImpl(int name)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Matrix4x4 matrix4x;
			MaterialPropertyBlock.GetMatrixImpl_Injected(intPtr, name, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x0001B2E0 File Offset: 0x000194E0
		[NativeName("GetTextureFromScript")]
		[ThreadSafe]
		private Texture GetTextureImpl(int name)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Texture>(MaterialPropertyBlock.GetTextureImpl_Injected(intPtr, name));
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x0001B308 File Offset: 0x00019508
		[NativeName("HasPropertyFromScript")]
		private bool HasPropertyImpl(int name)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return MaterialPropertyBlock.HasPropertyImpl_Injected(intPtr, name);
		}

		// Token: 0x06000F56 RID: 3926 RVA: 0x0001B32C File Offset: 0x0001952C
		[NativeName("HasFloatFromScript")]
		private bool HasFloatImpl(int name)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return MaterialPropertyBlock.HasFloatImpl_Injected(intPtr, name);
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0001B350 File Offset: 0x00019550
		[NativeName("HasIntegerFromScript")]
		private bool HasIntImpl(int name)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return MaterialPropertyBlock.HasIntImpl_Injected(intPtr, name);
		}

		// Token: 0x06000F58 RID: 3928 RVA: 0x0001B374 File Offset: 0x00019574
		[NativeName("HasTextureFromScript")]
		private bool HasTextureImpl(int name)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return MaterialPropertyBlock.HasTextureImpl_Injected(intPtr, name);
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0001B398 File Offset: 0x00019598
		[NativeName("HasMatrixFromScript")]
		private bool HasMatrixImpl(int name)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return MaterialPropertyBlock.HasMatrixImpl_Injected(intPtr, name);
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0001B3BC File Offset: 0x000195BC
		[NativeName("HasVectorFromScript")]
		private bool HasVectorImpl(int name)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return MaterialPropertyBlock.HasVectorImpl_Injected(intPtr, name);
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0001B3E0 File Offset: 0x000195E0
		[NativeName("HasBufferFromScript")]
		private bool HasBufferImpl(int name)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return MaterialPropertyBlock.HasBufferImpl_Injected(intPtr, name);
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x0001B404 File Offset: 0x00019604
		[NativeName("HasConstantBufferFromScript")]
		private bool HasConstantBufferImpl(int name)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return MaterialPropertyBlock.HasConstantBufferImpl_Injected(intPtr, name);
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0001B428 File Offset: 0x00019628
		[ThreadSafe]
		[NativeName("SetIntFromScript")]
		private void SetIntImpl(int name, int value)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			MaterialPropertyBlock.SetIntImpl_Injected(intPtr, name, value);
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0001B44C File Offset: 0x0001964C
		[NativeName("SetFloatFromScript")]
		[ThreadSafe]
		private void SetFloatImpl(int name, float value)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			MaterialPropertyBlock.SetFloatImpl_Injected(intPtr, name, value);
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0001B470 File Offset: 0x00019670
		[ThreadSafe]
		[NativeName("SetVectorFromScript")]
		private void SetVectorImpl(int name, Vector4 value)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			MaterialPropertyBlock.SetVectorImpl_Injected(intPtr, name, ref value);
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0001B498 File Offset: 0x00019698
		[NativeName("SetColorFromScript")]
		[ThreadSafe]
		private void SetColorImpl(int name, Color value)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			MaterialPropertyBlock.SetColorImpl_Injected(intPtr, name, ref value);
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0001B4C0 File Offset: 0x000196C0
		[NativeName("SetMatrixFromScript")]
		[ThreadSafe]
		private void SetMatrixImpl(int name, Matrix4x4 value)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			MaterialPropertyBlock.SetMatrixImpl_Injected(intPtr, name, ref value);
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0001B4E8 File Offset: 0x000196E8
		[NativeName("SetTextureFromScript")]
		[ThreadSafe]
		private void SetTextureImpl(int name, [NotNull] Texture value)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(value, "value");
			}
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Texture>(value);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(value, "value");
			}
			MaterialPropertyBlock.SetTextureImpl_Injected(intPtr, name, intPtr2);
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0001B530 File Offset: 0x00019730
		[ThreadSafe]
		[NativeName("SetRenderTextureFromScript")]
		private void SetRenderTextureImpl(int name, [NotNull] RenderTexture value, RenderTextureSubElement element)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(value, "value");
			}
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(value);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(value, "value");
			}
			MaterialPropertyBlock.SetRenderTextureImpl_Injected(intPtr, name, intPtr2, element);
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0001B578 File Offset: 0x00019778
		[ThreadSafe]
		[NativeName("SetBufferFromScript")]
		private void SetBufferImpl(int name, ComputeBuffer value)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			MaterialPropertyBlock.SetBufferImpl_Injected(intPtr, name, (value == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(value));
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0001B5AC File Offset: 0x000197AC
		[ThreadSafe]
		[NativeName("SetBufferFromScript")]
		private void SetGraphicsBufferImpl(int name, GraphicsBuffer value)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			MaterialPropertyBlock.SetGraphicsBufferImpl_Injected(intPtr, name, (value == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(value));
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0001B5E0 File Offset: 0x000197E0
		[ThreadSafe]
		[NativeName("SetConstantBufferFromScript")]
		private void SetConstantBufferImpl(int name, ComputeBuffer value, int offset, int size)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			MaterialPropertyBlock.SetConstantBufferImpl_Injected(intPtr, name, (value == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(value), offset, size);
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0001B618 File Offset: 0x00019818
		[NativeName("SetConstantBufferFromScript")]
		[ThreadSafe]
		private void SetConstantGraphicsBufferImpl(int name, GraphicsBuffer value, int offset, int size)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			MaterialPropertyBlock.SetConstantGraphicsBufferImpl_Injected(intPtr, name, (value == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(value), offset, size);
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0001B650 File Offset: 0x00019850
		[NativeName("SetFloatArrayFromScript")]
		[ThreadSafe]
		private unsafe void SetFloatArrayImpl(int name, float[] values, int count)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<float> span = new Span<float>(values);
			fixed (float* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				MaterialPropertyBlock.SetFloatArrayImpl_Injected(intPtr, name, ref managedSpanWrapper, count);
			}
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0001B69C File Offset: 0x0001989C
		[NativeName("SetVectorArrayFromScript")]
		[ThreadSafe]
		private unsafe void SetVectorArrayImpl(int name, Vector4[] values, int count)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Vector4> span = new Span<Vector4>(values);
			fixed (Vector4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				MaterialPropertyBlock.SetVectorArrayImpl_Injected(intPtr, name, ref managedSpanWrapper, count);
			}
		}

		// Token: 0x06000F6A RID: 3946 RVA: 0x0001B6E8 File Offset: 0x000198E8
		[NativeName("SetMatrixArrayFromScript")]
		[ThreadSafe]
		private unsafe void SetMatrixArrayImpl(int name, Matrix4x4[] values, int count)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Matrix4x4> span = new Span<Matrix4x4>(values);
			fixed (Matrix4x4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				MaterialPropertyBlock.SetMatrixArrayImpl_Injected(intPtr, name, ref managedSpanWrapper, count);
			}
		}

		// Token: 0x06000F6B RID: 3947 RVA: 0x0001B734 File Offset: 0x00019934
		[NativeName("GetFloatArrayFromScript")]
		[ThreadSafe]
		private float[] GetFloatArrayImpl(int name)
		{
			float[] array2;
			try
			{
				IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				MaterialPropertyBlock.GetFloatArrayImpl_Injected(intPtr, name, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				float[] array;
				blittableArrayWrapper.Unmarshal<float>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000F6C RID: 3948 RVA: 0x0001B778 File Offset: 0x00019978
		[ThreadSafe]
		[NativeName("GetVectorArrayFromScript")]
		private Vector4[] GetVectorArrayImpl(int name)
		{
			Vector4[] array2;
			try
			{
				IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				MaterialPropertyBlock.GetVectorArrayImpl_Injected(intPtr, name, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Vector4[] array;
				blittableArrayWrapper.Unmarshal<Vector4>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000F6D RID: 3949 RVA: 0x0001B7BC File Offset: 0x000199BC
		[ThreadSafe]
		[NativeName("GetMatrixArrayFromScript")]
		private Matrix4x4[] GetMatrixArrayImpl(int name)
		{
			Matrix4x4[] array2;
			try
			{
				IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				MaterialPropertyBlock.GetMatrixArrayImpl_Injected(intPtr, name, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Matrix4x4[] array;
				blittableArrayWrapper.Unmarshal<Matrix4x4>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06000F6E RID: 3950 RVA: 0x0001B800 File Offset: 0x00019A00
		[NativeName("GetFloatArrayCountFromScript")]
		[ThreadSafe]
		private int GetFloatArrayCountImpl(int name)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return MaterialPropertyBlock.GetFloatArrayCountImpl_Injected(intPtr, name);
		}

		// Token: 0x06000F6F RID: 3951 RVA: 0x0001B824 File Offset: 0x00019A24
		[NativeName("GetVectorArrayCountFromScript")]
		[ThreadSafe]
		private int GetVectorArrayCountImpl(int name)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return MaterialPropertyBlock.GetVectorArrayCountImpl_Injected(intPtr, name);
		}

		// Token: 0x06000F70 RID: 3952 RVA: 0x0001B848 File Offset: 0x00019A48
		[NativeName("GetMatrixArrayCountFromScript")]
		[ThreadSafe]
		private int GetMatrixArrayCountImpl(int name)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return MaterialPropertyBlock.GetMatrixArrayCountImpl_Injected(intPtr, name);
		}

		// Token: 0x06000F71 RID: 3953 RVA: 0x0001B86C File Offset: 0x00019A6C
		[ThreadSafe]
		[NativeName("ExtractFloatArrayFromScript")]
		private unsafe void ExtractFloatArrayImpl(int name, [Out] float[] val)
		{
			try
			{
				IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				if (val != null)
				{
					fixed (float[] array = val)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				MaterialPropertyBlock.ExtractFloatArrayImpl_Injected(intPtr, name, out blittableArrayWrapper);
			}
			finally
			{
				float[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<float>(ref array);
			}
		}

		// Token: 0x06000F72 RID: 3954 RVA: 0x0001B8CC File Offset: 0x00019ACC
		[ThreadSafe]
		[NativeName("ExtractVectorArrayFromScript")]
		private unsafe void ExtractVectorArrayImpl(int name, [Out] Vector4[] val)
		{
			try
			{
				IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				if (val != null)
				{
					fixed (Vector4[] array = val)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				MaterialPropertyBlock.ExtractVectorArrayImpl_Injected(intPtr, name, out blittableArrayWrapper);
			}
			finally
			{
				Vector4[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<Vector4>(ref array);
			}
		}

		// Token: 0x06000F73 RID: 3955 RVA: 0x0001B92C File Offset: 0x00019B2C
		[NativeName("ExtractMatrixArrayFromScript")]
		[ThreadSafe]
		private unsafe void ExtractMatrixArrayImpl(int name, [Out] Matrix4x4[] val)
		{
			try
			{
				IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				if (val != null)
				{
					fixed (Matrix4x4[] array = val)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				MaterialPropertyBlock.ExtractMatrixArrayImpl_Injected(intPtr, name, out blittableArrayWrapper);
			}
			finally
			{
				Matrix4x4[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<Matrix4x4>(ref array);
			}
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x0001B98C File Offset: 0x00019B8C
		[ThreadSafe]
		[FreeFunction("ConvertAndCopySHCoefficientArraysToPropertySheetFromScript")]
		internal unsafe static void Internal_CopySHCoefficientArraysFrom(MaterialPropertyBlock properties, SphericalHarmonicsL2[] lightProbes, int sourceStart, int destStart, int count)
		{
			IntPtr intPtr = ((properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties));
			Span<SphericalHarmonicsL2> span = new Span<SphericalHarmonicsL2>(lightProbes);
			fixed (SphericalHarmonicsL2* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				MaterialPropertyBlock.Internal_CopySHCoefficientArraysFrom_Injected(intPtr, ref managedSpanWrapper, sourceStart, destStart, count);
			}
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0001B9D8 File Offset: 0x00019BD8
		[FreeFunction("CopyProbeOcclusionArrayToPropertySheetFromScript")]
		[ThreadSafe]
		internal unsafe static void Internal_CopyProbeOcclusionArrayFrom(MaterialPropertyBlock properties, Vector4[] occlusionProbes, int sourceStart, int destStart, int count)
		{
			IntPtr intPtr = ((properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties));
			Span<Vector4> span = new Span<Vector4>(occlusionProbes);
			fixed (Vector4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				MaterialPropertyBlock.Internal_CopyProbeOcclusionArrayFrom_Injected(intPtr, ref managedSpanWrapper, sourceStart, destStart, count);
			}
		}

		// Token: 0x06000F76 RID: 3958
		[NativeMethod(Name = "MaterialPropertyBlockScripting::Create", IsFreeFunction = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr CreateImpl();

		// Token: 0x06000F77 RID: 3959
		[NativeMethod(Name = "MaterialPropertyBlockScripting::Destroy", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DestroyImpl(IntPtr mpb);

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x0001BA24 File Offset: 0x00019C24
		public bool isEmpty
		{
			[ThreadSafe]
			[NativeName("IsEmpty")]
			get
			{
				IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return MaterialPropertyBlock.get_isEmpty_Injected(intPtr);
			}
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x0001BA48 File Offset: 0x00019C48
		[ThreadSafe]
		private void Clear(bool keepMemory)
		{
			IntPtr intPtr = MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			MaterialPropertyBlock.Clear_Injected(intPtr, keepMemory);
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0001BA6B File Offset: 0x00019C6B
		public void Clear()
		{
			this.Clear(true);
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0001BA78 File Offset: 0x00019C78
		private void SetFloatArray(int name, float[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			this.SetFloatArrayImpl(name, values, count);
		}

		// Token: 0x06000F7C RID: 3964 RVA: 0x0001BACC File Offset: 0x00019CCC
		private void SetVectorArray(int name, Vector4[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			this.SetVectorArrayImpl(name, values, count);
		}

		// Token: 0x06000F7D RID: 3965 RVA: 0x0001BB20 File Offset: 0x00019D20
		private void SetMatrixArray(int name, Matrix4x4[] values, int count)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			bool flag2 = values.Length == 0;
			if (flag2)
			{
				throw new ArgumentException("Zero-sized array is not allowed.");
			}
			bool flag3 = values.Length < count;
			if (flag3)
			{
				throw new ArgumentException("array has less elements than passed count.");
			}
			this.SetMatrixArrayImpl(name, values, count);
		}

		// Token: 0x06000F7E RID: 3966 RVA: 0x0001BB74 File Offset: 0x00019D74
		private void ExtractFloatArray(int name, List<float> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int floatArrayCountImpl = this.GetFloatArrayCountImpl(name);
			bool flag2 = floatArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<float>(values, floatArrayCountImpl);
				this.ExtractFloatArrayImpl(name, NoAllocHelpers.ExtractArrayFromList<float>(values));
			}
		}

		// Token: 0x06000F7F RID: 3967 RVA: 0x0001BBC4 File Offset: 0x00019DC4
		private void ExtractVectorArray(int name, List<Vector4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int vectorArrayCountImpl = this.GetVectorArrayCountImpl(name);
			bool flag2 = vectorArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Vector4>(values, vectorArrayCountImpl);
				this.ExtractVectorArrayImpl(name, NoAllocHelpers.ExtractArrayFromList<Vector4>(values));
			}
		}

		// Token: 0x06000F80 RID: 3968 RVA: 0x0001BC14 File Offset: 0x00019E14
		private void ExtractMatrixArray(int name, List<Matrix4x4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int matrixArrayCountImpl = this.GetMatrixArrayCountImpl(name);
			bool flag2 = matrixArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Matrix4x4>(values, matrixArrayCountImpl);
				this.ExtractMatrixArrayImpl(name, NoAllocHelpers.ExtractArrayFromList<Matrix4x4>(values));
			}
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x0001BC64 File Offset: 0x00019E64
		public MaterialPropertyBlock()
		{
			this.m_Ptr = MaterialPropertyBlock.CreateImpl();
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x0001BC7C File Offset: 0x00019E7C
		~MaterialPropertyBlock()
		{
			this.Dispose();
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0001BCAC File Offset: 0x00019EAC
		private void Dispose()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				MaterialPropertyBlock.DestroyImpl(this.m_Ptr);
				this.m_Ptr = IntPtr.Zero;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0001BCEE File Offset: 0x00019EEE
		public void SetInt(string name, int value)
		{
			this.SetFloatImpl(Shader.PropertyToID(name), (float)value);
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0001BD00 File Offset: 0x00019F00
		public void SetInt(int nameID, int value)
		{
			this.SetFloatImpl(nameID, (float)value);
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x0001BD0D File Offset: 0x00019F0D
		public void SetFloat(string name, float value)
		{
			this.SetFloatImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0001BD1E File Offset: 0x00019F1E
		public void SetFloat(int nameID, float value)
		{
			this.SetFloatImpl(nameID, value);
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0001BD2A File Offset: 0x00019F2A
		public void SetInteger(string name, int value)
		{
			this.SetIntImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0001BD3B File Offset: 0x00019F3B
		public void SetInteger(int nameID, int value)
		{
			this.SetIntImpl(nameID, value);
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0001BD47 File Offset: 0x00019F47
		public void SetVector(string name, Vector4 value)
		{
			this.SetVectorImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0001BD58 File Offset: 0x00019F58
		public void SetVector(int nameID, Vector4 value)
		{
			this.SetVectorImpl(nameID, value);
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0001BD64 File Offset: 0x00019F64
		public void SetColor(string name, Color value)
		{
			this.SetColorImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0001BD75 File Offset: 0x00019F75
		public void SetColor(int nameID, Color value)
		{
			this.SetColorImpl(nameID, value);
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x0001BD81 File Offset: 0x00019F81
		public void SetMatrix(string name, Matrix4x4 value)
		{
			this.SetMatrixImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x0001BD92 File Offset: 0x00019F92
		public void SetMatrix(int nameID, Matrix4x4 value)
		{
			this.SetMatrixImpl(nameID, value);
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0001BD9E File Offset: 0x00019F9E
		public void SetBuffer(string name, ComputeBuffer value)
		{
			this.SetBufferImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x0001BDAF File Offset: 0x00019FAF
		public void SetBuffer(int nameID, ComputeBuffer value)
		{
			this.SetBufferImpl(nameID, value);
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x0001BDBB File Offset: 0x00019FBB
		public void SetBuffer(string name, GraphicsBuffer value)
		{
			this.SetGraphicsBufferImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x0001BDCC File Offset: 0x00019FCC
		public void SetBuffer(int nameID, GraphicsBuffer value)
		{
			this.SetGraphicsBufferImpl(nameID, value);
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x0001BDD8 File Offset: 0x00019FD8
		public void SetTexture(string name, Texture value)
		{
			this.SetTextureImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x0001BDE9 File Offset: 0x00019FE9
		public void SetTexture(int nameID, Texture value)
		{
			this.SetTextureImpl(nameID, value);
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x0001BDF5 File Offset: 0x00019FF5
		public void SetTexture(string name, RenderTexture value, RenderTextureSubElement element)
		{
			this.SetRenderTextureImpl(Shader.PropertyToID(name), value, element);
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x0001BE07 File Offset: 0x0001A007
		public void SetTexture(int nameID, RenderTexture value, RenderTextureSubElement element)
		{
			this.SetRenderTextureImpl(nameID, value, element);
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0001BE14 File Offset: 0x0001A014
		public void SetConstantBuffer(string name, ComputeBuffer value, int offset, int size)
		{
			this.SetConstantBufferImpl(Shader.PropertyToID(name), value, offset, size);
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x0001BE28 File Offset: 0x0001A028
		public void SetConstantBuffer(int nameID, ComputeBuffer value, int offset, int size)
		{
			this.SetConstantBufferImpl(nameID, value, offset, size);
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x0001BE37 File Offset: 0x0001A037
		public void SetConstantBuffer(string name, GraphicsBuffer value, int offset, int size)
		{
			this.SetConstantGraphicsBufferImpl(Shader.PropertyToID(name), value, offset, size);
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x0001BE4B File Offset: 0x0001A04B
		public void SetConstantBuffer(int nameID, GraphicsBuffer value, int offset, int size)
		{
			this.SetConstantGraphicsBufferImpl(nameID, value, offset, size);
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x0001BE5A File Offset: 0x0001A05A
		public void SetFloatArray(string name, List<float> values)
		{
			this.SetFloatArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromList<float>(values), values.Count);
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x0001BE76 File Offset: 0x0001A076
		public void SetFloatArray(int nameID, List<float> values)
		{
			this.SetFloatArray(nameID, NoAllocHelpers.ExtractArrayFromList<float>(values), values.Count);
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x0001BE8D File Offset: 0x0001A08D
		public void SetFloatArray(string name, float[] values)
		{
			this.SetFloatArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x0001BEA1 File Offset: 0x0001A0A1
		public void SetFloatArray(int nameID, float[] values)
		{
			this.SetFloatArray(nameID, values, values.Length);
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x0001BEB0 File Offset: 0x0001A0B0
		public void SetVectorArray(string name, List<Vector4> values)
		{
			this.SetVectorArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromList<Vector4>(values), values.Count);
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0001BECC File Offset: 0x0001A0CC
		public void SetVectorArray(int nameID, List<Vector4> values)
		{
			this.SetVectorArray(nameID, NoAllocHelpers.ExtractArrayFromList<Vector4>(values), values.Count);
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0001BEE3 File Offset: 0x0001A0E3
		public void SetVectorArray(string name, Vector4[] values)
		{
			this.SetVectorArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0001BEF7 File Offset: 0x0001A0F7
		public void SetVectorArray(int nameID, Vector4[] values)
		{
			this.SetVectorArray(nameID, values, values.Length);
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x0001BF06 File Offset: 0x0001A106
		public void SetMatrixArray(string name, List<Matrix4x4> values)
		{
			this.SetMatrixArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromList<Matrix4x4>(values), values.Count);
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x0001BF22 File Offset: 0x0001A122
		public void SetMatrixArray(int nameID, List<Matrix4x4> values)
		{
			this.SetMatrixArray(nameID, NoAllocHelpers.ExtractArrayFromList<Matrix4x4>(values), values.Count);
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0001BF39 File Offset: 0x0001A139
		public void SetMatrixArray(string name, Matrix4x4[] values)
		{
			this.SetMatrixArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0001BF4D File Offset: 0x0001A14D
		public void SetMatrixArray(int nameID, Matrix4x4[] values)
		{
			this.SetMatrixArray(nameID, values, values.Length);
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0001BF5C File Offset: 0x0001A15C
		public bool HasProperty(string name)
		{
			return this.HasPropertyImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0001BF7C File Offset: 0x0001A17C
		public bool HasProperty(int nameID)
		{
			return this.HasPropertyImpl(nameID);
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0001BF98 File Offset: 0x0001A198
		public bool HasInt(string name)
		{
			return this.HasFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0001BFB8 File Offset: 0x0001A1B8
		public bool HasInt(int nameID)
		{
			return this.HasFloatImpl(nameID);
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0001BFD4 File Offset: 0x0001A1D4
		public bool HasFloat(string name)
		{
			return this.HasFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0001BFF4 File Offset: 0x0001A1F4
		public bool HasFloat(int nameID)
		{
			return this.HasFloatImpl(nameID);
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0001C010 File Offset: 0x0001A210
		public bool HasInteger(string name)
		{
			return this.HasIntImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x0001C030 File Offset: 0x0001A230
		public bool HasInteger(int nameID)
		{
			return this.HasIntImpl(nameID);
		}

		// Token: 0x06000FB0 RID: 4016 RVA: 0x0001C04C File Offset: 0x0001A24C
		public bool HasTexture(string name)
		{
			return this.HasTextureImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0001C06C File Offset: 0x0001A26C
		public bool HasTexture(int nameID)
		{
			return this.HasTextureImpl(nameID);
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0001C088 File Offset: 0x0001A288
		public bool HasMatrix(string name)
		{
			return this.HasMatrixImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0001C0A8 File Offset: 0x0001A2A8
		public bool HasMatrix(int nameID)
		{
			return this.HasMatrixImpl(nameID);
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0001C0C4 File Offset: 0x0001A2C4
		public bool HasVector(string name)
		{
			return this.HasVectorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0001C0E4 File Offset: 0x0001A2E4
		public bool HasVector(int nameID)
		{
			return this.HasVectorImpl(nameID);
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0001C100 File Offset: 0x0001A300
		public bool HasColor(string name)
		{
			return this.HasVectorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x0001C120 File Offset: 0x0001A320
		public bool HasColor(int nameID)
		{
			return this.HasVectorImpl(nameID);
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x0001C13C File Offset: 0x0001A33C
		public bool HasBuffer(string name)
		{
			return this.HasBufferImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x0001C15C File Offset: 0x0001A35C
		public bool HasBuffer(int nameID)
		{
			return this.HasBufferImpl(nameID);
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x0001C178 File Offset: 0x0001A378
		public bool HasConstantBuffer(string name)
		{
			return this.HasConstantBufferImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x0001C198 File Offset: 0x0001A398
		public bool HasConstantBuffer(int nameID)
		{
			return this.HasConstantBufferImpl(nameID);
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x0001C1B4 File Offset: 0x0001A3B4
		public float GetFloat(string name)
		{
			return this.GetFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x0001C1D4 File Offset: 0x0001A3D4
		public float GetFloat(int nameID)
		{
			return this.GetFloatImpl(nameID);
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x0001C1F0 File Offset: 0x0001A3F0
		public int GetInt(string name)
		{
			return (int)this.GetFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x0001C210 File Offset: 0x0001A410
		public int GetInt(int nameID)
		{
			return (int)this.GetFloatImpl(nameID);
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x0001C22C File Offset: 0x0001A42C
		public int GetInteger(string name)
		{
			return this.GetIntImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0001C24C File Offset: 0x0001A44C
		public int GetInteger(int nameID)
		{
			return this.GetIntImpl(nameID);
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x0001C268 File Offset: 0x0001A468
		public Vector4 GetVector(string name)
		{
			return this.GetVectorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0001C288 File Offset: 0x0001A488
		public Vector4 GetVector(int nameID)
		{
			return this.GetVectorImpl(nameID);
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0001C2A4 File Offset: 0x0001A4A4
		public Color GetColor(string name)
		{
			return this.GetColorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x0001C2C4 File Offset: 0x0001A4C4
		public Color GetColor(int nameID)
		{
			return this.GetColorImpl(nameID);
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0001C2E0 File Offset: 0x0001A4E0
		public Matrix4x4 GetMatrix(string name)
		{
			return this.GetMatrixImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0001C300 File Offset: 0x0001A500
		public Matrix4x4 GetMatrix(int nameID)
		{
			return this.GetMatrixImpl(nameID);
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0001C31C File Offset: 0x0001A51C
		public Texture GetTexture(string name)
		{
			return this.GetTextureImpl(Shader.PropertyToID(name));
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x0001C33C File Offset: 0x0001A53C
		public Texture GetTexture(int nameID)
		{
			return this.GetTextureImpl(nameID);
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0001C358 File Offset: 0x0001A558
		public float[] GetFloatArray(string name)
		{
			return this.GetFloatArray(Shader.PropertyToID(name));
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x0001C378 File Offset: 0x0001A578
		public float[] GetFloatArray(int nameID)
		{
			return (this.GetFloatArrayCountImpl(nameID) != 0) ? this.GetFloatArrayImpl(nameID) : null;
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x0001C3A0 File Offset: 0x0001A5A0
		public Vector4[] GetVectorArray(string name)
		{
			return this.GetVectorArray(Shader.PropertyToID(name));
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x0001C3C0 File Offset: 0x0001A5C0
		public Vector4[] GetVectorArray(int nameID)
		{
			return (this.GetVectorArrayCountImpl(nameID) != 0) ? this.GetVectorArrayImpl(nameID) : null;
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x0001C3E8 File Offset: 0x0001A5E8
		public Matrix4x4[] GetMatrixArray(string name)
		{
			return this.GetMatrixArray(Shader.PropertyToID(name));
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0001C408 File Offset: 0x0001A608
		public Matrix4x4[] GetMatrixArray(int nameID)
		{
			return (this.GetMatrixArrayCountImpl(nameID) != 0) ? this.GetMatrixArrayImpl(nameID) : null;
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x0001C42D File Offset: 0x0001A62D
		public void GetFloatArray(string name, List<float> values)
		{
			this.ExtractFloatArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x0001C43E File Offset: 0x0001A63E
		public void GetFloatArray(int nameID, List<float> values)
		{
			this.ExtractFloatArray(nameID, values);
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x0001C44A File Offset: 0x0001A64A
		public void GetVectorArray(string name, List<Vector4> values)
		{
			this.ExtractVectorArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x0001C45B File Offset: 0x0001A65B
		public void GetVectorArray(int nameID, List<Vector4> values)
		{
			this.ExtractVectorArray(nameID, values);
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x0001C467 File Offset: 0x0001A667
		public void GetMatrixArray(string name, List<Matrix4x4> values)
		{
			this.ExtractMatrixArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x0001C478 File Offset: 0x0001A678
		public void GetMatrixArray(int nameID, List<Matrix4x4> values)
		{
			this.ExtractMatrixArray(nameID, values);
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x0001C484 File Offset: 0x0001A684
		public void CopySHCoefficientArraysFrom(List<SphericalHarmonicsL2> lightProbes)
		{
			bool flag = lightProbes == null;
			if (flag)
			{
				throw new ArgumentNullException("lightProbes");
			}
			this.CopySHCoefficientArraysFrom(NoAllocHelpers.ExtractArrayFromList<SphericalHarmonicsL2>(lightProbes), 0, 0, lightProbes.Count);
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x0001C4BC File Offset: 0x0001A6BC
		public void CopySHCoefficientArraysFrom(SphericalHarmonicsL2[] lightProbes)
		{
			bool flag = lightProbes == null;
			if (flag)
			{
				throw new ArgumentNullException("lightProbes");
			}
			this.CopySHCoefficientArraysFrom(lightProbes, 0, 0, lightProbes.Length);
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x0001C4EA File Offset: 0x0001A6EA
		public void CopySHCoefficientArraysFrom(List<SphericalHarmonicsL2> lightProbes, int sourceStart, int destStart, int count)
		{
			this.CopySHCoefficientArraysFrom(NoAllocHelpers.ExtractArrayFromList<SphericalHarmonicsL2>(lightProbes), sourceStart, destStart, count);
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0001C500 File Offset: 0x0001A700
		public void CopySHCoefficientArraysFrom(SphericalHarmonicsL2[] lightProbes, int sourceStart, int destStart, int count)
		{
			bool flag = lightProbes == null;
			if (flag)
			{
				throw new ArgumentNullException("lightProbes");
			}
			bool flag2 = sourceStart < 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("sourceStart", "Argument sourceStart must not be negative.");
			}
			bool flag3 = destStart < 0;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("sourceStart", "Argument destStart must not be negative.");
			}
			bool flag4 = count < 0;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("count", "Argument count must not be negative.");
			}
			bool flag5 = lightProbes.Length < sourceStart + count;
			if (flag5)
			{
				throw new ArgumentOutOfRangeException("The specified source start index or count is out of the range.");
			}
			MaterialPropertyBlock.Internal_CopySHCoefficientArraysFrom(this, lightProbes, sourceStart, destStart, count);
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0001C590 File Offset: 0x0001A790
		public void CopyProbeOcclusionArrayFrom(List<Vector4> occlusionProbes)
		{
			bool flag = occlusionProbes == null;
			if (flag)
			{
				throw new ArgumentNullException("occlusionProbes");
			}
			this.CopyProbeOcclusionArrayFrom(NoAllocHelpers.ExtractArrayFromList<Vector4>(occlusionProbes), 0, 0, occlusionProbes.Count);
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0001C5C8 File Offset: 0x0001A7C8
		public void CopyProbeOcclusionArrayFrom(Vector4[] occlusionProbes)
		{
			bool flag = occlusionProbes == null;
			if (flag)
			{
				throw new ArgumentNullException("occlusionProbes");
			}
			this.CopyProbeOcclusionArrayFrom(occlusionProbes, 0, 0, occlusionProbes.Length);
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0001C5F6 File Offset: 0x0001A7F6
		public void CopyProbeOcclusionArrayFrom(List<Vector4> occlusionProbes, int sourceStart, int destStart, int count)
		{
			this.CopyProbeOcclusionArrayFrom(NoAllocHelpers.ExtractArrayFromList<Vector4>(occlusionProbes), sourceStart, destStart, count);
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x0001C60C File Offset: 0x0001A80C
		public void CopyProbeOcclusionArrayFrom(Vector4[] occlusionProbes, int sourceStart, int destStart, int count)
		{
			bool flag = occlusionProbes == null;
			if (flag)
			{
				throw new ArgumentNullException("occlusionProbes");
			}
			bool flag2 = sourceStart < 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("sourceStart", "Argument sourceStart must not be negative.");
			}
			bool flag3 = destStart < 0;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("sourceStart", "Argument destStart must not be negative.");
			}
			bool flag4 = count < 0;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("count", "Argument count must not be negative.");
			}
			bool flag5 = occlusionProbes.Length < sourceStart + count;
			if (flag5)
			{
				throw new ArgumentOutOfRangeException("The specified source start index or count is out of the range.");
			}
			MaterialPropertyBlock.Internal_CopyProbeOcclusionArrayFrom(this, occlusionProbes, sourceStart, destStart, count);
		}

		// Token: 0x06000FDE RID: 4062
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetIntImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x06000FDF RID: 4063
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetFloatImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x06000FE0 RID: 4064
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetVectorImpl_Injected(IntPtr _unity_self, int name, out Vector4 ret);

		// Token: 0x06000FE1 RID: 4065
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetColorImpl_Injected(IntPtr _unity_self, int name, out Color ret);

		// Token: 0x06000FE2 RID: 4066
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMatrixImpl_Injected(IntPtr _unity_self, int name, out Matrix4x4 ret);

		// Token: 0x06000FE3 RID: 4067
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetTextureImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x06000FE4 RID: 4068
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasPropertyImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x06000FE5 RID: 4069
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasFloatImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x06000FE6 RID: 4070
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasIntImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x06000FE7 RID: 4071
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasTextureImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x06000FE8 RID: 4072
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasMatrixImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x06000FE9 RID: 4073
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasVectorImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x06000FEA RID: 4074
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasBufferImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x06000FEB RID: 4075
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasConstantBufferImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x06000FEC RID: 4076
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetIntImpl_Injected(IntPtr _unity_self, int name, int value);

		// Token: 0x06000FED RID: 4077
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFloatImpl_Injected(IntPtr _unity_self, int name, float value);

		// Token: 0x06000FEE RID: 4078
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVectorImpl_Injected(IntPtr _unity_self, int name, [In] ref Vector4 value);

		// Token: 0x06000FEF RID: 4079
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetColorImpl_Injected(IntPtr _unity_self, int name, [In] ref Color value);

		// Token: 0x06000FF0 RID: 4080
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMatrixImpl_Injected(IntPtr _unity_self, int name, [In] ref Matrix4x4 value);

		// Token: 0x06000FF1 RID: 4081
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTextureImpl_Injected(IntPtr _unity_self, int name, IntPtr value);

		// Token: 0x06000FF2 RID: 4082
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRenderTextureImpl_Injected(IntPtr _unity_self, int name, IntPtr value, RenderTextureSubElement element);

		// Token: 0x06000FF3 RID: 4083
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBufferImpl_Injected(IntPtr _unity_self, int name, IntPtr value);

		// Token: 0x06000FF4 RID: 4084
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGraphicsBufferImpl_Injected(IntPtr _unity_self, int name, IntPtr value);

		// Token: 0x06000FF5 RID: 4085
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetConstantBufferImpl_Injected(IntPtr _unity_self, int name, IntPtr value, int offset, int size);

		// Token: 0x06000FF6 RID: 4086
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetConstantGraphicsBufferImpl_Injected(IntPtr _unity_self, int name, IntPtr value, int offset, int size);

		// Token: 0x06000FF7 RID: 4087
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFloatArrayImpl_Injected(IntPtr _unity_self, int name, ref ManagedSpanWrapper values, int count);

		// Token: 0x06000FF8 RID: 4088
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVectorArrayImpl_Injected(IntPtr _unity_self, int name, ref ManagedSpanWrapper values, int count);

		// Token: 0x06000FF9 RID: 4089
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMatrixArrayImpl_Injected(IntPtr _unity_self, int name, ref ManagedSpanWrapper values, int count);

		// Token: 0x06000FFA RID: 4090
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetFloatArrayImpl_Injected(IntPtr _unity_self, int name, out BlittableArrayWrapper ret);

		// Token: 0x06000FFB RID: 4091
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetVectorArrayImpl_Injected(IntPtr _unity_self, int name, out BlittableArrayWrapper ret);

		// Token: 0x06000FFC RID: 4092
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMatrixArrayImpl_Injected(IntPtr _unity_self, int name, out BlittableArrayWrapper ret);

		// Token: 0x06000FFD RID: 4093
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetFloatArrayCountImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x06000FFE RID: 4094
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetVectorArrayCountImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x06000FFF RID: 4095
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetMatrixArrayCountImpl_Injected(IntPtr _unity_self, int name);

		// Token: 0x06001000 RID: 4096
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExtractFloatArrayImpl_Injected(IntPtr _unity_self, int name, out BlittableArrayWrapper val);

		// Token: 0x06001001 RID: 4097
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExtractVectorArrayImpl_Injected(IntPtr _unity_self, int name, out BlittableArrayWrapper val);

		// Token: 0x06001002 RID: 4098
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExtractMatrixArrayImpl_Injected(IntPtr _unity_self, int name, out BlittableArrayWrapper val);

		// Token: 0x06001003 RID: 4099
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CopySHCoefficientArraysFrom_Injected(IntPtr properties, ref ManagedSpanWrapper lightProbes, int sourceStart, int destStart, int count);

		// Token: 0x06001004 RID: 4100
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CopyProbeOcclusionArrayFrom_Injected(IntPtr properties, ref ManagedSpanWrapper occlusionProbes, int sourceStart, int destStart, int count);

		// Token: 0x06001005 RID: 4101
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isEmpty_Injected(IntPtr _unity_self);

		// Token: 0x06001006 RID: 4102
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Clear_Injected(IntPtr _unity_self, bool keepMemory);

		// Token: 0x04000500 RID: 1280
		internal IntPtr m_Ptr;

		// Token: 0x02000187 RID: 391
		internal static class BindingsMarshaller
		{
			// Token: 0x06001007 RID: 4103 RVA: 0x0001C69C File Offset: 0x0001A89C
			public static IntPtr ConvertToNative(MaterialPropertyBlock materialPropertyBlock)
			{
				return materialPropertyBlock.m_Ptr;
			}
		}
	}
}
