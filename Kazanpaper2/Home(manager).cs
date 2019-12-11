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
    public partial class Home_manager_ : Form
    {
        Session2Entities db = new Session2Entities();
        public Home_manager_()
        {
            InitializeComponent();
            var query = db.EmergencyMaintenances.ToList();
            DataTable table = importData(query);
            dataGridView1.DataSource = table;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        static DataTable importData(List<EmergencyMaintenance> EM)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Asset SN");
            table.Columns.Add("Asset Name");
            table.Columns.Add("Request Date");
            table.Columns.Add("Employee Full Name");
            table.Columns.Add("Department");
            foreach(var item in EM.OrderByDescending( x=> x.PriorityID).OrderBy( x=> x.EMReportDate))
            {
                DataRow dataRow = table.NewRow();
                dataRow["Asset SN"] = item.Asset.AssetSN;
                dataRow["Asset Name"] = item.Asset.AssetName;
                //TODO: CHANGE THE REQUEST DATE
                dataRow["Request Date"] = item.EMReportDate;
                dataRow["Employee Full Name"] = item.Asset.Employee.FirstName + " " + item.Asset.Employee.LastName;
                dataRow["Department"] = item.Asset.DepartmentLocation.Department.Name;
                table.Rows.Add(dataRow);
            }
            return table;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
