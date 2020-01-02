namespace Corazon.Specification
{
    public class Rule
    {
        public string Name { get; private set; }

        public string Definition { get; private set; }

        public Rule(string name, string definition)
        {
            this.Name = name;
            this.Definition = definition;
        }
    }
}