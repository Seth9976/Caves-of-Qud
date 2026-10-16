using System;
using System.Collections.Specialized;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Defines the methods, properties, and events for server-side access to the HTML5 input element. </summary>
	// Token: 0x0200078F RID: 1935
	[ValidationProperty("Value")]
	[DefaultEvent("ServerChange")]
	public class HtmlInputGenericControl : HtmlInputControl, IPostBackDataHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputGenericControl" /> class.</summary>
		// Token: 0x06004E3E RID: 20030 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public HtmlInputGenericControl()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputGenericControl" /> class based on the specified type.</summary>
		/// <param name="type">The type of the control.</param>
		// Token: 0x06004E3F RID: 20031 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public HtmlInputGenericControl(string type)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Occurs when the content of the HTML5 input element changes between posts to the server.</summary>
		// Token: 0x1400012F RID: 303
		// (add) Token: 0x06004E40 RID: 20032 RVA: 0x0000B3D8 File Offset: 0x000095D8
		// (remove) Token: 0x06004E41 RID: 20033 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public event EventHandler ServerChange
		{
			add
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
			remove
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Processes the postback data for the HTML5 input element.</summary>
		/// <returns>true if the posted content differs from the content in a previous postback; otherwise, false.</returns>
		/// <param name="postDataKey">The index in the posted collection that references the content to load.</param>
		/// <param name="postCollection">The collection of all posted values.</param>
		// Token: 0x06004E42 RID: 20034 RVA: 0x000C8A60 File Offset: 0x000C6C60
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlInputGenericControl.ServerChange" /> event.</summary>
		/// <param name="e">The event data.</param>
		// Token: 0x06004E43 RID: 20035 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected virtual void OnServerChange(EventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Invokes the <see cref="M:System.Web.UI.HtmlControls.HtmlInputGenericControl.OnServerChange(System.EventArgs)" /> method when the posted data for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputGenericControl" /> element has changed.</summary>
		// Token: 0x06004E44 RID: 20036 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected virtual void RaisePostDataChangedEvent()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>When implemented in a derived class, processes postback data for an HTML5 input element.</summary>
		/// <returns>true if the state of the HTML5 input element changes because of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The index in the posted collection that references the content to load.</param>
		/// <param name="postCollection">The collection of all posted values.</param>
		// Token: 0x06004E45 RID: 20037 RVA: 0x000C8A7C File Offset: 0x000C6C7C
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>When implemented in a derived class, invokes the <see cref="M:System.Web.UI.HtmlControls.HtmlInputGenericControl.OnServerChange(System.EventArgs)" /> method when the posted data for the HTML5 input element has changed.</summary>
		// Token: 0x06004E46 RID: 20038 RVA: 0x0000B3D8 File Offset: 0x000095D8
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
