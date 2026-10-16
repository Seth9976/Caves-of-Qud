using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Rendering
{
	// Token: 0x020004E5 RID: 1253
	[MovedFrom("UnityEngine.Experimental.Rendering")]
	public sealed class RayTracingAccelerationStructure : IDisposable
	{
		// Token: 0x06002EA0 RID: 11936 RVA: 0x00058268 File Offset: 0x00056468
		~RayTracingAccelerationStructure()
		{
			this.Dispose(false);
		}

		// Token: 0x06002EA1 RID: 11937 RVA: 0x0005829C File Offset: 0x0005649C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002EA2 RID: 11938 RVA: 0x000582B0 File Offset: 0x000564B0
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				RayTracingAccelerationStructure.Destroy(this);
			}
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x06002EA3 RID: 11939 RVA: 0x000582D7 File Offset: 0x000564D7
		public RayTracingAccelerationStructure(RayTracingAccelerationStructure.Settings settings)
		{
			this.m_Ptr = RayTracingAccelerationStructure.Create(settings);
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x000582F0 File Offset: 0x000564F0
		public RayTracingAccelerationStructure()
		{
			RayTracingAccelerationStructure.Settings settings = new RayTracingAccelerationStructure.Settings
			{
				rayTracingModeMask = RayTracingAccelerationStructure.RayTracingModeMask.Everything,
				managementMode = RayTracingAccelerationStructure.ManagementMode.Manual,
				layerMask = -1,
				buildFlagsStaticGeometries = RayTracingAccelerationStructureBuildFlags.PreferFastTrace,
				buildFlagsDynamicGeometries = RayTracingAccelerationStructureBuildFlags.PreferFastTrace
			};
			this.m_Ptr = RayTracingAccelerationStructure.Create(settings);
		}

		// Token: 0x06002EA5 RID: 11941 RVA: 0x00058345 File Offset: 0x00056545
		public void Release()
		{
			this.Dispose();
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x0005834F File Offset: 0x0005654F
		public void Build()
		{
			this.Build(new RayTracingAccelerationStructure.BuildSettings());
		}

		// Token: 0x06002EA7 RID: 11943 RVA: 0x00058360 File Offset: 0x00056560
		public void Build(Vector3 relativeOrigin)
		{
			RayTracingAccelerationStructure.BuildSettings buildSettings = new RayTracingAccelerationStructure.BuildSettings
			{
				relativeOrigin = relativeOrigin
			};
			this.Build(buildSettings);
		}

		// Token: 0x06002EA8 RID: 11944 RVA: 0x00058388 File Offset: 0x00056588
		public int AddInstance(Renderer targetRenderer, RayTracingSubMeshFlags[] subMeshFlags, bool enableTriangleCulling = true, bool frontTriangleCounterClockwise = false, uint mask = 255U, uint id = 4294967295U)
		{
			return this.AddInstanceSubMeshFlagsArray(targetRenderer, subMeshFlags, enableTriangleCulling, frontTriangleCounterClockwise, mask, id);
		}

		// Token: 0x06002EA9 RID: 11945 RVA: 0x000583AC File Offset: 0x000565AC
		public int AddInstance(RayTracingAABBsInstanceConfig config, Matrix4x4 matrix, uint id = 4294967295U)
		{
			bool flag = config.aabbBuffer == null;
			if (flag)
			{
				throw new ArgumentNullException("config.aabbBuffer.");
			}
			bool flag2 = config.aabbBuffer.target != GraphicsBuffer.Target.Structured;
			if (flag2)
			{
				throw new ArgumentException("config.aabbBuffer.target must be GraphicsBuffer.Target.Structured.");
			}
			bool flag3 = config.aabbBuffer.stride != 24;
			if (flag3)
			{
				throw new ArgumentException("config.aabbBuffer.stride must be 6 floats.");
			}
			bool flag4 = config.aabbCount == 0;
			if (flag4)
			{
				throw new ArgumentException("config.aabbCount cannot be 0.");
			}
			return this.AddAABBsInstance(config, matrix, id);
		}

		// Token: 0x06002EAA RID: 11946 RVA: 0x00058440 File Offset: 0x00056640
		public unsafe int AddInstance(in RayTracingMeshInstanceConfig config, Matrix4x4 matrix, [DefaultValue("null")] Matrix4x4? prevMatrix = null, uint id = 4294967295U)
		{
			bool flag = config.mesh == null;
			if (flag)
			{
				throw new ArgumentNullException("config.mesh");
			}
			bool flag2 = (ulong)config.subMeshIndex >= (ulong)((long)config.mesh.subMeshCount);
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("config.subMeshIndex", "config.subMeshIndex is out of range.");
			}
			bool flag3 = config.lightProbeUsage == LightProbeUsage.UseProxyVolume && config.lightProbeProxyVolume == null;
			if (flag3)
			{
				throw new ArgumentException("config.lightProbeProxyVolume must not be null if config.lightProbeUsage is set to UseProxyVolume.");
			}
			bool flag4 = prevMatrix != null;
			int num;
			if (flag4)
			{
				Matrix4x4 value = prevMatrix.Value;
				num = this.AddMeshInstance(config, matrix, &value, id);
			}
			else
			{
				num = this.AddMeshInstance(config, matrix, null, id);
			}
			return num;
		}

		// Token: 0x06002EAB RID: 11947 RVA: 0x00058500 File Offset: 0x00056700
		public unsafe int AddInstance(in RayTracingGeometryInstanceConfig config, Matrix4x4 matrix, [DefaultValue("null")] Matrix4x4? prevMatrix = null, uint id = 4294967295U)
		{
			bool flag = config.vertexBuffer == null;
			if (flag)
			{
				throw new ArgumentException("config.vertexBuffer must not be null.");
			}
			bool flag2 = config.vertexCount == -1 && (ulong)config.vertexStart >= (ulong)((long)config.vertexBuffer.count);
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("config.vertexStart", string.Format("config.vertexStart ({0}) is out of range. Not enough vertices in the vertex buffer ({1}).", config.vertexStart, config.vertexBuffer.count));
			}
			bool flag3 = config.vertexCount != -1 && (ulong)config.vertexStart + (ulong)((long)config.vertexCount) > (ulong)((long)config.vertexBuffer.count);
			if (flag3)
			{
				throw new ArgumentOutOfRangeException("config.vertexStart", string.Format("config.vertexStart ({0}) + config.vertexCount ({1}) is out of range. Not enough vertices in the vertex buffer ({2}).", config.vertexStart, config.vertexCount, config.vertexBuffer.count));
			}
			int num = ((config.vertexCount < 0) ? config.vertexBuffer.count : config.vertexCount);
			bool flag4 = num == 0;
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("config.vertexCount", "The amount of vertices used must be greater than 0.");
			}
			bool flag5 = config.indexBuffer != null;
			if (flag5)
			{
				bool flag6 = config.indexBuffer.count < 3;
				if (flag6)
				{
					throw new ArgumentOutOfRangeException("config.indexBuffer", "config.indexBuffer must contain at least 3 indices.");
				}
				bool flag7 = config.indexCount == -1 && (ulong)config.indexStart >= (ulong)((long)config.indexBuffer.count);
				if (flag7)
				{
					throw new ArgumentOutOfRangeException("config.indexStart", string.Format("config.indexStart ({0}) is out of range. Not enough indices in the index buffer ({1}).", config.indexStart, config.indexBuffer.count));
				}
				bool flag8 = config.indexCount != -1 && (ulong)config.indexStart + (ulong)((long)config.indexCount) > (ulong)((long)config.indexBuffer.count);
				if (flag8)
				{
					throw new ArgumentOutOfRangeException("config.indexStart", string.Format("config.indexStart ({0}) + config.indexCount ({1}) is out of range. Not enough indices in the index buffer ({2}).", config.indexStart, config.indexCount, config.indexBuffer.count));
				}
				int num2 = ((config.indexCount < 0) ? config.indexBuffer.count : config.indexCount);
				bool flag9 = num2 % 3 != 0;
				if (flag9)
				{
					throw new ArgumentOutOfRangeException("config.indexBuffer", string.Format("The amount of indices used must be a multiple of 3. Only triangle geometries are supported. Currently using {0} indices.", num2));
				}
			}
			else
			{
				bool flag10 = num % 3 != 0;
				if (flag10)
				{
					throw new ArgumentOutOfRangeException("config.vertexBuffer", string.Format("When config.indexBuffer is null, the amount of vertices used must be a multiple of 3. Only triangle geometries are supported. Currently using {0} vertices.", num));
				}
			}
			bool flag11 = config.lightProbeUsage == LightProbeUsage.UseProxyVolume && config.lightProbeProxyVolume == null;
			if (flag11)
			{
				throw new ArgumentException("config.lightProbeProxyVolume must not be null if config.lightProbeUsage is set to UseProxyVolume.");
			}
			bool flag12 = config.vertexAttributes == null;
			if (flag12)
			{
				throw new ArgumentNullException("config.vertexAttributes");
			}
			bool flag13 = config.vertexAttributes.Length == 0;
			if (flag13)
			{
				throw new ArgumentException("config.vertexAttributes must contain at least one entry.");
			}
			bool flag14 = prevMatrix != null;
			int num3;
			if (flag14)
			{
				Matrix4x4 value = prevMatrix.Value;
				num3 = this.AddGeometryInstance(in config, matrix, &value, id);
			}
			else
			{
				num3 = this.AddGeometryInstance(in config, matrix, null, id);
			}
			return num3;
		}

		// Token: 0x06002EAC RID: 11948 RVA: 0x00058824 File Offset: 0x00056A24
		public unsafe int AddInstances<[IsUnmanaged] T>(in RayTracingMeshInstanceConfig config, T[] instanceData, [DefaultValue("-1")] int instanceCount = -1, [DefaultValue("0")] int startInstance = 0, uint id = 4294967295U) where T : struct, ValueType
		{
			bool flag = instanceData == null;
			if (flag)
			{
				throw new ArgumentNullException("instanceData");
			}
			bool flag2 = config.material == null;
			if (flag2)
			{
				throw new ArgumentNullException("config.material");
			}
			bool flag3 = !RayTracingAccelerationStructure.CheckMaterialEnablesInstancing(config.material);
			if (flag3)
			{
				throw new InvalidOperationException("config.material (" + config.material.name + ") needs to enable GPU Instancing for use with AddInstances.");
			}
			bool flag4 = config.mesh == null;
			if (flag4)
			{
				throw new ArgumentNullException("config.mesh");
			}
			bool flag5 = (ulong)config.subMeshIndex >= (ulong)((long)config.mesh.subMeshCount);
			if (flag5)
			{
				throw new ArgumentOutOfRangeException("config.subMeshIndex", "config.subMeshIndex is out of range.");
			}
			bool flag6 = config.lightProbeUsage == LightProbeUsage.UseProxyVolume && config.lightProbeProxyVolume == null;
			if (flag6)
			{
				throw new ArgumentException("config.lightProbeProxyVolume argument must not be null if config.lightProbeUsage is set to UseProxyVolume.");
			}
			RenderInstancedDataLayout cachedRenderInstancedDataLayout = Graphics.GetCachedRenderInstancedDataLayout(typeof(T));
			instanceCount = ((instanceCount == -1) ? instanceData.Length : instanceCount);
			startInstance = Math.Clamp(startInstance, 0, Math.Max(0, instanceData.Length - 1));
			instanceCount = Math.Clamp(instanceCount, 0, Math.Max(0, instanceData.Length - startInstance));
			bool flag7 = instanceCount > Graphics.kMaxDrawMeshInstanceCount;
			if (flag7)
			{
				throw new InvalidOperationException(string.Format("Instance count cannot exceed {0}.", Graphics.kMaxDrawMeshInstanceCount));
			}
			T* ptr;
			if (instanceData == null || instanceData.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &instanceData[0];
			}
			return this.AddMeshInstances(config, (IntPtr)((void*)(ptr + (IntPtr)startInstance * (IntPtr)sizeof(T) / (IntPtr)sizeof(T))), cachedRenderInstancedDataLayout, (uint)instanceCount, id);
		}

		// Token: 0x06002EAD RID: 11949 RVA: 0x000589BC File Offset: 0x00056BBC
		public unsafe int AddInstances<[IsUnmanaged] T>(in RayTracingMeshInstanceConfig config, List<T> instanceData, [DefaultValue("-1")] int instanceCount = -1, [DefaultValue("0")] int startInstance = 0, uint id = 4294967295U) where T : struct, ValueType
		{
			bool flag = instanceData == null;
			if (flag)
			{
				throw new ArgumentNullException("instanceData");
			}
			bool flag2 = config.material == null;
			if (flag2)
			{
				throw new ArgumentNullException("config.material");
			}
			bool flag3 = !RayTracingAccelerationStructure.CheckMaterialEnablesInstancing(config.material);
			if (flag3)
			{
				throw new InvalidOperationException("config.material (" + config.material.name + ") needs to enable GPU Instancing for use with AddInstances.");
			}
			bool flag4 = config.mesh == null;
			if (flag4)
			{
				throw new ArgumentNullException("config.mesh");
			}
			bool flag5 = (ulong)config.subMeshIndex >= (ulong)((long)config.mesh.subMeshCount);
			if (flag5)
			{
				throw new ArgumentOutOfRangeException("config.subMeshIndex", "config.subMeshIndex is out of range.");
			}
			bool flag6 = config.lightProbeUsage == LightProbeUsage.UseProxyVolume && config.lightProbeProxyVolume == null;
			if (flag6)
			{
				throw new ArgumentException("config.lightProbeProxyVolume argument must not be null if config.lightProbeUsage is set to UseProxyVolume.");
			}
			RenderInstancedDataLayout cachedRenderInstancedDataLayout = Graphics.GetCachedRenderInstancedDataLayout(typeof(T));
			instanceCount = ((instanceCount == -1) ? instanceData.Count : instanceCount);
			startInstance = Math.Clamp(startInstance, 0, Math.Max(0, instanceData.Count - 1));
			instanceCount = Math.Clamp(instanceCount, 0, Math.Max(0, instanceData.Count - startInstance));
			bool flag7 = instanceCount > Graphics.kMaxDrawMeshInstanceCount;
			if (flag7)
			{
				throw new InvalidOperationException(string.Format("Instance count cannot exceed {0}.", Graphics.kMaxDrawMeshInstanceCount));
			}
			T[] array;
			T* ptr;
			if ((array = NoAllocHelpers.ExtractArrayFromList<T>(instanceData)) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			return this.AddMeshInstances(config, (IntPtr)((void*)(ptr + (IntPtr)startInstance * (IntPtr)sizeof(T) / (IntPtr)sizeof(T))), cachedRenderInstancedDataLayout, (uint)instanceCount, id);
		}

		// Token: 0x06002EAE RID: 11950 RVA: 0x00058B60 File Offset: 0x00056D60
		public unsafe int AddInstances<[IsUnmanaged] T>(in RayTracingMeshInstanceConfig config, NativeArray<T> instanceData, [DefaultValue("-1")] int instanceCount = -1, [DefaultValue("0")] int startInstance = 0, uint id = 4294967295U) where T : struct, ValueType
		{
			bool flag = config.material == null;
			if (flag)
			{
				throw new ArgumentNullException("config.material");
			}
			bool flag2 = !RayTracingAccelerationStructure.CheckMaterialEnablesInstancing(config.material);
			if (flag2)
			{
				throw new InvalidOperationException("config.material (" + config.material.name + ") needs to enable GPU Instancing for use with AddInstances.");
			}
			bool flag3 = config.mesh == null;
			if (flag3)
			{
				throw new ArgumentNullException("config.mesh");
			}
			bool flag4 = (ulong)config.subMeshIndex >= (ulong)((long)config.mesh.subMeshCount);
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("config.subMeshIndex", "config.subMeshIndex is out of range.");
			}
			bool flag5 = config.lightProbeUsage == LightProbeUsage.UseProxyVolume && config.lightProbeProxyVolume == null;
			if (flag5)
			{
				throw new ArgumentException("config.lightProbeProxyVolume argument must not be null if config.lightProbeUsage is set to UseProxyVolume.");
			}
			RenderInstancedDataLayout cachedRenderInstancedDataLayout = Graphics.GetCachedRenderInstancedDataLayout(typeof(T));
			instanceCount = ((instanceCount == -1) ? instanceData.Length : instanceCount);
			startInstance = Math.Clamp(startInstance, 0, Math.Max(0, instanceData.Length - 1));
			instanceCount = Math.Clamp(instanceCount, 0, Math.Max(0, instanceData.Length - startInstance));
			bool flag6 = instanceCount > Graphics.kMaxDrawMeshInstanceCount;
			if (flag6)
			{
				throw new InvalidOperationException(string.Format("Instance count cannot exceed {0}.", Graphics.kMaxDrawMeshInstanceCount));
			}
			return this.AddMeshInstances(config, (IntPtr)((void*)((byte*)instanceData.GetUnsafeReadOnlyPtr<T>() + (IntPtr)startInstance * (IntPtr)sizeof(T))), cachedRenderInstancedDataLayout, (uint)instanceCount, id);
		}

		// Token: 0x06002EAF RID: 11951 RVA: 0x00058CD4 File Offset: 0x00056ED4
		public int AddInstances<[IsUnmanaged] T>(in RayTracingMeshInstanceConfig config, NativeSlice<T> instanceData, uint id = 4294967295U) where T : struct, ValueType
		{
			bool flag = config.material == null;
			if (flag)
			{
				throw new ArgumentNullException("config.material");
			}
			bool flag2 = !RayTracingAccelerationStructure.CheckMaterialEnablesInstancing(config.material);
			if (flag2)
			{
				throw new InvalidOperationException("config.material (" + config.material.name + ") needs to enable GPU Instancing for use with AddInstances.");
			}
			bool flag3 = config.mesh == null;
			if (flag3)
			{
				throw new ArgumentNullException("config.mesh");
			}
			bool flag4 = (ulong)config.subMeshIndex >= (ulong)((long)config.mesh.subMeshCount);
			if (flag4)
			{
				throw new ArgumentOutOfRangeException("config.subMeshIndex", "config.subMeshIndex is out of range.");
			}
			bool flag5 = config.lightProbeUsage == LightProbeUsage.UseProxyVolume && config.lightProbeProxyVolume == null;
			if (flag5)
			{
				throw new ArgumentException("config.lightProbeProxyVolume argument must not be null if config.lightProbeUsage is set to UseProxyVolume.");
			}
			RenderInstancedDataLayout cachedRenderInstancedDataLayout = Graphics.GetCachedRenderInstancedDataLayout(typeof(T));
			bool flag6 = instanceData.Length > Graphics.kMaxDrawMeshInstanceCount;
			if (flag6)
			{
				throw new InvalidOperationException(string.Format("Instance count cannot exceed {0}.", Graphics.kMaxDrawMeshInstanceCount));
			}
			return this.AddMeshInstances(config, (IntPtr)instanceData.GetUnsafeReadOnlyPtr<T>(), cachedRenderInstancedDataLayout, (uint)instanceData.Length, id);
		}

		// Token: 0x06002EB0 RID: 11952 RVA: 0x00058E05 File Offset: 0x00057005
		public void RemoveInstance(Renderer targetRenderer)
		{
			this.RemoveInstance_Renderer(targetRenderer);
		}

		// Token: 0x06002EB1 RID: 11953 RVA: 0x00058E10 File Offset: 0x00057010
		public void RemoveInstance(int handle)
		{
			this.RemoveInstance_InstanceID(handle);
		}

		// Token: 0x06002EB2 RID: 11954 RVA: 0x00058E1B File Offset: 0x0005701B
		public void UpdateInstanceTransform(Renderer renderer)
		{
			this.UpdateInstanceTransform_Renderer(renderer);
		}

		// Token: 0x06002EB3 RID: 11955 RVA: 0x00058E26 File Offset: 0x00057026
		public void UpdateInstanceTransform(int handle, Matrix4x4 matrix)
		{
			this.UpdateInstanceTransform_Handle(handle, matrix);
		}

		// Token: 0x06002EB4 RID: 11956 RVA: 0x00058E32 File Offset: 0x00057032
		public void UpdateInstanceID(Renderer renderer, uint instanceID)
		{
			this.UpdateInstanceID_Renderer(renderer, instanceID);
		}

		// Token: 0x06002EB5 RID: 11957 RVA: 0x00058E3E File Offset: 0x0005703E
		public void UpdateInstanceID(int handle, uint instanceID)
		{
			this.UpdateInstanceID_Handle(handle, instanceID);
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x00058E4A File Offset: 0x0005704A
		public void UpdateInstanceMask(Renderer renderer, uint mask)
		{
			this.UpdateInstanceMask_Renderer(renderer, mask);
		}

		// Token: 0x06002EB7 RID: 11959 RVA: 0x00058E56 File Offset: 0x00057056
		public void UpdateInstanceMask(int handle, uint mask)
		{
			this.UpdateInstanceMask_Handle(handle, mask);
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x00058E64 File Offset: 0x00057064
		[FreeFunction(Name = "RayTracingAccelerationStructure_Bindings::Build", HasExplicitThis = true)]
		public void Build(RayTracingAccelerationStructure.BuildSettings buildSettings)
		{
			IntPtr intPtr = RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RayTracingAccelerationStructure.Build_Injected(intPtr, ref buildSettings);
		}

		// Token: 0x06002EB9 RID: 11961 RVA: 0x00058E88 File Offset: 0x00057088
		[FreeFunction(Name = "RayTracingAccelerationStructure_Bindings::AddVFXInstances", HasExplicitThis = true)]
		public unsafe void AddVFXInstances([NotNull] Renderer targetRenderer, uint[] vfxSystemMasks)
		{
			if (targetRenderer == null)
			{
				ThrowHelper.ThrowArgumentNullException(targetRenderer, "targetRenderer");
			}
			IntPtr intPtr = RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(targetRenderer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(targetRenderer, "targetRenderer");
			}
			Span<uint> span = new Span<uint>(vfxSystemMasks);
			fixed (uint* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				RayTracingAccelerationStructure.AddVFXInstances_Injected(intPtr, intPtr2, ref managedSpanWrapper);
			}
		}

		// Token: 0x06002EBA RID: 11962 RVA: 0x00058EF4 File Offset: 0x000570F4
		[FreeFunction(Name = "RayTracingAccelerationStructure_Bindings::RemoveVFXInstances", HasExplicitThis = true)]
		public void RemoveVFXInstances([NotNull] Renderer targetRenderer)
		{
			if (targetRenderer == null)
			{
				ThrowHelper.ThrowArgumentNullException(targetRenderer, "targetRenderer");
			}
			IntPtr intPtr = RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(targetRenderer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(targetRenderer, "targetRenderer");
			}
			RayTracingAccelerationStructure.RemoveVFXInstances_Injected(intPtr, intPtr2);
		}

		// Token: 0x06002EBB RID: 11963 RVA: 0x00058F3C File Offset: 0x0005713C
		[FreeFunction(Name = "RayTracingAccelerationStructure_Bindings::UpdateInstancePropertyBlock", HasExplicitThis = true)]
		public void UpdateInstancePropertyBlock(int handle, MaterialPropertyBlock properties)
		{
			IntPtr intPtr = RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RayTracingAccelerationStructure.UpdateInstancePropertyBlock_Injected(intPtr, handle, (properties == null) ? ((IntPtr)0) : MaterialPropertyBlock.BindingsMarshaller.ConvertToNative(properties));
		}

		// Token: 0x06002EBC RID: 11964 RVA: 0x00058F70 File Offset: 0x00057170
		[FreeFunction(Name = "RayTracingAccelerationStructure_Bindings::GetSize", HasExplicitThis = true)]
		public ulong GetSize()
		{
			IntPtr intPtr = RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return RayTracingAccelerationStructure.GetSize_Injected(intPtr);
		}

		// Token: 0x06002EBD RID: 11965 RVA: 0x00058F94 File Offset: 0x00057194
		[FreeFunction(Name = "RayTracingAccelerationStructure_Bindings::GetInstanceCount", HasExplicitThis = true)]
		public uint GetInstanceCount()
		{
			IntPtr intPtr = RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return RayTracingAccelerationStructure.GetInstanceCount_Injected(intPtr);
		}

		// Token: 0x06002EBE RID: 11966 RVA: 0x00058FB8 File Offset: 0x000571B8
		[FreeFunction(Name = "RayTracingAccelerationStructure_Bindings::ClearInstances", HasExplicitThis = true)]
		public void ClearInstances()
		{
			IntPtr intPtr = RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RayTracingAccelerationStructure.ClearInstances_Injected(intPtr);
		}

		// Token: 0x06002EBF RID: 11967 RVA: 0x00058FDA File Offset: 0x000571DA
		public RayTracingInstanceCullingResults CullInstances(ref RayTracingInstanceCullingConfig cullingConfig)
		{
			return this.Internal_CullInstances(in cullingConfig);
		}

		// Token: 0x06002EC0 RID: 11968 RVA: 0x00058FE4 File Offset: 0x000571E4
		[FreeFunction("RayTracingAccelerationStructure_Bindings::Create")]
		private static IntPtr Create(RayTracingAccelerationStructure.Settings desc)
		{
			return RayTracingAccelerationStructure.Create_Injected(ref desc);
		}

		// Token: 0x06002EC1 RID: 11969 RVA: 0x00058FF8 File Offset: 0x000571F8
		[FreeFunction("RayTracingAccelerationStructure_Bindings::Destroy")]
		private static void Destroy(RayTracingAccelerationStructure accelStruct)
		{
			RayTracingAccelerationStructure.Destroy_Injected((accelStruct == null) ? ((IntPtr)0) : RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(accelStruct));
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x0005901C File Offset: 0x0005721C
		[FreeFunction(Name = "RayTracingAccelerationStructure_Bindings::RemoveInstance", HasExplicitThis = true)]
		private void RemoveInstance_Renderer([NotNull] Renderer targetRenderer)
		{
			if (targetRenderer == null)
			{
				ThrowHelper.ThrowArgumentNullException(targetRenderer, "targetRenderer");
			}
			IntPtr intPtr = RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(targetRenderer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(targetRenderer, "targetRenderer");
			}
			RayTracingAccelerationStructure.RemoveInstance_Renderer_Injected(intPtr, intPtr2);
		}

		// Token: 0x06002EC3 RID: 11971 RVA: 0x00059064 File Offset: 0x00057264
		[FreeFunction(Name = "RayTracingAccelerationStructure_Bindings::RemoveInstance", HasExplicitThis = true)]
		private void RemoveInstance_InstanceID(int instanceID)
		{
			IntPtr intPtr = RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RayTracingAccelerationStructure.RemoveInstance_InstanceID_Injected(intPtr, instanceID);
		}

		// Token: 0x06002EC4 RID: 11972 RVA: 0x00059088 File Offset: 0x00057288
		[FreeFunction(Name = "RayTracingAccelerationStructure_Bindings::UpdateInstanceTransform", HasExplicitThis = true)]
		private void UpdateInstanceTransform_Renderer([NotNull] Renderer renderer)
		{
			if (renderer == null)
			{
				ThrowHelper.ThrowArgumentNullException(renderer, "renderer");
			}
			IntPtr intPtr = RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(renderer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(renderer, "renderer");
			}
			RayTracingAccelerationStructure.UpdateInstanceTransform_Renderer_Injected(intPtr, intPtr2);
		}

		// Token: 0x06002EC5 RID: 11973 RVA: 0x000590D0 File Offset: 0x000572D0
		[FreeFunction(Name = "RayTracingAccelerationStructure_Bindings::UpdateInstanceTransform", HasExplicitThis = true)]
		private void UpdateInstanceTransform_Handle(int handle, Matrix4x4 matrix)
		{
			IntPtr intPtr = RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RayTracingAccelerationStructure.UpdateInstanceTransform_Handle_Injected(intPtr, handle, ref matrix);
		}

		// Token: 0x06002EC6 RID: 11974 RVA: 0x000590F8 File Offset: 0x000572F8
		[FreeFunction(Name = "RayTracingAccelerationStructure_Bindings::UpdateInstanceMask", HasExplicitThis = true)]
		private void UpdateInstanceMask_Renderer([NotNull] Renderer renderer, uint mask)
		{
			if (renderer == null)
			{
				ThrowHelper.ThrowArgumentNullException(renderer, "renderer");
			}
			IntPtr intPtr = RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(renderer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(renderer, "renderer");
			}
			RayTracingAccelerationStructure.UpdateInstanceMask_Renderer_Injected(intPtr, intPtr2, mask);
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x00059140 File Offset: 0x00057340
		[FreeFunction(Name = "RayTracingAccelerationStructure_Bindings::UpdateInstanceMask", HasExplicitThis = true)]
		private void UpdateInstanceMask_Handle(int handle, uint mask)
		{
			IntPtr intPtr = RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RayTracingAccelerationStructure.UpdateInstanceMask_Handle_Injected(intPtr, handle, mask);
		}

		// Token: 0x06002EC8 RID: 11976 RVA: 0x00059164 File Offset: 0x00057364
		[FreeFunction(Name = "RayTracingAccelerationStructure_Bindings::UpdateInstanceID", HasExplicitThis = true)]
		private void UpdateInstanceID_Renderer([NotNull] Renderer renderer, uint id)
		{
			if (renderer == null)
			{
				ThrowHelper.ThrowArgumentNullException(renderer, "renderer");
			}
			IntPtr intPtr = RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(renderer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(renderer, "renderer");
			}
			RayTracingAccelerationStructure.UpdateInstanceID_Renderer_Injected(intPtr, intPtr2, id);
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x000591AC File Offset: 0x000573AC
		[FreeFunction(Name = "RayTracingAccelerationStructure_Bindings::UpdateInstanceID", HasExplicitThis = true)]
		private void UpdateInstanceID_Handle(int handle, uint id)
		{
			IntPtr intPtr = RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RayTracingAccelerationStructure.UpdateInstanceID_Handle_Injected(intPtr, handle, id);
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x000591D0 File Offset: 0x000573D0
		[FreeFunction(Name = "RayTracingAccelerationStructure_Bindings::AddInstanceSubMeshFlagsArray", HasExplicitThis = true)]
		private unsafe int AddInstanceSubMeshFlagsArray([NotNull] Renderer targetRenderer, RayTracingSubMeshFlags[] subMeshFlags, bool enableTriangleCulling = true, bool frontTriangleCounterClockwise = false, uint mask = 255U, uint id = 4294967295U)
		{
			if (targetRenderer == null)
			{
				ThrowHelper.ThrowArgumentNullException(targetRenderer, "targetRenderer");
			}
			IntPtr intPtr = RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = Object.MarshalledUnityObject.MarshalNotNull<Renderer>(targetRenderer);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(targetRenderer, "targetRenderer");
			}
			Span<RayTracingSubMeshFlags> span = new Span<RayTracingSubMeshFlags>(subMeshFlags);
			int num;
			fixed (RayTracingSubMeshFlags* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				num = RayTracingAccelerationStructure.AddInstanceSubMeshFlagsArray_Injected(intPtr, intPtr2, ref managedSpanWrapper, enableTriangleCulling, frontTriangleCounterClockwise, mask, id);
			}
			return num;
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x00059244 File Offset: 0x00057444
		[FreeFunction("RayTracingAccelerationStructure_Bindings::AddMeshInstance", HasExplicitThis = true)]
		private unsafe int AddMeshInstance(RayTracingMeshInstanceConfig config, Matrix4x4 matrix, Matrix4x4* prevMatrix, uint id = 4294967295U)
		{
			IntPtr intPtr = RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return RayTracingAccelerationStructure.AddMeshInstance_Injected(intPtr, ref config, ref matrix, prevMatrix, id);
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x00059270 File Offset: 0x00057470
		[FreeFunction("RayTracingAccelerationStructure_Bindings::AddGeometryInstance", HasExplicitThis = true)]
		private unsafe int AddGeometryInstance(in RayTracingGeometryInstanceConfig config, Matrix4x4 matrix, Matrix4x4* prevMatrix, uint id = 4294967295U)
		{
			IntPtr intPtr = RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return RayTracingAccelerationStructure.AddGeometryInstance_Injected(intPtr, in config, ref matrix, prevMatrix, id);
		}

		// Token: 0x06002ECD RID: 11981 RVA: 0x00059298 File Offset: 0x00057498
		[FreeFunction("RayTracingAccelerationStructure_Bindings::AddMeshInstances", HasExplicitThis = true)]
		private int AddMeshInstances(RayTracingMeshInstanceConfig config, IntPtr instancedData, RenderInstancedDataLayout layout, uint instanceCount, uint id = 4294967295U)
		{
			IntPtr intPtr = RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return RayTracingAccelerationStructure.AddMeshInstances_Injected(intPtr, ref config, instancedData, ref layout, instanceCount, id);
		}

		// Token: 0x06002ECE RID: 11982 RVA: 0x000592C4 File Offset: 0x000574C4
		[FreeFunction("RayTracingAccelerationStructure_Bindings::AddAABBsInstance", HasExplicitThis = true)]
		private int AddAABBsInstance(RayTracingAABBsInstanceConfig config, Matrix4x4 matrix, uint id = 4294967295U)
		{
			IntPtr intPtr = RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return RayTracingAccelerationStructure.AddAABBsInstance_Injected(intPtr, ref config, ref matrix, id);
		}

		// Token: 0x06002ECF RID: 11983 RVA: 0x000592EC File Offset: 0x000574EC
		[FreeFunction(Name = "RayTracingAccelerationStructure_Bindings::CullInstances", HasExplicitThis = true)]
		private RayTracingInstanceCullingResults Internal_CullInstances(in RayTracingInstanceCullingConfig cullingConfig)
		{
			IntPtr intPtr = RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			RayTracingInstanceCullingResults rayTracingInstanceCullingResults;
			RayTracingAccelerationStructure.Internal_CullInstances_Injected(intPtr, in cullingConfig, out rayTracingInstanceCullingResults);
			return rayTracingInstanceCullingResults;
		}

		// Token: 0x06002ED0 RID: 11984 RVA: 0x00059314 File Offset: 0x00057514
		[FreeFunction(Name = "RayTracingAccelerationStructure_Bindings::CheckMaterialEnablesInstancing")]
		private static bool CheckMaterialEnablesInstancing(Material material)
		{
			return RayTracingAccelerationStructure.CheckMaterialEnablesInstancing_Injected(Object.MarshalledUnityObject.Marshal<Material>(material));
		}

		// Token: 0x06002ED1 RID: 11985 RVA: 0x0005932C File Offset: 0x0005752C
		[Obsolete("Method Update is deprecated and it will be removed in Unity 2024.1. Use Build instead (UnityUpgradable) -> Build()", true)]
		public void Update()
		{
			new NotSupportedException("Method Update is deprecated and it will be removed in Unity 2024.1. Use Build instead (UnityUpgradable) -> Build()");
		}

		// Token: 0x06002ED2 RID: 11986 RVA: 0x00059339 File Offset: 0x00057539
		[Obsolete("Method Update is deprecated and it will be removed in Unity 2024.1. Use Build instead (UnityUpgradable) -> Build(*)", true)]
		public void Update(Vector3 relativeOrigin)
		{
			new NotSupportedException("Method Update is deprecated and it will be removed in Unity 2024.1. Use Build instead (UnityUpgradable) -> Build(*)");
		}

		// Token: 0x06002ED3 RID: 11987 RVA: 0x00059346 File Offset: 0x00057546
		[Obsolete("This AddInstance method is deprecated and will be removed and it will be removed in Unity 2024.1. Please use the alternative AddInstance method for adding Renderers to the acceleration structure.", true)]
		public void AddInstance(Renderer targetRenderer, bool[] subMeshMask = null, bool[] subMeshTransparencyFlags = null, bool enableTriangleCulling = true, bool frontTriangleCounterClockwise = false, uint mask = 255U, uint id = 4294967295U)
		{
			new NotSupportedException("This AddInstance method is deprecated and will be removed and it will be removed in Unity 2024.1. Please use the alternative AddInstance method for adding Renderers to the acceleration structure.");
		}

		// Token: 0x06002ED4 RID: 11988 RVA: 0x00059353 File Offset: 0x00057553
		[Obsolete("This AddInstance method is deprecated and it will be removed in Unity 2024.1. Please use the alternative AddInstance method for adding procedural geometry (AABBs) to the acceleration structure.", true)]
		public void AddInstance(GraphicsBuffer aabbBuffer, uint numElements, Material material, bool isCutOff, bool enableTriangleCulling = true, bool frontTriangleCounterClockwise = false, uint mask = 255U, bool reuseBounds = false, uint id = 4294967295U)
		{
			new NotSupportedException("This AddInstance method is deprecated and it will be removed in Unity 2024.1. Please use the alternative AddInstance method for adding procedural geometry (AABBs) to the acceleration structure.");
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x00059360 File Offset: 0x00057560
		[Obsolete("This AddInstance method is deprecated and it will be removed in Unity 2024.1. Please use the alternative AddInstance method for adding procedural geometry (AABBs) to the acceleration structure.", true)]
		public int AddInstance(GraphicsBuffer aabbBuffer, uint aabbCount, bool dynamicData, Matrix4x4 matrix, Material material, bool opaqueMaterial, MaterialPropertyBlock properties, uint mask = 255U, uint id = 4294967295U)
		{
			throw new NotSupportedException("This AddInstance method is deprecated and it will be removed in Unity 2024.1. Please use the alternative AddInstance method for adding procedural geometry (AABBs) to the acceleration structure.");
		}

		// Token: 0x06002ED6 RID: 11990
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Build_Injected(IntPtr _unity_self, [In] ref RayTracingAccelerationStructure.BuildSettings buildSettings);

		// Token: 0x06002ED7 RID: 11991
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddVFXInstances_Injected(IntPtr _unity_self, IntPtr targetRenderer, ref ManagedSpanWrapper vfxSystemMasks);

		// Token: 0x06002ED8 RID: 11992
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveVFXInstances_Injected(IntPtr _unity_self, IntPtr targetRenderer);

		// Token: 0x06002ED9 RID: 11993
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateInstancePropertyBlock_Injected(IntPtr _unity_self, int handle, IntPtr properties);

		// Token: 0x06002EDA RID: 11994
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ulong GetSize_Injected(IntPtr _unity_self);

		// Token: 0x06002EDB RID: 11995
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetInstanceCount_Injected(IntPtr _unity_self);

		// Token: 0x06002EDC RID: 11996
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearInstances_Injected(IntPtr _unity_self);

		// Token: 0x06002EDD RID: 11997
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr Create_Injected([In] ref RayTracingAccelerationStructure.Settings desc);

		// Token: 0x06002EDE RID: 11998
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Destroy_Injected(IntPtr accelStruct);

		// Token: 0x06002EDF RID: 11999
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveInstance_Renderer_Injected(IntPtr _unity_self, IntPtr targetRenderer);

		// Token: 0x06002EE0 RID: 12000
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveInstance_InstanceID_Injected(IntPtr _unity_self, int instanceID);

		// Token: 0x06002EE1 RID: 12001
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateInstanceTransform_Renderer_Injected(IntPtr _unity_self, IntPtr renderer);

		// Token: 0x06002EE2 RID: 12002
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateInstanceTransform_Handle_Injected(IntPtr _unity_self, int handle, [In] ref Matrix4x4 matrix);

		// Token: 0x06002EE3 RID: 12003
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateInstanceMask_Renderer_Injected(IntPtr _unity_self, IntPtr renderer, uint mask);

		// Token: 0x06002EE4 RID: 12004
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateInstanceMask_Handle_Injected(IntPtr _unity_self, int handle, uint mask);

		// Token: 0x06002EE5 RID: 12005
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateInstanceID_Renderer_Injected(IntPtr _unity_self, IntPtr renderer, uint id);

		// Token: 0x06002EE6 RID: 12006
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void UpdateInstanceID_Handle_Injected(IntPtr _unity_self, int handle, uint id);

		// Token: 0x06002EE7 RID: 12007
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddInstanceSubMeshFlagsArray_Injected(IntPtr _unity_self, IntPtr targetRenderer, ref ManagedSpanWrapper subMeshFlags, bool enableTriangleCulling, bool frontTriangleCounterClockwise, uint mask, uint id);

		// Token: 0x06002EE8 RID: 12008
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern int AddMeshInstance_Injected(IntPtr _unity_self, [In] ref RayTracingMeshInstanceConfig config, [In] ref Matrix4x4 matrix, Matrix4x4* prevMatrix, uint id);

		// Token: 0x06002EE9 RID: 12009
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern int AddGeometryInstance_Injected(IntPtr _unity_self, in RayTracingGeometryInstanceConfig config, [In] ref Matrix4x4 matrix, Matrix4x4* prevMatrix, uint id);

		// Token: 0x06002EEA RID: 12010
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddMeshInstances_Injected(IntPtr _unity_self, [In] ref RayTracingMeshInstanceConfig config, IntPtr instancedData, [In] ref RenderInstancedDataLayout layout, uint instanceCount, uint id);

		// Token: 0x06002EEB RID: 12011
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddAABBsInstance_Injected(IntPtr _unity_self, [In] ref RayTracingAABBsInstanceConfig config, [In] ref Matrix4x4 matrix, uint id);

		// Token: 0x06002EEC RID: 12012
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CullInstances_Injected(IntPtr _unity_self, in RayTracingInstanceCullingConfig cullingConfig, out RayTracingInstanceCullingResults ret);

		// Token: 0x06002EED RID: 12013
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CheckMaterialEnablesInstancing_Injected(IntPtr material);

		// Token: 0x04000F34 RID: 3892
		internal IntPtr m_Ptr;

		// Token: 0x04000F35 RID: 3893
		private const string obsoleteBuildMsg1 = "Method Update is deprecated and it will be removed in Unity 2024.1. Use Build instead (UnityUpgradable) -> Build()";

		// Token: 0x04000F36 RID: 3894
		private const string obsoleteBuildMsg2 = "Method Update is deprecated and it will be removed in Unity 2024.1. Use Build instead (UnityUpgradable) -> Build(*)";

		// Token: 0x04000F37 RID: 3895
		private const string obsoleteRendererMsg = "This AddInstance method is deprecated and will be removed and it will be removed in Unity 2024.1. Please use the alternative AddInstance method for adding Renderers to the acceleration structure.";

		// Token: 0x04000F38 RID: 3896
		private const string obsoleteAABBMsg1 = "This AddInstance method is deprecated and it will be removed in Unity 2024.1. Please use the alternative AddInstance method for adding procedural geometry (AABBs) to the acceleration structure.";

		// Token: 0x04000F39 RID: 3897
		private const string obsoleteAABBMsg2 = "This AddInstance method is deprecated and it will be removed in Unity 2024.1. Please use the alternative AddInstance method for adding procedural geometry (AABBs) to the acceleration structure.";

		// Token: 0x020004E6 RID: 1254
		[Flags]
		public enum RayTracingModeMask
		{
			// Token: 0x04000F3B RID: 3899
			Nothing = 0,
			// Token: 0x04000F3C RID: 3900
			Static = 2,
			// Token: 0x04000F3D RID: 3901
			DynamicTransform = 4,
			// Token: 0x04000F3E RID: 3902
			DynamicGeometry = 8,
			// Token: 0x04000F3F RID: 3903
			Everything = 14
		}

		// Token: 0x020004E7 RID: 1255
		public enum ManagementMode
		{
			// Token: 0x04000F41 RID: 3905
			Manual,
			// Token: 0x04000F42 RID: 3906
			Automatic
		}

		// Token: 0x020004E8 RID: 1256
		public struct BuildSettings
		{
			// Token: 0x170007D3 RID: 2003
			// (get) Token: 0x06002EEE RID: 12014 RVA: 0x0005936D File Offset: 0x0005756D
			// (set) Token: 0x06002EEF RID: 12015 RVA: 0x00059375 File Offset: 0x00057575
			public RayTracingAccelerationStructureBuildFlags buildFlags { readonly get; set; }

			// Token: 0x170007D4 RID: 2004
			// (get) Token: 0x06002EF0 RID: 12016 RVA: 0x0005937E File Offset: 0x0005757E
			// (set) Token: 0x06002EF1 RID: 12017 RVA: 0x00059386 File Offset: 0x00057586
			public Vector3 relativeOrigin { readonly get; set; }

			// Token: 0x06002EF2 RID: 12018 RVA: 0x0005938F File Offset: 0x0005758F
			public BuildSettings()
			{
				this.buildFlags = RayTracingAccelerationStructureBuildFlags.PreferFastTrace;
				this.relativeOrigin = Vector3.zero;
			}

			// Token: 0x06002EF3 RID: 12019 RVA: 0x000593A6 File Offset: 0x000575A6
			public BuildSettings(RayTracingAccelerationStructureBuildFlags buildFlags, Vector3 relativeOrigin)
			{
				this.buildFlags = buildFlags;
				this.relativeOrigin = relativeOrigin;
			}
		}

		// Token: 0x020004E9 RID: 1257
		[Obsolete("RayTracingAccelerationStructure.RASSettings is deprecated. Use RayTracingAccelerationStructure.Settings instead. (UnityUpgradable) -> RayTracingAccelerationStructure/Settings", false)]
		public struct RASSettings
		{
			// Token: 0x06002EF4 RID: 12020 RVA: 0x000593B9 File Offset: 0x000575B9
			public RASSettings(RayTracingAccelerationStructure.ManagementMode sceneManagementMode, RayTracingAccelerationStructure.RayTracingModeMask rayTracingModeMask, int layerMask)
			{
				this.managementMode = sceneManagementMode;
				this.rayTracingModeMask = rayTracingModeMask;
				this.layerMask = layerMask;
			}

			// Token: 0x04000F45 RID: 3909
			public RayTracingAccelerationStructure.ManagementMode managementMode;

			// Token: 0x04000F46 RID: 3910
			public RayTracingAccelerationStructure.RayTracingModeMask rayTracingModeMask;

			// Token: 0x04000F47 RID: 3911
			public int layerMask;
		}

		// Token: 0x020004EA RID: 1258
		public struct Settings
		{
			// Token: 0x170007D5 RID: 2005
			// (get) Token: 0x06002EF5 RID: 12021 RVA: 0x000593D1 File Offset: 0x000575D1
			// (set) Token: 0x06002EF6 RID: 12022 RVA: 0x000593D9 File Offset: 0x000575D9
			public RayTracingAccelerationStructureBuildFlags buildFlagsStaticGeometries { readonly get; set; }

			// Token: 0x170007D6 RID: 2006
			// (get) Token: 0x06002EF7 RID: 12023 RVA: 0x000593E2 File Offset: 0x000575E2
			// (set) Token: 0x06002EF8 RID: 12024 RVA: 0x000593EA File Offset: 0x000575EA
			public RayTracingAccelerationStructureBuildFlags buildFlagsDynamicGeometries { readonly get; set; }

			// Token: 0x170007D7 RID: 2007
			// (get) Token: 0x06002EF9 RID: 12025 RVA: 0x000593F3 File Offset: 0x000575F3
			// (set) Token: 0x06002EFA RID: 12026 RVA: 0x000593FB File Offset: 0x000575FB
			public bool enableCompaction { readonly get; set; }

			// Token: 0x06002EFB RID: 12027 RVA: 0x00059404 File Offset: 0x00057604
			public Settings()
			{
				this.managementMode = RayTracingAccelerationStructure.ManagementMode.Manual;
				this.rayTracingModeMask = RayTracingAccelerationStructure.RayTracingModeMask.Everything;
				this.layerMask = -1;
				this.buildFlagsStaticGeometries = RayTracingAccelerationStructureBuildFlags.PreferFastTrace;
				this.buildFlagsDynamicGeometries = RayTracingAccelerationStructureBuildFlags.PreferFastTrace;
				this.enableCompaction = true;
			}

			// Token: 0x06002EFC RID: 12028 RVA: 0x00059435 File Offset: 0x00057635
			public Settings(RayTracingAccelerationStructure.ManagementMode sceneManagementMode, RayTracingAccelerationStructure.RayTracingModeMask rayTracingModeMask, int layerMask)
			{
				this.managementMode = sceneManagementMode;
				this.rayTracingModeMask = rayTracingModeMask;
				this.layerMask = layerMask;
				this.buildFlagsStaticGeometries = RayTracingAccelerationStructureBuildFlags.PreferFastTrace;
				this.buildFlagsDynamicGeometries = RayTracingAccelerationStructureBuildFlags.PreferFastTrace;
				this.enableCompaction = true;
			}

			// Token: 0x06002EFD RID: 12029 RVA: 0x00059465 File Offset: 0x00057665
			public Settings(RayTracingAccelerationStructure.ManagementMode sceneManagementMode, RayTracingAccelerationStructure.RayTracingModeMask rayTracingModeMask, int layerMask, RayTracingAccelerationStructureBuildFlags buildFlagsStaticGeometries, RayTracingAccelerationStructureBuildFlags buildFlagsDynamicGeometries)
			{
				this.managementMode = sceneManagementMode;
				this.rayTracingModeMask = rayTracingModeMask;
				this.layerMask = layerMask;
				this.buildFlagsStaticGeometries = buildFlagsStaticGeometries;
				this.buildFlagsDynamicGeometries = buildFlagsDynamicGeometries;
				this.enableCompaction = true;
			}

			// Token: 0x04000F48 RID: 3912
			public RayTracingAccelerationStructure.ManagementMode managementMode;

			// Token: 0x04000F49 RID: 3913
			public RayTracingAccelerationStructure.RayTracingModeMask rayTracingModeMask;

			// Token: 0x04000F4A RID: 3914
			public int layerMask;
		}

		// Token: 0x020004EB RID: 1259
		internal static class BindingsMarshaller
		{
			// Token: 0x06002EFE RID: 12030 RVA: 0x00059497 File Offset: 0x00057697
			public static IntPtr ConvertToNative(RayTracingAccelerationStructure rayTracingAccelerationStructure)
			{
				return rayTracingAccelerationStructure.m_Ptr;
			}
		}
	}
}
