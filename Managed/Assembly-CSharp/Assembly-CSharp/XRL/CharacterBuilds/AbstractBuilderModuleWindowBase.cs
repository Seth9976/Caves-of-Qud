using System;
using System.Collections.Generic;
using System.Linq;
using Qud.UI;
using UnityEngine;
using XRL.CharacterBuilds.UI;
using XRL.UI.Framework;

namespace XRL.CharacterBuilds
{
	// Token: 0x0200058D RID: 1421
	public abstract class AbstractBuilderModuleWindowBase : WindowBase
	{
		/// <summary>
		///   Returns the UI Framework Context for the "middle chuck" of the window.
		/// </summary>
		// Token: 0x06003D0C RID: 15628 RVA: 0x0012BAA8 File Offset: 0x00129CA8
		public virtual NavigationContext GetNavigationContext()
		{
			return null;
		}

		/// <summary>
		///   If a selection has been made and saved in module data.
		/// </summary>
		/// <returns />
		// Token: 0x06003D0D RID: 15629 RVA: 0x0012BAAB File Offset: 0x00129CAB
		public virtual bool HasSelection()
		{
			AbstractEmbarkBuilderModule module = this._module;
			return ((module != null) ? module.getData() : null) != null;
		}

		/// <summary>
		/// Checks if the overlay should be shown and enabled on this screen.
		/// </summary>
		/// <returns />
		// Token: 0x06003D0E RID: 15630 RVA: 0x0012BAC2 File Offset: 0x00129CC2
		public virtual bool UseOverlay()
		{
			return true;
		}

		// Token: 0x06003D0F RID: 15631 RVA: 0x0012BAC5 File Offset: 0x00129CC5
		public virtual string DataErrors()
		{
			return this._module.DataErrors();
		}

		// Token: 0x06003D10 RID: 15632 RVA: 0x0012BAD2 File Offset: 0x00129CD2
		public virtual string DataWarnings()
		{
			return this._module.DataWarnings();
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06003D11 RID: 15633 RVA: 0x0012BAE0 File Offset: 0x00129CE0
		public RectTransform safeArea
		{
			get
			{
				if (this._safeArea == null)
				{
					this._safeArea = (from g in base.GetComponentsInChildren<RectTransform>()
						where g.gameObject.name == "SafeArea"
						select g).FirstOrDefault<RectTransform>();
				}
				return this._safeArea;
			}
		}

		// Token: 0x06003D12 RID: 15634 RVA: 0x0012BB36 File Offset: 0x00129D36
		public virtual void AfterShow(EmbarkBuilderModuleWindowDescriptor descriptor)
		{
		}

		// Token: 0x06003D13 RID: 15635 RVA: 0x0012BB38 File Offset: 0x00129D38
		public virtual GameObject InstantiatePrefab(GameObject prefab)
		{
			return prefab;
		}

		// Token: 0x06003D14 RID: 15636 RVA: 0x0012BB3B File Offset: 0x00129D3B
		public virtual void BeforeShow(EmbarkBuilderModuleWindowDescriptor descriptor)
		{
			this.descriptor = descriptor;
			NavigationContext navigationContext = this.GetNavigationContext();
			if (navigationContext != null)
			{
				navigationContext.Setup();
			}
			NavigationContext navigationContext2 = this.GetNavigationContext();
			if (navigationContext2 == null)
			{
				return;
			}
			navigationContext2.ActivateAndEnable();
		}

		// Token: 0x06003D15 RID: 15637 RVA: 0x0012BB66 File Offset: 0x00129D66
		public virtual void EnsureContentsActive()
		{
		}

		/// <summary>
		///     Windows can be individually marked as disabled if need be, but will ultimately
		///     be disabled when the module is disabled.
		/// </summary>
		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06003D16 RID: 15638 RVA: 0x0012BB68 File Offset: 0x00129D68
		public virtual bool isWindowEnabled
		{
			get
			{
				return this._module.enabled;
			}
		}

		// Token: 0x06003D17 RID: 15639 RVA: 0x0012BB75 File Offset: 0x00129D75
		public T GetModule<T>() where T : AbstractEmbarkBuilderModule
		{
			return this._module.builder.GetModule<T>();
		}

		// Token: 0x06003D18 RID: 15640 RVA: 0x0012BB87 File Offset: 0x00129D87
		public virtual UIBreadcrumb GetBreadcrumb()
		{
			return null;
		}

		// Token: 0x06003D19 RID: 15641 RVA: 0x0012BB8A File Offset: 0x00129D8A
		public EmbarkBuilderOverlayWindow GetOverlayWindow()
		{
			AbstractEmbarkBuilderModule module = this._module;
			if (module == null)
			{
				return null;
			}
			EmbarkBuilder builder = module.builder;
			if (builder == null)
			{
				return null;
			}
			return builder.GetOverlayWindow();
		}

		/// <summary>
		/// Used to describe any keys used for navigational purposes, but shouldn't be clickable buttons/navigable regions on their own. 
		/// I.E. [arrow keys] navigate [enter/space] select
		/// </summary>
		/// <returns />
		// Token: 0x06003D1A RID: 15642 RVA: 0x0012BBA8 File Offset: 0x00129DA8
		public virtual IEnumerable<MenuOption> GetKeyLegend()
		{
			yield return new MenuOption
			{
				InputCommand = "NavigationXYAxis",
				Description = "navigate"
			};
			yield return new MenuOption
			{
				InputCommand = "Accept",
				Description = "select"
			};
			yield break;
		}

		/// <summary>
		/// These keys items will be navigable using up/down/left/right, clicks, etc.  Picking one will call HandleMenuOption() on your module.
		/// Hotkey Framework TBD // TODO:brian TODO:gnarf
		/// </summary>
		/// <returns />
		// Token: 0x06003D1B RID: 15643 RVA: 0x0012BBB1 File Offset: 0x00129DB1
		public virtual IEnumerable<MenuOption> GetKeyMenuBar()
		{
			yield return new MenuOption
			{
				Id = AbstractBuilderModuleWindowBase.RANDOM,
				InputCommand = "CmdChargenRandom",
				Description = "Randomize Selection"
			};
			if (this.HasSelection())
			{
				yield return new MenuOption
				{
					Id = AbstractBuilderModuleWindowBase.RESET,
					InputCommand = "CmdChargenReset",
					Description = "Reset Selection"
				};
			}
			yield break;
		}

		// Token: 0x06003D1C RID: 15644 RVA: 0x0012BBC4 File Offset: 0x00129DC4
		public void HandleMenuOption(FrameworkDataElement menuOption)
		{
			MenuOption menuOption2 = menuOption as MenuOption;
			if (menuOption2 != null)
			{
				this.HandleMenuOption(menuOption2);
			}
		}

		// Token: 0x06003D1D RID: 15645 RVA: 0x0012BBE2 File Offset: 0x00129DE2
		public virtual void DebugQuickstart(string type)
		{
			this.RandomSelectionNoUI();
		}

		// Token: 0x06003D1E RID: 15646 RVA: 0x0012BBEA File Offset: 0x00129DEA
		public virtual void DailySelection()
		{
			this.RandomSelection();
		}

		// Token: 0x06003D1F RID: 15647 RVA: 0x0012BBF2 File Offset: 0x00129DF2
		public virtual void RandomSelectionNoUI()
		{
			AbstractEmbarkBuilderModule module = this._module;
			if (module == null)
			{
				return;
			}
			module.RandomSelectionNoUI();
		}

		// Token: 0x06003D20 RID: 15648 RVA: 0x0012BC04 File Offset: 0x00129E04
		public virtual void RandomSelection()
		{
			AbstractEmbarkBuilderModule module = this._module;
			if (module != null)
			{
				module.RandomSelection();
			}
			this._module.builder.RefreshActiveWindow();
		}

		// Token: 0x06003D21 RID: 15649 RVA: 0x0012BC27 File Offset: 0x00129E27
		public virtual void ResetSelection()
		{
			AbstractEmbarkBuilderModule module = this._module;
			if (module != null)
			{
				module.ResetSelection();
			}
			if (!this._module.builder.SkippingUIUpdates)
			{
				this._module.builder.RefreshActiveWindow();
			}
		}

		// Token: 0x06003D22 RID: 15650 RVA: 0x0012BC5C File Offset: 0x00129E5C
		public virtual void HandleMenuOption(MenuOption menuOption)
		{
			if (menuOption.Id == AbstractBuilderModuleWindowBase.RANDOM)
			{
				this.RandomSelection();
				return;
			}
			if (menuOption.Id == AbstractBuilderModuleWindowBase.RESET)
			{
				this.ResetSelection();
			}
		}

		// Token: 0x06003D23 RID: 15651 RVA: 0x0012BC90 File Offset: 0x00129E90
		public GameObject GetChild(string Name)
		{
			GameObject gameObject;
			try
			{
				gameObject = base.gameObject.transform.Find(Name).gameObject;
			}
			catch (Exception ex)
			{
				string text = "Exception getting control name ";
				string text2 = " : ";
				Exception ex2 = ex;
				Debug.LogError(text + Name + text2 + ((ex2 != null) ? ex2.ToString() : null));
				gameObject = null;
			}
			return gameObject;
		}

		// Token: 0x04001E63 RID: 7779
		public AbstractEmbarkBuilderModule _module;

		// Token: 0x04001E64 RID: 7780
		public EmbarkBuilderModuleWindowDescriptor descriptor;

		// Token: 0x04001E65 RID: 7781
		public RectTransform _safeArea;

		// Token: 0x04001E66 RID: 7782
		public static readonly string RANDOM = "random";

		// Token: 0x04001E67 RID: 7783
		public static readonly string RESET = "reset";
	}
}
