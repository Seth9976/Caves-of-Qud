using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x02000569 RID: 1385
	[NativeHeader("Runtime/Shaders/Keywords/KeywordSpaceScriptBindings.h")]
	public readonly struct LocalKeywordSpace : IEquatable<LocalKeywordSpace>
	{
		// Token: 0x060035DB RID: 13787
		[FreeFunction("keywords::GetKeywords", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern LocalKeyword[] GetKeywords();

		// Token: 0x060035DC RID: 13788
		[FreeFunction("keywords::GetKeywordNames", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string[] GetKeywordNames();

		// Token: 0x060035DD RID: 13789
		[FreeFunction("keywords::GetKeywordCount", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern uint GetKeywordCount();

		// Token: 0x060035DE RID: 13790 RVA: 0x00069AD0 File Offset: 0x00067CD0
		[FreeFunction("keywords::GetKeyword", HasExplicitThis = true)]
		private unsafe LocalKeyword GetKeyword(string name)
		{
			LocalKeyword localKeyword2;
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
				LocalKeyword localKeyword;
				LocalKeywordSpace.GetKeyword_Injected(ref this, ref managedSpanWrapper, out localKeyword);
			}
			finally
			{
				char* ptr = null;
				LocalKeyword localKeyword;
				localKeyword2 = localKeyword;
			}
			return localKeyword2;
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x060035DF RID: 13791 RVA: 0x00069B2C File Offset: 0x00067D2C
		public LocalKeyword[] keywords
		{
			get
			{
				return this.GetKeywords();
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x060035E0 RID: 13792 RVA: 0x00069B44 File Offset: 0x00067D44
		public string[] keywordNames
		{
			get
			{
				return this.GetKeywordNames();
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x060035E1 RID: 13793 RVA: 0x00069B5C File Offset: 0x00067D5C
		public uint keywordCount
		{
			get
			{
				return this.GetKeywordCount();
			}
		}

		// Token: 0x060035E2 RID: 13794 RVA: 0x00069B74 File Offset: 0x00067D74
		public LocalKeyword FindKeyword(string name)
		{
			return this.GetKeyword(name);
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x00069B90 File Offset: 0x00067D90
		public override bool Equals(object o)
		{
			bool flag;
			if (o is LocalKeywordSpace)
			{
				LocalKeywordSpace localKeywordSpace = (LocalKeywordSpace)o;
				flag = this.Equals(localKeywordSpace);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060035E4 RID: 13796 RVA: 0x00069BBC File Offset: 0x00067DBC
		public bool Equals(LocalKeywordSpace rhs)
		{
			return this.m_KeywordSpace == rhs.m_KeywordSpace;
		}

		// Token: 0x060035E5 RID: 13797 RVA: 0x00069BE0 File Offset: 0x00067DE0
		public static bool operator ==(LocalKeywordSpace lhs, LocalKeywordSpace rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x060035E6 RID: 13798 RVA: 0x00069BFC File Offset: 0x00067DFC
		public static bool operator !=(LocalKeywordSpace lhs, LocalKeywordSpace rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060035E7 RID: 13799 RVA: 0x00069C18 File Offset: 0x00067E18
		public override int GetHashCode()
		{
			return this.m_KeywordSpace.GetHashCode();
		}

		// Token: 0x060035E8 RID: 13800
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetKeyword_Injected(ref LocalKeywordSpace _unity_self, ref ManagedSpanWrapper name, out LocalKeyword ret);

		// Token: 0x0400122A RID: 4650
		private readonly IntPtr m_KeywordSpace;
	}
}
