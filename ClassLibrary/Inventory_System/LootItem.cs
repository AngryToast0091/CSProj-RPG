using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Inventory_System
{
    public class LootItem
    {
        #region Properties
        //----------------------------------- Properties -----------------------------------
        public string Name { get; private set; }
        public int Amount { get; set; } = 1;
        public int DefaultWeight { get; private set; } //default drop rate
        public int CurrentWeight { get; private set; } //current drop rate => default + bad luck protection

        #endregion


        #region Functions
        //----------------------------------- Functions -----------------------------------
        public void IncreaseWeight(int amount)
        {
            CurrentWeight += amount;
        }

        public void IncreaseWight()
        {
            CurrentWeight++;
        }

        public void ResetWeight()
        {
            CurrentWeight = DefaultWeight;
        }

        public override string ToString()
        {
            return $"{Name},{Amount},{DefaultWeight}";
        }

        #endregion

        #region Constructors
        //----------------------------------- Constructors -----------------------------------
        public LootItem(string name, int defaultWeight, int amount)
        {
            Name = name;
            DefaultWeight = defaultWeight;
            Amount = amount;
        }
        public LootItem(string name, int defaultWeight)
        {
            Name = name;
            DefaultWeight = defaultWeight;
            CurrentWeight = defaultWeight;
        }

        public LootItem(string name)
        {
            Name = name;
        }

        #endregion
    }
}
