using GongSolutions.Wpf.DragDrop;
using Microsoft.Win32;
using Programs_Starter.HandlersManaging;
using Programs_Starter.Models;
using Programs_Starter.Models.Helpers;
using Programs_Starter.ViewModels.Base;
using Programs_Starter.ViewModels.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace Programs_Starter.ViewModels.Wrappers
{
    public class ProgramsToStartDataGridWrapper : BaseDataGridWrapper<ProgramToStartWrapper>, IDropTarget
    {
        public ICommand AddProgramToProgramsToStartList { get; private set; }
        public ICommand RemoveProgramFromProgramsToStartList { get; private set; }

        public ProgramsToStartDataGridWrapper()
        {
            HandlersManager.StartingProgramsHandler.ProgramsToStartCollectionChanged += ProgramsToStartCollectionChanged;
            HandlersManager.StartingProgramsHandler.ProgramsToStartLoadedSuccesfully += ProgramsToStartCollectionInitialized;

            AddProgramToProgramsToStartList = new RelayCommand(AddProgramToCollectionCommand);
            RemoveProgramFromProgramsToStartList = new RelayCommand(RemoveProgramFromCollectionCommand);
        }

        public void DragOver(IDropInfo dropInfo)
        {
            ProgramToStartWrapper sourceItem = dropInfo.Data as ProgramToStartWrapper;   //dragged item
            ProgramToStartWrapper targetItem = dropInfo.TargetItem as ProgramToStartWrapper;//item on with user drops the sourceitem

            if (sourceItem != null && targetItem != null)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dropInfo.Effects = DragDropEffects.Copy;
            }
            else
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dropInfo.Effects = DragDropEffects.None;
            }
        }

        public void Drop(IDropInfo dropInfo)
        {
            ProgramToStartWrapper sourceItem = dropInfo.Data as ProgramToStartWrapper;   //dragged item
            ProgramToStartWrapper targetItem = dropInfo.TargetItem as ProgramToStartWrapper;//item on with user drops the sourceitem
            RelativeInsertPosition positionOfItem = dropInfo.InsertPosition;   //position (before or after targetItem)
            int insertIndex = dropInfo.InsertIndex;   //positon in ProgramsToStart where item was dropped

            if (positionOfItem == RelativeInsertPosition.BeforeTargetItem || targetItem.Order >= HandlersManager.StartingProgramsHandler.ProgramsToStart.Count)
            {
                HandlersManager.StartingProgramsHandler.TryChangeProgramToStartIndex(sourceItem.Order, targetItem.Order);
            }
            else
            {
                HandlersManager.StartingProgramsHandler.TryChangeProgramToStartIndex(sourceItem.Order, targetItem.Order + 1);
            }
            
        }

        private void ProgramsToStartCollectionInitialized()
        {
            DataCollection = new ObservableCollection<ProgramToStartWrapper>();
            foreach (var program in HandlersManager.StartingProgramsHandler.ProgramsToStart.OrderBy(x => x.Key))
            {
                DataCollection.Add(new ProgramToStartWrapper(program.Value, program.Key));
            }
        }

        private void ProgramsToStartCollectionChanged(OperationType operation, bool wasSuccessful, string programName)
        {
            DataCollection.Clear();
            foreach (var program in HandlersManager.StartingProgramsHandler.ProgramsToStart.OrderBy(x => x.Key))
            {
                DataCollection.Add(new ProgramToStartWrapper(program.Value, program.Key));
            }
        }

        private void RemoveProgramFromCollectionCommand()
        {
            if (SelectedItem != null)
            {
                HandlersManager.StartingProgramsHandler.TryRemoveProgramToStart(SelectedItem.Order);
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
                    HandlersManager.StartingProgramsHandler.TryInsertProgramToStart(program, SelectedItem.Order);
                }
                else //if user clicked on some empty field then add new program at the end of the list
                {
                    HandlersManager.StartingProgramsHandler.TryAddProgramToStart(program);
                }
            }

        }
    }
}
