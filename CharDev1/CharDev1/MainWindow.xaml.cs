using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CharDev1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            GlobalData.PopulateStances();
        }

        private void btnGo_Click(object sender, RoutedEventArgs e)
        {
            Stance stance = GlobalData.GetStance("Air");
           // stance.Description = "Earth";
           // stance.Modifiers = new Dictionary<string, List<Modifier>>();
           // stance.ProficiencyType = "strength";

           // stance.AddModifier("Strength", 2, "stance1","strength");
           // stance.AddModifier("Dex", -2, "stance1","dex");
           // stance.AddModifier("Int", -1, "stance1","int");
           // stance.AddModifier("Constitution", 3, "stance1","con");
           // stance.AddModifier("AC", -2, "stance1", "ac");
           // stance.AddModifier("Charisma", 1, "stance1", "charisma");
           // stance.AddModifier("Fortitude", 3, "stance1", "fort");
           // stance.AddModifier("Reflexes", -1, "stance1", "reflex");

           // stance.AddModifier("Willpower", -1, "stance1", "willpower");
           // stance.AddModifier("Initiative", -2, "stance1", "initiative");
           // stance.AddModifier("Fire Resistence", 1, "stance1", "Fire Resistence");

           // stance.AddModifier("Water Resistence", -1, "stance1", "Water Resistence");
           // stance.AddModifier("Earth Resistence", 2, "stance1", "Earth Resistence");
           // stance.AddModifier("Wind Resistence", -2, "stance1", "Wind Resistence");
           // stance.AddModifier("Max Hp", 10, "stance1", "maxhp");
           //// stance.AddModifier("Max energy", 0, "stance1", "max energy");
           // stance.AddModifier("Max stamina", 3, "stance1", "max stamina");
           // //stance.AddModifier("Attack Bonus", 0, "stance1", "attackbonus");

            CharacterClass c = new CharacterClass();
            c.Description = "Warrior";
            c.LevelUpHpMod = 12;
            c.StartBaseHp = 14;
            c.ObjId = "";
            Character character = new Character();
            character.CharacterName = "Clint";

            character.AddModifier("Strength", 2, c.ObjId, "strength",1,"Warrior");
            character.AddModifier("Dex", 2, c.ObjId, "dex", 1, "Warrior");

            character.SelectedClass = c;
            character.AddStance(stance);

            

            character.Level = 10;
            character.RefreshAllData();
            character.FillHealth();

            txtDisplay.Text = character.ToString();

        }
    }
}
