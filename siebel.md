# Some of the most commonly used SQL queries in Siebel to retrieve information from the database (2016)

### SQL Query to find **partners** in Siebel.

```sql
SELECT COUNT (*)  
FROM siebel.s_org_ext  
WHERE row_id IN  
    (SELECT a.row_id  
     FROM siebel.s_org_ext a,  
          siebel.s_org_ext_x ax,  
          siebel.s_postn p,  
          siebel.s_bu I  
     WHERE a.row_id = ax.par_row_id  
       AND a.pr_postn_id = p.row_id  
       AND a.bu_id = i.row_id  
       AND ax.attrib_35 = 'Delete'  
       AND i.name NOT LIKE 'Delete%')  
  AND prtnr_flg <> 'N'; 
```

### SQL Query for pulling up **active and expired assignment rules**

```sql
SELECT a.created,  
  (SELECT login  
   FROM s_user  
   WHERE row_id = a.created_by), a.last_upd,  
  (SELECT login  
   FROM s_user  
   WHERE row_id = a.last_upd_by), a.NAME,  
                                  a.eff_start_dt,  
                                  a.eff_end_dt  
FROM s_asgn_grp a  
WHERE EXISTS  
    ( SELECT *  
     FROM s_asgn_grp_obj  
     WHERE asgn_object_name = 'Service Request'  
       AND a.row_id = asgn_grp_id)  
  AND (eff_end_dt IS NULL  
       OR eff_end_dt > SYSDATE)
```

### Query to find the division and **position** of the **Login**:

```sql
SELECT div.name AS Division,  
       div.loc AS LOCATION,  
       pos.name AS POSITION,  
       u.login AS Login  
FROM s_org_ext div,  
     s_postn pos,  
     s_user u  
WHERE pos.ou_id = div.row_id  
  AND pos.pr_emp_id = u.row_id  
  AND u.login ='BMARK
```

### SQL Query for **Assignment rules** for a particular user

```sql
SELECT distinct(a.name)  
FROM s_asgn_grp a,  
     S_ASGN_GRP_EMP b  
WHERE b.asgn_grp_id = a.row_id  
  AND b.EMP_ID = '1-AN0VX'
```

### SQL Query to Find Template of an **Activity**

```sql
SELECT name  
FROM s_tmpl_planitem  
WHERE row_id IN  
    (SELECT TMPL_PLANITEM_ID  
     FROM s_evt_act  
     WHERE todo_cd LIKE 'Pend%')
```

### **Query to find the dedicated users who are currently logged in**

```sql
SELECT username,  
       schemaname,  
       osuser,  
       terminal,  
       program,  
       TYPE,  
       status  
FROM sys.v_$session  
WHERE username NOT LIKE 'SADMI%'  
  AND program = 'siebel.exe' 
```

### SQL Query for finding **accounts belonging** to a specific organization

```sql
SELECT row_id,  
       name  
FROM siebel.s_org_ext  
WHERE row_id IN  
    (SELECT a.row_id  
     FROM siebel.s_org_ext a,  
          siebel.s_org_ext_x ax,  
          siebel.s_postn p,  
          siebel.s_org_int I  
     WHERE a.row_id = ax.par_row_id  
       AND a.pr_postn_id = p.row_id  
       AND a.bu_id = i.row_id  
       AND ax.attrib_35 LIKE 'Disq%'  
       AND a.PR_POSTN_ID<>'0-5220'  
       AND i.name LIKE 'GE Capital%') 
```

### SQL Query to find when was a user added to a **responsibility**

```sql
SELECT login,  
       pr.*  
FROM S_PER_RESP pr,  
     s_user u  
WHERE resp_id = '1-N6DW8'  
  AND pr.per_id = u.ROW_ID  
  AND Login IN ('BMARK') 
```

### SQL Query to find mobile users **last synchronization** sessions

This query displays each active mobile user, the number of sessions since last extract, their extract date and last sync date.  

```sql
SELECT T7.NAME "Userid",  
       T6.FST_NAME,  
       t6.last_name,  
       ep.x_attrib_35 "Bus Comp",  
       ep.x_attrib_34 "Parent RM Div",  
       T7.APP_SERVER_NAME,  
       t2.Last_file_num "Total sessions",  
       decode(t2.last_file_num, 0, t2.last_upd, NULL) "Extracted",  
       decode(t2.last_file_num, 0, to_date(NULL, 'mm/dd/yyyy'), t2.last_upd) "Last sync"  
FROM SIEBEL.S_DOCK_STATUS T2,  
     SIEBEL.S_CONTACT T6,  
     SIEBEL.S_NODE T7,  
     siebel.s_emp_per ep  
WHERE t7.emp_id = t6.PAR_ROW_ID  
  AND T7.ROW_ID = T2.NODE_ID  
  AND T7.NODE_TYPE_CD = 'REMOTE'  
  AND T2.TYPE = 'SESSION'  
  AND T7.EFF_END_DT IS NULL  
  AND t2.local_flg = 'N'  
  AND t6.par_row_id = ep.par_row_id  
ORDER BY T7.NAME
```

### SQL Query to find Organization of an **Opportunity**

```sql
SELECT name  
FROM s_bu  
WHERE row_id =  
    (SELECT bu_id  
     FROM s_opty  
     WHERE row_id = '1-2BM7S8')
```

### SQL Query to find **Division** of an Opportunity 

```sql
SELECT name  
FROM s_org_ext  
WHERE row_id IN  
    (SELECT ou_id  
     FROM s_postn  
     WHERE row_id IN  
         (SELECT pr_postn_id  
          FROM S_opty  
          WHERE row_id = '1-7VC0PN'))
```

source - [http://www.aired.in/2016/07/important-sql-queries-in-siebel.html](http://www.aired.in/2016/07/important-sql-queries-in-siebel.html)  

# List All Active RCR jobs in Siebel (2016)

```sql
-- src - http://siebelinsider.blogspot.com/2016/04/sql-query-to-list-all-active-rcr-jobs.html
SELECT
    PAR_REQ_ID "Job Id",
    rpt_interval||' '||rpt_uom "Frequency",
    STATUS,
    ACTL_START_DT,
    EXEC_SRVR_NAME
FROM
    SIEBEL.S_SRM_REQUEST
WHERE
    REQ_TYPE_CD = 'RPT_INSTANCE'
    AND STATUS = 'ACTIVE'
    AND PAR_REQ_ID in
(
select
    par.row_id
from
    siebel.S_SRM_REQUEST par,
    siebel.S_SRM_ACT_PARAM aparam ,
    siebel.S_SRM_REQUEST child ,
    siebel.S_SRM_REQ_PARAM param
where
    par.row_id = child.par_req_id
    and par.req_type_cd = 'RPT_PARENT'
    and par.STATUS = 'ACTIVE'
    and child.status in ('QUEUED','ACTIVE')
    and par.row_id = param.req_id
    and child.req_type_cd = 'RPT_INSTANCE'
    and param.ACTPARAM_ID = aparam.row_id
    and aparam.NAME = 'Workflow Process Name'
group by par.row_id,param.value, par.rpt_interval||' '||par.rpt_uom
)
order by ACTL_START_DT desc;
```

* [(2021) Microsoft - Import Siebel Data Using SQL Server Management Studio](https://learn.microsoft.com/en-us/biztalk/adapters-and-accelerators/adapter-siebel/import-siebel-data-using-sql-server-management-studio)
  * [(2013) Creating the Siebel Database on MSSQL](https://rojythomas.wordpress.com/installing-siebel-8-2-in-a-windows-environment-step-by-step/creating-the-siebel-database-and-running-grantusr-sql-or-just-plain-sql/)
* [(2020) Using Siebel Workflows](https://web.deu.edu.tr/doc/oracle/B14099_16/integrate.1012/b14062/app_siebworkflows.htm)
* [(2017) Configuring a Siebel Workflow](https://siebelenhance.blogspot.com/2017/01/configuring-siebel-workflow.html)
* [(2013) SiebelInstaller v8.1 Linux](https://github.com/henkwiedig/SiebelInstaller)