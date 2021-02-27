using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WpfAppDatagridGroupingHeader.Extensions
{
   public static  class XmlExtensions
    { 
        public static IEnumerable<XmlNode> TraverseAllNodes(this XmlDocument doc)
        {
            if (doc == null)
                throw new ArgumentNullException(nameof(doc));

            return TraverseNodesImpl(doc).Skip(1);
        }
        private static IEnumerable<XmlNode> TraverseNodesImpl(XmlNode node)
        {
            yield return node;

            foreach (XmlNode childNode in node.ChildNodes)
            {
                foreach (var node1 in TraverseNodesImpl(childNode))
                    yield return node1;
            }
        }
    }
}
