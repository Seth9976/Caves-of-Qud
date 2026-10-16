using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Stores ASP.NET page view state on the Web client in a hidden HTML element. </summary>
	// Token: 0x020001CD RID: 461
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HiddenFieldPageStatePersister : PageStatePersister
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HiddenFieldPageStatePersister" /> class.</summary>
		/// <param name="page">The <see cref="T:System.Web.UI.Page" /> that the view state persistence mechanism is created for.</param>
		// Token: 0x060012A9 RID: 4777 RVA: 0x00030AE2 File Offset: 0x0002ECE2
		public HiddenFieldPageStatePersister(Page page)
			: base(page)
		{
		}

		/// <summary>Deserializes and loads persisted state information from an <see cref="T:System.Web.HttpRequest" /> object when a <see cref="T:System.Web.UI.Page" /> object initializes its control hierarchy.</summary>
		/// <exception cref="T:System.Web.HttpException">The <see cref="M:System.Web.UI.HiddenFieldPageStatePersister.Load" /> method could not successfully deserialize the state information contained in the request to the Web server.</exception>
		// Token: 0x060012AA RID: 4778 RVA: 0x00030AEC File Offset: 0x0002ECEC
		public override void Load()
		{
			string rawViewState = base.Page.RawViewState;
			IStateFormatter stateFormatter = base.StateFormatter;
			if (!string.IsNullOrEmpty(rawViewState))
			{
				Pair pair = stateFormatter.Deserialize(rawViewState) as Pair;
				if (pair != null)
				{
					base.ViewState = pair.First;
					base.ControlState = pair.Second;
				}
			}
		}

		/// <summary>Serializes any object state contained in the <see cref="P:System.Web.UI.PageStatePersister.ViewState" /> or <see cref="P:System.Web.UI.PageStatePersister.ControlState" /> property and writes the state to the response stream.</summary>
		// Token: 0x060012AB RID: 4779 RVA: 0x00030B3C File Offset: 0x0002ED3C
		public override void Save()
		{
			IStateFormatter stateFormatter = base.StateFormatter;
			base.Page.RawViewState = stateFormatter.Serialize(new Pair(base.ViewState, base.ControlState));
		}
	}
}
