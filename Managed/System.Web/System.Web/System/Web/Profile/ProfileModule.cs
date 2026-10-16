using System;
using System.ComponentModel;
using System.Text;
using System.Web.Configuration;

namespace System.Web.Profile
{
	/// <summary>Manages the creation of the user profile and profile events. This class cannot be inherited.</summary>
	// Token: 0x02000508 RID: 1288
	public sealed class ProfileModule : IHttpModule
	{
		/// <summary>Occurs when the anonymous user for a profile logs in.</summary>
		// Token: 0x14000109 RID: 265
		// (add) Token: 0x06003941 RID: 14657 RVA: 0x00098166 File Offset: 0x00096366
		// (remove) Token: 0x06003942 RID: 14658 RVA: 0x00098179 File Offset: 0x00096379
		public event ProfileMigrateEventHandler MigrateAnonymous
		{
			add
			{
				this.events.AddHandler(ProfileModule.migrateAnonymousEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(ProfileModule.migrateAnonymousEvent, value);
			}
		}

		/// <summary>Occurs before the user profile is created.</summary>
		// Token: 0x1400010A RID: 266
		// (add) Token: 0x06003943 RID: 14659 RVA: 0x0009818C File Offset: 0x0009638C
		// (remove) Token: 0x06003944 RID: 14660 RVA: 0x0009819F File Offset: 0x0009639F
		[global::System.MonoTODO("implement event rising")]
		public event ProfileEventHandler Personalize
		{
			add
			{
				this.events.AddHandler(ProfileModule.personalizeEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(ProfileModule.personalizeEvent, value);
			}
		}

		/// <summary>Occurs at the end of page execution if automatic profile saving is enabled.</summary>
		// Token: 0x1400010B RID: 267
		// (add) Token: 0x06003945 RID: 14661 RVA: 0x000981B2 File Offset: 0x000963B2
		// (remove) Token: 0x06003946 RID: 14662 RVA: 0x000981C5 File Offset: 0x000963C5
		public event ProfileAutoSaveEventHandler ProfileAutoSaving
		{
			add
			{
				this.events.AddHandler(ProfileModule.profileAutoSavingEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(ProfileModule.profileAutoSavingEvent, value);
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Web.Profile.ProfileModule" />. </summary>
		// Token: 0x06003948 RID: 14664 RVA: 0x000981EB File Offset: 0x000963EB
		public void Dispose()
		{
			this.app.EndRequest -= this.OnLeave;
			this.app.PostMapRequestHandler -= this.OnEnter;
		}

		/// <summary>Calls initialization code when a <see cref="T:System.Web.Profile.ProfileModule" /> object is created.</summary>
		/// <param name="app">The current application. </param>
		// Token: 0x06003949 RID: 14665 RVA: 0x0009821C File Offset: 0x0009641C
		public void Init(HttpApplication app)
		{
			this.app = app;
			app.PostMapRequestHandler += this.OnEnter;
			app.EndRequest += this.OnLeave;
			AnonymousIdentificationSection anonymousIdentificationSection = (AnonymousIdentificationSection)WebConfigurationManager.GetSection("system.web/anonymousIdentification");
			if (anonymousIdentificationSection == null)
			{
				return;
			}
			this.anonymousCookieName = anonymousIdentificationSection.CookieName;
		}

		// Token: 0x0600394A RID: 14666 RVA: 0x00098274 File Offset: 0x00096474
		private void OnEnter(object o, EventArgs eventArgs)
		{
			if (!ProfileManager.Enabled)
			{
				return;
			}
			if (HttpContext.Current.Request.IsAuthenticated)
			{
				HttpCookie httpCookie = this.app.Request.Cookies[this.anonymousCookieName];
				if (httpCookie != null && httpCookie.Expires != DateTime.MinValue && httpCookie.Expires > DateTime.Now)
				{
					ProfileMigrateEventHandler profileMigrateEventHandler = this.events[ProfileModule.migrateAnonymousEvent] as ProfileMigrateEventHandler;
					if (profileMigrateEventHandler != null)
					{
						ProfileMigrateEventArgs e = new ProfileMigrateEventArgs(HttpContext.Current, Encoding.Unicode.GetString(Convert.FromBase64String(httpCookie.Value)));
						profileMigrateEventHandler(this, e);
					}
					HttpCookie httpCookie2 = new HttpCookie(this.anonymousCookieName);
					httpCookie2.Path = this.app.Request.ApplicationPath;
					httpCookie2.Expires = new DateTime(1970, 1, 1);
					httpCookie2.Value = "";
					this.app.Response.AppendCookie(httpCookie2);
				}
			}
		}

		// Token: 0x0600394B RID: 14667 RVA: 0x0009837C File Offset: 0x0009657C
		private void OnLeave(object o, EventArgs eventArgs)
		{
			if (!ProfileManager.Enabled)
			{
				return;
			}
			if (!this.app.Context.ProfileInitialized)
			{
				return;
			}
			if (ProfileManager.AutomaticSaveEnabled)
			{
				this.profile = this.app.Context.Profile;
				if (this.profile == null)
				{
					return;
				}
				ProfileAutoSaveEventHandler profileAutoSaveEventHandler = this.events[ProfileModule.profileAutoSavingEvent] as ProfileAutoSaveEventHandler;
				if (profileAutoSaveEventHandler != null)
				{
					ProfileAutoSaveEventArgs e = new ProfileAutoSaveEventArgs(this.app.Context);
					profileAutoSaveEventHandler(this, e);
					if (!e.ContinueWithProfileAutoSave)
					{
						return;
					}
				}
				this.profile.Save();
			}
		}

		// Token: 0x04001F26 RID: 7974
		private static readonly object migrateAnonymousEvent = new object();

		// Token: 0x04001F27 RID: 7975
		private static readonly object personalizeEvent = new object();

		// Token: 0x04001F28 RID: 7976
		private static readonly object profileAutoSavingEvent = new object();

		// Token: 0x04001F29 RID: 7977
		private HttpApplication app;

		// Token: 0x04001F2A RID: 7978
		private ProfileBase profile;

		// Token: 0x04001F2B RID: 7979
		private string anonymousCookieName;

		// Token: 0x04001F2C RID: 7980
		private EventHandlerList events = new EventHandlerList();
	}
}
