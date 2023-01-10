using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace TODO
{
    public partial class TodoForm : Form
    {
        BindingList<Todo> bindingTDList = new BindingList<Todo>() ;
        BindingSource bs = new BindingSource();

        public TodoForm()
        {
            InitializeComponent();
            CreateList();
        }

        private void Add_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            if (textBoxMyTodo.Text != "")
            {
  
                bindingTDList.Add(new Todo { ID = rnd.Next(), MyTodo = textBoxMyTodo.Text, DateTodo = (DateTime)dateTimePicker1.Value });
                Todos tds = new Todos();
                tds.todo = bindingTDList;
                bs.DataSource = bindingTDList;
                todoes.DataSource = bs;
                XmlSerializer serializer = new XmlSerializer(typeof(Todos));
                TextWriter writer = new StreamWriter("todo.xml");
                serializer.Serialize(writer, tds);
                writer.Close();
            }
            textBoxMyTodo.Text = "";
            dateTimePicker1.Text = "";
        }
        public void CreateList()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Todos));
            Todos todos = new Todos();
            FileStream fs = new FileStream("todo.xml", FileMode.OpenOrCreate);
            
            try
            {
                todos = (Todos)serializer.Deserialize(fs);
                bindingTDList = todos.todo;
                bs.DataSource = bindingTDList;
                todoes.DataSource = bs;

                fs.Close();
            }
             finally
            {
                fs.Close();
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
                int ind, i = 0; // введенное значение позиции
                try
                {
                bool success = int.TryParse(todoes.SelectedRows[0].Index.ToString(), out ind);
                if (success)
                    i = ind;
                }
                catch
                {
                    MessageBox.Show("You  should select row", "Error", MessageBoxButtons.OK);
                    return;
                }
                     Todo p = bindingTDList[i];

                DialogResult dialogResult = MessageBox.Show("Sure DELETE ? ", $"selectedRow Todo: {p.MyTodo}", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {     
                   var res = bindingTDList.Remove(p);

                    Todos todos = new Todos();
                    todos.todo = bindingTDList;
                    XmlSerializer serializer = new XmlSerializer(typeof(Todos));
                    TextWriter writer = new StreamWriter("todo.xml");
                    serializer.Serialize(writer, todos);
                    writer.Close();
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }

           
        }
    }
}
