using System;
using System.Security.Permissions;

namespace System.Diagnostics
{
	/// <summary>Provides a multilevel switch to control tracing and debug output without recompiling your code.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000226 RID: 550
	public class SourceSwitch : Switch
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.SourceSwitch" /> class, specifying the name of the source.</summary>
		/// <param name="name">The name of the source.</param>
		// Token: 0x06000FE8 RID: 4072 RVA: 0x00046443 File Offset: 0x00044643
		public SourceSwitch(string name)
			: base(name, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.SourceSwitch" /> class, specifying the display name and the default value for the source switch.</summary>
		/// <param name="displayName">The name of the source switch. </param>
		/// <param name="defaultSwitchValue">The default value for the switch. </param>
		// Token: 0x06000FE9 RID: 4073 RVA: 0x00046451 File Offset: 0x00044651
		public SourceSwitch(string displayName, string defaultSwitchValue)
			: base(displayName, string.Empty, defaultSwitchValue)
		{
		}

		/// <summary>Gets or sets the level of the switch.</summary>
		/// <returns>One of the <see cref="T:System.Diagnostics.SourceLevels" /> values that represents the event level of the switch.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000FEA RID: 4074 RVA: 0x00046460 File Offset: 0x00044660
		// (set) Token: 0x06000FEB RID: 4075 RVA: 0x00046468 File Offset: 0x00044668
		public SourceLevels Level
		{
			get
			{
				return (SourceLevels)base.SwitchSetting;
			}
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			set
			{
				base.SwitchSetting = (int)value;
			}
		}

		/// <summary>Determines if trace listeners should be called, based on the trace event type.</summary>
		/// <returns>True if the trace listeners should be called; otherwise, false.</returns>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000FEC RID: 4076 RVA: 0x00046471 File Offset: 0x00044671
		public bool ShouldTrace(TraceEventType eventType)
		{
			return (base.SwitchSetting & (int)eventType) != 0;
		}

		/// <summary>Invoked when the value of the <see cref="P:System.Diagnostics.Switch.Value" /> property changes.</summary>
		/// <exception cref="T:System.ArgumentException">The new value of <see cref="P:System.Diagnostics.Switch.Value" /> is not one of the <see cref="T:System.Diagnostics.SourceLevels" /> values.</exception>
		// Token: 0x06000FED RID: 4077 RVA: 0x0004647E File Offset: 0x0004467E
		protected override void OnValueChanged()
		{
			base.SwitchSetting = (int)Enum.Parse(typeof(SourceLevels), base.Value, true);
		}
	}
}
