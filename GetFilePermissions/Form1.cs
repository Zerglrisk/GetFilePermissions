using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace GetFilePermissions
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.Size = new Size(Screen.PrimaryScreen.Bounds.Width / 10, Screen.PrimaryScreen.Bounds.Height / 10);
        }

        //All Getting Authority
        private void button1_Click(object sender, EventArgs e)
        {
            RegistryKey rkey = Registry.ClassesRoot.CreateSubKey("*\\shell\\runas");
            rkey.SetValue("", "모든 권한 얻기", RegistryValueKind.String);
            rkey.SetValue("NoWorkingDirectory", "", RegistryValueKind.String);
            rkey = Registry.ClassesRoot.CreateSubKey("*\\shell\\runas\\command");
            rkey.SetValue("", "cmd.exe /c takeown /f \"%1\" && icacls \"%1\" /grant administrators:F", RegistryValueKind.String);
            rkey.SetValue("IsolatedCommand", "cmd.exe /c takeown /f \"%1\" && icacls \"%1\" /grant administrators:F", RegistryValueKind.String);
            rkey = Registry.ClassesRoot.CreateSubKey("exefile\\shell\\runas2");
            rkey.SetValue("", "모든 권한 얻기", RegistryValueKind.String);
            rkey.SetValue("NoWorkingDirectory", "", RegistryValueKind.String);
            rkey = Registry.ClassesRoot.CreateSubKey("exefile\\shell\\runas2\\command");
            rkey.SetValue("", "cmd.exe /c takeown /f \"%1\" && icacls \"%1\" /grant administrators:F", RegistryValueKind.String);
            rkey.SetValue("IsolatedCommand", "cmd.exe /c takeown /f \"%1\" && icacls \"%1\" /grant administrators:F", RegistryValueKind.String);
            rkey = Registry.ClassesRoot.CreateSubKey("Directory\\shell\\runas");
            rkey.SetValue("", "모든 권한 얻기", RegistryValueKind.String);
            rkey.SetValue("NoWorkingDirectory", "", RegistryValueKind.String);
            rkey = Registry.ClassesRoot.CreateSubKey("Directory\\shell\\runas\\command");
            rkey.SetValue("", "cmd.exe /c takeown /f \"%1\" /r /d y && icacls \"%1\" /grant administrators:F /t", RegistryValueKind.String);
            rkey.SetValue("IsolatedCommand", "cmd.exe /c takeown /f \"%1\" /r /d y && icacls \"%1\" /grant administrators:F /t", RegistryValueKind.String);
            rkey.Close();
        }

        //All Deleting Authority
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Registry.ClassesRoot.DeleteSubKey("*\\shell\\runas\\command", false);
                Registry.ClassesRoot.DeleteSubKey("*\\shell\\runas", false);
                Registry.ClassesRoot.DeleteSubKey("exefile\\shell\\runas2\\command", false);
                Registry.ClassesRoot.DeleteSubKey("exefile\\shell\\runas2", false);
                Registry.ClassesRoot.DeleteSubKey("Directory\\shell\\runas\\command", false);
                Registry.ClassesRoot.DeleteSubKey("Directory\\shell\\runas", false);
            }

            catch (System.InvalidOperationException err)
            {
                if(MessageBox.Show("권한 제거에 실패하였습니다.\n강제로 삭제하시겠습니까?\n이 행동으로 인한 결과의 책임은 지지않습니다.","",MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    Registry.ClassesRoot.DeleteSubKeyTree("*\\shell\\runas", false);
                    Registry.ClassesRoot.DeleteSubKeyTree("exefile\\shell\\runas2", false);
                    Registry.ClassesRoot.DeleteSubKeyTree("Directory\\shell\\runas", false);
                }
            }
        }

        //about
        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Github : https://github.com/Zerglrisk/GetFilePermissions\n"
                + "Origin Resource : http://jungle-e.tistory.com/385 \n"
                + "Made by : Zerglrisk"
                + "Version : 0.0.1");
        }
    }
}
