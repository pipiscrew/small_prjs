# ORACLE - SQL DEVELOPER

Normally we using **spool** command at **sql*plus** or either at sqldeveloper\sqldeveloper\bin\ **sql.exe**

--

but this can be achieved also by SQL DEVELOPER (underneath using **sql.exe**) :

```sql
--SET TERMOUT OFF; --this is only for sql.exe dont display the output
SET sqlformat CSV;
spool C:\Temp\export.csv;
select * from customers;
spool off;
```


the sqlformat can be :  
* DEFAULT
* CSV
* HTML
* XML
* JSON
* ANSICONSOLE
* INSERT
* LOADER
* FIXED
* DELIMITED

more  
* https://oracle-base.com/articles/misc/sqlcl-format-query-results-with-the-set-sqlformat-command
* https://docs.oracle.com/en/database/oracle/oracle-database/19/sqpug/SET-system-variable-summary.html#GUID-0AA910C4-C22A-4A9E-BE13-AAA059CC7919

---

you can preview the files :
* [ora_original.html](https://htmlpreview.github.io/?https://github.com/pipiscrew/small_prjs/blob/master/oracle_html_search/ora_original.html)
* [ora_original_quick_extended.html](https://htmlpreview.github.io/?https://github.com/pipiscrew/small_prjs/blob/master/oracle_html_search/ora_original_quick_extended.html)

---

ref - [search a li](https://www.pipiscrew.com/threads/js-search-a-li.73057)