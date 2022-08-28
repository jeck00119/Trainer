using Memory;
using MetroFramework.Forms;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Threading;
using System.Windows.Forms;




namespace Trainer
{
    public partial class Trainer_Form : MetroForm
    {

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool VirtualFreeEx(
           IntPtr hProcess,
           UIntPtr lpAddress,
           UIntPtr dwSize,
           uint dwFreeType
       );


        [DllImport("User32.dll")]
        private static extern short GetAsyncKeyState(System.Int32 vKey);



        Mem mem = new Mem();

        public IntPtr getHandle()
        {
            Process[] processes = Process.GetProcessesByName("ac_client");
            if (processes.Length != 0)
            {
                IntPtr pHandle = processes[0].Handle;
                return pHandle;
            }
            return IntPtr.Zero;
        }

        string ammoAdress = "ac_client.exe+0x00183828,8,190,428";
        string hpAdress = "ac_client.exe+0x00183828,8,C88,B54";
        bool ProcOpen = false;
        bool alreadyDown = false;

        public Trainer_Form()
        {
            InitializeComponent();
        }


        string FloatToHex(float f)
        {
            var bytes = BitConverter.GetBytes(f);
            var i = BitConverter.ToInt32(bytes, 0);
            return i.ToString("X8");
        }


        public static byte[] HexToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public byte[] ModifValue(int TrackBar)
        {
            var OriginalValue = FloatToHex((float)TrackBar);
            var ReverseHex = new SoapHexBinary(SoapHexBinary.Parse(OriginalValue).Value.Reverse().ToArray()).ToString();
            var HexToBArray = HexToByteArray(ReverseHex);
            return HexToBArray;
        }


        public class Globals
        {
            public UIntPtr codecavebase;
            public byte[] originalBytes_jump = { 0xC7, 0x46, 0x18, 0x00, 0x00, 0x00, 0x40 };
            public byte[] originalBytes_damage = { 0x89, 0x82, 0xEC, 0x00, 0x00, 0x00 };
            public byte[] newBytes_jump = { 0xC7, 0x46, 0x18, 0x00, 0x00, 0x00, 0x40 };
            public byte[] newBytes_damage = { 0xB8, 0xF4, 0x01, 0x00, 0x00, 0x89, 0x82, 0xEC, 0x00, 0x00, 0x00 };


            public UIntPtr Codecavebase { get => codecavebase; set => codecavebase = value; }
            public byte[] NewBytes_jump { get => newBytes_jump; set => newBytes_jump = value; }
            public byte[] NewBytes_damage { get => newBytes_damage; set => newBytes_damage = value; }
            public byte[] OriginalBytes_jump { get => originalBytes_jump; }
            public byte[] OriginalBytes_damage { get => originalBytes_damage; }
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            trackBar_Jump.ValueChanged += trackBar_Jump_ValueChanged;
        }

        private void BGWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            ProcOpen = mem.OpenProcess("ac_client");
            if (!ProcOpen)
            {
                Thread.Sleep(100);
                return;
            }

            Thread.Sleep(100);
            BGWorker.ReportProgress(0);

        }

        private void Trainer_Form_Shown(object sender, EventArgs e)
        {
            BGWorker.RunWorkerAsync();
        }

        
        private void BGWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            if (ProcOpen)
            {
                ProcOpenLabel.ForeColor = System.Drawing.Color.Green;
                ProcOpenLabel.Text = "Game Found";
                Infinite_Ammo.AutoCheck = true;
                Infinite_Health.AutoCheck = true;
                Jump.AutoCheck = true;
                No_Damage.AutoCheck = true;

                if (GetAsyncKeyState((int)Keys.NumPad1) < 0 && alreadyDown == false)
                {
                    alreadyDown = true;
                    Infinite_Health.Checked = !Infinite_Health.Checked;
                }
                if (GetAsyncKeyState((int)Keys.NumPad1) == 0 && alreadyDown == true)
                {
                    alreadyDown = false;

                }


            }

            if (Infinite_Ammo.Checked)
            {
                mem.WriteMemory(ammoAdress, "int", "9999");
            }

            if (Infinite_Health.Checked)
            {
                mem.WriteMemory(hpAdress, "int", "1000");
            }
        }

        private void BGWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (!ProcOpen)
            {
                ProcOpenLabel.ForeColor = System.Drawing.Color.Red;
                ProcOpenLabel.Text = "Game Not Found";
                Infinite_Ammo.Checked = false;
                Infinite_Ammo.AutoCheck = false;
                Infinite_Health.Checked = false;
                Infinite_Health.AutoCheck = false;
                Jump.Checked = false;
                Jump.AutoCheck = false;
                No_Damage.Checked = false;
                No_Damage.AutoCheck = false;


            }

            BGWorker.RunWorkerAsync();
        }

        private void trackBar_Jump_ValueChanged(object sender, System.EventArgs e)
        {
            var Modif_Jump = ModifValue(trackBar_Jump.Value);
            jump.NewBytes_jump = new byte[] { 0xC7, 0x46, 0x18, Modif_Jump[0], Modif_Jump[1], Modif_Jump[2], Modif_Jump[3] };

            mem.WriteBytes("ac_client.exe+C2486", jump.originalBytes_jump);
            VirtualFreeEx(getHandle(), (UIntPtr)jump.Codecavebase, (UIntPtr)0, 0x8000);

            if (Jump.Checked == true)
            {
                jump.Codecavebase = mem.CreateCodeCave("ac_client.exe+C2486", jump.newBytes_jump, 7, 1000);
            }

            if (Jump.Checked == false)
            {
                mem.WriteBytes("ac_client.exe+C2486", jump.originalBytes_jump);
                VirtualFreeEx(getHandle(), (UIntPtr)jump.Codecavebase, (UIntPtr)0, 0x8000);
            }

        }

        private void Jump_CheckedChanged(object sender, EventArgs e)
        {
            if (Jump.Checked == true)
            {
                jump.Codecavebase = mem.CreateCodeCave("ac_client.exe+C2486", jump.newBytes_jump, 7, 1000);
            }

            if (Jump.Checked == false)
            {
                mem.WriteBytes("ac_client.exe+C2486", jump.originalBytes_jump);
                VirtualFreeEx(getHandle(), (UIntPtr)jump.Codecavebase, (UIntPtr)0, 0x8000);
            }
        }

        private void No_Damage_CheckedChanged(object sender, EventArgs e)
        {
            if (No_Damage.Checked == true)
            {
                damage.Codecavebase = mem.CreateCodeCave("ac_client.exe+84499", damage.NewBytes_damage, 6, 1000);
            }


            if (No_Damage.Checked == false)
            {
                mem.WriteBytes("ac_client.exe+84499", damage.originalBytes_damage);
                VirtualFreeEx(getHandle(), (UIntPtr)damage.Codecavebase, (UIntPtr)0, 0x8000);
            }
        }




        Globals jump = new Globals();
        Globals damage = new Globals();


    }
}
