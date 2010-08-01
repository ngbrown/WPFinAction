using System.Windows.Input;

namespace Desktop_Wiki
{
    public static class WikiCommands
    {
        private static RoutedUICommand createLinkFromSelection;

        static WikiCommands()
        {
            createLinkFromSelection = new RoutedUICommand(
                "Create Link from Selection",
                "CreateLinkFromSelection",
                typeof(WikiCommands));
        }

        public static RoutedUICommand CreateLinkFromSelection
        {
            get { return createLinkFromSelection; }
        }
    }
}
