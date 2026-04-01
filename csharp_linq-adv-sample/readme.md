## Entity of product model

First is the `profil` option
* Analytical
* Creative
* Practical

each `product` has an entry to all `profiles`
depend on `product&profile combination` there are `ProfilTypes`, currently is 1-3 on database, theoretically is 1-n.  

This application would like to achieve a listing that presents `unique ProfilTypes` product independently.  

Is not so common, now you know more, let me tell it again -- the application actually compares the `ProfilTypes` records (which is 1-n) and lists the unique product independently...  

On exported HTML you will see the `Product Name` then the `ProfilTypes` (1-n) then the `accessories` in each `ProfilType`..  

![Image](https://github.com/user-attachments/assets/68a612e1-72a8-49e0-a602-12d1ffc7484b)  

When a `Product Name` printed without children, means all `profilTypes` are already shown in previous product(s).  

There are cases that can list for example only the profileType number 2, this means that the 1st and the 3rd are already shown in previous product(s).  

The count of the percentage of each `ProfilType` children (aka accessories) always is 100% by factory (dbase).  

---

tools used : 
* .net framework v4.7.2
* VS2017
