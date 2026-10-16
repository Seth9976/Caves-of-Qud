using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering
{
	// Token: 0x02000567 RID: 1383
	[NativeHeader("Runtime/Shaders/Keywords/KeywordSpaceScriptBindings.h")]
	[NativeHeader("Runtime/Graphics/ShaderScriptBindings.h")]
	[UsedByNativeCode]
	public readonly struct GlobalKeyword
	{
		// Token: 0x060035B5 RID: 13749
		[FreeFunction("ShaderScripting::GetGlobalKeywordCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetGlobalKeywordCount();

		// Token: 0x060035B6 RID: 13750 RVA: 0x000695E4 File Offset: 0x000677E4
		[FreeFunction("ShaderScripting::GetGlobalKeywordIndex")]
		private unsafe static uint GetGlobalKeywordIndex(string keyword)
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
				globalKeywordIndex_Injected = GlobalKeyword.GetGlobalKeywordIndex_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return globalKeywordIndex_Injected;
		}

		// Token: 0x060035B7 RID: 13751 RVA: 0x0006963C File Offset: 0x0006783C
		[FreeFunction("ShaderScripting::CreateGlobalKeyword")]
		private unsafe static void CreateGlobalKeyword(string keyword)
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
				GlobalKeyword.CreateGlobalKeyword_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x060035B8 RID: 13752 RVA: 0x00069690 File Offset: 0x00067890
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x060035B9 RID: 13753 RVA: 0x000696A8 File Offset: 0x000678A8
		public static GlobalKeyword Create(string name)
		{
			GlobalKeyword.CreateGlobalKeyword(name);
			return new GlobalKeyword(name);
		}

		// Token: 0x060035BA RID: 13754 RVA: 0x000696C8 File Offset: 0x000678C8
		public GlobalKeyword(string name)
		{
			this.m_Name = name;
			this.m_Index = GlobalKeyword.GetGlobalKeywordIndex(name);
			bool flag = this.m_Index >= GlobalKeyword.GetGlobalKeywordCount();
			if (flag)
			{
				Debug.LogErrorFormat("Global keyword {0} doesn't exist.", new object[] { name });
			}
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x00069714 File Offset: 0x00067914
		public override string ToString()
		{
			return this.m_Name;
		}

		// Token: 0x060035BC RID: 13756
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern uint GetGlobalKeywordIndex_Injected(ref ManagedSpanWrapper keyword);

		// Token: 0x060035BD RID: 13757
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateGlobalKeyword_Injected(ref ManagedSpanWrapper keyword);

		// Token: 0x04001225 RID: 4645
		internal readonly string m_Name;

		// Token: 0x04001226 RID: 4646
		internal readonly uint m_Index;
	}
}
