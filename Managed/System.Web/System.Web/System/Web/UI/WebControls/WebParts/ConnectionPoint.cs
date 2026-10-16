using System;
using System.Reflection;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Serves as the base class for defining connection point objects that enable the consumer control and the provider control in a Web Parts connection to share data.</summary>
	// Token: 0x0200047A RID: 1146
	public abstract class ConnectionPoint
	{
		// Token: 0x0600340C RID: 13324 RVA: 0x00088044 File Offset: 0x00086244
		internal ConnectionPoint(MethodInfo callBack, Type interFace, Type control, string name, string id, bool allowsMultiConnections)
		{
			this.name = string.Empty;
			this.id = "default";
			base..ctor();
			this.allowMultiConn = allowsMultiConnections;
			this.interfaceType = interFace;
			this.controlType = control;
			this.name = name;
			this.id = id;
			this.callBackMethod = callBack;
		}

		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x0600340D RID: 13325 RVA: 0x0008809A File Offset: 0x0008629A
		internal MethodInfo CallbackMethod
		{
			get
			{
				return this.callBackMethod;
			}
		}

		/// <summary>Returns a value that indicates whether a connection point can participate in connections. </summary>
		/// <returns>true if the control can create a connection point to participate in a connection; otherwise, false.</returns>
		/// <param name="control">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or other server control that is associated with a connection point.</param>
		// Token: 0x0600340E RID: 13326 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public virtual bool GetEnabled(Control control)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value that indicates whether a connection point supports multiple simultaneous connections.</summary>
		/// <returns>true if the connection point supports multiple connections; otherwise, false. </returns>
		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x0600340F RID: 13327 RVA: 0x000880A2 File Offset: 0x000862A2
		public bool AllowsMultipleConnections
		{
			get
			{
				return this.allowMultiConn;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> of the server control with which a connection point is associated.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the control type.</returns>
		// Token: 0x1700106C RID: 4204
		// (get) Token: 0x06003410 RID: 13328 RVA: 0x000880AA File Offset: 0x000862AA
		public Type ControlType
		{
			get
			{
				return this.controlType;
			}
		}

		/// <summary>Gets a string that contains the identifier for a connection point.</summary>
		/// <returns>A string that contains the identifier for a connection point.</returns>
		// Token: 0x1700106D RID: 4205
		// (get) Token: 0x06003411 RID: 13329 RVA: 0x000880B2 File Offset: 0x000862B2
		public string ID
		{
			get
			{
				return this.id;
			}
		}

		/// <summary>Gets the type of the interface used by a connection point.</summary>
		/// <returns>A <see cref="T:System.Type" /> that corresponds to the interface type provided or consumed by a control.</returns>
		// Token: 0x1700106E RID: 4206
		// (get) Token: 0x06003412 RID: 13330 RVA: 0x000880BA File Offset: 0x000862BA
		public Type InterfaceType
		{
			get
			{
				return this.interfaceType;
			}
		}

		// Token: 0x1700106F RID: 4207
		// (get) Token: 0x06003413 RID: 13331 RVA: 0x000880C2 File Offset: 0x000862C2
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06003414 RID: 13332 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal ConnectionPoint()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a string that serves as a friendly display name to represent a connection point in the user interface (UI). </summary>
		/// <returns>A string that contains a friendly display name for a <see cref="T:System.Web.UI.WebControls.WebParts.ConnectionPoint" /> object. </returns>
		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x06003415 RID: 13333 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public string DisplayName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x04001CF7 RID: 7415
		private bool allowMultiConn;

		// Token: 0x04001CF8 RID: 7416
		private string name;

		// Token: 0x04001CF9 RID: 7417
		private string id;

		// Token: 0x04001CFA RID: 7418
		private Type interfaceType;

		// Token: 0x04001CFB RID: 7419
		private Type controlType;

		// Token: 0x04001CFC RID: 7420
		private MethodInfo callBackMethod;

		/// <summary>Represents a string used to identify the default connection point within a collection of connection points associated with a server control. </summary>
		// Token: 0x04001CFD RID: 7421
		public const string DefaultID = "default";
	}
}
