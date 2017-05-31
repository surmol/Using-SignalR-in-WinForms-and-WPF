using Microsoft.AspNet.SignalR.Client;
using Npgsql;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;

namespace WinFormsClient
{
    /// <summary>
    /// SignalR client hosted in a WinForms application. The client
    /// lets the user pick a user name, connect to the server asynchronously
    /// to not block the UI thread, and send chat messages to all connected 
    /// clients whether they are hosted in WinForms, WPF, or a web application.
    /// </summary>
    public partial class WinFormsClient : Form
    {

        public Color icon_colour;



        private String UserName { get; set; }
        private IHubProxy HubProxy { get; set; }
        const string ServerURI = "http://192.168.1.63:8089";
        private HubConnection Connection { get; set; }
        
        internal WinFormsClient()
        {
            InitializeComponent();

            //dt.Columns.Add(new DataColumn("Camera id", typeof(long)));
            //dt.Columns.Add(new DataColumn("Pose", typeof(string)));
            //dt.Columns.Add(new DataColumn("Danger level", typeof(string)));
            //dt.Columns.Add(new DataColumn("Timestamp", typeof(DateTime)));
            //dt.Rows.Add(1, "OFFLINE", "OFFLINE", DateTime.Now);
            //dataGridView1.DataSource = dt;
            //dataGridView1.Update();
            //dataGridView1.Refresh();
            get_all_cameras();
            ConnectAsync();
        }
        public enum DangerLevel
        {
            Undefined = -1,
            Normal = 0,
            Acceptable = 1,
            Suspicious = 2,
            Warning = 3,
            Danger = 4,
            Alarm = 5
        }
        public enum Pose
        {
            Multiple = -2,
            NotProcessed = -1,
            Unknown = 0,
            Moving = 1,
            Lying = 2,
            LyingArmToRight = 3,
            LyingArmToLeft = 4,
            SittingLegsOnBed = 5,
            SittingLegsDownToRight = 6,
            SittingLegsDownToLeft = 7,
            StandingToRight = 8,
            StandingToLeft = 9,
            SuspiciousToRight = 10,
            SuspiciousToLeft = 11
        }
        public DataTable dt = new DataTable();
        private DataSet ds = new DataSet();

        public void get_all_cameras()
        {
            try
            {
                // PostgeSQL-style connection string
                string connstring = String.Format("Server=192.168.1.63;Port=5432;Database=SAFE;User Id=fsps;Password=F$P$123");
                // Making connection with Npgsql provider
                NpgsqlConnection conn = new NpgsqlConnection(connstring);
                conn.Open();
                // quite complex sql statement
                string sql = "SELECT * FROM public.\"Cameras\" ORDER BY \"Id\" ASC";
                // data adapter making request from our connection
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                // i always reset DataSet before i do
                // something with it.... i don't know why :-)
                ds.Reset();

               
                // fillng DataSet with result from NpgsqlDataAdapter
                da.Fill(ds);
                // since it C# DataSet can handle multiple tables, we will select first
                dt = ds.Tables[0];
                
                dt.Columns.Remove("Description");
                dt.Columns.Remove("CameraStatus");
                dt.Columns.Remove("SerialNumber");
                dt.Columns.Remove("FramesMode");
                dt.Columns.Add("Pose");
                dt.Columns.Add("Danger Level");
                dt.Columns.Add("TimeStamp");
               // dt.Columns.Add("XXX");
                // connect grid to DataTable
                dataGridView1.DataSource = dt;
                // since we only showing the result we don't need connection anymore
                conn.Close();
            }
            catch (Exception msg)
            {
                // something went wrong, and you wanna know why
                MessageBox.Show(msg.ToString());
                throw;
            }



        }
        private void ButtonSend_Click(object sender, EventArgs e)
        {
            //HubProxy.Invoke("Send", UserName, TextBoxMessage.Text);
            //TextBoxMessage.Text = String.Empty;
            //TextBoxMessage.Focus();
            String searchValue = "1";
            int rowIndex = -1;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(searchValue))
                {
                    rowIndex = row.Index;
                    Console.WriteLine(rowIndex.ToString());
                    break;
                }
            }



            //dt.Columns["colStatus"].Expression = String.Format("IIF(colBestBefore < #{0}#, 'Ok','Not ok')", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));




        }
        public static class Camera
        {
            public static long cameraId { get; set; }
            public static Pose pose { get; set; }
            public static DangerLevel dangerLevel { get; set; }
            public static DateTime timestamp { get; set; }

            static Camera() {

            }
        }
        

        /// <summary>
        /// Creates and connects the hub connection and hub proxy. This method
        /// is called asynchronously from SignInButton_Click.
        /// </summary>
        private async void ConnectAsync()
        {
            
            Connection = new HubConnection(ServerURI);
            Connection.Closed += Connection_Closed;
            HubProxy = Connection.CreateHubProxy("FramesHub");
            //Handle incoming event from server: use Invoke to write to console from SignalR's thread
            HubProxy.On<long, Pose, DangerLevel, DateTime>("broadcastPoseChanged", (cameraId, pose, dangerLevel, timestamp) =>
               {
               Camera.cameraId = cameraId;
               Camera.pose = pose;
               Camera.dangerLevel = dangerLevel;
               Camera.timestamp = timestamp;
                   if (IsHandleCreated)
                       Invoke(new EventHandler(delegate
                       {
                           //label2.Text= cameraId.ToString();
                           //label3.Text = pose.ToString();
                           //label4.Text = dangerLevel.ToString();
                           //label5.Text = timestamp.ToString();

                           
                           switch (dangerLevel)
                           {
                               case DangerLevel.Undefined:
                                   icon_colour = Color.Gray;
                                   break;
                               case DangerLevel.Normal:
                                   icon_colour = Color.Green;
                                   break;
                               case DangerLevel.Acceptable:
                                   icon_colour = Color.DarkGreen;
                                   break;
                               case DangerLevel.Suspicious:
                                   icon_colour = Color.Yellow;
                                   break;
                               case DangerLevel.Warning:
                                   icon_colour = Color.Orange;
                                   break;
                               case DangerLevel.Danger:
                                   icon_colour = Color.DarkOrange;
                                   break;
                               case DangerLevel.Alarm:
                                   icon_colour = Color.Red;
                                   break;
                            
                               default:
                                   Console.WriteLine("Default case");
                                   break;
                           }

                           int rowIndex = -1;

                           //DataGridViewRow row = dataGridView1.Rows
                           //    .Cast<DataGridViewRow>()
                           //    .Where(r => r.Cells["Id"].Value.ToString().Equals(cameraId.ToString()))
                           //    .First();

                           //rowIndex = row.Index;
                           rowIndex = Convert.ToInt32(cameraId) - 73;
                           //Console.WriteLine(rowIndex.ToString());
                           dataGridView1.Rows[rowIndex].Cells[0].Value = cameraId;
                           dataGridView1.Rows[rowIndex].Cells[2].Value = pose;
                           dataGridView1.Rows[rowIndex].Cells[3].Value = dangerLevel;
                           dataGridView1.Rows[rowIndex].Cells[4].Value = timestamp;

                           //dataGridView1.Rows[rowIndex].Cells[5].Value =Convert.ToInt32(dangerLevel.ToString("D"));
                           dataGridView1.Rows[rowIndex].DefaultCellStyle.BackColor = icon_colour;
                           //dataGridView1.Rows[rowIndex].Cells[1].Style.BackColor = icon_colour;
                           //dataGridView1.Rows[rowIndex].Cells[2].Style.BackColor = icon_colour;
                           //dataGridView1.Rows[rowIndex].Cells[3].Style.BackColor = icon_colour;
                           //dataGridView1.Rows[rowIndex].Cells[4].Style.BackColor = icon_colour;

                           // dataGridView1.DataSource = dt;
                           //dt.Sort(dataGridView1.Columns[5], ListSortDirection.Ascending);

                           dataGridView1.Update();
                           //dataGridView1.Refresh();
                       }));
               });


            //            HubProxy.On<long, Pose, DangerLevel, DateTime>("broadcastPoseChanged", (cameraId, pose, dangerLevel, timestamp) =>
            //   this.Invoke((Action)(() =>
            //       RichTextBoxConsole.AppendText(String.Format("Camera id:{0},Pose: {1},Danger level: {2},Timestamp: {3}", cameraId.ToString() + System.Environment.NewLine, pose + System.Environment.NewLine, dangerLevel + System.Environment.NewLine, timestamp + System.Environment.NewLine))
            //   ))
            //);


            try
            {
                await Connection.Start();
            }
            catch (HttpRequestException)
            {
                StatusText.Text = "Unable to connect to server: Start server before connecting clients.";
                //No connection: Don't enable Send button or show chat UI
                return;
            }

            //Activate UI
            SignInPanel.Visible = false;
            ChatPanel.Visible = true;
            //ButtonSend.Enabled = true;
           // TextBoxMessage.Focus();
           
        }


        private void Connection_Closed()
        {
            //Deactivate chat UI; show login UI. 
            this.Invoke((Action)(() => ChatPanel.Visible = false));
          //  this.Invoke((Action)(() => ButtonSend.Enabled = false));
            this.Invoke((Action)(() => StatusText.Text = "You have been disconnected."));
            this.Invoke((Action)(() => SignInPanel.Visible = true));
        }

        private void SignInButton_Click(object sender, EventArgs e)
        {
            UserName = UserNameTextBox.Text;
            //Connect to server (use async method to avoid blocking UI thread)
            if (!String.IsNullOrEmpty(UserName))
            {
                StatusText.Visible = true;
                StatusText.Text = "Connecting to server...";
                ConnectAsync();
            }
        }

        private void WinFormsClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Connection != null)
            {
                Connection.Stop();
                Connection.Dispose();
            }
        }
    }
}
