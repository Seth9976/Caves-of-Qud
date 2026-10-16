using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using Unity;

namespace System.Web.UI.Adapters
{
	/// <summary>Adapts a Web page for a specific browser and provides the base class from which all page adapters inherit, directly or indirectly. </summary>
	// Token: 0x02000276 RID: 630
	public abstract class PageAdapter : ControlAdapter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Adapters.PageAdapter" /> class. </summary>
		// Token: 0x06001A0A RID: 6666 RVA: 0x000430B6 File Offset: 0x000412B6
		protected PageAdapter()
		{
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x000430BE File Offset: 0x000412BE
		internal PageAdapter(Page p)
			: base(p)
		{
		}

		/// <summary>Gets a list of additional HTTP headers by which caching is varied for the Web page to which this derived page adapter is attached.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> that contains a list of HTTP headers; otherwise, null.</returns>
		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06001A0C RID: 6668 RVA: 0x00003BF6 File Offset: 0x00001DF6
		public virtual StringCollection CacheVaryByHeaders
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets a list of additional parameters from HTTP GET and POST requests by which caching is varied for the Web page to which this derived page adapter is attached.</summary>
		/// <returns>An <see cref="T:System.Collections.IList" /> that contains a list of the GET and POST parameters; otherwise, null.</returns>
		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06001A0D RID: 6669 RVA: 0x00003BF6 File Offset: 0x00001DF6
		public virtual StringCollection CacheVaryByParams
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets an encoded string that contains the view and control states data of the Web page to which this derived page adapter is attached.</summary>
		/// <returns>An encoded <see cref="T:System.String" /> containing the combined view and control states of the controls on the associated <see cref="T:System.Web.UI.Page" />.</returns>
		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06001A0E RID: 6670 RVA: 0x000430C7 File Offset: 0x000412C7
		protected string ClientState
		{
			get
			{
				return base.Page.GetSavedViewState();
			}
		}

		/// <summary>Determines whether the Web page is in postback and returns a name/value collection of the postback variables.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> of the postback variables, if any; otherwise null. </returns>
		// Token: 0x06001A0F RID: 6671 RVA: 0x000430D4 File Offset: 0x000412D4
		public virtual NameValueCollection DeterminePostBackMode()
		{
			return base.Page.DeterminePostBackMode();
		}

		/// <summary>Retrieves a collection of radio button controls specified by <paramref name="groupName" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> of <see cref="T:System.Web.UI.WebControls.RadioButton" /> controls that make up <paramref name="groupName" />.</returns>
		/// <param name="groupName">A <see cref="T:System.String" /> that is the name of the <see cref="T:System.Web.UI.WebControls.RadioButton" /> group to retrieve. </param>
		// Token: 0x06001A10 RID: 6672 RVA: 0x000430E4 File Offset: 0x000412E4
		public virtual ICollection GetRadioButtonsByGroup(string groupName)
		{
			if (this.radio_button_group == null)
			{
				return new ArrayList();
			}
			ArrayList arrayList = (ArrayList)this.radio_button_group[groupName];
			if (arrayList == null)
			{
				return new ArrayList();
			}
			return arrayList;
		}

		/// <summary>Returns an object that is used by the Web page to maintain the control and view states.</summary>
		/// <returns>An object derived from <see cref="T:System.Web.UI.PageStatePersister" /> that supports creating and extracting the combined control and view states for the <see cref="T:System.Web.UI.Page" />.</returns>
		// Token: 0x06001A11 RID: 6673 RVA: 0x0004311B File Offset: 0x0004131B
		public virtual PageStatePersister GetStatePersister()
		{
			return new HiddenFieldPageStatePersister((Page)base.Control);
		}

		/// <summary>Adds a radio button control to the collection for a specified radio button group.</summary>
		/// <param name="radioButton">The <see cref="T:System.Web.UI.WebControls.RadioButton" /> to add to the collection. </param>
		// Token: 0x06001A12 RID: 6674 RVA: 0x00043130 File Offset: 0x00041330
		public virtual void RegisterRadioButton(RadioButton radioButton)
		{
			if (this.radio_button_group == null)
			{
				this.radio_button_group = new ListDictionary();
			}
			ArrayList arrayList = (ArrayList)this.radio_button_group[radioButton.GroupName];
			if (arrayList == null)
			{
				arrayList = (this.radio_button_group[radioButton.GroupName] = new ArrayList());
			}
			if (!arrayList.Contains(radioButton))
			{
				arrayList.Add(radioButton);
			}
		}

		/// <summary>Renders an opening hyperlink tag that includes the target URL to the response stream.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to render the target-specific output. </param>
		/// <param name="targetUrl">The <see cref="T:System.String" /> value holding the target URL of the link. </param>
		/// <param name="encodeUrl">true to use <see cref="M:System.Web.HttpUtility.HtmlAttributeEncode(System.String)" /> to encode the stream output; otherwise, false. </param>
		/// <param name="softkeyLabel">The <see cref="T:System.String" /> value to use as a soft key label. </param>
		// Token: 0x06001A13 RID: 6675 RVA: 0x00043193 File Offset: 0x00041393
		public virtual void RenderBeginHyperlink(HtmlTextWriter writer, string targetUrl, bool encodeUrl, string softkeyLabel)
		{
			this.InternalRenderBeginHyperlink(writer, targetUrl, encodeUrl, softkeyLabel, null);
		}

		/// <summary>Renders an opening hyperlink tag that includes the target URL and an access key to the response stream.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to render the target-specific output. </param>
		/// <param name="targetUrl">The <see cref="T:System.String" /> value holding the target URL of the link. </param>
		/// <param name="encodeUrl">true to use <see cref="M:System.Web.HttpUtility.HtmlAttributeEncode(System.String)" /> to encode the stream output; otherwise, false. </param>
		/// <param name="softkeyLabel">The <see cref="T:System.String" /> value to use as a soft key label. </param>
		/// <param name="accessKey">The <see cref="T:System.String" /> value to assign to the accessKey attribute of the link to create. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="accessKey" /> is longer than one character.</exception>
		// Token: 0x06001A14 RID: 6676 RVA: 0x000431A1 File Offset: 0x000413A1
		public virtual void RenderBeginHyperlink(HtmlTextWriter writer, string targetUrl, bool encodeUrl, string softkeyLabel, string accessKey)
		{
			if (accessKey != null && accessKey.Length > 1)
			{
				throw new ArgumentOutOfRangeException("accessKey");
			}
			this.InternalRenderBeginHyperlink(writer, targetUrl, encodeUrl, softkeyLabel, accessKey);
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x000431C9 File Offset: 0x000413C9
		private void InternalRenderBeginHyperlink(HtmlTextWriter w, string targetUrl, bool encodeUrl, string softKeyLabel, string accessKey)
		{
			w.AddAttribute(HtmlTextWriterAttribute.Href, targetUrl, encodeUrl);
			if (accessKey != null)
			{
				w.AddAttribute(HtmlTextWriterAttribute.Accesskey, accessKey);
			}
			w.RenderBeginTag(HtmlTextWriterTag.A);
		}

		/// <summary>Renders a closing hyperlink tag to the response stream.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains methods to render the target-specific output. </param>
		// Token: 0x06001A16 RID: 6678 RVA: 0x000431E9 File Offset: 0x000413E9
		public virtual void RenderEndHyperlink(HtmlTextWriter writer)
		{
			writer.RenderEndTag();
		}

		/// <summary>Renders a postback event into the response stream as a hyperlink, including the encoded and possibly encrypted view state, and event target and argument.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to render the target-specific output. </param>
		/// <param name="target">The <see cref="T:System.String" /> value holding the postback event target name. </param>
		/// <param name="argument">The <see cref="T:System.String" /> value holding the argument to pass to the postback target event. </param>
		/// <param name="softkeyLabel">The <see cref="T:System.String" /> value to use as a soft key label. </param>
		/// <param name="text">The <see cref="T:System.String" /> value of the text to display as the link. </param>
		// Token: 0x06001A17 RID: 6679 RVA: 0x000431F4 File Offset: 0x000413F4
		public virtual void RenderPostBackEvent(HtmlTextWriter writer, string target, string argument, string softkeyLabel, string text)
		{
			this.RenderPostBackEvent(writer, target, argument, softkeyLabel, text, base.Page.Request.FilePath, null, true);
		}

		/// <summary>Renders a postback event into the response stream as a hyperlink, including the encoded and possibly encrypted view state, an event target and argument, a previous-page parameter, and an access key.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to render the target-specific output. </param>
		/// <param name="target">The <see cref="T:System.String" /> value holding the postback event target name. </param>
		/// <param name="argument">The <see cref="T:System.String" /> value holding the argument to pass to the postback target event. </param>
		/// <param name="softkeyLabel">The <see cref="T:System.String" /> value to use as a soft key label. </param>
		/// <param name="text">The <see cref="T:System.String" /> value of the text to display as the link. </param>
		/// <param name="postUrl">The <see cref="T:System.String" /> value holding the URL target page of the postback. </param>
		/// <param name="accessKey">The <see cref="T:System.String" /> value used to assign to the accessKey attribute of the created link. </param>
		// Token: 0x06001A18 RID: 6680 RVA: 0x00043220 File Offset: 0x00041420
		public virtual void RenderPostBackEvent(HtmlTextWriter writer, string target, string argument, string softkeyLabel, string text, string postUrl, string accessKey)
		{
			this.RenderPostBackEvent(writer, target, argument, softkeyLabel, text, postUrl, accessKey, true);
		}

		/// <summary>Renders a postback event into the response stream as a hyperlink, including the encoded view state, an event target and argument, a previous-page parameter, and an access key.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to render the target-specific output. </param>
		/// <param name="target">The <see cref="T:System.String" /> value holding the postback event target name. </param>
		/// <param name="argument">The <see cref="T:System.String" /> value holding the argument to pass to the postback target event. </param>
		/// <param name="softkeyLabel">The <see cref="T:System.String" /> value to use as a soft key label. </param>
		/// <param name="text">The <see cref="T:System.String" /> value of the text to display as the link. </param>
		/// <param name="postUrl">The <see cref="T:System.String" /> value holding the URL target page of the postback. </param>
		/// <param name="accessKey">The <see cref="T:System.String" /> value to assign to the accessKey attribute of the created link. </param>
		/// <param name="encode">true to use &amp;amp; as the URL parameter separator; false to use &amp;. </param>
		// Token: 0x06001A19 RID: 6681 RVA: 0x00043240 File Offset: 0x00041440
		protected void RenderPostBackEvent(HtmlTextWriter writer, string target, string argument, string softkeyLabel, string text, string postUrl, string accessKey, bool encode)
		{
			string text2 = string.Format("{0}?__VIEWSTATE={1}&__EVENTTARGET={2}&__EVENTARGUMENT={3}&__PREVIOUSPAGE={4}", new object[]
			{
				postUrl,
				HttpUtility.UrlEncode(base.Page.GetSavedViewState()),
				target,
				argument,
				base.Page.Request.FilePath
			});
			this.RenderBeginHyperlink(writer, text2, encode, softkeyLabel, accessKey);
			writer.Write(text);
			this.RenderEndHyperlink(writer);
		}

		/// <summary>Transforms text for the target browser.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the transformed text.</returns>
		/// <param name="text">A <see cref="T:System.String" /> that is the text to transform.</param>
		// Token: 0x06001A1A RID: 6682 RVA: 0x0000207C File Offset: 0x0000027C
		public virtual string TransformText(string text)
		{
			return text;
		}

		/// <summary>Returns a DHTML code fragment that the client browser can use to reference the form on the page that was posted.</summary>
		/// <returns>A <see cref="T:System.String" /> with a reference to the form on the page that was posted.</returns>
		/// <param name="formId">A <see cref="T:System.String" /> containing the client ID of the form that was posted. </param>
		// Token: 0x06001A1B RID: 6683 RVA: 0x000432AE File Offset: 0x000414AE
		protected internal virtual string GetPostBackFormReference(string formId)
		{
			return string.Format("document.forms['{0}']", formId);
		}

		/// <summary>Returns a name-value collection of data that was posted to the page using either a POST or a GET command, without performing ASP.NET request validation on the request.</summary>
		/// <returns>The unvalidated form data.</returns>
		// Token: 0x06001A1C RID: 6684 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public virtual NameValueCollection DeterminePostBackModeUnvalidated()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x04001645 RID: 5701
		private ListDictionary radio_button_group;
	}
}
