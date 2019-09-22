using Microsoft.Win32;
using Programs_Starter.HandlersManaging;
using Programs_Starter.Models;
using Programs_Starter.ViewModels.Base;
using Programs_Starter.ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Programs_Starter.ViewModels.Wrappers
{
    public class ProgramsToStartDataGridWrapper : BaseDataGridWrapper<ProgramToStartWrapper>
    {
        public ICommand AddProgramToProgramsToStartList { get; private set; }

        public ProgramsToStartDataGridWrapper()
        {
            HandlersManager.StartingProgramsHandler.ProgramsToStartCollectionChanged += ProgramsToStartCollectionChanged;
            HandlersManager.StartingProgramsHandler.ProgramsToStartLoadedSuccesfully += ProgramsToStartCollectionInitialized;

            AddProgramToProgramsToStartList = new RelayCommand(AddProgramToCollectionCommand);
        }

        private void ProgramsToStartCollectionInitialized()
        {
            DataCollection = new ObservableCollection<ProgramToStartWrapper>();
            foreach (var program in HandlersManager.StartingProgramsHandler.ProgramsToStart.OrderBy(x => x.Key))
            {
                DataCollection.Add(new ProgramToStartWrapper(program.Value, program.Key));
            }
        }

        private void ProgramsToStartCollectionChanged()
        {
            DataCollection.Clear();
            foreach (var program in HandlersManager.StartingProgramsHandler.ProgramsToStart.OrderBy(x => x.Key))
            {
                DataCollection.Add(new ProgramToStartWrapper(program.Value, program.Key));
            }
        }

        private void AddProgramToCollectionCommand()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.CheckPathExists = true;
            openFileDialog.Multiselect = false;
            openFileDialog.Title = "Select Programs to add to Programs Starter";
            openFileDialog.ValidateNames = true;

            if (openFileDialog.ShowDialog() == true)
            {
                ProgramToStart program = new ProgramToStart(openFileDialog.SafeFileName, openFileDialog.FileName);

                //check if user clicked on program or on some empty field in ListView
                //if user clicked on program then insert new program before the selected item
                if (SelectedItem != null)
                {
                    //ProgramsToStartList.Insert(SelectedProgramOnProgramsToStartListView.StartingOrder - 1, program);
                }
                else //if user clicked on some empty field then add new program at the end of the list
                {
                    HandlersManager.StartingProgramsHandler.TryAddProgramToStart(program);
                }
            }

        }
    }
}
