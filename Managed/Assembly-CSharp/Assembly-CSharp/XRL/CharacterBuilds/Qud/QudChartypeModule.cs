using System;
using System.Collections.Generic;
using System.Globalization;
using Platform.IO;
using UnityEngine;
using XRL.Core;
using XRL.UI;

namespace XRL.CharacterBuilds.Qud
{
	// Token: 0x0200059B RID: 1435
	public class QudChartypeModule : QudEmbarkBuilderModule<QudChartypeModuleData>
	{
		// Token: 0x06003D63 RID: 15715 RVA: 0x0012CC29 File Offset: 0x0012AE29
		public override bool IncludeInBuildCodes()
		{
			QudChartypeModuleData data = base.data;
			return ((data != null) ? data.type : null) == "Pregen";
		}

		// Token: 0x06003D64 RID: 15716 RVA: 0x0012CC47 File Offset: 0x0012AE47
		public override bool shouldBeEnabled()
		{
			return this.builder.GetModule<QudGamemodeModule>().IsDataValid();
		}

		// Token: 0x06003D65 RID: 15717 RVA: 0x0012CC5C File Offset: 0x0012AE5C
		public override bool shouldBeEditable()
		{
			if (this.builder.IsEditableGameMode())
			{
				EmbarkBuilder builder = this.builder;
				string text;
				if (builder == null)
				{
					text = null;
				}
				else
				{
					QudGamemodeModule module = builder.GetModule<QudGamemodeModule>();
					if (module == null)
					{
						text = null;
					}
					else
					{
						QudGamemodeModuleData data = module.data;
						text = ((data != null) ? data.Mode : null);
					}
				}
				return text != "Tutorial";
			}
			return false;
		}

		// Token: 0x06003D66 RID: 15718 RVA: 0x0012CCAC File Offset: 0x0012AEAC
		public void selectType(string type)
		{
			if (type == "Random")
			{
				this.builder.SkippingUIUpdates = true;
			}
			try
			{
				this.setData(new QudChartypeModuleData(type));
				this.builder.ResetForwardViews();
			}
			finally
			{
				this.builder.SkippingUIUpdates = false;
			}
			if (!(type == "Daily"))
			{
				if (type == "Last")
				{
					try
					{
						string text = File.ReadAllText(DataManager.SavePath("lastcharacter.txt"));
						this.builder.InitModulesFromCode(text, false);
						this.builder.advanceToViewId("Chargen/BuildSummary");
						return;
					}
					catch (Exception ex)
					{
						MetricsManager.LogError("Error loading lastcharacter.txt", ex);
						Popup.ShowAsync("There is no valid last character to use.", true, true, true, true, false);
						return;
					}
				}
				if (type == "Random")
				{
					EmbarkBuilderModuleWindowDescriptor embarkBuilderModuleWindowDescriptor = this.builder.activeWindow;
					for (;;)
					{
						try
						{
							this.builder.SkippingUIUpdates = true;
							this.builder.advance(false, false, true);
						}
						finally
						{
							this.builder.SkippingUIUpdates = false;
						}
						if (this.builder.activeWindow == embarkBuilderModuleWindowDescriptor)
						{
							break;
						}
						embarkBuilderModuleWindowDescriptor = this.builder.activeWindow;
						this.builder.SkippingUIUpdates = true;
						try
						{
							embarkBuilderModuleWindowDescriptor.window.RandomSelectionNoUI();
						}
						finally
						{
							this.builder.SkippingUIUpdates = false;
						}
						EmbarkBuilder builder = this.builder;
						string text2;
						if (builder == null)
						{
							text2 = null;
						}
						else
						{
							EmbarkBuilderModuleWindowDescriptor activeWindow = builder.activeWindow;
							text2 = ((activeWindow != null) ? activeWindow.viewID : null);
						}
						if (!(text2 != "Chargen/BuildSummary"))
						{
							goto IL_01B7;
						}
					}
					if (this.builder.activeWindow != null)
					{
						this.builder.ShowWindow(this.builder.activeWindow);
						EmbarkBuilderModuleWindowDescriptor activeWindow2 = this.builder.activeWindow;
						if (activeWindow2 != null)
						{
							AbstractBuilderModuleWindowBase window = activeWindow2.window;
							if (window != null)
							{
								window.AfterShow(this.builder.activeWindow);
							}
						}
					}
					IL_01B7:
					EmbarkBuilder builder2 = this.builder;
					string text3;
					if (builder2 == null)
					{
						text3 = null;
					}
					else
					{
						EmbarkBuilderModuleWindowDescriptor activeWindow3 = builder2.activeWindow;
						text3 = ((activeWindow3 != null) ? activeWindow3.viewID : null);
					}
					if (text3 == "Chargen/BuildSummary")
					{
						this.builder.ShowWindow(this.builder.activeWindow);
						EmbarkBuilderModuleWindowDescriptor activeWindow4 = this.builder.activeWindow;
						if (activeWindow4 == null)
						{
							return;
						}
						AbstractBuilderModuleWindowBase window2 = activeWindow4.window;
						if (window2 == null)
						{
							return;
						}
						window2.AfterShow(this.builder.activeWindow);
						return;
					}
				}
				else
				{
					this.builder.advance(false, false, false);
				}
			}
		}

		// Token: 0x06003D67 RID: 15719 RVA: 0x0012CF20 File Offset: 0x0012B120
		public string getSelectedType()
		{
			QudChartypeModuleData data = base.data;
			if (data == null)
			{
				return null;
			}
			return data.type;
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06003D68 RID: 15720 RVA: 0x0012CF33 File Offset: 0x0012B133
		public override Dictionary<string, Action<XmlDataHelper>> XmlNodes
		{
			get
			{
				Dictionary<string, Action<XmlDataHelper>> xmlNodes = base.XmlNodes;
				xmlNodes.Add("types", new Action<XmlDataHelper>(this.HandleTypesNode));
				return xmlNodes;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06003D69 RID: 15721 RVA: 0x0012CF54 File Offset: 0x0012B154
		public Dictionary<string, Action<XmlDataHelper>> XmlTypesNodes
		{
			get
			{
				return new Dictionary<string, Action<XmlDataHelper>>
				{
					{
						"type",
						new Action<XmlDataHelper>(this.HandleTypeNode)
					},
					{
						"icon",
						new Action<XmlDataHelper>(this.HandleTypeIconNode)
					},
					{
						"description",
						new Action<XmlDataHelper>(this.HandleTypeDescriptionNode)
					}
				};
			}
		}

		// Token: 0x06003D6A RID: 15722 RVA: 0x0012CFAB File Offset: 0x0012B1AB
		public void HandleTypesNode(XmlDataHelper xml)
		{
			xml.HandleNodes(this.XmlTypesNodes, null);
		}

		// Token: 0x06003D6B RID: 15723 RVA: 0x0012CFBC File Offset: 0x0012B1BC
		public override void bootGame(XRLGame game, EmbarkInfo info)
		{
			if (this.getSelectedType() == "Daily")
			{
				int dayOfYear = new CultureInfo("en-US").Calendar.GetDayOfYear(DateTime.Now);
				XRLCore.Core.Game.SetStringGameState("leaderboardMode", "daily:" + DateTime.Now.Year.ToString() + ":" + dayOfYear.ToString());
				Debug.Log(">>> " + XRLCore.Core.Game.GetStringGameState("leaderboardMode", ""));
			}
		}

		// Token: 0x06003D6C RID: 15724 RVA: 0x0012D060 File Offset: 0x0012B260
		protected void HandleTypeNode(XmlDataHelper xml)
		{
			string attribute = xml.GetAttribute("ID");
			if (!this.GameTypes.TryGetValue(attribute, out this.CurrentReadingGameTypeDescriptor))
			{
				this.CurrentReadingGameTypeDescriptor = new QudChartypeModule.GameTypeDescriptor
				{
					ID = attribute
				};
				this.GameTypes.Add(attribute, this.CurrentReadingGameTypeDescriptor);
			}
			this.CurrentReadingGameTypeDescriptor.Title = xml.GetAttribute("Title");
			xml.HandleNodes(this.XmlTypesNodes, null);
			this.CurrentReadingGameTypeDescriptor = null;
		}

		// Token: 0x06003D6D RID: 15725 RVA: 0x0012D0DC File Offset: 0x0012B2DC
		protected void HandleTypeIconNode(XmlDataHelper xml)
		{
			this.CurrentReadingGameTypeDescriptor.IconTile = xml.GetAttribute("Tile");
			this.CurrentReadingGameTypeDescriptor.IconDetail = xml.GetAttributeString("Detail", "W", false);
			this.CurrentReadingGameTypeDescriptor.IconForeground = xml.GetAttributeString("Foreground", "y", false);
			xml.DoneWithElement();
		}

		// Token: 0x06003D6E RID: 15726 RVA: 0x0012D13D File Offset: 0x0012B33D
		protected void HandleTypeDescriptionNode(XmlDataHelper xml)
		{
			this.CurrentReadingGameTypeDescriptor.Description = xml.GetTextNode();
		}

		// Token: 0x06003D6F RID: 15727 RVA: 0x0012D150 File Offset: 0x0012B350
		public override void InitFromSeed(string seed)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001E7B RID: 7803
		public Dictionary<string, QudChartypeModule.GameTypeDescriptor> GameTypes = new Dictionary<string, QudChartypeModule.GameTypeDescriptor>();

		// Token: 0x04001E7C RID: 7804
		protected QudChartypeModule.GameTypeDescriptor CurrentReadingGameTypeDescriptor;

		// Token: 0x02001994 RID: 6548
		public class GameTypeDescriptor
		{
			// Token: 0x0400664C RID: 26188
			public string ID;

			// Token: 0x0400664D RID: 26189
			public string Title;

			// Token: 0x0400664E RID: 26190
			public string IconTile;

			// Token: 0x0400664F RID: 26191
			public string IconForeground;

			// Token: 0x04006650 RID: 26192
			public string IconDetail;

			// Token: 0x04006651 RID: 26193
			public string Description;
		}
	}
}
