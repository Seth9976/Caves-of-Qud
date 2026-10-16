using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Net
{
	// Token: 0x02000583 RID: 1411
	internal class CommandStream : NetworkStreamWrapper
	{
		// Token: 0x06002DAB RID: 11691 RVA: 0x0009C324 File Offset: 0x0009A524
		internal CommandStream(TcpClient client)
			: base(client)
		{
			this._decoder = this._encoding.GetDecoder();
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x0009C354 File Offset: 0x0009A554
		internal virtual void Abort(Exception e)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, "closing control Stream", "Abort");
			}
			lock (this)
			{
				if (this._aborted)
				{
					return;
				}
				this._aborted = true;
			}
			try
			{
				base.Close(0);
			}
			finally
			{
				if (e != null)
				{
					this.InvokeRequestCallback(e);
				}
				else
				{
					this.InvokeRequestCallback(null);
				}
			}
		}

		// Token: 0x06002DAD RID: 11693 RVA: 0x0009C3DC File Offset: 0x0009A5DC
		protected override void Dispose(bool disposing)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, null, "Dispose");
			}
			this.InvokeRequestCallback(null);
		}

		// Token: 0x06002DAE RID: 11694 RVA: 0x0009C3F8 File Offset: 0x0009A5F8
		protected void InvokeRequestCallback(object obj)
		{
			WebRequest request = this._request;
			if (request != null)
			{
				((FtpWebRequest)request).RequestCallback(obj);
			}
		}

		// Token: 0x17000929 RID: 2345
		// (get) Token: 0x06002DAF RID: 11695 RVA: 0x0009C41B File Offset: 0x0009A61B
		internal bool RecoverableFailure
		{
			get
			{
				return this._recoverableFailure;
			}
		}

		// Token: 0x06002DB0 RID: 11696 RVA: 0x0009C423 File Offset: 0x0009A623
		protected void MarkAsRecoverableFailure()
		{
			if (this._index <= 1)
			{
				this._recoverableFailure = true;
			}
		}

		// Token: 0x06002DB1 RID: 11697 RVA: 0x0009C438 File Offset: 0x0009A638
		internal Stream SubmitRequest(WebRequest request, bool isAsync, bool readInitalResponseOnConnect)
		{
			this.ClearState();
			CommandStream.PipelineEntry[] array = this.BuildCommandsList(request);
			this.InitCommandPipeline(request, array, isAsync);
			if (readInitalResponseOnConnect)
			{
				this._doSend = false;
				this._index = -1;
			}
			return this.ContinueCommandPipeline();
		}

		// Token: 0x06002DB2 RID: 11698 RVA: 0x0009C473 File Offset: 0x0009A673
		protected virtual void ClearState()
		{
			this.InitCommandPipeline(null, null, false);
		}

		// Token: 0x06002DB3 RID: 11699 RVA: 0x00002F6A File Offset: 0x0000116A
		protected virtual CommandStream.PipelineEntry[] BuildCommandsList(WebRequest request)
		{
			return null;
		}

		// Token: 0x06002DB4 RID: 11700 RVA: 0x0009C47E File Offset: 0x0009A67E
		protected Exception GenerateException(string message, WebExceptionStatus status, Exception innerException)
		{
			return new WebException(message, innerException, status, null);
		}

		// Token: 0x06002DB5 RID: 11701 RVA: 0x0009C489 File Offset: 0x0009A689
		protected Exception GenerateException(FtpStatusCode code, string statusDescription, Exception innerException)
		{
			return new WebException(SR.Format("The remote server returned an error: {0}.", NetRes.GetWebStatusCodeString(code, statusDescription)), innerException, WebExceptionStatus.ProtocolError, null);
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x0009C4A4 File Offset: 0x0009A6A4
		protected void InitCommandPipeline(WebRequest request, CommandStream.PipelineEntry[] commands, bool isAsync)
		{
			this._commands = commands;
			this._index = 0;
			this._request = request;
			this._aborted = false;
			this._doRead = true;
			this._doSend = true;
			this._currentResponseDescription = null;
			this._isAsync = isAsync;
			this._recoverableFailure = false;
			this._abortReason = string.Empty;
		}

		// Token: 0x06002DB7 RID: 11703 RVA: 0x0009C4FC File Offset: 0x0009A6FC
		internal void CheckContinuePipeline()
		{
			if (this._isAsync)
			{
				return;
			}
			try
			{
				this.ContinueCommandPipeline();
			}
			catch (Exception ex)
			{
				this.Abort(ex);
			}
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x0009C538 File Offset: 0x0009A738
		protected Stream ContinueCommandPipeline()
		{
			bool isAsync = this._isAsync;
			while (this._index < this._commands.Length)
			{
				if (this._doSend)
				{
					if (this._index < 0)
					{
						throw new InternalException();
					}
					byte[] bytes = this.Encoding.GetBytes(this._commands[this._index].Command);
					if (NetEventSource.Log.IsEnabled())
					{
						string text = this._commands[this._index].Command.Substring(0, this._commands[this._index].Command.Length - 2);
						if (this._commands[this._index].HasFlag(CommandStream.PipelineEntryFlags.DontLogParameter))
						{
							int num = text.IndexOf(' ');
							if (num != -1)
							{
								text = text.Substring(0, num) + " ********";
							}
						}
						if (NetEventSource.IsEnabled)
						{
							NetEventSource.Info(this, FormattableStringFactory.Create("Sending command {0}", new object[] { text }), "ContinueCommandPipeline");
						}
					}
					try
					{
						if (isAsync)
						{
							this.BeginWrite(bytes, 0, bytes.Length, CommandStream.s_writeCallbackDelegate, this);
						}
						else
						{
							this.Write(bytes, 0, bytes.Length);
						}
					}
					catch (IOException)
					{
						this.MarkAsRecoverableFailure();
						throw;
					}
					catch
					{
						throw;
					}
					if (isAsync)
					{
						return null;
					}
				}
				Stream stream = null;
				if (this.PostSendCommandProcessing(ref stream))
				{
					return stream;
				}
			}
			lock (this)
			{
				this.Close();
			}
			return null;
		}

		// Token: 0x06002DB9 RID: 11705 RVA: 0x0009C6CC File Offset: 0x0009A8CC
		private bool PostSendCommandProcessing(ref Stream stream)
		{
			if (this._doRead)
			{
				bool isAsync = this._isAsync;
				int index = this._index;
				CommandStream.PipelineEntry[] commands = this._commands;
				try
				{
					ResponseDescription responseDescription = this.ReceiveCommandResponse();
					if (isAsync)
					{
						return true;
					}
					this._currentResponseDescription = responseDescription;
				}
				catch
				{
					if (index < 0 || index >= commands.Length || commands[index].Command != "QUIT\r\n")
					{
						throw;
					}
				}
			}
			return this.PostReadCommandProcessing(ref stream);
		}

		// Token: 0x06002DBA RID: 11706 RVA: 0x0009C74C File Offset: 0x0009A94C
		private bool PostReadCommandProcessing(ref Stream stream)
		{
			if (this._index >= this._commands.Length)
			{
				return false;
			}
			this._doSend = false;
			this._doRead = false;
			CommandStream.PipelineEntry pipelineEntry;
			if (this._index == -1)
			{
				pipelineEntry = null;
			}
			else
			{
				pipelineEntry = this._commands[this._index];
			}
			CommandStream.PipelineInstruction pipelineInstruction;
			if (this._currentResponseDescription == null && pipelineEntry.Command == "QUIT\r\n")
			{
				pipelineInstruction = CommandStream.PipelineInstruction.Advance;
			}
			else
			{
				pipelineInstruction = this.PipelineCallback(pipelineEntry, this._currentResponseDescription, false, ref stream);
			}
			if (pipelineInstruction == CommandStream.PipelineInstruction.Abort)
			{
				Exception ex;
				if (this._abortReason != string.Empty)
				{
					ex = new WebException(this._abortReason);
				}
				else
				{
					ex = this.GenerateException("The underlying connection was closed: The server committed a protocol violation", WebExceptionStatus.ServerProtocolViolation, null);
				}
				this.Abort(ex);
				throw ex;
			}
			if (pipelineInstruction == CommandStream.PipelineInstruction.Advance)
			{
				this._currentResponseDescription = null;
				this._doSend = true;
				this._doRead = true;
				this._index++;
			}
			else
			{
				if (pipelineInstruction == CommandStream.PipelineInstruction.Pause)
				{
					return true;
				}
				if (pipelineInstruction == CommandStream.PipelineInstruction.GiveStream)
				{
					this._currentResponseDescription = null;
					this._doRead = true;
					if (this._isAsync)
					{
						this.ContinueCommandPipeline();
						this.InvokeRequestCallback(stream);
					}
					return true;
				}
				if (pipelineInstruction == CommandStream.PipelineInstruction.Reread)
				{
					this._currentResponseDescription = null;
					this._doRead = true;
				}
			}
			return false;
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x00003062 File Offset: 0x00001262
		protected virtual CommandStream.PipelineInstruction PipelineCallback(CommandStream.PipelineEntry entry, ResponseDescription response, bool timeout, ref Stream stream)
		{
			return CommandStream.PipelineInstruction.Abort;
		}

		// Token: 0x06002DBC RID: 11708 RVA: 0x0009C86C File Offset: 0x0009AA6C
		private static void ReadCallback(IAsyncResult asyncResult)
		{
			ReceiveState receiveState = (ReceiveState)asyncResult.AsyncState;
			try
			{
				Stream connection = receiveState.Connection;
				int num = 0;
				try
				{
					num = connection.EndRead(asyncResult);
					if (num == 0)
					{
						receiveState.Connection.CloseSocket();
					}
				}
				catch (IOException)
				{
					receiveState.Connection.MarkAsRecoverableFailure();
					throw;
				}
				catch
				{
					throw;
				}
				receiveState.Connection.ReceiveCommandResponseCallback(receiveState, num);
			}
			catch (Exception ex)
			{
				receiveState.Connection.Abort(ex);
			}
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x0009C900 File Offset: 0x0009AB00
		private static void WriteCallback(IAsyncResult asyncResult)
		{
			CommandStream commandStream = (CommandStream)asyncResult.AsyncState;
			try
			{
				try
				{
					commandStream.EndWrite(asyncResult);
				}
				catch (IOException)
				{
					commandStream.MarkAsRecoverableFailure();
					throw;
				}
				catch
				{
					throw;
				}
				Stream stream = null;
				if (!commandStream.PostSendCommandProcessing(ref stream))
				{
					commandStream.ContinueCommandPipeline();
				}
			}
			catch (Exception ex)
			{
				commandStream.Abort(ex);
			}
		}

		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06002DBE RID: 11710 RVA: 0x0009C978 File Offset: 0x0009AB78
		// (set) Token: 0x06002DBF RID: 11711 RVA: 0x0009C980 File Offset: 0x0009AB80
		protected Encoding Encoding
		{
			get
			{
				return this._encoding;
			}
			set
			{
				this._encoding = value;
				this._decoder = this._encoding.GetDecoder();
			}
		}

		// Token: 0x06002DC0 RID: 11712 RVA: 0x00003062 File Offset: 0x00001262
		protected virtual bool CheckValid(ResponseDescription response, ref int validThrough, ref int completeLength)
		{
			return false;
		}

		// Token: 0x06002DC1 RID: 11713 RVA: 0x0009C99C File Offset: 0x0009AB9C
		private ResponseDescription ReceiveCommandResponse()
		{
			ReceiveState receiveState = new ReceiveState(this);
			try
			{
				if (this._buffer.Length > 0)
				{
					this.ReceiveCommandResponseCallback(receiveState, -1);
				}
				else
				{
					try
					{
						if (this._isAsync)
						{
							this.BeginRead(receiveState.Buffer, 0, receiveState.Buffer.Length, CommandStream.s_readCallbackDelegate, receiveState);
							return null;
						}
						int num = this.Read(receiveState.Buffer, 0, receiveState.Buffer.Length);
						if (num == 0)
						{
							base.CloseSocket();
						}
						this.ReceiveCommandResponseCallback(receiveState, num);
					}
					catch (IOException)
					{
						this.MarkAsRecoverableFailure();
						throw;
					}
					catch
					{
						throw;
					}
				}
			}
			catch (Exception ex)
			{
				if (ex is WebException)
				{
					throw;
				}
				throw this.GenerateException("The underlying connection was closed: An unexpected error occurred on a receive", WebExceptionStatus.ReceiveFailure, ex);
			}
			return receiveState.Resp;
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x0009CA74 File Offset: 0x0009AC74
		private void ReceiveCommandResponseCallback(ReceiveState state, int bytesRead)
		{
			int num = -1;
			for (;;)
			{
				int validThrough = state.ValidThrough;
				if (this._buffer.Length > 0)
				{
					state.Resp.StatusBuffer.Append(this._buffer);
					this._buffer = string.Empty;
					if (!this.CheckValid(state.Resp, ref validThrough, ref num))
					{
						break;
					}
				}
				else
				{
					if (bytesRead <= 0)
					{
						goto Block_3;
					}
					char[] array = new char[this._decoder.GetCharCount(state.Buffer, 0, bytesRead)];
					int chars = this._decoder.GetChars(state.Buffer, 0, bytesRead, array, 0, false);
					string text = new string(array, 0, chars);
					state.Resp.StatusBuffer.Append(text);
					if (!this.CheckValid(state.Resp, ref validThrough, ref num))
					{
						goto Block_4;
					}
					if (num >= 0)
					{
						int num2 = state.Resp.StatusBuffer.Length - num;
						if (num2 > 0)
						{
							this._buffer = text.Substring(text.Length - num2, num2);
						}
					}
				}
				if (num < 0)
				{
					state.ValidThrough = validThrough;
					try
					{
						if (this._isAsync)
						{
							this.BeginRead(state.Buffer, 0, state.Buffer.Length, CommandStream.s_readCallbackDelegate, state);
							return;
						}
						bytesRead = this.Read(state.Buffer, 0, state.Buffer.Length);
						if (bytesRead == 0)
						{
							base.CloseSocket();
						}
						continue;
					}
					catch (IOException)
					{
						this.MarkAsRecoverableFailure();
						throw;
					}
					catch
					{
						throw;
					}
					goto IL_017B;
				}
				goto IL_017B;
			}
			throw this.GenerateException("The underlying connection was closed: The server committed a protocol violation", WebExceptionStatus.ServerProtocolViolation, null);
			Block_3:
			throw this.GenerateException("The underlying connection was closed: The server committed a protocol violation", WebExceptionStatus.ServerProtocolViolation, null);
			Block_4:
			throw this.GenerateException("The underlying connection was closed: The server committed a protocol violation", WebExceptionStatus.ServerProtocolViolation, null);
			IL_017B:
			string text2 = state.Resp.StatusBuffer.ToString();
			state.Resp.StatusDescription = text2.Substring(0, num);
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, FormattableStringFactory.Create("Received response: {0}", new object[] { text2.Substring(0, num - 2) }), "ReceiveCommandResponseCallback");
			}
			if (this._isAsync)
			{
				if (state.Resp != null)
				{
					this._currentResponseDescription = state.Resp;
				}
				Stream stream = null;
				if (this.PostReadCommandProcessing(ref stream))
				{
					return;
				}
				this.ContinueCommandPipeline();
			}
		}

		// Token: 0x0400191B RID: 6427
		private static readonly AsyncCallback s_writeCallbackDelegate = new AsyncCallback(CommandStream.WriteCallback);

		// Token: 0x0400191C RID: 6428
		private static readonly AsyncCallback s_readCallbackDelegate = new AsyncCallback(CommandStream.ReadCallback);

		// Token: 0x0400191D RID: 6429
		private bool _recoverableFailure;

		// Token: 0x0400191E RID: 6430
		protected WebRequest _request;

		// Token: 0x0400191F RID: 6431
		protected bool _isAsync;

		// Token: 0x04001920 RID: 6432
		private bool _aborted;

		// Token: 0x04001921 RID: 6433
		protected CommandStream.PipelineEntry[] _commands;

		// Token: 0x04001922 RID: 6434
		protected int _index;

		// Token: 0x04001923 RID: 6435
		private bool _doRead;

		// Token: 0x04001924 RID: 6436
		private bool _doSend;

		// Token: 0x04001925 RID: 6437
		private ResponseDescription _currentResponseDescription;

		// Token: 0x04001926 RID: 6438
		protected string _abortReason;

		// Token: 0x04001927 RID: 6439
		private const int WaitingForPipeline = 1;

		// Token: 0x04001928 RID: 6440
		private const int CompletedPipeline = 2;

		// Token: 0x04001929 RID: 6441
		private string _buffer = string.Empty;

		// Token: 0x0400192A RID: 6442
		private Encoding _encoding = Encoding.UTF8;

		// Token: 0x0400192B RID: 6443
		private Decoder _decoder;

		// Token: 0x02000584 RID: 1412
		internal enum PipelineInstruction
		{
			// Token: 0x0400192D RID: 6445
			Abort,
			// Token: 0x0400192E RID: 6446
			Advance,
			// Token: 0x0400192F RID: 6447
			Pause,
			// Token: 0x04001930 RID: 6448
			Reread,
			// Token: 0x04001931 RID: 6449
			GiveStream
		}

		// Token: 0x02000585 RID: 1413
		[Flags]
		internal enum PipelineEntryFlags
		{
			// Token: 0x04001933 RID: 6451
			UserCommand = 1,
			// Token: 0x04001934 RID: 6452
			GiveDataStream = 2,
			// Token: 0x04001935 RID: 6453
			CreateDataConnection = 4,
			// Token: 0x04001936 RID: 6454
			DontLogParameter = 8
		}

		// Token: 0x02000586 RID: 1414
		internal class PipelineEntry
		{
			// Token: 0x06002DC4 RID: 11716 RVA: 0x0009CCC0 File Offset: 0x0009AEC0
			internal PipelineEntry(string command)
			{
				this.Command = command;
			}

			// Token: 0x06002DC5 RID: 11717 RVA: 0x0009CCCF File Offset: 0x0009AECF
			internal PipelineEntry(string command, CommandStream.PipelineEntryFlags flags)
			{
				this.Command = command;
				this.Flags = flags;
			}

			// Token: 0x06002DC6 RID: 11718 RVA: 0x0009CCE5 File Offset: 0x0009AEE5
			internal bool HasFlag(CommandStream.PipelineEntryFlags flags)
			{
				return (this.Flags & flags) > (CommandStream.PipelineEntryFlags)0;
			}

			// Token: 0x04001937 RID: 6455
			internal string Command;

			// Token: 0x04001938 RID: 6456
			internal CommandStream.PipelineEntryFlags Flags;
		}
	}
}
