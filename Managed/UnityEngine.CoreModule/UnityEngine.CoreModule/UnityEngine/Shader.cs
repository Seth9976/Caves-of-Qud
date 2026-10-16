using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;

namespace UnityEngine
{
	// Token: 0x0200018A RID: 394
	[NativeHeader("Runtime/Shaders/GpuPrograms/ShaderVariantCollection.h")]
	[NativeHeader("Runtime/Shaders/Keywords/KeywordSpaceScriptBindings.h")]
	[NativeHeader("Runtime/Misc/ResourceManager.h")]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	[NativeHeader("Runtime/Shaders/ComputeShader.h")]
	[NativeHeader("Runtime/Shaders/ShaderNameRegistry.h")]
	[NativeHeader("Runtime/Shaders/Shader.h")]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	public sealed class Shader : Object
	{
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06001125 RID: 4389 RVA: 0x0001D930 File Offset: 0x0001BB30
		// (set) Token: 0x06001126 RID: 4390 RVA: 0x0001D947 File Offset: 0x0001BB47
		[Obsolete("Use Graphics.activeTier instead (UnityUpgradable) -> UnityEngine.Graphics.activeTier", false)]
		public static ShaderHardwareTier globalShaderHardwareTier
		{
			get
			{
				return (ShaderHardwareTier)Graphics.activeTier;
			}
			set
			{
				Graphics.activeTier = (GraphicsTier)value;
			}
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0001D951 File Offset: 0x0001BB51
		public static Shader Find(string name)
		{
			return ResourcesAPI.ActiveAPI.FindShaderByName(name);
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x0001D960 File Offset: 0x0001BB60
		[FreeFunction("GetBuiltinResource<Shader>")]
		internal unsafe static Shader FindBuiltin(string name)
		{
			Shader shader;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				IntPtr intPtr = Shader.FindBuiltin_Injected(ref managedSpanWrapper);
			}
			finally
			{
				IntPtr intPtr;
				shader = Unmarshal.UnmarshalUnityObject<Shader>(intPtr);
				char* ptr = null;
			}
			return shader;
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06001129 RID: 4393
		// (set) Token: 0x0600112A RID: 4394
		[NativeProperty("MaxChunksRuntimeOverride")]
		public static extern int maximumChunksOverride
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x0001D9C0 File Offset: 0x0001BBC0
		// (set) Token: 0x0600112C RID: 4396 RVA: 0x0001D9E4 File Offset: 0x0001BBE4
		[NativeProperty("MaximumShaderLOD")]
		public int maximumLOD
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Shader.get_maximumLOD_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Shader.set_maximumLOD_Injected(intPtr, value);
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600112D RID: 4397
		// (set) Token: 0x0600112E RID: 4398
		[NativeProperty("GlobalMaximumShaderLOD")]
		public static extern int globalMaximumLOD
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x0600112F RID: 4399 RVA: 0x0001DA08 File Offset: 0x0001BC08
		public bool isSupported
		{
			[NativeMethod("IsSupported")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Shader.get_isSupported_Injected(intPtr);
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x0001DA2C File Offset: 0x0001BC2C
		// (set) Token: 0x06001131 RID: 4401 RVA: 0x0001DA5C File Offset: 0x0001BC5C
		public unsafe static string globalRenderPipeline
		{
			get
			{
				string stringAndDispose;
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					Shader.get_globalRenderPipeline_Injected(out managedSpanWrapper);
				}
				finally
				{
					ManagedSpanWrapper managedSpanWrapper;
					stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
				}
				return stringAndDispose;
			}
			set
			{
				try
				{
					ManagedSpanWrapper managedSpanWrapper;
					if (!StringMarshaller.TryMarshalEmptyOrNullString(value, ref managedSpanWrapper))
					{
						ReadOnlySpan<char> readOnlySpan = value.AsSpan();
						fixed (char* ptr = readOnlySpan.GetPinnableReference())
						{
							managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
						}
					}
					Shader.set_globalRenderPipeline_Injected(ref managedSpanWrapper);
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06001132 RID: 4402 RVA: 0x0001DAB0 File Offset: 0x0001BCB0
		public static GlobalKeyword[] enabledGlobalKeywords
		{
			get
			{
				return Shader.GetEnabledGlobalKeywords();
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06001133 RID: 4403 RVA: 0x0001DAC8 File Offset: 0x0001BCC8
		public static GlobalKeyword[] globalKeywords
		{
			get
			{
				return Shader.GetAllGlobalKeywords();
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06001134 RID: 4404 RVA: 0x0001DAE0 File Offset: 0x0001BCE0
		public LocalKeywordSpace keywordSpace
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				LocalKeywordSpace localKeywordSpace;
				Shader.get_keywordSpace_Injected(intPtr, out localKeywordSpace);
				return localKeywordSpace;
			}
		}

		// Token: 0x06001135 RID: 4405
		[FreeFunction("keywords::GetEnabledGlobalKeywords")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern GlobalKeyword[] GetEnabledGlobalKeywords();

		// Token: 0x06001136 RID: 4406
		[FreeFunction("keywords::GetAllGlobalKeywords")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern GlobalKeyword[] GetAllGlobalKeywords();

		// Token: 0x06001137 RID: 4407 RVA: 0x0001DB08 File Offset: 0x0001BD08
		[FreeFunction("ShaderScripting::EnableKeyword")]
		public unsafe static void EnableKeyword(string keyword)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(keyword, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = keyword.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Shader.EnableKeyword_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06001138 RID: 4408 RVA: 0x0001DB5C File Offset: 0x0001BD5C
		[FreeFunction("ShaderScripting::DisableKeyword")]
		public unsafe static void DisableKeyword(string keyword)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(keyword, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = keyword.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Shader.DisableKeyword_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x0001DBB0 File Offset: 0x0001BDB0
		[FreeFunction("ShaderScripting::IsKeywordEnabled")]
		public unsafe static bool IsKeywordEnabled(string keyword)
		{
			bool flag;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(keyword, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = keyword.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = Shader.IsKeywordEnabled_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x0001DC08 File Offset: 0x0001BE08
		[FreeFunction("ShaderScripting::EnableKeyword")]
		internal static void EnableKeywordFast(GlobalKeyword keyword)
		{
			Shader.EnableKeywordFast_Injected(ref keyword);
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x0001DC1C File Offset: 0x0001BE1C
		[FreeFunction("ShaderScripting::DisableKeyword")]
		internal static void DisableKeywordFast(GlobalKeyword keyword)
		{
			Shader.DisableKeywordFast_Injected(ref keyword);
		}

		// Token: 0x0600113C RID: 4412 RVA: 0x0001DC30 File Offset: 0x0001BE30
		[FreeFunction("ShaderScripting::SetKeyword")]
		internal static void SetKeywordFast(GlobalKeyword keyword, bool value)
		{
			Shader.SetKeywordFast_Injected(ref keyword, value);
		}

		// Token: 0x0600113D RID: 4413 RVA: 0x0001DC48 File Offset: 0x0001BE48
		[FreeFunction("ShaderScripting::IsKeywordEnabled")]
		internal static bool IsKeywordEnabledFast(GlobalKeyword keyword)
		{
			return Shader.IsKeywordEnabledFast_Injected(ref keyword);
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x0001DC5C File Offset: 0x0001BE5C
		public static void EnableKeyword(in GlobalKeyword keyword)
		{
			Shader.EnableKeywordFast(keyword);
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x0001DC6B File Offset: 0x0001BE6B
		public static void DisableKeyword(in GlobalKeyword keyword)
		{
			Shader.DisableKeywordFast(keyword);
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x0001DC7A File Offset: 0x0001BE7A
		public static void SetKeyword(in GlobalKeyword keyword, bool value)
		{
			Shader.SetKeywordFast(keyword, value);
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x0001DC8C File Offset: 0x0001BE8C
		public static bool IsKeywordEnabled(in GlobalKeyword keyword)
		{
			return Shader.IsKeywordEnabledFast(keyword);
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x0001DCAC File Offset: 0x0001BEAC
		public int renderQueue
		{
			[FreeFunction("ShaderScripting::GetRenderQueue", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Shader.get_renderQueue_Injected(intPtr);
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x0001DCD0 File Offset: 0x0001BED0
		internal DisableBatchingType disableBatching
		{
			[FreeFunction("ShaderScripting::GetDisableBatchingType", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Shader.get_disableBatching_Injected(intPtr);
			}
		}

		// Token: 0x06001144 RID: 4420
		[FreeFunction]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void WarmupAllShaders();

		// Token: 0x06001145 RID: 4421 RVA: 0x0001DCF4 File Offset: 0x0001BEF4
		[FreeFunction("ShaderScripting::TagToID")]
		internal unsafe static int TagToID(string name)
		{
			int num;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				num = Shader.TagToID_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x06001146 RID: 4422 RVA: 0x0001DD4C File Offset: 0x0001BF4C
		[FreeFunction("ShaderScripting::IDToTag")]
		internal static string IDToTag(int name)
		{
			string stringAndDispose;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				Shader.IDToTag_Injected(name, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06001147 RID: 4423 RVA: 0x0001DD7C File Offset: 0x0001BF7C
		[FreeFunction(Name = "ShaderScripting::PropertyToID", IsThreadSafe = true)]
		public unsafe static int PropertyToID(string name)
		{
			int num;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				num = Shader.PropertyToID_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x06001148 RID: 4424 RVA: 0x0001DDD4 File Offset: 0x0001BFD4
		public unsafe Shader GetDependency(string name)
		{
			Shader shader;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
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
				IntPtr dependency_Injected = Shader.GetDependency_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				IntPtr dependency_Injected;
				shader = Unmarshal.UnmarshalUnityObject<Shader>(dependency_Injected);
				char* ptr = null;
			}
			return shader;
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06001149 RID: 4425 RVA: 0x0001DE44 File Offset: 0x0001C044
		public int passCount
		{
			[FreeFunction(Name = "ShaderScripting::GetPassCount", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Shader.get_passCount_Injected(intPtr);
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x0001DE68 File Offset: 0x0001C068
		public int subshaderCount
		{
			[FreeFunction(Name = "ShaderScripting::GetSubshaderCount", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Shader.get_subshaderCount_Injected(intPtr);
			}
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x0001DE8C File Offset: 0x0001C08C
		[FreeFunction(Name = "ShaderScripting::GetPassCountInSubshader", HasExplicitThis = true)]
		public int GetPassCountInSubshader(int subshaderIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Shader.GetPassCountInSubshader_Injected(intPtr, subshaderIndex);
		}

		// Token: 0x0600114C RID: 4428 RVA: 0x0001DEB0 File Offset: 0x0001C0B0
		public ShaderTagId FindPassTagValue(int passIndex, ShaderTagId tagName)
		{
			bool flag = passIndex < 0 || passIndex >= this.passCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("passIndex");
			}
			int num = this.Internal_FindPassTagValue(passIndex, tagName.id);
			return new ShaderTagId
			{
				id = num
			};
		}

		// Token: 0x0600114D RID: 4429 RVA: 0x0001DF08 File Offset: 0x0001C108
		public ShaderTagId FindPassTagValue(int subshaderIndex, int passIndex, ShaderTagId tagName)
		{
			bool flag = subshaderIndex < 0 || subshaderIndex >= this.subshaderCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException("subshaderIndex");
			}
			bool flag2 = passIndex < 0 || passIndex >= this.GetPassCountInSubshader(subshaderIndex);
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("passIndex");
			}
			int num = this.Internal_FindPassTagValueInSubShader(subshaderIndex, passIndex, tagName.id);
			return new ShaderTagId
			{
				id = num
			};
		}

		// Token: 0x0600114E RID: 4430 RVA: 0x0001DF84 File Offset: 0x0001C184
		public ShaderTagId FindSubshaderTagValue(int subshaderIndex, ShaderTagId tagName)
		{
			bool flag = subshaderIndex < 0 || subshaderIndex >= this.subshaderCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Invalid subshaderIndex {0}. Value must be in the range [0, {1})", subshaderIndex, this.subshaderCount));
			}
			int num = this.Internal_FindSubshaderTagValue(subshaderIndex, tagName.id);
			return new ShaderTagId
			{
				id = num
			};
		}

		// Token: 0x0600114F RID: 4431 RVA: 0x0001DFF0 File Offset: 0x0001C1F0
		[FreeFunction(Name = "ShaderScripting::FindPassTagValue", HasExplicitThis = true)]
		private int Internal_FindPassTagValue(int passIndex, int tagName)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Shader.Internal_FindPassTagValue_Injected(intPtr, passIndex, tagName);
		}

		// Token: 0x06001150 RID: 4432 RVA: 0x0001E014 File Offset: 0x0001C214
		[FreeFunction(Name = "ShaderScripting::FindPassTagValue", HasExplicitThis = true)]
		private int Internal_FindPassTagValueInSubShader(int subShaderIndex, int passIndex, int tagName)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Shader.Internal_FindPassTagValueInSubShader_Injected(intPtr, subShaderIndex, passIndex, tagName);
		}

		// Token: 0x06001151 RID: 4433 RVA: 0x0001E03C File Offset: 0x0001C23C
		[FreeFunction(Name = "ShaderScripting::FindSubshaderTagValue", HasExplicitThis = true)]
		private int Internal_FindSubshaderTagValue(int subShaderIndex, int tagName)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Shader.Internal_FindSubshaderTagValue_Injected(intPtr, subShaderIndex, tagName);
		}

		// Token: 0x06001152 RID: 4434
		[FreeFunction("ShaderScripting::SetGlobalInt")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalIntImpl(int name, int value);

		// Token: 0x06001153 RID: 4435
		[FreeFunction("ShaderScripting::SetGlobalFloat")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalFloatImpl(int name, float value);

		// Token: 0x06001154 RID: 4436 RVA: 0x0001E060 File Offset: 0x0001C260
		[FreeFunction("ShaderScripting::SetGlobalVector")]
		private static void SetGlobalVectorImpl(int name, Vector4 value)
		{
			Shader.SetGlobalVectorImpl_Injected(name, ref value);
		}

		// Token: 0x06001155 RID: 4437 RVA: 0x0001E078 File Offset: 0x0001C278
		[FreeFunction("ShaderScripting::SetGlobalMatrix")]
		private static void SetGlobalMatrixImpl(int name, Matrix4x4 value)
		{
			Shader.SetGlobalMatrixImpl_Injected(name, ref value);
		}

		// Token: 0x06001156 RID: 4438 RVA: 0x0001E090 File Offset: 0x0001C290
		[FreeFunction("ShaderScripting::SetGlobalTexture")]
		private static void SetGlobalTextureImpl(int name, Texture value)
		{
			Shader.SetGlobalTextureImpl_Injected(name, Object.MarshalledUnityObject.Marshal<Texture>(value));
		}

		// Token: 0x06001157 RID: 4439 RVA: 0x0001E0AC File Offset: 0x0001C2AC
		[FreeFunction("ShaderScripting::SetGlobalRenderTexture")]
		private static void SetGlobalRenderTextureImpl(int name, RenderTexture value, RenderTextureSubElement element)
		{
			Shader.SetGlobalRenderTextureImpl_Injected(name, Object.MarshalledUnityObject.Marshal<RenderTexture>(value), element);
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x0001E0C8 File Offset: 0x0001C2C8
		[FreeFunction("ShaderScripting::SetGlobalBuffer")]
		private static void SetGlobalBufferImpl(int name, ComputeBuffer value)
		{
			Shader.SetGlobalBufferImpl_Injected(name, (value == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(value));
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x0001E0EC File Offset: 0x0001C2EC
		[FreeFunction("ShaderScripting::SetGlobalBuffer")]
		private static void SetGlobalGraphicsBufferImpl(int name, GraphicsBuffer value)
		{
			Shader.SetGlobalGraphicsBufferImpl_Injected(name, (value == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(value));
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x0001E110 File Offset: 0x0001C310
		[FreeFunction("ShaderScripting::SetGlobalConstantBuffer")]
		private static void SetGlobalConstantBufferImpl(int name, ComputeBuffer value, int offset, int size)
		{
			Shader.SetGlobalConstantBufferImpl_Injected(name, (value == null) ? ((IntPtr)0) : ComputeBuffer.BindingsMarshaller.ConvertToNative(value), offset, size);
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x0001E134 File Offset: 0x0001C334
		[FreeFunction("ShaderScripting::SetGlobalConstantBuffer")]
		private static void SetGlobalConstantGraphicsBufferImpl(int name, GraphicsBuffer value, int offset, int size)
		{
			Shader.SetGlobalConstantGraphicsBufferImpl_Injected(name, (value == null) ? ((IntPtr)0) : GraphicsBuffer.BindingsMarshaller.ConvertToNative(value), offset, size);
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x0001E158 File Offset: 0x0001C358
		[FreeFunction("ShaderScripting::SetGlobalRayTracingAccelerationStructure")]
		private static void SetGlobalRayTracingAccelerationStructureImpl(int name, RayTracingAccelerationStructure accelerationStructure)
		{
			Shader.SetGlobalRayTracingAccelerationStructureImpl_Injected(name, (accelerationStructure == null) ? ((IntPtr)0) : RayTracingAccelerationStructure.BindingsMarshaller.ConvertToNative(accelerationStructure));
		}

		// Token: 0x0600115D RID: 4445
		[FreeFunction("ShaderScripting::GetGlobalInt")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGlobalIntImpl(int name);

		// Token: 0x0600115E RID: 4446
		[FreeFunction("ShaderScripting::GetGlobalFloat")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetGlobalFloatImpl(int name);

		// Token: 0x0600115F RID: 4447 RVA: 0x0001E17C File Offset: 0x0001C37C
		[FreeFunction("ShaderScripting::GetGlobalVector")]
		private static Vector4 GetGlobalVectorImpl(int name)
		{
			Vector4 vector;
			Shader.GetGlobalVectorImpl_Injected(name, out vector);
			return vector;
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x0001E194 File Offset: 0x0001C394
		[FreeFunction("ShaderScripting::GetGlobalMatrix")]
		private static Matrix4x4 GetGlobalMatrixImpl(int name)
		{
			Matrix4x4 matrix4x;
			Shader.GetGlobalMatrixImpl_Injected(name, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x0001E1AC File Offset: 0x0001C3AC
		[FreeFunction("ShaderScripting::GetGlobalTexture")]
		private static Texture GetGlobalTextureImpl(int name)
		{
			return Unmarshal.UnmarshalUnityObject<Texture>(Shader.GetGlobalTextureImpl_Injected(name));
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x0001E1C4 File Offset: 0x0001C3C4
		[FreeFunction("ShaderScripting::SetGlobalFloatArray")]
		private unsafe static void SetGlobalFloatArrayImpl(int name, float[] values, int count)
		{
			Span<float> span = new Span<float>(values);
			fixed (float* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Shader.SetGlobalFloatArrayImpl_Injected(name, ref managedSpanWrapper, count);
			}
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x0001E200 File Offset: 0x0001C400
		[FreeFunction("ShaderScripting::SetGlobalVectorArray")]
		private unsafe static void SetGlobalVectorArrayImpl(int name, Vector4[] values, int count)
		{
			Span<Vector4> span = new Span<Vector4>(values);
			fixed (Vector4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Shader.SetGlobalVectorArrayImpl_Injected(name, ref managedSpanWrapper, count);
			}
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x0001E23C File Offset: 0x0001C43C
		[FreeFunction("ShaderScripting::SetGlobalMatrixArray")]
		private unsafe static void SetGlobalMatrixArrayImpl(int name, Matrix4x4[] values, int count)
		{
			Span<Matrix4x4> span = new Span<Matrix4x4>(values);
			fixed (Matrix4x4* pinnableReference = span.GetPinnableReference())
			{
				ManagedSpanWrapper managedSpanWrapper = new ManagedSpanWrapper((void*)pinnableReference, span.Length);
				Shader.SetGlobalMatrixArrayImpl_Injected(name, ref managedSpanWrapper, count);
			}
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x0001E278 File Offset: 0x0001C478
		[FreeFunction("ShaderScripting::GetGlobalFloatArray")]
		private static float[] GetGlobalFloatArrayImpl(int name)
		{
			float[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Shader.GetGlobalFloatArrayImpl_Injected(name, out blittableArrayWrapper);
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

		// Token: 0x06001166 RID: 4454 RVA: 0x0001E2AC File Offset: 0x0001C4AC
		[FreeFunction("ShaderScripting::GetGlobalVectorArray")]
		private static Vector4[] GetGlobalVectorArrayImpl(int name)
		{
			Vector4[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Shader.GetGlobalVectorArrayImpl_Injected(name, out blittableArrayWrapper);
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

		// Token: 0x06001167 RID: 4455 RVA: 0x0001E2E0 File Offset: 0x0001C4E0
		[FreeFunction("ShaderScripting::GetGlobalMatrixArray")]
		private static Matrix4x4[] GetGlobalMatrixArrayImpl(int name)
		{
			Matrix4x4[] array2;
			try
			{
				BlittableArrayWrapper blittableArrayWrapper;
				Shader.GetGlobalMatrixArrayImpl_Injected(name, out blittableArrayWrapper);
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

		// Token: 0x06001168 RID: 4456
		[FreeFunction("ShaderScripting::GetGlobalFloatArrayCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGlobalFloatArrayCountImpl(int name);

		// Token: 0x06001169 RID: 4457
		[FreeFunction("ShaderScripting::GetGlobalVectorArrayCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGlobalVectorArrayCountImpl(int name);

		// Token: 0x0600116A RID: 4458
		[FreeFunction("ShaderScripting::GetGlobalMatrixArrayCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetGlobalMatrixArrayCountImpl(int name);

		// Token: 0x0600116B RID: 4459 RVA: 0x0001E314 File Offset: 0x0001C514
		[FreeFunction("ShaderScripting::ExtractGlobalFloatArray")]
		private unsafe static void ExtractGlobalFloatArrayImpl(int name, [Out] float[] val)
		{
			try
			{
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
				Shader.ExtractGlobalFloatArrayImpl_Injected(name, out blittableArrayWrapper);
			}
			finally
			{
				float[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<float>(ref array);
			}
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x0001E364 File Offset: 0x0001C564
		[FreeFunction("ShaderScripting::ExtractGlobalVectorArray")]
		private unsafe static void ExtractGlobalVectorArrayImpl(int name, [Out] Vector4[] val)
		{
			try
			{
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
				Shader.ExtractGlobalVectorArrayImpl_Injected(name, out blittableArrayWrapper);
			}
			finally
			{
				Vector4[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<Vector4>(ref array);
			}
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x0001E3B4 File Offset: 0x0001C5B4
		[FreeFunction("ShaderScripting::ExtractGlobalMatrixArray")]
		private unsafe static void ExtractGlobalMatrixArrayImpl(int name, [Out] Matrix4x4[] val)
		{
			try
			{
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
				Shader.ExtractGlobalMatrixArrayImpl_Injected(name, out blittableArrayWrapper);
			}
			finally
			{
				Matrix4x4[] array;
				BlittableArrayWrapper blittableArrayWrapper;
				blittableArrayWrapper.Unmarshal<Matrix4x4>(ref array);
			}
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x0001E404 File Offset: 0x0001C604
		private static void SetGlobalFloatArray(int name, float[] values, int count)
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
			Shader.SetGlobalFloatArrayImpl(name, values, count);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x0001E458 File Offset: 0x0001C658
		private static void SetGlobalVectorArray(int name, Vector4[] values, int count)
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
			Shader.SetGlobalVectorArrayImpl(name, values, count);
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x0001E4AC File Offset: 0x0001C6AC
		private static void SetGlobalMatrixArray(int name, Matrix4x4[] values, int count)
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
			Shader.SetGlobalMatrixArrayImpl(name, values, count);
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x0001E500 File Offset: 0x0001C700
		private static void ExtractGlobalFloatArray(int name, List<float> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int globalFloatArrayCountImpl = Shader.GetGlobalFloatArrayCountImpl(name);
			bool flag2 = globalFloatArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<float>(values, globalFloatArrayCountImpl);
				Shader.ExtractGlobalFloatArrayImpl(name, NoAllocHelpers.ExtractArrayFromList<float>(values));
			}
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x0001E550 File Offset: 0x0001C750
		private static void ExtractGlobalVectorArray(int name, List<Vector4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int globalVectorArrayCountImpl = Shader.GetGlobalVectorArrayCountImpl(name);
			bool flag2 = globalVectorArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Vector4>(values, globalVectorArrayCountImpl);
				Shader.ExtractGlobalVectorArrayImpl(name, NoAllocHelpers.ExtractArrayFromList<Vector4>(values));
			}
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x0001E5A0 File Offset: 0x0001C7A0
		private static void ExtractGlobalMatrixArray(int name, List<Matrix4x4> values)
		{
			bool flag = values == null;
			if (flag)
			{
				throw new ArgumentNullException("values");
			}
			values.Clear();
			int globalMatrixArrayCountImpl = Shader.GetGlobalMatrixArrayCountImpl(name);
			bool flag2 = globalMatrixArrayCountImpl > 0;
			if (flag2)
			{
				NoAllocHelpers.EnsureListElemCount<Matrix4x4>(values, globalMatrixArrayCountImpl);
				Shader.ExtractGlobalMatrixArrayImpl(name, NoAllocHelpers.ExtractArrayFromList<Matrix4x4>(values));
			}
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x0001E5EE File Offset: 0x0001C7EE
		public static void SetGlobalInt(string name, int value)
		{
			Shader.SetGlobalFloatImpl(Shader.PropertyToID(name), (float)value);
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x0001E5FF File Offset: 0x0001C7FF
		public static void SetGlobalInt(int nameID, int value)
		{
			Shader.SetGlobalFloatImpl(nameID, (float)value);
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x0001E60B File Offset: 0x0001C80B
		public static void SetGlobalFloat(string name, float value)
		{
			Shader.SetGlobalFloatImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x0001E61B File Offset: 0x0001C81B
		public static void SetGlobalFloat(int nameID, float value)
		{
			Shader.SetGlobalFloatImpl(nameID, value);
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x0001E626 File Offset: 0x0001C826
		public static void SetGlobalInteger(string name, int value)
		{
			Shader.SetGlobalIntImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x0001E636 File Offset: 0x0001C836
		public static void SetGlobalInteger(int nameID, int value)
		{
			Shader.SetGlobalIntImpl(nameID, value);
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x0001E641 File Offset: 0x0001C841
		public static void SetGlobalVector(string name, Vector4 value)
		{
			Shader.SetGlobalVectorImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x0001E651 File Offset: 0x0001C851
		public static void SetGlobalVector(int nameID, Vector4 value)
		{
			Shader.SetGlobalVectorImpl(nameID, value);
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x0001E65C File Offset: 0x0001C85C
		public static void SetGlobalColor(string name, Color value)
		{
			Shader.SetGlobalVectorImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x0001E671 File Offset: 0x0001C871
		public static void SetGlobalColor(int nameID, Color value)
		{
			Shader.SetGlobalVectorImpl(nameID, value);
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x0001E681 File Offset: 0x0001C881
		public static void SetGlobalMatrix(string name, Matrix4x4 value)
		{
			Shader.SetGlobalMatrixImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x0001E691 File Offset: 0x0001C891
		public static void SetGlobalMatrix(int nameID, Matrix4x4 value)
		{
			Shader.SetGlobalMatrixImpl(nameID, value);
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x0001E69C File Offset: 0x0001C89C
		public static void SetGlobalTexture(string name, Texture value)
		{
			Shader.SetGlobalTextureImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x0001E6AC File Offset: 0x0001C8AC
		public static void SetGlobalTexture(int nameID, Texture value)
		{
			Shader.SetGlobalTextureImpl(nameID, value);
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0001E6B7 File Offset: 0x0001C8B7
		public static void SetGlobalTexture(string name, RenderTexture value, RenderTextureSubElement element)
		{
			Shader.SetGlobalRenderTextureImpl(Shader.PropertyToID(name), value, element);
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x0001E6C8 File Offset: 0x0001C8C8
		public static void SetGlobalTexture(int nameID, RenderTexture value, RenderTextureSubElement element)
		{
			Shader.SetGlobalRenderTextureImpl(nameID, value, element);
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0001E6D4 File Offset: 0x0001C8D4
		public static void SetGlobalBuffer(string name, ComputeBuffer value)
		{
			Shader.SetGlobalBufferImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x0001E6E4 File Offset: 0x0001C8E4
		public static void SetGlobalBuffer(int nameID, ComputeBuffer value)
		{
			Shader.SetGlobalBufferImpl(nameID, value);
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x0001E6EF File Offset: 0x0001C8EF
		public static void SetGlobalBuffer(string name, GraphicsBuffer value)
		{
			Shader.SetGlobalGraphicsBufferImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x0001E6FF File Offset: 0x0001C8FF
		public static void SetGlobalBuffer(int nameID, GraphicsBuffer value)
		{
			Shader.SetGlobalGraphicsBufferImpl(nameID, value);
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x0001E70A File Offset: 0x0001C90A
		public static void SetGlobalConstantBuffer(string name, ComputeBuffer value, int offset, int size)
		{
			Shader.SetGlobalConstantBufferImpl(Shader.PropertyToID(name), value, offset, size);
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x0001E71C File Offset: 0x0001C91C
		public static void SetGlobalConstantBuffer(int nameID, ComputeBuffer value, int offset, int size)
		{
			Shader.SetGlobalConstantBufferImpl(nameID, value, offset, size);
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x0001E729 File Offset: 0x0001C929
		public static void SetGlobalConstantBuffer(string name, GraphicsBuffer value, int offset, int size)
		{
			Shader.SetGlobalConstantGraphicsBufferImpl(Shader.PropertyToID(name), value, offset, size);
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x0001E73B File Offset: 0x0001C93B
		public static void SetGlobalConstantBuffer(int nameID, GraphicsBuffer value, int offset, int size)
		{
			Shader.SetGlobalConstantGraphicsBufferImpl(nameID, value, offset, size);
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x0001E748 File Offset: 0x0001C948
		public static void SetGlobalRayTracingAccelerationStructure(string name, RayTracingAccelerationStructure value)
		{
			Shader.SetGlobalRayTracingAccelerationStructureImpl(Shader.PropertyToID(name), value);
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x0001E758 File Offset: 0x0001C958
		public static void SetGlobalRayTracingAccelerationStructure(int nameID, RayTracingAccelerationStructure value)
		{
			Shader.SetGlobalRayTracingAccelerationStructureImpl(nameID, value);
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x0001E763 File Offset: 0x0001C963
		public static void SetGlobalFloatArray(string name, List<float> values)
		{
			Shader.SetGlobalFloatArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromList<float>(values), values.Count);
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x0001E77E File Offset: 0x0001C97E
		public static void SetGlobalFloatArray(int nameID, List<float> values)
		{
			Shader.SetGlobalFloatArray(nameID, NoAllocHelpers.ExtractArrayFromList<float>(values), values.Count);
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x0001E794 File Offset: 0x0001C994
		public static void SetGlobalFloatArray(string name, float[] values)
		{
			Shader.SetGlobalFloatArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x0001E7A7 File Offset: 0x0001C9A7
		public static void SetGlobalFloatArray(int nameID, float[] values)
		{
			Shader.SetGlobalFloatArray(nameID, values, values.Length);
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x0001E7B5 File Offset: 0x0001C9B5
		public static void SetGlobalVectorArray(string name, List<Vector4> values)
		{
			Shader.SetGlobalVectorArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromList<Vector4>(values), values.Count);
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x0001E7D0 File Offset: 0x0001C9D0
		public static void SetGlobalVectorArray(int nameID, List<Vector4> values)
		{
			Shader.SetGlobalVectorArray(nameID, NoAllocHelpers.ExtractArrayFromList<Vector4>(values), values.Count);
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x0001E7E6 File Offset: 0x0001C9E6
		public static void SetGlobalVectorArray(string name, Vector4[] values)
		{
			Shader.SetGlobalVectorArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x0001E7F9 File Offset: 0x0001C9F9
		public static void SetGlobalVectorArray(int nameID, Vector4[] values)
		{
			Shader.SetGlobalVectorArray(nameID, values, values.Length);
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0001E807 File Offset: 0x0001CA07
		public static void SetGlobalMatrixArray(string name, List<Matrix4x4> values)
		{
			Shader.SetGlobalMatrixArray(Shader.PropertyToID(name), NoAllocHelpers.ExtractArrayFromList<Matrix4x4>(values), values.Count);
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x0001E822 File Offset: 0x0001CA22
		public static void SetGlobalMatrixArray(int nameID, List<Matrix4x4> values)
		{
			Shader.SetGlobalMatrixArray(nameID, NoAllocHelpers.ExtractArrayFromList<Matrix4x4>(values), values.Count);
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x0001E838 File Offset: 0x0001CA38
		public static void SetGlobalMatrixArray(string name, Matrix4x4[] values)
		{
			Shader.SetGlobalMatrixArray(Shader.PropertyToID(name), values, values.Length);
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x0001E84B File Offset: 0x0001CA4B
		public static void SetGlobalMatrixArray(int nameID, Matrix4x4[] values)
		{
			Shader.SetGlobalMatrixArray(nameID, values, values.Length);
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x0001E85C File Offset: 0x0001CA5C
		public static int GetGlobalInt(string name)
		{
			return (int)Shader.GetGlobalFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x0001E87C File Offset: 0x0001CA7C
		public static int GetGlobalInt(int nameID)
		{
			return (int)Shader.GetGlobalFloatImpl(nameID);
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x0001E898 File Offset: 0x0001CA98
		public static float GetGlobalFloat(string name)
		{
			return Shader.GetGlobalFloatImpl(Shader.PropertyToID(name));
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x0001E8B8 File Offset: 0x0001CAB8
		public static float GetGlobalFloat(int nameID)
		{
			return Shader.GetGlobalFloatImpl(nameID);
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x0001E8D0 File Offset: 0x0001CAD0
		public static int GetGlobalInteger(string name)
		{
			return Shader.GetGlobalIntImpl(Shader.PropertyToID(name));
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x0001E8F0 File Offset: 0x0001CAF0
		public static int GetGlobalInteger(int nameID)
		{
			return Shader.GetGlobalIntImpl(nameID);
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x0001E908 File Offset: 0x0001CB08
		public static Vector4 GetGlobalVector(string name)
		{
			return Shader.GetGlobalVectorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x0001E928 File Offset: 0x0001CB28
		public static Vector4 GetGlobalVector(int nameID)
		{
			return Shader.GetGlobalVectorImpl(nameID);
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x0001E940 File Offset: 0x0001CB40
		public static Color GetGlobalColor(string name)
		{
			return Shader.GetGlobalVectorImpl(Shader.PropertyToID(name));
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x0001E964 File Offset: 0x0001CB64
		public static Color GetGlobalColor(int nameID)
		{
			return Shader.GetGlobalVectorImpl(nameID);
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x0001E984 File Offset: 0x0001CB84
		public static Matrix4x4 GetGlobalMatrix(string name)
		{
			return Shader.GetGlobalMatrixImpl(Shader.PropertyToID(name));
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0001E9A4 File Offset: 0x0001CBA4
		public static Matrix4x4 GetGlobalMatrix(int nameID)
		{
			return Shader.GetGlobalMatrixImpl(nameID);
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x0001E9BC File Offset: 0x0001CBBC
		public static Texture GetGlobalTexture(string name)
		{
			return Shader.GetGlobalTextureImpl(Shader.PropertyToID(name));
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x0001E9DC File Offset: 0x0001CBDC
		public static Texture GetGlobalTexture(int nameID)
		{
			return Shader.GetGlobalTextureImpl(nameID);
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x0001E9F4 File Offset: 0x0001CBF4
		public static float[] GetGlobalFloatArray(string name)
		{
			return Shader.GetGlobalFloatArray(Shader.PropertyToID(name));
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x0001EA14 File Offset: 0x0001CC14
		public static float[] GetGlobalFloatArray(int nameID)
		{
			return (Shader.GetGlobalFloatArrayCountImpl(nameID) != 0) ? Shader.GetGlobalFloatArrayImpl(nameID) : null;
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x0001EA38 File Offset: 0x0001CC38
		public static Vector4[] GetGlobalVectorArray(string name)
		{
			return Shader.GetGlobalVectorArray(Shader.PropertyToID(name));
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x0001EA58 File Offset: 0x0001CC58
		public static Vector4[] GetGlobalVectorArray(int nameID)
		{
			return (Shader.GetGlobalVectorArrayCountImpl(nameID) != 0) ? Shader.GetGlobalVectorArrayImpl(nameID) : null;
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x0001EA7C File Offset: 0x0001CC7C
		public static Matrix4x4[] GetGlobalMatrixArray(string name)
		{
			return Shader.GetGlobalMatrixArray(Shader.PropertyToID(name));
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x0001EA9C File Offset: 0x0001CC9C
		public static Matrix4x4[] GetGlobalMatrixArray(int nameID)
		{
			return (Shader.GetGlobalMatrixArrayCountImpl(nameID) != 0) ? Shader.GetGlobalMatrixArrayImpl(nameID) : null;
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x0001EABF File Offset: 0x0001CCBF
		public static void GetGlobalFloatArray(string name, List<float> values)
		{
			Shader.ExtractGlobalFloatArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x0001EACF File Offset: 0x0001CCCF
		public static void GetGlobalFloatArray(int nameID, List<float> values)
		{
			Shader.ExtractGlobalFloatArray(nameID, values);
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x0001EADA File Offset: 0x0001CCDA
		public static void GetGlobalVectorArray(string name, List<Vector4> values)
		{
			Shader.ExtractGlobalVectorArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x0001EAEA File Offset: 0x0001CCEA
		public static void GetGlobalVectorArray(int nameID, List<Vector4> values)
		{
			Shader.ExtractGlobalVectorArray(nameID, values);
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x0001EAF5 File Offset: 0x0001CCF5
		public static void GetGlobalMatrixArray(string name, List<Matrix4x4> values)
		{
			Shader.ExtractGlobalMatrixArray(Shader.PropertyToID(name), values);
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x0001EB05 File Offset: 0x0001CD05
		public static void GetGlobalMatrixArray(int nameID, List<Matrix4x4> values)
		{
			Shader.ExtractGlobalMatrixArray(nameID, values);
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x00008C6F File Offset: 0x00006E6F
		private Shader()
		{
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x0001EB10 File Offset: 0x0001CD10
		[FreeFunction("ShaderScripting::GetPropertyName")]
		private static string GetPropertyName([NotNull] Shader shader, int propertyIndex)
		{
			if (shader == null)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(shader);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(shader, "shader");
				}
				ManagedSpanWrapper managedSpanWrapper;
				Shader.GetPropertyName_Injected(intPtr, propertyIndex, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x0001EB64 File Offset: 0x0001CD64
		[FreeFunction("ShaderScripting::GetPropertyNameId")]
		private static int GetPropertyNameId([NotNull] Shader shader, int propertyIndex)
		{
			if (shader == null)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(shader);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			return Shader.GetPropertyNameId_Injected(intPtr, propertyIndex);
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x0001EB9C File Offset: 0x0001CD9C
		[FreeFunction("ShaderScripting::GetPropertyType")]
		private static ShaderPropertyType GetPropertyType([NotNull] Shader shader, int propertyIndex)
		{
			if (shader == null)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(shader);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			return Shader.GetPropertyType_Injected(intPtr, propertyIndex);
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0001EBD4 File Offset: 0x0001CDD4
		[FreeFunction("ShaderScripting::GetPropertyDescription")]
		private static string GetPropertyDescription([NotNull] Shader shader, int propertyIndex)
		{
			if (shader == null)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(shader);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(shader, "shader");
				}
				ManagedSpanWrapper managedSpanWrapper;
				Shader.GetPropertyDescription_Injected(intPtr, propertyIndex, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0001EC28 File Offset: 0x0001CE28
		[FreeFunction("ShaderScripting::GetPropertyFlags")]
		private static ShaderPropertyFlags GetPropertyFlags([NotNull] Shader shader, int propertyIndex)
		{
			if (shader == null)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(shader);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			return Shader.GetPropertyFlags_Injected(intPtr, propertyIndex);
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0001EC60 File Offset: 0x0001CE60
		[FreeFunction("ShaderScripting::GetPropertyAttributes")]
		private static string[] GetPropertyAttributes([NotNull] Shader shader, int propertyIndex)
		{
			if (shader == null)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(shader);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			return Shader.GetPropertyAttributes_Injected(intPtr, propertyIndex);
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x0001EC98 File Offset: 0x0001CE98
		[FreeFunction("ShaderScripting::GetPropertyDefaultIntValue")]
		private static int GetPropertyDefaultIntValue([NotNull] Shader shader, int propertyIndex)
		{
			if (shader == null)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(shader);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			return Shader.GetPropertyDefaultIntValue_Injected(intPtr, propertyIndex);
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x0001ECD0 File Offset: 0x0001CED0
		[FreeFunction("ShaderScripting::GetPropertyDefaultValue")]
		private static Vector4 GetPropertyDefaultValue([NotNull] Shader shader, int propertyIndex)
		{
			if (shader == null)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(shader);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			Vector4 vector;
			Shader.GetPropertyDefaultValue_Injected(intPtr, propertyIndex, out vector);
			return vector;
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x0001ED0C File Offset: 0x0001CF0C
		[FreeFunction("ShaderScripting::GetPropertyTextureDimension")]
		private static TextureDimension GetPropertyTextureDimension([NotNull] Shader shader, int propertyIndex)
		{
			if (shader == null)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(shader);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			return Shader.GetPropertyTextureDimension_Injected(intPtr, propertyIndex);
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x0001ED44 File Offset: 0x0001CF44
		[FreeFunction("ShaderScripting::GetPropertyTextureDefaultName")]
		private static string GetPropertyTextureDefaultName([NotNull] Shader shader, int propertyIndex)
		{
			if (shader == null)
			{
				ThrowHelper.ThrowArgumentNullException(shader, "shader");
			}
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(shader);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(shader, "shader");
				}
				ManagedSpanWrapper managedSpanWrapper;
				Shader.GetPropertyTextureDefaultName_Injected(intPtr, propertyIndex, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x0001ED98 File Offset: 0x0001CF98
		[FreeFunction("ShaderScripting::FindTextureStack")]
		private static bool FindTextureStackImpl([NotNull] Shader s, int propertyIdx, out string stackName, out int layerIndex)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(s, "s");
			}
			bool flag;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(s);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowArgumentNullException(s, "s");
				}
				ManagedSpanWrapper managedSpanWrapper;
				flag = Shader.FindTextureStackImpl_Injected(intPtr, propertyIdx, out managedSpanWrapper, out layerIndex);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stackName = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return flag;
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x0001EDF0 File Offset: 0x0001CFF0
		private static void CheckPropertyIndex(Shader s, int propertyIndex)
		{
			bool flag = propertyIndex < 0 || propertyIndex >= s.GetPropertyCount();
			if (flag)
			{
				throw new ArgumentOutOfRangeException("propertyIndex");
			}
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x0001EE20 File Offset: 0x0001D020
		public int GetPropertyCount()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Shader.GetPropertyCount_Injected(intPtr);
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x0001EE44 File Offset: 0x0001D044
		public unsafe int FindPropertyIndex(string propertyName)
		{
			int num;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Shader>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(propertyName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = propertyName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				num = Shader.FindPropertyIndex_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x0001EEAC File Offset: 0x0001D0AC
		public string GetPropertyName(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyName(this, propertyIndex);
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x0001EED0 File Offset: 0x0001D0D0
		public int GetPropertyNameId(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyNameId(this, propertyIndex);
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x0001EEF4 File Offset: 0x0001D0F4
		public ShaderPropertyType GetPropertyType(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyType(this, propertyIndex);
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x0001EF18 File Offset: 0x0001D118
		public string GetPropertyDescription(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyDescription(this, propertyIndex);
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x0001EF3C File Offset: 0x0001D13C
		public ShaderPropertyFlags GetPropertyFlags(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyFlags(this, propertyIndex);
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x0001EF60 File Offset: 0x0001D160
		public string[] GetPropertyAttributes(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			return Shader.GetPropertyAttributes(this, propertyIndex);
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x0001EF84 File Offset: 0x0001D184
		public float GetPropertyDefaultFloatValue(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			ShaderPropertyType propertyType = this.GetPropertyType(propertyIndex);
			bool flag = propertyType != ShaderPropertyType.Float && propertyType != ShaderPropertyType.Range;
			if (flag)
			{
				throw new ArgumentException("Property type is not Float or Range.");
			}
			return Shader.GetPropertyDefaultValue(this, propertyIndex)[0];
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x0001EFD4 File Offset: 0x0001D1D4
		public Vector4 GetPropertyDefaultVectorValue(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			ShaderPropertyType propertyType = this.GetPropertyType(propertyIndex);
			bool flag = propertyType != ShaderPropertyType.Color && propertyType != ShaderPropertyType.Vector;
			if (flag)
			{
				throw new ArgumentException("Property type is not Color or Vector.");
			}
			return Shader.GetPropertyDefaultValue(this, propertyIndex);
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x0001F01C File Offset: 0x0001D21C
		public Vector2 GetPropertyRangeLimits(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			bool flag = this.GetPropertyType(propertyIndex) != ShaderPropertyType.Range;
			if (flag)
			{
				throw new ArgumentException("Property type is not Range.");
			}
			Vector4 propertyDefaultValue = Shader.GetPropertyDefaultValue(this, propertyIndex);
			return new Vector2(propertyDefaultValue[1], propertyDefaultValue[2]);
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0001F070 File Offset: 0x0001D270
		public int GetPropertyDefaultIntValue(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			bool flag = this.GetPropertyType(propertyIndex) != ShaderPropertyType.Int;
			if (flag)
			{
				throw new ArgumentException("Property type is not Int.");
			}
			return Shader.GetPropertyDefaultIntValue(this, propertyIndex);
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0001F0B0 File Offset: 0x0001D2B0
		public TextureDimension GetPropertyTextureDimension(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			bool flag = this.GetPropertyType(propertyIndex) != ShaderPropertyType.Texture;
			if (flag)
			{
				throw new ArgumentException("Property type is not TexEnv.");
			}
			return Shader.GetPropertyTextureDimension(this, propertyIndex);
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0001F0F0 File Offset: 0x0001D2F0
		public string GetPropertyTextureDefaultName(int propertyIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			ShaderPropertyType propertyType = this.GetPropertyType(propertyIndex);
			bool flag = propertyType != ShaderPropertyType.Texture;
			if (flag)
			{
				throw new ArgumentException("Property type is not Texture.");
			}
			return Shader.GetPropertyTextureDefaultName(this, propertyIndex);
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0001F130 File Offset: 0x0001D330
		public bool FindTextureStack(int propertyIndex, out string stackName, out int layerIndex)
		{
			Shader.CheckPropertyIndex(this, propertyIndex);
			ShaderPropertyType propertyType = this.GetPropertyType(propertyIndex);
			bool flag = propertyType != ShaderPropertyType.Texture;
			if (flag)
			{
				throw new ArgumentException("Property type is not Texture.");
			}
			return Shader.FindTextureStackImpl(this, propertyIndex, out stackName, out layerIndex);
		}

		// Token: 0x060011D0 RID: 4560
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr FindBuiltin_Injected(ref ManagedSpanWrapper name);

		// Token: 0x060011D1 RID: 4561
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_maximumLOD_Injected(IntPtr _unity_self);

		// Token: 0x060011D2 RID: 4562
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_maximumLOD_Injected(IntPtr _unity_self, int value);

		// Token: 0x060011D3 RID: 4563
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isSupported_Injected(IntPtr _unity_self);

		// Token: 0x060011D4 RID: 4564
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_globalRenderPipeline_Injected(out ManagedSpanWrapper ret);

		// Token: 0x060011D5 RID: 4565
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_globalRenderPipeline_Injected(ref ManagedSpanWrapper value);

		// Token: 0x060011D6 RID: 4566
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_keywordSpace_Injected(IntPtr _unity_self, out LocalKeywordSpace ret);

		// Token: 0x060011D7 RID: 4567
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableKeyword_Injected(ref ManagedSpanWrapper keyword);

		// Token: 0x060011D8 RID: 4568
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableKeyword_Injected(ref ManagedSpanWrapper keyword);

		// Token: 0x060011D9 RID: 4569
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsKeywordEnabled_Injected(ref ManagedSpanWrapper keyword);

		// Token: 0x060011DA RID: 4570
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableKeywordFast_Injected([In] ref GlobalKeyword keyword);

		// Token: 0x060011DB RID: 4571
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableKeywordFast_Injected([In] ref GlobalKeyword keyword);

		// Token: 0x060011DC RID: 4572
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetKeywordFast_Injected([In] ref GlobalKeyword keyword, bool value);

		// Token: 0x060011DD RID: 4573
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsKeywordEnabledFast_Injected([In] ref GlobalKeyword keyword);

		// Token: 0x060011DE RID: 4574
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_renderQueue_Injected(IntPtr _unity_self);

		// Token: 0x060011DF RID: 4575
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern DisableBatchingType get_disableBatching_Injected(IntPtr _unity_self);

		// Token: 0x060011E0 RID: 4576
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int TagToID_Injected(ref ManagedSpanWrapper name);

		// Token: 0x060011E1 RID: 4577
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void IDToTag_Injected(int name, out ManagedSpanWrapper ret);

		// Token: 0x060011E2 RID: 4578
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int PropertyToID_Injected(ref ManagedSpanWrapper name);

		// Token: 0x060011E3 RID: 4579
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetDependency_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x060011E4 RID: 4580
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_passCount_Injected(IntPtr _unity_self);

		// Token: 0x060011E5 RID: 4581
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_subshaderCount_Injected(IntPtr _unity_self);

		// Token: 0x060011E6 RID: 4582
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPassCountInSubshader_Injected(IntPtr _unity_self, int subshaderIndex);

		// Token: 0x060011E7 RID: 4583
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_FindPassTagValue_Injected(IntPtr _unity_self, int passIndex, int tagName);

		// Token: 0x060011E8 RID: 4584
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_FindPassTagValueInSubShader_Injected(IntPtr _unity_self, int subShaderIndex, int passIndex, int tagName);

		// Token: 0x060011E9 RID: 4585
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_FindSubshaderTagValue_Injected(IntPtr _unity_self, int subShaderIndex, int tagName);

		// Token: 0x060011EA RID: 4586
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalVectorImpl_Injected(int name, [In] ref Vector4 value);

		// Token: 0x060011EB RID: 4587
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalMatrixImpl_Injected(int name, [In] ref Matrix4x4 value);

		// Token: 0x060011EC RID: 4588
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalTextureImpl_Injected(int name, IntPtr value);

		// Token: 0x060011ED RID: 4589
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalRenderTextureImpl_Injected(int name, IntPtr value, RenderTextureSubElement element);

		// Token: 0x060011EE RID: 4590
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalBufferImpl_Injected(int name, IntPtr value);

		// Token: 0x060011EF RID: 4591
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalGraphicsBufferImpl_Injected(int name, IntPtr value);

		// Token: 0x060011F0 RID: 4592
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalConstantBufferImpl_Injected(int name, IntPtr value, int offset, int size);

		// Token: 0x060011F1 RID: 4593
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalConstantGraphicsBufferImpl_Injected(int name, IntPtr value, int offset, int size);

		// Token: 0x060011F2 RID: 4594
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalRayTracingAccelerationStructureImpl_Injected(int name, IntPtr accelerationStructure);

		// Token: 0x060011F3 RID: 4595
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGlobalVectorImpl_Injected(int name, out Vector4 ret);

		// Token: 0x060011F4 RID: 4596
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGlobalMatrixImpl_Injected(int name, out Matrix4x4 ret);

		// Token: 0x060011F5 RID: 4597
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetGlobalTextureImpl_Injected(int name);

		// Token: 0x060011F6 RID: 4598
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalFloatArrayImpl_Injected(int name, ref ManagedSpanWrapper values, int count);

		// Token: 0x060011F7 RID: 4599
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalVectorArrayImpl_Injected(int name, ref ManagedSpanWrapper values, int count);

		// Token: 0x060011F8 RID: 4600
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGlobalMatrixArrayImpl_Injected(int name, ref ManagedSpanWrapper values, int count);

		// Token: 0x060011F9 RID: 4601
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGlobalFloatArrayImpl_Injected(int name, out BlittableArrayWrapper ret);

		// Token: 0x060011FA RID: 4602
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGlobalVectorArrayImpl_Injected(int name, out BlittableArrayWrapper ret);

		// Token: 0x060011FB RID: 4603
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGlobalMatrixArrayImpl_Injected(int name, out BlittableArrayWrapper ret);

		// Token: 0x060011FC RID: 4604
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExtractGlobalFloatArrayImpl_Injected(int name, out BlittableArrayWrapper val);

		// Token: 0x060011FD RID: 4605
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExtractGlobalVectorArrayImpl_Injected(int name, out BlittableArrayWrapper val);

		// Token: 0x060011FE RID: 4606
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ExtractGlobalMatrixArrayImpl_Injected(int name, out BlittableArrayWrapper val);

		// Token: 0x060011FF RID: 4607
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPropertyName_Injected(IntPtr shader, int propertyIndex, out ManagedSpanWrapper ret);

		// Token: 0x06001200 RID: 4608
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPropertyNameId_Injected(IntPtr shader, int propertyIndex);

		// Token: 0x06001201 RID: 4609
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ShaderPropertyType GetPropertyType_Injected(IntPtr shader, int propertyIndex);

		// Token: 0x06001202 RID: 4610
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPropertyDescription_Injected(IntPtr shader, int propertyIndex, out ManagedSpanWrapper ret);

		// Token: 0x06001203 RID: 4611
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ShaderPropertyFlags GetPropertyFlags_Injected(IntPtr shader, int propertyIndex);

		// Token: 0x06001204 RID: 4612
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetPropertyAttributes_Injected(IntPtr shader, int propertyIndex);

		// Token: 0x06001205 RID: 4613
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPropertyDefaultIntValue_Injected(IntPtr shader, int propertyIndex);

		// Token: 0x06001206 RID: 4614
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPropertyDefaultValue_Injected(IntPtr shader, int propertyIndex, out Vector4 ret);

		// Token: 0x06001207 RID: 4615
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern TextureDimension GetPropertyTextureDimension_Injected(IntPtr shader, int propertyIndex);

		// Token: 0x06001208 RID: 4616
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetPropertyTextureDefaultName_Injected(IntPtr shader, int propertyIndex, out ManagedSpanWrapper ret);

		// Token: 0x06001209 RID: 4617
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool FindTextureStackImpl_Injected(IntPtr s, int propertyIdx, out ManagedSpanWrapper stackName, out int layerIndex);

		// Token: 0x0600120A RID: 4618
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetPropertyCount_Injected(IntPtr _unity_self);

		// Token: 0x0600120B RID: 4619
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int FindPropertyIndex_Injected(IntPtr _unity_self, ref ManagedSpanWrapper propertyName);
	}
}
