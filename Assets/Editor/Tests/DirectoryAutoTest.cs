using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class DirectoryAutoTest : MonoBehaviour
{
    string  testKey   = "8-999-66-55-404";
    Citizen testValue = new Citizen("Test Testov", "Street Test");

    DirectoryData dictionary;

    ReadOnlyDictionary<string, Citizen> Data => dictionary.Data;

    [Test]
    public void AddElementTest()
    {
        InitDictionary();
        AddElementIfKeyNotFound();
        if (Data.ContainsKey(testKey))
        {
            var citizen = Data[testKey];
            Assert.IsTrue(citizen.name == testValue.name);
            Assert.IsTrue(citizen.address == testValue.address);
        }
    }

    [Test]
    public void SearchByNumberTest()
    {
        AddElementIfKeyNotFound();
        Assert.IsTrue(dictionary.SearchByNumber(testKey) != null);

    }

    [Test]
    public void SearchByNameTest()
    {
        AddElementIfKeyNotFound();
        Assert.IsTrue(dictionary.SearchByName(testValue.name) != null);
    }

    [Test]
    public void RemoveElementTest()
    {
        AddElementIfKeyNotFound();
        dictionary.RemoveCitizenData(testKey);
        Assert.IsTrue(!Data.ContainsKey(testKey));
    }

    void InitDictionary()
    {
        if (dictionary == null)
            dictionary = new DirectoryData(false);
    }

    void AddElementIfKeyNotFound()
    {
        if (!Data.ContainsKey(testKey))
            dictionary.AddCitizenData(testKey, testValue);
    }

}
