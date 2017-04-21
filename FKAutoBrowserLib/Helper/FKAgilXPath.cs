//---------------------------------------------------------------
// author: FreeKnight
// date: 2017-4-5
//---------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;
using System.Diagnostics;
using System.Text.RegularExpressions;
//---------------------------------------------------------------
namespace FKAutoBrowser
{
    class FKAgilXPath
    {
        HtmlDocument m_AgilXPathHtmlDocument = new HtmlDocument();
        List<string> m_ListXmlPaths = new List<string>();
        List<int> m_ListFoundItems = new List<int>();
        private FKAgilXPath()
        {
        }

        /// <summary>
        /// 查找并返回一个节点
        /// Returns a single code
        /// </summary>
        /// <param name="node">xpath of node</param>
        /// <returns>the html node</returns>
        public HtmlNode this[string node]
        {
            get
            {
                try
                {
                    return m_AgilXPathHtmlDocument.DocumentNode.SelectSingleNode(node);
                }
                catch (Exception err)
                {
                    System.Windows.Forms.MessageBox.Show(err.Message);
                    return null;
                }
            }
        }

        /// <summary>
        /// 返回一个Document的全部HTML
        /// Returns all html from web document
        /// </summary>
        /// <returns></returns>
        public string getHtml()
        {
            return m_AgilXPathHtmlDocument.DocumentNode.OuterHtml;
        }

        public static FKAgilXPath LoadFromURL(string url)
        {
            FKAgilXPath item = new FKAgilXPath();
            HtmlWeb webget = new HtmlWeb();
            item.m_AgilXPathHtmlDocument = webget.Load(url);
            return item;
        }

        public static FKAgilXPath LoadFromFile(string path)
        {
            FKAgilXPath item = new FKAgilXPath();
            item.m_AgilXPathHtmlDocument.Load(path);
            return item;
        }

        public static FKAgilXPath LoadFromString(string html)
        {
            FKAgilXPath item = new FKAgilXPath();
            item.m_AgilXPathHtmlDocument.LoadHtml(html);
            return item;
        }
        void findNode(HtmlNode node, string search, int count)
        {   
            // in case we have two cases down the tree
            int countofterm = Regex.Matches(m_AgilXPathHtmlDocument.DocumentNode.OuterHtml, search).Cast<Match>().Count();
            if (node.OuterHtml.IndexOf(search, StringComparison.InvariantCultureIgnoreCase) != -1)
            {
                if (!node.XPath.Contains("#"))
                    m_ListXmlPaths.Add(node.XPath);

                //Debug.WriteLine(node.XPath);

                if (node.ChildNodes.Count == 0)
                {
                    m_ListFoundItems.Add(m_ListXmlPaths.Count - 1);
                    //Debug.WriteLine(node.OuterHtml);
                }
                else if (countofterm < count)
                {
                    m_ListFoundItems.Add(m_ListXmlPaths.Count - 2);
                    //Debug.WriteLine("Previous Search term is one of the matches");
                }
            }
            else if (countofterm < count)
            {
                m_ListFoundItems.Add(m_ListXmlPaths.Count - 1);
            }
            foreach (HtmlNode child in node.ChildNodes)
            {
                findNode(child, search, countofterm);
            }
        }

        public void findText(string term)
        {
            m_ListXmlPaths.Clear();
            m_ListFoundItems.Clear();

            if (term == null)
                return;
            findNode(m_AgilXPathHtmlDocument.DocumentNode, term, Regex.Matches(m_AgilXPathHtmlDocument.DocumentNode.OuterHtml, term).Cast<Match>().Count());
        }

        public List<string> Xpaths
        {
            get
            {
                return new List<string>(m_ListXmlPaths);
            }
        }
        public List<int> FoundNodes
        {
            get
            {
                return new List<int>(m_ListFoundItems);
            }
        }
    }
}
