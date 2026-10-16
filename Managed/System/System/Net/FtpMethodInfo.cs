using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x02000590 RID: 1424
	internal class FtpMethodInfo
	{
		// Token: 0x06002E0D RID: 11789 RVA: 0x0009EAAC File Offset: 0x0009CCAC
		internal FtpMethodInfo(string method, FtpOperation operation, FtpMethodFlags flags, string httpCommand)
		{
			this.Method = method;
			this.Operation = operation;
			this.Flags = flags;
			this.HttpCommand = httpCommand;
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x0009EAD1 File Offset: 0x0009CCD1
		internal bool HasFlag(FtpMethodFlags flags)
		{
			return (this.Flags & flags) > FtpMethodFlags.None;
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06002E0F RID: 11791 RVA: 0x0009EADE File Offset: 0x0009CCDE
		internal bool IsCommandOnly
		{
			get
			{
				return (this.Flags & (FtpMethodFlags.IsDownload | FtpMethodFlags.IsUpload)) == FtpMethodFlags.None;
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06002E10 RID: 11792 RVA: 0x0009EAEB File Offset: 0x0009CCEB
		internal bool IsUpload
		{
			get
			{
				return (this.Flags & FtpMethodFlags.IsUpload) > FtpMethodFlags.None;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06002E11 RID: 11793 RVA: 0x0009EAF8 File Offset: 0x0009CCF8
		internal bool IsDownload
		{
			get
			{
				return (this.Flags & FtpMethodFlags.IsDownload) > FtpMethodFlags.None;
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06002E12 RID: 11794 RVA: 0x0009EB05 File Offset: 0x0009CD05
		internal bool ShouldParseForResponseUri
		{
			get
			{
				return (this.Flags & FtpMethodFlags.ShouldParseForResponseUri) > FtpMethodFlags.None;
			}
		}

		// Token: 0x06002E13 RID: 11795 RVA: 0x0009EB14 File Offset: 0x0009CD14
		internal static FtpMethodInfo GetMethodInfo(string method)
		{
			method = method.ToUpper(CultureInfo.InvariantCulture);
			foreach (FtpMethodInfo ftpMethodInfo in FtpMethodInfo.s_knownMethodInfo)
			{
				if (method == ftpMethodInfo.Method)
				{
					return ftpMethodInfo;
				}
			}
			throw new ArgumentException("This method is not supported.", "method");
		}

		// Token: 0x04001985 RID: 6533
		internal string Method;

		// Token: 0x04001986 RID: 6534
		internal FtpOperation Operation;

		// Token: 0x04001987 RID: 6535
		internal FtpMethodFlags Flags;

		// Token: 0x04001988 RID: 6536
		internal string HttpCommand;

		// Token: 0x04001989 RID: 6537
		private static readonly FtpMethodInfo[] s_knownMethodInfo = new FtpMethodInfo[]
		{
			new FtpMethodInfo("RETR", FtpOperation.DownloadFile, FtpMethodFlags.IsDownload | FtpMethodFlags.TakesParameter | FtpMethodFlags.HasHttpCommand, "GET"),
			new FtpMethodInfo("NLST", FtpOperation.ListDirectory, FtpMethodFlags.IsDownload | FtpMethodFlags.MayTakeParameter | FtpMethodFlags.HasHttpCommand | FtpMethodFlags.MustChangeWorkingDirectoryToPath, "GET"),
			new FtpMethodInfo("LIST", FtpOperation.ListDirectoryDetails, FtpMethodFlags.IsDownload | FtpMethodFlags.MayTakeParameter | FtpMethodFlags.HasHttpCommand | FtpMethodFlags.MustChangeWorkingDirectoryToPath, "GET"),
			new FtpMethodInfo("STOR", FtpOperation.UploadFile, FtpMethodFlags.IsUpload | FtpMethodFlags.TakesParameter, null),
			new FtpMethodInfo("STOU", FtpOperation.UploadFileUnique, FtpMethodFlags.IsUpload | FtpMethodFlags.DoesNotTakeParameter | FtpMethodFlags.ShouldParseForResponseUri | FtpMethodFlags.MustChangeWorkingDirectoryToPath, null),
			new FtpMethodInfo("APPE", FtpOperation.AppendFile, FtpMethodFlags.IsUpload | FtpMethodFlags.TakesParameter, null),
			new FtpMethodInfo("DELE", FtpOperation.DeleteFile, FtpMethodFlags.TakesParameter, null),
			new FtpMethodInfo("MDTM", FtpOperation.GetDateTimestamp, FtpMethodFlags.TakesParameter, null),
			new FtpMethodInfo("SIZE", FtpOperation.GetFileSize, FtpMethodFlags.TakesParameter, null),
			new FtpMethodInfo("RENAME", FtpOperation.Rename, FtpMethodFlags.TakesParameter, null),
			new FtpMethodInfo("MKD", FtpOperation.MakeDirectory, FtpMethodFlags.TakesParameter | FtpMethodFlags.ParameterIsDirectory, null),
			new FtpMethodInfo("RMD", FtpOperation.RemoveDirectory, FtpMethodFlags.TakesParameter | FtpMethodFlags.ParameterIsDirectory, null),
			new FtpMethodInfo("PWD", FtpOperation.PrintWorkingDirectory, FtpMethodFlags.DoesNotTakeParameter, null)
		};
	}
}
