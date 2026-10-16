using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001E8 RID: 488
	[NativeHeader("Runtime/Graphics/Mesh/MeshScriptBindings.h")]
	[RequiredByNativeCode]
	[ExcludeFromPreset]
	public sealed class Mesh : Object
	{
		// Token: 0x06001556 RID: 5462
		[FreeFunction("MeshScripting::CreateMesh")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] Mesh mono);

		// Token: 0x06001557 RID: 5463 RVA: 0x00023F65 File Offset: 0x00022165
		[RequiredByNativeCode]
		public Mesh()
		{
			Mesh.Internal_Create(this);
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x00023F78 File Offset: 0x00022178
		[FreeFunction("MeshScripting::MeshFromInstanceId")]
		internal static Mesh FromInstanceID(int id)
		{
			return Unmarshal.UnmarshalUnityObject<Mesh>(Mesh.FromInstanceID_Injected(id));
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x00023F90 File Offset: 0x00022190
		// (set) Token: 0x0600155A RID: 5466 RVA: 0x00023FB4 File Offset: 0x000221B4
		public IndexFormat indexFormat
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Mesh.get_indexFormat_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Mesh.set_indexFormat_Injected(intPtr, value);
			}
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x00023FD8 File Offset: 0x000221D8
		internal uint GetTotalIndexCount()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Mesh.GetTotalIndexCount_Injected(intPtr);
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x00023FFC File Offset: 0x000221FC
		[FreeFunction(Name = "MeshScripting::SetIndexBufferParams", HasExplicitThis = true, ThrowsException = true)]
		public void SetIndexBufferParams(int indexCount, IndexFormat format)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.SetIndexBufferParams_Injected(intPtr, indexCount, format);
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x00024020 File Offset: 0x00022220
		[FreeFunction(Name = "MeshScripting::InternalSetIndexBufferData", HasExplicitThis = true, ThrowsException = true)]
		private void InternalSetIndexBufferData(IntPtr data, int dataStart, int meshBufferStart, int count, int elemSize, MeshUpdateFlags flags)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.InternalSetIndexBufferData_Injected(intPtr, data, dataStart, meshBufferStart, count, elemSize, flags);
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x0002404C File Offset: 0x0002224C
		[FreeFunction(Name = "MeshScripting::InternalSetIndexBufferDataFromArray", HasExplicitThis = true, ThrowsException = true)]
		private void InternalSetIndexBufferDataFromArray(Array data, int dataStart, int meshBufferStart, int count, int elemSize, MeshUpdateFlags flags)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.InternalSetIndexBufferDataFromArray_Injected(intPtr, data, dataStart, meshBufferStart, count, elemSize, flags);
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x00024078 File Offset: 0x00022278
		[FreeFunction(Name = "MeshScripting::SetVertexBufferParamsFromPtr", HasExplicitThis = true, ThrowsException = true)]
		private void SetVertexBufferParamsFromPtr(int vertexCount, IntPtr attributesPtr, int attributesCount)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.SetVertexBufferParamsFromPtr_Injected(intPtr, vertexCount, attributesPtr, attributesCount);
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x000240A0 File Offset: 0x000222A0
		[FreeFunction(Name = "MeshScripting::SetVertexBufferParamsFromArray", HasExplicitThis = true, ThrowsException = true)]
		private unsafe void SetVertexBufferParamsFromArray(int vertexCount, params VertexAttributeDescriptor[] attributes)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<VertexAttributeDescriptor> span = new Span<VertexAttributeDescriptor>(attributes);
			fixed (VertexAttributeDescriptor* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Mesh.SetVertexBufferParamsFromArray_Injected(intPtr, vertexCount, ref managedSpanWrapper);
			}
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x000240EC File Offset: 0x000222EC
		[FreeFunction(Name = "MeshScripting::InternalSetVertexBufferData", HasExplicitThis = true)]
		private void InternalSetVertexBufferData(int stream, IntPtr data, int dataStart, int meshBufferStart, int count, int elemSize, MeshUpdateFlags flags)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.InternalSetVertexBufferData_Injected(intPtr, stream, data, dataStart, meshBufferStart, count, elemSize, flags);
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x0002411C File Offset: 0x0002231C
		[FreeFunction(Name = "MeshScripting::InternalSetVertexBufferDataFromArray", HasExplicitThis = true)]
		private void InternalSetVertexBufferDataFromArray(int stream, Array data, int dataStart, int meshBufferStart, int count, int elemSize, MeshUpdateFlags flags)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.InternalSetVertexBufferDataFromArray_Injected(intPtr, stream, data, dataStart, meshBufferStart, count, elemSize, flags);
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x0002414C File Offset: 0x0002234C
		[FreeFunction(Name = "MeshScripting::GetVertexAttributesAlloc", HasExplicitThis = true)]
		private Array GetVertexAttributesAlloc()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Mesh.GetVertexAttributesAlloc_Injected(intPtr);
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x00024170 File Offset: 0x00022370
		[FreeFunction(Name = "MeshScripting::GetVertexAttributesArray", HasExplicitThis = true)]
		private unsafe int GetVertexAttributesArray([NotNull] VertexAttributeDescriptor[] attributes)
		{
			if (attributes == null)
			{
				ThrowHelper.ThrowArgumentNullException(attributes, "attributes");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<VertexAttributeDescriptor> span = new Span<VertexAttributeDescriptor>(attributes);
			int vertexAttributesArray_Injected;
			fixed (VertexAttributeDescriptor* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				vertexAttributesArray_Injected = Mesh.GetVertexAttributesArray_Injected(intPtr, ref managedSpanWrapper);
			}
			return vertexAttributesArray_Injected;
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x000241C8 File Offset: 0x000223C8
		[FreeFunction(Name = "MeshScripting::GetVertexAttributesList", HasExplicitThis = true)]
		private unsafe int GetVertexAttributesList([NotNull] List<VertexAttributeDescriptor> attributes)
		{
			if (attributes == null)
			{
				ThrowHelper.ThrowArgumentNullException(attributes, "attributes");
			}
			int vertexAttributesList_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				fixed (VertexAttributeDescriptor[] array = NoAllocHelpers.ExtractArrayFromList<VertexAttributeDescriptor>(attributes))
				{
					BlittableArrayWrapper blittableArrayWrapper;
					if (array.Length != 0)
					{
						blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
					}
					BlittableListWrapper blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, attributes.Count);
					vertexAttributesList_Injected = Mesh.GetVertexAttributesList_Injected(intPtr, ref blittableListWrapper);
				}
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<VertexAttributeDescriptor>(attributes);
			}
			return vertexAttributesList_Injected;
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x00024248 File Offset: 0x00022448
		[FreeFunction(Name = "MeshScripting::GetVertexAttributesCount", HasExplicitThis = true)]
		private int GetVertexAttributeCountImpl()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Mesh.GetVertexAttributeCountImpl_Injected(intPtr);
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x0002426C File Offset: 0x0002246C
		[FreeFunction(Name = "MeshScripting::GetVertexAttributeByIndex", HasExplicitThis = true, ThrowsException = true)]
		public VertexAttributeDescriptor GetVertexAttribute(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			VertexAttributeDescriptor vertexAttributeDescriptor;
			Mesh.GetVertexAttribute_Injected(intPtr, index, out vertexAttributeDescriptor);
			return vertexAttributeDescriptor;
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x00024294 File Offset: 0x00022494
		[FreeFunction(Name = "MeshScripting::GetIndexStart", HasExplicitThis = true)]
		private uint GetIndexStartImpl(int submesh)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Mesh.GetIndexStartImpl_Injected(intPtr, submesh);
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x000242B8 File Offset: 0x000224B8
		[FreeFunction(Name = "MeshScripting::GetIndexCount", HasExplicitThis = true)]
		private uint GetIndexCountImpl(int submesh)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Mesh.GetIndexCountImpl_Injected(intPtr, submesh);
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x000242DC File Offset: 0x000224DC
		[FreeFunction(Name = "MeshScripting::GetTrianglesCount", HasExplicitThis = true)]
		private uint GetTrianglesCountImpl(int submesh)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Mesh.GetTrianglesCountImpl_Injected(intPtr, submesh);
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x00024300 File Offset: 0x00022500
		[FreeFunction(Name = "MeshScripting::GetBaseVertex", HasExplicitThis = true)]
		private uint GetBaseVertexImpl(int submesh)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Mesh.GetBaseVertexImpl_Injected(intPtr, submesh);
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x00024324 File Offset: 0x00022524
		[FreeFunction(Name = "MeshScripting::GetTriangles", HasExplicitThis = true)]
		private int[] GetTrianglesImpl(int submesh, bool applyBaseVertex)
		{
			int[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				Mesh.GetTrianglesImpl_Injected(intPtr, submesh, applyBaseVertex, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				int[] array;
				blittableArrayWrapper.Unmarshal<int>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0002436C File Offset: 0x0002256C
		[FreeFunction(Name = "MeshScripting::GetIndices", HasExplicitThis = true)]
		private int[] GetIndicesImpl(int submesh, bool applyBaseVertex)
		{
			int[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				Mesh.GetIndicesImpl_Injected(intPtr, submesh, applyBaseVertex, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				int[] array;
				blittableArrayWrapper.Unmarshal<int>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x000243B4 File Offset: 0x000225B4
		[FreeFunction(Name = "SetMeshIndicesFromScript", HasExplicitThis = true, ThrowsException = true)]
		private void SetIndicesImpl(int submesh, MeshTopology topology, IndexFormat indicesFormat, Array indices, int arrayStart, int arraySize, bool calculateBounds, int baseVertex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.SetIndicesImpl_Injected(intPtr, submesh, topology, indicesFormat, indices, arrayStart, arraySize, calculateBounds, baseVertex);
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x000243E4 File Offset: 0x000225E4
		[FreeFunction(Name = "SetMeshIndicesFromNativeArray", HasExplicitThis = true, ThrowsException = true)]
		private void SetIndicesNativeArrayImpl(int submesh, MeshTopology topology, IndexFormat indicesFormat, IntPtr indices, int arrayStart, int arraySize, bool calculateBounds, int baseVertex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.SetIndicesNativeArrayImpl_Injected(intPtr, submesh, topology, indicesFormat, indices, arrayStart, arraySize, calculateBounds, baseVertex);
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x00024414 File Offset: 0x00022614
		[FreeFunction(Name = "MeshScripting::ExtractTrianglesToArray", HasExplicitThis = true)]
		private unsafe void GetTrianglesNonAllocImpl([Out] int[] values, int submesh, bool applyBaseVertex)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				if (values != null)
				{
					fixed (int[] array = values)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				Mesh.GetTrianglesNonAllocImpl_Injected(intPtr, out blittableArrayWrapper, submesh, applyBaseVertex);
			}
			finally
			{
				int[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<int>(ref array);
			}
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x00024474 File Offset: 0x00022674
		[FreeFunction(Name = "MeshScripting::ExtractTrianglesToArray16", HasExplicitThis = true)]
		private unsafe void GetTrianglesNonAllocImpl16([Out] ushort[] values, int submesh, bool applyBaseVertex)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				if (values != null)
				{
					fixed (ushort[] array = values)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				Mesh.GetTrianglesNonAllocImpl16_Injected(intPtr, out blittableArrayWrapper, submesh, applyBaseVertex);
			}
			finally
			{
				ushort[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<ushort>(ref array);
			}
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x000244D4 File Offset: 0x000226D4
		[FreeFunction(Name = "MeshScripting::ExtractIndicesToArray", HasExplicitThis = true)]
		private unsafe void GetIndicesNonAllocImpl([Out] int[] values, int submesh, bool applyBaseVertex)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				if (values != null)
				{
					fixed (int[] array = values)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				Mesh.GetIndicesNonAllocImpl_Injected(intPtr, out blittableArrayWrapper, submesh, applyBaseVertex);
			}
			finally
			{
				int[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<int>(ref array);
			}
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x00024534 File Offset: 0x00022734
		[FreeFunction(Name = "MeshScripting::ExtractIndicesToArray16", HasExplicitThis = true)]
		private unsafe void GetIndicesNonAllocImpl16([Out] ushort[] values, int submesh, bool applyBaseVertex)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				if (values != null)
				{
					fixed (ushort[] array = values)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				Mesh.GetIndicesNonAllocImpl16_Injected(intPtr, out blittableArrayWrapper, submesh, applyBaseVertex);
			}
			finally
			{
				ushort[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<ushort>(ref array);
			}
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x00024594 File Offset: 0x00022794
		[FreeFunction(Name = "MeshScripting::PrintErrorCantAccessChannel", HasExplicitThis = true)]
		private void PrintErrorCantAccessChannel(VertexAttribute ch)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.PrintErrorCantAccessChannel_Injected(intPtr, ch);
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x000245B8 File Offset: 0x000227B8
		[FreeFunction(Name = "MeshScripting::HasChannel", HasExplicitThis = true)]
		public bool HasVertexAttribute(VertexAttribute attr)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Mesh.HasVertexAttribute_Injected(intPtr, attr);
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x000245DC File Offset: 0x000227DC
		[FreeFunction(Name = "MeshScripting::GetChannelDimension", HasExplicitThis = true)]
		public int GetVertexAttributeDimension(VertexAttribute attr)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Mesh.GetVertexAttributeDimension_Injected(intPtr, attr);
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x00024600 File Offset: 0x00022800
		[FreeFunction(Name = "MeshScripting::GetChannelFormat", HasExplicitThis = true)]
		public VertexAttributeFormat GetVertexAttributeFormat(VertexAttribute attr)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Mesh.GetVertexAttributeFormat_Injected(intPtr, attr);
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x00024624 File Offset: 0x00022824
		[FreeFunction(Name = "MeshScripting::GetChannelStream", HasExplicitThis = true)]
		public int GetVertexAttributeStream(VertexAttribute attr)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Mesh.GetVertexAttributeStream_Injected(intPtr, attr);
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x00024648 File Offset: 0x00022848
		[FreeFunction(Name = "MeshScripting::GetChannelOffset", HasExplicitThis = true)]
		public int GetVertexAttributeOffset(VertexAttribute attr)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Mesh.GetVertexAttributeOffset_Injected(intPtr, attr);
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x0002466C File Offset: 0x0002286C
		[FreeFunction(Name = "SetMeshComponentFromArrayFromScript", HasExplicitThis = true)]
		private void SetArrayForChannelImpl(VertexAttribute channel, VertexAttributeFormat format, int dim, Array values, int arraySize, int valuesStart, int valuesCount, MeshUpdateFlags flags)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.SetArrayForChannelImpl_Injected(intPtr, channel, format, dim, values, arraySize, valuesStart, valuesCount, flags);
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x0002469C File Offset: 0x0002289C
		[FreeFunction(Name = "SetMeshComponentFromNativeArrayFromScript", HasExplicitThis = true)]
		private void SetNativeArrayForChannelImpl(VertexAttribute channel, VertexAttributeFormat format, int dim, IntPtr values, int arraySize, int valuesStart, int valuesCount, MeshUpdateFlags flags)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.SetNativeArrayForChannelImpl_Injected(intPtr, channel, format, dim, values, arraySize, valuesStart, valuesCount, flags);
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x000246CC File Offset: 0x000228CC
		[FreeFunction(Name = "AllocExtractMeshComponentFromScript", HasExplicitThis = true)]
		private Array GetAllocArrayFromChannelImpl(VertexAttribute channel, VertexAttributeFormat format, int dim)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Mesh.GetAllocArrayFromChannelImpl_Injected(intPtr, channel, format, dim);
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x000246F4 File Offset: 0x000228F4
		[FreeFunction(Name = "ExtractMeshComponentFromScript", HasExplicitThis = true)]
		private void GetArrayFromChannelImpl(VertexAttribute channel, VertexAttributeFormat format, int dim, Array values)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.GetArrayFromChannelImpl_Injected(intPtr, channel, format, dim, values);
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x0002471C File Offset: 0x0002291C
		public int vertexBufferCount
		{
			[FreeFunction(Name = "MeshScripting::GetVertexBufferCount", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Mesh.get_vertexBufferCount_Injected(intPtr);
			}
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x00024740 File Offset: 0x00022940
		[FreeFunction(Name = "MeshScripting::GetVertexBufferStride", HasExplicitThis = true)]
		public int GetVertexBufferStride(int stream)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Mesh.GetVertexBufferStride_Injected(intPtr, stream);
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x00024764 File Offset: 0x00022964
		[FreeFunction(Name = "MeshScripting::GetNativeVertexBufferPtr", HasExplicitThis = true)]
		[NativeThrows]
		public IntPtr GetNativeVertexBufferPtr(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Mesh.GetNativeVertexBufferPtr_Injected(intPtr, index);
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x00024788 File Offset: 0x00022988
		[FreeFunction(Name = "MeshScripting::GetNativeIndexBufferPtr", HasExplicitThis = true)]
		public IntPtr GetNativeIndexBufferPtr()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Mesh.GetNativeIndexBufferPtr_Injected(intPtr);
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x000247AC File Offset: 0x000229AC
		[FreeFunction(Name = "MeshScripting::GetVertexBufferPtr", HasExplicitThis = true, ThrowsException = true)]
		private GraphicsBuffer GetVertexBufferImpl(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr vertexBufferImpl_Injected = Mesh.GetVertexBufferImpl_Injected(intPtr, index);
			return (vertexBufferImpl_Injected == 0) ? null : GraphicsBuffer.BindingsMarshaller.ConvertToManaged(vertexBufferImpl_Injected);
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x000247E0 File Offset: 0x000229E0
		[FreeFunction(Name = "MeshScripting::GetIndexBufferPtr", HasExplicitThis = true, ThrowsException = true)]
		private GraphicsBuffer GetIndexBufferImpl()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr indexBufferImpl_Injected = Mesh.GetIndexBufferImpl_Injected(intPtr);
			return (indexBufferImpl_Injected == 0) ? null : GraphicsBuffer.BindingsMarshaller.ConvertToManaged(indexBufferImpl_Injected);
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x00024810 File Offset: 0x00022A10
		[FreeFunction(Name = "MeshScripting::GetBoneWeightBufferPtr", HasExplicitThis = true, ThrowsException = true)]
		private GraphicsBuffer GetBoneWeightBufferImpl(int bonesPerVertex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr boneWeightBufferImpl_Injected = Mesh.GetBoneWeightBufferImpl_Injected(intPtr, bonesPerVertex);
			return (boneWeightBufferImpl_Injected == 0) ? null : GraphicsBuffer.BindingsMarshaller.ConvertToManaged(boneWeightBufferImpl_Injected);
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x00024844 File Offset: 0x00022A44
		[FreeFunction(Name = "MeshScripting::GetBlendShapeBufferPtr", HasExplicitThis = true, ThrowsException = true)]
		private GraphicsBuffer GetBlendShapeBufferImpl(int layout)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr blendShapeBufferImpl_Injected = Mesh.GetBlendShapeBufferImpl_Injected(intPtr, layout);
			return (blendShapeBufferImpl_Injected == 0) ? null : GraphicsBuffer.BindingsMarshaller.ConvertToManaged(blendShapeBufferImpl_Injected);
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x00024878 File Offset: 0x00022A78
		// (set) Token: 0x06001587 RID: 5511 RVA: 0x0002489C File Offset: 0x00022A9C
		public GraphicsBuffer.Target vertexBufferTarget
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Mesh.get_vertexBufferTarget_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Mesh.set_vertexBufferTarget_Injected(intPtr, value);
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001588 RID: 5512 RVA: 0x000248C0 File Offset: 0x00022AC0
		// (set) Token: 0x06001589 RID: 5513 RVA: 0x000248E4 File Offset: 0x00022AE4
		public GraphicsBuffer.Target indexBufferTarget
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Mesh.get_indexBufferTarget_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Mesh.set_indexBufferTarget_Injected(intPtr, value);
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x0600158A RID: 5514 RVA: 0x00024908 File Offset: 0x00022B08
		public int blendShapeCount
		{
			[NativeMethod(Name = "GetBlendShapeChannelCount")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Mesh.get_blendShapeCount_Injected(intPtr);
			}
		}

		// Token: 0x0600158B RID: 5515 RVA: 0x0002492C File Offset: 0x00022B2C
		[FreeFunction(Name = "MeshScripting::ClearBlendShapes", HasExplicitThis = true)]
		public void ClearBlendShapes()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.ClearBlendShapes_Injected(intPtr);
		}

		// Token: 0x0600158C RID: 5516 RVA: 0x00024950 File Offset: 0x00022B50
		[FreeFunction(Name = "MeshScripting::GetBlendShapeName", HasExplicitThis = true, ThrowsException = true)]
		public string GetBlendShapeName(int shapeIndex)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				Mesh.GetBlendShapeName_Injected(intPtr, shapeIndex, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x0600158D RID: 5517 RVA: 0x00024990 File Offset: 0x00022B90
		[FreeFunction(Name = "MeshScripting::GetBlendShapeIndex", HasExplicitThis = true, ThrowsException = true)]
		public unsafe int GetBlendShapeIndex(string blendShapeName)
		{
			int blendShapeIndex_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(blendShapeName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = blendShapeName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				blendShapeIndex_Injected = Mesh.GetBlendShapeIndex_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return blendShapeIndex_Injected;
		}

		// Token: 0x0600158E RID: 5518 RVA: 0x000249F8 File Offset: 0x00022BF8
		[FreeFunction(Name = "MeshScripting::GetBlendShapeFrameCount", HasExplicitThis = true, ThrowsException = true)]
		public int GetBlendShapeFrameCount(int shapeIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Mesh.GetBlendShapeFrameCount_Injected(intPtr, shapeIndex);
		}

		// Token: 0x0600158F RID: 5519 RVA: 0x00024A1C File Offset: 0x00022C1C
		[FreeFunction(Name = "MeshScripting::GetBlendShapeFrameWeight", HasExplicitThis = true, ThrowsException = true)]
		public float GetBlendShapeFrameWeight(int shapeIndex, int frameIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Mesh.GetBlendShapeFrameWeight_Injected(intPtr, shapeIndex, frameIndex);
		}

		// Token: 0x06001590 RID: 5520 RVA: 0x00024A40 File Offset: 0x00022C40
		[FreeFunction(Name = "GetBlendShapeFrameVerticesFromScript", HasExplicitThis = true, ThrowsException = true)]
		public unsafe void GetBlendShapeFrameVertices(int shapeIndex, int frameIndex, Vector3[] deltaVertices, Vector3[] deltaNormals, Vector3[] deltaTangents)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<Vector3> span = new Span<Vector3>(deltaVertices);
			fixed (Vector3* ptr = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, span.Length);
				Span<Vector3> span2 = new Span<Vector3>(deltaNormals);
				fixed (Vector3* ptr2 = span2.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, span2.Length);
					Span<Vector3> span3 = new Span<Vector3>(deltaTangents);
					fixed (Vector3* pinnableReference = span3.GetPinnableReference())
					{
						ManagedSpanWrapper managedSpanWrapper3 = new ManagedSpanWrapper((void*)pinnableReference, span3.Length);
						Mesh.GetBlendShapeFrameVertices_Injected(intPtr, shapeIndex, frameIndex, ref managedSpanWrapper, ref managedSpanWrapper2, ref managedSpanWrapper3);
						ptr = null;
						ptr2 = null;
					}
				}
			}
		}

		// Token: 0x06001591 RID: 5521 RVA: 0x00024AE0 File Offset: 0x00022CE0
		[FreeFunction(Name = "AddBlendShapeFrameFromScript", HasExplicitThis = true, ThrowsException = true)]
		public unsafe void AddBlendShapeFrame(string shapeName, float frameWeight, Vector3[] deltaVertices, Vector3[] deltaNormals, Vector3[] deltaTangents)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(shapeName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = shapeName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Span<Vector3> span = new Span<Vector3>(deltaVertices);
				fixed (Vector3* ptr2 = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper2 = new ManagedSpanWrapper((void*)ptr2, span.Length);
					Span<Vector3> span2 = new Span<Vector3>(deltaNormals);
					fixed (Vector3* ptr3 = span2.GetPinnableReference())
					{
						ManagedSpanWrapper managedSpanWrapper3 = new ManagedSpanWrapper((void*)ptr3, span2.Length);
						Span<Vector3> span3 = new Span<Vector3>(deltaTangents);
						fixed (Vector3* ptr4 = span3.GetPinnableReference())
						{
							ManagedSpanWrapper managedSpanWrapper4 = new ManagedSpanWrapper((void*)ptr4, span3.Length);
							Mesh.AddBlendShapeFrame_Injected(intPtr, ref managedSpanWrapper, frameWeight, ref managedSpanWrapper2, ref managedSpanWrapper3, ref managedSpanWrapper4);
						}
					}
				}
			}
			finally
			{
				char* ptr = null;
				Vector3* ptr2 = null;
				Vector3* ptr3 = null;
				Vector3* ptr4 = null;
			}
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x00024BC4 File Offset: 0x00022DC4
		[FreeFunction(Name = "MeshScripting::GetBlendShapeOffset", HasExplicitThis = true)]
		private BlendShape GetBlendShapeOffsetInternal(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			BlendShape blendShape;
			Mesh.GetBlendShapeOffsetInternal_Injected(intPtr, index, out blendShape);
			return blendShape;
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x00024BEC File Offset: 0x00022DEC
		[NativeMethod("HasBoneWeights")]
		private bool HasBoneWeights()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Mesh.HasBoneWeights_Injected(intPtr);
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x00024C10 File Offset: 0x00022E10
		[FreeFunction(Name = "MeshScripting::GetBoneWeights", HasExplicitThis = true)]
		private BoneWeight[] GetBoneWeightsImpl()
		{
			BoneWeight[] array2;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				Mesh.GetBoneWeightsImpl_Injected(intPtr, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				BoneWeight[] array;
				blittableArrayWrapper.Unmarshal<BoneWeight>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x00024C54 File Offset: 0x00022E54
		[FreeFunction(Name = "MeshScripting::SetBoneWeights", HasExplicitThis = true)]
		private unsafe void SetBoneWeightsImpl(BoneWeight[] weights)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<BoneWeight> span = new Span<BoneWeight>(weights);
			fixed (BoneWeight* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Mesh.SetBoneWeightsImpl_Injected(intPtr, ref managedSpanWrapper);
			}
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x00024C9C File Offset: 0x00022E9C
		public void SetBoneWeights(NativeArray<byte> bonesPerVertex, NativeArray<BoneWeight1> weights)
		{
			this.InternalSetBoneWeights((IntPtr)bonesPerVertex.GetUnsafeReadOnlyPtr<byte>(), bonesPerVertex.Length, (IntPtr)weights.GetUnsafeReadOnlyPtr<BoneWeight1>(), weights.Length);
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x00024CCC File Offset: 0x00022ECC
		[FreeFunction(Name = "MeshScripting::SetBoneWeights", HasExplicitThis = true)]
		[SecurityCritical]
		private void InternalSetBoneWeights(IntPtr bonesPerVertex, int bonesPerVertexSize, IntPtr weights, int weightsSize)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.InternalSetBoneWeights_Injected(intPtr, bonesPerVertex, bonesPerVertexSize, weights, weightsSize);
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x00024CF4 File Offset: 0x00022EF4
		public unsafe NativeArray<BoneWeight1> GetAllBoneWeights()
		{
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<BoneWeight1>((void*)this.GetAllBoneWeightsArray(), this.GetAllBoneWeightsArraySize(), Allocator.None);
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x00024D20 File Offset: 0x00022F20
		public unsafe NativeArray<byte> GetBonesPerVertex()
		{
			int num = (this.HasBoneWeights() ? this.vertexCount : 0);
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>((void*)this.GetBonesPerVertexArray(), num, Allocator.None);
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x00024D58 File Offset: 0x00022F58
		[FreeFunction(Name = "MeshScripting::GetAllBoneWeightsArraySize", HasExplicitThis = true)]
		private int GetAllBoneWeightsArraySize()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Mesh.GetAllBoneWeightsArraySize_Injected(intPtr);
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x00024D7C File Offset: 0x00022F7C
		[NativeMethod("GetBoneWeightBufferDimension")]
		private int GetBoneWeightBufferLayoutInternal()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Mesh.GetBoneWeightBufferLayoutInternal_Injected(intPtr);
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x00024DA0 File Offset: 0x00022FA0
		[FreeFunction(Name = "MeshScripting::GetAllBoneWeightsArray", HasExplicitThis = true)]
		[SecurityCritical]
		private IntPtr GetAllBoneWeightsArray()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Mesh.GetAllBoneWeightsArray_Injected(intPtr);
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x00024DC4 File Offset: 0x00022FC4
		[SecurityCritical]
		[FreeFunction(Name = "MeshScripting::GetBonesPerVertexArray", HasExplicitThis = true)]
		private IntPtr GetBonesPerVertexArray()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Mesh.GetBonesPerVertexArray_Injected(intPtr);
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x00024DE8 File Offset: 0x00022FE8
		public int bindposeCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Mesh.get_bindposeCount_Injected(intPtr);
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x0600159F RID: 5535 RVA: 0x00024E0C File Offset: 0x0002300C
		// (set) Token: 0x060015A0 RID: 5536 RVA: 0x00024E50 File Offset: 0x00023050
		[NativeName("BindPosesFromScript")]
		public unsafe Matrix4x4[] bindposes
		{
			get
			{
				Matrix4x4[] array2;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					BlittableArrayWrapper blittableArrayWrapper;
					Mesh.get_bindposes_Injected(intPtr, out blittableArrayWrapper);
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
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Span<Matrix4x4> span = new Span<Matrix4x4>(value);
				fixed (Matrix4x4* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					Mesh.set_bindposes_Injected(intPtr, ref managedSpanWrapper);
				}
			}
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x00024E98 File Offset: 0x00023098
		public unsafe NativeArray<Matrix4x4> GetBindposes()
		{
			return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<Matrix4x4>((void*)this.GetBindposesArray(), this.bindposeCount, Allocator.None);
		}

		// Token: 0x060015A2 RID: 5538 RVA: 0x00024EC4 File Offset: 0x000230C4
		public void SetBindposes(NativeArray<Matrix4x4> poses)
		{
			bool flag = !poses.IsCreated || poses.Length == 0;
			if (flag)
			{
				throw new ArgumentException("Cannot set bindposes as the native poses array is empty.", "poses");
			}
			this.SetBindposesFromScript_NativeArray((IntPtr)poses.GetUnsafeReadOnlyPtr<Matrix4x4>(), poses.Length);
		}

		// Token: 0x060015A3 RID: 5539 RVA: 0x00024F18 File Offset: 0x00023118
		[NativeMethod("SetBindposes")]
		private void SetBindposesFromScript_NativeArray(IntPtr posesPtr, int posesCount)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.SetBindposesFromScript_NativeArray_Injected(intPtr, posesPtr, posesCount);
		}

		// Token: 0x060015A4 RID: 5540 RVA: 0x00024F3C File Offset: 0x0002313C
		[SecurityCritical]
		[FreeFunction(Name = "MeshScripting::GetBindposesArray", HasExplicitThis = true)]
		private IntPtr GetBindposesArray()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Mesh.GetBindposesArray_Injected(intPtr);
		}

		// Token: 0x060015A5 RID: 5541 RVA: 0x00024F60 File Offset: 0x00023160
		[FreeFunction(Name = "MeshScripting::ExtractBoneWeightsIntoArray", HasExplicitThis = true)]
		private unsafe void GetBoneWeightsNonAllocImpl([Out] BoneWeight[] values)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				if (values != null)
				{
					fixed (BoneWeight[] array = values)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				Mesh.GetBoneWeightsNonAllocImpl_Injected(intPtr, out blittableArrayWrapper);
			}
			finally
			{
				BoneWeight[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<BoneWeight>(ref array);
			}
		}

		// Token: 0x060015A6 RID: 5542 RVA: 0x00024FC0 File Offset: 0x000231C0
		[FreeFunction(Name = "MeshScripting::ExtractBindPosesIntoArray", HasExplicitThis = true)]
		private unsafe void GetBindposesNonAllocImpl([Out] Matrix4x4[] values)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				if (values != null)
				{
					fixed (Matrix4x4[] array = values)
					{
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
					}
				}
				Mesh.GetBindposesNonAllocImpl_Injected(intPtr, out blittableArrayWrapper);
			}
			finally
			{
				Matrix4x4[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<Matrix4x4>(ref array);
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060015A7 RID: 5543 RVA: 0x00025020 File Offset: 0x00023220
		public bool isReadable
		{
			[NativeMethod("GetIsReadable")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Mesh.get_isReadable_Injected(intPtr);
			}
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060015A8 RID: 5544 RVA: 0x00025044 File Offset: 0x00023244
		internal bool canAccess
		{
			[NativeMethod("CanAccessFromScript")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Mesh.get_canAccess_Injected(intPtr);
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060015A9 RID: 5545 RVA: 0x00025068 File Offset: 0x00023268
		public int vertexCount
		{
			[NativeMethod("GetVertexCount")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Mesh.get_vertexCount_Injected(intPtr);
			}
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060015AA RID: 5546 RVA: 0x0002508C File Offset: 0x0002328C
		// (set) Token: 0x060015AB RID: 5547 RVA: 0x000250B0 File Offset: 0x000232B0
		public int subMeshCount
		{
			[NativeMethod(Name = "GetSubMeshCount")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Mesh.get_subMeshCount_Injected(intPtr);
			}
			[FreeFunction(Name = "MeshScripting::SetSubMeshCount", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Mesh.set_subMeshCount_Injected(intPtr, value);
			}
		}

		// Token: 0x060015AC RID: 5548 RVA: 0x000250D4 File Offset: 0x000232D4
		[FreeFunction("MeshScripting::SetSubMesh", HasExplicitThis = true, ThrowsException = true)]
		public void SetSubMesh(int index, SubMeshDescriptor desc, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.SetSubMesh_Injected(intPtr, index, ref desc, flags);
		}

		// Token: 0x060015AD RID: 5549 RVA: 0x000250FC File Offset: 0x000232FC
		[FreeFunction("MeshScripting::GetSubMesh", HasExplicitThis = true, ThrowsException = true)]
		public SubMeshDescriptor GetSubMesh(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			SubMeshDescriptor subMeshDescriptor;
			Mesh.GetSubMesh_Injected(intPtr, index, out subMeshDescriptor);
			return subMeshDescriptor;
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x00025124 File Offset: 0x00023324
		[FreeFunction("MeshScripting::SetAllSubMeshesAtOnceFromArray", HasExplicitThis = true, ThrowsException = true)]
		private unsafe void SetAllSubMeshesAtOnceFromArray(SubMeshDescriptor[] desc, int start, int count, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<SubMeshDescriptor> span = new Span<SubMeshDescriptor>(desc);
			fixed (SubMeshDescriptor* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Mesh.SetAllSubMeshesAtOnceFromArray_Injected(intPtr, ref managedSpanWrapper, start, count, flags);
			}
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x00025170 File Offset: 0x00023370
		[FreeFunction("MeshScripting::SetAllSubMeshesAtOnceFromNativeArray", HasExplicitThis = true, ThrowsException = true)]
		private void SetAllSubMeshesAtOnceFromNativeArray(IntPtr desc, int start, int count, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.SetAllSubMeshesAtOnceFromNativeArray_Injected(intPtr, desc, start, count, flags);
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x00025198 File Offset: 0x00023398
		// (set) Token: 0x060015B1 RID: 5553 RVA: 0x000251C0 File Offset: 0x000233C0
		public Bounds bounds
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Bounds bounds;
				Mesh.get_bounds_Injected(intPtr, out bounds);
				return bounds;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Mesh.set_bounds_Injected(intPtr, ref value);
			}
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x000251E4 File Offset: 0x000233E4
		[NativeMethod("Clear")]
		private void ClearImpl(bool keepVertexLayout)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.ClearImpl_Injected(intPtr, keepVertexLayout);
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x00025208 File Offset: 0x00023408
		[NativeMethod("RecalculateBounds")]
		private void RecalculateBoundsImpl(MeshUpdateFlags flags)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.RecalculateBoundsImpl_Injected(intPtr, flags);
		}

		// Token: 0x060015B4 RID: 5556 RVA: 0x0002522C File Offset: 0x0002342C
		[NativeMethod("RecalculateNormals")]
		private void RecalculateNormalsImpl(MeshUpdateFlags flags)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.RecalculateNormalsImpl_Injected(intPtr, flags);
		}

		// Token: 0x060015B5 RID: 5557 RVA: 0x00025250 File Offset: 0x00023450
		[NativeMethod("RecalculateTangents")]
		private void RecalculateTangentsImpl(MeshUpdateFlags flags)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.RecalculateTangentsImpl_Injected(intPtr, flags);
		}

		// Token: 0x060015B6 RID: 5558 RVA: 0x00025274 File Offset: 0x00023474
		[NativeMethod("MarkDynamic")]
		private void MarkDynamicImpl()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.MarkDynamicImpl_Injected(intPtr);
		}

		// Token: 0x060015B7 RID: 5559 RVA: 0x00025298 File Offset: 0x00023498
		[NativeMethod("MarkModified")]
		public void MarkModified()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.MarkModified_Injected(intPtr);
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x000252BC File Offset: 0x000234BC
		[NativeMethod("UploadMeshData")]
		private void UploadMeshDataImpl(bool markNoLongerReadable)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.UploadMeshDataImpl_Injected(intPtr, markNoLongerReadable);
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x000252E0 File Offset: 0x000234E0
		[FreeFunction(Name = "MeshScripting::GetPrimitiveType", HasExplicitThis = true)]
		private MeshTopology GetTopologyImpl(int submesh)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Mesh.GetTopologyImpl_Injected(intPtr, submesh);
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x00025304 File Offset: 0x00023504
		[NativeMethod("RecalculateMeshMetric")]
		private void RecalculateUVDistributionMetricImpl(int uvSetIndex, float uvAreaThreshold)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.RecalculateUVDistributionMetricImpl_Injected(intPtr, uvSetIndex, uvAreaThreshold);
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x00025328 File Offset: 0x00023528
		[NativeMethod("RecalculateMeshMetrics")]
		private void RecalculateUVDistributionMetricsImpl(float uvAreaThreshold)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.RecalculateUVDistributionMetricsImpl_Injected(intPtr, uvAreaThreshold);
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x0002534C File Offset: 0x0002354C
		[NativeMethod("GetMeshMetric")]
		public float GetUVDistributionMetric(int uvSetIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Mesh.GetUVDistributionMetric_Injected(intPtr, uvSetIndex);
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x00025370 File Offset: 0x00023570
		[NativeMethod(Name = "MeshScripting::CombineMeshes", IsFreeFunction = true, ThrowsException = true, HasExplicitThis = true)]
		private unsafe void CombineMeshesImpl(CombineInstance[] combine, bool mergeSubMeshes, bool useMatrices, bool hasLightmapData)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Span<CombineInstance> span = new Span<CombineInstance>(combine);
			fixed (CombineInstance* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Mesh.CombineMeshesImpl_Injected(intPtr, ref managedSpanWrapper, mergeSubMeshes, useMatrices, hasLightmapData);
			}
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x000253BC File Offset: 0x000235BC
		[NativeMethod("Optimize")]
		private void OptimizeImpl()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.OptimizeImpl_Injected(intPtr);
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x000253E0 File Offset: 0x000235E0
		[NativeMethod("OptimizeIndexBuffers")]
		private void OptimizeIndexBuffersImpl()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.OptimizeIndexBuffersImpl_Injected(intPtr);
		}

		// Token: 0x060015C0 RID: 5568 RVA: 0x00025404 File Offset: 0x00023604
		[NativeMethod("OptimizeReorderVertexBuffer")]
		private void OptimizeReorderVertexBufferImpl()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Mesh.OptimizeReorderVertexBufferImpl_Injected(intPtr);
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x00025428 File Offset: 0x00023628
		internal static VertexAttribute GetUVChannel(int uvIndex)
		{
			bool flag = uvIndex < 0 || uvIndex > 7;
			if (flag)
			{
				throw new ArgumentException("GetUVChannel called for bad uvIndex", "uvIndex");
			}
			return VertexAttribute.TexCoord0 + uvIndex;
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x0002545C File Offset: 0x0002365C
		internal static int DefaultDimensionForChannel(VertexAttribute channel)
		{
			bool flag = channel == VertexAttribute.Position || channel == VertexAttribute.Normal;
			int num;
			if (flag)
			{
				num = 3;
			}
			else
			{
				bool flag2 = channel >= VertexAttribute.TexCoord0 && channel <= VertexAttribute.TexCoord7;
				if (flag2)
				{
					num = 2;
				}
				else
				{
					bool flag3 = channel == VertexAttribute.Tangent || channel == VertexAttribute.Color;
					if (!flag3)
					{
						throw new ArgumentException("DefaultDimensionForChannel called for bad channel", "channel");
					}
					num = 4;
				}
			}
			return num;
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x000254B8 File Offset: 0x000236B8
		private T[] GetAllocArrayFromChannel<T>(VertexAttribute channel, VertexAttributeFormat format, int dim)
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				bool flag = this.HasVertexAttribute(channel);
				if (flag)
				{
					return (T[])this.GetAllocArrayFromChannelImpl(channel, format, dim);
				}
			}
			else
			{
				this.PrintErrorCantAccessChannel(channel);
			}
			return new T[0];
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x00025504 File Offset: 0x00023704
		private T[] GetAllocArrayFromChannel<T>(VertexAttribute channel)
		{
			return this.GetAllocArrayFromChannel<T>(channel, VertexAttributeFormat.Float32, Mesh.DefaultDimensionForChannel(channel));
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x00025524 File Offset: 0x00023724
		private void SetSizedArrayForChannel(VertexAttribute channel, VertexAttributeFormat format, int dim, Array values, int valuesArrayLength, int valuesStart, int valuesCount, MeshUpdateFlags flags)
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				bool flag = valuesStart < 0;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("valuesStart", valuesStart, "Mesh data array start index can't be negative.");
				}
				bool flag2 = valuesCount < 0;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("valuesCount", valuesCount, "Mesh data array length can't be negative.");
				}
				bool flag3 = valuesStart >= valuesArrayLength && valuesCount != 0;
				if (flag3)
				{
					throw new ArgumentOutOfRangeException("valuesStart", valuesStart, "Mesh data array start is outside of array size.");
				}
				bool flag4 = valuesStart + valuesCount > valuesArrayLength;
				if (flag4)
				{
					throw new ArgumentOutOfRangeException("valuesCount", valuesStart + valuesCount, "Mesh data array start+count is outside of array size.");
				}
				bool flag5 = values == null;
				if (flag5)
				{
					valuesStart = 0;
				}
				this.SetArrayForChannelImpl(channel, format, dim, values, valuesArrayLength, valuesStart, valuesCount, flags);
			}
			else
			{
				this.PrintErrorCantAccessChannel(channel);
			}
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x00025600 File Offset: 0x00023800
		private void SetSizedNativeArrayForChannel(VertexAttribute channel, VertexAttributeFormat format, int dim, IntPtr values, int valuesArrayLength, int valuesStart, int valuesCount, MeshUpdateFlags flags)
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				bool flag = valuesStart < 0;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("valuesStart", valuesStart, "Mesh data array start index can't be negative.");
				}
				bool flag2 = valuesCount < 0;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("valuesCount", valuesCount, "Mesh data array length can't be negative.");
				}
				bool flag3 = valuesStart >= valuesArrayLength && valuesCount != 0;
				if (flag3)
				{
					throw new ArgumentOutOfRangeException("valuesStart", valuesStart, "Mesh data array start is outside of array size.");
				}
				bool flag4 = valuesStart + valuesCount > valuesArrayLength;
				if (flag4)
				{
					throw new ArgumentOutOfRangeException("valuesCount", valuesStart + valuesCount, "Mesh data array start+count is outside of array size.");
				}
				this.SetNativeArrayForChannelImpl(channel, format, dim, values, valuesArrayLength, valuesStart, valuesCount, flags);
			}
			else
			{
				this.PrintErrorCantAccessChannel(channel);
			}
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x000256D0 File Offset: 0x000238D0
		private void SetArrayForChannel<T>(VertexAttribute channel, VertexAttributeFormat format, int dim, T[] values, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			int num = NoAllocHelpers.SafeLength(values);
			this.SetSizedArrayForChannel(channel, format, dim, values, num, 0, num, flags);
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x000256F8 File Offset: 0x000238F8
		private void SetArrayForChannel<T>(VertexAttribute channel, T[] values, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			int num = NoAllocHelpers.SafeLength(values);
			this.SetSizedArrayForChannel(channel, VertexAttributeFormat.Float32, Mesh.DefaultDimensionForChannel(channel), values, num, 0, num, flags);
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x00025724 File Offset: 0x00023924
		private void SetListForChannel<T>(VertexAttribute channel, VertexAttributeFormat format, int dim, List<T> values, int start, int length, MeshUpdateFlags flags)
		{
			this.SetSizedArrayForChannel(channel, format, dim, NoAllocHelpers.ExtractArrayFromList<T>(values), NoAllocHelpers.SafeLength<T>(values), start, length, flags);
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x00025750 File Offset: 0x00023950
		private void SetListForChannel<T>(VertexAttribute channel, List<T> values, int start, int length, MeshUpdateFlags flags)
		{
			this.SetSizedArrayForChannel(channel, VertexAttributeFormat.Float32, Mesh.DefaultDimensionForChannel(channel), NoAllocHelpers.ExtractArrayFromList<T>(values), NoAllocHelpers.SafeLength<T>(values), start, length, flags);
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x0002577E File Offset: 0x0002397E
		private void GetListForChannel<T>(List<T> buffer, int capacity, VertexAttribute channel, int dim)
		{
			this.GetListForChannel<T>(buffer, capacity, channel, dim, VertexAttributeFormat.Float32);
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x00025790 File Offset: 0x00023990
		private void GetListForChannel<T>(List<T> buffer, int capacity, VertexAttribute channel, int dim, VertexAttributeFormat channelType)
		{
			buffer.Clear();
			bool flag = !this.canAccess;
			if (flag)
			{
				this.PrintErrorCantAccessChannel(channel);
			}
			else
			{
				bool flag2 = !this.HasVertexAttribute(channel);
				if (!flag2)
				{
					NoAllocHelpers.EnsureListElemCount<T>(buffer, capacity);
					this.GetArrayFromChannelImpl(channel, channelType, dim, NoAllocHelpers.ExtractArrayFromList<T>(buffer));
				}
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x060015CD RID: 5581 RVA: 0x000257E8 File Offset: 0x000239E8
		// (set) Token: 0x060015CE RID: 5582 RVA: 0x00025801 File Offset: 0x00023A01
		public Vector3[] vertices
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector3>(VertexAttribute.Position);
			}
			set
			{
				this.SetArrayForChannel<Vector3>(VertexAttribute.Position, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x060015CF RID: 5583 RVA: 0x00025810 File Offset: 0x00023A10
		// (set) Token: 0x060015D0 RID: 5584 RVA: 0x00025829 File Offset: 0x00023A29
		public Vector3[] normals
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector3>(VertexAttribute.Normal);
			}
			set
			{
				this.SetArrayForChannel<Vector3>(VertexAttribute.Normal, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x00025838 File Offset: 0x00023A38
		// (set) Token: 0x060015D2 RID: 5586 RVA: 0x00025851 File Offset: 0x00023A51
		public Vector4[] tangents
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector4>(VertexAttribute.Tangent);
			}
			set
			{
				this.SetArrayForChannel<Vector4>(VertexAttribute.Tangent, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x060015D3 RID: 5587 RVA: 0x00025860 File Offset: 0x00023A60
		// (set) Token: 0x060015D4 RID: 5588 RVA: 0x00025879 File Offset: 0x00023A79
		public Vector2[] uv
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord0);
			}
			set
			{
				this.SetArrayForChannel<Vector2>(VertexAttribute.TexCoord0, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x060015D5 RID: 5589 RVA: 0x00025888 File Offset: 0x00023A88
		// (set) Token: 0x060015D6 RID: 5590 RVA: 0x000258A1 File Offset: 0x00023AA1
		public Vector2[] uv2
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord1);
			}
			set
			{
				this.SetArrayForChannel<Vector2>(VertexAttribute.TexCoord1, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x060015D7 RID: 5591 RVA: 0x000258B0 File Offset: 0x00023AB0
		// (set) Token: 0x060015D8 RID: 5592 RVA: 0x000258C9 File Offset: 0x00023AC9
		public Vector2[] uv3
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord2);
			}
			set
			{
				this.SetArrayForChannel<Vector2>(VertexAttribute.TexCoord2, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x060015D9 RID: 5593 RVA: 0x000258D8 File Offset: 0x00023AD8
		// (set) Token: 0x060015DA RID: 5594 RVA: 0x000258F1 File Offset: 0x00023AF1
		public Vector2[] uv4
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord3);
			}
			set
			{
				this.SetArrayForChannel<Vector2>(VertexAttribute.TexCoord3, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x060015DB RID: 5595 RVA: 0x00025900 File Offset: 0x00023B00
		// (set) Token: 0x060015DC RID: 5596 RVA: 0x00025919 File Offset: 0x00023B19
		public Vector2[] uv5
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord4);
			}
			set
			{
				this.SetArrayForChannel<Vector2>(VertexAttribute.TexCoord4, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x060015DD RID: 5597 RVA: 0x00025928 File Offset: 0x00023B28
		// (set) Token: 0x060015DE RID: 5598 RVA: 0x00025942 File Offset: 0x00023B42
		public Vector2[] uv6
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord5);
			}
			set
			{
				this.SetArrayForChannel<Vector2>(VertexAttribute.TexCoord5, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x060015DF RID: 5599 RVA: 0x00025950 File Offset: 0x00023B50
		// (set) Token: 0x060015E0 RID: 5600 RVA: 0x0002596A File Offset: 0x00023B6A
		public Vector2[] uv7
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord6);
			}
			set
			{
				this.SetArrayForChannel<Vector2>(VertexAttribute.TexCoord6, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x060015E1 RID: 5601 RVA: 0x00025978 File Offset: 0x00023B78
		// (set) Token: 0x060015E2 RID: 5602 RVA: 0x00025992 File Offset: 0x00023B92
		public Vector2[] uv8
		{
			get
			{
				return this.GetAllocArrayFromChannel<Vector2>(VertexAttribute.TexCoord7);
			}
			set
			{
				this.SetArrayForChannel<Vector2>(VertexAttribute.TexCoord7, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x060015E3 RID: 5603 RVA: 0x000259A0 File Offset: 0x00023BA0
		// (set) Token: 0x060015E4 RID: 5604 RVA: 0x000259B9 File Offset: 0x00023BB9
		public Color[] colors
		{
			get
			{
				return this.GetAllocArrayFromChannel<Color>(VertexAttribute.Color);
			}
			set
			{
				this.SetArrayForChannel<Color>(VertexAttribute.Color, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x060015E5 RID: 5605 RVA: 0x000259C8 File Offset: 0x00023BC8
		// (set) Token: 0x060015E6 RID: 5606 RVA: 0x000259E3 File Offset: 0x00023BE3
		public Color32[] colors32
		{
			get
			{
				return this.GetAllocArrayFromChannel<Color32>(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4);
			}
			set
			{
				this.SetArrayForChannel<Color32>(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4, value, MeshUpdateFlags.Default);
			}
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x000259F4 File Offset: 0x00023BF4
		public void GetVertices(List<Vector3> vertices)
		{
			bool flag = vertices == null;
			if (flag)
			{
				throw new ArgumentNullException("vertices", "The result vertices list cannot be null.");
			}
			this.GetListForChannel<Vector3>(vertices, this.vertexCount, VertexAttribute.Position, Mesh.DefaultDimensionForChannel(VertexAttribute.Position));
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x00025A2F File Offset: 0x00023C2F
		public void SetVertices(List<Vector3> inVertices)
		{
			this.SetVertices(inVertices, 0, NoAllocHelpers.SafeLength<Vector3>(inVertices));
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x00025A41 File Offset: 0x00023C41
		[ExcludeFromDocs]
		public void SetVertices(List<Vector3> inVertices, int start, int length)
		{
			this.SetVertices(inVertices, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x00025A4F File Offset: 0x00023C4F
		public void SetVertices(List<Vector3> inVertices, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetListForChannel<Vector3>(VertexAttribute.Position, inVertices, start, length, flags);
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00025A5F File Offset: 0x00023C5F
		public void SetVertices(Vector3[] inVertices)
		{
			this.SetVertices(inVertices, 0, NoAllocHelpers.SafeLength(inVertices));
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x00025A71 File Offset: 0x00023C71
		[ExcludeFromDocs]
		public void SetVertices(Vector3[] inVertices, int start, int length)
		{
			this.SetVertices(inVertices, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x00025A80 File Offset: 0x00023C80
		public void SetVertices(Vector3[] inVertices, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetSizedArrayForChannel(VertexAttribute.Position, VertexAttributeFormat.Float32, Mesh.DefaultDimensionForChannel(VertexAttribute.Position), inVertices, NoAllocHelpers.SafeLength(inVertices), start, length, flags);
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x00025AA8 File Offset: 0x00023CA8
		public void SetVertices<T>(NativeArray<T> inVertices) where T : struct
		{
			this.SetVertices<T>(inVertices, 0, inVertices.Length);
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x00025ABB File Offset: 0x00023CBB
		[ExcludeFromDocs]
		public void SetVertices<T>(NativeArray<T> inVertices, int start, int length) where T : struct
		{
			this.SetVertices<T>(inVertices, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x00025ACC File Offset: 0x00023CCC
		public void SetVertices<T>(NativeArray<T> inVertices, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags) where T : struct
		{
			bool flag = UnsafeUtility.SizeOf<T>() != 12;
			if (flag)
			{
				throw new ArgumentException("SetVertices with NativeArray should use struct type that is 12 bytes (3x float) in size");
			}
			this.SetSizedNativeArrayForChannel(VertexAttribute.Position, VertexAttributeFormat.Float32, 3, (IntPtr)inVertices.GetUnsafeReadOnlyPtr<T>(), inVertices.Length, start, length, flags);
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x00025B18 File Offset: 0x00023D18
		public void GetNormals(List<Vector3> normals)
		{
			bool flag = normals == null;
			if (flag)
			{
				throw new ArgumentNullException("normals", "The result normals list cannot be null.");
			}
			this.GetListForChannel<Vector3>(normals, this.vertexCount, VertexAttribute.Normal, Mesh.DefaultDimensionForChannel(VertexAttribute.Normal));
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x00025B53 File Offset: 0x00023D53
		public void SetNormals(List<Vector3> inNormals)
		{
			this.SetNormals(inNormals, 0, NoAllocHelpers.SafeLength<Vector3>(inNormals));
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x00025B65 File Offset: 0x00023D65
		[ExcludeFromDocs]
		public void SetNormals(List<Vector3> inNormals, int start, int length)
		{
			this.SetNormals(inNormals, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x00025B73 File Offset: 0x00023D73
		public void SetNormals(List<Vector3> inNormals, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetListForChannel<Vector3>(VertexAttribute.Normal, inNormals, start, length, flags);
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x00025B83 File Offset: 0x00023D83
		public void SetNormals(Vector3[] inNormals)
		{
			this.SetNormals(inNormals, 0, NoAllocHelpers.SafeLength(inNormals));
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x00025B95 File Offset: 0x00023D95
		[ExcludeFromDocs]
		public void SetNormals(Vector3[] inNormals, int start, int length)
		{
			this.SetNormals(inNormals, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x00025BA4 File Offset: 0x00023DA4
		public void SetNormals(Vector3[] inNormals, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetSizedArrayForChannel(VertexAttribute.Normal, VertexAttributeFormat.Float32, Mesh.DefaultDimensionForChannel(VertexAttribute.Normal), inNormals, NoAllocHelpers.SafeLength(inNormals), start, length, flags);
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x00025BCC File Offset: 0x00023DCC
		public void SetNormals<T>(NativeArray<T> inNormals) where T : struct
		{
			this.SetNormals<T>(inNormals, 0, inNormals.Length);
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x00025BDF File Offset: 0x00023DDF
		[ExcludeFromDocs]
		public void SetNormals<T>(NativeArray<T> inNormals, int start, int length) where T : struct
		{
			this.SetNormals<T>(inNormals, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x00025BF0 File Offset: 0x00023DF0
		public void SetNormals<T>(NativeArray<T> inNormals, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags) where T : struct
		{
			bool flag = UnsafeUtility.SizeOf<T>() != 12;
			if (flag)
			{
				throw new ArgumentException("SetNormals with NativeArray should use struct type that is 12 bytes (3x float) in size");
			}
			this.SetSizedNativeArrayForChannel(VertexAttribute.Normal, VertexAttributeFormat.Float32, 3, (IntPtr)inNormals.GetUnsafeReadOnlyPtr<T>(), inNormals.Length, start, length, flags);
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x00025C3C File Offset: 0x00023E3C
		public void GetTangents(List<Vector4> tangents)
		{
			bool flag = tangents == null;
			if (flag)
			{
				throw new ArgumentNullException("tangents", "The result tangents list cannot be null.");
			}
			this.GetListForChannel<Vector4>(tangents, this.vertexCount, VertexAttribute.Tangent, Mesh.DefaultDimensionForChannel(VertexAttribute.Tangent));
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x00025C77 File Offset: 0x00023E77
		public void SetTangents(List<Vector4> inTangents)
		{
			this.SetTangents(inTangents, 0, NoAllocHelpers.SafeLength<Vector4>(inTangents));
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x00025C89 File Offset: 0x00023E89
		[ExcludeFromDocs]
		public void SetTangents(List<Vector4> inTangents, int start, int length)
		{
			this.SetTangents(inTangents, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x00025C97 File Offset: 0x00023E97
		public void SetTangents(List<Vector4> inTangents, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetListForChannel<Vector4>(VertexAttribute.Tangent, inTangents, start, length, flags);
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x00025CA7 File Offset: 0x00023EA7
		public void SetTangents(Vector4[] inTangents)
		{
			this.SetTangents(inTangents, 0, NoAllocHelpers.SafeLength(inTangents));
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x00025CB9 File Offset: 0x00023EB9
		[ExcludeFromDocs]
		public void SetTangents(Vector4[] inTangents, int start, int length)
		{
			this.SetTangents(inTangents, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x00025CC8 File Offset: 0x00023EC8
		public void SetTangents(Vector4[] inTangents, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetSizedArrayForChannel(VertexAttribute.Tangent, VertexAttributeFormat.Float32, Mesh.DefaultDimensionForChannel(VertexAttribute.Tangent), inTangents, NoAllocHelpers.SafeLength(inTangents), start, length, flags);
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x00025CF0 File Offset: 0x00023EF0
		public void SetTangents<T>(NativeArray<T> inTangents) where T : struct
		{
			this.SetTangents<T>(inTangents, 0, inTangents.Length);
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x00025D03 File Offset: 0x00023F03
		[ExcludeFromDocs]
		public void SetTangents<T>(NativeArray<T> inTangents, int start, int length) where T : struct
		{
			this.SetTangents<T>(inTangents, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x00025D14 File Offset: 0x00023F14
		public void SetTangents<T>(NativeArray<T> inTangents, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags) where T : struct
		{
			bool flag = UnsafeUtility.SizeOf<T>() != 16;
			if (flag)
			{
				throw new ArgumentException("SetTangents with NativeArray should use struct type that is 16 bytes (4x float) in size");
			}
			this.SetSizedNativeArrayForChannel(VertexAttribute.Tangent, VertexAttributeFormat.Float32, 4, (IntPtr)inTangents.GetUnsafeReadOnlyPtr<T>(), inTangents.Length, start, length, flags);
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x00025D60 File Offset: 0x00023F60
		public void GetColors(List<Color> colors)
		{
			bool flag = colors == null;
			if (flag)
			{
				throw new ArgumentNullException("colors", "The result colors list cannot be null.");
			}
			this.GetListForChannel<Color>(colors, this.vertexCount, VertexAttribute.Color, Mesh.DefaultDimensionForChannel(VertexAttribute.Color));
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x00025D9B File Offset: 0x00023F9B
		public void SetColors(List<Color> inColors)
		{
			this.SetColors(inColors, 0, NoAllocHelpers.SafeLength<Color>(inColors));
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x00025DAD File Offset: 0x00023FAD
		[ExcludeFromDocs]
		public void SetColors(List<Color> inColors, int start, int length)
		{
			this.SetColors(inColors, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x00025DBB File Offset: 0x00023FBB
		public void SetColors(List<Color> inColors, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetListForChannel<Color>(VertexAttribute.Color, inColors, start, length, flags);
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x00025DCB File Offset: 0x00023FCB
		public void SetColors(Color[] inColors)
		{
			this.SetColors(inColors, 0, NoAllocHelpers.SafeLength(inColors));
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x00025DDD File Offset: 0x00023FDD
		[ExcludeFromDocs]
		public void SetColors(Color[] inColors, int start, int length)
		{
			this.SetColors(inColors, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x00025DEC File Offset: 0x00023FEC
		public void SetColors(Color[] inColors, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetSizedArrayForChannel(VertexAttribute.Color, VertexAttributeFormat.Float32, Mesh.DefaultDimensionForChannel(VertexAttribute.Color), inColors, NoAllocHelpers.SafeLength(inColors), start, length, flags);
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x00025E14 File Offset: 0x00024014
		public void GetColors(List<Color32> colors)
		{
			bool flag = colors == null;
			if (flag)
			{
				throw new ArgumentNullException("colors", "The result colors list cannot be null.");
			}
			this.GetListForChannel<Color32>(colors, this.vertexCount, VertexAttribute.Color, 4, VertexAttributeFormat.UNorm8);
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x00025E4B File Offset: 0x0002404B
		public void SetColors(List<Color32> inColors)
		{
			this.SetColors(inColors, 0, NoAllocHelpers.SafeLength<Color32>(inColors));
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x00025E5D File Offset: 0x0002405D
		[ExcludeFromDocs]
		public void SetColors(List<Color32> inColors, int start, int length)
		{
			this.SetColors(inColors, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x00025E6B File Offset: 0x0002406B
		public void SetColors(List<Color32> inColors, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetListForChannel<Color32>(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4, inColors, start, length, flags);
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x00025E7D File Offset: 0x0002407D
		public void SetColors(Color32[] inColors)
		{
			this.SetColors(inColors, 0, NoAllocHelpers.SafeLength(inColors));
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x00025E8F File Offset: 0x0002408F
		[ExcludeFromDocs]
		public void SetColors(Color32[] inColors, int start, int length)
		{
			this.SetColors(inColors, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x00025EA0 File Offset: 0x000240A0
		public void SetColors(Color32[] inColors, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetSizedArrayForChannel(VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4, inColors, NoAllocHelpers.SafeLength(inColors), start, length, flags);
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x00025EC3 File Offset: 0x000240C3
		public void SetColors<T>(NativeArray<T> inColors) where T : struct
		{
			this.SetColors<T>(inColors, 0, inColors.Length);
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x00025ED6 File Offset: 0x000240D6
		[ExcludeFromDocs]
		public void SetColors<T>(NativeArray<T> inColors, int start, int length) where T : struct
		{
			this.SetColors<T>(inColors, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x00025EE4 File Offset: 0x000240E4
		public void SetColors<T>(NativeArray<T> inColors, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags) where T : struct
		{
			int num = UnsafeUtility.SizeOf<T>();
			bool flag = num != 16 && num != 4;
			if (flag)
			{
				throw new ArgumentException("SetColors with NativeArray should use struct type that is 16 bytes (4x float) or 4 bytes (4x unorm) in size");
			}
			this.SetSizedNativeArrayForChannel(VertexAttribute.Color, (num == 4) ? VertexAttributeFormat.UNorm8 : VertexAttributeFormat.Float32, 4, (IntPtr)inColors.GetUnsafeReadOnlyPtr<T>(), inColors.Length, start, length, flags);
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x00025F40 File Offset: 0x00024140
		private void SetUvsImpl<T>(int uvIndex, int dim, List<T> uvs, int start, int length, MeshUpdateFlags flags)
		{
			bool flag = uvIndex < 0 || uvIndex > 7;
			if (flag)
			{
				Debug.LogError("The uv index is invalid. Must be in the range 0 to 7.");
			}
			else
			{
				this.SetListForChannel<T>(Mesh.GetUVChannel(uvIndex), VertexAttributeFormat.Float32, dim, uvs, start, length, flags);
			}
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x00025F81 File Offset: 0x00024181
		public void SetUVs(int channel, List<Vector2> uvs)
		{
			this.SetUVs(channel, uvs, 0, NoAllocHelpers.SafeLength<Vector2>(uvs));
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x00025F94 File Offset: 0x00024194
		public void SetUVs(int channel, List<Vector3> uvs)
		{
			this.SetUVs(channel, uvs, 0, NoAllocHelpers.SafeLength<Vector3>(uvs));
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x00025FA7 File Offset: 0x000241A7
		public void SetUVs(int channel, List<Vector4> uvs)
		{
			this.SetUVs(channel, uvs, 0, NoAllocHelpers.SafeLength<Vector4>(uvs));
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x00025FBA File Offset: 0x000241BA
		[ExcludeFromDocs]
		public void SetUVs(int channel, List<Vector2> uvs, int start, int length)
		{
			this.SetUVs(channel, uvs, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x00025FCA File Offset: 0x000241CA
		public void SetUVs(int channel, List<Vector2> uvs, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetUvsImpl<Vector2>(channel, 2, uvs, start, length, flags);
		}

		// Token: 0x0600161C RID: 5660 RVA: 0x00025FDC File Offset: 0x000241DC
		[ExcludeFromDocs]
		public void SetUVs(int channel, List<Vector3> uvs, int start, int length)
		{
			this.SetUVs(channel, uvs, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x0600161D RID: 5661 RVA: 0x00025FEC File Offset: 0x000241EC
		public void SetUVs(int channel, List<Vector3> uvs, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetUvsImpl<Vector3>(channel, 3, uvs, start, length, flags);
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x00025FFE File Offset: 0x000241FE
		[ExcludeFromDocs]
		public void SetUVs(int channel, List<Vector4> uvs, int start, int length)
		{
			this.SetUVs(channel, uvs, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x0002600E File Offset: 0x0002420E
		public void SetUVs(int channel, List<Vector4> uvs, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetUvsImpl<Vector4>(channel, 4, uvs, start, length, flags);
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x00026020 File Offset: 0x00024220
		private void SetUvsImpl(int uvIndex, int dim, Array uvs, int arrayStart, int arraySize, MeshUpdateFlags flags)
		{
			bool flag = uvIndex < 0 || uvIndex > 7;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("uvIndex", uvIndex, "The uv index is invalid. Must be in the range 0 to 7.");
			}
			this.SetSizedArrayForChannel(Mesh.GetUVChannel(uvIndex), VertexAttributeFormat.Float32, dim, uvs, NoAllocHelpers.SafeLength(uvs), arrayStart, arraySize, flags);
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x0002606F File Offset: 0x0002426F
		public void SetUVs(int channel, Vector2[] uvs)
		{
			this.SetUVs(channel, uvs, 0, NoAllocHelpers.SafeLength(uvs));
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x00026082 File Offset: 0x00024282
		public void SetUVs(int channel, Vector3[] uvs)
		{
			this.SetUVs(channel, uvs, 0, NoAllocHelpers.SafeLength(uvs));
		}

		// Token: 0x06001623 RID: 5667 RVA: 0x00026095 File Offset: 0x00024295
		public void SetUVs(int channel, Vector4[] uvs)
		{
			this.SetUVs(channel, uvs, 0, NoAllocHelpers.SafeLength(uvs));
		}

		// Token: 0x06001624 RID: 5668 RVA: 0x000260A8 File Offset: 0x000242A8
		[ExcludeFromDocs]
		public void SetUVs(int channel, Vector2[] uvs, int start, int length)
		{
			this.SetUVs(channel, uvs, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06001625 RID: 5669 RVA: 0x000260B8 File Offset: 0x000242B8
		public void SetUVs(int channel, Vector2[] uvs, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetUvsImpl(channel, 2, uvs, start, length, flags);
		}

		// Token: 0x06001626 RID: 5670 RVA: 0x000260CA File Offset: 0x000242CA
		[ExcludeFromDocs]
		public void SetUVs(int channel, Vector3[] uvs, int start, int length)
		{
			this.SetUVs(channel, uvs, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x000260DA File Offset: 0x000242DA
		public void SetUVs(int channel, Vector3[] uvs, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetUvsImpl(channel, 3, uvs, start, length, flags);
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x000260EC File Offset: 0x000242EC
		[ExcludeFromDocs]
		public void SetUVs(int channel, Vector4[] uvs, int start, int length)
		{
			this.SetUVs(channel, uvs, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x000260FC File Offset: 0x000242FC
		public void SetUVs(int channel, Vector4[] uvs, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			this.SetUvsImpl(channel, 4, uvs, start, length, flags);
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x0002610E File Offset: 0x0002430E
		public void SetUVs<T>(int channel, NativeArray<T> uvs) where T : struct
		{
			this.SetUVs<T>(channel, uvs, 0, uvs.Length);
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x00026122 File Offset: 0x00024322
		[ExcludeFromDocs]
		public void SetUVs<T>(int channel, NativeArray<T> uvs, int start, int length) where T : struct
		{
			this.SetUVs<T>(channel, uvs, start, length, MeshUpdateFlags.Default);
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x00026134 File Offset: 0x00024334
		public void SetUVs<T>(int channel, NativeArray<T> uvs, int start, int length, [DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags) where T : struct
		{
			bool flag = channel < 0 || channel > 7;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("channel", channel, "The uv index is invalid. Must be in the range 0 to 7.");
			}
			int num = UnsafeUtility.SizeOf<T>();
			bool flag2 = (num & 3) != 0;
			if (flag2)
			{
				throw new ArgumentException("SetUVs with NativeArray should use struct type that is multiple of 4 bytes in size");
			}
			int num2 = num / 4;
			bool flag3 = num2 < 1 || num2 > 4;
			if (flag3)
			{
				throw new ArgumentException("SetUVs with NativeArray should use struct type that is 1..4 floats in size");
			}
			this.SetSizedNativeArrayForChannel(Mesh.GetUVChannel(channel), VertexAttributeFormat.Float32, num2, (IntPtr)uvs.GetUnsafeReadOnlyPtr<T>(), uvs.Length, start, length, flags);
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x000261C8 File Offset: 0x000243C8
		private void GetUVsImpl<T>(int uvIndex, List<T> uvs, int dim)
		{
			bool flag = uvs == null;
			if (flag)
			{
				throw new ArgumentNullException("uvs", "The result uvs list cannot be null.");
			}
			bool flag2 = uvIndex < 0 || uvIndex > 7;
			if (flag2)
			{
				throw new IndexOutOfRangeException("The uv index is invalid. Must be in the range 0 to 7.");
			}
			this.GetListForChannel<T>(uvs, this.vertexCount, Mesh.GetUVChannel(uvIndex), dim);
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x0002621D File Offset: 0x0002441D
		public void GetUVs(int channel, List<Vector2> uvs)
		{
			this.GetUVsImpl<Vector2>(channel, uvs, 2);
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x0002622A File Offset: 0x0002442A
		public void GetUVs(int channel, List<Vector3> uvs)
		{
			this.GetUVsImpl<Vector3>(channel, uvs, 3);
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x00026237 File Offset: 0x00024437
		public void GetUVs(int channel, List<Vector4> uvs)
		{
			this.GetUVsImpl<Vector4>(channel, uvs, 4);
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001631 RID: 5681 RVA: 0x00026244 File Offset: 0x00024444
		public int vertexAttributeCount
		{
			get
			{
				return this.GetVertexAttributeCountImpl();
			}
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x0002625C File Offset: 0x0002445C
		public VertexAttributeDescriptor[] GetVertexAttributes()
		{
			return (VertexAttributeDescriptor[])this.GetVertexAttributesAlloc();
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x0002627C File Offset: 0x0002447C
		public int GetVertexAttributes(VertexAttributeDescriptor[] attributes)
		{
			return this.GetVertexAttributesArray(attributes);
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x00026298 File Offset: 0x00024498
		public int GetVertexAttributes(List<VertexAttributeDescriptor> attributes)
		{
			return this.GetVertexAttributesList(attributes);
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x000262B1 File Offset: 0x000244B1
		public void SetVertexBufferParams(int vertexCount, params VertexAttributeDescriptor[] attributes)
		{
			this.SetVertexBufferParamsFromArray(vertexCount, attributes);
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x000262BD File Offset: 0x000244BD
		public void SetVertexBufferParams(int vertexCount, NativeArray<VertexAttributeDescriptor> attributes)
		{
			this.SetVertexBufferParamsFromPtr(vertexCount, (IntPtr)attributes.GetUnsafeReadOnlyPtr<VertexAttributeDescriptor>(), attributes.Length);
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x000262DC File Offset: 0x000244DC
		public void SetVertexBufferData<T>(NativeArray<T> data, int dataStart, int meshBufferStart, int count, int stream = 0, MeshUpdateFlags flags = MeshUpdateFlags.Default) where T : struct
		{
			bool flag = !this.canAccess;
			if (flag)
			{
				throw new InvalidOperationException("Not allowed to access vertex data on mesh '" + base.name + "' (isReadable is false; Read/Write must be enabled in import settings)");
			}
			bool flag2 = dataStart < 0 || meshBufferStart < 0 || count < 0 || dataStart + count > data.Length;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (dataStart:{0} meshBufferStart:{1} count:{2})", dataStart, meshBufferStart, count));
			}
			this.InternalSetVertexBufferData(stream, (IntPtr)data.GetUnsafeReadOnlyPtr<T>(), dataStart, meshBufferStart, count, UnsafeUtility.SizeOf<T>(), flags);
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x00026378 File Offset: 0x00024578
		public void SetVertexBufferData<T>(T[] data, int dataStart, int meshBufferStart, int count, int stream = 0, MeshUpdateFlags flags = MeshUpdateFlags.Default) where T : struct
		{
			bool flag = !this.canAccess;
			if (flag)
			{
				throw new InvalidOperationException("Not allowed to access vertex data on mesh '" + base.name + "' (isReadable is false; Read/Write must be enabled in import settings)");
			}
			bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
			if (flag2)
			{
				throw new ArgumentException("Array passed to SetVertexBufferData must be blittable.\n" + UnsafeUtility.GetReasonForArrayNonBlittable(data));
			}
			bool flag3 = dataStart < 0 || meshBufferStart < 0 || count < 0 || dataStart + count > data.Length;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (dataStart:{0} meshBufferStart:{1} count:{2})", dataStart, meshBufferStart, count));
			}
			this.InternalSetVertexBufferDataFromArray(stream, data, dataStart, meshBufferStart, count, UnsafeUtility.SizeOf<T>(), flags);
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x00026428 File Offset: 0x00024628
		public void SetVertexBufferData<T>(List<T> data, int dataStart, int meshBufferStart, int count, int stream = 0, MeshUpdateFlags flags = MeshUpdateFlags.Default) where T : struct
		{
			bool flag = !this.canAccess;
			if (flag)
			{
				throw new InvalidOperationException("Not allowed to access vertex data on mesh '" + base.name + "' (isReadable is false; Read/Write must be enabled in import settings)");
			}
			bool flag2 = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag2)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to {1} must be blittable.\n{2}", typeof(T), "SetVertexBufferData", UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			bool flag3 = dataStart < 0 || meshBufferStart < 0 || count < 0 || dataStart + count > data.Count;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (dataStart:{0} meshBufferStart:{1} count:{2})", dataStart, meshBufferStart, count));
			}
			this.InternalSetVertexBufferDataFromArray(stream, NoAllocHelpers.ExtractArrayFromList<T>(data), dataStart, meshBufferStart, count, UnsafeUtility.SizeOf<T>(), flags);
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x000264EC File Offset: 0x000246EC
		public static Mesh.MeshDataArray AcquireReadOnlyMeshData(Mesh mesh)
		{
			return new Mesh.MeshDataArray(mesh, true, false);
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x00026508 File Offset: 0x00024708
		public static Mesh.MeshDataArray AcquireReadOnlyMeshData(Mesh[] meshes)
		{
			bool flag = meshes == null;
			if (flag)
			{
				throw new ArgumentNullException("meshes", "Mesh array is null");
			}
			return new Mesh.MeshDataArray(meshes, meshes.Length, true, false);
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x00026540 File Offset: 0x00024740
		public static Mesh.MeshDataArray AcquireReadOnlyMeshData(List<Mesh> meshes)
		{
			bool flag = meshes == null;
			if (flag)
			{
				throw new ArgumentNullException("meshes", "Mesh list is null");
			}
			return new Mesh.MeshDataArray(NoAllocHelpers.ExtractArrayFromList<Mesh>(meshes), meshes.Count, true, false);
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x00026580 File Offset: 0x00024780
		public static Mesh.MeshDataArray AllocateWritableMeshData(int meshCount)
		{
			return new Mesh.MeshDataArray(meshCount);
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x00026598 File Offset: 0x00024798
		public static Mesh.MeshDataArray AllocateWritableMeshData(Mesh mesh)
		{
			return new Mesh.MeshDataArray(mesh, true, true);
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x000265B4 File Offset: 0x000247B4
		public static Mesh.MeshDataArray AllocateWritableMeshData(Mesh[] meshes)
		{
			bool flag = meshes == null;
			if (flag)
			{
				throw new ArgumentNullException("meshes", "Mesh array is null");
			}
			return new Mesh.MeshDataArray(meshes, meshes.Length, true, true);
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x000265EC File Offset: 0x000247EC
		public static Mesh.MeshDataArray AllocateWritableMeshData(List<Mesh> meshes)
		{
			bool flag = meshes == null;
			if (flag)
			{
				throw new ArgumentNullException("meshes", "Mesh list is null");
			}
			return new Mesh.MeshDataArray(NoAllocHelpers.ExtractArrayFromList<Mesh>(meshes), meshes.Count, true, true);
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x0002662C File Offset: 0x0002482C
		public static void ApplyAndDisposeWritableMeshData(Mesh.MeshDataArray data, Mesh mesh, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			bool flag = mesh == null;
			if (flag)
			{
				throw new ArgumentNullException("mesh", "Mesh is null");
			}
			bool flag2 = data.Length != 1;
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("{0} length must be 1 to apply to one mesh, was {1}", "MeshDataArray", data.Length));
			}
			data.ApplyToMeshAndDispose(mesh, flags);
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x00026694 File Offset: 0x00024894
		public static void ApplyAndDisposeWritableMeshData(Mesh.MeshDataArray data, Mesh[] meshes, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			bool flag = meshes == null;
			if (flag)
			{
				throw new ArgumentNullException("meshes", "Mesh array is null");
			}
			bool flag2 = data.Length != meshes.Length;
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("{0} length ({1}) must match destination meshes array length ({2})", "MeshDataArray", data.Length, meshes.Length));
			}
			data.ApplyToMeshesAndDispose(meshes, flags);
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x00026700 File Offset: 0x00024900
		public static void ApplyAndDisposeWritableMeshData(Mesh.MeshDataArray data, List<Mesh> meshes, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			bool flag = meshes == null;
			if (flag)
			{
				throw new ArgumentNullException("meshes", "Mesh list is null");
			}
			bool flag2 = data.Length != meshes.Count;
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("{0} length ({1}) must match destination meshes list length ({2})", "MeshDataArray", data.Length, meshes.Count));
			}
			data.ApplyToMeshesAndDispose(NoAllocHelpers.ExtractArrayFromList<Mesh>(meshes), flags);
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x00026778 File Offset: 0x00024978
		public GraphicsBuffer GetVertexBuffer(int index)
		{
			bool flag = this == null;
			if (flag)
			{
				throw new NullReferenceException();
			}
			return this.GetVertexBufferImpl(index);
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x000267A4 File Offset: 0x000249A4
		public GraphicsBuffer GetIndexBuffer()
		{
			bool flag = this == null;
			if (flag)
			{
				throw new NullReferenceException();
			}
			return this.GetIndexBufferImpl();
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x000267D0 File Offset: 0x000249D0
		public GraphicsBuffer GetBoneWeightBuffer(SkinWeights layout)
		{
			bool flag = this == null;
			if (flag)
			{
				throw new NullReferenceException();
			}
			bool flag2 = layout == SkinWeights.None;
			GraphicsBuffer graphicsBuffer;
			if (flag2)
			{
				Debug.LogError(string.Format("Only possible to access bone weights buffer for values: {0}, {1}, {2} and {3}.", new object[]
				{
					SkinWeights.OneBone,
					SkinWeights.TwoBones,
					SkinWeights.FourBones,
					SkinWeights.Unlimited
				}));
				graphicsBuffer = null;
			}
			else
			{
				GraphicsBuffer boneWeightBufferImpl = this.GetBoneWeightBufferImpl((int)layout);
				graphicsBuffer = boneWeightBufferImpl;
			}
			return graphicsBuffer;
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x00026848 File Offset: 0x00024A48
		public GraphicsBuffer GetBlendShapeBuffer(BlendShapeBufferLayout layout)
		{
			bool flag = this == null;
			if (flag)
			{
				throw new NullReferenceException();
			}
			bool flag2 = !SystemInfo.supportsComputeShaders;
			GraphicsBuffer graphicsBuffer;
			if (flag2)
			{
				Debug.LogError("Only possible to access Blend Shape buffer on platforms that supports compute shaders.");
				graphicsBuffer = null;
			}
			else
			{
				GraphicsBuffer blendShapeBufferImpl = this.GetBlendShapeBufferImpl((int)layout);
				graphicsBuffer = blendShapeBufferImpl;
			}
			return graphicsBuffer;
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x00026890 File Offset: 0x00024A90
		public GraphicsBuffer GetBlendShapeBuffer()
		{
			bool flag = this == null;
			if (flag)
			{
				throw new NullReferenceException();
			}
			bool flag2 = !SystemInfo.supportsComputeShaders;
			GraphicsBuffer graphicsBuffer;
			if (flag2)
			{
				Debug.LogError("Only possible to access Blend Shape buffer on platforms that supports compute shaders.");
				graphicsBuffer = null;
			}
			else
			{
				GraphicsBuffer blendShapeBufferImpl = this.GetBlendShapeBufferImpl(0);
				graphicsBuffer = blendShapeBufferImpl;
			}
			return graphicsBuffer;
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x000268D8 File Offset: 0x00024AD8
		public BlendShapeBufferRange GetBlendShapeBufferRange(int blendShapeIndex)
		{
			bool flag = blendShapeIndex >= this.blendShapeCount || blendShapeIndex < 0;
			BlendShapeBufferRange blendShapeBufferRange;
			if (flag)
			{
				Debug.LogError("Incorrect index used to get blend shape buffer range");
				blendShapeBufferRange = default(BlendShapeBufferRange);
			}
			else
			{
				BlendShape blendShapeOffsetInternal = this.GetBlendShapeOffsetInternal(blendShapeIndex);
				blendShapeBufferRange = new BlendShapeBufferRange
				{
					startIndex = blendShapeOffsetInternal.firstVertex,
					endIndex = blendShapeOffsetInternal.firstVertex + blendShapeOffsetInternal.vertexCount - 1U
				};
			}
			return blendShapeBufferRange;
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x00026952 File Offset: 0x00024B52
		private void PrintErrorCantAccessIndices()
		{
			Debug.LogError(string.Format("Not allowed to access triangles/indices on mesh '{0}' (isReadable is false; Read/Write must be enabled in import settings)", base.name));
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x0002696C File Offset: 0x00024B6C
		private bool CheckCanAccessSubmesh(int submesh, bool errorAboutTriangles)
		{
			bool flag = !this.canAccess;
			bool flag2;
			if (flag)
			{
				this.PrintErrorCantAccessIndices();
				flag2 = false;
			}
			else
			{
				bool flag3 = submesh < 0 || submesh >= this.subMeshCount;
				if (flag3)
				{
					Debug.LogError(string.Format("Failed getting {0}. Submesh index is out of bounds.", errorAboutTriangles ? "triangles" : "indices"), this);
					flag2 = false;
				}
				else
				{
					flag2 = true;
				}
			}
			return flag2;
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x000269D4 File Offset: 0x00024BD4
		private bool CheckCanAccessSubmeshTriangles(int submesh)
		{
			return this.CheckCanAccessSubmesh(submesh, true);
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x000269F0 File Offset: 0x00024BF0
		private bool CheckCanAccessSubmeshIndices(int submesh)
		{
			return this.CheckCanAccessSubmesh(submesh, false);
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x00026A0C File Offset: 0x00024C0C
		// (set) Token: 0x0600164F RID: 5711 RVA: 0x00026A40 File Offset: 0x00024C40
		public int[] triangles
		{
			get
			{
				bool canAccess = this.canAccess;
				int[] array;
				if (canAccess)
				{
					array = this.GetTrianglesImpl(-1, true);
				}
				else
				{
					this.PrintErrorCantAccessIndices();
					array = new int[0];
				}
				return array;
			}
			set
			{
				bool canAccess = this.canAccess;
				if (canAccess)
				{
					this.SetTrianglesImpl(-1, IndexFormat.UInt32, value, NoAllocHelpers.SafeLength(value), 0, NoAllocHelpers.SafeLength(value), true, 0);
				}
				else
				{
					this.PrintErrorCantAccessIndices();
				}
			}
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x00026A7C File Offset: 0x00024C7C
		public int[] GetTriangles(int submesh)
		{
			return this.GetTriangles(submesh, true);
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x00026A98 File Offset: 0x00024C98
		public int[] GetTriangles(int submesh, [DefaultValue("true")] bool applyBaseVertex)
		{
			return this.CheckCanAccessSubmeshTriangles(submesh) ? this.GetTrianglesImpl(submesh, applyBaseVertex) : new int[0];
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x00026AC3 File Offset: 0x00024CC3
		public void GetTriangles(List<int> triangles, int submesh)
		{
			this.GetTriangles(triangles, submesh, true);
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x00026AD0 File Offset: 0x00024CD0
		public void GetTriangles(List<int> triangles, int submesh, [DefaultValue("true")] bool applyBaseVertex)
		{
			bool flag = triangles == null;
			if (flag)
			{
				throw new ArgumentNullException("triangles", "The result triangles list cannot be null.");
			}
			bool flag2 = submesh < 0 || submesh >= this.subMeshCount;
			if (flag2)
			{
				throw new IndexOutOfRangeException("Specified sub mesh is out of range. Must be greater or equal to 0 and less than subMeshCount.");
			}
			NoAllocHelpers.EnsureListElemCount<int>(triangles, (int)(3U * this.GetTrianglesCountImpl(submesh)));
			this.GetTrianglesNonAllocImpl(NoAllocHelpers.ExtractArrayFromList<int>(triangles), submesh, applyBaseVertex);
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x00026B38 File Offset: 0x00024D38
		public void GetTriangles(List<ushort> triangles, int submesh, bool applyBaseVertex = true)
		{
			bool flag = triangles == null;
			if (flag)
			{
				throw new ArgumentNullException("triangles", "The result triangles list cannot be null.");
			}
			bool flag2 = submesh < 0 || submesh >= this.subMeshCount;
			if (flag2)
			{
				throw new IndexOutOfRangeException("Specified sub mesh is out of range. Must be greater or equal to 0 and less than subMeshCount.");
			}
			NoAllocHelpers.EnsureListElemCount<ushort>(triangles, (int)(3U * this.GetTrianglesCountImpl(submesh)));
			this.GetTrianglesNonAllocImpl16(NoAllocHelpers.ExtractArrayFromList<ushort>(triangles), submesh, applyBaseVertex);
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x00026BA0 File Offset: 0x00024DA0
		[ExcludeFromDocs]
		public int[] GetIndices(int submesh)
		{
			return this.GetIndices(submesh, true);
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x00026BBC File Offset: 0x00024DBC
		public int[] GetIndices(int submesh, [DefaultValue("true")] bool applyBaseVertex)
		{
			return this.CheckCanAccessSubmeshIndices(submesh) ? this.GetIndicesImpl(submesh, applyBaseVertex) : new int[0];
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x00026BE7 File Offset: 0x00024DE7
		[ExcludeFromDocs]
		public void GetIndices(List<int> indices, int submesh)
		{
			this.GetIndices(indices, submesh, true);
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x00026BF4 File Offset: 0x00024DF4
		public void GetIndices(List<int> indices, int submesh, [DefaultValue("true")] bool applyBaseVertex)
		{
			bool flag = indices == null;
			if (flag)
			{
				throw new ArgumentNullException("indices", "The result indices list cannot be null.");
			}
			bool flag2 = submesh < 0 || submesh >= this.subMeshCount;
			if (flag2)
			{
				throw new IndexOutOfRangeException("Specified sub mesh is out of range. Must be greater or equal to 0 and less than subMeshCount.");
			}
			NoAllocHelpers.EnsureListElemCount<int>(indices, (int)this.GetIndexCount(submesh));
			this.GetIndicesNonAllocImpl(NoAllocHelpers.ExtractArrayFromList<int>(indices), submesh, applyBaseVertex);
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x00026C5C File Offset: 0x00024E5C
		public void GetIndices(List<ushort> indices, int submesh, bool applyBaseVertex = true)
		{
			bool flag = indices == null;
			if (flag)
			{
				throw new ArgumentNullException("indices", "The result indices list cannot be null.");
			}
			bool flag2 = submesh < 0 || submesh >= this.subMeshCount;
			if (flag2)
			{
				throw new IndexOutOfRangeException("Specified sub mesh is out of range. Must be greater or equal to 0 and less than subMeshCount.");
			}
			NoAllocHelpers.EnsureListElemCount<ushort>(indices, (int)this.GetIndexCount(submesh));
			this.GetIndicesNonAllocImpl16(NoAllocHelpers.ExtractArrayFromList<ushort>(indices), submesh, applyBaseVertex);
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x00026CC4 File Offset: 0x00024EC4
		public void SetIndexBufferData<T>(NativeArray<T> data, int dataStart, int meshBufferStart, int count, MeshUpdateFlags flags = MeshUpdateFlags.Default) where T : struct
		{
			bool flag = !this.canAccess;
			if (flag)
			{
				this.PrintErrorCantAccessIndices();
			}
			else
			{
				bool flag2 = dataStart < 0 || meshBufferStart < 0 || count < 0 || dataStart + count > data.Length;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (dataStart:{0} meshBufferStart:{1} count:{2})", dataStart, meshBufferStart, count));
				}
				this.InternalSetIndexBufferData((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), dataStart, meshBufferStart, count, UnsafeUtility.SizeOf<T>(), flags);
			}
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x00026D4C File Offset: 0x00024F4C
		public void SetIndexBufferData<T>(T[] data, int dataStart, int meshBufferStart, int count, MeshUpdateFlags flags = MeshUpdateFlags.Default) where T : struct
		{
			bool flag = !this.canAccess;
			if (flag)
			{
				this.PrintErrorCantAccessIndices();
			}
			else
			{
				bool flag2 = !UnsafeUtility.IsArrayBlittable(data);
				if (flag2)
				{
					throw new ArgumentException("Array passed to SetIndexBufferData must be blittable.\n" + UnsafeUtility.GetReasonForArrayNonBlittable(data));
				}
				bool flag3 = dataStart < 0 || meshBufferStart < 0 || count < 0 || dataStart + count > data.Length;
				if (flag3)
				{
					throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (dataStart:{0} meshBufferStart:{1} count:{2})", dataStart, meshBufferStart, count));
				}
				this.InternalSetIndexBufferDataFromArray(data, dataStart, meshBufferStart, count, UnsafeUtility.SizeOf<T>(), flags);
			}
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x00026DE8 File Offset: 0x00024FE8
		public void SetIndexBufferData<T>(List<T> data, int dataStart, int meshBufferStart, int count, MeshUpdateFlags flags = MeshUpdateFlags.Default) where T : struct
		{
			bool flag = !this.canAccess;
			if (flag)
			{
				this.PrintErrorCantAccessIndices();
			}
			else
			{
				bool flag2 = !UnsafeUtility.IsGenericListBlittable<T>();
				if (flag2)
				{
					throw new ArgumentException(string.Format("List<{0}> passed to {1} must be blittable.\n{2}", typeof(T), "SetIndexBufferData", UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
				}
				bool flag3 = dataStart < 0 || meshBufferStart < 0 || count < 0 || dataStart + count > data.Count;
				if (flag3)
				{
					throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (dataStart:{0} meshBufferStart:{1} count:{2})", dataStart, meshBufferStart, count));
				}
				this.InternalSetIndexBufferDataFromArray(NoAllocHelpers.ExtractArrayFromList<T>(data), dataStart, meshBufferStart, count, UnsafeUtility.SizeOf<T>(), flags);
			}
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x00026E9C File Offset: 0x0002509C
		public uint GetIndexStart(int submesh)
		{
			bool flag = submesh < 0 || submesh >= this.subMeshCount;
			if (flag)
			{
				throw new IndexOutOfRangeException("Specified sub mesh is out of range. Must be greater or equal to 0 and less than subMeshCount.");
			}
			return this.GetIndexStartImpl(submesh);
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x00026ED8 File Offset: 0x000250D8
		public uint GetIndexCount(int submesh)
		{
			bool flag = submesh < 0 || submesh >= this.subMeshCount;
			if (flag)
			{
				throw new IndexOutOfRangeException("Specified sub mesh is out of range. Must be greater or equal to 0 and less than subMeshCount.");
			}
			return this.GetIndexCountImpl(submesh);
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x00026F14 File Offset: 0x00025114
		public uint GetBaseVertex(int submesh)
		{
			bool flag = submesh < 0 || submesh >= this.subMeshCount;
			if (flag)
			{
				throw new IndexOutOfRangeException("Specified sub mesh is out of range. Must be greater or equal to 0 and less than subMeshCount.");
			}
			return this.GetBaseVertexImpl(submesh);
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x00026F50 File Offset: 0x00025150
		private void CheckIndicesArrayRange(int valuesLength, int start, int length)
		{
			bool flag = start < 0;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("start", start, "Mesh indices array start can't be negative.");
			}
			bool flag2 = length < 0;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("length", length, "Mesh indices array length can't be negative.");
			}
			bool flag3 = start >= valuesLength && length != 0;
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("start", start, "Mesh indices array start is outside of array size.");
			}
			bool flag4 = start + length > valuesLength;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("length", start + length, "Mesh indices array start+count is outside of array size.");
			}
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x00026FE4 File Offset: 0x000251E4
		private void SetTrianglesImpl(int submesh, IndexFormat indicesFormat, Array triangles, int trianglesArrayLength, int start, int length, bool calculateBounds, int baseVertex)
		{
			this.CheckIndicesArrayRange(trianglesArrayLength, start, length);
			this.SetIndicesImpl(submesh, MeshTopology.Triangles, indicesFormat, triangles, start, length, calculateBounds, baseVertex);
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x00027012 File Offset: 0x00025212
		[ExcludeFromDocs]
		public void SetTriangles(int[] triangles, int submesh)
		{
			this.SetTriangles(triangles, submesh, true, 0);
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x00027020 File Offset: 0x00025220
		[ExcludeFromDocs]
		public void SetTriangles(int[] triangles, int submesh, bool calculateBounds)
		{
			this.SetTriangles(triangles, submesh, calculateBounds, 0);
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x0002702E File Offset: 0x0002522E
		public void SetTriangles(int[] triangles, int submesh, [DefaultValue("true")] bool calculateBounds, [DefaultValue("0")] int baseVertex)
		{
			this.SetTriangles(triangles, 0, NoAllocHelpers.SafeLength(triangles), submesh, calculateBounds, baseVertex);
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x00027044 File Offset: 0x00025244
		public void SetTriangles(int[] triangles, int trianglesStart, int trianglesLength, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			bool flag = this.CheckCanAccessSubmeshTriangles(submesh);
			if (flag)
			{
				this.SetTrianglesImpl(submesh, IndexFormat.UInt32, triangles, NoAllocHelpers.SafeLength(triangles), trianglesStart, trianglesLength, calculateBounds, baseVertex);
			}
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x00027075 File Offset: 0x00025275
		public void SetTriangles(ushort[] triangles, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			this.SetTriangles(triangles, 0, NoAllocHelpers.SafeLength(triangles), submesh, calculateBounds, baseVertex);
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x0002708C File Offset: 0x0002528C
		public void SetTriangles(ushort[] triangles, int trianglesStart, int trianglesLength, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			bool flag = this.CheckCanAccessSubmeshTriangles(submesh);
			if (flag)
			{
				this.SetTrianglesImpl(submesh, IndexFormat.UInt16, triangles, NoAllocHelpers.SafeLength(triangles), trianglesStart, trianglesLength, calculateBounds, baseVertex);
			}
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x000270BD File Offset: 0x000252BD
		[ExcludeFromDocs]
		public void SetTriangles(List<int> triangles, int submesh)
		{
			this.SetTriangles(triangles, submesh, true, 0);
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x000270CB File Offset: 0x000252CB
		[ExcludeFromDocs]
		public void SetTriangles(List<int> triangles, int submesh, bool calculateBounds)
		{
			this.SetTriangles(triangles, submesh, calculateBounds, 0);
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x000270D9 File Offset: 0x000252D9
		public void SetTriangles(List<int> triangles, int submesh, [DefaultValue("true")] bool calculateBounds, [DefaultValue("0")] int baseVertex)
		{
			this.SetTriangles(triangles, 0, NoAllocHelpers.SafeLength<int>(triangles), submesh, calculateBounds, baseVertex);
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x000270F0 File Offset: 0x000252F0
		public void SetTriangles(List<int> triangles, int trianglesStart, int trianglesLength, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			bool flag = this.CheckCanAccessSubmeshTriangles(submesh);
			if (flag)
			{
				this.SetTrianglesImpl(submesh, IndexFormat.UInt32, NoAllocHelpers.ExtractArrayFromList<int>(triangles), NoAllocHelpers.SafeLength<int>(triangles), trianglesStart, trianglesLength, calculateBounds, baseVertex);
			}
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x00027126 File Offset: 0x00025326
		public void SetTriangles(List<ushort> triangles, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			this.SetTriangles(triangles, 0, NoAllocHelpers.SafeLength<ushort>(triangles), submesh, calculateBounds, baseVertex);
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x0002713C File Offset: 0x0002533C
		public void SetTriangles(List<ushort> triangles, int trianglesStart, int trianglesLength, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			bool flag = this.CheckCanAccessSubmeshTriangles(submesh);
			if (flag)
			{
				this.SetTrianglesImpl(submesh, IndexFormat.UInt16, NoAllocHelpers.ExtractArrayFromList<ushort>(triangles), NoAllocHelpers.SafeLength<ushort>(triangles), trianglesStart, trianglesLength, calculateBounds, baseVertex);
			}
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x00027172 File Offset: 0x00025372
		[ExcludeFromDocs]
		public void SetIndices(int[] indices, MeshTopology topology, int submesh)
		{
			this.SetIndices(indices, topology, submesh, true, 0);
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x00027181 File Offset: 0x00025381
		[ExcludeFromDocs]
		public void SetIndices(int[] indices, MeshTopology topology, int submesh, bool calculateBounds)
		{
			this.SetIndices(indices, topology, submesh, calculateBounds, 0);
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x00027191 File Offset: 0x00025391
		public void SetIndices(int[] indices, MeshTopology topology, int submesh, [DefaultValue("true")] bool calculateBounds, [DefaultValue("0")] int baseVertex)
		{
			this.SetIndices(indices, 0, NoAllocHelpers.SafeLength(indices), topology, submesh, calculateBounds, baseVertex);
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x000271AC File Offset: 0x000253AC
		public void SetIndices(int[] indices, int indicesStart, int indicesLength, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			bool flag = this.CheckCanAccessSubmeshIndices(submesh);
			if (flag)
			{
				this.CheckIndicesArrayRange(NoAllocHelpers.SafeLength(indices), indicesStart, indicesLength);
				this.SetIndicesImpl(submesh, topology, IndexFormat.UInt32, indices, indicesStart, indicesLength, calculateBounds, baseVertex);
			}
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x000271EA File Offset: 0x000253EA
		public void SetIndices(ushort[] indices, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			this.SetIndices(indices, 0, NoAllocHelpers.SafeLength(indices), topology, submesh, calculateBounds, baseVertex);
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x00027204 File Offset: 0x00025404
		public void SetIndices(ushort[] indices, int indicesStart, int indicesLength, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			bool flag = this.CheckCanAccessSubmeshIndices(submesh);
			if (flag)
			{
				this.CheckIndicesArrayRange(NoAllocHelpers.SafeLength(indices), indicesStart, indicesLength);
				this.SetIndicesImpl(submesh, topology, IndexFormat.UInt16, indices, indicesStart, indicesLength, calculateBounds, baseVertex);
			}
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x00027242 File Offset: 0x00025442
		public void SetIndices<T>(NativeArray<T> indices, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0) where T : struct
		{
			this.SetIndices<T>(indices, 0, indices.Length, topology, submesh, calculateBounds, baseVertex);
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x0002725C File Offset: 0x0002545C
		public void SetIndices<T>(NativeArray<T> indices, int indicesStart, int indicesLength, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0) where T : struct
		{
			bool flag = this.CheckCanAccessSubmeshIndices(submesh);
			if (flag)
			{
				int num = UnsafeUtility.SizeOf<T>();
				bool flag2 = num != 2 && num != 4;
				if (flag2)
				{
					throw new ArgumentException("SetIndices with NativeArray should use type is 2 or 4 bytes in size");
				}
				this.CheckIndicesArrayRange(indices.Length, indicesStart, indicesLength);
				this.SetIndicesNativeArrayImpl(submesh, topology, (num == 2) ? IndexFormat.UInt16 : IndexFormat.UInt32, (IntPtr)indices.GetUnsafeReadOnlyPtr<T>(), indicesStart, indicesLength, calculateBounds, baseVertex);
			}
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x000272CF File Offset: 0x000254CF
		public void SetIndices(List<int> indices, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			this.SetIndices(indices, 0, NoAllocHelpers.SafeLength<int>(indices), topology, submesh, calculateBounds, baseVertex);
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x000272E8 File Offset: 0x000254E8
		public void SetIndices(List<int> indices, int indicesStart, int indicesLength, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			bool flag = this.CheckCanAccessSubmeshIndices(submesh);
			if (flag)
			{
				int[] array = NoAllocHelpers.ExtractArrayFromList<int>(indices);
				this.CheckIndicesArrayRange(NoAllocHelpers.SafeLength<int>(indices), indicesStart, indicesLength);
				this.SetIndicesImpl(submesh, topology, IndexFormat.UInt32, array, indicesStart, indicesLength, calculateBounds, baseVertex);
			}
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x0002732D File Offset: 0x0002552D
		public void SetIndices(List<ushort> indices, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			this.SetIndices(indices, 0, NoAllocHelpers.SafeLength<ushort>(indices), topology, submesh, calculateBounds, baseVertex);
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x00027348 File Offset: 0x00025548
		public void SetIndices(List<ushort> indices, int indicesStart, int indicesLength, MeshTopology topology, int submesh, bool calculateBounds = true, int baseVertex = 0)
		{
			bool flag = this.CheckCanAccessSubmeshIndices(submesh);
			if (flag)
			{
				ushort[] array = NoAllocHelpers.ExtractArrayFromList<ushort>(indices);
				this.CheckIndicesArrayRange(NoAllocHelpers.SafeLength<ushort>(indices), indicesStart, indicesLength);
				this.SetIndicesImpl(submesh, topology, IndexFormat.UInt16, array, indicesStart, indicesLength, calculateBounds, baseVertex);
			}
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x00027390 File Offset: 0x00025590
		public void SetSubMeshes(SubMeshDescriptor[] desc, int start, int count, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			bool flag = count > 0 && desc == null;
			if (flag)
			{
				throw new ArgumentNullException("desc", "Array of submeshes cannot be null unless count is zero.");
			}
			int num = ((desc != null) ? desc.Length : 0);
			bool flag2 = start < 0 || count < 0 || start + count > num;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (start:{0} count:{1} desc.Length:{2})", start, count, num));
			}
			for (int i = start; i < start + count; i++)
			{
				MeshTopology topology = desc[i].topology;
				bool flag3 = topology < MeshTopology.Triangles || topology > MeshTopology.Points;
				if (flag3)
				{
					throw new ArgumentException("desc", string.Format("{0}-th submesh descriptor has invalid topology ({1}).", i, (int)topology));
				}
				bool flag4 = topology == (MeshTopology)1;
				if (flag4)
				{
					throw new ArgumentException("desc", string.Format("{0}-th submesh descriptor has triangles strip topology, which is no longer supported.", i));
				}
			}
			this.SetAllSubMeshesAtOnceFromArray(desc, start, count, flags);
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x00027489 File Offset: 0x00025689
		public void SetSubMeshes(SubMeshDescriptor[] desc, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			this.SetSubMeshes(desc, 0, (desc != null) ? desc.Length : 0, flags);
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x0002749F File Offset: 0x0002569F
		public void SetSubMeshes(List<SubMeshDescriptor> desc, int start, int count, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			this.SetSubMeshes(NoAllocHelpers.ExtractArrayFromList<SubMeshDescriptor>(desc), start, count, flags);
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x000274B3 File Offset: 0x000256B3
		public void SetSubMeshes(List<SubMeshDescriptor> desc, MeshUpdateFlags flags = MeshUpdateFlags.Default)
		{
			this.SetSubMeshes(NoAllocHelpers.ExtractArrayFromList<SubMeshDescriptor>(desc), 0, (desc != null) ? desc.Count : 0, flags);
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x000274D4 File Offset: 0x000256D4
		public void SetSubMeshes<T>(NativeArray<T> desc, int start, int count, MeshUpdateFlags flags = MeshUpdateFlags.Default) where T : struct
		{
			bool flag = UnsafeUtility.SizeOf<T>() != UnsafeUtility.SizeOf<SubMeshDescriptor>();
			if (flag)
			{
				throw new ArgumentException(string.Format("{0} with NativeArray should use struct type that is {1} bytes in size", "SetSubMeshes", UnsafeUtility.SizeOf<SubMeshDescriptor>()));
			}
			bool flag2 = start < 0 || count < 0 || start + count > desc.Length;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (start:{0} count:{1} desc.Length:{2})", start, count, desc.Length));
			}
			this.SetAllSubMeshesAtOnceFromNativeArray((IntPtr)desc.GetUnsafeReadOnlyPtr<T>(), start, count, flags);
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x0002756D File Offset: 0x0002576D
		public void SetSubMeshes<T>(NativeArray<T> desc, MeshUpdateFlags flags = MeshUpdateFlags.Default) where T : struct
		{
			this.SetSubMeshes<T>(desc, 0, desc.Length, flags);
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x00027584 File Offset: 0x00025784
		public void GetBindposes(List<Matrix4x4> bindposes)
		{
			bool flag = bindposes == null;
			if (flag)
			{
				throw new ArgumentNullException("bindposes", "The result bindposes list cannot be null.");
			}
			NoAllocHelpers.EnsureListElemCount<Matrix4x4>(bindposes, this.bindposeCount);
			this.GetBindposesNonAllocImpl(NoAllocHelpers.ExtractArrayFromList<Matrix4x4>(bindposes));
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x000275C4 File Offset: 0x000257C4
		public void GetBoneWeights(List<BoneWeight> boneWeights)
		{
			bool flag = boneWeights == null;
			if (flag)
			{
				throw new ArgumentNullException("boneWeights", "The result boneWeights list cannot be null.");
			}
			bool flag2 = this.HasBoneWeights();
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<BoneWeight>(boneWeights, this.vertexCount);
			}
			this.GetBoneWeightsNonAllocImpl(NoAllocHelpers.ExtractArrayFromList<BoneWeight>(boneWeights));
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001682 RID: 5762 RVA: 0x00027610 File Offset: 0x00025810
		// (set) Token: 0x06001683 RID: 5763 RVA: 0x00027628 File Offset: 0x00025828
		public BoneWeight[] boneWeights
		{
			get
			{
				return this.GetBoneWeightsImpl();
			}
			set
			{
				this.SetBoneWeightsImpl(value);
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x00027634 File Offset: 0x00025834
		public SkinWeights skinWeightBufferLayout
		{
			get
			{
				return (SkinWeights)this.GetBoneWeightBufferLayoutInternal();
			}
		}

		// Token: 0x06001685 RID: 5765 RVA: 0x0002764C File Offset: 0x0002584C
		public void Clear([DefaultValue("true")] bool keepVertexLayout)
		{
			this.ClearImpl(keepVertexLayout);
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x00027657 File Offset: 0x00025857
		[ExcludeFromDocs]
		public void Clear()
		{
			this.ClearImpl(true);
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x00027662 File Offset: 0x00025862
		[ExcludeFromDocs]
		public void RecalculateBounds()
		{
			this.RecalculateBounds(MeshUpdateFlags.Default);
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x0002766D File Offset: 0x0002586D
		[ExcludeFromDocs]
		public void RecalculateNormals()
		{
			this.RecalculateNormals(MeshUpdateFlags.Default);
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x00027678 File Offset: 0x00025878
		[ExcludeFromDocs]
		public void RecalculateTangents()
		{
			this.RecalculateTangents(MeshUpdateFlags.Default);
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x00027684 File Offset: 0x00025884
		public void RecalculateBounds([DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.RecalculateBoundsImpl(flags);
			}
			else
			{
				Debug.LogError(string.Format("Not allowed to call RecalculateBounds() on mesh '{0}'", base.name));
			}
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x000276BC File Offset: 0x000258BC
		public void RecalculateNormals([DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.RecalculateNormalsImpl(flags);
			}
			else
			{
				Debug.LogError(string.Format("Not allowed to call RecalculateNormals() on mesh '{0}'", base.name));
			}
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x000276F4 File Offset: 0x000258F4
		public void RecalculateTangents([DefaultValue("MeshUpdateFlags.Default")] MeshUpdateFlags flags)
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.RecalculateTangentsImpl(flags);
			}
			else
			{
				Debug.LogError(string.Format("Not allowed to call RecalculateTangents() on mesh '{0}'", base.name));
			}
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x0002772C File Offset: 0x0002592C
		public void RecalculateUVDistributionMetric(int uvSetIndex, float uvAreaThreshold = 1E-09f)
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.RecalculateUVDistributionMetricImpl(uvSetIndex, uvAreaThreshold);
			}
			else
			{
				Debug.LogError(string.Format("Not allowed to call RecalculateUVDistributionMetric() on mesh '{0}'", base.name));
			}
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x00027768 File Offset: 0x00025968
		public void RecalculateUVDistributionMetrics(float uvAreaThreshold = 1E-09f)
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.RecalculateUVDistributionMetricsImpl(uvAreaThreshold);
			}
			else
			{
				Debug.LogError(string.Format("Not allowed to call RecalculateUVDistributionMetrics() on mesh '{0}'", base.name));
			}
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x000277A0 File Offset: 0x000259A0
		public void MarkDynamic()
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.MarkDynamicImpl();
			}
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x000277C0 File Offset: 0x000259C0
		public void UploadMeshData(bool markNoLongerReadable)
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.UploadMeshDataImpl(markNoLongerReadable);
			}
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x000277E0 File Offset: 0x000259E0
		public void Optimize()
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.OptimizeImpl();
			}
			else
			{
				Debug.LogError(string.Format("Not allowed to call Optimize() on mesh '{0}'", base.name));
			}
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x00027818 File Offset: 0x00025A18
		public void OptimizeIndexBuffers()
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.OptimizeIndexBuffersImpl();
			}
			else
			{
				Debug.LogError(string.Format("Not allowed to call OptimizeIndexBuffers() on mesh '{0}'", base.name));
			}
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x00027850 File Offset: 0x00025A50
		public void OptimizeReorderVertexBuffer()
		{
			bool canAccess = this.canAccess;
			if (canAccess)
			{
				this.OptimizeReorderVertexBufferImpl();
			}
			else
			{
				Debug.LogError(string.Format("Not allowed to call OptimizeReorderVertexBuffer() on mesh '{0}'", base.name));
			}
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x00027888 File Offset: 0x00025A88
		public MeshTopology GetTopology(int submesh)
		{
			bool flag = submesh < 0 || submesh >= this.subMeshCount;
			MeshTopology meshTopology;
			if (flag)
			{
				Debug.LogError("Failed getting topology. Submesh index is out of bounds.", this);
				meshTopology = MeshTopology.Triangles;
			}
			else
			{
				meshTopology = this.GetTopologyImpl(submesh);
			}
			return meshTopology;
		}

		// Token: 0x06001695 RID: 5781 RVA: 0x000278C9 File Offset: 0x00025AC9
		public void CombineMeshes(CombineInstance[] combine, [DefaultValue("true")] bool mergeSubMeshes, [DefaultValue("true")] bool useMatrices, [DefaultValue("false")] bool hasLightmapData)
		{
			this.CombineMeshesImpl(combine, mergeSubMeshes, useMatrices, hasLightmapData);
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x000278D8 File Offset: 0x00025AD8
		[ExcludeFromDocs]
		public void CombineMeshes(CombineInstance[] combine, bool mergeSubMeshes, bool useMatrices)
		{
			this.CombineMeshesImpl(combine, mergeSubMeshes, useMatrices, false);
		}

		// Token: 0x06001697 RID: 5783 RVA: 0x000278E6 File Offset: 0x00025AE6
		[ExcludeFromDocs]
		public void CombineMeshes(CombineInstance[] combine, bool mergeSubMeshes)
		{
			this.CombineMeshesImpl(combine, mergeSubMeshes, true, false);
		}

		// Token: 0x06001698 RID: 5784 RVA: 0x000278F4 File Offset: 0x00025AF4
		[ExcludeFromDocs]
		public void CombineMeshes(CombineInstance[] combine)
		{
			this.CombineMeshesImpl(combine, true, true, false);
		}

		// Token: 0x06001699 RID: 5785
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr FromInstanceID_Injected(int id);

		// Token: 0x0600169A RID: 5786
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IndexFormat get_indexFormat_Injected(IntPtr _unity_self);

		// Token: 0x0600169B RID: 5787
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_indexFormat_Injected(IntPtr _unity_self, IndexFormat value);

		// Token: 0x0600169C RID: 5788
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetTotalIndexCount_Injected(IntPtr _unity_self);

		// Token: 0x0600169D RID: 5789
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetIndexBufferParams_Injected(IntPtr _unity_self, int indexCount, IndexFormat format);

		// Token: 0x0600169E RID: 5790
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetIndexBufferData_Injected(IntPtr _unity_self, IntPtr data, int dataStart, int meshBufferStart, int count, int elemSize, MeshUpdateFlags flags);

		// Token: 0x0600169F RID: 5791
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetIndexBufferDataFromArray_Injected(IntPtr _unity_self, Array data, int dataStart, int meshBufferStart, int count, int elemSize, MeshUpdateFlags flags);

		// Token: 0x060016A0 RID: 5792
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVertexBufferParamsFromPtr_Injected(IntPtr _unity_self, int vertexCount, IntPtr attributesPtr, int attributesCount);

		// Token: 0x060016A1 RID: 5793
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetVertexBufferParamsFromArray_Injected(IntPtr _unity_self, int vertexCount, params ManagedSpanWrapper attributes);

		// Token: 0x060016A2 RID: 5794
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetVertexBufferData_Injected(IntPtr _unity_self, int stream, IntPtr data, int dataStart, int meshBufferStart, int count, int elemSize, MeshUpdateFlags flags);

		// Token: 0x060016A3 RID: 5795
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetVertexBufferDataFromArray_Injected(IntPtr _unity_self, int stream, Array data, int dataStart, int meshBufferStart, int count, int elemSize, MeshUpdateFlags flags);

		// Token: 0x060016A4 RID: 5796
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Array GetVertexAttributesAlloc_Injected(IntPtr _unity_self);

		// Token: 0x060016A5 RID: 5797
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetVertexAttributesArray_Injected(IntPtr _unity_self, ref ManagedSpanWrapper attributes);

		// Token: 0x060016A6 RID: 5798
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetVertexAttributesList_Injected(IntPtr _unity_self, ref BlittableListWrapper attributes);

		// Token: 0x060016A7 RID: 5799
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetVertexAttributeCountImpl_Injected(IntPtr _unity_self);

		// Token: 0x060016A8 RID: 5800
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetVertexAttribute_Injected(IntPtr _unity_self, int index, out VertexAttributeDescriptor ret);

		// Token: 0x060016A9 RID: 5801
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetIndexStartImpl_Injected(IntPtr _unity_self, int submesh);

		// Token: 0x060016AA RID: 5802
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetIndexCountImpl_Injected(IntPtr _unity_self, int submesh);

		// Token: 0x060016AB RID: 5803
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetTrianglesCountImpl_Injected(IntPtr _unity_self, int submesh);

		// Token: 0x060016AC RID: 5804
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetBaseVertexImpl_Injected(IntPtr _unity_self, int submesh);

		// Token: 0x060016AD RID: 5805
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTrianglesImpl_Injected(IntPtr _unity_self, int submesh, bool applyBaseVertex, out BlittableArrayWrapper ret);

		// Token: 0x060016AE RID: 5806
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetIndicesImpl_Injected(IntPtr _unity_self, int submesh, bool applyBaseVertex, out BlittableArrayWrapper ret);

		// Token: 0x060016AF RID: 5807
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetIndicesImpl_Injected(IntPtr _unity_self, int submesh, MeshTopology topology, IndexFormat indicesFormat, Array indices, int arrayStart, int arraySize, bool calculateBounds, int baseVertex);

		// Token: 0x060016B0 RID: 5808
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetIndicesNativeArrayImpl_Injected(IntPtr _unity_self, int submesh, MeshTopology topology, IndexFormat indicesFormat, IntPtr indices, int arrayStart, int arraySize, bool calculateBounds, int baseVertex);

		// Token: 0x060016B1 RID: 5809
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTrianglesNonAllocImpl_Injected(IntPtr _unity_self, out BlittableArrayWrapper values, int submesh, bool applyBaseVertex);

		// Token: 0x060016B2 RID: 5810
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetTrianglesNonAllocImpl16_Injected(IntPtr _unity_self, out BlittableArrayWrapper values, int submesh, bool applyBaseVertex);

		// Token: 0x060016B3 RID: 5811
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetIndicesNonAllocImpl_Injected(IntPtr _unity_self, out BlittableArrayWrapper values, int submesh, bool applyBaseVertex);

		// Token: 0x060016B4 RID: 5812
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetIndicesNonAllocImpl16_Injected(IntPtr _unity_self, out BlittableArrayWrapper values, int submesh, bool applyBaseVertex);

		// Token: 0x060016B5 RID: 5813
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PrintErrorCantAccessChannel_Injected(IntPtr _unity_self, VertexAttribute ch);

		// Token: 0x060016B6 RID: 5814
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasVertexAttribute_Injected(IntPtr _unity_self, VertexAttribute attr);

		// Token: 0x060016B7 RID: 5815
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetVertexAttributeDimension_Injected(IntPtr _unity_self, VertexAttribute attr);

		// Token: 0x060016B8 RID: 5816
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern VertexAttributeFormat GetVertexAttributeFormat_Injected(IntPtr _unity_self, VertexAttribute attr);

		// Token: 0x060016B9 RID: 5817
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetVertexAttributeStream_Injected(IntPtr _unity_self, VertexAttribute attr);

		// Token: 0x060016BA RID: 5818
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetVertexAttributeOffset_Injected(IntPtr _unity_self, VertexAttribute attr);

		// Token: 0x060016BB RID: 5819
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetArrayForChannelImpl_Injected(IntPtr _unity_self, VertexAttribute channel, VertexAttributeFormat format, int dim, Array values, int arraySize, int valuesStart, int valuesCount, MeshUpdateFlags flags);

		// Token: 0x060016BC RID: 5820
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetNativeArrayForChannelImpl_Injected(IntPtr _unity_self, VertexAttribute channel, VertexAttributeFormat format, int dim, IntPtr values, int arraySize, int valuesStart, int valuesCount, MeshUpdateFlags flags);

		// Token: 0x060016BD RID: 5821
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Array GetAllocArrayFromChannelImpl_Injected(IntPtr _unity_self, VertexAttribute channel, VertexAttributeFormat format, int dim);

		// Token: 0x060016BE RID: 5822
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetArrayFromChannelImpl_Injected(IntPtr _unity_self, VertexAttribute channel, VertexAttributeFormat format, int dim, Array values);

		// Token: 0x060016BF RID: 5823
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_vertexBufferCount_Injected(IntPtr _unity_self);

		// Token: 0x060016C0 RID: 5824
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetVertexBufferStride_Injected(IntPtr _unity_self, int stream);

		// Token: 0x060016C1 RID: 5825
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetNativeVertexBufferPtr_Injected(IntPtr _unity_self, int index);

		// Token: 0x060016C2 RID: 5826
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetNativeIndexBufferPtr_Injected(IntPtr _unity_self);

		// Token: 0x060016C3 RID: 5827
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetVertexBufferImpl_Injected(IntPtr _unity_self, int index);

		// Token: 0x060016C4 RID: 5828
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetIndexBufferImpl_Injected(IntPtr _unity_self);

		// Token: 0x060016C5 RID: 5829
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetBoneWeightBufferImpl_Injected(IntPtr _unity_self, int bonesPerVertex);

		// Token: 0x060016C6 RID: 5830
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetBlendShapeBufferImpl_Injected(IntPtr _unity_self, int layout);

		// Token: 0x060016C7 RID: 5831
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsBuffer.Target get_vertexBufferTarget_Injected(IntPtr _unity_self);

		// Token: 0x060016C8 RID: 5832
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_vertexBufferTarget_Injected(IntPtr _unity_self, GraphicsBuffer.Target value);

		// Token: 0x060016C9 RID: 5833
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsBuffer.Target get_indexBufferTarget_Injected(IntPtr _unity_self);

		// Token: 0x060016CA RID: 5834
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_indexBufferTarget_Injected(IntPtr _unity_self, GraphicsBuffer.Target value);

		// Token: 0x060016CB RID: 5835
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_blendShapeCount_Injected(IntPtr _unity_self);

		// Token: 0x060016CC RID: 5836
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearBlendShapes_Injected(IntPtr _unity_self);

		// Token: 0x060016CD RID: 5837
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetBlendShapeName_Injected(IntPtr _unity_self, int shapeIndex, out ManagedSpanWrapper ret);

		// Token: 0x060016CE RID: 5838
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetBlendShapeIndex_Injected(IntPtr _unity_self, ref ManagedSpanWrapper blendShapeName);

		// Token: 0x060016CF RID: 5839
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetBlendShapeFrameCount_Injected(IntPtr _unity_self, int shapeIndex);

		// Token: 0x060016D0 RID: 5840
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetBlendShapeFrameWeight_Injected(IntPtr _unity_self, int shapeIndex, int frameIndex);

		// Token: 0x060016D1 RID: 5841
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetBlendShapeFrameVertices_Injected(IntPtr _unity_self, int shapeIndex, int frameIndex, ref ManagedSpanWrapper deltaVertices, ref ManagedSpanWrapper deltaNormals, ref ManagedSpanWrapper deltaTangents);

		// Token: 0x060016D2 RID: 5842
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddBlendShapeFrame_Injected(IntPtr _unity_self, ref ManagedSpanWrapper shapeName, float frameWeight, ref ManagedSpanWrapper deltaVertices, ref ManagedSpanWrapper deltaNormals, ref ManagedSpanWrapper deltaTangents);

		// Token: 0x060016D3 RID: 5843
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetBlendShapeOffsetInternal_Injected(IntPtr _unity_self, int index, out BlendShape ret);

		// Token: 0x060016D4 RID: 5844
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasBoneWeights_Injected(IntPtr _unity_self);

		// Token: 0x060016D5 RID: 5845
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetBoneWeightsImpl_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x060016D6 RID: 5846
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBoneWeightsImpl_Injected(IntPtr _unity_self, ref ManagedSpanWrapper weights);

		// Token: 0x060016D7 RID: 5847
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalSetBoneWeights_Injected(IntPtr _unity_self, IntPtr bonesPerVertex, int bonesPerVertexSize, IntPtr weights, int weightsSize);

		// Token: 0x060016D8 RID: 5848
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetAllBoneWeightsArraySize_Injected(IntPtr _unity_self);

		// Token: 0x060016D9 RID: 5849
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetBoneWeightBufferLayoutInternal_Injected(IntPtr _unity_self);

		// Token: 0x060016DA RID: 5850
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetAllBoneWeightsArray_Injected(IntPtr _unity_self);

		// Token: 0x060016DB RID: 5851
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetBonesPerVertexArray_Injected(IntPtr _unity_self);

		// Token: 0x060016DC RID: 5852
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_bindposeCount_Injected(IntPtr _unity_self);

		// Token: 0x060016DD RID: 5853
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_bindposes_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x060016DE RID: 5854
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bindposes_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x060016DF RID: 5855
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBindposesFromScript_NativeArray_Injected(IntPtr _unity_self, IntPtr posesPtr, int posesCount);

		// Token: 0x060016E0 RID: 5856
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetBindposesArray_Injected(IntPtr _unity_self);

		// Token: 0x060016E1 RID: 5857
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetBoneWeightsNonAllocImpl_Injected(IntPtr _unity_self, out BlittableArrayWrapper values);

		// Token: 0x060016E2 RID: 5858
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetBindposesNonAllocImpl_Injected(IntPtr _unity_self, out BlittableArrayWrapper values);

		// Token: 0x060016E3 RID: 5859
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isReadable_Injected(IntPtr _unity_self);

		// Token: 0x060016E4 RID: 5860
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_canAccess_Injected(IntPtr _unity_self);

		// Token: 0x060016E5 RID: 5861
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_vertexCount_Injected(IntPtr _unity_self);

		// Token: 0x060016E6 RID: 5862
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_subMeshCount_Injected(IntPtr _unity_self);

		// Token: 0x060016E7 RID: 5863
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_subMeshCount_Injected(IntPtr _unity_self, int value);

		// Token: 0x060016E8 RID: 5864
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSubMesh_Injected(IntPtr _unity_self, int index, [In] ref SubMeshDescriptor desc, MeshUpdateFlags flags);

		// Token: 0x060016E9 RID: 5865
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSubMesh_Injected(IntPtr _unity_self, int index, out SubMeshDescriptor ret);

		// Token: 0x060016EA RID: 5866
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAllSubMeshesAtOnceFromArray_Injected(IntPtr _unity_self, ref ManagedSpanWrapper desc, int start, int count, MeshUpdateFlags flags);

		// Token: 0x060016EB RID: 5867
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAllSubMeshesAtOnceFromNativeArray_Injected(IntPtr _unity_self, IntPtr desc, int start, int count, MeshUpdateFlags flags);

		// Token: 0x060016EC RID: 5868
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_bounds_Injected(IntPtr _unity_self, out Bounds ret);

		// Token: 0x060016ED RID: 5869
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bounds_Injected(IntPtr _unity_self, [In] ref Bounds value);

		// Token: 0x060016EE RID: 5870
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearImpl_Injected(IntPtr _unity_self, bool keepVertexLayout);

		// Token: 0x060016EF RID: 5871
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RecalculateBoundsImpl_Injected(IntPtr _unity_self, MeshUpdateFlags flags);

		// Token: 0x060016F0 RID: 5872
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RecalculateNormalsImpl_Injected(IntPtr _unity_self, MeshUpdateFlags flags);

		// Token: 0x060016F1 RID: 5873
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RecalculateTangentsImpl_Injected(IntPtr _unity_self, MeshUpdateFlags flags);

		// Token: 0x060016F2 RID: 5874
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MarkDynamicImpl_Injected(IntPtr _unity_self);

		// Token: 0x060016F3 RID: 5875
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MarkModified_Injected(IntPtr _unity_self);

		// Token: 0x060016F4 RID: 5876
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UploadMeshDataImpl_Injected(IntPtr _unity_self, bool markNoLongerReadable);

		// Token: 0x060016F5 RID: 5877
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern MeshTopology GetTopologyImpl_Injected(IntPtr _unity_self, int submesh);

		// Token: 0x060016F6 RID: 5878
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RecalculateUVDistributionMetricImpl_Injected(IntPtr _unity_self, int uvSetIndex, float uvAreaThreshold);

		// Token: 0x060016F7 RID: 5879
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RecalculateUVDistributionMetricsImpl_Injected(IntPtr _unity_self, float uvAreaThreshold);

		// Token: 0x060016F8 RID: 5880
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetUVDistributionMetric_Injected(IntPtr _unity_self, int uvSetIndex);

		// Token: 0x060016F9 RID: 5881
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CombineMeshesImpl_Injected(IntPtr _unity_self, ref ManagedSpanWrapper combine, bool mergeSubMeshes, bool useMatrices, bool hasLightmapData);

		// Token: 0x060016FA RID: 5882
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OptimizeImpl_Injected(IntPtr _unity_self);

		// Token: 0x060016FB RID: 5883
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OptimizeIndexBuffersImpl_Injected(IntPtr _unity_self);

		// Token: 0x060016FC RID: 5884
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OptimizeReorderVertexBufferImpl_Injected(IntPtr _unity_self);

		// Token: 0x020001E9 RID: 489
		[StaticAccessor("MeshDataBindings", StaticAccessorType.DoubleColon)]
		[NativeHeader("Runtime/Graphics/Mesh/MeshScriptBindings.h")]
		public struct MeshData
		{
			// Token: 0x060016FD RID: 5885
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern bool HasVertexAttribute(IntPtr self, VertexAttribute attr);

			// Token: 0x060016FE RID: 5886
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetVertexAttributeDimension(IntPtr self, VertexAttribute attr);

			// Token: 0x060016FF RID: 5887
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern VertexAttributeFormat GetVertexAttributeFormat(IntPtr self, VertexAttribute attr);

			// Token: 0x06001700 RID: 5888
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetVertexAttributeStream(IntPtr self, VertexAttribute attr);

			// Token: 0x06001701 RID: 5889
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetVertexAttributeOffset(IntPtr self, VertexAttribute attr);

			// Token: 0x06001702 RID: 5890
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetVertexCount(IntPtr self);

			// Token: 0x06001703 RID: 5891
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetVertexBufferCount(IntPtr self);

			// Token: 0x06001704 RID: 5892
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern IntPtr GetVertexDataPtr(IntPtr self, int stream);

			// Token: 0x06001705 RID: 5893
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ulong GetVertexDataSize(IntPtr self, int stream);

			// Token: 0x06001706 RID: 5894
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetVertexBufferStride(IntPtr self, int stream);

			// Token: 0x06001707 RID: 5895
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void CopyAttributeIntoPtr(IntPtr self, VertexAttribute attr, VertexAttributeFormat format, int dim, IntPtr dst);

			// Token: 0x06001708 RID: 5896
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void CopyIndicesIntoPtr(IntPtr self, int submesh, bool applyBaseVertex, int dstStride, IntPtr dst);

			// Token: 0x06001709 RID: 5897
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern IndexFormat GetIndexFormat(IntPtr self);

			// Token: 0x0600170A RID: 5898
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetIndexCount(IntPtr self, int submesh);

			// Token: 0x0600170B RID: 5899
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern IntPtr GetIndexDataPtr(IntPtr self);

			// Token: 0x0600170C RID: 5900
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern ulong GetIndexDataSize(IntPtr self);

			// Token: 0x0600170D RID: 5901
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetSubMeshCount(IntPtr self);

			// Token: 0x0600170E RID: 5902 RVA: 0x00027904 File Offset: 0x00025B04
			[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
			private static SubMeshDescriptor GetSubMesh(IntPtr self, int index)
			{
				SubMeshDescriptor subMeshDescriptor;
				Mesh.MeshData.GetSubMesh_Injected(self, index, out subMeshDescriptor);
				return subMeshDescriptor;
			}

			// Token: 0x0600170F RID: 5903
			[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetVertexBufferParamsFromPtr(IntPtr self, int vertexCount, IntPtr attributesPtr, int attributesCount);

			// Token: 0x06001710 RID: 5904 RVA: 0x0002791C File Offset: 0x00025B1C
			[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
			private unsafe static void SetVertexBufferParamsFromArray(IntPtr self, int vertexCount, params VertexAttributeDescriptor[] attributes)
			{
				Span<VertexAttributeDescriptor> span = new Span<VertexAttributeDescriptor>(attributes);
				fixed (VertexAttributeDescriptor* pinnableReference = span.GetPinnableReference())
				{
					ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
					Mesh.MeshData.SetVertexBufferParamsFromArray_Injected(self, vertexCount, ref managedSpanWrapper);
				}
			}

			// Token: 0x06001711 RID: 5905
			[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetIndexBufferParamsImpl(IntPtr self, int indexCount, IndexFormat indexFormat);

			// Token: 0x06001712 RID: 5906
			[NativeMethod(IsThreadSafe = true)]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetSubMeshCount(IntPtr self, int count);

			// Token: 0x06001713 RID: 5907 RVA: 0x00027958 File Offset: 0x00025B58
			[NativeMethod(IsThreadSafe = true, ThrowsException = true)]
			private static void SetSubMeshImpl(IntPtr self, int index, SubMeshDescriptor desc, MeshUpdateFlags flags)
			{
				Mesh.MeshData.SetSubMeshImpl_Injected(self, index, ref desc, flags);
			}

			// Token: 0x170003C5 RID: 965
			// (get) Token: 0x06001714 RID: 5908 RVA: 0x00027970 File Offset: 0x00025B70
			public int vertexCount
			{
				get
				{
					return Mesh.MeshData.GetVertexCount(this.m_Ptr);
				}
			}

			// Token: 0x170003C6 RID: 966
			// (get) Token: 0x06001715 RID: 5909 RVA: 0x00027990 File Offset: 0x00025B90
			public int vertexBufferCount
			{
				get
				{
					return Mesh.MeshData.GetVertexBufferCount(this.m_Ptr);
				}
			}

			// Token: 0x06001716 RID: 5910 RVA: 0x000279B0 File Offset: 0x00025BB0
			public int GetVertexBufferStride(int stream)
			{
				return Mesh.MeshData.GetVertexBufferStride(this.m_Ptr, stream);
			}

			// Token: 0x06001717 RID: 5911 RVA: 0x000279D0 File Offset: 0x00025BD0
			public bool HasVertexAttribute(VertexAttribute attr)
			{
				return Mesh.MeshData.HasVertexAttribute(this.m_Ptr, attr);
			}

			// Token: 0x06001718 RID: 5912 RVA: 0x000279F0 File Offset: 0x00025BF0
			public int GetVertexAttributeDimension(VertexAttribute attr)
			{
				return Mesh.MeshData.GetVertexAttributeDimension(this.m_Ptr, attr);
			}

			// Token: 0x06001719 RID: 5913 RVA: 0x00027A10 File Offset: 0x00025C10
			public VertexAttributeFormat GetVertexAttributeFormat(VertexAttribute attr)
			{
				return Mesh.MeshData.GetVertexAttributeFormat(this.m_Ptr, attr);
			}

			// Token: 0x0600171A RID: 5914 RVA: 0x00027A30 File Offset: 0x00025C30
			public int GetVertexAttributeStream(VertexAttribute attr)
			{
				return Mesh.MeshData.GetVertexAttributeStream(this.m_Ptr, attr);
			}

			// Token: 0x0600171B RID: 5915 RVA: 0x00027A50 File Offset: 0x00025C50
			public int GetVertexAttributeOffset(VertexAttribute attr)
			{
				return Mesh.MeshData.GetVertexAttributeOffset(this.m_Ptr, attr);
			}

			// Token: 0x0600171C RID: 5916 RVA: 0x00027A6E File Offset: 0x00025C6E
			public void GetVertices(NativeArray<Vector3> outVertices)
			{
				this.CopyAttributeInto<Vector3>(outVertices, VertexAttribute.Position, VertexAttributeFormat.Float32, 3);
			}

			// Token: 0x0600171D RID: 5917 RVA: 0x00027A7C File Offset: 0x00025C7C
			public void GetNormals(NativeArray<Vector3> outNormals)
			{
				this.CopyAttributeInto<Vector3>(outNormals, VertexAttribute.Normal, VertexAttributeFormat.Float32, 3);
			}

			// Token: 0x0600171E RID: 5918 RVA: 0x00027A8A File Offset: 0x00025C8A
			public void GetTangents(NativeArray<Vector4> outTangents)
			{
				this.CopyAttributeInto<Vector4>(outTangents, VertexAttribute.Tangent, VertexAttributeFormat.Float32, 4);
			}

			// Token: 0x0600171F RID: 5919 RVA: 0x00027A98 File Offset: 0x00025C98
			public void GetColors(NativeArray<Color> outColors)
			{
				this.CopyAttributeInto<Color>(outColors, VertexAttribute.Color, VertexAttributeFormat.Float32, 4);
			}

			// Token: 0x06001720 RID: 5920 RVA: 0x00027AA6 File Offset: 0x00025CA6
			public void GetColors(NativeArray<Color32> outColors)
			{
				this.CopyAttributeInto<Color32>(outColors, VertexAttribute.Color, VertexAttributeFormat.UNorm8, 4);
			}

			// Token: 0x06001721 RID: 5921 RVA: 0x00027AB4 File Offset: 0x00025CB4
			public void GetUVs(int channel, NativeArray<Vector2> outUVs)
			{
				bool flag = channel < 0 || channel > 7;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("channel", channel, "The uv index is invalid. Must be in the range 0 to 7.");
				}
				this.CopyAttributeInto<Vector2>(outUVs, Mesh.GetUVChannel(channel), VertexAttributeFormat.Float32, 2);
			}

			// Token: 0x06001722 RID: 5922 RVA: 0x00027AF8 File Offset: 0x00025CF8
			public void GetUVs(int channel, NativeArray<Vector3> outUVs)
			{
				bool flag = channel < 0 || channel > 7;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("channel", channel, "The uv index is invalid. Must be in the range 0 to 7.");
				}
				this.CopyAttributeInto<Vector3>(outUVs, Mesh.GetUVChannel(channel), VertexAttributeFormat.Float32, 3);
			}

			// Token: 0x06001723 RID: 5923 RVA: 0x00027B3C File Offset: 0x00025D3C
			public void GetUVs(int channel, NativeArray<Vector4> outUVs)
			{
				bool flag = channel < 0 || channel > 7;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("channel", channel, "The uv index is invalid. Must be in the range 0 to 7.");
				}
				this.CopyAttributeInto<Vector4>(outUVs, Mesh.GetUVChannel(channel), VertexAttributeFormat.Float32, 4);
			}

			// Token: 0x06001724 RID: 5924 RVA: 0x00027B80 File Offset: 0x00025D80
			public unsafe NativeArray<T> GetVertexData<T>([DefaultValue("0")] int stream = 0) where T : struct
			{
				bool flag = stream < 0 || stream >= this.vertexBufferCount;
				if (flag)
				{
					throw new ArgumentOutOfRangeException(string.Format("{0} out of bounds, should be below {1} but was {2}", "stream", this.vertexBufferCount, stream));
				}
				ulong vertexDataSize = Mesh.MeshData.GetVertexDataSize(this.m_Ptr, stream);
				ulong num = (ulong)((long)UnsafeUtility.SizeOf<T>());
				bool flag2 = vertexDataSize % num > 0UL;
				if (flag2)
				{
					throw new ArgumentException(string.Format("Type passed to {0} can't capture the vertex buffer. Mesh vertex buffer size is {1} which is not a multiple of type size {2}", "GetVertexData", vertexDataSize, num));
				}
				ulong num2 = vertexDataSize / num;
				return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)Mesh.MeshData.GetVertexDataPtr(this.m_Ptr, stream), (int)num2, Allocator.None);
			}

			// Token: 0x06001725 RID: 5925 RVA: 0x00027C38 File Offset: 0x00025E38
			private void CopyAttributeInto<T>(NativeArray<T> buffer, VertexAttribute channel, VertexAttributeFormat format, int dim) where T : struct
			{
				bool flag = !this.HasVertexAttribute(channel);
				if (flag)
				{
					throw new InvalidOperationException(string.Format("Mesh data does not have {0} vertex component", channel));
				}
				bool flag2 = buffer.Length < this.vertexCount;
				if (flag2)
				{
					throw new InvalidOperationException(string.Format("Not enough space in output buffer (need {0}, has {1})", this.vertexCount, buffer.Length));
				}
				Mesh.MeshData.CopyAttributeIntoPtr(this.m_Ptr, channel, format, dim, (IntPtr)buffer.GetUnsafePtr<T>());
			}

			// Token: 0x06001726 RID: 5926 RVA: 0x00027CBF File Offset: 0x00025EBF
			public void SetVertexBufferParams(int vertexCount, params VertexAttributeDescriptor[] attributes)
			{
				Mesh.MeshData.SetVertexBufferParamsFromArray(this.m_Ptr, vertexCount, attributes);
			}

			// Token: 0x06001727 RID: 5927 RVA: 0x00027CD0 File Offset: 0x00025ED0
			public void SetVertexBufferParams(int vertexCount, NativeArray<VertexAttributeDescriptor> attributes)
			{
				Mesh.MeshData.SetVertexBufferParamsFromPtr(this.m_Ptr, vertexCount, (IntPtr)attributes.GetUnsafeReadOnlyPtr<VertexAttributeDescriptor>(), attributes.Length);
			}

			// Token: 0x06001728 RID: 5928 RVA: 0x00027CF4 File Offset: 0x00025EF4
			public void SetIndexBufferParams(int indexCount, IndexFormat format)
			{
				Mesh.MeshData.SetIndexBufferParamsImpl(this.m_Ptr, indexCount, format);
			}

			// Token: 0x170003C7 RID: 967
			// (get) Token: 0x06001729 RID: 5929 RVA: 0x00027D08 File Offset: 0x00025F08
			public IndexFormat indexFormat
			{
				get
				{
					return Mesh.MeshData.GetIndexFormat(this.m_Ptr);
				}
			}

			// Token: 0x0600172A RID: 5930 RVA: 0x00027D28 File Offset: 0x00025F28
			public void GetIndices(NativeArray<ushort> outIndices, int submesh, [DefaultValue("true")] bool applyBaseVertex = true)
			{
				bool flag = submesh < 0 || submesh >= this.subMeshCount;
				if (flag)
				{
					throw new IndexOutOfRangeException(string.Format("Specified submesh ({0}) is out of range. Must be greater or equal to 0 and less than subMeshCount ({1}).", submesh, this.subMeshCount));
				}
				int indexCount = Mesh.MeshData.GetIndexCount(this.m_Ptr, submesh);
				bool flag2 = outIndices.Length < indexCount;
				if (flag2)
				{
					throw new InvalidOperationException(string.Format("Not enough space in output buffer (need {0}, has {1})", indexCount, outIndices.Length));
				}
				Mesh.MeshData.CopyIndicesIntoPtr(this.m_Ptr, submesh, applyBaseVertex, 2, (IntPtr)outIndices.GetUnsafePtr<ushort>());
			}

			// Token: 0x0600172B RID: 5931 RVA: 0x00027DC8 File Offset: 0x00025FC8
			public void GetIndices(NativeArray<int> outIndices, int submesh, [DefaultValue("true")] bool applyBaseVertex = true)
			{
				bool flag = submesh < 0 || submesh >= this.subMeshCount;
				if (flag)
				{
					throw new IndexOutOfRangeException(string.Format("Specified submesh ({0}) is out of range. Must be greater or equal to 0 and less than subMeshCount ({1}).", submesh, this.subMeshCount));
				}
				int indexCount = Mesh.MeshData.GetIndexCount(this.m_Ptr, submesh);
				bool flag2 = outIndices.Length < indexCount;
				if (flag2)
				{
					throw new InvalidOperationException(string.Format("Not enough space in output buffer (need {0}, has {1})", indexCount, outIndices.Length));
				}
				Mesh.MeshData.CopyIndicesIntoPtr(this.m_Ptr, submesh, applyBaseVertex, 4, (IntPtr)outIndices.GetUnsafePtr<int>());
			}

			// Token: 0x0600172C RID: 5932 RVA: 0x00027E68 File Offset: 0x00026068
			public unsafe NativeArray<T> GetIndexData<T>() where T : struct
			{
				ulong indexDataSize = Mesh.MeshData.GetIndexDataSize(this.m_Ptr);
				ulong num = (ulong)((long)UnsafeUtility.SizeOf<T>());
				bool flag = indexDataSize % num > 0UL;
				if (flag)
				{
					throw new ArgumentException(string.Format("Type passed to {0} can't capture the index buffer. Mesh index buffer size is {1} which is not a multiple of type size {2}", "GetIndexData", indexDataSize, num));
				}
				ulong num2 = indexDataSize / num;
				return NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<T>((void*)Mesh.MeshData.GetIndexDataPtr(this.m_Ptr), (int)num2, Allocator.None);
			}

			// Token: 0x170003C8 RID: 968
			// (get) Token: 0x0600172D RID: 5933 RVA: 0x00027EDC File Offset: 0x000260DC
			// (set) Token: 0x0600172E RID: 5934 RVA: 0x00027EF9 File Offset: 0x000260F9
			public int subMeshCount
			{
				get
				{
					return Mesh.MeshData.GetSubMeshCount(this.m_Ptr);
				}
				set
				{
					Mesh.MeshData.SetSubMeshCount(this.m_Ptr, value);
				}
			}

			// Token: 0x0600172F RID: 5935 RVA: 0x00027F0C File Offset: 0x0002610C
			public SubMeshDescriptor GetSubMesh(int index)
			{
				return Mesh.MeshData.GetSubMesh(this.m_Ptr, index);
			}

			// Token: 0x06001730 RID: 5936 RVA: 0x00027F2A File Offset: 0x0002612A
			public void SetSubMesh(int index, SubMeshDescriptor desc, MeshUpdateFlags flags = MeshUpdateFlags.Default)
			{
				Mesh.MeshData.SetSubMeshImpl(this.m_Ptr, index, desc, flags);
			}

			// Token: 0x06001731 RID: 5937 RVA: 0x00002925 File Offset: 0x00000B25
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private void CheckReadAccess()
			{
			}

			// Token: 0x06001732 RID: 5938 RVA: 0x00002925 File Offset: 0x00000B25
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private void CheckWriteAccess()
			{
			}

			// Token: 0x06001733 RID: 5939
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void GetSubMesh_Injected(IntPtr self, int index, out SubMeshDescriptor ret);

			// Token: 0x06001734 RID: 5940
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetVertexBufferParamsFromArray_Injected(IntPtr self, int vertexCount, params ManagedSpanWrapper attributes);

			// Token: 0x06001735 RID: 5941
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void SetSubMeshImpl_Injected(IntPtr self, int index, [In] ref SubMeshDescriptor desc, MeshUpdateFlags flags);

			// Token: 0x040006C4 RID: 1732
			[NativeDisableUnsafePtrRestriction]
			internal IntPtr m_Ptr;
		}

		// Token: 0x020001EA RID: 490
		[NativeContainer]
		[NativeContainerSupportsMinMaxWriteRestriction]
		[StaticAccessor("MeshDataArrayBindings", StaticAccessorType.DoubleColon)]
		public struct MeshDataArray : IDisposable
		{
			// Token: 0x06001736 RID: 5942 RVA: 0x00027F3C File Offset: 0x0002613C
			private unsafe static void AcquireReadOnlyMeshData([NotNull] Mesh mesh, IntPtr* datas)
			{
				if (mesh == null)
				{
					ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
				}
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(mesh);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
				}
				Mesh.MeshDataArray.AcquireReadOnlyMeshData_Injected(intPtr, datas);
			}

			// Token: 0x06001737 RID: 5943 RVA: 0x00027F74 File Offset: 0x00026174
			private unsafe static void AcquireReadOnlyMeshDatas([NotNull] Mesh[] meshes, IntPtr* datas, int count)
			{
				if (meshes == null)
				{
					ThrowHelper.ThrowArgumentNullException(meshes, "meshes");
				}
				Mesh.MeshDataArray.AcquireReadOnlyMeshDatas_Injected(meshes, datas, count);
			}

			// Token: 0x06001738 RID: 5944 RVA: 0x00027F98 File Offset: 0x00026198
			private unsafe static void AcquireMeshDataCopy([NotNull] Mesh mesh, IntPtr* datas)
			{
				if (mesh == null)
				{
					ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
				}
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(mesh);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
				}
				Mesh.MeshDataArray.AcquireMeshDataCopy_Injected(intPtr, datas);
			}

			// Token: 0x06001739 RID: 5945 RVA: 0x00027FD0 File Offset: 0x000261D0
			private unsafe static void AcquireMeshDatasCopy([NotNull] Mesh[] meshes, IntPtr* datas, int count)
			{
				if (meshes == null)
				{
					ThrowHelper.ThrowArgumentNullException(meshes, "meshes");
				}
				Mesh.MeshDataArray.AcquireMeshDatasCopy_Injected(meshes, datas, count);
			}

			// Token: 0x0600173A RID: 5946
			[MethodImpl(MethodImplOptions.InternalCall)]
			private unsafe static extern void ReleaseMeshDatas(IntPtr* datas, int count);

			// Token: 0x0600173B RID: 5947
			[MethodImpl(MethodImplOptions.InternalCall)]
			private unsafe static extern void CreateNewMeshDatas(IntPtr* datas, int count);

			// Token: 0x0600173C RID: 5948 RVA: 0x00027FF4 File Offset: 0x000261F4
			[NativeThrows]
			private unsafe static void ApplyToMeshesImpl([NotNull] Mesh[] meshes, IntPtr* datas, int count, MeshUpdateFlags flags)
			{
				if (meshes == null)
				{
					ThrowHelper.ThrowArgumentNullException(meshes, "meshes");
				}
				Mesh.MeshDataArray.ApplyToMeshesImpl_Injected(meshes, datas, count, flags);
			}

			// Token: 0x0600173D RID: 5949 RVA: 0x0002801C File Offset: 0x0002621C
			[NativeThrows]
			private static void ApplyToMeshImpl([NotNull] Mesh mesh, IntPtr data, MeshUpdateFlags flags)
			{
				if (mesh == null)
				{
					ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
				}
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Mesh>(mesh);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(mesh, "mesh");
				}
				Mesh.MeshDataArray.ApplyToMeshImpl_Injected(intPtr, data, flags);
			}

			// Token: 0x170003C9 RID: 969
			// (get) Token: 0x0600173E RID: 5950 RVA: 0x00028054 File Offset: 0x00026254
			public int Length
			{
				get
				{
					return this.m_Length;
				}
			}

			// Token: 0x170003CA RID: 970
			public unsafe Mesh.MeshData this[int index]
			{
				get
				{
					Mesh.MeshData meshData;
					meshData.m_Ptr = this.m_Ptrs[(IntPtr)index * (IntPtr)sizeof(IntPtr) / (IntPtr)sizeof(IntPtr)];
					return meshData;
				}
			}

			// Token: 0x06001740 RID: 5952 RVA: 0x00028088 File Offset: 0x00026288
			public unsafe void Dispose()
			{
				UnsafeUtility.LeakErase((IntPtr)((void*)this.m_Ptrs), LeakCategory.MeshDataArray);
				bool flag = this.m_Length != 0;
				if (flag)
				{
					Mesh.MeshDataArray.ReleaseMeshDatas(this.m_Ptrs, this.m_Length);
					UnsafeUtility.Free((void*)this.m_Ptrs, Allocator.Persistent);
				}
				this.m_Ptrs = null;
				this.m_Length = 0;
			}

			// Token: 0x06001741 RID: 5953 RVA: 0x000280E8 File Offset: 0x000262E8
			internal unsafe void ApplyToMeshAndDispose(Mesh mesh, MeshUpdateFlags flags)
			{
				bool flag = !mesh.canAccess;
				if (flag)
				{
					throw new InvalidOperationException("Not allowed to access vertex data on mesh '" + mesh.name + "' (isReadable is false; Read/Write must be enabled in import settings)");
				}
				Mesh.MeshDataArray.ApplyToMeshImpl(mesh, *this.m_Ptrs, flags);
				this.Dispose();
			}

			// Token: 0x06001742 RID: 5954 RVA: 0x00028134 File Offset: 0x00026334
			internal void ApplyToMeshesAndDispose(Mesh[] meshes, MeshUpdateFlags flags)
			{
				for (int i = 0; i < this.m_Length; i++)
				{
					Mesh mesh = meshes[i];
					bool flag = mesh == null;
					if (flag)
					{
						throw new ArgumentNullException("meshes", string.Format("Mesh at index {0} is null", i));
					}
					bool flag2 = !mesh.canAccess;
					if (flag2)
					{
						throw new InvalidOperationException(string.Format("Not allowed to access vertex data on mesh '{0}' at array index {1} (isReadable is false; Read/Write must be enabled in import settings)", mesh.name, i));
					}
				}
				Mesh.MeshDataArray.ApplyToMeshesImpl(meshes, this.m_Ptrs, this.m_Length, flags);
				this.Dispose();
			}

			// Token: 0x06001743 RID: 5955 RVA: 0x000281CC File Offset: 0x000263CC
			internal unsafe MeshDataArray(Mesh mesh, bool checkReadWrite = true, bool createAsCopy = false)
			{
				bool flag = mesh == null;
				if (flag)
				{
					throw new ArgumentNullException("mesh", "Mesh is null");
				}
				bool flag2 = checkReadWrite && !mesh.canAccess;
				if (flag2)
				{
					throw new InvalidOperationException("Not allowed to access vertex data on mesh '" + mesh.name + "' (isReadable is false; Read/Write must be enabled in import settings)");
				}
				this.m_Length = 1;
				int num = UnsafeUtility.SizeOf<IntPtr>();
				this.m_Ptrs = (IntPtr*)UnsafeUtility.Malloc((long)num, UnsafeUtility.AlignOf<IntPtr>(), Allocator.Persistent);
				if (createAsCopy)
				{
					Mesh.MeshDataArray.AcquireMeshDataCopy(mesh, this.m_Ptrs);
				}
				else
				{
					Mesh.MeshDataArray.AcquireReadOnlyMeshData(mesh, this.m_Ptrs);
				}
				UnsafeUtility.LeakRecord((IntPtr)((void*)this.m_Ptrs), LeakCategory.MeshDataArray, 0);
			}

			// Token: 0x06001744 RID: 5956 RVA: 0x00028278 File Offset: 0x00026478
			internal unsafe MeshDataArray(Mesh[] meshes, int meshesCount, bool checkReadWrite = true, bool createAsCopy = false)
			{
				bool flag = meshes.Length < meshesCount;
				if (flag)
				{
					throw new InvalidOperationException(string.Format("Meshes array size ({0}) is smaller than meshes count ({1})", meshes.Length, meshesCount));
				}
				for (int i = 0; i < meshesCount; i++)
				{
					Mesh mesh = meshes[i];
					bool flag2 = mesh == null;
					if (flag2)
					{
						throw new ArgumentNullException("meshes", string.Format("Mesh at index {0} is null", i));
					}
					bool flag3 = checkReadWrite && !mesh.canAccess;
					if (flag3)
					{
						throw new InvalidOperationException(string.Format("Not allowed to access vertex data on mesh '{0}' at array index {1} (isReadable is false; Read/Write must be enabled in import settings)", mesh.name, i));
					}
				}
				this.m_Length = meshesCount;
				int num = UnsafeUtility.SizeOf<IntPtr>() * meshesCount;
				this.m_Ptrs = (IntPtr*)UnsafeUtility.Malloc((long)num, UnsafeUtility.AlignOf<IntPtr>(), Allocator.Persistent);
				if (createAsCopy)
				{
					Mesh.MeshDataArray.AcquireMeshDatasCopy(meshes, this.m_Ptrs, meshesCount);
				}
				else
				{
					Mesh.MeshDataArray.AcquireReadOnlyMeshDatas(meshes, this.m_Ptrs, meshesCount);
				}
			}

			// Token: 0x06001745 RID: 5957 RVA: 0x00028368 File Offset: 0x00026568
			internal unsafe MeshDataArray(int meshesCount)
			{
				bool flag = meshesCount < 0;
				if (flag)
				{
					throw new InvalidOperationException(string.Format("Mesh count can not be negative (was {0})", meshesCount));
				}
				this.m_Length = meshesCount;
				int num = UnsafeUtility.SizeOf<IntPtr>() * meshesCount;
				this.m_Ptrs = (IntPtr*)UnsafeUtility.Malloc((long)num, UnsafeUtility.AlignOf<IntPtr>(), Allocator.Persistent);
				Mesh.MeshDataArray.CreateNewMeshDatas(this.m_Ptrs, meshesCount);
			}

			// Token: 0x06001746 RID: 5958 RVA: 0x00002925 File Offset: 0x00000B25
			[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
			private void CheckElementReadAccess(int index)
			{
			}

			// Token: 0x06001747 RID: 5959
			[MethodImpl(MethodImplOptions.InternalCall)]
			private unsafe static extern void AcquireReadOnlyMeshData_Injected(IntPtr mesh, IntPtr* datas);

			// Token: 0x06001748 RID: 5960
			[MethodImpl(MethodImplOptions.InternalCall)]
			private unsafe static extern void AcquireReadOnlyMeshDatas_Injected(Mesh[] meshes, IntPtr* datas, int count);

			// Token: 0x06001749 RID: 5961
			[MethodImpl(MethodImplOptions.InternalCall)]
			private unsafe static extern void AcquireMeshDataCopy_Injected(IntPtr mesh, IntPtr* datas);

			// Token: 0x0600174A RID: 5962
			[MethodImpl(MethodImplOptions.InternalCall)]
			private unsafe static extern void AcquireMeshDatasCopy_Injected(Mesh[] meshes, IntPtr* datas, int count);

			// Token: 0x0600174B RID: 5963
			[MethodImpl(MethodImplOptions.InternalCall)]
			private unsafe static extern void ApplyToMeshesImpl_Injected(Mesh[] meshes, IntPtr* datas, int count, MeshUpdateFlags flags);

			// Token: 0x0600174C RID: 5964
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void ApplyToMeshImpl_Injected(IntPtr mesh, IntPtr data, MeshUpdateFlags flags);

			// Token: 0x040006C5 RID: 1733
			[NativeDisableUnsafePtrRestriction]
			internal unsafe IntPtr* m_Ptrs;

			// Token: 0x040006C6 RID: 1734
			internal int m_Length;
		}
	}
}
