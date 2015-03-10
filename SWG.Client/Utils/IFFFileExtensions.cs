using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wxv.Swg.Common.Files;

namespace SWG.Client.Utils
{
    public static class IFFFileExtensions
    {
        public static IFFFile.Node FindNode(this IFFFile file, string nodeToFind, string rootNode = "SHOT")
        {
            if (rootNode != null && (file.Root.Type != rootNode || !file.Root.Children.Any()))
            {
                return null;
            }

            return FindSubNode(file.Root, nodeToFind);

        }

        public static IFFFile.Node FindSubNode(this IFFFile.Node node, string nodeToFind, bool includeChirldren = true)
        {
            foreach (var item in node.Children)
            {
                if (item.Type == nodeToFind)
                {
                    return item;
                }

                if (includeChirldren)
                {
                    var subSearch = FindSubNode(item, nodeToFind);
                    if (subSearch != null)
                    {
                        return subSearch;
                    }
                }

            }

            return null;
        }

        public static IEnumerable<IFFFile.Node> FindSiblingNodes(this IFFFile file, string nodeToFind, string rootNode = null)
        {
            if (file.Root.Type != rootNode || !file.Root.Children.Any())
            {
                return new IFFFile.Node[0];
            }

            var found = FindSubNode(file.Root, nodeToFind);
            if (found == null || found.Parent == null)
            {
                return new IFFFile.Node[0];
            }

            return found.Parent.Children;
        }

        public static IEnumerable<IFFFile.Node> FindSiblingNodes(this IFFFile.Node node, string nodeToFind)
        {
            var found = FindSubNode(node, nodeToFind);
            if (found == null || found.Parent == null)
            {
                return new IFFFile.Node[0];
            }

            return found.Parent.Children;
        }

        public static IEnumerable<IFFFile.Node> Siblings(this IFFFile.Node node)
        {
            if(node.Parent == null)
            {
                return null;
            }

            return node.Parent.Children;
        }

        public static IFFFile.Node FindNextSibling(this IFFFile.Node node)
        {
            var siblings = node.Siblings();
            if(siblings == null)
            {
                return null;
            }

            var nodeList = siblings as IList<IFFFile.Node>;
            if(nodeList != null)
            {
                var indexOfNext = nodeList.IndexOf(node) + 1;
                if(indexOfNext >= nodeList.Count)
                {
                    return null;
                }

                return nodeList[indexOfNext];
            }

            var found = false;

            return siblings.FirstOrDefault(cur =>
            {
                if (found)
                {
                    return true;
                }

                if (cur == node)
                {
                    found = true;
                }

                return false;
            });
        }
    }
}
