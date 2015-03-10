using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxv.Swg.Common.Files;

namespace SWG.Client.Object.Templates
{
    public class TemplateRepository : ITemplateRepository
    {
        public IArchiveRepository ArchiveRepo { get; set; }

        public IFFFile LoadIff(string toLoad)
        {
            return ArchiveRepo.LoadIff(toLoad);
        }

        public StringFile LoadStf(string toLoad)
        {
            return ArchiveRepo.LoadStf(toLoad);
        }
    }
}
