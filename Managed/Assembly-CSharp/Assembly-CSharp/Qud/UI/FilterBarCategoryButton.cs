using System;
using System.Collections.Generic;
using System.Text;
using Kobold;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XRL.UI;
using XRL.UI.Framework;

namespace Qud.UI
{
	// Token: 0x020004A9 RID: 1193
	public class FilterBarCategoryButton : FrameworkUnityScrollChild, IFrameworkControl, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
	{
		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06002DAC RID: 11692 RVA: 0x000F9A69 File Offset: 0x000F7C69
		public bool selected
		{
			get
			{
				NavigationContext navigationContext = this.GetNavigationContext();
				return navigationContext != null && navigationContext.IsActive(true);
			}
		}

		// Token: 0x06002DAD RID: 11693 RVA: 0x000F9A7D File Offset: 0x000F7C7D
		public NavigationContext GetNavigationContext()
		{
			return this.context;
		}

		// Token: 0x06002DAE RID: 11694 RVA: 0x000F9A85 File Offset: 0x000F7C85
		public void Awake()
		{
			if (this.tooltipObject != null)
			{
				this.tooltipObject.SetActive(false);
			}
		}

		// Token: 0x06002DAF RID: 11695 RVA: 0x000F9AA4 File Offset: 0x000F7CA4
		public void Update()
		{
			if (this.tooltipObject != null && this.Tooltip != null)
			{
				bool selected = this.selected;
				if (this.tooltipObject.activeSelf != selected)
				{
					this.tooltipObject.SetActive(selected);
				}
			}
		}

		// Token: 0x06002DB0 RID: 11696 RVA: 0x000F9AE8 File Offset: 0x000F7CE8
		public void SetCategory(string category, string tooltip = null)
		{
			this.Tooltip = tooltip ?? category;
			this.category = category;
			UITextSkin uitextSkin = this.tooltipText;
			if (uitextSkin != null)
			{
				uitextSkin.SetText(this.Tooltip);
			}
			string text = null;
			FilterBarCategoryButton.categoryImageMap.TryGetValue(category, out text);
			if (text != null)
			{
				this.icon.gameObject.SetActive(true);
				if (FilterBarCategoryButton.categoryImageMap.ContainsKey(category))
				{
					text = FilterBarCategoryButton.categoryImageMap[category];
				}
				this.icon.image.sprite = SpriteManager.GetUnitySprite(text);
				this.icon.SetColors(new Color(0.596f, 0.529f, 0.372f), new Color(0.545f, 0.4f, 0.18f), new Color(0f, 0f, 0f, 0f));
			}
			else
			{
				this.icon.gameObject.SetActive(false);
			}
			string text2 = null;
			FilterBarCategoryButton.categoryTextMap.TryGetValue(category, out text2);
			if (text == null && text2 == null)
			{
				text2 = category;
			}
			if (text2 != null)
			{
				this.text.SetText(text2);
				this.text.gameObject.SetActive(true);
				return;
			}
			this.text.gameObject.SetActive(false);
		}

		// Token: 0x06002DB1 RID: 11697 RVA: 0x000F9C20 File Offset: 0x000F7E20
		public void HandleNavigationVAxis()
		{
			this.screen.HandleVAxis(NavigationController.currentEvent.axisValue);
			NavigationController.currentEvent.Handle();
		}

		// Token: 0x06002DB2 RID: 11698 RVA: 0x000F9C41 File Offset: 0x000F7E41
		public void HandleNavigationXAxis()
		{
			this.screen.HandleXAxis(NavigationController.currentEvent.axisValue);
			NavigationController.currentEvent.Handle();
		}

		// Token: 0x06002DB3 RID: 11699 RVA: 0x000F9C64 File Offset: 0x000F7E64
		public void LateUpdate()
		{
			if ((this.selected != this.wasSelected || this.categoryEnabled != this.wasEnabled) && this.background != null)
			{
				Color color;
				if (this.categoryEnabled)
				{
					if (this.selected)
					{
						ColorUtility.TryParseHtmlString("#FFFFFF", out color);
					}
					else
					{
						ColorUtility.TryParseHtmlString("#858951", out color);
					}
				}
				else if (this.selected)
				{
					ColorUtility.TryParseHtmlString("#4A757E", out color);
				}
				else
				{
					ColorUtility.TryParseHtmlString("#134F4E", out color);
				}
				this.background.color = color;
			}
			this.wasSelected = this.selected;
			this.wasEnabled = this.categoryEnabled;
		}

		// Token: 0x06002DB4 RID: 11700 RVA: 0x000F9D14 File Offset: 0x000F7F14
		public void setData(FrameworkDataElement data)
		{
			FilterBarCategoryButtonData filterBarCategoryButtonData = data as FilterBarCategoryButtonData;
			if (filterBarCategoryButtonData != null)
			{
				this._context.data = filterBarCategoryButtonData;
				this.SetCategory(filterBarCategoryButtonData.category, filterBarCategoryButtonData.tooltip);
				filterBarCategoryButtonData.button = this;
			}
			NavigationContext context = this.context;
			if (context.commandHandlers == null)
			{
				context.commandHandlers = new Dictionary<string, Action>();
			}
			this.context.commandHandlers["Accept"] = new Action(this.HandleSelect);
		}

		// Token: 0x06002DB5 RID: 11701 RVA: 0x000F9D8B File Offset: 0x000F7F8B
		public void FiltersUpdated(FilterBar bar)
		{
			this.categoryEnabled = bar.enabledCategories.Contains(this.category);
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x000F9DA4 File Offset: 0x000F7FA4
		public void OnBeginDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002DB7 RID: 11703 RVA: 0x000F9DA8 File Offset: 0x000F7FA8
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

		// Token: 0x06002DB8 RID: 11704 RVA: 0x000F9ECC File Offset: 0x000F80CC
		public void OnDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002DB9 RID: 11705 RVA: 0x000F9ECE File Offset: 0x000F80CE
		public void OnEndDrag(PointerEventData eventData)
		{
		}

		// Token: 0x06002DBA RID: 11706 RVA: 0x000F9ED0 File Offset: 0x000F80D0
		public void HandleSelect()
		{
			FilterBarCategoryButtonData data = this._context.data;
			if (data == null)
			{
				return;
			}
			data.onSelect(this.category);
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x000F9EF2 File Offset: 0x000F80F2
		public void OnPointerClick(PointerEventData eventData)
		{
			if (this.GetNavigationContext().IsActive(true) && eventData.button == PointerEventData.InputButton.Left)
			{
				this.HandleSelect();
			}
		}

		// Token: 0x06002DBC RID: 11708 RVA: 0x000F9F10 File Offset: 0x000F8110
		public void OnPointerEnter(PointerEventData eventData)
		{
			this.inside = true;
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x000F9F19 File Offset: 0x000F8119
		public void OnPointerExit(PointerEventData eventData)
		{
			this.inside = false;
		}

		// Token: 0x040019E7 RID: 6631
		private FilterBarCategoryButton.Context _context = new FilterBarCategoryButton.Context();

		// Token: 0x040019E8 RID: 6632
		public UITextSkin check;

		// Token: 0x040019E9 RID: 6633
		public UITextSkin hotkey;

		// Token: 0x040019EA RID: 6634
		public GameObject rightFloatSpacer;

		// Token: 0x040019EB RID: 6635
		public UITextSkin rightFloatText;

		// Token: 0x040019EC RID: 6636
		public UIThreeColorProperties icon;

		// Token: 0x040019ED RID: 6637
		public UITextSkin text;

		// Token: 0x040019EE RID: 6638
		public GameObject iconSpacer;

		// Token: 0x040019EF RID: 6639
		public TradeScreen screen;

		// Token: 0x040019F0 RID: 6640
		public StringBuilder SB = new StringBuilder();

		// Token: 0x040019F1 RID: 6641
		public static Dictionary<string, string> categoryImageMap = new Dictionary<string, string>
		{
			{ "Light Sources", "Items/sw_torch_lit.png" },
			{ "Melee Weapons", "Items/sw_longblade_14.bmp" },
			{ "Thrown Weapons", "Items/sw_throwing_axe.bmp" },
			{ "Miscellaneous", "Items/sw_hookah.bmp" },
			{ "Food", "Items/sw_apple.bmp" },
			{ "Corpses", "Items/sw_splat1.bmp" },
			{ "Plants", "Creatures/natural-weapon-frond.bmp" },
			{ "Missile Weapons", "Items/sw_revolver.bmp" },
			{ "Projectiles", "Items/sw_arrow.bmp" },
			{ "Ammo", "Items/sw_arrow.bmp" },
			{ "Armor", "Items/sw_leather_armor.bmp" },
			{ "Shields", "Items/sw_shield5.bmp" },
			{ "Grenades", "Items/sw_grenade_mki.bmp" },
			{ "Creatures", "Creatures/sw_snapjaw.bmp" },
			{ "Applicators", "Items/sw_spray.bmp" },
			{ "Energy Cells", "Items/sw_cell_chem.bmp" },
			{ "Natural Weapons", "Creatures/natural-weapon-claw.bmp" },
			{ "Natural Missile Weapons", "Creatures/natural-weapon-arc.bmp" },
			{ "Natural Missile Weapon", "Creatures/natural-weapon-arc.bmp" },
			{ "Natural Armor", "Items/sw_carapace.bmp" },
			{ "Meds", "Items/sw_bandage.bmp" },
			{ "Tonics", "Items/sw_injector.bmp" },
			{ "Water Containers", "Items/sw_waterskin.bmp" },
			{ "Books", "Items/sw_book1.bmp" },
			{ "Tools", "Tiles/sw_box.bmp" },
			{ "Artifacts", "Items/sw_gadget.bmp" },
			{ "Clothes", "Items/sw_cloak.bmp" },
			{ "Trade Goods", "Items/sw_rough_gemstone.bmp" },
			{ "Quest Items", "Items/sw_key.bmp" },
			{ "Data Disks", "items/sw_data_disc.bmp" },
			{ "Scrap", "Items/bit11.bmp" },
			{ "Trinkets", "Items/unidentified_artifact_trinket.bmp" },
			{ "Cybernetic Implants", "Items/sw_carbidehand.bmp" },
			{ "CommonMods", "Items/sw_vase.bmp" },
			{ "WeaponMods", "items/sw_axe_2.bmp" },
			{ "BladeMods", "Items/sw_shortblade_1.bmp" },
			{ "LongBladeMods", "Items/sw_longblade_14.bmp" },
			{ "CudgelMods", "Items/sw_cudgel_1.bmp" },
			{ "AxeMods", "Items/sw_axe_3.bmp" },
			{ "ThrownWeaponMods", "Items/sw_throwing_axe.bmp" },
			{ "GrenadeMods", "Items/sw_grenade_mki.bmp" },
			{ "MissileWeaponMods", "items/sw_flamethrower.bmp" },
			{ "BowMods", "items/sw_bow_1.bmp" },
			{ "FirearmMods", "Items/sw_musket.bmp" },
			{ "RifleMods", "Items/sw_rifle.bmp" },
			{ "PistolMods", "items/sw_revolver.bmp" },
			{ "BeamWeaponMods", "Items/sw_techrifle_1.bmp" },
			{ "MagazineMods", "items/sw_bullet.bmp" },
			{ "BodyMods", "Items/sw_leather_armor.bmp" },
			{ "CloakMods", "Items/sw_cloak.bmp" },
			{ "MaskMods", "Items/sw_gentlingmask.bmp" },
			{ "BootMods", "Items/sw_leather_boots.bmp" },
			{ "GauntletMods", "Items/sw_metal_gloves.bmp" },
			{ "HeadwearMods", "Items/sw_hat.bmp" },
			{ "HelmetMods", "Items/sw_metal_helmet.bmp" },
			{ "EyewearMods", "Items/sw_goggles.bmp" },
			{ "ShieldMods", "Items/sw_shield5.bmp" },
			{ "GloveMods", "Items/sw_metal_gloves.bmp" },
			{ "BracerMods", "Items/sw_leather_bracers.bmp" },
			{ "WingsMods", "items/sw_mechanical_wings.bmp" },
			{ "ExoskeletonMods", "Items/sw_exoskeleton.bmp" },
			{ "EnergyCellMods", "Items/sw_cell_chem.bmp" },
			{ "ElectronicsMods", "Items/bit11.bmp" },
			{ "Locations", "Terrain/sw_joppa.bmp" },
			{ "Gossip and Lore", "creatures/caste_13.bmp" },
			{ "Sultan Histories", "Terrain/sw_resheph_sultanstatue.bmp" },
			{ "Village Histories", "Terrain/sw_monument5.bmp" },
			{ "Chronology", "Items/sw_clockthing.bmp" },
			{ "General Notes", "Items/sw_crayons.bmp" },
			{ "Recipes", "Items/sw_oven.bmp" }
		};

		// Token: 0x040019F2 RID: 6642
		public static Dictionary<string, string> categoryTextMap = new Dictionary<string, string> { { "*All", "ALL" } };

		// Token: 0x040019F3 RID: 6643
		public string Tooltip;

		// Token: 0x040019F4 RID: 6644
		public GameObject tooltipObject;

		// Token: 0x040019F5 RID: 6645
		public UITextSkin tooltipText;

		// Token: 0x040019F6 RID: 6646
		public static List<MenuOption> categoryExpandOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Expand"
			}
		};

		// Token: 0x040019F7 RID: 6647
		public static List<MenuOption> categoryCollapseOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Collapse"
			}
		};

		// Token: 0x040019F8 RID: 6648
		public static List<MenuOption> itemOptions = new List<MenuOption>
		{
			new MenuOption
			{
				Id = "Accept",
				InputCommand = "Accept",
				Description = "Select"
			}
		};

		// Token: 0x040019F9 RID: 6649
		public Image background;

		// Token: 0x040019FA RID: 6650
		public bool wasSelected;

		// Token: 0x040019FB RID: 6651
		public bool wasEnabled;

		// Token: 0x040019FC RID: 6652
		public bool categoryEnabled;

		// Token: 0x040019FD RID: 6653
		private List<AbilityManagerSpacer> spacers = new List<AbilityManagerSpacer>();

		// Token: 0x040019FE RID: 6654
		public string category;

		// Token: 0x040019FF RID: 6655
		private bool inside;

		// Token: 0x02001912 RID: 6418
		public class Context : ScrollChildContext
		{
			// Token: 0x040064A4 RID: 25764
			public FilterBarCategoryButtonData data;
		}
	}
}
