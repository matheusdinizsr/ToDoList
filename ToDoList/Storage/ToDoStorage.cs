using System.Security.Cryptography.Xml;
using System.Text.Json;

namespace ToDoList.Storage
{
    public class ToDoStorage
    {
        private static List<ToDoItem> _todoItens = new List<ToDoItem>();
        const string dbPath = "C:\\Users\\mathe\\Desktop\\ToDoDB.txt";
        const string separator = "\n####\n";

        public ToDoItem[] GetTodo()
        {
            string load = File.ReadAllText(dbPath);
            try
            {
                _todoItens = System.Text.Json.JsonSerializer.Deserialize<List<ToDoItem>>(load) ?? new List<ToDoItem>();

            }
            catch (Exception)
            {
                _todoItens = new List<ToDoItem>();
                throw;
            }



            return _todoItens.ToArray();
        }

        public string Converter(ToDoItem i)
        {
            return i.Text;
        }

        public void AddTodo(string item)
        {

            ToDoItem input = new ToDoItem();
            input.Text = item;
            input.CreateDate = DateTime.Now;
            _todoItens.Add(input);
            SaveList();
        }

        public bool RemoveTodo(DateTime createDate)
        {
            var limite = TimeSpan.FromMilliseconds(1);
            var found = _todoItens.Find( x => 
            {
                var xConvert = TimeZoneInfo.ConvertTimeToUtc(x.CreateDate);
                var diff = xConvert > createDate ? xConvert - createDate : createDate - xConvert; 
                return diff < limite;
            });



            if (found == null)
            {
                return false;
            }
            _todoItens.Remove(found);
            SaveList();
            return true;

        }

        public void SaveList()
        {

            var options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            string list = System.Text.Json.JsonSerializer.Serialize(_todoItens, options);
            File.WriteAllText(dbPath, list);
        }


    }

    public class ToDoItem
    {
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
