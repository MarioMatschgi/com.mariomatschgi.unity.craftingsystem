using System.Collections.Generic;
using UnityEngine;
using MM.Systems.InventorySystem;

namespace MM.Systems.CraftingSystem
{
    public class CraftingSystem
    {
        public delegate void OnCraftEvent(CraftingRecipe _recipe);
        public static OnCraftEvent craftingCallback;


        #region Callback Methodes
        /*
         *
         *  Callback Methodes
         * 
         */

        #endregion

        #region Gameplay Methodes
        /*
         *
         *  Gameplay Methodes
         *
         */

        /// <summary>
        /// Checks if crafting is possible, if so it returns an empty List, else a list of the remaining ItemDatas
        /// </summary>
        /// <param name="_craftingRecipe"></param>
        /// <param name="_inItems"></param>
        /// <returns></returns>
        public static List<ItemData> CanCraft(CraftingRecipe _craftingRecipe, params ItemData[][] _inItems)
        {
            // Get recipe values
            ItemData[][] _newInItems = new ItemData[_inItems.Length][];
            for (int i = 0; i < _inItems.Length; i++)
                for (int j = 0; j < _inItems[i].Length; j++)
                {
                    if (_newInItems[i] == null)
                        _newInItems[i] = new ItemData[_inItems[i].Length];

                    if (_inItems[i][j] == null)
                        _newInItems[i][j] = null;
                    else if (_inItems[i][j].itemPreset == null)
                        _newInItems[i][j] = null;
                    else
                        _newInItems[i][j] = new ItemData(_inItems[i][j].itemPreset, _inItems[i][j].itemAmount);
                }

            // Set outItems (remove recipe input elements)
            // Iterate through all input items
            int _currIdx = 0;
            List<ItemData> _inItemsModified = new List<ItemData>();   // Successfully found items get set to null, else amount decreases
            foreach (ItemData _data in _craftingRecipe.inElements)
                _inItemsModified.Add(new ItemData(_data.itemPreset, _data.itemAmount));

            foreach (ItemData _inputData in _craftingRecipe.inElements)
            {
                foreach (ItemData[] _newInItemArr in _newInItems)
                    foreach (ItemData _newInItem in _newInItemArr)
                    {
                        // If outData is emtpy, continue
                        if (_newInItem == null || _newInItem.itemAmount <= 0 || _newInItem.itemPreset == null)
                            continue;

                        // Check if outData equals inputData, if so, decrease amount
                        if (_newInItem.itemPreset.Equals(_inputData.itemPreset))
                        {
                            _inItemsModified[_currIdx].itemAmount -= _inputData.itemAmount;

                            // If item is empty, remove
                            if (_inItemsModified[_currIdx].itemAmount <= 0)
                            {
                                _inItemsModified[_currIdx] = null;

                                goto End0;
                            }
                        }
                    }
                End0:
                _currIdx++;
            }
            // Return left items
            _inItemsModified.RemoveMissingElements();

            return _inItemsModified;
        }

        /// <summary>
        /// Returns the items left, null if crafting is not possible
        /// </summary>
        /// <returns></returns>
        [System.Obsolete]
        public static List<ItemData> TryCrafting(CraftingRecipe _craftingRecipe, params ItemData[] _inItems)
        {
            // Get recipe values
            List<ItemData> _outItems = new List<ItemData>(_craftingRecipe.outElements);
            List<ItemData> _requiredItems = new List<ItemData>(_craftingRecipe.inElements);

            // Set requiredPresetAmountDictionary
            Dictionary<ItemPreset, int> _requiredPresetAmountDictionary = new Dictionary<ItemPreset, int>();
            foreach (ItemData _data in _requiredItems)
            {
                // Fill Dictionary
                if (_requiredPresetAmountDictionary.ContainsKey(_data.itemPreset))
                    _requiredPresetAmountDictionary[_data.itemPreset] = _data.itemAmount;
                else
                    _requiredPresetAmountDictionary.Add(_data.itemPreset, _data.itemAmount);
            }
            // Set inPresetAmountDictionary
            Dictionary<ItemPreset, int> _inPresetAmountDictionary = new Dictionary<ItemPreset, int>();
            foreach (ItemData _data in _inItems)
            {
                // Fill Dictionary
                if (_inPresetAmountDictionary.ContainsKey(_data.itemPreset))
                    _inPresetAmountDictionary[_data.itemPreset] = _data.itemAmount;
                else
                    _inPresetAmountDictionary.Add(_data.itemPreset, _data.itemAmount);
            }

            // Test if requirements are met
            foreach (KeyValuePair<ItemPreset, int> _element in _requiredPresetAmountDictionary)
            {
                // Test if inItems contain, if so remove the amount, check if new amount is greater zero, else cancel crafting
                if (_inPresetAmountDictionary.ContainsKey(_element.Key))
                {
                    _inPresetAmountDictionary[_element.Key] = _inPresetAmountDictionary[_element.Key] - _element.Value;

                    // Not enough of item XYZ
                    if (_inPresetAmountDictionary[_element.Key] < 0)
                        return null;
                }
                // No item XYZ
                else
                    return null;
            }

            // Set leftItems
            List<ItemData> _leftItems = new List<ItemData>();
            foreach (KeyValuePair<ItemPreset, int> _element in _inPresetAmountDictionary)
                // Add to List, if itemAmount is greater than zero
                if (_element.Value > 0)
                    _leftItems.Add(new ItemData(_element.Key, _element.Value));

            // Combine leftItems and outItems
            _outItems.AddRange(_leftItems);

            // Invoke Event
            if (craftingCallback != null)
                craftingCallback.Invoke(_craftingRecipe);

            // Return result
            return _outItems;
        }

        /// <summary>
        /// Trys to craft a recipe <paramref name="_craftingRecipe"/> with items <paramref name="_inItems"/> and puts eventual not in original fit items into a List <paramref name="_notFit"/>
        /// Returns the items left, null if crafting is not possible
        /// </summary>
        /// <returns>Returns the items left, null if crafting is not possible</returns>
        public static ItemData[][] TryCrafting(CraftingRecipe _craftingRecipe, out List<ItemData> _notFit, params ItemData[][] _inItems)
        {
            // Get recipe values
            ItemData[][] _newInItems = new ItemData[_inItems.Length][];
            for (int i = 0; i < _inItems.Length; i++)
                for (int j = 0; j < _inItems[i].Length; j++)
                {
                    if (_newInItems[i] == null)
                        _newInItems[i] = new ItemData[_inItems[i].Length];

                    if (_inItems[i][j] == null)
                        _newInItems[i][j] = null;
                    else if (_inItems[i][j].itemPreset == null)
                        _newInItems[i][j] = null;
                    else
                        _newInItems[i][j] = new ItemData(_inItems[i][j].itemPreset, _inItems[i][j].itemAmount);
                }

            // Set outItems (remove recipe input elements)
            // Iterate through all input items
            List<ItemData> _inItemsModified = new List<ItemData>();   // Successfully found items get set to null, else amount decreases
            foreach (ItemData _data in _craftingRecipe.inElements)
                _inItemsModified.Add(new ItemData(_data.itemPreset, _data.itemAmount));

            int _currIdx = 0;
            foreach (ItemData _inputData in _craftingRecipe.inElements)
            {
                foreach (ItemData[] _outDatas in _newInItems)
                    foreach (ItemData _outData in _outDatas)
                    {
                        // If outData is emtpy, continue
                        if (_outData == null || _outData.itemAmount <= 0 || _outData.itemPreset == null)
                            continue;

                        // Check if outData equals inputData, if so, decrease amount
                        if (_outData.itemPreset.Equals(_inputData.itemPreset))
                        {
                            int _tmp = _inItemsModified[_currIdx].itemAmount;
                            _inItemsModified[_currIdx].itemAmount -= _outData.itemAmount;
                            _outData.itemAmount = Mathf.Clamp(_outData.itemAmount - _tmp, 0, _outData.itemAmount);

                            // If item is empty, remove
                            if (_inItemsModified[_currIdx].itemAmount <= 0)
                            {
                                _inItemsModified[_currIdx] = null;

                                goto End0;
                            }
                        }
                    }
                End0:
                _currIdx++;
            }
            // If there are required items left, cancel crafting
            _inItemsModified.RemoveMissingElements();
            if (_inItemsModified.Count > 0)
            {
                _notFit = new List<ItemData>();

                return null;
            }

            // Set outItems (Add recipe output elements)
            List<ItemData> _outItemsModified = new List<ItemData>();   // Successfully found items get set to null, else amount decreases
            for (int i = 0; i < _craftingRecipe.outElements.Count; i++)
                _outItemsModified.Add(new ItemData(_craftingRecipe.outElements[i].itemPreset, _craftingRecipe.outElements[i].itemAmount));

            // Try to stack
            for (int _total = 0; _total < _craftingRecipe.outElements.Count; _total++)
            {
                for (int i = 0; i < _newInItems.Length; i++)
                {
                    if (_newInItems[i] == null)
                        _newInItems[i] = new ItemData[_inItems[i].Length];

                    for (int j = 0; j < _newInItems[i].Length; j++)
                    {
                        if (_newInItems[i][j] == null || _newInItems[i][j].itemPreset == null || _outItemsModified[_total] == null)
                            continue;

                        // Else check if outItem equals modified
                        if (_newInItems[i][j].itemPreset.Equals(_outItemsModified[_total].itemPreset))
                        {
                            // Try to stack
                            int _restAmt = _newInItems[i][j].itemPreset.stackSize - _newInItems[i][j].itemAmount; // How many items are able to stack ontop
                            // If restAmt is less than stacksize, else skip since itemData is full
                            if (_restAmt > 0)
                            {
                                // If restAmt is smaller than crafting recipe's amount, set amount to stacksize, decrease crafting recipe's amount, so the rest rest gets added to new slot
                                if (_restAmt <= _outItemsModified[_total].itemAmount)
                                {
                                    _newInItems[i][j].itemAmount = _newInItems[i][j].itemPreset.stackSize;
                                    _outItemsModified[_total].itemAmount -= _restAmt;

                                    if (_outItemsModified[_total].itemAmount <= 0)
                                        _outItemsModified[_total] = null;
                                }
                                // Else restAmt is larger, so add the full out amount and increase total
                                else
                                {
                                    _newInItems[i][j].itemAmount += _outItemsModified[_total].itemAmount;
                                    _outItemsModified[_total] = null;

                                    _total++;
                                }
                            }
                        }
                        if (_total >= _craftingRecipe.outElements.Count)
                            goto End1;
                    }
                }
            End1:
                continue;
            }

            // Try to add the items, that were not able to stack
            _outItemsModified.RemoveMissingElements();
            for (int _total = 0; _total < _craftingRecipe.outElements.Count; _total++)
            {
                for (int i = 0; i < _newInItems.Length; i++)
                {
                    if (_newInItems[i] == null)
                        _newInItems[i] = new ItemData[_inItems[i].Length];

                    for (int j = 0; j < _newInItems[i].Length; j++)
                    {
                        // If data is empty put item there
                        if (_newInItems[i][j] == null || _newInItems[i][j].itemAmount <= 0 || _newInItems[i][j].itemPreset == null)
                        {
                            _newInItems[i][j] = _outItemsModified[_total];
                            _outItemsModified[_total] = null;

                            _total++;
                        }
                        if (_total >= _outItemsModified.Count)
                            goto End2;
                    }
                }
            End2:
                continue;
            }

            // Put the still remaining items in notFit
            _outItemsModified.RemoveMissingElements();
            _notFit = new List<ItemData>();
            foreach (ItemData _data in _outItemsModified)
                _notFit.Add(new ItemData(_data.itemPreset, _data.itemAmount));

            // Invoke Event
            if (craftingCallback != null)
                craftingCallback.Invoke(_craftingRecipe);

            // Return result
            return _newInItems;
        }

        #endregion

        #region Helper Methodes
        /*
         *
         *  Helper Methodes
         * 
         */

        #endregion
    }
}
