using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Web.Hosting
{
	// Token: 0x02000532 RID: 1330
	[Serializable]
	internal class HostingEnvironmentException : Exception
	{
		// Token: 0x06003A27 RID: 14887 RVA: 0x0009B0C6 File Offset: 0x000992C6
		protected HostingEnvironmentException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this._details = info.GetString("_details");
		}

		// Token: 0x06003A28 RID: 14888 RVA: 0x0009B0E1 File Offset: 0x000992E1
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("_details", this._details);
		}

		// Token: 0x06003A29 RID: 14889 RVA: 0x0009B0FC File Offset: 0x000992FC
		internal HostingEnvironmentException(string message, string details)
			: base(message)
		{
			this._details = details;
		}

		// Token: 0x1700120B RID: 4619
		// (get) Token: 0x06003A2A RID: 14890 RVA: 0x0009B10C File Offset: 0x0009930C
		internal string Details
		{
			get
			{
				if (this._details == null)
				{
					return string.Empty;
				}
				return this._details;
			}
		}

		// Token: 0x04001FC8 RID: 8136
		private string _details;
	}
}
