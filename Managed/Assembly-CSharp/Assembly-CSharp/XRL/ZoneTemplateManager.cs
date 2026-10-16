using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using XRL.UI;

namespace XRL
{
	// Token: 0x02000548 RID: 1352
	[HasModSensitiveStaticCache]
	public static class ZoneTemplateManager
	{
		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060039A4 RID: 14756 RVA: 0x0011CE11 File Offset: 0x0011B011
		public static Dictionary<string, ZoneTemplate> Templates
		{
			get
			{
				ZoneTemplateManager.CheckInit();
				return ZoneTemplateManager._Templates;
			}
		}

		// Token: 0x060039A5 RID: 14757 RVA: 0x0011CE1D File Offset: 0x0011B01D
		public static bool HasTemplates(string TemplatesName)
		{
			return ZoneTemplateManager.Templates.ContainsKey(TemplatesName);
		}

		// Token: 0x060039A6 RID: 14758 RVA: 0x0011CE2A File Offset: 0x0011B02A
		[PreGameCacheInit]
		public static void CheckInit()
		{
			if (ZoneTemplateManager._Templates == null)
			{
				Loading.LoadTask("Loading ZoneTemplates.xml", new Action(ZoneTemplateManager.LoadTemplates), true);
			}
		}

		// Token: 0x060039A7 RID: 14759 RVA: 0x0011CE4C File Offset: 0x0011B04C
		private static void LoadTemplates()
		{
			ZoneTemplateManager._Templates = new Dictionary<string, ZoneTemplate>();
			foreach (XmlDataHelper xmlDataHelper in DataManager.YieldXMLStreamsWithRoot("ZoneTemplates", true, true))
			{
				try
				{
					while (xmlDataHelper.Read())
					{
						if (xmlDataHelper.NodeType == XmlNodeType.Element && xmlDataHelper.Name == "zonetemplate")
						{
							ZoneTemplate zoneTemplate = ZoneTemplateManager.LoadTemplate(xmlDataHelper);
							if (ZoneTemplateManager._Templates.ContainsKey(zoneTemplate.Name))
							{
								ZoneTemplateManager._Templates[zoneTemplate.Name] = zoneTemplate;
							}
							else
							{
								ZoneTemplateManager._Templates.Add(zoneTemplate.Name, zoneTemplate);
							}
						}
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogPotentialModError(xmlDataHelper.modInfo, ex);
				}
			}
		}

		// Token: 0x060039A8 RID: 14760 RVA: 0x0011CF24 File Offset: 0x0011B124
		private static ZoneTemplate LoadTemplate(XmlReader Reader)
		{
			ZoneTemplate zoneTemplate = new ZoneTemplate();
			zoneTemplate.Name = Reader.GetAttribute("Name");
			zoneTemplate.RegionSize = ZoneTemplateManager.ReadIntAttribte(Reader, "RegionSize", 100);
			zoneTemplate.Root = new ZTGroupNode();
			zoneTemplate.GlobalRoot = new ZTGroupNode();
			zoneTemplate.SingleRoot = new ZTGroupNode();
			ZoneTemplateNode zoneTemplateNode = null;
			if (Reader.NodeType == XmlNodeType.EndElement || Reader.IsEmptyElement)
			{
				return zoneTemplate;
			}
			while (Reader.Read())
			{
				if (Reader.NodeType == XmlNodeType.Element && Reader.Name == "eachregion")
				{
					zoneTemplateNode = zoneTemplate.Root;
					zoneTemplate.Root.Style = ((Reader.GetAttribute("Style") == null) ? "pickeach" : Reader.GetAttribute("Style"));
				}
				else if (Reader.NodeType == XmlNodeType.Element && Reader.Name == "global")
				{
					zoneTemplateNode = zoneTemplate.GlobalRoot;
					zoneTemplate.GlobalRoot.Style = ((Reader.GetAttribute("Style") == null) ? "pickeach" : Reader.GetAttribute("Style"));
				}
				else if (Reader.NodeType == XmlNodeType.Element && Reader.Name == "single")
				{
					zoneTemplateNode = zoneTemplate.SingleRoot;
					zoneTemplate.SingleRoot.Style = ((Reader.GetAttribute("Style") == null) ? "pickeach" : Reader.GetAttribute("Style"));
				}
				else if (Reader.NodeType == XmlNodeType.Element)
				{
					ZoneTemplateNode zoneTemplateNode2 = ZoneTemplateManager.LoadNode(Reader);
					if (zoneTemplateNode == null)
					{
						Debug.LogError("zone template error, no context specified. Things should be inside global or eachregion tags.");
					}
					else
					{
						zoneTemplateNode.Children.Add(zoneTemplateNode2);
					}
				}
				if (Reader.NodeType == XmlNodeType.EndElement && Reader.Name == "zonetemplate")
				{
					return zoneTemplate;
				}
			}
			return zoneTemplate;
		}

		// Token: 0x060039A9 RID: 14761 RVA: 0x0011D0D8 File Offset: 0x0011B2D8
		private static int ReadIntAttribte(XmlReader Reader, string Attribute, int Default)
		{
			string attribute = Reader.GetAttribute(Attribute);
			if (string.IsNullOrEmpty(attribute))
			{
				return Default;
			}
			return Convert.ToInt32(attribute);
		}

		// Token: 0x060039AA RID: 14762 RVA: 0x0011D100 File Offset: 0x0011B300
		private static ZoneTemplateNode LoadNode(XmlReader Reader)
		{
			ZoneTemplateNode zoneTemplateNode = null;
			if (Reader.Name == "exit")
			{
				zoneTemplateNode = new ZTExitNode();
			}
			if (Reader.Name == "group")
			{
				zoneTemplateNode = new ZTGroupNode();
			}
			if (Reader.Name == "builder")
			{
				zoneTemplateNode = new ZTBuilderNode();
			}
			if (Reader.Name == "population")
			{
				zoneTemplateNode = new ZTPopulatonNode();
			}
			if (Reader.Name == "graph")
			{
				zoneTemplateNode = new ZTGraphNode();
			}
			if (Reader.Name == "cellfilterout")
			{
				zoneTemplateNode = new ZTCellFilterOutNode();
			}
			if (Reader.Name == "branch")
			{
				zoneTemplateNode = new ZTBranchNode();
			}
			if (Reader.Name.Length == 1)
			{
				zoneTemplateNode = new ZTGraphElementNode();
			}
			if (zoneTemplateNode == null)
			{
				Debug.LogError("Unexpected node type: " + Reader.Name);
			}
			zoneTemplateNode.Name = Reader.Name;
			zoneTemplateNode.Criteria = Reader.GetAttribute("Criteria");
			zoneTemplateNode.Filter = Reader.GetAttribute("Filter");
			zoneTemplateNode.Hint = Reader.GetAttribute("Hint");
			zoneTemplateNode.Weight = ((Reader.GetAttribute("Weight") == null) ? 1 : Convert.ToInt32(Reader.GetAttribute("Weight")));
			zoneTemplateNode.Style = ((Reader.GetAttribute("Style") == null) ? "pickeach" : Reader.GetAttribute("Style"));
			zoneTemplateNode.Chance = Reader.GetAttribute("Chance");
			zoneTemplateNode.MaxApplications = ZoneTemplateManager.ReadIntAttribte(Reader, "MaxApplications", 0);
			zoneTemplateNode.Load(Reader);
			if (Reader.NodeType == XmlNodeType.EndElement || Reader.IsEmptyElement)
			{
				return zoneTemplateNode;
			}
			while (Reader.Read())
			{
				if (Reader.NodeType == XmlNodeType.Element)
				{
					ZoneTemplateNode zoneTemplateNode2 = ZoneTemplateManager.LoadNode(Reader);
					zoneTemplateNode.Children.Add(zoneTemplateNode2);
				}
				else if (Reader.NodeType == XmlNodeType.EndElement)
				{
					return zoneTemplateNode;
				}
			}
			return null;
		}

		// Token: 0x04001D08 RID: 7432
		[ModSensitiveStaticCache(false)]
		public static Dictionary<string, ZoneTemplate> _Templates;
	}
}
