using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP_LAB1.Models
{
    /// <summary>
    /// Вузол однозв'язного списку
    /// </summary>
    public class ListNode
    {
        public ListElement Head { get; set; }
        public ListNode Tail { get; set; }

        public ListNode(ListElement head, ListNode tail = null)
        {
            Head = head;
            Tail = tail;
        }
    }
}
