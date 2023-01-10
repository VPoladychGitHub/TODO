using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TODO
{
    public class Todo
    {
        [XmlAttribute]
        public int ID { get; set; }
        [XmlAttribute]
        public string MyTodo { get; set; }
        [XmlAttribute]
        public DateTime DateTodo { get; set; }
    }
    public class Todos
    {
        [XmlArrayAttribute("Items")]
        public BindingList<Todo> todo;
    }
}
