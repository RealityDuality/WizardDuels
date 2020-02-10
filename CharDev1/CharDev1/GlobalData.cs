using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CharDev1
{
    static class GlobalData
    {
        static public Dictionary<string, CharacterClass> AllCharacterClasses = new Dictionary<string, CharacterClass>();
        static public Dictionary<string,Stance> AllStances = new Dictionary<string, Stance>();

        private static long objIdIdx = 0;

        static public long GetNewObjectId()
        {
            objIdIdx++;
            return objIdIdx;
        }

        static public void PopulateStances()
        {
            Stance stance = CreateStanceAir();

            if (!AllStances.ContainsKey(stance.Description))
                AllStances.Add(stance.Description,stance);

            stance = CreateStanceFire();

            if (!AllStances.ContainsKey(stance.Description))
                AllStances.Add(stance.Description, stance);

            stance = CreateStanceWater();

            if (!AllStances.ContainsKey(stance.Description))
                AllStances.Add(stance.Description, stance);

            stance = CreateStanceEarth();

            if (!AllStances.ContainsKey(stance.Description))
                AllStances.Add(stance.Description, stance);           
        }

        static public Stance GetStance(string description)
        {
            if(AllStances.ContainsKey(description))
            {
                return AllStances[description];
            }

            return null;
        }

        static public void PopulateCharacterClasses()
        {

        }

        static public CharacterClass CreateCharacterClassWarrior()
        {
            CharacterClass c = new CharacterClass();
            c.Description = "Warrior";
            c.LevelUpHpMod = 14;
            c.StartBaseHp = 12;
            c.ObjId = "";

            return c;
        }

        static public Stance CreateStanceEarth()
        {
            Stance stance = new Stance();
            stance.Level = 1;
            stance.Description = "Earth";
            stance.Modifiers = new Dictionary<string, List<Modifier>>();
            stance.ProficiencyType = "strength";
            stance.ObjId = "";

            int lvl = stance.Level.ToInt();

            stance.AddModifier("Strength", 2, "Stance Earth Level 1", "strength", lvl,"Earth");
            stance.AddModifier("Dex", -2, "Stance Earth Level 1", "dex", lvl, "Earth");
            stance.AddModifier("Int", -1, "Stance Earth Level 1", "int", lvl, "Earth");
            stance.AddModifier("Constitution", 3, "Stance Earth Level 1", "constitution", lvl, "Earth");
            stance.AddModifier("AC", -2, "Stance Earth Level 1", "ac", lvl, "Earth");
            stance.AddModifier("Charisma", 1, "Stance Earth Level 1", "charisma", lvl, "Earth");
            stance.AddModifier("Fortitude", 3, "Stance Earth Level 1", "fortitude", lvl, "Earth");
            stance.AddModifier("Reflexes", -1, "Stance Earth Level 1", "reflex", lvl, "Earth");

            stance.AddModifier("Willpower", -1, "Stance Earth Level 1", "willpower", lvl, "Earth");
            stance.AddModifier("Initiative", -2, "Stance Earth Level 1", "initiative", lvl, "Earth");
            stance.AddModifier("Fire Resistence", 1, "Stance Earth Level 1", "Fire Resistence", lvl, "Earth");

            stance.AddModifier("Water Resistence", -1, "Stance Earth Level 1", "Water Resistence", lvl, "Earth");
            stance.AddModifier("Earth Resistence", 2, "Stance Earth Level 1", "Earth Resistence",lvl, "Earth");
            stance.AddModifier("Wind Resistence", -2, "Stance Earth Level 1", "Wind Resistence", lvl, "Earth");
            stance.AddModifier("Max Hp", 10, "Stance Earth Level 1", "maxhp", lvl, "Earth");
            // stance.AddModifier("Max energy", 0, "Stance Earth Level 1", "max energy",stance.Level,"Earth");
            stance.AddModifier("Max stamina", 3, "Stance Earth Level 1", "max stamina", lvl, "Earth");
            //stance.AddModifier("Attack Bonus", 0, "Stance Earth Level 1", "attackbonus",stance.Level,"Earth");

            return stance;
        }

        static public Stance CreateStanceFire()
        {
            Stance stance = new Stance();
            stance.ObjId = "";
            stance.Level = 1;
            stance.Description = "Fire";
            stance.Modifiers = new Dictionary<string, List<Modifier>>();
            stance.ProficiencyType = "dex";
            int lvl = stance.Level.ToInt();
            stance.AddModifier("Strength", -2, "Stance " + stance.Description + " Level 1", "strength", lvl,"Fire");
            stance.AddModifier("Dex", 3, "Stance " + stance.Description + " Level 1", "dex", lvl, "Fire");
            //stance.AddModifier("Int", -1, "Stance Earth Level 1", "int",lvl,"Fire");
            stance.AddModifier("Wisdom", -2, "Stance " + stance.Description + " Level 1", "wisdom", lvl, "Fire");
            stance.AddModifier("Constitution", -2, "Stance " + stance.Description + " Level 1", "constitution", lvl, "Fire");
            stance.AddModifier("AC", -1, "Stance " + stance.Description + " Level 1", "ac", lvl, "Fire");
            stance.AddModifier("Charisma", 1, "Stance " + stance.Description + " Level 1", "charisma", lvl, "Fire");
            stance.AddModifier("Fortitude", -2, "Stance " + stance.Description + " Level 1", "fortitude", lvl, "Fire");
            stance.AddModifier("Reflexes", 1, "Stance " + stance.Description + " Level 1", "reflex", lvl, "Fire");

            stance.AddModifier("Willpower", 1, "Stance " + stance.Description + " Level 1", "willpower", lvl, "Fire");
            stance.AddModifier("Initiative", 1, "Stance " + stance.Description + " Level 1", "initiative", lvl, "Fire");
            stance.AddModifier("Fire Resistence", 2, "Stance " + stance.Description + " Level 1", "Fire Resistence", lvl, "Fire");

            stance.AddModifier("Water Resistence", -2, "Stance " + stance.Description + " Level 1", "Water Resistence", lvl, "Fire");
            stance.AddModifier("Earth Resistence", -1, "Stance " + stance.Description + " Level 1", "Earth Resistence", lvl, "Fire");
            stance.AddModifier("Wind Resistence", 1, "Stance " + stance.Description + " Level 1", "Wind Resistence", lvl, "Fire");
            stance.AddModifier("Max Hp", 2, "Stance " + stance.Description + " Level 1", "maxhp", lvl, "Fire");
            stance.AddModifier("Max energy", 4, "Stance " + stance.Description + " Level 1", "max energy", lvl, "Fire");
            stance.AddModifier("Max stamina", 6, "Stance " + stance.Description + " Level 1", "max stamina", lvl, "Fire");
            stance.AddModifier("Attack Bonus", 1, "Stance " + stance.Description + " Level 1", "attackbonus", lvl, "Fire");
            stance.AddModifier("Luck", 1, "Stance " + stance.Description + " Level 1", "luck", lvl, "Fire");

            return stance;
        }

        static public Stance CreateStanceWater()
        {
            Stance stance = new Stance();
            stance.Level = 1;
            stance.Description = "Water";
            stance.Modifiers = new Dictionary<string, List<Modifier>>();
            stance.ProficiencyType = "wisdom";
            stance.ObjId = "";
            int lvl = stance.Level.ToInt();
            stance.AddModifier("Strength", -2, "Stance " + stance.Description + " Level 1", "strength", lvl,"Water");
            //stance.AddModifier("Dex", 1, "Stance " + stance.Description + " Level 1", "dex",lvl,"Water");
            stance.AddModifier("Int", 1, "Stance Earth Level 1", "int", lvl, "Water");
            stance.AddModifier("Wisdom", 2, "Stance " + stance.Description + " Level 1", "wisdom", lvl, "Water");
            // stance.AddModifier("Constitution", -2, "Stance " + stance.Description + " Level 1", "constitution",lvl,"Water");
            stance.AddModifier("AC", 1, "Stance " + stance.Description + " Level 1", "ac", lvl, "Water");
            stance.AddModifier("Charisma", -2, "Stance " + stance.Description + " Level 1", "charisma", lvl, "Water");
            stance.AddModifier("Fortitude", -1, "Stance " + stance.Description + " Level 1", "fortitude", lvl, "Water");
            stance.AddModifier("Reflexes", 2, "Stance " + stance.Description + " Level 1", "reflex", lvl, "Water");

            //stance.AddModifier("Willpower", 1, "Stance " + stance.Description + " Level 1", "willpower",lvl,"Water");
            stance.AddModifier("Initiative", -1, "Stance " + stance.Description + " Level 1", "initiative", lvl, "Water");
            stance.AddModifier("Fire Resistence", 1, "Stance " + stance.Description + " Level 1", "Fire Resistence", lvl, "Water");

            stance.AddModifier("Water Resistence", 2, "Stance " + stance.Description + " Level 1", "Water Resistence", lvl, "Water");
            stance.AddModifier("Earth Resistence", -2, "Stance " + stance.Description + " Level 1", "Earth Resistence", lvl, "Water");
            stance.AddModifier("Wind Resistence", -1, "Stance " + stance.Description + " Level 1", "Wind Resistence", lvl, "Water");
            stance.AddModifier("Max Hp", 5, "Stance " + stance.Description + " Level 1", "maxhp", lvl, "Water");
            stance.AddModifier("Max energy", 2, "Stance " + stance.Description + " Level 1", "max energy", lvl, "Water");
            stance.AddModifier("Max stamina", 5, "Stance " + stance.Description + " Level 1", "max stamina", lvl, "Water");
            stance.AddModifier("Attack Bonus", 1, "Stance " + stance.Description + " Level 1", "attackbonus", lvl, "Water");
            // stance.AddModifier("Luck", 1, "Stance " + stance.Description + " Level 1", "luck",lvl,"Water");

            return stance;
        }

        static public Stance CreateStanceAir()
        {
            Stance stance = new Stance();
            stance.Level = 10;
            stance.Description = "Air";
            stance.Id = "";
            stance.ObjId = "";

            stance.Modifiers = new Dictionary<string, List<Modifier>>();
            stance.ProficiencyType = "int";
            int lvl = stance.Level.ToInt();
            stance.AddModifier("Strength", -2, "Stance " + stance.Description + " Level "+lvl, "strength", lvl, "Air");
            stance.AddModifier("Dex", 1, "Stance " + stance.Description + " Level "+lvl, "dex", lvl, "Air");
            stance.AddModifier("Int", 2, "Stance Earth Level " + lvl, "int", lvl, "Air");
            //  stance.AddModifier("Wisdom", 2, "Stance " + stance.Description + " Level 1", "wisdom",lvl,"Wind");
            stance.AddModifier("Constitution", -3, "Stance " + stance.Description + " Level " + lvl, "constitution", lvl, "Air");
            stance.AddModifier("AC", 2, "Stance " + stance.Description + " Level " + lvl, "ac", lvl, "Air");
            stance.AddModifier("Charisma", -2, "Stance " + stance.Description + " Level " + lvl, "charisma", lvl, "Air");
            stance.AddModifier("Fortitude", -1, "Stance " + stance.Description + " Level " + lvl, "fortitude", lvl, "Air");
            stance.AddModifier("Reflexes", 1, "Stance " + stance.Description + " Level " + lvl, "reflex", lvl, "Air");

            stance.AddModifier("Willpower", 1, "Stance " + stance.Description + " Level " + lvl, "willpower", lvl, "Air");
            stance.AddModifier("Initiative", -1, "Stance " + stance.Description + " Level " + lvl, "initiative", lvl, "Air");
            stance.AddModifier("Fire Resistence", 1, "Stance " + stance.Description + " Level " + lvl, "Fire Resistence", lvl, "Air");

            stance.AddModifier("Water Resistence", -1, "Stance " + stance.Description + " Level " + lvl, "Water Resistence", lvl, "Air");
            stance.AddModifier("Earth Resistence", -2, "Stance " + stance.Description + " Level " + lvl, "Earth Resistence", lvl, "Air");
            stance.AddModifier("Wind Resistence", 2, "Stance " + stance.Description + " Level " + lvl, "Wind Resistence", lvl, "Air");
            // stance.AddModifier("Max Hp", 5, "Stance " + stance.Description + " Level 1", "maxhp",lvl,"Wind");
            stance.AddModifier("Max energy", 10, "Stance " + stance.Description + " Level " + lvl, "max energy", lvl, "Air");
            stance.AddModifier("Max stamina", 1, "Stance " + stance.Description + " Level " + lvl, "max stamina", lvl, "Air");
            stance.AddModifier("Attack Bonus", 2, "Stance " + stance.Description + " Level " + lvl, "attackbonus", lvl, "Air");
            stance.AddModifier("Luck", -1, stance.Id, "luck", lvl, "Air");

            stance.AddModifier("Luck", -1, stance.ObjId, "luck", 2, "Air");

            return stance;
        }
    }
}
