# Other ORM libraries

|            ORM |                        Method |  Return |      Mean |    StdDev |     Error |   Gen 0 |  Gen 1 |  Gen 2 | Allocated |
|--------------- |------------------------------ |-------- |----------:|----------:|----------:|--------:|-------:|-------:|----------:|
|       Belgrade |                 ExecuteReader |    Post |  94.46 μs |  8.115 μs | 12.268 μs |  1.7500 | 0.5000 |      - |   8.42 KB |
|     Hand Coded |                     DataTable | dynamic | 105.43 μs |  0.998 μs |  1.508 μs |  3.0000 |      - |      - |   9.37 KB |
|     Hand Coded |                    SqlCommand |    Post | 106.58 μs |  1.191 μs |  1.801 μs |  1.5000 | 0.7500 | 0.1250 |   7.42 KB |
|         Dapper |  QueryFirstOrDefault&lt;dynamic&gt; | dynamic | 119.52 μs |  1.320 μs |  2.219 μs |  3.6250 |      - |      - |  11.39 KB |
|         Dapper |   &#39;Query&lt;dynamic&gt; (buffered)&#39; | dynamic | 119.93 μs |  1.943 μs |  2.937 μs |  2.3750 | 1.0000 | 0.2500 |  11.73 KB |
|        Massive |             &#39;Query (dynamic)&#39; | dynamic | 120.31 μs |  1.340 μs |  2.252 μs |  2.2500 | 1.0000 | 0.1250 |  12.07 KB |
|         Dapper |        QueryFirstOrDefault&lt;T&gt; |    Post | 121.57 μs |  1.564 μs |  2.364 μs |  1.7500 | 0.7500 |      - |  11.35 KB |
|         Dapper |         &#39;Query&lt;T&gt; (buffered)&#39; |    Post | 121.67 μs |  2.913 μs |  4.403 μs |  1.8750 | 0.8750 |      - |  11.65 KB |
|       PetaPoco |             &#39;Fetch&lt;T&gt; (Fast)&#39; |    Post | 124.91 μs |  4.015 μs |  6.747 μs |  2.0000 | 1.0000 |      - |   11.5 KB |
|         Mighty |                      Query&lt;T&gt; |    Post | 125.23 μs |  2.932 μs |  4.433 μs |  2.2500 | 1.0000 |      - |   12.6 KB |
|     LINQ to DB |                      Query&lt;T&gt; |    Post | 125.76 μs |  2.038 μs |  3.081 μs |  2.2500 | 0.7500 | 0.2500 |  10.62 KB |
|       PetaPoco |                      Fetch&lt;T&gt; |    Post | 127.48 μs |  4.283 μs |  6.475 μs |  2.0000 | 1.0000 |      - |  12.18 KB |
|     LINQ to DB |            &#39;First (Compiled)&#39; |    Post | 128.89 μs |  2.627 μs |  3.971 μs |  2.5000 | 0.7500 |      - |  10.92 KB |
|         Mighty |                Query&lt;dynamic&gt; | dynamic | 129.20 μs |  2.577 μs |  3.896 μs |  2.0000 | 1.0000 |      - |  12.43 KB |
|         Mighty |            SingleFromQuery&lt;T&gt; |    Post | 129.41 μs |  2.094 μs |  3.166 μs |  2.2500 | 1.0000 |      - |   12.6 KB |
|         Mighty |      SingleFromQuery&lt;dynamic&gt; | dynamic | 130.59 μs |  2.432 μs |  3.677 μs |  2.0000 | 1.0000 |      - |  12.43 KB |
|         Dapper |              &#39;Contrib Get&lt;T&gt;&#39; |    Post | 134.74 μs |  1.816 μs |  2.746 μs |  2.5000 | 1.0000 | 0.2500 |  12.29 KB |
|   ServiceStack |                 SingleById&lt;T&gt; |    Post | 135.01 μs |  1.213 μs |  2.320 μs |  3.0000 | 1.0000 | 0.2500 |  15.27 KB |
|     LINQ to DB |                         First |    Post | 151.87 μs |  3.826 μs |  5.784 μs |  3.0000 | 1.0000 | 0.2500 |  13.97 KB |
|           EF 6 |                      SqlQuery |    Post | 171.00 μs |  1.460 μs |  2.791 μs |  3.7500 | 1.0000 |      - |  23.67 KB |
| DevExpress.XPO |             GetObjectByKey&lt;T&gt; |    Post | 172.36 μs |  3.758 μs |  5.681 μs |  5.5000 | 1.2500 |      - |  29.06 KB |
|         Dapper |       &#39;Query&lt;T&gt; (unbuffered)&#39; |    Post | 174.40 μs |  3.296 μs |  4.983 μs |  2.0000 | 1.0000 |      - |  11.77 KB |
|         Dapper | &#39;Query&lt;dynamic&gt; (unbuffered)&#39; | dynamic | 174.45 μs |  1.988 μs |  3.340 μs |  2.0000 | 1.0000 |      - |  11.81 KB |
| DevExpress.XPO |                 FindObject&lt;T&gt; |    Post | 181.76 μs |  5.554 μs |  9.333 μs |  8.0000 |      - |      - |  27.15 KB |
| DevExpress.XPO |                      Query&lt;T&gt; |    Post | 189.81 μs |  4.187 μs |  8.004 μs | 10.0000 |      - |      - |  31.61 KB |
|        EF Core |            &#39;First (Compiled)&#39; |    Post | 199.72 μs |  3.983 μs |  7.616 μs |  4.5000 |      - |      - |   13.8 KB |
|     NHibernate |                        Get&lt;T&gt; |    Post | 248.71 μs |  6.604 μs | 11.098 μs |  5.0000 | 1.0000 |      - |  29.79 KB |
|        EF Core |                         First |    Post | 253.20 μs |  3.033 μs |  5.097 μs |  5.5000 |      - |      - |   17.7 KB |
|     NHibernate |                           HQL |    Post | 258.70 μs | 11.716 μs | 17.712 μs |  5.0000 | 1.0000 |      - |   32.1 KB |
|        EF Core |                      SqlQuery |    Post | 268.89 μs | 19.349 μs | 32.516 μs |  6.0000 |      - |      - |   18.5 KB |
|           EF 6 |                         First |    Post | 278.46 μs | 12.094 μs | 18.284 μs | 13.5000 |      - |      - |  44.18 KB |
|        EF Core |         &#39;First (No Tracking)&#39; |    Post | 280.88 μs |  8.192 μs | 13.765 μs |  3.0000 | 0.5000 |      - |  19.38 KB |
|     NHibernate |                      Criteria |    Post | 304.90 μs |  2.232 μs |  4.267 μs | 11.0000 | 1.0000 |      - |  60.29 KB |
|           EF 6 |         &#39;First (No Tracking)&#39; |    Post | 316.55 μs |  7.667 μs | 11.592 μs |  8.5000 | 1.0000 |      - |  50.95 KB |
|     NHibernate |                           SQL |    Post | 335.41 μs |  3.111 μs |  4.703 μs | 19.0000 | 1.0000 |      - |  78.86 KB |
|     NHibernate |                          LINQ |    Post | 807.79 μs | 27.207 μs | 45.719 μs |  8.0000 | 2.0000 |      - |  53.65 KB |

[source](https://github.com/DapperLib/Dapper)