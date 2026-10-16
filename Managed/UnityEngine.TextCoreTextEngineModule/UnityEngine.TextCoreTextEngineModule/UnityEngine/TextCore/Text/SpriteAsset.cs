using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000033 RID: 51
	[ExcludeFromPreset]
	[HelpURL("https://docs.unity3d.com/2023.3/Documentation/Manual/UIE-sprite.html")]
	public class SpriteAsset : TextAsset
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000A774 File Offset: 0x00008974
		// (set) Token: 0x0600015A RID: 346 RVA: 0x0000A78C File Offset: 0x0000898C
		public FaceInfo faceInfo
		{
			get
			{
				return this.m_FaceInfo;
			}
			internal set
			{
				this.m_FaceInfo = value;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000A798 File Offset: 0x00008998
		// (set) Token: 0x0600015C RID: 348 RVA: 0x0000A7B0 File Offset: 0x000089B0
		public Texture spriteSheet
		{
			get
			{
				return this.m_SpriteAtlasTexture;
			}
			internal set
			{
				this.m_SpriteAtlasTexture = value;
				this.width = (float)this.m_SpriteAtlasTexture.width;
				this.height = (float)this.m_SpriteAtlasTexture.height;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600015D RID: 349 RVA: 0x0000A7E0 File Offset: 0x000089E0
		// (set) Token: 0x0600015E RID: 350 RVA: 0x0000A7E8 File Offset: 0x000089E8
		internal float width { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000A7F1 File Offset: 0x000089F1
		// (set) Token: 0x06000160 RID: 352 RVA: 0x0000A7F9 File Offset: 0x000089F9
		internal float height { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000161 RID: 353 RVA: 0x0000A804 File Offset: 0x00008A04
		// (set) Token: 0x06000162 RID: 354 RVA: 0x0000A830 File Offset: 0x00008A30
		public List<SpriteCharacter> spriteCharacterTable
		{
			get
			{
				bool flag = this.m_GlyphIndexLookup == null;
				if (flag)
				{
					this.UpdateLookupTables();
				}
				return this.m_SpriteCharacterTable;
			}
			internal set
			{
				this.m_SpriteCharacterTable = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000163 RID: 355 RVA: 0x0000A83C File Offset: 0x00008A3C
		// (set) Token: 0x06000164 RID: 356 RVA: 0x0000A868 File Offset: 0x00008A68
		public Dictionary<uint, SpriteCharacter> spriteCharacterLookupTable
		{
			get
			{
				bool flag = this.m_SpriteCharacterLookup == null;
				if (flag)
				{
					this.UpdateLookupTables();
				}
				return this.m_SpriteCharacterLookup;
			}
			internal set
			{
				this.m_SpriteCharacterLookup = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000165 RID: 357 RVA: 0x0000A874 File Offset: 0x00008A74
		// (set) Token: 0x06000166 RID: 358 RVA: 0x0000A88C File Offset: 0x00008A8C
		public List<SpriteGlyph> spriteGlyphTable
		{
			get
			{
				return this.m_SpriteGlyphTable;
			}
			internal set
			{
				this.m_SpriteGlyphTable = value;
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000A896 File Offset: 0x00008A96
		private void Awake()
		{
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000A89C File Offset: 0x00008A9C
		public void UpdateLookupTables()
		{
			this.width = (float)this.m_SpriteAtlasTexture.width;
			this.height = (float)this.m_SpriteAtlasTexture.height;
			bool flag = this.m_GlyphIndexLookup == null;
			if (flag)
			{
				this.m_GlyphIndexLookup = new Dictionary<uint, int>();
			}
			else
			{
				this.m_GlyphIndexLookup.Clear();
			}
			bool flag2 = this.m_SpriteGlyphLookup == null;
			if (flag2)
			{
				this.m_SpriteGlyphLookup = new Dictionary<uint, SpriteGlyph>();
			}
			else
			{
				this.m_SpriteGlyphLookup.Clear();
			}
			for (int i = 0; i < this.m_SpriteGlyphTable.Count; i++)
			{
				SpriteGlyph spriteGlyph = this.m_SpriteGlyphTable[i];
				uint index = spriteGlyph.index;
				bool flag3 = !this.m_GlyphIndexLookup.ContainsKey(index);
				if (flag3)
				{
					this.m_GlyphIndexLookup.Add(index, i);
				}
				bool flag4 = !this.m_SpriteGlyphLookup.ContainsKey(index);
				if (flag4)
				{
					this.m_SpriteGlyphLookup.Add(index, spriteGlyph);
				}
			}
			bool flag5 = this.m_NameLookup == null;
			if (flag5)
			{
				this.m_NameLookup = new Dictionary<int, int>();
			}
			else
			{
				this.m_NameLookup.Clear();
			}
			bool flag6 = this.m_SpriteCharacterLookup == null;
			if (flag6)
			{
				this.m_SpriteCharacterLookup = new Dictionary<uint, SpriteCharacter>();
			}
			else
			{
				this.m_SpriteCharacterLookup.Clear();
			}
			for (int j = 0; j < this.m_SpriteCharacterTable.Count; j++)
			{
				SpriteCharacter spriteCharacter = this.m_SpriteCharacterTable[j];
				bool flag7 = spriteCharacter == null;
				if (!flag7)
				{
					uint glyphIndex = spriteCharacter.glyphIndex;
					bool flag8 = !this.m_SpriteGlyphLookup.ContainsKey(glyphIndex);
					if (!flag8)
					{
						spriteCharacter.glyph = this.m_SpriteGlyphLookup[glyphIndex];
						spriteCharacter.textAsset = this;
						int hashCodeCaseInSensitive = TextUtilities.GetHashCodeCaseInSensitive(this.m_SpriteCharacterTable[j].name);
						bool flag9 = !this.m_NameLookup.ContainsKey(hashCodeCaseInSensitive);
						if (flag9)
						{
							this.m_NameLookup.Add(hashCodeCaseInSensitive, j);
						}
						uint unicode = this.m_SpriteCharacterTable[j].unicode;
						bool flag10 = unicode != 65534U && !this.m_SpriteCharacterLookup.ContainsKey(unicode);
						if (flag10)
						{
							this.m_SpriteCharacterLookup.Add(unicode, spriteCharacter);
						}
					}
				}
			}
			this.m_IsSpriteAssetLookupTablesDirty = false;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000AB00 File Offset: 0x00008D00
		public int GetSpriteIndexFromHashcode(int hashCode)
		{
			bool flag = this.m_NameLookup == null;
			if (flag)
			{
				this.UpdateLookupTables();
			}
			int num;
			bool flag2 = this.m_NameLookup.TryGetValue(hashCode, out num);
			int num2;
			if (flag2)
			{
				num2 = num;
			}
			else
			{
				num2 = -1;
			}
			return num2;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000AB40 File Offset: 0x00008D40
		public int GetSpriteIndexFromUnicode(uint unicode)
		{
			bool flag = this.m_SpriteCharacterLookup == null;
			if (flag)
			{
				this.UpdateLookupTables();
			}
			SpriteCharacter spriteCharacter;
			bool flag2 = this.m_SpriteCharacterLookup.TryGetValue(unicode, out spriteCharacter);
			int num;
			if (flag2)
			{
				num = (int)spriteCharacter.glyphIndex;
			}
			else
			{
				num = -1;
			}
			return num;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000AB84 File Offset: 0x00008D84
		public int GetSpriteIndexFromName(string name)
		{
			bool flag = this.m_NameLookup == null;
			if (flag)
			{
				this.UpdateLookupTables();
			}
			int hashCodeCaseInSensitive = TextUtilities.GetHashCodeCaseInSensitive(name);
			return this.GetSpriteIndexFromHashcode(hashCodeCaseInSensitive);
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000ABB8 File Offset: 0x00008DB8
		public static SpriteAsset SearchForSpriteByUnicode(SpriteAsset spriteAsset, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			bool flag = spriteAsset == null;
			SpriteAsset spriteAsset2;
			if (flag)
			{
				spriteIndex = -1;
				spriteAsset2 = null;
			}
			else
			{
				spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(unicode);
				bool flag2 = spriteIndex != -1;
				if (flag2)
				{
					spriteAsset2 = spriteAsset;
				}
				else
				{
					HashSet<int> hashSet = new HashSet<int>();
					int instanceID = spriteAsset.GetInstanceID();
					hashSet.Add(instanceID);
					bool flag3 = includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0;
					if (flag3)
					{
						spriteAsset2 = SpriteAsset.SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, unicode, true, hashSet, out spriteIndex);
					}
					else
					{
						spriteIndex = -1;
						spriteAsset2 = null;
					}
				}
			}
			return spriteAsset2;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000AC48 File Offset: 0x00008E48
		private static SpriteAsset SearchForSpriteByUnicodeInternal(List<SpriteAsset> spriteAssets, uint unicode, bool includeFallbacks, HashSet<int> searchedSpriteAssets, out int spriteIndex)
		{
			for (int i = 0; i < spriteAssets.Count; i++)
			{
				SpriteAsset spriteAsset = spriteAssets[i];
				bool flag = spriteAsset == null;
				if (!flag)
				{
					int instanceID = spriteAsset.GetInstanceID();
					bool flag2 = !searchedSpriteAssets.Add(instanceID);
					if (!flag2)
					{
						spriteAsset = SpriteAsset.SearchForSpriteByUnicodeInternal(spriteAsset, unicode, includeFallbacks, searchedSpriteAssets, out spriteIndex);
						bool flag3 = spriteAsset != null;
						if (flag3)
						{
							return spriteAsset;
						}
					}
				}
			}
			spriteIndex = -1;
			return null;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000ACC8 File Offset: 0x00008EC8
		private static SpriteAsset SearchForSpriteByUnicodeInternal(SpriteAsset spriteAsset, uint unicode, bool includeFallbacks, HashSet<int> searchedSpriteAssets, out int spriteIndex)
		{
			spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(unicode);
			bool flag = spriteIndex != -1;
			SpriteAsset spriteAsset2;
			if (flag)
			{
				spriteAsset2 = spriteAsset;
			}
			else
			{
				bool flag2 = includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0;
				if (flag2)
				{
					spriteAsset2 = SpriteAsset.SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, unicode, true, searchedSpriteAssets, out spriteIndex);
				}
				else
				{
					spriteIndex = -1;
					spriteAsset2 = null;
				}
			}
			return spriteAsset2;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000AD30 File Offset: 0x00008F30
		public static SpriteAsset SearchForSpriteByHashCode(SpriteAsset spriteAsset, int hashCode, bool includeFallbacks, out int spriteIndex, TextSettings textSettings = null)
		{
			bool flag = spriteAsset == null;
			SpriteAsset spriteAsset2;
			if (flag)
			{
				spriteIndex = -1;
				spriteAsset2 = null;
			}
			else
			{
				spriteIndex = spriteAsset.GetSpriteIndexFromHashcode(hashCode);
				bool flag2 = spriteIndex != -1;
				if (flag2)
				{
					spriteAsset2 = spriteAsset;
				}
				else
				{
					HashSet<int> hashSet = new HashSet<int>();
					int hashCode2 = spriteAsset.GetHashCode();
					hashSet.Add(hashCode2);
					bool flag3 = includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0;
					if (flag3)
					{
						SpriteAsset spriteAsset3 = SpriteAsset.SearchForSpriteByHashCodeInternal(spriteAsset.fallbackSpriteAssets, hashCode, true, hashSet, out spriteIndex);
						bool flag4 = spriteIndex != -1;
						if (flag4)
						{
							return spriteAsset3;
						}
					}
					bool flag5 = textSettings == null;
					if (flag5)
					{
						spriteIndex = -1;
						spriteAsset2 = null;
					}
					else
					{
						bool flag6 = includeFallbacks && textSettings.defaultSpriteAsset != null;
						if (flag6)
						{
							SpriteAsset spriteAsset3 = SpriteAsset.SearchForSpriteByHashCodeInternal(textSettings.defaultSpriteAsset, hashCode, true, hashSet, out spriteIndex);
							bool flag7 = spriteIndex != -1;
							if (flag7)
							{
								return spriteAsset3;
							}
						}
						hashSet.Clear();
						uint missingSpriteCharacterUnicode = textSettings.missingSpriteCharacterUnicode;
						spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(missingSpriteCharacterUnicode);
						bool flag8 = spriteIndex != -1;
						if (flag8)
						{
							spriteAsset2 = spriteAsset;
						}
						else
						{
							hashSet.Add(hashCode2);
							bool flag9 = includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0;
							if (flag9)
							{
								SpriteAsset spriteAsset3 = SpriteAsset.SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, missingSpriteCharacterUnicode, true, hashSet, out spriteIndex);
								bool flag10 = spriteIndex != -1;
								if (flag10)
								{
									return spriteAsset3;
								}
							}
							bool flag11 = includeFallbacks && textSettings.defaultSpriteAsset != null;
							if (flag11)
							{
								SpriteAsset spriteAsset3 = SpriteAsset.SearchForSpriteByUnicodeInternal(textSettings.defaultSpriteAsset, missingSpriteCharacterUnicode, true, hashSet, out spriteIndex);
								bool flag12 = spriteIndex != -1;
								if (flag12)
								{
									return spriteAsset3;
								}
							}
							spriteIndex = -1;
							spriteAsset2 = null;
						}
					}
				}
			}
			return spriteAsset2;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000AEF8 File Offset: 0x000090F8
		private static SpriteAsset SearchForSpriteByHashCodeInternal(List<SpriteAsset> spriteAssets, int hashCode, bool searchFallbacks, HashSet<int> searchedSpriteAssets, out int spriteIndex)
		{
			for (int i = 0; i < spriteAssets.Count; i++)
			{
				SpriteAsset spriteAsset = spriteAssets[i];
				bool flag = spriteAsset == null;
				if (!flag)
				{
					int hashCode2 = spriteAsset.GetHashCode();
					bool flag2 = !searchedSpriteAssets.Add(hashCode2);
					if (!flag2)
					{
						spriteAsset = SpriteAsset.SearchForSpriteByHashCodeInternal(spriteAsset, hashCode, searchFallbacks, searchedSpriteAssets, out spriteIndex);
						bool flag3 = spriteAsset != null;
						if (flag3)
						{
							return spriteAsset;
						}
					}
				}
			}
			spriteIndex = -1;
			return null;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000AF78 File Offset: 0x00009178
		private static SpriteAsset SearchForSpriteByHashCodeInternal(SpriteAsset spriteAsset, int hashCode, bool searchFallbacks, HashSet<int> searchedSpriteAssets, out int spriteIndex)
		{
			spriteIndex = spriteAsset.GetSpriteIndexFromHashcode(hashCode);
			bool flag = spriteIndex != -1;
			SpriteAsset spriteAsset2;
			if (flag)
			{
				spriteAsset2 = spriteAsset;
			}
			else
			{
				bool flag2 = searchFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0;
				if (flag2)
				{
					spriteAsset2 = SpriteAsset.SearchForSpriteByHashCodeInternal(spriteAsset.fallbackSpriteAssets, hashCode, true, searchedSpriteAssets, out spriteIndex);
				}
				else
				{
					spriteIndex = -1;
					spriteAsset2 = null;
				}
			}
			return spriteAsset2;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000AFE0 File Offset: 0x000091E0
		public void SortGlyphTable()
		{
			bool flag = this.m_SpriteGlyphTable == null || this.m_SpriteGlyphTable.Count == 0;
			if (!flag)
			{
				this.m_SpriteGlyphTable = this.m_SpriteGlyphTable.OrderBy<SpriteGlyph, uint>((SpriteGlyph item) => item.index).ToList<SpriteGlyph>();
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000B044 File Offset: 0x00009244
		internal void SortCharacterTable()
		{
			bool flag = this.m_SpriteCharacterTable != null && this.m_SpriteCharacterTable.Count > 0;
			if (flag)
			{
				this.m_SpriteCharacterTable = this.m_SpriteCharacterTable.OrderBy<SpriteCharacter, uint>((SpriteCharacter c) => c.unicode).ToList<SpriteCharacter>();
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000B0A4 File Offset: 0x000092A4
		internal void SortGlyphAndCharacterTables()
		{
			this.SortGlyphTable();
			this.SortCharacterTable();
		}

		// Token: 0x0400013F RID: 319
		internal Dictionary<int, int> m_NameLookup;

		// Token: 0x04000140 RID: 320
		internal Dictionary<uint, int> m_GlyphIndexLookup;

		// Token: 0x04000141 RID: 321
		[SerializeField]
		internal FaceInfo m_FaceInfo;

		// Token: 0x04000142 RID: 322
		[SerializeField]
		[FormerlySerializedAs("spriteSheet")]
		internal Texture m_SpriteAtlasTexture;

		// Token: 0x04000145 RID: 325
		[SerializeField]
		private List<SpriteCharacter> m_SpriteCharacterTable = new List<SpriteCharacter>();

		// Token: 0x04000146 RID: 326
		internal Dictionary<uint, SpriteCharacter> m_SpriteCharacterLookup;

		// Token: 0x04000147 RID: 327
		[SerializeField]
		private List<SpriteGlyph> m_SpriteGlyphTable = new List<SpriteGlyph>();

		// Token: 0x04000148 RID: 328
		internal Dictionary<uint, SpriteGlyph> m_SpriteGlyphLookup;

		// Token: 0x04000149 RID: 329
		[SerializeField]
		public List<SpriteAsset> fallbackSpriteAssets;

		// Token: 0x0400014A RID: 330
		internal bool m_IsSpriteAssetLookupTablesDirty = false;
	}
}
