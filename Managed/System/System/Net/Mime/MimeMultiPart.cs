using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace System.Net.Mime
{
	// Token: 0x020007DE RID: 2014
	internal class MimeMultiPart : MimeBasePart
	{
		// Token: 0x0600406B RID: 16491 RVA: 0x000DC1FF File Offset: 0x000DA3FF
		internal MimeMultiPart(MimeMultiPartType type)
		{
			this.MimeMultiPartType = type;
		}

		// Token: 0x17000E8B RID: 3723
		// (set) Token: 0x0600406C RID: 16492 RVA: 0x000DC20E File Offset: 0x000DA40E
		internal MimeMultiPartType MimeMultiPartType
		{
			set
			{
				if (value > MimeMultiPartType.Related || value < MimeMultiPartType.Mixed)
				{
					throw new NotSupportedException(value.ToString());
				}
				this.SetType(value);
			}
		}

		// Token: 0x0600406D RID: 16493 RVA: 0x000DC232 File Offset: 0x000DA432
		private void SetType(MimeMultiPartType type)
		{
			base.ContentType.MediaType = "multipart/" + type.ToString().ToLower(CultureInfo.InvariantCulture);
			base.ContentType.Boundary = this.GetNextBoundary();
		}

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x0600406E RID: 16494 RVA: 0x000DC271 File Offset: 0x000DA471
		internal Collection<MimeBasePart> Parts
		{
			get
			{
				if (this._parts == null)
				{
					this._parts = new Collection<MimeBasePart>();
				}
				return this._parts;
			}
		}

		// Token: 0x0600406F RID: 16495 RVA: 0x000DC28C File Offset: 0x000DA48C
		internal void Complete(IAsyncResult result, Exception e)
		{
			MimeMultiPart.MimePartContext mimePartContext = (MimeMultiPart.MimePartContext)result.AsyncState;
			if (mimePartContext._completed)
			{
				ExceptionDispatchInfo.Throw(e);
			}
			try
			{
				mimePartContext._outputStream.Close();
			}
			catch (Exception ex)
			{
				if (e == null)
				{
					e = ex;
				}
			}
			mimePartContext._completed = true;
			mimePartContext._result.InvokeCallback(e);
		}

		// Token: 0x06004070 RID: 16496 RVA: 0x000DC2F0 File Offset: 0x000DA4F0
		internal void MimeWriterCloseCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimeMultiPart.MimePartContext)result.AsyncState)._completedSynchronously = false;
			try
			{
				this.MimeWriterCloseCallbackHandler(result);
			}
			catch (Exception ex)
			{
				this.Complete(result, ex);
			}
		}

		// Token: 0x06004071 RID: 16497 RVA: 0x000DC33C File Offset: 0x000DA53C
		private void MimeWriterCloseCallbackHandler(IAsyncResult result)
		{
			((MimeWriter)((MimeMultiPart.MimePartContext)result.AsyncState)._writer).EndClose(result);
			this.Complete(result, null);
		}

		// Token: 0x06004072 RID: 16498 RVA: 0x000DC364 File Offset: 0x000DA564
		internal void MimePartSentCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimeMultiPart.MimePartContext)result.AsyncState)._completedSynchronously = false;
			try
			{
				this.MimePartSentCallbackHandler(result);
			}
			catch (Exception ex)
			{
				this.Complete(result, ex);
			}
		}

		// Token: 0x06004073 RID: 16499 RVA: 0x000DC3B0 File Offset: 0x000DA5B0
		private void MimePartSentCallbackHandler(IAsyncResult result)
		{
			MimeMultiPart.MimePartContext mimePartContext = (MimeMultiPart.MimePartContext)result.AsyncState;
			mimePartContext._partsEnumerator.Current.EndSend(result);
			if (mimePartContext._partsEnumerator.MoveNext())
			{
				IAsyncResult asyncResult = mimePartContext._partsEnumerator.Current.BeginSend(mimePartContext._writer, this._mimePartSentCallback, this._allowUnicode, mimePartContext);
				if (asyncResult.CompletedSynchronously)
				{
					this.MimePartSentCallbackHandler(asyncResult);
				}
				return;
			}
			IAsyncResult asyncResult2 = ((MimeWriter)mimePartContext._writer).BeginClose(new AsyncCallback(this.MimeWriterCloseCallback), mimePartContext);
			if (asyncResult2.CompletedSynchronously)
			{
				this.MimeWriterCloseCallbackHandler(asyncResult2);
			}
		}

		// Token: 0x06004074 RID: 16500 RVA: 0x000DC448 File Offset: 0x000DA648
		internal void ContentStreamCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimeMultiPart.MimePartContext)result.AsyncState)._completedSynchronously = false;
			try
			{
				this.ContentStreamCallbackHandler(result);
			}
			catch (Exception ex)
			{
				this.Complete(result, ex);
			}
		}

		// Token: 0x06004075 RID: 16501 RVA: 0x000DC494 File Offset: 0x000DA694
		private void ContentStreamCallbackHandler(IAsyncResult result)
		{
			MimeMultiPart.MimePartContext mimePartContext = (MimeMultiPart.MimePartContext)result.AsyncState;
			mimePartContext._outputStream = mimePartContext._writer.EndGetContentStream(result);
			mimePartContext._writer = new MimeWriter(mimePartContext._outputStream, base.ContentType.Boundary);
			if (mimePartContext._partsEnumerator.MoveNext())
			{
				MimeBasePart mimeBasePart = mimePartContext._partsEnumerator.Current;
				this._mimePartSentCallback = new AsyncCallback(this.MimePartSentCallback);
				IAsyncResult asyncResult = mimeBasePart.BeginSend(mimePartContext._writer, this._mimePartSentCallback, this._allowUnicode, mimePartContext);
				if (asyncResult.CompletedSynchronously)
				{
					this.MimePartSentCallbackHandler(asyncResult);
				}
				return;
			}
			IAsyncResult asyncResult2 = ((MimeWriter)mimePartContext._writer).BeginClose(new AsyncCallback(this.MimeWriterCloseCallback), mimePartContext);
			if (asyncResult2.CompletedSynchronously)
			{
				this.MimeWriterCloseCallbackHandler(asyncResult2);
			}
		}

		// Token: 0x06004076 RID: 16502 RVA: 0x000DC55C File Offset: 0x000DA75C
		internal override IAsyncResult BeginSend(BaseWriter writer, AsyncCallback callback, bool allowUnicode, object state)
		{
			this._allowUnicode = allowUnicode;
			base.PrepareHeaders(allowUnicode);
			writer.WriteHeaders(base.Headers, allowUnicode);
			MimeBasePart.MimePartAsyncResult mimePartAsyncResult = new MimeBasePart.MimePartAsyncResult(this, state, callback);
			MimeMultiPart.MimePartContext mimePartContext = new MimeMultiPart.MimePartContext(writer, mimePartAsyncResult, this.Parts.GetEnumerator());
			IAsyncResult asyncResult = writer.BeginGetContentStream(new AsyncCallback(this.ContentStreamCallback), mimePartContext);
			if (asyncResult.CompletedSynchronously)
			{
				this.ContentStreamCallbackHandler(asyncResult);
			}
			return mimePartAsyncResult;
		}

		// Token: 0x06004077 RID: 16503 RVA: 0x000DC5C8 File Offset: 0x000DA7C8
		internal override void Send(BaseWriter writer, bool allowUnicode)
		{
			base.PrepareHeaders(allowUnicode);
			writer.WriteHeaders(base.Headers, allowUnicode);
			Stream contentStream = writer.GetContentStream();
			MimeWriter mimeWriter = new MimeWriter(contentStream, base.ContentType.Boundary);
			foreach (MimeBasePart mimeBasePart in this.Parts)
			{
				mimeBasePart.Send(mimeWriter, allowUnicode);
			}
			mimeWriter.Close();
			contentStream.Close();
		}

		// Token: 0x06004078 RID: 16504 RVA: 0x000DC650 File Offset: 0x000DA850
		internal string GetNextBoundary()
		{
			return "--boundary_" + (Interlocked.Increment(ref MimeMultiPart.s_boundary) - 1).ToString(CultureInfo.InvariantCulture) + "_" + Guid.NewGuid().ToString(null, CultureInfo.InvariantCulture);
		}

		// Token: 0x040026AE RID: 9902
		private Collection<MimeBasePart> _parts;

		// Token: 0x040026AF RID: 9903
		private static int s_boundary;

		// Token: 0x040026B0 RID: 9904
		private AsyncCallback _mimePartSentCallback;

		// Token: 0x040026B1 RID: 9905
		private bool _allowUnicode;

		// Token: 0x020007DF RID: 2015
		internal class MimePartContext
		{
			// Token: 0x06004079 RID: 16505 RVA: 0x000DC698 File Offset: 0x000DA898
			internal MimePartContext(BaseWriter writer, LazyAsyncResult result, IEnumerator<MimeBasePart> partsEnumerator)
			{
				this._writer = writer;
				this._result = result;
				this._partsEnumerator = partsEnumerator;
			}

			// Token: 0x040026B2 RID: 9906
			internal IEnumerator<MimeBasePart> _partsEnumerator;

			// Token: 0x040026B3 RID: 9907
			internal Stream _outputStream;

			// Token: 0x040026B4 RID: 9908
			internal LazyAsyncResult _result;

			// Token: 0x040026B5 RID: 9909
			internal BaseWriter _writer;

			// Token: 0x040026B6 RID: 9910
			internal bool _completed;

			// Token: 0x040026B7 RID: 9911
			internal bool _completedSynchronously = true;
		}
	}
}
