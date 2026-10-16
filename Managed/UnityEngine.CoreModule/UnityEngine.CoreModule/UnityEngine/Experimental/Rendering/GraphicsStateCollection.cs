using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Jobs;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x0200059D RID: 1437
	[NativeHeader("Runtime/Graphics/GraphicsStateCollection.h")]
	public sealed class GraphicsStateCollection : Object
	{
		// Token: 0x06003780 RID: 14208 RVA: 0x0006D078 File Offset: 0x0006B278
		public bool BeginTrace()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GraphicsStateCollection>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return GraphicsStateCollection.BeginTrace_Injected(intPtr);
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x0006D09C File Offset: 0x0006B29C
		public void EndTrace()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GraphicsStateCollection>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GraphicsStateCollection.EndTrace_Injected(intPtr);
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06003782 RID: 14210 RVA: 0x0006D0C0 File Offset: 0x0006B2C0
		public bool isTracing
		{
			[NativeName("IsTracing")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GraphicsStateCollection>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GraphicsStateCollection.get_isTracing_Injected(intPtr);
			}
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06003783 RID: 14211 RVA: 0x0006D0E4 File Offset: 0x0006B2E4
		// (set) Token: 0x06003784 RID: 14212 RVA: 0x0006D108 File Offset: 0x0006B308
		public int version
		{
			[NativeName("GetVersion")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GraphicsStateCollection>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GraphicsStateCollection.get_version_Injected(intPtr);
			}
			[NativeName("SetVersion")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GraphicsStateCollection>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GraphicsStateCollection.set_version_Injected(intPtr, value);
			}
		}

		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06003785 RID: 14213 RVA: 0x0006D12C File Offset: 0x0006B32C
		// (set) Token: 0x06003786 RID: 14214 RVA: 0x0006D150 File Offset: 0x0006B350
		public GraphicsDeviceType graphicsDeviceType
		{
			[NativeName("GetDeviceRenderer")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GraphicsStateCollection>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GraphicsStateCollection.get_graphicsDeviceType_Injected(intPtr);
			}
			[NativeName("SetDeviceRenderer")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GraphicsStateCollection>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GraphicsStateCollection.set_graphicsDeviceType_Injected(intPtr, value);
			}
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06003787 RID: 14215 RVA: 0x0006D174 File Offset: 0x0006B374
		// (set) Token: 0x06003788 RID: 14216 RVA: 0x0006D198 File Offset: 0x0006B398
		public RuntimePlatform runtimePlatform
		{
			[NativeName("GetRuntimePlatform")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GraphicsStateCollection>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GraphicsStateCollection.get_runtimePlatform_Injected(intPtr);
			}
			[NativeName("SetRuntimePlatform")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GraphicsStateCollection>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				GraphicsStateCollection.set_runtimePlatform_Injected(intPtr, value);
			}
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06003789 RID: 14217 RVA: 0x0006D1BC File Offset: 0x0006B3BC
		// (set) Token: 0x0600378A RID: 14218 RVA: 0x0006D1FC File Offset: 0x0006B3FC
		public unsafe string qualityLevelName
		{
			[NativeName("GetQualityLevelName")]
			get
			{
				string stringAndDispose;
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GraphicsStateCollection>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					GraphicsStateCollection.get_qualityLevelName_Injected(intPtr, out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
			[NativeName("SetQualityLevelName")]
			set
			{
				try
				{
					IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GraphicsStateCollection>(this);
					if (intPtr == 0)
					{
						ThrowHelper.ThrowNullReferenceException(this);
					}
					ManagedSpanWrapper managedSpanWrapper;
					if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
					{
						ReadOnlySpan<char> readOnlySpan = value.AsSpan();
						fixed (char* ptr = readOnlySpan.GetPinnableReference())
						{
							managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
						}
					}
					GraphicsStateCollection.set_qualityLevelName_Injected(intPtr, ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x0600378B RID: 14219 RVA: 0x0006D260 File Offset: 0x0006B460
		public unsafe bool LoadFromFile(string filePath)
		{
			bool flag;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GraphicsStateCollection>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(filePath, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = filePath.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = GraphicsStateCollection.LoadFromFile_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x0600378C RID: 14220 RVA: 0x0006D2C8 File Offset: 0x0006B4C8
		public unsafe bool SaveToFile(string filePath)
		{
			bool flag;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GraphicsStateCollection>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(filePath, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = filePath.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = GraphicsStateCollection.SaveToFile_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x0600378D RID: 14221 RVA: 0x0006D330 File Offset: 0x0006B530
		public unsafe bool SendToEditor(string fileName)
		{
			bool flag;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GraphicsStateCollection>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(fileName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = fileName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = GraphicsStateCollection.SendToEditor_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x0600378E RID: 14222 RVA: 0x0006D398 File Offset: 0x0006B598
		[NativeName("Warmup")]
		public JobHandle WarmUp(JobHandle dependency = default(JobHandle))
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GraphicsStateCollection>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			JobHandle jobHandle;
			GraphicsStateCollection.WarmUp_Injected(intPtr, ref dependency, out jobHandle);
			return jobHandle;
		}

		// Token: 0x0600378F RID: 14223 RVA: 0x0006D3C0 File Offset: 0x0006B5C0
		[NativeName("WarmupProgressively")]
		public JobHandle WarmUpProgressively(int count, JobHandle dependency = default(JobHandle))
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GraphicsStateCollection>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			JobHandle jobHandle;
			GraphicsStateCollection.WarmUpProgressively_Injected(intPtr, count, ref dependency, out jobHandle);
			return jobHandle;
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06003790 RID: 14224 RVA: 0x0006D3E8 File Offset: 0x0006B5E8
		public int totalGraphicsStateCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GraphicsStateCollection>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GraphicsStateCollection.get_totalGraphicsStateCount_Injected(intPtr);
			}
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06003791 RID: 14225 RVA: 0x0006D40C File Offset: 0x0006B60C
		public int completedWarmupCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GraphicsStateCollection>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GraphicsStateCollection.get_completedWarmupCount_Injected(intPtr);
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06003792 RID: 14226 RVA: 0x0006D430 File Offset: 0x0006B630
		public bool isWarmedUp
		{
			[NativeName("IsWarmedUp")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GraphicsStateCollection>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GraphicsStateCollection.get_isWarmedUp_Injected(intPtr);
			}
		}

		// Token: 0x06003793 RID: 14227 RVA: 0x0006D454 File Offset: 0x0006B654
		private void GetVariants([Out] GraphicsStateCollection.ShaderVariant[] results)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GraphicsStateCollection>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GraphicsStateCollection.GetVariants_Injected(intPtr, results);
		}

		// Token: 0x06003794 RID: 14228 RVA: 0x0006D478 File Offset: 0x0006B678
		public void GetVariants(List<GraphicsStateCollection.ShaderVariant> results)
		{
			bool flag = results == null;
			if (flag)
			{
				throw new ArgumentNullException("The result shader variant list cannot be null.");
			}
			results.Clear();
			NoAllocHelpers.EnsureListElemCount<GraphicsStateCollection.ShaderVariant>(results, this.variantCount);
			this.GetVariants(NoAllocHelpers.ExtractArrayFromList<GraphicsStateCollection.ShaderVariant>(results));
		}

		// Token: 0x06003795 RID: 14229 RVA: 0x0006D4BC File Offset: 0x0006B6BC
		private void GetGraphicsStatesForVariant(Shader shader, PassIdentifier passId, LocalKeyword[] keywords, [Out] GraphicsStateCollection.GraphicsState[] results)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GraphicsStateCollection>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GraphicsStateCollection.GetGraphicsStatesForVariant_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Shader>(shader), ref passId, keywords, results);
		}

		// Token: 0x06003796 RID: 14230 RVA: 0x0006D4EC File Offset: 0x0006B6EC
		public void GetGraphicsStatesForVariant(Shader shader, PassIdentifier passId, LocalKeyword[] keywords, List<GraphicsStateCollection.GraphicsState> results)
		{
			bool flag = results == null;
			if (flag)
			{
				throw new ArgumentNullException("The result graphics state list cannot be null.");
			}
			results.Clear();
			NoAllocHelpers.EnsureListElemCount<GraphicsStateCollection.GraphicsState>(results, this.GetGraphicsStateCountForVariant(shader, passId, keywords));
			this.GetGraphicsStatesForVariant(shader, passId, keywords, NoAllocHelpers.ExtractArrayFromList<GraphicsStateCollection.GraphicsState>(results));
		}

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06003797 RID: 14231 RVA: 0x0006D538 File Offset: 0x0006B738
		public int variantCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GraphicsStateCollection>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return GraphicsStateCollection.get_variantCount_Injected(intPtr);
			}
		}

		// Token: 0x06003798 RID: 14232 RVA: 0x0006D55C File Offset: 0x0006B75C
		public int GetGraphicsStateCountForVariant(Shader shader, PassIdentifier passId, LocalKeyword[] keywords)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GraphicsStateCollection>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return GraphicsStateCollection.GetGraphicsStateCountForVariant_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Shader>(shader), ref passId, keywords);
		}

		// Token: 0x06003799 RID: 14233 RVA: 0x0006D588 File Offset: 0x0006B788
		public bool AddVariant(Shader shader, PassIdentifier passId, LocalKeyword[] keywords)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GraphicsStateCollection>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return GraphicsStateCollection.AddVariant_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Shader>(shader), ref passId, keywords);
		}

		// Token: 0x0600379A RID: 14234 RVA: 0x0006D5B4 File Offset: 0x0006B7B4
		public bool RemoveVariant(Shader shader, PassIdentifier passId, LocalKeyword[] keywords)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GraphicsStateCollection>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return GraphicsStateCollection.RemoveVariant_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Shader>(shader), ref passId, keywords);
		}

		// Token: 0x0600379B RID: 14235 RVA: 0x0006D5E0 File Offset: 0x0006B7E0
		public bool ContainsVariant(Shader shader, PassIdentifier passId, LocalKeyword[] keywords)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GraphicsStateCollection>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return GraphicsStateCollection.ContainsVariant_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Shader>(shader), ref passId, keywords);
		}

		// Token: 0x0600379C RID: 14236 RVA: 0x0006D60C File Offset: 0x0006B80C
		public void ClearVariants()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GraphicsStateCollection>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			GraphicsStateCollection.ClearVariants_Injected(intPtr);
		}

		// Token: 0x0600379D RID: 14237 RVA: 0x0006D630 File Offset: 0x0006B830
		public bool AddGraphicsStateForVariant(Shader shader, PassIdentifier passId, LocalKeyword[] keywords, GraphicsStateCollection.GraphicsState setup)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GraphicsStateCollection>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return GraphicsStateCollection.AddGraphicsStateForVariant_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Shader>(shader), ref passId, keywords, ref setup);
		}

		// Token: 0x0600379E RID: 14238 RVA: 0x0006D660 File Offset: 0x0006B860
		public bool RemoveGraphicsStatesForVariant(Shader shader, PassIdentifier passId, LocalKeyword[] keywords)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<GraphicsStateCollection>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return GraphicsStateCollection.RemoveGraphicsStatesForVariant_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Shader>(shader), ref passId, keywords);
		}

		// Token: 0x0600379F RID: 14239
		[NativeName("CreateFromScript")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] GraphicsStateCollection gsc);

		// Token: 0x060037A0 RID: 14240 RVA: 0x0006D68B File Offset: 0x0006B88B
		public GraphicsStateCollection()
		{
			GraphicsStateCollection.Internal_Create(this);
		}

		// Token: 0x060037A1 RID: 14241 RVA: 0x0006D69C File Offset: 0x0006B89C
		public GraphicsStateCollection(string filePath)
		{
			GraphicsStateCollection.Internal_Create(this);
			this.LoadFromFile(filePath);
		}

		// Token: 0x060037A2 RID: 14242 RVA: 0x0006D6B5 File Offset: 0x0006B8B5
		public void GetGraphicsStatesForVariant(GraphicsStateCollection.ShaderVariant variant, List<GraphicsStateCollection.GraphicsState> results)
		{
			this.GetGraphicsStatesForVariant(variant.shader, variant.passId, variant.keywords, results);
		}

		// Token: 0x060037A3 RID: 14243 RVA: 0x0006D6D4 File Offset: 0x0006B8D4
		public int GetGraphicsStateCountForVariant(GraphicsStateCollection.ShaderVariant variant)
		{
			return this.GetGraphicsStateCountForVariant(variant.shader, variant.passId, variant.keywords);
		}

		// Token: 0x060037A4 RID: 14244
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool BeginTrace_Injected(IntPtr _unity_self);

		// Token: 0x060037A5 RID: 14245
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EndTrace_Injected(IntPtr _unity_self);

		// Token: 0x060037A6 RID: 14246
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isTracing_Injected(IntPtr _unity_self);

		// Token: 0x060037A7 RID: 14247
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_version_Injected(IntPtr _unity_self);

		// Token: 0x060037A8 RID: 14248
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_version_Injected(IntPtr _unity_self, int value);

		// Token: 0x060037A9 RID: 14249
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GraphicsDeviceType get_graphicsDeviceType_Injected(IntPtr _unity_self);

		// Token: 0x060037AA RID: 14250
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_graphicsDeviceType_Injected(IntPtr _unity_self, GraphicsDeviceType value);

		// Token: 0x060037AB RID: 14251
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RuntimePlatform get_runtimePlatform_Injected(IntPtr _unity_self);

		// Token: 0x060037AC RID: 14252
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_runtimePlatform_Injected(IntPtr _unity_self, RuntimePlatform value);

		// Token: 0x060037AD RID: 14253
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_qualityLevelName_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x060037AE RID: 14254
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_qualityLevelName_Injected(IntPtr _unity_self, ref ManagedSpanWrapper value);

		// Token: 0x060037AF RID: 14255
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool LoadFromFile_Injected(IntPtr _unity_self, ref ManagedSpanWrapper filePath);

		// Token: 0x060037B0 RID: 14256
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SaveToFile_Injected(IntPtr _unity_self, ref ManagedSpanWrapper filePath);

		// Token: 0x060037B1 RID: 14257
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SendToEditor_Injected(IntPtr _unity_self, ref ManagedSpanWrapper fileName);

		// Token: 0x060037B2 RID: 14258
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WarmUp_Injected(IntPtr _unity_self, [In] ref JobHandle dependency, out JobHandle ret);

		// Token: 0x060037B3 RID: 14259
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WarmUpProgressively_Injected(IntPtr _unity_self, int count, [In] ref JobHandle dependency, out JobHandle ret);

		// Token: 0x060037B4 RID: 14260
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_totalGraphicsStateCount_Injected(IntPtr _unity_self);

		// Token: 0x060037B5 RID: 14261
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_completedWarmupCount_Injected(IntPtr _unity_self);

		// Token: 0x060037B6 RID: 14262
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isWarmedUp_Injected(IntPtr _unity_self);

		// Token: 0x060037B7 RID: 14263
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetVariants_Injected(IntPtr _unity_self, [Out] GraphicsStateCollection.ShaderVariant[] results);

		// Token: 0x060037B8 RID: 14264
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGraphicsStatesForVariant_Injected(IntPtr _unity_self, IntPtr shader, [In] ref PassIdentifier passId, LocalKeyword[] keywords, [Out] GraphicsStateCollection.GraphicsState[] results);

		// Token: 0x060037B9 RID: 14265
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_variantCount_Injected(IntPtr _unity_self);

		// Token: 0x060037BA RID: 14266
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGraphicsStateCountForVariant_Injected(IntPtr _unity_self, IntPtr shader, [In] ref PassIdentifier passId, LocalKeyword[] keywords);

		// Token: 0x060037BB RID: 14267
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool AddVariant_Injected(IntPtr _unity_self, IntPtr shader, [In] ref PassIdentifier passId, LocalKeyword[] keywords);

		// Token: 0x060037BC RID: 14268
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RemoveVariant_Injected(IntPtr _unity_self, IntPtr shader, [In] ref PassIdentifier passId, LocalKeyword[] keywords);

		// Token: 0x060037BD RID: 14269
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ContainsVariant_Injected(IntPtr _unity_self, IntPtr shader, [In] ref PassIdentifier passId, LocalKeyword[] keywords);

		// Token: 0x060037BE RID: 14270
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearVariants_Injected(IntPtr _unity_self);

		// Token: 0x060037BF RID: 14271
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool AddGraphicsStateForVariant_Injected(IntPtr _unity_self, IntPtr shader, [In] ref PassIdentifier passId, LocalKeyword[] keywords, [In] ref GraphicsStateCollection.GraphicsState setup);

		// Token: 0x060037C0 RID: 14272
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RemoveGraphicsStatesForVariant_Injected(IntPtr _unity_self, IntPtr shader, [In] ref PassIdentifier passId, LocalKeyword[] keywords);

		// Token: 0x0200059E RID: 1438
		public struct GraphicsState
		{
			// Token: 0x040013B2 RID: 5042
			public VertexAttributeDescriptor[] vertexAttributes;

			// Token: 0x040013B3 RID: 5043
			public AttachmentDescriptor[] attachments;

			// Token: 0x040013B4 RID: 5044
			public SubPassDescriptor[] subPasses;

			// Token: 0x040013B5 RID: 5045
			public RenderStateBlock renderState;

			// Token: 0x040013B6 RID: 5046
			public MeshTopology topology;

			// Token: 0x040013B7 RID: 5047
			public CullMode forceCullMode;

			// Token: 0x040013B8 RID: 5048
			public float depthBias;

			// Token: 0x040013B9 RID: 5049
			public float slopeDepthBias;

			// Token: 0x040013BA RID: 5050
			public int depthAttachmentIndex;

			// Token: 0x040013BB RID: 5051
			public int subPassIndex;

			// Token: 0x040013BC RID: 5052
			public int shadingRateIndex;

			// Token: 0x040013BD RID: 5053
			public int multiviewCount;

			// Token: 0x040013BE RID: 5054
			public int sampleCount;

			// Token: 0x040013BF RID: 5055
			public bool wireframe;

			// Token: 0x040013C0 RID: 5056
			public bool invertCulling;

			// Token: 0x040013C1 RID: 5057
			public bool negativeScale;

			// Token: 0x040013C2 RID: 5058
			public bool invertProjection;
		}

		// Token: 0x0200059F RID: 1439
		public struct ShaderVariant
		{
			// Token: 0x040013C3 RID: 5059
			public Shader shader;

			// Token: 0x040013C4 RID: 5060
			public PassIdentifier passId;

			// Token: 0x040013C5 RID: 5061
			public LocalKeyword[] keywords;
		}
	}
}
