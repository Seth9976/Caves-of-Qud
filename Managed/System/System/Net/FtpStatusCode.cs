using System;

namespace System.Net
{
	/// <summary>Specifies the status codes returned for a File Transfer Protocol (FTP) operation.</summary>
	// Token: 0x020005CF RID: 1487
	public enum FtpStatusCode
	{
		/// <summary>Included for completeness, this value is never returned by servers.</summary>
		// Token: 0x04001A9A RID: 6810
		Undefined,
		/// <summary>Specifies that the response contains a restart marker reply. The text of the description that accompanies this status contains the user data stream marker and the server marker.</summary>
		// Token: 0x04001A9B RID: 6811
		RestartMarker = 110,
		/// <summary>Specifies that the service is not available now; try your request later.</summary>
		// Token: 0x04001A9C RID: 6812
		ServiceTemporarilyNotAvailable = 120,
		/// <summary>Specifies that the data connection is already open and the requested transfer is starting.</summary>
		// Token: 0x04001A9D RID: 6813
		DataAlreadyOpen = 125,
		/// <summary>Specifies that the server is opening the data connection.</summary>
		// Token: 0x04001A9E RID: 6814
		OpeningData = 150,
		/// <summary>Specifies that the command completed successfully.</summary>
		// Token: 0x04001A9F RID: 6815
		CommandOK = 200,
		/// <summary>Specifies that the command is not implemented by the server because it is not needed.</summary>
		// Token: 0x04001AA0 RID: 6816
		CommandExtraneous = 202,
		/// <summary>Specifies the status of a directory.</summary>
		// Token: 0x04001AA1 RID: 6817
		DirectoryStatus = 212,
		/// <summary>Specifies the status of a file.</summary>
		// Token: 0x04001AA2 RID: 6818
		FileStatus,
		/// <summary>Specifies the system type name using the system names published in the Assigned Numbers document published by the Internet Assigned Numbers Authority.</summary>
		// Token: 0x04001AA3 RID: 6819
		SystemType = 215,
		/// <summary>Specifies that the server is ready for a user login operation.</summary>
		// Token: 0x04001AA4 RID: 6820
		SendUserCommand = 220,
		/// <summary>Specifies that the server is closing the control connection.</summary>
		// Token: 0x04001AA5 RID: 6821
		ClosingControl,
		/// <summary>Specifies that the server is closing the data connection and that the requested file action was successful.</summary>
		// Token: 0x04001AA6 RID: 6822
		ClosingData = 226,
		/// <summary>Specifies that the server is entering passive mode.</summary>
		// Token: 0x04001AA7 RID: 6823
		EnteringPassive,
		/// <summary>Specifies that the user is logged in and can send commands.</summary>
		// Token: 0x04001AA8 RID: 6824
		LoggedInProceed = 230,
		/// <summary>Specifies that the server accepts the authentication mechanism specified by the client, and the exchange of security data is complete.</summary>
		// Token: 0x04001AA9 RID: 6825
		ServerWantsSecureSession = 234,
		/// <summary>Specifies that the requested file action completed successfully.</summary>
		// Token: 0x04001AAA RID: 6826
		FileActionOK = 250,
		/// <summary>Specifies that the requested path name was created.</summary>
		// Token: 0x04001AAB RID: 6827
		PathnameCreated = 257,
		/// <summary>Specifies that the server expects a password to be supplied.</summary>
		// Token: 0x04001AAC RID: 6828
		SendPasswordCommand = 331,
		/// <summary>Specifies that the server requires a login account to be supplied.</summary>
		// Token: 0x04001AAD RID: 6829
		NeedLoginAccount,
		/// <summary>Specifies that the requested file action requires additional information.</summary>
		// Token: 0x04001AAE RID: 6830
		FileCommandPending = 350,
		/// <summary>Specifies that the service is not available.</summary>
		// Token: 0x04001AAF RID: 6831
		ServiceNotAvailable = 421,
		/// <summary>Specifies that the data connection cannot be opened.</summary>
		// Token: 0x04001AB0 RID: 6832
		CantOpenData = 425,
		/// <summary>Specifies that the connection has been closed.</summary>
		// Token: 0x04001AB1 RID: 6833
		ConnectionClosed,
		/// <summary>Specifies that the requested action cannot be performed on the specified file because the file is not available or is being used.</summary>
		// Token: 0x04001AB2 RID: 6834
		ActionNotTakenFileUnavailableOrBusy = 450,
		/// <summary>Specifies that an error occurred that prevented the request action from completing.</summary>
		// Token: 0x04001AB3 RID: 6835
		ActionAbortedLocalProcessingError,
		/// <summary>Specifies that the requested action cannot be performed because there is not enough space on the server.</summary>
		// Token: 0x04001AB4 RID: 6836
		ActionNotTakenInsufficientSpace,
		/// <summary>Specifies that the command has a syntax error or is not a command recognized by the server.</summary>
		// Token: 0x04001AB5 RID: 6837
		CommandSyntaxError = 500,
		/// <summary>Specifies that one or more command arguments has a syntax error.</summary>
		// Token: 0x04001AB6 RID: 6838
		ArgumentSyntaxError,
		/// <summary>Specifies that the command is not implemented by the FTP server.</summary>
		// Token: 0x04001AB7 RID: 6839
		CommandNotImplemented,
		/// <summary>Specifies that the sequence of commands is not in the correct order.</summary>
		// Token: 0x04001AB8 RID: 6840
		BadCommandSequence,
		/// <summary>Specifies that login information must be sent to the server.</summary>
		// Token: 0x04001AB9 RID: 6841
		NotLoggedIn = 530,
		/// <summary>Specifies that a user account on the server is required.</summary>
		// Token: 0x04001ABA RID: 6842
		AccountNeeded = 532,
		/// <summary>Specifies that the requested action cannot be performed on the specified file because the file is not available.</summary>
		// Token: 0x04001ABB RID: 6843
		ActionNotTakenFileUnavailable = 550,
		/// <summary>Specifies that the requested action cannot be taken because the specified page type is unknown. Page types are described in RFC 959 Section 3.1.2.3</summary>
		// Token: 0x04001ABC RID: 6844
		ActionAbortedUnknownPageType,
		/// <summary>Specifies that the requested action cannot be performed.</summary>
		// Token: 0x04001ABD RID: 6845
		FileActionAborted,
		/// <summary>Specifies that the requested action cannot be performed on the specified file.</summary>
		// Token: 0x04001ABE RID: 6846
		ActionNotTakenFilenameNotAllowed
	}
}
