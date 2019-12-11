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
    public partial class EMReq : Form
    {
        Session2Entities db = new Session2Entities();
        public EMReq(string assetID)
        {
            InitializeComponent();
            var query = db.EmergencyMaintenances.Where(x => x.Asset.AssetSN == assetID).FirstOrDefault();
            AssetSNLbl.Text = query.Asset.AssetSN;
            AssetNameLBL.Text = query.Asset.AssetName;
            DEPTLbl.Text = query.Asset.DepartmentLocation.Department.Name;
            var query2 = db.Priorities.Select( x=> x.Name);
            foreach(var item in query2)
            {
                Priority.Items.Add(item);
            }
            Priority.SelectedIndex = 0;
        }

        private void SendBtn_Click(object sender, EventArgs e)
        {
            var SN = AssetSNLbl.Text;
            var assetID = db.Assets.Where(x => x.AssetSN == SN).Select(x => x.ID).FirstOrDefault();

            var desc = DescEMTxt.Text.Trim();
            var consider = ConsiderationsTxt.Text.Trim();
            var priority = Priority.Text.ToString();
            var query3 = db.EmergencyMaintenances.Where(x => x.Priority.Name == priority).FirstOrDefault();

            EmergencyMaintenance EM = new EmergencyMaintenance();
            EM.AssetID = assetID;
            EM.PriorityID = query3.PriorityID;
            EM.DescriptionEmergency = desc;
            EM.OtherConsiderations = consider;
            EM.EMReportDate = DateTime.Now;
            EM.EMStartDate = DateTime.Now;
            EM.EMEndDate = null;
            EM.EMTechnicianNote = null;
            EmergencyMaintenance emergency = db.EmergencyMaintenances.Add(EM);
            try
            {
                db.SaveChanges();
                MessageBox.Show("Success!");
                this.Hide();
                
            }
            catch(Exception es){
                MessageBox.Show(es.ToString());
            }

        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
