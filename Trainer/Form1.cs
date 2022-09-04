using Memory;
using MetroFramework.Controls;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Input;

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



        static Mem mem = new Mem();




        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory(IntPtr process, IntPtr baseAddress, [Out] byte[] buffer, int size,
        out IntPtr bytesRead);


        public static long ReadInt64(IntPtr process, IntPtr baseAddress)
        {
            var buffer = new byte[8];
            IntPtr bytesRead;
            ReadProcessMemory(process, baseAddress, buffer, 8, out bytesRead);
            return BitConverter.ToInt64(buffer, 0);
        }

        private static ProcessModule GetProcessModule(Process process, string moduleName)
        {
            foreach (ProcessModule module in process.Modules)
            {
                if (module.ModuleName == moduleName)
                {
                    return module;
                }
            }
            return null;
        }

        private byte[] Convert64AddressToByteArray(long address)
        {
            string hexString = address.ToString("X");
            hexString = hexString.Remove(0, 2); //64 bits -> 32 bits
            var BigEndian = IPAddress.HostToNetworkOrder(long.Parse(hexString, NumberStyles.HexNumber)).ToString("X");
            var HexToBArray = HexToByteArray(BigEndian);
            return HexToBArray;
        }

        public static long GetRealAddress(IntPtr process, IntPtr baseAddress, int[] offsets)
        {
            var address = baseAddress.ToInt64();
            foreach (var offset in offsets)
            {
                address = ReadInt64(process, (IntPtr)address) + offset;
            }
            return address;
        }

        bool ProcOpen = false;
        bool AlreadyDown = false;

        public Trainer_Form()
        {
            InitializeComponent();
        }

        private void BGWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            ProcOpen = mem.OpenProcess("ffxiv_dx11");
            if (!ProcOpen)
            {
                Thread.Sleep(100);
                return;
            }

            Thread.Sleep(100);
            BGWorker.ReportProgress(0);

        }


        string FloatToHex(float f)
        {
            var bytes = BitConverter.GetBytes(f);
            var i = BitConverter.ToInt32(bytes, 0);
            return i.ToString("X8");
        }


        public static byte[] HexToByteArray(String hex)
        {
            byte[] bytes = new byte[hex.Length / 2];

            for (int i = 0; i < hex.Length; i += 2)
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


        public static IEnumerable<Key> KeysDown()
        {
            foreach (Key key in Enum.GetValues(typeof(Key)))
            {
                if (key != Key.None && Keyboard.IsKeyDown(key))

                    yield return key;
            }
        }

        public void CheckBoxToggleOption(CheckBox Checkbox, Key Key)
        {
            if (Keyboard.IsKeyDown(Key) && KeysDown().Count() == 1 && AlreadyDown == false)
            {
                AlreadyDown = true;
                Checkbox.Checked = !Checkbox.Checked;
            }
            if (Keyboard.IsKeyUp(Key) && KeysDown().Count() == 0 && AlreadyDown == true)
            {
                AlreadyDown = false;

            }

        }

        public void ButtonActivateOption(MetroButton button, Key Key)
        {
            if (Keyboard.IsKeyDown(Key) && KeysDown().Count() == 1 && AlreadyDown == false)
            {
                AlreadyDown = true;
                button.PerformClick();
            }
            if (Keyboard.IsKeyUp(Key) && KeysDown().Count() == 0 && AlreadyDown == true)
            {
                AlreadyDown = false;

            }
        }

        public void CheckKeyCombination(Key Key1, Key Key2)
        {
            if (Keyboard.IsKeyDown(Key1) && Keyboard.IsKeyDown(Key2) && KeysDown().Count() == 2 && AlreadyDown == false)
            {
                AlreadyDown = true;

                if (Key1 == Key.LeftCtrl)
                    SaveLocation(Key2);

                else if (Key1 == Key.LeftAlt)
                    LoadLocation(Key2);

                else if (Key1 == Key.LeftAlt && Key2 == Key.F)
                    Fly_NoClip.Checked = !Fly_NoClip.Checked;

            }

            if (Keyboard.IsKeyDown(Key1) && Keyboard.IsKeyUp(Key2) && (KeysDown().Count() == 1 || KeysDown().Count() == 0) && AlreadyDown == true)
            {
                AlreadyDown = false;
            }

        }

        public void SaveLocation(Key Key)
        {
            teleport.X = mem.ReadFloat("ffxiv_dx11.exe+02043638,A0");
            teleport.Y = mem.ReadFloat("ffxiv_dx11.exe+02043638,A8");
            teleport.Z = mem.ReadFloat("ffxiv_dx11.exe+02043638,A4");


            if (!(teleport.X == 0f && teleport.Y == 0f))
            {

                if (Key == Key.NumPad1)
                {
                    teleport.Xpos1 = teleport.X;
                    teleport.Ypos1 = teleport.Y;
                    teleport.Zpos1 = teleport.Z;
                }
                else if (Key == Key.NumPad2)
                {
                    teleport.Xpos2 = teleport.X;
                    teleport.Ypos2 = teleport.Y;
                    teleport.Zpos2 = teleport.Z;
                }
                else if (Key == Key.NumPad3)
                {
                    teleport.Xpos3 = teleport.X;
                    teleport.Ypos3 = teleport.Y;
                    teleport.Zpos3 = teleport.Z;
                }
                else if (Key == Key.NumPad4)
                {
                    teleport.Xpos4 = teleport.X;
                    teleport.Ypos4 = teleport.Y;
                    teleport.Zpos4 = teleport.Z;
                }
                else if (Key == Key.NumPad5)
                {
                    teleport.Xpos5 = teleport.X;
                    teleport.Ypos5 = teleport.Y;
                    teleport.Zpos5 = teleport.Z;
                }
                else if (Key == Key.NumPad6)
                {
                    teleport.Xpos6 = teleport.X;
                    teleport.Ypos6 = teleport.Y;
                    teleport.Zpos6 = teleport.Z;
                }
                else if (Key == Key.NumPad7)
                {
                    teleport.Xpos7 = teleport.X;
                    teleport.Ypos7 = teleport.Y;
                    teleport.Zpos7 = teleport.Z;
                }
                else if (Key == Key.NumPad8)
                {
                    teleport.Xpos8 = teleport.X;
                    teleport.Ypos8 = teleport.Y;
                    teleport.Zpos8 = teleport.Z;
                }
                else if (Key == Key.NumPad9)
                {
                    teleport.Xpos9 = teleport.X;
                    teleport.Ypos9 = teleport.Y;
                    teleport.Zpos9 = teleport.Z;
                }
            }
        }

        public void LoadLocation(Key Key)
        {
            string Xaddress = "ffxiv_dx11.exe+02043638,A0";
            string Yaddress = "ffxiv_dx11.exe+02043638,A8";
            string Zaddress = "ffxiv_dx11.exe+02043638,A4";

            if (!(teleport.X == 0f && teleport.Y == 0f))
            {

                if (Key == Key.NumPad1)
                {

                    mem.WriteMemory(Xaddress, "float", teleport.Xpos1.ToString());
                    mem.WriteMemory(Yaddress, "float", teleport.Ypos1.ToString());
                    mem.WriteMemory(Zaddress, "float", teleport.Zpos1.ToString());
                }
                else if (Key == Key.NumPad2)
                {
                    mem.WriteMemory(Xaddress, "float", teleport.Xpos2.ToString());
                    mem.WriteMemory(Yaddress, "float", teleport.Ypos2.ToString());
                    mem.WriteMemory(Zaddress, "float", teleport.Zpos2.ToString());
                }
                else if (Key == Key.NumPad3)
                {
                    mem.WriteMemory(Xaddress, "float", teleport.Xpos3.ToString());
                    mem.WriteMemory(Yaddress, "float", teleport.Ypos3.ToString());
                    mem.WriteMemory(Zaddress, "float", teleport.Zpos3.ToString());
                }
                else if (Key == Key.NumPad4)
                {
                    mem.WriteMemory(Xaddress, "float", teleport.Xpos4.ToString());
                    mem.WriteMemory(Yaddress, "float", teleport.Ypos4.ToString());
                    mem.WriteMemory(Zaddress, "float", teleport.Zpos4.ToString());
                }
                else if (Key == Key.NumPad5)
                {
                    mem.WriteMemory(Xaddress, "float", teleport.Xpos5.ToString());
                    mem.WriteMemory(Yaddress, "float", teleport.Ypos5.ToString());
                    mem.WriteMemory(Zaddress, "float", teleport.Zpos5.ToString());
                }
                else if (Key == Key.NumPad6)
                {
                    mem.WriteMemory(Xaddress, "float", teleport.Xpos6.ToString());
                    mem.WriteMemory(Yaddress, "float", teleport.Ypos6.ToString());
                    mem.WriteMemory(Zaddress, "float", teleport.Zpos6.ToString());
                }
                else if (Key == Key.NumPad7)
                {
                    mem.WriteMemory(Xaddress, "float", teleport.Xpos7.ToString());
                    mem.WriteMemory(Yaddress, "float", teleport.Ypos7.ToString());
                    mem.WriteMemory(Zaddress, "float", teleport.Zpos7.ToString());
                }
                else if (Key == Key.NumPad8)
                {
                    mem.WriteMemory(Xaddress, "float", teleport.Xpos8.ToString());
                    mem.WriteMemory(Yaddress, "float", teleport.Ypos8.ToString());
                    mem.WriteMemory(Zaddress, "float", teleport.Zpos8.ToString());
                }
                else if (Key == Key.NumPad9)
                {
                    mem.WriteMemory(Xaddress, "float", teleport.Xpos9.ToString());
                    mem.WriteMemory(Yaddress, "float", teleport.Ypos9.ToString());
                    mem.WriteMemory(Zaddress, "float", teleport.Zpos9.ToString());
                }
            }

        }


        public class ProcessInfo
        {

            private IntPtr processHandle = mem.mProc.Handle;
            private static ProcessModule processModule = mem.mProc.MainModule;
            private long processBaseAddress64 = ProcessModule.BaseAddress.ToInt64();

            public IntPtr ProcessHandle { get => processHandle; set => processHandle = value; }
            public static ProcessModule ProcessModule { get => processModule; set => processModule = value; }
            public long ProcessBaseAddress64 { get => processBaseAddress64; set => processBaseAddress64 = value; }
        }


        public class Globals
        {

            
            private bool once = false;
            private bool reattachProcess = false;
            private int[] pointerOffsets = { 0x00 };
            private long baseAddress;
            private long pointerAddress;
            private byte[] modifBytes;
            private float wpX;
            private float wpY;
            private float x;
            private float y;
            private float z;
            private float xpos1;
            private float ypos1;
            private float zpos1;
            private float xpos2;
            private float ypos2;
            private float zpos2;
            private float xpos3;
            private float ypos3;
            private float zpos3;
            private float xpos4;
            private float ypos4;
            private float zpos4;
            private float xpos5;
            private float ypos5;
            private float zpos5;
            private float xpos6;
            private float ypos6;
            private float zpos6;
            private float xpos7;
            private float ypos7;
            private float zpos7;
            private float xpos8;
            private float ypos8;
            private float zpos8;
            private float xpos9;
            private float ypos9;
            private float zpos9;

          
            public UIntPtr codecavebase;

            private byte[] originalBytes_zoom = { 0xF3, 0x0F, 0x10, 0x9B, 0x1C, 0x01, 0x00, 0x00 };
            private byte[] originalBytes_speed = { 0xF3, 0x0F, 0x11, 0x73, 0x44, 0x0F };
            private byte[] originalBytes_jump_height = { 0xF3, 0x0F, 0x11, 0xB3, 0xE4, 0x00, 0x00, 0x00, 0x40 };
            private byte[] originalBytes_jump_collision = { 0x89, 0x8B, 0xD8, 0x00, 0x00, 0x00, 0x48, 0x8B, 0xCB };
            private byte[] originalBytes_wpTP = { 0x8B, 0x10, 0x89, 0x53, 0x30 };
            private byte[] originalBytes_NopZ = { 0xF3, 0x0F, 0x11, 0x91, 0xA4, 0x00, 0x00, 0x00 };
            private byte[] originalBytes_NoGravity = { 0x8B, 0x81, 0xD0, 0x00, 0x00, 0x00 };
            private byte[] originalBytes_NoCollisionBarriers = { 0x4C, 0x8B, 0xDC, 0x55, 0x56 };
            private byte[] originalBytes_NoTerrainCollision = { 0x48, 0x8B, 0xC4, 0x48, 0x89, 0x58, 0x08 };

            private byte[] newBytes_zoom = { 0x50, 0xB8, 0x00, 0x00, 0x7A, 0x44, 0x89, 0x83, 0x1C, 0x01, 0x00, 0x00, 0x58, 0xF3, 0x0F, 0x10, 0x9B, 0x1C, 0x01, 0x00, 0x00 };
            private byte[] newBytes_speed = { 0x50, 0xB8, 0x00, 0x00, 0x20, 0x41, 0x89, 0x43, 0x44, 0x58 };
            private byte[] newBytes_jump_height = { 0x50, 0xB8, 0x00, 0x00, 0x20, 0x41, 0x89, 0x83, 0xE4, 0x00, 0x00, 0x00, 0x58 };
            private byte[] newBytes_jump_collision = { 0xC7, 0x83, 0xD8, 0x00, 0x00, 0x00, 0x80, 0x4F, 0xC3, 0x47 };
            private byte[] newBytes_wpTP = { 0xC7, 0x40, 0x04, 0x00, 0x00, 0xC8, 0x42, 0xC7, 0x00, 0x00, 0x00, 0x02, 0x43, 0xC7, 0x40, 0x08, 0x00, 0x80, 0x8C, 0x43, 0x8B, 0x10, 0x89, 0x53, 0x30 };
            private byte[] newBytes_NopZ = { 0x81, 0xF9, 0x50, 0xA0, 0x88, 0xCE, 0x75, 0x10, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x81, 0xF9, 0x39, 0x1B, 0x00, 0x00, 0x75, 0x08, 0xF3, 0x0F, 0x11, 0x91, 0xA4, 0x00, 0x00, 0x00 };
            private byte[] newBytes_NoGravity = { 0xC7, 0x81, 0xD0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x8B, 0x81, 0xD0, 0x00, 0x00, 0x00 };
            private byte[] newBytes_NoCollisionBarriers = { 0xC3, 0x90, 0x90, 0x55, 0x56 };
            private byte[] newBytes_NoTerrainCollision = { 0xC3, 0x90, 0x90, 0x48, 0x89, 0x58, 0x08 };

            public float WpX { get => WpX1; set => WpX1 = value; }
            public float WpY { get => WpY1; set => WpY1 = value; }
            public bool Once { get => once; set => once = value; }
            public UIntPtr Codecavebase { get => codecavebase; set => codecavebase = value; }
            public byte[] OriginalBytes_zoom { get => originalBytes_zoom; set => originalBytes_zoom = value; }
            public byte[] OriginalBytes_speed { get => originalBytes_speed; set => originalBytes_speed = value; }
            public byte[] OriginalBytes_jump_height { get => originalBytes_jump_height; set => originalBytes_jump_height = value; }
            public byte[] OriginalBytes_jump_collision { get => originalBytes_jump_collision; set => originalBytes_jump_collision = value; }
            public byte[] OriginalBytes_wpTP { get => originalBytes_wpTP; set => originalBytes_wpTP = value; }
            public byte[] NewBytes_zoom { get => newBytes_zoom; set => newBytes_zoom = value; }
            public byte[] NewBytes_speed { get => newBytes_speed; set => newBytes_speed = value; }
            public byte[] NewBytes_jump_height { get => newBytes_jump_height; set => newBytes_jump_height = value; }
            public byte[] NewBytes_jump_collision { get => newBytes_jump_collision; set => newBytes_jump_collision = value; }
            public byte[] NewBytes_wpTP { get => newBytes_wpTP; set => newBytes_wpTP = value; }
            public byte[] OriginalBytes_NopZ { get => originalBytes_NopZ; set => originalBytes_NopZ = value; }
            public byte[] OriginalBytes_NoGravity { get => originalBytes_NoGravity; set => originalBytes_NoGravity = value; }
            public byte[] OriginalBytes_NoCollisionBarriers { get => originalBytes_NoCollisionBarriers; set => originalBytes_NoCollisionBarriers = value; }
            public byte[] OriginalBytes_NoTerrainCollision { get => originalBytes_NoTerrainCollision; set => originalBytes_NoTerrainCollision = value; }
            public byte[] NewBytes_NopZ { get => newBytes_NopZ; set => newBytes_NopZ = value; }
            public byte[] NewBytes_NoGravity { get => newBytes_NoGravity; set => newBytes_NoGravity = value; }
            public byte[] NewBytes_NoCollisionBarriers { get => newBytes_NoCollisionBarriers; set => newBytes_NoCollisionBarriers = value; }
            public byte[] NewBytes_NoTerrainCollision { get => newBytes_NoTerrainCollision; set => newBytes_NoTerrainCollision = value; }
            public float WpX1 { get => wpX; set => wpX = value; }
            public float WpY1 { get => wpY; set => wpY = value; }
            public float Xpos1 { get => xpos1; set => xpos1 = value; }
            public float Ypos1 { get => ypos1; set => ypos1 = value; }
            public float Zpos1 { get => zpos1; set => zpos1 = value; }
            public float Xpos2 { get => xpos2; set => xpos2 = value; }
            public float Ypos2 { get => ypos2; set => ypos2 = value; }
            public float Zpos2 { get => zpos2; set => zpos2 = value; }
            public float Xpos3 { get => xpos3; set => xpos3 = value; }
            public float Ypos3 { get => ypos3; set => ypos3 = value; }
            public float Zpos3 { get => zpos3; set => zpos3 = value; }
            public float Xpos4 { get => xpos4; set => xpos4 = value; }
            public float Ypos4 { get => ypos4; set => ypos4 = value; }
            public float Zpos4 { get => zpos4; set => zpos4 = value; }
            public float Xpos5 { get => xpos5; set => xpos5 = value; }
            public float Ypos5 { get => ypos5; set => ypos5 = value; }
            public float Zpos5 { get => zpos5; set => zpos5 = value; }
            public float Xpos6 { get => xpos6; set => xpos6 = value; }
            public float Ypos6 { get => ypos6; set => ypos6 = value; }
            public float Zpos6 { get => zpos6; set => zpos6 = value; }
            public float Xpos7 { get => xpos7; set => xpos7 = value; }
            public float Ypos7 { get => ypos7; set => ypos7 = value; }
            public float Zpos7 { get => zpos7; set => zpos7 = value; }
            public float Xpos8 { get => xpos8; set => xpos8 = value; }
            public float Ypos8 { get => ypos8; set => ypos8 = value; }
            public float Zpos8 { get => zpos8; set => zpos8 = value; }
            public float Xpos9 { get => xpos9; set => xpos9 = value; }
            public float Ypos9 { get => ypos9; set => ypos9 = value; }
            public float Zpos9 { get => zpos9; set => zpos9 = value; }
            public float X { get => x; set => x = value; }
            public float Y { get => y; set => y = value; }
            public float Z { get => z; set => z = value; }
            public int[] PointerOffsets { get => pointerOffsets; set => pointerOffsets = value; }
            public long BaseAddress { get => baseAddress; set => baseAddress = value; }
            public long PointerAddress { get => pointerAddress; set => pointerAddress = value; }
            public byte[] ModifBytes { get => modifBytes; set => modifBytes = value; }
            public bool ReattachProcess { get => reattachProcess; set => reattachProcess = value; }
        }


        public class AOBscan
        {
            private string aobResult_zoom;
            private string aobResult_speed;
            private string aobResult_jump;
            private string aobResult_jump_collision;
            private string aobResult_wpTP;
            private string aobResult_NopZ;
            private string aobResult_NoGravity;
            private string aobResult_NoCollisionBarrier;
            private string aobResult_NoTerrainCollision;

            public string AobResult_zoom { get => aobResult_zoom; set => aobResult_zoom = value; }
            public string AobResult_speed { get => aobResult_speed; set => aobResult_speed = value; }
            public string AobResult_jump_height { get => aobResult_jump; set => aobResult_jump = value; }
            public string AobResult_jump_collision { get => aobResult_jump_collision; set => aobResult_jump_collision = value; }
            public string AobResult_wpTP { get => aobResult_wpTP; set => aobResult_wpTP = value; }
            public string AobResult_NopZ { get => aobResult_NopZ; set => aobResult_NopZ = value; }
            public string AobResult_NoGravity { get => aobResult_NoGravity; set => aobResult_NoGravity = value; }
            public string AobResult_NoCollisionBarrier { get => aobResult_NoCollisionBarrier; set => aobResult_NoCollisionBarrier = value; }
            public string AobResult_NoTerrainCollision { get => aobResult_NoTerrainCollision; set => aobResult_NoTerrainCollision = value; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            trackBar_Jump.ValueChanged += trackBar_Jump_ValueChanged;
        }



        private void Trainer_Form_Shown(object sender, EventArgs e)
        {
            BGWorker.RunWorkerAsync();
        }

        float inc_Z = 0;
        float dec_Z = 0;

        private void BGWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            if (ProcOpen)
            {

                ProcOpenLabel.ForeColor = System.Drawing.Color.Green;
                ProcOpenLabel.Text = "Game Found";

                if (!globals.ReattachProcess)
                {
                    Zoom.AutoCheck = true;
                    Walk_Speed.AutoCheck = true;
                    trackBar_Speed.Enabled = true;
                    trackBar_Jump.Enabled = true;
                    Jump.AutoCheck = true;
                    wpTP_Button.Enabled = true;
                    Teleport.AutoCheck = true;
                    Fly_NoClip.AutoCheck = true;

                    globals.ReattachProcess = true;
                }

                if (!Walk_Speed.Checked)
                    trackBar_Speed.Enabled = false;
                else
                    trackBar_Speed.Enabled = true;

                if (!Jump.Checked)
                    trackBar_Jump.Enabled = false;
                else
                    trackBar_Jump.Enabled = true;

                if(!globals.Once)
                {

                    aobScan.AobResult_zoom = mem.AoBScan("F3 0F 10 9B 1C 01 00 00 4C 8D 45 C7", false, true).Result.FirstOrDefault().ToString("X");

                    aobScan.AobResult_speed = mem.AoBScan("F3 0F 11 73 44 0F", false, true).Result.FirstOrDefault().ToString("X");

                    aobScan.AobResult_jump_height = mem.AoBScan("F3 0F 11 B3 E4 00 00 00 40", false, true).Result.FirstOrDefault().ToString("X");
                    aobScan.AobResult_jump_collision = mem.AoBScan("89 8B D8 00 00 00 48 8B CB", false, true).Result.FirstOrDefault().ToString("X");

                    aobScan.AobResult_wpTP = mem.AoBScan("8B 10 89 53 30 8B 50 04 89 53 34 8B 40 08 89 43 38 48 83 C4 20 5B C3 C6 43 20 01 45 33 C9", false, true).Result.FirstOrDefault().ToString("X");

                    aobScan.AobResult_NopZ = mem.AoBScan("F3 0F 11 91 A4 00 00 00 F3 0F 11 99 A8 00 00 00 F6 81 ?? ?? ?? ?? ??", false, true).Result.FirstOrDefault().ToString("X");
                    aobScan.AobResult_NoGravity = mem.AoBScan("8B 81 D0 00 00 00 83 E8 02 83 F8 02 0F", false, true).Result.FirstOrDefault().ToString("X");
                    aobScan.AobResult_NoCollisionBarrier = mem.AoBScan("4C 8B DC 55 56 57 41 55 41 56 49 8D AB ?? FE FF FF 48 81 EC ?? 02 00 00 F3 0F 10 05 ?? ?? ?? 00 48 8D 82 90 06 00 00 F3 41 0F 10 21", false, true).Result.FirstOrDefault().ToString("X");
                    aobScan.AobResult_NoTerrainCollision = mem.AoBScan("48 8B C4 48 89 58 08 48 89 70 10 48 89 78 18 4C 89 70 20 55 48 8D 68 B9 48 81 EC 90 00 00 00 F3 0F ?? ?? ?? ?? ?? ?? 4D 8B F1 48 8B 5D 77 49 8B F8", false, true).Result.FirstOrDefault().ToString("X");


                    globals.Once = true;

                }
                

           


                CheckBoxToggleOption(Zoom, Key.NumPad1);
                CheckBoxToggleOption(Walk_Speed, Key.NumPad2);
                CheckBoxToggleOption(Jump, Key.NumPad3);
                ButtonActivateOption(wpTP_Button, Key.NumPad4);
                CheckBoxToggleOption(Teleport, Key.NumPad5);
                CheckBoxToggleOption(Fly_NoClip, Key.NumPad6);

                if (Teleport.Checked == true)
                {   
                    Teleport.ForeColor = System.Drawing.Color.Red;



                    CheckKeyCombination(Key.LeftCtrl, Key.NumPad1);
                    CheckKeyCombination(Key.LeftCtrl, Key.NumPad2);
                    CheckKeyCombination(Key.LeftCtrl, Key.NumPad3);
                    CheckKeyCombination(Key.LeftCtrl, Key.NumPad4);
                    CheckKeyCombination(Key.LeftCtrl, Key.NumPad5);
                    CheckKeyCombination(Key.LeftCtrl, Key.NumPad6);
                    CheckKeyCombination(Key.LeftCtrl, Key.NumPad7);
                    CheckKeyCombination(Key.LeftCtrl, Key.NumPad8);
                    CheckKeyCombination(Key.LeftCtrl, Key.NumPad9);

                    CheckKeyCombination(Key.LeftAlt, Key.NumPad1);
                    CheckKeyCombination(Key.LeftAlt, Key.NumPad2);
                    CheckKeyCombination(Key.LeftAlt, Key.NumPad3);
                    CheckKeyCombination(Key.LeftAlt, Key.NumPad4);
                    CheckKeyCombination(Key.LeftAlt, Key.NumPad5);
                    CheckKeyCombination(Key.LeftAlt, Key.NumPad6);
                    CheckKeyCombination(Key.LeftAlt, Key.NumPad7);
                    CheckKeyCombination(Key.LeftAlt, Key.NumPad8);
                    CheckKeyCombination(Key.LeftAlt, Key.NumPad9);

                        
                }
                else
                    Teleport.ForeColor = System.Drawing.Color.DarkCyan;

                if (Fly_NoClip.Checked == true)
                {
                    
                    fly.Z = mem.ReadFloat("ffxiv_dx11.exe+02043638,A4");
                    string Zaddress = "ffxiv_dx11.exe+02043638,A4";


                    if (Keyboard.IsKeyDown(Key.Space))
                    {
                        inc_Z = fly.Z + 2;
                        mem.WriteMemory(Zaddress, "float", inc_Z.ToString());
                    }
                    else if (Keyboard.IsKeyDown(Key.LeftCtrl))
                    {
                        dec_Z = fly.Z - 2;
                        mem.WriteMemory(Zaddress, "float", dec_Z.ToString());
                    }
                }
                else
                    Fly_NoClip.ForeColor = System.Drawing.Color.DarkCyan;





            }

        }

        private void BGWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (!ProcOpen)
            {
                ProcOpenLabel.ForeColor = System.Drawing.Color.Red;
                ProcOpenLabel.Text = "Game Not Found";

                if (globals.ReattachProcess)
                {

                    Zoom.Checked = false;
                    Zoom.AutoCheck = false;
                    Walk_Speed.Checked = false;
                    Walk_Speed.AutoCheck = false;
                    trackBar_Speed.Enabled = false;
                    Jump.Checked = false;
                    Jump.AutoCheck = false;
                    trackBar_Jump.Enabled = false;
                    wpTP_Button.Enabled = false;
                    Teleport.Checked = false;
                    Teleport.AutoCheck = false;
                    Teleport.ForeColor = System.Drawing.Color.DarkCyan;
                    Fly_NoClip.Checked = false;
                    Fly_NoClip.AutoCheck = false;

                    globals.ReattachProcess = false;
                }
            }
            BGWorker.RunWorkerAsync();
        }




        private void Zoom_CheckedChanged(object sender, EventArgs e)
        {

            if (Zoom.Checked == true)
            {
                zoom.Codecavebase = mem.CreateCodeCave(aobScan.AobResult_zoom, zoom.NewBytes_zoom, 8, 1000);
                Zoom.ForeColor = System.Drawing.Color.Red;

            }

            if (Zoom.Checked == false)
            {
                mem.WriteBytes(aobScan.AobResult_zoom, zoom.OriginalBytes_zoom);
                VirtualFreeEx(mem.mProc.Handle, (UIntPtr)zoom.Codecavebase, (UIntPtr)0, 0x8000);
                Zoom.ForeColor = System.Drawing.Color.DarkCyan;

            }
        }

        private void Walk_Speed_CheckedChanged(object sender, EventArgs e)
        {

            if (Walk_Speed.Checked == true)
            {
                speed.Codecavebase = mem.CreateCodeCave(aobScan.AobResult_speed, speed.NewBytes_speed, 5, 1000);
                Walk_Speed.ForeColor = System.Drawing.Color.Red;
            }

            if (Walk_Speed.Checked == false)
            {
                mem.WriteBytes(aobScan.AobResult_speed, speed.OriginalBytes_speed);
                VirtualFreeEx(mem.mProc.Handle, (UIntPtr)speed.Codecavebase, (UIntPtr)0, 0x8000);
                Walk_Speed.ForeColor = System.Drawing.Color.DarkCyan;
            }
        }

        private void trackBar_Speed_ValueChanged(object sender, EventArgs e)
        {
            var Modif_Jump = ModifValue(trackBar_Speed.Value);
            speed.NewBytes_speed = new byte[] { 0x50, 0xB8, 0x00, 0x00, Modif_Jump[2], Modif_Jump[3], 0x89, 0x43, 0x44, 0x58 };

            mem.WriteBytes(aobScan.AobResult_speed, speed.OriginalBytes_speed);
            VirtualFreeEx(mem.mProc.Handle, (UIntPtr)speed.Codecavebase, (UIntPtr)0, 0x8000);

            speed.Codecavebase = mem.CreateCodeCave(aobScan.AobResult_speed, speed.NewBytes_speed, 5, 1000);

        }

        private void Jump_CheckedChanged(object sender, EventArgs e)
        {


            if (Jump.Checked == true)
            {
                jump_height.Codecavebase = mem.CreateCodeCave(aobScan.AobResult_jump_height, jump.NewBytes_jump_height, 8, 1000);
                jump_collision.Codecavebase = mem.CreateCodeCave(aobScan.AobResult_jump_collision, jump.NewBytes_jump_collision, 6, 1000);
                Jump.ForeColor = System.Drawing.Color.Red;
            }

            if (Jump.Checked == false)
            {
                mem.WriteBytes(aobScan.AobResult_jump_height, jump.OriginalBytes_jump_height);
                mem.WriteBytes(aobScan.AobResult_jump_collision, jump.OriginalBytes_jump_collision);
                VirtualFreeEx(mem.mProc.Handle, (UIntPtr)jump_height.Codecavebase, (UIntPtr)0, 0x8000);
                VirtualFreeEx(mem.mProc.Handle, (UIntPtr)jump_collision.Codecavebase, (UIntPtr)0, 0x8000);
                Jump.ForeColor = System.Drawing.Color.DarkCyan;
            }
        }

        private void trackBar_Jump_ValueChanged(object sender, EventArgs e)
        {
            var Modif_Jump = ModifValue(trackBar_Jump.Value);
            jump.NewBytes_jump_height = new byte[] { 0x50, 0xB8, 0x00, 0x00, Modif_Jump[2], Modif_Jump[3], 0x89, 0x83, 0xE4, 0x00, 0x00, 0x00, 0x58 };

            mem.WriteBytes(aobScan.AobResult_jump_height, jump.OriginalBytes_jump_height);
            VirtualFreeEx(mem.mProc.Handle, (UIntPtr)jump_height.Codecavebase, (UIntPtr)0, 0x8000);

            jump_height.Codecavebase = mem.CreateCodeCave(aobScan.AobResult_jump_height, jump.NewBytes_jump_height, 8, 1000);

        }


        private void wpTP_Button_Click(object sender, EventArgs e)
        {


            waypoint.WpX = mem.ReadFloat("ffxiv_dx11.exe+0201DB70,0x1E8,0x30,0x8,0x37FC");
            waypoint.WpY = mem.ReadFloat("ffxiv_dx11.exe+0201DB70,0x1E8,0x30,0x8,0x37F8");

            if ((waypoint.WpX < 1500f && waypoint.WpY < 1500f) && (waypoint.WpX != 0f && waypoint.WpY != 0f) && (waypoint.WpX > -1500f && waypoint.WpY > -1500f))
            {

                var Modif_WPx = ModifValue((int)waypoint.WpX);
                var Modif_WPy = ModifValue((int)waypoint.WpY);

                waypoint.NewBytes_wpTP = new byte[] { 0xC7, 0x40, 0x04, 0x00, 0x00, 0xC8, 0x42, 0xC7, 0x00, Modif_WPy[0], Modif_WPy[1], Modif_WPy[2], Modif_WPy[3], 0xC7, 0x40, 0x08, Modif_WPx[0], Modif_WPx[1], Modif_WPx[2], Modif_WPx[3], 0x8B, 0x10, 0x89, 0x53, 0x30 };

                waypoint.Codecavebase = mem.CreateCodeCave(aobScan.AobResult_wpTP, waypoint.NewBytes_wpTP, 5, 1000);

                Thread.Sleep(500);
                mem.WriteBytes(aobScan.AobResult_wpTP, waypoint.OriginalBytes_wpTP);
                VirtualFreeEx(mem.mProc.Handle, (UIntPtr)waypoint.Codecavebase, (UIntPtr)0, 0x8000);
            }
        }


        private void Fly_NoClip_CheckedChanged(object sender, EventArgs e)
        {



            if (Fly_NoClip.Checked == true)
            {

                ProcessInfo processInfo = new ProcessInfo();
                fly.BaseAddress = 0x02043638;
                fly.PointerOffsets = new[] { 0x00 };
                fly.BaseAddress = processInfo.ProcessBaseAddress64 + fly.BaseAddress;
                fly.PointerAddress = GetRealAddress(processInfo.ProcessHandle, (IntPtr)fly.BaseAddress, fly.PointerOffsets);
                fly.ModifBytes = Convert64AddressToByteArray(fly.PointerAddress);

                /*
                Console.WriteLine(string.Format("{0:X2}", fly.ModifBytes[0]));
                Console.WriteLine(string.Format("{0:X2}", fly.ModifBytes[1]));
                Console.WriteLine(string.Format("{0:X2}", fly.ModifBytes[2]));
                Console.WriteLine(string.Format("{0:X2}", fly.ModifBytes[3]));
                */
                var Modif_NopZ = fly.ModifBytes;
                                                                                                                        
                fly.NewBytes_NopZ = new byte[] { 0x81, 0xF9, Modif_NopZ[0], Modif_NopZ[1], Modif_NopZ[2], Modif_NopZ[3], 0x75, 0x10, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x90, 0x81, 0xF9, 0x39, 0x1B, 0x00, 0x00, 0x75, 0x08, 0xF3, 0x0F, 0x11, 0x91, 0xA4, 0x00, 0x00, 0x00 };
                nopZ.Codecavebase = mem.CreateCodeCave(aobScan.AobResult_NopZ, fly.NewBytes_NopZ, 8, 1000);
                noGravity.Codecavebase = mem.CreateCodeCave(aobScan.AobResult_NoGravity, fly.NewBytes_NoGravity, 6, 1000);
                noCollisionBarriers.Codecavebase = mem.CreateCodeCave(aobScan.AobResult_NoCollisionBarrier, fly.NewBytes_NoCollisionBarriers, 5, 1000);
                noTerrainCollision.Codecavebase = mem.CreateCodeCave(aobScan.AobResult_NoTerrainCollision, fly.NewBytes_NoTerrainCollision, 7, 1000);
                Fly_NoClip.ForeColor = System.Drawing.Color.Red;

            }

            if (Fly_NoClip.Checked == false)
            {
                mem.WriteBytes(aobScan.AobResult_NopZ, fly.OriginalBytes_NopZ);
                mem.WriteBytes(aobScan.AobResult_NoGravity, fly.OriginalBytes_NoGravity);
                mem.WriteBytes(aobScan.AobResult_NoCollisionBarrier, fly.OriginalBytes_NoCollisionBarriers);
                mem.WriteBytes(aobScan.AobResult_NoTerrainCollision, fly.OriginalBytes_NoTerrainCollision);
                VirtualFreeEx(mem.mProc.Handle, (UIntPtr)nopZ.Codecavebase, (UIntPtr)0, 0x8000);
                VirtualFreeEx(mem.mProc.Handle, (UIntPtr)noGravity.Codecavebase, (UIntPtr)0, 0x8000);
                VirtualFreeEx(mem.mProc.Handle, (UIntPtr)noCollisionBarriers.Codecavebase, (UIntPtr)0, 0x8000);
                VirtualFreeEx(mem.mProc.Handle, (UIntPtr)noTerrainCollision.Codecavebase, (UIntPtr)0, 0x8000);
                Fly_NoClip.ForeColor = System.Drawing.Color.DarkCyan;
     
            }

        }



        AOBscan aobScan = new AOBscan();
        Globals globals = new Globals();
        Globals zoom = new Globals();
        Globals speed = new Globals();
        Globals jump = new Globals();
        Globals jump_height = new Globals();
        Globals jump_collision = new Globals();
        Globals waypoint = new Globals();
        Globals teleport = new Globals();
        Globals fly = new Globals();
        Globals nopZ = new Globals();
        Globals noGravity = new Globals();
        Globals noCollisionBarriers = new Globals();
        Globals noTerrainCollision = new Globals();


    }
}
