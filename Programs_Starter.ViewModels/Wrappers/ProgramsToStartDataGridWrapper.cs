using Programs_Starter.HandlersManaging;
using Programs_Starter.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Programs_Starter.ViewModels.Wrappers
{
    public class ProgramsToStartDataGridWrapper : BaseDataGridWrapper<ProgramToStartWrapper>
    {
        public ProgramsToStartDataGridWrapper()
        {
            HandlersManager.StartingProgramsHandler.ProgramsToStartCollectionChanged += ProgramsToStartCollectionChanged;
            HandlersManager.StartingProgramsHandler.ProgramsToStartLoadedSuccesfully += ProgramsToStartCollectionInitialized;
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
    }
}
