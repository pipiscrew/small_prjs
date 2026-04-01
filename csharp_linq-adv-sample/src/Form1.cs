using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace linq_adv_sample
{
    public partial class Form1 : Form
    {
        string exportFile = Path.Combine(Application.StartupPath, "index.html");
        public Form1()
        {
            InitializeComponent();
            General.appPath = Path.Combine(Application.StartupPath, "dbase.txt");
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            int[] categories = new int[] { 3, 14, 4, 15, 20, 21, 22 };

            var rows = General.ReadDB().Where(o=>o.Profil== "Analytical"); // Analytical / Creative / Practical

            var profils = rows.GroupBy(o => new { o.Profil, o.Ordinal })
                .Select(y => new
                {
                    y.Key.Profil,
                    y.Key.Ordinal
                }).OrderBy(o => o.Ordinal);

            var seen = new HashSet<string>();
            var seenGroupSignatures = new HashSet<string>();
            Func<ProductDTO, string> pairKey = r => $"{r.Code}|{r.Percentage:F2}";
            StringBuilder sb = new StringBuilder("<style> table, th, td {   border: 1px solid; } table {   border-collapse: collapse; } th, td {   padding: 10px; } </style>\r\n<table>  <tr>     <th>prod</th>     <th>profil</th>      <th>Accessory</th> 	<th>Percentage</th>   </tr>");

            foreach (var p in profils)
            {
                foreach (var cg in categories)
                {
                    var options = rows.Where(o => o.Profil == p.Profil && o.Category_ID == cg)
                         .OrderBy(o => o.ProfilOption)
                         .ThenBy(o => o.Ordinal)
                         .Select(o => new ProductDTO(o.ProductName, o.Accessory, o.Code, o.ProfilOption, o.Percentage))
                         .ToList();

                    if (options.Count == 0) continue;

                    //take 'MONKEY JUMPING' etc..
                    var prodsInOrder = options.Select(r => r.ProductName).Distinct().ToList();
                    foreach (string ProductName in prodsInOrder)
                    {
                        var byProfil = options.Where(r => r.ProductName == ProductName).GroupBy(r => r.ProfilOption).OrderBy(g => g.Key);

                        sb.AppendLine(string.Format("<tr><td>{0}</td></tr>", ProductName));

                        //profilGroup = ProfilOption 1-2-3-n with code&percentage
                        foreach (var profilGroup in byProfil)
                        {
                            //create sign for all ProfilOption
                            var pairs = profilGroup.Select(pairKey).Distinct().OrderBy(x => x).ToList();
                            var signature = string.Join("||", pairs); // 1508358873|85,00||1508359095|15,00||1913447238|0,00

                            //check duplication
                            if (seenGroupSignatures.Contains(signature))
                                continue;
                            else
                            {
                                sb.AppendLine(string.Format("<tr><td></td><td>{0}</td></tr>", profilGroup.Key));
                               // Console.WriteLine(ProductName);
                                
                                //lists the codes with percentages
                                foreach (var rec in profilGroup)
                                {
                                    sb.AppendLine(string.Format("<tr><td></td><td></td><td>{0}</td><td>{1}</td></tr>", rec.Accessory, rec.Percentage.ToString("N2")));
                                  //  Console.WriteLine(string.Format("{0} - {1}", rec.Accessory, rec.Percentage.ToString("N2")));
                                }
                            }

                            //add sign for later check duplication
                            seenGroupSignatures.Add(signature);
                        }
                    }

                   
                }
            }

            sb.AppendLine("</table>");
            File.WriteAllText(exportFile, sb.ToString());
        }
    }
}
