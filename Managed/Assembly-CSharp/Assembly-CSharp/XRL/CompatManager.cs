using System;
using System.Collections.Generic;
using XRL.Collections;
using XRL.UI;

namespace XRL
{
	// Token: 0x0200051C RID: 1308
	[HasModSensitiveStaticCache]
	public static class CompatManager
	{
		// Token: 0x060037FF RID: 14335 RVA: 0x001118CD File Offset: 0x0010FACD
		public static void CheckInit()
		{
			if (!CompatManager.Loaded || CompatManager.Skills == null)
			{
				CompatManager.Init();
			}
		}

		// Token: 0x06003800 RID: 14336 RVA: 0x001118E4 File Offset: 0x0010FAE4
		[ModSensitiveCacheInit]
		private static void Init()
		{
			CompatManager.Loaded = false;
			CompatManager.CompatEntries.Clear();
			if (CompatManager.Skills == null)
			{
				CompatManager.Skills = new StringMap<string>();
			}
			CompatManager.Skills.Clear();
			if (CompatManager.Mutations == null)
			{
				CompatManager.Mutations = new StringMap<string>();
			}
			CompatManager.Mutations.Clear();
			if (CompatManager.Factions == null)
			{
				CompatManager.Factions = new StringMap<string>();
			}
			CompatManager.Factions.Clear();
			CompatManager.CompatEntries.Add("skill", CompatManager.Skills);
			CompatManager.CompatEntries.Add("mutation", CompatManager.Mutations);
			CompatManager.CompatEntries.Add("faction", CompatManager.Factions);
			Loading.LoadTask("Loading Compat.xml", delegate
			{
				CompatManager.Loaded = true;
				foreach (XmlDataHelper xmlDataHelper in DataManager.YieldXMLStreamsWithRoot("compat", true, true))
				{
					xmlDataHelper.HandleNodes(CompatManager._outerNodes, null);
				}
			}, true);
		}

		// Token: 0x06003801 RID: 14337 RVA: 0x001119B8 File Offset: 0x0010FBB8
		public static void HandleDeprecatedNode(XmlDataHelper Reader)
		{
			Reader.ParseWarning(Reader.Name + " node deprecated, the children of this node are parsed outside of the container anyway.");
			Reader.HandleNodes(CompatManager._innerNodes, new Action<XmlDataHelper>(CompatManager.HandleGenericCompatNode));
		}

		// Token: 0x06003802 RID: 14338 RVA: 0x001119E7 File Offset: 0x0010FBE7
		public static void HandleInnerNode(XmlDataHelper xml)
		{
			xml.HandleNodes(CompatManager._innerNodes, new Action<XmlDataHelper>(CompatManager.HandleGenericCompatNode));
		}

		// Token: 0x06003803 RID: 14339 RVA: 0x00111A00 File Offset: 0x0010FC00
		public static void HandleGenericCompatNode(XmlDataHelper Reader)
		{
			string text = Reader.ParseAttribute<string>("Old", null, true, null);
			if (text.IsNullOrEmpty())
			{
				throw new Exception(Reader.Name + " tag had missing or empty Old attribute");
			}
			string text2 = Reader.ParseAttribute<string>("New", null, true, null);
			if (text2.IsNullOrEmpty())
			{
				throw new Exception(Reader.Name + " tag had missing or empty New attribute");
			}
			CompatManager.SetCompatEntry(Reader.Name.ToLower(), text, text2);
			Reader.DoneWithElement();
		}

		// Token: 0x06003804 RID: 14340 RVA: 0x00111A80 File Offset: 0x0010FC80
		public static void SetCompatEntry(string Type, string Old, string New)
		{
			CompatManager.CheckInit();
			StringMap<string> stringMap;
			if (!CompatManager.CompatEntries.TryGetValue(Type, out stringMap))
			{
				stringMap = new StringMap<string>();
				CompatManager.CompatEntries.Add(Type, stringMap);
			}
			stringMap[Old] = New;
		}

		// Token: 0x06003805 RID: 14341 RVA: 0x00111ABC File Offset: 0x0010FCBC
		public static bool TryGetCompatEntry(string Type, string ID, out string NewID)
		{
			CompatManager.CheckInit();
			NewID = null;
			StringMap<string> stringMap;
			return CompatManager.CompatEntries.TryGetValue(Type, out stringMap) && stringMap.TryGetValue(ID, out NewID);
		}

		// Token: 0x06003806 RID: 14342 RVA: 0x00111AEC File Offset: 0x0010FCEC
		public static bool ProcessCompatEntry(string Type, ref string Name)
		{
			string text;
			if (CompatManager.TryGetCompatEntry(Type, Name, out text))
			{
				Name = text;
				return true;
			}
			return false;
		}

		// Token: 0x06003807 RID: 14343 RVA: 0x00111B0C File Offset: 0x0010FD0C
		public static string ProcessCompatEntry(string Type, string Name)
		{
			string text;
			if (!CompatManager.TryGetCompatEntry(Type, Name, out text))
			{
				return Name;
			}
			return text;
		}

		/// <summary>
		///     Convert an old part class name into a new one.
		///     Checks all compat namespaces that resolve as Part names. (Skills, Mutations)
		/// </summary>
		// Token: 0x06003808 RID: 14344 RVA: 0x00111B27 File Offset: 0x0010FD27
		public static void ProcessPart(ref string Part)
		{
			CompatManager.ProcessSkill(ref Part);
			CompatManager.ProcessMutation(ref Part);
		}

		/// <inheritdoc cref="M:XRL.CompatManager.ProcessPart(System.String@)" />
		// Token: 0x06003809 RID: 14345 RVA: 0x00111B35 File Offset: 0x0010FD35
		public static string ProcessPart(string Part)
		{
			CompatManager.ProcessPart(ref Part);
			return Part;
		}

		/// <summary>
		///     Get a replacement class name for a part (if one exists, null otherwise).
		///     Checks all compat namespaces that resolve as Part names. (Skills, Mutations)
		/// </summary>
		// Token: 0x0600380A RID: 14346 RVA: 0x00111B40 File Offset: 0x0010FD40
		public static string GetNewPart(string Part)
		{
			string text = Part;
			CompatManager.ProcessPart(ref text);
			if (text == Part)
			{
				return null;
			}
			return text;
		}

		/// <summary>
		/// Gets the new class name of a superseded part.
		/// </summary>
		/// <param name="Part">The potentially deprecated part.</param>
		/// <param name="NewPart">The class name of the superseding part.</param>
		/// <param name="Type">The type category of the new part.</param>
		/// <returns><see langword="true" /> if the <see cref="T:XRL.CompatManager" /> contains a superseding part; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600380B RID: 14347 RVA: 0x00111B62 File Offset: 0x0010FD62
		public static bool TryGetPart(string Part, out string NewPart, out string Type)
		{
			CompatManager.CheckInit();
			if (CompatManager.Skills.TryGetValue(Part, out NewPart))
			{
				Type = "Skill";
				return true;
			}
			if (CompatManager.Mutations.TryGetValue(Part, out NewPart))
			{
				Type = "Mutation";
				return true;
			}
			Type = null;
			return false;
		}

		// Token: 0x0600380C RID: 14348 RVA: 0x00111B9C File Offset: 0x0010FD9C
		public static bool TryGetPart(string Part, out string NewPart)
		{
			string text;
			return CompatManager.TryGetPart(Part, out NewPart, out text);
		}

		// Token: 0x0600380D RID: 14349 RVA: 0x00111BB4 File Offset: 0x0010FDB4
		public static string GetNewSkill(string Skill)
		{
			CompatManager.CheckInit();
			string text;
			if (CompatManager.Skills.TryGetValue(Skill, out text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x0600380E RID: 14350 RVA: 0x00111BD8 File Offset: 0x0010FDD8
		public static void ProcessSkill(ref string Skill)
		{
			CompatManager.CheckInit();
			string text;
			if (CompatManager.Skills.TryGetValue(Skill, out text))
			{
				Skill = text;
			}
		}

		// Token: 0x0600380F RID: 14351 RVA: 0x00111C00 File Offset: 0x0010FE00
		public static string ProcessSkill(string Skill)
		{
			CompatManager.CheckInit();
			string text;
			if (CompatManager.Skills.TryGetValue(Skill, out text))
			{
				Skill = text;
			}
			return Skill;
		}

		// Token: 0x06003810 RID: 14352 RVA: 0x00111C28 File Offset: 0x0010FE28
		public static string GetNewMutation(string Mutation)
		{
			CompatManager.CheckInit();
			string text;
			if (CompatManager.Mutations.TryGetValue(Mutation, out text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x06003811 RID: 14353 RVA: 0x00111C4C File Offset: 0x0010FE4C
		public static void ProcessMutation(ref string Mutation)
		{
			CompatManager.CheckInit();
			string text;
			if (CompatManager.Mutations.TryGetValue(Mutation, out text))
			{
				Mutation = text;
			}
		}

		// Token: 0x06003812 RID: 14354 RVA: 0x00111C74 File Offset: 0x0010FE74
		public static string ProcessMutation(string Mutation)
		{
			CompatManager.CheckInit();
			string text;
			if (CompatManager.Mutations.TryGetValue(Mutation, out text))
			{
				Mutation = text;
			}
			return Mutation;
		}

		// Token: 0x06003813 RID: 14355 RVA: 0x00111C9C File Offset: 0x0010FE9C
		public static string GetNewFaction(string Faction)
		{
			CompatManager.CheckInit();
			string text;
			if (CompatManager.Factions.TryGetValue(Faction, out text))
			{
				return text;
			}
			return null;
		}

		// Token: 0x06003814 RID: 14356 RVA: 0x00111CC0 File Offset: 0x0010FEC0
		public static void ProcessFaction(ref string Faction)
		{
			CompatManager.CheckInit();
			string text;
			if (CompatManager.Factions.TryGetValue(Faction, out text))
			{
				Faction = text;
			}
		}

		// Token: 0x06003815 RID: 14357 RVA: 0x00111CE8 File Offset: 0x0010FEE8
		public static string ProcessFaction(string Faction)
		{
			CompatManager.CheckInit();
			string text;
			if (CompatManager.Factions.TryGetValue(Faction, out text))
			{
				Faction = text;
			}
			return Faction;
		}

		// Token: 0x04001C15 RID: 7189
		public static bool Loaded;

		// Token: 0x04001C16 RID: 7190
		[ModSensitiveStaticCache(false, CreateEmptyInstance = true)]
		public static StringMap<StringMap<string>> CompatEntries;

		// Token: 0x04001C17 RID: 7191
		[ModSensitiveStaticCache(false, CreateEmptyInstance = false)]
		public static StringMap<string> Skills;

		// Token: 0x04001C18 RID: 7192
		[ModSensitiveStaticCache(false, CreateEmptyInstance = false)]
		public static StringMap<string> Mutations;

		// Token: 0x04001C19 RID: 7193
		[ModSensitiveStaticCache(false, CreateEmptyInstance = false)]
		public static StringMap<string> Factions;

		// Token: 0x04001C1A RID: 7194
		private static readonly Dictionary<string, Action<XmlDataHelper>> _outerNodes = new Dictionary<string, Action<XmlDataHelper>> { 
		{
			"compat",
			new Action<XmlDataHelper>(CompatManager.HandleInnerNode)
		} };

		// Token: 0x04001C1B RID: 7195
		private static readonly Dictionary<string, Action<XmlDataHelper>> _innerNodes = new Dictionary<string, Action<XmlDataHelper>>
		{
			{
				"skills",
				new Action<XmlDataHelper>(CompatManager.HandleDeprecatedNode)
			},
			{
				"mutations",
				new Action<XmlDataHelper>(CompatManager.HandleDeprecatedNode)
			},
			{
				"factions",
				new Action<XmlDataHelper>(CompatManager.HandleDeprecatedNode)
			}
		};
	}
}
