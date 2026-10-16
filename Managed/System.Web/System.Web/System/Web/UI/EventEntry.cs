using System;

namespace System.Web.UI
{
	/// <summary>Acts as the property entry for event handlers.</summary>
	// Token: 0x0200015B RID: 347
	public class EventEntry
	{
		/// <summary>Gets or sets the name of the method that handles the event.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the event handler method name.</returns>
		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x000287ED File Offset: 0x000269ED
		// (set) Token: 0x06000EFD RID: 3837 RVA: 0x000287F5 File Offset: 0x000269F5
		public string HandlerMethodName
		{
			get
			{
				return this._handlerMethodName;
			}
			set
			{
				this._handlerMethodName = value;
			}
		}

		/// <summary>Gets or sets the type of delegate for the event.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of delegate for the event.</returns>
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x000287FE File Offset: 0x000269FE
		// (set) Token: 0x06000EFF RID: 3839 RVA: 0x00028806 File Offset: 0x00026A06
		public Type HandlerType
		{
			get
			{
				return this._handlerType;
			}
			set
			{
				this._handlerType = value;
			}
		}

		/// <summary>Gets or sets the name of the property from the expression.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the property.</returns>
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x0002880F File Offset: 0x00026A0F
		// (set) Token: 0x06000F01 RID: 3841 RVA: 0x00028817 File Offset: 0x00026A17
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x04001239 RID: 4665
		private Type _handlerType;

		// Token: 0x0400123A RID: 4666
		private string _handlerMethodName;

		// Token: 0x0400123B RID: 4667
		private string _name;
	}
}
