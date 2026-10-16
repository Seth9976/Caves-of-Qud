using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Net
{
	// Token: 0x02000636 RID: 1590
	internal abstract class ProxyChain : IEnumerable<Uri>, IEnumerable, IDisposable
	{
		// Token: 0x0600322A RID: 12842 RVA: 0x000AD888 File Offset: 0x000ABA88
		protected ProxyChain(Uri destination)
		{
			this.m_Destination = destination;
		}

		// Token: 0x0600322B RID: 12843 RVA: 0x000AD8A4 File Offset: 0x000ABAA4
		public IEnumerator<Uri> GetEnumerator()
		{
			ProxyChain.ProxyEnumerator proxyEnumerator = new ProxyChain.ProxyEnumerator(this);
			if (this.m_MainEnumerator == null)
			{
				this.m_MainEnumerator = proxyEnumerator;
			}
			return proxyEnumerator;
		}

		// Token: 0x0600322C RID: 12844 RVA: 0x000AD8C8 File Offset: 0x000ABAC8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0600322D RID: 12845 RVA: 0x00003917 File Offset: 0x00001B17
		public virtual void Dispose()
		{
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x0600322E RID: 12846 RVA: 0x000AD8D0 File Offset: 0x000ABAD0
		internal IEnumerator<Uri> Enumerator
		{
			get
			{
				if (this.m_MainEnumerator != null)
				{
					return this.m_MainEnumerator;
				}
				return this.GetEnumerator();
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x0600322F RID: 12847 RVA: 0x000AD8F4 File Offset: 0x000ABAF4
		internal Uri Destination
		{
			get
			{
				return this.m_Destination;
			}
		}

		// Token: 0x06003230 RID: 12848 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void Abort()
		{
		}

		// Token: 0x06003231 RID: 12849 RVA: 0x000AD8FC File Offset: 0x000ABAFC
		internal bool HttpAbort(HttpWebRequest request, WebException webException)
		{
			this.Abort();
			return true;
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06003232 RID: 12850 RVA: 0x000AD905 File Offset: 0x000ABB05
		internal HttpAbortDelegate HttpAbortDelegate
		{
			get
			{
				if (this.m_HttpAbortDelegate == null)
				{
					this.m_HttpAbortDelegate = new HttpAbortDelegate(this.HttpAbort);
				}
				return this.m_HttpAbortDelegate;
			}
		}

		// Token: 0x06003233 RID: 12851
		protected abstract bool GetNextProxy(out Uri proxy);

		// Token: 0x04001D6E RID: 7534
		private List<Uri> m_Cache = new List<Uri>();

		// Token: 0x04001D6F RID: 7535
		private bool m_CacheComplete;

		// Token: 0x04001D70 RID: 7536
		private ProxyChain.ProxyEnumerator m_MainEnumerator;

		// Token: 0x04001D71 RID: 7537
		private Uri m_Destination;

		// Token: 0x04001D72 RID: 7538
		private HttpAbortDelegate m_HttpAbortDelegate;

		// Token: 0x02000637 RID: 1591
		private class ProxyEnumerator : IEnumerator<Uri>, IDisposable, IEnumerator
		{
			// Token: 0x06003234 RID: 12852 RVA: 0x000AD927 File Offset: 0x000ABB27
			internal ProxyEnumerator(ProxyChain chain)
			{
				this.m_Chain = chain;
			}

			// Token: 0x17000A11 RID: 2577
			// (get) Token: 0x06003235 RID: 12853 RVA: 0x000AD93D File Offset: 0x000ABB3D
			public Uri Current
			{
				get
				{
					if (this.m_Finished || this.m_CurrentIndex < 0)
					{
						throw new InvalidOperationException(SR.GetString("Enumeration has either not started or has already finished."));
					}
					return this.m_Chain.m_Cache[this.m_CurrentIndex];
				}
			}

			// Token: 0x17000A12 RID: 2578
			// (get) Token: 0x06003236 RID: 12854 RVA: 0x000AD976 File Offset: 0x000ABB76
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06003237 RID: 12855 RVA: 0x000AD980 File Offset: 0x000ABB80
			public bool MoveNext()
			{
				if (this.m_Finished)
				{
					return false;
				}
				checked
				{
					this.m_CurrentIndex++;
					if (this.m_Chain.m_Cache.Count > this.m_CurrentIndex)
					{
						return true;
					}
					if (this.m_Chain.m_CacheComplete)
					{
						this.m_Finished = true;
						return false;
					}
					List<Uri> cache = this.m_Chain.m_Cache;
					bool flag2;
					lock (cache)
					{
						if (this.m_Chain.m_Cache.Count > this.m_CurrentIndex)
						{
							flag2 = true;
						}
						else if (this.m_Chain.m_CacheComplete)
						{
							this.m_Finished = true;
							flag2 = false;
						}
						else
						{
							Uri uri;
							while (this.m_Chain.GetNextProxy(out uri))
							{
								if (uri == null)
								{
									if (this.m_TriedDirect)
									{
										continue;
									}
									this.m_TriedDirect = true;
								}
								this.m_Chain.m_Cache.Add(uri);
								return true;
							}
							this.m_Finished = true;
							this.m_Chain.m_CacheComplete = true;
							flag2 = false;
						}
					}
					return flag2;
				}
			}

			// Token: 0x06003238 RID: 12856 RVA: 0x000ADA90 File Offset: 0x000ABC90
			public void Reset()
			{
				this.m_Finished = false;
				this.m_CurrentIndex = -1;
			}

			// Token: 0x06003239 RID: 12857 RVA: 0x00003917 File Offset: 0x00001B17
			public void Dispose()
			{
			}

			// Token: 0x04001D73 RID: 7539
			private ProxyChain m_Chain;

			// Token: 0x04001D74 RID: 7540
			private bool m_Finished;

			// Token: 0x04001D75 RID: 7541
			private int m_CurrentIndex = -1;

			// Token: 0x04001D76 RID: 7542
			private bool m_TriedDirect;
		}
	}
}
