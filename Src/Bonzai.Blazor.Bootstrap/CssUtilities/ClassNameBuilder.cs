using System.Text;

namespace Bonzai.Blazor.Bootstrap.CssUtilities
{
    public class ClassNameBuilder : IClassNameBuilder
    {
        private readonly StringBuilder _stringBuilder;

        public ClassNameBuilder()
        {
            _stringBuilder = new StringBuilder();
        }

        public ClassNameBuilder(string initialClassName)
        {
            _stringBuilder = new StringBuilder(initialClassName);
        }

        public void AddFirstClassName(string firstClassName)
        {
            _stringBuilder.Append(firstClassName);
        }

        public void AddClassName(string className)
        {
            _stringBuilder.Append(" ");
            _stringBuilder.Append(className);
        }

        public string GetClassNames()
        {
            return _stringBuilder.ToString();
        }
    }

    /// <summary>
    /// Utility for for adding class names that require complex if/else logic
    /// </summary>
    public interface IClassNameBuilder
    {
        /// <summary>
        /// Initializes the builder with the given class name(s).
        /// </summary>
        /// <param name="firstClassName"></param>
        void AddFirstClassName(string firstClassName);

        /// <summary>
        /// Appends the class name to this instance.
        /// </summary>
        /// <param name="className">The name of the class to append.</param>
        void AddClassName(string className);

        /// <summary>
        /// Retrieves the class names that have been added to this instance.
        /// </summary>
        /// <returns>All of the class names as a single string.</returns>
        string GetClassNames();
    }
}
