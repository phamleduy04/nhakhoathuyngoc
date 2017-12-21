using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
namespace DentalManagementSystem
{
    public partial class frmPatientRecord : Form
    {
        ConnectionString cs = new ConnectionString();
        CommonClasses cc = new CommonClasses();
        public frmPatientRecord()
        {
            InitializeComponent();
        }
        public void GetData()
        {
            try
            {
                cc.con = new SqlConnection(cs.DBConn);
                cc.con.Open();
                cc.cmd = new SqlCommand("SELECT RTRIM(P_ID) as [ID],RTRIM(PatientID) as [Patient ID],RTRIM(Name) as [Patient Name],RTRIM(Gender) as [Gender],Convert(Date,DOB,103) as [DOB],RTRIM(Email) as [Email],RTRIM(ContactNo) as [Contact No.],RTRIM(InsuranceCompany) as [Insurance Company],RTRIM(Address) as [Address],RTRIM(BillingContactPerson) as [Billing Contact Person],RTRIM(telephoneNo) as [Telephone No.] from Patient order by name", cc.con);
                cc.da = new SqlDataAdapter(cc.cmd);
                cc.ds = new DataSet();
                cc.da.Fill(cc.ds, "Patient");
                dgw.DataSource = cc.ds.Tables["Patient"].DefaultView;
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtPatientName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cc.con = new SqlConnection(cs.DBConn);
                cc.con.Open();
                cc.cmd = new SqlCommand("SELECT RTRIM(P_ID) as [ID],RTRIM(PatientID) as [Patient ID],RTRIM(Name) as [Patient Name],RTRIM(Gender) as [Gender],Convert(Date,DOB,103) as [DOB],RTRIM(Email) as [Email],RTRIM(ContactNo) as [Contact No.],RTRIM(InsuranceCompany) as [Insurance Company],RTRIM(Address) as [Address],RTRIM(BillingContactPerson) as [Billing Contact Person],RTRIM(telephoneNo) as [Telephone No.] from Patient  WHERE name like '" + txtPatientName.Text + "%' order by name", cc.con);
                cc.da = new SqlDataAdapter(cc.cmd);
                cc.ds = new DataSet();
                cc.da.Fill(cc.ds, "Patient");
                dgw.DataSource = cc.ds.Tables["Patient"].DefaultView;
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void Reset()
        {
            txtPatientName.Text = "";
            GetData();
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgw_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            string strRowNumber = (e.RowIndex + 1).ToString();
            SizeF size = e.Graphics.MeasureString(strRowNumber, this.Font);
            if (dgw.RowHeadersWidth < Convert.ToInt32((size.Width + 20)))
            {
               dgw.RowHeadersWidth = Convert.ToInt32((size.Width + 20));
            }
            Brush b = SystemBrushes.ControlText;
            e.Graphics.DrawString(strRowNumber, this.Font, b, e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + ((e.RowBounds.Height - size.Height) / 2));
     
        }

        private void dgw_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (lblOperation.Text == "Patient Master")
                {
                    DataGridViewRow dr = dgw.SelectedRows[0];
                    this.Hide();
                    frmPatient frm = new frmPatient();
                    frm.Show();
                    frm.txtID.Text = dr.Cells[0].Value.ToString();
                    frm.txtPatientID.Text = dr.Cells[1].Value.ToString();
                    frm.txtPatientName.Text = dr.Cells[2].Value.ToString();
                    if (dr.Cells[3].Value.ToString()=="Male")
                    {
                        frm.rbMale.Checked=true;
                    }
                        else
                    {
                        frm.rbFemale.Checked = true;
                    }

                    frm.dtpDOB.Text = dr.Cells[4].Value.ToString();
                    frm.txtEmailID.Text = dr.Cells[5].Value.ToString();
                    frm.txtContactNo.Text = dr.Cells[6].Value.ToString();
                    frm.txtInsuranceCompany.Text = dr.Cells[7].Value.ToString();
                    frm.txtAddress.Text = dr.Cells[8].Value.ToString();
                    frm.txtBillingContactPerson.Text = dr.Cells[9].Value.ToString();
                    frm.txtTelephoneNo.Text = dr.Cells[10].Value.ToString();
                   
                    frm.btnUpdate.Enabled = true;
                    frm.btnDelete.Enabled = true;
                    frm.btnSave.Enabled = false;
                    frm.lblUser.Text = lblUser.Text;
                    lblOperation.Text = "";
                }
         
                if (lblOperation.Text == "Procedure")
                {
                    DataGridViewRow dr = dgw.SelectedRows[0];
                    this.Hide();
                    frmProcedure frm = new frmProcedure();
                    frm.Show();
                    frm.txtP_ID.Text = dr.Cells[0].Value.ToString();
                    frm.txtPatientID.Text = dr.Cells[1].Value.ToString();
                    frm.txtPatientName.Text = dr.Cells[2].Value.ToString();
                    frm.lblUser.Text = lblUser.Text;
                    lblOperation.Text = "";
                    frm.auto();
                }
                if (lblOperation.Text == "Invoice")
                {
                    DataGridViewRow dr = dgw.SelectedRows[0];
                    this.Hide();
                    frmInvoice frm = new frmInvoice();
                    frm.Show();
                    frm.txtP_ID.Text = dr.Cells[0].Value.ToString();
                    frm.txtPatientID.Text = dr.Cells[1].Value.ToString();
                    frm.txtPatientName.Text = dr.Cells[2].Value.ToString();
                    frm.lblUser.Text = lblUser.Text;
                    lblOperation.Text = "";
                    frm.auto();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmCustomerRecord_Load(object sender, EventArgs e)
        {
            GetData();
        }

    }
}
