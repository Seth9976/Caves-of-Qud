using System;

namespace System.Web
{
	// Token: 0x02000075 RID: 117
	internal class HeadersCollection : BaseParamsCollection
	{
		// Token: 0x0600047C RID: 1148 RVA: 0x0000943E File Offset: 0x0000763E
		public HeadersCollection(HttpRequest request)
			: base(request)
		{
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00009447 File Offset: 0x00007647
		public override void Add(string name, string value)
		{
			if (base.IsReadOnly)
			{
				throw new PlatformNotSupportedException();
			}
			base.Set(name, value);
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00009447 File Offset: 0x00007647
		public override void Set(string name, string value)
		{
			if (base.IsReadOnly)
			{
				throw new PlatformNotSupportedException();
			}
			base.Set(name, value);
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0000945F File Offset: 0x0000765F
		public override void Remove(string name)
		{
			if (base.IsReadOnly)
			{
				throw new PlatformNotSupportedException();
			}
			base.Remove(name);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00009478 File Offset: 0x00007678
		protected override void InsertInfo()
		{
			HttpWorkerRequest workerRequest = this._request.WorkerRequest;
			if (workerRequest != null)
			{
				for (int i = 0; i < 40; i++)
				{
					string knownRequestHeader = workerRequest.GetKnownRequestHeader(i);
					if (knownRequestHeader != null && !(knownRequestHeader == ""))
					{
						this.Add(HttpWorkerRequest.GetKnownRequestHeaderName(i), knownRequestHeader);
					}
				}
				string[][] unknownRequestHeaders = workerRequest.GetUnknownRequestHeaders();
				if (unknownRequestHeaders != null && unknownRequestHeaders.GetUpperBound(0) != -1)
				{
					int num = unknownRequestHeaders.GetUpperBound(0) + 1;
					for (int j = 0; j < num; j++)
					{
						this.Add(unknownRequestHeaders[j][0], unknownRequestHeaders[j][1]);
					}
				}
				base.Protect();
			}
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00009510 File Offset: 0x00007710
		protected override string InternalGet(string name)
		{
			int knownRequestHeaderIndex = HttpWorkerRequest.GetKnownRequestHeaderIndex(name);
			string text = null;
			if (knownRequestHeaderIndex >= 0)
			{
				text = this._request.WorkerRequest.GetKnownRequestHeader(knownRequestHeaderIndex);
			}
			if (text == null)
			{
				text = this._request.WorkerRequest.GetUnknownRequestHeader(name);
			}
			return text;
		}
	}
}
