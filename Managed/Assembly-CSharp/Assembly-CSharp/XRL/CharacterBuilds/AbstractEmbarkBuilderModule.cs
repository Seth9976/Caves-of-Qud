using System;
using System.Collections.Generic;
using Genkit;
using XRL.UI.Framework;

namespace XRL.CharacterBuilds
{
	/// <summary>
	///     Abstract base class for all EmbarkBuilder modules.  Implements the basics and provides many override points.
	/// </summary>
	// Token: 0x02000586 RID: 1414
	public abstract class AbstractEmbarkBuilderModule
	{
		/// <summary>
		///     The string name of the module class.
		/// </summary>
		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06003CAB RID: 15531 RVA: 0x0012A572 File Offset: 0x00128772
		public string type
		{
			get
			{
				return base.GetType().Name;
			}
		}

		/// <summary>
		///     Check if the modules data should be included in the build code.
		///     Defaults to module is enabled and has data.
		/// </summary>
		// Token: 0x06003CAC RID: 15532 RVA: 0x0012A57F File Offset: 0x0012877F
		public virtual bool IncludeInBuildCodes()
		{
			return this.enabled && this.getData() != null;
		}

		/// <summary>
		/// Return the name of your mod if you want to have a notice about required mod for the build code.
		/// Leave null to silently ignore this data when your module is not found.
		/// </summary>
		// Token: 0x06003CAD RID: 15533 RVA: 0x0012A594 File Offset: 0x00128794
		public virtual string GetRequiredMod()
		{
			return null;
		}

		/// <summary>
		///     Get the summary info block presented by this module.
		/// </summary>
		/// <returns>A SummaryBlockData or null.</returns>
		// Token: 0x06003CAE RID: 15534 RVA: 0x0012A597 File Offset: 0x00128797
		public virtual SummaryBlockData GetSummaryBlock()
		{
			return null;
		}

		/// <summary>
		/// use enable() and disable() to change state
		/// </summary>
		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06003CAF RID: 15535 RVA: 0x0012A59A File Offset: 0x0012879A
		public bool enabled
		{
			get
			{
				return this._enabled;
			}
		}

		// Token: 0x06003CB0 RID: 15536 RVA: 0x0012A5A2 File Offset: 0x001287A2
		public virtual bool shouldBeEnabled()
		{
			return this.enabled;
		}

		// Token: 0x06003CB1 RID: 15537 RVA: 0x0012A5AA File Offset: 0x001287AA
		public virtual bool shouldBeEditable()
		{
			return true;
		}

		/// <summary>
		/// Return a list of window descriptors. Each descriptor defines a window to be displayed when the module is enabled.
		/// </summary>
		/// <param name="windows" />
		// Token: 0x06003CB2 RID: 15538 RVA: 0x0012A5AD File Offset: 0x001287AD
		public virtual void assembleWindowDescriptors(List<EmbarkBuilderModuleWindowDescriptor> windows)
		{
			windows.AddRange(this.windows.Values);
		}

		/// <summary>
		/// Enable the module. OnEnabled() will be called if the window was not already enabled.
		/// </summary>
		// Token: 0x06003CB3 RID: 15539 RVA: 0x0012A5C0 File Offset: 0x001287C0
		public void enable()
		{
			if (!this._enabled)
			{
				this._enabled = true;
				this.OnEnabled();
			}
		}

		/// <summary>
		/// Called after enable() if the window was not already enabled.
		/// </summary>
		// Token: 0x06003CB4 RID: 15540 RVA: 0x0012A5D7 File Offset: 0x001287D7
		public virtual void OnEnabled()
		{
		}

		/// <summary>
		/// Disable the module. OnDisabled() will be caled if the window was not already disabled.
		/// </summary>
		// Token: 0x06003CB5 RID: 15541 RVA: 0x0012A5D9 File Offset: 0x001287D9
		public void disable()
		{
			if (this._enabled)
			{
				this._enabled = false;
				this.OnDisabled();
			}
		}

		/// <summary>
		/// Called after disable() if the window was not already disabled.
		/// </summary>
		// Token: 0x06003CB6 RID: 15542 RVA: 0x0012A5F0 File Offset: 0x001287F0
		public virtual void OnDisabled()
		{
		}

		/// <summary>
		/// Called when the data for any other module changes.
		/// Immediately after this call the module's shouldBeEnabled state will be checked and the module will be enabled or disabled appropriately.
		/// </summary>
		/// <param name="module" />
		/// <param name="oldValues" />
		/// <param name="newValues" />
		// Token: 0x06003CB7 RID: 15543 RVA: 0x0012A5F2 File Offset: 0x001287F2
		public virtual void handleModuleDataChange(AbstractEmbarkBuilderModule module, AbstractEmbarkBuilderModuleData oldValues, AbstractEmbarkBuilderModuleData newValues)
		{
		}

		/// <summary>
		/// Called after the data has been replaced, when the data for a module is changing.
		/// </summary>
		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06003CB8 RID: 15544 RVA: 0x0012A5F4 File Offset: 0x001287F4
		// (remove) Token: 0x06003CB9 RID: 15545 RVA: 0x0012A62C File Offset: 0x0012882C
		public event Action<AbstractEmbarkBuilderModuleData, AbstractEmbarkBuilderModuleData> OnChange;

		/// <summary>
		/// Called before the data has been replaced, when the data for a module is changing.
		/// </summary>
		/// <param name="oldValues" />
		/// <param name="newValues" />
		// Token: 0x06003CBA RID: 15546 RVA: 0x0012A661 File Offset: 0x00128861
		public virtual void OnBeforeDataChange(AbstractEmbarkBuilderModuleData oldValues, AbstractEmbarkBuilderModuleData newValues)
		{
		}

		/// <summary>
		/// Called to perform the data replacement, when the data for a module is changing.
		/// You should probably call the base if you override this.
		/// </summary>
		/// <param name="oldValues" />
		/// <param name="newValues" />
		// Token: 0x06003CBB RID: 15547 RVA: 0x0012A663 File Offset: 0x00128863
		public virtual void OnDataChange(AbstractEmbarkBuilderModuleData oldValues, AbstractEmbarkBuilderModuleData newValues)
		{
			this._values = newValues;
		}

		/// <summary>
		/// Called after the data has been replaced, when the data for a module is changing.
		/// </summary>
		/// <param name="oldValues" />
		/// <param name="newValues" />
		// Token: 0x06003CBC RID: 15548 RVA: 0x0012A66C File Offset: 0x0012886C
		public virtual void OnAfterDataChange(AbstractEmbarkBuilderModuleData oldValues, AbstractEmbarkBuilderModuleData newValues)
		{
			this.builder.NotifyModuleChanges(this, oldValues, newValues);
		}

		/// <summary>
		/// Set the data for a module. OnBeforeDataChange, OnDataChange, OnChange and OnAfterDataChange will be called.
		/// </summary>
		/// <param name="values" />
		// Token: 0x06003CBD RID: 15549 RVA: 0x0012A67C File Offset: 0x0012887C
		public virtual void setData(AbstractEmbarkBuilderModuleData values)
		{
			AbstractEmbarkBuilderModuleData values2 = this._values;
			this.OnBeforeDataChange(values2, values);
			this.OnDataChange(values2, values);
			if (this.OnChange != null)
			{
				this.OnChange(values2, values);
			}
			this.OnAfterDataChange(values2, values);
		}

		/// <summary>
		/// Set the data for a module. OnBeforeDataChange, OnDataChange, OnChange and OnAfterDataChange WILL NOT be called.
		/// </summary>
		/// <param name="values" />
		// Token: 0x06003CBE RID: 15550 RVA: 0x0012A6BD File Offset: 0x001288BD
		public virtual void setDataDirect(AbstractEmbarkBuilderModuleData values)
		{
			this._values = values;
		}

		/// <summary>
		/// Returns the typeof the data.
		/// </summary>
		/// <returns />
		// Token: 0x06003CBF RID: 15551 RVA: 0x0012A6C6 File Offset: 0x001288C6
		public virtual Type getDataType()
		{
			return typeof(AbstractEmbarkBuilderModuleData);
		}

		/// <summary>
		/// Returns the module data.
		/// </summary>
		/// <returns />
		// Token: 0x06003CC0 RID: 15552 RVA: 0x0012A6D2 File Offset: 0x001288D2
		public virtual AbstractEmbarkBuilderModuleData getData()
		{
			return this._values;
		}

		/// <summary>
		/// Called to determine if the module is well-configured. By default it is true only if both DataErrors and DataWarnings return null.
		/// </summary>
		/// <returns />
		// Token: 0x06003CC1 RID: 15553 RVA: 0x0012A6DA File Offset: 0x001288DA
		public virtual bool IsDataValid()
		{
			return this.DataErrors() == null && this.DataWarnings() == null;
		}

		/// <summary>
		/// A List of errors to print (one per line)
		/// </summary>
		/// <returns />
		// Token: 0x06003CC2 RID: 15554 RVA: 0x0012A6EF File Offset: 0x001288EF
		public virtual string DataErrors()
		{
			if (this.getData() == null)
			{
				return "You must make a selection before advancing.";
			}
			return null;
		}

		/// <summary>
		/// A List of errors to print (one per line)
		/// </summary>
		/// <returns />
		// Token: 0x06003CC3 RID: 15555 RVA: 0x0012A700 File Offset: 0x00128900
		public virtual string DataWarnings()
		{
			return null;
		}

		/// <summary>
		/// Get a Random sequence that is seeded with the module type and given seed.
		/// </summary>
		/// <param name="seed" />
		/// <returns />
		// Token: 0x06003CC4 RID: 15556 RVA: 0x0012A703 File Offset: 0x00128903
		protected Random getModuleRngFromSeed(string seed)
		{
			return new Random(Hash.String(this.type + seed));
		}

		/// <summary>
		/// Given a seed, fully initialize your modules values to a valid configuration.
		/// </summary>
		/// <param name="seed" />
		// Token: 0x06003CC5 RID: 15557 RVA: 0x0012A71B File Offset: 0x0012891B
		public virtual void InitFromSeed(string seed)
		{
		}

		/// <summary>
		/// Initialize your modules values to a random valid configuration.
		/// </summary>
		// Token: 0x06003CC6 RID: 15558 RVA: 0x0012A71D File Offset: 0x0012891D
		public virtual void RandomSelection()
		{
		}

		// Token: 0x06003CC7 RID: 15559 RVA: 0x0012A71F File Offset: 0x0012891F
		public virtual void RandomSelectionNoUI()
		{
		}

		/// <summary>
		/// The default values when a module is initialized or re-initialized.
		/// </summary>
		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06003CC8 RID: 15560 RVA: 0x0012A721 File Offset: 0x00128921
		public virtual AbstractEmbarkBuilderModuleData DefaultData
		{
			get
			{
				return null;
			}
		}

		/// <summary>
		/// Set the value to DefaultData.
		/// </summary>
		// Token: 0x06003CC9 RID: 15561 RVA: 0x0012A724 File Offset: 0x00128924
		public virtual void ResetSelection()
		{
			this.setData(this.DefaultData);
		}

		/// <summary>
		/// Perform initialization before the UI is configured.
		/// </summary>
		// Token: 0x06003CCA RID: 15562 RVA: 0x0012A732 File Offset: 0x00128932
		public virtual void Init()
		{
		}

		/// <summary>
		/// Perform module specific game boot behavior.
		/// </summary>
		/// <param name="game" />
		/// <param name="info" />
		// Token: 0x06003CCB RID: 15563 RVA: 0x0012A734 File Offset: 0x00128934
		public virtual void bootGame(XRLGame game, EmbarkInfo info)
		{
		}

		/// <summary>
		/// Called for any UI event. If no changes or updates to an element are performed, the element should be returned.
		/// </summary>
		/// <param name="ID" />
		/// <param name="Element" />
		/// <returns />
		// Token: 0x06003CCC RID: 15564 RVA: 0x0012A736 File Offset: 0x00128936
		public virtual object handleUIEvent(string ID, object Element)
		{
			return Element;
		}

		/// <summary>
		/// Called for any game boot event. If no changes or updates to an element are performed, the element should be returned.
		/// </summary>
		/// <param name="id" />
		/// <param name="game" />
		/// <param name="info" />
		/// <param name="element" />
		/// <returns />
		// Token: 0x06003CCD RID: 15565 RVA: 0x0012A739 File Offset: 0x00128939
		public virtual object handleBootEvent(string id, XRLGame game, EmbarkInfo info, object element = null)
		{
			return element;
		}

		/// <summary>
		/// Behavior when advancing to the next screen. By default builder.advance() is called.
		/// </summary>
		// Token: 0x06003CCE RID: 15566 RVA: 0x0012A73D File Offset: 0x0012893D
		public virtual void onNext()
		{
			this.builder.advance(false, false, false);
		}

		/// <summary>
		/// Behavior when returning to the previous screen. By default builder.back() is called.
		/// </summary>
		// Token: 0x06003CCF RID: 15567 RVA: 0x0012A74D File Offset: 0x0012894D
		public virtual void onBack()
		{
			this.builder.back();
		}

		/// <summary>
		///     XmlNodes used when parsing the modules file with <c>XmlDataHelper</c>
		/// </summary>
		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06003CD0 RID: 15568 RVA: 0x0012A75A File Offset: 0x0012895A
		public virtual Dictionary<string, Action<XmlDataHelper>> XmlNodes
		{
			get
			{
				return new Dictionary<string, Action<XmlDataHelper>> { 
				{
					"window",
					new Action<XmlDataHelper>(this.HandleWindowNode)
				} };
			}
		}

		/// <summary>
		///     Parse the XML configuration for this module.
		/// </summary>
		/// <param name="xml">Handle to xml to parse</param>
		// Token: 0x06003CD1 RID: 15569 RVA: 0x0012A779 File Offset: 0x00128979
		public virtual void HandleNodes(XmlDataHelper xml)
		{
			xml.HandleNodes(this.XmlNodes, null);
		}

		/// <summary>
		///     Nodes that exist for parsing XmlWindowNodes
		/// </summary>
		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06003CD2 RID: 15570 RVA: 0x0012A788 File Offset: 0x00128988
		protected virtual Dictionary<string, Action<XmlDataHelper>> XmlWindowNodes
		{
			get
			{
				return new Dictionary<string, Action<XmlDataHelper>>
				{
					{
						"icon",
						new Action<XmlDataHelper>(this.HandleWindowIconNode)
					},
					{
						"name",
						new Action<XmlDataHelper>(this.HandleWindowNameNode)
					},
					{
						"title",
						new Action<XmlDataHelper>(this.HandleWindowTitleNode)
					}
				};
			}
		}

		/// <summary>
		///     Parses a &lt;window ID="key"&gt; xml node, adding it to the dictionary.
		///     Uses a new Guid string if no ID is present(worst case, not reusable).
		///     Checks "Class" attribute and looks for the UI type.
		/// </summary>
		/// <param name="xml">A handle the XML window node.</param>
		// Token: 0x06003CD3 RID: 15571 RVA: 0x0012A7E4 File Offset: 0x001289E4
		public virtual void HandleWindowNode(XmlDataHelper xml)
		{
			string text = xml.GetAttribute("ID") ?? Guid.NewGuid().ToString();
			if (!this.windows.TryGetValue(text, out this.CurrentLoadingWindowDescriptor))
			{
				this.CurrentLoadingWindowDescriptor = new EmbarkBuilderModuleWindowDescriptor(this);
				this.CurrentLoadingWindowDescriptor.viewID = text;
				this.windows.Set(text, this.CurrentLoadingWindowDescriptor);
			}
			this.CurrentLoadingWindowDescriptor.prefab = xml.GetAttributeString("Prefab", this.CurrentLoadingWindowDescriptor.prefab, false);
			try
			{
				string attributeString = xml.GetAttributeString("Class", null, false);
				Type type = ModManager.ResolveType(attributeString, false, false, true);
				if (type == null)
				{
					xml.ParseWarning("Unresolved type " + attributeString);
				}
				if (type != null)
				{
					this.CurrentLoadingWindowDescriptor.windowType = type;
				}
			}
			catch
			{
				xml.ParseWarning("Error finding Class");
			}
			xml.HandleNodes(this.XmlWindowNodes, null);
			this.CurrentLoadingWindowDescriptor = null;
		}

		// Token: 0x06003CD4 RID: 15572 RVA: 0x0012A8F0 File Offset: 0x00128AF0
		public virtual void HandleWindowIconNode(XmlDataHelper xml)
		{
			this.CurrentLoadingWindowDescriptor.tile = xml.GetAttribute("Tile");
			xml.DoneWithElement();
		}

		// Token: 0x06003CD5 RID: 15573 RVA: 0x0012A90E File Offset: 0x00128B0E
		public virtual void HandleWindowNameNode(XmlDataHelper xml)
		{
			this.CurrentLoadingWindowDescriptor.name = xml.GetTextNode();
		}

		// Token: 0x06003CD6 RID: 15574 RVA: 0x0012A921 File Offset: 0x00128B21
		public virtual void HandleWindowTitleNode(XmlDataHelper xml)
		{
			this.CurrentLoadingWindowDescriptor.title = xml.GetTextNode();
		}

		/// <summary>
		///     A reference to the current EmbarkBuilder
		/// </summary>
		// Token: 0x04001E4F RID: 7759
		public EmbarkBuilder builder;

		/// <summary>
		///     A dictionary of string keyed window descriptors.
		/// </summary>
		// Token: 0x04001E50 RID: 7760
		public Dictionary<string, EmbarkBuilderModuleWindowDescriptor> windows = new Dictionary<string, EmbarkBuilderModuleWindowDescriptor>();

		/// <summary>
		/// holds the internal state of the enabled switch
		/// </summary>
		// Token: 0x04001E51 RID: 7761
		protected bool _enabled = true;

		// Token: 0x04001E53 RID: 7763
		private AbstractEmbarkBuilderModuleData _values;

		/// <summary>
		///     Stores the EmbarkBuilderModuleWindowDescriptor being currently being loaded by the XmlDataHelper.
		/// </summary>
		// Token: 0x04001E54 RID: 7764
		protected EmbarkBuilderModuleWindowDescriptor CurrentLoadingWindowDescriptor;
	}
}
