using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ARCH.Commands;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using System.IO.Compression;
using System.IO;
using System.ComponentModel;
using ARCH.Models;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;


namespace ARCH.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        #region variables
        string name;
        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        string password;
        public string Password
        {
            get => password;
            set => Set(ref password, value);
        }

        string filePath;
        public string FilePath
        {
            get => filePath;
            set => Set(ref filePath, value);
        }

        private string folderToArchive;
        public string FolderToArchive
        {
            get => folderToArchive;
            set => Set(ref folderToArchive, value);
        }

        #endregion

        #region SelectFilePath
        public ICommand SelectFilePath { get; set; }
        private bool CanSelectFilePathExecute(object p) => true;
        private void OnSelectFilePathExecuted(object p)
        {
            var dlg = new FolderPicker();
            if (dlg.ShowDialog() == true)
                FilePath = dlg.ResultPath;
            /*OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                FilePath = System.IO.Path.GetDirectoryName(openFileDialog.FileName);*/
        }
        #endregion

        #region SelectFile
        public ICommand SelectFile { get; set; }
        private bool CanSelectFileExecute(object p) => true;
        private void OnSelectFileExecuted(object p)
        {
/*            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                FolderToArchive = System.IO.Path.GetDirectoryName(openFileDialog.FileName);
            }*/
            var dlg = new FolderPicker();
            if (dlg.ShowDialog() == true)
                FolderToArchive = dlg.ResultPath;

        }
        #endregion

        #region Archive
        public ICommand Archive { get; set; }
        private bool CanArchivehExecute(object p) => true;
        private void OnArchiveExecuted(object p)
        {
            string fullPath = FilePath + "\\" + Name + ".zip";
            ZipFile.CreateFromDirectory(FolderToArchive, fullPath);
            MessageBox.Show($"Архивация произошла успешно");
        }
        #endregion

        public MainWindowViewModel()
        {
            SelectFilePath = new RelayCommand(OnSelectFilePathExecuted, CanSelectFilePathExecute);
            SelectFile = new RelayCommand(OnSelectFileExecuted, CanSelectFileExecute);
            Archive = new RelayCommand(OnArchiveExecuted, CanArchivehExecute);
        }
    }
}