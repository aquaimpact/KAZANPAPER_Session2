using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kazanpaper2
{
    public partial class Home : Form
    {
        public Home(Employee employeeDetails)
        {
            InitializeComponent();
            var assetInfo = db.Assets.Where(x => x.EmployeeID == employeeDetails.ID).ToList();
            DataTable table = CLTDT(assetInfo);
            dataGridView1.DataSource = table;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        Session2Entities db = new Session2Entities();
        static DataTable CLTDT(List<Asset> assets)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Asset SN");
            table.Columns.Add("Asset Name");
            table.Columns.Add("Last Closed EM");
            table.Columns.Add("Number of EMs");
            table.Columns.Add("Status");

            foreach(var item in assets)
            {
                DataRow dataRow = table.NewRow();
                dataRow["Asset SN"] = item.AssetSN;
                dataRow["Asset Name"] = item.AssetName;
                var EMdate = item.EmergencyMaintenances.OrderByDescending( x => x.EMStartDate).FirstOrDefault();
                var state = "";
                if (EMdate != null)
                {
                    if (EMdate.EMEndDate != null)
                    {
                        dataRow["Status"] = "Completed!";
                        state = EMdate.EMEndDate.ToString();
                    }
                    else
                    {
                        dataRow["Status"] = "Not Completed!";
                        state = "--";
                    }
                    
                }
                else
                {
                    dataRow["Status"] = "NIL";
                    state = "--";
                }
                dataRow["Last Closed EM"] = state;
                var NoEM = item.EmergencyMaintenances.Where(x => x.EMEndDate != null).Count();
                dataRow["Number of EMs"] = NoEM;
                table.Rows.Add(dataRow);
            }
            return table;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count > 1)
            {
                MessageBox.Show("Invalid Selection!");
            }
            else
            {
                foreach(DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    string ID = row.Cells["Asset SN"].FormattedValue.ToString();
                    EMReq eMReq = new EMReq(ID);
                    eMReq.ShowDialog();
                    this.Hide();
                }
            }
        }
    }
}
