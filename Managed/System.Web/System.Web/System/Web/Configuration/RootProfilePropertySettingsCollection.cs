using System;
using System.Configuration;
using System.Xml;

namespace System.Web.Configuration
{
	/// <summary>Acts as the top of a two-level named hierarchy of <see cref="T:System.Web.Configuration.ProfilePropertySettingsCollection" /> collections.</summary>
	// Token: 0x020005CE RID: 1486
	[ConfigurationCollection(typeof(ProfilePropertySettings), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class RootProfilePropertySettingsCollection : ProfilePropertySettingsCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.RootProfilePropertySettingsCollection" /> class using default settings.</summary>
		// Token: 0x06004020 RID: 16416 RVA: 0x000A7091 File Offset: 0x000A5291
		public RootProfilePropertySettingsCollection()
		{
			this.groupSettings = new ProfileGroupSettingsCollection();
		}

		/// <summary>Compares the current <see cref="T:System.Web.Configuration.RootProfilePropertySettingsCollection" /> object to another A <see cref="T:System.Web.Configuration.RootProfilePropertySettingsCollection" /> object.</summary>
		/// <returns>true if the passed <see cref="T:System.Web.Configuration.RootProfilePropertySettingsCollection" /> object is equal to the current object; otherwise, false.</returns>
		/// <param name="rootProfilePropertySettingsCollection">A <see cref="T:System.Web.Configuration.RootProfilePropertySettingsCollection" /> object to compare to.</param>
		// Token: 0x06004021 RID: 16417 RVA: 0x000A70A4 File Offset: 0x000A52A4
		public override bool Equals(object rootProfilePropertySettingsCollection)
		{
			RootProfilePropertySettingsCollection rootProfilePropertySettingsCollection2 = rootProfilePropertySettingsCollection as RootProfilePropertySettingsCollection;
			if (rootProfilePropertySettingsCollection2 == null)
			{
				return false;
			}
			if (base.GetType() != rootProfilePropertySettingsCollection2.GetType())
			{
				return false;
			}
			if (base.Count != rootProfilePropertySettingsCollection2.Count)
			{
				return false;
			}
			for (int i = 0; i < base.Count; i++)
			{
				if (!base.BaseGet(i).Equals(rootProfilePropertySettingsCollection2.BaseGet(i)))
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Generates a hash code for the collection.</summary>
		/// <returns>Unique integer hash code for the current object.</returns>
		// Token: 0x06004022 RID: 16418 RVA: 0x000A710C File Offset: 0x000A530C
		public override int GetHashCode()
		{
			int num = 0;
			for (int i = 0; i < base.Count; i++)
			{
				num += base.BaseGet(i).GetHashCode();
			}
			return num;
		}

		// Token: 0x17001455 RID: 5205
		// (get) Token: 0x06004023 RID: 16419 RVA: 0x00008B4A File Offset: 0x00006D4A
		protected override bool AllowClear
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004024 RID: 16420 RVA: 0x000A713C File Offset: 0x000A533C
		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			if (elementName == "group")
			{
				ProfileGroupSettings profileGroupSettings = new ProfileGroupSettings();
				profileGroupSettings.DoDeserialize(reader);
				this.GroupSettings.AddNewSettings(profileGroupSettings);
				return true;
			}
			return base.OnDeserializeUnrecognizedElement(elementName, reader);
		}

		// Token: 0x06004025 RID: 16421 RVA: 0x000A7179 File Offset: 0x000A5379
		protected internal override void Unmerge(ConfigurationElement sourceElement, ConfigurationElement parentElement, ConfigurationSaveMode saveMode)
		{
			base.Unmerge(sourceElement, parentElement, saveMode);
		}

		/// <summary>Gets a <see cref="T:System.Web.Configuration.ProfileGroupSettingsCollection" /> containing a collection of <see cref="T:System.Web.Configuration.ProfileGroupSettings" /> objects.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.ProfileGroupSettingsCollection" /> collection.</returns>
		// Token: 0x17001456 RID: 5206
		// (get) Token: 0x06004026 RID: 16422 RVA: 0x000A7184 File Offset: 0x000A5384
		[ConfigurationProperty("group")]
		public ProfileGroupSettingsCollection GroupSettings
		{
			get
			{
				return this.groupSettings;
			}
		}

		// Token: 0x17001457 RID: 5207
		// (get) Token: 0x06004027 RID: 16423 RVA: 0x000A718C File Offset: 0x000A538C
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return RootProfilePropertySettingsCollection.properties;
			}
		}

		// Token: 0x17001458 RID: 5208
		// (get) Token: 0x06004028 RID: 16424 RVA: 0x00008B4A File Offset: 0x00006D4A
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004029 RID: 16425 RVA: 0x000A6086 File Offset: 0x000A4286
		protected internal override bool IsModified()
		{
			return base.IsModified();
		}

		// Token: 0x0600402A RID: 16426 RVA: 0x000A608E File Offset: 0x000A428E
		protected internal override void ResetModified()
		{
			base.ResetModified();
		}

		// Token: 0x0600402B RID: 16427 RVA: 0x000A7194 File Offset: 0x000A5394
		protected internal override void Reset(ConfigurationElement parentElement)
		{
			base.Reset(parentElement);
			RootProfilePropertySettingsCollection rootProfilePropertySettingsCollection = (RootProfilePropertySettingsCollection)parentElement;
			if (rootProfilePropertySettingsCollection == null)
			{
				return;
			}
			this.GroupSettings.ResetInternal(rootProfilePropertySettingsCollection.GroupSettings);
		}

		// Token: 0x040022E1 RID: 8929
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040022E2 RID: 8930
		private ProfileGroupSettingsCollection groupSettings;
	}
}
