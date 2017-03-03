using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Injector
{
    public partial class Form_Main : Form
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true)]
        public static extern UIntPtr GetProcAddress(IntPtr hModule, string procName);
        /************/
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);
        /************/
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi)]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpAddress, string lpBuffer, UIntPtr nSize, out IntPtr lpNumberOfBytesWritten);
        /************/
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);
        /************/
        [DllImport("kernel32")]
        public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, UIntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, out IntPtr lpThreadId);
        /************/
        [DllImport("kernel32", SetLastError = true, ExactSpelling = true)]
        internal static extern Int32 WaitForSingleObject(IntPtr handle, Int32 milliseconds);
        /************/
        [DllImport("kernel32.dll")]
        public static extern Int32 CloseHandle(IntPtr hObject);
        /************/
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool VirtualFreeEx(IntPtr hProcess, IntPtr lpAddress, UIntPtr dwSize, uint dwFreeType);
        /************/
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(UInt32 dwDesiredAccess, Int32 bInheritHandle, Int32 dwProcessId);
        /************/
        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWow64Process([In] IntPtr hProcess, [Out] out bool lpSystemInfo);


        public Form_Main() // основная форма и инициализация компонентов
        {
            InitializeComponent();
        }

        private void listBoxLog_DrawItem(object sender, DrawItemEventArgs e) //если в listBoxLog попадает строка начинающаяся с "Ошибка .." то делаем строку красной
        {
            e.DrawBackground();
            Brush textBrush;
            string str = ((ListBox)sender).Items[e.Index].ToString();

            if (String.Compare(str, 0, " Ошибка", 0, 5, StringComparison.CurrentCulture) == 0)
            {
                textBrush = Brushes.Red;
            }
            else
            {
                textBrush = Brushes.Black;
            }
            e.Graphics.DrawString(((ListBox)sender).Items[e.Index].ToString(), e.Font, textBrush, e.Bounds, StringFormat.GenericDefault);
        }      

        private void comboBoxProcess_DropDown(object sender, EventArgs e) //при раскрытиии выпадающиего списка comboBox, заполняем его именами запущенных процессов
        {           
            comboBoxProcess.Items.Clear();
            foreach (Process proc in Process.GetProcesses())
            {
                try
                {
                    bool is64bit;
                    IsWow64Process(OpenProcess(0x1F0FFF, 0, proc.Id), out is64bit); //определяем битность процесса
                    if (!is64bit) 
                        comboBoxProcess.Items.Add(proc.ProcessName); //если процесс х64, то добавляем в колекцию
                }
                catch (Exception exept)
                {
                    listBoxLog.Items.Insert(0, " Ошибка: " + exept.Message);
                }
            }
        }

        private void comboBoxProcess_SelectedIndexChanged(object sender, EventArgs e) //обрабатываем процесс выбранный пользователем
        {
            try
            {
                listBoxLog.Items.Clear();
                listBoxLog.Items.Insert(0, " Выбран процесс: " + comboBoxProcess.SelectedItem.ToString());
            }
            catch (Exception exept) //и такое бывает ;)
            {                 
                listBoxLog.Items.Insert(0, " Ошибка: " + exept.Message);
            }
        }

        private void buttonAddDll_Click(object sender, EventArgs e) //добавляем в список загружаемых dll выбранные пользователем библиотеки
        {
            if (openFileDialogDll.ShowDialog() == DialogResult.OK)
            {
                foreach (string item in openFileDialogDll.FileNames)
                {
                    listViewDll.Items.Add(item);
                }
            }
        }

        private void buttonDelDll_Click(object sender, EventArgs e) //удаляем из списка загружаемых dll выбранные пользователем библиотеки
        {
            foreach (ListViewItem item in listViewDll.SelectedItems)
            {
                listViewDll.Items.Remove(item);
            }
        }

        private void buttonDelAllDll_Click(object sender, EventArgs e) //очищаем список загружаемых dll
        {
            if (listViewDll.Items.Count != 0)
            {
                listViewDll.Items.Clear();
            }
        }

        private void buttonLoadDll_Click(object sender, EventArgs e)
        {
            LoadDll();
        }

        private void buttonUnloadDll_Click(object sender, EventArgs e)
        {
            UnloadDll();
        }

        private bool PrepareForWork() //проверяем, все ли готово к загрузке dll
        {
            if (comboBoxProcess.Items.Count < 1)
            {
                listBoxLog.Items.Insert(0, " Ошибка: не выбран процесс!");
                return false;
            }

            if (listViewDll.Items.Count < 1)
            {
                listBoxLog.Items.Insert(0, " Ошибка: не выбрана(ны) dll!");
                return false;
            }

            return true;
        }

        public Int32 GetIdbyName(string procName) //получаем ID процесса
        {
            Process procList;
            try
            {
                procList = Process.GetProcessesByName(procName)[0];
                return procList.Id;                
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private IntPtr dllisLoaded(string procName, string dllName) //если dll загружена, то возвращает базовый адресс dll в памяти процесса
        {
            try
            {
                Process proc = Process.GetProcessesByName(procName)[0];
                foreach (ProcessModule module in proc.Modules)
                {
                    if (dllName == module.ModuleName) return module.BaseAddress;               
                }
            }
            catch (Exception e)
            {
                listBoxLog.Items.Insert(0, " Ошибка: " + e.Message);
                listBoxLog.Items.Insert(0, " Ошибка: запустите инжектор от имени администратора!!!");
            }
            return (IntPtr)null;
        }

        private void InjectionMethod(string pathDll) //метод загрузки dll по Джеффри Рихтеру
        {
            try
            {
                int ProcID = GetIdbyName(comboBoxProcess.SelectedItem.ToString());
                if (ProcID > 0)
                {
                    listBoxLog.Items.Insert(0, " Процесс " + comboBoxProcess.SelectedItem.ToString() + " найден, его ID: " + ProcID.ToString());
                }
                else
                {
                    listBoxLog.Items.Insert(0, " Ошибка: процесс " + comboBoxProcess.SelectedItem.ToString() + " уже не существует!");
                    return;
                }

                if (dllisLoaded(comboBoxProcess.SelectedItem.ToString(), Path.GetFileName(pathDll)) != (IntPtr)null)
                {
                    listBoxLog.Items.Insert(0, " Ошибка: " + Path.GetFileName(pathDll) + " уже загружена!");
                    return;
                }
                else
                    listBoxLog.Items.Insert(0, " Начата загрузка " + Path.GetFileName(pathDll) + " в " + comboBoxProcess.SelectedItem.ToString());

                IntPtr hProcess = OpenProcess(0x1F0FFF, 1, ProcID);
                if (hProcess == (IntPtr)null)
                {
                    listBoxLog.Items.Insert(0, " Ошибка: не возможно получить доступ к процессу " + comboBoxProcess.SelectedItem.ToString());
                    CloseHandle(hProcess);
                    return;
                }

                IntPtr bOut;
                int LenWrite = pathDll.Length + 1;

                IntPtr AllocateMem = VirtualAllocEx(hProcess, (IntPtr)null, (uint)LenWrite, 0x1000, 0x40);
                if (AllocateMem == (IntPtr)null)
                {
                    listBoxLog.Items.Insert(0, " Ошибка: не возможно выделить память в процессе " + comboBoxProcess.SelectedItem.ToString());
                    CloseHandle(hProcess);
                    return;
                }

                if (!WriteProcessMemory(hProcess, AllocateMem, pathDll, (UIntPtr)LenWrite, out bOut))
                {
                    listBoxLog.Items.Insert(0, " Ошибка: не возможно совершить запись в процесс " + comboBoxProcess.SelectedItem.ToString());
                    CloseHandle(hProcess);
                    return;
                }

                UIntPtr dwLoadLib = (UIntPtr)GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
                if (dwLoadLib == (UIntPtr)null)
                {
                    listBoxLog.Items.Insert(0, " Ошибка: не возможно получить адресс функции LoadLibrary в процессе " + comboBoxProcess.SelectedItem.ToString());
                    CloseHandle(hProcess);
                    return;
                }

                IntPtr hThread = (IntPtr)CreateRemoteThread(hProcess, (IntPtr)null, 0, dwLoadLib, AllocateMem, 0, out bOut);
                if (hThread == (IntPtr)null)
                {
                    listBoxLog.Items.Insert(0, " Ошибка: не возможно создать удаленный поток в процессе " + comboBoxProcess.SelectedItem.ToString());
                    CloseHandle(hProcess);
                    CloseHandle(hThread);
                    return;
                }

                int Result = WaitForSingleObject(hThread, 5000);
                if (Result == 0x00000102L)
                {
                    listBoxLog.Items.Insert(0, " Превышено ожидание завершения удаленного потока");
                }

                Thread.Sleep(1000);
                if (!VirtualFreeEx(hProcess, AllocateMem, (UIntPtr)0, 0x8000))
                    listBoxLog.Items.Insert(0, " Выделенная память не освобождена\nОстались следы загрузки dll");
                CloseHandle(hProcess);
                CloseHandle(hThread);

                if (dllisLoaded(comboBoxProcess.SelectedItem.ToString(), Path.GetFileName(pathDll)) != (IntPtr)null)
                    listBoxLog.Items.Insert(0, " Загрузка " + Path.GetFileName(pathDll) + " успешно завершена");
            }
            catch (Exception e)
            {
                listBoxLog.Items.Insert(0, " Ошибка: " + e.Message);
                return;
            }
        }

        private void EjectionMethod(string pathDll) //метод выгрузки dll по Джеффри Рихтеру
        {
            try
            {
                int ProcID = GetIdbyName(comboBoxProcess.SelectedItem.ToString());
                if (ProcID > 0)
                {
                    listBoxLog.Items.Insert(0, " Процесс " + comboBoxProcess.SelectedItem.ToString() + " найден, его ID: " + ProcID.ToString());
                }
                else
                {
                    listBoxLog.Items.Insert(0, " Ошибка: процесс " + comboBoxProcess.SelectedItem.ToString() + " уже не существует!");
                    return;
                }

                IntPtr moduleAddress = dllisLoaded(comboBoxProcess.SelectedItem.ToString(), Path.GetFileName(pathDll));
                if (moduleAddress == (IntPtr)null)
                {
                    listBoxLog.Items.Insert(0, " Ошибка: " + Path.GetFileName(pathDll) + " в " + comboBoxProcess.SelectedItem.ToString() + " не найдена!");
                    return;
                }
                else
                    listBoxLog.Items.Insert(0, " Начата выгрузка " + Path.GetFileName(pathDll) + " из " + comboBoxProcess.SelectedItem.ToString());

                IntPtr hProcess = (IntPtr)OpenProcess(0x1F0FFF, 1, ProcID);
                if (hProcess == (IntPtr)null)
                {
                    listBoxLog.Items.Insert(0, " Ошибка: не возможно получить доступ к процессу " + comboBoxProcess.SelectedItem.ToString());
                    CloseHandle(hProcess);
                    return;
                }

                UIntPtr dwFreeLib = (UIntPtr)GetProcAddress(GetModuleHandle("kernel32.dll"), "FreeLibrary");
                if (dwFreeLib == (UIntPtr)null)
                {
                    listBoxLog.Items.Insert(0, " Ошибка: не возможно получить адресс функции FreeLibrary в процессе " + comboBoxProcess.SelectedItem.ToString());
                    CloseHandle(hProcess);
                    return;
                }

                IntPtr bOut;
                IntPtr hThread = (IntPtr)CreateRemoteThread(hProcess, (IntPtr)null, 0, dwFreeLib, moduleAddress, 0, out bOut);
                if (hThread == (IntPtr)null)
                {
                    listBoxLog.Items.Insert(0, " Ошибка: не возможно создать удаленный поток в процессе " + comboBoxProcess.SelectedItem.ToString());
                    CloseHandle(hProcess);
                    CloseHandle(hThread);
                    return;
                }

                int Result = WaitForSingleObject(hThread, 5000);
                if (Result == 0x00000102L)
                {
                    listBoxLog.Items.Insert(0, " Превышено ожидание завершения удаленного потока");
                }

                Thread.Sleep(1000);
                CloseHandle(hProcess);
                CloseHandle(hThread);

                if (dllisLoaded(comboBoxProcess.SelectedItem.ToString(), Path.GetFileName(pathDll)) == (IntPtr)null)
                    listBoxLog.Items.Insert(0, " Выгрузка " + Path.GetFileName(pathDll) + " успешно завершена");
                else
                    listBoxLog.Items.Insert(0, " Ошибка: " + Path.GetFileName(pathDll) + " не выгружена!");
            }
            catch (Exception e)
            {
                listBoxLog.Items.Insert(0, " Ошибка: " + e.Message);
                return;
            }
        }

        private void LoadDll() //основная функция загрузки dll в процесс
        {
            if (!PrepareForWork()) return;
            foreach (ListViewItem currentDll in listViewDll.Items)
            {
                InjectionMethod(currentDll.Text);
            }
        }

        private void UnloadDll() //основная функция выгрузки dll из процесса
        {
            if (!PrepareForWork()) return;
            foreach (ListViewItem currentDll in listViewDll.Items)
            {
                EjectionMethod(currentDll.Text);
            }           
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://cheaton.ru");
        }
    }
}
