uses crt,dos,screens;
type str=string [30];
type Bus= Record
          Name : String[17];
          Typ  : String[17];
          Cont : String[20];
        ContTel: String[15];
        Add1   : String[20];
        Add2   : String[20];
        City   : String[20];
        Zip    : String[6];
        Tel1   : String[15];
        Tel2   : String[15];
        Web1   : String[20];
        Web2   : String[20];
        WebUser: String[13];
        WebPass: String[10];
        Comm   : String[63];
      end;

     BirthDa= Record
                 Day:String[2];
               Month:String[2];
                Year:String[2];
          end;

     Person= Record
             LName   : String[20];
             FName   : String[20];
             Add1    : String[20];
             Add2    : String[20];
             City    : String[20];
             Zip     : String[8];
             BirthDay: birthda;
             Email1  : String[20];
             Email2  : String[20];
             Url     : String[40];
             Comment : String[62];
             Tel1    : String[15];
             Tel2    : String[15];
             Cell1   : String[14];
             Cell2   : String[14];
             Work1   : String[15];
             Work2   : String[15];
             Icq     : String[11];
             Msn     : String[11];
           end;

var arr: array [1..10] of str;
    i : integer;
    ext:boolean;
    usedstr :string;
    Perso : Person;
    FilePerson : File Of Person;
    Buss : bus;
    FileBus : file of Bus;

Procedure ClrXY(x,y,why,spac:integer);forward;
Procedure EditPerson;forward;
Procedure EditBusiness;forward;

Procedure ExportHTMLBusin;
var html : text;
begin
assign(html,'Business.htm');
rewrite(html);
writeln(html,'<html>');
writeln(html,'<head>');
writeln(html,'<title>TelePhoneBook v1.oO By Black Bird (c) 2002</title>');
writeln(html,'<meta http-equiv="Content-Type" content="text/html; charset=windows-1253">');
writeln(html,'</head>');
writeln(html,'<body bgcolor="#000000" text="#00FFFF">');
assign(filebus,'business.dat');
reset(filebus);
write(html,'<p align="center"><font color="#FF0000" face="Comic Sans MS" size="5"><b>Total Records ');
write(html,filesize(filebus));
writeln(html,'</b></font></p>');
while not EOF(filebus) do
begin
read(filebus,buss);
writeln(html,'<hr width="100%" size="0" noshade>');
writeln(html,'<table width="100%" border="1">');
writeln(html,'<tr>');
writeln(html,'<td colspan="2">');
write(html,'<p><font size="3" face="Comic Sans MS">Name : <font color="#FFFF00">');
writeln(html,buss.name,'</font></font></p>');
write(html,'<p><font face="Comic Sans MS" size="3" color="#00FFFF">Type : <font color="#FFFF00">');
writeln(html,buss.typ,'</font></font></p>');
write(html,'<td colspan="2" height="0" width="64%"> <font size="3" face="Comic Sans MS">');
write(html,'<font color="#00FFFF">Telephone&gt;&gt;</font><font color="#FFFF00">');
writeln(html,buss.tel1,'</font>');
if (length(buss.tel2)>0) then begin
  write(html,'<font color="#00FF00">---</font><font color="#00FFFF"> Telephone2&gt;&gt;</font><font color="#FFFF00">');
  writeln(html,buss.tel2,'</font>');
end;
if (length(buss.city)>0) then begin
  write(html,'<font color="#00FF00">---</font><font color="#00FFFF"> City&gt;&gt;</font><font color="#FFFF00">');
  writeln(html,buss.city,'</font>');
end;
if (length(buss.add1)>0) then begin
  write(html,'<font color="#00FF00">---</font><font color="#00FFFF"> Address&gt;&gt;</font><font color="#FFFF00">');
  writeln(html,buss.add1,'</font>');
end;
if (length(buss.cont)>0) then begin
  write(html,'<font color="#00FF00">---</font><font color="#00FFFF"> ContactName&gt;&gt;</font><font color="#FFFF00">');
  writeln(html,buss.cont,'</font>');
end;
if (length(buss.conttel)>0) then begin
  write(html,'<font color="#00FF00">---</font><font color="#00FFFF"> ContactPhone&gt;&gt;</font><font color="#FFFF00">');
  writeln(html,buss.conttel,'</font>');
end;
if (length(buss.web1)>0) then begin
  write(html,'<font color="#00FF00">---</font><font color="#00FFFF"> URL&gt;&gt;</font><font color="#FFFF00">');
  writeln(html,buss.web1,'</font>');
end;
if (length(buss.web2)>0) then begin
  write(html,'<font color="#00FF00">---</font><font color="#00FFFF"> URL2&gt;&gt;</font><font color="#FFFF00">');
  writeln(html,buss.web2,'</font>');
end;
writeln(html,'</td></tr></table>');
writeln(html,'</body></html>');
end;
close(filebus);
close(html);
end;


Procedure ExportHTMLPers;
var html : text;
begin
assign(html,'Personal.htm');
rewrite(html);
writeln(html,'<html>');
writeln(html,'<head>');
writeln(html,'<title>TelePhoneBook v1.oO By Black Bird (c) 2002</title>');
writeln(html,'<meta http-equiv="Content-Type" content="text/html; charset=windows-1253">');
writeln(html,'</head>');
writeln(html,'<body bgcolor="#000000" text="#00FFFF">');
assign(fileperson,'personal.dat');
reset(fileperson);
write(html,'<p align="center"><font color="#FF0000" face="Comic Sans MS" size="5"><b>Total Records ');
write(html,filesize(fileperson));
writeln(html,'</b></font></p>');
i:=0;
while not EOF(fileperson) do
begin
read(fileperson,perso);
i:=i+1;
writeln(html,'<hr width="100%" size="0" noshade>');
writeln(html,'<table width="100%" border="1">');
writeln(html,'  <tr>');
writeln(html,'    <td colspan="2">');
write(html,'<p><font size="3" face="Comic Sans MS">Name : <font color="#FFFF00">');
writeln(html,perso.lname,' ',perso.fname,'</font></font></p>');
writeln(html,'<td colspan="2" height="15" width="64%">');
writeln(html,'<font size="3" face="Comic Sans MS">');
write(html,'<font color="#00FFFF">Telephone1&gt;&gt;</font><font color="#FFFF00">');
writeln(html,perso.tel1,'</font>');
if (length(perso.tel2)>0) then begin
  write(html,'<font color="#00FF00">---</font><font color="#00FFFF"> Telephone2&gt;&gt;</font><font color="#FFFF00">');
  writeln(html,perso.tel2,'</font>');
end;
if (length(perso.cell1)>0) then begin
  write(html,'<font color="#00FF00">---</font><font color="#00FFFF"> CellPhone1&gt;&gt;</font><font color="#FFFF00">');
  writeln(html,perso.cell1,'</font>');
end;
if (length(perso.cell2)>0) then begin
  write(html,'<font color="#00FF00">---</font><font color="#00FFFF"> CellPhone2&gt;&gt;</font><font color="#FFFF00">');
  writeln(html,perso.cell2,'</font>');
end;
if (length(perso.icq)>0) then begin
  write(html,'<font color="#00FF00">---</font><font color="#00FFFF"> ICQ#&gt;&gt;</font><font color="#FFFF00">');
  writeln(html,perso.icq,'</font>');
end;
if (length(perso.msn)>0) then begin
  write(html,'<font color="#00FF00">---</font><font color="#00FFFF"> MSN#&gt;&gt;</font><font color="#FFFF00">');
  writeln(html,perso.msn,'</font>');
end;
if (length(perso.email1)>0) then begin
  write(html,'<font color="#00FF00">---</font><font color="#00FFFF"> Email&gt;&gt;</font><font color="#FFFF00">');
  writeln(html,perso.email1,'</font>');
end;
if (length(perso.url)>0) then begin
  write(html,'<font color="#00FF00">---</font> URL&gt;&gt;<font color="#FFFF00">');
  writeln(html,perso.url,'</font>');
end;
writeln(html,'</td></tr></table>');
writeln(html,'</body></html>');
end;
close(fileperson);
close(html);
end;

{-------------------------}
Procedure Curoff;
var regs : registers;
begin
regs.ah:=1;
regs.cx:=$100;
intr($10,regs);
end;

Procedure Curon;
var regs : registers;
begin
regs.ah:=1;
regs.cx:=$10;
intr($10,regs);
end;

Procedure ClrScreen;
var
  i,i2,t : integer;
begin
i := 24;
i2 := 1;
gotoxy(1,25);delline;
repeat
textcolor(8);
gotoxy(1,i);for t:=1 to 80 do write('Ä');
gotoxy(1,i2);for t:=1 to 80 do write('Ä');delay(300);
gotoxy(1,i);for t:=1 to 80 do write(' ');
gotoxy(1,i2);for t:=1 to 80 do write(' ');
i:=i-1;
i2:=i2 + 1;
until i2 = 13;
begin
textcolor(8);gotoxy(1,12);for t:=1 to 80 do Write('Ä');delay(300);
textcolor(7);gotoxy(1,12);for t:=1 to 80 do Write('Ä');delay(300);
textcolor(8);gotoxy(1,12);for t:=1 to 80 do Write('Ä');delay(300);
textcolor(7);gotoxy(1,12);for t:=1 to 80 do Write('Ä');delay(300);
textcolor(15);gotoxy(1,12);for t:=1 to 80 do Write('Ä');delay(300);
gotoxy(1,12);for t:=1 to 80 do write(' ');
textcolor(7);
end;
end;


Procedure ClrDelUp(x,y,z,d:integer);
var
  i: integer;
begin
for i:=1 to z do
begin
gotoxy(x,y);delline;delay(d);
end;
end;

Procedure ClrDelDown(x,y,z,d:integer);
var
  i: integer;
begin
for i:=1 to z do
begin
gotoxy(x,y);insline;delay(d);
end;
end;

Procedure BgC(tb:integer;tc:integer);
begin
textbackground(tb);
textcolor(tc);
end;

Procedure WaitEnter(x,y,d:integer);
var t:char;
    const
         messy = 'ÄÍşPress Enter To ContinueşÍÄ';
begin
curoff;
repeat
if (keypressed=true) then t:=readkey;
bgc(0,3);gotoxy(x,y);write(messy);delay(d);
bgc(0,11);gotoxy(x,y);write(messy);delay(d);
bgc(0,15);gotoxy(x,y);write(messy);delay(d);
until (t=#13);
bgc(0,7);gotoxy(x,y);write('                             ');
end;

Procedure CommandsBar;
var z:integer;
begin
bgc(5,15);
gotoxy(1,24);
for z:=1 to 80 do write(' ');
gotoxy(14,24);
bgc(5,7);write('[');bgc(5,15);write('V');
bgc(5,7);write(']iewúRecordúDetails [');
bgc(5,15);write('E');
bgc(5,7);write(']ditúRecord [');bgc(5,15);write('D');
bgc(5,7);write(']eleteúRecord [');bgc(5,15);write('B');
bgc(5,7);write(']ackúToúMenu');bgc(0,7);
end;

Procedure CheckFiles;
var f: text; t:char;
begin
{$I-}
assign(f,'business.dat');
reset(f);
if IOResult <> 0 then begin
	clrscr;
	bgc(0,12);writeln('ş Business.Dat Not Found...SoRRy(HeHe)');
        bgc(0,15);write('ş Do yOU WaNT To CReaTe a BLaNK BuSiNeSS Book (Y/n) ?? ');
repeat
    if keypressed then t:=readkey;
  until ((t='Y') or (t='y') or (t='N') or (t='n') or (t=#13));
  case t of
 'Y','y',#13 : begin
                   assign(filebus,'Business.dat');
                   rewrite(filebus);
                   close(filebus);
               end;
  'N','n' : halt(0);
	end;
  end;
  t:='z';
assign(f,'personal.dat');
reset(f);
if IOResult <> 0 then begin
	clrscr;
	bgc(0,12);writeln('ş Personal.Dat Not Found...SoRRy(HeHe)');
        bgc(0,15);write('ş Do yOU WaNT To CReaTe a BLaNK PeRSoNaL Book (Y/n) ?? ');
repeat
    if keypressed then t:=readkey;
  until ((t='Y') or (t='y') or (t='N') or (t='n') or (t=#13));
  case t of
 'Y','y',#13 : begin
                     assign(fileperson,'Personal.dat');
                     rewrite(fileperson);
                     close(fileperson);
               end;
  'N','n' : halt(0);
	end;
  end;
{$I+}
end;

FUNCTION GetKey:Char;
{‹œ«˜«¨â§œ  ®˜¨˜¡«ã¨œª ©œ £¦¨­ã §¦¬ £§¦¨œå
  ¤˜ ›é©œ  ¡à› ¡¦çª œ › ¡é¤ §¢ã¡«¨à¤ }
Const
  Null      =#0;

VAR
   C:Char;
BEGIN
  C:=ReadKey;
  Repeat
  if C=NULL THEN
  BEGIN
    C:=ReadKey;
    if Ord(C) > 127 THEN
       C:=NULL
       else
         GetKey:=Chr(Ord(C)+128);
      END
      else
         GetKey:=C;
   until   C<>NULL;
END;


Function LockRead(x,y,len,bg,tc:integer;p:string;eidos:integer;mpara:integer):string;

var c,b:char;
    lock : boolean;
    j,k:integer;
    agreek,all,amik,ameg,ari8m,aext,atel,ap:set of char;

begin
aext:=[{HomeKey}#199,{EndKey}#207,{Up}#200,{Down}#208,{PgUp}#201,{PgDN}#209,{L-Key}#203,{Ins}#210,{R-Key}#205,{DelKey}#211];
all:=[#0..#255];agreek:=[#128..#240];
amik:=['a'..'z'];ameg:=['A'..'Z'];
ari8m:=['0'..'9'];atel:=['(',')','+','-'];
case eidos of
1: begin;
   if mpara=1 then begin
   bgc(5,15);clrxy(1,24,1,79);gotoxy(25,24);write('Characters Can Be Used:[A-Z,a-z]');
   end;
   ap:=amik+ameg-aext;
   end;
2: begin
   if mpara=1 then begin
   bgc(5,15);clrxy(1,24,1,79);gotoxy(24,24);write('Characters Can Be Used:[all keys]');
   end;
   ap:=all-agreek-aext;
   end;
3: begin
   if mpara=1 then begin
   bgc(5,15);clrxy(1,24,1,79);gotoxy(26,24);write('Characters Can Be Used:[0-9]');
   end;
   ap:=ari8m;
   end;
4: begin
   if mpara=1 then begin
   bgc(5,15);clrxy(1,24,1,79);gotoxy(22,24);write('Characters Can Be Used:[0-9,(),+,-]');
   end;
   ap:=ari8m+atel;
   end;
5: begin
   if mpara=1 then begin
   bgc(5,15);clrxy(1,24,1,79);gotoxy(23,24);write('Characters Can Be Used:[A-Z,a-z,0-9]');
   end;
   ap:=amik+ameg+ari8m-aext;
   end;
end;
curon;
usedstr:='';b:=' ';c:=' ';
lock:=false;
bgc(bg,tc);gotoxy(x,y);for j:=1 to len do write(p);
gotoxy(x,y);
repeat
  if keypressed=true then
  begin
  c:=getkey;
   if ((c in ap) or (c=#8) or (c=#32)) then begin
          if (c=#8) and (length(usedstr)<>0) then begin
             gotoxy(wherex-1,wherey);write(p);
             gotoxy(wherex-1,wherey);
             delete(usedstr,length(usedstr),1);lock:=true;
          end;

          if length(usedstr)=len then begin
             gotoxy(wherex-1,y);
             write(c);
             delete(usedstr,length(usedstr),1);
             usedstr:=usedstr+c;lock:=false;
             end;

          if (length(usedstr)<len) and (c<>#8) and (c<>#13) then begin
             write(c);
             usedstr:=usedstr+c;lock:=true;
             end;

          if (length(usedstr)=len) and (c<>#13) then b:=c;
  end;
  end;
until (c=#13);

if mpara=1 then begin
bgc(0,8);clrxy(1,24,1,79);
end;

if (lock=false) then begin
insert(b,usedstr,length(usedstr));delete(usedstr,length(usedstr),1);
end;

bgc(0,8);gotoxy(x,y);
write(usedstr);for j:=1 to (len-length(usedstr)) do write(p);
for k := 1 to Length(usedstr) do usedstr[k] := UpCase(usedstr[k]);
bgc(0,7);lockread:=usedstr;usedstr:='';
end;

Function LockRead2(x,y,len,bg,tc:integer;p:string;t:string;eidos:integer):string;

var c:char;
    lock : boolean;
    j,k:integer;
    agreek,all,amik,ameg,ari8m,aext,atel,ap:set of char;

begin
aext:=[{HomeKey}#199,{EndKey}#207,{Up}#200,{Down}#208,{PgUp}#201,{PgDN}#209,{L-Key}#203,{Ins}#210,{R-Key}#205,{DelKey}#211];
all:=[#0..#255];agreek:=[#128..#240];
amik:=['a'..'z'];ameg:=['A'..'Z'];
ari8m:=['0'..'9'];atel:=['(',')','+','-'];
case eidos of
1: begin;
   bgc(5,15);clrxy(1,24,1,79);gotoxy(25,24);write('Characters Can Be Used:[A-Z,a-z]');
   ap:=amik+ameg-aext;
   end;
2: begin
   bgc(5,15);clrxy(1,24,1,79);gotoxy(24,24);write('Characters Can Be Used:[all keys]');
   ap:=all-agreek-aext;
   end;
3: begin
   bgc(5,15);clrxy(1,24,1,79);gotoxy(26,24);write('Characters Can Be Used:[0-9]');
   ap:=ari8m;
   end;
4: begin
   bgc(5,15);clrxy(1,24,1,79);gotoxy(22,24);write('Characters Can Be Used:[0-9,(),+,-]');
   ap:=ari8m+atel;
   end;
5: begin
   bgc(5,15);clrxy(1,24,1,79);gotoxy(23,24);write('Characters Can Be Used:[A-Z,a-z,0-9]');
   ap:=amik+ameg+ari8m-aext;
   end;
end;
curon;
usedstr:=t;c:=' ';
lock:=false;
bgc(bg,tc);gotoxy(x,y);for j:=1 to len do write(p);
gotoxy(x,y);write(usedstr);
gotoxy(x+length(usedstr),y);
repeat
  if keypressed=true then
  begin
  c:=getkey;
   if ((c in ap) or (c=#8) or (c=#32)) then begin
          if (c=#8) and (length(usedstr)<>0) then begin
             gotoxy(wherex-1,wherey);write(p);
             gotoxy(wherex-1,wherey);
             delete(usedstr,length(usedstr),1);
          end;

          if (length(usedstr)<len) and (c<>#8) and (c<>#13) then begin
             write(c);
             usedstr:=usedstr+c;
             end;

  end;
  end;
until (c=#13);
bgc(0,8);gotoxy(x,y);
write(usedstr);for j:=1 to (len-length(usedstr)) do write(p);
for k := 1 to Length(usedstr) do usedstr[k] := UpCase(usedstr[k]);
bgc(0,8);clrxy(1,24,1,79);
lockread2:=usedstr;usedstr:='';
end;

Procedure DisplayScr (var Addr1,Addr2; BlkLen:Integer);
var s:string;
begin
s := 'Hey! LaMeR WHaT aRe yOU DoiNG NoW???? yOU WaNT tO CHaNGe CoPyRiGHT (c) 2002 By ÄÍşBLaCKúBiRD!şÍÄ ';
  inline (
    $1E/               {       PUSH    DS             ;Save data segment.}
    $C5/$B6/ADDR1/     {       LDS     SI,[BP+Addr1]  ;Source Address}
    $C4/$BE/ADDR2/     {       LES     DI,[BP+Addr2]  ;Destination Addr}
    $8B/$8E/BLKLEN/    {       MOV     CX,[BP+BlkLen] ;Length of block}
    $E3/$5B/           {       JCXZ    Done}
    $8B/$D7/           {       MOV     DX,DI          ;Save X coordinate for later.}
    $33/$C0/           {       XOR     AX,AX          ;Set Current attributes.}
    $FC/               {       CLD}
    $AC/               {LOOPA: LODSB                  ;Get next character.}
    $3C/$20/           {       CMP     AL,32          ;If a control character, jump.}
    $72/$05/           {       JC      ForeGround}
    $AB/               {       STOSW                  ;Save letter on screen.}
    $E2/$F8/           {Next:  LOOP    LOOPA}
    $EB/$4C/           {       JMP     Short Done}
                       {ForeGround:}
    $3C/$10/           {       CMP     AL,16          ;If less than 16, then change the}
    $73/$07/           {       JNC     BackGround     ;foreground color.  Otherwise jump.}
    $80/$E4/$F0/       {       AND     AH,0F0H        ;Strip off old foreground.}
    $0A/$E0/           {       OR      AH,AL}
    $EB/$F1/           {       JMP     Next}
                       {BackGround:}
    $3C/$18/           {       CMP     AL,24          ;If less than 24, then change the}
    $74/$13/           {       JZ      NextLine       ;background color.  If exactly 24,}
    $73/$19/           {       JNC     FlashBitToggle ;then jump down to next line.}
    $2C/$10/           {       SUB     AL,16          ;Otherwise jump to multiple output}
    $02/$C0/           {       ADD     AL,AL          ;routines.}
    $02/$C0/           {       ADD     AL,AL}
    $02/$C0/           {       ADD     AL,AL}
    $02/$C0/           {       ADD     AL,AL}
    $80/$E4/$8F/       {       AND     AH,8FH         ;Strip off old background.}
    $0A/$E0/           {       OR      AH,AL}
    $EB/$DA/           {       JMP     Next}
                       {NextLine:}
    $81/$C2/$A0/$00/   {       ADD     DX,160         ;If equal to 24,}
    $8B/$FA/           {       MOV     DI,DX          ;then jump down to}
    $EB/$D2/           {       JMP     Next           ;the next line.}
                       {FlashBitToggle:}
    $3C/$1B/           {       CMP     AL,27          ;Does user want to toggle the blink}
    $72/$07/           {       JC      MultiOutput    ;attribute?}
    $75/$CC/           {       JNZ     Next}
    $80/$F4/$80/       {       XOR     AH,128         ;Done.}
    $EB/$C7/           {       JMP     Next}
                       {MultiOutput:}
    $3C/$19/           {       CMP     AL,25          ;Set Z flag if multi-space output.}
    $8B/$D9/           {       MOV     BX,CX          ;Save main counter.}
    $AC/               {       LODSB                  ;Get count of number of times}
    $8A/$C8/           {       MOV     CL,AL          ;to display character.}
    $B0/$20/           {       MOV     AL,32}
    $74/$02/           {       JZ      StartOutput    ;Jump here if displaying spaces.}
    $AC/               {       LODSB                  ;Otherwise get character to use.}
    $4B/               {       DEC     BX             ;Adjust main counter.}
                       {StartOutput:}
    $32/$ED/           {       XOR     CH,CH}
    $41/               {       INC     CX}
    $F3/$AB/           {       REP STOSW}
    $8B/$CB/           {       MOV     CX,BX}
    $49/               {       DEC     CX             ;Adjust main counter.}
    $E0/$AA/           {       LOOPNZ  LOOPA          ;Loop if anything else to do...}
    $1F);              {Done:  POP     DS             ;Restore data segment.}
end;

Function Lightbar (ix:integer;iy:integer;column:integer;untb:integer;untc:integer;ustb:integer;ustc:integer):integer;
var x,y,z:integer;
    choice : char;
    unlock : boolean;

begin
i:=1;
z:=1;
unlock:=false;
bgc(untb,untc);
x:=ix;y:=iy;gotoxy(x,y);
repeat
z:=z+1;
gotoxy(x,wherey+1);write(arr[z]);
until (z=column);
gotoxy(x,y);bgc(ustb,ustc);write(arr[1]);
gotoxy(x,y);
repeat
  choice:='p';
  repeat
    if keypressed then choice:=readkey;
  until ((choice=#80) OR (choice=#72) or (choice=#13));
  case choice of
#80 : begin
      if (i<=column) then begin
      gotoxy(x,wherey);bgc(untb,untc);write(arr[i]);
      gotoxy(x,wherey+1);
      i:=i+1;
      bgc(ustb,ustc);write(arr[i])
      end;
      if (i>column) then begin
      gotoxy(x,wherey);bgc(untb,untc);write(arr[i]);
      gotoxy(x,y);i:=1;bgc(ustb,ustc);write(arr[i]);
      end;
           end;
#72 : begin
      if (i>1) then begin
      gotoxy(x,wherey);bgc(untb,untc);write(arr[i]);
      gotoxy(x,wherey-1);
      i:=i-1; if i=1 then unlock:=true;
      bgc(ustb,ustc);write(arr[i]);
      end;
      if (i=1) and (unlock=false)then begin
      gotoxy(x,y);bgc(untb,untc);write(arr[i]);
      gotoxy(x,wherey+column-1);i:=column;bgc(ustb,ustc);write(arr[i]);
      end;

     unlock:=false;
     end;
end;
until (choice=#13);
bgc(0,7);
LightBar:=i;
end;

Procedure ClrXY(x,y,why,spac:integer);
var n,m:integer;
begin
n:=0;m:=0;
repeat
m:=m+1;
       if m=1 then begin
       gotoxy(x,y);for n:=1 to spac do write(' ');
       end;
       if m<>1 then
       begin
       gotoxy(x,wherey+1);for n:=1 to spac do write(' ');
       end;
until m=why;
end;

Procedure ChoiceData;
begin
clrxy(52,14,7,20);
arr[1]:=' Personal Book ';
arr[2]:=' Business Book ';
arr[3]:='     Back!     ';
arr[4]:='';
lightbar(55,15,3,0,8,1,15);
end;

Procedure DisplayPerson;
begin
    gotoxy(1,wherey+1);bgc(0,10);write(filepos(fileperson),'.');
    gotoxy(5,wherey);bgc(0,11);write(perso.lname);
    gotoxy(6,wherey+1);bgc(0,3);write(perso.fname);
    gotoxy(28,wherey-1);bgc(0,11);write(perso.tel1);
    gotoxy(29,wherey+1);bgc(0,3);write(perso.cell1);
         if length(perso.cell1)=0 then begin bgc(0,9);write('[Hasn`t Cell]');bgc(0,11)end;
    gotoxy(46,wherey-1);bgc(0,11);write(perso.city);
         if length(perso.city)=0 then begin bgc(0,9);write('[No City Found]');bgc(0,11)end;
    gotoxy(47,wherey+1);bgc(0,3);write(perso.email1);
         if length(perso.email1)=0 then begin bgc(0,9);write('[No Email Found]');bgc(0,11)end;
    gotoxy(69,wherey-1);bgc(0,11);write(perso.icq);
         if length(perso.icq)=0 then begin bgc(0,9);write('[No ICQ]');bgc(0,11)end;
    gotoxy(69,wherey+1);bgc(0,3);write(perso.msn);
         if length(perso.msn)=0 then begin bgc(0,9);write('[No MSN]');bgc(0,11)end;
end;

Procedure DisplayBusin;
begin
    gotoxy(5,wherey+1);bgc(0,10);write(filepos(filebus),'.');
    gotoxy(10,wherey);bgc(0,11);write(buss.name);
    gotoxy(11,wherey+1);bgc(0,3);write(buss.typ);
    gotoxy(31,wherey-1);bgc(0,11);write(buss.tel1);
    gotoxy(32,wherey+1);bgc(0,3);write(buss.tel2);
         if length(buss.tel2)=0 then begin bgc(0,9);write('[No Telephone2]');bgc(0,11)end;
    gotoxy(50,wherey-1);bgc(0,11);write(buss.city);
         if length(buss.city)=0 then begin bgc(0,9);write('[No City Found]');bgc(0,11)end;
    gotoxy(51,wherey+1);bgc(0,3);write(buss.add1);
         if length(buss.add1)=0 then begin bgc(0,9);write('[Empty Street Address]');bgc(0,11)end;
end;


Procedure ViewPersonDetails;
var whichrec:string;
    which,bara:integer;
    t:char;
begin
gotoxy(1,24);bgc(1,15);for bara:=1 to 80 do write(' ');
gotoxy(30,24);write('View Record [   ]');
whichrec:=lockread(43,24,3,15,10,'°',3,0);curoff;

if length(whichrec)=1 then which:=ord(whichrec[1])-48;
if length(whichrec)=2 then which:=((ord(whichrec[1])-48)*10)+(ord(whichrec[2])-48);
if length(whichrec)=3 then which:=((ord(whichrec[1])-48)*100)+((ord(whichrec[2])-48)*10)+(ord(whichrec[3])-48);

if (which=0) or (which>filesize(fileperson)) then
begin
gotoxy(1,24);bgc(0,12);for which:=1 to 80 do write(' ');
gotoxy(26,24);write('ERROR!! You Must Enter 1 - ',filesize(fileperson));
waitenter(26,25,300);
end;

if (which<>0) and (which<=filesize(fileperson)) then
begin
clrscr;
seek(fileperson,which-1);
read(fileperson,perso);
DisplayScr(Pers,ScreenAddr[ (1*2) + (1*160) -162],Pers_LENGTH);
gotoxy(17,2);write(perso.lname);
gotoxy(17,3);write(perso.fname);
gotoxy(17,4);write(perso.add1);
gotoxy(17,5);write(perso.add2);
gotoxy(12,6);write(perso.city);
gotoxy(15,7);write(perso.zip);
gotoxy(16,8);write(perso.birthday.day);
gotoxy(19,8);write(perso.birthday.month);
gotoxy(22,8);write(perso.birthday.year);
gotoxy(14,9);write(perso.email1);
gotoxy(14,10);write(perso.email2);
gotoxy(20,11);write(perso.url);
gotoxy(15,12);write(perso.comment);
gotoxy(60,2);write(perso.tel1);
gotoxy(60,3);write(perso.tel2);
gotoxy(61,4);write(perso.cell1);
gotoxy(61,5);write(perso.cell2);
gotoxy(61,6);write(perso.work1);
gotoxy(61,7);write(perso.work2);
gotoxy(64,9);write(perso.icq);
gotoxy(64,10);write(perso.msn);
waitenter(26,25,300);
clrdeldown(1,1,25,100);
end;
curoff;
end;

Procedure ViewBusinessDetails;
var whichrec:string;
    which,bara:integer;
    t:char;
begin
gotoxy(1,24);bgc(1,15);for bara:=1 to 80 do write(' ');
gotoxy(30,24);write('View Record [   ]');
whichrec:=lockread(43,24,3,15,10,'°',3,0);curoff;

if length(whichrec)=1 then which:=ord(whichrec[1])-48;
if length(whichrec)=2 then which:=((ord(whichrec[1])-48)*10)+(ord(whichrec[2])-48);
if length(whichrec)=3 then which:=((ord(whichrec[1])-48)*100)+((ord(whichrec[2])-48)*10)+(ord(whichrec[3])-48);

if (which=0) or (which>filesize(filebus)) then
begin
gotoxy(1,24);bgc(0,12);for which:=1 to 80 do write(' ');
gotoxy(26,24);write('ERROR!! You Must Enter 1 - ',filesize(filebus));
waitenter(26,25,300);
end;

if (which<>0) and (which<=filesize(filebus)) then
begin
clrscr;
seek(filebus,which-1);
read(filebus,buss);
DisplayScr(Busin,ScreenAddr[ (1*2) + (1*160) -162],Busin_LENGTH);
gotoxy(21,2);write(buss.name);
gotoxy(59,2);write(buss.typ);
gotoxy(19,3);write(buss.Cont);
gotoxy(63,3);write(buss.Conttel);
gotoxy(17,4);write(buss.add1);
gotoxy(54,4);write(buss.add2);
gotoxy(12,5);write(buss.city);
gotoxy(47,5);write(buss.zip);
gotoxy(17,6);write(buss.tel1);
gotoxy(49,6);write(buss.tel2);
gotoxy(16,7);write(buss.web1);
gotoxy(52,7);write(buss.web2);
gotoxy(25,8);write(buss.webuser);
gotoxy(54,8);write(buss.webpass);
gotoxy(15,9);write(buss.comm);
waitenter(26,25,300);
clrdelup(1,1,25,100);
end;
curoff;
end;

Procedure DeletePerson;
var whichrec:string;
    g,which,bara:integer;
    fileperson2 : file of person;
begin
gotoxy(1,24);bgc(1,15);for bara:=1 to 80 do write(' ');
gotoxy(29,24);write('Delete Record [   ]');
whichrec:=lockread(44,24,3,15,10,'°',3,0);curoff;

if length(whichrec)=1 then which:=ord(whichrec[1])-48;
if length(whichrec)=2 then which:=((ord(whichrec[1])-48)*10)+(ord(whichrec[2])-48);
if length(whichrec)=3 then which:=((ord(whichrec[1])-48)*100)+((ord(whichrec[2])-48)*10)+(ord(whichrec[3])-48);

if (which=0) or (which>filesize(fileperson)) then
begin
gotoxy(1,24);bgc(0,12);for which:=1 to 80 do write(' ');
gotoxy(26,24);write('ERROR!! You Must Enter 1 - ',filesize(fileperson));
waitenter(26,25,300);
end;

if (which<>0) and (which<=filesize(fileperson)) then
begin
clrscr;
assign(fileperson2,'personal.$$$');
rewrite(fileperson2);
reset(fileperson);
if which<>1 then begin
for g:=1 to (which-1) do begin
read(fileperson,perso);
write(fileperson2,perso);
end;
end;

seek(fileperson,filepos(fileperson)+1);
while not eof(fileperson) do begin
read(fileperson,perso);
write(fileperson2,perso);
end;
close(fileperson);close(fileperson2);
erase(fileperson);rename(fileperson2,'personal.dat');
end;
end;

Procedure DeleteBusiness;
var whichrec:string;
    g,which,bara:integer;
    filebus2 : file of bus;
begin
gotoxy(1,24);bgc(1,15);for bara:=1 to 80 do write(' ');
gotoxy(29,24);write('Delete Record [   ]');
whichrec:=lockread(44,24,3,15,10,'°',3,0);curoff;

if length(whichrec)=1 then which:=ord(whichrec[1])-48;
if length(whichrec)=2 then which:=((ord(whichrec[1])-48)*10)+(ord(whichrec[2])-48);
if length(whichrec)=3 then which:=((ord(whichrec[1])-48)*100)+((ord(whichrec[2])-48)*10)+(ord(whichrec[3])-48);

if (which=0) or (which>filesize(filebus)) then
begin
gotoxy(1,24);bgc(0,12);for which:=1 to 80 do write(' ');
gotoxy(26,24);write('ERROR!! You Must Enter 1 - ',filesize(filebus));
waitenter(26,25,300);
end;

if (which<>0) and (which<=filesize(filebus)) then
begin
clrscr;
assign(filebus2,'business.$$$');
rewrite(filebus2);
reset(filebus);
if which<>1 then begin
for g:=1 to (which-1) do begin
read(filebus,buss);
write(filebus2,buss);
end;
end;

seek(filebus,filepos(filebus)+1);
while not eof(filebus) do begin
read(filebus,buss);
write(filebus2,buss);
end;
close(filebus);close(filebus2);
erase(filebus);rename(filebus2,'business.dat');
end;
end;

Procedure ViewPerson;
var i:integer;
    com:char;
    exitview:boolean;
begin
exitview:=false;
assign(fileperson,'personal.dat');
reset(fileperson);
i:=0;
clrscr;
DisplayScr(VPers,ScreenAddr[ (1*2) + (1*160) -162],VPers_LENGTH);
gotoxy(44,1);bgc(0,8);write(filesize(fileperson));
commandsbar;
gotoxy(1,4);
while not EOF(fileperson) and (exitview=false) do
begin
read(fileperson,perso);
i:=i+1;
DisplayPerson;
if (I mod 9=0) and (i <> filesize(fileperson)) then begin
   gotoxy(2,24);
   bgc(5,7);write('[');bgc(5,15);write('N');
   bgc(5,7);write(']extúPage');bgc(0,7);
   repeat
    com:=readkey;
   until (com='N') or (com='n') or (com='V') or (com='v') or (com='E') or (com='e') or (com='D') or (com='d') or (com='B') or
   (com='b');
     case com of
          'N','n' : begin
                     clrxy(1,5,18,21);clrxy(27,5,18,16);clrxy(45,5,18,21);clrxy(68,5,18,12);
                     commandsbar;
                     gotoxy(1,4);
                    end;
          'V','v' : begin
                     viewpersondetails;
                     i:=0;
                     reset(fileperson);
                     clrscr;
                     DisplayScr(VPers,ScreenAddr[ (1*2) + (1*160) -162],VPers_LENGTH);
                     gotoxy(44,1);bgc(0,8);write(filesize(fileperson));
                     commandsbar;
                     gotoxy(1,4);
                     end;
          'E','e' : begin
                     editperson;
                     i:=0;
                     reset(fileperson);
                     clrscr;
                     DisplayScr(VPers,ScreenAddr[ (1*2) + (1*160) -162],VPers_LENGTH);
                     gotoxy(44,1);bgc(0,8);write(filesize(fileperson));
                     commandsbar;
                     gotoxy(1,4);
                     end;
          'D','d'  : begin
                     deleteperson;
                     i:=0;
                     reset(fileperson);
                     clrscr;
                     DisplayScr(VPers,ScreenAddr[ (1*2) + (1*160) -162],VPers_LENGTH);
                     gotoxy(44,1);bgc(0,8);write(filesize(fileperson));
                     commandsbar;
                     gotoxy(1,4);
                     end;
          'B','b'  : begin
                     clrscr;exitview:=true
                     end;
    end;
end;

if filepos(fileperson) = (filesize(fileperson)) then begin
   repeat
    com:=readkey;
   until (com='E') or (com='e') or (com='V') or (com='v') or (com='D') or (com='d') or (com='B') or (com='b');
         case com of
              'V','v' : begin
                        viewpersondetails;
                        i:=0;
                        reset(fileperson);
                        clrscr;
                        DisplayScr(VPers,ScreenAddr[ (1*2) + (1*160) -162],VPers_LENGTH);
                        gotoxy(44,1);bgc(0,8);write(filesize(fileperson));
                        commandsbar;
                        gotoxy(1,4);
                        end;
              'E','e' : begin
                         editperson;
                         i:=0;
                         reset(fileperson);
                         clrscr;
                         DisplayScr(VPers,ScreenAddr[ (1*2) + (1*160) -162],VPers_LENGTH);
                         gotoxy(44,1);bgc(0,8);write(filesize(fileperson));
                         commandsbar;
                         gotoxy(1,4);
                        end;
          'D','d'  : begin
                     deleteperson;
                     i:=0;
                     reset(fileperson);
                     clrscr;
                     DisplayScr(VPers,ScreenAddr[ (1*2) + (1*160) -162],VPers_LENGTH);
                     gotoxy(44,1);bgc(0,8);write(filesize(fileperson));
                     commandsbar;
                     gotoxy(1,4);
                     end;
              'B','b'  : begin
                          clrscr;exitview:=true
                         end;
   end;
    end;
end;
close(fileperson);
{write('                                                                                ');
waitenter(24,24,300);}
end;

Procedure ViewBusiness;
var i:integer;
    com:char;
    exitview:boolean;
begin
exitview:=false;
assign(filebus,'business.dat');
reset(filebus);
i:=0;
clrscr;
DisplayScr(VBusin,ScreenAddr[ (1*2) + (1*160) -162],VBusin_LENGTH);
gotoxy(44,1);bgc(0,8);write(filesize(filebus));
commandsbar;
gotoxy(1,4);
while not EOF(filebus) and (exitview=false) do
begin
read(filebus,buss);
i:=i+1;
DisplayBusin;
if (I mod 9=0) and (i <> filesize(filebus)) then begin
   gotoxy(2,24);
   bgc(5,7);write('[');bgc(5,15);write('N');
   bgc(5,7);write(']extúPage');bgc(0,7);
   repeat
    com:=readkey;
   until (com='N') or (com='n') or (com='V') or (com='v') or (com='E') or (com='e') or (com='D') or (com='d') or (com='B') or
   (com='b');
     case com of
          'N','n' : begin
                     clrxy(1,5,18,28);clrxy(30,5,18,17);clrxy(49,5,18,26);
                     commandsbar;
                     gotoxy(1,4);
                    end;
          'V','v' : begin
                     ViewBusinessDetails;
                     i:=0;
                     reset(filebus);
                     clrscr;
                     DisplayScr(VBusin,ScreenAddr[ (1*2) + (1*160) -162],VBusin_LENGTH);
                     gotoxy(44,1);bgc(0,8);write(filesize(filebus));
                     commandsbar;
                     gotoxy(1,4);
                     end;
          'E','e' : begin
                     editbusiness;
                     i:=0;
                     reset(filebus);
                     clrscr;
                     DisplayScr(VBusin,ScreenAddr[ (1*2) + (1*160) -162],VBusin_LENGTH);
                     gotoxy(44,1);bgc(0,8);write(filesize(filebus));
                     commandsbar;
                     gotoxy(1,4);
                     end;
          'D','d' : begin
                     deletebusiness;
                     i:=0;
                     reset(filebus);
                     clrscr;
                     DisplayScr(VBusin,ScreenAddr[ (1*2) + (1*160) -162],VBusin_LENGTH);
                     gotoxy(44,1);bgc(0,8);write(filesize(filebus));
                     commandsbar;
                     gotoxy(1,4);
                     end;
          'B','b'  : begin
                     clrscr;exitview:=true
                     end;
    end;
end;

if filepos(filebus) = (filesize(filebus)) then begin
   repeat
    com:=readkey;
   until (com='E') or (com='e') or (com='V') or (com='v') or (com='D') or (com='d') or (com='B') or (com='b');
         case com of
              'V','v' : begin
                        ViewBusinessDetails;
                        i:=0;
                        reset(filebus);
                        clrscr;
                        DisplayScr(VBusin,ScreenAddr[ (1*2) + (1*160) -162],VBusin_LENGTH);
                        gotoxy(44,1);bgc(0,8);write(filesize(filebus));
                        commandsbar;
                        gotoxy(1,4);
                        end;
              'E','e' : begin
                         editbusiness;
                         i:=0;
                         reset(filebus);
                         clrscr;
                         DisplayScr(VBusin,ScreenAddr[ (1*2) + (1*160) -162],VBusin_LENGTH);
                         gotoxy(44,1);bgc(0,8);write(filesize(filebus));
                         commandsbar;
                         gotoxy(1,4);
                        end;
          'D','d' : begin
                     deletebusiness;
                     i:=0;
                     reset(filebus);
                     clrscr;
                     DisplayScr(VBusin,ScreenAddr[ (1*2) + (1*160) -162],VBusin_LENGTH);
                     gotoxy(44,1);bgc(0,8);write(filesize(filebus));
                     commandsbar;
                     gotoxy(1,4);
                     end;
              'B','b'  : begin
                          clrscr;exitview:=true
                         end;
   end;
    end;
end;
close(filebus);
end;


Procedure EditCatalog;
begin
choicedata;
case i of
1 : editperson;
2 : write('Business');
3 : ;
end;
end;


Procedure ViewCatalog;
begin
choicedata;
case i of
1 : viewperson;
2 : viewbusiness;
3 : ;
end;
end;

Procedure EditPerson;
var whichrec:string;
    which,bara:integer;
    t:char;
    perscheck,perscheck2,telcheck:boolean;
begin
perscheck:=false;perscheck2:=false;telcheck:=false;
gotoxy(1,24);bgc(1,15);for bara:=1 to 80 do write(' ');
gotoxy(19,24);write('Edit Record [   ] That Is Front Of Name');
whichrec:=lockread(32,24,3,15,10,'°',3,0);curoff;

if length(whichrec)=1 then which:=ord(whichrec[1])-48;
if length(whichrec)=2 then which:=((ord(whichrec[1])-48)*10)+(ord(whichrec[2])-48);
if length(whichrec)=3 then which:=((ord(whichrec[1])-48)*100)+((ord(whichrec[2])-48)*10)+(ord(whichrec[3])-48);

if (which=0) or (which>filesize(fileperson)) then
begin
gotoxy(1,24);bgc(0,12);for which:=1 to 80 do write(' ');
gotoxy(26,24);write('ERROR!! You Must Enter 1 - ',filesize(fileperson));
waitenter(26,25,300);
end;

if (which<>0) and (which<=filesize(fileperson)) then
begin
clrscr;
seek(fileperson,which-1);
read(fileperson,perso);
DisplayScr(Pers,ScreenAddr[ (1*2) + (1*160) -162],Pers_LENGTH);
gotoxy(17,2);write(perso.lname);
gotoxy(17,3);write(perso.fname);
gotoxy(17,4);write(perso.add1);
gotoxy(17,5);write(perso.add2);
gotoxy(12,6);write(perso.city);
gotoxy(15,7);write(perso.zip);
gotoxy(16,8);write(perso.birthday.day);
gotoxy(19,8);write(perso.birthday.month);
gotoxy(22,8);write(perso.birthday.year);
gotoxy(14,9);write(perso.email1);
gotoxy(14,10);write(perso.email2);
gotoxy(20,11);write(perso.url);
gotoxy(15,12);write(perso.comment);
gotoxy(60,2);write(perso.tel1);
gotoxy(60,3);write(perso.tel2);
gotoxy(61,4);write(perso.cell1);
gotoxy(61,5);write(perso.cell2);
gotoxy(61,6);write(perso.work1);
gotoxy(61,7);write(perso.work2);
gotoxy(64,9);write(perso.icq);
gotoxy(64,10);write(perso.msn);
Perso.lname:=LockRead2(17,2,20,4,15,'.',perso.lname,1);
Perso.fname:=LockRead2(17,3,20,4,15,'.',perso.fname,1);
Perso.add1:=LockRead2(17,4,20,4,15,'.',perso.add1,5);
Perso.add2:=LockRead2(17,5,20,4,15,'.',perso.add2,5);
Perso.city:=LockRead2(12,6,20,4,15,'.',perso.city,1);
Perso.zip:=LockRead2(15,7,8,4,15,'.',perso.zip,5);
Perso.birthday.day:=LockRead2(16,8,2,4,15,'.',Perso.birthday.day,3);
Perso.birthday.month:=LockRead2(19,8,2,4,15,'.',Perso.birthday.month,3);
Perso.birthday.year:=LockRead2(22,8,2,4,15,'.',perso.birthday.year,3);
Perso.email1:=LockRead2(14,9,20,4,15,'.',perso.email1,2);
Perso.email2:=LockRead2(14,10,20,4,15,'.',perso.email2,2);
Perso.url:=LockRead2(20,11,39,4,15,'.',perso.url,2);
Perso.comment:=LockRead2(15,12,62,4,15,'.',perso.comment,2);
Perso.tel1:=LockRead2(60,2,15,4,15,'.',perso.tel1,4);
Perso.tel2:=LockRead2(60,3,15,4,15,'.',perso.tel2,4);
Perso.cell1:=LockRead2(61,4,14,4,15,'.',perso.cell1,4);
Perso.cell2:=LockRead2(61,5,14,4,15,'.',perso.cell2,4);
Perso.work1:=LockRead2(61,6,15,4,15,'.',perso.work1,4);
Perso.work2:=LockRead2(61,7,15,4,15,'.',perso.work2,4);
Perso.icq:=LockRead2(64,9,11,4,15,'.',perso.icq,3);
Perso.msn:=LockRead2(64,10,11,4,15,'.',perso.msn,3);
bgc(0,3);gotoxy(33,17);write('[Y = yes / N = no]');
bgc(0,11);gotoxy(23,16);write('Do You Want To Replace This Record (y/N) : ');
repeat
    if keypressed then t:=readkey;
  until ((t='Y') or (t='y') or (t='N') or (t='n') or (t=#13));
  case t of

'Y' ,'y' : begin

        repeat
        if length(perso.lname) < 4 then begin
            clrxy(12,16,2,68);
            bgc(0,12);gotoxy(16,16);
            write('Error! :');
            bgc(0,15);write(' Last Name Must Be At Least 4 Characters ');
            Perso.lname:=LockRead(17,2,20,4,15,'.',1,1); end
                  else Perscheck:=true;
        if length(perso.fname) < 4 then begin
            clrxy(12,16,2,68);
            bgc(0,12);gotoxy(16,16);
            write('Error! :');
            bgc(0,15);write(' First Name Must Be At Least 4 Characters');
            Perso.fname:=LockRead(17,3,20,4,15,'.',1,1); end
                  else Perscheck2:=true;
        if length(perso.tel1) < 7 then begin
            clrxy(12,16,2,68);
            bgc(0,12);gotoxy(16,16);
            write('Error! :');
            bgc(0,15);write(' Telephone Must Be At Least 7 Characters');
            Perso.tel1:=LockRead(60,2,15,4,15,'.',4,1); end
                  else TelCheck:=true;

        until (PersCheck=true) and (PersCheck2=true) and (TelCheck=true);


           seek(fileperson,filepos(fileperson)-1);
           write(fileperson,perso);
   end;
'N' ,'n',#13 : ;

end;
curoff;
clrdeldown(1,1,25,100);
end;
end;

Procedure EditBusiness;
var whichrec:string;
    which,bara:integer;
    t:char;
    buscheck,buscheck2,telcheck:boolean;
begin
buscheck:=false;buscheck2:=false;telcheck:=false;
gotoxy(1,24);bgc(1,15);for bara:=1 to 80 do write(' ');
gotoxy(15,24);write('Edit Record [   ] That Is Front Of Business Name');
whichrec:=lockread(28,24,3,15,10,'°',3,0);curoff;

if length(whichrec)=1 then which:=ord(whichrec[1])-48;
if length(whichrec)=2 then which:=((ord(whichrec[1])-48)*10)+(ord(whichrec[2])-48);
if length(whichrec)=3 then which:=((ord(whichrec[1])-48)*100)+((ord(whichrec[2])-48)*10)+(ord(whichrec[3])-48);

if (which=0) or (which>filesize(filebus)) then
begin
gotoxy(1,24);bgc(0,12);for which:=1 to 80 do write(' ');
gotoxy(26,24);write('ERROR!! You Must Enter 1 - ',filesize(filebus));
waitenter(26,25,300);
end;

if (which<>0) and (which<=filesize(filebus)) then
begin
clrscr;
seek(filebus,which-1);
read(filebus,buss);
DisplayScr(Busin,ScreenAddr[ (1*2) + (1*160) -162],Busin_LENGTH);
gotoxy(21,2);write(buss.name);
gotoxy(59,2);write(buss.typ);
gotoxy(19,3);write(buss.Cont);
gotoxy(63,3);write(buss.Conttel);
gotoxy(17,4);write(buss.add1);
gotoxy(54,4);write(buss.add2);
gotoxy(12,5);write(buss.city);
gotoxy(47,5);write(buss.zip);
gotoxy(17,6);write(buss.tel1);
gotoxy(49,6);write(buss.tel2);
gotoxy(16,7);write(buss.web1);
gotoxy(52,7);write(buss.web2);
gotoxy(25,8);write(buss.webuser);
gotoxy(54,8);write(buss.webpass);
gotoxy(15,9);write(buss.comm);
buss.name:=LockRead2(21,2,17,4,15,'.',buss.name,5);
buss.typ:=LockRead2(59,2,17,4,15,'.',buss.typ,1);
buss.Cont:=LockRead2(19,3,20,4,15,'.',buss.cont,1);
buss.Conttel:=LockRead2(63,3,15,4,15,'.',buss.conttel,4);
buss.add1:=LockRead2(17,4,20,4,15,'.',buss.add1,5);
buss.add2:=LockRead2(54,4,20,4,15,'.',buss.add2,5);
buss.city:=LockRead2(12,5,20,4,15,'.',buss.city,1);
buss.zip:=LockRead2(47,5,6,4,15,'.',buss.zip,5);
buss.tel1:=LockRead2(17,6,15,4,15,'.',buss.tel1,4);
buss.tel2:=LockRead2(49,6,15,4,15,'.',buss.tel2,4);
buss.web1:=LockRead2(16,7,20,4,15,'.',buss.web1,2);
buss.web2:=LockRead2(52,7,20,4,15,'.',buss.web2,2);
buss.webuser:=LockRead2(25,8,13,4,15,'.',buss.webuser,2);
buss.webpass:=LockRead2(54,8,10,4,15,'.',buss.webpass,2);
buss.comm:=LockRead2(15,9,63,4,15,'.',buss.comm,2);
bgc(0,3);gotoxy(33,15);write('[Y = yes / N = no]');
bgc(0,11);gotoxy(23,14);write('Do You Want To Replace This Record (y/N) : ');
repeat
    if keypressed then t:=readkey;
  until ((t='Y') or (t='y') or (t='N') or (t='n') or (t=#13));
  case t of

'Y' ,'y' : begin
        repeat
        if length(buss.name) < 2 then begin
            clrxy(12,13,3,68);
            bgc(0,12);gotoxy(14,13);
            write('Error! :');
            bgc(0,15);write(' Business Name Must Be At Least 2 Characters');
            buss.name:=LockRead(21,2,17,4,15,'.',5,1); end
                  else BusCheck:=true;
        if length(buss.typ) < 4 then begin
            clrxy(12,13,3,68);
            bgc(0,12);gotoxy(14,13);
            write('Error! :');
            bgc(0,15);write(' Business Type Must Be At Least 4 Characters');
            buss.typ:=LockRead(59,2,17,4,15,'.',1,1); end
                  else BusCheck2:=true;
        if length(buss.tel1) < 7 then begin
            clrxy(12,13,3,68);
            bgc(0,12);gotoxy(14,13);
            write('Error! :');
            bgc(0,15);write(' Telephone Must Be At Least 7 Characters');
            buss.tel1:=LockRead(17,6,15,4,15,'.',4,1); end
                  else TelCheck:=true;

        until (BusCheck=true) and (BusCheck2=true) and (TelCheck=true);

           seek(filebus,filepos(filebus)-1);
           write(filebus,buss);
   end;
'N' ,'n',#13 : ;

end;
curoff;
clrdelup(1,1,25,100);
{clrdeldown(1,1,25,100);}
end;
end;

Procedure AddBuss;
var t:char;
    BusCheck,BusCheck2,TelCheck:boolean;
begin
BusCheck:=false;BusCheck2:=false;TelCheck:=false;
clrscr;
DisplayScr(Busin,ScreenAddr[ (1*2) + (1*160) -162],Busin_LENGTH);
buss.name:=LockRead(21,2,17,4,15,'.',5,1);
buss.typ:=LockRead(59,2,17,4,15,'.',1,1);
buss.Cont:=LockRead(19,3,20,4,15,'.',1,1);
buss.Conttel:=LockRead(63,3,15,4,15,'.',4,1);
buss.add1:=LockRead(17,4,20,4,15,'.',5,1);
buss.add2:=LockRead(54,4,20,4,15,'.',5,1);
buss.city:=LockRead(12,5,20,4,15,'.',1,1);
buss.zip:=LockRead(47,5,6,4,15,'.',5,1);
buss.tel1:=LockRead(17,6,15,4,15,'.',4,1);
buss.tel2:=LockRead(49,6,15,4,15,'.',4,1);
buss.web1:=LockRead(16,7,20,4,15,'.',2,1);
buss.web2:=LockRead(52,7,20,4,15,'.',2,1);
buss.webuser:=LockRead(25,8,13,4,15,'.',2,1);
buss.webpass:=LockRead(54,8,10,4,15,'.',2,1);
buss.comm:=LockRead(15,9,63,4,15,'.',2,1);
bgc(0,3);gotoxy(12,14); write('[Y = yes / N = no / A = save this record & add new business]');
bgc(0,11);gotoxy(21,13);write('Do You Want To Save This Record (Y/n/a) : ');
repeat
    if keypressed then t:=readkey;
  until ((t='Y') OR (t='y') or (t='N') or (t='n') or (t='A') or (t='a') or (t=#13));
  case t of
'Y' ,'y',#13,'A','a' : begin
        repeat
        if length(buss.name) < 2 then begin
            clrxy(12,13,2,68);
            bgc(0,12);gotoxy(14,13);
            write('Error! :');
            bgc(0,15);write(' Business Name Must Be At Least 2 Characters');
            buss.name:=LockRead(21,2,17,4,15,'.',5,1); end
                  else BusCheck:=true;
        if length(buss.typ) < 4 then begin
            clrxy(12,13,2,68);
            bgc(0,12);gotoxy(14,13);
            write('Error! :');
            bgc(0,15);write(' Business Type Must Be At Least 4 Characters');
            buss.typ:=LockRead(59,2,17,4,15,'.',1,1); end
                  else BusCheck2:=true;
        if length(buss.tel1) < 7 then begin
            clrxy(12,13,2,68);
            bgc(0,12);gotoxy(14,13);
            write('Error! :');
            bgc(0,15);write(' Telephone Must Be At Least 7 Characters');
            buss.tel1:=LockRead(17,6,15,4,15,'.',4,1); end
                  else TelCheck:=true;


        until (BusCheck=true) and (BusCheck2=true) and (TelCheck=true);

           assign(filebus,'business.dat');
           reset(filebus);
           seek(filebus,filesize(filebus));
           write(filebus,buss);
           close(filebus);

        if (t='A') or (t='a') then begin
             curoff;
             clrdelup(1,1,25,100);
             addbuss;
             end;
        end;
'N' ,'n' : ;

end;
curoff;
clrdelup(1,1,25,100);
end;

Procedure AddPers;
var t:char;
    k,birtd,birtm,birty:string;
    h:integer;
    PersCheck,PersCheck2,TelCheck:boolean;
begin
PersCheck:=false;PersCheck2:=false;TelCheck:=false;
Perso.lname:=' ';Perso.fname:=' ';Perso.add1:=' ';
Perso.add2:=' ';Perso.city:=' ';Perso.zip:=' ';
Perso.BirthDay.Day:=' ';Perso.BirthDay.Month:=' ';Perso.BirthDay.Year:=' ';
Perso.email1:=' ';Perso.email2:=' ';Perso.url:=' ';
Perso.comment:=' ';Perso.tel1:=' ';Perso.tel2:=' ';
Perso.cell1:=' ';Perso.cell2:=' ';Perso.work1:=' ';
Perso.work2:=' ';Perso.icq:=' ';Perso.msn:=' ';
clrscr;
DisplayScr(Pers,ScreenAddr[ (1*2) + (1*160) -162],Pers_LENGTH);
Perso.lname:=LockRead(17,2,20,4,15,'.',1,1);{gotoxy(13,13);write(perso.lname,' ',length(perso.lname));}
Perso.fname:=LockRead(17,3,20,4,15,'.',1,1);
Perso.add1:=LockRead(17,4,20,4,15,'.',5,1);
Perso.add2:=LockRead(17,5,20,4,15,'.',5,1);
Perso.city:=LockRead(12,6,20,4,15,'.',1,1);
Perso.zip:=LockRead(15,7,8,4,15,'.',5,1);
Perso.BirthDay.Day:=LockRead(16,8,2,4,15,'.',3,1);
Perso.BirthDay.Month:=LockRead(19,8,2,4,15,'.',3,1);
Perso.BirthDay.Year:=LockRead(22,8,2,4,15,'.',3,1);
Perso.email1:=LockRead(14,9,20,4,15,'.',2,1);
Perso.email2:=LockRead(14,10,20,4,15,'.',2,1);
Perso.url:=LockRead(20,11,39,4,15,'.',2,1);
Perso.comment:=LockRead(15,12,62,4,15,'.',2,1);
Perso.tel1:=LockRead(60,2,15,4,15,'.',4,1);
Perso.tel2:=LockRead(60,3,15,4,15,'.',4,1);
Perso.cell1:=LockRead(61,4,14,4,15,'.',4,1);
Perso.cell2:=LockRead(61,5,14,4,15,'.',4,1);
Perso.work1:=LockRead(61,6,15,4,15,'.',4,1);
Perso.work2:=LockRead(61,7,15,4,15,'.',4,1);
Perso.icq:=LockRead(64,9,11,4,15,'.',3,1);
Perso.msn:=LockRead(64,10,11,4,15,'.',3,1);
bgc(0,3);gotoxy(12,17); write('[Y = yes / N = no / A = save this record & add new personal]');
bgc(0,11);gotoxy(21,16);write('Do You Want To Save This Record (Y/n/a) : ');
repeat
    if keypressed then t:=readkey;
  until ((t='Y') or (t='y') or (t='N') or (t='n') or (t='A') or (t='a') or (t=#13));
  case t of

'Y' ,'y',#13,'A','a' : begin

        repeat
        if length(perso.lname) < 4 then begin
            clrxy(12,16,2,68);
            bgc(0,12);gotoxy(16,16);
            write('Error! :');
            bgc(0,15);write(' Last Name Must Be At Least 4 Characters ');
            Perso.lname:=LockRead(17,2,20,4,15,'.',1,1); end
                  else Perscheck:=true;
        if length(perso.fname) < 4 then begin
            clrxy(12,16,2,68);
            bgc(0,12);gotoxy(16,16);
            write('Error! :');
            bgc(0,15);write(' First Name Must Be At Least 4 Characters');
            Perso.fname:=LockRead(17,3,20,4,15,'.',1,1); end
                  else Perscheck2:=true;
        if length(perso.tel1) < 7 then begin
            clrxy(12,16,2,68);
            bgc(0,12);gotoxy(16,16);
            write('Error! :');
            bgc(0,15);write(' Telephone Must Be At Least 7 Characters');
            Perso.tel1:=LockRead(60,2,15,4,15,'.',4,1); end
                  else TelCheck:=true;


        until (PersCheck=true) and (PersCheck2=true) and (TelCheck=true);


           assign(fileperson,'personal.dat');
           reset(fileperson);
           seek(fileperson,filesize(fileperson));
           write(fileperson,perso);
           close(fileperson);
           if (t='a') or (t='A') then begin
                             curoff;
                             clrdeldown(1,1,25,100);
                             addpers;
           end;
   end;
'N' ,'n' : ;

end;
curoff;
clrdeldown(1,1,25,100);
end;

Procedure AddCatalog;
begin
choicedata;
case i of
1 : addpers;
2 : addbuss;
3 : ;
end;
end;

Procedure SearchPers;
var o2,o3,searchstring:string[20];
    found,exitview,fuck : boolean;
    o,k:integer;
    com:char;
begin
exitview:=false;fuck:=false;found:=false;
clrxy(52,13,6,20);
arr[1]:='   Search By Lname   ';
arr[2]:='   Search By Fname   ';
arr[3]:=' Search By Telephone ';
arr[4]:='   Search By Cell    ';
arr[5]:='        Back         ';
arr[6]:='';
lightbar(52,14,5,0,8,1,15);
case i of
1 : begin
    k:=0;
    clrscr;
    DisplayScr(VPers,ScreenAddr[ (1*2) + (1*160) -162],VPers_LENGTH);
    bgc(0,8);gotoxy(18,1);write('Search Last Names For [                    ]');
    searchstring:=LockRead(41,1,20,4,15,'.',1,0);curoff;
    o:=length(searchstring);
    assign(fileperson,'personal.dat');
    reset(fileperson);
    commandsbar;
    found:=false;
    gotoxy(1,4);
    while not (EOF(fileperson)) and (exitview=false) do
    begin
    read(fileperson,perso);
    if filepos(fileperson)=Filesize(fileperson) then fuck:=true;
    o2:=perso.lname;
    o2[0]:=chr(ord(o));
    if (searchstring=o2) and (searchstring[0]<>chr(ord(0))) then
    begin
    k:=k+1;
    DisplayPerson;
    found:=true;
    if (K mod 9=0) and (K <> filesize(fileperson)) then begin
   gotoxy(2,24);
   bgc(5,7);write('[');bgc(5,15);write('N');
   bgc(5,7);write(']extúPage');bgc(0,7);
   repeat
    com:=readkey;
   until (com='N') or (com='n') or (com='V') or (com='v') or (com='E') or (com='e') or (com='D') or (com='d') or (com='B') or
   (com='b');
     case com of
          'N','n' : begin
                     clrxy(1,5,18,21);clrxy(27,5,18,16);clrxy(45,5,18,21);clrxy(68,5,18,12);
                     commandsbar;
                     gotoxy(1,4);
                    end;
          'V','v' : begin
                     viewpersondetails;
                     k:=0;
                     reset(fileperson);
                     clrscr;
                     DisplayScr(VPers,ScreenAddr[ (1*2) + (1*160) -162],VPers_LENGTH);
                     bgc(0,8);gotoxy(29,1);write('ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ');
                     commandsbar;
                     gotoxy(1,4);
                     end;
          'E','e' : begin
                     editperson;
                     k:=0;
                     reset(fileperson);
                     clrscr;
                     DisplayScr(VPers,ScreenAddr[ (1*2) + (1*160) -162],VPers_LENGTH);
                     bgc(0,8);gotoxy(29,1);write('ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ');
                     commandsbar;
                     gotoxy(1,4);
                     end;
          'D','d' : begin
                     deleteperson;
                     k:=0;
                     reset(fileperson);
                     clrscr;
                     DisplayScr(VPers,ScreenAddr[ (1*2) + (1*160) -162],VPers_LENGTH);
                     bgc(0,8);gotoxy(29,1);write('ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ');
                     commandsbar;
                     gotoxy(1,4);
                    end;
          'B','b'  : begin
                     clrscr;exitview:=true
                     end;

          end;
          end;
          end;

     end;
    end;

2: begin
    k:=0;
    clrscr;
    DisplayScr(VPers,ScreenAddr[ (1*2) + (1*160) -162],VPers_LENGTH);
    bgc(0,8);gotoxy(18,1);write('Search First Names For [                    ]');
    searchstring:=LockRead(42,1,20,4,15,'.',1,0);curoff;
    o:=length(searchstring);
    assign(fileperson,'personal.dat');
    reset(fileperson);
    commandsbar;
    found:=false;
    gotoxy(1,4);
    while not (EOF(fileperson)) and (exitview=false) do
    begin
    read(fileperson,perso);
    if filepos(fileperson)=Filesize(fileperson) then fuck:=true;
    o2:=perso.fname;
    o2[0]:=chr(ord(o));
    if (searchstring=o2) and (searchstring[0]<>chr(ord(0))) then
    begin
    k:=k+1;
    DisplayPerson;
    found:=true;

   if (K mod 9=0) and (K <> filesize(fileperson)) then begin
   gotoxy(2,24);
   bgc(5,7);write('[');bgc(5,15);write('N');
   bgc(5,7);write(']extúPage');bgc(0,7);
   repeat
    com:=readkey;
   until (com='N') or (com='n') or (com='V') or (com='v') or (com='E') or (com='e') or (com='D') or (com='d') or (com='B') or
   (com='b');
     case com of
          'N','n' : begin
                     clrxy(1,5,18,21);clrxy(27,5,18,16);clrxy(45,5,18,21);clrxy(68,5,18,12);
                     commandsbar;
                     gotoxy(1,4);
                    end;
          'V','v' : begin
                     viewpersondetails;
                     k:=0;
                     reset(fileperson);
                     clrscr;
                     DisplayScr(VPers,ScreenAddr[ (1*2) + (1*160) -162],VPers_LENGTH);
                     bgc(0,8);gotoxy(29,1);write('ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ');
                     commandsbar;
                     gotoxy(1,4);
                     end;
          'E','e' : begin
                     editperson;
                     k:=0;
                     reset(fileperson);
                     clrscr;
                     DisplayScr(VPers,ScreenAddr[ (1*2) + (1*160) -162],VPers_LENGTH);
                     bgc(0,8);gotoxy(29,1);write('ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ');
                     commandsbar;
                     gotoxy(1,4);
                     end;
          'D','d' : begin
                     deleteperson;
                     k:=0;
                     reset(fileperson);
                     clrscr;
                     DisplayScr(VPers,ScreenAddr[ (1*2) + (1*160) -162],VPers_LENGTH);
                     bgc(0,8);gotoxy(29,1);write('ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ');
                     commandsbar;
                     gotoxy(1,4);
                    end;
          'B','b'  : begin
                     clrscr;exitview:=true
                     end;

          end;
          end;
          end;

      end;
    end;

3: begin
    k:=0;
    clrscr;
    DisplayScr(VPers,ScreenAddr[ (1*2) + (1*160) -162],VPers_LENGTH);
    bgc(0,8);gotoxy(18,1);write('Search Telephones(1 & 2) For [               ]');
    searchstring:=LockRead(48,1,15,4,15,'.',4,0);curoff;
    o:=length(searchstring);
    assign(fileperson,'personal.dat');
    reset(fileperson);
    commandsbar;
    found:=false;
    gotoxy(1,4);
    while not (EOF(fileperson)) and (exitview=false) do
    begin
    read(fileperson,perso);
    if filepos(fileperson)=Filesize(fileperson) then fuck:=true;
    o2:=perso.tel1;
    o3:=perso.tel2;
    o2[0]:=chr(ord(o));
    o3[0]:=chr(ord(o));
    if ((searchstring=o3) or (searchstring=o2)) and (searchstring[0]<>chr(ord(0))) then
    begin
    k:=k+1;
    DisplayPerson;
    found:=true;
    o2:=' #';o3:='#';perso.tel1:='#';perso.tel2:='#';
   if (K mod 9=0) and (K <> filesize(fileperson)) then begin
   gotoxy(2,24);
   bgc(5,7);write('[');bgc(5,15);write('N');
   bgc(5,7);write(']extúPage');bgc(0,7);
   repeat
    com:=readkey;
   until (com='N') or (com='n') or (com='V') or (com='v') or (com='E') or (com='e') or (com='D') or (com='d') or (com='B') or
   (com='b');
     case com of
          'N','n' : begin
                     clrxy(1,5,18,21);clrxy(27,5,18,16);clrxy(45,5,18,21);clrxy(68,5,18,12);
                     commandsbar;
                     gotoxy(1,4);
                    end;
          'V','v' : begin
                     viewpersondetails;
                     k:=0;
                     reset(fileperson);
                     clrscr;
                     DisplayScr(VPers,ScreenAddr[ (1*2) + (1*160) -162],VPers_LENGTH);
                     bgc(0,8);gotoxy(29,1);write('ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ');
                     commandsbar;
                     gotoxy(1,4);
                     end;
          'E','e' : begin
                     editperson;
                     k:=0;
                     reset(fileperson);
                     clrscr;
                     DisplayScr(VPers,ScreenAddr[ (1*2) + (1*160) -162],VPers_LENGTH);
                     bgc(0,8);gotoxy(29,1);write('ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ');
                     commandsbar;
                     gotoxy(1,4);
                     end;
          'D','d' : begin
                     deleteperson;
                     k:=0;
                     reset(fileperson);
                     clrscr;
                     DisplayScr(VPers,ScreenAddr[ (1*2) + (1*160) -162],VPers_LENGTH);
                     bgc(0,8);gotoxy(29,1);write('ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ');
                     commandsbar;
                     gotoxy(1,4);
                    end;
          'B','b'  : begin
                     clrscr;exitview:=true
                     end;

          end;
          end;
          end;

      end;
    end;
4: begin
    k:=0;
    clrscr;
    DisplayScr(VPers,ScreenAddr[ (1*2) + (1*160) -162],VPers_LENGTH);
    bgc(0,8);gotoxy(18,1);write('Search CellPhones(1 & 2) For [              ]');
    searchstring:=LockRead(48,1,14,4,15,'.',4,0);curoff;
    o:=length(searchstring);
    assign(fileperson,'personal.dat');
    reset(fileperson);
    commandsbar;
    found:=false;
    gotoxy(1,4);
    while not (EOF(fileperson))  and (exitview=false) do
    begin
    read(fileperson,perso);
    if filepos(fileperson)=Filesize(fileperson) then fuck:=true;
    o2:=perso.cell1;
    o3:=perso.cell2;
    o2[0]:=chr(ord(o));
    o3[0]:=chr(ord(o));
    if ((searchstring=o3) or (searchstring=o2)) and (searchstring[0]<>chr(ord(0))) then
    begin
    k:=k+1;
    DisplayPerson;
    found:=true;
    o2:='#';o3:='#';perso.tel1:='#';perso.tel2:='#';
   if (K mod 9=0) and (K <> filesize(fileperson)) then begin
   gotoxy(2,24);
   bgc(5,7);write('[');bgc(5,15);write('N');
   bgc(5,7);write(']extúPage');bgc(0,7);
   repeat
    com:=readkey;
   until (com='N') or (com='n') or (com='V') or (com='v') or (com='E') or (com='e') or (com='D') or (com='d') or (com='B') or
   (com='b');
     case com of
          'N','n' : begin
                     clrxy(1,5,18,21);clrxy(27,5,18,16);clrxy(45,5,18,21);clrxy(68,5,18,12);
                     commandsbar;
                     gotoxy(1,4);
                    end;
          'V','v' : begin
                     viewpersondetails;
                     k:=0;
                     reset(fileperson);
                     clrscr;
                     DisplayScr(VPers,ScreenAddr[ (1*2) + (1*160) -162],VPers_LENGTH);
                     bgc(0,8);gotoxy(29,1);write('ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ');
                     commandsbar;
                     gotoxy(1,4);
                     end;
          'E','e' : begin
                     editperson;
                     k:=0;
                     reset(fileperson);
                     clrscr;
                     DisplayScr(VPers,ScreenAddr[ (1*2) + (1*160) -162],VPers_LENGTH);
                     bgc(0,8);gotoxy(29,1);write('ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ');
                     commandsbar;
                     gotoxy(1,4);
                     end;
          'D','d' : begin
                     deleteperson;
                     k:=0;
                     reset(fileperson);
                     clrscr;
                     DisplayScr(VPers,ScreenAddr[ (1*2) + (1*160) -162],VPers_LENGTH);
                     bgc(0,8);gotoxy(29,1);write('ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ');
                     commandsbar;
                     gotoxy(1,4);
                    end;
          'B','b'  : begin
                     clrscr;exitview:=true
                     end;

          end;
          end;
          end;

      end;
    end;

5 :;
end;
if (fuck=true) and (found=false) then begin
clrxy(1,24,1,80);
bgc(0,8);gotoxy(16,1);write('ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ');
bgc(0,12);write('[Nothing Found]');
bgc(0,8);write('ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ');waitenter(26,24,300);
end;

if (fuck=true) and (found=true) then begin

   repeat
    com:=readkey;
   until (com='E') or (com='e') or (com='V') or (com='v') or (com='D') or (com='d') or (com='B') or (com='b');
         case com of
              'V','v' : begin
                         viewpersondetails;
                         clrscr;
                        end;
              'E','e' : begin
                         editperson;
                         clrscr;
                        end;
              'D','d' : begin
                         deleteperson;
                         clrscr;
                        end;
              'B','b'  : begin
                         end;

              end;

    end;
end;

Procedure SearchBusin;
var o2,o3,searchstring:string[20];
    found,exitview,fuck : boolean;
    o,k:integer;
    com:char;
begin
exitview:=false;fuck:=false;found:=false;
clrxy(52,13,6,20);
arr[1]:='   Search By Name   ';
arr[2]:='   Search By Type   ';
arr[3]:=' Search By Telephone ';
arr[4]:='        Back         ';
arr[5]:='';
arr[6]:='';
lightbar(52,14,4,0,8,1,15);
case i of
1 : begin
    k:=0;
    clrscr;
    DisplayScr(VBusin,ScreenAddr[ (1*2) + (1*160) -162],VBusin_LENGTH);
    bgc(0,8);gotoxy(16,1);write('Search Business Names For [                 ]');
    searchstring:=LockRead(43,1,17,4,15,'.',5,0);curoff;
    o:=length(searchstring);
    assign(filebus,'business.dat');
    reset(filebus);
    commandsbar;
    found:=false;
    gotoxy(1,4);
    while not (EOF(filebus)) and (exitview=false) do
    begin
    read(filebus,buss);
    if filepos(filebus)=Filesize(filebus) then fuck:=true;
    o2:=buss.name;
    o2[0]:=chr(ord(o));
    if (searchstring=o2) and (searchstring[0]<>chr(ord(0))) then
    begin
    k:=k+1;
    DisplayBusin;
    found:=true;
    if (K mod 9=0) and (K <> filesize(filebus)) then begin
    gotoxy(2,24);
    bgc(5,7);write('[');bgc(5,15);write('N');
    bgc(5,7);write(']extúPage');bgc(0,7);
    repeat
     com:=readkey;
    until (com='N') or (com='n') or (com='V') or (com='v') or (com='E') or (com='e') or (com='D') or (com='d') or (com='B') or
    (com='b');
     case com of
          'N','n' : begin
                     clrxy(1,5,18,28);clrxy(30,5,18,17);clrxy(49,5,18,26);
                     commandsbar;
                     gotoxy(1,4);
                    end;
          'V','v' : begin
                     viewbusinessdetails;
                     k:=0;
                     reset(filebus);
                     clrscr;
                     DisplayScr(VBusin,ScreenAddr[ (1*2) + (1*160) -162],VBusin_LENGTH);
                     bgc(0,8);gotoxy(29,1);write('ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ');
                     commandsbar;
                     gotoxy(1,4);
                     end;
          'E','e' : begin
                     editbusiness;
                     k:=0;
                     reset(filebus);
                     clrscr;
                     DisplayScr(VBusin,ScreenAddr[ (1*2) + (1*160) -162],VBusin_LENGTH);
                     bgc(0,8);gotoxy(29,1);write('ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ');
                     commandsbar;
                     gotoxy(1,4);
                     end;
          'D','d' : begin
                     deletebusiness;
                     k:=0;
                     reset(filebus);
                     clrscr;
                     DisplayScr(VBusin,ScreenAddr[ (1*2) + (1*160) -162],VBusin_LENGTH);
                     bgc(0,8);gotoxy(29,1);write('ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ');
                     commandsbar;
                     gotoxy(1,4);
                    end;
          'B','b'  : begin
                     clrscr;exitview:=true
                     end;

          end;
          end;
          end;

     end;
    end;

2: begin
    k:=0;
    clrscr;
    DisplayScr(VBusin,ScreenAddr[ (1*2) + (1*160) -162],VBusin_LENGTH);
    bgc(0,8);gotoxy(16,1);write('Search Business Types For [                 ]');
    searchstring:=LockRead(43,1,17,4,15,'.',1,0);curoff;
    o:=length(searchstring);
    assign(filebus,'business.dat');
    reset(filebus);
    commandsbar;
    found:=false;
    gotoxy(1,4);
    while not (EOF(filebus)) and (exitview=false) do
    begin
    read(filebus,buss);
    if filepos(filebus)=Filesize(filebus) then fuck:=true;
    o2:=buss.typ;
    o2[0]:=chr(ord(o));
    if (searchstring=o2) and (searchstring[0]<>chr(ord(0))) then
    begin
    k:=k+1;
    DisplayBusin;
    found:=true;
    if (K mod 9=0) and (K <> filesize(filebus)) then begin
    gotoxy(2,24);
    bgc(5,7);write('[');bgc(5,15);write('N');
    bgc(5,7);write(']extúPage');bgc(0,7);
    repeat
     com:=readkey;
    until (com='N') or (com='n') or (com='V') or (com='v') or (com='E') or (com='e') or (com='D') or (com='d') or (com='B') or
    (com='b');
     case com of
          'N','n' : begin
                     clrxy(1,5,18,28);clrxy(30,5,18,17);clrxy(49,5,18,26);
                     commandsbar;
                     gotoxy(1,4);
                    end;
          'V','v' : begin
                     viewbusinessdetails;
                     k:=0;
                     reset(filebus);
                     clrscr;
                     DisplayScr(VBusin,ScreenAddr[ (1*2) + (1*160) -162],VBusin_LENGTH);
                     bgc(0,8);gotoxy(29,1);write('ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ');
                     commandsbar;
                     gotoxy(1,4);
                     end;
          'E','e' : begin
                     editbusiness;
                     k:=0;
                     reset(filebus);
                     clrscr;
                     DisplayScr(VBusin,ScreenAddr[ (1*2) + (1*160) -162],VBusin_LENGTH);
                     bgc(0,8);gotoxy(29,1);write('ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ');
                     commandsbar;
                     gotoxy(1,4);
                     end;
          'D','d' : begin
                     deletebusiness;
                     k:=0;
                     reset(filebus);
                     clrscr;
                     DisplayScr(VBusin,ScreenAddr[ (1*2) + (1*160) -162],VBusin_LENGTH);
                     bgc(0,8);gotoxy(29,1);write('ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ');
                     commandsbar;
                     gotoxy(1,4);
                    end;
          'B','b'  : begin
                     clrscr;exitview:=true
                     end;

          end;
          end;
          end;

     end;
    end;

3: begin
    k:=0;
    clrscr;
    DisplayScr(VBusin,ScreenAddr[ (1*2) + (1*160) -162],VBusin_LENGTH);
    bgc(0,8);gotoxy(18,1);write('Search Telephones(1 & 2) For [               ]');
    searchstring:=LockRead(48,1,15,4,15,'.',4,0);
    o:=length(searchstring);
    assign(filebus,'business.dat');
    reset(filebus);
    commandsbar;
    found:=false;
    gotoxy(1,4);
    while not (EOF(filebus))  and (exitview=false) do
    begin
    read(filebus,buss);
    if filepos(filebus)=Filesize(filebus) then fuck:=true;
    o2:=buss.tel1;
    o3:=buss.tel2;
    o2[0]:=chr(ord(o));
    o3[0]:=chr(ord(o));
    if ((searchstring=o3) or (searchstring=o2)) and (searchstring[0]<>chr(ord(0))) then
    begin
    k:=k+1;
    DisplayBusin;
    found:=true;
    o2:='#';o3:='#';buss.tel1:='#';buss.tel2:='#';
    if (K mod 9=0) and (K <> filesize(filebus)) then begin
    gotoxy(2,24);
    bgc(5,7);write('[');bgc(5,15);write('N');
    bgc(5,7);write(']extúPage');bgc(0,7);
    repeat
     com:=readkey;
    until (com='N') or (com='n') or (com='V') or (com='v') or (com='E') or (com='e') or (com='D') or (com='d') or (com='B') or
    (com='b');
     case com of
          'N','n' : begin
                     clrxy(1,5,18,28);clrxy(30,5,18,17);clrxy(49,5,18,26);
                     commandsbar;
                     gotoxy(1,4);
                    end;
          'V','v' : begin
                     viewbusinessdetails;
                     k:=0;
                     reset(filebus);
                     clrscr;
                     DisplayScr(VBusin,ScreenAddr[ (1*2) + (1*160) -162],VBusin_LENGTH);
                     bgc(0,8);gotoxy(29,1);write('ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ');
                     commandsbar;
                     gotoxy(1,4);
                     end;
          'E','e' : begin
                     editbusiness;
                     k:=0;
                     reset(filebus);
                     clrscr;
                     DisplayScr(VBusin,ScreenAddr[ (1*2) + (1*160) -162],VBusin_LENGTH);
                     bgc(0,8);gotoxy(29,1);write('ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ');
                     commandsbar;
                     gotoxy(1,4);
                     end;
          'D','d' : begin
                     deletebusiness;
                     k:=0;
                     reset(filebus);
                     clrscr;
                     DisplayScr(VBusin,ScreenAddr[ (1*2) + (1*160) -162],VBusin_LENGTH);
                     bgc(0,8);gotoxy(29,1);write('ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ');
                     commandsbar;
                     gotoxy(1,4);
                    end;
          'B','b'  : begin
                     clrscr;exitview:=true
                     end;

          end;
          end;
          end;

     end;
    end;

4 :;
end;
if (fuck=true) and (found=false) then begin
clrxy(1,24,1,80);
bgc(0,8);gotoxy(16,1);write('ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ');
bgc(0,12);write('[Nothing Found]');
bgc(0,8);write('ÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄÄ');waitenter(26,24,300);
end;

if (fuck=true) and (found=true) then begin

   repeat
    com:=readkey;
   until (com='E') or (com='e') or (com='V') or (com='v') or (com='D') or (com='d') or (com='B') or (com='b');
         case com of
              'V','v' : begin
                         viewbusinessdetails;
                         clrscr;
                        end;
              'E','e' : begin
                         editbusiness;
                         clrscr;
                        end;
              'D','d' : begin
                         deletebusiness;
                         clrscr;
                        end;
              'B','b'  : begin
                         end;

              end;

    end;
end;


Procedure SearchCatalog;
begin
choicedata;
case i of
1 : SearchPers;
2 : SearchBusin;
3 : ;
end;
end;

Procedure NewBus;
var t:char;
begin
gotoxy(10,24);bgc(0,28);write('Warning! You Are Going To Delete All Business Phones');
clrxy(52,13,6,20);
bgc(0,12);gotoxy(59,14);write('Warning!');
gotoxy(56,15);write('Continue (y/N)');
t:=readkey;
if (t='Y') or (t='y') then begin
assign(filebus,'Business.dat');
rewrite(filebus);
close(filebus);
end;
end;

Procedure NewPers;
var t:char;
begin
gotoxy(10,24);bgc(0,28);write('Warning! You Are Going To Delete All Personal Phones');
clrxy(52,13,6,20);
bgc(0,12);gotoxy(59,14);write('Warning!');
gotoxy(56,15);write('Continue (y/N)');
t:=readkey;
if (t='Y') or (t='y') then begin
assign(fileperson,'Personal.dat');
rewrite(fileperson);
close(fileperson);
end;
end;



Procedure CreateNewCatalog;
begin
gotoxy(10,24);bgc(0,28);write('Warning! You Are Going To Delete Your Phones');
choicedata;
case i of
1 : newpers;
2 : newbus;
3 : ;
end;
{gotoxy(10,24);write('                                              ');}
end;

Procedure MainMenu;
var a:char;
begin
arr[1]:='    View Catalog    ';
arr[2]:='   Add To Catalog   ';
arr[3]:='   Search Catalog   ';
arr[4]:=' Useful Phones 2002 ';
arr[5]:='   Export as HTML   ';
arr[6]:=' Create New Catalog ';
arr[7]:='        Exit        ';
lightbar(52,14,7,0,8,1,15);
case i of
1 : ViewCatalog;
2 : AddCatalog;
3 : SearchCatalog;
4 : Begin
    clrscr;
    DisplayScr(Help,ScreenAddr[ (1*2) + (1*160) -162],Help_LENGTH);
    waitenter(26,25,300);
    clrdeldown(1,1,25,100);
    end;
5 : Begin
    ExportHTMLPers;
    ExportHTMLBusin;
    clrxy(52,14,7,20);
    repeat
    if (keypressed=true) then a:=readkey;
    bgc(0,3);gotoxy(52,16);write('Personal.HTM Exported');
             gotoxy(52,wherey+1);write('Business.HTM Exported');delay(300);
    bgc(0,11);gotoxy(52,16);write('Personal.HTM Exported');
              gotoxy(52,wherey+1);write('Business.HTM Exported');delay(300);
    bgc(0,15);gotoxy(52,16);write('Personal.HTM Exported');
              gotoxy(52,wherey+1);write('Business.HTM Exported');delay(300);
    bgc(0,13);gotoxy(51,20);write('Press Enter To Continue');
    until (a=#13);
    end;
6 : CreateNewCatalog;
7 : begin
    gotoxy(1,25);
    clrscreen;
    ext:=true;
    end;
end;
end;

{Procedure mybreak;  { Terminate Ctrl + C
interrupt;
begin;
end;}

begin
{SetIntVec($1B,@MyBreak);     { Terminate Ctrl + C}
checkfiles;
curoff;
clrscr;
ext:=false;
repeat
clrscr;
curoff;
DisplayScr(MAIN,ScreenAddr[ (1*2) + (1*160) -162],main_LENGTH);
mainmenu;
until ext=true;
clrscr;
bgc(0,15);writeln('ş TelePhoneBook v1.oO By Black Bird (c) 2002');
bgc(0,9);write('ş Return To OS');
end.