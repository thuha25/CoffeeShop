using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.DAO
{
    public class MyList
    {
        private Node listHead;
        private Node listTail;
        private uint length;
        public MyList()
        {
            this.length = 0;
            this.listHead = null;
            this.listTail = null;
        }
        public void Add(object obj)
        {
            Node newNode = new Node();
            newNode.obj = obj;
            newNode.next = null;
            if (this.listHead == null) this.listHead = this.listTail = newNode;
            else
            {
                this.listTail.next = newNode;
                this.listTail = this.listTail.next;
            }
        }
        public static Node operator+(MyList l, int index)
        {
            Node node = l.listHead; 
            for (int i = 1; i<=index; ++i)
            {
                node = node.next;
            }
            return node;
        }
}
    public class Node
    {
        public object obj = null;
        public Node next = null;
        public Node() { }
    }
    
}
