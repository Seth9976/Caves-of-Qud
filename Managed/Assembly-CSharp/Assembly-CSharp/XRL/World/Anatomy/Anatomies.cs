using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using XRL.UI;
using XRL.World.Capabilities;

namespace XRL.World.Anatomy
{
	// Token: 0x02000BC0 RID: 3008
	[HasModSensitiveStaticCache]
	public static class Anatomies
	{
		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x060078DB RID: 30939 RVA: 0x002D9415 File Offset: 0x002D7615
		public static Dictionary<string, BodyPartType> BodyPartTypeTable
		{
			get
			{
				Anatomies.CheckInit();
				return Anatomies._BodyPartTypeTable;
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x060078DC RID: 30940 RVA: 0x002D9421 File Offset: 0x002D7621
		public static List<BodyPartType> BodyPartTypeList
		{
			get
			{
				Anatomies.CheckInit();
				return Anatomies._BodyPartTypeList;
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x060078DD RID: 30941 RVA: 0x002D942D File Offset: 0x002D762D
		public static Dictionary<string, Anatomy> AnatomyTable
		{
			get
			{
				Anatomies.CheckInit();
				return Anatomies._AnatomyTable;
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x060078DE RID: 30942 RVA: 0x002D9439 File Offset: 0x002D7639
		public static List<Anatomy> AnatomyList
		{
			get
			{
				Anatomies.CheckInit();
				return Anatomies._AnatomyList;
			}
		}

		// Token: 0x060078DF RID: 30943 RVA: 0x002D9445 File Offset: 0x002D7645
		public static void CheckInit()
		{
			if (Anatomies._BodyPartTypeTable == null)
			{
				Loading.LoadTask("Loading Bodies.xml", new Action(Anatomies.Init), true);
			}
		}

		// Token: 0x060078E0 RID: 30944 RVA: 0x002D9468 File Offset: 0x002D7668
		private static void Init()
		{
			Anatomies._BodyPartTypeTable = new Dictionary<string, BodyPartType>(32);
			Anatomies._BodyPartTypeList = new List<BodyPartType>(32);
			Anatomies._AnatomyTable = new Dictionary<string, Anatomy>(64);
			Anatomies._AnatomyList = new List<Anatomy>(64);
			foreach (DataFile dataFile in DataManager.GetXMLFilesWithRoot("Bodies"))
			{
				try
				{
					Anatomies.ProcessBodiesXmlFile(dataFile, dataFile.IsMod);
				}
				catch (Exception ex)
				{
					MetricsManager.LogPotentialModError(dataFile.Mod, ex);
				}
			}
		}

		// Token: 0x060078E1 RID: 30945 RVA: 0x002D9518 File Offset: 0x002D7718
		public static Anatomy GetRandomAnatomy()
		{
			return Anatomies.AnatomyTable.Values.GetRandomElement<Anatomy>(null);
		}

		// Token: 0x060078E2 RID: 30946 RVA: 0x002D952C File Offset: 0x002D772C
		public static BodyPartType GetBodyPartType(string name)
		{
			BodyPartType bodyPartType;
			if (Anatomies.BodyPartTypeTable.TryGetValue(name, out bodyPartType))
			{
				return bodyPartType;
			}
			return null;
		}

		// Token: 0x060078E3 RID: 30947 RVA: 0x002D954C File Offset: 0x002D774C
		public static BodyPartType GetBodyPartTypeOrFail(string name)
		{
			BodyPartType bodyPartType;
			if (Anatomies.BodyPartTypeTable.TryGetValue(name, out bodyPartType))
			{
				return bodyPartType;
			}
			throw new Exception("invalid body part type " + name);
		}

		// Token: 0x060078E4 RID: 30948 RVA: 0x002D957C File Offset: 0x002D777C
		public static Anatomy GetAnatomy(string name)
		{
			Anatomy anatomy;
			if (Anatomies.AnatomyTable.TryGetValue(name, out anatomy))
			{
				return anatomy;
			}
			return null;
		}

		// Token: 0x060078E5 RID: 30949 RVA: 0x002D959C File Offset: 0x002D779C
		public static Anatomy GetAnatomyOrFail(string name)
		{
			Anatomy anatomy;
			if (Anatomies.AnatomyTable.TryGetValue(name, out anatomy))
			{
				return anatomy;
			}
			throw new Exception("invalid anatomy " + name);
		}

		// Token: 0x060078E6 RID: 30950 RVA: 0x002D95CC File Offset: 0x002D77CC
		private static void ProcessBodiesXmlFile(string file, bool mod)
		{
			using (XmlTextReader streamingAssetsXMLStream = DataManager.GetStreamingAssetsXMLStream(file))
			{
				streamingAssetsXMLStream.WhitespaceHandling = WhitespaceHandling.None;
				while (streamingAssetsXMLStream.Read())
				{
					if (streamingAssetsXMLStream.NodeType == XmlNodeType.Element)
					{
						if (!(streamingAssetsXMLStream.Name == "bodies"))
						{
							throw new XmlUnsupportedElementException(streamingAssetsXMLStream);
						}
						Anatomies.LoadBodiesNode(streamingAssetsXMLStream, mod);
					}
				}
				streamingAssetsXMLStream.Close();
			}
		}

		// Token: 0x060078E7 RID: 30951 RVA: 0x002D9640 File Offset: 0x002D7840
		public static void LoadBodiesNode(XmlTextReader Reader, bool mod = false)
		{
			while (Reader.Read())
			{
				if (Reader.NodeType == XmlNodeType.Element)
				{
					if (Reader.Name == "bodyparttypes")
					{
						Anatomies.LoadBodyPartTypeTableNode(Reader, mod);
					}
					else if (Reader.Name == "bodyparttypevariants")
					{
						Anatomies.LoadBodyPartTypeVariantsNode(Reader, mod);
					}
					else
					{
						if (!(Reader.Name == "anatomies"))
						{
							throw new XmlUnsupportedElementException(Reader);
						}
						Anatomies.LoadAnatomiesNode(Reader, mod);
					}
				}
				else if (Reader.NodeType == XmlNodeType.EndElement)
				{
					break;
				}
			}
		}

		// Token: 0x060078E8 RID: 30952 RVA: 0x002D96C4 File Offset: 0x002D78C4
		public static void LoadBodyPartTypeTableNode(XmlTextReader Reader, bool mod = false)
		{
			while (Reader.Read())
			{
				if (Reader.NodeType == XmlNodeType.Element)
				{
					if (Reader.Name == "bodyparttype")
					{
						Anatomies.LoadBodyPartTypeNode(Reader, mod);
					}
					else
					{
						if (!(Reader.Name == "removebodyparttype"))
						{
							throw new XmlUnsupportedElementException(Reader);
						}
						string attribute = Reader.GetAttribute("Name");
						if (string.IsNullOrEmpty(attribute))
						{
							throw new XmlException("removebodyparttype tag had no Name attribute", Reader);
						}
						Anatomies._BodyPartTypeTable.Remove(attribute);
					}
				}
				else if (Reader.NodeType == XmlNodeType.EndElement)
				{
					break;
				}
			}
		}

		// Token: 0x060078E9 RID: 30953 RVA: 0x002D9750 File Offset: 0x002D7950
		private static void LoadBodyPartTypeNode(XmlTextReader Reader, bool mod = false)
		{
			string attribute = Reader.GetAttribute("Type");
			if (string.IsNullOrEmpty(attribute))
			{
				throw new XmlException(Reader.Name + " tag had no Type attribute", Reader);
			}
			BodyPartType bodyPartType;
			if (Anatomies._BodyPartTypeTable.ContainsKey(attribute))
			{
				if (!mod)
				{
					Debug.LogError("duplicate bodyparttype " + attribute);
				}
				bodyPartType = Anatomies._BodyPartTypeTable[attribute];
			}
			else
			{
				bodyPartType = new BodyPartType(attribute, null, null);
				Anatomies._BodyPartTypeTable.Add(bodyPartType.Type, bodyPartType);
				Anatomies._BodyPartTypeList.Add(bodyPartType);
			}
			Anatomies.LoadBodyPartInfo(bodyPartType, Reader, mod);
		}

		// Token: 0x060078EA RID: 30954 RVA: 0x002D97E4 File Offset: 0x002D79E4
		public static void LoadBodyPartTypeVariantsNode(XmlTextReader Reader, bool mod = false)
		{
			while (Reader.Read())
			{
				if (Reader.NodeType == XmlNodeType.Element)
				{
					if (Reader.Name == "bodyparttypevariant")
					{
						Anatomies.LoadBodyPartTypeVariantNode(Reader, mod);
					}
					else
					{
						if (!(Reader.Name == "removebodyparttypevariant"))
						{
							throw new XmlUnsupportedElementException(Reader);
						}
						string attribute = Reader.GetAttribute("Name");
						if (string.IsNullOrEmpty(attribute))
						{
							throw new XmlException("removebodyparttypevariant tag had no Name attribute", Reader);
						}
						if (Anatomies._BodyPartTypeTable.ContainsKey(attribute))
						{
							if (Anatomies._BodyPartTypeTable[attribute].FinalType == attribute)
							{
								throw new XmlException("cannot remove type " + attribute + " as variant", Reader);
							}
							Anatomies._BodyPartTypeTable.Remove(attribute);
						}
					}
				}
				else if (Reader.NodeType == XmlNodeType.EndElement)
				{
					break;
				}
			}
		}

		// Token: 0x060078EB RID: 30955 RVA: 0x002D98B8 File Offset: 0x002D7AB8
		private static void LoadBodyPartTypeVariantNode(XmlTextReader Reader, bool mod = false)
		{
			string attribute = Reader.GetAttribute("VariantOf");
			if (string.IsNullOrEmpty(attribute))
			{
				throw new XmlException(Reader.Name + " tag had no VariantOf attribute", Reader);
			}
			string attribute2 = Reader.GetAttribute("Type");
			if (string.IsNullOrEmpty(attribute2))
			{
				throw new XmlException(Reader.Name + " tag had no Type attribute", Reader);
			}
			BodyPartType bodyPartType;
			if (Anatomies._BodyPartTypeTable.ContainsKey(attribute2))
			{
				if (!mod)
				{
					Debug.LogError("duplicate bodyparttypevariant " + attribute2);
				}
				bodyPartType = Anatomies._BodyPartTypeTable[attribute2];
				if (bodyPartType.Type == bodyPartType.FinalType)
				{
					throw new XmlException("cannot modify type " + attribute2 + " as variant", Reader);
				}
			}
			else
			{
				bodyPartType = new BodyPartType(Anatomies.GetBodyPartTypeOrFail(attribute), attribute2);
				bodyPartType.Description = bodyPartType.Type;
				bodyPartType.Name = bodyPartType.Type.ToLower();
				Anatomies._BodyPartTypeTable.Add(bodyPartType.Type, bodyPartType);
				Anatomies._BodyPartTypeList.Add(bodyPartType);
			}
			Anatomies.LoadBodyPartInfo(bodyPartType, Reader, mod);
		}

		// Token: 0x060078EC RID: 30956 RVA: 0x002D99C0 File Offset: 0x002D7BC0
		private static void LoadBodyPartInfo(BodyPartType Entry, XmlTextReader Reader, bool mod = false)
		{
			string text = Reader.GetAttribute("Name");
			if (text != null)
			{
				Entry.Name = ((text == "") ? null : text);
			}
			text = Reader.GetAttribute("Description");
			if (text != null)
			{
				Entry.Description = ((text == "") ? null : text);
			}
			text = Reader.GetAttribute("DescriptionPrefix");
			if (text != null)
			{
				Entry.DescriptionPrefix = ((text == "") ? null : text);
			}
			text = Reader.GetAttribute("DefaultBehavior");
			if (text != null)
			{
				Entry.DefaultBehavior = ((text == "") ? null : text);
			}
			text = Reader.GetAttribute("UsuallyOn");
			if (text != null)
			{
				Entry.UsuallyOn = ((text == "") ? null : text);
			}
			text = Reader.GetAttribute("UsuallyOnVariant");
			if (text != null)
			{
				Entry.UsuallyOnVariant = ((text == "") ? null : text);
			}
			text = Reader.GetAttribute("ImpliedBy");
			if (text != null)
			{
				Entry.ImpliedBy = ((text == "") ? null : text);
			}
			text = Reader.GetAttribute("RequiresType");
			if (text != null)
			{
				Entry.RequiresType = ((text == "") ? null : text);
			}
			text = Reader.GetAttribute("LimbBlueprintProperty");
			if (text != null)
			{
				Entry.LimbBlueprintProperty = ((text == "") ? null : text);
			}
			text = Reader.GetAttribute("LimbBlueprintDefault");
			if (text != null)
			{
				Entry.LimbBlueprintDefault = ((text == "") ? null : text);
			}
			text = Reader.GetAttribute("EquipSound");
			if (text != null)
			{
				Entry.EquipSound = ((text == "") ? null : text);
			}
			text = Reader.GetAttribute("UnequipSound");
			if (text != null)
			{
				Entry.UnequipSound = ((text == "") ? null : text);
			}
			text = Reader.GetAttribute("Category");
			if (text != null)
			{
				Entry.Category = ((text == "") ? null : new int?(BodyPartCategory.GetCode(text, true)));
			}
			text = Reader.GetAttribute("Laterality");
			if (text != null)
			{
				Entry.Laterality = ((text == "") ? null : new int?(Laterality.GetCode(text)));
			}
			text = Reader.GetAttribute("ImpliedPer");
			if (text != null)
			{
				Entry.ImpliedPer = ((text == "") ? null : new int?(Anatomies.TryInt(text, "ImpliedPer attribute")));
			}
			text = Reader.GetAttribute("RequiresLaterality");
			if (text != null)
			{
				Entry.RequiresLaterality = ((text == "") ? null : new int?(Laterality.GetCode(text)));
			}
			text = Reader.GetAttribute("Mobility");
			if (text != null)
			{
				Entry.Mobility = ((text == "") ? null : new int?(Anatomies.TryInt(text, "Mobility attribute")));
			}
			text = Reader.GetAttribute("ChimeraWeight");
			if (text != null)
			{
				Entry.ChimeraWeight = ((text == "") ? null : new int?(Anatomies.TryInt(text, "ChimeraWeight attribute")));
			}
			text = Reader.GetAttribute("Appendage");
			if (text != null)
			{
				Entry.Appendage = ((text == "") ? null : new bool?(Convert.ToBoolean(text)));
			}
			text = Reader.GetAttribute("Integral");
			if (text != null)
			{
				Entry.Integral = ((text == "") ? null : new bool?(Convert.ToBoolean(text)));
			}
			text = Reader.GetAttribute("Abstract");
			if (text != null)
			{
				Entry.Abstract = ((text == "") ? null : new bool?(Convert.ToBoolean(text)));
			}
			text = Reader.GetAttribute("Mortal");
			if (text != null)
			{
				Entry.Mortal = ((text == "") ? null : new bool?(Convert.ToBoolean(text)));
			}
			text = Reader.GetAttribute("Extrinsic");
			if (text != null)
			{
				Entry.Extrinsic = ((text == "") ? null : new bool?(Convert.ToBoolean(text)));
			}
			text = Reader.GetAttribute("Plural");
			if (text != null)
			{
				Entry.Plural = ((text == "") ? null : new bool?(Convert.ToBoolean(text)));
			}
			text = Reader.GetAttribute("Mass");
			if (text != null)
			{
				Entry.Mass = ((text == "") ? null : new bool?(Convert.ToBoolean(text)));
			}
			text = Reader.GetAttribute("Contact");
			if (text != null)
			{
				Entry.Contact = ((text == "") ? null : new bool?(Convert.ToBoolean(text)));
			}
			text = Reader.GetAttribute("IgnorePosition");
			if (text != null)
			{
				Entry.IgnorePosition = ((text == "") ? null : new bool?(Convert.ToBoolean(text)));
			}
			text = Reader.GetAttribute("NoArmorAveraging");
			if (text != null)
			{
				Entry.NoArmorAveraging = ((text == "") ? null : new bool?(Convert.ToBoolean(text)));
			}
			text = Reader.GetAttribute("Branching");
			if (!string.IsNullOrEmpty(text))
			{
				string[] array = text.Split(',', StringSplitOptions.None);
				int[] array2 = new int[array.Length];
				int i = 0;
				int num = array.Length;
				while (i < num)
				{
					try
					{
						array2[i] = Laterality.GetAxisCode(array[i]);
					}
					catch (Exception ex)
					{
						throw new XmlException(ex.Message, Reader);
					}
					i++;
				}
				Entry.Branching = array2;
			}
		}

		// Token: 0x060078ED RID: 30957 RVA: 0x002D9F98 File Offset: 0x002D8198
		public static void LoadAnatomiesNode(XmlTextReader Reader, bool mod = false)
		{
			while (Reader.Read())
			{
				if (Reader.NodeType == XmlNodeType.Element)
				{
					if (!(Reader.Name == "anatomy"))
					{
						throw new XmlUnsupportedElementException(Reader);
					}
					Anatomies.LoadAnatomyNode(Reader, mod);
				}
				else if (Reader.NodeType == XmlNodeType.EndElement)
				{
					break;
				}
			}
		}

		// Token: 0x060078EE RID: 30958 RVA: 0x002D9FE4 File Offset: 0x002D81E4
		public static void LoadAnatomyNode(XmlTextReader Reader, bool mod = false)
		{
			string attribute = Reader.GetAttribute("Name");
			if (string.IsNullOrEmpty(attribute))
			{
				throw new XmlException(Reader.Name + " tag had no Name attribute", Reader);
			}
			Anatomy anatomy;
			if (Anatomies._AnatomyTable.ContainsKey(attribute))
			{
				if (!mod)
				{
					Debug.LogError("duplicate anatomy " + attribute);
				}
				anatomy = Anatomies._AnatomyTable[attribute];
			}
			else
			{
				anatomy = new Anatomy(attribute);
				Anatomies._AnatomyTable.Add(attribute, anatomy);
				Anatomies._AnatomyList.Add(anatomy);
			}
			string text = Reader.GetAttribute("Category");
			if (!string.IsNullOrEmpty(text))
			{
				anatomy.Category = new int?(BodyPartCategory.GetCode(text, true));
			}
			text = Reader.GetAttribute("BodyType");
			if (!string.IsNullOrEmpty(text))
			{
				anatomy.BodyType = text;
			}
			text = Reader.GetAttribute("BodyCategory");
			if (!string.IsNullOrEmpty(text))
			{
				anatomy.BodyCategory = new int?(BodyPartCategory.GetCode(text, true));
			}
			text = Reader.GetAttribute("BodyMobility");
			if (!string.IsNullOrEmpty(text))
			{
				anatomy.BodyMobility = new int?(Anatomies.TryInt(text, "BodyMobility attribute"));
			}
			text = Reader.GetAttribute("ThrownWeapon");
			if (!string.IsNullOrEmpty(text))
			{
				anatomy.ThrownWeapon = text;
			}
			text = Reader.GetAttribute("FloatingNearby");
			if (!string.IsNullOrEmpty(text))
			{
				anatomy.FloatingNearby = text;
			}
			while (Reader.Read())
			{
				if (Reader.NodeType == XmlNodeType.Element)
				{
					if (!(Reader.Name == "part"))
					{
						throw new XmlUnsupportedElementException(Reader);
					}
					Anatomies.LoadAnatomyPartNode(anatomy, null, Reader, mod);
				}
				else if (Reader.NodeType == XmlNodeType.EndElement)
				{
					break;
				}
			}
		}

		// Token: 0x060078EF RID: 30959 RVA: 0x002DA170 File Offset: 0x002D8370
		public static void LoadAnatomyPartNode(Anatomy anatomy, AnatomyPart parentPart, XmlTextReader Reader, bool mod = false)
		{
			string attribute = Reader.GetAttribute("Type");
			if (string.IsNullOrEmpty(attribute))
			{
				throw new XmlException(Reader.Name + " tag had no Type attribute", Reader);
			}
			BodyPartType bodyPartType = Anatomies.GetBodyPartType(attribute);
			if (bodyPartType == null)
			{
				throw new XmlException("unknown bodyparttype " + attribute, Reader);
			}
			AnatomyPart anatomyPart = new AnatomyPart(bodyPartType);
			string text = Reader.GetAttribute("SupportsDependent");
			if (text != null)
			{
				anatomyPart.SupportsDependent = ((text == "") ? null : text);
			}
			text = Reader.GetAttribute("DependsOn");
			if (text != null)
			{
				anatomyPart.DependsOn = ((text == "") ? null : text);
			}
			text = Reader.GetAttribute("RequiresType");
			if (text != null)
			{
				anatomyPart.DependsOn = ((text == "") ? null : text);
			}
			text = Reader.GetAttribute("DefaultBehavior");
			if (text != null)
			{
				anatomyPart.DefaultBehavior = ((text == "") ? null : text);
			}
			text = Reader.GetAttribute("Category");
			if (!string.IsNullOrEmpty(text))
			{
				anatomyPart.Category = new int?(BodyPartCategory.GetCode(text, true));
			}
			text = Reader.GetAttribute("Laterality");
			if (!string.IsNullOrEmpty(text))
			{
				anatomyPart.Laterality = new int?(Laterality.GetCode(text));
			}
			text = Reader.GetAttribute("RequiresLaterality");
			if (!string.IsNullOrEmpty(text))
			{
				anatomyPart.RequiresLaterality = new int?(Laterality.GetCode(text));
			}
			text = Reader.GetAttribute("Mobility");
			if (!string.IsNullOrEmpty(text))
			{
				anatomyPart.Mobility = new int?(Anatomies.TryInt(text, "Mobility attribute"));
			}
			text = Reader.GetAttribute("Integral");
			if (!string.IsNullOrEmpty(text))
			{
				anatomyPart.Integral = new bool?(Convert.ToBoolean(text));
			}
			text = Reader.GetAttribute("Mortal");
			if (!string.IsNullOrEmpty(text))
			{
				anatomyPart.Mortal = new bool?(Convert.ToBoolean(text));
			}
			text = Reader.GetAttribute("Abstract");
			if (!string.IsNullOrEmpty(text))
			{
				anatomyPart.Abstract = new bool?(Convert.ToBoolean(text));
			}
			text = Reader.GetAttribute("Extrinsic");
			if (!string.IsNullOrEmpty(text))
			{
				anatomyPart.Extrinsic = new bool?(Convert.ToBoolean(text));
			}
			text = Reader.GetAttribute("Plural");
			if (!string.IsNullOrEmpty(text))
			{
				anatomyPart.Plural = new bool?(Convert.ToBoolean(text));
			}
			text = Reader.GetAttribute("Mass");
			if (!string.IsNullOrEmpty(text))
			{
				anatomyPart.Mass = new bool?(Convert.ToBoolean(text));
			}
			text = Reader.GetAttribute("Contact");
			if (!string.IsNullOrEmpty(text))
			{
				anatomyPart.Contact = new bool?(Convert.ToBoolean(text));
			}
			text = Reader.GetAttribute("IgnorePosition");
			if (!string.IsNullOrEmpty(text))
			{
				anatomyPart.IgnorePosition = new bool?(Convert.ToBoolean(text));
			}
			if (parentPart != null)
			{
				parentPart.Subparts.Add(anatomyPart);
			}
			else
			{
				anatomy.Parts.Add(anatomyPart);
			}
			if (Reader.IsEmptyElement)
			{
				return;
			}
			while (Reader.Read())
			{
				if (Reader.NodeType == XmlNodeType.Element)
				{
					if (!(Reader.Name == "part"))
					{
						throw new XmlUnsupportedElementException(Reader);
					}
					Anatomies.LoadAnatomyPartNode(anatomy, anatomyPart, Reader, mod);
				}
				else if (Reader.NodeType == XmlNodeType.EndElement)
				{
					break;
				}
			}
		}

		// Token: 0x060078F0 RID: 30960 RVA: 0x002DA488 File Offset: 0x002D8688
		private static int TryInt(string Spec, string What)
		{
			try
			{
				return Convert.ToInt32(Spec);
			}
			catch
			{
				Debug.LogError("Error in " + What + ": " + Spec);
			}
			return 0;
		}

		// Token: 0x060078F1 RID: 30961 RVA: 0x002DA4CC File Offset: 0x002D86CC
		public static Dictionary<BodyPartType, int> GetBodyPartTypeSelector(bool IncludeVariants = true, bool? RequireAppendage = true, bool? RequireAbstract = false, bool RequireLiveCategory = true, int[] IncludeCategories = null, int[] ExcludeCategories = null, bool UseChimeraWeight = false)
		{
			Dictionary<BodyPartType, int> dictionary = new Dictionary<BodyPartType, int>();
			int i = 0;
			int count = Anatomies.BodyPartTypeList.Count;
			while (i < count)
			{
				BodyPartType bodyPartType = Anatomies.BodyPartTypeList[i];
				if (IncludeVariants || !(bodyPartType.FinalType != bodyPartType.Type))
				{
					if (RequireAppendage != null)
					{
						bool valueOrDefault = bodyPartType.Appendage.GetValueOrDefault();
						bool? flag = RequireAppendage;
						if (!((valueOrDefault == flag.GetValueOrDefault()) & (flag != null)))
						{
							goto IL_0152;
						}
					}
					if (RequireAbstract != null)
					{
						bool valueOrDefault2 = bodyPartType.Abstract.GetValueOrDefault();
						bool? flag = RequireAbstract;
						if (!((valueOrDefault2 == flag.GetValueOrDefault()) & (flag != null)))
						{
							goto IL_0152;
						}
					}
					if ((!RequireLiveCategory || BodyPartCategory.IsLiveCategory(bodyPartType.Category ?? 1)) && (IncludeCategories == null || Array.IndexOf<int>(IncludeCategories, bodyPartType.Category ?? 1) != -1) && (ExcludeCategories == null || Array.IndexOf<int>(ExcludeCategories, bodyPartType.Category ?? 1) != -1))
					{
						int num;
						if (UseChimeraWeight && bodyPartType.ChimeraWeight != null)
						{
							num = bodyPartType.ChimeraWeight ?? 1;
						}
						else
						{
							num = 1;
						}
						if (num > 0)
						{
							dictionary.Add(bodyPartType, num);
						}
					}
				}
				IL_0152:
				i++;
			}
			if (IncludeVariants)
			{
				Dictionary<BodyPartType, int> dictionary2 = new Dictionary<BodyPartType, int>(dictionary);
				foreach (BodyPartType bodyPartType2 in dictionary2.Keys)
				{
					if (bodyPartType2.FinalType == bodyPartType2.Type)
					{
						foreach (KeyValuePair<BodyPartType, int> keyValuePair in dictionary2)
						{
							if (keyValuePair.Key != bodyPartType2 && keyValuePair.Key.FinalType == bodyPartType2.Type)
							{
								Dictionary<BodyPartType, int> dictionary3 = dictionary;
								BodyPartType bodyPartType3 = bodyPartType2;
								dictionary3[bodyPartType3] += keyValuePair.Value;
							}
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060078F2 RID: 30962 RVA: 0x002DA724 File Offset: 0x002D8924
		public static BodyPartType GetRandomBodyPartType(bool IncludeVariants = true, bool? RequireAppendage = true, bool? RequireAbstract = false, bool RequireLiveCategory = true, int[] IncludeCategories = null, int[] ExcludeCategories = null, bool UseChimeraWeight = false)
		{
			return Anatomies.GetBodyPartTypeSelector(IncludeVariants, RequireAppendage, RequireAbstract, RequireLiveCategory, IncludeCategories, ExcludeCategories, UseChimeraWeight).GetRandomElement<BodyPartType>(null);
		}

		// Token: 0x060078F3 RID: 30963 RVA: 0x002DA73C File Offset: 0x002D893C
		public static List<BodyPartType> FindUsualChildBodyPartTypes(BodyPartType ParentType)
		{
			List<BodyPartType> list = null;
			int i = 0;
			int count = Anatomies.BodyPartTypeList.Count;
			while (i < count)
			{
				BodyPartType bodyPartType = Anatomies.BodyPartTypeList[i];
				bool flag = false;
				if (!string.IsNullOrEmpty(bodyPartType.UsuallyOnVariant))
				{
					if (bodyPartType.UsuallyOnVariant == ParentType.Type)
					{
						int? num = bodyPartType.Category;
						int? num2 = ParentType.Category;
						if ((num.GetValueOrDefault() == num2.GetValueOrDefault()) & (num != null == (num2 != null)))
						{
							flag = true;
						}
					}
				}
				else if (!string.IsNullOrEmpty(bodyPartType.UsuallyOn) && bodyPartType.UsuallyOn == ParentType.Type)
				{
					int? num2 = bodyPartType.Category;
					int? num = ParentType.Category;
					if ((num2.GetValueOrDefault() == num.GetValueOrDefault()) & (num2 != null == (num != null)))
					{
						flag = true;
					}
				}
				if (flag)
				{
					if (list == null)
					{
						list = new List<BodyPartType> { bodyPartType };
					}
					else
					{
						bool flag2 = false;
						using (List<BodyPartType>.Enumerator enumerator = list.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								if (enumerator.Current.FinalType == bodyPartType.FinalType)
								{
									flag2 = true;
									break;
								}
							}
						}
						if (!flag2)
						{
							list.Add(bodyPartType);
						}
					}
				}
				i++;
			}
			return list;
		}

		// Token: 0x04003706 RID: 14086
		[ModSensitiveStaticCache(false)]
		private static Dictionary<string, BodyPartType> _BodyPartTypeTable;

		// Token: 0x04003707 RID: 14087
		[ModSensitiveStaticCache(false)]
		private static List<BodyPartType> _BodyPartTypeList;

		// Token: 0x04003708 RID: 14088
		[ModSensitiveStaticCache(false)]
		private static Dictionary<string, Anatomy> _AnatomyTable;

		// Token: 0x04003709 RID: 14089
		[ModSensitiveStaticCache(false)]
		private static List<Anatomy> _AnatomyList;
	}
}
