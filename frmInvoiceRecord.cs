﻿using System;
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
    public partial class frmInvoiceRecord : Form
    {
        ConnectionString cs = new ConnectionString();
        CommonClasses cc = new CommonClasses();
        public frmInvoiceRecord()
        {
            InitializeComponent();
        }
        public void GetData()
        {
            try
            {
                cc.con = new SqlConnection(cs.DBConn);
                cc.con.Open();
                cc.cmd = new SqlCommand("SELECT RTRIM(INV_Id) as [ID], RTRIM(InvoiceNo) as [Invoice No.],Date,Convert(Date,DateFrom,103) as [Procedure Date From],Convert(Date,DateTo,103) as [Procedure Date To],RTRIM(P_ID) as [PID],RTRIM(Patient.PatientID) as [Patient ID],RTRIM(Name) as [Patient Name],RTRIM(TotalCopayAmount) as [Copay Amount],RTRIM(TotalCharges) as [Total Charges],RTRIM(GrandTotal_inv) as [Grand Total],RTRIM(TotalPaid_Inv) as [Total Paid],RTRIM(PaymentDue_Inv) as [Payment Due] from Patient,Invoice where Patient.P_ID=Invoice.PatientID order by date", cc.con);
                cc.da = new SqlDataAdapter(cc.cmd);
                cc.ds = new DataSet();
                cc.da.Fill(cc.ds, "Invoice");
                dgw.DataSource = cc.ds.Tables["Invoice"].DefaultView;
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
                cc.cmd = new SqlCommand("SELECT RTRIM(INV_Id) as [ID], RTRIM(InvoiceNo) as [Invoice No.],Date,Convert(Date,DateFrom,103) as [Procedure Date From],Convert(Date,DateTo,103) as [Procedure Date To],RTRIM(P_ID) as [PID],RTRIM(Patient.PatientID) as [Patient ID],RTRIM(Name) as [Patient Name],RTRIM(TotalCopayAmount) as [Copay Amount],RTRIM(TotalCharges) as [Total Charges],RTRIM(GrandTotal_inv) as [Grand Total],RTRIM(TotalPaid_Inv) as [Total Paid],RTRIM(PaymentDue_Inv) as [Payment Due] from Patient,Invoice where Patient.P_ID=Invoice.PatientID and name like '" + txtPatientName.Text + "%' order by name", cc.con);
                cc.da = new SqlDataAdapter(cc.cmd);
                cc.ds = new DataSet();
                cc.da.Fill(cc.ds, "Invoice");
                dgw.DataSource = cc.ds.Tables["Invoice"].DefaultView;
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
            dtpDateFrom.Text = System.DateTime.Today.ToString();
            dtpDateTo.Text = System.DateTime.Now.ToString();
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
                if (lblOperation.Text == "Invoice")
                {
                    DataGridViewRow dr = dgw.SelectedRows[0];
                    this.Hide();
                    frmInvoice frm = new frmInvoice();
                    frm.Show();
                    frm.txtID.Text = dr.Cells[0].Value.ToString();
                    frm.txtInvoiceNo.Text = dr.Cells[1].Value.ToString();
                    frm.dtpInvoiceDate.Text = dr.Cells[2].Value.ToString();
                    frm.dtpDateFrom.Text = dr.Cells[3].Value.ToString();
                    frm.dtpDateTo.Text = dr.Cells[4].Value.ToString();
                    frm.txtP_ID.Text = dr.Cells[5].Value.ToString();
                    frm.txtPatientID.Text = dr.Cells[6].Value.ToString();
                    frm.txtPatientName.Text = dr.Cells[7].Value.ToString();
                    frm.txtCopayAmount.Text = dr.Cells[8].Value.ToString();
                    frm.txtCharges.Text = dr.Cells[9].Value.ToString();
                    frm.txtGrandTotal.Text = dr.Cells[10].Value.ToString();
                    frm.txtTotalPaid.Text = dr.Cells[11].Value.ToString();
                    frm.txtPaymentDue.Text = dr.Cells[12].Value.ToString();
                    frm.btnUpdate.Enabled = true;
                    frm.btnDelete.Enabled = true;
                    frm.btnSave.Enabled = false;
                    frm.button2.Enabled = false;
                    frm.dtpInvoiceDate.Enabled = false;
                    frm.lblUser.Text = lblUser.Text;
                    lblOperation.Text = "";
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

        private void btnGetData_Click(object sender, EventArgs e)
        {
            try
            {
                cc.con = new SqlConnection(cs.DBConn);
                cc.con.Open();
                cc.cmd = new SqlCommand("SELECT RTRIM(INV_Id) as [ID], RTRIM(InvoiceNo) as [Invoice No.],Date,Convert(Date,DateFrom,103) as [Procedure Date From],Convert(Date,DateTo,103) as [Procedure Date To],RTRIM(P_ID) as [PID],RTRIM(Patient.PatientID) as [Patient ID],RTRIM(Name) as [Patient Name],RTRIM(TotalCopayAmount) as [Copay Amount],RTRIM(TotalCharges) as [Total Charges],RTRIM(GrandTotal_inv) as [Grand Total],RTRIM(TotalPaid_Inv) as [Total Paid],RTRIM(PaymentDue_Inv) as [Payment Due] from Patient,Invoice where Patient.P_ID=Invoice.PatientID and Date between @d1 and @d2 order by Date", cc.con);
                cc.cmd.Parameters.Add("@d1", SqlDbType.DateTime, 30, "Date").Value = dtpDateFrom.Value.Date;
                cc.cmd.Parameters.Add("@d2", SqlDbType.DateTime, 30, "Date").Value = dtpDateTo.Value;
                cc.da = new SqlDataAdapter(cc.cmd);
                cc.ds = new DataSet();
                cc.da.Fill(cc.ds, "Invoice");
                dgw.DataSource = cc.ds.Tables["Invoice"].DefaultView;
                cc.con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
