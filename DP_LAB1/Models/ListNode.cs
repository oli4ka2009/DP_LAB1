using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_LAB1.Models
{
    /// <summary>
    /// Простий вузол зв'язного списку
    /// </summary>
    public class ListNode
    {
        public ListElement Element { get; }
        public ListNode Next { get; }

        public ListNode(ListElement element, ListNode next = null)
        {
            Element = element;
            Next = next;
        }
    }
}
