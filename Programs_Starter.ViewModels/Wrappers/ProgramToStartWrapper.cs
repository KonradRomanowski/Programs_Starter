using Programs_Starter.Models;
using Programs_Starter.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Programs_Starter.ViewModels.Wrappers
{
    public class ProgramToStartWrapper : BaseWrapper<ProgramToStart>
    {
        public int Order { get; set; }

        public string Name => entity.Name ?? string.Empty;

        public string Path => entity.Path ?? string.Empty;

        public ProgramStatus Status => entity.ProgramStatus ?? ProgramStatus.Unknown;

        public ProgramToStartWrapper(ProgramToStart programToStart, int order) : base(programToStart)
        {
            Order = order;
        }
    }
}
