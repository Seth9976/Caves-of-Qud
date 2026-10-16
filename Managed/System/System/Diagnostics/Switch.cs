using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Xml.Serialization;

namespace System.Diagnostics
{
	/// <summary>Provides an abstract base class to create new debugging and tracing switches.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000227 RID: 551
	public abstract class Switch
	{
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000FEE RID: 4078 RVA: 0x000464A4 File Offset: 0x000446A4
		private object IntializedLock
		{
			get
			{
				if (this.m_intializedLock == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref this.m_intializedLock, obj, null);
				}
				return this.m_intializedLock;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Switch" /> class.</summary>
		/// <param name="displayName">The name of the switch. </param>
		/// <param name="description">The description for the switch. </param>
		// Token: 0x06000FEF RID: 4079 RVA: 0x000464D3 File Offset: 0x000446D3
		protected Switch(string displayName, string description)
			: this(displayName, description, "0")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Switch" /> class, specifying the display name, description, and default value for the switch. </summary>
		/// <param name="displayName">The name of the switch. </param>
		/// <param name="description">The description of the switch. </param>
		/// <param name="defaultSwitchValue">The default value for the switch.</param>
		// Token: 0x06000FF0 RID: 4080 RVA: 0x000464E4 File Offset: 0x000446E4
		protected Switch(string displayName, string description, string defaultSwitchValue)
		{
			if (displayName == null)
			{
				displayName = string.Empty;
			}
			this.displayName = displayName;
			this.description = description;
			List<WeakReference> list = Switch.switches;
			lock (list)
			{
				Switch._pruneCachedSwitches();
				Switch.switches.Add(new WeakReference(this));
			}
			this.defaultValue = defaultSwitchValue;
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x00046564 File Offset: 0x00044764
		private static void _pruneCachedSwitches()
		{
			List<WeakReference> list = Switch.switches;
			lock (list)
			{
				if (Switch.s_LastCollectionCount != GC.CollectionCount(2))
				{
					List<WeakReference> list2 = new List<WeakReference>(Switch.switches.Count);
					for (int i = 0; i < Switch.switches.Count; i++)
					{
						if ((Switch)Switch.switches[i].Target != null)
						{
							list2.Add(Switch.switches[i]);
						}
					}
					if (list2.Count < Switch.switches.Count)
					{
						Switch.switches.Clear();
						Switch.switches.AddRange(list2);
						Switch.switches.TrimExcess();
					}
					Switch.s_LastCollectionCount = GC.CollectionCount(2);
				}
			}
		}

		/// <summary>Gets the custom switch attributes defined in the application configuration file.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringDictionary" /> containing the case-insensitive custom attributes for the trace switch.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x00046638 File Offset: 0x00044838
		[XmlIgnore]
		public StringDictionary Attributes
		{
			get
			{
				this.Initialize();
				if (this.attributes == null)
				{
					this.attributes = new StringDictionary();
				}
				return this.attributes;
			}
		}

		/// <summary>Gets a name used to identify the switch.</summary>
		/// <returns>The name used to identify the switch. The default value is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x00046659 File Offset: 0x00044859
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
		}

		/// <summary>Gets a description of the switch.</summary>
		/// <returns>The description of the switch. The default value is an empty string ("").</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x00046661 File Offset: 0x00044861
		public string Description
		{
			get
			{
				if (this.description != null)
				{
					return this.description;
				}
				return string.Empty;
			}
		}

		/// <summary>Gets or sets the current setting for this switch.</summary>
		/// <returns>The current setting for this switch. The default is zero.</returns>
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x00046677 File Offset: 0x00044877
		// (set) Token: 0x06000FF6 RID: 4086 RVA: 0x00046698 File Offset: 0x00044898
		protected int SwitchSetting
		{
			get
			{
				if (!this.initialized && this.InitializeWithStatus())
				{
					this.OnSwitchSettingChanged();
				}
				return this.switchSetting;
			}
			set
			{
				bool flag = false;
				object intializedLock = this.IntializedLock;
				lock (intializedLock)
				{
					this.initialized = true;
					if (this.switchSetting != value)
					{
						this.switchSetting = value;
						flag = true;
					}
				}
				if (flag)
				{
					this.OnSwitchSettingChanged();
				}
			}
		}

		/// <summary>Gets or sets the value of the switch.</summary>
		/// <returns>A string representing the value of the switch.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The value is null.-or-The value does not consist solely of an optional negative sign followed by a sequence of digits ranging from 0 to 9.-or-The value represents a number less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x000466F8 File Offset: 0x000448F8
		// (set) Token: 0x06000FF8 RID: 4088 RVA: 0x00046708 File Offset: 0x00044908
		protected string Value
		{
			get
			{
				this.Initialize();
				return this.switchValueString;
			}
			set
			{
				this.Initialize();
				this.switchValueString = value;
				try
				{
					this.OnValueChanged();
				}
				catch (ArgumentException ex)
				{
					throw new ConfigurationErrorsException(SR.GetString("The config value for Switch '{0}' was invalid.", new object[] { this.DisplayName }), ex);
				}
				catch (FormatException ex2)
				{
					throw new ConfigurationErrorsException(SR.GetString("The config value for Switch '{0}' was invalid.", new object[] { this.DisplayName }), ex2);
				}
				catch (OverflowException ex3)
				{
					throw new ConfigurationErrorsException(SR.GetString("The config value for Switch '{0}' was invalid.", new object[] { this.DisplayName }), ex3);
				}
			}
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x000467B8 File Offset: 0x000449B8
		private void Initialize()
		{
			this.InitializeWithStatus();
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x000467C4 File Offset: 0x000449C4
		private bool InitializeWithStatus()
		{
			if (!this.initialized)
			{
				object intializedLock = this.IntializedLock;
				lock (intializedLock)
				{
					if (this.initialized || this.initializing)
					{
						return false;
					}
					this.initializing = true;
					if (this.switchSettings == null && !this.InitializeConfigSettings())
					{
						this.initialized = true;
						this.initializing = false;
						return false;
					}
					if (this.switchSettings != null)
					{
						SwitchElement switchElement = this.switchSettings[this.displayName];
						if (switchElement != null)
						{
							string value = switchElement.Value;
							if (value != null)
							{
								this.Value = value;
							}
							else
							{
								this.Value = this.defaultValue;
							}
							try
							{
								TraceUtils.VerifyAttributes(switchElement.Attributes, this.GetSupportedAttributes(), this);
							}
							catch (ConfigurationException)
							{
								this.initialized = false;
								this.initializing = false;
								throw;
							}
							this.attributes = new StringDictionary();
							this.attributes.ReplaceHashtable(switchElement.Attributes);
						}
						else
						{
							this.switchValueString = this.defaultValue;
							this.OnValueChanged();
						}
					}
					else
					{
						this.switchValueString = this.defaultValue;
						this.OnValueChanged();
					}
					this.initialized = true;
					this.initializing = false;
				}
				return true;
			}
			return true;
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0004693C File Offset: 0x00044B3C
		private bool InitializeConfigSettings()
		{
			if (this.switchSettings != null)
			{
				return true;
			}
			if (!DiagnosticsConfiguration.CanInitialize())
			{
				return false;
			}
			this.switchSettings = DiagnosticsConfiguration.SwitchSettings;
			return true;
		}

		/// <summary>Gets the custom attributes supported by the switch.</summary>
		/// <returns>A string array that contains the names of the custom attributes supported by the switch, or null if there no custom attributes are supported.</returns>
		// Token: 0x06000FFC RID: 4092 RVA: 0x00002F6A File Offset: 0x0000116A
		protected internal virtual string[] GetSupportedAttributes()
		{
			return null;
		}

		/// <summary>Invoked when the <see cref="P:System.Diagnostics.Switch.SwitchSetting" /> property is changed.</summary>
		// Token: 0x06000FFD RID: 4093 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void OnSwitchSettingChanged()
		{
		}

		/// <summary>Invoked when the <see cref="P:System.Diagnostics.Switch.Value" /> property is changed.</summary>
		// Token: 0x06000FFE RID: 4094 RVA: 0x0004695D File Offset: 0x00044B5D
		protected virtual void OnValueChanged()
		{
			this.SwitchSetting = int.Parse(this.Value, CultureInfo.InvariantCulture);
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x00046978 File Offset: 0x00044B78
		internal static void RefreshAll()
		{
			List<WeakReference> list = Switch.switches;
			lock (list)
			{
				Switch._pruneCachedSwitches();
				for (int i = 0; i < Switch.switches.Count; i++)
				{
					Switch @switch = (Switch)Switch.switches[i].Target;
					if (@switch != null)
					{
						@switch.Refresh();
					}
				}
			}
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x000469EC File Offset: 0x00044BEC
		internal void Refresh()
		{
			object intializedLock = this.IntializedLock;
			lock (intializedLock)
			{
				this.initialized = false;
				this.switchSettings = null;
				this.Initialize();
			}
		}

		// Token: 0x040009D7 RID: 2519
		private SwitchElementsCollection switchSettings;

		// Token: 0x040009D8 RID: 2520
		private readonly string description;

		// Token: 0x040009D9 RID: 2521
		private readonly string displayName;

		// Token: 0x040009DA RID: 2522
		private int switchSetting;

		// Token: 0x040009DB RID: 2523
		private volatile bool initialized;

		// Token: 0x040009DC RID: 2524
		private bool initializing;

		// Token: 0x040009DD RID: 2525
		private volatile string switchValueString = string.Empty;

		// Token: 0x040009DE RID: 2526
		private StringDictionary attributes;

		// Token: 0x040009DF RID: 2527
		private string defaultValue;

		// Token: 0x040009E0 RID: 2528
		private object m_intializedLock;

		// Token: 0x040009E1 RID: 2529
		private static List<WeakReference> switches = new List<WeakReference>();

		// Token: 0x040009E2 RID: 2530
		private static int s_LastCollectionCount;
	}
}
