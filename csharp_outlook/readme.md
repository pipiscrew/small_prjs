# send attachment with outlook

* `interop.cs` - interop method to interact with outlook
* `LateBind.cs` - most comfortable way to go
* `Windows_Explorer_SendBy_MailRecipient_is_using_sendmail_dll.cs` - emulates the common way all users using the windows explorer context menu `Send To` > `Mail recipient` [[source by Castorix31](https://learn.microsoft.com/en-us/answers/questions/1162850/opening-the-default-e-email-app)]

be careful :  
is possible for all of the above implementations to end up that when **outlook is running** doesnt pop the new email window.. Has to do that the existing instance of outlook is running under **another user** or even the same user with `administrator rights`. The error is :

```js
COM Exception: Retrieving the COM class factory for component with CLSID {0006F03A-0000-0000-C000-000000000046}
failed due to the following error: 80080005 Server execution failed (Exception from HRESULT: 0x80080005 (CO_E_SERVER_EXEC_FAILURE)).
```