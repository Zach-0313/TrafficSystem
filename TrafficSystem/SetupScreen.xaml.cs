using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TrafficSystem
{
    /// <summary>
    /// Interaction logic for SetupScreen.xaml
    /// </summary>
    public partial class SetupScreen : Page
    {
            SimulationConfig simulationConfig = new SimulationConfig();
        public SetupScreen()
        {
            InitializeComponent();
            FieldInfo[] members = simulationConfig.GetType().GetFields();

            List<Action> buttonActions = new List<Action>();
            foreach (var item in members)
            {
            var button = new TextBox() { Name = item.Name }; // Creating button
            button.TextChanged += ValueUpdated; //Hooking up to event
            Stack_Panel.Children.Add(button);
            }
        }

        private void ValueUpdated(object sender, RoutedEventArgs e) //Event which will be triggerd on click of ya button
        {
            var type = sender.GetType();
            simulationConfig.GetType().GetMember(type.Name);

        }
        private void Button_Click(object sender, RoutedEventArgs e) //Event which will be triggerd on click of ya button
        {
            var newForm = new MainWindow(); //create your new form.
            newForm.Show(); //show the new form.
            
        }
    }
}
