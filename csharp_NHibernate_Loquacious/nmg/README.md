# NHibernate Mapping Generator (modded)

This is a modded version on [rvrn22.nmg](https://github.com/rvrn22/nmg).  

Tested mysql & sqlite, found some bugs, tried to solve.. In this release the following enhancements made :  
* remove 3rd party unwanted dbtypes, left only mysql / sqlite / sqlserver / oracle / sybase oledb
* sqlite implementation to work with **FKs**
* fix **null exception**, that stops the code generation at NMG.Core.Generator.CodeGenerator.FixPropertyWithSameClassName
* compiled under framework v4.5.2
* update FastColoredTextBox (https://www.nuget.org/packages/FCTB/) due compilation problems on main PRJ.
* remain System.Data.SQLite to v1.0.82 (frm4client - anycpu) -- [grab latest](https://www.nuget.org/packages/System.Data.SQLite.x86)
* fontsize on Connection dialog
* make readonly and disable row resize on datagridview
* sqlite fix table name with brackets

references :
* [2022 winforms](https://github.com/PialKanti/NHibernateSample)
* [2012](https://github.com/SzymonPobiega/NHibernate-Deep-Dive/blob/master/NHibernate%20Deep%20Dive/SpecificationBase.cs)
* [with repo + crud w/ dg](https://www.youtube.com/watch?v=nttnRL7UEdc)
* [dg](https://youtu.be/xgWamkf_rDI?t=1926)

see releases for full distro with DLLs.