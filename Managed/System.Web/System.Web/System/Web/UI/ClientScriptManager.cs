using System;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.Web.Handlers;
using System.Web.Util;
using Unity;

namespace System.Web.UI
{
	/// <summary>Defines methods for managing client scripts in Web applications.</summary>
	// Token: 0x020001A6 RID: 422
	public sealed class ClientScriptManager
	{
		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x0600102B RID: 4139 RVA: 0x0002A28C File Offset: 0x0002848C
		internal bool ScriptsPresent
		{
			get
			{
				return this._webFormClientScriptRequired || this._initCallBackRegistered || this._hasRegisteredForEventValidationOnCallback || this.clientScriptBlocks != null || this.startupScriptBlocks != null || this.submitStatements != null || this.registeredArrayDeclares != null || this.expandoAttributes != null;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x0002A2DC File Offset: 0x000284DC
		private Page OwnerPage
		{
			get
			{
				if (this.ownerPage == null)
				{
					throw new InvalidOperationException("Associated Page instance is required to complete this operation.");
				}
				return this.ownerPage;
			}
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0002A2F7 File Offset: 0x000284F7
		internal ClientScriptManager(Page page)
		{
			this.ownerPage = page;
		}

		/// <summary>Gets a reference, with javascript: appended to the beginning of it, that can be used in a client event to post back to the server for the specified control and with the specified event arguments.</summary>
		/// <returns>A string representing a JavaScript call to the postback function that includes the target control's ID and event arguments.</returns>
		/// <param name="control">The server control to process the postback.</param>
		/// <param name="argument">The parameter passed to the server control. </param>
		// Token: 0x0600102E RID: 4142 RVA: 0x0002A306 File Offset: 0x00028506
		public string GetPostBackClientHyperlink(Control control, string argument)
		{
			return "javascript:" + this.GetPostBackEventReference(control, argument);
		}

		/// <summary>Gets a reference, with javascript: appended to the beginning of it, that can be used in a client event to post back to the server for the specified control with the specified event arguments and Boolean indication whether to register the post back for event validation.</summary>
		/// <returns>A string representing a JavaScript call to the postback function that includes the target control's ID and event arguments.</returns>
		/// <param name="control">The server control to process the postback.</param>
		/// <param name="argument">The parameter passed to the server control.</param>
		/// <param name="registerForEventValidation">true to register the postback event for validation; false to not register the post back event for validation.</param>
		// Token: 0x0600102F RID: 4143 RVA: 0x0002A31A File Offset: 0x0002851A
		public string GetPostBackClientHyperlink(Control control, string argument, bool registerForEventValidation)
		{
			if (registerForEventValidation)
			{
				this.RegisterForEventValidation(control.UniqueID, argument);
			}
			return "javascript:" + this.GetPostBackEventReference(control, argument);
		}

		/// <summary>Returns a string that can be used in a client event to cause postback to the server. The reference string is defined by the specified control that handles the postback and a string argument of additional event information.</summary>
		/// <returns>A string that, when treated as script on the client, initiates the postback.</returns>
		/// <param name="control">The server <see cref="T:System.Web.UI.Control" /> that processes the postback on the server.</param>
		/// <param name="argument">A string of optional arguments to pass to the control that processes the postback.</param>
		/// <exception cref="T:System.ArgumentNullException">The specified <see cref="T:System.Web.UI.Control" /> is null.</exception>
		// Token: 0x06001030 RID: 4144 RVA: 0x0002A340 File Offset: 0x00028540
		public string GetPostBackEventReference(Control control, string argument)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			Page page = this.OwnerPage;
			page.RequiresPostBackScript();
			if (page.IsMultiForm)
			{
				return string.Concat(new string[] { page.theForm, ".__doPostBack('", control.UniqueID, "','", argument, "')" });
			}
			return string.Concat(new string[] { "__doPostBack('", control.UniqueID, "','", argument, "')" });
		}

		/// <summary>Returns a string to use in a client event to cause postback to the server. The reference string is defined by the specified control that handles the postback and a string argument of additional event information. Optionally, registers the event reference for validation.</summary>
		/// <returns>A string that, when treated as script on the client, initiates the postback.</returns>
		/// <param name="control">The server <see cref="T:System.Web.UI.Control" /> that processes the postback on the server.</param>
		/// <param name="argument">A string of optional arguments to pass to <paramref name="control" />.</param>
		/// <param name="registerForEventValidation">true to register the event reference for validation; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">The specified <see cref="T:System.Web.UI.Control" /> is null.</exception>
		// Token: 0x06001031 RID: 4145 RVA: 0x0002A3DA File Offset: 0x000285DA
		public string GetPostBackEventReference(Control control, string argument, bool registerForEventValidation)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (registerForEventValidation)
			{
				this.RegisterForEventValidation(control.UniqueID, argument);
			}
			return this.GetPostBackEventReference(control, argument);
		}

		/// <summary>Returns a string that can be used in a client event to cause postback to the server. The reference string is defined by the specified <see cref="T:System.Web.UI.PostBackOptions" /> object. Optionally, registers the event reference for validation.</summary>
		/// <returns>A string that, when treated as script on the client, initiates the client postback.</returns>
		/// <param name="options">A <see cref="T:System.Web.UI.PostBackOptions" /> that defines the postback.</param>
		/// <param name="registerForEventValidation">true to register the event reference for validation; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.PostBackOptions" /> is null.</exception>
		// Token: 0x06001032 RID: 4146 RVA: 0x0002A402 File Offset: 0x00028602
		public string GetPostBackEventReference(PostBackOptions options, bool registerForEventValidation)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			if (registerForEventValidation)
			{
				this.RegisterForEventValidation(options);
			}
			return this.GetPostBackEventReference(options);
		}

		/// <summary>Returns a string that can be used in a client event to cause postback to the server. The reference string is defined by the specified <see cref="T:System.Web.UI.PostBackOptions" /> instance.</summary>
		/// <returns>A string that, when treated as script on the client, initiates the client postback.</returns>
		/// <param name="options">A <see cref="T:System.Web.UI.PostBackOptions" /> that defines the postback.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.PostBackOptions" /> parameter is null</exception>
		// Token: 0x06001033 RID: 4147 RVA: 0x0002A424 File Offset: 0x00028624
		public string GetPostBackEventReference(PostBackOptions options)
		{
			if (options == null)
			{
				throw new ArgumentNullException("options");
			}
			string actionUrl = options.ActionUrl;
			if (actionUrl != null || options.ValidationGroup != null || options.TrackFocus || options.AutoPostBack || options.PerformValidation)
			{
				this.RegisterWebFormClientScript();
				Page page = this.OwnerPage;
				HttpRequest requestInternal = page.RequestInternal;
				Uri uri = ((requestInternal != null) ? requestInternal.Url : null);
				if (uri != null)
				{
					this.RegisterHiddenField("__PREVIOUSPAGE", uri.AbsolutePath);
				}
				if (options.TrackFocus)
				{
					this.RegisterHiddenField("__LASTFOCUS", string.Empty);
				}
				string text = (options.RequiresJavaScriptProtocol ? "javascript:" : string.Empty);
				if (page.IsMultiForm)
				{
					text = text + page.theForm + ".";
				}
				return string.Concat(new string[]
				{
					text,
					"WebForm_DoPostback(",
					ClientScriptManager.GetScriptLiteral(options.TargetControl.UniqueID),
					",",
					ClientScriptManager.GetScriptLiteral(options.Argument),
					",",
					ClientScriptManager.GetScriptLiteral(actionUrl),
					",",
					ClientScriptManager.GetScriptLiteral(options.AutoPostBack),
					",",
					ClientScriptManager.GetScriptLiteral(options.PerformValidation),
					",",
					ClientScriptManager.GetScriptLiteral(options.TrackFocus),
					",",
					ClientScriptManager.GetScriptLiteral(options.ClientSubmit),
					",",
					ClientScriptManager.GetScriptLiteral(options.ValidationGroup),
					")"
				});
			}
			if (!options.ClientSubmit)
			{
				return null;
			}
			if (options.RequiresJavaScriptProtocol)
			{
				return this.GetPostBackClientHyperlink(options.TargetControl, options.Argument);
			}
			return this.GetPostBackEventReference(options.TargetControl, options.Argument);
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0002A60C File Offset: 0x0002880C
		internal void RegisterWebFormClientScript()
		{
			if (this._webFormClientScriptRequired)
			{
				return;
			}
			this.OwnerPage.RequiresPostBackScript();
			this._webFormClientScriptRequired = true;
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0002A62C File Offset: 0x0002882C
		internal void WriteWebFormClientScript(HtmlTextWriter writer)
		{
			if (!this._webFormClientScriptRendered && this._webFormClientScriptRequired)
			{
				Page page = this.OwnerPage;
				writer.WriteLine();
				this.WriteClientScriptInclude(writer, this.GetWebResourceUrl(typeof(Page), "webform.js"), typeof(Page), "webform.js");
				ClientScriptManager.WriteBeginScriptBlock(writer);
				writer.WriteLine("WebForm_Initialize({0});", page.IsMultiForm ? page.theForm : "window");
				ClientScriptManager.WriteEndScriptBlock(writer);
				this._webFormClientScriptRendered = true;
			}
		}

		/// <summary>Obtains a reference to a client function that, when invoked, initiates a client call back to a server event. The client function for this overloaded method includes a specified control, argument, client script, and context.</summary>
		/// <returns>The name of a client function that invokes the client callback. </returns>
		/// <param name="control">The server <see cref="T:System.Web.UI.Control" /> that handles the client callback. The control must implement the <see cref="T:System.Web.UI.ICallbackEventHandler" /> interface and provide a <see cref="M:System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent(System.String)" /> method. </param>
		/// <param name="argument">An argument passed from the client script to the server <see cref="M:System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent(System.String)" />  method. </param>
		/// <param name="clientCallback">The name of the client event handler that receives the result of the successful server event. </param>
		/// <param name="context">The client script that is evaluated on the client prior to initiating the callback. The result of the script is passed back to the client event handler. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.Control" /> specified is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.Control" /> specified does not implement the <see cref="T:System.Web.UI.ICallbackEventHandler" /> interface.</exception>
		// Token: 0x06001036 RID: 4150 RVA: 0x0002A6B4 File Offset: 0x000288B4
		public string GetCallbackEventReference(Control control, string argument, string clientCallback, string context)
		{
			return this.GetCallbackEventReference(control, argument, clientCallback, context, null, false);
		}

		/// <summary>Obtains a reference to a client function that, when invoked, initiates a client call back to server events. The client function for this overloaded method includes a specified control, argument, client script, context, and Boolean value.</summary>
		/// <returns>The name of a client function that invokes the client callback. </returns>
		/// <param name="control">The server <see cref="T:System.Web.UI.Control" /> that handles the client callback. The control must implement the <see cref="T:System.Web.UI.ICallbackEventHandler" /> interface and provide a <see cref="M:System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent(System.String)" /> method. </param>
		/// <param name="argument">An argument passed from the client script to the server <see cref="M:System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent(System.String)" />  method. </param>
		/// <param name="clientCallback">The name of the client event handler that receives the result of the successful server event. </param>
		/// <param name="context">The client script that is evaluated on the client prior to initiating the callback. The result of the script is passed back to the client event handler. </param>
		/// <param name="useAsync">true to perform the callback asynchronously; false to perform the callback synchronously.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.Control" /> specified is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.Control" /> specified does not implement the <see cref="T:System.Web.UI.ICallbackEventHandler" /> interface.</exception>
		// Token: 0x06001037 RID: 4151 RVA: 0x0002A6C3 File Offset: 0x000288C3
		public string GetCallbackEventReference(Control control, string argument, string clientCallback, string context, bool useAsync)
		{
			return this.GetCallbackEventReference(control, argument, clientCallback, context, null, useAsync);
		}

		/// <summary>Obtains a reference to a client function that, when invoked, initiates a client call back to server events. The client function for this overloaded method includes a specified control, argument, client script, context, error handler, and Boolean value.</summary>
		/// <returns>The name of a client function that invokes the client callback. </returns>
		/// <param name="control">The server <see cref="T:System.Web.UI.Control" /> that handles the client callback. The control must implement the <see cref="T:System.Web.UI.ICallbackEventHandler" /> interface and provide a <see cref="M:System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent(System.String)" /> method. </param>
		/// <param name="argument">An argument passed from the client script to the server <see cref="M:System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent(System.String)" />  method. </param>
		/// <param name="clientCallback">The name of the client event handler that receives the result of the successful server event. </param>
		/// <param name="context">The client script that is evaluated on the client prior to initiating the callback. The result of the script is passed back to the client event handler. </param>
		/// <param name="clientErrorCallback">The name of the client event handler that receives the result when an error occurs in the server event handler. </param>
		/// <param name="useAsync">true to perform the callback asynchronously; false to perform the callback synchronously. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.Control" /> specified is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.Control" /> specified does not implement the <see cref="T:System.Web.UI.ICallbackEventHandler" /> interface.</exception>
		// Token: 0x06001038 RID: 4152 RVA: 0x0002A6D4 File Offset: 0x000288D4
		public string GetCallbackEventReference(Control control, string argument, string clientCallback, string context, string clientErrorCallback, bool useAsync)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (!(control is ICallbackEventHandler))
			{
				throw new InvalidOperationException("The control must implement the ICallbackEventHandler interface and provide a RaiseCallbackEvent method.");
			}
			return this.GetCallbackEventReference("'" + control.UniqueID + "'", argument, clientCallback, context, clientErrorCallback, useAsync);
		}

		/// <summary>Obtains a reference to a client function that, when invoked, initiates a client call back to server events. The client function for this overloaded method includes a specified target, argument, client script, context, error handler, and Boolean value.</summary>
		/// <returns>The name of a client function that invokes the client callback. </returns>
		/// <param name="target">The name of a server <see cref="T:System.Web.UI.Control" /> that handles the client callback. The control must implement the <see cref="T:System.Web.UI.ICallbackEventHandler" /> interface and provide a <see cref="M:System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent(System.String)" /> method.</param>
		/// <param name="argument">An argument passed from the client script to the server <see cref="M:System.Web.UI.ICallbackEventHandler.RaiseCallbackEvent(System.String)" />  method. </param>
		/// <param name="clientCallback">The name of the client event handler that receives the result of the successful server event. </param>
		/// <param name="context">The client script that is evaluated on the client prior to initiating the callback. The result of the script is passed back to the client event handler.</param>
		/// <param name="clientErrorCallback">The name of the client event handler that receives the result when an error occurs in the server event handler. </param>
		/// <param name="useAsync">true  to perform the callback asynchronously; false to perform the callback synchronously.</param>
		// Token: 0x06001039 RID: 4153 RVA: 0x0002A728 File Offset: 0x00028928
		public string GetCallbackEventReference(string target, string argument, string clientCallback, string context, string clientErrorCallback, bool useAsync)
		{
			this.RegisterWebFormClientScript();
			Page page = this.OwnerPage;
			if (!this._initCallBackRegistered)
			{
				this._initCallBackRegistered = true;
				this.RegisterStartupScript(typeof(Page), "WebForm_InitCallback", page.WebFormScriptReference + ".WebForm_InitCallback();", true);
			}
			return string.Concat(new string[]
			{
				page.WebFormScriptReference,
				".WebForm_DoCallback(",
				target,
				",",
				argument ?? "null",
				",",
				clientCallback,
				",",
				context ?? "null",
				",",
				clientErrorCallback ?? "null",
				",",
				useAsync ? "true" : "false",
				")"
			});
		}

		/// <summary>Gets a URL reference to a resource in an assembly.</summary>
		/// <returns>The URL reference to the resource.</returns>
		/// <param name="type">The type of the resource. </param>
		/// <param name="resourceName">The fully qualified name of the resource in the assembly. </param>
		/// <exception cref="T:System.ArgumentNullException">The web resource type is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">The web resource name is null.- or -The web resource name has a length of zero.</exception>
		// Token: 0x0600103A RID: 4154 RVA: 0x0002A80D File Offset: 0x00028A0D
		public string GetWebResourceUrl(Type type, string resourceName)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (resourceName == null || resourceName.Length == 0)
			{
				throw new ArgumentNullException("type");
			}
			return AssemblyResourceLoader.GetResourceUrl(type, resourceName);
		}

		/// <summary>Determines whether the client script block is registered with the <see cref="T:System.Web.UI.Page" /> object using the specified key. </summary>
		/// <returns>true if the client script block is registered; otherwise, false.</returns>
		/// <param name="key">The key of the client script block to search for.</param>
		// Token: 0x0600103B RID: 4155 RVA: 0x0002A840 File Offset: 0x00028A40
		public bool IsClientScriptBlockRegistered(string key)
		{
			return this.IsScriptRegistered(this.clientScriptBlocks, base.GetType(), key);
		}

		/// <summary>Determines whether the client script block is registered with the <see cref="T:System.Web.UI.Page" /> object using a key and type.</summary>
		/// <returns>true if the client script block is registered; otherwise, false.</returns>
		/// <param name="type">The type of the client script block to search for.  </param>
		/// <param name="key">The key of the client script block to search for. </param>
		/// <exception cref="T:System.ArgumentNullException">The client script type is null.</exception>
		// Token: 0x0600103C RID: 4156 RVA: 0x0002A855 File Offset: 0x00028A55
		public bool IsClientScriptBlockRegistered(Type type, string key)
		{
			return this.IsScriptRegistered(this.clientScriptBlocks, type, key);
		}

		/// <summary>Determines whether the startup script is registered with the <see cref="T:System.Web.UI.Page" /> object using the specified key.</summary>
		/// <returns>true if the startup script is registered; otherwise, false.</returns>
		/// <param name="key">The key of the startup script to search for.</param>
		// Token: 0x0600103D RID: 4157 RVA: 0x0002A865 File Offset: 0x00028A65
		public bool IsStartupScriptRegistered(string key)
		{
			return this.IsScriptRegistered(this.startupScriptBlocks, base.GetType(), key);
		}

		/// <summary>Determines whether the startup script is registered with the <see cref="T:System.Web.UI.Page" /> object using the specified key and type.</summary>
		/// <returns>true if the startup script is registered; otherwise, false.</returns>
		/// <param name="type">The type of the startup script to search for. </param>
		/// <param name="key">The key of the startup script to search for.</param>
		/// <exception cref="T:System.ArgumentNullException">The startup script type is null.</exception>
		// Token: 0x0600103E RID: 4158 RVA: 0x0002A87A File Offset: 0x00028A7A
		public bool IsStartupScriptRegistered(Type type, string key)
		{
			return this.IsScriptRegistered(this.startupScriptBlocks, type, key);
		}

		/// <summary>Determines whether the OnSubmit statement is registered with the <see cref="T:System.Web.UI.Page" /> object using the specified key. </summary>
		/// <returns>true if the OnSubmit statement is registered; otherwise, false.</returns>
		/// <param name="key">The key of the OnSubmit statement to search for.</param>
		// Token: 0x0600103F RID: 4159 RVA: 0x0002A88A File Offset: 0x00028A8A
		public bool IsOnSubmitStatementRegistered(string key)
		{
			return this.IsScriptRegistered(this.submitStatements, base.GetType(), key);
		}

		/// <summary>Determines whether the OnSubmit statement is registered with the <see cref="T:System.Web.UI.Page" /> object using the specified key and type.</summary>
		/// <returns>true if the OnSubmit statement is registered; otherwise, false.</returns>
		/// <param name="type">The type of the OnSubmit statement to search for. </param>
		/// <param name="key">The key of the OnSubmit statement to search for. </param>
		/// <exception cref="T:System.ArgumentNullException">The OnSubmit statement type is null.</exception>
		// Token: 0x06001040 RID: 4160 RVA: 0x0002A89F File Offset: 0x00028A9F
		public bool IsOnSubmitStatementRegistered(Type type, string key)
		{
			return this.IsScriptRegistered(this.submitStatements, type, key);
		}

		/// <summary>Determines whether the client script include is registered with the <see cref="T:System.Web.UI.Page" /> object using the specified key. </summary>
		/// <returns>true if the client script include is registered; otherwise, false.</returns>
		/// <param name="key">The key of the client script include to search for. </param>
		// Token: 0x06001041 RID: 4161 RVA: 0x0002A8AF File Offset: 0x00028AAF
		public bool IsClientScriptIncludeRegistered(string key)
		{
			return this.IsClientScriptIncludeRegistered(base.GetType(), key);
		}

		/// <summary>Determines whether the client script include is registered with the <see cref="T:System.Web.UI.Page" /> object using a key and type.</summary>
		/// <returns>true if the client script include is registered; otherwise, false.</returns>
		/// <param name="type">The type of the client script include to search for. </param>
		/// <param name="key">The key of the client script include to search for. </param>
		/// <exception cref="T:System.ArgumentNullException">The client script include type is null.</exception>
		// Token: 0x06001042 RID: 4162 RVA: 0x0002A8BE File Offset: 0x00028ABE
		public bool IsClientScriptIncludeRegistered(Type type, string key)
		{
			return this.IsScriptRegistered(this.clientScriptBlocks, type, "include-" + key);
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0002A8D8 File Offset: 0x00028AD8
		private bool IsScriptRegistered(ClientScriptManager.ScriptEntry scriptList, Type type, string key)
		{
			while (scriptList != null)
			{
				if (scriptList.Type == type && scriptList.Key == key)
				{
					return true;
				}
				scriptList = scriptList.Next;
			}
			return false;
		}

		/// <summary>Registers a JavaScript array declaration with the <see cref="T:System.Web.UI.Page" /> object using an array name and array value.</summary>
		/// <param name="arrayName">The array name to register.</param>
		/// <param name="arrayValue">The array value or values to register.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="arrayName" /> is null.</exception>
		// Token: 0x06001044 RID: 4164 RVA: 0x0002A908 File Offset: 0x00028B08
		public void RegisterArrayDeclaration(string arrayName, string arrayValue)
		{
			if (this.registeredArrayDeclares == null)
			{
				this.registeredArrayDeclares = new Hashtable();
			}
			if (!this.registeredArrayDeclares.ContainsKey(arrayName))
			{
				this.registeredArrayDeclares.Add(arrayName, new ArrayList());
			}
			((ArrayList)this.registeredArrayDeclares[arrayName]).Add(arrayValue);
			this.OwnerPage.RequiresFormScriptDeclaration();
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0002A96A File Offset: 0x00028B6A
		private void RegisterScript(ref ClientScriptManager.ScriptEntry scriptList, Type type, string key, string script, bool addScriptTags)
		{
			this.RegisterScript(ref scriptList, type, key, script, addScriptTags ? ClientScriptManager.ScriptEntryFormat.AddScriptTag : ClientScriptManager.ScriptEntryFormat.None);
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0002A980 File Offset: 0x00028B80
		private void RegisterScript(ref ClientScriptManager.ScriptEntry scriptList, Type type, string key, string script, ClientScriptManager.ScriptEntryFormat format)
		{
			ClientScriptManager.ScriptEntry scriptEntry = null;
			ClientScriptManager.ScriptEntry scriptEntry2;
			for (scriptEntry2 = scriptList; scriptEntry2 != null; scriptEntry2 = scriptEntry2.Next)
			{
				if (scriptEntry2.Type == type && scriptEntry2.Key == key)
				{
					return;
				}
				scriptEntry = scriptEntry2;
			}
			scriptEntry2 = new ClientScriptManager.ScriptEntry(type, key, script, format);
			if (scriptEntry != null)
			{
				scriptEntry.Next = scriptEntry2;
				return;
			}
			scriptList = scriptEntry2;
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0002A9D7 File Offset: 0x00028BD7
		internal void RegisterClientScriptBlock(string key, string script)
		{
			this.RegisterScript(ref this.clientScriptBlocks, base.GetType(), key, script, false);
		}

		/// <summary>Registers the client script with the <see cref="T:System.Web.UI.Page" /> object using a type, key, and script literal.</summary>
		/// <param name="type">The type of the client script to register. </param>
		/// <param name="key">The key of the client script to register. </param>
		/// <param name="script">The client script literal to register. </param>
		// Token: 0x06001048 RID: 4168 RVA: 0x0002A9EE File Offset: 0x00028BEE
		public void RegisterClientScriptBlock(Type type, string key, string script)
		{
			this.RegisterClientScriptBlock(type, key, script, false);
		}

		/// <summary>Registers the client script with the <see cref="T:System.Web.UI.Page" /> object using a type, key, script literal, and Boolean value indicating whether to add script tags.</summary>
		/// <param name="type">The type of the client script to register. </param>
		/// <param name="key">The key of the client script to register. </param>
		/// <param name="script">The client script literal to register.  </param>
		/// <param name="addScriptTags">A Boolean value indicating whether to add script tags.</param>
		/// <exception cref="T:System.ArgumentNullException">The client script block type is null.</exception>
		// Token: 0x06001049 RID: 4169 RVA: 0x0002A9FA File Offset: 0x00028BFA
		public void RegisterClientScriptBlock(Type type, string key, string script, bool addScriptTags)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.RegisterScript(ref this.clientScriptBlocks, type, key, script, addScriptTags);
		}

		/// <summary>Registers a hidden value with the <see cref="T:System.Web.UI.Page" /> object.</summary>
		/// <param name="hiddenFieldName">The name of the hidden field to register.</param>
		/// <param name="hiddenFieldInitialValue">The initial value of the field to register.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="hiddenFieldName" /> is null.</exception>
		// Token: 0x0600104A RID: 4170 RVA: 0x0002AA21 File Offset: 0x00028C21
		public void RegisterHiddenField(string hiddenFieldName, string hiddenFieldInitialValue)
		{
			if (this.hiddenFields == null)
			{
				this.hiddenFields = new Hashtable();
			}
			if (!this.hiddenFields.ContainsKey(hiddenFieldName))
			{
				this.hiddenFields.Add(hiddenFieldName, hiddenFieldInitialValue);
			}
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0002AA51 File Offset: 0x00028C51
		internal void RegisterOnSubmitStatement(string key, string script)
		{
			this.RegisterScript(ref this.submitStatements, base.GetType(), key, script, false);
		}

		/// <summary>Registers an OnSubmit statement with the <see cref="T:System.Web.UI.Page" /> object using a type, a key, and a script literal. The statement executes when the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> is submitted.</summary>
		/// <param name="type">The type of the OnSubmit statement to register. </param>
		/// <param name="key">The key of the OnSubmit statement to register. </param>
		/// <param name="script">The script literal of the OnSubmit statement to register. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x0600104C RID: 4172 RVA: 0x0002AA68 File Offset: 0x00028C68
		public void RegisterOnSubmitStatement(Type type, string key, string script)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.RegisterScript(ref this.submitStatements, type, key, script, false);
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0002AA8E File Offset: 0x00028C8E
		internal void RegisterStartupScript(string key, string script)
		{
			this.RegisterScript(ref this.startupScriptBlocks, base.GetType(), key, script, false);
		}

		/// <summary>Registers the startup script with the <see cref="T:System.Web.UI.Page" /> object using a type, a key, and a script literal.</summary>
		/// <param name="type">The type of the startup script to register. </param>
		/// <param name="key">The key of the startup script to register. </param>
		/// <param name="script">The startup script literal to register. </param>
		// Token: 0x0600104E RID: 4174 RVA: 0x0002AAA5 File Offset: 0x00028CA5
		public void RegisterStartupScript(Type type, string key, string script)
		{
			this.RegisterStartupScript(type, key, script, false);
		}

		/// <summary>Registers the startup script with the <see cref="T:System.Web.UI.Page" /> object using a type, a key, a script literal, and a Boolean value indicating whether to add script tags.</summary>
		/// <param name="type">The type of the startup script to register. </param>
		/// <param name="key">The key of the startup script to register. </param>
		/// <param name="script">The startup script literal to register. </param>
		/// <param name="addScriptTags">A Boolean value indicating whether to add script tags. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x0600104F RID: 4175 RVA: 0x0002AAB1 File Offset: 0x00028CB1
		public void RegisterStartupScript(Type type, string key, string script, bool addScriptTags)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this.RegisterScript(ref this.startupScriptBlocks, type, key, script, addScriptTags);
		}

		/// <summary>Registers the client script with the <see cref="T:System.Web.UI.Page" /> object using a key and a URL, which enables the script to be called from the client.</summary>
		/// <param name="key">The key of the client script include to register. </param>
		/// <param name="url">The URL of the client script include to register. </param>
		// Token: 0x06001050 RID: 4176 RVA: 0x0002AAD8 File Offset: 0x00028CD8
		public void RegisterClientScriptInclude(string key, string url)
		{
			this.RegisterClientScriptInclude(base.GetType(), key, url);
		}

		/// <summary>Registers the client script include with the <see cref="T:System.Web.UI.Page" /> object using a type, a key, and a URL.</summary>
		/// <param name="type">The type of the client script include to register. </param>
		/// <param name="key">The key of the client script include to register. </param>
		/// <param name="url">The URL of the client script include to register. </param>
		/// <exception cref="T:System.ArgumentNullException">The client script include type is null.</exception>
		/// <exception cref="T:System.ArgumentException">The URL is null. - or -The URL is empty.</exception>
		// Token: 0x06001051 RID: 4177 RVA: 0x0002AAE8 File Offset: 0x00028CE8
		public void RegisterClientScriptInclude(Type type, string key, string url)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (url == null || url.Length == 0)
			{
				throw new ArgumentException("url");
			}
			this.RegisterScript(ref this.clientScriptBlocks, type, "include-" + key, url, ClientScriptManager.ScriptEntryFormat.Include);
		}

		/// <summary>Registers the client script resource with the <see cref="T:System.Web.UI.Page" /> object using a type and a resource name.</summary>
		/// <param name="type">The type of the client script resource to register. </param>
		/// <param name="resourceName">The name of the client script resource to register. </param>
		/// <exception cref="T:System.ArgumentNullException">The client resource type is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">The client resource name is null.- or -The client resource name has a length of zero.</exception>
		// Token: 0x06001052 RID: 4178 RVA: 0x0002AB39 File Offset: 0x00028D39
		public void RegisterClientScriptResource(Type type, string resourceName)
		{
			this.RegisterScript(ref this.clientScriptBlocks, type, "resource-" + resourceName, this.GetWebResourceUrl(type, resourceName), ClientScriptManager.ScriptEntryFormat.Include);
		}

		/// <summary>Registers a name/value pair as a custom (expando) attribute of the specified control given a control ID, attribute name, and attribute value.</summary>
		/// <param name="controlId">The <see cref="T:System.Web.UI.Control" /> on the page that contains the custom attribute. </param>
		/// <param name="attributeName">The name of the custom attribute to register. </param>
		/// <param name="attributeValue">The value of the custom attribute. </param>
		// Token: 0x06001053 RID: 4179 RVA: 0x0002AB5C File Offset: 0x00028D5C
		public void RegisterExpandoAttribute(string controlId, string attributeName, string attributeValue)
		{
			this.RegisterExpandoAttribute(controlId, attributeName, attributeValue, true);
		}

		/// <summary>Registers a name/value pair as a custom (expando) attribute of the specified control given a control ID, an attribute name, an attribute value, and a Boolean value indicating whether to encode the attribute value.</summary>
		/// <param name="controlId">The <see cref="T:System.Web.UI.Control" /> on the page that contains the custom attribute.</param>
		/// <param name="attributeName">The name of the custom attribute to register.</param>
		/// <param name="attributeValue">The value of the custom attribute.</param>
		/// <param name="encode">A Boolean value indicating whether to encode the custom attribute to register.</param>
		// Token: 0x06001054 RID: 4180 RVA: 0x0002AB68 File Offset: 0x00028D68
		public void RegisterExpandoAttribute(string controlId, string attributeName, string attributeValue, bool encode)
		{
			if (controlId == null)
			{
				throw new ArgumentNullException("controlId");
			}
			if (attributeName == null)
			{
				throw new ArgumentNullException("attributeName");
			}
			if (this.expandoAttributes == null)
			{
				this.expandoAttributes = new Hashtable();
			}
			ListDictionary listDictionary = (ListDictionary)this.expandoAttributes[controlId];
			if (listDictionary == null)
			{
				listDictionary = new ListDictionary();
				this.expandoAttributes[controlId] = listDictionary;
			}
			listDictionary.Add(attributeName, encode ? StrUtils.EscapeQuotesAndBackslashes(attributeValue) : attributeValue);
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x0002ABE0 File Offset: 0x00028DE0
		private void EnsureEventValidationArray()
		{
			if (this.eventValidationValues == null || this.eventValidationValues.Length == 0)
			{
				this.eventValidationValues = new int[64];
			}
			int num = this.eventValidationValues.Length;
			if (this.eventValidationPos >= num)
			{
				int[] array = new int[num * 2];
				Array.Copy(this.eventValidationValues, array, num);
				this.eventValidationValues = array;
			}
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x0002AC3A File Offset: 0x00028E3A
		internal void ResetEventValidationState()
		{
			this._pageInRender = true;
			this.eventValidationPos = 0;
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x0002AC4C File Offset: 0x00028E4C
		private int CalculateEventHash(string uniqueId, string argument)
		{
			int hashCode = uniqueId.GetHashCode();
			int num = (string.IsNullOrEmpty(argument) ? 0 : argument.GetHashCode());
			return hashCode ^ num;
		}

		/// <summary>Registers an event reference for validation with <see cref="T:System.Web.UI.PostBackOptions" />.</summary>
		/// <param name="options">A <see cref="T:System.Web.UI.PostBackOptions" /> object that specifies how client JavaScript is generated to initiate a postback event.</param>
		// Token: 0x06001058 RID: 4184 RVA: 0x0002AC73 File Offset: 0x00028E73
		public void RegisterForEventValidation(PostBackOptions options)
		{
			this.RegisterForEventValidation(options.TargetControl.UniqueID, options.Argument);
		}

		/// <summary>Registers an event reference for validation with a unique control ID representing the client control generating the event.</summary>
		/// <param name="uniqueId">A unique ID representing the client control generating the event.</param>
		// Token: 0x06001059 RID: 4185 RVA: 0x0002AC8C File Offset: 0x00028E8C
		public void RegisterForEventValidation(string uniqueId)
		{
			this.RegisterForEventValidation(uniqueId, null);
		}

		/// <summary>Registers an event reference for validation with a unique control ID and event arguments representing the client control generating the event.</summary>
		/// <param name="uniqueId">A unique ID representing the client control generating the event.</param>
		/// <param name="argument">Event arguments passed with the client event.</param>
		/// <exception cref="T:System.InvalidOperationException">The method is called prior to the <see cref="M:System.Web.UI.Page.Render(System.Web.UI.HtmlTextWriter)" /> method.</exception>
		// Token: 0x0600105A RID: 4186 RVA: 0x0002AC98 File Offset: 0x00028E98
		public void RegisterForEventValidation(string uniqueId, string argument)
		{
			Page page = this.OwnerPage;
			if (!page.EnableEventValidation)
			{
				return;
			}
			if (uniqueId == null || uniqueId.Length == 0)
			{
				return;
			}
			if (page.IsCallback)
			{
				this._hasRegisteredForEventValidationOnCallback = true;
			}
			else if (!this._pageInRender)
			{
				throw new InvalidOperationException("RegisterForEventValidation may only be called from the Render method");
			}
			this.EnsureEventValidationArray();
			int num = this.CalculateEventHash(uniqueId, argument);
			for (int i = 0; i < this.eventValidationPos; i++)
			{
				if (this.eventValidationValues[i] == num)
				{
					return;
				}
			}
			int[] array = this.eventValidationValues;
			int num2 = this.eventValidationPos;
			this.eventValidationPos = num2 + 1;
			array[num2] = num;
		}

		/// <summary>Validates a client event that was registered for event validation using the <see cref="M:System.Web.UI.ClientScriptManager.RegisterForEventValidation(System.String)" /> method.</summary>
		/// <param name="uniqueId">A unique ID representing the client control generating the event.</param>
		// Token: 0x0600105B RID: 4187 RVA: 0x0002AD2A File Offset: 0x00028F2A
		public void ValidateEvent(string uniqueId)
		{
			this.ValidateEvent(uniqueId, null);
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x0002AD34 File Offset: 0x00028F34
		private ArgumentException InvalidPostBackException()
		{
			return new ArgumentException("Invalid postback or callback argument. Event validation is enabled using <pages enableEventValidation=\"true\"/> in configuration or <%@ Page EnableEventValidation=\"true\" %> in a page. For security purposes, this feature verifies that arguments to postback or callback events originate from the server control that originally rendered them. If the data is valid and expected, use the ClientScriptManager.RegisterForEventValidation method in order to register the postback or callback data for validation.");
		}

		/// <summary>Validates a client event that was registered for event validation using the <see cref="M:System.Web.UI.ClientScriptManager.RegisterForEventValidation(System.String,System.String)" /> method.</summary>
		/// <param name="uniqueId">A unique ID representing the client control generating the event.</param>
		/// <param name="argument">The event arguments passed with the client event.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="uniqueId" /> is null or an empty string ("").</exception>
		// Token: 0x0600105D RID: 4189 RVA: 0x0002AD40 File Offset: 0x00028F40
		public void ValidateEvent(string uniqueId, string argument)
		{
			if (uniqueId == null || uniqueId.Length == 0)
			{
				throw new ArgumentException("must not be null or empty", "uniqueId");
			}
			if (!this.OwnerPage.EnableEventValidation)
			{
				return;
			}
			if (this.eventValidationValues == null)
			{
				throw this.InvalidPostBackException();
			}
			int num = this.CalculateEventHash(uniqueId, argument);
			for (int i = 0; i < this.eventValidationValues.Length; i++)
			{
				if (this.eventValidationValues[i] == num)
				{
					return;
				}
			}
			throw this.InvalidPostBackException();
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0002ADB4 File Offset: 0x00028FB4
		private void WriteScripts(HtmlTextWriter writer, ClientScriptManager.ScriptEntry scriptList)
		{
			if (scriptList == null)
			{
				return;
			}
			writer.WriteLine();
			while (scriptList != null)
			{
				ClientScriptManager.ScriptEntryFormat format = scriptList.Format;
				if (format != ClientScriptManager.ScriptEntryFormat.AddScriptTag)
				{
					if (format != ClientScriptManager.ScriptEntryFormat.Include)
					{
						this.EnsureEndScriptBlock(writer);
						writer.WriteLine(scriptList.Script);
					}
					else
					{
						this.EnsureEndScriptBlock(writer);
						this.WriteClientScriptInclude(writer, scriptList.Script, scriptList.Type, scriptList.Key);
					}
				}
				else
				{
					this.EnsureBeginScriptBlock(writer);
					writer.Write(scriptList.Script);
				}
				scriptList = scriptList.Next;
			}
			this.EnsureEndScriptBlock(writer);
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0002AE3A File Offset: 0x0002903A
		private void EnsureBeginScriptBlock(HtmlTextWriter writer)
		{
			if (!this._scriptTagOpened)
			{
				ClientScriptManager.WriteBeginScriptBlock(writer);
				this._scriptTagOpened = true;
			}
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0002AE51 File Offset: 0x00029051
		private void EnsureEndScriptBlock(HtmlTextWriter writer)
		{
			if (this._scriptTagOpened)
			{
				ClientScriptManager.WriteEndScriptBlock(writer);
				this._scriptTagOpened = false;
			}
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0002AE68 File Offset: 0x00029068
		internal void RestoreEventValidationState(string fieldValue)
		{
			Page page = this.OwnerPage;
			if (!page.EnableEventValidation || fieldValue == null || fieldValue.Length == 0)
			{
				return;
			}
			IStateFormatter formatter = page.GetFormatter();
			this.eventValidationValues = (int[])formatter.Deserialize(fieldValue);
			this.eventValidationPos = this.eventValidationValues.Length;
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0002AEB8 File Offset: 0x000290B8
		internal void SaveEventValidationState()
		{
			if (!this.OwnerPage.EnableEventValidation)
			{
				return;
			}
			string eventValidationStateFormatted = this.GetEventValidationStateFormatted();
			if (eventValidationStateFormatted == null)
			{
				return;
			}
			this.RegisterHiddenField("__EVENTVALIDATION", eventValidationStateFormatted);
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0002AEEC File Offset: 0x000290EC
		internal string GetEventValidationStateFormatted()
		{
			if (this.eventValidationValues == null || this.eventValidationValues.Length == 0)
			{
				return null;
			}
			Page page = this.OwnerPage;
			if (page.IsCallback && !this._hasRegisteredForEventValidationOnCallback)
			{
				return null;
			}
			IStateFormatter formatter = page.GetFormatter();
			int[] array = new int[this.eventValidationPos];
			Array.Copy(this.eventValidationValues, array, this.eventValidationPos);
			return formatter.Serialize(array);
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x0002AF50 File Offset: 0x00029150
		internal void WriteExpandoAttributes(HtmlTextWriter writer)
		{
			if (this.expandoAttributes == null)
			{
				return;
			}
			writer.WriteLine();
			ClientScriptManager.WriteBeginScriptBlock(writer);
			foreach (object obj in this.expandoAttributes.Keys)
			{
				string text = (string)obj;
				writer.WriteLine("var {0} = document.all ? document.all [\"{0}\"] : document.getElementById (\"{0}\");", text);
				ListDictionary listDictionary = (ListDictionary)this.expandoAttributes[text];
				foreach (object obj2 in listDictionary.Keys)
				{
					string text2 = (string)obj2;
					writer.WriteLine("{0}.{1} = \"{2}\";", text, text2, listDictionary[text2]);
				}
			}
			ClientScriptManager.WriteEndScriptBlock(writer);
			writer.WriteLine();
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0002B048 File Offset: 0x00029248
		internal static void WriteBeginScriptBlock(HtmlTextWriter writer)
		{
			writer.WriteLine("<script type=\"text/javascript\">//<![CDATA[");
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0002B055 File Offset: 0x00029255
		internal static void WriteEndScriptBlock(HtmlTextWriter writer)
		{
			writer.WriteLine("//]]></script>");
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x0002B064 File Offset: 0x00029264
		internal void WriteHiddenFields(HtmlTextWriter writer)
		{
			if (this.hiddenFields == null)
			{
				return;
			}
			writer.WriteLine();
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "aspNetHidden");
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			int indent = writer.Indent;
			writer.Indent = 0;
			bool flag = true;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in this.hiddenFields.Keys)
			{
				string text = (string)obj;
				string text2 = this.hiddenFields[text] as string;
				if (flag)
				{
					flag = false;
				}
				else
				{
					writer.WriteLine();
				}
				stringBuilder.Append("<input type=\"hidden\" name=\"");
				stringBuilder.Append(text);
				stringBuilder.Append("\" id=\"");
				stringBuilder.Append(text);
				stringBuilder.Append("\" value=\"");
				stringBuilder.Append(HttpUtility.HtmlAttributeEncode(text2));
				stringBuilder.Append("\" />");
			}
			writer.Write(stringBuilder.ToString());
			writer.Indent = indent;
			writer.RenderEndTag();
			writer.WriteLine();
			this.hiddenFields = null;
		}

		// Token: 0x06001068 RID: 4200 RVA: 0x0002B194 File Offset: 0x00029394
		internal void WriteClientScriptInclude(HtmlTextWriter writer, string path, Type type, string key)
		{
			if (!this.OwnerPage.IsMultiForm)
			{
				writer.WriteLine("<script src=\"{0}\" type=\"text/javascript\"></script>", path);
				return;
			}
			string text = "inc_" + (type.FullName + key).GetHashCode().ToString("X");
			writer.WriteLine("<script type=\"text/javascript\">");
			writer.WriteLine("//<![CDATA[");
			writer.WriteLine("if (!window.{0}) {{", text);
			writer.WriteLine("\twindow.{0} = true", text);
			writer.WriteLine("\tdocument.write('<script src=\"{0}\" type=\"text/javascript\"><\\/script>'); }}", path);
			writer.WriteLine("//]]>");
			writer.WriteLine("</script>");
		}

		// Token: 0x06001069 RID: 4201 RVA: 0x0002B235 File Offset: 0x00029435
		internal void WriteClientScriptBlocks(HtmlTextWriter writer)
		{
			this.WriteScripts(writer, this.clientScriptBlocks);
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0002B244 File Offset: 0x00029444
		internal void WriteStartupScriptBlocks(HtmlTextWriter writer)
		{
			this.WriteScripts(writer, this.startupScriptBlocks);
		}

		// Token: 0x0600106B RID: 4203 RVA: 0x0002B254 File Offset: 0x00029454
		internal void WriteArrayDeclares(HtmlTextWriter writer)
		{
			if (this.registeredArrayDeclares != null)
			{
				writer.WriteLine();
				ClientScriptManager.WriteBeginScriptBlock(writer);
				IDictionaryEnumerator enumerator = this.registeredArrayDeclares.GetEnumerator();
				Page page = this.OwnerPage;
				while (enumerator.MoveNext())
				{
					if (page.IsMultiForm)
					{
						writer.Write("\t" + page.theForm + ".");
					}
					else
					{
						writer.Write("\tvar ");
					}
					writer.Write(enumerator.Key);
					writer.Write(" =  new Array(");
					IEnumerator enumerator2 = ((ArrayList)enumerator.Value).GetEnumerator();
					bool flag = true;
					while (enumerator2.MoveNext())
					{
						if (flag)
						{
							flag = false;
						}
						else
						{
							writer.Write(", ");
						}
						writer.Write(enumerator2.Current);
					}
					writer.WriteLine(");");
				}
				ClientScriptManager.WriteEndScriptBlock(writer);
				writer.WriteLine();
			}
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x0002B334 File Offset: 0x00029534
		internal string GetClientValidationEvent(string validationGroup)
		{
			Page page = this.OwnerPage;
			if (page.IsMultiForm)
			{
				return string.Concat(new string[] { "if (typeof(", page.theForm, ".Page_ClientValidate) == 'function') ", page.theForm, ".Page_ClientValidate('", validationGroup, "');" });
			}
			return "if (typeof(Page_ClientValidate) == 'function') Page_ClientValidate('" + validationGroup + "');";
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0002B3A4 File Offset: 0x000295A4
		internal string GetClientValidationEvent()
		{
			Page page = this.OwnerPage;
			if (page.IsMultiForm)
			{
				return string.Concat(new string[] { "if (typeof(", page.theForm, ".Page_ClientValidate) == 'function') ", page.theForm, ".Page_ClientValidate();" });
			}
			return "if (typeof(Page_ClientValidate) == 'function') Page_ClientValidate();";
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0002B3FC File Offset: 0x000295FC
		internal string WriteSubmitStatements()
		{
			if (this.submitStatements == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (ClientScriptManager.ScriptEntry next = this.submitStatements; next != null; next = next.Next)
			{
				stringBuilder.Append(ClientScriptManager.EnsureEndsWithSemicolon(next.Script));
			}
			Page page = this.OwnerPage;
			this.RegisterClientScriptBlock(base.GetType(), "HtmlForm-OnSubmitStatemen", string.Concat(new string[]
			{
				"\n",
				page.WebFormScriptReference,
				".WebForm_OnSubmit = function () {\n",
				stringBuilder.ToString(),
				"\nreturn true;\n}\n"
			}), true);
			return "javascript:return " + page.WebFormScriptReference + ".WebForm_OnSubmit();";
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0002B4A4 File Offset: 0x000296A4
		internal static string GetScriptLiteral(object ob)
		{
			if (ob == null)
			{
				return "null";
			}
			if (ob is string)
			{
				string text = (string)ob;
				bool flag = false;
				int length = text.Length;
				for (int i = 0; i < length; i++)
				{
					if (text[i] == '\\' || text[i] == '"')
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return "\"" + text + "\"";
				}
				StringBuilder stringBuilder = new StringBuilder(length + 10);
				stringBuilder.Append('"');
				for (int j = 0; j < length; j++)
				{
					if (text[j] == '"')
					{
						stringBuilder.Append("\\\"");
					}
					else if (text[j] == '\\')
					{
						stringBuilder.Append("\\\\");
					}
					else
					{
						stringBuilder.Append(text[j]);
					}
				}
				stringBuilder.Append('"');
				return stringBuilder.ToString();
			}
			else
			{
				if (ob is bool)
				{
					return ob.ToString().ToLower(Helpers.InvariantCulture);
				}
				return ob.ToString();
			}
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x0002B5AA File Offset: 0x000297AA
		internal static string EnsureEndsWithSemicolon(string value)
		{
			if (value != null && value.Length > 0 && value[value.Length - 1] != ';')
			{
				return value += ";";
			}
			return value;
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x0000B3D8 File Offset: 0x000095D8
		internal ClientScriptManager()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001369 RID: 4969
		internal const string EventStateFieldName = "__EVENTVALIDATION";

		// Token: 0x0400136A RID: 4970
		private Hashtable registeredArrayDeclares;

		// Token: 0x0400136B RID: 4971
		private ClientScriptManager.ScriptEntry clientScriptBlocks;

		// Token: 0x0400136C RID: 4972
		private ClientScriptManager.ScriptEntry startupScriptBlocks;

		// Token: 0x0400136D RID: 4973
		internal Hashtable hiddenFields;

		// Token: 0x0400136E RID: 4974
		private ClientScriptManager.ScriptEntry submitStatements;

		// Token: 0x0400136F RID: 4975
		private Page ownerPage;

		// Token: 0x04001370 RID: 4976
		private int[] eventValidationValues;

		// Token: 0x04001371 RID: 4977
		private int eventValidationPos;

		// Token: 0x04001372 RID: 4978
		private Hashtable expandoAttributes;

		// Token: 0x04001373 RID: 4979
		private bool _hasRegisteredForEventValidationOnCallback;

		// Token: 0x04001374 RID: 4980
		private bool _pageInRender;

		// Token: 0x04001375 RID: 4981
		private bool _initCallBackRegistered;

		// Token: 0x04001376 RID: 4982
		private bool _webFormClientScriptRendered;

		// Token: 0x04001377 RID: 4983
		private bool _webFormClientScriptRequired;

		// Token: 0x04001378 RID: 4984
		private bool _scriptTagOpened;

		// Token: 0x04001379 RID: 4985
		internal const string SCRIPT_BLOCK_START = "//<![CDATA[";

		// Token: 0x0400137A RID: 4986
		internal const string SCRIPT_BLOCK_END = "//]]>";

		// Token: 0x0400137B RID: 4987
		internal const string SCRIPT_ELEMENT_START = "<script type=\"text/javascript\">//<![CDATA[";

		// Token: 0x0400137C RID: 4988
		internal const string SCRIPT_ELEMENT_END = "//]]></script>";

		// Token: 0x020001A7 RID: 423
		private sealed class ScriptEntry
		{
			// Token: 0x06001072 RID: 4210 RVA: 0x0002B5DA File Offset: 0x000297DA
			public ScriptEntry(Type type, string key, string script, ClientScriptManager.ScriptEntryFormat format)
			{
				this.Key = key;
				this.Type = type;
				this.Script = script;
				this.Format = format;
			}

			// Token: 0x0400137D RID: 4989
			public readonly Type Type;

			// Token: 0x0400137E RID: 4990
			public readonly string Key;

			// Token: 0x0400137F RID: 4991
			public readonly string Script;

			// Token: 0x04001380 RID: 4992
			public readonly ClientScriptManager.ScriptEntryFormat Format;

			// Token: 0x04001381 RID: 4993
			public ClientScriptManager.ScriptEntry Next;
		}

		// Token: 0x020001A8 RID: 424
		private enum ScriptEntryFormat
		{
			// Token: 0x04001383 RID: 4995
			None,
			// Token: 0x04001384 RID: 4996
			AddScriptTag,
			// Token: 0x04001385 RID: 4997
			Include
		}
	}
}
