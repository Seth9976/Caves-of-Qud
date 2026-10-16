using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000568 RID: 1384
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Shaders/Keywords/KeywordSpaceScriptBindings.h")]
	public readonly struct LocalKeyword : IEquatable<LocalKeyword>
	{
		// Token: 0x060035BE RID: 13758 RVA: 0x0006972C File Offset: 0x0006792C
		[FreeFunction("keywords::IsKeywordDynamic")]
		private static bool IsDynamic(LocalKeyword kw)
		{
			return LocalKeyword.IsDynamic_Injected(ref kw);
		}

		// Token: 0x060035BF RID: 13759 RVA: 0x00069740 File Offset: 0x00067940
		[FreeFunction("keywords::IsKeywordOverridable")]
		private static bool IsOverridable(LocalKeyword kw)
		{
			return LocalKeyword.IsOverridable_Injected(ref kw);
		}

		// Token: 0x060035C0 RID: 13760 RVA: 0x00069754 File Offset: 0x00067954
		[FreeFunction("ShaderScripting::GetKeywordCount")]
		private static uint GetShaderKeywordCount(Shader shader)
		{
			return LocalKeyword.GetShaderKeywordCount_Injected(Object.MarshalledUnityObject.Marshal<Shader>(shader));
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x0006976C File Offset: 0x0006796C
		[FreeFunction("ShaderScripting::GetKeywordIndex")]
		private unsafe static uint GetShaderKeywordIndex(Shader shader, string keyword)
		{
			uint shaderKeywordIndex_Injected;
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
				shaderKeywordIndex_Injected = LocalKeyword.GetShaderKeywordIndex_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return shaderKeywordIndex_Injected;
		}

		// Token: 0x060035C2 RID: 13762 RVA: 0x000697C8 File Offset: 0x000679C8
		[FreeFunction("ShaderScripting::GetKeywordCount")]
		private static uint GetComputeShaderKeywordCount(ComputeShader shader)
		{
			return LocalKeyword.GetComputeShaderKeywordCount_Injected(Object.MarshalledUnityObject.Marshal<ComputeShader>(shader));
		}

		// Token: 0x060035C3 RID: 13763 RVA: 0x000697E0 File Offset: 0x000679E0
		[FreeFunction("ShaderScripting::GetKeywordIndex")]
		private unsafe static uint GetComputeShaderKeywordIndex(ComputeShader shader, string keyword)
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
				computeShaderKeywordIndex_Injected = LocalKeyword.GetComputeShaderKeywordIndex_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return computeShaderKeywordIndex_Injected;
		}

		// Token: 0x060035C4 RID: 13764 RVA: 0x0006983C File Offset: 0x00067A3C
		[FreeFunction("keywords::GetKeywordType")]
		private static ShaderKeywordType GetKeywordType(LocalKeywordSpace spaceInfo, uint keyword)
		{
			return LocalKeyword.GetKeywordType_Injected(ref spaceInfo, keyword);
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x00069854 File Offset: 0x00067A54
		[FreeFunction("keywords::IsKeywordValid")]
		private static bool IsValid(LocalKeywordSpace spaceInfo, uint keyword)
		{
			return LocalKeyword.IsValid_Injected(ref spaceInfo, keyword);
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x060035C6 RID: 13766 RVA: 0x0006986C File Offset: 0x00067A6C
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x060035C7 RID: 13767 RVA: 0x00069884 File Offset: 0x00067A84
		public bool isDynamic
		{
			get
			{
				return LocalKeyword.IsDynamic(this);
			}
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x060035C8 RID: 13768 RVA: 0x000698A4 File Offset: 0x00067AA4
		public bool isOverridable
		{
			get
			{
				return LocalKeyword.IsOverridable(this);
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x060035C9 RID: 13769 RVA: 0x000698C4 File Offset: 0x00067AC4
		public bool isValid
		{
			get
			{
				return LocalKeyword.IsValid(this.m_SpaceInfo, this.m_Index);
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x060035CA RID: 13770 RVA: 0x000698E8 File Offset: 0x00067AE8
		public ShaderKeywordType type
		{
			get
			{
				return LocalKeyword.GetKeywordType(this.m_SpaceInfo, this.m_Index);
			}
		}

		// Token: 0x060035CB RID: 13771 RVA: 0x0006990C File Offset: 0x00067B0C
		public LocalKeyword(Shader shader, string name)
		{
			bool flag = shader == null;
			if (flag)
			{
				Debug.LogError("Cannot initialize a LocalKeyword with a null Shader.");
			}
			this.m_SpaceInfo = shader.keywordSpace;
			this.m_Name = name;
			this.m_Index = LocalKeyword.GetShaderKeywordIndex(shader, name);
			bool flag2 = this.m_Index >= LocalKeyword.GetShaderKeywordCount(shader);
			if (flag2)
			{
				Debug.LogErrorFormat("Local keyword {0} doesn't exist in the shader.", new object[] { name });
			}
		}

		// Token: 0x060035CC RID: 13772 RVA: 0x0006997C File Offset: 0x00067B7C
		public LocalKeyword(ComputeShader shader, string name)
		{
			bool flag = shader == null;
			if (flag)
			{
				Debug.LogError("Cannot initialize a LocalKeyword with a null ComputeShader.");
			}
			this.m_SpaceInfo = shader.keywordSpace;
			this.m_Name = name;
			this.m_Index = LocalKeyword.GetComputeShaderKeywordIndex(shader, name);
			bool flag2 = this.m_Index >= LocalKeyword.GetComputeShaderKeywordCount(shader);
			if (flag2)
			{
				Debug.LogErrorFormat("Local keyword {0} doesn't exist in the compute shader.", new object[] { name });
			}
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x000699EC File Offset: 0x00067BEC
		public override string ToString()
		{
			return this.m_Name;
		}

		// Token: 0x060035CE RID: 13774 RVA: 0x00069A04 File Offset: 0x00067C04
		public override bool Equals(object o)
		{
			bool flag;
			if (o is LocalKeyword)
			{
				LocalKeyword localKeyword = (LocalKeyword)o;
				flag = this.Equals(localKeyword);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x00069A30 File Offset: 0x00067C30
		public bool Equals(LocalKeyword rhs)
		{
			return this.m_SpaceInfo == rhs.m_SpaceInfo && this.m_Index == rhs.m_Index;
		}

		// Token: 0x060035D0 RID: 13776 RVA: 0x00069A68 File Offset: 0x00067C68
		public static bool operator ==(LocalKeyword lhs, LocalKeyword rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x060035D1 RID: 13777 RVA: 0x00069A84 File Offset: 0x00067C84
		public static bool operator !=(LocalKeyword lhs, LocalKeyword rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060035D2 RID: 13778 RVA: 0x00069AA0 File Offset: 0x00067CA0
		public override int GetHashCode()
		{
			return this.m_Index.GetHashCode() ^ this.m_SpaceInfo.GetHashCode();
		}

		// Token: 0x060035D3 RID: 13779
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsDynamic_Injected([In] ref LocalKeyword kw);

		// Token: 0x060035D4 RID: 13780
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsOverridable_Injected([In] ref LocalKeyword kw);

		// Token: 0x060035D5 RID: 13781
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetShaderKeywordCount_Injected(IntPtr shader);

		// Token: 0x060035D6 RID: 13782
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetShaderKeywordIndex_Injected(IntPtr shader, ref ManagedSpanWrapper keyword);

		// Token: 0x060035D7 RID: 13783
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetComputeShaderKeywordCount_Injected(IntPtr shader);

		// Token: 0x060035D8 RID: 13784
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetComputeShaderKeywordIndex_Injected(IntPtr shader, ref ManagedSpanWrapper keyword);

		// Token: 0x060035D9 RID: 13785
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ShaderKeywordType GetKeywordType_Injected([In] ref LocalKeywordSpace spaceInfo, uint keyword);

		// Token: 0x060035DA RID: 13786
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsValid_Injected([In] ref LocalKeywordSpace spaceInfo, uint keyword);

		// Token: 0x04001227 RID: 4647
		internal readonly LocalKeywordSpace m_SpaceInfo;

		// Token: 0x04001228 RID: 4648
		internal readonly string m_Name;

		// Token: 0x04001229 RID: 4649
		internal readonly uint m_Index;
	}
}
