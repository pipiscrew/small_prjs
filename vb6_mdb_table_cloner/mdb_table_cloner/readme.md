# MDB_Table_Cloner

<img width="852" height="640" alt="Image" src="https://github.com/user-attachments/assets/8ee0cccb-1ff8-407d-b50c-97c7c7a41fa6" />

### MDB_Table_Cloner vs MDB_Table_ClonerEx

All below operations limited on first occurred per table. Example, when has 2 FK will create only the first one and so on.

| Function       | Origin | Ex |
| -------------- | ------ | -- |
| Creates PK     | x      | x  |
| Creates FK     |        | x  |
| Creates Unique |        | x  |
| Creates Index  |        |    |
| Export DDL     |        | x  |