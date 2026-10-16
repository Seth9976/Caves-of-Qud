using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x0200056F RID: 1391
	[NativeHeader("Runtime/Shaders/Keywords/KeywordSpaceScriptBindings.h")]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	[UsedByNativeCode]
	public struct ShaderKeyword
	{
		// Token: 0x06003638 RID: 13880
		[FreeFunction("ShaderScripting::GetGlobalKeywordCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint GetGlobalKeywordCount();

		// Token: 0x06003639 RID: 13881 RVA: 0x0006A4D4 File Offset: 0x000686D4
		[FreeFunction("ShaderScripting::GetGlobalKeywordIndex")]
		internal unsafe static uint GetGlobalKeywordIndex(string keyword)
		{
			uint globalKeywordIndex_Injected;
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
				globalKeywordIndex_Injected = ShaderKeyword.GetGlobalKeywordIndex_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return globalKeywordIndex_Injected;
		}

		// Token: 0x0600363A RID: 13882 RVA: 0x0006A52C File Offset: 0x0006872C
		[FreeFunction("ShaderScripting::GetKeywordCount")]
		internal static uint GetKeywordCount(Shader shader)
		{
			return ShaderKeyword.GetKeywordCount_Injected(Object.MarshalledUnityObject.Marshal<Shader>(shader));
		}

		// Token: 0x0600363B RID: 13883 RVA: 0x0006A544 File Offset: 0x00068744
		[FreeFunction("ShaderScripting::GetKeywordIndex")]
		internal unsafe static uint GetKeywordIndex(Shader shader, string keyword)
		{
			uint keywordIndex_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.Marshal<Shader>(shader);
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(keyword, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = keyword.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				keywordIndex_Injected = ShaderKeyword.GetKeywordIndex_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return keywordIndex_Injected;
		}

		// Token: 0x0600363C RID: 13884 RVA: 0x0006A5A0 File Offset: 0x000687A0
		[FreeFunction("ShaderScripting::GetKeywordCount")]
		internal static uint GetComputeShaderKeywordCount(ComputeShader shader)
		{
			return ShaderKeyword.GetComputeShaderKeywordCount_Injected(Object.MarshalledUnityObject.Marshal<ComputeShader>(shader));
		}

		// Token: 0x0600363D RID: 13885 RVA: 0x0006A5B8 File Offset: 0x000687B8
		[FreeFunction("ShaderScripting::GetKeywordIndex")]
		internal unsafe static uint GetComputeShaderKeywordIndex(ComputeShader shader, string keyword)
		{
			uint computeShaderKeywordIndex_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.Marshal<ComputeShader>(shader);
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(keyword, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = keyword.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				computeShaderKeywordIndex_Injected = ShaderKeyword.GetComputeShaderKeywordIndex_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return computeShaderKeywordIndex_Injected;
		}

		// Token: 0x0600363E RID: 13886 RVA: 0x0006A614 File Offset: 0x00068814
		[FreeFunction("ShaderScripting::CreateGlobalKeyword")]
		internal unsafe static void CreateGlobalKeyword(string keyword)
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
				ShaderKeyword.CreateGlobalKeyword_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600363F RID: 13887
		[FreeFunction("ShaderScripting::GetKeywordType")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ShaderKeywordType GetGlobalShaderKeywordType(uint keyword);

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06003640 RID: 13888 RVA: 0x0006A668 File Offset: 0x00068868
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x06003641 RID: 13889 RVA: 0x0006A680 File Offset: 0x00068880
		public static ShaderKeywordType GetGlobalKeywordType(ShaderKeyword index)
		{
			bool flag = index.IsValid();
			ShaderKeywordType shaderKeywordType;
			if (flag)
			{
				shaderKeywordType = ShaderKeyword.GetGlobalShaderKeywordType(index.m_Index);
			}
			else
			{
				shaderKeywordType = ShaderKeywordType.UserDefined;
			}
			return shaderKeywordType;
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x0006A6B0 File Offset: 0x000688B0
		public ShaderKeyword(string keywordName)
		{
			this.m_Name = keywordName;
			this.m_Index = ShaderKeyword.GetGlobalKeywordIndex(keywordName);
			bool flag = this.m_Index >= ShaderKeyword.GetGlobalKeywordCount();
			if (flag)
			{
				ShaderKeyword.CreateGlobalKeyword(keywordName);
				this.m_Index = ShaderKeyword.GetGlobalKeywordIndex(keywordName);
			}
			this.m_IsValid = true;
			this.m_IsLocal = false;
			this.m_IsCompute = false;
		}

		// Token: 0x06003643 RID: 13891 RVA: 0x0006A70F File Offset: 0x0006890F
		public ShaderKeyword(Shader shader, string keywordName)
		{
			this.m_Name = keywordName;
			this.m_Index = ShaderKeyword.GetKeywordIndex(shader, keywordName);
			this.m_IsValid = this.m_Index < ShaderKeyword.GetKeywordCount(shader);
			this.m_IsLocal = true;
			this.m_IsCompute = false;
		}

		// Token: 0x06003644 RID: 13892 RVA: 0x0006A748 File Offset: 0x00068948
		public ShaderKeyword(ComputeShader shader, string keywordName)
		{
			this.m_Name = keywordName;
			this.m_Index = ShaderKeyword.GetComputeShaderKeywordIndex(shader, keywordName);
			this.m_IsValid = this.m_Index < ShaderKeyword.GetComputeShaderKeywordCount(shader);
			this.m_IsLocal = true;
			this.m_IsCompute = true;
		}

		// Token: 0x06003645 RID: 13893 RVA: 0x0006A784 File Offset: 0x00068984
		public static bool IsKeywordLocal(ShaderKeyword keyword)
		{
			return keyword.m_IsLocal;
		}

		// Token: 0x06003646 RID: 13894 RVA: 0x0006A79C File Offset: 0x0006899C
		public bool IsValid()
		{
			return this.m_IsValid;
		}

		// Token: 0x06003647 RID: 13895 RVA: 0x0006A7B4 File Offset: 0x000689B4
		public bool IsValid(ComputeShader shader)
		{
			return this.m_IsValid;
		}

		// Token: 0x06003648 RID: 13896 RVA: 0x0006A7CC File Offset: 0x000689CC
		public bool IsValid(Shader shader)
		{
			return this.m_IsValid;
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06003649 RID: 13897 RVA: 0x0006A7E4 File Offset: 0x000689E4
		public int index
		{
			get
			{
				return (int)this.m_Index;
			}
		}

		// Token: 0x0600364A RID: 13898 RVA: 0x0006A7FC File Offset: 0x000689FC
		public override string ToString()
		{
			return this.m_Name;
		}

		// Token: 0x0600364B RID: 13899 RVA: 0x0006A814 File Offset: 0x00068A14
		[Obsolete("GetKeywordType is deprecated. Only global keywords can have a type. This method always returns ShaderKeywordType.UserDefined.")]
		public static ShaderKeywordType GetKeywordType(Shader shader, ShaderKeyword index)
		{
			return ShaderKeywordType.UserDefined;
		}

		// Token: 0x0600364C RID: 13900 RVA: 0x0006A828 File Offset: 0x00068A28
		[Obsolete("GetKeywordType is deprecated. Only global keywords can have a type. This method always returns ShaderKeywordType.UserDefined.")]
		public static ShaderKeywordType GetKeywordType(ComputeShader shader, ShaderKeyword index)
		{
			return ShaderKeywordType.UserDefined;
		}

		// Token: 0x0600364D RID: 13901 RVA: 0x0006A83C File Offset: 0x00068A3C
		[Obsolete("GetGlobalKeywordName is deprecated. Use the ShaderKeyword.name property instead.")]
		public static string GetGlobalKeywordName(ShaderKeyword index)
		{
			return index.m_Name;
		}

		// Token: 0x0600364E RID: 13902 RVA: 0x0006A854 File Offset: 0x00068A54
		[Obsolete("GetKeywordName is deprecated. Use the ShaderKeyword.name property instead.")]
		public static string GetKeywordName(Shader shader, ShaderKeyword index)
		{
			return index.m_Name;
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x0006A86C File Offset: 0x00068A6C
		[Obsolete("GetKeywordName is deprecated. Use the ShaderKeyword.name property instead.")]
		public static string GetKeywordName(ComputeShader shader, ShaderKeyword index)
		{
			return index.m_Name;
		}

		// Token: 0x06003650 RID: 13904 RVA: 0x0006A884 File Offset: 0x00068A84
		[Obsolete("GetKeywordType is deprecated. Use ShaderKeyword.name instead.")]
		public ShaderKeywordType GetKeywordType()
		{
			return ShaderKeyword.GetGlobalKeywordType(this);
		}

		// Token: 0x06003651 RID: 13905 RVA: 0x0006A8A4 File Offset: 0x00068AA4
		[Obsolete("GetKeywordName is deprecated. Use ShaderKeyword.name instead.")]
		public string GetKeywordName()
		{
			return ShaderKeyword.GetGlobalKeywordName(this);
		}

		// Token: 0x06003652 RID: 13906 RVA: 0x0006A8C4 File Offset: 0x00068AC4
		[Obsolete("GetName() has been deprecated. Use ShaderKeyword.name instead.")]
		public string GetName()
		{
			return this.GetKeywordName();
		}

		// Token: 0x06003653 RID: 13907
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetGlobalKeywordIndex_Injected(ref ManagedSpanWrapper keyword);

		// Token: 0x06003654 RID: 13908
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetKeywordCount_Injected(IntPtr shader);

		// Token: 0x06003655 RID: 13909
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetKeywordIndex_Injected(IntPtr shader, ref ManagedSpanWrapper keyword);

		// Token: 0x06003656 RID: 13910
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetComputeShaderKeywordCount_Injected(IntPtr shader);

		// Token: 0x06003657 RID: 13911
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetComputeShaderKeywordIndex_Injected(IntPtr shader, ref ManagedSpanWrapper keyword);

		// Token: 0x06003658 RID: 13912
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateGlobalKeyword_Injected(ref ManagedSpanWrapper keyword);

		// Token: 0x04001236 RID: 4662
		internal string m_Name;

		// Token: 0x04001237 RID: 4663
		internal uint m_Index;

		// Token: 0x04001238 RID: 4664
		internal bool m_IsLocal;

		// Token: 0x04001239 RID: 4665
		internal bool m_IsCompute;

		// Token: 0x0400123A RID: 4666
		internal bool m_IsValid;
	}
}
