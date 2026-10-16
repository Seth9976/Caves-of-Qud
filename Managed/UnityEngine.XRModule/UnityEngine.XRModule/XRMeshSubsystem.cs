using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000034 RID: 52
	[UsedByNativeCode]
	[NativeConditional("ENABLE_XR")]
	[NativeHeader("Modules/XR/Subsystems/Meshing/XRMeshingSubsystem.h")]
	[NativeHeader("Modules/XR/XRPrefix.h")]
	public class XRMeshSubsystem : IntegratedSubsystem<XRMeshSubsystemDescriptor>
	{
		// Token: 0x060001C0 RID: 448 RVA: 0x00005FCC File Offset: 0x000041CC
		public bool TryGetMeshInfos(List<MeshInfo> meshInfosOut)
		{
			bool flag = meshInfosOut == null;
			if (flag)
			{
				throw new ArgumentNullException("meshInfosOut");
			}
			return this.GetMeshInfosAsList(meshInfosOut);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00005FF8 File Offset: 0x000041F8
		private unsafe bool GetMeshInfosAsList(List<MeshInfo> meshInfos)
		{
			bool meshInfosAsList_Injected;
			try
			{
				IntPtr intPtr = XRMeshSubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableListWrapper blittableListWrapper;
				if (meshInfos != null)
				{
					fixed (MeshInfo[] array = NoAllocHelpers.ExtractArrayFromList<MeshInfo>(meshInfos))
					{
						BlittableArrayWrapper blittableArrayWrapper;
						if (array.Length != 0)
						{
							blittableArrayWrapper = new BlittableArrayWrapper((void*)(&array[0]), array.Length);
						}
						blittableListWrapper = new BlittableListWrapper(blittableArrayWrapper, meshInfos.Count);
					}
				}
				meshInfosAsList_Injected = XRMeshSubsystem.GetMeshInfosAsList_Injected(intPtr, ref blittableListWrapper);
			}
			finally
			{
				BlittableListWrapper blittableListWrapper;
				blittableListWrapper.Unmarshal<MeshInfo>(meshInfos);
			}
			return meshInfosAsList_Injected;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00006070 File Offset: 0x00004270
		private MeshInfo[] GetMeshInfosAsFixedArray()
		{
			MeshInfo[] array2;
			try
			{
				IntPtr intPtr = XRMeshSubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				BlittableArrayWrapper blittableArrayWrapper;
				XRMeshSubsystem.GetMeshInfosAsFixedArray_Injected(intPtr, out blittableArrayWrapper);
			}
			finally
			{
				BlittableArrayWrapper blittableArrayWrapper;
				MeshInfo[] array;
				blittableArrayWrapper.Unmarshal<MeshInfo>(ref array);
				array2 = array;
			}
			return array2;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000060B4 File Offset: 0x000042B4
		public void GenerateMeshAsync(MeshId meshId, Mesh mesh, MeshCollider meshCollider, MeshVertexAttributes attributes, Action<MeshGenerationResult> onMeshGenerationComplete)
		{
			this.GenerateMeshAsync(meshId, mesh, meshCollider, attributes, onMeshGenerationComplete, MeshGenerationOptions.None);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000060C8 File Offset: 0x000042C8
		public void GenerateMeshAsync(MeshId meshId, Mesh mesh, MeshCollider meshCollider, MeshVertexAttributes attributes, Action<MeshGenerationResult> onMeshGenerationComplete, MeshGenerationOptions options)
		{
			IntPtr intPtr = XRMeshSubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			XRMeshSubsystem.GenerateMeshAsync_Injected(intPtr, ref meshId, Object.MarshalledUnityObject.Marshal<Mesh>(mesh), Object.MarshalledUnityObject.Marshal<MeshCollider>(meshCollider), attributes, onMeshGenerationComplete, options);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00006100 File Offset: 0x00004300
		[RequiredByNativeCode]
		private void InvokeMeshReadyDelegate(MeshGenerationResult result, Action<MeshGenerationResult> onMeshGenerationComplete)
		{
			bool flag = onMeshGenerationComplete != null;
			if (flag)
			{
				onMeshGenerationComplete(result);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00006120 File Offset: 0x00004320
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x00006144 File Offset: 0x00004344
		public float meshDensity
		{
			get
			{
				IntPtr intPtr = XRMeshSubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return XRMeshSubsystem.get_meshDensity_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = XRMeshSubsystem.BindingsMarshaller.ConvertToNative(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				XRMeshSubsystem.set_meshDensity_Injected(intPtr, value);
			}
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00006168 File Offset: 0x00004368
		public bool SetBoundingVolume(Vector3 origin, Vector3 extents)
		{
			IntPtr intPtr = XRMeshSubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRMeshSubsystem.SetBoundingVolume_Injected(intPtr, ref origin, ref extents);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00006190 File Offset: 0x00004390
		public unsafe NativeArray<MeshTransform> GetUpdatedMeshTransforms(Allocator allocator)
		{
			NativeArray<MeshTransform> nativeArray2;
			using (XRMeshSubsystem.MeshTransformList meshTransformList = new XRMeshSubsystem.MeshTransformList(this.GetUpdatedMeshTransforms()))
			{
				NativeArray<MeshTransform> nativeArray = new NativeArray<MeshTransform>(meshTransformList.Count, allocator, NativeArrayOptions.UninitializedMemory);
				UnsafeUtility.MemCpy(nativeArray.GetUnsafePtr<MeshTransform>(), meshTransformList.Data.ToPointer(), (long)(meshTransformList.Count * sizeof(MeshTransform)));
				nativeArray2 = nativeArray;
			}
			return nativeArray2;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000620C File Offset: 0x0000440C
		private IntPtr GetUpdatedMeshTransforms()
		{
			IntPtr intPtr = XRMeshSubsystem.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return XRMeshSubsystem.GetUpdatedMeshTransforms_Injected(intPtr);
		}

		// Token: 0x060001CC RID: 460
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetMeshInfosAsList_Injected(IntPtr _unity_self, ref BlittableListWrapper meshInfos);

		// Token: 0x060001CD RID: 461
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMeshInfosAsFixedArray_Injected(IntPtr _unity_self, out BlittableArrayWrapper ret);

		// Token: 0x060001CE RID: 462
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GenerateMeshAsync_Injected(IntPtr _unity_self, [In] ref MeshId meshId, IntPtr mesh, IntPtr meshCollider, MeshVertexAttributes attributes, Action<MeshGenerationResult> onMeshGenerationComplete, MeshGenerationOptions options);

		// Token: 0x060001CF RID: 463
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_meshDensity_Injected(IntPtr _unity_self);

		// Token: 0x060001D0 RID: 464
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_meshDensity_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001D1 RID: 465
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetBoundingVolume_Injected(IntPtr _unity_self, [In] ref Vector3 origin, [In] ref Vector3 extents);

		// Token: 0x060001D2 RID: 466
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetUpdatedMeshTransforms_Injected(IntPtr _unity_self);

		// Token: 0x02000035 RID: 53
		[NativeConditional("ENABLE_XR")]
		private readonly struct MeshTransformList : IDisposable
		{
			// Token: 0x060001D3 RID: 467 RVA: 0x00006237 File Offset: 0x00004437
			public MeshTransformList(IntPtr self)
			{
				this.m_Self = self;
			}

			// Token: 0x1700004C RID: 76
			// (get) Token: 0x060001D4 RID: 468 RVA: 0x00006240 File Offset: 0x00004440
			public int Count
			{
				get
				{
					return XRMeshSubsystem.MeshTransformList.GetLength(this.m_Self);
				}
			}

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000624D File Offset: 0x0000444D
			public IntPtr Data
			{
				get
				{
					return XRMeshSubsystem.MeshTransformList.GetData(this.m_Self);
				}
			}

			// Token: 0x060001D6 RID: 470 RVA: 0x0000625A File Offset: 0x0000445A
			public void Dispose()
			{
				XRMeshSubsystem.MeshTransformList.Dispose(this.m_Self);
			}

			// Token: 0x060001D7 RID: 471
			[FreeFunction("UnityXRMeshTransformList_get_Length")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern int GetLength(IntPtr self);

			// Token: 0x060001D8 RID: 472
			[FreeFunction("UnityXRMeshTransformList_get_Data")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern IntPtr GetData(IntPtr self);

			// Token: 0x060001D9 RID: 473
			[FreeFunction("UnityXRMeshTransformList_Dispose")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern void Dispose(IntPtr self);

			// Token: 0x0400011C RID: 284
			private readonly IntPtr m_Self;
		}

		// Token: 0x02000036 RID: 54
		internal new static class BindingsMarshaller
		{
			// Token: 0x060001DA RID: 474 RVA: 0x00005467 File Offset: 0x00003667
			public static IntPtr ConvertToNative(XRMeshSubsystem subsystem)
			{
				return subsystem.m_Ptr;
			}
		}
	}
}
