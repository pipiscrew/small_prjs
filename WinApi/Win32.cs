using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Automation;
using System.Windows.Automation.Provider;
using System.Windows.Forms;

namespace WinApi
{

    
    public static class Win32
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool AllowSetForegroundWindow(int dwProcessId);

        [DllImport("shlwapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern uint AssocQueryString(AssocF flags, AssocStr str, string pszAssoc, string pszExtra, [Out] StringBuilder pszOut, ref uint pcchOut);

        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hObjSource, int nXSrc, int nYSrc, TernaryRasterOperations dwRop);

        [DllImport("comdlg32.dll", CharSet = CharSet.Auto)]
        public static extern bool ChooseFont(IntPtr lpcf);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool CloseClipboard();

        [DllImport("uxtheme.dll", ExactSpelling = true)]
        public static extern int CloseThemeData(IntPtr hTheme);

        [DllImport("advapi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ConvertStringSecurityDescriptorToSecurityDescriptorW([MarshalAs(UnmanagedType.LPWStr)] string strSecurityDescriptor, uint sDRevision, ref IntPtr securityDescriptor, ref uint securityDescriptorSize);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateBitmap(int nWidth, int nHeight, int nPlanes, int nBitCount, short[] lpBits);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateBrushIndirect(LOGBRUSH lpLogBrush);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr CreatePopupMenu();

        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport("user32.dll")]
        public static extern int DestroyIcon(IntPtr hIcon);

        [DllImport("user32.dll")]
        public static extern bool DestroyMenu(IntPtr hMenu);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
        public static extern int DrawTextW(IntPtr hDC, string lpszString, int nCount, [In] [Out] ref RECT lpRect, int nFormat);

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(out bool enabled);

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        [DllImport("user32.dll")]
        public static extern int EnableMenuItem(IntPtr hMenu, SC uIDEnableItem, MF uEnable);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnableWindow(IntPtr hWnd, bool bEnable);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr window, Win32.EnumWindowProc callback, IntPtr i);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern bool ExtTextOut(IntPtr hdc, int X, int Y, uint fuOptions, [In] ref RECT lprc, string lpString, int cbCount, IntPtr lpDx);

        [DllImport("user32.dll")]
        public static extern bool FlashWindow(IntPtr hwnd, bool bInvert);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern uint FormatMessage([MarshalAs(UnmanagedType.U4)] FormatMessageFlags dwFlags, IntPtr lpSource, uint dwMessageId, uint dwLanguageId, ref IntPtr lpBuffer, uint nSize, string[] Arguments);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FreeLibrary(IntPtr hModule);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hwnd, char[] className, int maxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetClipboardData(uint format);

        [DllImport("gdi32.dll")]
        public static extern IntPtr GetCurrentObject(IntPtr hdc, uint uObjectType);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int GetCurrentThreadId();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool GetCursorInfo(ref CURSORINFO pci);

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point p);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDCEx(IntPtr hWnd, IntPtr hrgnClip, DeviceContextValues flags);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetDesktopWindow();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetDiskFreeSpaceEx(string lpDirectoryName, out ulong lpFreeBytesAvailable, out ulong lpTotalNumberOfBytes, out ulong lpTotalNumberOfFreeBytes);

        [DllImport("shcore.dll")]
        public static extern int GetDpiForMonitor(IntPtr hmonitor, Win32.Monitor_DPI_Type dpiType, out uint dpiX, out uint dpiY);

        [DllImport("user32.dll")]
        public static extern IntPtr GetFocus();

        [DllImport("gdi32.dll")]
        public static extern uint GetFontUnicodeRanges(IntPtr hdc, IntPtr lpgs);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetGestureInfo(IntPtr hGestureInfo, ref GESTUREINFO pGestureInfo);

        [DllImport("user32.dll")]
        public static extern uint GetGuiResources(IntPtr hProcess, uint uiFlags);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetKeyboardLayout(int dwLayout);

        [DllImport("user32.dll")]
        public static extern bool GetKeyboardState(byte[] lpKeyState);

        [DllImport("user32.dll")]
        public static extern short GetKeyState(int vKey);

        [DllImport("kernel32.dll")]
        public static extern int GetLongPathName(string path, StringBuilder pszPath, int cchPath);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetMenuDefaultItem(IntPtr hMenu, uint fByPos, uint gmdiFlags);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetPhysicallyInstalledSystemMemory(out ulong memoryInKilobytes);

        [DllImport("advapi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetSecurityDescriptorSacl(IntPtr pSecurityDescriptor, out IntPtr lpbSaclPresent, out IntPtr pSacl, out IntPtr lpbSaclDefaulted);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr GetShellWindow();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern void GetStartupInfo([In] [Out] STARTUPINFO lpStartupInfo);

        [DllImport("gdi32.dll")]
        public static extern IntPtr GetStockObject(StockObjects fnObject);

        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, [MarshalAs(UnmanagedType.Bool)] bool bRevert);

        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(SystemMetric smIndex);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetTextExtentExPoint(IntPtr hDC, string lpszStr, int cchString, int nMaxExtent, out int lpnFit, int[] alpDx, out Size lpSize);

        [DllImport("uxtheme.dll", ExactSpelling = true)]
        public static extern int GetThemeColor(IntPtr hTheme, int iPartId, int iStateId, int iPropId, out COLORREF pColor);

        [DllImport("secur32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetUserNameEx([In] ExtendedNameFormat nameFormat, [Out] StringBuilder userName, [In] [Out] ref uint userNameSize);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindow(IntPtr hwnd, int uCmd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowDC(IntPtr handle);

        public static IntPtr GetWindowLong(IntPtr hWnd, GWL nIndex)
        {
            if (IntPtr.Size != 4)
            {
                return Win32.GetWindowLongPtr64(hWnd, nIndex);
            }
            return Win32.GetWindowLong32(hWnd, nIndex);
        }

        [DllImport("user32.dll", EntryPoint = "GetWindowLong", SetLastError = true)]
        public static extern IntPtr GetWindowLong32(IntPtr hWnd, GWL nIndex);

        [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr", SetLastError = true)]
        public static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, GWL nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowPlacement(IntPtr hWnd, out WINDOWPLACEMENT lpwndpl);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowRect(IntPtr hwnd, out RECT rc);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GlobalAlloc(uint uFlags, UIntPtr dwBytes);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GlobalFree(IntPtr hMem);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GlobalLock(IntPtr hMem);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GlobalReAlloc(IntPtr hMem, IntPtr bytes, int flags);

        [DllImport("kernel32.dll")]
        public static extern UIntPtr GlobalSize(IntPtr handle);

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GlobalUnlock(IntPtr hMem);

        [DllImport("gdi32.dll", EntryPoint = "GdiGradientFill", ExactSpelling = true)]
        public static extern bool GradientFill(IntPtr hdc, TRIVERTEX[] pVertex, uint dwNumVertex, GRADIENT_RECT[] pMesh, uint dwNumMesh, uint dwMode);

        [DllImport("imm32.dll")]
        public static extern IntPtr ImmAssociateContext(IntPtr hWnd, IntPtr hIMC);

        [DllImport("imm32.dll")]
        public static extern IntPtr ImmAssociateContextEx(IntPtr hWnd, IntPtr hIMC, int dwFlags);

        
        [DllImport("imm32.dll", CharSet = CharSet.Unicode)]
        public static extern int ImmGetCompositionStringW(IntPtr hIMC, int dwIndex, byte[] lpBuf, int dwBufLen);

        [DllImport("imm32.dll", CharSet = CharSet.Unicode)]
        public static extern int ImmGetCompositionStringW(IntPtr hIMC, int dwIndex, uint[] lpBuf, int dwBufLen);

        [DllImport("imm32.dll")]
        public static extern IntPtr ImmGetContext(IntPtr hWnd);

        [DllImport("imm32.dll")]
        public static extern bool ImmGetOpenStatus(IntPtr hWnd);

        [DllImport("imm32.dll")]
        public static extern IntPtr ImmReleaseContext(IntPtr hWnd, IntPtr hIMC);

        [DllImport("imm32.dll")]
        public static extern int ImmSetCandidateWindow(IntPtr himc, ref CANDIDATEFORM lpCandidateForm);

        [DllImport("imm32.dll")]
        public static extern bool ImmSetOpenStatus(IntPtr hWnd, bool b);

        [DllImport("gdi32.dll")]
        public static extern int IntersectClipRect(IntPtr hdc, int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsHungAppWindow(IntPtr hwnd);

        [DllImport("uxtheme.dll", ExactSpelling = true)]
        public static extern bool IsThemeActive();

        [DllImport("user32.dll")]
        public static extern IntPtr LoadCursor(IntPtr hInstcance, SystemCursor hcur);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern Win32.SafeModuleHandle LoadLibraryEx(string lpFileName, IntPtr hFile, LoadLibraryExFlags dwFlags);

        [DllImport("user32.dll", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true, ThrowOnUnmappableChar = true)]
        public static extern int LoadString(Win32.SafeModuleHandle hInstance, uint uID, StringBuilder lpBuffer, int nBufferMax);

        [DllImport("kernel32.dll")]
        public static extern uint LocalFree(IntPtr hMem);

        [DllImport("user32.dll")]
        public static extern uint MapVirtualKey(uint uCode, uint uMapType);

        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromPoint(POINT pt, int dwFlags);

        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromRect([In] ref RECT lprc, int dwFlags);

        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(IntPtr hwnd, int dwFlags);

        [DllImport("user32.dll")]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

        [DllImport("Netapi32.dll")]
        public static extern int NetApiBufferFree(IntPtr Buffer);

        
        [DllImport("Netapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int NetGetJoinInformation(string server, out IntPtr domain, out Win32.NetJoinStatus status);

        [DllImport("gdi32.dll")]
        public static extern bool OffsetViewportOrgEx(IntPtr hdc, int nXOffset, int nYOffset, out POINT lpPoint);

        
        [DllImport("ole32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int OleGetClipboard(ref IDataObject data);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll")]
        public static extern IntPtr OpenInputDesktop(uint dwFlags, bool fInherit, uint dwDesiredAccess);

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern IntPtr OpenThemeData(IntPtr hWnd, string classList);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern bool PatBlt(IntPtr hdc, int left, int top, int width, int height, int rop);

        [DllImport("shlwapi.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PathIsNetworkPath(string pszPath);

        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr ReleaseDC(IntPtr handle, IntPtr hDC);

        [DllImport("ole32.dll")]
        public static extern int RevokeDragDrop(IntPtr hwnd);

        [DllImport("gdi32.dll")]
        public static extern int SelectClipRgn(IntPtr hdc, IntPtr hrgn);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("user32.dll")]
        public static extern uint SendInput(uint nInputs, [MarshalAs(UnmanagedType.LPArray)] [In] INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr SetActiveWindow(IntPtr handle);

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        public static extern int SetBkColor(IntPtr hdc, int clr);

        [DllImport("gdi32.dll")]
        public static extern int SetBkMode(IntPtr hdc, int iBkMode);

        [DllImport("user32.dll")]
        public static extern IntPtr SetCursor(IntPtr hcur);

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern int SetForegroundWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetGestureConfig(IntPtr hWnd, int dwReserved, int cIDs, GESTURECONFIG[] pGestureConfig, int cbSize);

        [DllImport("advapi32.dll")]
        public static extern uint SetNamedSecurityInfoW([MarshalAs(UnmanagedType.LPWStr)] string pObjectName, SE_OBJECT_TYPE objectType, int securityInfo, IntPtr psidOwner, IntPtr psidGroup, IntPtr pDacl, IntPtr pSacl);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto)]
        public static extern uint SetSecurityInfo(IntPtr handle, SE_OBJECT_TYPE ObjectType, int SecurityInfo, IntPtr psidOwner, IntPtr psidGroup, IntPtr pDacl, IntPtr pSacl);

        [DllImport("gdi32.dll")]
        public static extern uint SetTextAlign(IntPtr hdc, uint fmode);

        [DllImport("gdi32.dll")]
        public static extern uint SetTextColor(IntPtr hdc, int crColor);

        public static IntPtr SetWindowLong(IntPtr hWnd, GWL nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size != 4)
            {
                return Win32.SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
            }
            return Win32.SetWindowLongPtr32(hWnd, nIndex, dwNewLong);
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong", SetLastError = true)]
        public static extern IntPtr SetWindowLongPtr32(IntPtr hWnd, GWL nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr", SetLastError = true)]
        public static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, GWL nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPlacement(IntPtr hWnd, [In] ref WINDOWPLACEMENT lpwndpl);

        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern void SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);

        [DllImport("shell32.dll", SetLastError = true)]
        public static extern IntPtr SHAppBarMessage(uint dwMessage, [In] ref APPBARDATA pData);

        [DllImport("shell32.dll", SetLastError = true)]
        public static extern void SHBindToParent([In] IntPtr pidl, [In] ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppv, out IntPtr ppidlLast);

        [DllImport("shell32.dll", CharSet = CharSet.Unicode)]
        public static extern int SHCreateItemFromParsingName([MarshalAs(UnmanagedType.LPWStr)] string pszPath, IntPtr pbc, ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppv);

        //[DllImport("shell32.dll")]
        //public static extern int SHCreateStdEnumFmtEtc(uint cfmt, FORMATETC[] afmt, out IEnumFORMATETC ppenumFormatEtc);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbFileInfo, uint uFlags);

        
        [DllImport("shell32.dll")]
        public static extern int SHGetImageList(int iImageList, ref Guid riid, ref IImageList ppv);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern int SHGetKnownFolderPath(ref Guid id, int flags, IntPtr token, out IntPtr path);

        [DllImport("shell32.dll", SetLastError = true)]
        public static extern int SHGetMalloc(out IMalloc ppMalloc);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("shell32.dll", SetLastError = true)]
        public static extern void SHParseDisplayName([MarshalAs(UnmanagedType.LPWStr)] string name, IntPtr bindingContext, out IntPtr pidl, uint sfgaoIn, out uint psfgaoOut);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern int SHQueryUserNotificationState(out int qns);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool ShutdownBlockReasonCreate(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] string pwszReason);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool ShutdownBlockReasonDestroy(IntPtr hWnd);

        public static bool SUCCEEDED(int hr)
        {
            return 0 <= hr;
        }

        [DllImport("user32.dll")]
        public static extern int ToUnicode(uint wVirtKey, uint wScanCode, byte[] lpKeyState, [MarshalAs(UnmanagedType.LPWStr)] [Out] StringBuilder pwszBuff, int cchBuff, uint wFlags);

        [DllImport("user32.dll")]
        public static extern uint TrackPopupMenuEx(IntPtr hmenu, uint fuFlags, int x, int y, IntPtr hwnd, IntPtr lptpm);

        [DllImport("UIAutomationCore.dll")]
        public static extern bool UiaClientsAreListening();

        [DllImport("UIAutomationCore.dll")]
        public static extern int UiaDisconnectProvider(IRawElementProviderSimple provider);

        [DllImport("UIAutomationCore.dll", CharSet = CharSet.Unicode)]
        public static extern int UiaGetReservedMixedAttributeValue([MarshalAs(UnmanagedType.IUnknown)] out object mixedAttributeValue);

        [DllImport("UIAutomationCore.dll", CharSet = CharSet.Unicode)]
        public static extern int UiaGetReservedNotSupportedValue([MarshalAs(UnmanagedType.IUnknown)] out object notSupportedValue);

        [DllImport("UIAutomationCore.dll")]
        public static extern int UiaHostProviderFromHwnd(IntPtr handle, [MarshalAs(UnmanagedType.Interface)] out IRawElementProviderSimple host);

        [DllImport("UIAutomationCore.dll", CharSet = CharSet.Unicode)]
        public static extern int UiaLookupId(Win32.AutomationIdentifierType type, ref Guid guid);

        [DllImport("UIAutomationCore.dll")]
        public static extern int UiaRaiseAsyncContentLoadedEvent(IRawElementProviderSimple provider, AsyncContentLoadedState asyncContentLoadedState, double PercentComplete);

        [DllImport("UIAutomationCore.dll")]
        public static extern int UiaRaiseAutomationEvent(IRawElementProviderSimple provider, int id);

        [DllImport("UIAutomationCore.dll")]
        public static extern int UiaRaiseAutomationPropertyChangedEvent(IRawElementProviderSimple provider, int id, object oldValue, object newValue);

        [DllImport("UIAutomationCore.dll")]
        public static extern int UiaRaiseStructureChangedEvent(IRawElementProviderSimple provider, StructureChangeType structureChangeType, int[] runtimeId, int runtimeIdLen);

        
        [DllImport("UIAutomationCore.dll")]
        public static extern IntPtr UiaReturnRawElementProvider(IntPtr hwnd, IntPtr wParam, IntPtr lParam, IRawElementProviderSimple provider);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pprSrc, int crKey, ref BLENDFUNCTION pblend, BlendFlags dwFlags);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern short VkKeyScan(char ch);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern int WideCharToMultiByte(int codePage, int flags, [MarshalAs(UnmanagedType.LPWStr)] string wideStr, int chars, [In] [Out] byte[] pOutBytes, int bufferBytes, IntPtr defaultChar, IntPtr pDefaultUsed);

        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(POINT Point);

        [DllImport("Wintrust.dll")]
        public static extern uint WinVerifyTrust(IntPtr hWnd, IntPtr pgActionID, IntPtr pWinTrustData);

        private const string AdvApi32 = "advapi32.dll";

        public const int ATTR_CONVERTED = 2;

        public const int ATTR_FIXEDCONVERTED = 5;

        public const int ATTR_INPUT = 0;

        public const int ATTR_INPUT_ERROR = 4;

        public const int ATTR_TARGET_CONVERTED = 1;

        public const int ATTR_TARGET_NOTCONVERTED = 3;

        public const uint BIF_BROWSEFORCOMPUTER = 4096u;

        public const uint BIF_BROWSEFORPRINTER = 8192u;

        public const uint BIF_BROWSEINCLUDEFILES = 16384u;

        public const uint BIF_BROWSEINCLUDEURLS = 128u;

        public const uint BIF_DONTGOBELOWDOMAIN = 2u;

        public const uint BIF_EDITBOX = 16u;

        public const uint BIF_NEWDIALOGSTYLE = 64u;

        public const uint BIF_RETURNFSANCESTORS = 8u;

        public const uint BIF_RETURNONLYFSDIRS = 1u;

        public const uint BIF_SHAREABLE = 32768u;

        public const uint BIF_STATUSTEXT = 4u;

        public const uint BIF_USENEWUI = 80u;

        public const uint BIF_VALIDATE = 32u;

        public const string CFSTR_FILECONTENTS = "FileContents";

        public const string CFSTR_FILEDESCRIPTOR = "FileGroupDescriptor";

        public const string CFSTR_FILEDESCRIPTORW = "FileGroupDescriptorW";

        public const string CFSTR_PASTESUCCEEDED = "Paste Succeeded";

        public const string CFSTR_PERFORMEDDROPEFFECT = "Performed DropEffect";

        public const string CFSTR_PREFERREDDROPEFFECT = "Preferred DropEffect";

        public const int CFS_CANDIDATEPOS = 64;

        public const int CF_APPLY = 512;

        public const int CF_BOTH = 3;

        public const int CF_EFFECTS = 256;

        public const int CF_ENABLEHOOK = 8;

        public const int CF_FIXEDPITCHONLY = 16384;

        public const int CF_FORCEFONTEXIST = 65536;

        public const int CF_INACTIVEFONTS = 33554432;

        public const int CF_INITTOLOGFONTSTRUCT = 64;

        public const int CF_LIMITSIZE = 8192;

        public const int CF_NOSCRIPTSEL = 8388608;

        public const int CF_NOSIMULATIONS = 4096;

        public const int CF_NOVECTORFONTS = 2048;

        public const int CF_NOVERTFONTS = 16777216;

        public const int CF_SCREENFONTS = 1;

        public const int CF_SCRIPTSONLY = 1024;

        public const int CF_SELECTSCRIPT = 4194304;

        public const int CF_SHOWHELP = 4;

        public const int CF_TTONLY = 262144;

        public const int CF_WYSIWYG = 32768;

        private const string ComDlg32 = "comdlg32.dll";

        public const int CP_ACP = 0;

        public const int CS_INSERTCHAR = 8192;

        public const int CS_NOMOVECARET = 16384;

        public const int CURSOR_SHOWING = 1;

        public const int CURSOR_SUPPRESSED = 2;

        public const int DV_E_DVASPECT = -2147221397;

        public const int DV_E_FORMATETC = -2147221404;

        public const int DV_E_TYMED = -2147221399;

        private const string DwmApi = "dwmapi.dll";

        public const int EM_CANUNDO = 198;

        public const int EM_CHARFROMPOS = 215;

        public const int EM_EMPTYUNDOBUFFER = 205;

        public const int EM_GETFIRSTVISIBLELINE = 206;

        public const int EM_GETLINE = 196;

        public const int EM_GETLINECOUNT = 186;

        public const int EM_GETMODIFY = 184;

        public const int EM_GETPASSWORDCHAR = 210;

        public const int EM_GETSEL = 176;

        public const int EM_LIMITTEXT = 197;

        public const int EM_LINEFROMCHAR = 201;

        public const int EM_LINEINDEX = 187;

        public const int EM_POSFROMCHAR = 214;

        public const int EM_REPLACESEL = 194;

        public const int EM_SCROLL = 181;

        public const int EM_SCROLLCARET = 183;

        public const int EM_SETMARGINS = 211;

        public const int EM_SETMODIFY = 185;

        public const int EM_SETPASSWORDCHAR = 204;

        public const int EM_SETREADONLY = 207;

        public const int EM_SETSEL = 177;

        public const int EM_UNDO = 199;

        public const int E_FAIL = -2147467259;

        public const uint FD_CREATETIME = 8u;

        public const uint FD_FILESIZE = 64u;

        public const uint FD_UNICODE = 2147483648u;

        public const uint FD_WRITESTIME = 32u;

        public const uint FILE_ATTRIBUTE_DIRECTORY = 16u;

        public const uint FILE_ATTRIBUTE_NORMAL = 128u;

        public const int GCS_COMPATTR = 16;

        public const int GCS_COMPCLAUSE = 32;

        public const int GCS_COMPREADATTR = 2;

        public const int GCS_COMPREADCLAUSE = 4;

        public const int GCS_COMPREADSTR = 1;

        public const int GCS_COMPSTR = 8;

        public const int GCS_CURSORPOS = 128;

        public const int GCS_DELTASTART = 256;

        public const int GCS_RESULTCLAUSE = 4096;

        public const int GCS_RESULTREADCLAUSE = 1024;

        public const int GCS_RESULTREADSTR = 512;

        public const int GCS_RESULTSTR = 2048;

        public const int GC_ALLGESTURES = 1;

        public const int GC_PAN = 1;

        public const int GC_PAN_WITH_GUTTER = 8;

        public const int GC_PAN_WITH_INERTIA = 16;

        public const int GC_PAN_WITH_SINGLE_FINGER_HORIZONTALLY = 4;

        public const int GC_PAN_WITH_SINGLE_FINGER_VERTICALLY = 2;

        public const int GC_ZOOM = 1;

        private const string Gdi32 = "gdi32.dll";

        public const int GF_BEGIN = 1;

        public const int GF_END = 4;

        public const int GF_INERTIA = 2;

        public const int GHND = 66;

        public const int GID_BEGIN = 1;

        public const int GID_END = 2;

        public const int GID_PAN = 4;

        public const int GID_PRESSANDTAP = 7;

        public const int GID_ROTATE = 5;

        public const int GID_TWOFINGERTAP = 6;

        public const int GID_ZOOM = 3;

        public const int GMEM_DDESHARE = 8192;

        public const int GMEM_DISCARDABLE = 256;

        public const int GMEM_FIXED = 0;

        public const int GMEM_INVALID_HANDLE = 32768;

        public const int GMEM_LOWER = 4096;

        public const int GMEM_MODIFY = 128;

        public const int GMEM_MOVEABLE = 2;

        public const int GMEM_NOCOMPACT = 16;

        public const int GMEM_NODISCARD = 32;

        public const int GMEM_NOTIFY = 16384;

        public const int GMEM_NOT_BANKED = 4096;

        public const int GMEM_SHARE = 8192;

        public const int GMEM_VALID_FLAGS = 32626;

        public const int GMEM_ZEROINIT = 64;

        public const int GPTR = 64;

        public const uint GRADIENT_FILL_OP_FLAG = 255u;

        public const uint GRADIENT_FILL_RECT_H = 0u;

        public const uint GRADIENT_FILL_RECT_V = 1u;

        public const uint GRADIENT_FILL_TRIANGLE = 2u;

        public const int GWL_WNDPROC = -4;

        public const int GW_CHILD = 5;

        public const int GW_HWNDFIRST = 0;

        public const int GW_HWNDLAST = 1;

        public const int GW_HWNDNEXT = 2;

        public const int GW_HWNDPREV = 3;

        public const int GW_OWNER = 4;

        public const int HC_ACTION = 0;

        public static readonly IntPtr HWND_TOPMOST = (IntPtr)(-1);

        private const string Imm32 = "imm32.dll";

        private const string Kernel32 = "kernel32.dll";

        public const int MAX_PATH = 260;

        public const int MA_ACTIVATE = 1;

        public const int MA_ACTIVATEANDEAT = 2;

        public const int MA_NOACTIVATE = 3;

        public const int MA_NOACTIVATEANDEAT = 4;

        public const int MK_CONTROL = 8;

        public const int MK_LBUTTON = 1;

        public const int MK_MBUTTON = 16;

        public const int MK_RBUTTON = 2;

        public const int MK_SHIFT = 4;

        public const int MK_XBUTTON1 = 32;

        public const int MK_XBUTTON2 = 64;

        public const int MONITORINFOF_PRIMARY = 1;

        public const int MONITOR_DEFAULTTONEAREST = 2;

        public const int MONITOR_DEFAULTTONULL = 0;

        public const int MONITOR_DEFAULTTOPRIMARY = 1;

        private const string Ole32 = "ole32.dll";

        public const int OLE_E_ADVISENOTSUPPORTED = -2147221501;

        private const string Secur32 = "secur32.dll";

        private const string Shcore = "shcore.dll";

        private const string Shell32 = "shell32.dll";

        public const uint SHGFI_ADDOVERLAYS = 32u;

        public const uint SHGFI_ATTRIBUTES = 2048u;

        public const uint SHGFI_ATTR_SPECIFIED = 131072u;

        public const uint SHGFI_DISPLAYNAME = 512u;

        public const uint SHGFI_EXETYPE = 8192u;

        public const uint SHGFI_ICON = 256u;

        public const uint SHGFI_ICONLOCATION = 4096u;

        public const uint SHGFI_LARGEICON = 0u;

        public const uint SHGFI_LINKOVERLAY = 32768u;

        public const uint SHGFI_OPENICON = 2u;

        public const uint SHGFI_OVERLAYINDEX = 64u;

        public const uint SHGFI_PIDL = 8u;

        public const uint SHGFI_SELECTED = 65536u;

        public const uint SHGFI_SHELLICONSIZE = 4u;

        public const uint SHGFI_SMALLICON = 1u;

        public const uint SHGFI_SYSICONINDEX = 16384u;

        public const uint SHGFI_TYPENAME = 1024u;

        public const uint SHGFI_USEFILEATTRIBUTES = 16u;

        private const string ShlWapi = "shlwapi.dll";

        public const int SIZE_MAXHIDE = 4;

        public const int SIZE_MAXIMIZED = 2;

        public const int SIZE_MAXSHOW = 3;

        public const int SIZE_MINIMIZED = 1;

        public const int SIZE_RESTORED = 0;

        public const uint SWP_NOACTIVATE = 16u;

        public const int SW_ERASE = 4;

        public const int SW_HIDE = 0;

        public const int SW_INVALIDATE = 2;

        public const int SW_MAXIMIZE = 3;

        public const int SW_MINIMIZE = 6;

        public const int SW_NORMAL = 1;

        public const int SW_RESTORE = 9;

        public const int SW_SCROLLCHILDREN = 1;

        public const int SW_SHOW = 5;

        public const int SW_SHOWMAXIMIZED = 3;

        public const int SW_SHOWMINIMIZED = 2;

        public const int SW_SHOWMINNOACTIVE = 7;

        public const int SW_SHOWNA = 8;

        public const int SW_SHOWNOACTIVATE = 4;

        public const int SW_SHOWNORMAL = 1;

        public const int SW_SMOOTHSCROLL = 16;

        public const int S_FALSE = 1;

        public const int S_OK = 0;

        public const int TOUCHEVENTF_DOWN = 2;

        public const int TOUCHEVENTF_INRANGE = 8;

        public const int TOUCHEVENTF_MOVE = 1;

        public const int TOUCHEVENTF_NOCOALESCE = 32;

        public const int TOUCHEVENTF_PEN = 64;

        public const int TOUCHEVENTF_PRIMARY = 16;

        public const int TOUCHEVENTF_UP = 4;

        public const int TOUCHINPUTMASKF_CONTACTAREA = 4;

        public const int TOUCHINPUTMASKF_EXTRAINFO = 2;

        public const int TOUCHINPUTMASKF_TIMEFROMSYSTEM = 1;

        private const string UIAutomationCore = "UIAutomationCore.dll";

        public const int UIA_E_ELEMENTNOTAVAILABLE = -2147220991;

        public const int UIA_E_ELEMENTNOTENABLED = -2147220992;

        public const int UIA_E_NOCLICKABLEPOINT = -2147220990;

        public const int UIA_E_PROXYASSEMBLYNOTLOADED = -2147220989;

        public const long ULL_ARGUMENTS_BIT_MASK = 4294967295L;

        private const string User32 = "user32.dll";

        public const int VARIANT_FALSE = 0;

        public const int VARIANT_TRUE = -1;

        public const int WA_ACTIVE = 1;

        public const int WA_CLICKACTIVE = 2;

        public const int WA_INACTIVE = 0;

        public const int WH_CALLWNDPROC = 4;

        private const string WinTrust = "Wintrust.dll";

        public const int WM_ACTIVATE = 6;

        public const int WM_ACTIVATEAPP = 28;

        public const int WM_APP_REOPEN = 1044;

        public const int WM_BEGINDRAG = 556;

        public const int WM_CANCELMODE = 31;

        public const int WM_CAPTURECHANGED = 533;

        public const int WM_CHANGEUISTATE = 295;

        public const int WM_CHAR = 258;

        public const int WM_CHARTOITEM = 47;

        public const int WM_CHILDACTIVATE = 34;

        public const int WM_CLEAR = 771;

        public const int WM_CLOSE = 16;

        public const int WM_COMMAND = 273;

        public const int WM_CONTEXTMENU = 123;

        public const int WM_COPY = 769;

        public const int WM_CREATE = 1;

        public const int WM_CTLCOLORBTN = 309;

        public const int WM_CTLCOLORDLG = 310;

        public const int WM_CTLCOLOREDIT = 307;

        public const int WM_CTLCOLORLISTBOX = 308;

        public const int WM_CTLCOLORMSGBOX = 306;

        public const int WM_CTLCOLORSCROLLBAR = 311;

        public const int WM_CTLCOLORSTATIC = 312;

        public const int WM_CUT = 768;

        public const int WM_DEADCHAR = 259;

        public const int WM_DELETEITEM = 45;

        public const int WM_DESTROY = 2;

        public const int WM_DEVICECHANGE = 537;

        public const int WM_DOCK_MENU = 1041;

        public const int WM_DRAGLOOP = 557;

        public const int WM_DRAGMOVE = 559;

        public const int WM_DRAGSELECT = 558;

        public const int WM_DRAWITEM = 43;

        public const int WM_DRAW_FOCUS_RING_MASK = 1046;

        public const int WM_DROPFILES = 563;

        public const int WM_DROPOBJECT = 554;

        public const int WM_DWMCOMPOSITIONCHANGED = 798;

        public const int WM_EFFECTIVE_APPEARANCE_CHANGED = 1045;

        public const int WM_ENABLE = 10;

        public const int WM_ENDSESSION = 22;

        public const int WM_ENTERIDLE = 289;

        public const int WM_ENTERMENULOOP = 529;

        public const int WM_ENTERSIZEMOVE = 561;

        public const int WM_ERASEBKGND = 20;

        public const int WM_EXITMENULOOP = 530;

        public const int WM_EXITSIZEMOVE = 562;

        public const int WM_FOCUS_RING_MASK_BOUNDS = 1047;

        public const int WM_FONTCHANGE = 29;

        public const int WM_GESTURE = 281;

        public const int WM_GESTURENOTIFY = 282;

        public const int WM_GETFONT = 49;

        public const int WM_GETHOTKEY = 51;

        public const int WM_GETMINMAXINFO = 36;

        public const int WM_GETOBJECT = 61;

        public const int WM_GETTEXT = 13;

        public const int WM_GETTEXTLENGTH = 14;

        public const int WM_HSCROLL = 276;

        public const int WM_ICONERASEBKGND = 39;

        public const int WM_IME_CHAR = 646;

        public const int WM_IME_COMPOSITION = 271;

        public const int WM_IME_COMPOSITIONFULL = 644;

        public const int WM_IME_CONTROL = 643;

        public const int WM_IME_ENDCOMPOSITION = 270;

        public const int WM_IME_GETCURRENTPOSITION = 1042;

        public const int WM_IME_KEYDOWN = 656;

        public const int WM_IME_KEYLAST = 271;

        public const int WM_IME_KEYUP = 657;

        public const int WM_IME_NOTIFY = 642;

        public const int WM_IME_REQUEST = 648;

        public const int WM_IME_SELECT = 645;

        public const int WM_IME_SETCONTEXT = 641;

        public const int WM_IME_STARTCOMPOSITION = 269;

        public const int WM_INITDIALOG = 272;

        public const int WM_INITMENU = 278;

        public const int WM_INITMENUPOPUP = 279;

        public const int WM_KEYDOWN = 256;

        public const int WM_KEYFIRST = 256;

        public const int WM_KEYLAST = 264;

        public const int WM_KEYUP = 257;

        public const int WM_KILLFOCUS = 8;

        public const int WM_LBTRACKPOINT = 305;

        public const int WM_LBUTTONDBLCLK = 515;

        public const int WM_LBUTTONDOWN = 513;

        public const int WM_LBUTTONUP = 514;

        public const int WM_MBUTTONDBLCLK = 521;

        public const int WM_MBUTTONDOWN = 519;

        public const int WM_MBUTTONUP = 520;

        public const int WM_MDIACTIVATE = 546;

        public const int WM_MDICASCADE = 551;

        public const int WM_MDICREATE = 544;

        public const int WM_MDIDESTROY = 545;

        public const int WM_MDIGETACTIVE = 553;

        public const int WM_MDIICONARRANGE = 552;

        public const int WM_MDIMAXIMIZE = 549;

        public const int WM_MDINEXT = 548;

        public const int WM_MDIREFRESHMENU = 564;

        public const int WM_MDIRESTORE = 547;

        public const int WM_MDISETMENU = 560;

        public const int WM_MDITILE = 550;

        public const int WM_MEASUREITEM = 44;

        public const int WM_MENUCHAR = 288;

        public const int WM_MENUCOMMAND = 294;

        public const int WM_MENUDRAG = 291;

        public const int WM_MENUGETOBJECT = 292;

        public const int WM_MENURBUTTONUP = 290;

        public const int WM_MENUSELECT = 287;

        public const int WM_MOUSEACTIVATE = 33;

        public const int WM_MOUSEFIRST = 512;

        public const int WM_MOUSEHOVER = 673;

        public const int WM_MOUSEHWHEEL = 526;

        public const int WM_MOUSELAST = 525;

        public const int WM_MOUSELEAVE = 675;

        public const int WM_MOUSEMOVE = 512;

        public const int WM_MOUSEWHEEL = 522;

        public const int WM_MOVE = 3;

        public const int WM_MOVING = 534;

        public const int WM_NCACTIVATE = 134;

        public const int WM_NCCALCSIZE = 131;

        public const int WM_NCCREATE = 129;

        public const int WM_NCHITTEST = 132;

        public const int WM_NCLBUTTONDOWN = 161;

        public const int WM_NCLBUTTONUP = 162;

        public const int WM_NCMOUSEHOVER = 672;

        public const int WM_NCMOUSELEAVE = 674;

        public const int WM_NCMOUSEMOVE = 160;

        public const int WM_NCPAINT = 133;

        public const int WM_NCRBUTTONUP = 165;

        public const int WM_NEXTDLGCTL = 40;

        public const int WM_NEXTMENU = 531;

        public const int WM_NULL = 0;

        public const int WM_OPEN_FILES = 1040;

        public const int WM_OPEN_URLS = 1043;

        public const int WM_PAINT = 15;

        public const int WM_PAINTICON = 38;

        public const int WM_PARENTNOTIFY = 528;

        public const int WM_PASTE = 770;

        public const int WM_POINTERACTIVATE = 587;

        public const int WM_POINTERDOWN = 582;

        public const int WM_POINTERUP = 583;

        public const int WM_POINTERUPDATE = 581;

        public const int WM_POWERBROADCAST = 536;

        public const int WM_PRINT = 791;

        public const int WM_QUERYDROPOBJECT = 555;

        public const int WM_QUERYENDSESSION = 17;

        public const int WM_QUERYOPEN = 19;

        public const int WM_QUERYUISTATE = 297;

        public const int WM_QUEUESYNC = 35;

        public const int WM_QUIT = 18;

        public const int WM_RBUTTONDBLCLK = 518;

        public const int WM_RBUTTONDOWN = 516;

        public const int WM_RBUTTONUP = 517;

        public const int WM_SELECT_ALL = 1028;

        public const int WM_SETCURSOR = 32;

        public const int WM_SETFOCUS = 7;

        public const int WM_SETFONT = 48;

        public const int WM_SETHOTKEY = 50;

        public const int WM_SETICON = 128;

        public const int WM_SETREDRAW = 11;

        public const int WM_SETTEXT = 12;

        public const int WM_SETTINGCHANGE = 26;

        public const int WM_SHARED_MENU = 482;

        public const int WM_SHOWWINDOW = 24;

        public const int WM_SIZE = 5;

        public const int WM_SIZING = 532;

        public const int WM_SPOOLERSTATUS = 42;

        public const int WM_SYSCHAR = 262;

        public const int WM_SYSCOLORCHANGE = 21;

        public const int WM_SYSCOMMAND = 274;

        public const int WM_SYSDEADCHAR = 263;

        public const int WM_SYSKEYDOWN = 260;

        public const int WM_SYSKEYUP = 261;

        public const int WM_SYSTIMER = 280;

        public const int WM_TIMECHANGE = 30;

        public const int WM_TIMER = 275;

        public const int WM_TOUCH = 576;

        public const int WM_UNINITMENUPOPUP = 293;

        public const int WM_UPDATEUISTATE = 296;

        public const int WM_USER = 1024;

        public const int WM_USERCHANGED = 84;

        public const int WM_VKEYTOITEM = 46;

        public const int WM_VSCROLL = 277;

        public const int WM_WINDOWPOSCHANGED = 71;

        public const int WM_WINDOWPOSCHANGING = 70;

        public const int WM_WININICHANGE = 26;

        public const int WM_XBUTTONDBLCLK = 525;

        public const int WM_XBUTTONDOWN = 523;

        public const int WM_XBUTTONUP = 524;

        public const int WVR_ALIGNBOTTOM = 64;

        public const int WVR_ALIGNLEFT = 32;

        public const int WVR_ALIGNRIGHT = 128;

        public const int WVR_ALIGNTOP = 16;

        public const int WVR_HREDRAW = 256;

        public const int WVR_REDRAW = 768;

        public const int WVR_VALIDRECTS = 1024;

        public const int WVR_VREDRAW = 512;

        
        public enum AutomationIdentifierType
        {

            Property,

            Pattern,

            Event,

            ControlType,

            TextAttribute,

            LandmarkType,

            Annotation,

            Changes,

            Style
        }

        
        public enum CDCONTROLSTATE
        {

            CDCS_INACTIVE,

            CDCS_ENABLED,

            CDCS_VISIBLE
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
        public struct COMDLG_FILTERSPEC
        {

            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszName;

            [MarshalAs(UnmanagedType.LPWStr)]
            public string pszSpec;
        }

        
        public struct DWMCOLORIZATIONPARAMS
        {

            public uint ColorizationColor;

            public uint ColorizationAfterglow;

            public uint ColorizationColorBalance;

            public uint ColorizationAfterglowBalance;

            public uint ColorizationBlurBalance;

            public uint ColorizationGlassReflectionIntensity;

            public uint ColorizationOpaqueBlend;
        }


        
        public delegate bool EnumWindowProc(IntPtr hWnd, IntPtr parameter);

        
        public enum FDAP
        {

            FDAP_BOTTOM,

            FDAP_TOP
        }

        
        public enum FDE_OVERWRITE_RESPONSE
        {

            FDEOR_DEFAULT,

            FDEOR_ACCEPT,

            FDEOR_REFUSE
        }

        
        public enum FDE_SHAREVIOLATION_RESPONSE
        {

            FDESVR_DEFAULT,

            FDESVR_ACCEPT,

            FDESVR_REFUSE
        }

        public enum FOS : uint
        {

            FOS_OVERWRITEPROMPT = 2u,

            FOS_STRICTFILETYPES = 4u,

            FOS_NOCHANGEDIR = 8u,

            FOS_PICKFOLDERS = 32u,

            FOS_FORCEFILESYSTEM = 64u,

            FOS_ALLNONSTORAGEITEMS = 128u,

            FOS_NOVALIDATE = 256u,

            FOS_ALLOWMULTISELECT = 512u,

            FOS_PATHMUSTEXIST = 2048u,

            FOS_FILEMUSTEXIST = 4096u,

            FOS_CREATEPROMPT = 8192u,

            FOS_SHAREAWARE = 16384u,

            FOS_NOREADONLYRETURN = 32768u,

            FOS_NOTESTFILECREATE = 65536u,

            FOS_HIDEMRUPLACES = 131072u,

            FOS_HIDEPINNEDPLACES = 262144u,

            FOS_NODEREFERENCELINKS = 1048576u,

            FOS_DONTADDTORECENT = 33554432u,

            FOS_FORCESHOWHIDDEN = 268435456u,

            FOS_DEFAULTNOMINIMODE = 536870912u
        }

        
        public enum Monitor_DPI_Type
        {

            MDT_Effective_DPI,

            MDT_Angular_DPI,

            MDT_Raw_DPI,

            MDT_Default = 0
        }

        
        public enum NetJoinStatus
        {
            NetSetupUnknownStatus,
            NetSetupUnjoined,
            NetSetupWorkgroupName,
            NetSetupDomainName
        }

        
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct PROPERTYKEY
        {

            internal Guid fmtid;
            internal uint pid;
        }

        
        public class SafeModuleHandle : SafeHandle
        {

            public SafeModuleHandle()
                : base(IntPtr.Zero, true)
            {
            }

            protected override bool ReleaseHandle()
            {
                return Win32.FreeLibrary(this.handle);
            }

            public override bool IsInvalid
            {
                get
                {
                    return this.handle == IntPtr.Zero;
                }
            }
        }

        
        public enum SIATTRIBFLAGS
        {
            SIATTRIBFLAGS_AND = 1,
            SIATTRIBFLAGS_OR,
            SIATTRIBFLAGS_APPCOMPAT
        }

        
        public enum SIGDN : uint
        {
            SIGDN_NORMALDISPLAY,
            SIGDN_PARENTRELATIVEPARSING = 2147581953u,
            SIGDN_DESKTOPABSOLUTEPARSING = 2147647488u,
            SIGDN_PARENTRELATIVEEDITING = 2147684353u,
            SIGDN_DESKTOPABSOLUTEEDITING = 2147794944u,
            SIGDN_FILESYSPATH = 2147844096u,
            SIGDN_URL = 2147909632u,
            SIGDN_PARENTRELATIVEFORADDRESSBAR = 2147991553u,
            SIGDN_PARENTRELATIVE = 2148007937u
        }
    }
}
