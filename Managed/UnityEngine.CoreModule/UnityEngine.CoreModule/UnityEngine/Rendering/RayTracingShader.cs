using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x0200056D RID: 1389
	[RayTracingShaderHelpURL]
	public sealed class RayTracingShader : Object
	{
		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x060035F7 RID: 13815 RVA: 0x00069DEC File Offset: 0x00067FEC
		public float maxRecursionDepth
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return RayTracingShader.get_maxRecursionDepth_Injected(intPtr);
			}
		}

		// Token: 0x060035F8 RID: 13816 RVA: 0x00069E10 File Offset: 0x00068010
		[FreeFunction(Name = "RayTracingShaderScripting::SetFloat", HasExplicitThis = true)]
		public void SetFloat(int nameID, float val)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RayTracingShader.SetFloat_Injected(intPtr, nameID, val);
		}

		// Token: 0x060035F9 RID: 13817 RVA: 0x00069E34 File Offset: 0x00068034
		[FreeFunction(Name = "RayTracingShaderScripting::SetInt", HasExplicitThis = true)]
		public void SetInt(int nameID, int val)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RayTracingShader.SetInt_Injected(intPtr, nameID, val);
		}

		// Token: 0x060035FA RID: 13818 RVA: 0x00069E58 File Offset: 0x00068058
		[FreeFunction(Name = "RayTracingShaderScripting::SetVector", HasExplicitThis = true)]
		public void SetVector(int nameID, Vector4 val)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RayTracingShader.SetVector_Injected(intPtr, nameID, ref val);
		}

		// Token: 0x060035FB RID: 13819 RVA: 0x00069E80 File Offset: 0x00068080
		[FreeFunction(Name = "RayTracingShaderScripting::SetMatrix", HasExplicitThis = true)]
		public void SetMatrix(int nameID, Matrix4x4 val)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RayTracingShader.SetMatrix_Injected(intPtr, nameID, ref val);
		}

		// Token: 0x060035FC RID: 13820 RVA: 0x00069EA8 File Offset: 0x000680A8
		[FreeFunction(Name = "RayTracingShaderScripting::SetFloatArray", HasExplicitThis = true)]
		private unsafe void SetFloatArray(int nameID, float[] values)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<float> span = new Span<float>(values);
			fixed (float* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				RayTracingShader.SetFloatArray_Injected(intPtr, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x060035FD RID: 13821 RVA: 0x00069EF4 File Offset: 0x000680F4
		[FreeFunction(Name = "RayTracingShaderScripting::SetIntArray", HasExplicitThis = true)]
		private unsafe void SetIntArray(int nameID, int[] values)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<int> span = new Span<int>(values);
			fixed (int* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				RayTracingShader.SetIntArray_Injected(intPtr, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x060035FE RID: 13822 RVA: 0x00069F40 File Offset: 0x00068140
		[FreeFunction(Name = "RayTracingShaderScripting::SetVectorArray", HasExplicitThis = true)]
		public unsafe void SetVectorArray(int nameID, Vector4[] values)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Vector4> span = new Span<Vector4>(values);
			fixed (Vector4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				RayTracingShader.SetVectorArray_Injected(intPtr, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x060035FF RID: 13823 RVA: 0x00069F8C File Offset: 0x0006818C
		[FreeFunction(Name = "RayTracingShaderScripting::SetMatrixArray", HasExplicitThis = true)]
		public unsafe void SetMatrixArray(int nameID, Matrix4x4[] values)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Matrix4x4> span = new Span<Matrix4x4>(values);
			fixed (Matrix4x4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				RayTracingShader.SetMatrixArray_Injected(intPtr, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x06003600 RID: 13824 RVA: 0x00069FD8 File Offset: 0x000681D8
		[NativeMethod(Name = "RayTracingShaderScripting::SetTexture", HasExplicitThis = true, IsFreeFunction = true)]
		public void SetTexture(int nameID, [NotNull] Texture texture)
		{
			if (texture == null)
			{
				ThrowHelper.ThrowArgumentNullException(texture, "texture");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Texture>(texture);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(texture, "texture");
			}
			RayTracingShader.SetTexture_Injected(intPtr, nameID, intPtr2);
		}

		// Token: 0x06003601 RID: 13825 RVA: 0x0006A020 File Offset: 0x00068220
		[NativeMethod(Name = "RayTracingShaderScripting::SetBuffer", HasExplicitThis = true, IsFreeFunction = true)]
		public void SetBuffer(int nameID, [NotNull] ComputeBuffer buffer)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = ComputeBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			RayTracingShader.SetBuffer_Injected(intPtr, nameID, intPtr2);
		}

		// Token: 0x06003602 RID: 13826 RVA: 0x0006A068 File Offset: 0x00068268
		[NativeMethod(Name = "RayTracingShaderScripting::SetBuffer", HasExplicitThis = true, IsFreeFunction = true)]
		private void SetGraphicsBuffer(int nameID, [NotNull] GraphicsBuffer buffer)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = GraphicsBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			RayTracingShader.SetGraphicsBuffer_Injected(intPtr, nameID, intPtr2);
		}

		// Token: 0x06003603 RID: 13827 RVA: 0x0006A0B0 File Offset: 0x000682B0
		[NativeMethod(Name = "RayTracingShaderScripting::SetBuffer", HasExplicitThis = true, IsFreeFunction = true)]
		private void SetGraphicsBufferHandle(int nameID, GraphicsBufferHandle bufferHandle)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RayTracingShader.SetGraphicsBufferHandle_Injected(intPtr, nameID, ref bufferHandle);
		}

		// Token: 0x06003604 RID: 13828 RVA: 0x0006A0D8 File Offset: 0x000682D8
		[FreeFunction(Name = "RayTracingShaderScripting::SetConstantBuffer", HasExplicitThis = true)]
		private void SetConstantComputeBuffer(int nameID, [NotNull] ComputeBuffer buffer, int offset, int size)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = ComputeBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			RayTracingShader.SetConstantComputeBuffer_Injected(intPtr, nameID, intPtr2, offset, size);
		}

		// Token: 0x06003605 RID: 13829 RVA: 0x0006A124 File Offset: 0x00068324
		[FreeFunction(Name = "RayTracingShaderScripting::SetConstantBuffer", HasExplicitThis = true)]
		private void SetConstantGraphicsBuffer(int nameID, [NotNull] GraphicsBuffer buffer, int offset, int size)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = GraphicsBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			RayTracingShader.SetConstantGraphicsBuffer_Injected(intPtr, nameID, intPtr2, offset, size);
		}

		// Token: 0x06003606 RID: 13830 RVA: 0x0006A170 File Offset: 0x00068370
		[NativeMethod(Name = "RayTracingShaderScripting::SetAccelerationStructure", HasExplicitThis = true, IsFreeFunction = true)]
		public void SetAccelerationStructure(int nameID, [NotNull] RayTracingAccelerationStructure accelerationStructure)
		{
			if (accelerationStructure == null)
			{
				ThrowHelper.ThrowArgumentNullException(accelerationStructure, "accelerationStructure");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(accelerationStructure);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(accelerationStructure, "accelerationStructure");
			}
			RayTracingShader.SetAccelerationStructure_Injected(intPtr, nameID, intPtr2);
		}

		// Token: 0x06003607 RID: 13831 RVA: 0x0006A1B8 File Offset: 0x000683B8
		public unsafe void SetShaderPass(string passName)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(passName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = passName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				RayTracingShader.SetShaderPass_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06003608 RID: 13832 RVA: 0x0006A21C File Offset: 0x0006841C
		[NativeMethod(Name = "RayTracingShaderScripting::SetTextureFromGlobal", HasExplicitThis = true, IsFreeFunction = true)]
		public void SetTextureFromGlobal(int nameID, int globalTextureNameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RayTracingShader.SetTextureFromGlobal_Injected(intPtr, nameID, globalTextureNameID);
		}

		// Token: 0x06003609 RID: 13833 RVA: 0x0006A240 File Offset: 0x00068440
		[NativeMethod(Name = "RayTracingShaderScripting::Dispatch", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		public unsafe void Dispatch(string rayGenFunctionName, int width, int height, int depth, Camera camera = null)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(rayGenFunctionName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = rayGenFunctionName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				RayTracingShader.Dispatch_Injected(intPtr, ref managedSpanWrapper, width, height, depth, Object.MarshalledUnityObject.Marshal<Camera>(camera));
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600360A RID: 13834 RVA: 0x0006A2B0 File Offset: 0x000684B0
		[NativeMethod(Name = "RayTracingShaderScripting::DispatchIndirect", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		public unsafe void DispatchIndirect(string rayGenFunctionName, [NotNull] GraphicsBuffer argsBuffer, uint argsOffset = 0U, Camera camera = null)
		{
			if (argsBuffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(argsBuffer, "argsBuffer");
			}
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<RayTracingShader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(rayGenFunctionName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = rayGenFunctionName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr2 = GraphicsBuffer.BindingsMarshaller.ConvertToNative(argsBuffer);
				if (intPtr2 == 0)
				{
					ThrowHelper.ThrowArgumentNullException(argsBuffer, "argsBuffer");
				}
				RayTracingShader.DispatchIndirect_Injected(intPtr, ref managedSpanWrapper, intPtr2, argsOffset, Object.MarshalledUnityObject.Marshal<Camera>(camera));
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600360B RID: 13835 RVA: 0x0006A340 File Offset: 0x00068540
		public void SetBuffer(int nameID, GraphicsBuffer buffer)
		{
			this.SetGraphicsBuffer(nameID, buffer);
		}

		// Token: 0x0600360C RID: 13836 RVA: 0x0006A34C File Offset: 0x0006854C
		public void SetBuffer(int nameID, GraphicsBufferHandle bufferHandle)
		{
			this.SetGraphicsBufferHandle(nameID, bufferHandle);
		}

		// Token: 0x0600360D RID: 13837 RVA: 0x00008C6F File Offset: 0x00006E6F
		private RayTracingShader()
		{
		}

		// Token: 0x0600360E RID: 13838 RVA: 0x0006A358 File Offset: 0x00068558
		public void SetFloat(string name, float val)
		{
			this.SetFloat(Shader.PropertyToID(name), val);
		}

		// Token: 0x0600360F RID: 13839 RVA: 0x0006A369 File Offset: 0x00068569
		public void SetInt(string name, int val)
		{
			this.SetInt(Shader.PropertyToID(name), val);
		}

		// Token: 0x06003610 RID: 13840 RVA: 0x0006A37A File Offset: 0x0006857A
		public void SetVector(string name, Vector4 val)
		{
			this.SetVector(Shader.PropertyToID(name), val);
		}

		// Token: 0x06003611 RID: 13841 RVA: 0x0006A38B File Offset: 0x0006858B
		public void SetMatrix(string name, Matrix4x4 val)
		{
			this.SetMatrix(Shader.PropertyToID(name), val);
		}

		// Token: 0x06003612 RID: 13842 RVA: 0x0006A39C File Offset: 0x0006859C
		public void SetVectorArray(string name, Vector4[] values)
		{
			this.SetVectorArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06003613 RID: 13843 RVA: 0x0006A3AD File Offset: 0x000685AD
		public void SetMatrixArray(string name, Matrix4x4[] values)
		{
			this.SetMatrixArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06003614 RID: 13844 RVA: 0x0006A3BE File Offset: 0x000685BE
		public void SetFloats(string name, params float[] values)
		{
			this.SetFloatArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06003615 RID: 13845 RVA: 0x0006A3CF File Offset: 0x000685CF
		public void SetFloats(int nameID, params float[] values)
		{
			this.SetFloatArray(nameID, values);
		}

		// Token: 0x06003616 RID: 13846 RVA: 0x0006A3DB File Offset: 0x000685DB
		public void SetInts(string name, params int[] values)
		{
			this.SetIntArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x06003617 RID: 13847 RVA: 0x0006A3EC File Offset: 0x000685EC
		public void SetInts(int nameID, params int[] values)
		{
			this.SetIntArray(nameID, values);
		}

		// Token: 0x06003618 RID: 13848 RVA: 0x0006A3F8 File Offset: 0x000685F8
		public void SetBool(string name, bool val)
		{
			this.SetInt(Shader.PropertyToID(name), val ? 1 : 0);
		}

		// Token: 0x06003619 RID: 13849 RVA: 0x0006A40F File Offset: 0x0006860F
		public void SetBool(int nameID, bool val)
		{
			this.SetInt(nameID, val ? 1 : 0);
		}

		// Token: 0x0600361A RID: 13850 RVA: 0x0006A421 File Offset: 0x00068621
		public void SetTexture(string name, Texture texture)
		{
			this.SetTexture(Shader.PropertyToID(name), texture);
		}

		// Token: 0x0600361B RID: 13851 RVA: 0x0006A432 File Offset: 0x00068632
		public void SetBuffer(string name, ComputeBuffer buffer)
		{
			this.SetBuffer(Shader.PropertyToID(name), buffer);
		}

		// Token: 0x0600361C RID: 13852 RVA: 0x0006A443 File Offset: 0x00068643
		public void SetBuffer(string name, GraphicsBuffer buffer)
		{
			this.SetBuffer(Shader.PropertyToID(name), buffer);
		}

		// Token: 0x0600361D RID: 13853 RVA: 0x0006A454 File Offset: 0x00068654
		public void SetBuffer(string name, GraphicsBufferHandle bufferHandle)
		{
			this.SetBuffer(Shader.PropertyToID(name), bufferHandle);
		}

		// Token: 0x0600361E RID: 13854 RVA: 0x0006A465 File Offset: 0x00068665
		public void SetConstantBuffer(int nameID, ComputeBuffer buffer, int offset, int size)
		{
			this.SetConstantComputeBuffer(nameID, buffer, offset, size);
		}

		// Token: 0x0600361F RID: 13855 RVA: 0x0006A474 File Offset: 0x00068674
		public void SetConstantBuffer(string name, ComputeBuffer buffer, int offset, int size)
		{
			this.SetConstantComputeBuffer(Shader.PropertyToID(name), buffer, offset, size);
		}

		// Token: 0x06003620 RID: 13856 RVA: 0x0006A488 File Offset: 0x00068688
		public void SetConstantBuffer(int nameID, GraphicsBuffer buffer, int offset, int size)
		{
			this.SetConstantGraphicsBuffer(nameID, buffer, offset, size);
		}

		// Token: 0x06003621 RID: 13857 RVA: 0x0006A497 File Offset: 0x00068697
		public void SetConstantBuffer(string name, GraphicsBuffer buffer, int offset, int size)
		{
			this.SetConstantGraphicsBuffer(Shader.PropertyToID(name), buffer, offset, size);
		}

		// Token: 0x06003622 RID: 13858 RVA: 0x0006A4AB File Offset: 0x000686AB
		public void SetAccelerationStructure(string name, RayTracingAccelerationStructure accelerationStructure)
		{
			this.SetAccelerationStructure(Shader.PropertyToID(name), accelerationStructure);
		}

		// Token: 0x06003623 RID: 13859 RVA: 0x0006A4BC File Offset: 0x000686BC
		public void SetTextureFromGlobal(string name, string globalTextureName)
		{
			this.SetTextureFromGlobal(Shader.PropertyToID(name), Shader.PropertyToID(globalTextureName));
		}

		// Token: 0x06003624 RID: 13860
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_maxRecursionDepth_Injected(IntPtr _unity_self);

		// Token: 0x06003625 RID: 13861
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFloat_Injected(IntPtr _unity_self, int nameID, float val);

		// Token: 0x06003626 RID: 13862
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetInt_Injected(IntPtr _unity_self, int nameID, int val);

		// Token: 0x06003627 RID: 13863
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVector_Injected(IntPtr _unity_self, int nameID, [In] ref Vector4 val);

		// Token: 0x06003628 RID: 13864
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMatrix_Injected(IntPtr _unity_self, int nameID, [In] ref Matrix4x4 val);

		// Token: 0x06003629 RID: 13865
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFloatArray_Injected(IntPtr _unity_self, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x0600362A RID: 13866
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetIntArray_Injected(IntPtr _unity_self, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x0600362B RID: 13867
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVectorArray_Injected(IntPtr _unity_self, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x0600362C RID: 13868
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMatrixArray_Injected(IntPtr _unity_self, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x0600362D RID: 13869
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTexture_Injected(IntPtr _unity_self, int nameID, IntPtr texture);

		// Token: 0x0600362E RID: 13870
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBuffer_Injected(IntPtr _unity_self, int nameID, IntPtr buffer);

		// Token: 0x0600362F RID: 13871
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGraphicsBuffer_Injected(IntPtr _unity_self, int nameID, IntPtr buffer);

		// Token: 0x06003630 RID: 13872
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGraphicsBufferHandle_Injected(IntPtr _unity_self, int nameID, [In] ref GraphicsBufferHandle bufferHandle);

		// Token: 0x06003631 RID: 13873
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetConstantComputeBuffer_Injected(IntPtr _unity_self, int nameID, IntPtr buffer, int offset, int size);

		// Token: 0x06003632 RID: 13874
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetConstantGraphicsBuffer_Injected(IntPtr _unity_self, int nameID, IntPtr buffer, int offset, int size);

		// Token: 0x06003633 RID: 13875
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAccelerationStructure_Injected(IntPtr _unity_self, int nameID, IntPtr accelerationStructure);

		// Token: 0x06003634 RID: 13876
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetShaderPass_Injected(IntPtr _unity_self, ref ManagedSpanWrapper passName);

		// Token: 0x06003635 RID: 13877
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTextureFromGlobal_Injected(IntPtr _unity_self, int nameID, int globalTextureNameID);

		// Token: 0x06003636 RID: 13878
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Dispatch_Injected(IntPtr _unity_self, ref ManagedSpanWrapper rayGenFunctionName, int width, int height, int depth, IntPtr camera);

		// Token: 0x06003637 RID: 13879
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DispatchIndirect_Injected(IntPtr _unity_self, ref ManagedSpanWrapper rayGenFunctionName, IntPtr argsBuffer, uint argsOffset, IntPtr camera);
	}
}
