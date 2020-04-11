using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MM.Systems.InventorySystem;

namespace MM.Systems.CraftingSystem
{
    public class CraftingSystem
    {


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
        /// Returns the items left, null if crafting is not possible
        /// </summary>
        /// <returns></returns>
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

            // Return result
            return _outItems;
        }
        /// <summary>
        /// Returns the items left, null if crafting is not possible
        /// </summary>
        /// <returns></returns>
        public static ItemData[][] TryCrafting(CraftingRecipe _craftingRecipe, params ItemData[][] _inItems)
        {
            // Get recipe values
            //ItemData[][] _outItems = new ItemData[_inItems.Length][];
            ItemData[][] _outItems = _inItems.Select(s => s.ToArray()).ToArray();
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
            //Dictionary<ItemPreset, int>[] _inPresetAmountDictionary = new Dictionary<ItemPreset, int>[_inItems.Length];
            //for (int i = 0; i < _inItems.Length; i++)
            //    for (int j = 0; j < _inItems[i].Length; j++)
            //    {
            //        ItemData _data = _inItems[i][j];
            //        if (_data == null)
            //            continue;
            //        if (_inPresetAmountDictionary[i] == null)
            //            _inPresetAmountDictionary[i] = new Dictionary<ItemPreset, int>();

            //        // Fill Dictionary
            //        if (_inPresetAmountDictionary[i].ContainsKey(_data.itemPreset))
            //            _inPresetAmountDictionary[i][_data.itemPreset] = _data.itemAmount;
            //        else
            //        {
            //            Dictionary<ItemPreset, int> _dict = _inPresetAmountDictionary[i];
            //            _dict.Add(_data.itemPreset, _data.itemAmount);
            //            _inPresetAmountDictionary[i] = _dict;
            //        }
            //    }
            //foreach (ItemData[] _datas in _inItems)
            //{
            //    foreach (ItemData _data in _datas)
            //    {
            //        // Fill Dictionary
            //        if (_inPresetAmountDictionary.ContainsKey(_data.itemPreset))
            //            _inPresetAmountDictionary[_data.itemPreset] = _data.itemAmount;
            //        else
            //            _inPresetAmountDictionary.Add(_data.itemPreset, _data.itemAmount);
            //    }
            //}

            // Test if requirements are met
            //foreach (KeyValuePair<ItemPreset, int> _element in _requiredPresetAmountDictionary)
            //{
            //    for (int i = 0; i < _inItems.Length; i++)
            //    {
            //        if (_inPresetAmountDictionary[i] == null)
            //            _inPresetAmountDictionary[i] = new Dictionary<ItemPreset, int>();

            //        // Test if inItems contain, if so remove the amount, check if new amount is greater zero, else cancel crafting
            //        if (_inPresetAmountDictionary[i].ContainsKey(_element.Key))
            //        {
            //            _inPresetAmountDictionary[i][_element.Key] = _inPresetAmountDictionary[i][_element.Key] - _element.Value;

            //            // Not enough of item XYZ
            //            if (_inPresetAmountDictionary[i][_element.Key] < 0)
            //            {
            //                //return null;
            //                continue;
            //            }
            //        }
            //        // No item XYZ
            //        else
            //        {
            //            //return null;
            //            continue;
            //        }
            //    }
            //}

            // Set leftItems
            //List<ItemData> _leftItems = new List<ItemData>();
            //for (int i = 0; i < _inItems.Length; i++)
            //    foreach (KeyValuePair<ItemPreset, int> _element in _inPresetAmountDictionary[i])
            //        // Add to List, if itemAmount is greater than zero
            //        if (_element.Value > 0)
            //            _leftItems.Add(new ItemData(_element.Key, _element.Value));

            // Set outItems (remove recipe input elements)
            // Iterate through all input items
            int _amt = 0;
            int _currIdx = 0;
            List<ItemData> _inItemsModified = new List<ItemData>(_craftingRecipe.inElements);   // Successfully found items get set to null, else amount decreases
            foreach (ItemData _inputData in _craftingRecipe.inElements)
            {
                foreach (ItemData[] _outDatas in _outItems)
                {
                    foreach (ItemData _outData in _outDatas)
                    {
                        // If outData is emtpy, continue
                        if (_outData == null || _outData.itemAmount <= 0 || _outData.itemPreset == null)
                            continue;

                        // Check if outData equals inputData, if so, decrease amount
                        if (_outData.itemPreset.Equals(_inputData.itemPreset))
                        {
                            // Calculate amount
                            _amt = _outData.itemAmount - _inputData.itemAmount;

                            // Set outData amount
                            _outData.itemAmount = Mathf.Clamp(_amt, 0, _outData.itemAmount);

                            // If amount less 0, the remaining should be set to modified's data
                            if (_amt < 0)
                            {
                                _inItemsModified[_currIdx].itemAmount = Mathf.Abs(_amt);
                            }
                            // Else set null
                            else
                            {
                                _inItemsModified[_currIdx] = null;
                            }
                        }
                    }
                }
                _currIdx++;
            }
            // If there are required items left, cancel crafting
            _inItemsModified.RemoveMissingElements();
            if (_inItemsModified.Count > 0)
                return null;

            // Set outItems (Add recipe output elements)
            int _total = 0;
            for (int i = 0; i < _outItems.Length; i++)
            {
                if (_outItems[i] == null)
                    _outItems[i] = new ItemData[_inItems[i].Length];

                for (int j = 0; j < _outItems[i].Length; j++)
                {
                    // If data is empty put item there
                    if (_outItems[i][j] == null || _outItems[i][j].itemAmount <= 0 || _outItems[i][j].itemPreset == null)
                    {
                        _outItems[i][j] = _craftingRecipe.outElements[_total];

                        _total++;

                        if (_total >= _craftingRecipe.outElements.Count)
                            goto End2;
                    }
                }
            }
        End2:

            //for (int i = 0; i < _leftItems.Count; i++)
            //{
            //    if (_outItems[i] == null)
            //        _outItems[i] = new ItemData[_inItems[i].Length];

            //    if (_outItems[Mathf.FloorToInt((float)i / _inItems.Length)][i % _inItems.Length] == null ||
            //        _outItems[Mathf.FloorToInt((float)i / _inItems.Length)][i % _inItems.Length].itemAmount <= 0 ||
            //        _outItems[Mathf.FloorToInt((float)i / _inItems.Length)][i % _inItems.Length].itemPreset == null)
            //        _outItems[Mathf.FloorToInt((float)i / _inItems.Length)][i % _inItems.Length] = _leftItems[i];
            //}
            //for (int i = 0; i < _leftItems.Count; i++)
            //{
            //    if (_outItems[i] == null)
            //        _outItems[i] = new ItemData[_inItems[i].Length];

            //    _outItems[Mathf.FloorToInt((float)i / _inItems.Length)][i % _inItems.Length] = _leftItems[i];
            //}
            //for (int i = 0; i < _outItems.Length; i++)
            //{
            //    // If all leftItems were set, return, else set
            //    if (i + 1 >= _leftItems.Count)
            //        break;
            //    else
            //    {
            //        Debug.Log("i: " + i + " %: " + Mathf.CeilToInt((float)i % _inItems.Length) + " / " + Mathf.CeilToInt((float)i / _inItems.Length) + " o: " + _outItems[i] + " l: " + _leftItems[i]);

            //        _outItems[Mathf.CeilToInt((float)i % _inItems.Length)][Mathf.CeilToInt((float)i / _inItems.Length)] = _leftItems[i];
            //    }
            //}
            //int _total = 0;
            //for (int i = 0; i < _outItems.Length; i++)
            //{
            //    for (int j = 0; j < _outItems[i].Length; j++)
            //    {
            //        // If all leftItems were set, return, else set
            //        if (_total + 1 >= _leftItems.Count)
            //            break;
            //        else
            //            _outItems[i][j] = _leftItems[_total];

            //        _total++;
            //    }
            //}

            // Add and stack the crafting recipe output items


            //int _total = 0;
            //    for (int a = 0; a < _craftingRecipe.outElements.Count; a++)
            //    {
            //        //Debug.Log("JUUU");  // 1x ausgeführt Check
            //        for (int i = 0; i < _outItems.Length; i++)
            //        {
            //            Debug.Log("JUUU");  // 1x ausgeführt
            //            for (int j = 0; j < _outItems[i].Length; j++)
            //            {
            //                // If all recipe outElements were set, return, else set
            //                if (_total + 1 >= _craftingRecipe.outElements.Count)
            //                    goto End;

            //                // If data is empty put item there
            //                if (_outItems[i][j].itemPreset == null)
            //                {
            //                    _outItems[i][j] = _craftingRecipe.outElements[_total];
            //                }
            //                // Else try to stack, if both presets are equal
            //                else if (_outItems[i][j].itemPreset.Equals(_craftingRecipe.outElements[_total]))
            //                {
            //                    // Try to stack
            //                    int _restAmt = _outItems[i][j].itemPreset.stackSize - _outItems[i][j].itemAmount; // How many items are able to stack ontop
            //                    // If restAmt is less than stacksize, else skip since itemData is full
            //                    if (_restAmt > 0)
            //                    {
            //                        // If restAmt is smaller than crafting recipe's amount, set amount to stacksize, decrease crafting recipe's amount, so the rest rest gets added to new slot
            //                        if (_restAmt < _craftingRecipe.outElements[_total].itemAmount)
            //                        {
            //                            _outItems[i][j].itemAmount = _outItems[i][j].itemPreset.stackSize;
            //                            _craftingRecipe.outElements[_total].itemAmount -= _restAmt;
            //                        }
            //                        // Else restAmt is larger, so add the full out amount and increase total
            //                        else
            //                        {
            //                            _outItems[i][j].itemAmount += _craftingRecipe.outElements[_total].itemAmount;

            //                            _total++;
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //End:



            // Combine leftItems and outItems
            //_outItems.AddRange(_leftItems);

            // Return result
            return _outItems;
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
