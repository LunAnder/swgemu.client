using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxv.Swg.Common.Files;

namespace SWG.Client.Object.Templates.Shared
{
    public class GenericSharedTemplate : SharedObjectTemplate
    {
        private static readonly LogAbstraction.ILogger _Logger = LogAbstraction.LogManagerFacad.GetCurrentClassLogger();

        public virtual string RootNodeType { get; set; }

        public GenericSharedTemplate(string rootNodeType,IFFFile file, ITemplateRepository repo) 
            : this(rootNodeType, file.Root, repo)
        {

        }

        public GenericSharedTemplate(string rootNodeType, IFFFile.Node node, ITemplateRepository repo)
        {
            RootNodeType = rootNodeType;
            ReadNode(node, repo);
        }

        public override void ReadNode(IFFFile.Node node, ITemplateRepository repo, bool checkType = true)
        {
            if (RootNodeType == null && checkType && node.Type != RootNodeType)
            {
                base.ReadNode(node, repo, checkType);
                return;
            }

            ParseData(node, repo, (n, r) => { }, _Logger);
        }
    }
}
