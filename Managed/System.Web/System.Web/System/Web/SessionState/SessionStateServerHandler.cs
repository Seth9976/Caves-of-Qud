using System;
using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
using System.Runtime.Remoting;
using System.Web.Configuration;

namespace System.Web.SessionState
{
	// Token: 0x020004A0 RID: 1184
	internal class SessionStateServerHandler : SessionStateStoreProviderBase
	{
		// Token: 0x060035AD RID: 13741 RVA: 0x0008B614 File Offset: 0x00089814
		public override SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout)
		{
			return new SessionStateStoreData(new SessionStateItemCollection(), HttpApplicationFactory.ApplicationState.SessionObjects, timeout);
		}

		// Token: 0x060035AE RID: 13742 RVA: 0x0008B62B File Offset: 0x0008982B
		public override void CreateUninitializedItem(HttpContext context, string id, int timeout)
		{
			this.EnsureGoodId(id, true);
			this.stateServer.CreateUninitializedItem(id, timeout);
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x00003946 File Offset: 0x00001B46
		public override void Dispose()
		{
		}

		// Token: 0x060035B0 RID: 13744 RVA: 0x00003946 File Offset: 0x00001B46
		public override void EndRequest(HttpContext context)
		{
		}

		// Token: 0x060035B1 RID: 13745 RVA: 0x0008B644 File Offset: 0x00089844
		private SessionStateStoreData GetItemInternal(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions, bool exclusive)
		{
			locked = false;
			lockAge = TimeSpan.MinValue;
			lockId = int.MinValue;
			actions = SessionStateActions.None;
			if (id == null)
			{
				return null;
			}
			StateServerItem item = this.stateServer.GetItem(id, out locked, out lockAge, out lockId, out actions, exclusive);
			if (item == null)
			{
				return null;
			}
			if (actions == SessionStateActions.InitializeItem)
			{
				return this.CreateNewStoreData(context, item.Timeout);
			}
			SessionStateItemCollection sessionStateItemCollection = null;
			HttpStaticObjectsCollection httpStaticObjectsCollection = null;
			MemoryStream memoryStream = null;
			BinaryReader binaryReader = null;
			GZipStream gzipStream = null;
			try
			{
				if (item.CollectionData != null && item.CollectionData.Length != 0)
				{
					memoryStream = new MemoryStream(item.CollectionData);
					Stream stream;
					if (this.config.CompressionEnabled)
					{
						gzipStream = (stream = new GZipStream(memoryStream, CompressionMode.Decompress, true));
					}
					else
					{
						stream = memoryStream;
					}
					binaryReader = new BinaryReader(stream);
					sessionStateItemCollection = SessionStateItemCollection.Deserialize(binaryReader);
					if (gzipStream != null)
					{
						gzipStream.Close();
					}
					binaryReader.Close();
				}
				else
				{
					sessionStateItemCollection = new SessionStateItemCollection();
				}
				if (item.StaticObjectsData != null && item.StaticObjectsData.Length != 0)
				{
					httpStaticObjectsCollection = HttpStaticObjectsCollection.FromByteArray(item.StaticObjectsData);
				}
				else
				{
					httpStaticObjectsCollection = new HttpStaticObjectsCollection();
				}
			}
			catch (Exception ex)
			{
				throw new HttpException("Failed to retrieve session state.", ex);
			}
			finally
			{
				if (memoryStream != null)
				{
					memoryStream.Dispose();
				}
				if (binaryReader != null)
				{
					binaryReader.Dispose();
				}
				if (gzipStream != null)
				{
					gzipStream.Dispose();
				}
			}
			return new SessionStateStoreData(sessionStateItemCollection, httpStaticObjectsCollection, item.Timeout);
		}

		// Token: 0x060035B2 RID: 13746 RVA: 0x0008B79C File Offset: 0x0008999C
		public override SessionStateStoreData GetItem(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
		{
			this.EnsureGoodId(id, false);
			return this.GetItemInternal(context, id, out locked, out lockAge, out lockId, out actions, false);
		}

		// Token: 0x060035B3 RID: 13747 RVA: 0x0008B7B6 File Offset: 0x000899B6
		public override SessionStateStoreData GetItemExclusive(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions)
		{
			this.EnsureGoodId(id, false);
			return this.GetItemInternal(context, id, out locked, out lockAge, out lockId, out actions, true);
		}

		// Token: 0x060035B4 RID: 13748 RVA: 0x0008B7D0 File Offset: 0x000899D0
		public override void Initialize(string name, NameValueCollection config)
		{
			this.config = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
			if (string.IsNullOrEmpty(name))
			{
				name = "Session Server handler";
			}
			RemotingConfiguration.Configure(null);
			string text = null;
			string text2 = null;
			string text3 = null;
			this.GetConData(out text, out text2, out text3);
			string text4 = string.Format("{0}://{1}:{2}/StateServer", text, text2, text3);
			this.stateServer = Activator.GetObject(typeof(RemoteStateServer), text4) as RemoteStateServer;
			base.Initialize(name, config);
		}

		// Token: 0x060035B5 RID: 13749 RVA: 0x00003946 File Offset: 0x00001B46
		public override void InitializeRequest(HttpContext context)
		{
		}

		// Token: 0x060035B6 RID: 13750 RVA: 0x0008B84C File Offset: 0x00089A4C
		public override void ReleaseItemExclusive(HttpContext context, string id, object lockId)
		{
			this.EnsureGoodId(id, true);
			this.stateServer.ReleaseItemExclusive(id, lockId);
		}

		// Token: 0x060035B7 RID: 13751 RVA: 0x0008B863 File Offset: 0x00089A63
		public override void RemoveItem(HttpContext context, string id, object lockId, SessionStateStoreData item)
		{
			this.EnsureGoodId(id, true);
			this.stateServer.Remove(id, lockId);
		}

		// Token: 0x060035B8 RID: 13752 RVA: 0x0008B87A File Offset: 0x00089A7A
		public override void ResetItemTimeout(HttpContext context, string id)
		{
			this.EnsureGoodId(id, true);
			this.stateServer.ResetItemTimeout(id);
		}

		// Token: 0x060035B9 RID: 13753 RVA: 0x0008B890 File Offset: 0x00089A90
		public override void SetAndReleaseItemExclusive(HttpContext context, string id, SessionStateStoreData item, object lockId, bool newItem)
		{
			if (item == null)
			{
				return;
			}
			this.EnsureGoodId(id, true);
			byte[] array = null;
			byte[] array2 = null;
			MemoryStream memoryStream = null;
			BinaryWriter binaryWriter = null;
			GZipStream gzipStream = null;
			try
			{
				SessionStateItemCollection sessionStateItemCollection = item.Items as SessionStateItemCollection;
				if (sessionStateItemCollection != null && sessionStateItemCollection.Count > 0)
				{
					memoryStream = new MemoryStream();
					Stream stream;
					if (this.config.CompressionEnabled)
					{
						gzipStream = (stream = new GZipStream(memoryStream, CompressionMode.Compress, true));
					}
					else
					{
						stream = memoryStream;
					}
					binaryWriter = new BinaryWriter(stream);
					sessionStateItemCollection.Serialize(binaryWriter);
					if (gzipStream != null)
					{
						gzipStream.Close();
					}
					binaryWriter.Close();
					array = memoryStream.ToArray();
				}
				HttpStaticObjectsCollection staticObjects = item.StaticObjects;
				if (staticObjects != null && staticObjects.Count > 0)
				{
					array2 = staticObjects.ToByteArray();
				}
			}
			catch (Exception ex)
			{
				throw new HttpException("Failed to store session data.", ex);
			}
			finally
			{
				if (binaryWriter != null)
				{
					binaryWriter.Dispose();
				}
				if (gzipStream != null)
				{
					gzipStream.Dispose();
				}
				if (memoryStream != null)
				{
					memoryStream.Dispose();
				}
			}
			this.stateServer.SetAndReleaseItemExclusive(id, array, array2, lockId, item.Timeout, newItem);
		}

		// Token: 0x060035BA RID: 13754 RVA: 0x00008A4D File Offset: 0x00006C4D
		public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
		{
			return false;
		}

		// Token: 0x060035BB RID: 13755 RVA: 0x00089C36 File Offset: 0x00087E36
		private void EnsureGoodId(string id, bool throwOnNull)
		{
			if (id == null)
			{
				if (throwOnNull)
				{
					throw new HttpException("Session ID is invalid");
				}
				return;
			}
			else
			{
				if (id.Length > SessionIDManager.SessionIDMaxLength)
				{
					throw new HttpException("Session ID too long");
				}
				return;
			}
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x0008B9A0 File Offset: 0x00089BA0
		private void GetConData(out string proto, out string server, out string port)
		{
			string stateConnectionString = this.config.StateConnectionString;
			int num = stateConnectionString.IndexOf('=');
			int num2 = stateConnectionString.IndexOf(':');
			if (num < 0 || num2 < 0)
			{
				throw new HttpException("Invalid StateConnectionString");
			}
			proto = stateConnectionString.Substring(0, num);
			server = stateConnectionString.Substring(num + 1, num2 - num - 1);
			port = stateConnectionString.Substring(num2 + 1, stateConnectionString.Length - num2 - 1);
			if (proto == "tcpip")
			{
				proto = "tcp";
			}
		}

		// Token: 0x04001D88 RID: 7560
		private const int lockAcquireTimeout = 30000;

		// Token: 0x04001D89 RID: 7561
		private SessionStateSection config;

		// Token: 0x04001D8A RID: 7562
		private RemoteStateServer stateServer;
	}
}
