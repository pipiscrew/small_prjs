# MDB_Table_Cloner vs PreEx vs Ex

* **PreEx** = All below operations **limited** on first occurred per table. Example, when has 2 FK will create only the first one and so on.  
* **Ex** = Supports **unlimited**.

| Function       | Origin | PreEx | Ex |
| -------------- | ------ | ----- | -- |
| Creates PK     | x      | x     | x  |
| Creates FK     |        | x     | x  |
| Creates Unique |        | x     | x  |
| Creates Index  |        |       | x  |
| Export DDL     |        | x     | x  |