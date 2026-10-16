using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XRL;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World;
using XRL.World.Parts;
using XRL.World.Tinkering;

namespace Qud.UI
{
	// Token: 0x0200047C RID: 1148
	public class TinkeringDetailsLine : MonoBehaviour, IFrameworkControl, IFrameworkControlSubcontexts, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
	{
		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06002BCC RID: 11212 RVA: 0x000EA15A File Offset: 0x000E835A
		public bool selected
		{
			get
			{
				TinkeringDetailsLine.Context context = this.context;
				return context != null && context.IsActive(true);
			}
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x000EA16E File Offset: 0x000E836E
		public NavigationContext GetNavigationContext()
		{
			return this.context;
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x000EA178 File Offset: 0x000E8378
		public void SetupContexts(ScrollChildContext scrollContext)
		{
			scrollContext.proxyTo = this.context;
			TinkeringDetailsLine.Context context = this.context;
			if (context.axisHandlers == null)
			{
				context.axisHandlers = new Dictionary<InputAxisTypes, Action>();
			}
			context = this.context;
			if (context.commandHandlers == null)
			{
				context.commandHandlers = new Dictionary<string, Action>();
			}
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x000EA1C5 File Offset: 0x000E83C5
		public void LateUpdate()
		{
			this.wasSelected = this.selected;
			this.wasHighlighted = this.isHighlighted;
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x000EA1E0 File Offset: 0x000E83E0
		public void setData(FrameworkDataElement data)
		{
			TinkeringLineData tinkeringLineData = data as TinkeringLineData;
			if (tinkeringLineData != null)
			{
				if (tinkeringLineData.category)
				{
					base.gameObject.SetActive(false);
					return;
				}
				base.gameObject.SetActive(true);
				this.context.data = tinkeringLineData;
				this.text.SetText(tinkeringLineData.data.DisplayName);
				if (tinkeringLineData.data.Type == "Build")
				{
					this.icon.gameObject.SetActive(true);
					if (!TinkeringDetailsLine.sampleObjects.ContainsKey(tinkeringLineData.data.Blueprint))
					{
						global::XRL.World.GameObject gameObject = global::XRL.World.GameObject.CreateSample(tinkeringLineData.data.Blueprint);
						TinkeringDetailsLine.sampleObjects.Add(tinkeringLineData.data.Blueprint, gameObject);
					}
					this.icon.FromRenderable(TinkeringDetailsLine.sampleObjects[tinkeringLineData.data.Blueprint].RenderForUI(null, true), true);
					this.descriptionText.SetText(tinkeringLineData.data.UnclippedDescription);
					if (tinkeringLineData.cost != null && tinkeringLineData.data.Type == "Build")
					{
						this.modBitCostText.SetText("{{K|| Bit Cost |}}\n" + tinkeringLineData.costString);
					}
					if (tinkeringLineData.cost != null && tinkeringLineData.data.Type == "Mod")
					{
						this.modBitCostText.SetText("{{K || Bit Cost |}}\n" + tinkeringLineData.costString);
					}
					if (tinkeringLineData.cost != null)
					{
						TinkerData data2 = tinkeringLineData.data;
						if (data2 != null && !data2.Ingredient.IsNullOrEmpty())
						{
							bool flag = false;
							string[] array = tinkeringLineData.data.Ingredient.Split(',', StringSplitOptions.None);
							int num = 14;
							if (array.Length > 1)
							{
								num--;
							}
							this.modBitCostText.SetText(this.modBitCostText.text + "\n\n{{K|| Ingredients |}}");
							foreach (string text in array)
							{
								if (The.Player.Inventory.FindObjectByBlueprint(text) != null)
								{
									if (flag)
									{
										this.modBitCostText.SetText(this.modBitCostText.text + "\n-or-");
									}
									this.modBitCostText.SetText(this.modBitCostText.text + string.Format("\n{{{{G|{0}}}}} {1}\n", 'û', TinkeringHelpers.TinkeredItemShortDisplayName(text)));
									flag = true;
								}
							}
							if (!flag)
							{
								foreach (string text2 in array)
								{
									if (flag)
									{
										this.modBitCostText.SetText(this.modBitCostText.text + "\n-or-\n");
									}
									this.modBitCostText.SetText(this.modBitCostText.text + "\n{{R|X}} " + TinkeringHelpers.TinkeredItemShortDisplayName(text2) + "\n");
									flag = true;
								}
							}
						}
					}
				}
				else if (tinkeringLineData.data.Type == "Mod")
				{
					UIThreeColorProperties uithreeColorProperties = this.icon;
					if (uithreeColorProperties != null)
					{
						global::UnityEngine.GameObject gameObject2 = uithreeColorProperties.gameObject;
						if (gameObject2 != null)
						{
							gameObject2.SetActive(true);
						}
					}
					UIThreeColorProperties uithreeColorProperties2 = this.icon;
					if (uithreeColorProperties2 != null)
					{
						global::XRL.World.GameObject modObject = tinkeringLineData.modObject;
						uithreeColorProperties2.FromRenderable((modObject != null) ? modObject.RenderForUI(null, true) : null, true);
					}
					UITextSkin uitextSkin = this.descriptionText;
					global::XRL.World.GameObject modObject2 = tinkeringLineData.modObject;
					string text3;
					if (modObject2 == null)
					{
						text3 = null;
					}
					else
					{
						Description part = modObject2.GetPart<Description>();
						text3 = ((part != null) ? part.GetShortDescription(true, true, "Tinkering") : null);
					}
					uitextSkin.SetText(text3);
					this.modDescriptionText.SetText("{{rules|" + ItemModding.GetModificationDescription(tinkeringLineData.data.Blueprint, tinkeringLineData.modObject) + "}}");
					if (tinkeringLineData.cost != null && tinkeringLineData.data.Type == "Build")
					{
						this.modBitCostText.SetText("{{K|| Bit Cost |}}\n" + tinkeringLineData.costString);
					}
					if (tinkeringLineData.cost != null && tinkeringLineData.data.Type == "Mod")
					{
						this.modBitCostText.SetText("{{K || Bit Cost |}}\n" + tinkeringLineData.costString);
					}
					if (tinkeringLineData.cost != null)
					{
						TinkerData data3 = tinkeringLineData.data;
						if (data3 != null && !data3.Ingredient.IsNullOrEmpty())
						{
							bool flag2 = false;
							string[] array3 = tinkeringLineData.data.Ingredient.Split(',', StringSplitOptions.None);
							int num2 = 14;
							if (array3.Length > 1)
							{
								num2--;
							}
							this.modBitCostText.SetText(this.modBitCostText.text + "\n\n{{K|| Ingredients |}}");
							foreach (string text4 in array3)
							{
								if (The.Player.Inventory.FindObjectByBlueprint(text4) != null)
								{
									if (flag2)
									{
										this.modBitCostText.SetText(this.modBitCostText.text + "\n-or-");
									}
									this.modBitCostText.SetText(this.modBitCostText.text + string.Format("\n{{{{G|{0}}}}} {1}\n", 'û', TinkeringHelpers.TinkeredItemShortDisplayName(text4)));
									flag2 = true;
								}
							}
							if (!flag2)
							{
								foreach (string text5 in array3)
								{
									if (flag2)
									{
										this.modBitCostText.SetText(this.modBitCostText.text + "\n-or-\n");
									}
									this.modBitCostText.SetText(this.modBitCostText.text + "\n{{R|X}} " + TinkeringHelpers.TinkeredItemShortDisplayName(text5) + "\n");
									flag2 = true;
								}
							}
						}
					}
				}
				else
				{
					this.icon.gameObject.SetActive(false);
				}
				this.modHBorder.SetActive(tinkeringLineData.data.Type == "Mod");
				this.modDescruptionContainer.SetActive(tinkeringLineData.data.Type == "Mod");
			}
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x000EA7E1 File Offset: 0x000E89E1
		public void OnBeginDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x000EA7E4 File Offset: 0x000E89E4
		public int HighlightClosestSpacer(Vector2 screenPosition)
		{
			AbilityManagerSpacer abilityManagerSpacer = ((this.spacers.Count > 0) ? this.spacers[0] : null);
			float num = float.MaxValue;
			int num2 = -1;
			Vector3[] array = new Vector3[4];
			int num3 = 0;
			foreach (AbilityManagerSpacer abilityManagerSpacer2 in this.spacers)
			{
				(abilityManagerSpacer2.transform as RectTransform).GetWorldCorners(array);
				float magnitude = (new Vector2((array[0].x + array[2].x) / 2f, (array[0].y + array[2].y) / 2f) - screenPosition).magnitude;
				if (magnitude < num)
				{
					abilityManagerSpacer = abilityManagerSpacer2;
					num = magnitude;
					num2 = num3;
				}
				abilityManagerSpacer2.image.enabled = false;
				num3++;
			}
			if (abilityManagerSpacer != null)
			{
				abilityManagerSpacer.image.enabled = true;
			}
			return num2;
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x000EA908 File Offset: 0x000E8B08
		public void OnDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x000EA90A File Offset: 0x000E8B0A
		public void OnEndDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002BD5 RID: 11221 RVA: 0x000EA90C File Offset: 0x000E8B0C
		public void OnPointerClick(PointerEventData eventData)
		{
			this.context.IsActive(true);
		}

		// Token: 0x040017E9 RID: 6121
		private TinkeringDetailsLine.Context context = new TinkeringDetailsLine.Context();

		// Token: 0x040017EA RID: 6122
		public UIThreeColorProperties icon;

		// Token: 0x040017EB RID: 6123
		public UITextSkin text;

		// Token: 0x040017EC RID: 6124
		public UITextSkin modBitCostText;

		// Token: 0x040017ED RID: 6125
		public UITextSkin descriptionText;

		// Token: 0x040017EE RID: 6126
		public global::UnityEngine.GameObject modHBorder;

		// Token: 0x040017EF RID: 6127
		public global::UnityEngine.GameObject modDescruptionContainer;

		// Token: 0x040017F0 RID: 6128
		public UITextSkin modDescriptionText;

		// Token: 0x040017F1 RID: 6129
		public StringBuilder SB = new StringBuilder();

		// Token: 0x040017F2 RID: 6130
		public static List<MenuOption> categoryExpandOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Expand"
			}
		};

		// Token: 0x040017F3 RID: 6131
		public static List<MenuOption> categoryCollapseOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Collapse"
			}
		};

		// Token: 0x040017F4 RID: 6132
		public Image background;

		// Token: 0x040017F5 RID: 6133
		public bool wasSelected;

		// Token: 0x040017F6 RID: 6134
		public bool wasHighlighted;

		// Token: 0x040017F7 RID: 6135
		public bool isHighlighted;

		// Token: 0x040017F8 RID: 6136
		private static Dictionary<string, global::XRL.World.GameObject> sampleObjects = new Dictionary<string, global::XRL.World.GameObject>();

		// Token: 0x040017F9 RID: 6137
		public UITextSkin requirementsHeaderText;

		// Token: 0x040017FA RID: 6138
		private List<AbilityManagerSpacer> spacers = new List<AbilityManagerSpacer>();

		// Token: 0x020018BF RID: 6335
		public class Context : NavigationContext
		{
			// Token: 0x04006369 RID: 25449
			public TinkeringLineData data;
		}
	}
}
