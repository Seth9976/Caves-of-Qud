using System;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides a user interface (UI) that enables users to form connections between <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> and other server controls that reside in <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zones. </summary>
	// Token: 0x020007A5 RID: 1957
	[SupportsEventValidation]
	[Designer("System.Web.UI.Design.WebControls.WebParts.ConnectionsZoneDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class ConnectionsZone : ToolZone
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionsZone" /> class. </summary>
		// Token: 0x06004ED5 RID: 20181 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ConnectionsZone()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object that enables end users to cancel the process of establishing a connection.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> that enables end users to cancel the process of connecting two controls.</returns>
		// Token: 0x17001808 RID: 6152
		// (get) Token: 0x06004ED6 RID: 20182 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public virtual WebPartVerb CancelVerb
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object that enables end users to close the connection user interface (UI) created by the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionsZone" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> that allows an end user to close the connection UI.</returns>
		// Token: 0x17001809 RID: 6153
		// (get) Token: 0x06004ED7 RID: 20183 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public virtual WebPartVerb CloseVerb
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the text displayed as the title of a subsection of the connection user interface (UI) created by a <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionsZone" /> control.</summary>
		/// <returns>A string containing the title for the Configure Connections section. The default title is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x1700180A RID: 6154
		// (get) Token: 0x06004ED8 RID: 20184 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004ED9 RID: 20185 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string ConfigureConnectionTitle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object used to open the configuration view in the connection user interface (UI).</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> used to open the Configure Connections section in the connection UI.</returns>
		// Token: 0x1700180B RID: 6155
		// (get) Token: 0x06004EDA RID: 20186 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public virtual WebPartVerb ConfigureVerb
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the instructional text displayed in the section of the connection user interface (UI) where users select a consumer connection point that the provider will connect to.</summary>
		/// <returns>A string containing the instructions for creating a consumer connection to the provider. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x1700180C RID: 6156
		// (get) Token: 0x06004EDB RID: 20187 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004EDC RID: 20188 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string ConnectToConsumerInstructionText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the text of a hyperlink that users click to open a view in which they can choose a consumer control for a connection.</summary>
		/// <returns>A string displayed as the text of a hyperlink that opens the view where users select consumers. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x1700180D RID: 6157
		// (get) Token: 0x06004EDD RID: 20189 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004EDE RID: 20190 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string ConnectToConsumerText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the title text of the section in the connection user interface (UI) in which users can select a specific consumer to connect with.</summary>
		/// <returns>A string serving as the title of the section where users select consumers. The default title is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x1700180E RID: 6158
		// (get) Token: 0x06004EDF RID: 20191 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004EE0 RID: 20192 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string ConnectToConsumerTitle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the instructional text displayed in the section of the connection user interface (UI) where users select a provider connection point that the consumer will connect to.</summary>
		/// <returns>A string containing the instructions on creating a provider connection to the consumer. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x1700180F RID: 6159
		// (get) Token: 0x06004EE1 RID: 20193 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004EE2 RID: 20194 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string ConnectToProviderInstructionText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the text of a hyperlink that users click to open a view in which they can choose a provider control for a connection.</summary>
		/// <returns>A string displayed as the text of a hyperlink that opens the view where users select providers. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x17001810 RID: 6160
		// (get) Token: 0x06004EE3 RID: 20195 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004EE4 RID: 20196 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string ConnectToProviderText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the title text of the section in the connection user interface (UI) in which users can select a specific provider to connect with.</summary>
		/// <returns>A string serving as the title of the section where users select providers. The default title is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x17001811 RID: 6161
		// (get) Token: 0x06004EE5 RID: 20197 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004EE6 RID: 20198 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string ConnectToProviderTitle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object that enables two <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls to establish a connection.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> that enables two controls to establish a connection.</returns>
		// Token: 0x17001812 RID: 6162
		// (get) Token: 0x06004EE7 RID: 20199 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public virtual WebPartVerb ConnectVerb
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the instructional text shown in the consumers section of the connection user interface (UI), when a connection already exists.</summary>
		/// <returns>A string serving as the instructional text for consumers participating in a connection. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x17001813 RID: 6163
		// (get) Token: 0x06004EE8 RID: 20200 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004EE9 RID: 20201 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string ConsumersInstructionText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the title shown above the consumers section of the connection user interface (UI), when a connection already exists.</summary>
		/// <returns>A string serving as the title text for consumers participating in a connection. The default title is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x17001814 RID: 6164
		// (get) Token: 0x06004EEA RID: 20202 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004EEB RID: 20203 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string ConsumersTitle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object that enables a user to disconnect two connected <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> that disconnects two connected <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</returns>
		// Token: 0x17001815 RID: 6165
		// (get) Token: 0x06004EEC RID: 20204 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public virtual WebPartVerb DisconnectVerb
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" /> is currently displayed; otherwise, false. The default value is false.</returns>
		// Token: 0x17001816 RID: 6166
		// (get) Token: 0x06004EED RID: 20205 RVA: 0x000C8C90 File Offset: 0x000C6E90
		protected override bool Display
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets or sets a text message that is displayed in an empty <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionsZone" /> control if there are insufficient controls on a Web page to establish a connection. </summary>
		/// <returns>A <see cref="T:System.String" /> that contains the message for an empty zone. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x17001817 RID: 6167
		// (get) Token: 0x06004EEE RID: 20206 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004EEF RID: 20207 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override string EmptyZoneText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the text of a message displayed in the connection user interface (UI) when there is an error or warning on an existing connection.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the text of the message. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x17001818 RID: 6168
		// (get) Token: 0x06004EF0 RID: 20208 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004EF1 RID: 20209 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string ExistingConnectionErrorMessage
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the text shown in the section of the connection user interface (UI) that precedes the named provider from which a consumer will retrieve data.</summary>
		/// <returns>A string that prefaces the named provider that the consumer will get data from. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x17001819 RID: 6169
		// (get) Token: 0x06004EF2 RID: 20210 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004EF3 RID: 20211 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string GetFromText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the text shown in the section of the connection user interface (UI) that precedes the named consumer that will receive data from a provider.</summary>
		/// <returns>A string that prefaces the named consumer in the connection. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x1700181A RID: 6170
		// (get) Token: 0x06004EF4 RID: 20212 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004EF5 RID: 20213 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string GetText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the header text that appears at the top of the connection user interface (UI) created by the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionsZone" /> control.</summary>
		/// <returns>A string that contains the header text for the connection UI. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x1700181B RID: 6171
		// (get) Token: 0x06004EF6 RID: 20214 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004EF7 RID: 20215 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override string HeaderText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or set the text that is used for general instructions about the selected control in the section of the connection user interface (UI) for managing existing connections.</summary>
		/// <returns>A string that contains the instruction text for the connection UI. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x1700181C RID: 6172
		// (get) Token: 0x06004EF8 RID: 20216 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004EF9 RID: 20217 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override string InstructionText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the text that is used for a general description of the action that can be performed on the consumer or provider control, within the connection user interface (UI) for managing existing connections.</summary>
		/// <returns>A string that contains the instruction title for the connection UI. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x1700181D RID: 6173
		// (get) Token: 0x06004EFA RID: 20218 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004EFB RID: 20219 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string InstructionTitle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the text of a message displayed in the connection user interface (UI) when there is an error or warning on a new connection that a user tries to create.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the text of the message. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x1700181E RID: 6174
		// (get) Token: 0x06004EFC RID: 20220 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004EFD RID: 20221 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string NewConnectionErrorMessage
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the instruction text that appears in the body of the connection user interface (UI) when a Web Parts control has no existing connection.</summary>
		/// <returns>A string that contains the text message for the case where there is no existing connection. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x1700181F RID: 6175
		// (get) Token: 0x06004EFE RID: 20222 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004EFF RID: 20223 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string NoExistingConnectionInstructionText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the title text that appears in the body of the connection user interface (UI) when a Web Parts control has no existing connection.</summary>
		/// <returns>A string that contains the title text for the case where there is no existing connection. The default text is a culture-specific string supplied by the .NET Framework. </returns>
		// Token: 0x17001820 RID: 6176
		// (get) Token: 0x06004F00 RID: 20224 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004F01 RID: 20225 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string NoExistingConnectionTitle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the type of border that frames the server controls contained in a <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionsZone" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeType" /> that determines the type of border that frames the controls contained in the connections zone.</returns>
		// Token: 0x17001821 RID: 6177
		// (get) Token: 0x06004F02 RID: 20226 RVA: 0x000C8CAC File Offset: 0x000C6EAC
		// (set) Token: 0x06004F03 RID: 20227 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override PartChromeType PartChromeType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return PartChromeType.Default;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the instructional text shown in the providers section of the connection user interface (UI), when a connection already exists.</summary>
		/// <returns>A string serving as the instructional text for providers participating in a connection. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x17001822 RID: 6178
		// (get) Token: 0x06004F04 RID: 20228 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004F05 RID: 20229 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string ProvidersInstructionText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the title shown above the providers section of the connection user interface (UI), when a connection already exists.</summary>
		/// <returns>A string serving as the title text for providers participating in a connection. The default title is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x17001823 RID: 6179
		// (get) Token: 0x06004F06 RID: 20230 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004F07 RID: 20231 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string ProvidersTitle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the text shown in the section of the connection user interface (UI) that precedes the named provider that will send data to a consumer.</summary>
		/// <returns>A string that prefaces the named provider in the connection. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x17001824 RID: 6180
		// (get) Token: 0x06004F08 RID: 20232 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004F09 RID: 20233 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string SendText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the text shown in the section of the connection user interface (UI) that precedes the named consumer to which a provider will send data.</summary>
		/// <returns>A string that prefaces the named consumer that the provider will send data to. The default text is a culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x17001825 RID: 6181
		// (get) Token: 0x06004F0A RID: 20234 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004F0B RID: 20235 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string SendToText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets the currently selected <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control to connect to.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</returns>
		// Token: 0x17001826 RID: 6182
		// (get) Token: 0x06004F0C RID: 20236 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected WebPart WebPartToConnect
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Closes the connection user interface (UI) created by the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionsZone" /> control.</summary>
		// Token: 0x06004F0D RID: 20237 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected override void Close()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.DisplayModeChanged" /> event.</summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayModeEventArgs" /> that contains the event data.</param>
		// Token: 0x06004F0E RID: 20238 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected override void OnDisplayModeChanged(object sender, WebPartDisplayModeEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.SelectedWebPartChanged" /> event.</summary>
		/// <param name="sender">An <see cref="T:System.Object" /> that identifies the sender of the event.</param>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartEventArgs" /> that contains the event data.</param>
		// Token: 0x06004F0F RID: 20239 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected override void OnSelectedWebPartChanged(object sender, WebPartEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises an event for the <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionsZone" /> control when the form that contains it posts back to the server. </summary>
		/// <param name="eventArgument">A <see cref="T:System.String" /> that contains the argument data for the event.</param>
		// Token: 0x06004F10 RID: 20240 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected override void RaisePostBackEvent(string eventArgument)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Sends the content of a <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionsZone" /> control's body area to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, which writes the content to the Web page.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the control's body content.</param>
		// Token: 0x06004F11 RID: 20241 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected override void RenderBody(HtmlTextWriter writer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Renders the zone-level verbs for a <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionsZone" /> control.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the content of the verbs to render in a connections zone.</param>
		// Token: 0x06004F12 RID: 20242 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected override void RenderVerbs(HtmlTextWriter writer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
