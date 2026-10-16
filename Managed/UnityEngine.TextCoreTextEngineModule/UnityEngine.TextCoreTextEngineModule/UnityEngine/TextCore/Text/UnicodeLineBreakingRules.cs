using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000072 RID: 114
	[Serializable]
	public class UnicodeLineBreakingRules
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600038B RID: 907 RVA: 0x00027D8B File Offset: 0x00025F8B
		public TextAsset lineBreakingRules
		{
			get
			{
				return this.m_UnicodeLineBreakingRules;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00027D93 File Offset: 0x00025F93
		public TextAsset leadingCharacters
		{
			get
			{
				return this.m_LeadingCharacters;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00027D9B File Offset: 0x00025F9B
		public TextAsset followingCharacters
		{
			get
			{
				return this.m_FollowingCharacters;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00027DA4 File Offset: 0x00025FA4
		// (set) Token: 0x0600038F RID: 911 RVA: 0x00027DD0 File Offset: 0x00025FD0
		internal HashSet<uint> leadingCharactersLookup
		{
			get
			{
				bool flag = this.m_LeadingCharactersLookup == null;
				if (flag)
				{
					this.LoadLineBreakingRules();
				}
				return this.m_LeadingCharactersLookup;
			}
			set
			{
				this.m_LeadingCharactersLookup = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000390 RID: 912 RVA: 0x00027DDC File Offset: 0x00025FDC
		// (set) Token: 0x06000391 RID: 913 RVA: 0x00027E08 File Offset: 0x00026008
		internal HashSet<uint> followingCharactersLookup
		{
			get
			{
				bool flag = this.m_LeadingCharactersLookup == null;
				if (flag)
				{
					this.LoadLineBreakingRules();
				}
				return this.m_FollowingCharactersLookup;
			}
			set
			{
				this.m_FollowingCharactersLookup = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00027E11 File Offset: 0x00026011
		// (set) Token: 0x06000393 RID: 915 RVA: 0x00027E19 File Offset: 0x00026019
		public bool useModernHangulLineBreakingRules
		{
			get
			{
				return this.m_UseModernHangulLineBreakingRules;
			}
			set
			{
				this.m_UseModernHangulLineBreakingRules = value;
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00027E24 File Offset: 0x00026024
		[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
		internal void LoadLineBreakingRules()
		{
			bool flag = this.m_LeadingCharactersLookup == null;
			if (flag)
			{
				bool flag2 = this.m_LeadingCharacters == null;
				if (flag2)
				{
					this.m_LeadingCharacters = Resources.Load<TextAsset>("LineBreaking Leading Characters");
				}
				this.m_LeadingCharactersLookup = ((this.m_LeadingCharacters != null) ? UnicodeLineBreakingRules.GetCharacters(this.m_LeadingCharacters) : new HashSet<uint>());
				bool flag3 = this.m_FollowingCharacters == null;
				if (flag3)
				{
					this.m_FollowingCharacters = Resources.Load<TextAsset>("LineBreaking Following Characters");
				}
				this.m_FollowingCharactersLookup = ((this.m_FollowingCharacters != null) ? UnicodeLineBreakingRules.GetCharacters(this.m_FollowingCharacters) : new HashSet<uint>());
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00027ED0 File Offset: 0x000260D0
		internal void LoadLineBreakingRules(TextAsset leadingRules, TextAsset followingRules)
		{
			bool flag = this.m_LeadingCharactersLookup == null;
			if (flag)
			{
				bool flag2 = leadingRules == null;
				if (flag2)
				{
					leadingRules = Resources.Load<TextAsset>("LineBreaking Leading Characters");
				}
				this.m_LeadingCharactersLookup = ((leadingRules != null) ? UnicodeLineBreakingRules.GetCharacters(leadingRules) : new HashSet<uint>());
				bool flag3 = followingRules == null;
				if (flag3)
				{
					followingRules = Resources.Load<TextAsset>("LineBreaking Following Characters");
				}
				this.m_FollowingCharactersLookup = ((followingRules != null) ? UnicodeLineBreakingRules.GetCharacters(followingRules) : new HashSet<uint>());
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00027F54 File Offset: 0x00026154
		private static HashSet<uint> GetCharacters(TextAsset file)
		{
			HashSet<uint> hashSet = new HashSet<uint>();
			string text = file.text;
			for (int i = 0; i < text.Length; i++)
			{
				hashSet.Add((uint)text[i]);
			}
			return hashSet;
		}

		// Token: 0x040004C3 RID: 1219
		[SerializeField]
		private TextAsset m_UnicodeLineBreakingRules;

		// Token: 0x040004C4 RID: 1220
		[SerializeField]
		private TextAsset m_LeadingCharacters;

		// Token: 0x040004C5 RID: 1221
		[SerializeField]
		private TextAsset m_FollowingCharacters;

		// Token: 0x040004C6 RID: 1222
		[SerializeField]
		private bool m_UseModernHangulLineBreakingRules;

		// Token: 0x040004C7 RID: 1223
		private HashSet<uint> m_LeadingCharactersLookup;

		// Token: 0x040004C8 RID: 1224
		private HashSet<uint> m_FollowingCharactersLookup;
	}
}
