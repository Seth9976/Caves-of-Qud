using System;
using System.Collections.Generic;
using XRL.UI.Framework;

namespace XRL.CharacterBuilds.Qud
{
	// Token: 0x020005AC RID: 1452
	public class QudPregenModule : QudEmbarkBuilderModule<QudPregenModuleData>
	{
		// Token: 0x06003DE1 RID: 15841 RVA: 0x0012FD20 File Offset: 0x0012DF20
		public override bool shouldBeEditable()
		{
			return this.builder.IsEditableGameMode();
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06003DE2 RID: 15842 RVA: 0x0012FD2D File Offset: 0x0012DF2D
		public override Dictionary<string, Action<XmlDataHelper>> XmlNodes
		{
			get
			{
				Dictionary<string, Action<XmlDataHelper>> xmlNodes = base.XmlNodes;
				xmlNodes.Add("pregens", delegate(XmlDataHelper xml)
				{
					xml.HandleNodes(this.XmlNodeHandlers, null);
				});
				return xmlNodes;
			}
		}

		// Token: 0x17000653 RID: 1619
		// (get) Token: 0x06003DE3 RID: 15843 RVA: 0x0012FD4C File Offset: 0x0012DF4C
		public Dictionary<string, Action<XmlDataHelper>> XmlNodeHandlers
		{
			get
			{
				return new Dictionary<string, Action<XmlDataHelper>>
				{
					{
						"pregen",
						delegate(XmlDataHelper xml)
						{
							string attribute = xml.GetAttribute("Name");
							if (!this.pregens.TryGetValue(attribute, out this.currentReadingQudPregenData))
							{
								this.currentReadingQudPregenData = new QudPregenModule.QudPregenData
								{
									Name = attribute
								};
								this.pregens.Add(attribute, this.currentReadingQudPregenData);
							}
							this.currentReadingQudPregenData.Name = xml.GetAttribute("Name");
							this.currentReadingQudPregenData.Id = attribute;
							this.currentReadingQudPregenData.Genotype = xml.GetAttribute("Genotype");
							this.currentReadingQudPregenData.Tile = xml.GetAttribute("Tile");
							this.currentReadingQudPregenData.Foreground = xml.GetAttribute("Foreground");
							this.currentReadingQudPregenData.Background = xml.GetAttribute("Background");
							this.currentReadingQudPregenData.Detail = xml.GetAttribute("Detail");
							xml.HandleNodes(this.XmlNodeHandlers, null);
							this.currentReadingQudPregenData = null;
						}
					},
					{
						"code",
						delegate(XmlDataHelper xml)
						{
							this.currentReadingQudPregenData.Code = xml.GetTextNode();
						}
					},
					{
						"description",
						delegate(XmlDataHelper xml)
						{
							this.currentReadingQudPregenData.Description = xml.GetTextNode();
						}
					}
				};
			}
		}

		// Token: 0x06003DE4 RID: 15844 RVA: 0x0012FDA3 File Offset: 0x0012DFA3
		public override bool shouldBeEnabled()
		{
			EmbarkBuilder builder = this.builder;
			string text;
			if (builder == null)
			{
				text = null;
			}
			else
			{
				QudChartypeModule module = builder.GetModule<QudChartypeModule>();
				if (module == null)
				{
					text = null;
				}
				else
				{
					QudChartypeModuleData data = module.data;
					text = ((data != null) ? data.type : null);
				}
			}
			return text == "Pregen";
		}

		// Token: 0x06003DE5 RID: 15845 RVA: 0x0012FDD9 File Offset: 0x0012DFD9
		public void SelectPregen(string pregen)
		{
			this.setData(new QudPregenModuleData(pregen));
			this.builder.InitModulesFromCode(this.pregens[pregen].Code, false);
			this.builder.advanceToSummary();
		}

		// Token: 0x06003DE6 RID: 15846 RVA: 0x0012FE0F File Offset: 0x0012E00F
		public override void InitFromSeed(string seed)
		{
		}

		// Token: 0x06003DE7 RID: 15847 RVA: 0x0012FE14 File Offset: 0x0012E014
		public override object handleBootEvent(string id, XRLGame game, EmbarkInfo info, object element = null)
		{
			if (id == QudGameBootModule.BOOTEVENT_BOOTPLAYERTILE && base.data.Pregen != null)
			{
				return this.pregens[base.data.Pregen].Tile;
			}
			if (id == QudGameBootModule.BOOTEVENT_BOOTPLAYERTILEFOREGROUND && base.data.Pregen != null)
			{
				return this.pregens[base.data.Pregen].Foreground;
			}
			if (id == QudGameBootModule.BOOTEVENT_BOOTPLAYERTILEBACKGROUND && base.data.Pregen != null)
			{
				return this.pregens[base.data.Pregen].Background;
			}
			if (id == QudGameBootModule.BOOTEVENT_BOOTPLAYERTILEDETAIL && base.data.Pregen != null)
			{
				return this.pregens[base.data.Pregen].Detail;
			}
			return base.handleBootEvent(id, game, info, element);
		}

		// Token: 0x04001EB4 RID: 7860
		public Dictionary<string, QudPregenModule.QudPregenData> pregens = new Dictionary<string, QudPregenModule.QudPregenData>();

		// Token: 0x04001EB5 RID: 7861
		private QudPregenModule.QudPregenData currentReadingQudPregenData;

		// Token: 0x0200199E RID: 6558
		public class QudPregenData : FrameworkDataElement
		{
			// Token: 0x0400666C RID: 26220
			public string Name;

			// Token: 0x0400666D RID: 26221
			public string Genotype;

			// Token: 0x0400666E RID: 26222
			public string Tile;

			// Token: 0x0400666F RID: 26223
			public string Code;

			// Token: 0x04006670 RID: 26224
			public string Foreground;

			// Token: 0x04006671 RID: 26225
			public string Background;

			// Token: 0x04006672 RID: 26226
			public string Detail;
		}
	}
}
