using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Assertions;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000570 RID: 1392
	[NativeHeader("Editor/Src/Graphics/ShaderCompilerData.h")]
	[UsedByNativeCode]
	public struct ShaderKeywordSet
	{
		// Token: 0x06003659 RID: 13913 RVA: 0x0006A8DC File Offset: 0x00068ADC
		[FreeFunction("keywords::IsKeywordEnabled")]
		private static bool IsGlobalKeywordEnabled(ShaderKeywordSet state, uint index)
		{
			return ShaderKeywordSet.IsGlobalKeywordEnabled_Injected(ref state, index);
		}

		// Token: 0x0600365A RID: 13914 RVA: 0x0006A8F4 File Offset: 0x00068AF4
		[FreeFunction("keywords::IsKeywordEnabled")]
		private static bool IsKeywordEnabled(ShaderKeywordSet state, LocalKeywordSpace keywordSpace, uint index)
		{
			return ShaderKeywordSet.IsKeywordEnabled_Injected(ref state, ref keywordSpace, index);
		}

		// Token: 0x0600365B RID: 13915 RVA: 0x0006A90C File Offset: 0x00068B0C
		[FreeFunction("keywords::IsKeywordEnabled")]
		private unsafe static bool IsKeywordNameEnabled(ShaderKeywordSet state, string name)
		{
			bool flag;
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
				flag = ShaderKeywordSet.IsKeywordNameEnabled_Injected(ref state, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x0600365C RID: 13916 RVA: 0x0006A964 File Offset: 0x00068B64
		[FreeFunction("keywords::EnableKeyword")]
		private static void EnableGlobalKeyword(ShaderKeywordSet state, uint index)
		{
			ShaderKeywordSet.EnableGlobalKeyword_Injected(ref state, index);
		}

		// Token: 0x0600365D RID: 13917 RVA: 0x0006A97C File Offset: 0x00068B7C
		[FreeFunction("keywords::EnableKeyword")]
		private unsafe static void EnableKeywordName(ShaderKeywordSet state, string name)
		{
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
				ShaderKeywordSet.EnableKeywordName_Injected(ref state, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600365E RID: 13918 RVA: 0x0006A9D4 File Offset: 0x00068BD4
		[FreeFunction("keywords::DisableKeyword")]
		private static void DisableGlobalKeyword(ShaderKeywordSet state, uint index)
		{
			ShaderKeywordSet.DisableGlobalKeyword_Injected(ref state, index);
		}

		// Token: 0x0600365F RID: 13919 RVA: 0x0006A9EC File Offset: 0x00068BEC
		[FreeFunction("keywords::DisableKeyword")]
		private unsafe static void DisableKeywordName(ShaderKeywordSet state, string name)
		{
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
				ShaderKeywordSet.DisableKeywordName_Injected(ref state, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06003660 RID: 13920 RVA: 0x0006AA44 File Offset: 0x00068C44
		[FreeFunction("keywords::GetEnabledKeywords")]
		private static ShaderKeyword[] GetEnabledKeywords(ShaderKeywordSet state)
		{
			return ShaderKeywordSet.GetEnabledKeywords_Injected(ref state);
		}

		// Token: 0x06003661 RID: 13921 RVA: 0x0006AA58 File Offset: 0x00068C58
		private void CheckKeywordCompatible(ShaderKeyword keyword)
		{
			bool isLocal = keyword.m_IsLocal;
			if (isLocal)
			{
				bool flag = this.m_Shader != IntPtr.Zero;
				if (flag)
				{
					Assert.IsTrue(!keyword.m_IsCompute, "Trying to use a keyword that comes from a different shader.");
				}
				else
				{
					Assert.IsTrue(keyword.m_IsCompute, "Trying to use a keyword that comes from a different shader.");
				}
			}
		}

		// Token: 0x06003662 RID: 13922 RVA: 0x0006AAB0 File Offset: 0x00068CB0
		public bool IsEnabled(ShaderKeyword keyword)
		{
			this.CheckKeywordCompatible(keyword);
			return ShaderKeywordSet.IsKeywordNameEnabled(this, keyword.m_Name);
		}

		// Token: 0x06003663 RID: 13923 RVA: 0x0006AADC File Offset: 0x00068CDC
		public bool IsEnabled(GlobalKeyword keyword)
		{
			return ShaderKeywordSet.IsGlobalKeywordEnabled(this, keyword.m_Index);
		}

		// Token: 0x06003664 RID: 13924 RVA: 0x0006AB00 File Offset: 0x00068D00
		public bool IsEnabled(LocalKeyword keyword)
		{
			return ShaderKeywordSet.IsKeywordEnabled(this, keyword.m_SpaceInfo, keyword.m_Index);
		}

		// Token: 0x06003665 RID: 13925 RVA: 0x0006AB2C File Offset: 0x00068D2C
		public void Enable(ShaderKeyword keyword)
		{
			this.CheckKeywordCompatible(keyword);
			bool flag = keyword.m_IsLocal || !keyword.IsValid();
			if (flag)
			{
				ShaderKeywordSet.EnableKeywordName(this, keyword.m_Name);
			}
			else
			{
				ShaderKeywordSet.EnableGlobalKeyword(this, keyword.m_Index);
			}
		}

		// Token: 0x06003666 RID: 13926 RVA: 0x0006AB84 File Offset: 0x00068D84
		public void Disable(ShaderKeyword keyword)
		{
			bool flag = keyword.m_IsLocal || !keyword.IsValid();
			if (flag)
			{
				ShaderKeywordSet.DisableKeywordName(this, keyword.m_Name);
			}
			else
			{
				ShaderKeywordSet.DisableGlobalKeyword(this, keyword.m_Index);
			}
		}

		// Token: 0x06003667 RID: 13927 RVA: 0x0006ABD4 File Offset: 0x00068DD4
		public ShaderKeyword[] GetShaderKeywords()
		{
			return ShaderKeywordSet.GetEnabledKeywords(this);
		}

		// Token: 0x06003668 RID: 13928 RVA: 0x0006ABF4 File Offset: 0x00068DF4
		public override string ToString()
		{
			ShaderKeyword[] enabledKeywords = ShaderKeywordSet.GetEnabledKeywords(this);
			Array.Sort<ShaderKeyword>(enabledKeywords, new Comparison<ShaderKeyword>(ShaderKeywordSet.ShaderKeywordComparer));
			return string.Join<ShaderKeyword>(' ', enabledKeywords);
		}

		// Token: 0x06003669 RID: 13929 RVA: 0x0006AC30 File Offset: 0x00068E30
		private static int ShaderKeywordComparer(ShaderKeyword kw1, ShaderKeyword kw2)
		{
			return string.Compare(kw1.name, kw2.name);
		}

		// Token: 0x0600366A RID: 13930
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsGlobalKeywordEnabled_Injected([In] ref ShaderKeywordSet state, uint index);

		// Token: 0x0600366B RID: 13931
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsKeywordEnabled_Injected([In] ref ShaderKeywordSet state, [In] ref LocalKeywordSpace keywordSpace, uint index);

		// Token: 0x0600366C RID: 13932
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsKeywordNameEnabled_Injected([In] ref ShaderKeywordSet state, ref ManagedSpanWrapper name);

		// Token: 0x0600366D RID: 13933
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableGlobalKeyword_Injected([In] ref ShaderKeywordSet state, uint index);

		// Token: 0x0600366E RID: 13934
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EnableKeywordName_Injected([In] ref ShaderKeywordSet state, ref ManagedSpanWrapper name);

		// Token: 0x0600366F RID: 13935
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableGlobalKeyword_Injected([In] ref ShaderKeywordSet state, uint index);

		// Token: 0x06003670 RID: 13936
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DisableKeywordName_Injected([In] ref ShaderKeywordSet state, ref ManagedSpanWrapper name);

		// Token: 0x06003671 RID: 13937
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ShaderKeyword[] GetEnabledKeywords_Injected([In] ref ShaderKeywordSet state);

		// Token: 0x0400123B RID: 4667
		private IntPtr m_KeywordState;

		// Token: 0x0400123C RID: 4668
		private IntPtr m_Shader;

		// Token: 0x0400123D RID: 4669
		private IntPtr m_ComputeShader;

		// Token: 0x0400123E RID: 4670
		private ulong m_StateIndex;
	}
}
