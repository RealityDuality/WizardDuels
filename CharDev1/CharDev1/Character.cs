using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CharDev1
{
    public class Character
    {
        private long CharacterId = 0;
        private float _strength = 10;
        private float _dex = 10;
        private float _int = 10;
        private float _wisdom = 10;
        private float _charisma = 10;
        private float _level = 1;
        private float _ac;
        private float _fortitute = 0;
        private float _reflexes = 0;
        private float _willpower = 0;
        private float _initiative = 0;
        private float _const = 0;
        private long _xp = 0;
        private long _xpNeedToLv = 100;
        private float _maxEnergy = 0;
        private float _maxStamina = 0;

        private float _luck = 0;

        public CharacterClass SelectedClass = null;
        public Stance CurrentStance = null;
        private List<Stance> AllStances = new List<Stance>();

        public void AddStance(Stance stance)
        {
            if (!AllStances.Contains(stance))
                AllStances.Add(stance);

            if (AllStances.Count == 1)
            {
                CurrentStance = stance;
            }
        }

        public float Level
        {
            set { _level = value; }
            get { return _level; }
        }

        public float Strength
        {
            set
            {
                _strength = GetAttributeValue("strength", ref _strength, 10);
            }
            get { return _strength; }

        }

        public float MaxEnergy
        {
            set
            {
                _maxEnergy = GetAttributeValue("max energy", ref _maxEnergy, 10);
            }
            get { return _maxEnergy.Truncate(); }

        }

        public float MaxStamina
        {
            set
            {
                _maxStamina = GetAttributeValue("max stamina", ref _maxStamina, 10);
            }
            get { return _maxEnergy.Truncate(); }

        }


        public float Dex
        {
            set { _dex = GetAttributeValue("dex", ref _dex, 10); }
            get { return _dex; }
        }

        public float Int
        {
            set { _int = GetAttributeValue("int", ref _int, 10); }
            get { return _int; }
        }

        public float Wisdom
        {
            set { _wisdom = GetAttributeValue("wisdom", ref _wisdom, 10); }
            get { return _wisdom; }
        }

        public float Charisma
        {
            set { _charisma = GetAttributeValue("charisma", ref _charisma, 10); }
            get { return _charisma; }
        }

        public float Luck
        {
            set { _luck = GetAttributeValue("luck", ref _luck, 10); }
            get { return _luck; }
        }

        public float AC
        {
            set
            {
                _ac = 10 + (_level / 2);
                _ac = GetAttributeValue("ac", ref _ac, _ac);
            }
            get { return _ac.Truncate(); }
        }

        public float Fortitude
        {
            set
            {
                _fortitute = 10 + (_level / 2);
                _fortitute = GetAttributeValue("fortitude", ref _fortitute, _fortitute);
            }
            get { return _fortitute.Truncate(); }
        }

        public float Reflexes
        {
            set
            {
                _reflexes = 10 + (_level / 2);
                _reflexes = GetAttributeValue("reflex", ref _reflexes, _reflexes);

            }
            get { return _reflexes.Truncate(); }
        }

        public float Willpower
        {
            set
            {
                _willpower = 10 + (_level / 2);
                _willpower = GetAttributeValue("willpower", ref _willpower, _willpower);
            }

            get { return _willpower.Truncate(); }
        }

        public float Initiative
        {
            set
            {
                _initiative = Dex + (_level / 2);
                _initiative = GetAttributeValue("initiative", ref _initiative, _initiative);
            }
            get { return _initiative.Truncate(); }
        }

        public float Constitution
        {
            set
            {
                _const = 10 + (_level / 2);
                _const = GetAttributeValue("constitution", ref _const, _const);
            }
            get { return _const.Truncate(); }
        }

        private float _maxHp;
        public float MaxHp
        {
            set
            {
                _maxHp = StartHp;//SelectedClass.StartBaseHp + ConstitutionMod;
                _maxHp += _level * (ConstitutionMod + 1) - 1 + SelectedClass.LevelUpHpMod;

                _maxHp += GetAttributeValue("maxhp", ref _maxHp, _maxHp);

            }
            get { return _maxHp.Truncate(); }

        }

        private float _currentHp = 0;
        public float CurrentHp
        {
            set { _currentHp = value; }
            get { return _currentHp; }
        }

        public void FillHealth()
        {
            CurrentHp = MaxHp;
        }


        public float StartHp
        {
            get
            {
                return SelectedClass.StartBaseHp + ConstitutionMod;
            }
        }

        public float StrengthMod
        {
            get { return (Strength - 10) / 2; }
        }

        public float ConstitutionMod
        {
            get { return (Constitution - 10) / 2; }
        }

        public float DexMod
        {
            get { return (Dex - 10) / 2; }
        }

        public float IntMod
        {
            get { return (Int - 10) / 2; }
        }

        public float WisdomMod
        {
            get { return (Wisdom - 10) / 2; }
        }

        public float CharismaMod
        {
            get { return (Charisma - 10) / 2; }
        }

        public float LuckMod
        {
            get { return (Luck - 10) / 2; }
        }

        private float GetAttributeValue(string modifierAttributeKey, ref float attributeValue, float baseValue)
        {
            attributeValue = baseValue;// base

            if (AllModifiers == null)
                AllModifiers = null;

            if (AllModifiers.ContainsKey(modifierAttributeKey))
            {
                AllModifiers[modifierAttributeKey].Sort((x, y) => x.SortLvl.CompareTo(y.SortLvl));
                foreach (Modifier item in AllModifiers[modifierAttributeKey])
                {
                    if (item.SortLvl == 1) // + value
                    {
                        attributeValue += item.Value;
                    }
                    else if (item.SortLvl == 2) // percentage value
                    {
                        attributeValue += (attributeValue * (item.Value / 100));
                    }
                    else if (item.SortLvl == 3)
                    {
                        attributeValue -= (attributeValue * (item.Value / 100));
                    }
                }
            }
            return attributeValue;
        }
        private float _attackBonus;
        public float AttackBonus
        {
            set
            {
                _attackBonus = CurrentStance.Proficiency;
                if (CurrentStance.ProficiencyType == "strength")
                    _attackBonus += StrengthMod;
                else if (CurrentStance.ProficiencyType == "int")
                    _attackBonus += IntMod;
                else if (CurrentStance.ProficiencyType == "dex")
                    _attackBonus += DexMod;
                else if (CurrentStance.ProficiencyType == "wisdom")
                    _attackBonus += WisdomMod;

                _attackBonus = GetAttributeValue("attackbonus", ref _attackBonus, _attackBonus);

            }
            get { return _attackBonus; }
        }

        public float GetChanceToHit(Character other)
        {
            float attbonus = AttackBonus;//CurrentStance.Proficiency;

            //if (CurrentStance.ProficiencyType == "strength")
            //    attbonus += StrengthMod;
            //else if (CurrentStance.ProficiencyType == "int")
            //    attbonus += IntMod;
            //else if (CurrentStance.ProficiencyType == "dex")
            //    attbonus += DexMod;
            //else if (CurrentStance.ProficiencyType == "wisdom")
            //    attbonus += WisdomMod;

            return (attbonus - other.AC + 21) * 5;

            //CurrentStance.Proficiency 
        }

        public void RefreshAllData()
        {
            AllModifiers = null;

            Strength = 0;
            Dex = 0;
            Int = 0;
            Charisma = 0;
            Luck = 0;
            Willpower = 0;
            Wisdom = 0;
            Reflexes = 0;
            AttackBonus = 0;
            Fortitude = 0;
            Constitution = 0;

            MaxEnergy = 0;
            MaxStamina = 0;
            AC = 0;
            MaxHp = 0;

            Initiative = 0;
        }
        private string _characterName = "";
        public string CharacterName
        {
            set { _characterName = value; }
            get { return _characterName; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Character Name: \t" + CharacterName);
            sb.AppendLine("Level: " + _level);
            sb.AppendLine("Max Hp: \t" + MaxHp.ToString());
            sb.AppendLine("Max Energy: \t" + MaxEnergy.ToString("N0"));
            sb.AppendLine("Max Stamina: \t" + MaxStamina.ToString("N0"));
            sb.AppendLine("Strength: \t" + Strength.ToString("N0") + "\t\t Mod: " + StrengthMod.ToString("N0"));
            sb.AppendLine("Dex: \t\t" + Dex.ToString("N0") + "\t\t Mod: " + DexMod.ToString("N0"));
            sb.AppendLine("Const: \t\t" + Constitution.ToString("N0") + "\t\t Mod: " + ConstitutionMod.ToString("N0"));
            sb.AppendLine("Int: \t\t" + Int.ToString("N0") + "\t\t Mod: " + IntMod.ToString("N0"));
            sb.AppendLine("Wisdom: \t" + Wisdom.ToString("N0") + "\t\t Mod: " + WisdomMod.ToString("N0"));
            sb.AppendLine("Charisma: \t" + Charisma.ToString("N0") + "\t\t Mod: " + CharismaMod.ToString("N0"));
            sb.AppendLine("Luck: \t\t" + Luck.ToString("N0") + "\t\t Mod: " + LuckMod.ToString("N0"));
            sb.AppendLine("");
            sb.AppendLine("Attack Bonus: \t" + AttackBonus.ToString("N0"));
            sb.AppendLine("AC: \t\t" + AC.ToString("N0"));
            sb.AppendLine("Fortitude: \t" + Fortitude.ToString("N0"));
            sb.AppendLine("Reflexes: \t" + Reflexes.ToString("N0"));
            sb.AppendLine("Willpower: \t" + Willpower.ToString("N0"));
            sb.AppendLine("");
            sb.AppendLine("Initiative: \t" + Initiative.ToString("N0"));
            sb.AppendLine("");
            sb.AppendLine("Current Stance: \t" + CurrentStance.Level + " " + CurrentStance.Description);
            sb.AppendLine("");
            sb.AppendLine("XP: \t\t" + _xp);
            sb.AppendLine("Xp To level up: \t" + _xpNeedToLv);
            sb.AppendLine("");

            foreach (string itemKey in AllModifiers.Keys)
            {
                foreach (Modifier itemModifier in AllModifiers[itemKey])
                {
                    sb.AppendLine(itemModifier.ToString());
                }

            }

            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");
            sb.AppendLine("");

            return sb.ToString();
        }

        //private float GetResistanceValue(string resistenceKeyValue, out float resistValue)
        //{
        //    resistValue = 0;

        //    if (AllResistences.ContainsKey(resistenceKeyValue))
        //    {
        //        AllResistences[resistenceKeyValue].Sort((x, y) => x.SortLvl.CompareTo(y.SortLvl));
        //        foreach (Resistence item in AllResistences[resistenceKeyValue])
        //        {
        //            if (item.SortLvl == 1) // + value
        //            {
        //                resistValue += item.Value;
        //            }
        //            else if (item.SortLvl == 2) // percentage value
        //            {
        //                resistValue += (resistValue * (item.Value / 100));
        //            }
        //            else if (item.SortLvl == 3)
        //            {
        //                resistValue -= (resistValue * (item.Value / 100));
        //            }
        //        }
        //    }

        //    return resistValue;
        //}

        //private Dictionary<string, List<Resistence>> _resistenceList; // key is the type of resistence
        //private Dictionary<string, List<Resistence>> _tempResistenceList;
        //private Dictionary<string, List<Resistence>> _allResistenceList;

        private Dictionary<string, List<Modifier>> _modifiers = new Dictionary<string, List<Modifier>>();
        private Dictionary<string, List<Modifier>> _tempModifiers = new Dictionary<string, List<Modifier>>();
        private Dictionary<string, List<Modifier>> _allModifiers;

        public void AddModifier(Modifier modifier)
        {
            if (_modifiers == null)
                _modifiers = new Dictionary<string, List<Modifier>>();

            if (!_modifiers.ContainsKey(modifier.ValueType))
                _modifiers.Add(modifier.ValueType, new List<Modifier>() { modifier });
            else
                _modifiers[modifier.ValueType].Add(modifier);
        }

        public void AddTempModifier(Modifier modifier)
        {
            if (_tempModifiers == null)
                _tempModifiers = new Dictionary<string, List<Modifier>>();

            if (!_tempModifiers.ContainsKey(modifier.ValueType))
                _tempModifiers.Add(modifier.ValueType, new List<Modifier>() { modifier });
            else
                _tempModifiers[modifier.ValueType].Add(modifier);
        }

        public void AddTempModifier(string description, float value, string parentId, string valueType, int minModifierLv, string idPrefix, int sortLevel = 1)
        {
            Modifier m = new Modifier(description, 0, value, valueType, parentId, sortLevel, minModifierLv, idPrefix);
            AddTempModifier(m);
        }

        public void AddModifier(string description, float value, string parentId, string valueType, int minModifierLv, string idPrefix, int sortLevel = 1)
        {
            Modifier m = new Modifier(description, 0, value, valueType, parentId, sortLevel, minModifierLv, idPrefix);
            AddModifier(m);
        }

        public Dictionary<string, List<Modifier>> AllModifiers
        {
            set
            {
                _allModifiers = new Dictionary<string, List<Modifier>>();

                foreach (string item in _modifiers.Keys)
                {
                    if (!_allModifiers.ContainsKey(item))
                        _allModifiers.Add(item, _modifiers[item]);
                    else
                        _allModifiers[item].AddRange(_modifiers[item]);
                }

                foreach (string item in _tempModifiers.Keys)
                {
                    if (!_allModifiers.ContainsKey(item))
                        _allModifiers.Add(item, _tempModifiers[item]);
                    else
                        _allModifiers[item].AddRange(_tempModifiers[item]);
                }

                if (CurrentStance != null)
                {
                    foreach (string item in CurrentStance.Modifiers.Keys)
                    {
                        if (!AllModifiers.ContainsKey(item))
                            _allModifiers.Add(item, CurrentStance.Modifiers[item]);
                        else
                            _allModifiers[item].AddRange(CurrentStance.Modifiers[item]);
                    }
                }

                if (SelectedClass != null)
                {
                    foreach (string item in SelectedClass.Modifiers.Keys)
                    {
                        if (!AllModifiers.ContainsKey(item))
                            _allModifiers.Add(item, CurrentStance.Modifiers[item]);
                        else
                            _allModifiers[item].AddRange(CurrentStance.Modifiers[item]);
                    }
                }
            }

            get { return _allModifiers; }
        }

        //    public Dictionary<string, List<Resistence>> AllResistences
        //    {
        //        set
        //        {
        //            _allResistenceList = new Dictionary<string, List<Resistence>>();

        //            foreach (string item in _resistenceList.Keys)
        //            {
        //                if (!_allResistenceList.ContainsKey(item))
        //                    _allResistenceList.Add(item, _resistenceList[item]);
        //                else
        //                    _allResistenceList[item].AddRange(_resistenceList[item]);
        //            }

        //            foreach (string item in _tempResistenceList.Keys)
        //            {
        //                if (!_allResistenceList.ContainsKey(item))
        //                    _allResistenceList.Add(item, _tempResistenceList[item]);
        //                else
        //                    _allResistenceList[item].AddRange(_tempResistenceList[item]);
        //            }
        //        }
        //        get { return _allResistenceList; }
        //    }

    }

    //public class Resistence
    //{
    //    public string Description = "";
    //    public string Type = "";// Fire, Water,Physical
    //    public float Value = 0;
    //    public int SortLvl;// modifier list are sorted base on 1 for + values, 2 for percentage values and 3 for reverved values. 
    //    public float Duration = 0; // -1 does not expire. 0 values will be removed from lists
    //    public string ParentID; // what applied the modifier
    //}

    public class Modifier
    {
        private string _objId = "";
        public string ObjId
        {
            set
            {
                if (_objId == "")
                    _objId = "Modifier:" + GlobalData.GetNewObjectId();
            }
            get { return _objId; }
        }

        private string _id = "";
        public string Id
        {
            set
            {
                ObjId = "";
                if (SortLvl == 1 && Value > 0)
                    _id = "+" + this.Value;
                else if (SortLvl == 1 && Value < 0)
                    _id = this.Value.ToString("N0");
                else if (SortLvl == 2)
                    _id = this.Value + "%";
                else if (SortLvl == 3)
                    _id = this.Value + "% reserved";

                _id += " " + this.Description + " " + value + " Lvl " + MinModifierLv;
            }
            get
            {
                return _id;
            }
        }
        public string Description = "";
        public float Duration = 0;
        public float Value;
        public int SortLvl;// modifier list are sorted base on 1 for + values, 2 for percentage values and 3 for reverved values. 
        public string ValueType; // what does the modifier effect. Strength, Fire Resistence
        public string ParentID; // what applied the modifier
        public int MinModifierLv = 1;// filters what modifiers can be add to an object. Filtering is based on ValueType and MinModifiierLv. So at lv 1 fire you will only see lv 1 fire modifiers.

        public override string ToString()
        {

            string r = "";

            if (SortLvl == 1 && Value > 0)
                r = "+" + this.Value;
            else if (SortLvl == 1 && Value < 0)
                r = this.Value.ToString("N0");
            else if (SortLvl == 2)
                r = this.Value + "%";
            else if (SortLvl == 3)
                r = this.Value + "% reserved";

            r += " " + this.Description + "\t\t" + ParentID + "\t\t" + Id;

            return r;
        }

        public Modifier(string description, float duration, float value, string valueType, string parentId, int sortLevel, int minModifierLv, string idPrefix)
        {
            Description = description;
            Duration = duration;
            Value = value;
            SortLvl = sortLevel;
            ValueType = valueType;
            ParentID = parentId;
            MinModifierLv = minModifierLv;
            Id = idPrefix;
        }
    }

    public class CharacterClass
    {
        private string _objId = "";
        public string ObjId
        {
            set
            {
                if (_objId == "")
                    _objId = "Class:" + GlobalData.GetNewObjectId();
            }
            get { return _objId; }
        }
        public string Description = "";
        public float StartBaseHp = 10;
        public float LevelUpHpMod = 5;
        // CharacterClass are use at level to display option to add to the character
        public Dictionary<string, List<Modifier>> Modifiers = new Dictionary<string, List<Modifier>>();

        public void AddModifier(string description, float value, string parentId, string valueType, int minModifierLv, string idPrefix, int sortLevel = 1)
        {
            Modifier m = new Modifier(description, 0, value, valueType, parentId, sortLevel, minModifierLv, idPrefix);
            AddModifier(m);
        }

        public void AddModifier(Modifier modifier)
        {
            if (Modifiers == null)
                Modifiers = new Dictionary<string, List<Modifier>>();

            if (!Modifiers.ContainsKey(modifier.ValueType))
                Modifiers.Add(modifier.ValueType, new List<Modifier>() { modifier });
            else
                Modifiers[modifier.ValueType].Add(modifier);
        }

    }

    public class Stance
    {
        private string _objId = "";
        public string ObjId
        {
            set
            {
                if (_objId == "")
                    _objId = "Stance:" + GlobalData.GetNewObjectId();
            }
            get { return _objId; }
        }

        private string _id = "";
        public string Id
        {
            set
            {                
                _id = "Stance " + this.Description + " Lvl " + this.Level;
            }
            get { return _id; }
        }
        public string Description = "";
        public Dictionary<string, List<Modifier>> Modifiers = new Dictionary<string, List<Modifier>>();
        public float Level = 1;
        public float Proficiency = 1;
        public string ProficiencyType = "strength";// one of the attributes
        //public float CurrentLevel = 1;
        public void AddModifier(string description, float value, string parentId, string valueType, int minModifierLv, string idPrefix, int sortLevel = 1)
        {
            Modifier m = new Modifier(description, 0, value, valueType, parentId, sortLevel, minModifierLv, idPrefix);
            AddModifier(m);
        }

        public void AddModifier(Modifier modifier)
        {
            if (Modifiers == null)
                Modifiers = new Dictionary<string, List<Modifier>>();

            if (!Modifiers.ContainsKey(modifier.ValueType))
                Modifiers.Add(modifier.ValueType, new List<Modifier>() { modifier });
            else
                Modifiers[modifier.ValueType].Add(modifier);
        }

        //static public Stance CreateStanceEarth()
        //{
        //    Stance stance = new Stance();
        //    stance.Level = 1;
        //    stance.Description = "Earth";
        //    stance.Modifiers = new Dictionary<string, List<Modifier>>();
        //    stance.ProficiencyType = "strength";

        //    stance.AddModifier("Strength", 2, "Stance Earth Level 1", "strength");
        //    stance.AddModifier("Dex", -2, "Stance Earth Level 1", "dex");
        //    stance.AddModifier("Int", -1, "Stance Earth Level 1", "int");
        //    stance.AddModifier("Constitution", 3, "Stance Earth Level 1", "constitution");
        //    stance.AddModifier("AC", -2, "Stance Earth Level 1", "ac");
        //    stance.AddModifier("Charisma", 1, "Stance Earth Level 1", "charisma");
        //    stance.AddModifier("Fortitude", 3, "Stance Earth Level 1", "fortitude");
        //    stance.AddModifier("Reflexes", -1, "Stance Earth Level 1", "reflex");

        //    stance.AddModifier("Willpower", -1, "Stance Earth Level 1", "willpower");
        //    stance.AddModifier("Initiative", -2, "Stance Earth Level 1", "initiative");
        //    stance.AddModifier("Fire Resistence", 1, "Stance Earth Level 1", "Fire Resistence");

        //    stance.AddModifier("Water Resistence", -1, "Stance Earth Level 1", "Water Resistence");
        //    stance.AddModifier("Earth Resistence", 2, "Stance Earth Level 1", "Earth Resistence");
        //    stance.AddModifier("Wind Resistence", -2, "Stance Earth Level 1", "Wind Resistence");
        //    stance.AddModifier("Max Hp", 10, "Stance Earth Level 1", "maxhp");
        //    // stance.AddModifier("Max energy", 0, "Stance Earth Level 1", "max energy");
        //    stance.AddModifier("Max stamina", 3, "Stance Earth Level 1", "max stamina");
        //    //stance.AddModifier("Attack Bonus", 0, "Stance Earth Level 1", "attackbonus");

        //    return stance;
        //}

        //static public Stance CreateStanceFire()
        //{
        //    Stance stance = new Stance();
        //    stance.Level = 1;
        //    stance.Description = "Fire";
        //    stance.Modifiers = new Dictionary<string, List<Modifier>>();
        //    stance.ProficiencyType = "dex";

        //    stance.AddModifier("Strength", -2, "Stance "+ stance.Description + " Level 1", "strength");
        //    stance.AddModifier("Dex", 3, "Stance "+ stance.Description + " Level 1", "dex");
        //    //stance.AddModifier("Int", -1, "Stance Earth Level 1", "int");
        //    stance.AddModifier("Wisdom", -2, "Stance "+ stance.Description + " Level 1", "wisdom");
        //    stance.AddModifier("Constitution", -2, "Stance "+ stance.Description + " Level 1", "constitution");
        //    stance.AddModifier("AC", -1, "Stance "+ stance.Description + " Level 1", "ac");
        //    stance.AddModifier("Charisma", 1, "Stance "+ stance.Description + " Level 1", "charisma");
        //    stance.AddModifier("Fortitude", -2, "Stance "+ stance.Description + " Level 1", "fortitude");
        //    stance.AddModifier("Reflexes", 1, "Stance "+ stance.Description + " Level 1", "reflex");

        //    stance.AddModifier("Willpower", 1, "Stance "+ stance.Description + " Level 1", "willpower");
        //    stance.AddModifier("Initiative", 1, "Stance "+ stance.Description + " Level 1", "initiative");
        //    stance.AddModifier("Fire Resistence", 2, "Stance "+ stance.Description + " Level 1", "Fire Resistence");

        //    stance.AddModifier("Water Resistence", -2, "Stance "+ stance.Description + " Level 1", "Water Resistence");
        //    stance.AddModifier("Earth Resistence", -1, "Stance "+ stance.Description + " Level 1", "Earth Resistence");
        //    stance.AddModifier("Wind Resistence", 1, "Stance "+ stance.Description + " Level 1", "Wind Resistence");
        //    stance.AddModifier("Max Hp", 2, "Stance "+ stance.Description + " Level 1", "maxhp");
        //     stance.AddModifier("Max energy", 4, "Stance "+stance.Description+" Level 1", "max energy");
        //    stance.AddModifier("Max stamina", 6, "Stance "+ stance.Description + " Level 1", "max stamina");
        //    stance.AddModifier("Attack Bonus", 1, "Stance "+stance.Description+" Level 1", "attackbonus");
        //    stance.AddModifier("Luck", 1, "Stance " + stance.Description + " Level 1", "luck");

        //    return stance;
        //}

        //static public Stance CreateStanceWater()
        //{
        //    Stance stance = new Stance();
        //    stance.Level = 1;
        //    stance.Description = "Water";
        //    stance.Modifiers = new Dictionary<string, List<Modifier>>();
        //    stance.ProficiencyType = "wisdom";

        //    stance.AddModifier("Strength", -2, "Stance " + stance.Description + " Level 1", "strength");
        //    //stance.AddModifier("Dex", 1, "Stance " + stance.Description + " Level 1", "dex");
        //    stance.AddModifier("Int", 1, "Stance Earth Level 1", "int");
        //    stance.AddModifier("Wisdom", 2, "Stance " + stance.Description + " Level 1", "wisdom");
        //   // stance.AddModifier("Constitution", -2, "Stance " + stance.Description + " Level 1", "constitution");
        //    stance.AddModifier("AC", 1, "Stance " + stance.Description + " Level 1", "ac");
        //    stance.AddModifier("Charisma", -2, "Stance " + stance.Description + " Level 1", "charisma");
        //    stance.AddModifier("Fortitude", -1, "Stance " + stance.Description + " Level 1", "fortitude");
        //    stance.AddModifier("Reflexes", 2, "Stance " + stance.Description + " Level 1", "reflex");

        //    //stance.AddModifier("Willpower", 1, "Stance " + stance.Description + " Level 1", "willpower");
        //    stance.AddModifier("Initiative", -1, "Stance " + stance.Description + " Level 1", "initiative");
        //    stance.AddModifier("Fire Resistence", 1, "Stance " + stance.Description + " Level 1", "Fire Resistence");

        //    stance.AddModifier("Water Resistence", 2, "Stance " + stance.Description + " Level 1", "Water Resistence");
        //    stance.AddModifier("Earth Resistence", -2, "Stance " + stance.Description + " Level 1", "Earth Resistence");
        //    stance.AddModifier("Wind Resistence", -1, "Stance " + stance.Description + " Level 1", "Wind Resistence");
        //    stance.AddModifier("Max Hp", 5, "Stance " + stance.Description + " Level 1", "maxhp");
        //    stance.AddModifier("Max energy", 2, "Stance " + stance.Description + " Level 1", "max energy");
        //    stance.AddModifier("Max stamina", 5, "Stance " + stance.Description + " Level 1", "max stamina");
        //    stance.AddModifier("Attack Bonus", 1, "Stance " + stance.Description + " Level 1", "attackbonus");
        //   // stance.AddModifier("Luck", 1, "Stance " + stance.Description + " Level 1", "luck");

        //    return stance;
        //}

        //static public Stance CreateStanceAir()
        //{
        //    Stance stance = new Stance();
        //    stance.Level = 1;
        //    stance.Description = "Air";
        //    stance.Modifiers = new Dictionary<string, List<Modifier>>();
        //    stance.ProficiencyType = "int";

        //    stance.AddModifier("Strength", -2, "Stance " + stance.Description + " Level 1", "strength");
        //    stance.AddModifier("Dex", 1, "Stance " + stance.Description + " Level 1", "dex");
        //    stance.AddModifier("Int", 2, "Stance Earth Level 1", "int");
        //  //  stance.AddModifier("Wisdom", 2, "Stance " + stance.Description + " Level 1", "wisdom");
        //    stance.AddModifier("Constitution", -3, "Stance " + stance.Description + " Level 1", "constitution");
        //    stance.AddModifier("AC", 2, "Stance " + stance.Description + " Level 1", "ac");
        //    stance.AddModifier("Charisma", -2, "Stance " + stance.Description + " Level 1", "charisma");
        //    stance.AddModifier("Fortitude", -1, "Stance " + stance.Description + " Level 1", "fortitude");
        //    stance.AddModifier("Reflexes", 1, "Stance " + stance.Description + " Level 1", "reflex");

        //    stance.AddModifier("Willpower", 1, "Stance " + stance.Description + " Level 1", "willpower");
        //    stance.AddModifier("Initiative", -1, "Stance " + stance.Description + " Level 1", "initiative");
        //    stance.AddModifier("Fire Resistence", 1, "Stance " + stance.Description + " Level 1", "Fire Resistence");

        //    stance.AddModifier("Water Resistence", -1, "Stance " + stance.Description + " Level 1", "Water Resistence");
        //    stance.AddModifier("Earth Resistence", -2, "Stance " + stance.Description + " Level 1", "Earth Resistence");
        //    stance.AddModifier("Wind Resistence", 2, "Stance " + stance.Description + " Level 1", "Wind Resistence");
        //   // stance.AddModifier("Max Hp", 5, "Stance " + stance.Description + " Level 1", "maxhp");
        //    stance.AddModifier("Max energy", 10, "Stance " + stance.Description + " Level 1", "max energy");
        //    stance.AddModifier("Max stamina", 1, "Stance " + stance.Description + " Level 1", "max stamina");
        //    stance.AddModifier("Attack Bonus", 2, "Stance " + stance.Description + " Level 1", "attackbonus");
        //     stance.AddModifier("Luck", -1, "Stance " + stance.Description + " Level 1", "luck");

        //    return stance;
        //}
    }
}
