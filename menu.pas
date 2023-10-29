Program Menu;

{ This code show an example of a menu driven program }
{ Use the following keys: UP, DOWN, CR/ENTER or ESC  }
{ Demo by Jakob Joergensen, jakob_dk2300@yahoo.com   }
{ src - https://www.tek-tips.com/viewthread.cfm?qid=578106 }

Uses
  Crt;

Const
  TotalItems = 3;    
  { /\ How many menu item do you want? }

  MenuItem : Array [1..TotalItems] of String =('Do this', 'Do that', 'Exit');
  { /\ What are the menu items called? }

  MenuXofs = 10;
  { /\ Where do you wat the menu to be shown = X-axis? }

  MenuYofs = 3;
  { /\ Where do you wat the menu to be shown = Y-axis? }

  SelectedFC = White;
  { /\ What's the Foreground Color for selected menu item? }

  SelectedBC = Blue;
  { /\ What's the Background Color for selected menu item? }

  NormalFC = LightGray;
  { /\ What's the Foreground Color for inactive menu items? }

  NormalBC = Black;
  { /\ What's the Background Color for inactive menu items? }

  SelectorWidth = 20;
  { /\ What's the width of the selector bar? }

  UpKey = #80;
  { /\ ASCII code for the UP key }

  DownKey = #72;
  { /\ ASCII code for the DOWN key }

  EscKey = #27;
  { /\ ASCII code for the ESC key }

  CRKey = #13;
  { /\ ASCII code for the CR/ENTER key }



Procedure ShowMenu(SelectedItem : Integer);
{  This shows the menu on the screen. Selected Item number is highlighted }

Var
  CurrentItem     : Integer;
  Blanks          : Integer;
  FC, BC          : Byte;
  CurrentMenuText : String;
  CurrentMenuSize : Integer;
Begin
  For CurrentItem := 1 to TotalItems do
    Begin
      CurrentMenuText := MenuItem[CurrentItem];
      CurrentMenuSize := Length(CurrentMenuText);
      if CurrentItem = SelectedItem then
        Begin
          FC := SelectedFC;
          BC := SelectedBC;
        End
      Else
        Begin
          FC := NormalFC;
          BC := NormalBC;
        End;
      TextColor(FC);
      TextBackground(BC);
      GotoXY(MenuXofs,MenuYofs+CurrentItem-1);
      Write(CurrentMenuText);
      For Blanks := 1 to (SelectorWidth-CurrentMenuSize) do
        Write(#32);
    End;
End;

Function Selector : Char;
{  This reads the "valid keys" }

Var
  Key : Char;
Begin
  Repeat
    Key := ReadKey;
  Until Key in [UpKey,DownKey,EscKey,CRKey];
  Selector := Key;
End;


Function Navigate(KeyFunc : Char; Curr : Integer) : Integer;
{  This browses the menu selected number }

Var
  Current : Integer;
Begin
  Current := Curr;
  If KeyFunc = UpKey then
    Inc(Current);
  If KeyFunc = DownKey then
    Dec(Current);
  If Current > TotalItems then
    Current := 1;
  If Current < 1 then
    Current := TotalItems;
  Navigate := Current;
End;


Procedure DoThis;
{ Your stuff for Menu Item 1 goes here }

Begin
  ClrScr;
  WriteLn('... i'll do THIS');
  WriteLn('Press ENTER...');
  ReadLn;
  ClrScr;
End;

Procedure DoThat;
{ Your stuff for Menu Item 2 goes here }

Begin
  ClrScr;
  WriteLn('... i'll do THAT');
  WriteLn('Press ENTER...');
  ReadLn;
  ClrScr;
End;

Var
  Key : Char;
  MenuPos : Integer;
Begin
  ClrScr;
  MenuPos := 1;
  Repeat
    Repeat
      ShowMenu(MenuPos);
      Key := Selector;
      MenuPos := Navigate(Key, MenuPos);
    Until Key in [CRKey,EscKey];
    If MenuPos = 1 then
      DoThis;
    If MenuPos = 2 then
      DoThat;
  Until (Key = EscKey) or (MenuPos = TotalItems);
  TextColor(NormalFC);
  TextBackground(NormalBC);
  ClrScr;
  WriteLn('Bye now ...');
End.