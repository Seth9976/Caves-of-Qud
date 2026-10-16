using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using AiUnity.Common.Attributes;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Internal.NetworkSenders;
using AiUnity.NLog.Core.Layouts;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Targets
{
	// Token: 0x02000224 RID: 548
	[Preserve]
	public abstract class NetworkTarget : TargetWithLayout
	{
		// Token: 0x0600144F RID: 5199 RVA: 0x00072DFC File Offset: 0x00070FFC
		public NetworkTarget()
		{
			this.SenderFactory = NetworkSenderFactory.Default;
			this.Encoding = Encoding.UTF8;
			this.OnOverflow = NetworkTargetOverflowAction.Split;
			this.KeepConnection = true;
			this.MaxMessageSize = 65000;
			this.ConnectionCacheSize = 5;
			this.Address = "udp://127.0.0.1:9999";
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x00072E6B File Offset: 0x0007106B
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06001451 RID: 5201 RVA: 0x00072E72 File Offset: 0x00071072
		// (set) Token: 0x06001452 RID: 5202 RVA: 0x00072E7A File Offset: 0x0007107A
		[Display("Address", "Network address", false, 0)]
		[DefaultValue("udp://127.0.0.1:9999")]
		public Layout Address { get; set; }

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06001453 RID: 5203 RVA: 0x00072E83 File Offset: 0x00071083
		// (set) Token: 0x06001454 RID: 5204 RVA: 0x00072E8B File Offset: 0x0007108B
		[DefaultValue(true)]
		public bool KeepConnection { get; set; }

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06001455 RID: 5205 RVA: 0x00072E94 File Offset: 0x00071094
		// (set) Token: 0x06001456 RID: 5206 RVA: 0x00072E9C File Offset: 0x0007109C
		[DefaultValue(false)]
		public bool NewLine { get; set; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06001457 RID: 5207 RVA: 0x00072EA5 File Offset: 0x000710A5
		// (set) Token: 0x06001458 RID: 5208 RVA: 0x00072EAD File Offset: 0x000710AD
		[DefaultValue(65000)]
		public int MaxMessageSize { get; set; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06001459 RID: 5209 RVA: 0x00072EB6 File Offset: 0x000710B6
		// (set) Token: 0x0600145A RID: 5210 RVA: 0x00072EBE File Offset: 0x000710BE
		[DefaultValue(5)]
		public int ConnectionCacheSize { get; set; }

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x0600145B RID: 5211 RVA: 0x00072EC7 File Offset: 0x000710C7
		// (set) Token: 0x0600145C RID: 5212 RVA: 0x00072ECF File Offset: 0x000710CF
		[DefaultValue(0)]
		public int MaxQueueSize { get; set; }

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x0600145D RID: 5213 RVA: 0x00072ED8 File Offset: 0x000710D8
		// (set) Token: 0x0600145E RID: 5214 RVA: 0x00072EE0 File Offset: 0x000710E0
		public NetworkTargetOverflowAction OnOverflow { get; set; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x0600145F RID: 5215 RVA: 0x00072EE9 File Offset: 0x000710E9
		// (set) Token: 0x06001460 RID: 5216 RVA: 0x00072EF1 File Offset: 0x000710F1
		[DefaultValue("utf-8")]
		public Encoding Encoding { get; set; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06001461 RID: 5217 RVA: 0x00072EFA File Offset: 0x000710FA
		// (set) Token: 0x06001462 RID: 5218 RVA: 0x00072F02 File Offset: 0x00071102
		internal INetworkSenderFactory SenderFactory { get; set; }

		// Token: 0x06001463 RID: 5219 RVA: 0x00072F0C File Offset: 0x0007110C
		protected override void FlushAsync(AsyncContinuation asyncContinuation)
		{
			int remainingCount = 0;
			AsyncContinuation asyncContinuation2 = delegate(Exception ex)
			{
				if (Interlocked.Decrement(ref remainingCount) == 0)
				{
					asyncContinuation(null);
				}
			};
			List<NetworkSender> list = this.openNetworkSenders;
			lock (list)
			{
				remainingCount = this.openNetworkSenders.Count;
				if (remainingCount == 0)
				{
					asyncContinuation(null);
				}
				else
				{
					foreach (NetworkSender networkSender in this.openNetworkSenders)
					{
						networkSender.FlushAsync(asyncContinuation2);
					}
				}
			}
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x00072FD0 File Offset: 0x000711D0
		protected override void CloseTarget()
		{
			base.CloseTarget();
			List<NetworkSender> list = this.openNetworkSenders;
			lock (list)
			{
				using (List<NetworkSender>.Enumerator enumerator = this.openNetworkSenders.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						enumerator.Current.Close(delegate(Exception ex)
						{
						});
					}
				}
				this.openNetworkSenders.Clear();
			}
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x0007307C File Offset: 0x0007127C
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			NetworkTarget.<>c__DisplayClass43_0 CS$<>8__locals1 = new NetworkTarget.<>c__DisplayClass43_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.logEvent = logEvent;
			string text = this.Address.Render(CS$<>8__locals1.logEvent.LogEvent);
			byte[] bytesToWrite = this.GetBytesToWrite(CS$<>8__locals1.logEvent.LogEvent);
			if (this.KeepConnection)
			{
				NetworkSender sender2 = this.GetCachedNetworkSender(text);
				this.ChunkedSend(sender2, bytesToWrite, delegate(Exception ex)
				{
					if (ex != null)
					{
						NetworkTarget.Logger.Error("Error when sending {0}", new object[] { ex });
						CS$<>8__locals1.<>4__this.ReleaseCachedConnection(sender2);
					}
					CS$<>8__locals1.logEvent.Continuation(ex);
				});
				return;
			}
			NetworkSender sender = this.SenderFactory.Create(text, this.MaxQueueSize);
			sender.Initialize();
			List<NetworkSender> list = this.openNetworkSenders;
			lock (list)
			{
				this.openNetworkSenders.Add(sender);
				this.ChunkedSend(sender, bytesToWrite, delegate(Exception ex)
				{
					List<NetworkSender> list2 = CS$<>8__locals1.<>4__this.openNetworkSenders;
					lock (list2)
					{
						CS$<>8__locals1.<>4__this.openNetworkSenders.Remove(sender);
					}
					if (ex != null)
					{
						NetworkTarget.Logger.Error("Error when sending {0}", new object[] { ex });
					}
					sender.Close(delegate(Exception ex2)
					{
					});
					CS$<>8__locals1.logEvent.Continuation(ex);
				});
			}
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x00073198 File Offset: 0x00071398
		protected virtual byte[] GetBytesToWrite(LogEventInfo logEvent)
		{
			string text;
			if (this.NewLine)
			{
				text = this.Layout.Render(logEvent) + Environment.NewLine;
			}
			else
			{
				text = this.Layout.Render(logEvent);
			}
			return this.Encoding.GetBytes(text);
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x000731E0 File Offset: 0x000713E0
		private NetworkSender GetCachedNetworkSender(string address)
		{
			Dictionary<string, NetworkSender> dictionary = this.currentSenderCache;
			NetworkSender networkSender2;
			lock (dictionary)
			{
				NetworkSender networkSender;
				if (this.currentSenderCache.TryGetValue(address, out networkSender))
				{
					networkSender.CheckSocket();
					networkSender2 = networkSender;
				}
				else
				{
					if (this.currentSenderCache.Count >= this.ConnectionCacheSize)
					{
						int num = int.MaxValue;
						NetworkSender networkSender3 = null;
						foreach (KeyValuePair<string, NetworkSender> keyValuePair in this.currentSenderCache)
						{
							if (keyValuePair.Value.LastSendTime < num)
							{
								num = keyValuePair.Value.LastSendTime;
								networkSender3 = keyValuePair.Value;
							}
						}
						if (networkSender3 != null)
						{
							this.ReleaseCachedConnection(networkSender3);
						}
					}
					networkSender = this.SenderFactory.Create(address, this.MaxQueueSize);
					networkSender.Initialize();
					List<NetworkSender> list = this.openNetworkSenders;
					lock (list)
					{
						this.openNetworkSenders.Add(networkSender);
					}
					this.currentSenderCache.Add(address, networkSender);
					networkSender2 = networkSender;
				}
			}
			return networkSender2;
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x00073328 File Offset: 0x00071528
		private void ReleaseCachedConnection(NetworkSender sender)
		{
			Dictionary<string, NetworkSender> dictionary = this.currentSenderCache;
			lock (dictionary)
			{
				List<NetworkSender> list = this.openNetworkSenders;
				lock (list)
				{
					if (this.openNetworkSenders.Remove(sender))
					{
						sender.Close(delegate(Exception ex)
						{
						});
					}
				}
				NetworkSender networkSender;
				if (this.currentSenderCache.TryGetValue(sender.Address, out networkSender) && sender == networkSender)
				{
					this.currentSenderCache.Remove(sender.Address);
				}
			}
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x000733EC File Offset: 0x000715EC
		private void ChunkedSend(NetworkSender sender, byte[] buffer, AsyncContinuation continuation)
		{
			int tosend = buffer.Length;
			int pos = 0;
			AsyncContinuation sendNextChunk = null;
			sendNextChunk = delegate(Exception ex)
			{
				if (ex != null)
				{
					continuation(ex);
					return;
				}
				if (tosend <= 0)
				{
					continuation(null);
					return;
				}
				int num = tosend;
				if (num > this.MaxMessageSize)
				{
					if (this.OnOverflow == NetworkTargetOverflowAction.Discard)
					{
						continuation(null);
						return;
					}
					if (this.OnOverflow == NetworkTargetOverflowAction.Error)
					{
						continuation(new OverflowException(string.Concat(new string[]
						{
							"Attempted to send a message larger than MaxMessageSize (",
							this.MaxMessageSize.ToString(),
							"). Actual size was: ",
							buffer.Length.ToString(),
							". Adjust OnOverflow and MaxMessageSize parameters accordingly."
						})));
						return;
					}
					num = this.MaxMessageSize;
				}
				int pos2 = pos;
				tosend -= num;
				pos += num;
				sender.Send(buffer, pos2, num, sendNextChunk);
			};
			sendNextChunk(null);
		}

		// Token: 0x04000D1D RID: 3357
		private Dictionary<string, NetworkSender> currentSenderCache = new Dictionary<string, NetworkSender>();

		// Token: 0x04000D1E RID: 3358
		private List<NetworkSender> openNetworkSenders = new List<NetworkSender>();
	}
}
