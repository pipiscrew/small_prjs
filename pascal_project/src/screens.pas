Unit Screens;

INTERFACE

const
  HELP_WIDTH=80;
  HELP_DEPTH=25;
  HELP_LENGTH=1214;
  HELP : array [1..1214] of Char = (
     #8,#16,#26,#11,'�','�',#10,'�','�','�','�','�',' ','�','�','�','�',
    '�', #8,'�',#26,#12,'�',#25, #3,#26, #9,'�','�',#10,'�','�','�','�',
    '�','�',' ','�','�','�','�','�','�','�','�', #8,'�',#26,#10,'�',#24,
    ' ',#15,'�', #3,'�','�','�','�',' ',#15,'�', #3,'�','�','�','�',' ',
    #15,'�', #3,'�','�','�','�','�','�','�','�','�',#25,#10,#11,'1','0',
    '0',#25, #5,#15,'�', #3,'�','�','�','�','�','�','�','�','�',' ',#15,
    '�', #3,'�','�','�',#25, #9,#11,'0','1','0','-','6','0','3','8','0',
    '2','3',#24,' ',#15,'�', #3,'�','�','�','�',' ',#15,'�', #3,'�','�',
    '�','�',' ',#15,'�', #3,'�','�','�','�','�','�','�','�',#25,#11,#11,
    '1','0','8',#25, #5,#15,'�', #3,'�','�','�','�','�',' ',#15,'�', #3,
    '�','�','�','�','�',' ',#15,'�', #3,'�','�','�','�','�','�','�',#25,
    #10,#11,'1','6','6',#24,' ',#15,'�', #3,'�','�','�','�','�','�','�',
    '�','�','�','�',#25,#20,#11,'1','9','9',#25, #5,#15,'�', #3,'�','�',
    '�','�','�','�','�','�','�','�','�',' ',#15,'�', #3,'�','�','�','�',
    '�','�','�','�','�',#25, #9,#11,'1','0','6',#24,' ',#15,'�', #3,'�',
    '�','�','�','�','�',' ',#15,'�', #3,'�','�','�','�','�','�',#25, #9,
    #11,'0','1','0','-','5','2','8','4','1','2','1',#25, #5,#15,'�', #3,
    '�','�','�','�','�','�','�','�',' ',#15,'�', #3,'�','�','�','�','�',
    #25,#16,#11,'1','0','7',#24,' ',#15,'�', #3,'�','�','�','�','�','�',
    ' ',#15,'�', #3,'�','�','�','�','�','�','�',#25, #8,#11,'0','1','0',
    '-','4','1','1','3','8','3','2',#25, #5,#15,'�', #3,'�','�','�','�',
    '�','�','�','�',' ',#15,'�', #3,'�','�','�','�','�','�','�','�','�',
    #25,#12,#11,'1','0','2',#24,' ',#15,'�', #3,'�','�','�','�','�','�',
    '�','�','�',' ',#15,'�', #3,'�','�','�','�','�','�','�','�',#25,#12,
    #11,'1','7','1',#25, #5,#15,'�', #3,'�','�','�','�','�',' ',#15,'�',
     #3,'�','�','�','�','�','�','�','�','�',#25, #7,#11,'0','1','0','-',
    '8','2','1','9','3','9','1',#24, #8,#26,'%','�',#25, #4,#15,'�', #3,
    '�','�','�','�','�',' ',#15,'�', #3,'�','�','�','�','�','�','�','�',
    '�','�','�','�',#25, #4,#11,'0','1','0','-','7','7','9','3','7','7',
    '7',#24,#25,'*',#15,'�', #3,'�','�','�','�','�',' ',#15,'�', #3,'�',
    '�','�','�','�','�','�','�','�','�',#25, #6,#11,'0','1','0','-','3',
    '6','1','7','0','8','9',#24, #8,#26, #8,'�','�',#10,'�','�','�','�',
    ' ','�','�','�','�','�','�','�','�','�', #8,'�',#26,#11,'�',#25, #2,
    #26,'%','�',#24,' ',#15,'�', #3,'�','�','�','�','�','�','�','�','�',
    '�',' ',#15,'�', #3,'�','�','�','�','�','�',#25, #4,#11,'0','1','0',
    '-','4','2','2','6','0','0','0',#24,' ',#15,'�', #3,'�','�','�','�',
    '�','�','�','�','�','�',' ',#15,'�', #3,'�','�','�','�','�','�',#25,
     #4,#11,'0','2','9','4','0','-','2','2','3','0','0',#25,#14, #8,#26,
     #8,'�','�',#10,'�','�','�','�','�','�','�','�','�', #8,'�',#26, #8,
    '�',#24,' ',#15,'�', #3,'�','�','�','�','�','�','�','�','�','�',' ',
    #15,'�', #3,'�','�','�','�','�','�',#25, #4,#11,'0','2','9','2','0',
    '-','2','5','2','4','9',#25,#15,#15,'�', #3,'�','�','�','�','�','�',
    #25, #8,#11,'0','1','0','-','3','6','3','6','5','0','8',#24,' ',#15,
    '�', #3,'�','�','�','�','�','�','�','�','�','�',' ',#15,'�', #3,'�',
    '.','�','�','�','/','�','�','�',' ',' ',#11,'0','2','3','5','0','-',
    '3','1','7','5','9',#25,#15,#15,'�', #3,'�','�','�','�','�',#25,#16,
    #11,'1','3','0','0',#24,' ',#15,'�', #3,'�','�','�','�',' ',#15,'�',
     #3,'�','�','�','�','�','�','�','�','�',#25,#15,#11,'1','4','3',#25,
    #15,#15,'�', #3,'�','�','�','�','�',#25, #9,#11,'0','1','0','-','5',
    '1','5','2','8','0','0',#24,' ',#15,'�', #3,'�','�','�','�',' ',#15,
    '�', #3,'�','�','�','�','�','�','�','�','�',#25, #7,#11,'0','1','0',
    '-','4','5','1','1','1','3','0',#25,#15,#15,'�', #3,'�','�','�','�',
    ' ','1',#25,#15,#11,'1','2','0','3',#24,' ',#15,'�', #3,'�','�','�',
    ' ',#15,'�', #3,'�','�','�','�','�','�',' ','-',' ',#15,'�', #3,'�',
    '�','�','�','�','�',#25, #9,#11,'1','4','2',#25,#15,#15,'�', #3,'�',
    '�','�','�','�','�',#25, #8,#11,'0','1','0','-','6','1','4','4','0',
    '0','0',#24,' ',#15,'�', #3,'�','�','�',' ',#15,'�', #3,'�','�','�',
    '�','�','�',#25,#11,#11,'0','1','0','-','8','8','4','3','2','5','0',
    #25,#15,#15,'�', #3,'�','�','�','�','�','�',#25, #8,#11,'0','1','0',
    '-','6','4','5','9','0','0','0',#24,' ',#15,'�', #3,'�','�','�','�',
    '�','�','�','�',' ','-',' ',#15,'�', #3,'�','�','�','�','�','�','�',
    '�',#25, #2,#11,'0','1','0','-','9',#26, #5,'6',#25,#15,#15,'�', #3,
    '�','�','�','�',' ',#15,'�', #3,'�','�','�','�','�',' ',' ',#11,'0',
    '8','0','1','-','1','1','-','5','7','0','0','0',#24,' ',#15,'�', #3,
    '�','�','�','�','�','�','�','�','�',' ',#15,'�', #3,'�','�','�','�',
    #25, #7,#11,'0','1','0','-','3','5','3',#26, #3,'0',#25,#15,#15,'�',
     #3,'�','�','�','�','�','�','�','�','�','�',#25, #4,#11,'0','1','0',
    '-','2','1','3','0','4','0','0',#24,' ',#15,'�', #3,'�','�','�','�',
    ' ','(','�','�','�',')',#25,#12,#11,'0','1','0','-','5','2','9',#26,
     #3,'7',#25,#15,#15,'�', #3,'�','�','�','�','�','�',#25, #8,#11,'0',
    '1','0','-','9','6','0','5','6','0','0',#24, #8,#26,'$','�',#25,#13,
    #26,#28,'�',#24,#24,#24,#24);

  VBUSIN_WIDTH=80;
  VBUSIN_DEPTH=25;
  VBUSIN_LENGTH=361;
  VBUSIN : array [1..361] of Char = (
    #15,#16,#25, #2,'�', #7,'�', #8,'�', #7,'�', #8,#26,#20,'�',#15,'T',
     #7,'o', #8,'t','a','l',' ',#15,'R', #7,'e', #8,'c','o','r','d','s',
    ' ', #7,'[', #8,'�','�','�', #7,']', #8,#26,#25,'�', #7,'�',#15,'�',
    #24,#25, #2, #7,'�',#25, #5,#15,'B', #7,'u','s','i','n','e','s','s',
    ' ',#15,'N', #7,'a','m','e',#25, #8,#15,'T', #7,'e','l','e','P','h',
    'o','n','e','1',#25,#10,#15,'C', #7,'i','t','y',#25,#16,'�',#24,#25,
     #2, #8,'�',#25, #6,#15,'B', #7,'u','s','i','n','e','s','s',' ',#15,
    'T', #7,'y','p','e',#25, #8,#15,'T', #7,'e','l','e','P','h','o','n',
    'e','2',#25,#10,#15,'A', #7,'d','d','r','e','s','s',#25,#12, #8,'�',
    #24,#25, #2,'�',#15,'�', #7,'�', #8,#26,#20,'�', #7,'�',#15,'�', #7,
    '�', #8,#26,#15,'�', #7,'�',#15,'�', #7,'�', #8,#26,#22,'�', #7,'�',
    #15,'�', #8,'�',#24,#25,#27,'�',#25,#17,'�',#24,#25,#27,'�',#25,#17,
    '�',#24,#25,#27,'�',#25,#17,'�',#24,#25,#27,'�',#25,#17,'�',#24,#25,
    #27,'�',#25,#17,'�',#24,#25,#27,'�',#25,#17,'�',#24,#25,#27,'�',#25,
    #17,'�',#24,#25,#27,'�',#25,#17,'�',#24,#25,#27,'�',#25,#17,'�',#24,
    #25,#27,'�',#25,#17,'�',#24,#25,#27,'�',#25,#17,'�',#24,#25,#27,'�',
    #25,#17,'�',#24,#25,#27,'�',#25,#17,'�',#24,#25,#27,'�',#25,#17,'�',
    #24,#25,#27,'�',#25,#17,'�',#24,#25,#27,'�',#25,#17,'�',#24,#25,#27,
    '�',#25,#17,'�',#24,#25,#27,'�',#25,#17,'�',#24,#25, #2,#15,'�', #7,
    '�', #8,#26,#21,'�', #7,'�',#15,'�', #7,'�', #8,#26,#15,'�', #7,'�',
    #15,'�', #7,'�', #8,#26,#21,'�', #7,'�', #8,'�', #7,'�',#15,'�', #8,
    '�',#24,#24,#24);

  VPERS_WIDTH=80;
  VPERS_DEPTH=25;
  VPERS_LENGTH=421;
  VPERS : array [1..421] of Char = (
    #15,#16,'�', #7,'�', #8,'�', #7,'�', #8,#26,#23,'�',#15,'T', #7,'o',
     #8,'t','a','l',' ',#15,'R', #7,'e', #8,'c','o','r','d','s',' ', #7,
    '[', #8,'�','�','�', #7,']', #8,#26,#30,'�', #7,'�',#15,'�',#24, #7,
    '�',#25, #3,#15,'L', #7,'a','s','t','N','a','m','e',#25,#13,#15,'T',
     #7,'e','l','e','P','h','o','n','e',#25,#11,#15,'C', #7,'i','t','y',
    #25,#16,#15,'I', #7,'C','Q',#25, #6,'�',#24, #8,'�',#25, #4,#15,'F',
     #7,'i','r','s','t','N','a','m','e',#25,#12,#15,'C', #7,'e','l','l',
    'P','h','o','n','e',#25,#11,#15,'E', #7,'m','a','i','l',#25,#15,#15,
    'M', #7,'S','N',#25, #5, #8,'�',#24,'�',#15,'�', #7,'�', #8,#26,#20,
    '�', #7,'�',#15,'�', #7,'�', #8,#26,#14,'�', #7,'�',#15,'�', #7,'�',
     #8,#26,#19,'�', #7,'�',#15,'�', #7,'�', #8,#26, #8,'�', #7,'�',#15,
    '�', #8,'�',#24,#25,#24,'�',#25,#16,'�',#25,#21,'�',#24,#25,#24,'�',
    #25,#16,'�',#25,#21,'�',#24,#25,#24,'�',#25,#16,'�',#25,#21,'�',#24,
    #25,#24,'�',#25,#16,'�',#25,#21,'�',#24,#25,#24,'�',#25,#16,'�',#25,
    #21,'�',#24,#25,#24,'�',#25,#16,'�',#25,#21,'�',#24,#25,#24,'�',#25,
    #16,'�',#25,#21,'�',#24,#25,#24,'�',#25,#16,'�',#25,#21,'�',#24,#25,
    #24,'�',#25,#16,'�',#25,#21,'�',#24,#25,#24,'�',#25,#16,'�',#25,#21,
    '�',#24,#25,#24,'�',#25,#16,'�',#25,#21,'�',#24,#25,#24,'�',#25,#16,
    '�',#25,#21,'�',#24,#25,#24,'�',#25,#16,'�',#25,#21,'�',#24,#25,#24,
    '�',#25,#16,'�',#25,#21,'�',#24,#25,#24,'�',#25,#16,'�',#25,#21,'�',
    #24,#25,#24,'�',#25,#16,'�',#25,#21,'�',#24,#25,#24,'�',#25,#16,'�',
    #25,#21,'�',#24,#25,#24,'�',#25,#16,'�',#25,#21,'�',#24,#15,'�', #7,
    '�', #8,#26,#21,'�', #7,'�',#15,'�', #7,'�', #8,#26,#14,'�', #7,'�',
    #15,'�', #7,'�', #8,#26,#19,'�', #7,'�',#15,'�', #7,'�', #8,#26, #6,
    '�', #7,'�', #8,'�','�', #7,'�',#15,'�',#24,#24,#24);
 
  PERS_WIDTH=80;
  PERS_DEPTH=25;
  PERS_LENGTH=502;
  PERS : array [1..502] of Char = (
     #8,#16,'�',#26,')','�','�',#26,'"','�','�',#24,'�','�','[', #9,'L',
    'a','s','t',' ','N','a','m','e', #8,']','�','�','[',#26,#19,'.',']',
    #25, #5,'�','�','[', #9,'T','e','l','e','p','h','o','n','e', #8,']',
    '�','�','[',#26,#14,'.',']',#25, #3,'�',#24,'�','�','[', #9,'F','i',
    'r','s','t',' ','N','a','m','e', #8,']','�','[',#26,#19,'.',']',#25,
     #5,'�','�','[', #9,'T','e','l','e','p','h','o','n','e','2', #8,']',
    '�','[',#26,#14,'.',']',#25, #3,'�',#24,'�','�','[', #9,'A','d','d',
    'r','e','s','s',' ','1', #8,']','�','�','[',#26,#19,'.',']',#25, #5,
    '�','�','[', #9,'C','e','l','l',' ','P','h','o','n','e', #8,']','�',
    '�','[',#26,#13,'.',']',#25, #3,'�',#24,'�','�','[', #9,'A','d','d',
    'r','e','s','s',' ','2', #8,']','�','�','[',#26,#19,'.',']',#25, #5,
    '�','�','[', #9,'C','e','l','l',' ','P','h','o','n','e','2', #8,']',
    '�','[',#26,#13,'.',']',#25, #3,'�',#24,'�','�','[', #9,'C','i','t',
    'y', #8,']','�','�','[',#26,#19,'.',']',#25,#10,'�','�','[', #9,'W',
    'o','r','k',' ','P','h','o','n','e', #8,']','�','�','[',#26,#14,'.',
    ']',#25, #2,'�',#24,'�','�','[', #9,'Z','i','p','C','o','d','e', #8,
    ']','�','�','[',#26, #7,'.',']',#25,#19,'�','�','[', #9,'W','o','r',
    'k',' ','P','h','o','n','e','2', #8,']','�','[',#26,#14,'.',']',#25,
     #2,'�',#24,'�','�','[', #9,'B','i','r','t','h','d','a','y', #8,']',
    '�','�','[','.','.','/','.','.','/','.','.',']',#25,#18,'�',#26, #7,
    '�','�',#25,#25,'�',#24,'�','�','[', #9,'e','-','M','a','i','l', #8,
    ']','�','�','[',#26,#19,'.',']',#25,#17,'�','�','[', #9,'I','C','Q',
    '#', #8,']','�','�','[',#26,#10,'.',']',#25, #3,'�',#24,'�','�','[',
     #9,'e','-','M','a','i','l','2', #8,']','�','[',#26,#19,'.',']',#25,
    #17,'�','�','[', #9,'M','S','N','#', #8,']','�','�','[',#26,#10,'.',
    ']',#25, #3,'�',#24,'�','�','[', #9,'P','e','r','s','o','n','a','l',
    ' ','U','R','L', #8,']','�','�','[',#26,'&','.',']',#26,#19,'�','�',
    #24,'�','�','[', #9,'C','o','m','m','e','n','t', #8,']','�','�','[',
    #26,'=','.',']','�','�','�',#24,'�',#26,'M','�','�',#24,#24,#24,#24,
    #24,#24,#24,#24,#24,#24,#24,#24,#24);

  BUSIN_WIDTH=80;
  BUSIN_DEPTH=25;
  BUSIN_LENGTH=402;
  BUSIN : array [1..402] of Char = (
     #8,#16,'�',#26,'M','�','�',#24,'�','�','[', #9,'B','u','s','i','n',
    'e','s','s',' ','N','a','m','e', #8,']','�','�','[',#26,#16,'.',']',
    '�','�','[', #9,'B','u','s','i','n','e','s','s',' ','T','y','p','e',
     #8,']','�','�','[',#26,#16,'.',']',#25, #2,'�',#24,'�','�','[', #9,
    'C','o','n','t','a','c','t',' ','N','a','m','e', #8,']','�','[',#26,
    #19,'.',']','�','[', #9,'C','o','n','t','a','c','t',' ','N','a','m',
    'e',' ','P','h','o','n','e', #8,']','�','[',#26,#14,'.',']',' ','�',
    #24,'�','�','[', #9,'A','d','d','r','e','s','s',' ','1', #8,']','�',
    '�','[',#26,#19,'.',']','�','�','[', #9,'A','d','d','r','e','s','s',
    ' ','2', #8,']','�','�','[',#26,#19,'.',']',#25, #4,'�',#24,'�','�',
    '[', #9,'C','i','t','y', #8,']','�','�','[',#26,#19,'.',']','�','�',
    '[', #9,'Z','i','p','C','o','d','e', #8,']','�','�','[',#26, #5,'.',
    ']',#25,#25,'�',#24,'�','�','[', #9,'T','e','l','e','p','h','o','n',
    'e', #8,']','�','�','[',#26,#14,'.',']','�','�','[', #9,'T','e','l',
    'e','p','h','o','n','e','2', #8,']','�','[',#26,#14,'.',']',#25,#14,
    '�',#24,'�','�','[', #9,'W','e','b',' ','S','i','t','e', #8,']','�',
    '�','[',#26,#19,'.',']','�','�','[', #9,'W','e','b',' ','S','i','t',
    'e','2', #8,']','�','[',#26,#19,'.',']',#25, #6,'�',#24,'�','�','[',
     #9,'W','e','b',' ','S','i','t','e',' ','U','s','e','r','N','a','m',
    'e', #8,']','�','�','[',#26,#12,'.',']','�','�','[', #9,'P','a','s',
    's','w','o','r','d', #8,']','�','�','[',#26, #9,'.',']',#25,#14,'�',
    #24,'�','�','[', #9,'C','o','m','m','e','n','t', #8,']','�','�','[',
    #26,'>','.',']','�','�',#24,'�',#26,'M','�','�',#24,#24,#24,#24,#24,
    #24,#24,#24,#24,#24,#24,#24,#24,#24,#24,#24);
    
  MAIN_WIDTH=80;
  MAIN_DEPTH=25;
  MAIN_LENGTH=1787;
  MAIN : array [1..1787] of Char = (
     #8,#16,#26,'O','�',#24,'�','�',' ', #9,'�','�','�', #8,'�','�','�',
    '�',#26, #5,'�','�','�','�','�','�','�','�','�','�','�','�','�','�',
    '�','�','�','�','�','�','�','�','�','�','�','�','�','�',' ',' ','�',
    '�',#25, #9, #9,'�',#24,' ',' ', #1,'�', #9,#17,'�','�',#16,'�','�',
    '�','�','�', #8,'�','�','�',#26, #3,'�',#26, #6,'�','�','�','�','�',
    '�','�',#26, #3,'�',#26, #6,'�',#25,#14, #1,'�',#25, #3,'�',#25, #3,
    '�', #9,'�',#24,' ',' ', #8,'�', #1,'�','�','�','�','�', #9,#17,'�',
    '�',#16,'�','�','�','�','�','�', #8,'�','�','�','�','�','�','�','�',
    '�','�','�','�','�','�','�',#26, #3,'�','�','�','�',#25, #2,'�',#25,
    #10, #9,'�',#25, #2,#17,'�','�','�', #1,#16,'�','�',' ', #9,'�',#25,
     #3, #1,'�',#24,#25, #2, #8,'�','�',' ', #1,'�','�',#26, #4,'�', #9,
    #17,#26, #5,'�', #8,'�', #9,#16,'�','�', #8,'�','�','�','�',#26, #3,
    '�','�','�','�',#26, #4,'�',' ',' ','�',#25, #8, #9,#17,'�',#19,#26,
     #3,'�',#15,'�','�','�','�','�','�','�', #9,#26, #3,'�',#17,'�','�',
     #1,#16,'�','�', #9,'�', #1,'�',#24, #8,'�','�',#25, #2,'�','�','�',
    ' ', #1,'�','�',#26, #4,'�', #8,#17,'�','�','�',' ','�',#16,'�', #4,
    '�','�', #8,'�','�','�','�','�','�',' ','�','�','�','�','�','�',' ',
    '�',#25, #5, #7,'�',#15,#23,'�','�', #7,#16,'�',' ',#15,#19,'�','�',
    '�',#16,'�',#19,'�', #9,'�',#17,'�','�', #1,#16,'�','�', #9,#17,'�',
    '�',#15,#19,#26, #3,'�','�','�', #9,'�','�',#17,'�', #1,#16,'�','�',
    ' ', #9,'�',#24, #8,'�','�','�',#25, #4,'�','�',' ',' ', #1,'�','�',
     #8,#17,'�','�',' ',' ','�',#16,'�',' ',#12,#20,'�','�','�','�','�',
    '�', #4,#16,'�', #8,#26, #4,'�','�','�',' ','�',#25, #2, #7,'�','�',
    '�',#15,#23,'�','�','�','�', #7,#16,'�',' ',' ', #9,#17,'�', #1,#16,
    '�','�','�', #9,#17,'�',#15,#19,'�', #9,'�',#17,' ',#16,#25, #3, #1,
    #26, #3,'�', #9,#17,'�','�',#15,#19,'�','�','�', #9,'�','�',#17,'�',
     #1,#16,'�',#24, #8,'�','�','�','�','�',#25, #4,'�','�','�','�',' ',
    ' ',#17,'�',#16,'�','�','�',#12,#20,'�','�',#15,#17,'�', #9,#16,'�',
    #12,#20,'�','�','�','�', #4,#16,'�', #8,'�','�','�','�','�','�',' ',
     #4,'�','�', #7,'�',#15,#23,'�',#26, #3,'�','�',' ',#16,#25, #3, #1,
    '�', #9,#17,'�','�','�',#15,#19,'�', #9,'�',#17,' ',#16,#25,#10, #1,
    '�','�', #9,#17,'�',#15,#19,'�',#16,'�',#19,' ', #9,'�',#17,' ', #1,
    #16,'�',#24, #8,'�','�','�','�','�','�','�',#25, #3,'�','�',' ','�',
    '�','�','�','�',#12,#20,'�','�','�','�','�','�','�', #4,#16,'�', #8,
    '�','�','�','�','�','�', #4,'�',#12,#20,'�',' ', #7,#16,'�',#15,#23,
    '�',#26, #4,'�','�','�','�', #7,#16,'�',' ', #9,'�', #1,'�', #9,#19,
    '�',#15,'�','�', #9,'�',#17,'�',#16,' ', #8,#26,#13,'�',' ',#17,' ',
     #9,#19,'�',#15,'�',' ', #9,'�',#17,' ',#24, #8,#16,'�','�','�','�',
    '�','�','�','�','�',#25, #3,'�','�','�','�','�','�',#12,#20,'�',#26,
     #3,'�','�', #4,#16,'�',' ',#26, #3,'�',#12,#20,'�','�','�',' ',#16,
    ' ', #7,'�',#15,#23,'�','�','�','�','�','�','�','�',' ',#16,' ', #1,
    '�',' ','�', #9,#17,'�',#19,'�',#15,'�','�', #9,'�', #1,#16,'�',' ',
     #2,'T',#10,'e', #2,'l',#10,'e', #2,'P','h',#10,'o', #2,'n',#10,'e',
     #2,'B',#10,'o','o', #2,'k',' ',#17,' ', #9,#19,'�',#15,#16,'�',#19,
    ' ', #9,'�', #1,#16,'�',#24, #8,'�','�','�','�','�','�','�','�','�',
    '�',#25, #4,'�','�','�','�',#20,'�',#12,'�','�','�',#26, #3,'�',#26,
     #5,'�',#16,'�',#20,' ',#16,#25, #2, #7,'�','�',' ',#26, #3,'�',' ',
     #9,'�',' ',#17,'�',#19,'�',#15,'�','�','�', #9,'�',#17,' ', #1,#16,
    '�',' ', #2,'B',#10,'y',' ', #2,'B','L',#10,'a', #2,'C',#10,'K',' ',
     #2,'B',#10,'i', #2,'R','D',' ',' ',#17,' ', #9,#19,'�',#15,#16,'�',
    #19,' ', #9,'�', #1,#16,'�',#24, #8,'�','�','�','�','�','�','�','�',
    '�','�',#25, #4, #4,'�','�','�','�',#12,#20,#26, #3,'�', #4,#16,#26,
     #4,'�',#20,#25, #3,#12,'�',#16,'�',#20,' ',#16,#25, #2, #4,'�','�',
    #25, #2, #1,'�',' ',' ', #9,#17,'�',#19,'�',#15,'�','�', #9,'�','�',
    #17,'�',#16,#25, #2, #8,#26,#13,'�',' ',#17,' ', #9,#19,'�',#15,#16,
    '�',#19,' ', #9,'�', #1,#16,'�',#24, #8,'�','�','�','�','�','�','�',
    '�','�',#25, #2, #4,'�','�','�',' ','�','�',#25, #9,'�','�',#20,#25,
     #2,#12,'�','�', #4,#16,'�',' ','�','�',#25, #2, #9,'�',' ', #1,'�',
     #9,#19,'�',' ',#15,#16,'�',#19,' ', #9,'�',#17,' ',#16,#25,#20,#17,
    ' ',#19,'�',#15,#16,'�',#19,' ', #9,'�', #1,#16,'�',#24, #8,'�','�',
    '�','�','�','�','�',#25, #3, #4,'�','�',' ','�','�','�',' ',' ',#20,
    ' ',#12,'�','�',' ', #4,#16,#26,#11,'�','�','�','�',' ',' ', #1,'�',
    ' ',' ','�', #9,#19,'�',' ',#15,#16,'�',#19,' ', #9,'�',#17,' ',#16,
    #25,#22,#17,' ',#19,'�',#15,#16,'�',#19,' ', #9,'�',#24, #8,#16,'�',
    '�','�','�','�',#25, #5, #4,'�','�','�','�',#25, #2,#20,#25, #2,#12,
    '�','�', #8,#16,'�','�','�','�','�','�','�','�','�','�','�','�',#25,
     #6, #9,#17,'�',#19,'�',#15,'�','�', #9,'�',#17,'�',#16,#25,#23,#17,
    ' ',#19,'�',#15,#16,'�',#19,' ', #9,'�', #1,#16,'�',#24, #8,'�','�',
    '�',#25, #3, #4,'�','�','�','�',#26, #3,'�','�','�',#20,' ',' ',#12,
    '�','�','�', #4,#16,'�', #8,'�','�','�','�','�','�','�','�','�','�',
    '�','�','�','�',#25, #2,#17,' ', #9,'�',#19,' ',#15,#16,'�',#19,' ',
     #9,'�',#17,' ',#16,#25,#25,#17,' ',#19,'�',#15,#16,'�',#19,' ', #1,
    #16,'�',#24, #8,'�',#25, #4, #4,'�','�',' ',' ',#26, #7,'�',#12,#20,
    '�','�', #4,#16,'�', #8,'�','�','�','�','�','�','�','�','�','�','�',
    '�','�','�','�','�',' ',' ', #9,'�',' ', #1,'�', #9,#17,'�',#19,'�',
    #15,'�','�', #9,'�',#17,'�', #1,#16,'�',#25,#23,#17,' ', #9,#19,'�',
    #15,#16,'�',#19,' ', #9,'�',#24,#16,#25, #5, #4,'�',' ',' ','�',' ',
    '�','�',#26, #3,'�','�','�','�','�', #8,'�','�','�','�','�','�','�',
    '�','�','�','�','�',#26, #4,'�','�', #9,'�',' ', #1,'�', #9,#17,'�',
    #15,#19,'�','�', #9,'�',#17,'�', #1,#16,'�',#25,#25,#17,' ', #9,#19,
    '�',#15,#16,'�',#19,' ',#24,#16,#25, #4, #4,'�','�',' ',' ','�','�',
    '�','�','�','�','�','�','�','�',#12,#20,'�',#16,'�',#20,'�', #4,#16,
    '�','�', #8,'�','�','�','�','�','�','�','�','�','�','�','�','�','�',
    '�','�',' ', #1,'�', #9,#17,'�',#15,#19,'�','�', #9,'�',#17,'�', #1,
    #16,'�',#25,#25,#17,' ', #9,#19,'�',#15,#16,'�',#19,' ',#24,#16,#25,
     #3, #4,'�','�','�','�','�','�','�','�','�','�','�','�','�','�','�',
    '�',#12,#20,'�','�','�','�','�', #4,#16,'�', #8,'�','�','�','�','�',
    '�','�','�',#26, #4,'�','�',' ',' ', #1,'�', #9,#17,'�',#15,#19,'�',
    '�', #9,'�',#17,'�', #1,#16,'�',#25,#25,#17,' ', #9,#19,'�',#15,#16,
    '�',#24, #4,'�','�','�','�',' ','�',#26, #6,'�','�',' ','�','�',' ',
    ' ','�',#26, #3,'�',#23,'�', #8,'�',#16,'�',#26, #6,'�',#25, #6, #1,
    '�', #9,#17,'�',#15,#19,'�','�', #9,'�',#17,' ',#16,#25, #9, #1,'�',
    ' ','�',#25,#10,#26, #3,'�',#17,' ', #9,#19,'�',#15,'�',#24,#16,' ',
     #4,'�','�','�','�', #8,#20,'�','�', #4,#23,'�','�', #8,'�',#16,'�',
    #25, #5, #4,'�','�', #8,'�',' ','�','�','�','�',#23,'�','�',#16,'�',
    #25,#12, #1,'�', #9,#17,'�',#15,#19,'�','�','�', #9,'�',#17,'�','�',
     #1,#16,#26, #5,'�', #9,#17,'�','�',#19,'�',#17,'�',#19,'�',#17,'�',
    '�','�', #1,#16,#26, #3,'�', #9,#17,#26, #3,'�',#19,#26, #3,'�',#15,
    '�','�', #9,#17,'�',#24, #4,#16,'�',' ', #8,'�','�',' ','�','�',#23,
    '�',' ','�',#16,'�',#25, #8,#26, #5,'�',#25,#16, #9,#17,'�','�','�',
    #15,#19,'�','�','�', #9,#26, #5,'�',#15,'�','�','�','�','�','�','�',
    '�', #9,#26, #3,'�',#15,#26, #3,'�',#26, #3,'�', #9,#16,'�', #1,'�',
    #24,#25, #3, #8,#26, #4,'�',#25,'$', #9,#17,'�','�','�',#15,#19,#26,
     #5,'�', #9,'�','�',#17,'�',#19,'�',#17,'�',#19,'�','�','�',#15,#26,
     #3,'�', #9,#26, #3,'�',#17,#26, #3,'�', #1,#16,'�',#24, #8,#26,'O',
    '�',#24);



   type ScreenType = array [0..3999] of Byte;
   var ScreenAddr : ScreenType absolute $B800:$0000;


implementation

end.