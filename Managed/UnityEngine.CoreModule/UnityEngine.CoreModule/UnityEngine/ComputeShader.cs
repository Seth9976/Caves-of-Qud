using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020002D1 RID: 721
	[NativeHeader("Runtime/Shaders/RayTracing/RayTracingAccelerationStructure.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	[NativeHeader("Runtime/Shaders/ComputeShader.h")]
	public sealed class ComputeShader : Object
	{
		// Token: 0x060022BB RID: 8891 RVA: 0x00044270 File Offset: 0x00042470
		[RequiredByNativeCode]
		[NativeMethod(Name = "ComputeShaderScripting::FindKernel", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		public unsafe int FindKernel(string name)
		{
			int num;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				num = ComputeShader.FindKernel_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x060022BC RID: 8892 RVA: 0x000442D8 File Offset: 0x000424D8
		[FreeFunction(Name = "ComputeShaderScripting::HasKernel", HasExplicitThis = true)]
		public unsafe bool HasKernel(string name)
		{
			bool flag;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = ComputeShader.HasKernel_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x00044340 File Offset: 0x00042540
		[FreeFunction(Name = "ComputeShaderScripting::SetValue<float>", HasExplicitThis = true)]
		public void SetFloat(int nameID, float val)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ComputeShader.SetFloat_Injected(intPtr, nameID, val);
		}

		// Token: 0x060022BE RID: 8894 RVA: 0x00044364 File Offset: 0x00042564
		[FreeFunction(Name = "ComputeShaderScripting::SetValue<int>", HasExplicitThis = true)]
		public void SetInt(int nameID, int val)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ComputeShader.SetInt_Injected(intPtr, nameID, val);
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x00044388 File Offset: 0x00042588
		[FreeFunction(Name = "ComputeShaderScripting::SetValue<Vector4f>", HasExplicitThis = true)]
		public void SetVector(int nameID, Vector4 val)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ComputeShader.SetVector_Injected(intPtr, nameID, ref val);
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x000443B0 File Offset: 0x000425B0
		[FreeFunction(Name = "ComputeShaderScripting::SetValue<Matrix4x4f>", HasExplicitThis = true)]
		public void SetMatrix(int nameID, Matrix4x4 val)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ComputeShader.SetMatrix_Injected(intPtr, nameID, ref val);
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x000443D8 File Offset: 0x000425D8
		[FreeFunction(Name = "ComputeShaderScripting::SetArray<float>", HasExplicitThis = true)]
		private unsafe void SetFloatArray(int nameID, float[] values)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<float> span = new Span<float>(values);
			fixed (float* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				ComputeShader.SetFloatArray_Injected(intPtr, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x00044424 File Offset: 0x00042624
		[FreeFunction(Name = "ComputeShaderScripting::SetArray<int>", HasExplicitThis = true)]
		private unsafe void SetIntArray(int nameID, int[] values)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<int> span = new Span<int>(values);
			fixed (int* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				ComputeShader.SetIntArray_Injected(intPtr, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x00044470 File Offset: 0x00042670
		[FreeFunction(Name = "ComputeShaderScripting::SetArray<Vector4f>", HasExplicitThis = true)]
		public unsafe void SetVectorArray(int nameID, Vector4[] values)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Vector4> span = new Span<Vector4>(values);
			fixed (Vector4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				ComputeShader.SetVectorArray_Injected(intPtr, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x000444BC File Offset: 0x000426BC
		[FreeFunction(Name = "ComputeShaderScripting::SetArray<Matrix4x4f>", HasExplicitThis = true)]
		public unsafe void SetMatrixArray(int nameID, Matrix4x4[] values)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Matrix4x4> span = new Span<Matrix4x4>(values);
			fixed (Matrix4x4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				ComputeShader.SetMatrixArray_Injected(intPtr, nameID, ref managedSpanWrapper);
			}
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x00044508 File Offset: 0x00042708
		[NativeMethod(Name = "ComputeShaderScripting::SetTexture", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		public void SetTexture(int kernelIndex, int nameID, [NotNull] Texture texture, int mipLevel)
		{
			if (texture == null)
			{
				ThrowHelper.ThrowArgumentNullException(texture, "texture");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Texture>(texture);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(texture, "texture");
			}
			ComputeShader.SetTexture_Injected(intPtr, kernelIndex, nameID, intPtr2, mipLevel);
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x00044554 File Offset: 0x00042754
		[NativeMethod(Name = "ComputeShaderScripting::SetRenderTexture", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		private void SetRenderTexture(int kernelIndex, int nameID, [NotNull] RenderTexture texture, int mipLevel, RenderTextureSubElement element)
		{
			if (texture == null)
			{
				ThrowHelper.ThrowArgumentNullException(texture, "texture");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<RenderTexture>(texture);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(texture, "texture");
			}
			ComputeShader.SetRenderTexture_Injected(intPtr, kernelIndex, nameID, intPtr2, mipLevel, element);
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x000445A0 File Offset: 0x000427A0
		[NativeMethod(Name = "ComputeShaderScripting::SetTextureFromGlobal", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		public void SetTextureFromGlobal(int kernelIndex, int nameID, int globalTextureNameID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ComputeShader.SetTextureFromGlobal_Injected(intPtr, kernelIndex, nameID, globalTextureNameID);
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x000445C8 File Offset: 0x000427C8
		[FreeFunction(Name = "ComputeShaderScripting::SetBuffer", HasExplicitThis = true)]
		private void Internal_SetBuffer(int kernelIndex, int nameID, [NotNull] ComputeBuffer buffer)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = ComputeBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			ComputeShader.Internal_SetBuffer_Injected(intPtr, kernelIndex, nameID, intPtr2);
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x00044610 File Offset: 0x00042810
		[FreeFunction(Name = "ComputeShaderScripting::SetBuffer", HasExplicitThis = true)]
		private void Internal_SetGraphicsBuffer(int kernelIndex, int nameID, [NotNull] GraphicsBuffer buffer)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = GraphicsBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			ComputeShader.Internal_SetGraphicsBuffer_Injected(intPtr, kernelIndex, nameID, intPtr2);
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x00044658 File Offset: 0x00042858
		[FreeFunction(Name = "ComputeShaderScripting::SetRayTracingAccelerationStructure", HasExplicitThis = true)]
		private void Internal_SetRayTracingAccelerationStructure(int kernelIndex, int nameID, [NotNull] RayTracingAccelerationStructure accelerationStructure)
		{
			if (accelerationStructure == null)
			{
				ThrowHelper.ThrowArgumentNullException(accelerationStructure, "accelerationStructure");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(accelerationStructure);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(accelerationStructure, "accelerationStructure");
			}
			ComputeShader.Internal_SetRayTracingAccelerationStructure_Injected(intPtr, kernelIndex, nameID, intPtr2);
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x000446A0 File Offset: 0x000428A0
		public void SetRayTracingAccelerationStructure(int kernelIndex, int nameID, RayTracingAccelerationStructure accelerationStructure)
		{
			this.Internal_SetRayTracingAccelerationStructure(kernelIndex, nameID, accelerationStructure);
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x000446AD File Offset: 0x000428AD
		public void SetBuffer(int kernelIndex, int nameID, ComputeBuffer buffer)
		{
			this.Internal_SetBuffer(kernelIndex, nameID, buffer);
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x000446BA File Offset: 0x000428BA
		public void SetBuffer(int kernelIndex, int nameID, GraphicsBuffer buffer)
		{
			this.Internal_SetGraphicsBuffer(kernelIndex, nameID, buffer);
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x000446C8 File Offset: 0x000428C8
		[FreeFunction(Name = "ComputeShaderScripting::SetConstantBuffer", HasExplicitThis = true)]
		private void SetConstantComputeBuffer(int nameID, [NotNull] ComputeBuffer buffer, int offset, int size)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = ComputeBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			ComputeShader.SetConstantComputeBuffer_Injected(intPtr, nameID, intPtr2, offset, size);
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x00044714 File Offset: 0x00042914
		[FreeFunction(Name = "ComputeShaderScripting::SetConstantBuffer", HasExplicitThis = true)]
		private void SetConstantGraphicsBuffer(int nameID, [NotNull] GraphicsBuffer buffer, int offset, int size)
		{
			if (buffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = GraphicsBuffer.BindingsMarshaller.ConvertToNative(buffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(buffer, "buffer");
			}
			ComputeShader.SetConstantGraphicsBuffer_Injected(intPtr, nameID, intPtr2, offset, size);
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x00044760 File Offset: 0x00042960
		[NativeMethod(Name = "ComputeShaderScripting::GetKernelThreadGroupSizes", HasExplicitThis = true, IsFreeFunction = true, ThrowsException = true)]
		public void GetKernelThreadGroupSizes(int kernelIndex, out uint x, out uint y, out uint z)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ComputeShader.GetKernelThreadGroupSizes_Injected(intPtr, kernelIndex, out x, out y, out z);
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x00044788 File Offset: 0x00042988
		[NativeName("DispatchComputeShader")]
		public void Dispatch(int kernelIndex, int threadGroupsX, int threadGroupsY, int threadGroupsZ)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ComputeShader.Dispatch_Injected(intPtr, kernelIndex, threadGroupsX, threadGroupsY, threadGroupsZ);
		}

		// Token: 0x060022D2 RID: 8914 RVA: 0x000447B0 File Offset: 0x000429B0
		[FreeFunction(Name = "ComputeShaderScripting::DispatchIndirect", HasExplicitThis = true)]
		private void Internal_DispatchIndirect(int kernelIndex, [NotNull] ComputeBuffer argsBuffer, uint argsOffset)
		{
			if (argsBuffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(argsBuffer, "argsBuffer");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = ComputeBuffer.BindingsMarshaller.ConvertToNative(argsBuffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(argsBuffer, "argsBuffer");
			}
			ComputeShader.Internal_DispatchIndirect_Injected(intPtr, kernelIndex, intPtr2, argsOffset);
		}

		// Token: 0x060022D3 RID: 8915 RVA: 0x000447F8 File Offset: 0x000429F8
		[FreeFunction(Name = "ComputeShaderScripting::DispatchIndirect", HasExplicitThis = true)]
		private void Internal_DispatchIndirectGraphicsBuffer(int kernelIndex, [NotNull] GraphicsBuffer argsBuffer, uint argsOffset)
		{
			if (argsBuffer == null)
			{
				ThrowHelper.ThrowArgumentNullException(argsBuffer, "argsBuffer");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = GraphicsBuffer.BindingsMarshaller.ConvertToNative(argsBuffer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(argsBuffer, "argsBuffer");
			}
			ComputeShader.Internal_DispatchIndirectGraphicsBuffer_Injected(intPtr, kernelIndex, intPtr2, argsOffset);
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x060022D4 RID: 8916 RVA: 0x00044840 File Offset: 0x00042A40
		public LocalKeywordSpace keywordSpace
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LocalKeywordSpace localKeywordSpace;
				ComputeShader.get_keywordSpace_Injected(intPtr, out localKeywordSpace);
				return localKeywordSpace;
			}
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x00044868 File Offset: 0x00042A68
		[FreeFunction("ComputeShaderScripting::EnableKeyword", HasExplicitThis = true)]
		public unsafe void EnableKeyword(string keyword)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(keyword, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = keyword.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ComputeShader.EnableKeyword_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x000448CC File Offset: 0x00042ACC
		[FreeFunction("ComputeShaderScripting::DisableKeyword", HasExplicitThis = true)]
		public unsafe void DisableKeyword(string keyword)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(keyword, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = keyword.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				ComputeShader.DisableKeyword_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x00044930 File Offset: 0x00042B30
		[FreeFunction("ComputeShaderScripting::IsKeywordEnabled", HasExplicitThis = true)]
		public unsafe bool IsKeywordEnabled(string keyword)
		{
			bool flag;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(keyword, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = keyword.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = ComputeShader.IsKeywordEnabled_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x00044998 File Offset: 0x00042B98
		[FreeFunction("ComputeShaderScripting::EnableKeyword", HasExplicitThis = true)]
		private void EnableLocalKeyword(LocalKeyword keyword)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ComputeShader.EnableLocalKeyword_Injected(intPtr, ref keyword);
		}

		// Token: 0x060022D9 RID: 8921 RVA: 0x000449BC File Offset: 0x00042BBC
		[FreeFunction("ComputeShaderScripting::DisableKeyword", HasExplicitThis = true)]
		private void DisableLocalKeyword(LocalKeyword keyword)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ComputeShader.DisableLocalKeyword_Injected(intPtr, ref keyword);
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x000449E0 File Offset: 0x00042BE0
		[FreeFunction("ComputeShaderScripting::SetKeyword", HasExplicitThis = true)]
		private void SetLocalKeyword(LocalKeyword keyword, bool value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ComputeShader.SetLocalKeyword_Injected(intPtr, ref keyword, value);
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x00044A08 File Offset: 0x00042C08
		[FreeFunction("ComputeShaderScripting::IsKeywordEnabled", HasExplicitThis = true)]
		private bool IsLocalKeywordEnabled(LocalKeyword keyword)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return ComputeShader.IsLocalKeywordEnabled_Injected(intPtr, ref keyword);
		}

		// Token: 0x060022DC RID: 8924 RVA: 0x00044A2C File Offset: 0x00042C2C
		public void EnableKeyword(in LocalKeyword keyword)
		{
			this.EnableLocalKeyword(keyword);
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x00044A3C File Offset: 0x00042C3C
		public void DisableKeyword(in LocalKeyword keyword)
		{
			this.DisableLocalKeyword(keyword);
		}

		// Token: 0x060022DE RID: 8926 RVA: 0x00044A4C File Offset: 0x00042C4C
		public void SetKeyword(in LocalKeyword keyword, bool value)
		{
			this.SetLocalKeyword(keyword, value);
		}

		// Token: 0x060022DF RID: 8927 RVA: 0x00044A60 File Offset: 0x00042C60
		public bool IsKeywordEnabled(in LocalKeyword keyword)
		{
			return this.IsLocalKeywordEnabled(keyword);
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x00044A80 File Offset: 0x00042C80
		[FreeFunction("ComputeShaderScripting::IsSupported", HasExplicitThis = true)]
		public bool IsSupported(int kernelIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return ComputeShader.IsSupported_Injected(intPtr, kernelIndex);
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x00044AA4 File Offset: 0x00042CA4
		[FreeFunction("ComputeShaderScripting::GetShaderKeywords", HasExplicitThis = true)]
		private string[] GetShaderKeywords()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return ComputeShader.GetShaderKeywords_Injected(intPtr);
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x00044AC8 File Offset: 0x00042CC8
		[FreeFunction("ComputeShaderScripting::SetShaderKeywords", HasExplicitThis = true)]
		private void SetShaderKeywords(string[] names)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ComputeShader.SetShaderKeywords_Injected(intPtr, names);
		}

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x060022E3 RID: 8931 RVA: 0x00044AEC File Offset: 0x00042CEC
		// (set) Token: 0x060022E4 RID: 8932 RVA: 0x00044B04 File Offset: 0x00042D04
		public string[] shaderKeywords
		{
			get
			{
				return this.GetShaderKeywords();
			}
			set
			{
				this.SetShaderKeywords(value);
			}
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x00044B10 File Offset: 0x00042D10
		[FreeFunction("ComputeShaderScripting::GetEnabledKeywords", HasExplicitThis = true)]
		private LocalKeyword[] GetEnabledKeywords()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return ComputeShader.GetEnabledKeywords_Injected(intPtr);
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x00044B34 File Offset: 0x00042D34
		[FreeFunction("ComputeShaderScripting::SetEnabledKeywords", HasExplicitThis = true)]
		private void SetEnabledKeywords(LocalKeyword[] keywords)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ComputeShader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ComputeShader.SetEnabledKeywords_Injected(intPtr, keywords);
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x060022E7 RID: 8935 RVA: 0x00044B58 File Offset: 0x00042D58
		// (set) Token: 0x060022E8 RID: 8936 RVA: 0x00044B70 File Offset: 0x00042D70
		public LocalKeyword[] enabledKeywords
		{
			get
			{
				return this.GetEnabledKeywords();
			}
			set
			{
				this.SetEnabledKeywords(value);
			}
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x00008C6F File Offset: 0x00006E6F
		private ComputeShader()
		{
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x00044B7B File Offset: 0x00042D7B
		public void SetFloat(string name, float val)
		{
			this.SetFloat(Shader.PropertyToID(name), val);
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x00044B8C File Offset: 0x00042D8C
		public void SetInt(string name, int val)
		{
			this.SetInt(Shader.PropertyToID(name), val);
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x00044B9D File Offset: 0x00042D9D
		public void SetVector(string name, Vector4 val)
		{
			this.SetVector(Shader.PropertyToID(name), val);
		}

		// Token: 0x060022ED RID: 8941 RVA: 0x00044BAE File Offset: 0x00042DAE
		public void SetMatrix(string name, Matrix4x4 val)
		{
			this.SetMatrix(Shader.PropertyToID(name), val);
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x00044BBF File Offset: 0x00042DBF
		public void SetVectorArray(string name, Vector4[] values)
		{
			this.SetVectorArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x00044BD0 File Offset: 0x00042DD0
		public void SetMatrixArray(string name, Matrix4x4[] values)
		{
			this.SetMatrixArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x00044BE1 File Offset: 0x00042DE1
		public void SetFloats(string name, params float[] values)
		{
			this.SetFloatArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x00044BF2 File Offset: 0x00042DF2
		public void SetFloats(int nameID, params float[] values)
		{
			this.SetFloatArray(nameID, values);
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x00044BFE File Offset: 0x00042DFE
		public void SetInts(string name, params int[] values)
		{
			this.SetIntArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x00044C0F File Offset: 0x00042E0F
		public void SetInts(int nameID, params int[] values)
		{
			this.SetIntArray(nameID, values);
		}

		// Token: 0x060022F4 RID: 8948 RVA: 0x00044C1B File Offset: 0x00042E1B
		public void SetBool(string name, bool val)
		{
			this.SetInt(Shader.PropertyToID(name), val ? 1 : 0);
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x00044C32 File Offset: 0x00042E32
		public void SetBool(int nameID, bool val)
		{
			this.SetInt(nameID, val ? 1 : 0);
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x00044C44 File Offset: 0x00042E44
		public void SetTexture(int kernelIndex, int nameID, Texture texture)
		{
			this.SetTexture(kernelIndex, nameID, texture, 0);
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x00044C52 File Offset: 0x00042E52
		public void SetTexture(int kernelIndex, string name, Texture texture)
		{
			this.SetTexture(kernelIndex, Shader.PropertyToID(name), texture, 0);
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x00044C65 File Offset: 0x00042E65
		public void SetTexture(int kernelIndex, string name, Texture texture, int mipLevel)
		{
			this.SetTexture(kernelIndex, Shader.PropertyToID(name), texture, mipLevel);
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x00044C79 File Offset: 0x00042E79
		public void SetTexture(int kernelIndex, int nameID, RenderTexture texture, int mipLevel, RenderTextureSubElement element)
		{
			this.SetRenderTexture(kernelIndex, nameID, texture, mipLevel, element);
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x00044C8A File Offset: 0x00042E8A
		public void SetTexture(int kernelIndex, string name, RenderTexture texture, int mipLevel, RenderTextureSubElement element)
		{
			this.SetRenderTexture(kernelIndex, Shader.PropertyToID(name), texture, mipLevel, element);
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x00044CA0 File Offset: 0x00042EA0
		public void SetTextureFromGlobal(int kernelIndex, string name, string globalTextureName)
		{
			this.SetTextureFromGlobal(kernelIndex, Shader.PropertyToID(name), Shader.PropertyToID(globalTextureName));
		}

		// Token: 0x060022FC RID: 8956 RVA: 0x00044CB7 File Offset: 0x00042EB7
		public void SetBuffer(int kernelIndex, string name, ComputeBuffer buffer)
		{
			this.SetBuffer(kernelIndex, Shader.PropertyToID(name), buffer);
		}

		// Token: 0x060022FD RID: 8957 RVA: 0x00044CC9 File Offset: 0x00042EC9
		public void SetBuffer(int kernelIndex, string name, GraphicsBuffer buffer)
		{
			this.SetBuffer(kernelIndex, Shader.PropertyToID(name), buffer);
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x00044CDB File Offset: 0x00042EDB
		public void SetRayTracingAccelerationStructure(int kernelIndex, string name, RayTracingAccelerationStructure accelerationStructure)
		{
			this.SetRayTracingAccelerationStructure(kernelIndex, Shader.PropertyToID(name), accelerationStructure);
		}

		// Token: 0x060022FF RID: 8959 RVA: 0x00044CED File Offset: 0x00042EED
		public void SetConstantBuffer(int nameID, ComputeBuffer buffer, int offset, int size)
		{
			this.SetConstantComputeBuffer(nameID, buffer, offset, size);
		}

		// Token: 0x06002300 RID: 8960 RVA: 0x00044CFC File Offset: 0x00042EFC
		public void SetConstantBuffer(string name, ComputeBuffer buffer, int offset, int size)
		{
			this.SetConstantBuffer(Shader.PropertyToID(name), buffer, offset, size);
		}

		// Token: 0x06002301 RID: 8961 RVA: 0x00044D10 File Offset: 0x00042F10
		public void SetConstantBuffer(int nameID, GraphicsBuffer buffer, int offset, int size)
		{
			this.SetConstantGraphicsBuffer(nameID, buffer, offset, size);
		}

		// Token: 0x06002302 RID: 8962 RVA: 0x00044D1F File Offset: 0x00042F1F
		public void SetConstantBuffer(string name, GraphicsBuffer buffer, int offset, int size)
		{
			this.SetConstantBuffer(Shader.PropertyToID(name), buffer, offset, size);
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x00044D34 File Offset: 0x00042F34
		public void DispatchIndirect(int kernelIndex, ComputeBuffer argsBuffer, [DefaultValue("0")] uint argsOffset)
		{
			bool flag = argsBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("argsBuffer");
			}
			bool flag2 = argsBuffer.m_Ptr == IntPtr.Zero;
			if (flag2)
			{
				throw new ObjectDisposedException("argsBuffer");
			}
			bool flag3 = SystemInfo.graphicsDeviceType == GraphicsDeviceType.Metal && !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag3)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			this.Internal_DispatchIndirect(kernelIndex, argsBuffer, argsOffset);
		}

		// Token: 0x06002304 RID: 8964 RVA: 0x00044DA1 File Offset: 0x00042FA1
		[ExcludeFromDocs]
		public void DispatchIndirect(int kernelIndex, ComputeBuffer argsBuffer)
		{
			this.DispatchIndirect(kernelIndex, argsBuffer, 0U);
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x00044DB0 File Offset: 0x00042FB0
		public void DispatchIndirect(int kernelIndex, GraphicsBuffer argsBuffer, [DefaultValue("0")] uint argsOffset)
		{
			bool flag = argsBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("argsBuffer");
			}
			bool flag2 = argsBuffer.m_Ptr == IntPtr.Zero;
			if (flag2)
			{
				throw new ObjectDisposedException("argsBuffer");
			}
			bool flag3 = SystemInfo.graphicsDeviceType == GraphicsDeviceType.Metal && !SystemInfo.supportsIndirectArgumentsBuffer;
			if (flag3)
			{
				throw new InvalidOperationException("Indirect argument buffers are not supported.");
			}
			this.Internal_DispatchIndirectGraphicsBuffer(kernelIndex, argsBuffer, argsOffset);
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x00044E1D File Offset: 0x0004301D
		[ExcludeFromDocs]
		public void DispatchIndirect(int kernelIndex, GraphicsBuffer argsBuffer)
		{
			this.DispatchIndirect(kernelIndex, argsBuffer, 0U);
		}

		// Token: 0x06002307 RID: 8967
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int FindKernel_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x06002308 RID: 8968
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasKernel_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x06002309 RID: 8969
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFloat_Injected(IntPtr _unity_self, int nameID, float val);

		// Token: 0x0600230A RID: 8970
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetInt_Injected(IntPtr _unity_self, int nameID, int val);

		// Token: 0x0600230B RID: 8971
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVector_Injected(IntPtr _unity_self, int nameID, [In] ref Vector4 val);

		// Token: 0x0600230C RID: 8972
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMatrix_Injected(IntPtr _unity_self, int nameID, [In] ref Matrix4x4 val);

		// Token: 0x0600230D RID: 8973
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFloatArray_Injected(IntPtr _unity_self, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x0600230E RID: 8974
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetIntArray_Injected(IntPtr _unity_self, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x0600230F RID: 8975
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVectorArray_Injected(IntPtr _unity_self, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x06002310 RID: 8976
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetMatrixArray_Injected(IntPtr _unity_self, int nameID, ref ManagedSpanWrapper values);

		// Token: 0x06002311 RID: 8977
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTexture_Injected(IntPtr _unity_self, int kernelIndex, int nameID, IntPtr texture, int mipLevel);

		// Token: 0x06002312 RID: 8978
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetRenderTexture_Injected(IntPtr _unity_self, int kernelIndex, int nameID, IntPtr texture, int mipLevel, RenderTextureSubElement element);

		// Token: 0x06002313 RID: 8979
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTextureFromGlobal_Injected(IntPtr _unity_self, int kernelIndex, int nameID, int globalTextureNameID);

		// Token: 0x06002314 RID: 8980
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetBuffer_Injected(IntPtr _unity_self, int kernelIndex, int nameID, IntPtr buffer);

		// Token: 0x06002315 RID: 8981
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetGraphicsBuffer_Injected(IntPtr _unity_self, int kernelIndex, int nameID, IntPtr buffer);

		// Token: 0x06002316 RID: 8982
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetRayTracingAccelerationStructure_Injected(IntPtr _unity_self, int kernelIndex, int nameID, IntPtr accelerationStructure);

		// Token: 0x06002317 RID: 8983
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetConstantComputeBuffer_Injected(IntPtr _unity_self, int nameID, IntPtr buffer, int offset, int size);

		// Token: 0x06002318 RID: 8984
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetConstantGraphicsBuffer_Injected(IntPtr _unity_self, int nameID, IntPtr buffer, int offset, int size);

		// Token: 0x06002319 RID: 8985
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetKernelThreadGroupSizes_Injected(IntPtr _unity_self, int kernelIndex, out uint x, out uint y, out uint z);

		// Token: 0x0600231A RID: 8986
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Dispatch_Injected(IntPtr _unity_self, int kernelIndex, int threadGroupsX, int threadGroupsY, int threadGroupsZ);

		// Token: 0x0600231B RID: 8987
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DispatchIndirect_Injected(IntPtr _unity_self, int kernelIndex, IntPtr argsBuffer, uint argsOffset);

		// Token: 0x0600231C RID: 8988
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DispatchIndirectGraphicsBuffer_Injected(IntPtr _unity_self, int kernelIndex, IntPtr argsBuffer, uint argsOffset);

		// Token: 0x0600231D RID: 8989
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_keywordSpace_Injected(IntPtr _unity_self, out LocalKeywordSpace ret);

		// Token: 0x0600231E RID: 8990
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableKeyword_Injected(IntPtr _unity_self, ref ManagedSpanWrapper keyword);

		// Token: 0x0600231F RID: 8991
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableKeyword_Injected(IntPtr _unity_self, ref ManagedSpanWrapper keyword);

		// Token: 0x06002320 RID: 8992
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsKeywordEnabled_Injected(IntPtr _unity_self, ref ManagedSpanWrapper keyword);

		// Token: 0x06002321 RID: 8993
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableLocalKeyword_Injected(IntPtr _unity_self, [In] ref LocalKeyword keyword);

		// Token: 0x06002322 RID: 8994
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableLocalKeyword_Injected(IntPtr _unity_self, [In] ref LocalKeyword keyword);

		// Token: 0x06002323 RID: 8995
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLocalKeyword_Injected(IntPtr _unity_self, [In] ref LocalKeyword keyword, bool value);

		// Token: 0x06002324 RID: 8996
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsLocalKeywordEnabled_Injected(IntPtr _unity_self, [In] ref LocalKeyword keyword);

		// Token: 0x06002325 RID: 8997
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsSupported_Injected(IntPtr _unity_self, int kernelIndex);

		// Token: 0x06002326 RID: 8998
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetShaderKeywords_Injected(IntPtr _unity_self);

		// Token: 0x06002327 RID: 8999
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetShaderKeywords_Injected(IntPtr _unity_self, string[] names);

		// Token: 0x06002328 RID: 9000
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern LocalKeyword[] GetEnabledKeywords_Injected(IntPtr _unity_self);

		// Token: 0x06002329 RID: 9001
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetEnabledKeywords_Injected(IntPtr _unity_self, LocalKeyword[] keywords);
	}
}
