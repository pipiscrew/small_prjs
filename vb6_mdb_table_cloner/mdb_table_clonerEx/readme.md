# MDB_Table_ClonerEx

<img width="850" height="638" alt="Image" src="https://github.com/user-attachments/assets/b8f9eb85-f3e1-42f2-8d5d-42f048a439f3" />

### MDB_Table_Cloner vs MDB_Table_ClonerEx

All below operations limited on first occurred per table. Example, when has 2 FK will create only the first one and so on.

| Function       | Origin | Ex |
| -------------- | ------ | -- |
| Creates PK     | x      | x  |
| Creates FK     |        | x  |
| Creates Unique |        | x  |
| Creates Index  |        |    |
| Export DDL     |        | x  |