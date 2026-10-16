using System;
using System.Collections.Specialized;
using System.IO;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Util;
using Unity;

namespace System.Web.Security
{
	/// <summary>Manages forms-authentication services for Web applications. This class cannot be inherited.</summary>
	// Token: 0x020004B9 RID: 1209
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class FormsAuthentication
	{
		/// <summary>Gets the amount of time before an authentication ticket expires.</summary>
		/// <returns>The amount of time before an authentication ticket expires.</returns>
		// Token: 0x17001117 RID: 4375
		// (get) Token: 0x0600364A RID: 13898 RVA: 0x0008C0F4 File Offset: 0x0008A2F4
		// (set) Token: 0x0600364B RID: 13899 RVA: 0x0008C0FB File Offset: 0x0008A2FB
		public static TimeSpan Timeout { get; private set; }

		/// <summary>Gets a value that indicates whether forms authentication is enabled.</summary>
		/// <returns>true if forms authentication is enabled; otherwise, false.</returns>
		// Token: 0x17001118 RID: 4376
		// (get) Token: 0x0600364C RID: 13900 RVA: 0x0008C103 File Offset: 0x0008A303
		public static bool IsEnabled
		{
			get
			{
				return FormsAuthentication.initialized;
			}
		}

		/// <summary>Enables forms authentication.</summary>
		/// <param name="configurationData">A name-value collection that contains values for "defaultUrl" and/or "loginUrl". The parameter can be null if there are no values for the default URL or the login URL. </param>
		/// <exception cref="T:System.InvalidOperationException">The application is not in the pre-start initialization phase.</exception>
		// Token: 0x0600364D RID: 13901 RVA: 0x0008C10C File Offset: 0x0008A30C
		public static void EnableFormsAuthentication(NameValueCollection configurationData)
		{
			BuildManager.AssertPreStartMethodsRunning();
			if (configurationData == null || configurationData.Count == 0)
			{
				return;
			}
			string text = configurationData["loginUrl"];
			if (!string.IsNullOrEmpty(text))
			{
				FormsAuthentication.login_url = text;
			}
			text = configurationData["defaultUrl"];
			if (!string.IsNullOrEmpty(text))
			{
				FormsAuthentication.default_url = text;
			}
		}

		/// <summary>Validates a user name and password against credentials stored in the configuration file for an application.</summary>
		/// <returns>true if the user name and password are valid; otherwise, false.</returns>
		/// <param name="name">The user name.</param>
		/// <param name="password">The password for the user. </param>
		// Token: 0x0600364F RID: 13903 RVA: 0x0008C160 File Offset: 0x0008A360
		public static bool Authenticate(string name, string password)
		{
			if (name == null || password == null)
			{
				return false;
			}
			FormsAuthentication.Initialize();
			if (HttpContext.Current == null)
			{
				throw new HttpException("Context is null!");
			}
			name = name.ToLower(Helpers.InvariantCulture);
			FormsAuthenticationCredentials credentials = ((AuthenticationSection)WebConfigurationManager.GetSection(FormsAuthentication.authConfigPath)).Forms.Credentials;
			FormsAuthenticationUser formsAuthenticationUser = credentials.Users[name];
			string text = null;
			if (formsAuthenticationUser != null)
			{
				text = formsAuthenticationUser.Password;
			}
			if (text == null)
			{
				return false;
			}
			bool flag = true;
			switch (credentials.PasswordFormat)
			{
			case FormsAuthPasswordFormat.Clear:
				flag = false;
				break;
			case FormsAuthPasswordFormat.SHA1:
				password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, FormsAuthPasswordFormat.SHA1);
				break;
			case FormsAuthPasswordFormat.MD5:
				password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, FormsAuthPasswordFormat.MD5);
				break;
			}
			return string.Compare(password, text, flag ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == 0;
		}

		// Token: 0x06003650 RID: 13904 RVA: 0x0008C21C File Offset: 0x0008A41C
		private static FormsAuthenticationTicket Decrypt2(byte[] bytes)
		{
			if (FormsAuthentication.protection == FormsProtectionEnum.None)
			{
				return FormsAuthenticationTicket.FromByteArray(bytes);
			}
			MachineKeySection machineKeySection = (MachineKeySection)WebConfigurationManager.GetWebApplicationSection(FormsAuthentication.machineKeyConfigPath);
			byte[] array = null;
			if (FormsAuthentication.protection == FormsProtectionEnum.All)
			{
				array = MachineKeySectionUtils.VerifyDecrypt(machineKeySection, bytes);
			}
			else if (FormsAuthentication.protection == FormsProtectionEnum.Encryption)
			{
				array = MachineKeySectionUtils.Decrypt(machineKeySection, bytes);
			}
			else if (FormsAuthentication.protection == FormsProtectionEnum.Validation)
			{
				array = MachineKeySectionUtils.Verify(machineKeySection, bytes);
			}
			return FormsAuthenticationTicket.FromByteArray(array);
		}

		/// <summary>Creates a <see cref="T:System.Web.Security.FormsAuthenticationTicket" /> object based on the encrypted forms-authentication ticket passed to the method.</summary>
		/// <returns>A <see cref="T:System.Web.Security.FormsAuthenticationTicket" /> object. If the <paramref name="encryptedTicket" /> parameter is not a valid ticket, null is returned.</returns>
		/// <param name="encryptedTicket">The encrypted authentication ticket. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="encryptedTicket" /> is null.- or -<paramref name="encryptedTicket" /> is an empty string ("").- or -The length of <paramref name="encryptedTicket" /> is greater than 4096 characters.- or -<paramref name="encryptedTicket" /> is of an invalid format. </exception>
		// Token: 0x06003651 RID: 13905 RVA: 0x0008C284 File Offset: 0x0008A484
		public static FormsAuthenticationTicket Decrypt(string encryptedTicket)
		{
			if (string.IsNullOrEmpty(encryptedTicket))
			{
				throw new ArgumentException("Invalid encrypted ticket", "encryptedTicket");
			}
			FormsAuthentication.Initialize();
			byte[] array = Convert.FromBase64String(encryptedTicket);
			FormsAuthenticationTicket formsAuthenticationTicket;
			try
			{
				formsAuthenticationTicket = FormsAuthentication.Decrypt2(array);
			}
			catch (Exception)
			{
				formsAuthenticationTicket = null;
			}
			return formsAuthenticationTicket;
		}

		/// <summary>Creates a string containing an encrypted forms-authentication ticket suitable for use in an HTTP cookie.</summary>
		/// <returns>A string containing an encrypted forms-authentication ticket.</returns>
		/// <param name="ticket">The <see cref="T:System.Web.Security.FormsAuthenticationTicket" /> object with which to create the encrypted forms-authentication ticket. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ticket" /> is null.</exception>
		// Token: 0x06003652 RID: 13906 RVA: 0x0008C2D4 File Offset: 0x0008A4D4
		public static string Encrypt(FormsAuthenticationTicket ticket)
		{
			if (ticket == null)
			{
				throw new ArgumentNullException("ticket");
			}
			FormsAuthentication.Initialize();
			byte[] array = ticket.ToByteArray();
			if (FormsAuthentication.protection == FormsProtectionEnum.None)
			{
				return Convert.ToBase64String(array);
			}
			byte[] array2 = null;
			MachineKeySection machineKeySection = (MachineKeySection)WebConfigurationManager.GetWebApplicationSection(FormsAuthentication.machineKeyConfigPath);
			if (FormsAuthentication.protection == FormsProtectionEnum.All)
			{
				array2 = MachineKeySectionUtils.EncryptSign(machineKeySection, array);
			}
			else if (FormsAuthentication.protection == FormsProtectionEnum.Encryption)
			{
				array2 = MachineKeySectionUtils.Encrypt(machineKeySection, array);
			}
			else if (FormsAuthentication.protection == FormsProtectionEnum.Validation)
			{
				array2 = MachineKeySectionUtils.Sign(machineKeySection, array);
			}
			return Convert.ToBase64String(array2);
		}

		/// <summary>Creates an authentication cookie for a given user name. This does not set the cookie as part of the outgoing response, so that an application can have more control over how the cookie is issued.</summary>
		/// <returns>An <see cref="T:System.Web.HttpCookie" /> that contains encrypted forms-authentication ticket information. The default value for the <see cref="P:System.Web.Security.FormsAuthentication.FormsCookiePath" /> property is used.</returns>
		/// <param name="userName">The name of the authenticated user. </param>
		/// <param name="createPersistentCookie">true to create a durable cookie (one that is saved across browser sessions); otherwise, false. </param>
		// Token: 0x06003653 RID: 13907 RVA: 0x0008C355 File Offset: 0x0008A555
		public static HttpCookie GetAuthCookie(string userName, bool createPersistentCookie)
		{
			return FormsAuthentication.GetAuthCookie(userName, createPersistentCookie, null);
		}

		/// <summary>Creates an authentication cookie for a given user name. This does not set the cookie as part of the outgoing response.</summary>
		/// <returns>An <see cref="T:System.Web.HttpCookie" /> that contains encrypted forms-authentication ticket information.</returns>
		/// <param name="userName">The name of the authenticated user. </param>
		/// <param name="createPersistentCookie">true to create a durable cookie (one that is saved across browser sessions); otherwise, false. </param>
		/// <param name="strCookiePath">The <see cref="P:System.Web.HttpCookie.Path" /> of the authentication cookie. </param>
		// Token: 0x06003654 RID: 13908 RVA: 0x0008C360 File Offset: 0x0008A560
		public static HttpCookie GetAuthCookie(string userName, bool createPersistentCookie, string strCookiePath)
		{
			FormsAuthentication.Initialize();
			if (userName == null)
			{
				userName = string.Empty;
			}
			if (strCookiePath == null || strCookiePath.Length == 0)
			{
				strCookiePath = FormsAuthentication.cookiePath;
			}
			DateTime now = DateTime.Now;
			DateTime dateTime = now.AddMinutes((double)FormsAuthentication.timeout);
			DateTime dateTime2 = (createPersistentCookie ? dateTime : DateTime.MinValue);
			FormsAuthenticationTicket formsAuthenticationTicket = new FormsAuthenticationTicket(1, userName, now, dateTime, createPersistentCookie, string.Empty, FormsAuthentication.cookiePath);
			HttpCookie httpCookie = new HttpCookie(FormsAuthentication.cookieName, FormsAuthentication.Encrypt(formsAuthenticationTicket), strCookiePath, dateTime2);
			if (FormsAuthentication.requireSSL)
			{
				httpCookie.Secure = true;
			}
			if (!string.IsNullOrEmpty(FormsAuthentication.cookie_domain))
			{
				httpCookie.Domain = FormsAuthentication.cookie_domain;
			}
			return httpCookie;
		}

		// Token: 0x17001119 RID: 4377
		// (get) Token: 0x06003655 RID: 13909 RVA: 0x0008C400 File Offset: 0x0008A600
		internal static string ReturnUrl
		{
			get
			{
				return HttpContext.Current.Request["RETURNURL"];
			}
		}

		/// <summary>Returns the redirect URL for the original request that caused the redirect to the login page.</summary>
		/// <returns>A string that contains the redirect URL.</returns>
		/// <param name="userName">The name of the authenticated user. </param>
		/// <param name="createPersistentCookie">This parameter is ignored.</param>
		// Token: 0x06003656 RID: 13910 RVA: 0x0008C418 File Offset: 0x0008A618
		public static string GetRedirectUrl(string userName, bool createPersistentCookie)
		{
			if (userName == null)
			{
				return null;
			}
			FormsAuthentication.Initialize();
			HttpRequest request = HttpContext.Current.Request;
			string text = FormsAuthentication.ReturnUrl;
			if (text != null)
			{
				return text;
			}
			text = request.ApplicationPath;
			string physicalApplicationPath = request.PhysicalApplicationPath;
			bool flag = false;
			foreach (string text2 in FormsAuthentication.indexFiles)
			{
				if (File.Exists(Path.Combine(physicalApplicationPath, text2)))
				{
					text = UrlUtils.Combine(text, text2);
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				text = UrlUtils.Combine(text, "index.aspx");
			}
			return text;
		}

		// Token: 0x06003657 RID: 13911 RVA: 0x0008C4A4 File Offset: 0x0008A6A4
		private static string HashPasswordForStoringInConfigFile(string password, FormsAuthPasswordFormat passwordFormat)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			byte[] array;
			if (passwordFormat != FormsAuthPasswordFormat.SHA1)
			{
				if (passwordFormat != FormsAuthPasswordFormat.MD5)
				{
					throw new ArgumentException("The format must be either MD5 or SHA1", "passwordFormat");
				}
				array = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
			}
			else
			{
				array = SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(password));
			}
			return MachineKeySectionUtils.GetHexString(array);
		}

		/// <summary>Produces a hash password suitable for storing in a configuration file based on the specified password and hash algorithm.</summary>
		/// <returns>The hashed password.</returns>
		/// <param name="password">The password to hash. </param>
		/// <param name="passwordFormat">The hash algorithm to use. <paramref name="passwordFormat" /> is a String that represents one of the <see cref="T:System.Web.Configuration.FormsAuthPasswordFormat" /> enumeration values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="password" /> is null-or-<paramref name="passwordFormat" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="passwordFormat" /> is not a valid <see cref="T:System.Web.Configuration.FormsAuthPasswordFormat" /> value.</exception>
		// Token: 0x06003658 RID: 13912 RVA: 0x0008C510 File Offset: 0x0008A710
		public static string HashPasswordForStoringInConfigFile(string password, string passwordFormat)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			if (passwordFormat == null)
			{
				throw new ArgumentNullException("passwordFormat");
			}
			if (string.Compare(passwordFormat, "MD5", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return FormsAuthentication.HashPasswordForStoringInConfigFile(password, FormsAuthPasswordFormat.MD5);
			}
			if (string.Compare(passwordFormat, "SHA1", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return FormsAuthentication.HashPasswordForStoringInConfigFile(password, FormsAuthPasswordFormat.SHA1);
			}
			throw new ArgumentException("The format must be either MD5 or SHA1", "passwordFormat");
		}

		/// <summary>Initializes the <see cref="T:System.Web.Security.FormsAuthentication" /> object based on the configuration settings for the application.</summary>
		// Token: 0x06003659 RID: 13913 RVA: 0x0008C574 File Offset: 0x0008A774
		public static void Initialize()
		{
			if (FormsAuthentication.initialized)
			{
				return;
			}
			object obj = FormsAuthentication.locker;
			lock (obj)
			{
				if (!FormsAuthentication.initialized)
				{
					FormsAuthenticationConfiguration forms = ((AuthenticationSection)WebConfigurationManager.GetSection(FormsAuthentication.authConfigPath)).Forms;
					FormsAuthentication.cookieName = forms.Name;
					FormsAuthentication.Timeout = forms.Timeout;
					FormsAuthentication.timeout = (int)forms.Timeout.TotalMinutes;
					FormsAuthentication.cookiePath = forms.Path;
					FormsAuthentication.protection = forms.Protection;
					FormsAuthentication.requireSSL = forms.RequireSSL;
					FormsAuthentication.slidingExpiration = forms.SlidingExpiration;
					FormsAuthentication.cookie_domain = forms.Domain;
					FormsAuthentication.cookie_mode = forms.Cookieless;
					FormsAuthentication.cookies_supported = true;
					if (!string.IsNullOrEmpty(FormsAuthentication.default_url))
					{
						FormsAuthentication.default_url = FormsAuthentication.MapUrl(FormsAuthentication.default_url);
					}
					else
					{
						FormsAuthentication.default_url = FormsAuthentication.MapUrl(forms.DefaultUrl);
					}
					FormsAuthentication.enable_crossapp_redirects = forms.EnableCrossAppRedirects;
					if (!string.IsNullOrEmpty(FormsAuthentication.login_url))
					{
						FormsAuthentication.login_url = FormsAuthentication.MapUrl(FormsAuthentication.login_url);
					}
					else
					{
						FormsAuthentication.login_url = FormsAuthentication.MapUrl(forms.LoginUrl);
					}
					FormsAuthentication.initialized = true;
				}
			}
		}

		// Token: 0x0600365A RID: 13914 RVA: 0x0008C6C0 File Offset: 0x0008A8C0
		private static string MapUrl(string url)
		{
			if (UrlUtils.IsRelativeUrl(url))
			{
				return UrlUtils.Combine(HttpRuntime.AppDomainAppVirtualPath, url);
			}
			return UrlUtils.ResolveVirtualPathFromAppAbsolute(url);
		}

		/// <summary>Redirects an authenticated user back to the originally requested URL or the default URL.</summary>
		/// <param name="userName">The authenticated user name. </param>
		/// <param name="createPersistentCookie">true to create a durable cookie (one that is saved across browser sessions); otherwise, false. </param>
		/// <exception cref="T:System.Web.HttpException">The return URL specified in the query string contains a protocol other than HTTP: or HTTPS:.</exception>
		// Token: 0x0600365B RID: 13915 RVA: 0x0008C6DC File Offset: 0x0008A8DC
		public static void RedirectFromLoginPage(string userName, bool createPersistentCookie)
		{
			FormsAuthentication.RedirectFromLoginPage(userName, createPersistentCookie, null);
		}

		/// <summary>Redirects an authenticated user back to the originally requested URL or the default URL using the specified cookie path for the forms-authentication cookie.</summary>
		/// <param name="userName">The authenticated user name. </param>
		/// <param name="createPersistentCookie">true to create a durable cookie (one that is saved across browser sessions); otherwise, false. </param>
		/// <param name="strCookiePath">The cookie path for the forms-authentication ticket. </param>
		/// <exception cref="T:System.Web.HttpException">The return URL specified in the query string contains a protocol other than HTTP: or HTTPS:.</exception>
		// Token: 0x0600365C RID: 13916 RVA: 0x0008C6E6 File Offset: 0x0008A8E6
		public static void RedirectFromLoginPage(string userName, bool createPersistentCookie, string strCookiePath)
		{
			if (userName == null)
			{
				return;
			}
			FormsAuthentication.Initialize();
			FormsAuthentication.SetAuthCookie(userName, createPersistentCookie, strCookiePath);
			FormsAuthentication.Redirect(FormsAuthentication.GetRedirectUrl(userName, createPersistentCookie), false);
		}

		/// <summary>Conditionally updates the issue date and time and expiration date and time for a <see cref="T:System.Web.Security.FormsAuthenticationTicket" />.</summary>
		/// <returns>The updated <see cref="T:System.Web.Security.FormsAuthenticationTicket" />.</returns>
		/// <param name="tOld">The forms-authentication ticket to update.</param>
		// Token: 0x0600365D RID: 13917 RVA: 0x0008C708 File Offset: 0x0008A908
		public static FormsAuthenticationTicket RenewTicketIfOld(FormsAuthenticationTicket tOld)
		{
			if (tOld == null)
			{
				return null;
			}
			DateTime now = DateTime.Now;
			TimeSpan timeSpan = now - tOld.IssueDate;
			if (tOld.Expiration - now > timeSpan)
			{
				return tOld;
			}
			FormsAuthenticationTicket formsAuthenticationTicket = tOld.Clone();
			formsAuthenticationTicket.SetDates(now, now + (tOld.Expiration - tOld.IssueDate));
			return formsAuthenticationTicket;
		}

		/// <summary>Creates an authentication ticket for the supplied user name and adds it to the cookies collection of the response, or to the URL if you are using cookieless authentication.</summary>
		/// <param name="userName">The name of an authenticated user. This does not have to map to a Windows account. </param>
		/// <param name="createPersistentCookie">true to create a persistent cookie (one that is saved across browser sessions); otherwise, false. </param>
		/// <exception cref="T:System.Web.HttpException">
		///   <see cref="P:System.Web.Security.FormsAuthentication.RequireSSL" /> is true and <see cref="P:System.Web.HttpRequest.IsSecureConnection" /> is false.</exception>
		// Token: 0x0600365E RID: 13918 RVA: 0x0008C767 File Offset: 0x0008A967
		public static void SetAuthCookie(string userName, bool createPersistentCookie)
		{
			FormsAuthentication.Initialize();
			FormsAuthentication.SetAuthCookie(userName, createPersistentCookie, FormsAuthentication.cookiePath);
		}

		/// <summary>Creates an authentication ticket for the supplied user name and adds it to the cookies collection of the response, using the supplied cookie path, or using the URL if you are using cookieless authentication.</summary>
		/// <param name="userName">The name of an authenticated user. </param>
		/// <param name="createPersistentCookie">true to create a durable cookie (one that is saved across browser sessions); otherwise, false. </param>
		/// <param name="strCookiePath">The cookie path for the forms-authentication ticket.</param>
		/// <exception cref="T:System.Web.HttpException">
		///   <see cref="P:System.Web.Security.FormsAuthentication.RequireSSL" /> is true and <see cref="P:System.Web.HttpRequest.IsSecureConnection" /> is false.</exception>
		// Token: 0x0600365F RID: 13919 RVA: 0x0008C77A File Offset: 0x0008A97A
		public static void SetAuthCookie(string userName, bool createPersistentCookie, string strCookiePath)
		{
			HttpContext httpContext = HttpContext.Current;
			if (httpContext == null)
			{
				throw new HttpException("Context is null!");
			}
			HttpResponse response = httpContext.Response;
			if (response == null)
			{
				throw new HttpException("Response is null!");
			}
			response.Cookies.Add(FormsAuthentication.GetAuthCookie(userName, createPersistentCookie, strCookiePath));
		}

		/// <summary>Removes the forms-authentication ticket from the browser.</summary>
		// Token: 0x06003660 RID: 13920 RVA: 0x0008C7B4 File Offset: 0x0008A9B4
		public static void SignOut()
		{
			FormsAuthentication.Initialize();
			HttpContext httpContext = HttpContext.Current;
			if (httpContext == null)
			{
				throw new HttpException("Context is null!");
			}
			HttpResponse response = httpContext.Response;
			if (response == null)
			{
				throw new HttpException("Response is null!");
			}
			HttpCookieCollection cookies = response.Cookies;
			cookies.Remove(FormsAuthentication.cookieName);
			HttpCookie httpCookie = new HttpCookie(FormsAuthentication.cookieName, string.Empty);
			httpCookie.Expires = new DateTime(1999, 10, 12);
			httpCookie.Path = FormsAuthentication.cookiePath;
			if (!string.IsNullOrEmpty(FormsAuthentication.cookie_domain))
			{
				httpCookie.Domain = FormsAuthentication.cookie_domain;
			}
			cookies.Add(httpCookie);
			Roles.DeleteCookie();
		}

		/// <summary>Gets the name of the cookie used to store the forms-authentication ticket.</summary>
		/// <returns>The name of the cookie used to store the forms-authentication ticket. The default is ".ASPXAUTH".</returns>
		// Token: 0x1700111A RID: 4378
		// (get) Token: 0x06003661 RID: 13921 RVA: 0x0008C84D File Offset: 0x0008AA4D
		public static string FormsCookieName
		{
			get
			{
				FormsAuthentication.Initialize();
				return FormsAuthentication.cookieName;
			}
		}

		/// <summary>Gets the path for the forms-authentication cookie.</summary>
		/// <returns>The path of the cookie where the forms-authentication ticket information is stored. The default is "/".</returns>
		// Token: 0x1700111B RID: 4379
		// (get) Token: 0x06003662 RID: 13922 RVA: 0x0008C859 File Offset: 0x0008AA59
		public static string FormsCookiePath
		{
			get
			{
				FormsAuthentication.Initialize();
				return FormsAuthentication.cookiePath;
			}
		}

		/// <summary>Gets a value indicating whether the forms-authentication cookie requires SSL in order to be returned to the server.</summary>
		/// <returns>true if SSL is required to return the forms-authentication cookie to the server; otherwise, false. The default is false.</returns>
		// Token: 0x1700111C RID: 4380
		// (get) Token: 0x06003663 RID: 13923 RVA: 0x0008C865 File Offset: 0x0008AA65
		public static bool RequireSSL
		{
			get
			{
				FormsAuthentication.Initialize();
				return FormsAuthentication.requireSSL;
			}
		}

		/// <summary>Gets a value indicating whether sliding expiration is enabled.</summary>
		/// <returns>true if sliding expiration is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x1700111D RID: 4381
		// (get) Token: 0x06003664 RID: 13924 RVA: 0x0008C871 File Offset: 0x0008AA71
		public static bool SlidingExpiration
		{
			get
			{
				FormsAuthentication.Initialize();
				return FormsAuthentication.slidingExpiration;
			}
		}

		/// <summary>Gets the value of the domain of the forms-authentication cookie.</summary>
		/// <returns>The <see cref="P:System.Web.HttpCookie.Domain" /> of the forms-authentication cookie. The default is an empty string ("").</returns>
		// Token: 0x1700111E RID: 4382
		// (get) Token: 0x06003665 RID: 13925 RVA: 0x0008C87D File Offset: 0x0008AA7D
		public static string CookieDomain
		{
			get
			{
				FormsAuthentication.Initialize();
				return FormsAuthentication.cookie_domain;
			}
		}

		/// <summary>Gets a value that indicates whether the application is configured for cookieless forms authentication.</summary>
		/// <returns>One of the <see cref="T:System.Web.HttpCookieMode" /> values that indicates whether the application is configured for cookieless forms authentication. The default is <see cref="F:System.Web.HttpCookieMode.UseDeviceProfile" />.</returns>
		// Token: 0x1700111F RID: 4383
		// (get) Token: 0x06003666 RID: 13926 RVA: 0x0008C889 File Offset: 0x0008AA89
		public static HttpCookieMode CookieMode
		{
			get
			{
				FormsAuthentication.Initialize();
				return FormsAuthentication.cookie_mode;
			}
		}

		/// <summary>Gets a value that indicates whether the application is configured to support cookieless forms authentication.</summary>
		/// <returns>false if the application is configured to support cookieless forms authentication; otherwise, true.</returns>
		// Token: 0x17001120 RID: 4384
		// (get) Token: 0x06003667 RID: 13927 RVA: 0x0008C895 File Offset: 0x0008AA95
		public static bool CookiesSupported
		{
			get
			{
				FormsAuthentication.Initialize();
				return FormsAuthentication.cookies_supported;
			}
		}

		/// <summary>Gets the URL that the <see cref="T:System.Web.Security.FormsAuthentication" /> class will redirect to if no redirect URL is specified.</summary>
		/// <returns>The URL that the <see cref="T:System.Web.Security.FormsAuthentication" /> class will redirect to if no redirect URL is specified. The default is "default.aspx."</returns>
		// Token: 0x17001121 RID: 4385
		// (get) Token: 0x06003668 RID: 13928 RVA: 0x0008C8A1 File Offset: 0x0008AAA1
		public static string DefaultUrl
		{
			get
			{
				FormsAuthentication.Initialize();
				return FormsAuthentication.default_url;
			}
		}

		/// <summary>Gets a value indicating whether authenticated users can be redirected to URLs in other Web applications.</summary>
		/// <returns>true if authenticated users can be redirected to URLs in other Web applications; otherwise, false. The default is false.</returns>
		// Token: 0x17001122 RID: 4386
		// (get) Token: 0x06003669 RID: 13929 RVA: 0x0008C8AD File Offset: 0x0008AAAD
		public static bool EnableCrossAppRedirects
		{
			get
			{
				FormsAuthentication.Initialize();
				return FormsAuthentication.enable_crossapp_redirects;
			}
		}

		/// <summary>Gets the URL for the login page that the <see cref="T:System.Web.Security.FormsAuthentication" /> class will redirect to.</summary>
		/// <returns>The URL for the login page that the <see cref="T:System.Web.Security.FormsAuthentication" /> class will redirect to. The default is "login.aspx."</returns>
		// Token: 0x17001123 RID: 4387
		// (get) Token: 0x0600366A RID: 13930 RVA: 0x0008C8B9 File Offset: 0x0008AAB9
		public static string LoginUrl
		{
			get
			{
				FormsAuthentication.Initialize();
				return FormsAuthentication.login_url;
			}
		}

		/// <summary>Redirects the browser to the login URL.</summary>
		// Token: 0x0600366B RID: 13931 RVA: 0x0008C8C5 File Offset: 0x0008AAC5
		public static void RedirectToLoginPage()
		{
			FormsAuthentication.Redirect(FormsAuthentication.LoginUrl);
		}

		/// <summary>Redirects the browser to the login URL with the specified query string.</summary>
		/// <param name="extraQueryString">The query string to include with the redirect URL.</param>
		// Token: 0x0600366C RID: 13932 RVA: 0x0008C8D1 File Offset: 0x0008AAD1
		[global::System.MonoTODO("needs more tests")]
		public static void RedirectToLoginPage(string extraQueryString)
		{
			FormsAuthentication.Redirect(FormsAuthentication.LoginUrl + "?" + extraQueryString);
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x0008C8E8 File Offset: 0x0008AAE8
		private static void Redirect(string url)
		{
			HttpContext.Current.Response.Redirect(url);
		}

		// Token: 0x0600366E RID: 13934 RVA: 0x0008C8FA File Offset: 0x0008AAFA
		private static void Redirect(string url, bool end)
		{
			HttpContext.Current.Response.Redirect(url, end);
		}

		// Token: 0x17001124 RID: 4388
		// (get) Token: 0x06003670 RID: 13936 RVA: 0x0008C970 File Offset: 0x0008AB70
		public static SameSiteMode CookieSameSite
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return SameSiteMode.None;
			}
		}

		/// <summary>Gets a value that indicates whether to use Coordinated Universal Time (UTC) or local time for the ticket expiration date.</summary>
		/// <returns>A value that indicates whether to use Coordinated Universal Time (UTC) or local time for the ticket expiration date.</returns>
		// Token: 0x17001125 RID: 4389
		// (get) Token: 0x06003671 RID: 13937 RVA: 0x0008C98C File Offset: 0x0008AB8C
		public static TicketCompatibilityMode TicketCompatibilityMode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return TicketCompatibilityMode.Framework20;
			}
		}

		// Token: 0x04001DB3 RID: 7603
		private static string authConfigPath = "system.web/authentication";

		// Token: 0x04001DB4 RID: 7604
		private static string machineKeyConfigPath = "system.web/machineKey";

		// Token: 0x04001DB5 RID: 7605
		private static object locker = new object();

		// Token: 0x04001DB6 RID: 7606
		private static bool initialized;

		// Token: 0x04001DB7 RID: 7607
		private static string cookieName;

		// Token: 0x04001DB8 RID: 7608
		private static string cookiePath;

		// Token: 0x04001DB9 RID: 7609
		private static int timeout;

		// Token: 0x04001DBA RID: 7610
		private static FormsProtectionEnum protection;

		// Token: 0x04001DBB RID: 7611
		private static bool requireSSL;

		// Token: 0x04001DBC RID: 7612
		private static bool slidingExpiration;

		// Token: 0x04001DBD RID: 7613
		private static string cookie_domain;

		// Token: 0x04001DBE RID: 7614
		private static HttpCookieMode cookie_mode;

		// Token: 0x04001DBF RID: 7615
		private static bool cookies_supported;

		// Token: 0x04001DC0 RID: 7616
		private static string default_url;

		// Token: 0x04001DC1 RID: 7617
		private static bool enable_crossapp_redirects;

		// Token: 0x04001DC2 RID: 7618
		private static string login_url;

		// Token: 0x04001DC3 RID: 7619
		private static string[] indexFiles = new string[] { "index.aspx", "Default.aspx", "default.aspx", "index.html", "index.htm" };
	}
}
